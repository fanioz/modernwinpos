using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using modernwinpos.Models;

namespace modernwinpos.ViewModels
{
    /// <summary>
    /// ViewModel for Inventory Management with functional search and filters
    /// </summary>
    public partial class InventoryViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Product> _allProducts;

        [ObservableProperty]
        private ObservableCollection<Product> _products = new();

        [ObservableProperty]
        private string _searchQuery = string.Empty;

        [ObservableProperty]
        private string _selectedFilter = "All Items";

        [ObservableProperty]
        private int _totalProducts;

        [ObservableProperty]
        private int _lowStockCount;

        [ObservableProperty]
        private string _lowStockAlertText = string.Empty;

        public ObservableCollection<string> FilterOptions { get; } = new()
        {
            "All Items",
            "Low Stock",
            "Electronics",
            "Peripherals",
            "Displays",
            "Accessories"
        };

        public InventoryViewModel()
        {
            Title = "Inventory";
            _allProducts = GenerateMockProducts();
            FilterProducts();
        }

        partial void OnSearchQueryChanged(string value)
        {
            FilterProducts();
        }

        partial void OnSelectedFilterChanged(string value)
        {
            FilterProducts();
        }

        private void FilterProducts()
        {
            Products.Clear();

            var filtered = _allProducts.Where(p =>
            {
                // Filter by category
                bool categoryMatch = SelectedFilter == "All Items" ||
                                  SelectedFilter == "Low Stock";

                // Filter by low stock if selected
                bool stockMatch = SelectedFilter != "Low Stock" || p.IsLowStock;

                // Filter by search query
                bool searchMatch = string.IsNullOrWhiteSpace(SearchQuery) ||
                                  p.Name.Contains(SearchQuery, System.StringComparison.OrdinalIgnoreCase) ||
                                  p.Sku.Contains(SearchQuery, System.StringComparison.OrdinalIgnoreCase) ||
                                  p.Category.Contains(SearchQuery, System.StringComparison.OrdinalIgnoreCase);

                return categoryMatch && stockMatch && searchMatch;
            });

            foreach (var product in filtered)
            {
                Products.Add(product);
            }

            UpdateCounts();
        }

        private void UpdateCounts()
        {
            TotalProducts = _allProducts.Count;
            LowStockCount = _allProducts.Count(p => p.IsLowStock);
            LowStockAlertText = LowStockCount > 0
                ? $"{LowStockCount} item{(LowStockCount == 1 ? "" : "s")} need restock"
                : string.Empty;
        }

        [RelayCommand]
        private void SelectFilter(string filter)
        {
            SelectedFilter = filter;
        }

        [RelayCommand]
        private void AddProduct()
        {
            // TODO: Navigate to add product page
        }

        [RelayCommand]
        private void ExportProducts()
        {
            // TODO: Implement export functionality
        }

        private ObservableCollection<Product> GenerateMockProducts()
        {
            return new ObservableCollection<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Wireless Mouse M510",
                    Sku = "4002-WM",
                    Price = 29.99m,
                    StockLevel = 4,
                    Category = "Electronics",
                    IsLowStock = true,
                    Icon = "Mouse",
                    ImageUrl = "ms-appx:///Assets/mouse.png"
                },
                new Product
                {
                    Id = 2,
                    Name = "Mechanical Keyboard K2",
                    Sku = "8812-KB",
                    Price = 124.50m,
                    StockLevel = 42,
                    Category = "Peripherals",
                    IsLowStock = false,
                    Icon = "Keyboard",
                    ImageUrl = "ms-appx:///Assets/keyboard.png"
                },
                new Product
                {
                    Id = 3,
                    Name = "UltraSharp 27\" 4K",
                    Sku = "1099-MN",
                    Price = 499.00m,
                    StockLevel = 12,
                    Category = "Displays",
                    IsLowStock = false,
                    Icon = "Monitor",
                    ImageUrl = "ms-appx:///Assets/monitor.png"
                },
                new Product
                {
                    Id = 4,
                    Name = "USB-C Power Hub",
                    Sku = "3341-PH",
                    Price = 55.00m,
                    StockLevel = 2,
                    Category = "Accessories",
                    IsLowStock = true,
                    Icon = "Battery",
                    ImageUrl = "ms-appx:///Assets/hub.png"
                }
            };
        }
    }
}
