using CommunityToolkit.Mvvm.ComponentModel;

namespace modernwinpos.Models
{
    /// <summary>
    /// Unified product model supporting both POS and Inventory features
    /// </summary>
    public partial class Product : ObservableObject
    {
        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _sku = string.Empty;

        [ObservableProperty]
        private decimal _price;

        [ObservableProperty]
        private int _stockLevel;

        [ObservableProperty]
        private string _category = string.Empty;

        [ObservableProperty]
        private string _imageUrl = string.Empty;

        [ObservableProperty]
        private bool _isHotSeller;

        [ObservableProperty]
        private string _icon = string.Empty; // e.g. "Mouse", "Keyboard"

        // Computed properties
        public string FormattedPrice => $"${Price:F2}";
        public string StockLevelText => $"{StockLevel} units";
        public bool IsLowStock => StockLevel < 10; // Threshold of 10

        // UI Helpers - these are computed, no UI types in model
        public string StatusText => IsLowStock ? "LOW STOCK" : "IN STOCK";

        // Icon name for SymbolIcon (converted at View layer via converter)
        public string IconSymbol => Icon switch
        {
            "Mouse" => "Target",
            "Keyboard" => "Keyboard",
            "Monitor" => "Video",
            "Battery" => "Battery",
            "Cup" => "Clock",
            "Food" => "Shop",
            "Bakery" => "Library",
            _ => "Placeholder"
        };
    }
}
