<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DutyAllotmentSlip.aspx.cs" Inherits="Auth_DutyAllotmentSlip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../assets/css/bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="../assets/css/bootstrap/bootstrap.min.css" rel="stylesheet" />
    <style>
        .table-sm td, .table-sm th {
            padding: 0.3rem;
            font-size: 10pt;
        }
    </style>
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
    <form id="form1" runat="server">
        <div class="row m-0 align-items-center">
            <div style="margin-right: 28px; width: 100%;" id="details" runat="server">
                <center>
                <asp:Button ID="Button1" Style="border-radius: 10px; background-color: aliceblue;margin-bottom:5px"
                    runat="server" Text="Print" OnClientClick="printDiv('maindiv')"
                    Width="50px"></asp:Button>
                </center>
                <div id="maindiv" style="width: 100%; border: 1px solid;">
                    <table id="divprint" class="divtable m-auto text-center">
                        <tr style="height: 130px; vertical-align: top;">
                            <td colspan="4">
                                <asp:Image runat="server" ID="imgLogo" AlternateText="Logo" Height="50px" /><br />
                                <h3 runat="server" id="lblHeading" style="display: inline">State Road Transport Corporation</h3>
                                <h4>Duty Allotment Slip</h4>
                            </td>
                        </tr>
                    </table>
                    <div style="padding: 10px;">
                        <table class="m-auto table table-sm table-borderless text-left p-3">
                            <tr>
                                <td>
                                    <asp:Label ID="label" runat="server" Text="Duty Reference No" CssClass="form-control-label"
                                        Font-Bold="true"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbldutyRefNo" runat="server" Text="N/A"
                                        CssClass="form-control-label"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" Text="Trip Date/Time" CssClass="form-control-label"
                                        Font-Bold="true"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTripDateTime" runat="server" Text="N/A" CssClass="form-control-label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="label2" runat="server" Text=" Service Code" CssClass="form-control-label"
                                        Font-Bold="true"></asp:Label>
                                </td>
                                <td colspan="3" style="text-align: left;">
                                    <asp:Label ID="lblServiceCode" runat="server" Text="N/A" CssClass="form-control-label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" Text="Service" CssClass="form-control-label"
                                        Font-Bold="true"></asp:Label>
                                </td>
                                <td colspan="3" style="text-align: left;">

                                    <asp:Label ID="lblServiceName" runat="server" Text="N/A" CssClass="form-control-label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" Text="Bus Type" CssClass="form-control-label"
                                        Font-Bold="true"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblBustype" runat="server" Text="N/A" CssClass="form-control-label"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label runat="server" Text="Bus No" CssClass="form-control-label"
                                        Font-Bold="true"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblBusNo" runat="server" CssClass="form-control-label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" Text="Driver" CssClass="form-control-label"
                                        Font-Bold="true"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDriver1" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" Text="Second Driver(If Any)" CssClass="form-control-label"
                                        Font-Bold="true"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDriver2" runat="server" Text="N/A" CssClass="form-control-label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" Text="Conductor" CssClass="form-control-label"
                                        Font-Bold="true"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblConductor1" runat="server" CssClass="form-control-label"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" Text="Second Condutor(If Any)" CssClass="form-control-label"
                                        Font-Bold="true"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblConductor2" runat="server" Text="N/A" CssClass="form-control-label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" Text="Duty Days" CssClass="form-control-label"
                                        Font-Bold="true"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDutyDays" runat="server" Text="N/A" CssClass="form-control-label"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label runat="server" Text="Duty Rest Days" CssClass="form-control-label"
                                        Font-Bold="true"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblRestDays" runat="server" Text="N/A" CssClass="form-control-label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" Text="Printed By" CssClass="form-control-label"
                                        Font-Bold="true"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPrintBy" runat="server" Text="ADMIN" CssClass="form-control-label"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label runat="server" Text="Print Date/Time" CssClass="form-control-label"
                                        Font-Bold="true"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPrintDateTime" runat="server" Text="N/A" CssClass="form-control-label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="vertical-align: middle; padding-top: 20px; text-align: center; color: red;">
                                    <h4>
                                        <asp:Label ID="lblHelpdeskdept" runat="server" Text="For Any Query Please Contact Starbus Helpdesk"
                                            Font-Size="11pt" Font-Bold="true"></asp:Label></h4>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <br />
                    <div class="row m-0 text-center">
                        <asp:Label ID="lblMessage" runat="server" Style="margin-left: 195px" Text="Sorry Something Went Wrong. Please Contact Starbus Helpdesk"
                            Visible="false"></asp:Label>
                    </div>
                    <br />
                </div>
            </div>
        </div>

        <asp:HiddenField ID="hidtoken" runat="server"></asp:HiddenField>
    </form>
</body>
</html>
