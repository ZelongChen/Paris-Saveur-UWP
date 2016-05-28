using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Paris_Saveur_UWP.Tools
{
    class ConnectionContext
    {
        public const string BaseUrl = "http://www.vivelevendredi.com";
        public const string RestaurantListUrl = "http://www.vivelevendredi.com/restaurants/json";
        public const string HotRestaurantsUrl = RestaurantListUrl + "/list/?order=-";
        public const string TagRestaurantsUrl = RestaurantListUrl + "/list-by-tag/?tag_name=";
        public const string RecommendedRestaurantsUrl = RestaurantListUrl +  "/recommended/?order=-popularity&page=1";

        public static bool CheckNetworkConnection()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }


    }
}
