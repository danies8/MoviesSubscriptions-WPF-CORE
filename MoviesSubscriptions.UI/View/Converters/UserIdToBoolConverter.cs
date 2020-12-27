using System;
using System.Globalization;
using System.Windows.Data;

namespace MoviesSubscriptions.UI.View.Converters
{
    public sealed class UserIdToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is int))
                return null;
            return (int)value == 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
