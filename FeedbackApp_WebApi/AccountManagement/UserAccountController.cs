using FeedbackApp.Core.Contracts.Persistence;
using FeedbackApp.Core.Model;
using FeedbackApp.WebApi.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FeedbackApp.WebApi.AccountManagement
{
    /// <summary>
    /// Additional User Account Management
    /// </summary>
    [Route("api/user")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserAccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// get the additional user data
        /// </summary>
        /// <param name="model"></param>
        /// <returns>additional user data (firstname, lastname, birthdate, school)</returns>
        /// /// <response code="200">User data sucessfully sent</response>
        /// <response code="500">Somethin went wrong (DB Server)</response>
        /// <response code="401">Incorrect Token</response>
        /// <response code="404">User Data not found. Check Request model</response>
        [HttpPost]
        [Route("getData")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetUserData([FromBody] UserDataRequestModel model)
        {
            if (model.Role == UserRoles.pupil)
            {
                var user = await _unitOfWork.StudentRepository.GetByIdentityIdAsync(model.IdentityId);

                if (user == null)
                {
                    return NotFound();
                }
                return Ok(new
                {
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    birthdate = user.Birthdate,
                    school = user.School,
                });
            }
            if (model.Role == UserRoles.teacher)
            {
                var user = await _unitOfWork.TeacherRepository.GetByIdentityIdAsync(model.IdentityId);

                if (user == null)
                {
                    return NotFound();
                }
                return Ok(new
                {
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    birthdate = user.Birthdate,
                    school = user.School,
                });
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error" }); ;
        }

        /// <summary>
        /// modify the additional user data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">User data sucessfully modified</response>
        /// <response code="500">Something went wrong (DB Server)</response>
        /// <response code="401">Incorrect Token</response>
        [HttpPost]
        [Route("modifierData")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetUserData([FromBody] ModifierUserDataModel model)
        {
            if (model.Role == UserRoles.pupil)
            {
                await _unitOfWork.StudentRepository.UpdateStudentAsync
                    (model.IdentityId, model.FirstName, model.LastName, model.Birthdate, model.School);
                await _unitOfWork.SaveChangesAsync();
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error" });
        }
    }
}
