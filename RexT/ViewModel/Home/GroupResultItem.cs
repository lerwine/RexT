using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Erwine.Leonard.T.RexT.ViewModel.Home
{
    public class GroupResultItem : BaseGroupItem
    {
        #region Number Property Members

        public const string PropertyName_Number = "Number";

        public static readonly DependencyProperty NumberProperty =
            DependencyProperty.Register(GroupResultItem.PropertyName_Number, typeof(int), typeof(GroupResultItem),
                new PropertyMetadata(0));

        public int Number
        {
            get { return (int)(this.GetValue(GroupResultItem.NumberProperty)); }
            set { this.SetValue(GroupResultItem.NumberProperty, value); }
        }

        #endregion

        #region Name Property Members

        public const string PropertyName_Name = "Name";

        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register(GroupResultItem.PropertyName_Name, typeof(string), typeof(GroupResultItem),
                new PropertyMetadata(""));

        public string Name
        {
            get { return this.GetValue(GroupResultItem.NameProperty) as string; }
            set { this.SetValue(GroupResultItem.NameProperty, value); }
        }

        #endregion

        public GroupResultItem()
            : base()
        {
            this.Captures = new ObservableCollection<CaptureResultItem>();
        }

        public GroupResultItem(PageViewModel pageViewModel, Group group, int number, string name)
            : base(pageViewModel, group)
        {
            this.Number = number;
            this.Name = name;
        }
    }
}
