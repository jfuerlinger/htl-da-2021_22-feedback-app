using FeedbackApp.Core.Contracts.Persistence;
using FeedbackApp.Core.Model;
using FeedbackApp.WebApi.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [HttpPost]
        [Route("createFeedback")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateFeedback([FromBody] FeedbackModel model)
        {
            User user = await _unitOfWork.UserRepository.GetByIdAsync(model.UserId);
            TeachingUnit teachingUnit = await _unitOfWork.FeedbackRepository.GetTeachingUnitById(model.TeachingUnitId);

            Core.Model.Feedback feedback = new Core.Model.Feedback() 
            { User = user, UserId = user.Id, TeachingUnit = teachingUnit, 
                TeachingUnitId = teachingUnit.Id, Stars = model.Stars, Comment = model.Comment};

            await _unitOfWork.FeedbackRepository.AddFeedbackAsync(feedback);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("getUserTeachingUnits")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetUserTeachingUnits(int id)
        {
            List<TeachingUnit> teachingUnits = await _unitOfWork.FeedbackRepository.GetAllTeachingUnitsByUserId(id);
            Dictionary <int, string> teachingUnitsOut = new Dictionary<int, string>();

            foreach (var teachingUnit in teachingUnits)
            {
                teachingUnitsOut.Add(teachingUnit.Id, teachingUnit.Title);
            }

            return Ok(new { teachingUnits = teachingUnitsOut });
        }

        [HttpGet]
        [Route("getTeachingUnit")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetTeachingUnit(int id)
        {
            TeachingUnit teachingUnit = await _unitOfWork.FeedbackRepository.GetTeachingUnitById(id);

            return Ok(new 
            { 
                id = teachingUnit.Id,
                userId = teachingUnit.UserId,
                isPublic = teachingUnit.IsPublic,
                title = teachingUnit.Title,
                subject = teachingUnit.Subject,
                description = teachingUnit.Description,
                date = teachingUnit.Date.ToString(),
                expiryDate = teachingUnit.ExpiryDate.ToString(),
                subscriptionKey = teachingUnit.SubscriptionKey
            });
        }

        [HttpGet]
        [Route("countUserTeachingUnits")]
        public async Task<IActionResult> CountUserTeachingUnits(int id)
        {
            int teachingUcount = await _unitOfWork.FeedbackRepository.CountTeachingUnitsAsync(id);
            return Ok(new { count = teachingUcount });
        }

        [HttpGet]
        [Route("countFeedbacks")]
        public async Task<IActionResult> CountFeedbacks(int id)
        {
            int feedbackCount = await _unitOfWork.FeedbackRepository.CountFeedbacksAsync(id);
            return Ok(new { count = feedbackCount });
        }

        [HttpGet]
        [Route("getFeedbacks")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetFeedbacks(int id)
        {
            List<Core.Model.Feedback> feedbacks = await _unitOfWork.FeedbackRepository.GetAllFeedbacksByTeachingUnitId(id);
            Dictionary<string, string> feedbackDict = new Dictionary<string, string>();

            foreach (var feedback in feedbacks)
            {
                feedbackDict.Add(feedback.Id.ToString(), $"{feedback.Stars} Stars; {feedback.Comment}"); // To-Do: display UserName
            }

            return Ok(feedbackDict);
        }

        [HttpDelete]
        [Route("deleteTeachingUnit")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteTeachingUnit(int id)
        {
            await _unitOfWork.FeedbackRepository.DeleteFeedbackRange(id);
            await _unitOfWork.FeedbackRepository.DeleteTeachingUnit(id);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("deleteFeedback")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            await _unitOfWork.FeedbackRepository.DeleteFeedback(id);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}
