<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/CscMasterPage.master" AutoEventWireup="true" CodeFile="MainCscBookingByAgent.aspx.cs" Inherits="Auth_MainCscBookingByAgent" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


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
                <div class="card mt-2">
                    <div class="card-header">
                        <div class="row ">
                            <div class="col-4">
                                <strong>Sub CSC Transaction Details</strong><br />
                                <asp:Label ID="Label3" runat="server" ForeColor="Green" Font-Size="small" Font-Bold="false" Text="(Details will be Available Booking/Cancellation/Bus Passes/Current Booking)"></asp:Label>
                            </div>

                            <div class="col-2">
                                <asp:Label ID="Label1" runat="server" Font-Size="small" Font-Bold="false" Text="Sub CSC"></asp:Label><br />
                                <asp:DropDownList ID="ddlagent" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-2">
                                <asp:Label ID="Label2" runat="server" Font-Size="small" Font-Bold="false" Text="From Date"></asp:Label><br />
                                <asp:TextBox ID="txtDateF" placeholder="DD/MM/YYYY" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                
                            </div>
                            <div class="col-2">
                                <asp:Label ID="Label4" runat="server" Font-Size="small" Font-Bold="false" Text="To Date"></asp:Label><br />
                                <asp:TextBox ID="txtDateT" placeholder="DD/MM/YYYY" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                
                            </div>
                            <div class="col-2" style="padding: 0px; padding-top: 20px;">
                                <asp:LinkButton ID="lbtnSearch" OnClick="lbtnSearch_Click" OnClientClick="return ShowLoading();" ToolTip="Click here for Search" Style="padding: 7px;" runat="server" CssClass="btn btn-warning btn-sm">
                                                                    <i class="fa fa-search" ></i></asp:LinkButton>
                               
                            </div>
                        </div>

                    </div>
                    <div class="card-body" style="min-height: 80vh;">
                        <asp:Panel ID="pnlreport" runat="server" Visible ="false" >
                        <div class="row mb-2">
                             <div class="col-lg-4">
                                    <span><b>Summary | </b>
                                        <asp:Label ID="lblsmry" runat="server" Text=""></asp:Label>
                                    </span>
                                </div>
                            <div class="col-lg-8 text-right">
                                 <asp:LinkButton ID="lbtndownload" OnClick="lbtndownload_Click" ToolTip="Click here for Download Pdf" Visible="false" Style="padding: 7px;" runat="server" CssClass="btn btn-danger btn-sm">
                                                                     <i class="fa fa-file-pdf" ></i> PDF</asp:LinkButton>
                                
                                 <asp:LinkButton ID="lbtnexcel" runat="server" OnClick="lbtnexcel_Click" Visible="false" ToolTip="Click here for Download Excel" CssClass="btn  btn-warning btn-sm"><i class="fa fa-file-excel" style="padding: 7px;"></i>EXCEL</asp:LinkButton>
                                </div>
                            </div>
                       <asp:GridView ID="grdtransactionDetails" runat="server" AllowPaging="true" PageSize="10" AutoGenerateColumns="False" ForeColor="#333333" Font-Size="14px"
                            OnRowDataBound="grdtransactionDetails_RowDataBound"
                            OnPageIndexChanging="grdtransactionDetails_PageIndexChanging" OnRowCommand="grdtransactionDetails_RowCommand"
                            CssClass="table table-responsive" GridLines="None" Font-Bold="false" DataKeyNames="transaction_number_,wallet_txn_type_code_" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="CSC Name">
                                    <ItemTemplate>
                                          <asp:Label runat="server" ID="lblcscname" Text='<%# Eval("csc_name_") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                   <asp:TemplateField HeaderText="CSC ID">
                                    <ItemTemplate>
                                          <asp:Label runat="server" ID="lblAGENT_CODE" Text='<%# Eval("cscid_") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="State">
                                    <ItemTemplate>
                                          <asp:Label runat="server" ID="lblstate" Text='<%# Eval("state_name_") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                            </asp:Panel>
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
    <!-- Top up Details -->
    <div class="row">
        <cc1:ModalPopupExtender ID="mpagentwallet" runat="server" PopupControlID="pnlagentwallet" TargetControlID="Button1"
            CancelControlID="LinkButton1" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlagentwallet" runat="server" Style="position: fixed;">
            <div class="modal-content mt-5">
                <div class="modal-header">
                    <div class="col-md-10">
                        <h3 class="m-0">Agent Wallets </h3>
                    </div>
                    <div class="col-md-2 text-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                    </div>
                </div>
                <div class="modal-body p-0">
                    <embed src="dashAgentwallet.aspx" style="height: 66vh; width: 52vw;" />
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="Button1" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>


</asp:Content>



