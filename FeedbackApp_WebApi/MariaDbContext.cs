using FeedbackApp_WebApi.DataModels;
using Microsoft.EntityFrameworkCore;

namespace FeedbackApp_WebApi
{
    public class MariaDbContext : DbContext
    {
        public MariaDbContext(DbContextOptions<MariaDbContext> options) : base(options)
        { }

        public virtual DbSet<TestDataModel> TestData { get; set; }
    }
}
