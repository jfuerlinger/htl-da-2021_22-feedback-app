using Feedback_App_XAML.ServicesHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Feedback_App_XAML.RestClient;

namespace Feedback_App_XAML.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registrierung : ContentPage
    {
        public Registrierung()
        {
            InitializeComponent();
        }

        private async void ButtonRegistrierung_Clicked(object sender, EventArgs e)
        {
            if ((string.IsNullOrWhiteSpace(EntryUsername.Text)) ||
                (string.IsNullOrWhiteSpace(EntryEmail.Text)) ||
                (string.IsNullOrWhiteSpace(EntryPassword.Text)))
            {
                await DisplayAlert("Eingabefehler!", "Daten eingeben.", "Okay");
            }
            else
            {
                RegisterService service = new RegisterService();
                    var response = await service.CheckRegisterIfExists(EntryUsername.Text, EntryEmail.Text, EntryPassword.Text);
                
                if(response)
                {
                    await DisplayAlert("Gratulation!", "Sie sind registriert.", "Okay");
                }
            };

        }
    }
}