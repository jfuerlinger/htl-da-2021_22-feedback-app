﻿using System;
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
            MainListView.ItemsSource = names;
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

        private void MainSearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            var keyword = MainSearchBar.Text;
            MainListView.ItemsSource =
            names.Where(name => name.ToLower().Contains(keyword.ToLower()));
        }

        private async void ButtonMyAcc_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyAccountPage());
        }

    }
}