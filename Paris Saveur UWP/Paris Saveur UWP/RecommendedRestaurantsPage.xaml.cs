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
                ((TextBlock)(this.NoConnectionText ?? FindName("NoConnectionText"))).Visibility = Visibility.Collapsed;

                DownloadRecommendedRestaurant();
            }
            else
            {
                ((TextBlock)(this.NoConnectionText ?? FindName("NoConnectionText"))).Visibility = Visibility.Visible;
            }
        }

        private async void DownloadRecommendedRestaurant()
        {

            ((ProgressRing)(this.LoadingRing ?? FindName("LoadingRing"))).IsActive = true;
            ((ProgressRing)(this.LoadingRing ?? FindName("LoadingRing"))).Visibility = Visibility.Visible;

            var result = await RestClient.GetResponseStringFromUrl(ConnectionContext.RecommendedRestaurantsUrl);
            RestaurantList list = new RestaurantList(result);

            ((ListView)(this.RecommendedRestaurantList ?? FindName("RecommendedRestaurantList"))).DataContext = list;
            ((GridView)(this.RecommendedRestaurantGridView ?? FindName("RecommendedRestaurantGridView"))).DataContext = list;

            ((ProgressRing)(this.LoadingRing ?? FindName("LoadingRing"))).IsActive = false;
            ((ProgressRing)(this.LoadingRing ?? FindName("LoadingRing"))).Visibility = Visibility.Collapsed;
        }

        private void BottomAppBar_Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshPage();
        }

        private void RecommendedRestaurantList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Restaurant restaurant = e.AddedItems[0] as Restaurant;
            Frame.Navigate(typeof(RestaurantDetailPage), restaurant);
        }
    }
}
