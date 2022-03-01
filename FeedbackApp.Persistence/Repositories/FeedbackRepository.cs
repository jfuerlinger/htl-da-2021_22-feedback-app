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
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly FeedbackDbContext _dbContext;

        public FeedbackRepository (FeedbackDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CountAllFeedbacksAsync()
        {
            return await _dbContext.Feedbacks.CountAsync();
        }

        public async Task<int> CountAllTeachingUnitsAsync()
        {
            return await _dbContext.TeachingUnits.CountAsync();
        }

        public Task<int> CountFeedbacksAsync(int teachingUnitId)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountTeachingUnitsAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task AddFeedbackAsync(Feedback feedback)
        {
            await _dbContext.Feedbacks.AddAsync(feedback);
        }

        public async Task AddTeachingUnitAsync(TeachingUnit teachingUnit)
        {
            await _dbContext.TeachingUnits.AddAsync(teachingUnit);
        }

        public async Task DeleteFeedback(int feedbackId)
        {
            var feedback = await _dbContext.Feedbacks.FindAsync(feedbackId);
            _dbContext.Feedbacks.Remove(feedback);
        }

        public async Task DeleteTeachingUnit(int teachingUnitId)
        {
            var teachingUnit = await _dbContext.TeachingUnits.FindAsync(teachingUnitId);
            _dbContext.TeachingUnits.Remove(teachingUnit);
        }

        public Task<List<Feedback>> GetAllFeedbacksByTeachingUnitId(int teachingUnitId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TeachingUnit>> GetAllTeachingUnitsByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Feedback> GetFeedbackById(int feedackId)
        {
            throw new NotImplementedException();
        }

        public Task<TeachingUnit> GetTeachingUnitById(int teachingUnit)
        {
            throw new NotImplementedException();
        }

        public Task ModifyFeedback(int feedbackId, int stars, string? comment)
        {
            throw new NotImplementedException();
        }

        public Task ModifyTeachingUnit(int teachingUnitId, string title, bool isPublic, string? subject, string? description, DateTime? date, DateTime? expiryDate, string? subscriptionKey)
        {
            throw new NotImplementedException();
        }
    }
}
