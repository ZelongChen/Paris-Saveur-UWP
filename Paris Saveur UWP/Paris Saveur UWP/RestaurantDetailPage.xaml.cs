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
        private int _lastSelectedItem;
        private enum SelectedItems
        {
            Comment,
            ToComment,
            Dishes
        }

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

            _lastSelectedItem = (int)SelectedItems.Comment;

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
                switch (_lastSelectedItem)
                {
                    case (int)SelectedItems.Dishes:
                        Frame.Navigate(typeof(DishesPage), _restaurant.Pk);
                        break;
                    case (int)SelectedItems.ToComment:
                        Frame.Navigate(typeof(ToCommentPage), _restaurant.Pk);
                        break;
                    default:
                        Frame.Navigate(typeof(CommentPage), _restaurant.Pk);
                        break;
                }

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
            _lastSelectedItem = (int)SelectedItems.Dishes;

            if (AdaptiveStates.CurrentState == NarrowState)
            {
                Frame.Navigate(typeof(DishesPage), _restaurant.Pk);
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
            _lastSelectedItem = (int)SelectedItems.Comment;

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

        private void ToCommentGrid_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            _lastSelectedItem = (int)SelectedItems.ToComment;

            if (AdaptiveStates.CurrentState == NarrowState)
            {
                Frame.Navigate(typeof(ToCommentPage), _restaurant.Pk);
            }
            else
            {
                this.DetailContentPresenter.ContentTemplate = this.ToCommentsTemplate;
                EnableContentTransitions();
            }
        }
    }
}
