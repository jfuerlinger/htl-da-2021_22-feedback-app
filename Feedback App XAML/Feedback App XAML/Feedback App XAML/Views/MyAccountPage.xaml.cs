using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Feedback_App_XAML.ServicesHandler;
using Feedback_App_XAML.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace Feedback_App_XAML.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAccountPage : ContentPage
    {
        public MyAccountPage()
        {
            InitializeComponent();
            //BindingContext = this;
            //MainListView.ItemsSource = names;

            var username = Application.Current.Properties["username"].ToString();
            var email = Application.Current.Properties["email"].ToString();

            var userData = new List<string>();
            userData.Add(username);
            userData.Add(email);
            MainListView.ItemsSource=userData;
        }

        private async void btnPopUpEmail_Clicked(object sender, EventArgs e)
        {
            var result = await DisplayPromptAsync("Daten ändern!", "Neue Email-Adresse");
            Application.Current.Properties["email"] = result;

            var username = Application.Current.Properties["username"].ToString();
            var email = Application.Current.Properties["email"].ToString();
            var name = Application.Current.Properties["name"].ToString();
            var schule = Application.Current.Properties["schule"].ToString();

            var userData = new List<string>();
            userData.Add(username);
            userData.Add(email);
            userData.Add(name);
            userData.Add(schule);
            MainListView.ItemsSource = userData;
        }

        private async void btnPopUpName_Clicked(object sender, EventArgs e)
        {
            var result = await DisplayPromptAsync("Name einfügen!", "Name eingeben");
            Application.Current.Properties["name"] = result;

            var username = Application.Current.Properties["username"].ToString();
            var email = Application.Current.Properties["email"].ToString();
            var name = Application.Current.Properties["name"].ToString();

            var userData = new List<string>();
            userData.Add(username);
            userData.Add(email);
            userData.Add(name);
            MainListView.ItemsSource = userData;

            Button btn = (Button)sender;
            btn.IsVisible = false;

            btnPopUpSchool.IsEnabled = true;
        }

        private async void btnPopUpSchool_Clicked(object sender, EventArgs e)
        {
            var result = await DisplayPromptAsync("Schule einfügen!", "Schule eingeben");
            Application.Current.Properties["schule"] = result;

            var username = Application.Current.Properties["username"].ToString();
            var email = Application.Current.Properties["email"].ToString();
            var name = Application.Current.Properties["name"].ToString();
            var schule = Application.Current.Properties["schule"].ToString();

            var userData = new List<string>();
            userData.Add(username);
            userData.Add(email);
            userData.Add(name);
            userData.Add(schule);
            MainListView.ItemsSource = userData;

            Button btn = (Button)sender;
            btn.IsVisible = false;

            btnPopUpEmail.IsEnabled = true;
        }

        private void btnDeleteAcc_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Success", "All Vaues stored", "OK");
        }

        private void btnPopUpPassword_Clicked(object sender, EventArgs e)
        {

        }
    }
}