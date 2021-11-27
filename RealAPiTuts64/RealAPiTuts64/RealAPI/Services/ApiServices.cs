using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RealAPI.Model.Login;
using Xamarin.Essentials;

namespace RealAPI.Services
{
    public class ApiServices
    {
        private static ApiServices _ServiceClientInstance;
        public static ApiServices ServiceClientInstance
        {
            get
            {
                if (_ServiceClientInstance == null)
                    _ServiceClientInstance = new ApiServices();
                return _ServiceClientInstance;
            }
        }

        private JsonSerializer _serializer = new JsonSerializer();
        private HttpClient client;


        public ApiServices()
        {
            client = new HttpClient();
            //Change your base address here
            client.BaseAddress = new Uri("https://localhost:5001");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<LoginApiResponseModel> AuthenticateUserAsync(string userName, string password)
        {
            try
            {
                LoginApiRequestModel loginRequestModel = new LoginApiRequestModel()
                {
                    Username = userName,
                    Password = password

                };
                var content = new StringContent(JsonConvert.SerializeObject(loginRequestModel), Encoding.UTF8, "application/json");
                //Change your base address tail part here and post it. 
                var response = await client.PostAsync("https://localhost:5001", content);
                response.EnsureSuccessStatusCode();
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    var jsoncontent = _serializer.Deserialize<LoginApiResponseModel>(json);
                    Preferences.Set("authToken", jsoncontent.authenticationToken);
                    return jsoncontent;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
