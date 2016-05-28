using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace Paris_Saveur_UWP.Models
{
    class RestaurantList
    {
        private ObservableCollection<Restaurant> _restaurantCollection = new ObservableCollection<Restaurant>();
        public ObservableCollection<Restaurant> RestaurantCollection
        {
            get { return _restaurantCollection; }
            set
            {
                _restaurantCollection = value;
            }
        }

        public RestaurantList(string jsonString)
        {
            loadMoreRestaurants(jsonString);
        }
        public RestaurantList() { }

        public void loadMoreRestaurants(string jsonString)
        {
            JsonObject json = JsonValue.Parse(jsonString).GetObject();
            JsonArray array = json.GetNamedArray("restaurant_list");
            for (int i = 0; i < array.Count; i++)
            {
                _restaurantCollection.Add(new Restaurant(array[i].GetObject()));
            }
        }

        public void clearRestaurantList()
        {
            this._restaurantCollection.Clear();
        }
    }
}
