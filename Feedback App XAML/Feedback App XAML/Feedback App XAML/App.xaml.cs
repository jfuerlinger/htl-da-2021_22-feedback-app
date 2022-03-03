using System;
using Xamarin.Forms;
using Feedback_App_XAML.Views;
using Xamarin.Forms.Xaml;
using Amazon.Util.Internal.PlatformServices;

namespace Feedback_App_XAML
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
