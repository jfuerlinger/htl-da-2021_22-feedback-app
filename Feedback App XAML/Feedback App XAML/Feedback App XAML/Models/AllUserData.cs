using System;
using System.Collections.Generic;
using System.Text;

namespace Feedback_App_XAML.Models
{
    internal class AllUserData
    {
        public string Token { get; set; }
        public string Expiration { get; set; }
        public int UserId { get; set; }
        public string IdentityId { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public string School { get; set; }
    }
}
