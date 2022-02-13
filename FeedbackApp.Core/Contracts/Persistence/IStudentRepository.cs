using FeedbackApp.Core.Model;

namespace FeedbackApp.Core.Contracts.Persistence
{
    public interface IStudentRepository
    {
        Task<int> CountAsync();
        Task<List<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int id);
        Task CreateStudentAsync(string identyId);
        Task DeleteStudentByIdentityIdAsync(string identityId);
        Task<Student> GetByIdentityIdAsync(string identityId);
        Task UpdateStudentAsync(string identityId, string firstName, string lastName, DateTime? birthdate, string school);
    }
}
