using System;
using System.Threading.Tasks;

namespace FeedbackApp_WebApi.FeedbackDB.Contracts
{
    public interface IUnitOfWork : IAsyncDisposable, IDisposable
    {
        IStudentRepository StudentRepository { get; }
        ITeacherRepository TeacherRepository { get; }

        Task<int> SaveChangesAsync();
        Task DeleteDatabaseAsync();
        Task MigrateDatabaseAsync();
        Task CreateDatabaseAsync();
    }
}
