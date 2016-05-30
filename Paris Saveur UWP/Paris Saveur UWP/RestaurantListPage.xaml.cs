using Paris_Saveur_UWP.Models;
using Paris_Saveur_UWP.Tools;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace Paris_Saveur_UWP
{

    public sealed partial class RestaurantListPage : Page
    {

        private readonly string SortByPopularity = "popularity";
        private readonly string SortByRatingNumbers = "rating_num";
        private readonly string SortByRatingScore = "rating_score";

        private int _currentPage;
        private string _sortBy;
        private string _restaurantTag;
        private RestaurantList _list = new RestaurantList();

        private enum ListType
        {
            Recommended,
            Tag
        }

        public RestaurantListPage()
        {
            this.InitializeComponent();
            _currentPage = 1;
            _sortBy = SortByPopularity;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ConnectionContext.CheckNetworkConnection())
            {
                ((TextBlock)(this.NoConnectionText ?? FindName("NoConnectionText"))).Visibility = Visibility.Collapsed;

                var parameterReceived = e.Parameter;
                if (parameterReceived == null)
                {
                    this.PagerHeader.Label = LocalizedStrings.Get("HotRestaurantPage_Title");
                    DownloadRestaurants((int)ListType.Recommended, "", _currentPage++);
                }
                else
                {
                    _restaurantTag = parameterReceived as string;
                    this.PagerHeader.Label = _restaurantTag;
                    DownloadRestaurants((int)ListType.Tag, _restaurantTag, _currentPage++);
                }
            }
            else
            {
                ((TextBlock)(this.NoConnectionText ?? FindName("NoConnectionText"))).Visibility = Visibility.Visible;
            }
        }

        private async void DownloadRestaurants(int type, string keyword, int page)
        {
            ((ProgressRing)(this.LoadingRing ?? FindName("LoadingRing"))).IsActive = true;
            ((ProgressRing)(this.LoadingRing ?? FindName("LoadingRing"))).Visibility = Visibility.Visible;

            switch (type)
            {
                case (int)ListType.Recommended:
                    var resultRecommended = await RestClient.GetResponseStringFromUrl(ConnectionContext.HotRestaurantsUrl + _sortBy + "&page=" + page);
                    _list.loadMoreRestaurants(resultRecommended);
                    break;
                default:
                    var resultTag = await RestClient.GetResponseStringFromUrl(ConnectionContext.TagRestaurantsUrl + keyword + "&order=-" + _sortBy + "&page=" + page);
                    _list.loadMoreRestaurants(resultTag);
                    break;
            }

            if (((ListView)(this.RestaurantListView ?? FindName("RestaurantListView"))).DataContext == null)
                ((ListView)(this.RestaurantListView ?? FindName("RestaurantListView"))).DataContext = _list;
            if (((GridView)(this.RestaurantGridView ?? FindName("RestaurantGridView"))).DataContext == null)
                ((GridView)(this.RestaurantGridView ?? FindName("RestaurantGridView"))).DataContext = _list;
            ((ProgressRing)(this.LoadingRing ?? FindName("LoadingRing"))).IsActive = false;
            ((ProgressRing)(this.LoadingRing ?? FindName("LoadingRing"))).Visibility = Visibility.Collapsed;
        }

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            var verticalOffset = this.RestaurantsScrollViewer.VerticalOffset;
            var maxVerticalOffset = this.RestaurantsScrollViewer.ScrollableHeight; //sv.ExtentHeight - sv.ViewportHeight;

            if (maxVerticalOffset < 0 || verticalOffset == maxVerticalOffset)
            {
                // Scrolled to bottom
                LoadPage(_currentPage++);
            }

        }

        private void LoadPage(int page)
        {
            if (ConnectionContext.CheckNetworkConnection())
            {
                ((TextBlock)(this.NoConnectionText ?? FindName("NoConnectionText"))).Visibility = Visibility.Collapsed;

                if (_restaurantTag == null)
                {
                    DownloadRestaurants((int)ListType.Recommended, "", page);
                }
                else
                {
                    DownloadRestaurants((int)ListType.Recommended, _restaurantTag, page);
                }
            }
            else
            {
                _currentPage--;
                ((TextBlock)(this.NoConnectionText ?? FindName("NoConnectionText"))).Visibility = Visibility.Visible;
            }
        }

        private void SortByPopularity_Click(object sender, RoutedEventArgs e)
        {
            _sortBy = SortByPopularity;
            RefreshPageSortBY();
        }

        private void SortByRatingScore_Click(object sender, RoutedEventArgs e)
        {
            _sortBy = SortByRatingScore;
            RefreshPageSortBY();
        }

        private void SortByRatingNum_Click(object sender, RoutedEventArgs e)
        {
            _sortBy = SortByRatingNumbers;
            RefreshPageSortBY();
        }

        private void RefreshPageSortBY()
        {
            _currentPage = 1;
            _list.clearRestaurantList();
            LoadPage(_currentPage++);
        }

        private void RestaurantListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Restaurant restaurant = e.AddedItems[0] as Restaurant;
            Frame.Navigate(typeof(RestaurantDetailPage), restaurant);
        }
    }
}
