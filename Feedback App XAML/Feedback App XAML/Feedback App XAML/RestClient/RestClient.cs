using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Feedback_App_XAML.RestClient
{
    public class RestClient<T>
    {
        //private const string MainWebServiceUrl = "https://172.23.128.1:5001/"; // Put your main host url here
        //private const string MainWebServiceUrl = "https://localhost:5001/"; // LocalHost
        
        private const string MainWebServiceUrl = "https://10.0.2.2:5001"; // For ANDROID EMULATOR

        private const string LoginWebServiceUrl = MainWebServiceUrl + "/api/login/"; // put your api extension url/uri here

        public async Task<bool> checkLogin(string userName, string password)
        {
            HttpClientHandler httpClientHandler;

            #if (DEBUG)
            httpClientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            #else
            httpClientHandler = new HttpClientHandler(); "#endif using (var client = new HttpClient(httpHandler));
            #endif

            var httpClient = new HttpClient(httpClientHandler);

            // http://MainHost/api/UserCredentials/username=foo/password=foo. The api value and response value should match in order to return a true status code. 
            var response = await httpClient.GetAsync(LoginWebServiceUrl + "userName=" + userName + "/" + "password=" + password);

            return response.IsSuccessStatusCode;
        }
    }
}
