using System;
using System.Windows;

namespace Erwine.Leonard.T.RexT.ViewModel.Home
{
    public class InputTextItem : DependencyObject
    {
        #region Name Property Members

        public const string PropertyName_Name = "Name";

        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register(InputTextItem.PropertyName_Name, typeof(string), typeof(InputTextItem),
                new PropertyMetadata(""));

        public string Name
        {
            get { return this.GetValue(InputTextItem.NameProperty) as string; }
            set { this.SetValue(InputTextItem.NameProperty, value); }
        }

        #endregion

        #region AlwaysEvaluate Property Members

        public const string PropertyName_AlwaysEvaluate = "AlwaysEvaluate";

        public static readonly DependencyProperty AlwaysEvaluateProperty =
            DependencyProperty.Register(InputTextItem.PropertyName_AlwaysEvaluate, typeof(bool), typeof(InputTextItem),
                new PropertyMetadata(false));

        public bool AlwaysEvaluate
        {
            get { return (bool)(this.GetValue(InputTextItem.AlwaysEvaluateProperty)); }
            set { this.SetValue(InputTextItem.AlwaysEvaluateProperty, value); }
        }

        #endregion

        #region Text Property Members

        public const string PropertyName_Text = "Text";

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(InputTextItem.PropertyName_Text, typeof(string), typeof(InputTextItem),
                new PropertyMetadata(""));

        public string Text
        {
            get { return this.GetValue(InputTextItem.TextProperty) as string; }
            set { this.SetValue(InputTextItem.TextProperty, value); }
        }

        #endregion

        #region IsMultiline Property Members

        public const string PropertyName_IsMultiline = "IsMultiline";

        public static readonly DependencyProperty IsMultilineProperty =
            DependencyProperty.Register(InputTextItem.PropertyName_IsMultiline, typeof(bool), typeof(InputTextItem),
                new PropertyMetadata(false));

        public bool IsMultiline
        {
            get { return (bool)(this.GetValue(InputTextItem.IsMultilineProperty)); }
            set { this.SetValue(InputTextItem.IsMultilineProperty, value); }
        }

        #endregion

        #region WordWrap Property Members

        public const string PropertyName_WordWrap = "WordWrap";

        public static readonly DependencyProperty WordWrapProperty =
            DependencyProperty.Register(InputTextItem.PropertyName_WordWrap, typeof(bool), typeof(InputTextItem),
                new PropertyMetadata(true));

        public bool WordWrap
        {
            get { return (bool)(this.GetValue(InputTextItem.WordWrapProperty)); }
            set { this.SetValue(InputTextItem.WordWrapProperty, value); }
        }

        #endregion

        #region TextInputEncoding Property Members

        public const string PropertyName_TextInputEncoding = "TextInputEncoding";

        public static readonly DependencyProperty TextInputEncodingProperty =
            DependencyProperty.Register(InputTextItem.PropertyName_TextInputEncoding, typeof(DataModel.TextInputEncodingValue), typeof(InputTextItem),
                new PropertyMetadata(DataModel.TextInputEncodingValue.Unicode, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as InputTextItem).OnTextInputEncodingPropertyChanged((DataModel.TextInputEncodingValue)(e.OldValue), (DataModel.TextInputEncodingValue)(e.NewValue))));

        public DataModel.TextInputEncodingValue TextInputEncoding
        {
            get { return (DataModel.TextInputEncodingValue)(this.GetValue(InputTextItem.TextInputEncodingProperty)); }
            set { this.SetValue(InputTextItem.TextInputEncodingProperty, value); }
        }

        protected virtual void OnTextInputEncodingPropertyChanged(DataModel.TextInputEncodingValue oldValue, DataModel.TextInputEncodingValue newValue)
        {
            switch (newValue)
            {
                case DataModel.TextInputEncodingValue.Utf8:
                    this.TextInputEncodingUtf8 = true;
                    break;
                case DataModel.TextInputEncodingValue.Html:
                    this.TextInputEncodingHtml = true;
                    break;
                case DataModel.TextInputEncodingValue.Uri:
                    this.TextInputEncodingUri = true;
                    break;
                default:
                    this.TextInputEncodingUnicode = true;
                    break;
            }
        }

        #endregion

        #region TextInputEncodingUnicode Property Members

        public const string PropertyName_TextInputEncodingUnicode = "TextInputEncodingUnicode";

