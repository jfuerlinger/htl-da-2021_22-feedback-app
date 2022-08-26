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
            ListViewUserData();
        }

        public void ListViewUserData()
        {
            var username = Application.Current.Properties["username"];
            var email = Application.Current.Properties["email"];
            var firstName = Application.Current.Properties["firstName"];
            var lastName = Application.Current.Properties["lastName"];
            var school = Application.Current.Properties["school"]; 

            var userData = new List<string>();
            userData.Add("Username: " + username);
            userData.Add("Email: " + email);
            userData.Add("Name: " + firstName + " " + lastName);
            userData.Add("Schule: " + school);

            MainListView.ItemsSource = userData;
        }

        private async void btnPopUpEmail_Clicked(object sender, EventArgs e)
        {
            var newemail = await DisplayPromptAsync("Email einfügen!", "Neue Email eingeben", keyboard: Keyboard.Email);

            if (newemail is null)
            {
                ListViewUserData();
            }
            else
            {
                if (newemail.Length == 0)
                {
                    await DisplayAlert("Error!", "Bitte wiederholen.", "Okay");
                }
                else
                {
                    var token = Application.Current.Properties["token"].ToString();
                    var username = Application.Current.Properties["username"].ToString();


                    LoginService serviceSetEmail = new LoginService();
                    var setEmail = await serviceSetEmail.SetEmail(username, newemail, token);

                    if (setEmail is true)
                    {
                        await DisplayAlert("Success!", "Die User Daten wurden erfolgreich aktualisiert.", "Okay");
                        Application.Current.Properties["email"] = newemail;
                    }
                }
                ListViewUserData();
            }
        }

        private async void btnPopUpName_Clicked(object sender, EventArgs e)
        {
            var input = await DisplayPromptAsync("Vor- Nachname einfügen!", "Vorname und Nachname eingeben", keyboard:Keyboard.Text);

            if (input is null)
            {
                return;
            }
            else
            {
                if (input.Length == 0)
                {
                    await DisplayAlert("Error!", "Bitte wiederholen.", "Okay");
                }
                else
                {
                    var firstNameInput = input.Split(' ')[0];
                    var lastNameInput = string.Empty;

                    var test = input.Split(' ').Length;

                    if (input.Split(' ').Length == 2)
                        lastNameInput = input.Split(' ')[1];
                    else
                        lastNameInput = null;

                    var token = Application.Current.Properties["token"].ToString();
                    var identityId = Application.Current.Properties["identityId"].ToString();

                    if(Application.Current.Properties["school"] is null)
                    {
                        Application.Current.Properties["school"] = String.Empty;
                    }
                    
                    var school = Application.Current.Properties["school"].ToString();
                    
                    LoginService serviceSetData = new LoginService();
                    var setName = await serviceSetData.SetData(firstNameInput, lastNameInput, token, identityId, school);

                    if (setName is true)
                    {
                            await DisplayAlert("Success!", "Die User Daten wurden erfolgreich aktualisiert.", "Okay");
                            Application.Current.Properties["firstName"] = firstNameInput;
                            Application.Current.Properties["lastName"] = lastNameInput;
                    }
                    
                }
                ListViewUserData();
            }
        }

        private async void btnPopUpSchool_Clicked(object sender, EventArgs e)
        {
            //var quantity = await DisplayPromptAsync("Quantity",
            //"How many?",
            //initialValue: "1",
            //maxLength: 2,
            //keyboard: Keyboard.Numeric,
            //accept: "OK",
            //cancel: "Cancel");

            var token = Application.Current.Properties["token"].ToString();
            var identityId = Application.Current.Properties["identityId"].ToString();

            if (Application.Current.Properties["firstName"] is null)
            {
                Application.Current.Properties["firstName"] = String.Empty;
            }
            if (Application.Current.Properties["lastName"] is null)
            {
                Application.Current.Properties["lastName"] = String.Empty;
            }

            var firstName = Application.Current.Properties["firstName"].ToString();
            var lastName = Application.Current.Properties["lastName"].ToString();

            var inputSchool = await DisplayPromptAsync("Schule einfügen!", "Schulename eingeben");

            if (inputSchool is null)
            {
                return;
            }
            else
            {
                LoginService serviceSetData = new LoginService();

                var setSchool = await serviceSetData.SetData(firstName, lastName, token, identityId, inputSchool);

                if (setSchool is true)
                {
                    await DisplayAlert("Success!", "Die User Daten wurden erfolgreich aktualisiert.", "Okay");
                    Application.Current.Properties["school"] = inputSchool;
                }
                else
                {
                    await DisplayAlert("Error!", "Bitte wiederholen.", "Okay");
                }

                ListViewUserData();
            }
        }

        private async void btnDeleteAcc_Clicked(object sender, EventArgs e)
        {
            var password = await DisplayPromptAsync("Konto löschen!", "Password eingeben");

            if (password is null)
            {
                return;
            }
            else
            {
                if (password.Length == 0)
                {
                    await DisplayAlert("Error!", "Bitte wiederholen.", "Okay");
                }
                else
                {
                    var token = Application.Current.Properties["token"].ToString();
                    var username = Application.Current.Properties["username"].ToString();


                    LoginService serviceDeleteUserAcc = new LoginService();
                    var setEmail = await serviceDeleteUserAcc.SetEmail(username, password, token);

                    if (setEmail is true)
                    {
                        await DisplayAlert("Success!", "Konto wurde erfolgreich gelöscht.", "Okay");
                        await Navigation.PushAsync(new LoginPage());
                    }
                }
            }
        }

        private async void btnPopUpPassword_Clicked(object sender, EventArgs e)
        {
            var inputpass = await DisplayPromptAsync("Password ändern!", "Altes Password eingeben");
            if (inputpass is null)
                return;
            else
            {
                var inputNewPass = await DisplayPromptAsync("Password ändern!", "Neues Password eingeben");
                if (inputNewPass is null)
                    return;
                else
                {
                    var userName = Application.Current.Properties["username"].ToString();

                    LoginService serviceSetData = new LoginService();
                    var setSchool = await serviceSetData.SetPass(userName, inputpass, inputNewPass);

                    if (setSchool is true)
                    {
                        await DisplayAlert("Success!", "Die User Daten wurden erfolgreich aktualisiert.", "Okay");
                    }
                    else
                    {
                        await DisplayAlert("Error!", "Bitte wiederholen. Die Daten sind ungenau oder entsprechen nicht den Kriterien. Das neue Passwort muss: (Min. 6 Zeichen, 1x Groß, 1x klein) enthalten.", "Okay");
                    }
                }
            }
        }
        private async void buttonMyStat_Clicked(object sender, EventArgs e)
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

            var role = Application.Current.Properties["role"].ToString();

            if(role is "student")
            {
                await DisplayAlert("Statistik!", "Sie haben " + createdFeedbacksCount + " Feedbaks gegeben.", "Okay");
            }
            else
            {
                await DisplayAlert("Statistik!", "Sie haben " + createdTeachingUnitsCount + " Einheiten erstellt. Ihr AVG ist: " + avgStars, "Okay");

            }
        }

        private async void btnAbmelden_Clicked(object sender, EventArgs e)
        {
            //NavigationPage.SetHasBackButton(this, false);

            await Navigation.PushAsync(new LoginPage());
        }
    }
}