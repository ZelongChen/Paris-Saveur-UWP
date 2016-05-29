using Paris_Saveur_UWP.Models;
using Paris_Saveur_UWP.Tools;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace Paris_Saveur_UWP
{

    public sealed partial class HotTagsPage : Page
    {
        public HotTagsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ConnectionContext.CheckNetworkConnection())
            {
                this.TagList.Visibility = Visibility.Visible;
                this.NoConnectionText.Visibility = Visibility.Collapsed;
                DownloadHotTag();
            }
            else
            {
                this.TagList.Visibility = Visibility.Collapsed;
                this.NoConnectionText.Visibility = Visibility.Visible;
            }

        }

        private async void DownloadHotTag()
        {
            LoadingRing.IsActive = true;
            LoadingRing.Visibility = Visibility.Visible;

            var result = await RestClient.GetResponseStringFromUrl(ConnectionContext.HotTagsUrl);

            TagList tagList = new TagList(result);
            this.TagList.ItemsSource = tagList.TagCollection;

            LoadingRing.IsActive = false;
            LoadingRing.Visibility = Visibility.Collapsed;
        }

        private void TagList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Tag tag = e.AddedItems[0] as Tag;
            Frame.Navigate(typeof(RestaurantListPage), tag.Name);
        }
    }
}
