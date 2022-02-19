using FeedbackApp.Core.Contracts.Persistence;
using FeedbackApp.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackApp.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FeedbackDbContext _dbContext;

        public UserRepository(FeedbackDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CountAllAsync()
        {
            return await _dbContext.Users.CountAsync();
        }

        public Task<int> CountStudentsAsync()
        {
            return Task.FromResult(0);
        }

        public Task<int> CountTeachersAsync()
        {
            return Task.FromResult(0);
        }

        public Task<int> CountAdminsAsync()
        {
            return Task.FromResult(0);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _dbContext.Users.OrderBy(p => p.LastName).ThenBy(p => p.FirstName).ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task CreateUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public async Task DeleteUserByIdentityIdAsync(string identityId)
        {
            User user = await GetByIdentityIdAsync(identityId);
            _dbContext.Users.Remove(user);
        }

        public async Task<User> GetByIdentityIdAsync(string identityId)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(p => p.IdentityId == identityId);
        }

        public async Task UpdateUserAsync(string identityId, string title ,string firstName, string lastName, DateTime? birthdate, string school)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(p => p.IdentityId == identityId);

            user.Title = title;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Birthdate = birthdate;
            user.School = school;

            _dbContext.Users.Update(user);
        }
    }
}
