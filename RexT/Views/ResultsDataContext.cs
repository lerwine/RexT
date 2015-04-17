using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Erwine.Leonard.T.RexT.Views
{
    public class ResultsDataContext : DependencyObject
    {

        #region Items Property Members

        public const string PropertyName_Items = "Items";

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register(ResultsDataContext.PropertyName_Items, typeof(ObservableCollection<Tuple<DateTime, string, string>>), typeof(ResultsDataContext),
                new PropertyMetadata(null));

        public ObservableCollection<Tuple<DateTime, string, string>> Items
        {
            get { return (ObservableCollection<Tuple<DateTime, string, string>>)(this.GetValue(ResultsDataContext.ItemsProperty)); }
            set { this.SetValue(ResultsDataContext.ItemsProperty, value); }
        }

        #endregion

        public ResultsDataContext() { this.Items = new ObservableCollection<Tuple<DateTime, string, string>>(); }
    }
}
