<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pass_reciept.aspx.cs" Inherits="Pass_reciept" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bus Pass Payment Receipt</title>

</head>
<body>
    <form id="form1" runat="server">

        <asp:Panel ID="panelError" runat="server" Visible="false" Style="width: 100%; margin: 50px;">
            <center>
                <h2 style="color: Red;">We apologize for the inconvenience caused</h2>
            </center>
            <center>
                <h3>Please Track your Pass Request or contact to Helpdesk</h3>
            </center>
        </asp:Panel>
    </form>
</body>
</html>
