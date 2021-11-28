﻿using Feedback_App_XAML.Models;
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
            if ((string.IsNullOrWhiteSpace(EntryUsername.Text)) ||
                (string.IsNullOrWhiteSpace(EntryPassword.Text)))
            {
                await DisplayAlert("Eingabefehler!", "Daten eingeben.", "Okay");
            };

            LoginService services = new LoginService();
            var getLoginDetails = await services.CheckLoginIfExists(EntryUsername.Text, EntryPassword.Text);

            if (getLoginDetails is true)
            {
                await DisplayAlert("Gratulation!", "Sie sind angemeldet.", "Okay");
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("Anmeldung fehlgeschlagen!", "Benutzername oder Passwort ist falsch oder existiert nicht.", "Okay");
            }
        }

        private async void ButtonRegistrierung_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registrierung());
        }

    }
}