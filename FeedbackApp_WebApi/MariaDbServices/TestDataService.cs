using FeedbackApp.WebApi.MariaDbModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FeedbackApp.WebApi.MariaDbServices
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
