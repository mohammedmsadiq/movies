using System;
using System.Globalization;
using Xamarin.Forms;

namespace movies.Converters
{
    public class ImageStringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {


            var r = AppConfig.ConfigConstants.BaseImageURL + value;
            return r;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
