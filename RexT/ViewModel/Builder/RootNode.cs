using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Erwine.Leonard.T.RexT.ViewModel.Builder
{
    public class RootNode : ContainerNode
    {
        public override RegexNodeType NodeType { get { return RegexNodeType.Root; } }

        protected override bool SupportsNodeType(RegexNodeType regexNodeType)
        {
            throw new NotImplementedException();
        }

        public override string OpeningText
        {
            get { throw new NotImplementedException(); }
        }

        public override string ClosingText
        {
            get { throw new NotImplementedException(); }
        }
    }
}
