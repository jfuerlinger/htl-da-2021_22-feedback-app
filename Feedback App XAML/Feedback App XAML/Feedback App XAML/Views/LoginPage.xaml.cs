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
        private async void ButtonAnmelden_Clicked(object sender, EventArgs e)
        {
            LoginService serviceData = new LoginService();
            var getData = await serviceData.GetData(EntryUsername.Text, EntryPassword.Text);

            var items = getData.Split('"');
            string token = items[3];
            string expiration = items[7];
            string userId = items[10];
            string identityId = items[13];
            string role = items[17];
            string username = items[21];
            string email = items[25];

            userId = userId.Replace(":", "");
            userId = userId.Replace(",", "");


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

                getGetUserData = getGetUserData.Replace('"',' ');
                getGetUserData = getGetUserData.Replace(" ", "");

                var datas = getGetUserData.Split(',');
                string title = getGetUserData.Split(':')[0].Split(',')[0];
                string firstName = getGetUserData.Split(':')[2].Split(',')[0];
                string lastName = getGetUserData.Split(':')[3].Split(',')[0];
                string birthdate = getGetUserData.Split(':')[4].Split(',')[0];
                string school = getGetUserData.Split(':')[5].Split('}')[0];

                Application.Current.Properties["title"] = title;
                Application.Current.Properties["firstName"] = firstName;
                Application.Current.Properties["lastName"] = lastName;
                Application.Current.Properties["birthdate"] = birthdate;
                Application.Current.Properties["school"] = school;
            }
            else { await DisplayAlert("Error!", "Benutzer anmelden fehlgeschlagen! Bitte Eingaben überprüfen und erneut versuchen.", "Okay"); }
        }
        private async void ButtonRegistrierung_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registrierung());
        }
    }
}