#region Imported Types

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#endregion

namespace DeviceSQL.Utilities.RealFLOMappingGenerator.Converter
{
    public class NegatedBoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (!(bool?)value).GetValueOrDefault() ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
