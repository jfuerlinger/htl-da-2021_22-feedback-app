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
    /// <summary>
    /// Manage Teaching Units and Feedbacks
    /// </summary>
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
        private readonly string msgTeachingUnitsNotFound = "Keine Lehreinheit(en) gefunden.";
        private readonly string msgWrongStarRating = "Eine Bewertung kann nur zwischen 1-5 Sternen sein";
        #endregion

        /// <summary>
        /// get the count of all teaching units
        /// </summary>
        /// <returns>all teaching units count</returns>
        [HttpGet]
        [Route("getAllTUCount")]
        public async Task<IActionResult> GetAllTeachingUnitsCount()
        {
            int teachingUnitscount = await _unitOfWork.FeedbackRepository.CountAllTeachingUnitsAsync();
            return Ok(new {count = teachingUnitscount});
        }

        /// <summary>
        /// get the count of all feedbacks
        /// </summary>
        /// <returns>all feedbacks count</returns>
        [HttpGet]
        [Route("getAllFeedbacksCount")]
        public async Task<IActionResult> GetAllFeedbacksCount()
        {
            int feedbacksCount = await _unitOfWork.FeedbackRepository.CountAllFeedbacksAsync();
            return Ok(new {count = feedbacksCount});
        }

        /// <summary>
        /// get the user teaching units count
        /// </summary>
        /// <param name="id"></param>
        /// <returns>user teaching units count</returns>
        [HttpGet]
        [Route("countUserTU")]
        public async Task<IActionResult> CountUserTeachingUnits(int id)
        {
            int teachingUcount = await _unitOfWork.FeedbackRepository.CountTeachingUnitsAsync(id);
            return Ok(new { count = teachingUcount });
        }

        /// <summary>
        /// get the count of all feedbacks in a teaching unit
        /// </summary>
        /// <param name="id"></param>
        /// <returns>count feedbacks from teaching unit</returns>
        [HttpGet]
        [Route("countFeedbacksTU")]
        public async Task<IActionResult> CountFeedbacks(int id)
        {
            int feedbackCount = await _unitOfWork.FeedbackRepository.CountFeedbacksAsync(id);
            return Ok(new { count = feedbackCount });
        }

        /// <summary>
        /// get all teaching units from an user (token)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>list of teaching units</returns>
        /// <response code="200">Teaching Units successfully sent</response>
        /// <response code="500">Something went wrong</response>
        [HttpGet]
        [Route("getUserTU")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetUserTeachingUnits(int id)
        {
            List<TeachingUnit> teachingUnits = await _unitOfWork.FeedbackRepository.GetAllTeachingUnitsByUserId(id);
            Dictionary<int, string> teachingUnitsOut = new Dictionary<int, string>();

            foreach (var teachingUnit in teachingUnits)
            {
                teachingUnitsOut.Add(teachingUnit.Id, teachingUnit.Title);
            }

            return Ok(new { teachingUnits = teachingUnitsOut });
        }

        /// <summary>
        /// get a teaching unit by id (token)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>teaching unit</returns>
        [HttpGet]
        [Route("getTU")]
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

        /// <summary>
        /// get all feedbacks from a teaching unit (token)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>list of feedbacks</returns>
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

        /// <summary>
        /// get all public teaching units
        /// </summary>
        /// <returns>list of public teaching units</returns>
        [HttpGet]
        [Route("getAllPublicTU")]
        public async Task<IActionResult> GetAllPublicTeachingUnits()
        {
            List<TeachingUnit> teachingUnits = await _unitOfWork.FeedbackRepository.GetAllPublicTeachingUnits();
            return Ok(teachingUnits); // To-do: Daten einschränken
        }

        /// <summary>
        /// create a teaching unit (token)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createTU")]
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

        /// <summary>
        /// create a feedback for a teaching unit (token)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Feedback successfully created</response>
        /// <response code="400">Incorrect star rating</response>
        /// 
        [HttpPost]
        [Route("createFeedback")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateFeedback([FromBody] FeedbackModel model)
        {
            if (model.Stars < 1 || model.Stars > 5)
                return BadRequest(new Response { Status = "Incorrect Input", Message = msgWrongStarRating});
            
            User user = await _unitOfWork.UserRepository.GetByIdAsync(model.UserId);
            TeachingUnit teachingUnit = await _unitOfWork.FeedbackRepository.GetTeachingUnitById(model.TeachingUnitId);

            Core.Model.Feedback feedback = new Core.Model.Feedback() 
            { User = user, UserId = user.Id, TeachingUnit = teachingUnit, 
                TeachingUnitId = teachingUnit.Id, Stars = model.Stars, Comment = model.Comment};

            await _unitOfWork.FeedbackRepository.AddFeedbackAsync(feedback);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// modify a teaching unit (token)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("modifyTU")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ModifyTeachingUnit([FromBody] ModifyTuModel model)
        {
            await _unitOfWork.FeedbackRepository.ModifyTeachingUnit(model.TeachingUnitId, model.Title, model.IsPublic,
                model.Subject, model.Description, model.Date, model.ExpiryDate, model.SubscriptionKey);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// modify a feedback (token)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("modifyFeedback")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ModifyFeedback([FromBody] ModifyFeedbackModel model)
        {
            if (model.Stars < 1 || model.Stars > 5)
                return BadRequest(new Response { Status = "Incorrect Input", Message = msgWrongStarRating });

            await _unitOfWork.FeedbackRepository.ModifyFeedback(model.FeedbackId, model.Stars, model.Comment);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// delete a teaching unit (token)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteTU")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteTeachingUnit(int id)
        {
            await _unitOfWork.FeedbackRepository.DeleteTeachingUnit(id);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// delete a feedback (token)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
