using System;
using System.Windows;
using System.Windows.Data;

namespace Erwine.Leonard.T.RexT.Converters
{
    public class BooleanToTextWrappingConverter : DependencyObject, IValueConverter
    {
        #region TrueValue Property Members

        public const string PropertyName_TrueValue = "TrueValue";

        public static readonly DependencyProperty TrueValueProperty =
            DependencyProperty.Register(BooleanToTextWrappingConverter.PropertyName_TrueValue, typeof(TextWrapping), typeof(BooleanToTextWrappingConverter),
                new PropertyMetadata(TextWrapping.Wrap));

        public TextWrapping TrueValue
        {
            get { return (TextWrapping)(this.GetValue(BooleanToTextWrappingConverter.TrueValueProperty)); }
            set { this.SetValue(BooleanToTextWrappingConverter.TrueValueProperty, value); }
        }

        #endregion

        #region FalseValue Property Members

        public const string PropertyName_FalseValue = "FalseValue";

        public static readonly DependencyProperty FalseValueProperty =
            DependencyProperty.Register(BooleanToTextWrappingConverter.PropertyName_FalseValue, typeof(TextWrapping), typeof(BooleanToTextWrappingConverter),
                new PropertyMetadata(TextWrapping.NoWrap));

        public TextWrapping FalseValue
        {
            get { return (TextWrapping)(this.GetValue(BooleanToTextWrappingConverter.FalseValueProperty)); }
            set { this.SetValue(BooleanToTextWrappingConverter.FalseValueProperty, value); }
        }

        #endregion

        #region NullValue Property Members

        public const string PropertyName_NullValue = "NullValue";

        public static readonly DependencyProperty NullValueProperty =
            DependencyProperty.Register(BooleanToTextWrappingConverter.PropertyName_NullValue, typeof(TextWrapping), typeof(BooleanToTextWrappingConverter),
                new PropertyMetadata(TextWrapping.NoWrap));

        public TextWrapping NullValue
        {
            get { return (TextWrapping)(this.GetValue(BooleanToTextWrappingConverter.NullValueProperty)); }
            set { this.SetValue(BooleanToTextWrappingConverter.NullValueProperty, value); }
        }

        #endregion

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? b = value as bool?;

            if (b.HasValue)
                return (b.Value) ? this.TrueValue : this.FalseValue;

            return this.NullValue;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
