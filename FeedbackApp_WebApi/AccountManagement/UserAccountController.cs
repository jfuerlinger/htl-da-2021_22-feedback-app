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
            User user = await _unitOfWork.UserRepository.GetByIdentityIdAsync(model.IdentityId);

            if (user == null)
                return NotFound();

            return Ok(new
            {
                title = user.Title,
                firstName = user.FirstName,
                lastName = user.LastName,
                birthdate = user.Birthdate,
                school = user.School
            });
        }

        /// <summary>
        /// modify the additional user data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">User data sucessfully modified</response>
        /// <response code="500">Something went wrong (DB Server)</response>
        /// <response code="401">Incorrect Token</response>
        /// <response code="404">User Data not found. Check Request model</response>
        [HttpPost]
        [Route("modifierData")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetUserData([FromBody] ModifierUserDataModel model)
        {
            User user = await _unitOfWork.UserRepository.GetByIdentityIdAsync(model.IdentityId);

            if (user == null)
                return NotFound();

            //To-Do DB Fehler abfangen
            await _unitOfWork.UserRepository.UpdateUserAsync
                (model.IdentityId, model.Title, model.FirstName, model.LastName, model.Birthdate, model.School);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}
