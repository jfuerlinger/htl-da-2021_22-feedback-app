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

namespace Feedback_App_XAML.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private string _getData;

        public string GetData()
        {
            return _getData;
        }

        private async void ButtonAnmelden_Clicked(object sender, EventArgs e)
        {
            LoginService serviceData = new LoginService();
            var getData = await serviceData.GetData(EntryUsername.Text, EntryPassword.Text);

            var items = getData.Split('"');
            string token = items[3];
            string expiration = items[7];
            string identityId = items[11];
            string role = items[15];
            string username = items[19];
            string email = items[23];

            Application.Current.Properties["token"] = token;
            Application.Current.Properties["expiration"] = expiration;
            Application.Current.Properties["identityId"] = identityId;
            Application.Current.Properties["role"] = role;
            Application.Current.Properties["username"] = username;
            Application.Current.Properties["email"] = email;

            UserModel model = new UserModel(username, email);
            string aa = model.Username;

            LoginService services = new LoginService();
            var getLoginDetails = await services.CheckLoginIfExists(EntryUsername.Text, EntryPassword.Text);

            if (getLoginDetails is true)
            {
                await DisplayAlert("Success!", "Benutzer " + username + " erfolgreich angemeldet.", "Okay");
                
                if (role is "student") { await Navigation.PushAsync(new HomePage()); }
                else { await Navigation.PushAsync(new HomePageLehrer()); }
            }
            else { await DisplayAlert("Error!", "Benutzer anmelden fehlgeschlagen! Bitte Eingaben überprüfen und erneut versuchen.", "Okay"); }
        }
        private async void ButtonRegistrierung_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registrierung());
        }
    }
}