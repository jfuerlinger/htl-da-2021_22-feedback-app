using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp_WebApi.Authentication
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username ist erforderlich")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Passwort ist erforderlich")]
        public string Password { get; set; }
    }
}
