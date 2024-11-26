<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dashTrackBus.aspx.cs" Inherits="Auth_dashTrackBus" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Grievance</title>
        <link rel="stylesheet" href="../assets/vendor/nucleo/css/nucleo.css" type="text/css" />
    <link rel="stylesheet" href="../assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" type="text/css" />
    <link rel="stylesheet" href="../assets/css/argon.css?v=1.2.0" type="text/css" />

    <link rel="stylesheet" href="../assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" type="text/css" />
    <script type="text/javascript">
        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("btnFile").click();
            }
        }
    </script>
    <style>
        .ModalPopupBG {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container-fluid">
            <div runat="server" id="divtrackbus" visible="true">
                <div class="card shadow">
                    <div class="row" style="font-size: 13px;">
                        <div id="divBusDetails" class="col-4 px-3 pb-3" style="border-right:solid;border-width:1px" runat="server" visible="false">
                            <span class="font-weight-900">Bus Details</span><br />
                            <asp:Label runat="server" Text="Bus No."></asp:Label>
                            -
                                <asp:Label ID="lblbusno" runat="server" Text="N/A"></asp:Label>
                            <br />
                            <asp:Label runat="server" CssClass="" Text="Depot"></asp:Label>
                            -
                                <asp:Label ID="lblDepot" runat="server" Text="N/A"></asp:Label><br />
                            <asp:Label runat="server" CssClass="" Text="Service Type"></asp:Label>
                            -
                                <asp:Label ID="lblServiceType" runat="server" Text="N/A"></asp:Label>
                        </div>
                        <div id="divEmpDetails" class="col-4 px-3 pb-3" style="border-right:solid;border-width:1px" runat="server" visible="false">
                            <span class="font-weight-900">Employee Details</span><br />
                            <asp:Label runat="server" Text="Employee"></asp:Label>
                            -
                                <asp:Label ID="lblempcode" runat="server" Text="N/A"></asp:Label>
                            <br />
                            <asp:Label runat="server" CssClass="" Text="Reporting Office"></asp:Label>
                            -
                                <asp:Label ID="lblRepOffice" runat="server" Text="N/A"></asp:Label><br />
                            <asp:Label runat="server" CssClass="" Text="Posting Office"></asp:Label>
                            -
                                <asp:Label ID="lblPostingoffice" runat="server" Text="N/A"></asp:Label>
                        </div>
                        <div class="col-8 pt-2 pl-3 pb-2">
                            <span class="font-weight-900">Duty Details</span><br />
                            <div id="divDutyDetails" runat="server" visible="false">
                                <asp:Label runat="server" Text="Duty Slip No."></asp:Label>
                                -
                                <asp:Label ID="lblDutySlipno" Font-Bold="true" runat="server" Text="N/A"></asp:Label>

                                <asp:Label runat="server" CssClass="ml-5" Text="Duty Date"></asp:Label>
                                -
                                <asp:Label ID="lblDutyDate" runat="server" Text="N/A"></asp:Label><br />
                                <asp:Label runat="server" CssClass="" Text="Driver"></asp:Label>
                                -
                                <asp:Label ID="lblDriver" runat="server" Text="N/A"></asp:Label>
                                <asp:Label runat="server" CssClass="ml-5" Text="Conductor"></asp:Label>
                                -
                                <asp:Label ID="lblConductor" runat="server" Text="N/A"></asp:Label><br />
                                <asp:Label runat="server" CssClass="" Text="Route"></asp:Label>
                                -
                                <asp:Label ID="lblRoute" Font-Bold="true" runat="server" Text="N/A"></asp:Label>
                                 <asp:Label runat="server" ID="lblBusemp1"  CssClass="ml-5" Text="Bus No. - "></asp:Label>
                                
                                <asp:Label ID="lblBusemp" Font-Bold="true" runat="server" Text="N/A"></asp:Label>
                            </div>
                            <div id="divNoDuty" runat="server" visible="false">
                                <asp:Label runat="server" Text="Current Duty Details Not Available." CssClass="ml-9 mt-3"></asp:Label>

                            </div>
                        </div>
                      

                    </div>
                </div>

                <div id="div1" visible="false" class="card shadow p-1" runat="server" style="min-height: 400px">
                </div>
                
                <div id="div2" visible="true" class="card shadow p-1 text-center" runat="server" style="min-height: 580px">
                        <asp:Label ID="lblmapmassage" CssClass="mt-7" ForeColor="LightGray" Font-Size="30px" Font-Bold="true" runat="server" Text="Please Check The GPS Status Of Bus."></asp:Label>
           
                </div>

            </div>
        </div>
        <div>
        </div>
    </form>
</body>
</html>
