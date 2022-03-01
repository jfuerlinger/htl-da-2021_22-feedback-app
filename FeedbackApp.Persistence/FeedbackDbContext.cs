using FeedbackApp.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace FeedbackApp.Persistence
{
    public class FeedbackDbContext : DbContext
    {
        public FeedbackDbContext()
        {
        }

        public FeedbackDbContext(DbContextOptions<FeedbackDbContext> options) : base(options)
        { }

        public DbSet<User> Users => Set<User>();
        public DbSet<TeachingUnit> TeachingUnits => Set<TeachingUnit>();
        public DbSet<Feedback> Feedbacks => Set<Feedback>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
