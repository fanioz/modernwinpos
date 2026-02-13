using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;

namespace modernwinpos.Converters
{
    /// <summary>
    /// Converts boolean values to Visibility with optional inverse
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public bool Inverse { get; set; } = false;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool boolValue)
            {
                if (Inverse) boolValue = !boolValue;
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Visibility visibility)
            {
                return visibility == Visibility.Visible;
            }
            return false;
        }
    }
}
