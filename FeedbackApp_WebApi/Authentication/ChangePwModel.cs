using System.ComponentModel.DataAnnotations;

namespace FeedbackApp.WebApi.Authentication
{
    public class ChangePwModel
    {
        [Required(ErrorMessage = "Username ist erforderlich")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Altes Passwort ist erforderlich")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Neues Passwort ist erforderlich")]
        public string NewPassword { get; set; }
    }
}
