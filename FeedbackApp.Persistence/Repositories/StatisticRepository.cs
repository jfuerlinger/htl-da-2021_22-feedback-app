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

        public async Task UpdateAvgStarsTeachingUnit(int teachingUnitId)
        {
            TeachingUnitStatistic teachingUnitStatistic = await GetTeachingUnitStatistic(teachingUnitId);
            List<Feedback> feedbacks = await _dbContext.Feedbacks.Where(x => x.TeachingUnitId == teachingUnitId).ToListAsync();
            double avgStars = 0;

            foreach (Feedback feedback in feedbacks)
            {
                avgStars += feedback.Stars;
            }

            avgStars = avgStars / (double)feedbacks.Count();
            teachingUnitStatistic.AvgStars = avgStars;

            _dbContext.TeachingUnitStatistics.Update(teachingUnitStatistic);
        }

        public async Task CalcAvgStarsUserStats(int userId)
        {
            double avgStars = 0;
            int count = 0;
            UserStatistic userStatistic = await GetUserStatistic(userId);
            List<TeachingUnit> teachingUnits = await _dbContext.TeachingUnits.Where(x => x.UserId == userId).ToListAsync();
            
            foreach (TeachingUnit teachingUnit in teachingUnits)
            {
                TeachingUnitStatistic teachingUnitStatistic = await GetTeachingUnitStatistic(teachingUnit.Id);
                avgStars += teachingUnitStatistic.AvgStars;
                count++;
            }

            userStatistic.AvgStars = avgStars / (double)count;
            _dbContext.UserStatistics.Update(userStatistic);
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

        public async Task DeleteTeachingUnitStats(int teachingUnitId)
        {
            var teachingUnitStats = await GetTeachingUnitStatistic(teachingUnitId);

            _dbContext.TeachingUnitStatistics.Remove(teachingUnitStats);
        }

        public async Task DeleteUserStats(int userid)
        {
            var userStats = await GetUserStatistic(userid);

            _dbContext.UserStatistics.Remove(userStats);
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

        public async Task<TeachingUnitStatistic> GetTeachingUnitStatistic(int teachingUnitId)
        {
            return await _dbContext.TeachingUnitStatistics.Where(x => x.TeachingUnitId == teachingUnitId).FirstOrDefaultAsync();
        }

        public async Task<UserStatistic> GetUserStatistic(int userId)
        {
            return await _dbContext.UserStatistics.Where(x => x.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task IncreaseFeedbackCounter(int teachingUnitId, int userId)
        {
            var globalHistory = await GetGlobalHistory();
            var teachingUnitStat = await GetTeachingUnitStatistic(teachingUnitId);
            var userStatistic = await GetUserStatistic(userId);

            globalHistory.CreatedFeedbacksCount++;
            teachingUnitStat.FeedbackCount++;
            userStatistic.CreatedFeedbacksCount++;

            _dbContext.GlobalHistories.Update(globalHistory);
            _dbContext.TeachingUnitStatistics.Update(teachingUnitStat);
            _dbContext.UserStatistics.Update(userStatistic);
        }

        public async Task IncreaseTeachingUnitCounter(int userId)
        {
            var globalHistory = await GetGlobalHistory();
            var userStatistic = await GetUserStatistic(userId);

            globalHistory.CreatedTeachingUnitsCount++;
            userStatistic.CreatedTeachingUnitsCount++;

            _dbContext.GlobalHistories.Update(globalHistory);
            _dbContext.UserStatistics.Update(userStatistic);
        }

        public async Task UpdateUserCount()
        {
            var globalHistory = await GetGlobalHistory();
            globalHistory.UserCount = await _dbContext.Users.CountAsync();

            _dbContext.GlobalHistories.Update(globalHistory);
        }
    }
}
