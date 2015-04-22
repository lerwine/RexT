using System;
using System.Collections;
using System.Windows;
using System.Windows.Data;

namespace Erwine.Leonard.T.RexT.Converters
{
    public class CollectionToVisibilityConverter : DependencyObject, IValueConverter
    {
        #region EmptyValue Property Members

        public const string PropertyName_EmptyValue = "EmptyValue";

        public static readonly DependencyProperty EmptyValueProperty =
            DependencyProperty.Register(CollectionToVisibilityConverter.PropertyName_EmptyValue, typeof(Visibility), typeof(CollectionToVisibilityConverter),
                new PropertyMetadata(Visibility.Collapsed));

        public Visibility EmptyValue
        {
            get { return (Visibility)(this.GetValue(CollectionToVisibilityConverter.EmptyValueProperty)); }
            set { this.SetValue(CollectionToVisibilityConverter.EmptyValueProperty, value); }
        }

        #endregion

        #region NonEmptyValue Property Members

        public const string PropertyName_NonEmptyValue = "NonEmptyValue";

        public static readonly DependencyProperty NonEmptyValueProperty =
            DependencyProperty.Register(CollectionToVisibilityConverter.PropertyName_NonEmptyValue, typeof(Visibility), typeof(CollectionToVisibilityConverter),
                new PropertyMetadata(Visibility.Visible));

        public Visibility NonEmptyValue
        {
            get { return (Visibility)(this.GetValue(CollectionToVisibilityConverter.NonEmptyValueProperty)); }
            set { this.SetValue(CollectionToVisibilityConverter.NonEmptyValueProperty, value); }
        }

        #endregion

        #region NullValue Property Members

        public const string PropertyName_NullValue = "NullValue";

        public static readonly DependencyProperty NullValueProperty =
            DependencyProperty.Register(CollectionToVisibilityConverter.PropertyName_NullValue, typeof(Visibility), typeof(CollectionToVisibilityConverter),
                new PropertyMetadata(Visibility.Collapsed));

        public Visibility NullValue
        {
            get { return (Visibility)(this.GetValue(CollectionToVisibilityConverter.NullValueProperty)); }
            set { this.SetValue(CollectionToVisibilityConverter.NullValueProperty, value); }
        }

        #endregion

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ICollection collection = value as ICollection;
            if (collection == null)
                return this.NullValue;

            return (collection.Count == 0) ? this.EmptyValue : this.NonEmptyValue;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
