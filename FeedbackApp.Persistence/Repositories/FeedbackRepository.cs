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

        public async Task<int> CountFeedbacksAsync(int teachingUnitId)
        {
            return await _dbContext.Feedbacks.Where(x => x.TeachingUnitId == teachingUnitId).CountAsync();
        }

        public async Task<int> CountTeachingUnitsAsync(int userId)
        {
            return await _dbContext.TeachingUnits.Where(x => x.UserId == userId).CountAsync();
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

        public async Task<List<Feedback>> GetAllFeedbacksByTeachingUnitId(int teachingUnitId)
        {
            return await _dbContext.Feedbacks.Where(x => x.TeachingUnitId == teachingUnitId).ToListAsync();
        }

        public async Task<List<TeachingUnit>> GetAllTeachingUnitsByUserId(int userId)
        {
            return await _dbContext.TeachingUnits.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Feedback> GetFeedbackById(int feedbackId)
        {
            return await _dbContext.Feedbacks.Where(x => x.Id == feedbackId).FirstOrDefaultAsync();
        }

        public async Task<TeachingUnit> GetTeachingUnitById(int teachingUnit)
        {
            return await _dbContext.TeachingUnits.FindAsync(teachingUnit);
        }

        public Task ModifyFeedback(int feedbackId, int stars, string? comment)
        {
            throw new NotImplementedException();
        }

        public Task ModifyTeachingUnit(int teachingUnitId, string title, bool isPublic, string? subject, string? description, DateTime? date, DateTime? expiryDate, string? subscriptionKey)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteFeedbackRange(int teachingUnitId)
        {
            List<Feedback> feedbacks = await GetAllFeedbacksByTeachingUnitId(teachingUnitId);
            _dbContext.Feedbacks.RemoveRange(feedbacks);
        }

        public async Task<List<TeachingUnit>> GetAllPublicTeachingUnits()
        {
            return await _dbContext.TeachingUnits.Where(x => x.IsPublic == true).ToListAsync();
        }
    }
}