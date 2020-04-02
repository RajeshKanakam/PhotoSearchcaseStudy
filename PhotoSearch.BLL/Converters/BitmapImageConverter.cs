using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PhotoSearch.BLL.Converters
{
    /// <summary>
    /// The BitmapImageConverter is used to convert a image url string
    /// to image uri so the Image Source binding can render the image in UI
    /// </summary>
    public class BitmapImageConverter : IValueConverter
    {
        /// <summary>
        /// Converts value to image uri
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string url)
                return new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));

            if (value is Uri uri)
                return new BitmapImage(uri);

            throw new NotSupportedException();
        }

        /// <summary>
        /// Placeholder method to convert back the bitmap image to string
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
