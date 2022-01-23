using FeedbackApp_WebApi.FeedbackDB;
using FeedbackApp_WebApi.FeedbackDB.Contracts;
using FeedbackApp_WebApi.Persistance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackApp_WebApi.Authentication
{
    [Route("api")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        private readonly string msgUserExists = "Benutzer exestiert bereits!";
        private readonly string msgCreateUserFail = "Benutzer erstellen fehlgeschlagen! Bitte Eingaben überprüfen und erneut versuchen.";
        private readonly string msgCreateUserSuccess = "Benutzer erfolgreich erstellt.";
        private readonly string msgDeleteUserFail = "Account löschen fehlgeschlagen! Bitte Eingaben überprüfen und erneut versuchen.";
        private readonly string msgDeleteUserSuccess = "Account erfolgreich gelöscht!";

        public AuthenticateController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork,IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);

            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken
                    (
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddMinutes(5),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                string role = "student";
                var roles = userRoles.ToArray();
                if (roles.Count() != 0)
                {
                    role = "teacher";
                }

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    Role = role
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.UserName);

            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response { Status = "Error", Message = msgUserExists });
            }

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };

            var result = await userManager.CreateAsync(user, model.Password);
            

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response { Status = "Error", Message = msgCreateUserFail });
            }
            await _unitOfWork.StudentRepository.CreateStudentAsync(user.Id);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new Response { Status = "Success", Message = msgCreateUserSuccess });
        }

        [HttpPost]
        [Route("register-teacher")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response { Status = "Error", Message = msgUserExists });

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response { Status = "Error", Message = msgCreateUserFail });

            if (!await roleManager.RoleExistsAsync(UserRoles.admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.pupil))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.pupil));
            if (!await roleManager.RoleExistsAsync(UserRoles.teacher))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.teacher));

            if (await roleManager.RoleExistsAsync(UserRoles.teacher))
            {
                await userManager.AddToRoleAsync(user, UserRoles.teacher);
            }
            await _unitOfWork.TeacherRepository.CreateTeacherAsync(user.Id);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new Response { Status = "Success", Message = msgCreateUserSuccess });
        }

        [HttpPost]
        [Route("deleteAccount")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteUserConfirmed([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            var rolesForUser = await userManager.GetRolesAsync(user);
            bool isTeacher = false;

            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                if (await userManager.IsInRoleAsync(user, UserRoles.teacher))
                {
                    await userManager.RemoveFromRoleAsync(user, UserRoles.teacher);
                    isTeacher = true;
                }
                await userManager.DeleteAsync(user);
            }
            else
                return BadRequest(new Response { Status="Error", Message=msgDeleteUserFail});

            if (isTeacher && user != null)
                await _unitOfWork.TeacherRepository.DeleteTeacherByIdentityIdAsync(user.Id);
            await _unitOfWork.StudentRepository.DeleteStudentByIdentityIdAsync(user.Id);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new Response { Status = "Success", Message=msgDeleteUserSuccess });
        }
    }
}
