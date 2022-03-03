using FeedbackApp.Core.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackApp.Persistence.Repositories
{
    public class StatisticRepository : IStatisticRepository
    {
        private readonly FeedbackDbContext _dbContext;

        public StatisticRepository(FeedbackDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
