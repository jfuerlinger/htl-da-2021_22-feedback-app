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
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void Button_TestDaten_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Token!", "Token:", "Okay");
        }
    }
}