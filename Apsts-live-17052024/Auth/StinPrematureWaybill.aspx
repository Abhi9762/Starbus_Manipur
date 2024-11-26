<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StinPrematureWaybill.aspx.cs" Inherits="Auth_StinPrematureWaybill" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id="details" runat="server">
            <div class="container-fluid watermark" id="maindiv">
                <div class="card mb-0">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-9 input-group-append">
                            </div>
                            <div class="col-3 input-group-append">

                                <asp:TextBox runat="server" placeholder="Enter Waybill No." ID="txtwaybillNo" MaxLength="12" CssClass="form-control ml-2 mr-2"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="ajxFtMobileNumber" runat="server" FilterType="Numbers" ValidChars=""
                                    TargetControlID="txtwaybillNo" />
                                <asp:LinkButton runat="server" ID="lbtnserachwaybill" OnClick="lbtnserachwaybill_Click" CssClass="btn btn-success btn-sm float-right"><i class="fa fa-search"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body mt-2">
                        <asp:Panel runat="server" ID="pnldata" Visible="false">
                            <div class="row">
                                <div class="col-6" style="border-right: solid; border-color: lightgray">
                                    <div class="row">
                                        <div class="col-lg-12 text-center">
                                            <h2 style="font-size: 13pt" class="text-center">Waybill Details</h2>
                                        </div>
                                        <div class="col-lg-12 mb-2">
                                            <asp:Label runat="server" ID="Label3" Style="color: #767373; font-size: 15px" Text="Waybill No. -"></asp:Label>
                                            <asp:Label runat="server" ID="lblwaybillno" Text="Service Name"></asp:Label>
                                        </div>
                                        <div class="col-lg-5 mb-2">
                                            <asp:Label runat="server" ID="Label7" Style="color: #767373; font-size: 15px" Text="Bus No. -"></asp:Label>
                                            <asp:Label runat="server" ID="lblbusno" Text="Service Name"></asp:Label>
                                        </div>
                                        <div class="col-lg-7 mb-2">
                                            <asp:Label runat="server" ID="Label6" Style="color: #767373; font-size: 15px" Text="Etm Serial No. -"></asp:Label>
                                            <asp:Label runat="server" ID="lbletmserialno" Text="Service Name"></asp:Label>
                                        </div>
                                        <div class="col-lg-12 mb-2">
                                            <asp:Label runat="server" ID="Label5" Style="color: #767373; font-size: 15px" Text="Service Name -"></asp:Label>
                                            <asp:Label runat="server" ID="lblservicename" Text="Service Name"></asp:Label>
                                        </div>

                                        <div class="col-lg-12 mb-2">
                                            <asp:Label runat="server" ID="Label9" Style="color: #767373; font-size: 15px" Text="Driver -"></asp:Label>
                                            <asp:Label runat="server" ID="lbldriver" Text="Service Name"></asp:Label>
                                        </div>
                                        <div class="col-lg-12 mb-2">
                                            <asp:Label runat="server" ID="Label2" Style="color: #767373; font-size: 15px" Text="Conductor -"></asp:Label>
                                            <asp:Label runat="server" ID="lblconductor" Text="Service Name"></asp:Label>
                                        </div>
                                        <div class="col-lg-12 mb-2">
                                            <asp:Label runat="server" ID="Label4" Style="color: #767373; font-size: 15px" Text="Departure DateTime -"></asp:Label>
                                            <asp:Label runat="server" ID="lbldeparturedate" Text="Service Name"></asp:Label>
                                        </div>
                                        <div class="col-lg-12 mb-2">
                                            <asp:Label runat="server" ID="Label8" Style="color: #767373; font-size: 15px" Text="Route -"></asp:Label>
                                            <asp:Label runat="server" ID="lblroute" Text="Service Name"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6 pl-3">
                                    <div class="row">
                                        <div class="col-lg-12 text-center">
                                            <h2 style="font-size: 13pt" class="text-center">Waybill Closure Initiate</h2>
                                        </div>
                                        <div class="col-lg-5 mb-2">
                                            <asp:Label runat="server" ID="Label1" Style="color: #767373; font-size: 15px" Text="Waybill Closure Reason ">Waybill Closure Reason<span class="text-warning">*</span></asp:Label>
                                        </div>
                                        <div class="col-lg-5 mb-2">
                                            <asp:DropDownList runat="server" OnSelectedIndexChanged="ddlclosure_SelectedIndexChanged" AutoPostBack="true" ID="ddlclosure" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-lg-5 mb-2">
                                            <asp:Label runat="server" ID="Label10" Style="color: #767373; font-size: 15px" Text="Waybill Closure Remark ">Waybill Closure Remark<span class="text-warning">*</span></asp:Label>
                                        </div>
                                        <div class="col-lg-7 mb-2">
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtremark" TextMode="MultiLine" placeholder="Enter Remark" Height="90px"></asp:TextBox>
                                        </div>

                                        <div class="col-lg-5 mb-2" runat="server" id="dvfile" visible="false">
                                            <asp:Label runat="server" ID="Label11" Style="color: #767373; font-size: 15px" Text="Upload Waybill File ">Upload Waybill File<span class="text-warning">*</span></asp:Label>
                                        </div>
                                        <div class="col-lg-5 mb-2" runat="server" id="dvfileupload" visible="false">

                                            <asp:Button ID="btnUploadpdf" OnClick="btnUploadpdf_Click" runat="server" CausesValidation="False" CssClass="button1"
                                                Style="display: none" TabIndex="18" Text="Upload File" Width="80px" />
                                            <asp:FileUpload ID="fudocfile" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-success btn-sm"
                                                onchange="UploadPDF(this);" />
                                            <asp:Label runat="server" ID="lblFileName" CssClass="col-form-label control-label" Style="font-size: 12px; font-weight: normal;"></asp:Label>
                                        </div>

                                        <div class="col-lg-12 mb-2">
                                            <asp:LinkButton runat="server" OnClick="btnsubmit_Click" ID="btnsubmit" CssClass="btn btn-success  float-right">Submit</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlNodata" Visible="true">
                            <div class="row mt-5 mb-5">
                                <div class="col-12 text-center">
                                    <asp:Label ID="lblnodata" runat="server" Text="" Font-Bold="true" Font-Size="XX-Large" ForeColor="LightGray"></asp:Label>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="row">
                    <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
                        CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed; display: none;">
                        <div class="card" style="min-width: 350px;">
                            <div class="card-header">
                                <h4 class="card-title">Please Confirm
                                </h4>
                            </div>
                            <div class="card-body" style="min-height: 100px;">
                                <asp:Label ID="lblConfirmation" runat="server"></asp:Label>
                                <div style="width: 100%; margin-top: 20px; text-align: right;">
                                    <asp:LinkButton ID="lbtnYesConfirmation" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbtnYesConfirmation_Click"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-3"> <i class="fa fa-times"></i> No </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button4" runat="server" Text="" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        function UploadPDF(fileUpload) {
            //alert(1);
            if ($('#fudocfile').value != '') {
                document.getElementById("<%=btnUploadpdf.ClientID %>").click();
            }
        }
    </script>
</body>
</html>
