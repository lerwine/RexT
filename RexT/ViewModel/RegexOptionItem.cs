using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace Erwine.Leonard.T.RexT.ViewModel
{
    public class RegexOptionItem : DependencyObject
    {
        public event EventHandler SelectedChanged;

        #region Value Property Members

        public const string PropertyName_Value = "Value";

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(RegexOptionItem.PropertyName_Value, typeof(RegexOptions), typeof(RegexOptionItem),
                new PropertyMetadata(RegexOptions.None));

        public RegexOptions Value
        {
            get { return (RegexOptions)(this.GetValue(RegexOptionItem.ValueProperty)); }
            private set { this.SetValue(RegexOptionItem.ValueProperty, value); }
        }

        #endregion

        #region DisplayText Property Members

        public const string PropertyName_DisplayText = "DisplayText";

        public static readonly DependencyProperty DisplayTextProperty =
            DependencyProperty.Register(RegexOptionItem.PropertyName_DisplayText, typeof(string), typeof(RegexOptionItem),
                new PropertyMetadata(""));

        public string DisplayText
        {
            get { return this.GetValue(RegexOptionItem.DisplayTextProperty) as string; }
            private set { this.SetValue(RegexOptionItem.DisplayTextProperty, value); }
        }

        #endregion

        #region HelpText Property Members

        public const string PropertyName_HelpText = "HelpText";

        public static readonly DependencyProperty HelpTextProperty =
            DependencyProperty.Register(RegexOptionItem.PropertyName_HelpText, typeof(string), typeof(RegexOptionItem),
                new PropertyMetadata(""));

        public string HelpText
        {
            get { return this.GetValue(RegexOptionItem.HelpTextProperty) as string; }
            private set { this.SetValue(RegexOptionItem.HelpTextProperty, value); }
        }

        #endregion

        #region IsSelected Property Members

        public const string PropertyName_IsSelected = "IsSelected";

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(RegexOptionItem.PropertyName_IsSelected, typeof(bool), typeof(RegexOptionItem),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as RegexOptionItem).OnIsSelectedPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool IsSelected
        {
            get { return (bool)(this.GetValue(RegexOptionItem.IsSelectedProperty)); }
            set { this.SetValue(RegexOptionItem.IsSelectedProperty, value); }
        }

        protected virtual void OnIsSelectedPropertyChanged(bool oldValue, bool newValue)
        {
            this.NotSelected = !newValue;
            this.RaiseSelectedChanged();
        }

        #region NotSelected Property Members

        public const string PropertyName_NotSelected = "NotSelected";

        public static readonly DependencyProperty NotSelectedProperty =
            DependencyProperty.Register(RegexOptionItem.PropertyName_NotSelected, typeof(bool), typeof(RegexOptionItem),
                new PropertyMetadata(true, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as RegexOptionItem).OnNotSelectedPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool NotSelected
        {
            get { return (bool)(this.GetValue(RegexOptionItem.NotSelectedProperty)); }
            set { this.SetValue(RegexOptionItem.NotSelectedProperty, value); }
        }

        protected virtual void OnNotSelectedPropertyChanged(bool oldValue, bool newValue)
        {
            this.IsSelected = !newValue;
        }

        #endregion

        #endregion

        public RegexOptionItem() : this(RegexOptions.None) { }

        public RegexOptionItem(RegexOptions value)
        {
            this.Value = value;
            switch (value)
            {
                case RegexOptions.CultureInvariant:
                    this.DisplayText = "Culture Invariant";
                    this.HelpText = "Specifies that cultural differences in language are ignored. Ordinarily, the regular expression engine performs string comparisons based on the conventions of the current culture. If the System.Text.RegularExpressions.RegexOptions.CultureInvariant option is specified, it uses the conventions of the invariant culture.";
                    break;
                case RegexOptions.ECMAScript:
                    this.DisplayText = "ECMAScript";
                    this.HelpText = "Enables ECMAScript-compliant behavior for the expression. This value can be used only in conjunction with the System.Text.RegularExpressions.RegexOptions.IgnoreCase and System.Text.RegularExpressions.RegexOptions.Multiline values. The use of this value with any other values results in an exception.";
                    break;
                case RegexOptions.ExplicitCapture:
                    this.DisplayText = "Explicit capture";
                    this.HelpText = "Specifies that the only valid captures are explicitly named or numbered groups of the form (?<name>…). This allows unnamed parentheses to act as noncapturing groups without the syntactic clumsiness of the expression (?:…).";
                    break;
                case RegexOptions.IgnoreCase:
                    this.DisplayText = "Ignore case";
                    this.HelpText = "Specifies case-insensitive matching.";
                    break;
                case RegexOptions.IgnorePatternWhitespace:
                    this.DisplayText = "Ignore pattern whitespace";
                    this.HelpText = "Eliminates unescaped white space from the pattern and enables comments marked with #. However, the System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace value does not affect or eliminate white space in character classes";
                    break;
                case RegexOptions.Multiline:
                    this.DisplayText = "Multi-line";
                    this.HelpText = "Multiline mode. Changes the meaning of ^ and $ so they match at the beginning and end, respectively, of any line, and not just the beginning and end of the entire string.";
                    break;
                case RegexOptions.RightToLeft:
                    this.DisplayText = "Right-to-Left";
                    this.HelpText = "Specifies that the search will be from right to left instead of from left to right.";
                    break;
                case RegexOptions.Singleline:
                    this.DisplayText = "Single-line";
                    this.HelpText = "Specifies single-line mode. Changes the meaning of the dot (.) so it matches every character (instead of every character except \n).";
                    break;
                default:
                    this.DisplayText = value.ToString("F");
                    this.HelpText = "";
                    break;
            }
        }

        protected void RaiseSelectedChanged()
        {
            if (this.SelectedChanged != null)
                this.SelectedChanged(this, EventArgs.Empty);
        }
    }
}
