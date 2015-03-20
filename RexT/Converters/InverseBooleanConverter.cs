using System;
using System.Windows;
using System.Windows.Data;

namespace Erwine.Leonard.T.RexT.Converters
{
    public class InverseBooleanConverter : DependencyObject, IValueConverter
    {

        #region NullValue Property Members

        public const string PropertyName_NullValue = "NullValue";

        public static readonly DependencyProperty NullValueProperty =
            DependencyProperty.Register(InverseBooleanConverter.PropertyName_NullValue, typeof(bool), typeof(InverseBooleanConverter),
                new PropertyMetadata(true));

        public bool NullValue
        {
            get { return (bool)(this.GetValue(InverseBooleanConverter.NullValueProperty)); }
            set { this.SetValue(InverseBooleanConverter.NullValueProperty, value); }
        }

        #endregion

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? b = value as bool?;

            if (b.HasValue)
                return !b.Value;

            return this.NullValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? b = value as bool?;

            if (b.HasValue)
                return !b.Value;

            return !this.NullValue;
        }
    }
}
