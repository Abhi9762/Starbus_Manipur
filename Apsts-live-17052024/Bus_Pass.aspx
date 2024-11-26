<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bus_Pass.aspx.cs" Inherits="Bus_Pass" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Bus Pass Payment Receipt</title>
    <link href="assets/css/StyleSheet_test1.css" rel="stylesheet" />
     <link href="../NewAssets/fonts/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
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

