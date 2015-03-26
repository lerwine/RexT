using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace Erwine.Leonard.T.RexT.Web
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service1
    {
        [OperationContract]
        [WebGet(UriTemplate = "add?x={x}&y={y}", ResponseFormat = WebMessageFormat.Json)]
        public AddResult Add(string x, string y)
        {
            AddResult result = new AddResult();
            StringBuilder message = new StringBuilder();
            int n1;
            if (String.IsNullOrWhiteSpace(x))
                message.AppendLine("X cannot be empty.");
            else if (Int32.TryParse(x.Trim(), out n1))
                result.X = n1;
            else
                message.AppendLine("Invalid X value.");

            if (String.IsNullOrWhiteSpace(y))
                message.AppendLine("Y cannot be empty.");
            else {
                var converted = y.Split(',').Select(s =>
                {
                    int n;
                    return new { Success = Int32.TryParse(s, out n), Value = n };
                });
                if (converted.Any(a => !a.Success))
                {
                    message.AppendLine("Invalid Y value.");
                    result.Y = null;
                }
                else
                    result.Y = converted.Select(a => a.Value).ToArray();
            }

            result.Message = message.ToString();
            if (result.X.HasValue && result.Y != null)
                result.Result = result.Y.Aggregate<int, long>((long)(result.X), (long acc, int i) => acc + i);

            return result;
        }
    }
}
