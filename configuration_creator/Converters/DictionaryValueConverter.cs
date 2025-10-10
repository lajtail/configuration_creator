using System;
using System.Globalization;
using System.Windows.Data;
using System.Collections.Generic;

namespace configuration_creator.Converters
{
    public class DictionaryValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var dict = values[0] as IDictionary<string, string>;
            var key = values[1] as string;
            if (dict != null && key != null && dict.ContainsKey(key))
                return dict[key];
            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            // Return the updated value and key so the view model can handle it
            return new object[] { value, parameter };
        }
    }
}
