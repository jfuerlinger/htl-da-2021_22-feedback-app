using Feedback_App_XAML.ServicesHandler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Feedback_App_XAML.Models
{
    public class UserModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        //string token = items[3];
        //string expiration = items[7];
        //string identityId = items[11];
        //string role = items[15];
        //string username = items[19];
        //string email = items[23];

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public UserModel() { }

        public UserModel(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }

        


        private string _infoText = "Throw in coin";

        public string InfoText
        {
            get { return _infoText; }
            set { _infoText = value; OnPropertyChanged(); }
        }


        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
