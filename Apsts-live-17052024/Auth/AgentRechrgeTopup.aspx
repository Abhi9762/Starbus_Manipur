<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/AgentMaster.master" AutoEventWireup="true" CodeFile="AgentRechrgeTopup.aspx.cs" Inherits="Auth_AgentRechrgeTopup" %>

   <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <style>
        .table td, .table th {
            padding: 5px 2px;
            vertical-align: top;
            border-top: 1px solid #eef1f5;
        }

        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }
    </style>

        <script>
            $(document).ready(function () {

                var currDate = new Date().getDate();
                var todayDate = new Date(new Date().setDate(currDate));
                $('[id*=txtDateF]').datepicker({
                    endDate: todayDate,
                    changeMonth: true,
                    changeYear: false,
                    format: "dd/mm/yyyy",
                    autoclose: true
                });
                $('[id*=txtDateT]').datepicker({
                    endDate: todayDate,
                    changeMonth: true,
                    changeYear: false,
                    format: "dd/mm/yyyy",
                    autoclose: true
                });
            });
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="content mt-3" style="width: 100%;">
        <div class="row">
            <div class="col-12 col-xl-4 col-lg-4 col-md-12 col-sm-12">
                <div class="card">
                    <div class="card-body">
                        <h3>
                            <asp:Label ID="lblWalletBalance" runat="server" Text=""></asp:Label>
                            <i class="fa fa-rupee-sign"></i>
                        </h3>
                        <div class="h5 font-weight-300">
                            <asp:Label ID="lblWalletLastUpdate" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="card">
                    <div class="card-header">
                        <h4>Recharge your wallet</h4>
                    </div>
                    <div class="card-body" style="min-height: 300px;">
                        <asp:Label ID="lblmaxlimit" runat="server" Text=""></asp:Label>
                        <hr class="my-4" />
                        <h6 class="heading-small text-muted mb-0 mb-1">1. Enter Topup Amount</h6>
                        <asp:TextBox ID="tbAmount" runat="server" class="form-control" MaxLength="5" placeholder="Enter Amount" Width="50%" AutoComplete="off"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="dd" runat="server" FilterType="Numbers" TargetControlID="tbAmount" />

                        <h6 class="heading-small text-muted mb-0 pt-4">2. Select Payment Gateway</h6>
                        <div class="row mt-3">
                            <div class="col-md-12">
                                <asp:Label ID="lblNoPG" runat="server" Text="Payment Gateways are not available.<br>Please contact to helpdesk." Visible="false" Style="font-size: 23px; color: #919691; font-weight: bold;"></asp:Label>
                            </div>
                            <asp:Repeater ID="rptrPG" runat="server" OnItemCommand="rptrPG_ItemCommand" >
                                <ItemTemplate>
                                    <div class="col-md-6">
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
                    </div>

                </div>

            </div>

            <div class="col-12 col-xl-8 col-lg-8 col-md-12 col-sm-12">
                <div class="row">
                    <div class="col-12 col-xl-12 col-lg-12 col-md-12 col-sm-12">
                        <div class="card">
                            <div class="card-header">
                                <div class="row">
                                    <div class="col">
                                        <h4>Agent Passbook<br />
                                            <span style="font-size: 10pt;">(Details Available 30 days at a time)</span>
                                        </h4>
                                    </div>
                                    <div class="col-auto">
                                        <div class="row">
                                            <div class="col-lg-5">
                                                <div class="form-group">
                                                    <label class="d-block mb-0">From Date</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtDateF" runat="server" AutoComplete="Off" CssClass="form-control px-1"
                                                            Placeholder="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                                                     
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom"
                                                            TargetControlID="txtDateF" ValidChars="/" />
                                                    </div>
                                                   
                                                </div>
                                            </div>
                                            <div class="col-lg-5">
                                                <div class="form-group">
                                                    <label class="d-block mb-0">To Date</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtDateT" runat="server" AutoComplete="Off" CssClass="form-control px-1"
                                                            Placeholder="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                                                     
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom"
                                                ValidChars="/" TargetControlID="txtDateT" />
                                                    </div>
                                                    
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <label class="d-block mb-0">&nbsp;</label>
                                                <asp:LinkButton ID="lbtnshow" runat="server" OnClick="lbtnshow_Click" Text="Show" CssClass="btn btn-danger btn-sm"></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body">
                                <asp:GridView ID="grdagpassbook" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    AllowSorting="true" AllowPaging="true" PageSize="10"
                                    class="table" DataKeyNames="trans_date,opening_balance,transaction_amount,deposit_amount,closing_balance"
                                     OnRowCommand="grdagpassbook_RowCommand" OnPageIndexChanging="grdagpassbook_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblagtransdate" runat="server" Text='<%# Eval("trans_date", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening (₹)" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblagOpenBlnc" runat="server" Text='<%# Eval("opening_balance") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transaction (₹)" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblagtransamt" runat="server" Text='<%# Eval("transaction_amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Deposit (₹)" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblagdepositamt" runat="server" Text='<%# Eval("deposit_amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Closing (₹)" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblagCloseBlnc" runat="server" Text='<%# Eval("closing_balance") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-CssClass="text-right">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnview" runat="server" Text="View" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    CssClass="btn btn-success btn-sm"><i class="fa fa-eye"></i> </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                </asp:GridView>
                                <asp:Panel ID="pnlNoPassbook" runat="server">
                                    <div class="row py-4">
                                        <div class="col-lg-12 text-center mt-4">
                                            <i class="fa fa-file-o fa-4x text-muted"></i>
                                        </div>
                                        <div class="col-lg-12 text-center mt-4">
                                            <span class="h2 font-weight-bold  text-muted mb-0">No Transaction</span>
                                            <h5 class="card-title text-uppercase text-muted mb-0">Details are not available for selected dates
                                            </h5>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 col-xl-6 col-lg-6 col-md-6 col-sm-12">
                        <div class="card">
                            <div class="card-header">
                                <h4>Wallet Transactions <span style="font-size: 9pt;">(30 Days)</span><br />
                                    <span style="font-size: 10pt;">(Recharge/Booking/Refund)</span>
                                </h4>
                            </div>
                            <div class="card-body p-3" style="min-height: 300px;">
                                <asp:GridView ID="gvTransactions" CssClass="table" runat="server" GridLines="None" ShowHeader="false"
                                    AllowPaging="true" PageSize="8"  AutoGenerateColumns="false" Width="100%" OnPageIndexChanging="gvTransactions_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField>
                                            <%--<HeaderTemplate>
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <h5 class="mb-0 text-dark">Refrence No.</h5>
                                            </div>
                                            <div class="col-lg-3 pr-0">
                                                <h5 class="mb-0 text-dark">Type</h5>
                                            </div>
                                            <div class="col-lg-3 text-right">
                                                <h5 class="mb-0 text-dark">Amount</h5>
                                            </div>
                                        </div>
                                    </HeaderTemplate>--%>
                                            <ItemTemplate>
                                                <div class="row">
                                                    <div class="col-auto pr-0 text-left">
                                                        <p class="mb-0 font-weight-bold"><%# Eval("txn_type")%></p>
                                                    </div>
                                                    <div class="col-auto text-left">
                                                       <%-- <p class="mb-0 font-weight-bold" style='<%# If(Eval("WALLET_TXN_TYPE_CODE").ToString() = "B", "color:Red;", "color:#0ed10e;") %>'>
                                                            <%# Eval("TXN_AMOUNT") %> <i class="fa fa-rupee"></i>
                                                        </p>--%>
                                                    </div>
                                                    <div class="col text-right">
                                                        <p class="mb-0 font-weight-light"><%# Eval("txnrefno") %> (<%# Eval("txn_date") %>)</p>
                                                    </div>

                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="GridPager" ForeColor="Black" />
                                </asp:GridView>
                                <asp:Panel ID="pnlNoTransaction" runat="server">
                                    <div class="row py-4">
                                        <div class="col-lg-12 text-center mt-4">
                                            <i class="ni ni-bus-front-12 ni-5x text-muted"></i>
                                        </div>
                                        <div class="col-lg-12 text-center mt-4">
                                            <span class="h2 font-weight-bold  text-muted mb-0">Thanks for being here</span>
                                            <h5 class="card-title text-uppercase text-muted mb-0">
                                                <asp:Label ID="lblNoTransactionMsg" runat="server"></asp:Label>
                                            </h5>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-xl-6 col-lg-6 col-md-6 col-sm-12">
                        <div class="card">
                            <div class="card-header">
                                <h4>Top up Transactions<br />
                                    <span style="font-size: 10pt;">(Recharge Wallet)</span>
                                </h4>
                            </div>
                            <div class="card-body p-3" style="min-height: 300px;">
                                <asp:GridView ID="gvtopuptransaction" CssClass="table table-sm" runat="server" GridLines="None" ShowHeader="false"
                                    AllowPaging="true" PageSize="8" AutoGenerateColumns="false" Width="100%" OnPageIndexChanging="gvtopuptransaction_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="row">
                                                    <div class="col text-left">
                                                        <p class="mb-0 font-weight-light"><%# Eval("txnrefno") %></p>
                                                        <p class="mb-0 font-weight-light" style="font-size:13px;"><%# Eval("txnstartdate") %></p>
                                                    </div>
                                                    <div class="col pr-0">
                                                        <p class="mb-0 font-weight-light"><%# Eval("txnamount") %> <i class="fa fa-rupee"></i></p>                                                        
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
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="GridPager" ForeColor="Black" />
                                </asp:GridView>
                                <asp:Panel ID="pnltopuptransaction" runat="server">
                                    <div class="row py-4">
                                        <div class="col-lg-12 text-center mt-4">
                                            <i class="ni ni-bus-front-12 ni-5x text-muted"></i>
                                        </div>
                                        <div class="col-lg-12 text-center mt-4">
                                            <span class="h2 font-weight-bold  text-muted mb-0">No Transaction</span>
                                            <h5 class="card-title text-uppercase text-muted mb-0">We are waiting for your first transaction
                                            </h5>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>


                </div>
            </div>


        </div>
        <div class="row">
        </div>
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
                            <asp:HiddenField ID="hdpgid" runat="server" />
                             <asp:HiddenField ID="hdpgurl" runat="server" />
                            <asp:Label ID="lblmsg" runat="server"></asp:Label>
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
</asp:Content>


