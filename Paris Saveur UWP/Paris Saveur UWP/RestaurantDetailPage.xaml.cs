using Paris_Saveur_UWP.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Paris_Saveur_UWP
{

    public sealed partial class RestaurantDetailPage : Page
    {
        private Restaurant _restaurant;

        public RestaurantDetailPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _restaurant = e.Parameter as Restaurant;
            this.DataContext = _restaurant;
            if (_restaurant.Description == null || _restaurant.Description.Length == 0)
            {
                this.RestaurantDescriptionGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void RestaurantAddressGrid_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

        }

        private void RestaurantTimeGrid_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

        }

        private void RestaurantCommentsGrid_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

        }
    }
}
