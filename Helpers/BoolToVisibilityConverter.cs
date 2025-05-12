using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BiblioGest.Helpers
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Check if value is null, which is important for objects like LoggedInAdherent
            if (value == null)
                return Visibility.Collapsed;
            
            // If it's a boolean, use it directly
            if (value is bool boolValue)
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            
            // Otherwise, treat non-null as visible
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}