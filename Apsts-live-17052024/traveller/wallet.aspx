<%@ Page Title="" Language="C#" MasterPageFile="~/traveller/trvlrMaster.master" AutoEventWireup="true" CodeFile="wallet.aspx.cs" Inherits="traveller_wallet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <asp:HiddenField ID="hidtoken" runat="server" />
    <div class="container-fluid mt-3">
        <div class="row">
            <div class="col-xl-4 order-xl-1">
                <div class="card card-profile">
                    <div class="card-body" style="min-height: 70vh !important;">
                        <div class="text-center mt-4">
                            <h5 class="h2 font-weight-light">Wallet Balance <span class="font-weight-600">
                                <asp:Label ID="lblWalletBalance" runat="server" Text="09-02-2022"></asp:Label>
                                <i class="fa fa-rupee-sign"></i></span>
                            </h5>
                            <div class="h5 font-weight-300">
                                Last wallet update
                                <asp:Label ID="lblWalletLastUpdate" runat="server" Text="09-02-2022"></asp:Label>
                            </div>
                        </div>
                        <hr class="my-4" />
                        <h6 class="heading-small text-muted mb-2">Topup your wallet</h6>
                        <h3 class="heading-small text-muted">Enter amount and Select any one payment gateway for recharge your wallet/ Click proceed to pay.</h3>

                        <div class="row">
                            <div class="col-12">
                                <asp:TextBox ID="tbAmount" runat="server" class="form-control w-50" MaxLength="5" placeholder="₹ Enter Amount" AutoComplete="off"></asp:TextBox>

                            </div>
                        </div>

                        <div class="row mt-2">
                            <div class="col-md-12">
                                <asp:Label ID="lblNoPG" runat="server" Text="Payment Gateways are not available.<br>Please contact to helpdesk." Visible="false" Style="font-size: 23px; color: #919691; font-weight: bold;"></asp:Label>
                            </div>
                            <asp:Repeater ID="rptrPG" runat="server" OnItemCommand="rptrPG_ItemCommand">
                                <ItemTemplate>
                                    <div class="col-md-6" style="border: 1px solid #e6e6e6; padding: 9px;">
                                        <asp:HiddenField ID="rptHdPGId" runat="server" Value='<%# Eval("gateway_id") %>' />
                                        <asp:HiddenField ID="hd_pgurl" runat="server" Value='<%# Eval("req_url") %>' />
                                        <img src="../Dbimg/PG/<%# Eval("gateway_name")%>_W.png" style="height: 30px; width: 50%;" />
                                        <h3 class="mb-0">
                                            <%# Eval("gateway_name")%>
                                        </h3>
                                        <p>
                                            <%# Eval("description")%>
                                        </p>
                                        <asp:Button ID="btnproceed" runat="server" Text="Proceed to Pay" CssClass="btn mb-2"
                                            Style="background-color: #00baf2; color: White; font-weight: bold;" CommandName="PAYNOW" />
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="card-footer p-3">
                        <span>Wallet Opening Date
                            <asp:Label ID="lblWalletOpenDate" runat="server" Text="05-02-2022"></asp:Label>
                        </span>
                    </div>
                </div>
            </div>
            <div class="col-xl-4 order-xl-2">
                <div class="card">
                    <div class="card-header">
                        <div class="row align-items-center">
                            <div class="col-12 input-group-prepend">
                                <h3 class="mb-0">Wallet Transactions </h3>
                                <asp:Label Font-Size="Smaller" runat="server" CssClass="ml-2 mt-1" Text="(Last 30 Days)"></asp:Label>
                            </div>
                            <div class="col-12">
                                <asp:Label Font-Size="Smaller" runat="server" CssClass=" text-danger " Text="(Recharge/Booking/Refund)"></asp:Label>
                            </div>

                            <%--  <div class="col-4 text-right">
                                <a href="#!" class="btn btn-sm btn-primary">View Transaction History </a>
                            </div>--%>
                        </div>
                    </div>
                    <div class="card-body" style="min-height: 70vh !important;">
                        <asp:GridView ID="gvTransactions" AllowPaging="true" PageSize="8" OnPageIndexChanging="gvTransactions_PageIndexChanging" CssClass="table" runat="server" GridLines="None" AutoGenerateColumns="false" Width="100%">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <div class="row">
                                            <div class="col-lg-7">
                                                <h5 class="mb-0">Refrence No.</h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5 class="mb-0">Type</h5>
                                            </div>
                                            <div class="col-lg-2 text-right">
                                                <h5 class="mb-0">Amount</h5>
                                            </div>

                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="row">
                                            <div class="col-lg-7">
                                                <h5 class="mb-0 font-weight-light"><%# Eval("wlt_trns_ref") %></h5>

                                                <h5 class="mb-0 font-weight-light"><%# Eval("trnsdate") %></h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5 class="mb-0 font-weight-light"><%# Eval("trns_type") %></h5>
                                            </div>
                                            <div class="col-lg-2 text-right">
                                                <h5 class="mb-0 font-weight-light font-weight-bold" style="color:#0ed10e"><%# Eval("trns_amount") %><i class="fa fa-rupee-sign ml-1"></i></h5>
                                            </div>

                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                        <asp:Panel ID="pnlNoTransaction" runat="server">
                            <div class="row py-4">
                                <div class="col-lg-12 text-center mt-4">
                                    <i class="ni ni-bus-front-12 ni-5x text-muted"></i>
                                </div>
                                <div class="col-lg-12 text-center mt-4">
                                    <span class="h2 font-weight-bold mb-0">Thanks for being here</span>
                                    <h5 class="card-title text-uppercase text-muted mb-0">
                                        <asp:Label ID="lblNoTransactionMsg" runat="server"></asp:Label>
                                    </h5>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <div class="col-xl-4 order-xl-2">
                <div class="card">
                    <div class="card-header">
                        <div class="row align-items-center">
                            <div class="col-12 input-group-prepend">
                                <h3 class="mb-0">Top up Transactions </h3>
                            </div>
                            <div class="col-12">
                                <asp:Label Font-Size="Smaller" runat="server" CssClass=" text-danger " Text="(Recharge Wallet)"></asp:Label>
                            </div>

                            <%--  <div class="col-4 text-right">
                                <a href="#!" class="btn btn-sm btn-primary">View Transaction History </a>
                            </div>--%>
                        </div>
                    </div>
                    <div class="card-body" style="min-height: 70vh !important;">
                        <asp:GridView ID="grdTopupTransaction" AllowPaging="true" PageSize="8" OnPageIndexChanging="grdTopupTransaction_PageIndexChanging" CssClass="table" runat="server" GridLines="None" AutoGenerateColumns="false" Width="100%">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <div class="row">
                                            <div class="col-lg-7">
                                                <h5 class="mb-0">Refrence No.</h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5 class="mb-0">Amount</h5>
                                            </div>
                                            <div class="col-lg-2 text-right">
                                                <h5 class="mb-0">Status</h5>
                                            </div>

                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="row">
                                            <div class="col-lg-7">
                                                <h5 class="mb-0 font-weight-light"><%# Eval("txnref") %></h5>

                                                <h5 class="mb-0 font-weight-light"><%# Eval("txnstdate") %></h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5 class="mb-0 font-weight-light"><%# Eval("amt") %><span class="fa fa-rupee-sign ml-1"></span></h5>
                                                <h5 class="mb-0 font-weight-light">(<%# Eval("gateway") %>)</h5>
                                            </div>
                                            <div class="col-lg-2 text-right">
                                                <h5 class="mb-0 font-weight-light font-weight-bold" 
                                                    style='<%# Eval("complete").ToString() == "N"? "color:Red;": "color:#0ed10e;" %>'
                                                    ><%# Eval("stat") %></h5>
                                            </div>

                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                        <asp:Panel ID="pnltopup" runat="server">
                            <div class="row py-4">
                                <div class="col-lg-12 text-center mt-4">
                                    <i class="ni ni-bus-front-12 ni-5x text-muted"></i>
                                </div>
                                <div class="col-lg-12 text-center mt-4">
                                    <span class="h2 font-weight-bold mb-0">Thanks for being here</span>
                                    <h5 class="card-title text-uppercase text-muted mb-0">
                                        <asp:Label ID="lbltopup" runat="server"></asp:Label>
                                    </h5>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <!-- Modal -->
            <cc1:ModalPopupExtender ID="mpconfirmation" runat="server" PopupControlID="pnlconfirmation"
                TargetControlID="Button1345" CancelControlID="lbtnno"
                BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlconfirmation" runat="server" Style="position: fixed; display: none;">
                <div class="modal-dialog">
                    <div class="modal-content " style="min-width: 400px;">
                        <div class="modal-header">
                            <div class="col-md-10">
                                <h3 class="m-0">
                                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Please Confirm"></asp:Label>
                                </h3>
                            </div>
                        </div>
                        <div class="modal-body">
                            <p class="full-width-separator mb-2" style="font-size: 17px; line-height: 24px;">
                                <asp:HiddenField ID="hdpgid" runat="server" />
                                <asp:HiddenField ID="hdpgurl" runat="server" />
                                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                            </p>

                        </div>
                        <div class="modal-footer text-right">
                            <div class="row w-100">
                                <div class="col-lg-6 mt-1">
                                    <h4 class="text-danger">Do you want to proceed ?</h4>
                                </div>
                                <div class="col-lg-4 text-right">
                                    <asp:LinkButton ID="lbtnyes" OnClick="lbtnyes_Click" runat="server" UseSubmitBehavior="false" CssClass="btn btn-success btn-sm"
                                        Style="cursor: pointer; font-weight: bold; font-size: 10pt;"><i class="fa fa-check"></i> Yes</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnno" runat="server" UseSubmitBehavior="false" CssClass="btn btn-danger btn-sm"
                                        Style="cursor: pointer; font-weight: bold; font-size: 10pt;"><i class="fa fa-times"></i> No</asp:LinkButton>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button1345" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

    </div>
</asp:Content>

