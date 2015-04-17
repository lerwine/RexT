using System;

namespace Erwine.Leonard.T.RexT.ViewModel
{
    public class OperationSettingsValueEventArgs : EventArgs
    {
        public DataModel.OperationType Value { get; set; }

        public OperationSettingsValueEventArgs() : base() { }

        public OperationSettingsValueEventArgs(DataModel.OperationType value)
        {
            this.Value = value;
        }
    }
}
