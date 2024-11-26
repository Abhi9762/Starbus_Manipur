<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Cntrmaster.master" AutoEventWireup="true" CodeFile="CntrCashregister.aspx.cs" Inherits="Auth_CntrCashregister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidtoken" runat="server" />
    <div class="container-fluid pt-2">
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="row">
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h4 class="mb-1">Amount Summary</h4>
                                        <div class="row m-0">
                                            <div class="col-6 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0"><asp:Label runat="server" ID="lblmandatry"></asp:Label> &nbsp;
                                 <asp:Label ID="lblmandatoryamt" runat="server" data-toggle="tooltip" data-placement="bottom" title="Mandatory Amount Pending for Deposit" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0"><asp:Label runat="server" ID="lblpending"></asp:Label>&nbsp;
                                 <asp:Label ID="lbltotpendingamt" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Pending Amount To Deposit" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-8 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h4 class="mb-1">
                                            <asp:Label ID="lbltodaysummary" runat="server"></asp:Label>
                                        </h4>
                                        <div class="row m-0">
                                            <div class="col-3 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h6 class="mb-1">Online Booking</h6>
                                                        <div class="row m-0">
                                                            <div class="col-12">
                                                                <h5 class="card-title text-uppercase text-muted mb-0">Amount &nbsp;<br />
                                                                    <asp:Label ID="lbltodaybookingamt" runat="server" data-toggle="tooltip" data-placement="bottom" title="total online ticket amount" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-12">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-3 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h6 class="mb-1">Current Booking</h6>
                                                        <div class="row m-0">
                                                            <div class="col-12">
                                                                <h5 class="card-title text-uppercase text-muted mb-0">Amount &nbsp;<br />
                                                                    <asp:Label ID="lblcurrtripamt" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Current Booking Trips" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-12">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-3 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h6 class="mb-1">Bus Passes</h6>
                                                        <div class="row m-0">
                                                            <div class="col-12">
                                                                <h5 class="card-title text-uppercase text-muted mb-0">Coming     Soon &nbsp;<br />
                                                                    <asp:Label ID="lbltodaypassamt" runat="server" Visible="false"  data-toggle="tooltip" data-placement="bottom" title="Total Amount New Bus Passes Generated" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text=""></asp:Label></h5>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-12">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h6 class="mb-1">Cancellation</h6>
                                                        <div class="row m-0">
                                                            <div class="col-12">
                                                                <h5 class="card-title text-uppercase text-muted mb-0">Amount &nbsp;<br />
                                                                    <asp:Label ID="lbltodayCancelamt" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Amount New Bus Passes Generated" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-12">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xl-12 order-xl-2">
                <div class="card" style="min-height: 490px">
                    <div class="card-header">
                        <div class="row align-items-center">
                            <div class="col-4">
                                <h3 class="mb-0">Cash Register</h3>
                                <span style="font-size: 10pt;">(Counter all transaction - online/current booking,cancellation and bus passes)</span>
                            </div>
                            <div class="col-md-8">
                                <div class="row pl-lg-3 pr-lg-3">
                                    <div class="col-lg-6"></div>
                                    <div class="col-lg-2">
                                        <span class="form-control-label">Year <span class="text-danger">*</span></span>
                                        <asp:DropDownList ID="ddlyear" OnSelectedIndexChanged="ddlyear_SelectedIndexChanged" CssClass="form-control form-control-sm" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-2">
                                        <span class="form-control-label">Month <span class="text-danger">*</span></span>
                                        <asp:DropDownList ID="ddlmonth" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlmonth_SelectedIndexChanged" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                   
                                    <div class="col-lg-2 pt-4">
                                        <asp:LinkButton ID="lbtnSearch" runat="server" data-toggle="tooltip" OnClick="lbtnSearch_Click" data-placement="top" ToolTip="Click here to search cash ragister" CssClass="btn btn-success btn-icon-only btn-sm" strle="z-i">
                                <i class="fa fa-search mt-2"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnReset" runat="server" data-toggle="tooltip" OnClick="lbtnReset_Click" data-placement="top" ToolTip="Click here to reset search values" CssClass="btn btn-danger btn-icon-only btn-sm">
                                            <i class="fa fa-undo mt-2"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                        <div class="col-12">
                            <asp:GridView ID="grdcashregister" runat="server" AutoGenerateColumns="False" GridLines="None" Font-Size="10pt"
                                AllowPaging="true" PageSize="31" class="table table-hover table-striped" OnRowCommand="grdcashregister_RowCommand"
                                DataKeyNames="date_,closing_amt">
                                <Columns>
                                    <asp:TemplateField HeaderText="Transaction<br/>Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblACCTDATE" Text='<%# Eval("date_") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening Balance<br/>(A)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOpening_Balance" runat="server" Text='<%# Eval("open_balance") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Booking Amount<br/>(B)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBooking_Amount" runat="server" Text='<%# Eval("booking_amt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Passes Amount<br/>(C)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPasses_Amount" runat="server" Text='<%# Eval("pass_amt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cancellation Amount<br/>(D)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCancellation_Amount" runat="server" Text='<%# Eval("cancel_amt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Deposit Amount<br/>(E)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeposit_Amount" runat="server" Text='<%# Eval("deposit_amt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing Balance<br/>(A+B+C)-(D+E)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblClosing_Balance" runat="server" Text='<%# Eval("closing_amt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn btn-success btn-sm" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="View">
                                            <i class="fa fa-search"></i> View
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Panel ID="pnlNocashregister" runat="server">
                                <div class="row">
                                    <div class="col-lg-12 text-center mt-5">
                                        <h1 class="card-title text-muted mt-5">
                                            <asp:Label ID="lblno" runat="server" Text="No Account Transaction Details Available"></asp:Label>
                                        </h1>
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
            <cc1:ModalPopupExtender ID="mpcashRegisterdtls" runat="server" CancelControlID="LinkButton1"
                TargetControlID="Button2" PopupControlID="pnlcashRegisterdtls" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlcashRegisterdtls" Style="display: none" runat="server">
                <div class="card">
                    <div class="card-header">
                         <div class="row">
                             <div class="col-8">
                                   <h4 class="card-title m-0" style="font-size: 20px;">
                            <asp:Label ID="lblcashregisterdtls" runat="server" Text=""></asp:Label>
                        </h4>
                             </div>
                              <div class="col-4 text-right">
                                  <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-danger btn-sm font-weight-bold px-3" Style="border-radius: 4px;"> <i class="fa fa-times"></i> </asp:LinkButton>
                                  </div>
                             </div>
                    </div>
                    <div class="card-body">
                        <div class="col-lg-12 text-center">
                            <asp:GridView ID="gvcashregisterdtls" runat="server" AutoGenerateColumns="False" GridLines="None" Font-Size="10pt"
                                AllowPaging="true" PageSize="10" class="table table-hover table-striped" OnPageIndexChanging="gvcashregisterdtls_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="Transaction Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransType" Text='<%# Eval("transaction_type") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Ref. Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRefNumber" Text='<%# Eval("refno") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("totalfare") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="card-footer text-danger" style="text-align: center;font-size: 25px;font-weight: bold;">
                        <asp:Label ID="lblclosingbalce" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div style="visibility: hidden; height: 0px;">
                    <asp:Button ID="Button2" runat="server" />
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

