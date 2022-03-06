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
        public async Task<string> GetUserData(string token, string identityId)
        {
            var check = await _restClient.GetUserData(token, identityId);

            return check;
        }
        public async Task<bool> SetData(string firstName, string lastName, string token, string identityId, string school)
        {
            var check = await _restClient.SetDataUser(firstName, lastName, token, identityId, school);

            return check;
        }
        public async Task<bool> SetPass(string username, string password, string newPassword)
        {
            var check = await _restClient.SetPass(username, password, newPassword);

            return check;
        }
        public async Task<bool> SetEmail(string username, string newemail, string token)
        {
            var check = await _restClient.SetEmail(username, newemail, token);

            return check;
        }
        public async Task<bool> DeleteUserAcc(string username, string password, string token)
        {
            var check = await _restClient.DeleteUserAcc(username, password, token);

            return check;
        }
        public async Task<string> SearchUnits(string searchSchlussel)
        {
            var check = await _restClient.SearchUnits(searchSchlussel);

            return check;
        }
        public async Task<bool> CreateFeedback(string teachingUnitId, string userId, string stars, string comment, string token)
        {
            var check = await _restClient.CreateFeedback(teachingUnitId, userId, stars, comment, token);

            return check;
        }
        public async Task<bool> CreateUnit(string userId, string title, string subject, string description, string subscriptionKey, string token)
        {
            var check = await _restClient.CreateUnit(userId, title, subject, description, subscriptionKey, token);

            return check;
        }
    }
}
