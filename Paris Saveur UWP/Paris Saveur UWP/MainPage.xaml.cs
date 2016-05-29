using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;


namespace Paris_Saveur_UWP
{

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void RecommendedGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(RecommendedRestaurantsPage));
        }

        private void NearByGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void HotGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(RestaurantListPage));
        }

        private void MyFavoritesGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void HotTagGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(HotTagsPage));
        }

        private void MetroGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(TransportStationsPage));
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
