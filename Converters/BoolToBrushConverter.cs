using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;

namespace modernwinpos.Converters
{
    /// <summary>
    /// Converts boolean values to brushes with null safety
    /// </summary>
    public class BoolToBrushConverter : IValueConverter
    {
        public Brush TrueBrush { get; set; }
        public Brush FalseBrush { get; set; }

        private static readonly Brush _defaultBrush = new SolidColorBrush(Microsoft.UI.Colors.Transparent);

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool b && b)
                return TrueBrush ?? _defaultBrush;
            return FalseBrush ?? _defaultBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
