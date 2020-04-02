using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PhotoSearch.BLL.Converters
{
    /// <summary>
    /// The BoolToVisibilityConverter class is used to convert value from
    /// boolean type to Visibility type
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts value from boolean type to Visibility type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object convertedValue = null;
            if (value is Visibility visibility)
                convertedValue = visibility == Visibility.Visible;

            if (value is bool isVisible)
                convertedValue = isVisible ? Visibility.Visible : Visibility.Hidden;

            return convertedValue;
        }

        /// <summary>
        /// Placeholder method to convert value from Visibility type to boolean type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
