using Paris_Saveur_UWP.Models;
using Paris_Saveur_UWP.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Paris_Saveur_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RestaurantListPage : Page
    {
        private int _currentPage;
        private string _sortBy;
        private string _restaurantStyle;
        private RestaurantList list = new RestaurantList();
        private enum LISTTYPE
        {
            Recommended,
            Tag,
            Style
        }

        public RestaurantListPage()
        {
            this.InitializeComponent();
            _currentPage = 1;
            _sortBy = "popularity";
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ConnectionContext.CheckNetworkConnection())
            {
                ((TextBlock)(NoConnectionText ?? FindName("NoConnectionText"))).Visibility = Visibility.Collapsed;

                var parameterReceived = e.Parameter;
                if (parameterReceived == null)
                {
                    this.Title.Label = LocalizedStrings.Get("HotRestaurantPage_Title");
                    DownloadRestaurants((int)LISTTYPE.Recommended, "", _sortBy, _currentPage++);
                }
                else if (parameterReceived is string)
                {
                    //_restaurantStyle = parameterReceived as string;
                    //this.Title.Label = Restaurant.StyleToProperLanguage(_restaurantStyle);
                    //DownloadRestaurants((int)LISTTYPE.Style, _restaurantStyle, _sortBy, _currentPage++);
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
                ((TextBlock)(NoConnectionText ?? FindName("NoConnectionText"))).Visibility = Visibility.Visible;
                //this.LoadMoreButoon.Visibility = Visibility.Collapsed;
            }
        }

        private async void DownloadRestaurants(int type, string keyword, string sortby, int page)
        {
            //LoadMoreButoon.Visibility = Visibility.Collapsed;
            ((ProgressRing)(LoadingRing ?? FindName("LoadingRing"))).IsActive = true;
            ((ProgressRing)(LoadingRing ?? FindName("LoadingRing"))).Visibility = Visibility.Visible;

            switch (type)
            {
                case (int)LISTTYPE.Recommended:
                    var resultRecommended = await RestClient.getResponseStringFromUri(ConnectionContext.RestaurantList_API + "/list/?order=-" + _sortBy + "&page=" + page);
                    list.loadMoreRestaurants(resultRecommended);
                    break;
                case (int)LISTTYPE.Style:
                    var resultStyle = await RestClient.getResponseStringFromUri(ConnectionContext.RestaurantList_API + "/list/?order=-" + _sortBy + "&page=" + page);
                    list = new RestaurantList(resultStyle);
                    break;
                default:
                    var resultTag = await RestClient.getResponseStringFromUri(ConnectionContext.RestaurantList_API + "/list/?order=-" + _sortBy + "&page=" + page);
                    list = new RestaurantList(resultTag);
                    break;
            }

            //if (list.Restaurant_list.Count < 12)
            //{
            //    LoadMoreButoon.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    LoadMoreButoon.Visibility = Visibility.Visible;
            //}
            foreach (Restaurant restaurant in list.Restaurant_list)
            {
                restaurant.SetupRestaurantModelToDisplay(this.BaseUri);
            }
            ((ListView)(RestaurantList ?? FindName("RestaurantList"))).DataContext = list;
            ((GridView)(RestaurantGridView ?? FindName("RestaurantGridView"))).DataContext = list;

            ((ProgressRing)(LoadingRing ?? FindName("LoadingRing"))).IsActive = false;
            ((ProgressRing)(LoadingRing ?? FindName("LoadingRing"))).Visibility = Visibility.Collapsed;
        }

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            var verticalOffset = scrollViewer.VerticalOffset;
            var maxVerticalOffset = scrollViewer.ScrollableHeight; //sv.ExtentHeight - sv.ViewportHeight;

            if (maxVerticalOffset < 0 ||
                verticalOffset == maxVerticalOffset)
            {
                // Scrolled to bottom
                loadMoreRestaurants();
            }


        }

        private void loadMoreRestaurants()
        {
            RefreshPage(_sortBy, _currentPage++);
        }

        private void RefreshPage(string _sortBy, int page)
        {
            if (ConnectionContext.CheckNetworkConnection())
            {
                this.RestaurantList.Visibility = Visibility.Visible;
                this.NoConnectionText.Visibility = Visibility.Collapsed;

                //if (_restaurantStyle == null && _restaurantTag == null)
                //{
                    DownloadRestaurants((int)LISTTYPE.Recommended, "", _sortBy, page);
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
                this.RestaurantList.Visibility = Visibility.Collapsed;
                this.NoConnectionText.Visibility = Visibility.Visible;
            }
        }
    }
}
