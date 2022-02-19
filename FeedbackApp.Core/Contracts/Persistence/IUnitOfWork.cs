using System;
using System.Threading.Tasks;

namespace FeedbackApp.Core.Contracts.Persistence
{
    public interface IUnitOfWork : IAsyncDisposable, IDisposable
    {
        IStudentRepository StudentRepository { get; }
        ITeacherRepository TeacherRepository { get; }
        IUserRepository UserRepository { get; }

        Task<int> SaveChangesAsync();
        Task DeleteDatabaseAsync();
        Task MigrateDatabaseAsync();
        Task CreateDatabaseAsync();
    }
}
