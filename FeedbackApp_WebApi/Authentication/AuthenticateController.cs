using FeedbackApp.Core.Contracts.Persistence;
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

namespace FeedbackApp.WebApi.Authentication
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
        private readonly string msgUserNotFound = "Benutzer wurde nicht gefunden!";
        private readonly string msgUserPwChangeSuccess = "Das Passwort wurde erfolgreich geändert.";
        private readonly string msgUserPwNotCorrect = "Das Passwort ist nicht korrekt.";

        public AuthenticateController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get a login token
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Token and additional information</returns>
        /// <response code="200">Returns the generated token with additional information</response>
        /// <response code="401">If the login data is incorrect</response>
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
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
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

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,

                    //Klartext für Entwicklungszwecke Xamarin!!!
                    identityId = user.Id,
                    role = userRoles.FirstOrDefault(),
                    username = user.UserName,
                    email = user.Email
                }) ;
            }
            return Unauthorized();
        }

        /// <summary>
        /// register a student
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Student account sucessfully created</response>
        /// <response code="500">Somethin went wrong in creation process</response>
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

            //if (!await roleManager.RoleExistsAsync(UserRoles.admin))
            //    await roleManager.CreateAsync(new IdentityRole(UserRoles.admin));
            //if (!await roleManager.RoleExistsAsync(UserRoles.pupil))
            //    await roleManager.CreateAsync(new IdentityRole(UserRoles.pupil));
            //if (!await roleManager.RoleExistsAsync(UserRoles.teacher))
            //    await roleManager.CreateAsync(new IdentityRole(UserRoles.teacher));

            await InsertRolesIfNotExists();

            if (await roleManager.RoleExistsAsync(UserRoles.pupil))
            {
                await userManager.AddToRoleAsync(user, UserRoles.pupil);
            }
            
            await _unitOfWork.StudentRepository.CreateStudentAsync(user.Id);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new Response { Status = "Success", Message = msgCreateUserSuccess });
        }

        /// <summary>
        /// register a teacher
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Teacher account sucessfully created</response>
        /// <response code="500">Somethin went wrong in account creation</response>
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

            //if (!await roleManager.RoleExistsAsync(UserRoles.admin))
            //    await roleManager.CreateAsync(new IdentityRole(UserRoles.admin));
            //if (!await roleManager.RoleExistsAsync(UserRoles.pupil))
            //    await roleManager.CreateAsync(new IdentityRole(UserRoles.pupil));
            //if (!await roleManager.RoleExistsAsync(UserRoles.teacher))
            //    await roleManager.CreateAsync(new IdentityRole(UserRoles.teacher));

            await InsertRolesIfNotExists();

            if (await roleManager.RoleExistsAsync(UserRoles.teacher))
            {
                await userManager.AddToRoleAsync(user, UserRoles.teacher);
            }
            await _unitOfWork.TeacherRepository.CreateTeacherAsync(user.Id);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new Response { Status = "Success", Message = msgCreateUserSuccess });
        }

        /// <summary>
        /// deletes a student or teacher account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Account successfully removed</response>
        /// <response code="404">Account not found, check LoginModel Data</response>
        /// <response code="500">Something went wrong in account deletion</response>
        /// <response code="401">Incorrect token</response>
        [HttpPost]
        [Route("deleteAccount")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteUserConfirmed([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            bool isTeacher = false;

            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                if (await userManager.IsInRoleAsync(user, UserRoles.teacher))
                {
                    await userManager.RemoveFromRoleAsync(user, UserRoles.teacher);
                    isTeacher = true;
                }
                if (await userManager.IsInRoleAsync(user, UserRoles.pupil))
                {
                    await userManager.RemoveFromRoleAsync(user, UserRoles.pupil);
                }
                await userManager.DeleteAsync(user);
            }
            else
            {
                if (user == null)
                    return NotFound(new Response { Status = "Not Found", Message = msgUserNotFound });
                else
                    return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = msgDeleteUserFail }); ;
            }

            if (isTeacher)
                await _unitOfWork.TeacherRepository.DeleteTeacherByIdentityIdAsync(user.Id);
            await _unitOfWork.StudentRepository.DeleteStudentByIdentityIdAsync(user.Id);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new Response { Status = "Success", Message = msgDeleteUserSuccess });
        }

        /// <summary>
        /// change the password of a student or teacher
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Password successfully changed</response>
        /// <response code="400">Something went wrong in password change process</response>
        /// <response code="401">Incorrect Token</response>
        [HttpPost]
        [Route("changePw")]
        public async Task<IActionResult> ChangeUserPassword([FromBody] ChangePwModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);

            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                await userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
                return Ok(new Response { Status = "Success", Message = msgUserPwChangeSuccess });
            }
            else
                return BadRequest(new Response { Status = "Error", Message = msgUserPwNotCorrect});
        }

        /// <summary>
        /// change the e-mail adress of a student or teacher 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// /// <response code="200">e-mail successfully changed</response>
        /// <response code="400">Something went wrong in e-mail change process</response>
        /// <response code="401">Incorrect Token</response>
        [HttpPost]
        [Route("changeEmail")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ChangeUserEmail([FromBody] ChangeEmailModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);

            if (user != null)
            {
                string token = await userManager.GenerateChangeEmailTokenAsync(user, model.NewEmail);
                await userManager.ChangeEmailAsync(user, model.NewEmail, token);
                return Ok();
            }
            else
                return BadRequest();
        }

        /// <summary>
        /// Insert Roles if not exists
        /// </summary>
        /// <returns></returns>
        private async Task InsertRolesIfNotExists()
        {
            if (!await roleManager.RoleExistsAsync(UserRoles.admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.pupil))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.pupil));
            if (!await roleManager.RoleExistsAsync(UserRoles.teacher))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.teacher));
        }
    }
}
