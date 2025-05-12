using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BiblioGest.Helpers
{
    public class EnumToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return Visibility.Collapsed;

            // Get the enum value as string
            string enumValue = value.ToString();
            
            // Get the target role from parameter
            string targetRole = parameter.ToString();

            // Check if the current role matches the target role
            if (enumValue.Equals(targetRole, StringComparison.OrdinalIgnoreCase))
                return Visibility.Visible;
            
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}