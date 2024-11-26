<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dashetmSubmission.aspx.cs" Inherits="Auth_dashetmSubmission" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="Start your development with a Dashboard for Bootstrap 4." />
    <meta name="author" content="Creative Tim" />
    <title>ETM Branch</title>
    <!-- Favicon -->
    <link rel="icon" href="../assets/img/brand/favicon.png" type="image/png" />
    <!-- Fonts -->
    <!-- Icons -->
    <link rel="stylesheet" href="../assets/vendor/nucleo/css/nucleo.css" type="text/css" />
    <link rel="stylesheet" href="../assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" type="text/css" />
    <!-- Page plugins -->
    <!-- Argon CSS -->
    <link rel="stylesheet" href="../assets/css/argon.css?v=1.2.0" type="text/css" />
    <link href="../assets/vendor/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <link href="../assets/css/jquery.dataTables.min.css" rel="stylesheet" />

    <script type="text/javascript" src="../assets/js/jquery-n.js"></script>
    <script src="../assets/js/jquery-ui.js" type="text/javascript"></script>
    <script src="../assets/vendor/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    <script src="../assets/vendor/js-cookie/js.cookie.js"></script>
    <script src="../assets/vendor/jquery.scrollbar/jquery.scrollbar.min.js"></script>
    <script src="../assets/vendor/jquery-scroll-lock/dist/jquery-scrollLock.min.js"></script>



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
    <script src="../assets/vendor/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <link href="../css/paging.css" rel="stylesheet" />

    <script type="text/javascript">
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }

        $(document).ready(function () {

            var currDate = new Date().getDate();
            var nextDate = new Date(new Date().setDate(currDate + 1));
            var prevDate = new Date(new Date().setDate(currDate - 1));
            var todayDate = new Date(new Date().setDate(currDate));


            $('[id*=txttodate]').datepicker({
                endDate: nextDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
            $('[id*=txtfromdate]').datepicker({
                endDate: nextDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
        });

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
        <div class="container-fluid pb-5">
            <div class="card card-stats mb-3 m-0" style="min-height: 600px">
                <div class="card-header p-2">
                    <div class="row m-0 ml-1">

                        <div class="col-lg-12">
                            <div class="row">
                                <div class="col-md-1">
                                    <asp:Label ID="lblDate" runat="server" CssClass="form-control-label">From Date</asp:Label>
                                    <br />
                                    <asp:TextBox CssClass="form-control form-control-sm" runat="server" ID="txtfromdate" MaxLength="10" placeholder="DD/MM/YYYY"
                                        Text="" Style="font-size: 9pt; padding: 2px 5px; height: 30px; width: 100%; margin-right: -10px;" autocomplete="off"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,Custom"
                                        TargetControlID="txtfromdate" ValidChars="/" />

                                </div>
                                <div class="col-md-1">
                                    <asp:Label ID="Label1" runat="server" CssClass="form-control-label">To Date</asp:Label>
                                    <br />
                                    <asp:TextBox CssClass="form-control form-control-sm" runat="server" ID="txttodate" MaxLength="10" placeholder="DD/MM/YYYY"
                                        Text="" Style="font-size: 9pt; padding: 2px 5px; height: 30px; width: 100%; margin-right: -10px;" autocomplete="off"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom"
                                        TargetControlID="txttodate" ValidChars="/" />

                                </div>
                                <div class="col-md-2">
                                    <asp:Label runat="server" CssClass="form-control-label">Bus Type</asp:Label>
                                    <asp:DropDownList ID="ddlBusType" runat="server" Style="height: 30px; font-size: 9pt;" ToolTip="Bus Type"
                                        CssClass="form-control form-control-sm">
                                        <asp:ListItem>Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label runat="server" CssClass="form-control-label"> Service Type</asp:Label>
                                    <asp:DropDownList ID="ddlServiceType" runat="server" Style="height: 30px; font-size: 9pt;" ToolTip="Service Type"
                                        CssClass="form-control form-control-sm">
                                        <asp:ListItem>Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label runat="server" CssClass="form-control-label"> Route</asp:Label>
                                    <asp:DropDownList ID="ddlRoutes" runat="server" Style="height: 30px; font-size: 9pt;" ToolTip="Route"
                                        CssClass="form-control form-control-sm">
                                        <asp:ListItem>Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4 pl-0 mt-2 pt-3 pr-0">
                                    <asp:LinkButton ID="lbtnsearch" runat="server" OnClick="lbtnsearch_Click" class="btn btn-sm btn-primary" Style="border-radius: 4px; height: 30px; font-size: 10pt; padding-top: 4px;"> <i class="fa fa-search"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnResetFilter" runat="server" OnClick="lbtnResetFilter_Click" class="btn btn-sm btn-warning" Style="border-radius: 4px; height: 30px; font-size: 10pt; padding-top: 4px;"> <i class="fa fa-undo"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnview" Visible="false" ToolTip="View Instructions" OnClick="lbtnview_Click" runat="server" Width="30px" CssClass="btn btn bg-gradient-orange btn-sm  text-white">
                                    <i class="fa fa-info"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnDownloadExcel" Visible="false" runat="server" class="btn btn bg-gradient-green btn-sm text-white" Style="border-radius: 4px; height: 30px; font-size: 10pt; padding-top: 4px;" ToolTip="Download Excel"> <i class="fa fa-download"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-body p-2">
                    <div class="row mt-2">
                        <div class="col-lg-12">
                            <asp:Panel runat="server" ID="pnlViewDuty" Visible="true">
                                <div>
                                    <asp:GridView ID="gvSubmittedEtm" runat="server" AutoGenerateColumns="false" GridLines="None"
                                        AllowPaging="true" PageSize="20" ShowHeader="false" OnRowCommand="gvSubmittedEtm_RowCommand" OnRowDataBound="gvSubmittedEtm_RowDataBound" CssClass="table table-hover table-striped mt-1"
                                        DataKeyNames="dutyrefno,etmrefno">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Service">
                                                <ItemTemplate>


                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <asp:Label runat="server" ID="Label4" Font-Bold="true" CssClass=""><%# Eval("serviceid") %></asp:Label>
                                                            |
                                                             <asp:Label runat="server" ID="Label2" CssClass="text-uppercase" Font-Size="12px"><%# Eval("servicename") %></asp:Label>
                                                        </div>
                                                        <div class="col-lg-10">
                                                            <div class="row">
                                                                <div class="col-lg-6">
                                                                    Duty Date/Time:
																	<asp:Label runat="server" ID="Label3" Font-Bold="true" CssClass="mr-4"><%# Eval("dutydate") %></asp:Label>

                                                                </div>
                                                                <div class="col-lg-6">
                                                                    Duty Ref No:
															<span style="font-weight: bold; padding-left: 2px;"><%# Eval("dutyrefno") %></span>

                                                                </div>
                                                                <div class="col-lg-6">
                                                                    Bus:
																<asp:Label runat="server" ID="lblBusNo" CssClass="form-control-label  text-sm" Font-Bold="true"><%# Eval("BUSNO") %></asp:Label>
                                                                    <br />
                                                                    <div runat="server" id="divCashStatus">
                                                                        Cash Status:
																<asp:Label runat="server" ID="lblCashStatus" CssClass="form-control-label  text-sm" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-6">
                                                                    Driver:
																<asp:Label runat="server" ID="lblDriver1" CssClass="" Font-Bold="true"><%# Eval("EMPDRIVER1") %></asp:Label>
                                                                    <br />
                                                                    <asp:Label runat="server" Visible="false" ID="lblDriver2" CssClass="" Font-Bold="true"><%# Eval("EMPDRIVER2") %></asp:Label>
                                                                    Conductor:
																<asp:Label runat="server" ID="lblConductor1" CssClass="" Font-Bold="true"><%# Eval("EMPCONDUCTOR1") %></asp:Label><br />
                                                                    <asp:Label runat="server" Visible="false" Font-Bold="true" ID="lblConductor2" CssClass=""><%# Eval("EMPCONDUCTOR2") %></asp:Label>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2 mt-1">
                                                            <asp:LinkButton ID="lbtnviewWayBill" CommandName="viewWaybill" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                Visible="true" runat="server" CssClass="btn btn-sm btn-warning"
                                                                ToolTip="View Waybill"> W</asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnviewDutyslip" CommandName="viewdutyslip" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                Visible="true" runat="server" CssClass="btn btn-sm btn-danger"
                                                                ToolTip="View Dutyslip">D</asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton5" CommandName="viewcashslip" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                Visible="true" runat="server" CssClass="btn btn-sm btn-success"
                                                                ToolTip="View Cash Deposit Slip">C</asp:LinkButton>
                                                        </div>

                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" CssClass="GridPager" ForeColor="Black" />
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlNoRecord1" runat="server" Width="100%" Visible="true">
                                <div class="col-md-12 p-0" style="text-align: center;">
                                    <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
                                        No Record Available<br />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>

                    </div>
                </div>
            </div>
               <div class="row">
                    <cc1:ModalPopupExtender ID="mpShowDutySlip" runat="server" PopupControlID="Panel2" TargetControlID="Button1"
                        CancelControlID="LinkButton2" BackgroundCssClass="ModalPopupBG">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="Panel2" runat="server" Style="position: fixed;">
                        <div class="modal-content mt-5" style="width: 65vw;">
                            <div class="card w-100">
                                <div class="card-header py-3">
                                    <div class="row">
                                        <div class="col">
                                            <h3 class="m-0">Duty Slip</h3>
                                        </div>
                                        <div class="col-auto">
                                            <asp:LinkButton ID="LinkButton2" runat="server" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body p-2">

                                    <asp:Literal ID="eSlip" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button1" runat="server" Text="" />
                            <asp:Button ID="Button2" runat="server" Text="" />
                        </div>
                    </asp:Panel>
                </div>
                <div class="row">
                    <cc1:ModalPopupExtender ID="mpShowWaybill" runat="server" PopupControlID="Panel3" TargetControlID="Button3"
                        CancelControlID="LinkButton3" BackgroundCssClass="ModalPopupBG">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="Panel3" runat="server" Style="position: fixed;">
                        <div class="modal-content mt-5" style="width: 65vw;">
                            <div class="card w-100">
                                <div class="card-header py-3">
                                    <div class="row">
                                        <div class="col">
                                            <h3 class="m-0">Waybill</h3>
                                        </div>
                                        <div class="col-auto">
                                            <asp:LinkButton ID="LinkButton3" runat="server" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body p-2">

                                    <asp:Literal ID="eWaybill" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button3" runat="server" Text="" />
                            <asp:Button ID="Button5" runat="server" Text="" />
                        </div>
                    </asp:Panel>
                </div>
            <div class="row">
                    <cc1:ModalPopupExtender ID="mpDepositedConductor" runat="server" PopupControlID="Panel4" TargetControlID="Button6"
                        CancelControlID="LinkButton4" BackgroundCssClass="ModalPopupBG">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="Panel4" runat="server" Style="position: fixed;">
                        <div class="modal-content mt-5" style="width: 40vw;">
                            <div class="card w-100">
                                <div class="card-header py-3">
                                    <div class="row">
                                        <div class="col">
                                            <h3 class="m-0">Cash Deposit Slip</h3>
                                        </div>
                                        <div class="col-auto">
                                            <asp:LinkButton ID="LinkButton4" runat="server" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body p-2">

                                    <asp:Literal ID="eDepositedSlip" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button6" runat="server" Text="" />
                            <asp:Button ID="Button7" runat="server" Text="" />
                        </div>
                    </asp:Panel>
                </div>
        </div>
    </form>
</body>
</html>
