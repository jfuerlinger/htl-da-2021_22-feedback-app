using Feedback_App_XAML.ServicesHandler;
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
    public partial class HomePageSchuler : ContentPage
    {
        public HomePageSchuler()
        {
            InitializeComponent();
        }

        public void ListViewUserData()
        {
            var titleUnit = Application.Current.Properties["titleUnit"].ToString();
            var subjectUnit = Application.Current.Properties["subjectUnit"].ToString();
            var descriptionUnit = Application.Current.Properties["descriptionUnit"].ToString();

            var userData = new List<string>();
            userData.Add("Title: " + titleUnit);
            userData.Add("Subject: " + subjectUnit);
            userData.Add("Description: " + descriptionUnit);
            MainListView.ItemsSource = userData;
        }

        private async void buttonMyAcc_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyAccountPage());
        }

        private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            var searchSchlussel = SearchBar.Text;

            LoginService servicesearchUnits = new LoginService();
            var serachUnitsData = await servicesearchUnits.SearchUnits(searchSchlussel);

            serachUnitsData = serachUnitsData.Replace('"', ' ');
            serachUnitsData = serachUnitsData.Replace(" ", "");

            var datas = serachUnitsData.Split(',');
            string idUnit = serachUnitsData.Split(':')[1].Split(',')[0];
            string titleUnit = serachUnitsData.Split(':')[3].Split(',')[0];
            string subjectUnit = serachUnitsData.Split(':')[4].Split(',')[0];
            string descriptionUnit = serachUnitsData.Split(':')[5].Split(',')[0];

            Application.Current.Properties["idUnit"] = idUnit;
            Application.Current.Properties["titleUnit"] = titleUnit;
            Application.Current.Properties["subjectUnit"] = subjectUnit;
            Application.Current.Properties["descriptionUnit"] = descriptionUnit;

            if(searchSchlussel == titleUnit)
            {
                ListViewUserData();
                BtnCreateFeedback.IsEnabled = true;

            }
            else
            {
                await DisplayAlert("Error!", "Bitte wiederholen. Die Daten sind ungenau oder Unit ist noch nicht erstellt.", "Okay");
            }
        }

        private async void BtnCreateFeedback_Clicked(object sender, EventArgs e)
        {
            var stars = await DisplayPromptAsync("Feedback erstellen!", "Stars eingeben (von 1 bis 5)");
            var comment = await DisplayPromptAsync("Feedback erstellen!", "Komentar eingeben");

            var token = Application.Current.Properties["token"].ToString();
            var userId = Application.Current.Properties["userId"].ToString();
            var teachingUnitId = Application.Current.Properties["idUnit"].ToString();

            LoginService serviceCreateFeedback = new LoginService();
            var setfeedback = await serviceCreateFeedback.CreateFeedback(teachingUnitId, userId, stars, comment, token);

            if(setfeedback is true)
            {
                BtnCreateFeedback.IsEnabled = false;
            }
        }
    }
}