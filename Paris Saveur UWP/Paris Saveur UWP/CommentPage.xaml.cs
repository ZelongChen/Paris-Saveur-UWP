using Paris_Saveur_UWP.Models;
using Paris_Saveur_UWP.Tools;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
    }
}
