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
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Teacher> Teachers => Set<Teacher>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
