<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E_ticket_current.aspx.cs" Inherits="E_ticket_current" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function printDiv(divID) {
            var divElements = document.getElementById(divID).innerHTML;
            var oldPage = document.body.innerHTML;
            document.body.innerHTML = "<html><head></head><body style='text-align: center;'>" + divElements + "</body>";
            window.print();
            document.body.innerHTML = oldPage;
        }

    </script>
    <style>
        @media print {
            .centered {
                text-align: center;
                clear: both;
                display: block;
                font-size: 12pt;
            }

            .centered1 {
                text-align: center;
                clear: both;
                display: block;
                font-size: 12pt;
                margin-top: 0px;
            }

            .lefted {
                font-size: 12pt;
            }

            .toped {
                text-align: center;
                clear: both;
                display: block;
                margin-top: -10px;
                font-size: 12pt;
                font-weight: bold;
            }

            .toped1 {
                text-align: center;
                clear: both;
                display: block;
                margin-top: -5px;
                font-size: 12pt;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="pnldetails" runat="server" Visible="false">
            <asp:Button ID="lbtnPrint" Style="border-radius: 10px; background-color: aliceblue; font-family: verdana; font-size: 12pt;"
                runat="server" Text="Print" OnClientClick="printDiv('maindiv')"
                Width="275px"></asp:Button>
            <br />
            <div class="watermark" id="maindiv" style="width: 275px; text-align: center; border: 1px solid; font-size: 12pt;">
                <table id="divprint">
                    <tr>
                        <td>
                            <asp:Label ID="lblDepartmentName" runat="server" CssClass="toped"></asp:Label>
                        <%--    <span class="toped">UTTARAKHAND TRANSPORT CORPORATION</span>--%>
                            <br />
                            <span class="toped1">-TICKET-</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblJdatetime" runat="server" Text="" CssClass="centered"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbltktNo" runat="server" Text="NA" CssClass="centered"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblbusno" runat="server" Text="NA" CssClass="centered"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblstn" runat="server" Text="NA" CssClass="centered"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblfare" runat="server" Text="NA" CssClass="centered"></asp:Label>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbltotfare" runat="server" Text="NA" CssClass="centered"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbltotseat" runat="server" Text="NA" CssClass="centered"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbltotdistance" runat="server" Text="NA" CssClass="centered"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblseatno" runat="server" Text="NA" CssClass="centered"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td><span class="centered">----------------</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblConductor" runat="server" Text="NA" CssClass="lefted"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblbookedby" runat="server" Text="NA" CssClass="lefted"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblbookingdt" runat="server" Text="NA" CssClass="lefted"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="centered1">!! Happy Journey !!</span>
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>


        <asp:Panel ID="panelError" runat="server" Visible="true" Style="width: 100%; margin: 50px;">
            <h2 style="color: Red;">We apologize for the inconvenience caused</h2>
            <h3>Please check your booking history or contact to Helpdesk</h3>
        </asp:Panel>
    </form>
</body>
</html>
