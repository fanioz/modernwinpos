using System.Collections.ObjectModel;
using System.Linq;
using modernwinpos.Models;

namespace modernwinpos.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Product> _allProducts;

        [ObservableProperty]
        private ObservableCollection<Product> _products;

        [ObservableProperty]
        private ObservableCollection<CartItem> _cartItems;

        [ObservableProperty]
        private ObservableCollection<string> _categories;

        [ObservableProperty]
        private string _selectedCategory;

        [ObservableProperty]
        private string _searchQuery;

        [ObservableProperty]
        private decimal _subtotal;

        [ObservableProperty]
        private decimal _tax;

        [ObservableProperty]
        private decimal _total;

        public MainViewModel()
        {
            Title = "POS Register";
            _cartItems = new ObservableCollection<CartItem>();
            _categories = new ObservableCollection<string> { "All Items", "Beverages", "Food & Snacks", "Pastries", "Combos" };
            _selectedCategory = "All Items";
            _searchQuery = "";

            _allProducts = GenerateMockProducts();
            _products = new ObservableCollection<Product>(_allProducts);

            _cartItems.CollectionChanged += (s, e) => CalculateTotals();
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
            var filtered = _allProducts.Where(p =>
                (SelectedCategory == "All Items" || p.Category == SelectedCategory) &&
                (string.IsNullOrEmpty(SearchQuery) || p.Name.Contains(SearchQuery, System.StringComparison.OrdinalIgnoreCase))
            ).ToList();

            Products = new ObservableCollection<Product>(filtered);
        }

        [RelayCommand]
        private void AddToCart(Product product)
        {
            var existingItem = CartItems.FirstOrDefault(c => c.Product.Id == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity++;
                CalculateTotals(); // Needed because Quantity change doesn't trigger CollectionChanged on CartItems directly unless we observe item changes
            }
            else
            {
                var newItem = new CartItem(product);
                // Subscribe to property changes to recalculate totals
                newItem.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(CartItem.TotalPrice))
                    {
                        CalculateTotals();
                    }
                };
                CartItems.Add(newItem);
            }
        }

        [RelayCommand]
        private void RemoveFromCart(CartItem item)
        {
            CartItems.Remove(item);
        }

        [RelayCommand]
        private void IncreaseQuantity(CartItem item)
        {
            item.Quantity++;
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
                CartItems.Remove(item);
            }
        }

        [RelayCommand]
        private void SelectCategory(string category)
        {
            SelectedCategory = category;
        }

        private void CalculateTotals()
        {
            Subtotal = CartItems.Sum(c => c.TotalPrice);
            Tax = Subtotal * 0.08m;
            Total = Subtotal + Tax;
        }

        private ObservableCollection<Product> GenerateMockProducts()
        {
            return new ObservableCollection<Product>
            {
                new Product { Id = 1, Name = "Iced Caffé Latte", Category = "Beverages", Price = 4.50m, ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuAMy6Uc1zUtJUKkEqj74tLdEYRhn-6yxD39fmoz9qOoXeS-0KQ1WqtsKR9LUFvMQpk7jzWTJAhxSgpOMtcTGliblTmrpNuC4Mb07PtCuqcem1epnWeABCsg7ANl4oFNQ5p3EwOWBGBAsNm0U1PlhtKBmRLy1_qxpy9d6TFpfTBVExnz3sVbN5HhwJOilQzMuKATEOxTjc24XWoRGXDkfcRd7qtmT-AKf6oZg9qyGsDaT6b4IXukzOx2y2alshVYnR1Pe8oID-Bx76w", IsHotSeller = true },
                new Product { Id = 2, Name = "Double Espresso", Category = "Beverages", Price = 3.25m, ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuACMZ5Ttmtvp2nHoG-pQERIcJjrQ1oV-rNttzkeJ2LTq_G2R5hlFZ4JtcMGTpgrFdvz7Rya_yVikOuM01qrJT5eEKg2okI51FVnJNKYz273-3N-lNdigGfWQlnw4kHSa-yX5-Vfb0kR4ezcYT2juoGAYkJZh_F0Wy_qDd6cteoz59bHioDRv5O3qgB4yM1TXOb5J_O17cd2TsNMCZW8n_prwFUEupzAobKXT7Z9OeZw0FzNFD7qypDl7EhMJfnc7ZEgjqsH-cUPee0", IsHotSeller = false },
                new Product { Id = 3, Name = "Glazed Doughnut", Category = "Pastries", Price = 2.50m, ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuACV_g3yILe8EKJijozAhLK625xg4Mbkaol4ygdCEFP87Ju_rgzQ_C9Zg7er-ewTRzLbKFdL5zlaabClypEXqJd-yoU8o-ecw27cX1_08c61GJUsmEnC279VHO3PuXtCaALOHAj1BJh7ijeDf2lqRvBtSRNY8zjw0Evg48XY6RPyinl_aK9GzI5bhzwtYHtLDdbqL-husVwXB6weLo9WqK2xlotBMYTIHUJh4g027MOqd_zXSiwykiivXw3spoEJa7oLU06WNLyFlI", IsHotSeller = false },
                new Product { Id = 4, Name = "Choco Muffin", Category = "Pastries", Price = 3.75m, ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuBTJZhZ_uPlnYDbeatoSAhBD449qZwxb3BjS8gTGV7f67xOlXDNkVQSU7x2y6PqIeeK1Lwa-t8nfR1QxnAtIaqGVmu7DOvQ6O2Vvm3Uu-QzlIeVM0yWzSz0RvuqkKDAYabQg2sM8eL-0g__fhUqH93G3CLPN5zPe_nLI2tKxGuIjJ6gIfnr-jxodt2St73ZdCn3ASPzn74145ACWERaARsd4THxX4EqjUS-N3AFNvbfb6uTj3FSkY057KxZupluFPRzBRWFyv8fQ8I", IsHotSeller = false },
                new Product { Id = 5, Name = "Butter Croissant", Category = "Pastries", Price = 3.50m, ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuBLEXaFVp-d6ofxC4ELqzXSmRlZtEfDru7PmmCHMvY9Hmsessxk1OaV-nqgPjInZV32Vt01vAqZpfGlIeJUD8ymM-aITithUcn1gN8IEysjbFH0SqqEaPKb8xWRR9c-WQs9iXpStuuSlhC3BnobyOoa34Sy64FH84O3FNzjflvJybFCHyrwrO_TnDJuF39r7xDaFOA2D84Li9qHucu4Tt5KYe68AFmMHl62AgLX22AHQ65gBJ_TBOkjj80zauVL145z2X9i5Z_lnjg", IsHotSeller = false },
                new Product { Id = 6, Name = "Organic Green Tea", Category = "Beverages", Price = 2.75m, ImageUrl = "https://lh3.googleusercontent.com/aida-public/AB6AXuBnffgZJD9Va81graFycnm1K6bCtfZGz7Hmu0IBT6-dkso-8TniqT2KCH09P0Mgte0ukxkDK5NLp3N8bHzhpPe70QXtIxbm21MyrYV5gjYdfxxSsFszZ5_fbYWnZods9gYyywHasSSQvsn9B_59zm8MzJduLd_lU8AY95tbdN-kRySoER1n75Odjay3s4gJ_cseKTeBmNBUsTObmyIfSSqyFICuHaZEuf-xm1mYWFkw6SXUsGqJZzNjzWMXzrDkjI7KtWxWkr91iko", IsHotSeller = false },
            };
        }
    }
}
