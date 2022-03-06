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

            return Ok(globalHistory);
        }

        [HttpGet]
        [Route("userStats")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetUserStats(int userId)
        {
            UserStatistic userStatistic = await _unitOfWork.StatisticRepository.GetUserStatistic(userId);

            if (userStatistic.CreatedTeachingUnitsCount != 0)
            {
                await _unitOfWork.StatisticRepository.UpdateAvgStarsUserStats(userId);
                await _unitOfWork.SaveChangesAsync();
                userStatistic = await _unitOfWork.StatisticRepository.GetUserStatistic(userId);
            }

            return Ok(userStatistic);
        }

        [HttpGet]
        [Route("teachingUnitStat")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetTeachingUnitStats(int teachingUnitId)
        {
            TeachingUnitStatistic teachingUnitStatistic = await _unitOfWork.StatisticRepository.GetTeachingUnitStatistic(teachingUnitId);

            return Ok(teachingUnitStatistic);
        }
    }
}
