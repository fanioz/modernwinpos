using System.Collections.ObjectModel;
using modernwinpos.Models;

namespace modernwinpos.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Product> Products { get; } = new();

        [ObservableProperty]
        private string _totalProducts = "142 Total Products";

        public MainViewModel()
        {
            Title = "Inventory";

            // Sample Data
            Products.Add(new Product
            {
                Name = "Wireless Mouse M510",
                Sku = "4002-WM",
                Price = 29.99m,
                StockLevel = 4,
                Category = "Electronics",
                IsLowStock = true,
                Icon = "Mouse" // Will map to Symbol.DeviceLaptop or similar
            });

            Products.Add(new Product
            {
                Name = "Mechanical Keyboard K2",
                Sku = "8812-KB",
                Price = 124.50m,
                StockLevel = 42,
                Category = "Peripherals",
                IsLowStock = false,
                Icon = "Keyboard"
            });

            Products.Add(new Product
            {
                Name = "UltraSharp 27\" 4K",
                Sku = "1099-MN",
                Price = 499.00m,
                StockLevel = 12,
                Category = "Displays",
                IsLowStock = false,
                Icon = "Monitor"
            });

            Products.Add(new Product
            {
                Name = "USB-C Power Hub",
                Sku = "3341-PH",
                Price = 55.00m,
                StockLevel = 2,
                Category = "Accessories",
                IsLowStock = true,
                Icon = "Battery"
            });
        }
    }
}
