using System;
using System.Globalization;
using System.Windows.Data;

namespace MediaViewer
{
    public class UriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? string.Empty : (value as Uri).AbsoluteUri;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;
            return string.IsNullOrEmpty(str) ? null : new Uri(str);
        }
    }
}
