using FeedbackApp.Core.Contracts.Persistence;
using FeedbackApp.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace FeedbackApp.Persistence.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly FeedbackDbContext _dbContext;

        public StudentRepository(FeedbackDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CountAsync()
        {
            return await _dbContext.Students.CountAsync();
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _dbContext.Students.OrderBy(p => p.LastName).ThenBy(p => p.FirstName).ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _dbContext.Students.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task CreateStudentAsync(string identityId)
        {
            Student student = new() { IdentityId = identityId };
            await _dbContext.Students.AddAsync(student);
        }

        public async Task DeleteStudentByIdentityIdAsync(string identityId)
        {
            Student student = await GetByIdentityIdAsync(identityId);
            _dbContext.Students.Remove(student);
        }

        public async Task<Student> GetByIdentityIdAsync(string identityId)
        {
            return await _dbContext.Students.SingleOrDefaultAsync(p => p.IdentityId == identityId);
        }
    }
}
