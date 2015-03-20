using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Erwine.Leonard.T.RexT.ViewModel.Home
{
    public class GroupResultItem : BaseGroupItem
    {
        public GroupResultItem()
            : base()
        {
            this.Captures = new ObservableCollection<CaptureResultItem>();
        }

        public GroupResultItem(Group group, int number, string name)
            : base(group)
        {
            this.Success = group.Success;
            
            this.Captures = new ObservableCollection<CaptureResultItem>(group.Captures.OfType<Capture>().Select(c => new CaptureResultItem(c)));
        }
    }
}
