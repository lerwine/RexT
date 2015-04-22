using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;

namespace Erwine.Leonard.T.RexT.ViewModel.Home
{
    public class TextDetailCharViewModel : DependencyObject
    {
        #region Column Property Members

        public const string PropertyName_Column = "Column";

        public static readonly DependencyProperty ColumnProperty =
            DependencyProperty.Register(TextDetailCharViewModel.PropertyName_Column, typeof(int), typeof(TextDetailCharViewModel),
                new PropertyMetadata(0));

        public int Column
        {
            get { return (int)(this.GetValue(TextDetailCharViewModel.ColumnProperty)); }
            set { this.SetValue(TextDetailCharViewModel.ColumnProperty, value); }
        }

        #endregion

        #region Category Property Members

        public const string PropertyName_Category = "Category";

        public static readonly DependencyProperty CategoryProperty =
            DependencyProperty.Register(TextDetailCharViewModel.PropertyName_Category, typeof(UnicodeCategory), typeof(TextDetailCharViewModel),
                new PropertyMetadata(default(UnicodeCategory)));

        public UnicodeCategory Category
        {
            get { return (UnicodeCategory)(this.GetValue(TextDetailCharViewModel.CategoryProperty)); }
            set { this.SetValue(TextDetailCharViewModel.CategoryProperty, value); }
        }

        #endregion

        #region Value Property Members

        public const string PropertyName_Value = "Value";

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(TextDetailCharViewModel.PropertyName_Value, typeof(ushort), typeof(TextDetailCharViewModel),
                new PropertyMetadata((ushort)0));

        public ushort Value
        {
            get { return (ushort)(this.GetValue(TextDetailCharViewModel.ValueProperty)); }
            set { this.SetValue(TextDetailCharViewModel.ValueProperty, value); }
        }

        #endregion

        #region Content Property Members

        public const string PropertyName_Content = "Content";

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(TextDetailCharViewModel.PropertyName_Content, typeof(string), typeof(TextDetailCharViewModel),
                new PropertyMetadata(""));

        public string Content
        {
            get { return this.GetValue(TextDetailCharViewModel.ContentProperty) as string; }
            set { this.SetValue(TextDetailCharViewModel.ContentProperty, value); }
        }

        #endregion

        public TextDetailCharViewModel() { }

        public TextDetailCharViewModel(int column, char value)
        {
            this.Column = column;
            this.Content = (Char.IsControl(value) || char.IsWhiteSpace(value)) ? "" : new String(new char[] { value });
            this.Category = Char.GetUnicodeCategory(value);
            this.Value = (ushort)value;
        }
    }
}
