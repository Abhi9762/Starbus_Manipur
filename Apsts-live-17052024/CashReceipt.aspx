<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CashReceipt.aspx.cs" Inherits="CashReceipt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>E_Receipt</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    
    <link rel="stylesheet" href="assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" type="text/css" />
      <script language="javascript" type="text/javascript">
        function printDiv(divID) {
            var divElements = document.getElementById(divID).innerHTML;
            var oldPage = document.body.innerHTML;
            document.body.innerHTML =
              "<html><head></head><body>" +
              divElements + "</body>";
            window.print();
            document.body.innerHTML = oldPage;
        }
    </script>
    <style>
        .divtable
        {
            width: 100%;
            background-repeat: no-repeat;
            background-position: center;
            position: relative;
            margin-left: -1px;
            line-height: 30px;
        }
        h1, h2, h3
        {
            line-height: 19px;
        }
        hr
        {
            margin: 20px 0;
            border: 0;
            margin-left: 10px;
            margin-right: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-right: 28px; width: 100%;" id="details" runat="server">
            <asp:Button ID="lbtnPrint" Style="border-radius: 10px; background-color: aliceblue; font-family: verdana; font-size: 16px;"
                runat="server" Text="Print" OnClientClick="printDiv('maindiv')"
                Width="100%"></asp:Button>
            <div class="watermark" id="maindiv" style="width: 100%; border: 1px solid;">
                <table id="divprint" class="divtable">
                    <tr style="height: 200px; vertical-align: top;">
                        <td colspan="4">
                            <center>
                            <img src="Logo/DeptLogo.png" style="height: 50px;" /><br />
                            <h3>
                                <asp:Label ID="lbldepart" runat="server"></asp:Label></h3>
                            <h4>
                                Cash Receipt</h4>
                            <hr />
                            <h3>
                                Conductor / Other Payee Copy</h3>
                        </center>
                        </td>
                    </tr>
                </table>
                <table class="table">
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="Label1" runat="server" Text="Receipt Number" Font-Size="9pt" Font-Names="Verdana"
                                Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No." Font-Size="9pt"
                                Font-Names="Verdana"></asp:Label>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label2" runat="server" Text="Deposit Date/Time" Font-Size="9pt" Font-Names="Verdana"
                                Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbltransdate" runat="server" Font-Size="9pt" Text="N/A" Font-Names="Verdana"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="Label3" runat="server" Text=" Office" Font-Size="9pt" Font-Names="Verdana"
                                Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="3" style="text-align: left;">
                            <asp:Label ID="lbloffice" runat="server" Text="Dehradun B (Depot)" Font-Size="9pt"
                                Font-Names="Verdana"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblIncomeSource" runat="server" Text="Income Source" Font-Size="9pt" Font-Names="Verdana"
                                Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="3" style="text-align: left;">

                            <asp:Label ID="lblsubhead" runat="server" Font-Size="9pt" Text="N/A" Font-Names="Verdana"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="Label5" runat="server" Text="Deposited Amount" Font-Size="9pt" Font-Names="Verdana"
                                Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbldepositamt" runat="server" Font-Size="9pt" Text="0" Font-Names="Verdana"></asp:Label>
                          <i class="fa fa-rupee-sign"></i>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label6" runat="server" Text="Deposited By" Font-Size="9pt" Font-Names="Verdana"
                                Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbldepositby" runat="server" Font-Size="9pt" Text="N/A" Font-Names="Verdana"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="Label7" runat="server" Text="Printed By" Font-Size="9pt" Font-Names="Verdana"
                                Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblprintby" runat="server" Font-Size="9pt" Text="UTCADMIN" Font-Names="Verdana"></asp:Label>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label8" runat="server" Text="Print Date/Time" Font-Size="9pt" Font-Names="Verdana"
                                Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblprintdatetime" runat="server" Font-Size="9pt" Text="N/A" Font-Names="Verdana"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="border: 1px solid #d7d1d1; vertical-align: middle; padding-top: 6px; background-color: #d7d2d2; color: red;">
                            <center>
                            <asp:Label ID="Label9" runat="server" Text="For any query please contact UTC helpdesk"
                                Font-Size="8pt" Font-Bold="true" Font-Names="Verdana"></asp:Label></center>
                        </td>
                    </tr>
                </table>
                <br />
                <hr style="border-top: 2px dotted #000000; border-bottom: 2px dotted #000000;" />
                <table id="Table1" class="divtable">
                    <tr style="height: 200px; vertical-align: top;">
                        <td colspan="4">
                            <center>
                            <img src="Logo/DeptLogo.png" style="height: 50px;" /><br />
                            <h3>
                                <asp:Label ID="lbldepart1" runat="server"></asp:Label></h3>
                            <h4>
                                Cash Receipt</h4>
                            <hr />
                            <h3>
                                Cashier Copy</h3>
                        </center>
                        </td>
                    </tr>
                </table>
                <table class="table">
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="Label10" runat="server" Text="Receipt Number" Font-Size="9pt" Font-Names="Verdana"
                                Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTransactionNo1" runat="server" Text="Transaction No." Font-Size="9pt" Font-Names="Verdana"></asp:Label>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label12" runat="server" Text="Deposit Date/Time" Font-Size="9pt" Font-Names="Verdana"
                                Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbltransdate1" runat="server" Font-Size="9pt" Text="N/A" Font-Names="Verdana"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="Label14" runat="server" Text=" Office" Font-Size="9pt" Font-Names="Verdana"
                                Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="3" style="text-align: left;">
                            <asp:Label ID="lbloffice1" runat="server" Text="Dehradun B (Depot)" Font-Size="9pt"
                                Font-Names="Verdana"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblIncomeSource1" runat="server" Text="Income Source" Font-Size="9pt" Font-Names="Verdana"
                                Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="3" style="text-align: left;">

                            <asp:Label ID="lblsubhead1" runat="server" Font-Size="9pt" Text="N/A" Font-Names="Verdana"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="Label19" runat="server" Text="Deposited Amount" Font-Size="9pt" Font-Names="Verdana"
                                Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbldepositamt1" runat="server" Font-Size="9pt" Text="0" Font-Names="Verdana"></asp:Label>
                             <i class="fa fa-rupee-sign"></i>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label21" runat="server" Text="Deposited By" Font-Size="9pt" Font-Names="Verdana"
                                Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbldepositby1" runat="server" Font-Size="9pt" Text="N/A" Font-Names="Verdana"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="Label23" runat="server" Text="Printed By" Font-Size="9pt" Font-Names="Verdana"
                                Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblprintby1" runat="server" Font-Size="9pt" Text="UTCADMIN" Font-Names="Verdana"></asp:Label>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label25" runat="server" Text="Print Date/Time" Font-Size="9pt" Font-Names="Verdana"
                                Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblprintdatetime1" runat="server" Font-Size="9pt" Text="N/A" Font-Names="Verdana"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:Panel ID="pnlerror" runat="server" Visible="true" Style="width: 100%; margin: 50px;">
            <h2 style="color: Red;">We apologize for the inconvenience caused</h2>
            <h3>Please check your booking history or contact to Helpdesk</h3>
        </asp:Panel>

    </form>
</body>
</html>
