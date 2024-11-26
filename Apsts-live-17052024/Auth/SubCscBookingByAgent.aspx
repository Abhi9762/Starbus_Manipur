<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/subCscMaster.Master" AutoEventWireup="true" CodeFile="SubCscBookingByAgent.aspx.cs" Inherits="Auth_SubCscBookingByAgent" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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


    <!-- Optional JS -->
    <script src="../assets/vendor/chart.js/dist/Chart.min.js"></script>
    <script src="../assets/vendor/chart.js/dist/Chart.extension.js"></script>
    <script src="../assets/vendor/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <link href="../css/paging.css" rel="stylesheet" />
    <!-- Argon JS -->
    <script src="../assets/js/argon.js?v=1.2.0"></script>

    <link rel="stylesheet" href="../assets/css/jquery-ui.css" type="text/css" />
    <style>
        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .pagination-ys {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }

            .pagination-ys table > tbody > tr > td {
                display: inline;
            }

                .pagination-ys table > tbody > tr > td > a, .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    color: #dd4814;
                    background-color: #ffffff;
                    border: 1px solid #dddddd;
                    margin-left: -1px;
                }

                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    margin-left: -1px;
                    z-index: 2;
                    color: #aea79f;
                    background-color: #f5f5f5;
                    border-color: #dddddd;
                    cursor: default;
                }

                .pagination-ys table > tbody > tr > td:first-child > a, .pagination-ys table > tbody > tr > td:first-child > span {
                    margin-left: 0;
                    border-bottom-left-radius: 4px;
                    border-top-left-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td:last-child > a, .pagination-ys table > tbody > tr > td:last-child > span {
                    border-bottom-right-radius: 4px;
                    border-top-right-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td > a:hover, .pagination-ys table > tbody > tr > td > span:hover, .pagination-ys table > tbody > tr > td > a:focus, .pagination-ys table > tbody > tr > td > span:focus {
                    color: #97310e;
                    background-color: #eeeeee;
                    border-color: #dddddd;
                }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            var todayDate = new Date().getDate();
            var endD = new Date(new Date().setDate(todayDate));

            var currDate = new Date();
            $('[id*=txtDateF]').datepicker({
                //startDate: "dateToday",
                endDate: endD,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });

            $('[id*=txtDateT]').datepicker({
                //startDate: "dateToday",
                endDate: endD,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });

        });


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid" style="padding-top: 20px;">

        <div class="row">
            <div class="col-12">
                <div class="card" style="min-height: 700px">
                    <div class="card-header">
                        <div class="row ">
                            <div class="col-4">
                                <strong>Sub CSC Transaction Details</strong><br />
                                <asp:Label ID="Label3" runat="server" ForeColor="Green" Font-Size="small" Font-Bold="false" Text="(Details will be Available Recharge wallet/Booking/Cancellation details)"></asp:Label>
                            </div>

                            <div class="col-2">
                                <asp:Label ID="Label2" runat="server" Font-Size="small" Font-Bold="false" Text="Transaction Date"></asp:Label><br />
                                <asp:TextBox ID="txtDateF" placeholder="DD/MM/YYYY" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                            </div>
                            <div class="col-2">
                                <asp:Label ID="Label1" runat="server" Font-Size="small" Font-Bold="false" Text="Transaction Date"></asp:Label><br />
                                <asp:TextBox ID="txtDateT" placeholder="DD/MM/YYYY" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                            </div>
                            <div class="col-2" style="padding: 0px; padding-top: 20px;">
                                <asp:LinkButton ID="lbtnSearch" OnClick="lbtnSearch_Click" OnClientClick="return ShowLoading();" ToolTip="Click here for Search" Style="padding: 7px;" runat="server" CssClass="btn btn-success btn-sm">
                                                                    <i class="fa fa-search" ></i></asp:LinkButton>

                            </div>
                        </div>
                    </div>
                    <div class="card-body" style="min-height: 80vh;">

                        <div class="row mb-2">
                            <div class="col-lg-4">
                                <span><b>Summary | </b>
                                    <asp:Label ID="lblsmry" runat="server" Text=""></asp:Label>
                                </span>
                            </div>
                            <div class="col-lg-8 text-right">
                                <asp:LinkButton ID="lbtndownload" OnClick="lbtndownload_Click" ToolTip="Click here for Download" Visible="false" Style="padding: 7px;" runat="server" CssClass="btn btn-danger btn-sm">
                                                                    <i class="fa fa-file-pdf" ></i> PDF</asp:LinkButton>
                                <asp:LinkButton ID="lbtnexcel" OnClick="lbtnexcel_Click" ToolTip="Click here for Download" Visible="false" Style="padding: 7px;" runat="server" CssClass="btn btn-warning btn-sm">
                                                                    <i class="fa fa-file-excel" ></i> EXCEL</asp:LinkButton>
                            </div>
                        </div>
                        <asp:GridView ID="grdtransactionDetails" runat="server" AllowPaging="true" PageSize="10" AutoGenerateColumns="False" ForeColor="#333333" Font-Size="14px"
                            OnPageIndexChanging="grdtransactionDetails_PageIndexChanging"
                            CssClass="table" GridLines="None" Font-Bold="false" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Booking/Cancellation">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblbookingcancellation" Text='<%# Eval("header_name_") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transaction Date">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbltxndate" Text='<%# Eval("transaction_date_") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transaction No.">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTXN_REF_NO" Text='<%# Eval("transaction_number_") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fare <br/> (A)">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblfare" Text='<%# Eval("fare_") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Reservation Charge <br/> (B)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblreservation" runat="server" Text='<%# Eval("reservation_charge_") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount <br/> (C)">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldiscount" runat="server" Text='<%# Eval("discount_") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Concession <br/> (D)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblconcession" runat="server" Text='<%# Eval("concession_") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Commission <br/> (E)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcommission" runat="server" Text='<%# Eval("commission_") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax <br/> (F)">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltax" runat="server" Text='<%# Eval("tax_") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Paid <br/> (G)">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotalpaid" runat="server" Text='<%# Eval("total_paid_") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Wallet Deduction <br/> (H)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblwalletdeduction" runat="server" Text='<%# Eval("wallet_deduction_") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Refund Amt <br/> (I)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblrefund" runat="server" Text='<%# Eval("refund_amt_") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                        <div class="row">
                            <div class="col-12 mb-2 mt-5">
                                <center>
                                    <asp:Label ID="grdmsg" runat="server" Text="Transaction Details Not Available for selected perameter"
                                        Style="color: #DDDDDD; font-size: xx-large" CssClass="mt-5"></asp:Label>
                                </center>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>




        <div class="row">
            <cc1:ModalPopupExtender ID="mpError" runat="server" PopupControlID="pnlError" CancelControlID="lbtnerrorclose"
                TargetControlID="Button3" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlError" runat="server" Style="position: fixed; display: none">
                <div class="card" style="min-width: 350px; max-width: 650px;">
                    <div class="card-header">
                        <h4 class="card-title m-0">Please Check
                        </h4>
                    </div>
                    <div class="card-body py-2 px-3" style="min-height: 100px; max-height: 70vh; overflow: auto;">
                        <asp:Label ID="lblerrmsg" runat="server" Font-Size="18px"></asp:Label>
                    </div>
                    <div class="card-footer text-right ">
                        <asp:LinkButton ID="lbtnerrorclose" runat="server" CssClass="btn btn-danger"> OK </asp:LinkButton>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button3" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>



