using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using System;

namespace modernwinpos.Converters
{
    /// <summary>
    /// Converts icon string names to WinUI Symbol icons
    /// </summary>
    public class IconToSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string iconName)
            {
                return iconName switch
                {
                    "Target" => Symbol.Target,
                    "Keyboard" => Symbol.Keyboard,
                    "Video" => Symbol.Video,
                    "Battery0" => Symbol.Battery0,
                    "Clock" => Symbol.Clock,
                    "Shop" => Symbol.Shop,
                    "Library" => Symbol.Library,
                    "Placeholder" => Symbol.Placeholder,
                    _ => Symbol.Placeholder
                };
            }
            return Symbol.Placeholder;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
