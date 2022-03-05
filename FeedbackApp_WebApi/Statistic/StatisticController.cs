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
        /// get the teacher statistics
        /// </summary>
        /// <returns>teacher statistics</returns>
        [HttpGet]
        [Route("teacherStat")]
        public async Task<IActionResult> GetTeacherStatistic()
        {
            return Ok();
        }
    }
}
