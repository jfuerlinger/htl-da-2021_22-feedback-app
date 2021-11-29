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
            //LoginModel model = new LoginModel() { Username="mirzet21", Password="Mirzet21+"};

            //HttpClientHandler httpClientHandler;
            //#if (DEBUG)
            //httpClientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true };
            //#else
            //    httpClientHandler = new HttpClientHandler(); "#endif using (var client = new HttpClient(httpHandler));
            //#endif

            //var client = new HttpClient(httpClientHandler);
            //var json = JsonConvert.SerializeObject(model);
            //var content = new StringContent(json, Encoding.UTF8, "application/json");
            //string debugContentJson = await content.ReadAsStringAsync();
            //var result = await client.PostAsync("https://10.0.2.2:5001/api/login", content).ConfigureAwait(false);

            //string responseString = await result.Content.ReadAsStringAsync();
            //TokenModel token = JsonConvert.DeserializeObject<TokenModel>(responseString);

            await DisplayAlert("Token!", "Token:", "Okay");
        }
    }
}