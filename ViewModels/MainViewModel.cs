using CommunityToolkit.Mvvm.Input;
using modernwinpos.Services;
using modernwinpos.Views;

namespace modernwinpos.ViewModels
{
    /// <summary>
    /// Main navigation hub ViewModel
    /// </summary>
    public partial class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            Title = "Modern POS";
        }

        [RelayCommand]
        private void NavigateToPOS()
        {
            NavigationService.NavigateTo<POSPage>();
        }

        [RelayCommand]
        private void NavigateToInventory()
        {
            NavigationService.NavigateTo<InventoryPage>();
        }
    }
}
