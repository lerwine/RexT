using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;

namespace Erwine.Leonard.T.RexT.Converters
{
    public class TextToSingleLineConverter : DependencyObject, IValueConverter
    {
        public static readonly Regex SingleLineRegex = new Regex(@"(?(?=\s*[\r\n])[\r\n\s]+|\s[\r\n\s]+)");

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string s = value as string;

            if (s == null)
                return s;

            return TextToSingleLineConverter.SingleLineRegex.Replace(s.Trim(), " ");
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
