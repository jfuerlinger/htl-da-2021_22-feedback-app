using FeedbackApp.Core.Contracts.Persistence;
using FeedbackApp.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace FeedbackApp.Persistence.Repositories
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

        public async Task CreateTeacherAsync(string identityId)
        {
            Teacher teacher = new() { IdentityId = identityId };
            await _dbContext.Teachers.AddAsync(teacher);
        }

        public async Task DeleteTeacherByIdentityIdAsync(string identityId)
        {
            Teacher teacher = await GetByIdentityIdAsync(identityId);
            _dbContext.Teachers.Remove(teacher);
        }

        public async Task<Teacher> GetByIdentityIdAsync(string identityId)
        {
            return await _dbContext.Teachers.SingleOrDefaultAsync(p => p.IdentityId == identityId);
        }
    }
}
