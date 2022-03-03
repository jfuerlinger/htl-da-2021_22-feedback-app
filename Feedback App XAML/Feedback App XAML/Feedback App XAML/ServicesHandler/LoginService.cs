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

        public async Task<bool> CheckLoginIfExists(string userName, string password)
        {
            var check = await _restClient.checkLogin(userName, password);

            return check;
        }
        public async Task<string> GetData(string userName, string password)
        {
            var check = await _restClient.GetData(userName, password);

            return check;
        }
    }
}
