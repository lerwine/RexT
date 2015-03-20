using System;
using System.Windows;
using System.Windows.Data;

namespace Erwine.Leonard.T.RexT.Converters
{
    public class BooleanToTextConverter : DependencyObject, IValueConverter
    {

        #region TrueValue Property Members

        public const string PropertyName_TrueValue = "TrueValue";

        public static readonly DependencyProperty TrueValueProperty =
            DependencyProperty.Register(BooleanToTextConverter.PropertyName_TrueValue, typeof(string), typeof(BooleanToTextConverter),
                new PropertyMetadata("True"));

        public string TrueValue
        {
            get { return this.GetValue(BooleanToTextConverter.TrueValueProperty) as string; }
            set { this.SetValue(BooleanToTextConverter.TrueValueProperty, value); }
        }

        #endregion

        #region FalseValue Property Members

        public const string PropertyName_FalseValue = "FalseValue";

        public static readonly DependencyProperty FalseValueProperty =
            DependencyProperty.Register(BooleanToTextConverter.PropertyName_FalseValue, typeof(string), typeof(BooleanToTextConverter),
                new PropertyMetadata("False"));

        public string FalseValue
        {
            get { return this.GetValue(BooleanToTextConverter.FalseValueProperty) as string; }
            set { this.SetValue(BooleanToTextConverter.FalseValueProperty, value); }
        }

        #endregion

        #region NullValue Property Members

        public const string PropertyName_NullValue = "NullValue";

        public static readonly DependencyProperty NullValueProperty =
            DependencyProperty.Register(BooleanToTextConverter.PropertyName_NullValue, typeof(string), typeof(BooleanToTextConverter),
                new PropertyMetadata("(null)"));

        public string NullValue
        {
            get { return this.GetValue(BooleanToTextConverter.NullValueProperty) as string; }
            set { this.SetValue(BooleanToTextConverter.NullValueProperty, value); }
        }

        #endregion

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? b = value as bool?;

            if (b.HasValue)
                return (b.Value) ? this.TrueValue : this.FalseValue;

            return this.NullValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
