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
    /// <summary>
    /// Main User Account Management
    /// </summary>
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
        private readonly string msgSomethingWentWrong = "Etwas ist schiefgelaufen.";
        private readonly string msgPwRequirements = "Das Passwort erfüllt die Mindestanforderungen nicht.";
        private readonly string msgUsernameRequirements = "Der Username erfüllt die Mindestanforderungen nicht.";

        public AuthenticateController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// get a login token
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Token and additional information</returns>
        /// <response code="201">Returns the generated token with additional information</response>
        /// <response code="401">Login data incorrect</response>
        /// <response code="500">Something went wrong (API)</response>
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

                return CreatedAtAction("", new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,

                    //Klartext für Entwicklungszwecke Xamarin!!!
                    identityId = user.Id,
                    role = userRoles.FirstOrDefault(),
                    username = user.UserName,
                    email = user.Email
                });
            }

            if (user == null)
            {
                return Unauthorized();
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = msgSomethingWentWrong });
        }

        /// <summary>
        /// register a student
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Student account sucessfully created</response>
        /// <response code="400">Student account already exists or PW, Username doesnt meet requirements.</response>
        /// <response code="500">Something went wrong (DB Server)</response>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.UserName);

            if (userExists != null)
            {
                return BadRequest(
                    new Response { Status = "Error", Message = msgUserExists });
            }

            // Username und PW Validierung
            bool isUsernameValid = AuthenticateValidations.CheckUsernameRequirements(model.UserName);
            bool isPwValid = AuthenticateValidations.CheckPwRequirements(model.Password);

            if (!isUsernameValid)
            {
                return BadRequest(
                    new Response { Status = "username", Message =  msgUsernameRequirements});
            }

            if (!isPwValid)
            {
                return BadRequest(
                    new Response { Status = "password", Message = msgPwRequirements });
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

            await InsertRolesIfNotExists();

            if (await roleManager.RoleExistsAsync(UserRoles.pupil))
            {
                await userManager.AddToRoleAsync(user, UserRoles.pupil);
            }
            
            await _unitOfWork.StudentRepository.CreateStudentAsync(user.Id);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction("",new Response { Status = "Success", Message = msgCreateUserSuccess });
        }

        /// <summary>
        /// register a teacher
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Teacher account sucessfully created</response>
        /// <response code="400">Teacher account already exists or PW, Username doesnt meet requirements.</response>
        /// <response code="500">Something went wrong (DB Server)</response>
        [HttpPost]
        [Route("register-teacher")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return BadRequest(
                    new Response { Status = "Error", Message = msgUserExists });

            // Username und PW Validierung
            bool isUsernameValid = AuthenticateValidations.CheckUsernameRequirements(model.UserName);
            bool isPwValid = AuthenticateValidations.CheckPwRequirements(model.Password);

            if (!isUsernameValid)
            {
                return BadRequest(
                    new Response { Status = "username", Message = msgUsernameRequirements });
            }

            if (!isPwValid)
            {
                return BadRequest(
                    new Response { Status = "password", Message = msgPwRequirements });
            }

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

            await InsertRolesIfNotExists();

            if (await roleManager.RoleExistsAsync(UserRoles.teacher))
            {
                await userManager.AddToRoleAsync(user, UserRoles.teacher);
            }
            await _unitOfWork.TeacherRepository.CreateTeacherAsync(user.Id);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction("", new Response { Status = "Success", Message = msgCreateUserSuccess });
        }

        /// <summary>
        /// deletes a student or teacher account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Account successfully removed</response>
        /// <response code="404">Account not found, check LoginModel Data</response>
        /// <response code="500">Something went wrong (DB Server)</response>
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
        /// <response code="500">Something went wrong (API)</response>
        /// <response code="401">Incorrect Token</response>
        /// <response code="400">Wrong Password</response>
        /// <response code="404">User not found. Check request model</response>
        [HttpPost]
        [Route("changePw")]
        public async Task<IActionResult> ChangeUserPassword([FromBody] ChangePwModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);

            //To-Do: Username und PW Validierung

            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                await userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
                return Ok(new Response { Status = "Success", Message = msgUserPwChangeSuccess });
            }
            if (user == null)
            {
                return NotFound(new Response { Status="Not Found", Message = msgUserNotFound});
            }
            if (await userManager.CheckPasswordAsync(user, model.Password) == false)
            {
                return BadRequest(
                    new Response { Status="Wrong Password", Message = msgUserPwNotCorrect});
            }
            else
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = msgSomethingWentWrong });
        }

        /// <summary>
        /// change the e-mail adress of a student or teacher 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// /// <response code="200">e-mail successfully changed</response>
        /// <response code="500">Something went wrong (API)</response>
        /// <response code="401">Incorrect Token</response>
        /// <response code="404">User not found. Check request model</response>
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
            if (user == null)
            {
                return NotFound(new Response { Status = "Not Found", Message = msgUserNotFound });
            }
            else
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = msgSomethingWentWrong });
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
