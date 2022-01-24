using FeedbackApp.WebApi.MariaDbModels;
using System.Threading.Tasks;

namespace FeedbackApp.WebApi.MariaDbServices
{
    public interface ITestDataService
    {
        public Task<TestDataModel> GetTestDataModel(int id);
    }
}