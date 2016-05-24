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
    class Restaurant : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public int pk { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string thumbnail { get; set; }
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
        public double consumption_per_capita { get; set; }
        public bool is_ad_ranking { get; set; }
        public List<String> tag_list { get; set; }
        //public LatestRating latest_rating { get; set; }
        private BitmapImage thumbnailBitmap;
        public BitmapImage star1 { get; set; }
        public BitmapImage star2 { get; set; }
        public BitmapImage star3 { get; set; }
        public BitmapImage star4 { get; set; }
        public BitmapImage star5 { get; set; }

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

        public Restaurant(string jsonString)
        {
            JsonObject json = JsonValue.Parse(jsonString).GetObject();
            this.pk = (int)json.GetNamedNumber("pk");
            this.name = json.GetNamedString("name");
            this.description = json.GetNamedString("description");
            this.thumbnail = json.GetNamedString("thumbnail");
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
            this.consumption_per_capita = json.GetNamedNumber("consumption_per_capita");
            this.is_ad_ranking = json.GetNamedBoolean("is_ad_ranking");
            
            int size = json.GetNamedArray("tag_list").Count();
            for (int i = 0; i < size; i++)
            {
                IJsonValue element = json.GetNamedArray("tag_list")[i];
                this.tag_list.Add(element.GetString());
            }

        }
    }
}
