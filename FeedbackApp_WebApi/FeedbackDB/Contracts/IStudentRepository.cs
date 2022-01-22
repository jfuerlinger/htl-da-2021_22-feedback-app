using FeedbackApp_WebApi.Persistance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedbackApp_WebApi.FeedbackDB.Contracts
{
    public interface IStudentRepository
    {
        Task<int> CountAsync();
        Task<List<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int id);
    }
}
