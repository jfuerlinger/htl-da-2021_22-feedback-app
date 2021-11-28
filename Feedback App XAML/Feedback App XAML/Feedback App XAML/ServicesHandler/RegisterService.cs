using Feedback_App_XAML.Models;
using Feedback_App_XAML.RestClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Feedback_App_XAML.ServicesHandler
{
    public class RegisterService
    {
        RestClientReg<RegisterModel> _restClient = new RestClientReg<RegisterModel>();

        public async Task<bool> CheckRegisterIfExists(string userName, string email, string password)
        {
            var check = await _restClient.checkRegister(userName, email, password);
            return check;
        }

    }
}
