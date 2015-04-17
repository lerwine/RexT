using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

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
            DependencyProperty.Register(PageViewModel.PropertyName_InputText, typeof(ObservableCollection<InputTextItem>), typeof(PageViewModel),
                new PropertyMetadata(null, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as PageViewModel).OnInputTextPropertyChanged((ObservableCollection<InputTextItem>)(e.OldValue), (ObservableCollection<InputTextItem>)(e.NewValue))));

        public ObservableCollection<InputTextItem> InputText
        {
            get { return (ObservableCollection<InputTextItem>)(this.GetValue(PageViewModel.InputTextProperty)); }
            set { this.SetValue(PageViewModel.InputTextProperty, value); }
        }

        protected virtual void OnInputTextPropertyChanged(ObservableCollection<InputTextItem> oldValue, ObservableCollection<InputTextItem> newValue)
        {
            if (oldValue != null)
            {
                oldValue.CollectionChanged -= this.InputText_CollectionChanged;

                foreach (InputTextItem item in oldValue)
                    item.Delete -= this.InputTextItem_Delete;
            }

            if (newValue != null)
            {
                newValue.CollectionChanged += this.InputText_CollectionChanged;

                foreach (InputTextItem item in newValue)
                    item.Delete += this.InputTextItem_Delete;
            }
        }

        void InputText_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (InputTextItem item in e.OldItems.OfType<InputTextItem>())
                    item.Delete -= this.InputTextItem_Delete;
            }

            if (e.NewItems != null)
            {
                foreach (InputTextItem item in e.NewItems.OfType<InputTextItem>())
                    item.Delete += this.InputTextItem_Delete;
            }
        }

        void InputTextItem_Delete(object sender, EventArgs e)
        {
            this.InputText.Remove(sender as InputTextItem);
        }

        #endregion

        #region EvalOperation Property Members

        public const string PropertyName_EvalOperation = "EvalOperation";

        public static readonly DependencyProperty EvalOperationProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_EvalOperation, typeof(OperationSettingsViewModel), typeof(PageViewModel),
                new PropertyMetadata(null, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as PageViewModel).OnEvalOperationPropertyChanged((OperationSettingsViewModel)(e.OldValue), (OperationSettingsViewModel)(e.NewValue))));

        public OperationSettingsViewModel EvalOperation
        {
            get { return (OperationSettingsViewModel)(this.GetValue(PageViewModel.EvalOperationProperty)); }
            set { this.SetValue(PageViewModel.EvalOperationProperty, value); }
        }

        protected virtual void OnEvalOperationPropertyChanged(OperationSettingsViewModel oldValue, OperationSettingsViewModel newValue)
        {
            if (oldValue != null)
                oldValue.ValueChanged -= this.EvalOperation_ValueChanged;

            if (newValue == null)
                this.EvalOperation = new OperationSettingsViewModel();
            else
            {
                newValue.ValueChanged += this.EvalOperation_ValueChanged;
                this.EvalOperation_ValueChanged(newValue, new OperationSettingsValueEventArgs(newValue.Value));
            }
        }

        private void EvalOperation_ValueChanged(object sender, OperationSettingsValueEventArgs e)
        {
            switch (e.Value)
            {
                case DataModel.OperationType.SingleMatch:
                    this.ShowCountOption = false;
                    break;
                case DataModel.OperationType.MultiMatch:
                    this.ShowCountOption = true;
                    break;
                case DataModel.OperationType.Replace:
                    this.ShowCountOption = false;
                    break;
                case DataModel.OperationType.Split:
                    this.ShowCountOption = true;
                    break;
            }
        }

        #endregion

        #region Options Property Members

        public const string PropertyName_Options = "Options";

        public static readonly DependencyProperty OptionsProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_Options, typeof(RegexOptionsViewModel), typeof(PageViewModel),
                new PropertyMetadata(null, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as PageViewModel).OnOptionsPropertyChanged((RegexOptionsViewModel)(e.OldValue), (RegexOptionsViewModel)(e.NewValue))));

        public RegexOptionsViewModel Options
        {
            get { return (RegexOptionsViewModel)(this.GetValue(PageViewModel.OptionsProperty)); }
            set { this.SetValue(PageViewModel.OptionsProperty, value); }
        }

        protected virtual void OnOptionsPropertyChanged(RegexOptionsViewModel oldValue, RegexOptionsViewModel newValue)
        {
            if (oldValue != null)
                oldValue.ValueChanged -= this.Options_ValueChanged;

            if (newValue == null)
                this.Options = new RegexOptionsViewModel();
            else
            {
                newValue.ValueChanged += this.Options_ValueChanged;
                this.Options_ValueChanged(newValue, new RegexOptionsValueEventArgs(newValue.Value));
            }
        }

        private void Options_ValueChanged(object sender, RegexOptionsValueEventArgs e)
        {
#warning not implemented
        }

        #endregion

        #region ErrorMessage Property Members

        public const string PropertyName_ErrorMessage = "ErrorMessage";

        public static readonly DependencyProperty ErrorMessageProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_ErrorMessage, typeof(string), typeof(PageViewModel),
                new PropertyMetadata("Provide pattern and input, then click the \"Evaluate\" button to evaluate the regular expression pattern.", (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
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

            if (this.HasError)
            {
                this.ShowMatchResultListing = false;
                this.ShowTextResultListing = false;
            }
        }

        #endregion

        #region HasError Property Members

        public const string PropertyName_HasError = "HasError";

        public static readonly DependencyProperty HasErrorProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_HasError, typeof(bool), typeof(PageViewModel),
                new PropertyMetadata(true));

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

        #region IsBusy Property Members

        public const string PropertyName_IsBusy = "IsBusy";

        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_IsBusy, typeof(bool), typeof(PageViewModel),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as PageViewModel).OnIsBusyPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool IsBusy
        {
            get { return (bool)(this.GetValue(PageViewModel.IsBusyProperty)); }
            set { this.SetValue(PageViewModel.IsBusyProperty, value); }
        }

        protected virtual void OnIsBusyPropertyChanged(bool oldValue, bool newValue)
        {
            this.EvaluateExpressionCommand.IsEnabled = !newValue;
            this.LoadCommand.IsEnabled = !newValue;
            this.SaveCommand.IsEnabled = !newValue;
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

        protected virtual void OnEvaluateExpression(object parameter)
        {
            this.IsBusy = true;
            this.ShowMatchResultListing = false;
            this.ShowTextResultListing = false;
            this.ErrorMessage = "";
            string pattern = this.Pattern;
            bool specifyCount = this.SpecifyCount;
            string replacementText = this.ReplacementText;
            int countValue = this.CountValue;
            List<Tuple<InputTextItem, Match[]>> matches = new List<Tuple<InputTextItem,Match[]>>();
            List<Tuple<InputTextItem, string[]>> results = new List<Tuple<InputTextItem,string[]>>();
            Regex regex = null;

            Task task = Task.Factory.StartNew(() =>
            {
                regex = new Regex(pattern, this.Options.Value);
                foreach (InputTextItem item in this.InputText)
                {
                    switch (this.EvalOperation.Value)
                    {
                        case DataModel.OperationType.SingleMatch:
                            matches.Add(new Tuple<InputTextItem, Match[]>(item, new Match[] { regex.Match(item.GetText()) }));
                            break;
                        case DataModel.OperationType.MultiMatch:
                            matches.Add(new Tuple<InputTextItem, Match[]>(item, regex.Matches(item.GetText()).OfType<Match>().ToArray()));
                            break;
                        case DataModel.OperationType.Replace:
                            results.Add(new Tuple<InputTextItem, string[]>(item, new string[] { (specifyCount) ? regex.Replace(item.GetText(), replacementText, countValue) :
                                regex.Replace(item.GetText(), replacementText) }));
                            break;
                        default:
                            results.Add(new Tuple<InputTextItem, string[]>(item, (specifyCount) ? regex.Split(item.GetText(), countValue) : regex.Split(item.GetText())));
                            break;
                    }
                }
            });

            task.Wait();

            if (task.IsFaulted)
                this.ErrorMessage = task.Exception.Message;
            else
            {
                switch (this.EvalOperation.Value)
                {
                    case DataModel.OperationType.SingleMatch:
                        this.ShowResults(matches, regex, false);
                        break;
                    case DataModel.OperationType.MultiMatch:
                        this.ShowResults(matches, regex, true);
                        break;
                    case DataModel.OperationType.Replace:
                        this.ShowResults(results, false);
                        break;
                    default:
                        this.ShowResults(results, true);
                        break;
                }
            }

            this.IsBusy = false;
        }

        private void ShowResults(List<Tuple<InputTextItem, string[]>> results, bool displayResultCount)
        {
            this.ShowMatchResultListing = false;

            if (results.All(r => r.Item2.Length == 0))
            {
                this.ErrorMessage = "No matches.";
                return;
            }

            this.TextResults.Clear();
            foreach (Tuple<InputTextItem, string[]> item in results)
            {
                this.TextResults.Add(new TextResultItem(String.Format("{0}: {1} result{2}.", item.Item1.Name, (item.Item2.Length == 0) ? "no" : item.Item2.Length.ToString(),
                    (item.Item2.Length == 1) ? "" : "s")));
                foreach (string s in item.Item2)
                    this.TextResults.Add(new TextResultItem(s, true));
            }

            this.ShowTextResultListing = true;
            this.CountResultMessage = (displayResultCount) ? String.Format("{0} items.", results.Count) : "";

        }

        private void ShowResults(List<Tuple<InputTextItem, Match[]>> matches, Regex regex, bool displayResultCount)
        {
            this.ShowTextResultListing = false;

            if (matches.All(r => r.Item2.Length == 0))
            {
                this.ErrorMessage = "No matches.";
                return;
            }

            this.MatchResults.Clear();
            foreach (Tuple<InputTextItem, Match[]> item in matches)
            {
                this.MatchResults.Add(new MatchResultItem(String.Format("{0}: {1} result{2}.", item.Item1.Name, (item.Item2.Length == 0) ? "no" : item.Item2.Length.ToString(),
                    (item.Item2.Length == 1) ? "" : "s")));
                foreach (Match m in item.Item2)
                    this.MatchResults.Add(new MatchResultItem(m, regex));
            }

            this.ShowMatchResultListing = true;
            this.CountResultMessage = (displayResultCount) ? String.Format("{0} matches.", matches.Count) : "";
        }

        #endregion

        #region NewInput Command Property Members

        private Events.RelayCommand _newInputCommand = null;

        public Events.RelayCommand NewInputCommand
        {
            get
            {
                if (this._newInputCommand == null)
                    this._newInputCommand = new Events.RelayCommand(this.OnNewInput);

                return this._newInputCommand;
            }
        }

        protected virtual void OnNewInput(object parameter)
        {
            this.InputText.Add(new InputTextItem());
        }

        #endregion

        #region Load Command Property Members

        private Events.RelayCommand _loadCommand = null;

        public Events.RelayCommand LoadCommand
        {
            get
            {
                if (this._loadCommand == null)
                    this._loadCommand = new Events.RelayCommand(this.OnLoad);

                return this._loadCommand;
            }
        }

        protected virtual void OnLoad(object parameter)
        {
            this.IsBusy = true;
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                dialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
                dialog.Multiselect = false;
                bool? result = dialog.ShowDialog();
                if (result.HasValue && result.Value)
                {
                    using (FileStream stream = dialog.File.OpenRead())
                    {
                        DataModel.SingleInputRegexTestSettings settings = DataModel.SingleInputRegexTestSettings.Load(stream);
                        this.Pattern = (settings.Pattern == null) ? "" : settings.Pattern;
                        this.EvalOperation.Value =  settings.Operation;
                        this.Options.Value = settings.Options;
                        this.InputText.Clear();
                        if (settings.Input != null)
                        {
                            foreach (DataModel.InputTextSettings input in settings.Input.Where(i => i != null))
                                this.InputText.Add(new InputTextItem(input));
                        }

                        if (this.InputText.Count == 0)
                            this.InputText.Add(new InputTextItem());
                    }
                }
            }
            catch (Exception exc)
            {
                this.ErrorMessage = exc.Message;
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        #endregion

        #region Save Command Property Members

        private Events.RelayCommand _saveCommand = null;

        public Events.RelayCommand SaveCommand
        {
            get
            {
                if (this._saveCommand == null)
                    this._saveCommand = new Events.RelayCommand(this.OnSave);

                return this._saveCommand;
            }
        }

        protected virtual void OnSave(object parameter)
        {
            this.IsBusy = true;
            try
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
                bool? result = dialog.ShowDialog();
                if (result.HasValue && result.Value)
                {
                    using (Stream stream = dialog.OpenFile())
                    {
                        DataModel.SingleInputRegexTestSettings settings = new DataModel.SingleInputRegexTestSettings
                        {
                            Input = this.InputText.Select(i => new DataModel.InputTextSettings(i)).ToArray(),
                            Operation = this.EvalOperation.Value,
                            Options = this.Options.Value,
                            Pattern = this.Pattern
                        };
                        settings.Save(stream);
                    }
                }
            }
            catch (Exception exc)
            {
                this.ErrorMessage = exc.Message;
            }
            finally
            {
                this.IsBusy = false;
            }
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

        public PageViewModel()
            : base()
        {
            this.Options = new RegexOptionsViewModel();
            this.EvalOperation = new OperationSettingsViewModel();
            this.TextResults = new ObservableCollection<TextResultItem>();
            this.MatchResults = new ObservableCollection<MatchResultItem>();
        }
    }
}
