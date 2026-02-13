using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using modernwinpos.Models;

namespace modernwinpos.ViewModels
{
    /// <summary>
    /// ViewModel for POS Register functionality with proper memory management
    /// </summary>
    public partial class POSViewModel : BaseViewModel
    {
        private const decimal TAX_RATE = 0.08m;

        private readonly ObservableCollection<Product> _allProducts;
        private readonly List<CartItem> _trackedCartItems = new();

        [ObservableProperty]
        private ObservableCollection<Product> _products = new();

        [ObservableProperty]
        private ObservableCollection<CartItem> _cartItems = new();

        [ObservableProperty]
        private ObservableCollection<string> _categories = new();

        [ObservableProperty]
        private string _selectedCategory = "All Items";

        [ObservableProperty]
        private string _searchQuery = string.Empty;

        [ObservableProperty]
        private decimal _subtotal;

        [ObservableProperty]
        private decimal _tax;

        [ObservableProperty]
        private decimal _total;

        [ObservableProperty]
        private string _userProfilePicture = string.Empty;

        public POSViewModel()
        {
            Title = "POS Register";
            _allProducts = GenerateMockProducts();
            _products = new ObservableCollection<Product>(_allProducts);

            InitializeCategories();
            InitializeCart();

            UserProfilePicture = "ms-appx:///Assets/user.png";
        }

        private void InitializeCategories()
        {
            Categories.Add("All Items");
            Categories.Add("Beverages");
            Categories.Add("Food & Snacks");
            Categories.Add("Pastries");
            Categories.Add("Combos");
        }

        private void InitializeCart()
        {
            CartItems.CollectionChanged += (s, e) => CalculateTotals();
        }

        partial void OnSelectedCategoryChanged(string value)
        {
            FilterProducts();
        }

        partial void OnSearchQueryChanged(string value)
        {
            FilterProducts();
        }

        private void FilterProducts()
        {
            Products.Clear();

            var filtered = _allProducts.Where(p =>
                (SelectedCategory == "All Items" || p.Category == SelectedCategory) &&
                (string.IsNullOrWhiteSpace(SearchQuery) || p.Name.Contains(SearchQuery, System.StringComparison.OrdinalIgnoreCase))
            );

            foreach (var product in filtered)
            {
                Products.Add(product);
            }
        }

        [RelayCommand]
        private void AddToCart(Product product)
        {
            var existingItem = CartItems.FirstOrDefault(c => c.Product.Id == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity++;
                // PropertyChanged will trigger CalculateTotals automatically
            }
            else
            {
                var newItem = new CartItem(product);
                // Subscribe to property changes for totals recalculation
                newItem.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(CartItem.Quantity))
                    {
                        CalculateTotals();
                    }
                };
                _trackedCartItems.Add(newItem);
                CartItems.Add(newItem);
                // CollectionChanged will trigger CalculateTotals
            }
        }

        [RelayCommand]
        private void RemoveFromCart(CartItem item)
        {
            if (CartItems.Contains(item))
            {
                item.PropertyChanged -= OnCartItemPropertyChanged;
                _trackedCartItems.Remove(item);
                CartItems.Remove(item);
            }
        }

        private void OnCartItemPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CartItem.Quantity))
            {
                CalculateTotals();
            }
        }

        [RelayCommand]
        private void IncreaseQuantity(CartItem item)
        {
            item.Quantity++;
            // Property change notification will recalculate totals
        }

        [RelayCommand]
        private void DecreaseQuantity(CartItem item)
        {
            if (item.Quantity > 1)
            {
                item.Quantity--;
            }
            else
            {
                RemoveFromCart(item);
            }
        }

        [RelayCommand]
        private void ClearCart()
        {
            foreach (var item in _trackedCartItems)
            {
                item.PropertyChanged -= OnCartItemPropertyChanged;
            }
            _trackedCartItems.Clear();
            CartItems.Clear();
        }

        private void CalculateTotals()
        {
            Subtotal = CartItems.Sum(c => c.TotalPrice);
            Tax = Subtotal * TAX_RATE;
            Total = Subtotal + Tax;
        }

        [RelayCommand]
        private void SelectCategory(string category)
        {
            SelectedCategory = category;
        }

        [RelayCommand]
        private void NavigateToInventory()
        {
            // TODO: Navigate to inventory page
        }

        private ObservableCollection<Product> GenerateMockProducts()
        {
            return new ObservableCollection<Product>
            {
                new Product { Id = 1, Name = "Iced Caffé Latte", Category = "Beverages", Price = 4.50m, StockLevel = 50, IsHotSeller = true, Icon = "Cup", ImageUrl = "ms-appx:///Assets/latte.png" },
                new Product { Id = 2, Name = "Double Espresso", Category = "Beverages", Price = 3.25m, StockLevel = 45, IsHotSeller = false, Icon = "Cup", ImageUrl = "ms-appx:///Assets/espresso.png" },
                new Product { Id = 3, Name = "Glazed Doughnut", Category = "Pastries", Price = 2.50m, StockLevel = 8, IsHotSeller = false, Icon = "Bakery", ImageUrl = "ms-appx:///Assets/doughnut.png" },
                new Product { Id = 4, Name = "Choco Muffin", Category = "Pastries", Price = 3.75m, StockLevel = 12, IsHotSeller = false, Icon = "Bakery", ImageUrl = "ms-appx:///Assets/muffin.png" },
                new Product { Id = 5, Name = "Butter Croissant", Category = "Pastries", Price = 3.50m, StockLevel = 15, IsHotSeller = false, Icon = "Bakery", ImageUrl = "ms-appx:///Assets/croissant.png" },
                new Product { Id = 6, Name = "Organic Green Tea", Category = "Beverages", Price = 2.75m, StockLevel = 30, IsHotSeller = false, Icon = "Clock", ImageUrl = "ms-appx:///Assets/tea.png" },
            };
        }

        // Cleanup method for proper disposal
        public void Dispose()
        {
            foreach (var item in _trackedCartItems)
            {
                item.PropertyChanged -= OnCartItemPropertyChanged;
            }
            _trackedCartItems.Clear();
        }
    }
}
