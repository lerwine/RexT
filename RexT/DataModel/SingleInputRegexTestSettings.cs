using System.Text.RegularExpressions;

namespace Erwine.Leonard.T.RexT.DataModel
{
    public class SingleInputRegexTestSettings : SerializableDataModel<SingleInputRegexTestSettings>
    {
        public OperationType Operation { get; set; }

        public RegexOptions Options { get; set; }

        public string Pattern { get; set; }

        public InputTextSettings[] Input { get; set; }
    }
}
