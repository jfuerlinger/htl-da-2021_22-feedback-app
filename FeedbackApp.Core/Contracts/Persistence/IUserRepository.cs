using System;
using System.Collections.Generic;
using FeedbackApp.Core.Model;

namespace FeedbackApp.Core.Contracts.Persistence
{
    public interface IUserRepository
    {
        Task<int> CountAsync();
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task CreateUserAsync(User user);
        Task DeleteUserByIdentityIdAsync(string identityId);
        Task<User> GetByIdentityIdAsync(string identityId);
        Task UpdateUserAsync(string identityId, string title, string firstName, string lastName, DateTime? birthdate, string school);
    }
}
