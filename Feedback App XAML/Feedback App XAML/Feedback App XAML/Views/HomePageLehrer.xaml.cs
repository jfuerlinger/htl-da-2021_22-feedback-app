using Feedback_App_XAML.Models;
using Feedback_App_XAML.ServicesHandler;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Feedback_App_XAML.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageLehrer : ContentPage
    {
        public HomePageLehrer()
        {
            InitializeComponent();
        }

        public void ListViewUnits()
        {
            var titleUnit = Application.Current.Properties["titleUnit"].ToString();
            var subjectUnit = Application.Current.Properties["subjectUnit"].ToString();
            var descriptionUnit = Application.Current.Properties["descriptionUnit"].ToString();

            var userData = new List<string>();
            userData.Add("Titel: " + titleUnit);
            //userData.Add("Subject: " + subjectUnit);
            userData.Add("Beschreibung: " + descriptionUnit);
            MainListView.ItemsSource = userData;
        }

        private async void buttonMyAcc_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyAccountPage());
        }

        private async void btncreateUnit_Clicked(object sender, EventArgs e)
        {
            var title = await DisplayPromptAsync("Unit erstellen!", "Title eingeben");
            var subject = await DisplayPromptAsync("Unit erstellen!", "Subject eingeben");
            var description = await DisplayPromptAsync("Unit erstellen!", "Description eingeben");
            var subscriptionKey = await DisplayPromptAsync("Unit erstellen!", "SubscriptionKey eingeben");


            var token = Application.Current.Properties["token"].ToString();
            var userId = Application.Current.Properties["userId"].ToString();

            LoginService serviceCreateUnit = new LoginService();
            var setunit = await serviceCreateUnit.CreateUnit(userId, title, subject, description, subscriptionKey, token);

            if(setunit is true)
            {
                await DisplayAlert("Success!", "Unit wurde erfolgreich erstellt.", "Okay");
            }
            else
            {
                await DisplayAlert("Error!", "Bitte wiederholen.", "Okay");
            }
        }

        private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            var searchSchlussel = SearchBar.Text;

            LoginService servicesearchUnits = new LoginService();
            var serachUnitsDataJson = await servicesearchUnits.SearchUnits(searchSchlussel);


            try
            {
                var result = JsonConvert.DeserializeObject<List<AllUnit_FeedbackData>>(serachUnitsDataJson);

                if (result != null)
                {
                    var titleUnit = result[0].Title;
                    var idUnit = result[0].Id;
                    var subjectUnit = result[0].Subject;
                    var descriptionUnit = result[0].Description;

                    Application.Current.Properties["idUnit"] = idUnit;
                    Application.Current.Properties["titleUnit"] = titleUnit;
                    Application.Current.Properties["subjectUnit"] = subjectUnit;
                    Application.Current.Properties["descriptionUnit"] = descriptionUnit;
                }
            }
            catch
            {
                await DisplayAlert("Error!", "Bitte wiederholen.", "Okay");
            }

            ListViewUnits();
        }
    }
}