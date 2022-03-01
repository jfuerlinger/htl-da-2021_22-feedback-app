using FeedbackApp.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackApp.Core.Contracts.Persistence
{
    public interface IFeedbackRepository
    {
        Task<int> CountAllTeachingUnitsAsync();
        Task<int> CountAllFeedbacksAsync();
        Task<int> CountTeachingUnitsAsync(int userId);
        Task<int> CountFeedbacksAsync(int teachingUnitId);

        Task CreateTeachingUnit(int userId, string title, bool isPublic, 
            string? subject, string? description, DateTime? date, 
            DateTime? expiryDate, string? subscriptionKey);

        Task ModifyTeachingUnit(int teachingUnitId, string title, bool isPublic,
           string? subject, string? description, DateTime? date,
           DateTime? expiryDate, string? subscriptionKey);

        Task DeleteTeachingUnit(int teachingUnitId);
        Task CreateFeedback(int userId, int teachingUnitId, int stars, string? comment);
        Task ModifyFeedback(int feedbackId, int stars, string? comment);
        Task DeleteFeedback(int feedbackId);

        Task<List<TeachingUnit>> GetAllTeachingUnitsByUserId(int userId);
        Task<List<Feedback>> GetAllFeedbacksByTeachingUnitId(int teachingUnitId);
        Task<TeachingUnit> GetTeachingUnitById(int teachingUnit);
        Task<Feedback> GetFeedbackById(int feedackId);
    }
}
