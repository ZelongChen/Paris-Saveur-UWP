using Paris_Saveur_UWP.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.UI.Xaml.Media.Imaging;

namespace Paris_Saveur_UWP.Models
{
    class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string AvatarUrl { get; set; }
        public string Biography { get; set; }
        public int CreditRating { get; set; }
        public int Karma { get; set; }
        public int NumbersFollowing { get; set; }
        public int NumbersFollowers { get; set; }

        public User(JsonObject json)
        {
            this.UserName = json.GetNamedString("username");
            this.DisplayName = json.GetNamedString("display_name");
            var avatarUrl = json.GetNamedString("avatar_url");
            if (avatarUrl.Contains("http://www.gravatar.com"))
            {
                this.AvatarUrl = avatarUrl;
            }
            else
            {
                this.AvatarUrl = ConnectionContext.BaseUrl + avatarUrl;
            }
            this.Biography = json.GetNamedString("biography");
            this.CreditRating = (int)json.GetNamedNumber("credit_rating");
            this.Karma = (int)json.GetNamedNumber("karma");
            this.NumbersFollowing = (int)json.GetNamedNumber("num_following");
            this.NumbersFollowers = (int)json.GetNamedNumber("num_followers");
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
