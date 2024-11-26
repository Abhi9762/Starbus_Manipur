<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="admWalletTransaction.aspx.cs" Inherits="Auth_admWalletTransaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .table td, .table th {
            padding: 7px 5px;
            vertical-align: top;
            border-top: none;
        }

        .GridPager a, .GridPager span {
            display: inline-block;
            padding: 0px 9px;
            border-radius: 2px;
            border: solid 1px #f3eded;
            background: #f3eded;
            box-shadow: inset 0px 1px 0px rgba(255,255,255, .8), 0px 1px 3px rgba(0,0,0, .1);
            font-size: .875em;
            font-weight: bold;
            text-decoration: none;
            color: #717171;
            text-shadow: 0px 1px 0px rgba(255,255,255, 1);
        }

        .GridPager a {
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #b0aeae;
        }

        .GridPager span {
            background: #f3eded;
            box-shadow: inset 0px 0px 8px rgba(0,0,0, .5), 0px 1px 0px rgba(255,255,255, .8);
            color: #000;
            text-shadow: 0px 0px 3px rgba(0,0,0, .5);
            border: 1px solid #f3eded;
        }
        /* The container */
        .containerRadio {
            display: block;
            position: relative;
            padding-left: 35px;
            margin-bottom: 12px;
            cursor: pointer;
            font-size: 18px;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
        }

            /* Hide the browser's default radio button */
            .containerRadio input {
                position: absolute;
                opacity: 0;
                cursor: pointer;
            }

        /* Create a custom radio button */
        .checkmarkRadio {
            position: absolute;
            top: 4px;
            left: 8px;
            height: 21px;
            width: 20px;
            background-color: #eee;
            border-radius: 50%;
        }

        /* On mouse-over, add a grey background color */
        .containerRadio:hover input ~ .checkmarkRadio {
            background-color: #ccc;
        }

        /* When the radio button is checked, add a blue background */
        .containerRadio input:checked ~ .checkmarkRadio {
            background-color: #2196F3;
        }

        /* Create the indicator (the dot/circle - hidden when not checked) */
        .checkmarkRadio:after {
            content: "";
            position: absolute;
            display: none;
        }

        .card {
            margin-bottom: 0px !important;
        }
        /* Show the indicator (dot/circle) when checked */
        .containerRadio input:checked ~ .checkmarkRadio:after {
            display: block;
        }

        /* Style the indicator (dot/circle) */
        .containerRadio .checkmarkRadio:after {
            top: 7px;
            left: 6px;
            width: 8px;
            height: 8px;
            border-radius: 50%;
            background: white;
        }

        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            var todayDate = new Date().getDate();
            var endD = new Date(new Date().setDate(todayDate));
            var currDate = new Date();

            $('[id*=txtFromDate]').datepicker({
                // startDate: "dateToday",
                endDate: endD,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
            $('[id*=txttodate]').datepicker({
                // startDate: "dateToday",
                endDate: endD,
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
        <div class="card">
            <div class="card-header">
                <div class="row">
                    <div class="col-xl-4">
                        <div runat="server" id="divSearchBar" style="display: flex; justify-content: center; padding-top: 20px;">
                            <label class="containerRadio">
                                Traveller Wallet
                                <asp:RadioButton runat="server" ID="rbtnTravellerWallet" OnCheckedChanged="rbtnTravellerWallet_CheckedChanged" GroupName="radio" Checked="true" AutoPostBack="true" />
                                <span class="checkmarkRadio"></span>
                            </label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <label class="containerRadio">
                                Agent/CSC Wallet
                                <asp:RadioButton runat="server" ID="rbtnAgentWallet" OnCheckedChanged="rbtnAgentWallet_CheckedChanged" GroupName="radio" AutoPostBack="true" />
                                <span class="checkmarkRadio"></span>
                            </label>
                        </div>

                    </div>
                    <div class="col-xl-8">
                        <div class="row">
                            <div class="col-lg-3">
                                <label class="text-muted form-control-label">From Date</label>
                                <div class="input-group">
                                    <asp:TextBox CssClass="form-control" AutoComplete="off" runat="server" ID="txtFromDate" MaxLength="10" ToolTip="Enter Journey Date"
                                        placeholder="DD/MM/YYYY" Text="" Style="display: inline;"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <label class="text-muted form-control-label">To Date</label>
                                <div class="input-group">
                                    <asp:TextBox CssClass="form-control" AutoComplete="off" runat="server" ID="txttodate" MaxLength="10" ToolTip="Enter Booking Date"
                                        placeholder="DD/MM/YYYY" Text="" Style="display: inline;"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <label class="text-muted form-control-label">Wallet Reference No. (<asp:Label runat="server" ID="lblstakHolder" Text="" Style="font-size: 10pt;"></asp:Label>)</label>
                                <br />
                                <asp:TextBox class="form-control" runat="server" ID="txtWalletRefNo" MaxLength="50"
                                    placeholder="Max 50 Characters" Text="" Style=""></asp:TextBox>
                            </div>
                            <div class="col-2" style="padding: 20px; padding-top: 28px;">
                                <asp:LinkButton ID="lbtnsearch" runat="server" class="btn btn-primary btn-sm" OnClick="lbtnsearch_Click" OnClientClick="return ShowLoading();"> <i class="fa fa-search"></i></asp:LinkButton>
                                <asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-warning btn-sm" ToolTip="Reset" OnClick="lbtnReset_Click" OnClientClick="return ShowLoading();"> <i class="fa fa-undo"></i></asp:LinkButton>


                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-lg-6 pt-2" style="border-right: 1px solid #e6e6e6;">
                        <div class="row pb-2 mr-1" style="border-bottom: 1px solid #e6e6e6;">
                            <div class="col-lg-6">
                                <asp:Label ID="lblwallettopuptrns" runat="server" Text="Wallet Topup Transaction"></asp:Label>
                            </div>
                            <div class="col-lg-6 text-right">
                                <asp:LinkButton ID="lbtndownloadWallettopup" OnClick="lbtndownloadWallettopup_Click" Visible="false" runat="server" CssClass="btn btn-warning"> <i class="fa fa-download"></i> Download</asp:LinkButton>
                            </div>
                        </div>


                        <asp:GridView ID="gvwallettopup" runat="server"
                            AllowPaging="true" PageSize="10" AutoGenerateColumns="False" ForeColor="#333333"
                            GridLines="None" Font-Bold="false" DataKeyNames="" AlternatingRowStyle-BackColor="#e6e6e6"
                            OnPageIndexChanging="gvwallettopup_PageIndexChanging"
                            CssClass="table" Width="100%" Style="display: inline-table;">
                            <Columns>
                                <asp:TemplateField HeaderText="Transaction Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("txndate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Refrence No.">
                                    <ItemTemplate>
                                        <h5 class="mb-0 font-weight-light"><%# Eval("txnrefno") %></h5>
                                        <h5 class="mb-0 font-weight-light"><span class="b"><i class="fa fa-clock"></i></span> <%# Eval("txnsdate") %></h5>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTXNAMOUNT" runat="server" Text='<%# Eval("txnamt") %>'></asp:Label>
                                        <i class="fa fa-rupee"></i>
                                        <br />
                                        <span class="text-muted">(<%# Eval("gatewayname") %>)</span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Credited Account">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCREDIT_AMT" runat="server" Text='<%# Eval("cr_amt") %>'></asp:Label>
                                        <i class="fa fa-rupee"></i>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                      
                                         <h5 class="mb-0 font-weight-bold" style='<%# Eval("txncomplete_yn").ToString() == "N"? "color:Red;": "color:#0ed10e;" %>'>
                                        <%# Eval("status_") %> </h5>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="GridPager" ForeColor="Black" />
                        </asp:GridView>
                        <asp:Panel ID="pnlNowallettopup" runat="server" Width="100%" Visible="true">
                            <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;">
                                    Wallet Top up transaction details not available
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-lg-6 pt-2">
                        <div class="row pb-2 mr-1" style="border-bottom: 1px solid #e6e6e6;">
                            <div class="col-lg-6">
                                <asp:Label ID="lblwallettrns" runat="server" Text="Wallet Transaction"></asp:Label>
                            </div>
                            <div class="col-lg-6 text-right">
                                <asp:LinkButton ID="lbtndownloadWallettransaction" OnClick="lbtndownloadWallettransaction_Click" Visible="false" runat="server" CssClass="btn btn-warning"> <i class="fa fa-download"></i> Download</asp:LinkButton>
                            </div>
                        </div>

                        <asp:GridView ID="gvwallettransaction" runat="server"
                            AllowPaging="true" PageSize="10" AutoGenerateColumns="False" ForeColor="#333333"
                            GridLines="None" Font-Bold="false" DataKeyNames="" AlternatingRowStyle-BackColor="#e6e6e6" OnPageIndexChanging="gvwallettransaction_PageIndexChanging"
                            CssClass="table table-hover" Width="100%" Style="display: inline-table;">
                            <Columns>
                                <asp:TemplateField HeaderText="Refrence No.">
                                    <ItemTemplate>
                                        <h5 class="mb-0 font-weight-light"><%# Eval("wallettxnrefno") %></h5>
                                        <h5 class="mb-0 font-weight-light"><span class="b"><i class="fa fa-clock"></i></span> <%# Eval("txn_date") %></h5>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <h5 class="mb-0 font-weight-light"><%# Eval("txn_type") %></h5>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                         <h5 class="mb-0 font-weight-bold" style='<%# Eval("wallettxn_typecode").ToString() == "B"? "color:Red;": "color:#0ed10e;" %>'>
                                       
                                        <%# Eval("txnamt") %> <i class="fa fa-rupee"></i></h5>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="GridPager" ForeColor="Black" />
                        </asp:GridView>
                        <asp:Panel ID="pnlNowallettransaction" runat="server" Width="100%" Visible="true">
                            <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;">
                                    Wallet transaction details not available
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>




    </div>
</asp:Content>

