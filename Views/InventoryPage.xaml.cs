using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using modernwinpos.ViewModels;

namespace modernwinpos.Views
{
    /// <summary>
    /// Inventory Management page
    /// </summary>
    public sealed partial class InventoryPage : Page
    {
        public InventoryViewModel ViewModel { get; private set; }

        public InventoryPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel = new InventoryViewModel();
            DataContext = ViewModel;
        }
    }
}
