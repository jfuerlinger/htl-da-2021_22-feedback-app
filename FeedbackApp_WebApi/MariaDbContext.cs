using FeedbackApp.WebApi.MariaDbModels;
using Microsoft.EntityFrameworkCore;

namespace FeedbackApp.WebApi
{
    public class MariaDbContext : DbContext
    {
        public MariaDbContext(DbContextOptions<MariaDbContext> options) : base(options)
        { }

        public virtual DbSet<TestDataModel> TestData { get; set; }
    }
}
