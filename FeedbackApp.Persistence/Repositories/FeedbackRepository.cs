using FeedbackApp.Core.Contracts.Persistence;
using FeedbackApp.Core.Model;
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

        public Task<int> CountAllFeedbacksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAllTeachingUnitsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountFeedbacksAsync(int teachingUnitId)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountTeachingUnitsAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task CreateFeedback(int userId, int teachingUnitId, int stars, string? comment)
        {
            throw new NotImplementedException();
        }

        public Task CreateTeachingUnit(int userId, string title, bool isPublic, string? subject, string? description, DateTime? date, DateTime? expiryDate, string? subscriptionKey)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFeedback(int feedbackId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTeachingUnit(int teachingUnitId)
        {
            throw new NotImplementedException();
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
