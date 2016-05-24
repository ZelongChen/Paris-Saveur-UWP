using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paris_Saveur_UWP.Models
{
    class RestaurantList
    {
        private ObservableCollection<Restaurant> restaurant_list = new ObservableCollection<Restaurant>();
        public ObservableCollection<Restaurant> Restaurant_list
        {
            get { return restaurant_list; }
            set
            {
                restaurant_list = value;
            }
        }

        public RestaurantList(string jsonString)
        {

        }
    }
}
