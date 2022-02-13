using FeedbackApp.Core.Contracts.Persistence;
using FeedbackApp.WebApi.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FeedbackApp.WebApi.AccountManagement
{
    [Route("api/user")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserAccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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
            return BadRequest();
        }

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
            return BadRequest();
        }
    }
}
