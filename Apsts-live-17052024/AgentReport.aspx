<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/AgentMaster.master" AutoEventWireup="true" CodeFile="AgentReport.aspx.cs" Inherits="Auth_AgentReport" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <script type="text/javascript">
        $(window).on('load', function () {
            HideLoading();
        });
        function ShowLoading() {
            var div = document.getElementById("loader");
            div.style.display = "block";
        }
        function HideLoading() {
            var div = document.getElementById("loader");
            div.style.display = "none";
        }
    </script>
    <style>
        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .pt-4, .py-4 {
            padding-top: 1.8rem !important;
        }

        label {
            font-size: 9pt;
        }

        .table {
            width: 100%;
        }

            .table th, .table td {
                padding: .5rem 0.75rem;
                vertical-align: top;
                border-top: 1px solid #dce1e3;
                font-size: 13px;
            }
    </style>

    <script>
        $(document).ready(function () {

            var currDate = new Date().getDate();
            var todayDate = new Date(new Date().setDate(currDate));
            $('[id*=txtfromdate]').datepicker({
                endDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
            $('[id*=txttodate]').datepicker({
                endDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
            $('[id*=txttransdate]').datepicker({
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
  
    <div class="content mt-3">
        <div class="animated fadeIn">
            <div class="row mt--2">
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-header pb-0">
                            <div class="card-title">
                                <h6>Account Summary </h6>
                            </div>
                        </div>
                        <div class="card-body" style="padding: 5px 10px; min-height: 70vh;">
                            <div class="row">
                                <div class="col-lg-4">
                                    <label>From Date</label>
                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control " MaxLength="10" Style="text-transform: uppercase"
                                        placeholder="dd/MM/yyyy"></asp:TextBox>
                                   <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom"
                                                            TargetControlID="txtfromdate" ValidChars="/" />
                                </div>
                                <div class="col-lg-4">
                                    <label>To Date</label>
                                    <asp:TextBox ID="txttodate" runat="server" CssClass="form-control " MaxLength="10" Style="text-transform: uppercase"
                                        placeholder="dd/MM/yyyy"></asp:TextBox>
                                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom"
                                                            TargetControlID="txttodate" ValidChars="/" />
                                </div>
                                <div class="col-lg-2 mt-4">
                                    <asp:LinkButton class="btn btn-success p-2" ID="lbtnAccountSummary" OnClick="lbtnAccountSummary_Click" runat="server" OnClientClick="return ShowLoading();" Style="border-radius: 4px;"
                                        ToolTip="Show Acount Summary">
                                                                         <i class="fa fa-search" title="Show Acount Summary"></i> 
                                    </asp:LinkButton>
                                    <asp:LinkButton class="btn btn-danger p-2" ID="lbtnresetAccountSummary" OnClick="lbtnresetAccountSummary_Click" OnClientClick="return ShowLoading();" runat="server" Style="border-radius: 4px;"
                                        ToolTip="Reset Acount Summary">
                                                                         <i class="fa fa-undo" title="Reset Acount Summary"></i> 
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <hr />
                            <asp:Panel runat="server" ID="pnlAccontsmryMsg" Visible="True">
                                <div class="row">
                                    <div class="col-12 mt-5">
                                        <center>
                                            <asp:Label runat="server" Text="To generate report Click on Search Button." Font-Bold="true" Font-Size="20px" ForeColor="LightGray"></asp:Label>

                                        </center>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:GridView ID="gvAccountsmryreport" runat="server" GridLines="None" CssClass="table table-responsive" AllowPaging="true"
                                PageSize="15" AutoGenerateColumns="false" DataKeyNames="">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Date">
                                        <ItemTemplate>
                                            <%# Eval("trans_date", "{0:dd/MM/yyyy}") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening Balance (A)">
                                        <ItemTemplate>
                                            <%#Eval("opening_balance") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Top Up Amount (B)">
                                        <ItemTemplate>
                                            <%#Eval("deposit_amount") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cancellation Amount (C)">
                                        <ItemTemplate>
                                            <%#Eval("dbticket_amt") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Amount (D)">
                                        <ItemTemplate>
                                            <%#Eval("transaction_amount") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Commission Amount">
                                        <ItemTemplate>
                                            <%#Eval("comm_amt") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing Balance (A+B+C)-D">
                                        <ItemTemplate>
                                            <%#Eval("closing_balance") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <PagerStyle CssClass="pagination-ys" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-header pb-0">
                            <div class="card-title">
                                <h6>Booking,Cancellation And Bus Passes Summary </h6>
                            </div>
                        </div>
                        <div class="card-body" style="padding: 5px 10px; min-height: 70vh;">
                            <div class="row">
                                <div class="col-lg-4">
                                    <label>Transaction Date</label>
                                    <asp:TextBox ID="txttransdate" runat="server" CssClass="form-control " MaxLength="10" Style="text-transform: uppercase"
                                        placeholder="dd/MM/yyyy"></asp:TextBox>
                                                                       <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, Custom"
                                                            TargetControlID="txttransdate" ValidChars="/" />
                                </div>

                                <div class="col-lg-2 mt-4">
                                    <asp:LinkButton class="btn btn-success p-2" ID="lbtnTransaction" OnClick="lbtnTransaction_Click" OnClientClick="return ShowLoading();" runat="server" Style="border-radius: 4px;"
                                        ToolTip="Search Booking,Cancellation And Bus Passes Summary">
                                                                         <i class="fa fa-search" title="Search Booking,Cancellation And Bus Passes Summary"></i> 
                                    </asp:LinkButton>
                                    <asp:LinkButton class="btn btn-danger p-2" ID="lbtnresetTransaction" OnClick="lbtnresetTransaction_Click" OnClientClick="return ShowLoading();" runat="server" Style="border-radius: 4px;"
                                        ToolTip="Reset Booking,Cancellation And Bus Passes Summary">
                                                                         <i class="fa fa-undo" title="Reset Booking,Cancellation And Bus Passes Summary"></i> 
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <hr />
                            <asp:Panel runat="server" ID="pnlTransactionmsg" Visible="True">
                                <div class="row">
                                    <div class="col-12 mt-5">
                                        <center>
                                            <asp:Label runat="server" Text="To generate report Click on Search Button." Font-Bold="true" Font-Size="20px" ForeColor="LightGray"></asp:Label>

                                        </center>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:GridView ID="gvTransactionreport" runat="server" GridLines="None" CssClass="table table-responsive" AllowPaging="true"
                                PageSize="15" AutoGenerateColumns="false" DataKeyNames="" OnPageIndexChanging="gvTransactionreport_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Date">
                                        <ItemTemplate>
                                            <%# Eval("val_Actiondate", "{0:dd/MM/yyyy}") %>                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action Type">
                                        <ItemTemplate>
                                            <%#Eval("val_action") %>                                          
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ref. No">
                                        <ItemTemplate>
                                           <%#Eval("val_TransactionNo") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Amount">
                                        <ItemTemplate>
                                            <%#Eval("val_TOTALAMT") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Commission Amount">
                                        <ItemTemplate>
                                            <%#Eval("val_COMMAMT") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Refund Amount">
                                        <ItemTemplate>
                                            <%#Eval("val_REFUNDAMOUNT") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="pagination-ys" />
                            </asp:GridView>
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
    </div>

</asp:Content>



