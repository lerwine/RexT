using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Erwine.Leonard.T.RexT.Converters
{
    public class OperationTypeToVisibilityConverter : DependencyObject, IValueConverter
    {
        #region SingleMatchValue Property Members

        public const string PropertyName_SingleMatchValue = "SingleMatchValue";

        public static readonly DependencyProperty SingleMatchValueProperty =
            DependencyProperty.Register(OperationTypeToVisibilityConverter.PropertyName_SingleMatchValue, typeof(Visibility?), typeof(OperationTypeToVisibilityConverter),
                new PropertyMetadata(Visibility.Visible));

        public Visibility? SingleMatchValue
        {
            get { return (Visibility?)(this.GetValue(OperationTypeToVisibilityConverter.SingleMatchValueProperty)); }
            set { this.SetValue(OperationTypeToVisibilityConverter.SingleMatchValueProperty, value); }
        }

        #endregion

        #region MultiMatchValue Property Members

        public const string PropertyName_MultiMatchValue = "MultiMatchValue";

        public static readonly DependencyProperty MultiMatchValueProperty =
            DependencyProperty.Register(OperationTypeToVisibilityConverter.PropertyName_MultiMatchValue, typeof(Visibility?), typeof(OperationTypeToVisibilityConverter),
                new PropertyMetadata(Visibility.Visible));

        public Visibility? MultiMatchValue
        {
            get { return (Visibility?)(this.GetValue(OperationTypeToVisibilityConverter.MultiMatchValueProperty)); }
            set { this.SetValue(OperationTypeToVisibilityConverter.MultiMatchValueProperty, value); }
        }

        #endregion

        #region SplitValue Property Members

        public const string PropertyName_SplitValue = "SplitValue";

        public static readonly DependencyProperty SplitValueProperty =
            DependencyProperty.Register(OperationTypeToVisibilityConverter.PropertyName_SplitValue, typeof(Visibility?), typeof(OperationTypeToVisibilityConverter),
                new PropertyMetadata(Visibility.Collapsed));

        public Visibility? SplitValue
        {
            get { return (Visibility?)(this.GetValue(OperationTypeToVisibilityConverter.SplitValueProperty)); }
            set { this.SetValue(OperationTypeToVisibilityConverter.SplitValueProperty, value); }
        }

        #endregion

        #region ReplaceValue Property Members

        public const string PropertyName_ReplaceValue = "ReplaceValue";

        public static readonly DependencyProperty ReplaceValueProperty =
            DependencyProperty.Register(OperationTypeToVisibilityConverter.PropertyName_ReplaceValue, typeof(Visibility?), typeof(OperationTypeToVisibilityConverter),
                new PropertyMetadata(Visibility.Collapsed));

        public Visibility? ReplaceValue
        {
            get { return (Visibility?)(this.GetValue(OperationTypeToVisibilityConverter.ReplaceValueProperty)); }
            set { this.SetValue(OperationTypeToVisibilityConverter.ReplaceValueProperty, value); }
        }

        #endregion

        #region NullValue Property Members

        public const string PropertyName_NullValue = "NullValue";

        public static readonly DependencyProperty NullValueProperty =
            DependencyProperty.Register(OperationTypeToVisibilityConverter.PropertyName_NullValue, typeof(Visibility?), typeof(OperationTypeToVisibilityConverter),
                new PropertyMetadata(null));

        public Visibility? NullValue
        {
            get { return (Visibility?)(this.GetValue(OperationTypeToVisibilityConverter.NullValueProperty)); }
            set { this.SetValue(OperationTypeToVisibilityConverter.NullValueProperty, value); }
        }

        #endregion

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DataModel.OperationType? operationType = value as DataModel.OperationType?;
            if (!operationType.HasValue)
                return this.NullValue;

            switch (operationType.Value)
            {
                case DataModel.OperationType.SingleMatch:
                    return this.SingleMatchValue;
                case DataModel.OperationType.MultiMatch:
                    return this.MultiMatchValue;
                case DataModel.OperationType.Replace:
                    return this.ReplaceValue;
            }

            return this.SplitValue;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
