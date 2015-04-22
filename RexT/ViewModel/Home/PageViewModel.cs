using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

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
            if (this.InputText.Count == 0)
            {
                this.InputText.Add(new InputTextItem());
                this.SelectedInput = 0;
            }
            else if (this.SelectedInput < 0)
                this.SelectedInput = 0;
            else if (this.SelectedInput >= this.InputText.Count)
                this.SelectedInput = this.InputText.Count - 1;
        }

        #endregion

        #region SelectedInput Property Members

        public const string PropertyName_SelectedInput = "SelectedInput";

        public static readonly DependencyProperty SelectedInputProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_SelectedInput, typeof(int), typeof(PageViewModel),
                new PropertyMetadata(-1));

        public int SelectedInput
        {
            get { return (int)(this.GetValue(PageViewModel.SelectedInputProperty)); }
            set { this.SetValue(PageViewModel.SelectedInputProperty, value); }
        }

        #endregion

        #region SelectedResult Property Members

        public const string PropertyName_SelectedResult = "SelectedResult";

        public static readonly DependencyProperty SelectedResultProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_SelectedResult, typeof(int), typeof(PageViewModel),
                new PropertyMetadata(-1));

        public int SelectedResult
        {
            get { return (int)(this.GetValue(PageViewModel.SelectedResultProperty)); }
            set { this.SetValue(PageViewModel.SelectedResultProperty, value); }
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

        #region Results Property Members

        public const string PropertyName_Results = "Results";

        public static readonly DependencyProperty ResultsProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_Results, typeof(ObservableCollection<EvalResultsViewModel>), typeof(PageViewModel),
                new PropertyMetadata(null));

        public ObservableCollection<EvalResultsViewModel> Results
        {
            get { return (ObservableCollection<EvalResultsViewModel>)(this.GetValue(PageViewModel.ResultsProperty)); }
            set { this.SetValue(PageViewModel.ResultsProperty, value); }
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

        #region OptionsTabSelected Property Members

        public const string PropertyName_OptionsTabSelected = "OptionsTabSelected";

        public static readonly DependencyProperty OptionsTabSelectedProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_OptionsTabSelected, typeof(bool), typeof(PageViewModel),
                new PropertyMetadata(true, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as PageViewModel).OnOptionsTabSelectedPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool OptionsTabSelected
        {
            get { return (bool)(this.GetValue(PageViewModel.OptionsTabSelectedProperty)); }
            set { this.SetValue(PageViewModel.OptionsTabSelectedProperty, value); }
        }

        protected virtual void OnOptionsTabSelectedPropertyChanged(bool oldValue, bool newValue)
        {
            if (!newValue)
                return;

            this.PatternTabSelected = false;
            this.InputTabSelected = false;
            this.ResultsTabSelected = false;
        }

        #endregion

        #region PatternTabSelected Property Members

        public const string PropertyName_PatternTabSelected = "PatternTabSelected";

        public static readonly DependencyProperty PatternTabSelectedProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_PatternTabSelected, typeof(bool), typeof(PageViewModel),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as PageViewModel).OnPatternTabSelectedPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool PatternTabSelected
        {
            get { return (bool)(this.GetValue(PageViewModel.PatternTabSelectedProperty)); }
            set { this.SetValue(PageViewModel.PatternTabSelectedProperty, value); }
        }

        protected virtual void OnPatternTabSelectedPropertyChanged(bool oldValue, bool newValue)
        {
            if (!newValue)
                return;

            this.OptionsTabSelected = false;
            this.InputTabSelected = false;
            this.ResultsTabSelected = false;
        }

        #endregion

        #region InputTabSelected Property Members

        public const string PropertyName_InputTabSelected = "InputTabSelected";

        public static readonly DependencyProperty InputTabSelectedProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_InputTabSelected, typeof(bool), typeof(PageViewModel),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as PageViewModel).OnInputTabSelectedPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool InputTabSelected
        {
            get { return (bool)(this.GetValue(PageViewModel.InputTabSelectedProperty)); }
            set { this.SetValue(PageViewModel.InputTabSelectedProperty, value); }
        }

        protected virtual void OnInputTabSelectedPropertyChanged(bool oldValue, bool newValue)
        {
            if (!newValue)
                return;

            this.OptionsTabSelected = false;
            this.PatternTabSelected = false;
            this.ResultsTabSelected = false;
        }

        #endregion

        #region ResultsTabSelected Property Members

        public const string PropertyName_ResultsTabSelected = "ResultsTabSelected";

        public static readonly DependencyProperty ResultsTabSelectedProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_ResultsTabSelected, typeof(bool), typeof(PageViewModel),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as PageViewModel).OnResultsTabSelectedPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool ResultsTabSelected
        {
            get { return (bool)(this.GetValue(PageViewModel.ResultsTabSelectedProperty)); }
            set { this.SetValue(PageViewModel.ResultsTabSelectedProperty, value); }
        }

        protected virtual void OnResultsTabSelectedPropertyChanged(bool oldValue, bool newValue)
        {
            if (!newValue)
                return;

            this.OptionsTabSelected = false;
            this.PatternTabSelected = false;
            this.InputTabSelected = false;
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

            try
            {
                Regex regex = new Regex(pattern, this.Options.Value);
                for (int index = 0; index < this.InputText.Count; index++)
                {
                    if (!this.InputText[index].AlwaysEvaluate && index != this.SelectedInput)
                        continue;

                    switch (this.EvalOperation.Value)
                    {
                        case DataModel.OperationType.SingleMatch:
                            matches.Add(new Tuple<InputTextItem, Match[]>(this.InputText[index], new Match[] { regex.Match(this.InputText[index].GetText()) }));
                            break;
                        case DataModel.OperationType.MultiMatch:
                            matches.Add(new Tuple<InputTextItem, Match[]>(this.InputText[index], regex.Matches(this.InputText[index].GetText()).OfType<Match>().ToArray()));
                            break;
                        case DataModel.OperationType.Replace:
                            results.Add(new Tuple<InputTextItem, string[]>(this.InputText[index], new string[] { (specifyCount) ? regex.Replace(this.InputText[index].GetText(), replacementText, countValue) :
                                regex.Replace(this.InputText[index].GetText(), replacementText) }));
                            break;
                        default:
                            results.Add(new Tuple<InputTextItem, string[]>(this.InputText[index], (specifyCount) ? regex.Split(this.InputText[index].GetText(), countValue) : regex.Split(this.InputText[index].GetText())));
                            break;
                    }
                }

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
            catch (Exception exc)
            {
                this.ErrorMessage = exc.Message;
            }

            this.ResultsTabSelected = true;
            this.SelectedResult = (this.Results.Count == 0) ? -1 : 0;
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

            this.Results.Clear();
            foreach (Tuple<InputTextItem, string[]> item in results)
                this.Results.Add(new EvalResultsViewModel(this, item.Item1, item.Item2, this.EvalOperation.Value));

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

            this.Results.Clear();
            foreach (Tuple<InputTextItem, Match[]> item in matches)
                this.Results.Add(new EvalResultsViewModel(this, item.Item1, regex, item.Item2, this.EvalOperation.Value));

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
            string name = "";
            for (int i = 0; i < int.MaxValue; i++)
            {
                string n = String.Format("Input #{0}", i);
                if (!this.InputText.Any(t => String.Compare(t.Name.Trim(), n, StringComparison.CurrentCultureIgnoreCase) == 0))
                {
                    name = n;
                    break;
                }
            }
            
            this.InputText.Add(new InputTextItem { Name = name });
            this.SelectedInput = this.InputText.Count - 1;
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

        public PageViewModel()
            : base()
        {
            this.Options = new RegexOptionsViewModel();
            this.EvalOperation = new OperationSettingsViewModel();
            this.Results = new ObservableCollection<EvalResultsViewModel>();
            this.InputText = new ObservableCollection<InputTextItem>();
            this.InputText.Add(new InputTextItem { Name = "Input #1" });
            this.SelectedInput = 0;
        }

        #region TextDetailIsVisible Property Members

        public const string PropertyName_TextDetailIsVisible = "TextDetailIsVisible";

        public static readonly DependencyProperty TextDetailIsVisibleProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_TextDetailIsVisible, typeof(bool), typeof(PageViewModel),
                new PropertyMetadata(false));

        public bool TextDetailIsVisible
        {
            get { return (bool)(this.GetValue(PageViewModel.TextDetailIsVisibleProperty)); }
            set { this.SetValue(PageViewModel.TextDetailIsVisibleProperty, value); }
        }

        #endregion

        #region TextDetailContent Property Members

        public const string PropertyName_TextDetailContent = "TextDetailContent";

        public static readonly DependencyProperty TextDetailContentProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_TextDetailContent, typeof(string), typeof(PageViewModel),
                new PropertyMetadata(""));

        public string TextDetailContent
        {
            get { return this.GetValue(PageViewModel.TextDetailContentProperty) as string; }
            set { this.SetValue(PageViewModel.TextDetailContentProperty, value); }
        }

        #endregion

        #region TextDetailLines Property Members

        public const string PropertyName_TextDetailLines = "TextDetailLines";

        public static readonly DependencyProperty TextDetailLinesProperty =
            DependencyProperty.Register(PageViewModel.PropertyName_TextDetailLines, typeof(ObservableCollection<TextDetailLineViewModel>), typeof(PageViewModel),
                new PropertyMetadata(null));

        public ObservableCollection<TextDetailLineViewModel> TextDetailLines
        {
            get { return (ObservableCollection<TextDetailLineViewModel>)(this.GetValue(PageViewModel.TextDetailLinesProperty)); }
            set { this.SetValue(PageViewModel.TextDetailLinesProperty, value); }
        }

        #endregion

        #region CloseTextDetails Command Property Members

        private Events.RelayCommand _closeTextDetailsCommand = null;

        public Events.RelayCommand CloseTextDetailsCommand
        {
            get
            {
                if (this._closeTextDetailsCommand == null)
                    this._closeTextDetailsCommand = new Events.RelayCommand(this.OnCloseTextDetails);

                return this._closeTextDetailsCommand;
            }
        }

        protected virtual void OnCloseTextDetails(object parameter)
        {
            this.TextDetailIsVisible = false;
        }

        #endregion

        public static readonly Regex GetLineRegex = new Regex(@"^[^\r\n]*(\r\n?|\n)");

        internal void ShowTextDetail(string text)
        {
            if (text == null)
            {
                this.TextDetailIsVisible = false;
                return;
            }

            this.TextDetailContent = text;
            this.TextDetailIsVisible = true;
            if (this.TextDetailLines == null)
                this.TextDetailLines = new ObservableCollection<TextDetailLineViewModel>();
            else
                this.TextDetailLines.Clear();

            string s = text;
            Match m;
            int lineNumber = 0;
            while ((m = PageViewModel.GetLineRegex.Match(s)).Success)
            {
                this.TextDetailLines.Add(new TextDetailLineViewModel(lineNumber, m.Value));
                lineNumber++;
                s = s.Substring(m.Length);
            }

            if (s.Length > 0)
                this.TextDetailLines.Add(new TextDetailLineViewModel(lineNumber, s));
        }
    }
}
