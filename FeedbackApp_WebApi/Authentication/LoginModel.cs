using System.ComponentModel.DataAnnotations;

namespace FeedbackApp.WebApi.Authentication
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username ist erforderlich")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Passwort ist erforderlich")]
        public string Password { get; set; }
    }
}
