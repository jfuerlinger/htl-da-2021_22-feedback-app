using System;

namespace FeedbackApp.WebApi.AccountManagement
{
    public class ModifierUserDataModel
    {
        public string IdentityId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string School { get; set; }
    }
}
