using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace Erwine.Leonard.T.RexT.ViewModel
{
    public class RegexOptionsViewModel : DependencyObject
    {
        public event EventHandler<RegexOptionsValueEventArgs> ValueChanged;

        private bool _ignoreEvent = false;

        #region Value Property Members

        public const string PropertyName_Value = "Value";

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(RegexOptionsViewModel.PropertyName_Value, typeof(RegexOptions), typeof(RegexOptionsViewModel),
                new PropertyMetadata(RegexOptions.None, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as RegexOptionsViewModel).OnValuePropertyChanged((RegexOptions)(e.OldValue), (RegexOptions)(e.NewValue))));

        public RegexOptions Value
        {
            get { return (RegexOptions)(this.GetValue(RegexOptionsViewModel.ValueProperty)); }
            set { this.SetValue(RegexOptionsViewModel.ValueProperty, value); }
        }

        protected virtual void OnValuePropertyChanged(RegexOptions oldValue, RegexOptions newValue)
        {
            if (this._ignoreEvent)
                return;

            if (newValue == RegexOptions.None)
            {
                this.None = true;
                if (this.ValueChanged != null)
                    this.ValueChanged(this, new RegexOptionsValueEventArgs(newValue));
                return;
            }

            this._ignoreEvent = true;
            try
            {
                this.None = false;
                if (newValue.HasFlag(RegexOptions.ECMAScript))
                {
                    RegexOptions value = RegexOptions.ECMAScript;
                    this.ECMAScript = true;
                    if (newValue.HasFlag(RegexOptions.IgnoreCase))
                    {
                        this.IgnoreCase = true;
                        value |= RegexOptions.IgnoreCase;
                    }
                    else
                        this.IgnoreCase = false;
                    if (newValue.HasFlag(RegexOptions.Multiline))
                    {
                        this.Multiline = true;
                        value |= RegexOptions.Multiline;
                    }
                    else
                        this.Multiline = false;

                    if (newValue != value)
                        this.Value = value;
                }
                else
                {
                    this.ECMAScript = false;
                    this.CultureInvariant = newValue.HasFlag(RegexOptions.CultureInvariant);
                    this.ExplicitCapture = newValue.HasFlag(RegexOptions.ExplicitCapture);
                    this.IgnoreCase = newValue.HasFlag(RegexOptions.IgnoreCase);
                    this.IgnorePatternWhitespace = newValue.HasFlag(RegexOptions.IgnorePatternWhitespace);
                    this.Multiline = newValue.HasFlag(RegexOptions.Multiline);
                    this.RightToLeft = newValue.HasFlag(RegexOptions.RightToLeft);
                    this.Singleline = newValue.HasFlag(RegexOptions.Singleline);
                }

                if (this.ValueChanged != null)
                    this.ValueChanged(this, new RegexOptionsValueEventArgs(newValue));
            }
            catch
            {
                throw;
            }
            finally
            {
                this._ignoreEvent = false;
            }
        }

        private void UpdateValue()
        {
            RegexOptions value = RegexOptions.None;
            if (this.None)
            {
                this.Value = value;
                return;
            }

            if (this.IgnoreCase)
                value |= RegexOptions.IgnoreCase;
            if (this.Multiline)
                value |= RegexOptions.Multiline;
            if (this.ECMAScript)
            {
                value |= RegexOptions.ECMAScript;
                this.Value = value;
                return;
            }

            if (this.CultureInvariant)
                value |= RegexOptions.CultureInvariant;
            if (this.ExplicitCapture)
                value |= RegexOptions.ExplicitCapture;
            if (this.IgnorePatternWhitespace)
                value |= RegexOptions.IgnorePatternWhitespace;
            if (this.RightToLeft)
                value |= RegexOptions.RightToLeft;
            if (this.Singleline)
                value |= RegexOptions.Singleline;
            this.Value = value;
        }

        #endregion

        public RegexOptionsViewModel() : base() { }

        #region None Property Members

        public const string PropertyName_None = "None";

        public static readonly DependencyProperty NoneProperty =
            DependencyProperty.Register(RegexOptionsViewModel.PropertyName_None, typeof(bool), typeof(RegexOptionsViewModel),
                new PropertyMetadata(true, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as RegexOptionsViewModel).OnNonePropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool None
        {
            get { return (bool)(this.GetValue(RegexOptionsViewModel.NoneProperty)); }
            set { this.SetValue(RegexOptionsViewModel.NoneProperty, value); }
        }

        protected virtual void OnNonePropertyChanged(bool oldValue, bool newValue)
        {
            if (!newValue || this._ignoreEvent)
                return;

            this.CultureInvariant = false;
            this.ECMAScript = false;
            this.ExplicitCapture = false;
            this.IgnoreCase = false;
            this.IgnorePatternWhitespace = false;
            this.Multiline = false;
            this.RightToLeft = false;
            this.Singleline = false;
            this.UpdateValue();
        }

        #endregion

        #region IgnoreCase Property Members

        public const string PropertyName_IgnoreCase = "IgnoreCase";

        public static readonly DependencyProperty IgnoreCaseProperty =
            DependencyProperty.Register(RegexOptionsViewModel.PropertyName_IgnoreCase, typeof(bool), typeof(RegexOptionsViewModel),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as RegexOptionsViewModel).OnIgnoreCasePropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool IgnoreCase
        {
            get { return (bool)(this.GetValue(RegexOptionsViewModel.IgnoreCaseProperty)); }
            set { this.SetValue(RegexOptionsViewModel.IgnoreCaseProperty, value); }
        }

        protected virtual void OnIgnoreCasePropertyChanged(bool oldValue, bool newValue)
        {
            if (!newValue || this._ignoreEvent)
                return;

            this.None = false;
            this.UpdateValue();
        }

        #endregion

        #region Multiline Property Members

        public const string PropertyName_Multiline = "Multiline";

        public static readonly DependencyProperty MultilineProperty =
            DependencyProperty.Register(RegexOptionsViewModel.PropertyName_Multiline, typeof(bool), typeof(RegexOptionsViewModel),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as RegexOptionsViewModel).OnMultilinePropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool Multiline
        {
            get { return (bool)(this.GetValue(RegexOptionsViewModel.MultilineProperty)); }
            set { this.SetValue(RegexOptionsViewModel.MultilineProperty, value); }
        }

        protected virtual void OnMultilinePropertyChanged(bool oldValue, bool newValue)
        {
            if (!newValue || this._ignoreEvent)
                return;

            this.None = false;
            this.UpdateValue();
        }

        #endregion

        #region ExplicitCapture Property Members

        public const string PropertyName_ExplicitCapture = "ExplicitCapture";

        public static readonly DependencyProperty ExplicitCaptureProperty =
            DependencyProperty.Register(RegexOptionsViewModel.PropertyName_ExplicitCapture, typeof(bool), typeof(RegexOptionsViewModel),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as RegexOptionsViewModel).OnExplicitCapturePropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool ExplicitCapture
        {
            get { return (bool)(this.GetValue(RegexOptionsViewModel.ExplicitCaptureProperty)); }
            set { this.SetValue(RegexOptionsViewModel.ExplicitCaptureProperty, value); }
        }

        protected virtual void OnExplicitCapturePropertyChanged(bool oldValue, bool newValue)
        {
            if (!newValue || this._ignoreEvent)
                return;

            this.None = false;
            this.ECMAScript = false;
            this.UpdateValue();
        }

        #endregion

        #region Singleline Property Members

        public const string PropertyName_Singleline = "Singleline";

        public static readonly DependencyProperty SinglelineProperty =
            DependencyProperty.Register(RegexOptionsViewModel.PropertyName_Singleline, typeof(bool), typeof(RegexOptionsViewModel),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as RegexOptionsViewModel).OnSinglelinePropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool Singleline
        {
            get { return (bool)(this.GetValue(RegexOptionsViewModel.SinglelineProperty)); }
            set { this.SetValue(RegexOptionsViewModel.SinglelineProperty, value); }
        }

        protected virtual void OnSinglelinePropertyChanged(bool oldValue, bool newValue)
        {
            if (!newValue || this._ignoreEvent)
                return;

            this.None = false;
            this.ECMAScript = false;
            this.UpdateValue();
        }

        #endregion

        #region IgnorePatternWhitespace Property Members

        public const string PropertyName_IgnorePatternWhitespace = "IgnorePatternWhitespace";

        public static readonly DependencyProperty IgnorePatternWhitespaceProperty =
            DependencyProperty.Register(RegexOptionsViewModel.PropertyName_IgnorePatternWhitespace, typeof(bool), typeof(RegexOptionsViewModel),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as RegexOptionsViewModel).OnIgnorePatternWhitespacePropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool IgnorePatternWhitespace
        {
            get { return (bool)(this.GetValue(RegexOptionsViewModel.IgnorePatternWhitespaceProperty)); }
            set { this.SetValue(RegexOptionsViewModel.IgnorePatternWhitespaceProperty, value); }
        }

        protected virtual void OnIgnorePatternWhitespacePropertyChanged(bool oldValue, bool newValue)
        {
            if (!newValue || this._ignoreEvent)
                return;

            this.None = false;
            this.ECMAScript = false;
            this.UpdateValue();
        }

        #endregion

        #region RightToLeft Property Members

        public const string PropertyName_RightToLeft = "RightToLeft";

        public static readonly DependencyProperty RightToLeftProperty =
            DependencyProperty.Register(RegexOptionsViewModel.PropertyName_RightToLeft, typeof(bool), typeof(RegexOptionsViewModel),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as RegexOptionsViewModel).OnRightToLeftPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool RightToLeft
        {
            get { return (bool)(this.GetValue(RegexOptionsViewModel.RightToLeftProperty)); }
            set { this.SetValue(RegexOptionsViewModel.RightToLeftProperty, value); }
        }

        protected virtual void OnRightToLeftPropertyChanged(bool oldValue, bool newValue)
        {
            if (!newValue || this._ignoreEvent)
                return;

            this.None = false;
            this.ECMAScript = false;
            this.UpdateValue();
        }

        #endregion

        #region ECMAScript Property Members

        public const string PropertyName_ECMAScript = "ECMAScript";

        public static readonly DependencyProperty ECMAScriptProperty =
            DependencyProperty.Register(RegexOptionsViewModel.PropertyName_ECMAScript, typeof(bool), typeof(RegexOptionsViewModel),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as RegexOptionsViewModel).OnECMAScriptPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool ECMAScript
        {
            get { return (bool)(this.GetValue(RegexOptionsViewModel.ECMAScriptProperty)); }
            set { this.SetValue(RegexOptionsViewModel.ECMAScriptProperty, value); }
        }

        protected virtual void OnECMAScriptPropertyChanged(bool oldValue, bool newValue)
        {
            if (!newValue || this._ignoreEvent)
                return;

            this.CultureInvariant = false;
            this.ExplicitCapture = false;
            this.IgnorePatternWhitespace = false;
            this.None = false;
            this.RightToLeft = false;
            this.Singleline = false;
            this.UpdateValue();
        }

        #endregion

        #region CultureInvariant Property Members

        public const string PropertyName_CultureInvariant = "CultureInvariant";

        public static readonly DependencyProperty CultureInvariantProperty =
            DependencyProperty.Register(RegexOptionsViewModel.PropertyName_CultureInvariant, typeof(bool), typeof(RegexOptionsViewModel),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as RegexOptionsViewModel).OnCultureInvariantPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool CultureInvariant
        {
            get { return (bool)(this.GetValue(RegexOptionsViewModel.CultureInvariantProperty)); }
            set { this.SetValue(RegexOptionsViewModel.CultureInvariantProperty, value); }
        }

        protected virtual void OnCultureInvariantPropertyChanged(bool oldValue, bool newValue)
        {
            if (!newValue || this._ignoreEvent)
                return;

            this.ECMAScript = false;
            this.None = false;
            this.UpdateValue();
        }

        #endregion
    }
}
