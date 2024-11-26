<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/subCscMaster.Master" AutoEventWireup="true" CodeFile="SubCscCashregister.aspx.cs" Inherits="Auth_SubCscCashregister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
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
    <script type="text/javascript">
        function SuccessMessage(msg) {
            $.confirm({
                icon: 'fa fa-check',
                title: 'Confirmation',
                content: msg,
                animation: 'zoom',
                closeAnimation: 'scale',
                type: 'green',
                typeAnimated: true,
                buttons: {
                    tryAgain: {
                        text: 'OK',
                        btnClass: 'btn-green',
                        action: function () {
                        }
                    },

                }
            });

        }

        function ErrorMessage(msg) {
            $.confirm({
                title: 'Warning!',
                content: msg,
                animation: 'zoom',
                closeAnimation: 'scale',
                type: 'red',
                typeAnimated: true,
                buttons: {
                    tryAgain: {
                        text: 'try again',
                        btnClass: 'btn-red',
                        action: function () {
                        }
                    },
                }
            });
        }
    </script>
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <style>
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
            $('[id*=txtSearch]').datepicker({
                //startDate: "dateToday",
                endDate: endD,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
            //$('[id*=gmdpJDate]').datepicker({
            //    //startDate: "dateToday",
            //    endDate: endD,
            //    changeMonth: true,
            //    changeYear: false,
            //    format: "dd/mm/yyyy",
            //    autoclose: true
            //});

            $('[id*=txtDateF]').datepicker({
                //startDate: "dateToday",
                endDate: endD,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });


           
                $('[id*=txtDateT]').datepicker({
                    //startDate: today,
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

    <div class="container-fluid">
        <div class="row">

            <div class="col-md-12 ">
                <div class="card mt-2">
                    <div class="card-header">
                        <div class="row ">
                            <div class="col-4">
                                <strong>Sub CSC Account Details</strong><br />
                                <asp:Label ID="Label3" runat="server" ForeColor="Green" Font-Size="small" Font-Bold="false" Text="(Details Available Only 30 Days at a time)"></asp:Label>

                            </div>
                           
                            <div class="col-2">
                                <asp:Label ID="Label1" runat="server" Font-Size="small" Font-Bold="false" Text="From Date"></asp:Label><br />
                                <asp:TextBox ID="txtDateF" placeholder="DD/MM/YYYY" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                            </div>
                            <div class="col-2">
                                <asp:Label ID="Label2" runat="server" Font-Size="small" Font-Bold="false" Text="To Date"></asp:Label><br />
                                <asp:TextBox ID="txtDateT" placeholder="DD/MM/YYYY" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                            </div>
                            <div class="col-2" style="padding: 0px; padding-top: 20px;">
                                <asp:LinkButton ID="lbtnSearch" OnClick="lbtnSearch_Click" ToolTip="Click here for Search" Style="padding: 7px;" runat="server" CssClass="btn btn-warning btn-sm">
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
                                                                    <i class="fa fa-download" ></i> Download</asp:LinkButton>
                                <asp:LinkButton ID="lbtnexcel" runat="server" OnClick="lbtnexcel_Click" Visible="false" ToolTip="Click here for Download Excel" CssClass="btn  btn-warning btn-sm"><i class="fa fa-file-excel" style="padding: 7px;"></i>EXCEL</asp:LinkButton>
                            </div>
                        </div>


                        <asp:GridView ID="grdtransactionDetails" runat="server" AllowPaging="true" PageSize="10" AutoGenerateColumns="False" ForeColor="#333333" Font-Size="14px"
                            OnPageIndexChanging="grdtransactionDetails_PageIndexChanging"
                            CssClass="table" GridLines="None" Font-Bold="false" DataKeyNames="trans_date" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="CSC Name/Code">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblAGENT_CODE" Text='<%# Eval("val_agentcode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transaction <br/>Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTRANSDATE" runat="server" CssClass="text-primary" Text='<%# Eval("trans_date", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opening Balance<br/> (A)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOPENINGBALANCE" runat="server" Text='<%# Eval("opening_balance") %>'></asp:Label>
                                        <i class="fa fa-rupee-sign"></i>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Wallet Recharge<br/> (B)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDEPOSITAMOUNT" runat="server" Text='<%# Eval("deposit_amount") %>'></asp:Label>
                                         <i class="fa fa-rupee-sign"></i>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transaction Amount<br/> (C)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTRANSACTIONAMOUNT" runat="server" Text='<%# Eval("transaction_amount") %>'></asp:Label>
                                         <i class="fa fa-rupee-sign"></i>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Commission Amount <br/>(D)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCOMMAMT" runat="server" Text='<%# Eval("comm_amt") %>'></asp:Label>
                                        <i class="fa fa-rupee-sign"></i>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Refund Amount <br/>(E)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDBTICKETAMT" runat="server" Text='<%# Eval("db_ticket_amt") %>'></asp:Label>
                                        <i class="fa fa-rupee-sign"></i>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closing Balance <br/>(A+B+E)-C">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCLOSINGBALANCE" ForeColor="Green" Font-Bold="true" runat="server" Text='<%# Eval("closing_balance") %>'></asp:Label>
                                        <i class="fa fa-rupee-sign"></i>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                        <div class="row">
                            <div class="col-12 mb-2 mt-5">
                                <center>

                                    <asp:Label ID="grdmsg" runat="server" Text="Details Not Available for selected dates"
                                        Style="color: #DDDDDD; font-size: xx-large" CssClass="mt-5"></asp:Label>
                                </center>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <cc1:ModalPopupExtender ID="mperror" runat="server" PopupControlID="pnlerror" CancelControlID="lbtnclose1"
            TargetControlID="Button1" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlerror" runat="server" Style="position: fixed;">
            <div class="card" style="width: 350px;">
                <div class="card-header">
                    <h4 class="card-title text-left mb-0">Please Check & Correct
                    </h4>
                </div>
                <div class="card-body text-left pt-2" style="min-height: 100px;">
                    <asp:Label ID="lblerrormsg" runat="server" Text="Please Check entered values."></asp:Label>
                    <div style="width: 100%; margin-top: 20px; text-align: right;">
                        <asp:LinkButton ID="lbtnclose1" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> OK </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button1" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
    <div class="row">
        <cc1:ModalPopupExtender ID="MpPaymentConfirm" runat="server" PopupControlID="pnlconfirm" TargetControlID="Button7666"
            CancelControlID="LinkButton1984548" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlconfirm" runat="server" Style="position: fixed;">
            <div class="modal-content mt-1">
                <div class="modal-header">
                    <div class="col-md-10">
                        <h3 class="m-0">
                            <asp:Label ID="lblAgmimitmsg" runat="server" Text="Please Confirm"></asp:Label></h3>
                    </div>
                    <div class="col-md-2 text-right">
                        <asp:LinkButton ID="LinkButton1984548" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                    </div>
                </div>
                <div class="modal-body p-0">
                    <div class="card" style="font-size: 12px; min-height: 100px; min-width: 250px">
                        <div class="row mt-0">
                            <div class="col-sm-12 flex-column d-flex stretch-card ">
                                <div class="card-body table table-responsive">
                                    <div class="row p-4">
                                        <asp:HiddenField ID="hdpgid" runat="server" />
                                        <asp:Label ID="lblTopupRequest" Font-Size="x-large" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="row px-2 mt-2">
                                        <div class="col input-group-prepend  ">
                                            <asp:Label ID="Label4" ForeColor="red" Font-Size="x-large" runat="server" Text="Do you want to proceed?"></asp:Label>
                                            <asp:LinkButton ID="lbtnPaymentYes" Font-Size="x-large" runat="server" CssClass="btn btn-success ml-2"> <i class="fa fa-check"></i> Yes </asp:LinkButton>


                                            <asp:LinkButton ID="lbtnPaymentClose" Font-Size="x-large" runat="server" CssClass="btn btn-warning ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="Button7666" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>


</asp:Content>





