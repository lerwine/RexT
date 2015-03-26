<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Erwine.Leonard.T.RexT.Web.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-2.1.3.js" type="text/javascript"></script>
    <script type="text/javascript">
        function LinesToArray(txt) {
            if (txt === undefined || txt === null)
                return txt;

            var re = /[\r\n]+/;
            return txt.trim().split(re).map(function(s) { return s.trim() });
        }

        function ArrayToCsv(textArray) {
            if (textArray === undefined || textArray === null)
                return textArray;

            var shouldEscapeRe = /[\r\n",]/;
            return textArray.map(function (s) {
                if (shouldEscapeRe.test(s))
                    return '"' + s.replace('"', '""') + '"';
                return s;
            }).join(",");
        }

        function ArrayToTsv(textArray) {
            if (textArray === undefined || textArray === null)
                return textArray;

            var shouldEscapeRe = /[\r\n~']/;
            return textArray.map(function (s) {
                if (shouldEscapeRe.test(s))
                    return '"' + s.replace("'", "''") + "'";
                return s;
            }).join("~");
        }

        function ExecRestMethod1(verb, arg1Name, arg1Value, arg2Name, arg2Value) {
            var testResultsPre = $('#<%= this.TestResultsPre.ClientID %>')[0];
            var testHyperLink = $('#<%= this.TestHyperLink.ClientID %>')[0];

            var endPoint = {
                type: "GET",
                url: "Service1.svc/" + verb + "?" + arg1Name + "=" + encodeURI(arg1Value),
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var s = JSON.stringify(data, undefined, '\t');
                    testResultsPre.innerText = s;
                },
                error: function (msg, url, line) {
                    testResultsPre.innerText = 'Error: msg = ' + msg + '\r\n\r\nurl = ' + url + '\r\n\r\nline = ' + line;
                }
            };

            if (arg2narg2Name !== undefined && arg2Name !== null)
                endPoint.url += "&" + arg2Name + "=" + encodeURI(arg2Value);

            testHyperLink.href = endPoint.url;
            testHyperLink.innerText = endPoint.url;
            testResultsPre.innerText = "Waiting for response..." + endPoint.url;
            $.ajax(endPoint);
            return false;
        }

        function ExecRestMethod2(verb, arg1Name, arg1Value, arg2Name, arg2Arr) {
            return ExecRestMethod1(verb, arg1Name, arg1Value, arg2Name, ArrayToCsv(arg2Arr));
        }

        function ExecRestMethod3(verb, arg1Name, arg1Arr, arg2Name, arg2Arr) {
            return ExecRestMethod1(verb, arg1Name, ArrayToCsv(arg1Arr), arg2Name, ArrayToCsv(arg2Arr));
        }

        function AddButtonClick() {
            return ExecRestMethod2('add', 'x', $('#<%= this.XTextBox.ClientID %>')[0].value, 'y', LinesToArray($('#<%= this.YTextBox.ClientID %>')[0].value));
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>X:</td>
            <td>
                <asp:TextBox ID="XTextBox" runat="server" Columns="40"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Y:</td>
            <td>
                <asp:TextBox ID="YTextBox" runat="server" TextMode="MultiLine" Columns="40" Rows="4"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="AddButton" runat="server" Text="Go" OnClientClick="AddButtonClick(); return false;" /></td>
        </tr>
        <tr>
            <td>Url:</td>
            <td>
                <asp:HyperLink ID="TestHyperLink" runat="server" NavigateUrl="Service1.svc?singleWsdl" Target="_blank">Service1.svc?singleWsdl</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>Result:</td>
            <td>
                <pre id="TestResultsPre" runat="server">Enter values and click the 'Go' button...</pre>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
