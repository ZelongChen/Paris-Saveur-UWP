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
                ((TextBlock)(NoConnectionText ?? FindName("NoConnectionText"))).Visibility = Visibility.Collapsed;

                DownloadRecommendedRestaurant();
            }
            else
            {
                ((TextBlock)(NoConnectionText ?? FindName("NoConnectionText"))).Visibility = Visibility.Visible;
            }
        }

        private async void DownloadRecommendedRestaurant()
        {

            ((ProgressRing)(LoadingRing ?? FindName("LoadingRing"))).IsActive = true;
            ((ProgressRing)(LoadingRing ?? FindName("LoadingRing"))).Visibility = Visibility.Visible;

            var result = await RestClient.getResponseStringFromUri(ConnectionContext.RecommendedRestaurants_API);
            RestaurantList list = new RestaurantList(result);
            foreach (Restaurant restaurant in list.Restaurant_list)
            {
                restaurant.SetupRestaurantModelToDisplay(this.BaseUri);
            }
            ((ListView)(RecommendedRestaurantList ?? FindName("RecommendedRestaurantList"))).DataContext = list;
            ((GridView)(RecommendedRestaurantGridView ?? FindName("RecommendedRestaurantGridView"))).DataContext = list;

            ((ProgressRing)(LoadingRing ?? FindName("LoadingRing"))).IsActive = false;
            ((ProgressRing)(LoadingRing ?? FindName("LoadingRing"))).Visibility = Visibility.Collapsed;
        }

        private void BottomAppBar_Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshPage();
        }
    }
}
