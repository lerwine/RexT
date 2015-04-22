using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace Erwine.Leonard.T.RexT.ViewModel.Builder
{
    public class CharacterNode : RegexNode
    {
        public static readonly Regex TextReplaceRegex = new Regex(@"[\\*+?|{\[\]()\^\$.#]");

        public static readonly Regex NonPrintableReplaceRegex = new Regex(@"[\\*+?|{\[\]()\^\$.#]");

        public override RegexNodeType NodeType { get { return RegexNodeType.Character; } }

        private string _rawText = "";

        private string _openingText = "";

        private ReadOnlyObservableCollection<RegexNode> _items;

        public string RawText
        {
            get { return this._rawText; }
            set
            {
                string s = (value == null) ? "" : value;
                if (this._rawText == s)
                    return;

                this.RaisePropertyChanged("RawText");

                string t = CharacterNode.NonPrintableReplaceRegex.Replace(CharacterNode.TextReplaceRegex.Replace(s, (Match m) => "\\" + m.Value),
                    (Match m) => "\\x" + ((int)(m.Value[0])).ToString("X2"));

                if (this._openingText == t)
                    return;

                this._openingText = t;
                this.RaisePropertyChanged("OpeningText");
            }
        }

        public override string OpeningText { get { return this._openingText; } }

        public override string ClosingText { get { return ""; } }

        public override ReadOnlyObservableCollection<RegexNode> Items { get { return this._items; } }

        public CharacterNode()
            : base()
        {
            this._items = new ReadOnlyObservableCollection<RegexNode>(new ObservableCollection<RegexNode>());
        }
    }
}
