using FeedbackApp.Core.Contracts.Persistence;
using FeedbackApp.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackApp.Persistence.Repositories
{
    public class StatisticRepository : IStatisticRepository
    {
        private readonly FeedbackDbContext _dbContext;

        public StatisticRepository(FeedbackDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task CalcAvgStarsTeachingUnit(int teachingUnitId)
        {
            throw new NotImplementedException();
        }

        public Task CalcAvgStarsUserStats(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateTeachingUnitStats(TeachingUnit teachingUnit)
        {
            TeachingUnitStatistic teachingUnitStatistic = 
                new() { TeachingUnitId = teachingUnit.Id, teachingUnit = teachingUnit};

            await _dbContext.TeachingUnitStatistics.AddAsync(teachingUnitStatistic);
        }

        public async Task CreateUserStats(User user)
        {
            UserStatistic userStatistic = new() { UserId = user.Id, User = user };

            await _dbContext.UserStatistics.AddAsync(userStatistic);
        }

        public async Task<GlobalHistory> GetGlobalHistory()
        {
            int count = await _dbContext.GlobalHistories.CountAsync();

            if (count == 0)
            {
                GlobalHistory globalHistory = new();
                await _dbContext.GlobalHistories.AddAsync(globalHistory);
                _dbContext.SaveChanges();
            }

            return await _dbContext.GlobalHistories.FirstOrDefaultAsync();
        }

        public Task<TeachingUnitStatistic> GetTeachingUnitStatistic(int teachingUnitId)
        {
            throw new NotImplementedException();
        }

        public Task<UserStatistic> GetUserStatistic(int userId)
        {
            throw new NotImplementedException();
        }

        public Task IncreaseFeedbackCounter(int teachingUnitId)
        {
            throw new NotImplementedException();
        }

        public Task IncreaseTeachingUnitCounter(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
