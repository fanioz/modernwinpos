using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml;
using modernwinpos.ViewModels;

namespace modernwinpos.Views
{
    /// <summary>
    /// POS Register page with proper lifecycle management
    /// </summary>
    public sealed partial class POSPage : Page
    {
        public POSViewModel ViewModel { get; private set; }

        public POSPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel = new POSViewModel();
            DataContext = ViewModel;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            ViewModel?.Dispose();
        }
    }
}
