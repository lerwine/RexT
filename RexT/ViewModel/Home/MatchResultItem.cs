using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Erwine.Leonard.T.RexT.ViewModel.Home
{
    public class MatchResultItem : BaseGroupItem
    {
        #region Groups Property Members

        public const string PropertyName_Groups = "Groups";

        public static readonly DependencyProperty GroupsProperty =
            DependencyProperty.Register(MatchResultItem.PropertyName_Groups, typeof(ObservableCollection<GroupResultItem>), typeof(MatchResultItem),
                new PropertyMetadata(null));

        public ObservableCollection<GroupResultItem> Groups
        {
            get { return (ObservableCollection<GroupResultItem>)(this.GetValue(MatchResultItem.GroupsProperty)); }
            set { this.SetValue(MatchResultItem.GroupsProperty, value); }
        }

        #endregion

        #region ShowGroups Property Members

        public const string PropertyName_ShowGroups = "ShowGroups";

        public static readonly DependencyProperty ShowGroupsProperty =
            DependencyProperty.Register(MatchResultItem.PropertyName_ShowGroups, typeof(bool), typeof(MatchResultItem),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as MatchResultItem).OnShowGroupsPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool ShowGroups
        {
            get { return (bool)(this.GetValue(MatchResultItem.ShowGroupsProperty)); }
            set { this.SetValue(MatchResultItem.ShowGroupsProperty, value); }
        }

        protected virtual void OnShowGroupsPropertyChanged(bool oldValue, bool newValue)
        {
            this.ExpandGroupListCommand.IsEnabled = !newValue;
            this.CollapseGroupListCommand.IsEnabled = newValue;
            this.CollapseAllCommand.IsEnabled = newValue || this.ShowCaptures;
            this.ShowEmptyGroups = this.Groups.Count == 0 && newValue;
        }

        protected override void OnShowCapturesPropertyChanged(bool oldValue, bool newValue)
        {
            this.CollapseAllCommand.IsEnabled = newValue || this.ShowGroups;
        }

        #endregion

        #region ExpandGroupList Command Property Members

        private Events.RelayCommand _expandGroupListCommand = null;

        public Events.RelayCommand ExpandGroupListCommand
        {
            get
            {
                if (this._expandGroupListCommand == null)
                {
                    this._expandGroupListCommand = new Events.RelayCommand(this.OnExpandGroupList);
                    this._expandGroupListCommand.IsEnabled = !this.ShowGroups;
                }

                return this._expandGroupListCommand;
            }
        }

        protected virtual void OnExpandGroupList(object parameter)
        {
            this.ShowGroups = true;
        }

        #endregion

        #region CollapseGroupList Command Property Members

        private Events.RelayCommand _collapseGroupListCommand = null;

        public Events.RelayCommand CollapseGroupListCommand
        {
            get
            {
                if (this._collapseGroupListCommand == null)
                {
                    this._collapseGroupListCommand = new Events.RelayCommand(this.OnCollapseGroupList);
                    this._collapseGroupListCommand.IsEnabled = this.ShowGroups;
                }

                return this._collapseGroupListCommand;
            }
        }

        protected virtual void OnCollapseGroupList(object parameter)
        {
            this.ShowGroups = false;
        }

        #endregion

        #region ToggleGroupList Command Property Members

        private Events.RelayCommand _toggleGroupListCommand = null;

        public Events.RelayCommand ToggleGroupListCommand
        {
            get
            {
                if (this._toggleGroupListCommand == null)
                    this._toggleGroupListCommand = new Events.RelayCommand(this.OnToggleGroupList);

                return this._toggleGroupListCommand;
            }
        }

        protected virtual void OnToggleGroupList(object parameter)
        {
            this.ShowGroups = !this.ShowGroups;
        }

        #endregion

        #region CollapseAll Command Property Members

        private Events.RelayCommand _collapseAllCommand = null;

        public Events.RelayCommand CollapseAllCommand
        {
            get
            {
                if (this._collapseAllCommand == null)
                {
                    this._collapseAllCommand = new Events.RelayCommand(this.OnCollapseAll);
                    this._collapseAllCommand.IsEnabled = this.ShowCaptures || this.ShowGroups;
                }

                return this._collapseAllCommand;
            }
        }

        protected virtual void OnCollapseAll(object parameter)
        {
            this.ShowCaptures = false;
            if (!ShowGroups)
                return;

            foreach (GroupResultItem item in this.Groups.Where(g => g != null && g.ShowCaptures))
                item.ShowCaptures = false;

            this.ShowGroups = false;
        }

        #endregion

        #region ShowEmptyGroups Property Members

        public const string PropertyName_ShowEmptyGroups = "ShowEmptyGroups";

        public static readonly DependencyProperty ShowEmptyGroupsProperty =
            DependencyProperty.Register(MatchResultItem.PropertyName_ShowEmptyGroups, typeof(bool), typeof(MatchResultItem),
                new PropertyMetadata(false));

        public bool ShowEmptyGroups
        {
            get { return (bool)(this.GetValue(MatchResultItem.ShowEmptyGroupsProperty)); }
            set { this.SetValue(MatchResultItem.ShowEmptyGroupsProperty, value); }
        }

        #endregion

        public MatchResultItem()
            : base()
        {
            this.Groups = new ObservableCollection<GroupResultItem>();
        }

        public MatchResultItem(Match match, Regex regex)
            : base(match)
        {
            this.Groups = new ObservableCollection<GroupResultItem>();
            for (int i = 0; i < match.Groups.Count; i++)
                this.Groups.Add(new GroupResultItem(match.Groups[i], i, regex.GroupNameFromNumber(i)));
            this.ShowEmptyGroups = this.Groups.Count == 0 && this.ShowGroups;
        }
    }
}
