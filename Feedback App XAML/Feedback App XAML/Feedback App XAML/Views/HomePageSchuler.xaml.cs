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
    public partial class HomePageSchuler : ContentPage
    {
        public HomePageSchuler()
        {
            NavigationPage.SetHasBackButton(this, false);
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


        private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            var searchSchlussel = SearchBar.Text;

            LoginService servicesearchUnits = new LoginService();
            var serachUnitsDataJson = await servicesearchUnits.SearchUnits(searchSchlussel);

            try
            {
                var result = JsonConvert.DeserializeObject<List<AllUnit_FeedbackData>>(serachUnitsDataJson);

                if(result != null)
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
            BtnCreateFeedback.IsEnabled = true;
        }

        private async void BtnCreateFeedback_Clicked(object sender, EventArgs e)
        {
            var starsinput = await DisplayPromptAsync("Feedback erstellen!", "Stars eingeben (von 1 bis 5)", keyboard:Keyboard.Numeric);
            if (starsinput is null)
            {
                return;
            }
            else
            {
                var comment = await DisplayPromptAsync("Feedback erstellen!", "Komentar eingeben");
                if (comment is null)
                {
                    return;
                }
                else
                {
                    var stars = Convert.ToInt32(starsinput);
                    var token = Application.Current.Properties["token"].ToString();
                    var userId = (int)Application.Current.Properties["userId"];
                    var teachingUnitId = (int)Application.Current.Properties["idUnit"];

                    LoginService serviceCreateFeedback = new LoginService();
                    var setfeedback = await serviceCreateFeedback.CreateFeedback(teachingUnitId, userId, stars, comment, token);

                    if (setfeedback is true)
                    {
                        BtnCreateFeedback.IsEnabled = false;
                        await DisplayAlert("Success!", "Feedback wurde erfolgreich erstellt.", "Okay");
                    }
                    else
                    {
                        await DisplayAlert("Error!", "Bitte wiederholen.", "Okay");
                    }
                }
            }
        }
    }
}