using FeedbackApp_WebApi.Persistance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedbackApp_WebApi.FeedbackDB.Contracts
{
    public interface ITeacherRepository
    {
        Task<int> CountAsync();
        Task<List<Teacher>> GetAllAsync();
        Task<Teacher> GetByIdAsync(int id);
    }
}
