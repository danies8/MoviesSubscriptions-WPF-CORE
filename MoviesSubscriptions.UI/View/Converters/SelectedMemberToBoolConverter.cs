using MoviesSubscriptions.UI.Wrapper;
using System;
using System.Globalization;
using System.Windows.Data;

namespace MoviesSubscriptions.UI.View.Converters
{
    public sealed class SelectedMemberToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is MemberWrapper))
                return false;
            return (MemberWrapper)value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
