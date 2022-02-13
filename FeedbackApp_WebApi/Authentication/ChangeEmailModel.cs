using System.ComponentModel.DataAnnotations;

namespace FeedbackApp.WebApi.Authentication
{
    public class ChangeEmailModel
    {
        [Required(ErrorMessage = "Username ist erforderlich")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Neue E-Mail ist erforderlich")]
        public string NewEmail { get; set; }
    }
}
