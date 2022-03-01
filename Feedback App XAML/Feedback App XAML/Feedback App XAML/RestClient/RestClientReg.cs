using Feedback_App_XAML.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Feedback_App_XAML.RestClient
{
    internal class RestClientReg<T>
    {
        //private const string MainWebServiceUrl = "https://localhost:5001/"; // LocalHost
        private const string MainWebServiceUrl = "https://10.0.2.2:5001"; // For ANDROID EMULATOR
        private const string RegWebServiceUrl = MainWebServiceUrl + "/api/register";
        private const string RegLehrerWebServiceUrl = MainWebServiceUrl + "/api/register-lehrer";

        public async Task<bool> checkRegister(string userName, string email, string password)
        {
            HttpClientHandler httpClientHandler;
            #if (DEBUG)
            httpClientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            #else
                httpClientHandler = new HttpClientHandler(); "#endif using (var client = new HttpClient(httpHandler));
            #endif

            RegisterModel model = new RegisterModel() { Username = userName, Email = email, Password = password };
            bool Response = false;
            var client = new HttpClient(httpClientHandler);

            var json = JsonConvert.SerializeObject(model);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType=new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = client.PostAsync(RegWebServiceUrl, httpContent);
            var mystring = response.GetAwaiter().GetResult();

            if(response.Result.IsSuccessStatusCode)
            {
                Response = true;
            }
            return Response;
        }

        public async Task<bool> checkRegisterLehrer(string userName, string email, string password)
        {
            HttpClientHandler httpClientHandler;
            #if (DEBUG)
            httpClientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            #else
                httpClientHandler = new HttpClientHandler(); "#endif using (var client = new HttpClient(httpHandler));
            #endif

            RegisterModel model = new RegisterModel() { Username = userName, Email = email, Password = password };
            bool Response = false;
            var client = new HttpClient(httpClientHandler);

            var json = JsonConvert.SerializeObject(model);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = client.PostAsync(RegLehrerWebServiceUrl, httpContent);
            var mystring = response.GetAwaiter().GetResult();

            if (response.Result.IsSuccessStatusCode)
            {
                Response = true;
            }
            return Response;
        }
    }
}
