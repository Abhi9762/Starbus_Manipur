<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/subCscMaster.master" AutoEventWireup="true" CodeFile="subCscDash.aspx.cs" Inherits="Auth_subCscDash" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <!-- Argon JS -->
    <script src="../assets/js/argon.js?v=1.2.0"></script>

    <link rel="stylesheet" href="../assets/css/jquery-ui.css" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$=ddlPlaceFrom]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "subCscDash.aspx/source_destination",
                        data: "{'stationText':'" + request.term + "','fromTo':'F'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {

                        }
                    });
                }
            });
            $("[id$=ddlPlaceTo]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "subCscDash.aspx/source_destination",
                        data: "{'stationText':'" + request.term + "','fromTo':'T'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {

                        }
                    });
                }
            });


        });
        function close() {
            alert('ok');
        }
    </script>





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
    <script>
        function showPop() {
            $('#todayTransactionModal').modal('show');
        }
    </script>
    <style>
        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .table td, .table th {
            padding: 0.25rem;
            vertical-align: top;
            border-top: 1px solid #dee2e6;
        }

        label {
            margin-bottom: .0rem;
        }

        .font-weight-bolder {
            font-weight: 600 !important;
        }

        .info {
            overflow: hidden;
        }

            .info .card-body .rotate {
                z-index: 8;
                float: right;
                height: 100%;
            }

                .info .card-body .rotate i {
                    color: rgba(20, 20, 20, 0.15);
                    position: absolute;
                    left: auto;
                    right: -5px;
                    bottom: -5px;
                    display: block;
                    -webkit-transform: rotate(-44deg);
                    -moz-transform: rotate(-44deg);
                    -o-transform: rotate(-44deg);
                    -ms-transform: rotate(-44deg);
                    transform: rotate(-44deg);
                }

        .table_head th {
            padding-top: 5px;
            padding-bottom: 5px;
        }
    </style>
    <style type="text/css">
        .grRowStyle {
            border-bottom: solid 1px #BBD9EE;
        }
    </style>
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

            $('[id*=txtBookingDT]').datepicker({
                //startDate: "dateToday",
                endDate: endD,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });


            $(document).ready(function () {
                var today = new Date();
                var lastDayOfMonth = new Date(today.getFullYear(), today.getMonth() + 1, 0);

                $('[id*=gmdpJDate]').datepicker({
                    startDate: today,
                    endDate: lastDayOfMonth,
                    changeMonth: true,
                    changeYear: false,
                    format: "dd/mm/yyyy",
                    autoclose: true
                });

                $('[id*=txtJourneyDt]').datepicker({
                    startDate: today,
                    endDate: lastDayOfMonth,
                    changeMonth: true,
                    changeYear: false,
                    format: "dd/mm/yyyy",
                    autoclose: true
                });
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid">
        <asp:HiddenField ID="hdmaxdate" runat="server" />
        <div class="row">
        </div>
        <div class="row" style="margin-top: 15px">
            <div class="col-3">
                <div class="row mt-2">
                    <div class="col-12">
                        <div class="card shadow pt-2 pb-2 pl-3 pr-3">
                            <asp:Label ID="Label23" Font-Bold="true" runat="server" Text="Please Note -"></asp:Label>
<div class="row">
                               <div class="col-12">
                                    Download Manual
                            <asp:LinkButton href="../Auth/UserManuals/Sub CSC/Help Document for Sub CSC.pdf" target="_blank" runat="server" CssClass="btn btn-success btn-sm  ml-1  mt-1" ToolTip="Click here for manual." Font-Size="small"><i class="fa fa-download"></i></asp:LinkButton>

                               </div>
                           </div>
                            <hr />
                            <asp:Label ID="Label16" Font-Bold="true" runat="server" Text="About Account"></asp:Label>

                           <ol>
                                <li>Account Balance managed by your CSC.
                                </li>
                                <li>Wallet Recharge is set by your CSC.
                                </li>
                                <%--<li>If you have reached to your limit then contact your CSC.
                                </li>--%>
                            </ol>
                               
                            
                          <br />
                            <br />

                            <asp:Label ID="Label17" Font-Bold="true" runat="server" Text="About Ticket Booking"></asp:Label>
                            1. You can book the tickets.
                               <br />
                            2. You can cancel the tickets.
                              <br />
                            3. You can resend ticket confirmation messages.
                               <br />
                            4. You can print the tickets.
                             <br />
                           <asp:Label ID="lbladvancedays" runat="server" Text=""></asp:Label>
                            <br />
                            <asp:Label ID="Label18" Font-Bold="true" runat="server" Text="About Bus Pass"></asp:Label>
                            1. You can register the bus pass.
                              <br />
                            2. There are two types of bus pass are available (Monthly and Govt. Bus Pass).
                                <br />
                            3. You can renew the bus pass.
                                                       
                            <hr />
                            <asp:Label ID="Label4" Font-Bold="true" runat="server" Text="Quick Links"></asp:Label>
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:LinkButton ID="lbtnTimeTable" OnClientClick="return ShowLoading();" runat="server" OnClick="lbtnTimeTable_Click" CssClass="btn btn-primary w-100 btn-outline-primary ml-1 mt-1" Font-Size="Small">  Time Table </asp:LinkButton>
                                </div>
                                <div class="col-md-6">
                                    <asp:LinkButton ID="lbtnCancellationpolicy" runat="server" CssClass="btn btn-primary w-100 btn-outline-primary ml-1 mt-1" Font-Size="Small" data-toggle="modal" data-target="#CPModal"> Cancellation policy </asp:LinkButton>
                                </div>
                                <div class="col-md-6">
                                    <asp:LinkButton ID="lbtncancelticket" OnClientClick="return ShowLoading();" runat="server" OnClick="lbtncancelticket_Click" CssClass="btn btn-primary w-100 btn-outline-primary ml-1 mt-1" Font-Size="Small">  Cancel a ticket </asp:LinkButton>
                                </div>
                                <div class="col-md-6">
                                    <asp:LinkButton ID="lbtndailycashregister" OnClientClick="return ShowLoading();" runat="server" OnClick="lbtndailycashregister_Click" CssClass="btn btn-primary w-100 btn-outline-primary ml-1 mt-1" Font-Size="Small">  Daily cash register </asp:LinkButton>
                                </div>
                                <div class="col-md-6">
                                    <asp:LinkButton ID="lbtnbookingcancel" OnClientClick="return ShowLoading();" runat="server" OnClick="lbtnbookingcancel_Click" CssClass="btn btn-primary w-100 btn-outline-primary ml-1 mt-1" Font-Size="Small">  Booking & Cancellation </asp:LinkButton>
                                </div>
                                 <div class="col-md-6">
                                    <asp:LinkButton ID="lbtnspecialrefund" OnClientClick="return ShowLoading();" runat="server" OnClick="lbtnspecialrefund_Click" CssClass="btn btn-primary w-100 btn-outline-primary ml-1 mt-1" Font-Size="Small">  Special Refund </asp:LinkButton>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-12">
                                    <asp:Label ID="Label19" runat="server" Text="You have any query/help you may contact us at."></asp:Label>
                                </div>

                                <div class="col-12 ml-3">

                                    <i class="fa fa-mobile "></i>
                                    <asp:Label ID="lblmobile" CssClass="" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-12 ml-3">
                                    <i class="fa fa-envelope "></i>
                                    <asp:Label ID="lblemail" CssClass="" runat="server" Text=""></asp:Label>
                                </div>
                            </div>


                        </div>
                    </div>
                </div>


            </div>
            <div class="col-md-4">

                <div class="row mt-2">
                    <div class="col-12 ">
                        <div class="card shadow pt-2 pb-2 pl-3 pr-3">
                            <div class="row">
                                <div class="col-12">
                                    <asp:Label ID="Label11" runat="server" Font-Bold="true" Text="Account Details"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <asp:Label ID="lblWalletBalance" runat="server" Text="0"></asp:Label>
                                    ₹ 
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <asp:Label ID="lblWalletLastUpdate" runat="server" Text="0"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card mt-2 shadow">
                    <div class="card-body pt-2" style="min-height: 520px;">
                        <div class="row mb-2">
                            <div class="col" style="padding-top: 6px;">
                                <asp:Label ID="Label10" Font-Bold="true" runat="server" Text="Daily Register"></asp:Label>
                            </div>
                            <div class="col-auto input-group float-right" style="width: auto;">

                                <asp:TextBox CssClass="form-control" AutoComplete="off" runat="server" ID="txtSearch" MaxLength="10" ToolTip="Enter Date"
                                    placeholder="DD/MM/YYYY" Text="" Style="display: inline;"></asp:TextBox>
                                <asp:LinkButton ID="lbtnSearch" ToolTip="Click here for Search" OnClick="lbtnSearch_Click" runat="server" Font-Size="Small" CssClass="btn btn-warning btn-sm">
                                                                    <i class="fa fa-search" ></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="row mt-0" style="font-size: 10pt;">
                            <div class="col-12 ">
                                <hr style="margin-bottom: 6px; margin-top: 3px;" />
                            </div>
                            <div class="col-6">
                                <asp:Label ID="Label1" runat="server" Text="Opening Balance"></asp:Label>
                                <asp:Label ID="lblopenblnc" runat="server" Font-Bold="true" Text="0"></asp:Label>
                                <i class="fa fa-rupee-sign"></i>
                            </div>
                            <div class="col-6">
                                <asp:Label ID="Label3" runat="server" Text="Closing Balance"></asp:Label>
                                <asp:Label ID="lblcloseblnc" runat="server" Font-Bold="true" Text="0"></asp:Label>
                                <i class="fa fa-rupee-sign"></i>
                            </div>
                            <div class="col-12 ">
                                <hr style="margin-bottom: 6px; margin-top: 3px;" />
                            </div>
                            <div class="col-3">
                                <asp:Label ID="Label24" runat="server" Text="Tickets"></asp:Label>
                            </div>
                            <div class="col-3">
                                <asp:Label ID="lblTotalBooking" runat="server" Font-Bold="true" Text="0"></asp:Label>
                            </div>
                            <div class="col-3">
                                <asp:Label ID="Label27" runat="server" Text="Ticket Amount"></asp:Label>
                            </div>
                            <div class="col-3">
                                <asp:Label ID="lblBookingAmt" runat="server" Font-Bold="true" Text="0"></asp:Label>
                                <i class="fa fa-rupee-sign"></i>
                            </div>
                            <div class="col-3">
                                <asp:Label ID="Label29" runat="server" Text="Cancellation"></asp:Label>
                            </div>
                            <div class="col-3">
                                <asp:Label ID="lblCancelledBooking" runat="server" Font-Bold="true" Text="0"></asp:Label>
                            </div>
                            <div class="col-3">
                                <asp:Label ID="Label39" runat="server" Text="Refund Amount"></asp:Label>
                            </div>
                            <div class="col-3">
                                <asp:Label ID="lblCancelamt" runat="server" Font-Bold="true" Text="0"></asp:Label>
                                <i class="fa fa-rupee-sign"></i>
                            </div>
                            <div class="col-3">
                                <asp:Label ID="Label41" runat="server" Text="Passes"></asp:Label>
                            </div>
                            <div class="col-3">
                                <asp:Label ID="lblTotalPass" runat="server" Font-Bold="true" Text="0"></asp:Label>
                            </div>
                            <div class="col-3">
                                <asp:Label ID="Label43" runat="server" Text="Pass Amount"></asp:Label>
                            </div>
                            <div class="col-3">
                                <asp:Label ID="lblPassAmount" runat="server" Font-Bold="true" Text="0"></asp:Label>
                                <i class="fa fa-rupee-sign"></i>
                            </div>
                            <div class="col-3">
                                <asp:Label ID="Label37" runat="server" Text="Commission"></asp:Label>
                            </div>
                            <div class="col-3">
                                <asp:Label ID="lblCommissionAmt" runat="server" Font-Bold="true" Text="0"></asp:Label>
                                <i class="fa fa-rupee-sign"></i>
                            </div>

                            <div class="col-12 mt-4">
                                <asp:GridView ID="gvDailyRegister" runat="server"
                                    ClientIDMode="Static" AutoGenerateColumns="False" Visible="true" AllowPaging="True" PageSize="4" CssClass="table " ForeColor="#333333" Font-Size="14px"
                                    GridLines="None" Font-Bold="false" OnRowCommand="gvDailyRegister_RowCommand" DataKeyNames="val_txn_ref_no,val_txn_type,val_wallet_txn_type_code" OnPageIndexChanging="gvDailyRegister_PageIndexChanging" OnRowDataBound="gvDailyRegister_RowDataBound" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Transaction No.">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblTXN_REF_NO" Text='<%# Eval("val_txn_ref_no") %>'></asp:Label><br />
                                                (<asp:Label runat="server" ID="lbltxndate" Text='<%# Eval("txn_date") %>'></asp:Label>)
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transaction Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTXNTYPE" runat="server" Text='<%# Eval("val_txn_type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblTXN_AMOUNT" ForeColor="Green" Text='<%# Eval("val_txn_amount") %>'></asp:Label>
                                                <i class="fa fa-rupee"></i>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="lbtnView" runat="server" Font-Size="Small" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                            class="btn btnIcon btn-warning " ToolTip="View Details"><i class="fa fa-eye  "></i> </asp:LinkButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>

                                <hr />
                            </div>
                        </div>


                    </div>
                </div>
                <div class="card mt-2 shadow">
                    <div class="row mb-3">
                        <div class="col-12 mt-3 ml-3">

                            <asp:Label ID="Label12" runat="server" Font-Bold="true" Text="Search a Ticket"></asp:Label>
                        </div>
                        <div class="col-lg-12">


                            <div class="row p-2 mr-2">
                                <div class="col-md-5">
                                    <p style="margin-bottom: 1px; color: #a8a8a8; font-weight: bold; font-size: 14px;">
                                        Enter Details 
                                    </p>
                                    <asp:TextBox ID="txtTicketSearch" autocomplete="off" Width="200px"
                                        placeholder="Ticket No./Name/Mobile/Email Id" ToolTip="Enter Details" CssClass="form-control" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-md-7">
                                    <div class="row">
                                        <div class="col-md-5">
                                            <p style="margin-bottom: 1px; color: #a8a8a8; font-weight: bold; font-size: 14px;">
                                                Booking Dt
                                            </p>
                                            <asp:TextBox ID="txtBookingDT" autocomplete="off" Width="120px"
                                                placeholder="DD/MM/YYYY" ToolTip="Select Booking Date" CssClass="form-control mr-1" runat="server"></asp:TextBox>


                                        </div>
                                        <div class="col-md-5">
                                            <p style="margin-bottom: 1px; color: #a8a8a8; font-weight: bold; font-size: 14px;">
                                                Journey Dt
                                            </p>
                                            <asp:TextBox ID="txtJourneyDt" autocomplete="off" Width="120px"
                                                placeholder="DD/MM/YYYY" ToolTip="Select Journey Date" CssClass="form-control mr-1" runat="server"></asp:TextBox>

                                        </div>

                                        <div class="col-auto input-group float-right" style="width: auto;">
                                            <asp:LinkButton ID="lbtnSearchTicketDetails" ToolTip="Click here for Search" runat="server" OnClick="lbtnSearchTicketDetails_Click" CssClass="btn btn-sm btn-warning mt-4 ml-0">
                                                                    <i class="fa fa-search" ></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="row mb-2" runat="server" visible="false">
                        <div class="col-12 mt-0 ml-3">

                            <asp:Label ID="Label25" runat="server" Font-Bold="true" Text="Search a Bus Pass"></asp:Label>
                        </div>
                        <div class="col-lg-12">
                            <asp:Panel ID="Panel1" runat="server" Width="100%" Visible="true">
                                <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                    <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 5px; padding-bottom: 50px; font-size: 20px; font-weight: bold;">
                                        will be available soon
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="row p-2 mr-2 d-none">
                                <div class="col-md-5">
                                    <p style="margin-bottom: 1px; color: #a8a8a8; font-weight: bold; font-size: 14px;">
                                        Enter Details 
                                    </p>
                                    <asp:TextBox ID="txtpassSearch" autocomplete="off" Width="200px"
                                        placeholder="Pass No./Name/Mobile/Email Id" ToolTip="Enter Details" CssClass="form-control" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-md-7">
                                    <div class="row">
                                        <div class="col-md-5">
                                            <p style="margin-bottom: 1px; color: #a8a8a8; font-weight: bold; font-size: 14px;">
                                                Apply Date
                                            </p>
                                            <asp:TextBox ID="txtBusPassApplyDate" autocomplete="off" Width="120px"
                                                placeholder="DD/MM/YYYY" ToolTip="Select Apply Date" CssClass="form-control mr-1" runat="server"></asp:TextBox>


                                        </div>
                                        <div class="col-md-5">
                                            <p style="margin-bottom: 1px; margin-left: 2px; color: #a8a8a8; font-weight: bold; font-size: 14px;">
                                                Pass Type
                                            </p>
                                            <asp:DropDownList ID="ddlBusPassType" CssClass="form-control ml-1" runat="server"></asp:DropDownList>
                                        </div>

                                        <div class="col-md-2">
                                            <asp:LinkButton ID="lbtnSearchBusPass" Enabled="true" ToolTip="Click here for Search" runat="server" CssClass="btn btn-sm btn-secondary mt-4 ml-0">
                                                                    <i class="fa fa-search" ></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div class="col-md-5">
                <div class="card mt-2 shadow">
                    <div class="card-header" style="height: 48px;">
                        <strong>Book Ticket</strong>
                    </div>
                    <div class="card-body" style="min-height: 585px;">
                        <div class="row mt-2">
                            <div class="col-lg-12">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="row p-2">
                                            <div class="col-md-4">
                                                <p style="margin-bottom: 1px; font-size: 14px;">
                                                    From Station
                                                </p>

                                                <asp:TextBox ID="ddlPlaceFrom" CssClass="form-control form-control-sm search-box" AutoComplete="Off" Placeholder="From Station" runat="server" MaxLength="20"></asp:TextBox>

                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Custom" TargetControlID="ddlPlaceFrom" ValidChars=" ()" />
                                            </div>
                                            <div class="col-md-4">
                                                <p style="margin-bottom: 1px; font-size: 14px;">
                                                    To Station
                                                </p>
                                                <asp:TextBox ID="ddlPlaceTo" CssClass="form-control form-control-sm search-box" AutoComplete="Off" Placeholder="To Station" runat="server" MaxLength="20"></asp:TextBox>

                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Custom" TargetControlID="ddlPlaceTo" ValidChars=" ()" />

                                            </div>
                                            <div class="col-md-4">
                                                <p style="margin-bottom: 1px; font-size: 14px;">
                                                    Service Type
                                                </p>
                                                <asp:DropDownList ID="ddlServiceType" CssClass="form-control  ml-1 mr-3" runat="server">

                                                    <asp:ListItem>Service Type</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-9">
                                        <div class="row p-2">
                                            <div class="row">
                                                <div class="col-auto" style="padding-top: 2px;">
                                                    <div class="input-group float-right" style="width: auto;">

                                                        <asp:LinkButton ID="btntoday" runat="server" OnClick="btntoday_Click" CssClass="btn btn-sm btn-success mr-2 mt-2">Today
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="btntomorrow" runat="server" OnClick="btntomorrow_Click" CssClass="btn btn-sm btn-primary mr-2 mt-2 ">Tomorrow
                                                        </asp:LinkButton>

                                                        <asp:TextBox ID="gmdpJDate" placeholder="DD/MM/YYYY" autocomplete="off" CssClass="form-control form-control-sm mt-1" runat="server"></asp:TextBox>


                                                        <asp:LinkButton ID="btnsearch" ToolTip="Click here for Search" OnClick="btnsearch_Click" runat="server" CssClass="btn btn-sm btn-warning ml-2  mr-2 mt-2">
                                                                    <i class="fa fa-search" ></i>Search </asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnReset" ToolTip="Click here for Reset" runat="server" OnClick="lbtnReset_Click" CssClass="btn btn-sm btn-danger mr-2 mt-2">
                                                                    <i class="fa fa-close " ></i>Reset </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="SearchTicketDetails" Visible="false" runat="server">
                                    <div class="card-body pb-0 px-2" style="padding-top: 1em;">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-lg-8">
                                                    <p class="m-0 text-warning" style="line-height: 18px;">
                                                        <asp:Label ID="LabelSearchStations" runat="server" Text="" Style="font-weight: bold;"></asp:Label>
                                                    </p>
                                                    <p class="m-0 text-warning" style="line-height: 18px;">
                                                        <asp:Label ID="LabelSearchDateService" runat="server" Text="" Style="font-weight: bold;"></asp:Label>
                                                    </p>
                                                    <p class="m-0 text-warning" style="line-height: 18px;">
                                                        <asp:Label ID="busfund" runat="server" Text="" Style="font-weight: bold;"></asp:Label>
                                                    </p>
                                                </div>
                                                <asp:Repeater ID="RepterDetails" runat="server" OnItemCommand="RepterDetails_ItemCommand">

                                                    <ItemTemplate>
                                                        <div class="card-body pt-2 pb-2">
                                                            <div class="row" style="border-top: 1px solid #f3eaea;">
                                                                <div class="col-md-5 pr-0 mt-1">
                                                                    <asp:Label ID="LabelRepteFromStationCode" runat="server" Visible="false" Text='<%#Eval("frstonid") %>' />
                                                                    <asp:Label ID="LabelRepteToStationCode" runat="server" Visible="false" Text='<%#Eval("tostonid") %>' />
                                                                    <asp:Label ID="lblopenClose" runat="server" Visible="false" />
                                                                    <asp:Label ID="lblservicetypecode" Visible="false" runat="server" Text='<%#Eval("srtpid") %>' />
                                                                    <asp:Label ID="lblservicetripcode" Visible="false" runat="server" Text='<%#Eval("strpid") %>' />
                                                                    <i class="fa fa-bus" aria-hidden="true"></i>&nbsp;
                                                                    <asp:Label ID="lblservicetypename" runat="server" Text='<%#Eval("servicetypename") %>'
                                                                        Style="font-size: 14px; font-weight: bold; font-family: verdana;" /><br />
                                                                    <span style="color: #000066">Bus Service Code </span>
                                                                    <asp:Label ID="lblservicecode" runat="server" Text='<%#Eval("dsvcid") %>' /><asp:Label ID="lblserviceRorJ" runat="server" Text='<%#Eval("tripdirection") %>' /><%#Eval("strpid") %><br />
                                                                    <span style="color: #000066">Total Seats </span>
                                                                    <asp:Label ID="lblseattobook" runat="server" CssClass="text-primary font-weight-bold " Font-Size="14px" Text='<%#Eval("totalseat") %>' />
                                                                </div>
                                                                <div class="col-md-4 mt-1 text-center">
                                                                    <p class="full-width-separator m-0">
                                                                        Distance
                                                                        <asp:Label ID="lbldistance" runat="server" Font-Bold="true" ForeColor="#1d4a84" Text='<%#Eval("distance") %>' />
                                                                        KM
                                                                    </p>
                                                                    <p class="full-width-separator m-0">
                                                                        Departure Time
                                                                         <asp:Label ID="lbldepttime" ForeColor="#1d4a84" runat="server" Text='<%#Eval("depttime") %>' />

                                                                    </p>
                                                                    <p class="full-width-separator m-0">
                                                                        Arrival Time
                                                                                             
                                                                        <asp:Label ID="lblarritime" runat="server" ForeColor="#1d4a84" Text='<%#Eval("arrtime") %>' />
                                                                    </p>
                                                                </div>
                                                                <div class="col-md-3 mt-1 text-right">
                                                                    <p class="full-width-separator m-0" style="font-size: 21px; line-height: 21px;">
                                                                        <asp:Label ID="Label44" Font-Size="14px" Font-Bold="true" runat="server" Text="Fare" />
                                                                        <asp:Label ID="lblfare" runat="server" Text='<%#Eval("fare") %> ' />&nbsp;<i class="fa fa-rupee"
                                                                            aria-hidden="true"></i>
                                                                    </p>
                                                                    <p class="full-width-separator m-0">
                                                                        <span style="font-size: 12px; padding-right: 6px;">Available Seats</span><asp:Label ID="lbltotseats"
                                                                            runat="server" Text='<%#Eval("totalavailablseats") %>' Style="font-size: 21px; font-weight: bold; color: green;" />

                                                                    </p>
                                                                    <asp:Button ID="btndel" runat="server" Font-Size="Small" CssClass="btn text-white" Text="Book Now"
                                                                        CommandArgument='<%#String.Format(Eval("dsvcid") + "," + Eval("tripdirection") + "," + (Eval("fare")) + "," + (Eval("distance")) + "," + Eval("depttime"))%>'
                                                                        CommandName="book" Style="background: #043e95; text-transform: none;"></asp:Button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card mt-2 shadow" style="min-height: 220px;" runat="server" visible="false">

                    <div class="row mb-3">
                        <div class="col-12 mt-3 ml-3">

                            <asp:Label ID="Label13" runat="server" Font-Bold="true" Text="Bus Pass Registration"></asp:Label>
                        </div>
                        <div class="col-lg-12 ml-2">
                            <div class="row p-2 d-none">
                                <div class="col-md-4">
                                    <p style="margin-bottom: 1px; color: #a8a8a8; font-weight: bold; font-size: 14px;">
                                        Pass Category 
                                    </p>
                                    <asp:DropDownList ID="ddlBusPassCategory" AutoPostBack="true" CssClass="form-control " runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <p style="margin-bottom: 1px; color: #a8a8a8; font-weight: bold; font-size: 14px;">
                                        Pass Type 
                                    </p>
                                    <asp:DropDownList ID="ddlpasstype" CssClass="form-control " runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:LinkButton ID="lbtnBusPassRegistration" Enabled="true" runat="server" CssClass="btn btn-sm btn-secondary ml-0 mt-4"><i class="fa fa-check "></i>Apply
                                    </asp:LinkButton>


                                </div>
                            </div>
                            <asp:Panel ID="pnlNoRecord" runat="server" Width="100%" Visible="true">
                                <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                    <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 30px; font-weight: bold;">
                                        will be available soon
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--Bus Pass Details-->
    <div class="row">
        <cc1:ModalPopupExtender ID="mpBusPass" runat="server" PopupControlID="pnlBussPassPopup" TargetControlID="Button9"
            CancelControlID="LinkButton75" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlBussPassPopup" runat="server" Style="position: fixed;">
            <div class="modal-content mt-5">
                <div class="modal-header">
                    <div class="col-md-10">
                        <h3 class="m-0">Bus Pass Detail</h3>
                    </div>
                    <div class="col-md-2 text-right">
                        <asp:LinkButton ID="LinkButton75" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                    </div>
                </div>
                <div class="modal-body p-0">
                    <embed src="dashpass.aspx" style="height: 85vh; width: 80vw" />
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="Button9" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
    <!-- Error Popup  -->
    <div class="row">
        <cc1:ModalPopupExtender ID="ModalPopupError" runat="server" PopupControlID="PanelModalError"
            TargetControlID="ButtonOpenModalError" CancelControlID="LinkButtonCloseModalError"
            BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="PanelModalError" runat="server" Style="position: fixed;">
            <div class="modal-dialog" role="document">
                <div class="modal-content" style="min-width: 400px">
                    <div class="modal-header">
                        <div class="col-md-10">
                            <h3 class="m-0">
                                <asp:Label ID="LabelModalErrorHeader" runat="server" ForeColor="Black"></asp:Label>
                            </h3>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="LinkButtonCloseModalError" runat="server" UseSubmitBehavior="false"
                                Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="modal-body">
                        <p class="full-width-separator text-center" style="font-size: 17px;">
                            <asp:Label ID="LabelModalErrorMessage" runat="server"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="ButtonOpenModalError" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
    <div class="row">
        <cc1:ModalPopupExtender ID="mperror" runat="server" PopupControlID="pnlerror" CancelControlID="LinkButton2"
            TargetControlID="Button1" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlerror" runat="server" Style="width: 500px !important">
            <div class="card">
                <div class="card-header">
                    <h4 class="card-title text-left mb-0">Please Check
                    </h4>
                </div>
                <div class="card-body text-left pt-2" style="min-height: 100px;">
                    <asp:Label ID="lblerrormsg" runat="server"></asp:Label>
                    <div style="margin-top: 20px; text-align: right;">
                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                    </div>
                    <div style="visibility: hidden;">
                        <asp:Button ID="Button1" runat="server" Text="" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
    <!-- ticket Details -->
    <div class="row">
        <cc1:ModalPopupExtender ID="mpTicket" runat="server" PopupControlID="pnlMPEmail" TargetControlID="Button21"
            CancelControlID="LinkButton71" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlMPEmail" runat="server" Style="position: fixed;">
            <div class="modal-content mt-5">
                <div class="modal-header">
                    <div class="col-md-10">
                        <h3 class="m-0">Ticket Details</h3>
                    </div>
                    <div class="col-md-2 text-right">
                        <asp:LinkButton ID="LinkButton71" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                    </div>
                </div>
                <div class="modal-body p-0">
                    <embed src="dashticket.aspx" style="height: 85vh; width: 80vw" />
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="Button21" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>


    <div class="row">
        <div class="modal fade" id="CPModal" tabindex="-1" role="dialog" aria-labelledby="CPModalLabel"
            aria-hidden="true">
            <div class="modal-dialog  modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="col-md-10">
                            <h3 class="m-0">Cancellation Policy</h3>
                        </div>
                        <div class="col-md-2 text-right">
                            <a usesubmitbehavior="false" data-dismiss="modal" tooltip="Close" style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></a>
                        </div>
                    </div>
                    <div class="modal-body">
                        <ul class="list-group" style="font-size: 13px; font-family: verdana; color: #b10021;">
                            <li class="list-group-item">Cancellation/Refund/Rescheduling Ticket booked through Online,
                                    refund will be done to their respective Credit Cards/Debit Cards/Bank Accounts according
                                    to the Bank procedure. No refund will be done at UTC ticket booking counters
                                    
                            </li>
                            <li class="list-group-item">
                                <asp:Label ID="lblcancellationpolicy" runat="server"></asp:Label>
                            </li>
                            <li class="list-group-item">Cancellation is not allowed after Up to 2 hr before Schedule
                                    service start time at origin of the bus. From the date of journey.</li>
                            <li class="list-group-item">Reservation Fee is non-refundable except in case of 100%
                                    cancellation of tickets, if the service is cancelled by UTC for operational or any
                                    other reasons.</li>
                            <li class="list-group-item">Passengers will be given normally in one month, after the
                                    cancellation of ticket or receipt of e-mail. If refunds are delayed more than a
                                    month, passengers may contact helpline telephone number at 8476007605 E-Mail help[dot]UTConline[at]gmail[dot]com.</li>
                            <li class="list-group-item">Payment Gateway Service charges will not be refunded for
                                    service cancellation/ failure transactions in e-ticketing.</li>
                            <li class="list-group-item">Partial cancellation is allowed for which cancellation terms
                                    & conditions will apply.</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>



