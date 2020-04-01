using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PhotoSearch.BLL.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object convertedValue = null;
            if (value is Visibility visibility)
                convertedValue = visibility == Visibility.Visible;

            if (value is bool isVisible)
                convertedValue = isVisible ? Visibility.Visible : Visibility.Hidden;

            return convertedValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
