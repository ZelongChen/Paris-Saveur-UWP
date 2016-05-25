using Paris_Saveur_UWP.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.UI.Xaml.Media.Imaging;

namespace Paris_Saveur_UWP.Models
{
    class Restaurant : INotifyPropertyChanged
    {
        public static BitmapImage thumbnail_placeholder;
        public static string RestaurantModel_CommentNumbers_Globalization = LocalizedStrings.Get("RestaurantModel_CommentNumbers") + ")";

        public event PropertyChangedEventHandler PropertyChanged;

        public int pk { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string thumbnail_url { get; set; }
        public string style { get; set; }
        public string address { get; set; }
        public double geo_lat { get; set; }
        public double geo_lon { get; set; }
        public string public_transit { get; set; }
        public string phone_number_1 { get; set; }
        public string phone_number_2 { get; set; }
        public string opening_hours { get; set; }
        public string website { get; set; }
        public bool is_closed { get; set; }
        public bool is_locked { get; set; }
        public string url_path { get; set; }
        public int rating_num { get; set; }
        public double rating_score { get; set; }
        public string RatingScoreAndReviewNum { get; set; }
        public int consumption_num { get; set; }
        public string consumption_per_capita { get; set; }
        public bool is_ad_ranking { get; set; }
        public List<String> tag_list { get; set; }
        //public LatestRating latest_rating { get; set; }
        private BitmapImage thumbnailBitmap;
        public string star1 { get; set; }
        public string star2 { get; set; }
        public string star3 { get; set; }
        public string star4 { get; set; }
        public string star5 { get; set; }

        public BitmapImage ThumbnailBitmap
        {
            get { return thumbnailBitmap; }
            set
            {
                thumbnailBitmap = value;
                NotifyPropertyChanged("ThumbnailBitmap");
            }
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Restaurant(JsonObject json)
        {

            this.pk = (int)json.GetNamedNumber("pk");
            this.name = json.GetNamedString("name");
            this.description = json.GetNamedString("description");
            this.thumbnail_url = json.GetNamedString("thumbnail");
            this.style = json.GetNamedString("style");
            this.address = json.GetNamedString("address");
            this.geo_lat = json.GetNamedNumber("geo_lat");
            this.geo_lon = json.GetNamedNumber("geo_lon");
            this.public_transit = json.GetNamedString("public_transit");
            this.phone_number_1 = json.GetNamedString("phone_number_1");
            this.phone_number_2 = json.GetNamedString("phone_number_2");
            this.opening_hours = json.GetNamedString("opening_hours");
            this.website = json.GetNamedString("website");
            this.is_closed = json.GetNamedBoolean("is_closed");
            this.is_locked = json.GetNamedBoolean("is_locked");
            this.url_path = json.GetNamedString("url_path");
            this.rating_num = (int)json.GetNamedNumber("rating_num");
            this.rating_score = json.GetNamedNumber("rating_score");
            this.consumption_num = (int)json.GetNamedNumber("consumption_num");
            this.consumption_per_capita = json.GetNamedNumber("consumption_per_capita").ToString() + "€";
            this.is_ad_ranking = json.GetNamedBoolean("is_ad_ranking");
            
            int size = json.GetNamedArray("tag_list").Count();
            this.tag_list = new List<string>();
            for (int i = 0; i < size; i++)
            {
                IJsonValue element = json.GetNamedArray("tag_list")[i];
                this.tag_list.Add(element.GetString());
            }

        }

        public void SetupRestaurantModelToDisplay(Uri baseUri)
        {
            this.ShowReviewScoreAndNumber();
            this.SetupThumbnail(baseUri);
            this.SetupStars();
            ImageDownloader.DownloadImageIntoImage(this);
        }

        private void ShowReviewScoreAndNumber()
        {
            this.RatingScoreAndReviewNum = this.rating_score + " (" + rating_num + " " + RestaurantModel_CommentNumbers_Globalization;

        }

        private void SetupThumbnail(Uri baseUri)
        {
            if (thumbnail_placeholder == null)
            {
                thumbnail_placeholder = new BitmapImage(new Uri(baseUri, "Assets/Images/restaurant_thumbnail_placeholder.jpg"));
            }
            this.ThumbnailBitmap = thumbnail_placeholder;
        }

        private void SetupStars()
        {
            string halfStar = "\uE7C6";
            string star = "\uE735";
            string emptyStar = "\uE734";
            double ratingScore = this.rating_score;
            if (ratingScore == 0)
            {
                this.star1 = emptyStar;
                this.star2 = emptyStar;
                this.star3 = emptyStar;
                this.star4 = emptyStar;
                this.star5 = emptyStar;
            }
            if (ratingScore > 0 && ratingScore < 1)
            {
                this.star1 = halfStar;
                this.star2 = emptyStar;
                this.star3 = emptyStar;
                this.star4 = emptyStar;
                this.star5 = emptyStar;
            }
            if (ratingScore == 1)
            {
                this.star1 = star;
                this.star2 = emptyStar;
                this.star3 = emptyStar;
                this.star4 = emptyStar;
                this.star5 = emptyStar;
            }
            if (ratingScore > 1 && ratingScore < 2)
            {
                this.star1 = star;
                this.star2 = halfStar;
                this.star3 = emptyStar;
                this.star4 = emptyStar;
                this.star5 = emptyStar;
            }
            if (ratingScore == 2)
            {
                this.star1 = star;
                this.star2 = star;
                this.star3 = emptyStar;
                this.star4 = emptyStar;
                this.star5 = emptyStar;
            }
            if (ratingScore > 2 && ratingScore < 3)
            {
                this.star1 = star;
                this.star2 = star;
                this.star3 = halfStar;
                this.star4 = emptyStar;
                this.star5 = emptyStar;
            }
            if (ratingScore == 3)
            {
                this.star1 = star;
                this.star2 = star;
                this.star3 = star;
                this.star4 = emptyStar;
                this.star5 = emptyStar;
            }
            if (ratingScore > 3 && ratingScore < 4)
            {
                this.star1 = star;
                this.star2 = star;
                this.star3 = star;
                this.star4 = halfStar;
                this.star5 = emptyStar;
            }
            if (ratingScore == 4)
            {
                this.star1 = star;
                this.star2 = star;
                this.star3 = star;
                this.star4 = star;
                this.star5 = emptyStar;
            }
            if (ratingScore > 4 && ratingScore < 5)
            {
                this.star1 = star;
                this.star2 = star;
                this.star3 = star;
                this.star4 = star;
                this.star5 = halfStar;
            }
            if (ratingScore == 5)
            {
                this.star1 = star;
                this.star2 = star;
                this.star3 = star;
                this.star4 = star;
                this.star5 = star;
            }
        }
    }
}
