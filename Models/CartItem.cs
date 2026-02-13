namespace modernwinpos.Models
{
    public partial class CartItem : ObservableObject
    {
        public Product Product { get; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TotalPrice))]
        [NotifyPropertyChangedFor(nameof(FormattedTotalPrice))]
        private int _quantity;

        public decimal TotalPrice => Product.Price * Quantity;
        public string FormattedTotalPrice => $"${TotalPrice:F2}";

        public CartItem(Product product, int quantity = 1)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}
