namespace Erwine.Leonard.T.RexT.DataModel
{
    public class InputTextSettings : SerializableDataModel<InputTextSettings>
    {
        public string Name { get; set; }

        public bool WordWrap { get; set; }

        public bool IsMultiline { get; set; }

        public string Text { get; set; }

        public TextInputEncodingValue Encoding { get; set; }

        public bool AlwaysEvaluate { get; set; }

        public InputTextSettings() : base() { }

        public InputTextSettings(ViewModel.Home.InputTextItem item)
        {
            this.Name = item.Name;
            this.WordWrap = item.WordWrap;
            this.IsMultiline = item.IsMultiline;
            this.Text = item.Text;
            this.Encoding = item.TextInputEncoding;
            this.AlwaysEvaluate = item.AlwaysEvaluate;
        }
    }
}
