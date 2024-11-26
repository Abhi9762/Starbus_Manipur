<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/CscMasterPage.master" AutoEventWireup="true" CodeFile="MainCscAccount.aspx.cs" Inherits="Auth_MainCscAccount" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">



    <link href="assets/css/download_jquery-confirm.min.css" rel="stylesheet" type="text/css" />
    <script src="assets/js/download_jquery-confirm.min.js" type="text/javascript"></script>

     <script type="text/javascript">
         $(document).ready(function () {
             var todayDate = new Date().getDate();
             var endD = new Date(new Date().setDate(todayDate));
             var currDate = new Date();
             $('[id*=txtfromdate]').datepicker({
                 //startDate: "dateToday",
                 endDate: endD,
                 changeMonth: true,
                 changeYear: false,
                 format: "dd/mm/yyyy",
                 autoclose: true
             });
             $('[id*=txttodate]').datepicker({
                 //startDate: "dateToday",
                 endDate: endD,
                 changeMonth: true,
                 changeYear: false,
                 format: "dd/mm/yyyy",
                 autoclose: true
             });

         });
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
        <div class="row">


            <div class="col-md-12 ">
                <div class="card mt-2 ">
                    <div class="card-header">
                        <div class="row ">
                            <div class="col-4">
                                <strong>CSC Account</strong><br />
                                <asp:Label ID="Label3" runat="server" ForeColor="Green" Font-Size="small" Font-Bold="false" Text="(Details Available Only 30 Days at a time)"></asp:Label>

                            </div>
                            <div class="col-3">
                                <asp:Label ID="Label1" runat="server" Font-Size="small" Font-Bold="false" Text="From Date"></asp:Label><br />
                              <%--  <asp:TextBox ID="txtDateF" placeholder="DD/MM/YYYY" runat="server" CssClass="form-control form-control-sm"></asp:TextBox--%>
                                     <asp:TextBox CssClass="form-control" AutoComplete="off" runat="server" ID="txtfromdate" MaxLength="10" ToolTip="Enter Date"
                                            placeholder="DD/MM/YYYY" Text="" Style="display: inline;"></asp:TextBox>
                                
                            </div>
                            <div class="col-3">
                                <asp:Label ID="Label2" runat="server" Font-Size="small" Font-Bold="false" Text="To Date"></asp:Label><br />
                                 <asp:TextBox CssClass="form-control" AutoComplete="off" runat="server" ID="txttodate" MaxLength="10" ToolTip="Enter Date"
                                            placeholder="DD/MM/YYYY" Text="" Style="display: inline;"></asp:TextBox>
                            </div>
                            <div class="col-2" style="padding: 0px; padding-top: 20px;">
                                <asp:LinkButton ID="lbtnSearch" OnClientClick="return ShowLoading();" OnClick="lbtnSearch_Click" ToolTip="Click here for Search" Style="padding: 7px;"  runat="server" CssClass="btn btn-warning btn-sm">
                                                                    <i class="fa fa-search" ></i></asp:LinkButton>
                                <asp:LinkButton ID="lbtndownload"  OnClick="lbtndownload_Click" ToolTip="Click here for Download Pdf" Style="padding: 7px;"  runat="server" CssClass="btn btn-danger btn-sm">
                                                                    <i class="fa fa-download" ></i></asp:LinkButton>
                                
                                 <asp:LinkButton ID="lbtnEXCEL"  runat="server" Style="padding: 7px;" OnClick="lbtnEXCEL_Click" Visible="false" ToolTip="Click here for Download Excel" CssClass="btn btn-success btn-sm">
                                     <i class="fa fa-file-excel" ></i></asp:LinkButton>
                            </div>

                        </div>

                    </div>
                    <div class="card-body" style="min-height: 80vh;">
                        <asp:GridView ID="grdagpassbook" runat="server" AllowPaging="true" PageSize="2" AutoGenerateColumns="False" ForeColor="#333333" Font-Size="14px"
                            OnPageIndexChanging="grdagpassbook_PageIndexChanging" OnRowCommand="grdagpassbook_RowCommand"
                            CssClass="table" GridLines="None" Font-Bold="false" DataKeyNames="tras_date" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Transaction Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTRANSDATE" runat="server" CssClass="text-primary" Text='<%# Eval("tras_date", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opening Balance (A)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOPENINGBALANCE" runat="server" Text='<%# Eval("opening_balance") %>'></asp:Label>
                                        <i class="fa fa-rupee-sign"></i>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Wallet Recharge (B)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDEPOSITAMOUNT" runat="server" Text='<%# Eval("deposit_amount") %>'></asp:Label>
                                        <i class="fa fa-rupee-sign"></i>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub CSC TopUp (C)">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnViewDetails" runat="server" CommandName="ViewAllocation" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                            ToolTip="View Sub CSC Allocaiton"> <%# Eval("csc_allocation") %> <i class="fa fa-rupee-sign"></i> </asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Commission Amount (D)">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnViewDetails" runat="server" CommandName="ViewAllocation" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                            ToolTip="View Sub CSC Allocaiton"> <%# Eval("comm_amt") %> <i class="fa fa-rupee-sign"></i> </asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closing Balance (A+B+D)-C">
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
                    <asp:Button ID="Button2" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="MpTopup" runat="server" PopupControlID="pnlTopup" TargetControlID="Button1"
                CancelControlID="LinkButton1" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlTopup" runat="server" Style="position: fixed;">
                <div class="modal-content mt-1" style="min-width:60vw; max-width:90vw; max-height:90vh; overflow:auto;">
                    <div class="modal-header">
                        <div class="col">
                            <h5 class="m-0">
                                <asp:Label ID="Label5" runat="server" Text="Sub CSC Topup Transaction Details"></asp:Label></h5>
                        </div>
                        <div class="col-auto">
                            <asp:LinkButton ID="LinkButton1" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="grvtopup" runat="server" AllowPaging="true" PageSize="10" AutoGenerateColumns="False" ForeColor="#333333" Font-Size="14px"
                            OnPageIndexChanging="grvtopup_PageIndexChanging"
                            CssClass="table" GridLines="None" Font-Bold="false" DataKeyNames="txndatetime" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Transaction Date/Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltransdate" runat="server" CssClass="text-primary" Text='<%# Eval("txn_date", "{0:dd/MM/yyyy hh:mm tt}") %>'></asp:Label>
                                        <br />
                                        <span class="text-muted" style="font-size: 9pt;">Ref. No.</span><%# Eval("txnrefno") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CSC">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAGENTNAME" runat="server" Text='<%# Eval("val_agent_name") %>'></asp:Label>(<%# Eval("csc_id") %>)<br />
                                        <span class="text-muted" style="font-size: 9pt;">Agent Code </span><%# Eval("val_agentcode") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount(Rs.) ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTXN_AMOUNT" runat="server" Text='<%# Eval("txnamount") %>'></asp:Label>
                                        <i class="fa fa-rupee"></i>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button1" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>


    </div>




</asp:Content>



