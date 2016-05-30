using Paris_Saveur_UWP.Tools;
using System.ComponentModel;
using System.Linq;
using Windows.Data.Json;

namespace Paris_Saveur_UWP.Models
{
    class Restaurant : INotifyPropertyChanged
    {
        public static string RestaurantModel_CommentNumbers_Globalization = LocalizedStrings.Get("RestaurantModel_CommentNumbers") + ")";

        public event PropertyChangedEventHandler PropertyChanged;

        public int Pk { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Style { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string PublicTransit { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string OpeningHours { get; set; }
        public string Website { get; set; }
        public bool IsClosed { get; set; }
        public bool IsLocked { get; set; }
        public string UrlPath { get; set; }
        public int RatingNumbers { get; set; }
        public double RatingScore { get; set; }
        public string RatingScoreAndReviewNumbers { get; set; }
        public int ConsumptionNumbers { get; set; }
        public string ConsumptionPerCapita { get; set; }
        public bool IsAdRanking { get; set; }
        public string Tags { get; set; }
        //public LatestRating latest_rating { get; set; }
        public string Star1 { get; set; }
        public string Star2 { get; set; }
        public string Star3 { get; set; }
        public string Star4 { get; set; }
        public string Star5 { get; set; }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Restaurant(JsonObject json)
        {

            this.Pk = (int)json.GetNamedNumber("pk");
            this.Name = json.GetNamedString("name");
            this.Description = json.GetNamedString("description");
            if (json.ContainsKey("thumbnail"))
            {
                this.ThumbnailUrl = json.GetNamedString("thumbnail");
            }
            this.Style = json.GetNamedString("style");
            this.Address = json.GetNamedString("address");
            this.Latitude = json.GetNamedNumber("geo_lat");
            this.Longitude = json.GetNamedNumber("geo_lon");
            this.PublicTransit = json.GetNamedString("public_transit");
            this.PhoneNumber1 = json.GetNamedString("phone_number_1");
            this.PhoneNumber2 = json.GetNamedString("phone_number_2");
            this.OpeningHours = json.GetNamedString("opening_hours");
            this.Website = json.GetNamedString("website");
            this.IsClosed = json.GetNamedBoolean("is_closed");
            this.IsLocked = json.GetNamedBoolean("is_locked");
            this.UrlPath = json.GetNamedString("url_path");
            this.RatingNumbers = (int)json.GetNamedNumber("rating_num");
            this.RatingScore = json.GetNamedNumber("rating_score");
            this.ConsumptionNumbers = (int)json.GetNamedNumber("consumption_num");
            this.ConsumptionPerCapita = json.GetNamedNumber("consumption_per_capita").ToString() + "€";
            this.IsAdRanking = json.GetNamedBoolean("is_ad_ranking");
            
            int size = json.GetNamedArray("tag_list").Count();
            for (int i = 0; i < size; i++)
            {
                IJsonValue element = json.GetNamedArray("tag_list")[i];
                this.Tags += " " + element.GetString();
            }

        }

        public void SetupRestaurantModelToDisplay()
        {
            this.ShowReviewScoreAndNumber();
            this.SetupThumbnail();
            this.SetupStars();
        }

        private void ShowReviewScoreAndNumber()
        {
            this.RatingScoreAndReviewNumbers = this.RatingScore + " (" + RatingNumbers + " " + RestaurantModel_CommentNumbers_Globalization;

        }

        private void SetupThumbnail()
        {
            if (this.ThumbnailUrl == null)
            {
                this.ThumbnailUrl = "Assets/Images/restaurant_thumbnail_placeholder.jpg";
            } else
            {
                this.ThumbnailUrl = ConnectionContext.BaseUrl + this.ThumbnailUrl;
            }

        }

        private void SetupStars()
        {
            string halfStar = "\uE7C6";
            string star = "\uE735";
            string emptyStar = "\uE734";
            double ratingScore = this.RatingScore;
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
