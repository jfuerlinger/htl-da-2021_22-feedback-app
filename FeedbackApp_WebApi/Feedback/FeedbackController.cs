using FeedbackApp.Core.Contracts.Persistence;
using FeedbackApp.Core.Model;
using FeedbackApp.WebApi.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FeedbackApp.WebApi.Feedback
{
    [Route("api/feedback")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public FeedbackController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Messages

        #endregion

        [HttpGet]
        [Route("getAllTeachingUnitsCount")]
        public async Task<IActionResult> GetAllTeachingUnitsCount()
        {
            int teachingUnitscount = await _unitOfWork.FeedbackRepository.CountAllTeachingUnitsAsync();
            return Ok(new {count = teachingUnitscount});
        }

        [HttpGet]
        [Route("getAllFeedbacksCount")]
        public async Task<IActionResult> GetAllFeedbacksCount()
        {
            int feedbacksCount = await _unitOfWork.FeedbackRepository.CountAllFeedbacksAsync();
            return Ok(new {count = feedbacksCount});
        }

        [HttpPost]
        [Route("createTeachingUnit")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateTeachingUnit([FromBody] TeachingUnitModel model)
        {
            User user = await _unitOfWork.UserRepository.GetByIdAsync(model.UserId);

            DateTime? date = null;
            DateTime? expiryDate = null;

            TeachingUnit teachingUnit = new TeachingUnit { Title = model.Title, IsPublic = model.IsPublic,
                Description = model.Description, Subject = model.Subject, SubscriptionKey = model.SubscriptionKey, 
                User = user, UserId = user.Id};

            await _unitOfWork.FeedbackRepository.AddTeachingUnitAsync(teachingUnit);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }
    }
}
