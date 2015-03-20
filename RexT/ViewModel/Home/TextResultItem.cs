using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Erwine.Leonard.T.RexT.ViewModel.Home
{
    public class TextResultItem : DependencyObject
    {
        #region Length Property Members

        public const string PropertyName_Length = "Length";

        public static readonly DependencyProperty LengthProperty =
            DependencyProperty.Register(TextResultItem.PropertyName_Length, typeof(int), typeof(TextResultItem),
                new PropertyMetadata(0));

        public int Length
        {
            get { return (int)(this.GetValue(TextResultItem.LengthProperty)); }
            private set { this.SetValue(TextResultItem.LengthProperty, value); }
        }

        #endregion

        #region Value Property Members

        public const string PropertyName_Value = "Value";

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(TextResultItem.PropertyName_Value, typeof(string), typeof(TextResultItem),
                new PropertyMetadata("", (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as TextResultItem).OnValuePropertyChanged(e.OldValue as string, e.NewValue as string)));

        public string Value
        {
            get { return this.GetValue(TextResultItem.ValueProperty) as string; }
            set { this.SetValue(TextResultItem.ValueProperty, value); }
        }

        protected virtual void OnValuePropertyChanged(string oldValue, string newValue)
        {
            this.Length = (newValue == null) ? 0 : newValue.Length;
        }

        #endregion

        public TextResultItem() : this("") { }

        public TextResultItem(string value)
            : base()
        {
            this.Value = value;
        }
    }
}
