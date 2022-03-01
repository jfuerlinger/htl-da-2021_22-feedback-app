using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feedback_App_XAML.Models
{
    public class LoginModel
    {
        string role;
        public string Username { get; set; }
        public string Password { get; set; }

        public string Role
        {
            get { return role; }
            set { role = value; }
        }
    }
}
