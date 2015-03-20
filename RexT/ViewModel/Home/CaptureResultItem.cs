using System.Text.RegularExpressions;
using System.Windows;

namespace Erwine.Leonard.T.RexT.ViewModel.Home
{
    public class CaptureResultItem : DependencyObject
    {
        #region Index Property Members

        public const string PropertyName_Index = "Index";

        public static readonly DependencyProperty IndexProperty =
            DependencyProperty.Register(CaptureResultItem.PropertyName_Index, typeof(int), typeof(CaptureResultItem),
                new PropertyMetadata(0));

        public int Index
        {
            get { return (int)(this.GetValue(CaptureResultItem.IndexProperty)); }
            set { this.SetValue(CaptureResultItem.IndexProperty, value); }
        }

        #endregion

        #region Length Property Members

        public const string PropertyName_Length = "Length";

        public static readonly DependencyProperty LengthProperty =
            DependencyProperty.Register(CaptureResultItem.PropertyName_Length, typeof(int), typeof(CaptureResultItem),
                new PropertyMetadata(0));

        public int Length
        {
            get { return (int)(this.GetValue(CaptureResultItem.LengthProperty)); }
            set { this.SetValue(CaptureResultItem.LengthProperty, value); }
        }

        #endregion

        #region Value Property Members

        public const string PropertyName_Value = "Value";

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(CaptureResultItem.PropertyName_Value, typeof(string), typeof(CaptureResultItem),
                new PropertyMetadata(""));

        public string Value
        {
            get { return this.GetValue(CaptureResultItem.ValueProperty) as string; }
            set { this.SetValue(CaptureResultItem.ValueProperty, value); }
        }

        #endregion

        public CaptureResultItem() : base() { }

        public CaptureResultItem(Capture capture)
            : base()
        {
            this.Index = capture.Index;
            this.Length = capture.Length;
            this.Value = capture.Value;
        }
    }
}
