using FeedbackApp.Core.Contracts.Persistence;
using FeedbackApp.Core.Model;
using FeedbackApp.WebApi.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetAllTeachingUnitsCount()
        {
            int count = await _unitOfWork.FeedbackRepository.CountAllTeachingUnitsAsync();

            return Ok();
        }
    }
}
