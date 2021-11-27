using Feedback_App_XAML.Models;
using Feedback_App_XAML.RestClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Feedback_App_XAML.ServicesHandler
{
    public class LoginService
    {

        RestClient<LoginModel> _restClient = new RestClient<LoginModel>();

        // Boolean function with the following parameters of username & password.
        public async Task<bool> CheckLoginIfExists(string username, string password)
        {
            var check = await _restClient.checkLogin(username, password);

            return check;
        }
    }
}
