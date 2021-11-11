using FeedbackApp_WebApi.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp_WebApi.MariaDbServices
{
    public class TestDataService : ITestDataService
    {
        private readonly MariaDbContext _context;

        public TestDataService(MariaDbContext context)
        {
            _context = context;
        }

        public async Task<TestDataModel> GetTestDataModel(int id)
        {
            return await _context.TestData.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
