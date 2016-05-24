using Paris_Saveur_UWP.Models;
using Paris_Saveur_UWP.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Paris_Saveur_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RecommendedRestaurantsPage : Page
    {
        public RecommendedRestaurantsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RefreshPage();
        }

        private void RefreshPage()
        {
            if (ConnectionContext.CheckNetworkConnection())
            {
                this.NoConnectionText.Visibility = Visibility.Collapsed;
                this.RecommendedRestaurantList.Visibility = Visibility.Visible;
                DownloadRecommendedRestaurant();
            }
            else
            {
                this.NoConnectionText.Visibility = Visibility.Visible;
                this.RecommendedRestaurantList.Visibility = Visibility.Collapsed;
            }
        }

        private async void DownloadRecommendedRestaurant()
        {
            LoadingRing.IsActive = true;
            LoadingRing.Visibility = Visibility.Visible;

            var result = await RestClient.getResponseStringFromUri(ConnectionContext.RecommendedRestaurants_API);
            RestaurantList list = new RestaurantList(result);
            foreach (Restaurant restaurant in list.Restaurant_list)
            {
                restaurant.SetupRestaurantModelToDisplay(this.BaseUri);
            }
            this.RecommendedRestaurantList.DataContext = list;

            LoadingRing.IsActive = false;
            LoadingRing.Visibility = Visibility.Collapsed;
        }
    }
}
