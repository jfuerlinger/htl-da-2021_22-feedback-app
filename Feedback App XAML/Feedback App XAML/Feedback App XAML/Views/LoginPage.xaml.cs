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
            LoginService services = new LoginService();
            var getLoginDetails = await services.CheckLoginIfExists(EntryUsername.Text, EntryPassword.Text);

            if (getLoginDetails is true)
            {
                try
                {
                    await SecureStorage.SetAsync("token", EntryPassword.Text);
                }
                catch (Exception ex)

                {

                }

                await DisplayAlert("Success!", "Benutzer erfolgreich angemeldet.", "Okay");
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("Error!", "Benutzer anmelden fehlgeschlagen! Bitte Eingaben überprüfen und erneut versuchen.", "Okay");
            }
        }

        private async void ButtonRegistrierung_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registrierung());
        }

    }
}