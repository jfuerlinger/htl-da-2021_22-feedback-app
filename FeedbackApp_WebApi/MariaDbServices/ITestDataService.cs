using FeedbackApp_WebApi.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedbackApp_WebApi.MariaDbServices
{
    public interface ITestDataService
    {
        public Task<TestDataModel> GetTestDataModel(int id);
    }
}