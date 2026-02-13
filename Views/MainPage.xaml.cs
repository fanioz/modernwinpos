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
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel = new MainViewModel();
            DataContext = ViewModel;
        }
    }
}
