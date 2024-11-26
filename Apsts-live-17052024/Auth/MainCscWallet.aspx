<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/CscMasterPage.master" AutoEventWireup="true" CodeFile="MainCscWallet.aspx.cs" Inherits="Auth_MainCscWallet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">



    <link href="assets/css/download_jquery-confirm.min.css" rel="stylesheet" type="text/css" />
    <script src="assets/js/download_jquery-confirm.min.js" type="text/javascript"></script>
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
    <style type="text/css">
        .grRowStyle {
            border-bottom: solid 1px #BBD9EE;
        }
    </style>
    <style type="text/css">
        .grRowStylePass {
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid">
        <asp:Panel ID="pnlWallet" runat="server">


            <div class="row">
                <div class="col-md-6">
                    <div class="card mt-2 shadow">
                        <div class="card-header" style="height: 70px;">
                            <strong>

                                <asp:Label ID="Label2" Font-Size="25px" runat="server" Text="Wallet Topup "></asp:Label>
                            </strong>

                        </div>
                        <div class="card-body" style="min-height: 80vh;">

                            <asp:Panel ID="pnlwallettopup" Visible="true" runat="server" Style="padding-top: 2px;">
                                <div class="row mt-1" style="margin-bottom: 1px; font-size: 16px;">
                                    <div class="col-lg-12 mt-2">
                                       <asp:Label ID="lblWalletBalance" runat="server" Text="0"></asp:Label>
                                        
                                    </div>
                                    <div class="col-lg-12 mt-2">
                                        <asp:Label ID="lblWalletLastUpdate" runat="server" Text="0"></asp:Label>
                                     <hr class="mt-4" />
                                    </div>
                                    <div class="col-lg-12 mt-1">
                                        <center>
                                            <asp:Label ID="Label5" runat="server" Font-Size="Larger" Font-Bold="true" Text="Topup Your Wallet"></asp:Label>
                                        </center>
                                    </div>
                                    <div class="col-md-12 mt-3">
                                        <center>

                                            <asp:Label ID="lblmaxlimit" runat="server" Font-Size="16px" Text="Choose Payment Gateway To Pay"></asp:Label>

                                        </center>
                                    </div>
                                    <div class="col-lg-12 mt-4 input-group-prepend ">
                                        <asp:Label ID="Label15" MaxLength="5" runat="server" Font-Size="16px" Text="Step 1. Enter Topup Amount" CssClass="mt-1"></asp:Label>

                                        <asp:TextBox ID="txtAmount" placeholder="Amount₹" CssClass="form-control ml-2" autocomplete="off" MaxLength="5" Width="120px" runat="server"></asp:TextBox>
                                        <asp:Label ID="Label6" MaxLength="5" runat="server" Font-Size="18px" Text=" ₹" CssClass="mt-1 ml-2"></asp:Label>

                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                            TargetControlID="txtAmount" />
                                    </div>
                                    <div class="col-lg-12 mt-3 input-group-prepend ">
                                        <asp:Label ID="Label16" MaxLength="5" Font-Size="16px" runat="server" Text="Step 2. Select Payment Gateway" CssClass="mt-1"></asp:Label>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="panelPgGateway" runat="server" Visible="true">
                                <div class="row mt-3">

                                    <div class="col-md-12">
                                        <asp:Label ID="lblNoPG" runat="server" Text="Payment Gateways are not available.<br>Please contact to helpdesk." Visible="false" Style="font-size: 23px; color: #919691; font-weight: bold;"></asp:Label>
                                    </div>

                                    <asp:Repeater ID="rptrPG" runat="server" OnItemCommand="rptrPG_ItemCommand">
                                        <ItemTemplate>
                                            <div class="col-md-6 mt-3 ">
                                                <asp:HiddenField ID="rptHdPGId" runat="server" Value='<%# Eval("gateway_id") %>' />
                                                     <asp:HiddenField ID="hd_pgurl" runat="server" Value='<%# Eval("req_url") %>' />
                                                <img src="../Dbimg/PG/<%# Eval("gateway_name")%>_W.png" style="height: 30px;" />
                                                <h3 class="mt-2 mb-1">
                                                    <%# Eval("gateway_name")%>
                                                </h3>
                                                <p>
                                                    <%# Eval("description")%>
                                                </p>
                                                <asp:Button ID="btnpaytm" runat="server" Text="Proceed to Pay" CssClass="btn mb-2"
                                                    Style="background-color: #00baf2; color: White; font-weight: bold;" CommandName="PAYNOW" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>

                <div class="col-md-6 ">
                    <div class="card mt-2 shadow">
                        <div class="card-header" style="height: 70px;">
                            <div class="row ">
                                <div class="col">
                                    <strong>

                                        <asp:Label ID="Label1" Font-Size="25px" runat="server" Text="Topup Transactions"></asp:Label>
                                    </strong>
                                    <asp:Label ID="Label3" runat="server" ForeColor="red" Font-Size="small" Font-Bold="true" Text="(Last 30 Days)"></asp:Label>
                                </div>

                                <%--<div class="col-auto input-group float-right" style="width: auto;">
                                <asp:Label ID="Label11" runat="server" Style="margin-bottom: 1px; color: #a8a8a8; font-size: 16px;" Text="From"></asp:Label>
                                <asp:TextBox ID="txtDateF" Width="105px" placeholder="DD/MM/YYYY" runat="server" CssClass="form-control ml-2 mr-2" Style="margin-top: -5px; font-size: smaller;"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtenderFrom" runat="server" CssClass="black" PopupPosition="BottomRight"
                                    Format="dd/MM/yyyy" PopupButtonID="txtDateF" TargetControlID="txtDateF" BehaviorID="CalendarExtenderFrom"></cc1:CalendarExtender>

                                <asp:Label ID="Label2" runat="server" Style="margin-bottom: 1px; color: #a8a8a8; font-size: 16px;" Text="To"></asp:Label>
                                <asp:TextBox ID="txtDateT" Width="105px" placeholder="DD/MM/YYYY" runat="server" CssClass="form-control ml-2 " Style="margin-top: -5px; font-size: smaller;"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtenderTo" runat="server" CssClass="black" PopupPosition="BottomRight"
                                    Format="dd/MM/yyyy" PopupButtonID="txtDateT" TargetControlID="txtDateT" BehaviorID="CalendarExtenderTo"></cc1:CalendarExtender>

                                <asp:LinkButton ID="lbtnSearch" ToolTip="Click here for Search" runat="server" Style="margin-top: -5px" CssClass="btn btn-warning ml-1">
                                                                    <i class="fa fa-search" ></i></asp:LinkButton>
                            </div>--%>
                            </div>
                        </div>
                        <div class="card-body" style="min-height: 80vh;">


                            <asp:GridView ID="gvtopuptransaction" CssClass="table table-sm" runat="server" GridLines="None" ShowHeader="false"
                                    AllowPaging="true" PageSize="8"  AutoGenerateColumns="false" Width="100%" OnPageIndexChanging="gvtopuptransaction_PageIndexChanging" OnRowDataBound="gvtopuptransaction_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="row">
                                                    <div class="col text-left">
                                                        <p class="mb-0 font-weight-light"><%# Eval("txnrefno") %></p>
                                                        <p class="mb-0 font-weight-light" style="font-size:13px;"><%# Eval("txnstartdate") %></p>
                                                    </div>
                                                    <div class="col pr-0">
                                                        <p class="mb-0 font-weight-light"><%# Eval("txnamount") %> <i class="fa fa-rupee-sign"></i></p>                                                        
                                                        <p class="mb-0 font-weight-light" style="font-size:13px;"><%# Eval("gatewayname") %></p>
                                                    </div>
                                                      <div class="col-auto text-right">
                                                        <h5 class="mb-0 font-weight-light font-weight-bold" 
                                                    style='<%# Eval("txncompleteyn").ToString() == "N"? "color:Red;": "color:#0ed10e;" %>'
                                                    ><%# Eval("val_status") %></h5>
                                                      <%--  <p class="mb-0 font-weight-bold" style='<%# If(Eval("TXN_COMPLETE_YN").ToString() = "N", "color:Red;", "color:#0ed10e;") %>'>
                                                            <%# Eval("status") %>
                                                        </p>--%>
                                                    </div>
                                                    <div class="col-auto text-right">
                                                        <%--<p class="mb-0 font-weight-bold" style='<%# If(Eval("TXN_COMPLETE_YN").ToString() = "N", "color:Red;", "color:#0ed10e;") %>'>
                                                            <%# Eval("status") %>
                                                        </p>--%>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="GridPager" ForeColor="Black" />
                                </asp:GridView>

                            <div class="row">
                                <div class="col-12 mb-2">
                                    <center>

                                        <asp:Label ID="grdmsg1" runat="server" Text="Details Not Available for selected dates"
                                            Style="color: #DDDDDD; font-size: xx-large" CssClass="mt-5"></asp:Label>
                                    </center>
                                </div>

                            </div>

                            <%--<asp:GridView ID="grdagpassbook" OnPageIndexChanging="grdagpassbook_PageIndexChanging1" OnRowDataBound="grdagpassbook_RowDataBound" runat="server"
                            AllowPaging="true" PageSize="20" CssClass="table" AutoGenerateColumns="False" ForeColor="#333333" Font-Size="14px"
                            GridLines="None" Font-Bold="false" ClientIDMode="Static" DataKeyNames="TRANSDATE,OPENINGBALANCE,TRANSACTIONAMOUNT,DEPOSITAMOUNT,CLOSINGBALANCE"
                            Width="100%">

                            <Columns>
                                <asp:TemplateField HeaderText="Tranasaction <br/>Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTRANSDATE" runat="server" CssClass="text-primary" Text='<%# Eval("TRANSDATE", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opening<br/> Balance">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOPENINGBALANCE" runat="server" Text='<%# Eval("OPENINGBALANCE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transaction<br/> Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTRANSACTIONAMOUNT" runat="server" Text='<%# Eval("TRANSACTIONAMOUNT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deposit <br/>Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDEPOSITAMOUNT" runat="server" Text='<%# Eval("DEPOSITAMOUNT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closing <br/>Balance">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCLOSINGBALANCE" ForeColor="Green" Font-Bold="true" runat="server" Text='<%# Eval("CLOSINGBALANCE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                           
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>--%>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
           <div class="row">
        <!-- Modal -->
        <cc1:ModalPopupExtender ID="ModalPopupError" runat="server" PopupControlID="PanelModalError"
            TargetControlID="ButtonOpenModalError" CancelControlID="LinkButtonCloseModalError"
            BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="PanelModalError" runat="server" Style="position: fixed; display: none;">
            <div class="modal-dialog">
                <div class="modal-content " style="min-width: 400px;">
                    <div class="modal-header">
                        <div class="col-md-10 pl-0">
                            <h3 class="m-0">
                                <asp:Label ID="LabelModalErrorHeader" runat="server" ForeColor="Red"></asp:Label>
                            </h3>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="LinkButtonCloseModalError" runat="server" UseSubmitBehavior="false"
                                Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="modal-body">
                        <p class="full-width-separator" style="font-size: 17px;">
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
            <cc1:ModalPopupExtender ID="mpsuccess" runat="server" PopupControlID="pnlsuccess"
                CancelControlID="lbtnsuccessclose1" TargetControlID="Button6" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlsuccess" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Confirm
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblsuccessmsg" runat="server" Text="Do you want to Update ?"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnsuccessclose1" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button6" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

    <div class="row">
        <!-- Modal -->
        <cc1:ModalPopupExtender ID="mpconfirmation" runat="server" PopupControlID="pnlconfirmation"
            TargetControlID="btnOpenmpConfirm" CancelControlID="LinkButtonCloseModalError"
            BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlconfirmation" runat="server" Style="position: fixed; display: none;">
            <div class="modal-dialog">
                <div class="modal-content " style="min-width: 400px; max-width: 90vw;">
                    <div class="modal-header">
                        <h3 class="m-0">Please Confirm
                        </h3>
                    </div>
                    <div class="modal-body">
                        <p class="full-width-separator mb-2" style="font-size: 17px; line-height: 24px;">
                            <asp:HiddenField ID="HiddenField2" runat="server" />
                             <asp:HiddenField ID="hdpgurl" runat="server" />
                            <asp:Label ID="Label8" runat="server"></asp:Label>
                        </p>
                        <h5 class="text-danger">Do you want to proceed ?</h5>
                    </div>
                    <div class="modal-footer text-right">
                        <asp:LinkButton ID="lbtnyes" runat="server" OnClick="lbtnyes_Click" UseSubmitBehavior="false" CssClass="btn btn-success btn-sm"
                            Style="cursor: pointer; font-weight: bold; font-size: 10pt;"><i class="fa fa-check"></i> Yes</asp:LinkButton>
                        <asp:LinkButton ID="lbtnno" runat="server" UseSubmitBehavior="false" CssClass="btn btn-danger btn-sm"
                            Style="cursor: pointer; font-weight: bold; font-size: 10pt;"><i class="fa fa-times"></i> No</asp:LinkButton>
                    </div>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="btnOpenmpConfirm" runat="server" Text="" />
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
                                    <div class="row px-4">
                                        <asp:HiddenField ID="hdpgid" runat="server" />
                                        <asp:Label ID="lblTopupRequest" Font-Size="large" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="row px-4 mt-2">
                                        <div class="col input-group-prepend  ">
                                            <asp:Label ID="Label4" ForeColor="red" Font-Size="large" runat="server" Text="Do you want to proceed?"></asp:Label>
                                            <asp:LinkButton ID="lbtnPaymentYes" Font-Size="large" OnClick="lbtnPaymentYes_Click" runat="server" CssClass="btn btn-success ml-2"> <i class="fa fa-check"></i> Yes </asp:LinkButton>


                                            <asp:LinkButton ID="lbtnPaymentClose" Font-Size="large" runat="server" CssClass="btn btn-warning ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>

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

    <div class="row">
        <cc1:ModalPopupExtender ID="mptranshistory" runat="server" PopupControlID="pnltranshistory"
            TargetControlID="Button2" CancelControlID="Button3" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnltranshistory" runat="server" Style="position: fixed;">
            <div class="card" style="margin-bottom: 0px; padding-bottom: 0px; overflow: auto;">
                <div class="modal-content" style="width: 600px;">
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <center>
                                    <strong>Agent Transaction History</strong><br />
                                </center>
                                <asp:LinkButton ID="btntransclose1" runat="server" CssClass="btn btn-danger" ToolTip="Close"
                                    Style="float: right; margin-top: -30px; padding: 5px; border-radius: 4px;">X</asp:LinkButton>
                                <div class="row" style="margin-top: 20px;">
                                    <div class="col-lg-12">
                                        <asp:GridView ID="agtransstatus" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            AllowSorting="true" AllowPaging="true" PageSize="10" Font-Size="10pt" Width="100%"
                                            class="table" DataKeyNames="AGENTTRANSACTIONREFNO,STATUS,UPDATIONDATETIME">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblagtransstatus" runat="server" Text='<%# Eval("statusname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Time">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblagupdatime" runat="server" Text='<%# Eval("UPDATIONDATETIME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle BackColor="White" BorderColor="#333333" />
                                            <AlternatingRowStyle BackColor="#eaf4ff" VerticalAlign="Middle" />
                                            <HeaderStyle BackColor="#6699FF" ForeColor="White" VerticalAlign="Middle" />
                                            <PagerStyle HorizontalAlign="Left" CssClass="GridPager" />
                                        </asp:GridView>
                                        <center>
                                            <asp:Label ID="grdmsg2" runat="server" Text="Details Not Available" Style="color: Red;"></asp:Label>
                                        </center>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 20px;">
                                    <div class="col-lg-12" style="text-align: right;">
                                        <asp:Button ID="btntransclose" runat="server" Text="Close" CssClass="btn btn-success"
                                            Style="border-radius: 4px;" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="Button2" runat="server" Text="" />
                <asp:Button ID="Button3" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>

    <div class="row">
        <cc1:ModalPopupExtender ID="mPconfirmPayment" runat="server" PopupControlID="pnlConfirmPayment" TargetControlID="Button143254"
            CancelControlID="LinkButton14q3534" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlConfirmPayment" runat="server" Style="position: fixed;">
            <div class="modal-content mt-1">
                <div class="modal-header">
                    <div class="col-md-10">
                        <h5 class="m-0">
                            <asp:Label ID="Label7" runat="server" Text="Wallet Topup Successfully"></asp:Label></h5>
                    </div>
                    <div class="col-md-2 text-right">
                        <asp:LinkButton ID="LinkButton14q3534" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                    </div>
                </div>
                <div class="modal-body p-0">
                    <div class="card" style="font-size: 12px; min-height: 100px; min-width: 450px">
                        <div class="row mt-0">
                            <div class="col-sm-12 flex-column d-flex stretch-card ">
                                <div class="card-body table table-responsive">
                                    <div class="row px-4">
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <asp:Label ID="lblmsg" Font-Size="large" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="row px-4 mt-2" style="font-size:15px">
                                       

                                        


                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="Button143254" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>



