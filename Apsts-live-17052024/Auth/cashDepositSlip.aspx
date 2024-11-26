<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cashDepositSlip.aspx.cs" Inherits="Auth_cashDepositSlip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
    <link rel="icon" href="../assets/img/brand/favicon.png" type="image/png" />
    <!-- Fonts -->
    <!-- Icons -->
    <link rel="stylesheet" href="../assets/vendor/nucleo/css/nucleo.css" type="text/css" />
    <link rel="stylesheet" href="../assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" type="text/css" />
    <!-- Page plugins -->
    <!-- Argon CSS -->
    <link rel="stylesheet" href="../assets/css/argon.css?v=1.2.0" type="text/css" />
    <link href="../assets/css/jquery.dataTables.min.css" rel="stylesheet" />

    <script type="text/javascript" src="../assets/js/jquery-n.js"></script>
    <script src="../assets/js/jquery-ui.js" type="text/javascript"></script>
    <script src="../assets/vendor/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    <script src="../assets/vendor/js-cookie/js.cookie.js"></script>
    <script src="../assets/vendor/jquery.scrollbar/jquery.scrollbar.min.js"></script>
    <script src="../assets/vendor/jquery-scroll-lock/dist/jquery-scrollLock.min.js"></script>
    <script src="../assets/vendor/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>


    <script src="../DataTables/js/jquery.dataTables.min.js"></script>
    <script src="../DataTables/js/dataTables.buttons.min.js"></script>
    <script src="../DataTables/js/buttons.print.min.js"></script>
    <script src="../DataTables/js/buttons.html5.min.js"></script>
    <script src="../DataTables/js/pdfmake.min.js"></script>
    <script src="../DataTables/js/vfs_fonts.js"></script>
    <script src="../DataTables/js/jszip.min.js"></script>
    <!-- Optional JS -->
    <script src="../assets/vendor/chart.js/dist/Chart.min.js"></script>
    <script src="../assets/vendor/chart.js/dist/Chart.extension.js"></script>
    <link href="../css/paging.css" rel="stylesheet" />
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
        <div id="details" runat="server">
            <div class="container-fluid watermark" id="maindiv">
                <div class="card mb-0">
                    <div class="card-header">

                        <div class="row">
                            <div class="col-lg-12">
                                <asp:LinkButton ID="lbtnPrint" CssClass="btn btn-danger btn-sm float-right" Style="margin-top: -12px;" runat="server" OnClientClick="printDiv('maindiv')"><i class="fa fa-print"></i> Print</asp:LinkButton>
                            </div>

                            <div class="col-lg-12 text-center">
                                <h4>
                                    <asp:Label runat="server" class="mb-2" Style="font-size: 13pt;" ID="lblHeading"></asp:Label>
                                </h4>
                                <h2 style="font-size: 10pt" class="text-center">Cash Deposit Slip</h2>
                            </div>
                        </div>
                    </div>
                    <div class="card-body mb-0">
                        <div class="row">
                            <div class="col-lg-12 text-center">
                                <asp:Label ID="lblServiceName" runat="server" Font-Size="11pt" Text="Dehradun - Delhi"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <span style="color: #767373; font-size: small">Waybill No</span><br />
                                <asp:Label ID="lblRefNo" runat="server" Text="123456789" Font-Size="9pt"
                                    Font-Names="Verdana"></asp:Label>
                            </div>
                            <div class="col-6">
                                <span style="color: #767373; font-size: small">Duty Date/Time</span><br />
                                <asp:Label ID="lblDutyDate" runat="server" Font-Size="9pt" Text="123456789"></asp:Label>
                            </div>
                        </div>

                        <div class="row mt-2">
                            <div class="col-6">
                                <span style="color: #767373; font-size: small">Bus No</span><br />
                                <asp:Label ID="lblBusNo" runat="server" Font-Size="9pt" Text="-"></asp:Label>
                            </div>
                            <div class="col-6">
                                <span style="color: #767373; font-size: small">Alloted ETM No</span><br />
                                <asp:Label ID="lblETM" runat="server" Font-Size="9pt" Text="-"></asp:Label>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-6">
                                <span style="color: #767373; font-size: small">Driver</span><br />
                                <asp:Label ID="lblDriver1" runat="server" Font-Size="9pt" Text="-"></asp:Label>
                            </div>
                            <div class="col-6">
                                <span style="color: #767373; font-size: small">Condutor</span><br />
                                <asp:Label ID="lblConductor1" runat="server" Font-Size="9pt" Text="-"></asp:Label>
                            </div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-lg-12 text-center">
                                <h4>Total Amount To Pay ₹
                                    <asp:Label ID="lblCondDeposit" Text="0.00" runat="server" Style="font-size: 1.1rem;"></asp:Label>
                                </h4>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-8">
                            </div>
                            <div class="col-4 text-center">
                                <b><span style="color: #767373">Signature</span></b><br />
                                <asp:Label ID="lblJIName" Text="Ayush" runat="server" Font-Size="9pt"></asp:Label><br />
                                (<asp:Label ID="lblJIDesignation" runat="server" Font-Size="9pt" Text="Managing Director"></asp:Label>)
                            </div>
                        </div>

                        <asp:Label ID="msg" runat="server" Text="Sorry Something went wrong. please contact UTC helpdesk"
                            Visible="false"></asp:Label>
                    </div>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
