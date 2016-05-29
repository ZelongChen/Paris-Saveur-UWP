using Paris_Saveur_UWP.Models;
using Paris_Saveur_UWP.Tools;
using System;
using Windows.Devices.Geolocation;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Paris_Saveur_UWP
{

    public sealed partial class NearByRestaurantPage : Page
    {
        private Geoposition _geoposition;
        private RestaurantList _list;

        public NearByRestaurantPage()
        {
            this.InitializeComponent();
            _list = new RestaurantList();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ConnectionContext.CheckNetworkConnection())
            {
                ((TextBlock)(this.NoConnectionText ?? FindName("NoConnectionText"))).Visibility = Visibility.Collapsed;

                if (e.Parameter == null)
                {
                    ((CommandBar)(this.BottomAppBar ?? FindName("BottomAppBar"))).Visibility = Visibility.Visible;
                    ((CommandBar)(this.TopAppBar ?? FindName("TopAppBar"))).Visibility = Visibility.Visible;

                    FindCurrentLocationAnRestaurantsNearby();
                }
                else
                {
                    ((CommandBar)(this.BottomAppBar ?? FindName("BottomAppBar"))).Visibility = Visibility.Collapsed;
                    ((CommandBar)(this.TopAppBar ?? FindName("TopAppBar"))).Visibility = Visibility.Collapsed;

                    var station = e.Parameter as TransportStation;
                    FindRestauransAroundStation(station);
                }

            }
            else
            {
                ((TextBlock)(this.NoConnectionText ?? FindName("NoConnectionText"))).Visibility = Visibility.Visible;
            }
        }

        private async void DownloadNearbyRestaurant(string latitude, string longitude)
        {
            var result = await RestClient.GetResponseStringFromUrl(ConnectionContext.NearbyRestaurantsUrl.Replace("latitude", latitude).Replace("longitude", longitude));
            _list.loadMoreRestaurants(result);

            if (_list.RestaurantCollection.Count == 0)
            {
                ((TextBlock)(this.NoRestaurantText ?? FindName("NoRestaurantText"))).Visibility = Visibility.Visible;
            }
            else
            {
                ((TextBlock)(this.NoRestaurantText ?? FindName("NoRestaurantText"))).Visibility = Visibility.Collapsed;
                foreach (Restaurant restaurant in _list.RestaurantCollection)
                {
                    restaurant.SetupRestaurantModelToDisplay();
                }
                if (((ListView)(this.RestaurantListView ?? FindName("RestaurantListView"))).DataContext == null)
                    ((ListView)(this.RestaurantListView ?? FindName("RestaurantListView"))).DataContext = _list;
                if (((GridView)(this.RestaurantGridView ?? FindName("RestaurantGridView"))).DataContext == null)
                    ((GridView)(this.RestaurantGridView ?? FindName("RestaurantGridView"))).DataContext = _list;
            }
            AfterLoading();
        }

        private void FindRestauransAroundStation(TransportStation station)
        {
            BeforeLoading();
            DownloadNearbyRestaurant(station.Latitude, station.Longitude);
        }

        private async void FindCurrentLocationAnRestaurantsNearby()
        {
            BeforeLoading();

            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;
            if (geolocator.LocationStatus == PositionStatus.Disabled)
            {
                AfterLoading();
                MessageBox(LocalizedStrings.Get("NearByRestaurantPage_LocationDisabled"));
            }
            else
            {
                try
                {
                    // Getting Current Location  
                    _geoposition = await geolocator.GetGeopositionAsync(
                        maximumAge: TimeSpan.FromMinutes(5),
                        timeout: TimeSpan.FromSeconds(10));
                    DownloadNearbyRestaurant("" + _geoposition.Coordinate.Point.Position.Latitude, "" + _geoposition.Coordinate.Point.Position.Longitude);
                }
                catch (UnauthorizedAccessException)
                {
                    AfterLoading();
                    MessageBox(LocalizedStrings.Get("NearByRestaurantPage_LocationDisabled"));
                }
            }
        }

        private void BeforeLoading()
        {
            ((ProgressRing)(this.LoadingRing ?? FindName("LoadingRing"))).Visibility = Visibility.Visible;
            ((ProgressRing)(this.LoadingRing ?? FindName("LoadingRing"))).IsActive = true;
            ((AppBarButton)(this.TopLaunchMapButton ?? FindName("TopLaunchMapButton"))).IsEnabled = false;
            ((AppBarButton)(this.TopRefreshButton ?? FindName("TopRefreshButton"))).IsEnabled = false;
            ((AppBarButton)(this.BottomLaunchMapButton ?? FindName("BottomLaunchMapButton"))).IsEnabled = false;
            ((AppBarButton)(this.BottomRefreshButton ?? FindName("BottomRefreshButton"))).IsEnabled = false;
        }

        private void AfterLoading()
        {
            ((ProgressRing)(this.LoadingRing ?? FindName("LoadingRing"))).Visibility = Visibility.Collapsed;
            ((ProgressRing)(this.LoadingRing ?? FindName("LoadingRing"))).IsActive = false;
            ((AppBarButton)(this.TopLaunchMapButton ?? FindName("TopLaunchMapButton"))).IsEnabled = true;
            ((AppBarButton)(this.TopRefreshButton ?? FindName("TopRefreshButton"))).IsEnabled = true;
            ((AppBarButton)(this.BottomLaunchMapButton ?? FindName("BottomLaunchMapButton"))).IsEnabled = true;
            ((AppBarButton)(this.BottomRefreshButton ?? FindName("BottomRefreshButton"))).IsEnabled = true;
        }

        // Custom Message Dialog Box  
        private async void MessageBox(string message)
        {
            var dialog = new MessageDialog(message.ToString());
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => await dialog.ShowAsync());
        }

        private async void LaunchMap_Click(object sender, RoutedEventArgs e)
        {
            string uriToLaunch = @"bingmaps:?cp=" + _geoposition.Coordinate.Point.Position.Latitude + "~" + _geoposition.Coordinate.Point.Position.Longitude + "&lvl=16";
            var uri = new Uri(uriToLaunch);
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectionContext.CheckNetworkConnection())
            {
                this.RestaurantsScrollViewer.ChangeView(null, 0d, null);
                FindCurrentLocationAnRestaurantsNearby();
            }
            else
            {
                ((TextBlock)(this.NoConnectionText ?? FindName("NoConnectionText"))).Visibility = Visibility.Visible;
            }
        }

        private void NearbyRestaurantList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
