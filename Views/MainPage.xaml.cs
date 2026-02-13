using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using modernwinpos.Services;
using modernwinpos.ViewModels;

namespace modernwinpos.Views
{
    /// <summary>
    /// Main navigation hub page
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; private set; }

        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = new MainViewModel();
            DataContext = ViewModel;

            // Initialize navigation service
            NavigationService.Initialize(ContentFrame);

            // Hide welcome when a page is navigated to
            ContentFrame.NavigatedTo += (s, e) =>
            {
                if (ContentFrame.Content != null)
                {
                    WelcomeGrid.Visibility = Visibility.Collapsed;
                }
            };

            // Show welcome when back at main page
            ContentFrame.NavigatedFrom += (s, e) =>
            {
                if (e.NavigationMode == NavigationMode.Back && ContentFrame.Content == null)
                {
                    WelcomeGrid.Visibility = Visibility.Visible;
                }
            };
        }
    }
}
