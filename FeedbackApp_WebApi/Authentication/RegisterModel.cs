using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp_WebApi.Authentication
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username ist erforderlich")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "E-Mail ist erforderlich")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Passwort ist erforderlich")]
        public string Password { get; set; }
    }
}
