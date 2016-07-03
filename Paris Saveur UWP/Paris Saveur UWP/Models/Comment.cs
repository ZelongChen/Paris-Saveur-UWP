using System;
using System.ComponentModel;
using System.Globalization;
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
        public string Consumption { get; set; }
        public string CommentText { get; set; }
        public string RateDate { get; set; }
        public bool IsTrustable { get; set; }
        public bool IsSuspicious { get; set; }
        public int NumbersReplies { get; set; }
        public int NumbersAgreed { get; set; }
        public int NumbersDisagreed { get; set; }
        public int NumbersLikeToShow { get; set; }
        public int Popularity { get; set; }
        public string Star1 { get; set; }
        public string Star2 { get; set; }
        public string Star3 { get; set; }
        public string Star4 { get; set; }
        public string Star5 { get; set; }
        //TODO: Add reply comment
        public Comment Reply { get; set; }

        public Comment(JsonObject json)
        {
            this.Pk = (int)json.GetNamedNumber("pk");
            this.User = new User(json.GetNamedObject("user"));
            this.Score = (int)json.GetNamedNumber("score");
            SetupStars();
            if (json.ContainsKey("consumption"))
            {
                this.Consumption = "" + json.GetNamedNumber("consumption") + "€";
            }
            this.CommentText = json.GetNamedString("comment");
            this.RateDate = json.GetNamedString("rate_date");
            DateConversion();
            this.IsTrustable = json.GetNamedBoolean("is_trustable");
            this.IsSuspicious = json.GetNamedBoolean("is_suspicious");
            this.NumbersReplies = (int)json.GetNamedNumber("num_replies");
            this.NumbersAgreed = (int)json.GetNamedNumber("num_agreed");
            this.NumbersDisagreed = (int)json.GetNamedNumber("num_disagreed");
            this.NumbersLikeToShow = this.NumbersAgreed - this.NumbersDisagreed;
            this.Popularity = (int)json.GetNamedNumber("popularity");
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private const int SECOND = 1;
        private const int MINUTE = 60 * SECOND;
        private const int HOUR = 60 * MINUTE;
        private const int DAY = 24 * HOUR;
        private const int MONTH = 30 * DAY;

        private void DateConversion()
        {
            DateTime dateTime = Convert.ToDateTime(this.RateDate);
            var ts = new TimeSpan(DateTime.UtcNow.Ticks - dateTime.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                this.RateDate = ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            if (delta < 2 * MINUTE)
                this.RateDate = "a minute ago";

            if (delta < 45 * MINUTE)
                this.RateDate = ts.Minutes + " minutes ago";

            if (delta < 90 * MINUTE)
                this.RateDate = "an hour ago";

            if (delta < 24 * HOUR)
                this.RateDate = ts.Hours + " hours ago";

            if (delta < 48 * HOUR)
                this.RateDate = "yesterday";

            if (delta < 30 * DAY)
                this.RateDate = ts.Days + " days ago";

            if (delta >= 30 * DAY)
            {
                DateTime dt = DateTime.ParseExact(RateDate, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
                this.RateDate = dt.ToString("dd/MM/yyyy");
            }

        }

        private void SetupStars()
        {
            string halfStar = "\uE7C6";
            string star = "\uE735";
            string emptyStar = "\uE734";
            double ratingScore = this.Score;
            if (ratingScore == 0)
            {
                this.Star1 = emptyStar;
                this.Star2 = emptyStar;
                this.Star3 = emptyStar;
                this.Star4 = emptyStar;
                this.Star5 = emptyStar;
            }
            if (ratingScore > 0 && ratingScore < 1)
            {
                this.Star1 = halfStar;
                this.Star2 = emptyStar;
                this.Star3 = emptyStar;
                this.Star4 = emptyStar;
                this.Star5 = emptyStar;
            }
            if (ratingScore == 1)
            {
                this.Star1 = star;
                this.Star2 = emptyStar;
                this.Star3 = emptyStar;
                this.Star4 = emptyStar;
                this.Star5 = emptyStar;
            }
            if (ratingScore > 1 && ratingScore < 2)
            {
                this.Star1 = star;
                this.Star2 = halfStar;
                this.Star3 = emptyStar;
                this.Star4 = emptyStar;
                this.Star5 = emptyStar;
            }
            if (ratingScore == 2)
            {
                this.Star1 = star;
                this.Star2 = star;
                this.Star3 = emptyStar;
                this.Star4 = emptyStar;
                this.Star5 = emptyStar;
            }
            if (ratingScore > 2 && ratingScore < 3)
            {
                this.Star1 = star;
                this.Star2 = star;
                this.Star3 = halfStar;
                this.Star4 = emptyStar;
                this.Star5 = emptyStar;
            }
            if (ratingScore == 3)
            {
                this.Star1 = star;
                this.Star2 = star;
                this.Star3 = star;
                this.Star4 = emptyStar;
                this.Star5 = emptyStar;
            }
            if (ratingScore > 3 && ratingScore < 4)
            {
                this.Star1 = star;
                this.Star2 = star;
                this.Star3 = star;
                this.Star4 = halfStar;
                this.Star5 = emptyStar;
            }
            if (ratingScore == 4)
            {
                this.Star1 = star;
                this.Star2 = star;
                this.Star3 = star;
                this.Star4 = star;
                this.Star5 = emptyStar;
            }
            if (ratingScore > 4 && ratingScore < 5)
            {
                this.Star1 = star;
                this.Star2 = star;
                this.Star3 = star;
                this.Star4 = star;
                this.Star5 = halfStar;
            }
            if (ratingScore == 5)
            {
                this.Star1 = star;
                this.Star2 = star;
                this.Star3 = star;
                this.Star4 = star;
                this.Star5 = star;
            }
        }
    }
}
