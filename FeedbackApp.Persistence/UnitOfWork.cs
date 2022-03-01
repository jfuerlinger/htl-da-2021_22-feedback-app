using FeedbackApp.Core.Contracts.Persistence;
using FeedbackApp.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FeedbackApp.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FeedbackDbContext _dbContext;

        public UnitOfWork() : this(new FeedbackDbContext()) { }

        public UnitOfWork(FeedbackDbContext dbContext)
        {
            _dbContext = dbContext;
            UserRepository = new UserRepository(dbContext);
            FeedbackRepository = new FeedbackRepository(dbContext);
        }

        public IUserRepository UserRepository { get; }

        public IFeedbackRepository FeedbackRepository { get; }

        public async Task CreateDatabaseAsync()
        {
            await _dbContext!.Database.EnsureCreatedAsync();
        }

        public async Task DeleteDatabaseAsync()
        {
            await _dbContext!.Database.EnsureDeletedAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        private async Task DisposeAsync(bool disposing)
        {
            if (disposing)
            {
                await _dbContext.DisposeAsync();
            }
        }

        public async Task MigrateDatabaseAsync()
        {
            await _dbContext!.Database.MigrateAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
