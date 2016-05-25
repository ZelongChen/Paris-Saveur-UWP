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
        public const string BASE_URL = "http://www.vivelevendredi.com";
        public const string RestaurantList_API = "http://www.vivelevendredi.com/restaurants/json";
        public const string RecommendedRestaurants_API = BASE_URL +  "/restaurants/json/recommended/?order=-popularity&page=1";

        public static bool CheckNetworkConnection()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }


    }
}
