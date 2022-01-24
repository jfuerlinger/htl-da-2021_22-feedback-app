using FeedbackApp.WebApi.Authentication;
using FeedbackApp.WebApi.MariaDbServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FeedbackApp.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> _logger;
        private readonly ITestDataService _testDataService;

        public DataController(ILogger<DataController> logger, ITestDataService testDataService)
        {
            _logger = logger;
            _testDataService = testDataService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("testdata")]
        public async Task<IActionResult> TestData()
        {
            var result = await _testDataService.GetTestDataModel(1);

            if (result == default)
            {
                return NotFound();
            }

            string resultText = result.TestText.ToString();

            return Content(resultText, "application/json");
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.teacher, AuthenticationSchemes = "Bearer")]
        [Route("testdata-teacher")]
        public IActionResult TestDataTeacher()
        {
            string testData = "Testdaten für Lehrer, Login funktioniert!";

            return Content(testData, "application/json");
        }
    }
}
