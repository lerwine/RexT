using System;
using System.Collections;
using System.Windows;
using System.Windows.Data;

namespace Erwine.Leonard.T.RexT.Converters
{
    public class CollectionToTextConverter : DependencyObject, IValueConverter
    {
        #region EmptyValue Property Members

        public const string PropertyName_EmptyValue = "EmptyValue";

        public static readonly DependencyProperty EmptyValueProperty =
            DependencyProperty.Register(CollectionToTextConverter.PropertyName_EmptyValue, typeof(string), typeof(CollectionToTextConverter),
                new PropertyMetadata(null));

        public string EmptyValue
        {
            get { return this.GetValue(CollectionToTextConverter.EmptyValueProperty) as string; }
            set { this.SetValue(CollectionToTextConverter.EmptyValueProperty, value); }
        }

        #endregion

        #region NonEmptyValue Property Members

        public const string PropertyName_NonEmptyValue = "NonEmptyValue";

        public static readonly DependencyProperty NonEmptyValueProperty =
            DependencyProperty.Register(CollectionToTextConverter.PropertyName_NonEmptyValue, typeof(string), typeof(CollectionToTextConverter),
                new PropertyMetadata(null));

        public string NonEmptyValue
        {
            get { return this.GetValue(CollectionToTextConverter.NonEmptyValueProperty) as string; }
            set { this.SetValue(CollectionToTextConverter.NonEmptyValueProperty, value); }
        }

        #endregion

        #region NullValue Property Members

        public const string PropertyName_NullValue = "NullValue";

        public static readonly DependencyProperty NullValueProperty =
            DependencyProperty.Register(CollectionToTextConverter.PropertyName_NullValue, typeof(string), typeof(CollectionToTextConverter),
                new PropertyMetadata("(null)"));

        public string NullValue
        {
            get { return this.GetValue(CollectionToTextConverter.NullValueProperty) as string; }
            set { this.SetValue(CollectionToTextConverter.NullValueProperty, value); }
        }

        #endregion

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ICollection collection = value as ICollection;
            if (collection == null)
                return this.NullValue;

            if (collection.Count == 0)
                return (this.EmptyValue == null) ? collection.Count.ToString() : this.EmptyValue;

            return (this.NonEmptyValue == null) ? collection.Count.ToString() : this.NonEmptyValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
