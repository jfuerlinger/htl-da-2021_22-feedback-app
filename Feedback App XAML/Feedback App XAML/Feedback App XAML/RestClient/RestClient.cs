using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Newtonsoft.Json;
using Feedback_App_XAML.Models;


namespace Feedback_App_XAML.RestClient
{
    public class RestClient<T>
    {
        private const string MainWebServiceUrl = "https://10.0.2.2:5001"; // FOR ANDROID EMULATOR
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
        }

        public async Task<string> GetData(string userName, string password)
        {
            LoginModel model = new LoginModel() { Username = userName, Password = password };

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

            string responseString = await result.Content.ReadAsStringAsync();
            var text = JsonConvert.DeserializeObject(responseString);
            return responseString;
        }
    }
}
