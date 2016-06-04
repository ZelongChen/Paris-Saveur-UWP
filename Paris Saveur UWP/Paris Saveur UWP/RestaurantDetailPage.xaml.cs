using Paris_Saveur_UWP.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
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

            UpdateForVisualState(AdaptiveStates.CurrentState);

            DisableContentTransitions();
        }

        private void AdaptiveStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            UpdateForVisualState(e.NewState, e.OldState);
        }

        private void UpdateForVisualState(VisualState newState, VisualState oldState = null)
        {
            var isNarrow = newState == NarrowState;

            if (isNarrow && oldState == DefaultState)
            {
                // Resize down to the detail item. Don't play a transition.
                Frame.Navigate(typeof(CommentPage), _restaurant.Pk);
            }

            //EntranceNavigationTransitionInfo.SetIsTargetElement(RestaurantDetailPage, isNarrow);
            if (DetailContentPresenter != null)
            {
                EntranceNavigationTransitionInfo.SetIsTargetElement(DetailContentPresenter, !isNarrow);
            }
        }

        private void EnableContentTransitions()
        {
            DetailContentPresenter.ContentTransitions.Clear();
            DetailContentPresenter.ContentTransitions.Add(new EntranceThemeTransition());
        }

        private void DisableContentTransitions()
        {
            if (DetailContentPresenter != null)
            {
                DetailContentPresenter.ContentTransitions.Clear();
            }
        }

        private void RestaurantDishesGrid_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

            if (AdaptiveStates.CurrentState == NarrowState)
            {
                Frame.Navigate(typeof(CommentPage), _restaurant.Pk);
            }
            else
            {
                this.DetailContentPresenter.ContentTemplate = this.DishesTemplate;
                EnableContentTransitions();
            }
        }

        private void RestaurantAddressGrid_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

        }

        private void RestaurantCommentsGrid_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

            if (AdaptiveStates.CurrentState == NarrowState)
            {
                Frame.Navigate(typeof(CommentPage), _restaurant.Pk);
            }
            else
            {
                this.DetailContentPresenter.ContentTemplate = this.CommentsTemplate;
                EnableContentTransitions();
            }
        }
    }
}
