using FeedbackApp.Core.Contracts.Persistence;
using FeedbackApp.Core.Model;
using FeedbackApp.WebApi.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedbackApp.WebApi.Statistic
{
    /// <summary>
    /// Show Statistics
    /// </summary>
    [Route("api/statistic")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatisticController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        /// <summary>
        /// get global Statistics
        /// </summary>
        /// <returns>created teaching units count, created feedbacks count</returns>
        [HttpGet]
        [Route("globalStats")]
        public async Task<IActionResult> GetGlobalStats()
        {
            GlobalHistory globalHistory = await _unitOfWork.StatisticRepository.GetGlobalHistory();

            return Ok(new { CreatedTuCount = globalHistory.CreatedTeachingUnitsCount, 
                CreatedFeedbacksCount = globalHistory.CreatedFeedbacksCount });
        }
    }
}
