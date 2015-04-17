using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Erwine.Leonard.T.RexT.ViewModel.Builder
{
    public abstract class RegexNode : INotifyPropertyChanged
    {
        private ContainerNode _parent = null;

        public event PropertyChangedEventHandler PropertyChanged;

        public abstract RegexNodeType NodeType { get; }

        public abstract ReadOnlyObservableCollection<RegexNode> Items { get; }

        public abstract string OpeningText { get; }

        public abstract string ClosingText { get; }

        public ContainerNode Parent
        {
            get { return this._parent; }
            set
            {
                if (value == null)
                {
                    if (this._parent == null)
                        return;

                    if (this._parent.Contains(this, false))
                        throw new InvalidOperationException("Parent cannot be cleared using this property.");

                    this.OnSetParent(value);
                    return;
                }

                if (this._parent != null)
                    throw new InvalidOperationException("Item belongs to another container.");

                this.OnSetParent(value);
            }
        }

        protected virtual void OnSetParent(ContainerNode value)
        {
            this._parent = value;
            this.RaisePropertyChanged("Parent");
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, args);
        }
    }
}
