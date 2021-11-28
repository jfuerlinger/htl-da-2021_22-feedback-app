using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Newtonsoft.Json;
using Feedback_App_XAML.Models;
using Xamarin.Essentials;


namespace Feedback_App_XAML.RestClient
{
    public class RestClient<T>
    {
        //private const string MainWebServiceUrl = "https://localhost:5001/"; // LocalHost
        private const string MainWebServiceUrl = "https://10.0.2.2:5001"; // For ANDROID EMULATOR
        private const string LoginWebServiceUrl = MainWebServiceUrl + "/api/login";

        public async Task<bool> checkLogin(string userName, string password)
        {
            LoginModel model = new LoginModel() { Username = userName, Password = password};
            
            HttpClientHandler httpClientHandler;
                #if (DEBUG)
                httpClientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
                #else
                httpClientHandler = new HttpClientHandler(); "#endif using (var client = new HttpClient(httpHandler));
                #endif

            var client = new HttpClient(httpClientHandler);
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            string debugContentJson = await content.ReadAsStringAsync();
            var result = await client.PostAsync(LoginWebServiceUrl, content).ConfigureAwait(false);
            return result.IsSuccessStatusCode;




            


            //var client = new HttpClient();
            //client.BaseAddress = new Uri("localhost:8080");
            //string jsonData = @"{""username"" : ""myusername"", ""password"" : ""mypassword""}"
            //var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            //HttpResponseMessage response = await client.PostAsync("/foo/login", content);
            //// this result string should be something like: "{"token":"rgh2ghgdsfds"}"
            //var result = await response.Content.ReadAsStringAsync();
        }
    }
}
