using System.ComponentModel;
using Windows.Data.Json;

namespace Paris_Saveur_UWP.Models
{
    class Comment : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int Pk { get; set; }
        private User _user;
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                NotifyPropertyChanged("User");
            }
        }
        public string Username { get; set; }
        public int Score { get; set; }
        public int Consumption { get; set; }
        public string CommentText { get; set; }
        public string RateDate { get; set; }
        public bool IsTrustable { get; set; }
        public bool IsSuspicious { get; set; }
        public int NumbersReplies { get; set; }
        public int NumbersAgreed { get; set; }
        public int NumbersDisagreed { get; set; }
        public int Popularity { get; set; }
        //TODO: Add reply comment
        public Comment Reply { get; set; }

        //public void convertDateToChinese()
        //{
        //    rate_date = rate_date.Substring(0, 4) + "年" + rate_date.Substring(5, 2) + "月" + rate_date.Substring(8, 2) + "日";
        //}

        public Comment(JsonObject json)
        {
            this.Pk = (int)json.GetNamedNumber("pk");
            this.User = new User(json.GetNamedObject("user"));
            this.Score = (int)json.GetNamedNumber("score");
            this.CommentText = json.GetNamedString("comment");
            this.RateDate = json.GetNamedString("rate_date");
            this.IsTrustable = json.GetNamedBoolean("is_trustable");
            this.IsSuspicious = json.GetNamedBoolean("is_suspicious");
            this.NumbersReplies = (int)json.GetNamedNumber("num_replies");
            this.NumbersAgreed = (int)json.GetNamedNumber("num_agreed");
            this.NumbersDisagreed = (int)json.GetNamedNumber("num_disagreed");
            this.Popularity = (int)json.GetNamedNumber("popularity");
        }

        public void SetupCommentModelToDisplay()
        {

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
