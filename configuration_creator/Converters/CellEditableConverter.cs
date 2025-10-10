using System;
using System.Globalization;
using System.Windows.Data;

namespace YourProjectNamespace.Converters
{
    public class CellEditableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string colorCode)
            {
                // Ha piros (#FF0000), akkor true (szerkeszthető)
                return string.Equals(colorCode, "#FF0000", StringComparison.OrdinalIgnoreCase);
            }
            return false; // alapértelmezett nem szerkeszthető
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
