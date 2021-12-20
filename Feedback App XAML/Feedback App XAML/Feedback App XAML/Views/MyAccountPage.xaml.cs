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
        }

        List<string> names = new List<string>
        {
           "Angular 7.Sem",
           "Oracle SQL 7.Sem",
           "Xaml 7.Sem",
           "Recht 7.Sem",
           "Nvs 7.Sem",
           "Betriebsystem 5.Sem",
           "Syp 4.Sem"
        };

        private void ButtonSave_Clicked(object sender, EventArgs e)
        {

        }

        private void ButtonEdit_Clicked(object sender, EventArgs e)
        {

        }
    }
}