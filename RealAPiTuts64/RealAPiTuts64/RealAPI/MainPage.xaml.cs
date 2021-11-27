using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealAPI.Services;
using Xamarin.Forms;

namespace RealAPI
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

       async void Button_Clicked(System.Object sender, System.EventArgs e)
        {

            var content = await ApiServices.ServiceClientInstance.AuthenticateUserAsync(MyUserName.Text, MyUserPassword.Text);

            if(!string.IsNullOrEmpty(content.authenticationToken))
            {
                await Navigation.PushAsync(new DashboardPage());

            }
            else
            {
              await  App.Current.MainPage.DisplayAlert("Alert","Something Went Worng","Ok");

            }
     
        }
    }
}
