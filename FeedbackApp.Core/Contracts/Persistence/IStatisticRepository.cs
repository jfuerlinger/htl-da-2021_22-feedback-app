﻿using FeedbackApp.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackApp.Core.Contracts.Persistence
{
    public interface IStatisticRepository
    {
        Task<GlobalHistory> GetGlobalHistory();
        Task CreateUserStats(User user);
        Task CreateTeachingUnitStats(TeachingUnit teachingUnit);
        Task IncreaseTeachingUnitCounter(int userId);
        Task IncreaseFeedbackCounter(int teachingUnitId, int userId);
        Task UpdateUserCount();
        Task UpdateAvgStarsTeachingUnit(int teachingUnitId);
        Task UpdateAvgStarsUserStats(int userId);
        Task<UserStatistic> GetUserStatistic(int userId);
        Task<TeachingUnitStatistic> GetTeachingUnitStatistic(int teachingUnitId);
        Task DeleteUserStats(int userid);
        Task DeleteTeachingUnitStats(int teachingUnitId);
    }
}
