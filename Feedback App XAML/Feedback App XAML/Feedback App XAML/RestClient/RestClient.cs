using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace Feedback_App_XAML.RestClient
{
    public class RestClient<T>
    {
        //private const string MainWebServiceUrl = "https://10.0.2.2:5001/"; // Put your main host url here

        private const string MainWebServiceUrl = "https://172.23.128.1:5001/"; // Put your main host url here
        

        //private const string MainWebServiceUrl = "https://localhost:5001/"; // Put your main host url here

        private const string LoginWebServiceUrl = MainWebServiceUrl + "api/login/"; // put your api extension url/uri here

        public async Task<bool> checkLogin(string userName, string password)
        {
            var httpClient = new HttpClient();
            // http://MainHost/api/UserCredentials/username=foo/password=foo. The api value and response value should match in order to return a true status code. 
            var response = await httpClient.GetAsync(LoginWebServiceUrl + "userName=" + userName + "/" + "password=" + password);

            return response.IsSuccessStatusCode;
        }
    }
}
