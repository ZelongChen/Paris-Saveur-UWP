using Paris_Saveur_UWP.Models;
using Paris_Saveur_UWP.Tools;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace Paris_Saveur_UWP
{

    public sealed partial class RestaurantListPage : Page
    {
        private int _currentPage;
        private string _sortBy;
        private RestaurantList _list = new RestaurantList();

        private readonly string SORTBY_POPULARITY = "popularity";
        private readonly string SORTBY_RATINGNUM = "rating_num";
        private readonly string SORTBY_RATINGSCORE = "rating_score";

        private enum LISTTYPE
        {
            Recommended,
            Tag
        }

        public RestaurantListPage()
        {
            this.InitializeComponent();
            _currentPage = 1;
            _sortBy = SORTBY_POPULARITY;
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
                    DownloadRestaurants((int)LISTTYPE.Recommended, "", _currentPage++);
                }
                else
                {
                    //_restaurantTag = new Tag();
                    //_restaurantTag = parameterReceived as Tag;
                    //this.Title.Label = _restaurantTag.name;
                    //DownloadRestaurants((int)LISTTYPE.Tag, _restaurantTag.name, _sortBy, _currentPage++);
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
                case (int)LISTTYPE.Recommended:
                    var resultRecommended = await RestClient.getResponseStringFromUri(ConnectionContext.HotRestaurantsUrl + _sortBy + "&page=" + page);
                    _list.loadMoreRestaurants(resultRecommended);
                    break;
                default:
                    var resultTag = await RestClient.getResponseStringFromUri(ConnectionContext.TagRestaurantsUrl + keyword + "&order=-" + _sortBy + "&page=" + page);
                    _list.loadMoreRestaurants(resultTag);
                    break;
            }

            foreach (Restaurant restaurant in _list.Restaurant_list)
            {
                restaurant.SetupRestaurantModelToDisplay();
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

                //if (_restaurantStyle == null && _restaurantTag == null)
                //{
                DownloadRestaurants((int)LISTTYPE.Recommended, "", page);
                //}
                //else if (_restaurantStyle == null && _restaurantTag != null)
                //{
                //    DownloadRestaurants((int)LISTTYPE.Recommended, _restaurantTag.name, _sortBy, page);
                //}
                //else
                //{
                //    DownloadRestaurants((int)LISTTYPE.Recommended, _restaurantStyle, _sortBy, page);
                //}
            }
            else
            {
                _currentPage--;
                ((TextBlock)(this.NoConnectionText ?? FindName("NoConnectionText"))).Visibility = Visibility.Visible;
            }
        }

        private void SortByPopularity_Click(object sender, RoutedEventArgs e)
        {
            _sortBy = SORTBY_POPULARITY;
            RefreshPageSortBY();
        }

        private void SortByRatingScore_Click(object sender, RoutedEventArgs e)
        {
            _sortBy = SORTBY_RATINGSCORE;
            RefreshPageSortBY();
        }

        private void SortByRatingNum_Click(object sender, RoutedEventArgs e)
        {
            _sortBy = SORTBY_RATINGNUM;
            RefreshPageSortBY();
        }

        private void RefreshPageSortBY()
        {
            _currentPage = 1;
            _list.clearRestaurantList();
            LoadPage(_currentPage++);
        }
    }
}
