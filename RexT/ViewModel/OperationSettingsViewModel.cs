using System;
using System.Windows;

namespace Erwine.Leonard.T.RexT.ViewModel
{
    public class OperationSettingsViewModel : DependencyObject
    {
        public event EventHandler<OperationSettingsValueEventArgs> ValueChanged;

        #region Value Property Members

        public const string PropertyName_Value = "Value";

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(OperationSettingsViewModel.PropertyName_Value, typeof(DataModel.OperationType), typeof(OperationSettingsViewModel),
                new PropertyMetadata(DataModel.OperationType.SingleMatch, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as OperationSettingsViewModel).OnValuePropertyChanged((DataModel.OperationType)(e.OldValue), (DataModel.OperationType)(e.NewValue))));

        public DataModel.OperationType Value
        {
            get { return (DataModel.OperationType)(this.GetValue(OperationSettingsViewModel.ValueProperty)); }
            set { this.SetValue(OperationSettingsViewModel.ValueProperty, value); }
        }

        protected virtual void OnValuePropertyChanged(DataModel.OperationType oldValue, DataModel.OperationType newValue)
        {
            this.SingleMatch = newValue == DataModel.OperationType.SingleMatch;
            this.MultiMatch = newValue == DataModel.OperationType.MultiMatch;
            this.Split = newValue == DataModel.OperationType.Split;
            this.Replace = newValue == DataModel.OperationType.Replace;
            if (this.ValueChanged != null)
                this.ValueChanged(this, new OperationSettingsValueEventArgs(newValue));
        }

        #endregion

        #region SingleMatch Property Members

        public const string PropertyName_SingleMatch = "SingleMatch";

        public static readonly DependencyProperty SingleMatchProperty =
            DependencyProperty.Register(OperationSettingsViewModel.PropertyName_SingleMatch, typeof(bool), typeof(OperationSettingsViewModel),
                new PropertyMetadata(true, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as OperationSettingsViewModel).OnSingleMatchPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool SingleMatch
        {
            get { return (bool)(this.GetValue(OperationSettingsViewModel.SingleMatchProperty)); }
            set { this.SetValue(OperationSettingsViewModel.SingleMatchProperty, value); }
        }

        protected virtual void OnSingleMatchPropertyChanged(bool oldValue, bool newValue)
        {
            if (newValue)
                this.Value = DataModel.OperationType.SingleMatch;
        }

        #endregion

        #region MultiMatch Property Members

        public const string PropertyName_MultiMatch = "MultiMatch";

        public static readonly DependencyProperty MultiMatchProperty =
            DependencyProperty.Register(OperationSettingsViewModel.PropertyName_MultiMatch, typeof(bool), typeof(OperationSettingsViewModel),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as OperationSettingsViewModel).OnMultiMatchPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool MultiMatch
        {
            get { return (bool)(this.GetValue(OperationSettingsViewModel.MultiMatchProperty)); }
            set { this.SetValue(OperationSettingsViewModel.MultiMatchProperty, value); }
        }

        protected virtual void OnMultiMatchPropertyChanged(bool oldValue, bool newValue)
        {
            if (newValue)
                this.Value = DataModel.OperationType.MultiMatch;
        }

        #endregion

        #region Split Property Members

        public const string PropertyName_Split = "Split";

        public static readonly DependencyProperty SplitProperty =
            DependencyProperty.Register(OperationSettingsViewModel.PropertyName_Split, typeof(bool), typeof(OperationSettingsViewModel),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as OperationSettingsViewModel).OnSplitPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool Split
        {
            get { return (bool)(this.GetValue(OperationSettingsViewModel.SplitProperty)); }
            set { this.SetValue(OperationSettingsViewModel.SplitProperty, value); }
        }

        protected virtual void OnSplitPropertyChanged(bool oldValue, bool newValue)
        {
            if (newValue)
                this.Value = DataModel.OperationType.Split;
        }

        #endregion

        #region Replace Property Members

        public const string PropertyName_Replace = "Replace";

        public static readonly DependencyProperty ReplaceProperty =
            DependencyProperty.Register(OperationSettingsViewModel.PropertyName_Replace, typeof(bool), typeof(OperationSettingsViewModel),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as OperationSettingsViewModel).OnReplacePropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool Replace
        {
            get { return (bool)(this.GetValue(OperationSettingsViewModel.ReplaceProperty)); }
            set { this.SetValue(OperationSettingsViewModel.ReplaceProperty, value); }
        }

        protected virtual void OnReplacePropertyChanged(bool oldValue, bool newValue)
        {
            if (newValue)
                this.Value = DataModel.OperationType.Replace;
        }

        #endregion
    }
}
