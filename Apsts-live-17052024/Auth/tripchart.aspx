﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tripchart.aspx.cs" Inherits="Auth_tripchart" %>

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
</head>
<body>
    <form id="form1" runat="server" style="width: 100%; height: 450px;">

        <div style="text-align: right;">
            <asp:Button ID="lbtnPrint" Style="width: 200px; border-radius: 10px; background-color: aliceblue; font-family: verdana; font-size: 16px; height: 30px;"
                runat="server" Text="Print" OnClientClick="printDiv('maindiv')"
                Width="275px"></asp:Button>
        </div>
        <br />
        <div id="maindiv" style="border: thin solid #000000;">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 20%;">
                        <img src="../Logo/DeptLogo.png" style="width:50%;" />
                    </td>

                    <td style="width: 60%; text-align: center"><span id="Label1" style="font-family: Verdana; font-size: Large; font-weight: bold;">
                        <asp:Label runat="server" ID="lblDepartmentName"></asp:Label><br />
                        Trip Chart </span></td>
                    <td style="width: 20%;">

                    </td>
                </tr>
                

            </table>
            <table style="width: 100%; margin-left: 10px; padding-right: 20px; margin-top: 5px;">
                <tbody>
                    <tr>
                        <td>
                            <asp:Label ID="lblgstn" runat="server" Text="" Style="font-family: Verdana; font-size: 8pt; font-weight: bold;"></asp:Label></td>
                        <td></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblbkngby" runat="server" Text="" Style="font-family: Verdana; font-size: 8pt; font-weight: bold;"></asp:Label></td>
                    </tr>
                </tbody>
            </table>
            <div><span id="Label2" style="font-family: Verdana; font-size: 8pt; font-weight: bold; margin-left: 10px;">
                <%--Your Trip Chart has been successfully Generted.--%>
                Thank you for availing our services. Your Trip and its details are as follows- </span></div>
            <table style="border-top: thin solid #000000; border-bottom: thin solid #000000; width: 100%; padding-right: 20px;">
                <tr>
                    <td><span id="Label3" style="font-family: Verdana; font-size: 8pt;">Service Code/Type:</span></td>
                    <td>
                        <asp:Label ID="lblservice" runat="server" Style="font-family: Verdana; font-size: 8pt; font-weight: bold;"></asp:Label></td>
                    <td><span id="Label7" style="font-family: Verdana; font-size: 8pt;">Bus No:</span></td>
                    <td>
                        <asp:Label ID="lblbusno" runat="server" Style="font-family: Verdana; font-size: 8pt; font-weight: bold;"></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td><span id="Label5" style="font-family: Verdana; font-size: 8pt;">Journey Date:</span></td>
                    <td>
                        <asp:Label ID="lbljourneytDate" runat="server" Style="font-family: Verdana; font-size: 8pt; font-weight: bold;"></asp:Label></td>
                    <td><span id="Label4" style="display: inline-block; font-family: Verdana; font-size: 8pt; width: 100%;">Departure Time:</span></td>
                    <td>
                        <asp:Label ID="lbldeparturetime" runat="server" Style="font-family: Verdana; font-size: 8pt; font-weight: bold;"></asp:Label></td>
                </tr>
                <tr>

                    <td><span id="Label6" style="font-family: Verdana; font-size: 8pt;">Source:</span></td>
                    <td>
                        <asp:Label ID="lblsource" runat="server" Style="font-family: Verdana; font-size: 8pt; font-weight: bold;"></asp:Label></td>
                    <td><span id="Label9" style="font-family: Verdana; font-size: 8pt;">Destination:</span></td>
                    <td>
                        <asp:Label ID="lbldestination" runat="server" Style="font-family: Verdana; font-size: 8pt; font-weight: bold;"></asp:Label></td>
                </tr>
                <tr>
                    <td><span id="Label11" style="font-family: Verdana; font-size: 8pt;">Driver:</span></td>
                    <td>
                        <asp:Label ID="lbldriver" runat="server" Style="font-family: Verdana; font-size: 8pt; font-weight: bold;"></asp:Label></td>

                    <td><span id="Label13" style="font-family: Verdana; font-size: 8pt;">Depot:</span></td>
                    <td>
                        <asp:Label ID="lbldepot" runat="server" Style="font-family: Verdana; font-size: 8pt; font-weight: bold;"></asp:Label></td>
                </tr>
                <tr>
                    <td><span id="Label12" style="font-family: Verdana; font-size: 8pt;">Conductor:</span></td>
                    <td>
                        <asp:Label ID="lblConductor" runat="server" Style="font-family: Verdana; font-size: 8pt; font-weight: bold;"></asp:Label></td>
                    <%--   <td><span id="Label14" style="font-family: Verdana; font-size: 8pt;">Route:</span></td>
                    <td>
                        <asp:Label ID="lblroute" runat="server" Style="font-family: Verdana; font-size: 8pt; font-weight: bold;"></asp:Label></td>
                    --%>
                </tr>
            </table>

            <div><span id="Label16" style="font-family: Verdana; font-size: Small; font-weight: bold; margin-left: 345px;">Detail of Seat(s)</span> </div>
            <div class="watermark">
                <table id="tbl" runat="server" style="text-align: center; width: 100%; border: thin solid Black; font-family: Verdana; font-size: 8pt; text-align: left; table-layout: auto; border-collapse: collapse; border-spacing: 1px;" border="1" cellspacing="1">
                    <tbody>
                        <tr style="background-color: #CCCCCC; border: thin solid #000000; line-height: normal; vertical-align: baseline; text-align: center;">
                            <td><span style="font-family: Verdana; font-size: 8pt; font-weight: bold;">Seat<br />
                                No.</span></td>
                            <td><span style="font-family: Verdana; font-size: 8pt; font-weight: bold;">Ticket No.</span></td>
                            <td><span style="font-family: Verdana; font-size: 8pt; font-weight: bold;">Passenger<br />
                                Name</span></td>
                            <td><span style="font-family: Verdana; font-size: 8pt; font-weight: bold;">Gender</span></td>
                            <td><span style="font-family: Verdana; font-size: 8pt; font-weight: bold;">Age</span></td>
                            <td><span style="font-family: Verdana; font-size: 8pt; font-weight: bold;">Boarding<br />
                                Station</span></td>
                            <td><span style="font-family: Verdana; font-size: 8pt; font-weight: bold;">Fare</span></td>
                            <td><span style="font-family: Verdana; font-size: 8pt; font-weight: bold;">Booking<br />
                                Type</span></td>
                        </tr>
                    </tbody>
                </table>
                <div>

                    <table style="width: 100%; margin-top: 10px; text-align: center;">
                        <tbody>
                            <%-- <tr>
                                <td style="font-family: Verdana;font-size: 8pt;padding-bottom: 8px;">Conductor must enter all the entries in Trip sheet into R-Ticket machine before depature of the bus.

                                </td>
                            </tr>--%>
                            <tr style="background: gainsboro;">
                                <td style="padding: 5px; text-align: center;">
                                    <span style="font-family: Verdana; font-size: 8pt;">Summary</span>

                                </td>
                                <td style="padding: 5px; text-align: center;">
                                    <span style="font-family: Verdana; font-size: 8pt;">Total Seats:</span>
                                    <asp:Label ID="lblTotalSeats" runat="server" Style="font-family: Verdana; font-size: 8pt; font-weight: bold; margin-right: 150px"></asp:Label>

                                    <span style="font-family: Verdana; font-size: 8pt;">Total Fare:</span>
                                    <asp:Label ID="lblTotalFare" runat="server" Style="font-family: Verdana; font-size: 8pt; font-weight: bold;"></asp:Label>

                                </td>
                            </tr>

                        </tbody>
                    </table>
                </div>
                <br />
            </div>
            <div style="text-align:right;font-weight:bold;margin-top:10px">
               (Station Superintendent)
            </div>
             <%--<img src='../assets/img/footer-nic.png' style='height: 30px;' /> --%>
            <br />
            <br />
            <br />
          <%--  <div>
                <table style="width: 100%;">
                    <tr>
                       
                        <td style="width: 20%;"></td>
                    </tr>
                </table>
            </div>--%>
           
        </div>

    </form>
</body>
</html>
