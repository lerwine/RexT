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
    public class OperationTypeToTextConverter : DependencyObject, IValueConverter
    {
        #region SingleMatchValue Property Members

        public const string PropertyName_SingleMatchValue = "SingleMatchValue";

        public static readonly DependencyProperty SingleMatchValueProperty =
            DependencyProperty.Register(OperationTypeToTextConverter.PropertyName_SingleMatchValue, typeof(string), typeof(OperationTypeToTextConverter),
                new PropertyMetadata("Match not found."));

        public string SingleMatchValue
        {
            get { return this.GetValue(OperationTypeToTextConverter.SingleMatchValueProperty) as string; }
            set { this.SetValue(OperationTypeToTextConverter.SingleMatchValueProperty, value); }
        }

        #endregion

        #region MultiMatchValue Property Members

        public const string PropertyName_MultiMatchValue = "MultiMatchValue";

        public static readonly DependencyProperty MultiMatchValueProperty =
            DependencyProperty.Register(OperationTypeToTextConverter.PropertyName_MultiMatchValue, typeof(string), typeof(OperationTypeToTextConverter),
                new PropertyMetadata("No matches found."));

        public string MultiMatchValue
        {
            get { return this.GetValue(OperationTypeToTextConverter.MultiMatchValueProperty) as string; }
            set { this.SetValue(OperationTypeToTextConverter.MultiMatchValueProperty, value); }
        }

        #endregion

        #region SplitValue Property Members

        public const string PropertyName_SplitValue = "SplitValue";

        public static readonly DependencyProperty SplitValueProperty =
            DependencyProperty.Register(OperationTypeToTextConverter.PropertyName_SplitValue, typeof(string), typeof(OperationTypeToTextConverter),
                new PropertyMetadata("No items to be shown."));

        public string SplitValue
        {
            get { return this.GetValue(OperationTypeToTextConverter.SplitValueProperty) as string; }
            set { this.SetValue(OperationTypeToTextConverter.SplitValueProperty, value); }
        }

        #endregion

        #region ReplaceValue Property Members

        public const string PropertyName_ReplaceValue = "ReplaceValue";

        public static readonly DependencyProperty ReplaceValueProperty =
            DependencyProperty.Register(OperationTypeToTextConverter.PropertyName_ReplaceValue, typeof(string), typeof(OperationTypeToTextConverter),
                new PropertyMetadata("No text to show."));

        public string ReplaceValue
        {
            get { return this.GetValue(OperationTypeToTextConverter.ReplaceValueProperty) as string; }
            set { this.SetValue(OperationTypeToTextConverter.ReplaceValueProperty, value); }
        }

        #endregion

        #region NullValue Property Members

        public const string PropertyName_NullValue = "NullValue";

        public static readonly DependencyProperty NullValueProperty =
            DependencyProperty.Register(OperationTypeToTextConverter.PropertyName_NullValue, typeof(string), typeof(OperationTypeToTextConverter),
                new PropertyMetadata(null));

        public string NullValue
        {
            get { return this.GetValue(OperationTypeToTextConverter.NullValueProperty) as string; }
            set { this.SetValue(OperationTypeToTextConverter.NullValueProperty, value); }
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
