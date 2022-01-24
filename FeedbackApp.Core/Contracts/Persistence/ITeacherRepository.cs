using FeedbackApp.Core.Model;

namespace FeedbackApp.Core.Contracts.Persistence
{
    public interface ITeacherRepository
    {
        Task<int> CountAsync();
        Task<List<Teacher>> GetAllAsync();
        Task<Teacher> GetByIdAsync(int id);
        Task CreateTeacherAsync(string identityId);
        Task DeleteTeacherByIdentityIdAsync(string identityId);
        Task<Teacher> GetByIdentityIdAsync(string identityId);
    }
}
