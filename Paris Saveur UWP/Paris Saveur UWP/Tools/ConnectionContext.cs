﻿using System.Net.NetworkInformation;

namespace Paris_Saveur_UWP.Tools
{
    class ConnectionContext
    {
        public const string BaseUrl = "http://www.vivelevendredi.com";
        public const string RestaurantListUrl = "http://www.vivelevendredi.com/restaurants/json";
        public const string HotRestaurantsUrl = RestaurantListUrl + "/list/?order=-";
        public const string TagRestaurantsUrl = RestaurantListUrl + "/list-by-tag/?tag_name=";
        public const string RecommendedRestaurantsUrl = RestaurantListUrl +  "/recommended/?order=-popularity&page=1";
        public const string HotTagsUrl = RestaurantListUrl + "/tag-cloud/";
        public const string NearbyRestaurantsUrl = RestaurantListUrl + "/list-by-location/?geo_lat=latitude&geo_lon=longitude&criterion=geopoint&order=-popularity&page=1";
        public const string RestaurantCommentsUrl = RestaurantListUrl + "/rating-list/restaurantPk/?page=pageToDownload";

        public static bool CheckNetworkConnection()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }


    }
}
