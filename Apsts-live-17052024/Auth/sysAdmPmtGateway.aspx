<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="sysAdmPmtGateway.aspx.cs" Inherits="Auth_sysAdmPmtGateway" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .table td, .table th {
            padding: 0.50rem;
        }

        .form-control-label1 {
            font-size: .75rem;
            font-weight: 600;
            color: white;
        }

        .headerCss {
            color: #8898aa;
            border-color: #e9ecef;
            background-color: #f6f9fc;
            text-align: center;
            font-weight: bold;
        }

        .rbl input[type="radio"] {
            margin-left: 10px;
            margin-right: 10px;
        }

        .border-right {
            border-right: 1px solid #050505;
        }

        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }
    </style>
    <script>
        $(document).ready(function () {

            var currDate = new Date().getDate();
            var preDate = new Date(new Date().setDate(currDate - 1));
            var todayDate = new Date(new Date().setDate(currDate));
            var FutureDate = new Date(new Date().setDate(currDate + 30000));

            $('[id*=txtsummarydate]').datepicker({
                endDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
            $('[id*=txtscrolldate]').datepicker({
                endDate: preDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
            $('[id*=txttransdateOrphan]').datepicker({
                endDate: preDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
            $('[id*=txttransdateRefund]').datepicker({
                endDate: preDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
        });


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid" style="padding-top: 10px; padding-bottom: 30px;">
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats">
                    <div class="row">
                        <div class="col-4 border-right">
                            <div class="card-body p-2">
                                <div class="row m-0">
                                    <p class="text-danger"><b>For Payment Refunds please follow the process given below</b></p>
                                </div>
                                <div class="row m-0">
                                    <p class="text-black">1. Double Verification by Visiting <b>Pending for Settlement</b> Section</p>

                                    <p class="text-black">2. Refund Posting by Visiting <b>Pending for Refund</b> Section</p>

                                    <p class="text-black">3. Refund Status by Visiting <b>Pending for Refund Status</b> Section</p>
                                </div>

                            </div>
                        </div>
                        <div class="col-4 border-right">
                            <div class="card-body p-2">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h4 class="mb-1">Cancellation Scroll</h4>
                                        <div class="row mt-2">
                                            <div class="col-lg-6">
                                                <label class="text-muted form-control-label" style="font-size: 14px">Date</label><br />
                                                <div class="input-group">
                                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtscrolldate" MaxLength="10" ToolTip="Enter Discount Starting Date"
                                                        placeholder="DD/MM/YYYY" Text="" Style="display: inline;"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="txtscrolldate" ValidChars="/-" />

                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <label class="text-muted form-control-label" style="font-size: 14px">Payment Gateway</label><br />
                                                <asp:DropDownList ID="ddlpayment" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12 mt-2 text-center">
                                                <asp:LinkButton ID="lbtndownload" OnClick="lbtndownload_Click" ToolTip="Download" runat="server" CssClass="btn btn-success">
                                            <i class="fa fa-download"></i> Download
                                                </asp:LinkButton>
                                            </div>
                                        </div>


                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="col-4">
                            <div class="card-body">
                                <div class="row mr-0">
                                    <div class="col">
                                        <h4 class="mb-1">Pending For Refund Due to Payment Gateway Error Response</h4>
                                        <div class="row mt-2">
                                            <div class="col-lg-12 mt-2 text-center">
                                                <asp:GridView ID="gvPGCounts" runat="server" GridLines="None" AutoGenerateColumns="false" ShowHeader="false" CssClass="w-100"
                                                    DataKeyNames="gateway_id,gatewayname" OnRowCommand="gvPGCounts_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <li class="list-group-item d-flex justify-content-between align-items-center py-2">
                                                                    <%# Eval("gatewayname") %>
                                                                    <asp:LinkButton ID="lbtn" runat="server" CssClass="badge badge-primary badge-pill" OnClientClick="return ShowLoading()" CommandName="COUNTVIEW" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'>
                                                            <%# Eval("record_count") %></asp:LinkButton>
                                                                </li>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
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

        <div class="row my-2">
            <div class="col-md-4 col-lg-4 order-xl-1">
                <div class="card" style="min-height: 100vh">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-md-4">
                                <h4 class="mb-0">Pending For Double Verification</h4>
                            </div>
                            <div class="col-md-3">
                                <label class="text-muted form-control-label mb-0" style="font-size: 14px">Transaction Date</label><br />
                                <div class="input-group">
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txttransdateOrphan" MaxLength="10" ToolTip="Enter Discount Starting Date"
                                        placeholder="DD/MM/YYYY" AutoComplete="off" Text="" Style="display: inline;"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="txttransdateOrphan" ValidChars="/" />

                                </div>
                            </div>
                            <div class="col-md-3">
                                <label class="text-muted form-control-label mb-0" style="font-size: 14px">Payment Gateway</label><br />
                                <asp:DropDownList ID="ddlpmtgatewayorphan" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 mt-4">
                                <asp:LinkButton ID="lbtnsearchOrphan" ToolTip="Search Orphan Transaction" OnClick="lbtnsearchOrphan_Click" runat="server" CssClass="btn btn-warning btn-sm">
                                            <i class="fa fa-search"></i> 
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbtnResetOrphan" ToolTip="Reset Orphan Transaction" OnClick="lbtnResetOrphan_Click" runat="server" CssClass="btn btn-success btn-sm">
                                            <i class="fa fa-undo"></i> 
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">

                        <asp:GridView ID="gvOrphanTrans" runat="server" OnRowCommand="gvOrphanTrans_RowCommand" PageSize="10" AutoGenerateColumns="False" GridLines="None" AllowSorting="true" AllowPaging="true" CssClass="table table-striped table-hover"
                            HeaderStyle-CssClass="thead-light font-weight-bold" OnPageIndexChanging="gvOrphanTrans_PageIndexChanging" DataKeyNames="txndate,gateway,gatewayid">
                            <Columns>
                                <asp:TemplateField HeaderText="Transaction Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltxndate1" runat="server" Text='<%# Eval("txndate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No. Of Transaction">
                                    <ItemTemplate>
                                        <asp:Label ID="lblorphantxn" runat="server" Text='<%# Eval("orptxn") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Gateway">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPMTGATEWAYNAME" runat="server" Text='<%# Eval("gateway") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnSettle" ToolTip="Settle Orphan Transaction" CommandName="Settle" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' runat="server" CssClass="btn btn-sm btn-warning"><i class="fa fa-eye">&nbsp;</i></asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                        </asp:GridView>
                        <div class="text-center busListBox" id="dvOrphanTrans" runat="server"
                            style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;"
                            visible="true">
                            No Orphan Transaction Pending for Settlement 
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-lg-4 col-md-4 order-xl-2">
                <div class="card" style="min-height: 100vh">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-md-4">
                                <h4 class="mb-0">Pending For Refund Posting</h4>
                            </div>
                            <div class="col-md-3">
                                <label class="text-muted form-control-label mb-0" style="font-size: 14px">Cancellation Date</label><br />
                                <div class="input-group">
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txttransdateRefund" MaxLength="10" ToolTip="Enter Discount Starting Date"
                                        placeholder="DD/MM/YYYY" AutoComplete="off" Text="" Style="display: inline;"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="txttransdateRefund" ValidChars="/" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label class="text-muted form-control-label mb-0 " style="font-size: 14px">Payment Gateway</label><br />
                                <asp:DropDownList ID="ddlpmtgatewayrefund" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 mt-4">
                                <asp:LinkButton ID="lbtnsearchRefund" OnClick="lbtnsearchRefund_Click" ToolTip="Search Pending Refund Transaction" runat="server" CssClass="btn btn-warning btn-sm"> <i class="fa fa-search"></i> 
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbtnResetRefund" OnClick="lbtnResetRefund_Click" ToolTip="Reset Pending Refund Transaction" runat="server" CssClass="btn btn-success btn-sm">
                                            <i class="fa fa-undo"></i> 
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">

                        <asp:GridView ID="gvRefundTrans" runat="server" PageSize="10" OnPageIndexChanging="gvRefundTrans_PageIndexChanging" AutoGenerateColumns="False" GridLines="None" AllowSorting="true" AllowPaging="true" CssClass="table table-striped table-hover"
                            HeaderStyle-CssClass="thead-light font-weight-bold" DataKeyNames="txndate,gateway,gatewayid " OnRowCommand="gvRefundTrans_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Transaction Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltxndate2" runat="server" Text='<%# Eval("txndate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No. Of Transaction">
                                    <ItemTemplate>
                                        <asp:Label ID="lblorphantxn" runat="server" Text='<%# Eval("reftxn") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Gateway">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPMTGATEWAYNAME" runat="server" Text='<%# Eval("gateway") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnRefund" ToolTip="Refund Transaction" CommandName="Refund" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' runat="server" CssClass="btn btn-sm btn-warning"><i class="fa fa-eye">&nbsp;</i></asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                        </asp:GridView>
                        <div class="text-center busListBox" id="dvRefundTrans" runat="server"
                            style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;"
                            visible="true">
                            No Cancelled Transaction Pending for Refund 
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4 col-md-4 order-xl-2">
                <div class="card" style="min-height: 100vh">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-md-4">
                                <h4 class="mb-0">Pending For
                                    <br />
                                    Refund Status</h4>
                            </div>
                            <div class="col-md-3">
                                <label class="text-muted form-control-label mb-0" style="font-size: 14px">Refund Date</label><br />
                                <div class="input-group">
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txttransdateRefunded" MaxLength="10" ToolTip="Enter Refund Initiation Date"
                                        placeholder="DD/MM/YYYY" AutoComplete="off" Text="" Style="display: inline;"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="txttransdateRefunded" ValidChars="/" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label class="text-muted form-control-label mb-0 " style="font-size: 14px">Payment Gateway</label><br />
                                <asp:DropDownList ID="ddlpmtgatewayrefunded" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 mt-4">
                                <asp:LinkButton ID="lbtnSearchRefunded" OnClick="lbtnSearchRefunded_Click" ToolTip="Search Pending Refund Status Transaction" runat="server" CssClass="btn btn-warning btn-sm"> <i class="fa fa-search"></i> 
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbtnResetRefunded" OnClick="lbtnResetRefunded_Click" ToolTip="Reset Pending Refund Status Transaction" runat="server" CssClass="btn btn-success btn-sm">
                                            <i class="fa fa-undo"></i> 
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">

                        <asp:GridView ID="gvRefundedTrans" runat="server" PageSize="10" OnPageIndexChanging="gvRefundedTrans_PageIndexChanging" AutoGenerateColumns="False" GridLines="None" AllowSorting="true" AllowPaging="true" CssClass="table table-striped table-hover"
                            HeaderStyle-CssClass="thead-light font-weight-bold" DataKeyNames="txndate,refundtxn,pmtgatewayname,payment_gateway_id " OnRowCommand="gvRefundedTrans_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Refund Initiation Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltxndate3" runat="server" Text='<%# Eval("txndate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No. Of Transaction">
                                    <ItemTemplate>
                                        <asp:Label ID="lblrefundedtxn" runat="server" Text='<%# Eval("refundtxn") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Gateway">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpmtgatewayname" runat="server" Text='<%# Eval("pmtgatewayname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnRefunded" ToolTip="Refunded Transaction" CommandName="RefundStatus" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' runat="server" CssClass="btn btn-sm btn-warning"><i class="fa fa-eye">&nbsp;</i></asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                        </asp:GridView>
                        <div class="text-center busListBox" id="dvRefundedTrans" runat="server"
                            style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;"
                            visible="true">
                            No Refunded Transaction Pending for status        
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

