using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Erwine.Leonard.T.RexT.Web
{
    [DataContract]
    public class AddResult
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public int? X { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public int[] Y { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public long? Result { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Message { get; set; }
    }
}
