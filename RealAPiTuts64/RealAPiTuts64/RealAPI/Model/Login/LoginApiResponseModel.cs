using System;
namespace RealAPI.Model.Login
{
   
    public class User
    {
        public string userId { get; set; }
    }

    public class LoginApiResponseModel
    {
        public string authenticationToken { get; set; }
        public User user { get; set; }
    }
}
