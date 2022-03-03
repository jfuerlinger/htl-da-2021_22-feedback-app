using System;
using System.Threading.Tasks;

namespace FeedbackApp.Core.Contracts.Persistence
{
    public interface IUnitOfWork : IAsyncDisposable, IDisposable
    {
        IUserRepository UserRepository { get; }
        IFeedbackRepository FeedbackRepository { get; }
        IStatisticRepository StatisticRepository { get; }

        Task<int> SaveChangesAsync();
        Task DeleteDatabaseAsync();
        Task MigrateDatabaseAsync();
        Task CreateDatabaseAsync();
    }
}
