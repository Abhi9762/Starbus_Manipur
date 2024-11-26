<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E_cancellationvoucher.aspx.cs" Inherits="E_cancellationvoucher" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Cancellation Voucher</title>
    <style type="text/css">
        table td
        {
            padding-left: 10px;
        }
        .style1
        {
            margin-left: 10px;
            width: 800px;
        }
        
        .style3
        {
            width: 130px;
        }
        
        .style4
        {
            width: 180px;
        }
        tr.example td
        {
            border-style: solid;
            border-color: red;
            border-width: 1px;
            padding: 10px;
        }
        tr.test td
        {
            border-style: solid;
            border-color: Black;
            border-width: 1px;
            text-align: left;
        }
       
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center;">
     <asp:Panel ID="panelError" runat="server" Visible="false" style="width:100%; margin:50px;">
    <h2 style="color:Red;">We apologize for the inconvenience caused</h2>
    <h3>Please check your booking history or contact to Helpdesk</h3>
    </asp:Panel>
    </div>
    </form>
</body>
</html>
