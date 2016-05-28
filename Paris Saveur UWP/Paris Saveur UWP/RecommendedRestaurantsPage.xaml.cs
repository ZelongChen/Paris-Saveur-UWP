using Paris_Saveur_UWP.Models;
using Paris_Saveur_UWP.Tools;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Paris_Saveur_UWP
{

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
                restaurant.SetupRestaurantModelToDisplay();
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
