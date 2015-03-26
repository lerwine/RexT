using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Erwine.Leonard.T.RexT.ViewModel.Home
{
    public class PageViewModel : DependencyObject
    {
        #region Pattern Property Members

        public const string PropertyName_Pattern = "Pattern";

        public static readonly DependencyProperty PatternProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_Pattern, typeof(string), typeof(PageViewModel),
                new PropertyMetadata("", (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as PageViewModel).OnPatternPropertyChanged(e.OldValue as string, e.NewValue as string)));

        public string Pattern
        {
            get
            {
                string s = this.GetValue(PageViewModel.PatternProperty) as string;
                if (s == null)
                {
                    s = "";
                    this.SetValue(PageViewModel.PatternProperty, s);
                }

                return s;
            }
            set { this.SetValue(PageViewModel.PatternProperty, (value == null) ? "" : value); }
        }

        protected virtual void OnPatternPropertyChanged(string oldValue, string newValue)
        {
            if (newValue == null)
                this.Pattern = "";
        }

        #endregion

        #region InputText Property Members

        public const string PropertyName_InputText = "InputText";

        public static readonly DependencyProperty InputTextProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_InputText, typeof(string), typeof(PageViewModel),
                new PropertyMetadata("", (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as PageViewModel).OnInputTextPropertyChanged(e.OldValue as string, e.NewValue as string)));

        public string InputText
        {
            get
            {
                string s = this.GetValue(PageViewModel.InputTextProperty) as string;
                if (s == null)
                {
                    s = "";
                    this.SetValue(PageViewModel.InputTextProperty, s);
                }

                return s;
            }
            set { this.SetValue(PageViewModel.InputTextProperty, (value == null) ? "" : value); }
        }

        protected virtual void OnInputTextPropertyChanged(string oldValue, string newValue)
        {
            if (newValue == null)
                this.InputText = "";
        }

        #endregion

        #region Options Property Members

        public const string PropertyName_Options = "Options";

        public static readonly DependencyProperty OptionsProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_Options, typeof(RegexOptionsViewModel), typeof(PageViewModel),
                new PropertyMetadata(null));

        public RegexOptionsViewModel Options
        {
            get { return (RegexOptionsViewModel)(this.GetValue(PageViewModel.OptionsProperty)); }
            private set { this.SetValue(PageViewModel.OptionsProperty, value); }
        }

        #endregion

        #region ErrorMessage Property Members

        public const string PropertyName_ErrorMessage = "ErrorMessage";

        public static readonly DependencyProperty ErrorMessageProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_ErrorMessage, typeof(string), typeof(PageViewModel),
                new PropertyMetadata("", (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as PageViewModel).OnErrorMessagePropertyChanged(e.OldValue as string, e.NewValue as string)));

        public string ErrorMessage
        {
            get
            {
                string s = this.GetValue(PageViewModel.ErrorMessageProperty) as string;
                if (s == null)
                {
                    s = "";
                    this.SetValue(PageViewModel.ErrorMessageProperty, s);
                }

                return s;
            }
            private set { this.SetValue(PageViewModel.ErrorMessageProperty, (value == null) ? "" : value); }
        }

        protected virtual void OnErrorMessagePropertyChanged(string oldValue, string newValue)
        {
            if (newValue == null)
                this.ErrorMessage = "";
            else
                this.HasError = !String.IsNullOrWhiteSpace(newValue);
        }

        #endregion

        #region HasError Property Members

        public const string PropertyName_HasError = "HasError";

        public static readonly DependencyProperty HasErrorProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_HasError, typeof(bool), typeof(PageViewModel),
                new PropertyMetadata(false));

        public bool HasError
        {
            get { return (bool)(this.GetValue(PageViewModel.HasErrorProperty)); }
            private set { this.SetValue(PageViewModel.HasErrorProperty, value); }
        }

        #endregion

        #region ReplacementText Property Members

        public const string PropertyName_ReplacementText = "ReplacementText";

        public static readonly DependencyProperty ReplacementTextProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_ReplacementText, typeof(string), typeof(PageViewModel),
                new PropertyMetadata("", (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as PageViewModel).OnReplacementTextPropertyChanged(e.OldValue as string, e.NewValue as string)));

        public string ReplacementText
        {
            get
            {
                string s = this.GetValue(PageViewModel.ReplacementTextProperty) as string;
                if (s == null)
                {
                    s = "";
                    this.SetValue(PageViewModel.ReplacementTextProperty, s);
                }

                return s;
            }
            set { this.SetValue(PageViewModel.ReplacementTextProperty, (value == null) ? "" : value); }
        }

        protected virtual void OnReplacementTextPropertyChanged(string oldValue, string newValue)
        {
            if (newValue == null)
                this.ReplacementText = "";
        }

        #endregion

        #region SingleMatchOption Property Members

        public const string PropertyName_SingleMatchOption = "SingleMatchOption";

        public static readonly DependencyProperty SingleMatchOptionProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_SingleMatchOption, typeof(bool), typeof(PageViewModel),
                new PropertyMetadata(true, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as PageViewModel).OnSingleMatchOptionPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool SingleMatchOption
        {
            get { return (bool)(this.GetValue(PageViewModel.SingleMatchOptionProperty)); }
            set { this.SetValue(PageViewModel.SingleMatchOptionProperty, value); }
        }

        protected virtual void OnSingleMatchOptionPropertyChanged(bool oldValue, bool newValue)
        {
            if (!newValue)
                return;

            this.ShowCountOption = false;
            this.MultiMatchOption = false;
            this.ReplaceOption = false;
            this.SplitOption = false;
        }

        #endregion

        #region MultiMatchOption Property Members

        public const string PropertyName_MultiMatchOption = "MultiMatchOption";

        public static readonly DependencyProperty MultiMatchOptionProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_MultiMatchOption, typeof(bool), typeof(PageViewModel),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as PageViewModel).OnMultiMatchOptionPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool MultiMatchOption
        {
            get { return (bool)(this.GetValue(PageViewModel.MultiMatchOptionProperty)); }
            set { this.SetValue(PageViewModel.MultiMatchOptionProperty, value); }
        }

        protected virtual void OnMultiMatchOptionPropertyChanged(bool oldValue, bool newValue)
        {
            if (!newValue)
                return;

            this.ShowCountOption = false;
            this.SingleMatchOption = false;
            this.ReplaceOption = false;
            this.SplitOption = false;
        }

        #endregion

        #region ReplaceOption Property Members

        public const string PropertyName_ReplaceOption = "ReplaceOption";

        public static readonly DependencyProperty ReplaceOptionProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_ReplaceOption, typeof(bool), typeof(PageViewModel),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as PageViewModel).OnReplaceOptionPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool ReplaceOption
        {
            get { return (bool)(this.GetValue(PageViewModel.ReplaceOptionProperty)); }
            set { this.SetValue(PageViewModel.ReplaceOptionProperty, value); }
        }

        protected virtual void OnReplaceOptionPropertyChanged(bool oldValue, bool newValue)
        {
            if (!newValue)
                return;

            this.ShowCountOption = true;
            this.SingleMatchOption = false;
            this.MultiMatchOption = false;
            this.SplitOption = false;
        }

        #endregion

        #region SplitOption Property Members

        public const string PropertyName_SplitOption = "SplitOption";

        public static readonly DependencyProperty SplitOptionProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_SplitOption, typeof(bool), typeof(PageViewModel),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as PageViewModel).OnSplitOptionPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool SplitOption
        {
            get { return (bool)(this.GetValue(PageViewModel.SplitOptionProperty)); }
            set { this.SetValue(PageViewModel.SplitOptionProperty, value); }
        }

        protected virtual void OnSplitOptionPropertyChanged(bool oldValue, bool newValue)
        {
            if (!newValue)
                return;

            this.ShowCountOption = true;
            this.SingleMatchOption = false;
            this.MultiMatchOption = false;
            this.ReplaceOption = false;
        }

        #endregion

        #region CountValue Property Members

        public const string PropertyName_CountValue = "CountValue";

        public static readonly DependencyProperty CountValueProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_CountValue, typeof(int), typeof(PageViewModel), new PropertyMetadata(1));

        public int CountValue
        {
            get { return (int)(this.GetValue(PageViewModel.CountValueProperty)); }
            set { this.SetValue(PageViewModel.CountValueProperty, value); }
        }

        #endregion

        #region SpecifyCount Property Members

        public const string PropertyName_SpecifyCount = "SpecifyCount";

        public static readonly DependencyProperty SpecifyCountProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_SpecifyCount, typeof(bool), typeof(PageViewModel),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as PageViewModel).OnSpecifyCountPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool SpecifyCount
        {
            get { return (bool)(this.GetValue(PageViewModel.SpecifyCountProperty)); }
            set { this.SetValue(PageViewModel.SpecifyCountProperty, value); }
        }

        protected virtual void OnSpecifyCountPropertyChanged(bool oldValue, bool newValue)
        {
            this.UnlimitedCount = !newValue;
        }

        #endregion

        #region UnlimitedCount Property Members

        public const string PropertyName_UnlimitedCount = "UnlimitedCount";

        public static readonly DependencyProperty UnlimitedCountProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_UnlimitedCount, typeof(bool), typeof(PageViewModel),
                new PropertyMetadata(true, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as PageViewModel).OnUnlimitedCountPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool UnlimitedCount
        {
            get { return (bool)(this.GetValue(PageViewModel.UnlimitedCountProperty)); }
            set { this.SetValue(PageViewModel.UnlimitedCountProperty, value); }
        }

        protected virtual void OnUnlimitedCountPropertyChanged(bool oldValue, bool newValue)
        {
            this.SpecifyCount = !newValue;
        }

        #endregion

        #region ShowCountOption Property Members

        public const string PropertyName_ShowCountOption = "ShowCountOption";

        public static readonly DependencyProperty ShowCountOptionProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_ShowCountOption, typeof(bool), typeof(PageViewModel),
                new PropertyMetadata(false));

        public bool ShowCountOption
        {
            get { return (bool)(this.GetValue(PageViewModel.ShowCountOptionProperty)); }
            set { this.SetValue(PageViewModel.ShowCountOptionProperty, value); }
        }

        #endregion

        #region EvaluateExpression Command Property Members

        private Events.RelayCommand _evaluateExpressionCommand = null;

        public Events.RelayCommand EvaluateExpressionCommand
        {
            get
            {
                if (this._evaluateExpressionCommand == null)
                    this._evaluateExpressionCommand = new Events.RelayCommand(this.OnEvaluateExpression);

                return this._evaluateExpressionCommand;
            }
        }

        #region EnableControls Property Members

        public const string PropertyName_EnableControls = "EnableControls";

        public static readonly DependencyProperty EnableControlsProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_EnableControls, typeof(bool), typeof(PageViewModel),
                new PropertyMetadata(true));

        public bool EnableControls
        {
            get { return (bool)(this.GetValue(PageViewModel.EnableControlsProperty)); }
            private set { this.SetValue(PageViewModel.EnableControlsProperty, value); }
        }

        #endregion

        #region ShowMatchResultListing Property Members

        public const string PropertyName_ShowMatchResultListing = "ShowMatchResultListing";

        public static readonly DependencyProperty ShowMatchResultListingProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_ShowMatchResultListing, typeof(bool), typeof(PageViewModel),
                new PropertyMetadata(false));

        public bool ShowMatchResultListing
        {
            get { return (bool)(this.GetValue(PageViewModel.ShowMatchResultListingProperty)); }
            private set { this.SetValue(PageViewModel.ShowMatchResultListingProperty, value); }
        }

        #endregion

        #region ShowTextResultListing Property Members

        public const string PropertyName_ShowTextResultListing = "ShowTextResultListing";

        public static readonly DependencyProperty ShowTextResultListingProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_ShowTextResultListing, typeof(bool), typeof(PageViewModel),
                new PropertyMetadata(false));

        public bool ShowTextResultListing
        {
            get { return (bool)(this.GetValue(PageViewModel.ShowTextResultListingProperty)); }
            private set { this.SetValue(PageViewModel.ShowTextResultListingProperty, value); }
        }

        #endregion

        #region TextResults Property Members

        public const string PropertyName_TextResults = "TextResults";

        public static readonly DependencyProperty TextResultsProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_TextResults, typeof(ObservableCollection<TextResultItem>), typeof(PageViewModel),
                new PropertyMetadata(null));

        public ObservableCollection<TextResultItem> TextResults
        {
            get { return (ObservableCollection<TextResultItem>)(this.GetValue(PageViewModel.TextResultsProperty)); }
            private set { this.SetValue(PageViewModel.TextResultsProperty, value); }
        }

        #endregion

        #region MatchResults Property Members

        public const string PropertyName_MatchResults = "MatchResults";

        public static readonly DependencyProperty MatchResultsProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_MatchResults, typeof(ObservableCollection<MatchResultItem>), typeof(PageViewModel),
                new PropertyMetadata(null));

        public ObservableCollection<MatchResultItem> MatchResults
        {
            get { return (ObservableCollection<MatchResultItem>)(this.GetValue(PageViewModel.MatchResultsProperty)); }
            private set { this.SetValue(PageViewModel.MatchResultsProperty, value); }
        }

        #endregion

        #region CountResultMessage Property Members

        public const string PropertyName_CountResultMessage = "CountResultMessage";

        public static readonly DependencyProperty CountResultMessageProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_CountResultMessage, typeof(string), typeof(PageViewModel),
                new PropertyMetadata("", (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as PageViewModel).OnCountResultMessagePropertyChanged(e.OldValue as string, e.NewValue as string)));

        public string CountResultMessage
        {
            get { return this.GetValue(PageViewModel.CountResultMessageProperty) as string; }
            set { this.SetValue(PageViewModel.CountResultMessageProperty, value); }
        }

        protected virtual void OnCountResultMessagePropertyChanged(string oldValue, string newValue)
        {
            this.ShowCountMessage = !String.IsNullOrWhiteSpace(newValue);
        }

        #endregion

        #region ShowCountMessage Property Members

        public const string PropertyName_ShowCountMessage = "ShowCountMessage";

        public static readonly DependencyProperty ShowCountMessageProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_ShowCountMessage, typeof(bool), typeof(PageViewModel),
                new PropertyMetadata(false));

        public bool ShowCountMessage
        {
            get { return (bool)(this.GetValue(PageViewModel.ShowCountMessageProperty)); }
            set { this.SetValue(PageViewModel.ShowCountMessageProperty, value); }
        }

        #endregion

        protected virtual void OnEvaluateExpression(object parameter)
        {
            this.EnableControls = false;
            this.ShowMatchResultListing = false;
            this.ShowTextResultListing = false;
            this.ErrorMessage = "";
            string pattern = this.Pattern;
            RegexOptions options = this.Options.Value;
            int op;
            if (this.SingleMatchOption)
                op = 1;
            else if (this.MultiMatchOption)
                op = 2;
            else if (this.ReplaceOption)
                op = 3;
            else
                op = 4;
            string inputText = this.InputText;
            bool specifyCount = this.SpecifyCount;
            string replacementText = this.ReplacementText;
            int countValue = this.CountValue;
            Match[] matches = null;
            string[] results = null;
            Regex regex = null;
            Task task = Task.Factory.StartNew(() =>
            {
                regex = new Regex(pattern, options);
                switch (op)
                {
                    case 1:
                        matches = new Match[] { regex.Match(inputText) };
                        break;
                    case 2:
                        matches = regex.Matches(inputText).OfType<Match>().ToArray();
                        break;
                    case 3:
                        results = new string[] { (specifyCount) ? regex.Replace(inputText, replacementText, countValue) :
                            regex.Replace(inputText, replacementText) };
                        break;
                    default:
                        results = (specifyCount) ? regex.Split(inputText, countValue) : regex.Split(inputText);
                        break;
                }
            });

            task.Wait();
            if (task.IsFaulted)
                this.ErrorMessage = task.Exception.Message;
            else
            {
                switch (op)
                {
                    case 1:
                        this.ShowResults((matches[0].Success) ? matches : new Match[0], regex, false);
                        break;
                    case 2:
                        this.ShowResults(matches, regex, true);
                        break;
                    case 3:
                        this.ShowResults(results, false);
                        break;
                    default:
                        this.ShowResults(results, true);
                        break;
                }
            }
            this.EnableControls = true;
        }

        private void ShowResults(string[] results, bool displayResultCount)
        {
            this.TextResults.Clear();
            foreach (string s in results)
                this.TextResults.Add(new TextResultItem(s));

            this.ShowTextResultListing = true;
            this.CountResultMessage = (displayResultCount) ? String.Format("{0} items.", results.Length) : "";
        }

        private void ShowResults(Match[] matches, Regex regex, bool displayResultCount)
        {
            this.MatchResults.Clear();
            foreach (Match m in matches)
                this.MatchResults.Add(new MatchResultItem(m, regex));

            this.ShowMatchResultListing = true;
            this.CountResultMessage = (displayResultCount) ? String.Format("{0} matches.", matches.Length) : "";
        }

        #endregion

        public PageViewModel()
            : base()
        {
            this.Options = new RegexOptionsViewModel();
            this.TextResults = new ObservableCollection<TextResultItem>();
            this.MatchResults = new ObservableCollection<MatchResultItem>();
        }
    }
}
