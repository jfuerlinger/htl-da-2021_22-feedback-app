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
    public partial class HomePageLehrer : ContentPage
    {
        public HomePageLehrer()
        {
            InitializeComponent();
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
        }
    }
}