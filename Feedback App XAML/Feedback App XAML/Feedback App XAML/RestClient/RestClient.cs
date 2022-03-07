using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Newtonsoft.Json;
using Feedback_App_XAML.Models;
using System.Net;
using System.IO;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific.AppCompat;

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

        public async Task<string> GetUserData (string token, string identityId)
        {
            GetUserData model = new GetUserData() { Token = token, IdentityId = identityId };

            HttpClientHandler httpClientHandler;
                #if (DEBUG)
            httpClientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            #else
                            httpClientHandler = new HttpClientHandler(); "#endif using (var client = new HttpClient(httpHandler));
            #endif

            var client = new HttpClient(httpClientHandler);
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://10.0.2.2:5001/api/user/getData");
            request.Method = HttpMethod.Post;

            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");
            request.Headers.Add("Authorization", "Bearer " + token);

            var bodyString = "{    \"identityId\": \"" + identityId + "\"}";

            var content = new StringContent(bodyString, Encoding.UTF8, "application/json");
            request.Content = content;

            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<bool> SetDataUser(string firstName, string lastName, string token, string identityId, string school)
        {
            SetData model = new SetData() { FirstName = firstName, LastName = lastName, Token=token, IdentityId=identityId, School=school };

            HttpClientHandler httpClientHandler;
            #if (DEBUG)
            httpClientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            #else
                            httpClientHandler = new HttpClientHandler(); "#endif using (var client = new HttpClient(httpHandler));
            #endif


            var client = new HttpClient(httpClientHandler);
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://10.0.2.2:5001/api/user/modifierData");
            request.Method = HttpMethod.Post;

            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");


            request.Headers.Add("Authorization", "Bearer " + token);
            var bodyString = "{    \"identityId\": \"" + identityId + "\",    \"title\": null,    \"firstName\": \"" + firstName + "\",    \"lastName\": \"" + lastName + "\",    \"birthdate\": null,    \"school\": \"" + school + "\"}";
            var content = new StringContent(bodyString, Encoding.UTF8, "application/json");
            request.Content = content;

            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SetPass(string userName, string password, string newPassword)
        {
            SetPass model = new SetPass() { Username=userName,  Password=password, NewPassword= newPassword };

            HttpClientHandler httpClientHandler;
            #if (DEBUG)
            httpClientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            #else
                            httpClientHandler = new HttpClientHandler(); "#endif using (var client = new HttpClient(httpHandler));
            #endif


            var client = new HttpClient(httpClientHandler);
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://10.0.2.2:5001/api/changePw");
            request.Method = HttpMethod.Post;

            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");

            var bodyString = "{    \"username\": \"" + userName + "\",    \"password\": \"" + password + "\",    \"newPassword\": \"" + newPassword + "\"}"; 
            var content = new StringContent(bodyString, Encoding.UTF8, "application/json");
            request.Content = content;

            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SetEmail(string userName, string newemail, string token)
        {
            SetEmail model = new SetEmail() { Username = userName, NewEmail=newemail, Token=token};

            HttpClientHandler httpClientHandler;
            #if (DEBUG)
            httpClientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            #else
                            httpClientHandler = new HttpClientHandler(); "#endif using (var client = new HttpClient(httpHandler));
            #endif


            var client = new HttpClient(httpClientHandler);
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://10.0.2.2:5001/api/changeEmail");
            request.Method = HttpMethod.Post;

            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");
            request.Headers.Add("Authorization", "Bearer " + token);

            var bodyString = "{    \"username\": \"" + userName + "\",    \"newEmail\": \"" + newemail + "\"}";
            var content = new StringContent(bodyString, Encoding.UTF8, "application/json");
            request.Content = content;

            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUserAcc(string userName, string password, string token)
        {
            DeleteUser model = new DeleteUser() { Username = userName, Password=password, Token = token };

            HttpClientHandler httpClientHandler;
            #if (DEBUG)
            httpClientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            #else
                            httpClientHandler = new HttpClientHandler(); "#endif using (var client = new HttpClient(httpHandler));
            #endif


            var client = new HttpClient(httpClientHandler);
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://10.0.2.2:5001/api/deleteAccount");
            request.Method = HttpMethod.Post;

            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");
            request.Headers.Add("Authorization", "Bearer " + token);

            var bodyString = "{    \"username\": \"" + userName + "\",    \"password\": \"" + password + "\"}";
            var content = new StringContent(bodyString, Encoding.UTF8, "application/json");
            request.Content = content;

            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode;
        }

        public async Task<string> SearchUnits(string searchSchlussel)
        {
            SearchUnits model = new SearchUnits() {SearchSchlussel=searchSchlussel };

            HttpClientHandler httpClientHandler;
            #if (DEBUG)
            httpClientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            #else
                httpClientHandler = new HttpClientHandler(); "#endif using (var client = new HttpClient(httpHandler));
            #endif



            var client = new HttpClient(httpClientHandler);
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://10.0.2.2:5001/api/feedback/searchPublicTu?search=" + searchSchlussel);
            request.Method = HttpMethod.Get;

            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");

            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<bool> CreateFeedback(string teachingUnitId, string userId, string stars, string comment, string token)
        {
            CreateFeedback model = new CreateFeedback() { TeachingUnitId = teachingUnitId, UserId = userId, Stars = stars, Comment=comment, Token=token };

            HttpClientHandler httpClientHandler;
            #if (DEBUG)
            httpClientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            #else
                            httpClientHandler = new HttpClientHandler(); "#endif using (var client = new HttpClient(httpHandler));
            #endif


            var client = new HttpClient(httpClientHandler);
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://10.0.2.2:5001/api/feedback/createFeedback");
            request.Method = HttpMethod.Post;

            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");
            request.Headers.Add("Authorization", "Bearer " + token);
            var bodyString = "{    \"teachingUnitId\": \"" + teachingUnitId + "\",    \"userId\": \"" + userId + "\",    \"stars\": \"" + stars + "\",    \"comment\": \"" + comment + "\"}";
            var content = new StringContent(bodyString, Encoding.UTF8, "application/json");
            request.Content = content;

            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateUnit(string userId, string title, string subject, string description, string subscriptionKey, string token)
        {
            CreateTeachUnit model = new CreateTeachUnit() {UserId=userId, Title=title, Subject=subject, Description=description, SubscriptionKey=subject, Token=token};

            HttpClientHandler httpClientHandler;
            #if (DEBUG)
            httpClientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            #else
                            httpClientHandler = new HttpClientHandler(); "#endif using (var client = new HttpClient(httpHandler));
            #endif


            var client = new HttpClient(httpClientHandler);
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://10.0.2.2:5001/api/feedback/createTU");
            request.Method = HttpMethod.Post;

            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");
            request.Headers.Add("Authorization", "Bearer " + token);

            var bodyString = "{    \"userId\": \"" + userId + "\",    \"title\": \"" + title + "\",    \"isPublic\": true,    \"subject\": \"" + subject + "\",    \"description\": \"" + description + "\",    \"dateString\": null,    \"expiryDateString\": null,    \"subscriptionKey\": \"" + subscriptionKey + "\"}";
            var content = new StringContent(bodyString, Encoding.UTF8, "application/json");
            request.Content = content;

            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode;
        }

        public async Task<string> GetUnitsByUserId(string token, int userId)
        {
            GetUnitsByUserId model = new GetUnitsByUserId() { Token = token, UserId = userId };

            HttpClientHandler httpClientHandler;
            #if (DEBUG)
            httpClientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            #else
                            httpClientHandler = new HttpClientHandler(); "#endif using (var client = new HttpClient(httpHandler));
            #endif

            var client = new HttpClient(httpClientHandler);
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://10.0.2.2:5001/api/feedback/getUserTU?id=" + userId);
            request.Method = HttpMethod.Get;

            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");
            request.Headers.Add("Authorization", "Bearer " + token);

            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<string> GetUserStatistic(string token, int userId)
        {
            GetUserStatistik model = new GetUserStatistik() {Token=token, UserId=userId};

            HttpClientHandler httpClientHandler;
            #if (DEBUG)
            httpClientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            #else
                httpClientHandler = new HttpClientHandler(); "#endif using (var client = new HttpClient(httpHandler));
            #endif



            var client = new HttpClient(httpClientHandler);
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://10.0.2.2:5001/api/statistic/userStats?userId=1");
            request.Method = HttpMethod.Get;

            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");
            request.Headers.Add("Authorization", "Bearer " + token);

            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
