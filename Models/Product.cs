using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace modernwinpos.Models
{
    public class Product
    {
        public string Name { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public int StockLevel { get; set; }
        public string Category { get; set; }
        public bool IsLowStock { get; set; }
        public string Icon { get; set; } // e.g. "Mouse", "Keyboard"

        public string PriceFormatted => $"${Price:F2}";
        public string StockLevelText => $"{StockLevel} units";

        // UI Helper for SymbolIcon
        public Symbol SymbolIcon => Icon switch
        {
            "Mouse" => Symbol.Target, // Close enough or use FontIcon
            "Keyboard" => Symbol.Keyboard,
            "Monitor" => Symbol.Video,
            "Battery" => Symbol.BatteryUnknown, // Or use a battery symbol
            _ => Symbol.Placeholder
        };

        // UI Helpers for text and colors
        public string StatusText => IsLowStock ? "LOW STOCK" : "IN STOCK";

        // These can be used with converters in XAML for brushes if needed
    }
}
