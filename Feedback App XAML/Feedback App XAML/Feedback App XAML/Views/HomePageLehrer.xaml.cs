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

            var userData = new List<string>();
            userData.Add("Title: " + titleUnit);
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

        private async void btnStatistic_Clicked(object sender, EventArgs e)
        {
            var token = Application.Current.Properties["token"].ToString();
            var userId = (int)Application.Current.Properties["userId"];

            LoginService serGetUserStat = new LoginService();
            var getStat = await serGetUserStat.GetUserStatistic(token, userId);

            AllUnit_FeedbackData jsonCreatedTeachingUnitsCount = JsonConvert.DeserializeObject<AllUnit_FeedbackData>(getStat);
            int createdTeachingUnitsCount = jsonCreatedTeachingUnitsCount.CreatedTeachingUnitsCount;

            AllUnit_FeedbackData jsonCreatedFeedbacksCount = JsonConvert.DeserializeObject<AllUnit_FeedbackData>(getStat);
            int createdFeedbacksCount = jsonCreatedFeedbacksCount.CreatedFeedbacksCount;

            AllUnit_FeedbackData jsonAvgStars = JsonConvert.DeserializeObject<AllUnit_FeedbackData>(getStat);
            double avgStars = jsonAvgStars.AvgStars;


            Application.Current.Properties["createdTeachingUnitsCount"] = createdTeachingUnitsCount;
            Application.Current.Properties["createdFeedbacksCount"] = createdFeedbacksCount;
            Application.Current.Properties["avgStars"] = avgStars;

            await DisplayAlert("Statistik!", "Sie haben " + createdTeachingUnitsCount + " Einheiten erstellt und " + createdFeedbacksCount + " Feedbaks gegeben. Ihr AVG ist: " + avgStars, "Okay");
        }
    }
}