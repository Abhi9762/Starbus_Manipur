<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E_tripchart_current.aspx.cs" Inherits="E_tripchart_current" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <script language="javascript" type="text/javascript">
        function printDiv(divID) {
            var divElements = document.getElementById(divID).innerHTML;
            var oldPage = document.body.innerHTML;
            document.body.innerHTML =
              "<html><head></head><body style='text-align: center;'>" +
              divElements + "</body>";
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

            .lefted {
                font-size: 10pt;
                text-align:left;
            }

            .toped {
                text-align: center;
                clear: both;
                display: block;
                margin-top: -10px;
                font-size: 12pt;
                 font-weight:bold;
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
            <asp:Button ID="lbtnPrint" Style="border-radius: 10px; background-color: aliceblue; font-family: verdana; font-size: 16px;"
                runat="server" Text="Print" OnClientClick="printDiv('maindiv')"
                Width="275px"></asp:Button>
            <br />
            <div class="watermark" id="maindiv" style="width: 275px; text-align: center; border: 1px solid;">
                <table id="divprint">
                    <tr>
                        <td>
                           
                            <asp:Label runat="server" CssClass="toped1" ID="lblDeptName"></asp:Label>
                            <br />
                            <span class="toped1">-TRIP CHART-</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblbkngstn" runat="server" Text="Booking Station" CssClass="centered"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblservice" runat="server" Text="Service" CssClass="centered"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblbusno" runat="server" Text="Bus No" CssClass="centered"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblRoute" runat="server" Text="Route" CssClass="centered"></asp:Label>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblConductor" runat="server" Text="Conductor" CssClass="centered"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDeprtDateTime" runat="server" Text="Deprt.Time" CssClass="centered"></asp:Label>
                        </td>
                    </tr>
                      <tr>
                        <td><span class="centered">---------------------------</span>
                            </td>
                         </tr>
                    <tr>
                        <td>
                            <table id="tbl" runat="server" class="lefted">
                                <tr>

                                    <th class="lefted">TICKET-NO</th>
                                    <th class="lefted">SEATS</th>
                                </tr>

                            </table>
                        </td>
                    </tr>
                     <tr>
                        <td><span class="centered">---------------------------</span>
                            </td>
                         </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbltotseatsbook" runat="server" Text="Total Seats Booked" CssClass="lefted"></asp:Label>
                            <asp:Label ID="lbltotfare" runat="server" Text="Total Fare" CssClass="lefted"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblbkngcntr" runat="server" Text="Booking Counter" CssClass="lefted"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblbkngby" runat="server" Text="Booked By" CssClass="lefted"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblprintdt" runat="server" Text="Print Date & Time" CssClass="lefted"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td><span class="centered">---------------------------</span>
                            </td>
                         </tr>
                    <tr>
                        <td><span class="centered">Conductor must enter all the entries in
                            <br />
                            trip sheet into E-Ticket machine before<br />
                            depature of the bus.</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="centered">!! Happy Journey !!</span>
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