        public static readonly DependencyProperty TextInputEncodingUnicodeProperty =
            DependencyProperty.Register(InputTextItem.PropertyName_TextInputEncodingUnicode, typeof(bool), typeof(InputTextItem),
                new PropertyMetadata(true, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as InputTextItem).OnTextInputEncodingUnicodePropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool TextInputEncodingUnicode
        {
            get { return (bool)(this.GetValue(InputTextItem.TextInputEncodingUnicodeProperty)); }
            set { this.SetValue(InputTextItem.TextInputEncodingUnicodeProperty, value); }
        }

        protected virtual void OnTextInputEncodingUnicodePropertyChanged(bool oldValue, bool newValue)
        {
            if (newValue)
                this.TextInputEncoding = DataModel.TextInputEncodingValue.Unicode;
        }

        #endregion

        #region TextInputEncodingUtf8 Property Members

        public const string PropertyName_TextInputEncodingUtf8 = "TextInputEncodingUtf8";

        public static readonly DependencyProperty TextInputEncodingUtf8Property =
            DependencyProperty.Register(InputTextItem.PropertyName_TextInputEncodingUtf8, typeof(bool), typeof(InputTextItem),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as InputTextItem).OnTextInputEncodingUtf8PropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool TextInputEncodingUtf8
        {
            get { return (bool)(this.GetValue(InputTextItem.TextInputEncodingUtf8Property)); }
            set { this.SetValue(InputTextItem.TextInputEncodingUtf8Property, value); }
        }

        protected virtual void OnTextInputEncodingUtf8PropertyChanged(bool oldValue, bool newValue)
        {
            if (newValue)
                this.TextInputEncoding = DataModel.TextInputEncodingValue.Utf8;
        }

        #endregion

        #region TextInputEncodingHtml Property Members

        public const string PropertyName_TextInputEncodingHtml = "TextInputEncodingHtml";

        public static readonly DependencyProperty TextInputEncodingHtmlProperty =
            DependencyProperty.Register(InputTextItem.PropertyName_TextInputEncodingHtml, typeof(bool), typeof(InputTextItem),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as InputTextItem).OnTextInputEncodingHtmlPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool TextInputEncodingHtml
        {
            get { return (bool)(this.GetValue(InputTextItem.TextInputEncodingHtmlProperty)); }
            set { this.SetValue(InputTextItem.TextInputEncodingHtmlProperty, value); }
        }

        protected virtual void OnTextInputEncodingHtmlPropertyChanged(bool oldValue, bool newValue)
        {
            if (newValue)
                this.TextInputEncoding = DataModel.TextInputEncodingValue.Html;
        }

        #endregion

        #region TextInputEncodingUri Property Members

        public const string PropertyName_TextInputEncodingUri = "TextInputEncodingUri";

        public static readonly DependencyProperty TextInputEncodingUriProperty =
            DependencyProperty.Register(InputTextItem.PropertyName_TextInputEncodingUri, typeof(bool), typeof(InputTextItem),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as InputTextItem).OnTextInputEncodingUriPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool TextInputEncodingUri
        {
            get { return (bool)(this.GetValue(InputTextItem.TextInputEncodingUriProperty)); }
            set { this.SetValue(InputTextItem.TextInputEncodingUriProperty, value); }
        }

        protected virtual void OnTextInputEncodingUriPropertyChanged(bool oldValue, bool newValue)
        {
            if (newValue)
                this.TextInputEncoding = DataModel.TextInputEncodingValue.Uri;
        }

        #endregion

        #region Delete Command Property Members

        public event EventHandler Delete;

        private Events.RelayCommand _deleteCommand = null;

        public Events.RelayCommand DeleteCommand
        {
            get
            {
                if (this._deleteCommand == null)
                    this._deleteCommand = new Events.RelayCommand(this.OnDelete);

                return this._deleteCommand;
            }
        }

        protected virtual void OnDelete(object parameter)
        {
            if (this.Delete != null)
                this.Delete(this, EventArgs.Empty);
        }

        #endregion

        public InputTextItem() : base() { }

        public InputTextItem(DataModel.InputTextSettings input)
            : base()
        {
            this.AlwaysEvaluate = input.AlwaysEvaluate;
            this.IsMultiline = input.IsMultiline;
            this.Name = input.Name;
            this.Text = input.Text;
            this.TextInputEncoding = input.Encoding;
            this.WordWrap = input.WordWrap;
        }

        internal string GetText()
        {
            throw new NotImplementedException();
        }
    }
}
