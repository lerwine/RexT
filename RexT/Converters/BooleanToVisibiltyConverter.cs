using System;
using System.Windows;
using System.Windows.Data;

namespace Erwine.Leonard.T.RexT.Converters
{
    public class BooleanToVisibiltyConverter : DependencyObject, IValueConverter
    {
        #region TrueValue Property Members

        public const string PropertyName_TrueValue = "TrueValue";

        public static readonly DependencyProperty TrueValueProperty =
            DependencyProperty.Register(BooleanToVisibiltyConverter.PropertyName_TrueValue, typeof(Visibility), typeof(BooleanToVisibiltyConverter),
                new PropertyMetadata(Visibility.Visible));

        public Visibility TrueValue
        {
            get { return (Visibility)(this.GetValue(BooleanToVisibiltyConverter.TrueValueProperty)); }
            set { this.SetValue(BooleanToVisibiltyConverter.TrueValueProperty, value); }
        }

        #endregion

        #region FalseValue Property Members

        public const string PropertyName_FalseValue = "FalseValue";

        public static readonly DependencyProperty FalseValueProperty =
            DependencyProperty.Register(BooleanToVisibiltyConverter.PropertyName_FalseValue, typeof(Visibility), typeof(BooleanToVisibiltyConverter),
                new PropertyMetadata(Visibility.Collapsed));

        public Visibility FalseValue
        {
            get { return (Visibility)(this.GetValue(BooleanToVisibiltyConverter.FalseValueProperty)); }
            set { this.SetValue(BooleanToVisibiltyConverter.FalseValueProperty, value); }
        }

        #endregion

        #region NullValue Property Members

        public const string PropertyName_NullValue = "NullValue";

        public static readonly DependencyProperty NullValueProperty =
            DependencyProperty.Register(BooleanToVisibiltyConverter.PropertyName_NullValue, typeof(Visibility), typeof(BooleanToVisibiltyConverter),
                new PropertyMetadata(Visibility.Collapsed));

        public Visibility NullValue
        {
            get { return (Visibility)(this.GetValue(BooleanToVisibiltyConverter.NullValueProperty)); }
            set { this.SetValue(BooleanToVisibiltyConverter.NullValueProperty, value); }
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
