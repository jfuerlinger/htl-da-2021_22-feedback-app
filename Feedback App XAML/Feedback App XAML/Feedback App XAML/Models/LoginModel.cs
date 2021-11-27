using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feedback_App_XAML.Models
{
    public class LoginModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
