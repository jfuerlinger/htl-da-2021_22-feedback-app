using FeedbackApp_WebApi.FeedbackDB.Contracts;
using FeedbackApp_WebApi.Persistance;
using FeedbackApp_WebApi.Persistance.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp_WebApi.FeedbackDB
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly FeedbackDbContext _dbContext;

        public TeacherRepository(FeedbackDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CountAsync()
        {
            return await _dbContext.Teachers.CountAsync();
        }

        public async Task<List<Teacher>> GetAllAsync()
        {
            return await _dbContext.Teachers.OrderBy(p => p.LastName).ThenBy(p => p.FirstName).ToListAsync();
        }

        public async Task<Teacher> GetByIdAsync(int id)
        {
            return await _dbContext.Teachers.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task CreateTeacher(string identityId)
        {
            Teacher teacher = new() { IdentityId = identityId };
            await _dbContext.Teachers.AddAsync(teacher);
        }
    }
}
