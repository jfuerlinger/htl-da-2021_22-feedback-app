using FeedbackApp_WebApi.FeedbackDB.Contracts;
using FeedbackApp_WebApi.Persistance;
using FeedbackApp_WebApi.Persistance.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp_WebApi.FeedbackDB
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
    }
}
