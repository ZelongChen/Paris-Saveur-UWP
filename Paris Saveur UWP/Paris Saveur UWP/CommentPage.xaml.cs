using Paris_Saveur_UWP.Models;
using Paris_Saveur_UWP.Tools;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Paris_Saveur_UWP
{

    public sealed partial class CommentPage : Page
    {

        private Restaurant _restaurant;
        private CommentList _commentList = new CommentList();

        public CommentPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _restaurant = e.Parameter as Restaurant;
            DownloadRestaurantComments();
        }

        private async void DownloadRestaurantComments()
        {
            var response = await RestClient.GetResponseStringFromUrl(ConnectionContext.RestaurantCommentsUrl.Replace("restaurantPk", "" + _restaurant.Pk).Replace("pageToDownload", "" + _commentList.CurrentPage));
            _commentList.loadMoreComments(response);

            if (_commentList.CommentCollection.Count == 0)
            {
                this.NoCommentText.Visibility = Visibility.Visible;
            }
            else
            {
                this.NoCommentText.Visibility = Visibility.Collapsed;
            }

            RestaurantCommentList.DataContext = _commentList;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            _commentList.clearComments();
        }

        void NavigateBackForWideState(bool useTransition)
        {
            // Evict this page from the cache as we may not need it again.
            NavigationCacheMode = NavigationCacheMode.Disabled;

            if (useTransition)
            {
                Frame.GoBack(new EntranceNavigationTransitionInfo());
            }
            else
            {
                Frame.GoBack(new SuppressNavigationTransitionInfo());
            }
        }

        private bool ShouldGoToWideState()
        {
            return Window.Current.Bounds.Width >= 600;
        }

        private void PageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            if (ShouldGoToWideState())
            {
                // We shouldn't see this page since we are in "wide master-detail" mode.
                // Play a transition as we are navigating from a separate page.
                NavigateBackForWideState(useTransition: true);
            }
            else
            {
                // Realize the main page content.
                FindName("RootView");
            }

            Window.Current.SizeChanged += Window_SizeChanged;
        }

        private void PageRoot_Unloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= Window_SizeChanged;
        }

        private void Window_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            if (ShouldGoToWideState())
            {
                // Make sure we are no longer listening to window change events.
                Window.Current.SizeChanged -= Window_SizeChanged;

                // We shouldn't see this page since we are in "wide master-detail" mode.
                NavigateBackForWideState(useTransition: false);
            }
        }

    }
}
