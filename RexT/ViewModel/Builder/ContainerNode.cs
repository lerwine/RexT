using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Erwine.Leonard.T.RexT.ViewModel.Builder
{
    public abstract class ContainerNode : RegexNode
    {
        private ObservableCollection<RegexNode> _innerItems;
        private ReadOnlyObservableCollection<RegexNode> _items;

        public override ReadOnlyObservableCollection<RegexNode> Items
        {
            get { return this._items; }
        }

        public abstract override RegexNodeType NodeType { get; }

        protected ContainerNode()
            : base()
        {
            this._innerItems = new ObservableCollection<RegexNode>();
            this._items = new ReadOnlyObservableCollection<RegexNode>(this._innerItems);
        }

        protected abstract bool SupportsNodeType(RegexNodeType regexNodeType);

        public bool Contains(RegexNode node, bool recursive = false)
        {
            if (this._items.Any(n => Object.ReferenceEquals(n, node)))
                return true;

            return recursive && this._items.OfType<ContainerNode>().Any(i => i.Contains(node, true));
        }

        public void Insert(int index, RegexNode childNode)
        {
            if (childNode == null)
                throw new ArgumentNullException("childNode");

            if (childNode.Parent != null)
                throw new ArgumentException("Child belongs to another container.", "childNode");

            if (Object.ReferenceEquals(childNode, this))
                throw new ArgumentException("Items cannot be self-containing.", "childNode");

            if ((childNode is ContainerNode) && (childNode as ContainerNode).Contains(this, true))
                throw new ArgumentException("Cannot nest a child within itself.", "childNode");

            if (!this.SupportsNodeType(childNode.NodeType))
                throw new ArgumentOutOfRangeException("childNode", "Cannot add node of that type");

            this._innerItems.Insert(index, childNode);

            childNode.Parent = this;
        }

        public void Add(RegexNode childNode)
        {
            if (childNode == null)
                throw new ArgumentNullException("childNode");

            if (childNode.Parent != null)
                throw new ArgumentException("Child belongs to another container.", "childNode");

            if (Object.ReferenceEquals(childNode, this))
                throw new ArgumentException("Items cannot be self-containing.", "childNode");

            if ((childNode is ContainerNode) && (childNode as ContainerNode).Contains(this, true))
                throw new ArgumentException("Cannot nest a child within itself.", "childNode");

            if (!this.SupportsNodeType(childNode.NodeType))
                throw new ArgumentOutOfRangeException("childNode", "Cannot add node of that type");
            this._innerItems.Add(childNode);
            childNode.Parent = this;
        }

        public void Remove(RegexNode childNode)
        {
            if (childNode == null)
                throw new ArgumentNullException("childNode");

            if (childNode.Parent == null || !Object.ReferenceEquals(childNode.Parent, this))
                throw new ArgumentException("Child does not belong to container.", "childNode");

            int index = this._innerItems.TakeWhile(i => !Object.ReferenceEquals(i, childNode)).Count();
            this._innerItems.RemoveAt(index);
            childNode.Parent = null;
        }

        public void RemoveAt(int index)
        {
            RegexNode childNode = this._innerItems[index];
            this._innerItems.RemoveAt(index);
            childNode.Parent = null;
        }
    }
}
