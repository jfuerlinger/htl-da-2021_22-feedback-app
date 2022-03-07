using Feedback_App_XAML.Models;
using Feedback_App_XAML.ServicesHandler;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Feedback_App_XAML.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http.Headers;
using Xamarin.Essentials;
using System.Collections.ObjectModel;
using System.IO;

namespace Feedback_App_XAML.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public async void GetUserData()
        {
            LoginService serviceData = new LoginService();
            var getData = await serviceData.GetData(EntryUsername.Text, EntryPassword.Text);

            AllUserData jsonToken = JsonConvert.DeserializeObject<AllUserData>(getData);
            string token = jsonToken.Token;
            AllUserData jsonExpiration = JsonConvert.DeserializeObject<AllUserData>(getData);
            string expiration = jsonExpiration.Expiration;
            AllUserData jsonUserId = JsonConvert.DeserializeObject<AllUserData>(getData);
            int userId = jsonUserId.UserId;
            AllUserData jsonIdentityId = JsonConvert.DeserializeObject<AllUserData>(getData);
            string identityId = jsonExpiration.IdentityId;
            AllUserData jsonRole = JsonConvert.DeserializeObject<AllUserData>(getData);
            string role = jsonRole.Role;
            AllUserData jsonUsername = JsonConvert.DeserializeObject<AllUserData>(getData);
            string username = jsonUsername.Username;
            AllUserData jsonEmail = JsonConvert.DeserializeObject<AllUserData>(getData);
            string email = jsonUsername.Email;

            Application.Current.Properties["token"] = token;
            Application.Current.Properties["expiration"] = expiration;
            Application.Current.Properties["userId"] = userId;
            Application.Current.Properties["identityId"] = identityId;
            Application.Current.Properties["role"] = role;
            Application.Current.Properties["username"] = username;
            Application.Current.Properties["email"] = email;
        }
        private async void ButtonAnmelden_Clicked(object sender, EventArgs e)
        {
            LoginService serviceData = new LoginService();
            var getData = await serviceData.GetData(EntryUsername.Text, EntryPassword.Text);

            AllUserData jsonToken = JsonConvert.DeserializeObject<AllUserData>(getData);
            string token = jsonToken.Token;
            AllUserData jsonExpiration = JsonConvert.DeserializeObject<AllUserData>(getData);
            string expiration = jsonExpiration.Expiration;
            AllUserData jsonUserId = JsonConvert.DeserializeObject<AllUserData>(getData);
            int userId = jsonUserId.UserId;
            AllUserData jsonIdentityId = JsonConvert.DeserializeObject<AllUserData>(getData);
            string identityId = jsonExpiration.IdentityId;
            AllUserData jsonRole = JsonConvert.DeserializeObject<AllUserData>(getData);
            string role = jsonRole.Role;
            AllUserData jsonUsername = JsonConvert.DeserializeObject<AllUserData>(getData);
            string username = jsonUsername.Username;
            AllUserData jsonEmail = JsonConvert.DeserializeObject<AllUserData>(getData);
            string email = jsonUsername.Email;

            Application.Current.Properties["token"] = token;
            Application.Current.Properties["expiration"] = expiration;
            Application.Current.Properties["userId"] = userId;
            Application.Current.Properties["identityId"] = identityId;
            Application.Current.Properties["role"] = role;
            Application.Current.Properties["username"] = username;
            Application.Current.Properties["email"] = email;

            LoginService services = new LoginService();
            var getLoginDetails = await services.CheckLoginIfExists(EntryUsername.Text, EntryPassword.Text);

            if (getLoginDetails is true)
            {
                await DisplayAlert("Success!", "Benutzer " + username + " erfolgreich angemeldet.", "Okay");

                if (role is "student") { await Navigation.PushAsync(new HomePageSchuler()); }
                else { await Navigation.PushAsync(new HomePageLehrer()); }

                LoginService serviceGetUserData = new LoginService();
                var getGetUserData = await serviceGetUserData.GetUserData(token, identityId);

                AllUserData jsonTitle = JsonConvert.DeserializeObject<AllUserData>(getGetUserData);
                string title = jsonTitle.Title;
                AllUserData jsonFirstName = JsonConvert.DeserializeObject<AllUserData>(getGetUserData);
                string firstName = jsonFirstName.FirstName;
                AllUserData jsonLastName = JsonConvert.DeserializeObject<AllUserData>(getGetUserData);
                string lastName = jsonLastName.LastName;
                AllUserData jsonBirthdate = JsonConvert.DeserializeObject<AllUserData>(getGetUserData);
                string birthdate = jsonBirthdate.Birthdate;
                AllUserData jsonSchool = JsonConvert.DeserializeObject<AllUserData>(getGetUserData);
                string school = jsonSchool.School;

                Application.Current.Properties["title"] = title;
                Application.Current.Properties["firstName"] = firstName;
                Application.Current.Properties["lastName"] = lastName;
                Application.Current.Properties["birthdate"] = birthdate;
                Application.Current.Properties["school"] = school;

                if(role is "teacher")
                {
                    LoginService serviceGetUnitsByUserId = new LoginService();
                    var unitsByUserId = await serviceGetUnitsByUserId.GetUnitsByUserId(token, userId);


                }
            }
            else { await DisplayAlert("Error!", "Benutzer anmelden fehlgeschlagen! Bitte Eingaben überprüfen und erneut versuchen.", "Okay"); }
        }
        private async void ButtonRegistrierung_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registrierung());
        }
    }
}