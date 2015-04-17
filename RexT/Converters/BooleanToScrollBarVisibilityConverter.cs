using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Erwine.Leonard.T.RexT.Converters
{
    public class BooleanToScrollBarVisibilityConverter : DependencyObject, IValueConverter
    {
        #region TrueValue Property Members

        public const string PropertyName_TrueValue = "TrueValue";

        public static readonly DependencyProperty TrueValueProperty =
            DependencyProperty.Register(BooleanToScrollBarVisibilityConverter.PropertyName_TrueValue, typeof(ScrollBarVisibility), typeof(BooleanToScrollBarVisibilityConverter),
                new PropertyMetadata(ScrollBarVisibility.Auto));

        public ScrollBarVisibility TrueValue
        {
            get { return (ScrollBarVisibility)(this.GetValue(BooleanToScrollBarVisibilityConverter.TrueValueProperty)); }
            set { this.SetValue(BooleanToScrollBarVisibilityConverter.TrueValueProperty, value); }
        }

        #endregion

        #region FalseValue Property Members

        public const string PropertyName_FalseValue = "FalseValue";

        public static readonly DependencyProperty FalseValueProperty =
            DependencyProperty.Register(BooleanToScrollBarVisibilityConverter.PropertyName_FalseValue, typeof(ScrollBarVisibility), typeof(BooleanToScrollBarVisibilityConverter),
                new PropertyMetadata(ScrollBarVisibility.Disabled));

        public ScrollBarVisibility FalseValue
        {
            get { return (ScrollBarVisibility)(this.GetValue(BooleanToScrollBarVisibilityConverter.FalseValueProperty)); }
            set { this.SetValue(BooleanToScrollBarVisibilityConverter.FalseValueProperty, value); }
        }

        #endregion

        #region NullValue Property Members

        public const string PropertyName_NullValue = "NullValue";

        public static readonly DependencyProperty NullValueProperty =
            DependencyProperty.Register(BooleanToScrollBarVisibilityConverter.PropertyName_NullValue, typeof(ScrollBarVisibility), typeof(BooleanToScrollBarVisibilityConverter),
                new PropertyMetadata(ScrollBarVisibility.Disabled));

        public ScrollBarVisibility NullValue
        {
            get { return (ScrollBarVisibility)(this.GetValue(BooleanToScrollBarVisibilityConverter.NullValueProperty)); }
            set { this.SetValue(BooleanToScrollBarVisibilityConverter.NullValueProperty, value); }
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
