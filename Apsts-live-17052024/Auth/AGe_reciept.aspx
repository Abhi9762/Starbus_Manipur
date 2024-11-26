<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AGe_reciept.aspx.cs" Inherits="Auth_AGe_reciept" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agent Receipt</title>
    <link href="assets/css/printdivstyle.css" rel="stylesheet" type="text/css" />
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
</head>
<body>
    <form id="form1" runat="server" style="width: 820px; height: 450px;">
        <div style="margin-right: 28px; width: 100%;" id="details" runat="server">
            <asp:Button ID="lbtnPrint" Style="width: 100%; border-radius: 10px; background-color: aliceblue; font-family: verdana; font-size: 16px;"
                runat="server" Text="Print" OnClientClick="printDiv('maindiv')"></asp:Button>
            <div class="watermark" id="maindiv">
                <table id="divprint" class="divtable">
                    <tr>
                        <td colspan="4" align="center">
                            <h3 style="font-size: 18pt; font-weight: bold; margin: 0px">Arunachal Pradesh State Transport Corporation</h3>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Label ID="lblpasstype" runat="server" Text="Agent Online Recharge Account Receipt"
                                Font-Bold="True" Font-Size="12pt" Font-Names="Verdana"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdlabel" align="right">
                            <asp:Label ID="Label3" runat="server" Text="Agent Code." Font-Size="8pt" Font-Names="Verdana"></asp:Label>
                        </td>
                        <td align="left" class="tdentry">
                            <asp:Label ID="lblAGcode" runat="server" Font-Size="8pt" Font-Names="Verdana" Text="N/A"
                                Font-Bold="True"></asp:Label>
                        </td>
                        <td class="tdlabel" align="right">
                            <asp:Label ID="lbltran" runat="server" Text="Transaction No." Font-Size="8pt" Font-Names="Verdana"></asp:Label>
                        </td>
                        <td class="style1" align="left">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="N/A" Font-Size="8pt" Font-Names="Verdana"
                                Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdlabel" align="right">
                            <asp:Label ID="Label4" runat="server" Text="Agent Name" Font-Size="8pt" Width="100%"
                                Font-Names="Verdana"></asp:Label>
                        </td>
                        <td class="tdentry" align="left">
                            <asp:Label ID="lblagname" runat="server" Text="N/A" Font-Size="8pt" Font-Names="Verdana"
                                Font-Bold="True"></asp:Label>
                        </td>
                        <td class="tdlabel" align="right">
                            <asp:Label ID="Label11" runat="server" Text="Agent Type" Font-Size="8pt" Font-Names="Verdana"></asp:Label>
                        </td>
                        <td class="tdentry" align="left">
                            <asp:Label ID="lblagtype" runat="server" Font-Size="8pt" Text="N/A" Font-Names="Verdana"
                                Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdlabel" align="right">
                            <asp:Label ID="Label9" runat="server" Text="Deposit Amount" Font-Size="8pt" Font-Names="Verdana"></asp:Label>
                        </td>
                        <td class="tdentry" align="left">
                            <asp:Label ID="lbldepositamt" runat="server" Font-Size="8pt" Text="0" Font-Names="Verdana"
                                Font-Bold="True"></asp:Label>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="tdlabel" align="right">
                            <asp:Label ID="Label32" runat="server" Text="Transation initiate Date/Time" Font-Size="8pt"
                                Font-Names="Verdana"></asp:Label>
                        </td>
                        <td class="style1" align="left">
                            <asp:Label ID="lbltransinitdate" runat="server" Font-Size="8pt" Text="N/A" Font-Names="Verdana"
                                Font-Bold="True"></asp:Label>
                        </td>
                        <td class="tdlabel" align="right">
                            <asp:Label ID="Label1" runat="server" Text="Transation Complete Date/Time" Font-Size="8pt"
                                Font-Names="Verdana"></asp:Label>
                        </td>
                        <td class="style1" align="left">
                            <asp:Label ID="lbltranscmpltedate" runat="server" Font-Size="8pt" Text="N/A" Font-Names="Verdana"
                                Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr style="width: 100%; text-align: center; height: 34px; vertical-align: middle;">
                    </tr>
                    <tr style="width: 100%; text-align: center; height: 34px; vertical-align: middle;">
                        <td colspan="4" style="border: 1px solid #d7d1d1; vertical-align: middle; padding-top: 0px; background-color: #d7d2d2; color: red;">
                            <asp:Label ID="lblcollect" runat="server" Text="For any query please contact helpdesk"
                                Font-Size="8pt" Font-Bold="true" Font-Names="Verdana"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <asp:Label ID="msg" runat="server" Text="Sorry Something went wrong. please contact helpdesk"></asp:Label>
        <asp:HiddenField ID="hidtoken" runat="server"></asp:HiddenField>
    </form>
</body>
</html>

