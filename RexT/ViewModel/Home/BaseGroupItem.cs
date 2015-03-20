using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Erwine.Leonard.T.RexT.ViewModel.Home
{
    public class BaseGroupItem : CaptureResultItem
    {
        #region Success Property Members

        public const string PropertyName_Success = "Success";

        public static readonly DependencyProperty SuccessProperty =
            DependencyProperty.Register(BaseGroupItem.PropertyName_Success, typeof(bool), typeof(BaseGroupItem),
                new PropertyMetadata(false));

        public bool Success
        {
            get { return (bool)(this.GetValue(BaseGroupItem.SuccessProperty)); }
            set { this.SetValue(BaseGroupItem.SuccessProperty, value); }
        }

        #endregion

        #region Captures Property Members

        public const string PropertyName_Captures = "Captures";

        public static readonly DependencyProperty CapturesProperty =
            DependencyProperty.Register(BaseGroupItem.PropertyName_Captures, typeof(ObservableCollection<CaptureResultItem>), typeof(BaseGroupItem),
                new PropertyMetadata(null));

        public ObservableCollection<CaptureResultItem> Captures
        {
            get { return (ObservableCollection<CaptureResultItem>)(this.GetValue(BaseGroupItem.CapturesProperty)); }
            set { this.SetValue(BaseGroupItem.CapturesProperty, value); }
        }

        #endregion

        #region ShowCaptures Property Members

        public const string PropertyName_ShowCaptures = "ShowCaptures";

        public static readonly DependencyProperty ShowCapturesProperty =
            DependencyProperty.Register(BaseGroupItem.PropertyName_ShowCaptures, typeof(bool), typeof(BaseGroupItem),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as BaseGroupItem).OnShowCapturesPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool ShowCaptures
        {
            get { return (bool)(this.GetValue(BaseGroupItem.ShowCapturesProperty)); }
            set { this.SetValue(BaseGroupItem.ShowCapturesProperty, value); }
        }

        protected virtual void OnShowCapturesPropertyChanged(bool oldValue, bool newValue)
        {
            this.ExpandCapturesCommand.IsEnabled = !newValue;
            this.CollapseCapturesCommand.IsEnabled = newValue;
            this.ShowEmptyCaptures = this.Captures.Count == 0 && newValue;
        }

        #endregion

        #region ExpandCaptures Command Property Members

        private Events.RelayCommand _expandCapturesCommand = null;

        public Events.RelayCommand ExpandCapturesCommand
        {
            get
            {
                if (this._expandCapturesCommand == null)
                    this._expandCapturesCommand = new Events.RelayCommand(this.OnExpandCaptures);

                return this._expandCapturesCommand;
            }
        }

        protected virtual void OnExpandCaptures(object parameter)
        {
            this.ShowCaptures = true;
        }

        #endregion

        #region CollapseCaptures Command Property Members

        private Events.RelayCommand _collapseCapturesCommand = null;

        public Events.RelayCommand CollapseCapturesCommand
        {
            get
            {
                if (this._collapseCapturesCommand == null)
                    this._collapseCapturesCommand = new Events.RelayCommand(this.OnCollapseCaptures);

                return this._collapseCapturesCommand;
            }
        }

        protected virtual void OnCollapseCaptures(object parameter)
        {
            this.ShowCaptures = false;
        }

        #endregion

        #region ToggleCaptures Command Property Members

        private Events.RelayCommand _toggleCapturesCommand = null;

        public Events.RelayCommand ToggleCapturesCommand
        {
            get
            {
                if (this._toggleCapturesCommand == null)
                    this._toggleCapturesCommand = new Events.RelayCommand(this.OnToggleCaptures);

                return this._toggleCapturesCommand;
            }
        }

        protected virtual void OnToggleCaptures(object parameter)
        {
            this.ShowCaptures = !this.ShowCaptures;
        }

        #endregion

        #region ShowEmptyCaptures Property Members

        public const string PropertyName_ShowEmptyCaptures = "ShowEmptyCaptures";

        public static readonly DependencyProperty ShowEmptyCapturesProperty =
            DependencyProperty.Register(BaseGroupItem.PropertyName_ShowEmptyCaptures, typeof(bool), typeof(BaseGroupItem),
                new PropertyMetadata(false));

        public bool ShowEmptyCaptures
        {
            get { return (bool)(this.GetValue(BaseGroupItem.ShowEmptyCapturesProperty)); }
            set { this.SetValue(BaseGroupItem.ShowEmptyCapturesProperty, value); }
        }

        #endregion

        public BaseGroupItem()
            : base()
        {
            this.Captures = new ObservableCollection<CaptureResultItem>();
        }

        public BaseGroupItem(Group group)
            : base(group)
        {
            this.Success = group.Success;
            
            this.Captures = new ObservableCollection<CaptureResultItem>(group.Captures.OfType<Capture>().Select(c => new CaptureResultItem(c)));
            this.ShowEmptyCaptures = this.Captures.Count == 0 && this.ShowCaptures;
        }
    }
}
