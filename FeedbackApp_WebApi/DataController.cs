using FeedbackApp_WebApi.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp_WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("testdata")]
        public IActionResult TestData()
        {
            string testData = "Testdaten, Login funktioniert!";

            return Content(testData, "application/json");
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
