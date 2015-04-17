using System;
using System.Text.RegularExpressions;

namespace Erwine.Leonard.T.RexT.ViewModel
{
    public class RegexOptionsValueEventArgs : EventArgs
    {
        public RegexOptions Value { get; set; }

        public RegexOptionsValueEventArgs() : base() { }

        public RegexOptionsValueEventArgs(RegexOptions value)
        {
            this.Value = value;
        }
    }
}
