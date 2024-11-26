<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/AgentMaster.master" AutoEventWireup="true" CodeFile="agBookingAndCancellationSummary.aspx.cs" Inherits="Auth_agBookingAndCancellationSummary_aspx" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <style>
        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .border-right {
            border-right: 1px solid #e6e6e6;
        }

        .btn-QuickLinks {
            color: #1d4ab1;
            background-color: transparent;
            background-image: none;
            border-width: 2px;
            border-color: #236bb3;
            font-weight: 600 !important;
            border-radius: 5px !important;
            font-size: 14px;
        }
    </style>
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

        <script>
            $(document).ready(function () {

                var currDate = new Date().getDate();
                var todayDate = new Date(new Date().setDate(currDate));
                $('[id*=tbfromdate]').datepicker({
                    endDate: todayDate,
                    changeMonth: true,
                    changeYear: false,
                    format: "dd/mm/yyyy",
                    autoclose: true
                });
                $('[id*=tbtodate]').datepicker({
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
    <asp:HiddenField ID="hdmaxdate" runat="server" />
    
    <div class="content mt-3">
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="card-header">
                        <div class="row px-2">
                            <div class="col-lg-6">
                                <h4 class="mb-0">Agent Daily Account Summary</h4>
                            </div>
                            <div class="col-lg-2">
                                <span class="form-control-label">From Date</span>
                                <asp:TextBox ID="tbfromdate" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="DD/MM/YYYY" runat="server" MaxLength="10"></asp:TextBox>
                                   <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom"
                                                            TargetControlID="tbfromdate" ValidChars="/" />
                            </div>
                            <div class="col-lg-2">
                                <span class="form-control-label">To Date</span>
                                <asp:TextBox ID="tbtodate" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="DD/MM/YYYY" runat="server" MaxLength="10"></asp:TextBox>
                                   <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom"
                                                            TargetControlID="tbtodate" ValidChars="/" />
                            </div>

                            <div class="col-lg-2 pt-4">
                                <asp:LinkButton ID="LinkButton1" OnClick="lbtnsearch_Click" OnClientClick="return ShowLoading()" runat="server" Style="padding: 2px;" CssClass="btn btn-success btn-icon-only btn-sm mr-1 " strle="z-i">
                                            <i class="fa fa-search"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row px-2">
                            <div class="col-md-12 col-lg-12">
                                <asp:GridView ID="grdtransactionDetails" OnRowDataBound="grdtransactionDetails_RowDataBound"
                                    OnPageIndexChanging="grdtransactionDetails_PageIndexChanging" runat="server" AllowPaging="true" PageSize="20"
                                    ClientIDMode="Static" AutoGenerateColumns="False" ForeColor="#333333" Font-Size="16px"
                                    GridLines="None" Font-Bold="false" DataKeyNames="" Width="100%" CssClass="table table-hover">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Transaction Date ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransDate" runat="server" Text='<%# Eval("TransDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. of Booking">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotBooking" runat="server" Text='<%# Eval("TotBooking") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Booking Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotBookingAmt" Font-Bold="true" ForeColor="Green" runat="server" Text='<%# Eval("TotBookingAmt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tax Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotTaxAmt" runat="server" ForeColor="Green" Text='<%# Eval("TotTaxAmt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Commission Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotCommissionAmt" ForeColor="Green" runat="server" Text='<%# Eval("TotCommissionAmt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cancellation Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl" runat="server" ForeColor="Red" Text='<%# Eval("TotCancelAmt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pass Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl" runat="server" Font-Bold="true" ForeColor="Green" Text='<%# Eval("PassAmt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pass Tax Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl" runat="server" ForeColor="Green" Text='<%# Eval("PassTaxAmt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle  />
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
        </div>
    </div>


</asp:Content>


