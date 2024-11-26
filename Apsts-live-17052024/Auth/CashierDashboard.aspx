<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/cashierMaster.master" AutoEventWireup="true" CodeFile="CashierDashboard.aspx.cs" Inherits="Auth_CashierDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            var todayDate = new Date().getDate();
            var endD = new Date(new Date().setDate(todayDate));
            var currDate = new Date();

            $('[id*=txttransdate]').datepicker({
                // startDate: "dateToday",
                endDate: endD,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
        });
    </script>
    <style>
        .ModalPopupBG {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".searchable-dropdown").select2(); // Assuming you're using Select2 library
        });
    </script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/js/select2.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid pt-2 pb-5">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <h4 class="mb-1">Summary As On
                                            <asp:Label runat="server" ID="lbldatesummary"></asp:Label>
                    </h4>
                    <div class="row">

                        <div class="col-3 border-right">

                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">

                                        <div class="row mt-2">
                                            <div class="col-6 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-muted mb-0">Collection
                                                            <br />
                                                            <asp:Label ID="lbltotalCollection" runat="server" data-toggle="tooltip" data-placement="bottom" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-muted mb-0">Deposit<br />
                                                            <asp:Label ID="lbltotaldeposit" runat="server" data-toggle="tooltip" data-placement="bottom" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>

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
                        <div class="col-6 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h4 class="mb-1">Opening/Closing Summary
                                        </h4>
                                        <div class="row m-0">
                                            <div class="col-6 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-muted mb-0">Opened By&nbsp;
                                                            <asp:Label ID="lblopenby" runat="server" data-toggle="tooltip" data-placement="bottom" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>

                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-muted mb-0">Opened At&nbsp;
                                                            <asp:Label ID="lblopenat" runat="server" data-toggle="tooltip" data-placement="bottom" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>

                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-muted mb-0">Opening Balance&nbsp;
                                                            <asp:Label ID="lblopenbal" runat="server" data-toggle="tooltip" data-placement="bottom" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>

                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-6">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-muted mb-0">Closed By&nbsp;
                                                            <asp:Label ID="lblcloseby" runat="server" data-toggle="tooltip" data-placement="bottom" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>

                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-muted mb-0">Closed At&nbsp;
                                                            <asp:Label ID="lblcloseat" runat="server" data-toggle="tooltip" data-placement="bottom" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>

                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-muted mb-0">Closed Balance&nbsp;
                                                            <asp:Label ID="lblclosebal" runat="server" data-toggle="tooltip" data-placement="bottom" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-3 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-md-12 text-center">
                                        <h4 class="card-title text-muted mb-1">Current Balance<br />
                                            <asp:Label ID="lblcurrentbal" runat="server" data-toggle="tooltip" data-placement="bottom" CssClass="h3 font-weight-bold mb-0" Text="0"></asp:Label>
                                            <i class="fa fa-rupee-sign"></i>
                                        </h4>
                                        <div class="row m-0">
                                            <div class="col-12">
                                                <asp:LinkButton ID="lbtnbankdeposit" OnClick="lbtnbankdeposit_Click" runat="server" CssClass="btn btn-success" Style="width: 100%; box-shadow: rgb(0 0 0 / 24%) 0px 3px 8px;">
                                    <i class="fa fa-rupee-sign"></i> Bank Deposit
                                                </asp:LinkButton>
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
            <div class="col-md-3">
                <div class="card shadow" style="min-height: 80vh;">
                    <div class="card-header pb-2 pt-2">
                        <h4>Please Note</h4>
                    </div>
                    <div class="card-body text-danger">
                        <div class="row">
                            1. Please check/verify your opening/closing/current balance. 
                        </div>
                        <div class="row">
                            2. Cash can be collected from different heads. Make sure cash collection head should be correct at the time of cash deposit.
                        </div>
                       <div class="row">
                            3. Download Manual <asp:LinkButton href="../Auth/UserManuals/Cashier/Help Document for Cashier.pdf" target="_blank" runat="server" CssClass="btn btn-success btn-sm ml-1" ToolTip="Click here for manual."><i class="fa fa-download"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-5">
                <div class="card shadow" style="min-height: 80vh;">
                    <div class="card-header pb-2 pt-2">
                        <div class="row pl-3">
                            <div class="col-md-8">
                                <h4>Cash Deposit<br />
                                    <span class="text-danger text-sm">All Marked * Fields are mandatory</span></h4>
                            </div>
                            <%-- <div class="col-md-4 text-right">
                                <asp:LinkButton ID="LinkButtonInfo" runat="server" class="btn text-danger">help <i class="fa fa-info-circle" ></i></asp:LinkButton>
                            </div>--%>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="form-row" style="padding: 5px; background: #f1f0f0;">
                            <div class="col-md-12">
                                <span style="font-size: 10pt; color: black; font-weight: bold;">Received For</span>
                            </div>
                        </div>
                        <div class="form-row mt-3">
                            <div class="form-group col-md-4">
                                <label for="ddhead">
                                    Head <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddhead" Enabled="false" runat="server" OnSelectedIndexChanged="ddhead_SelectedIndexChanged" CssClass="form-control form-control-sm" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-md-8">
                                <label for="ddsubhead">
                                    Subhead <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddsubhead" runat="server" OnSelectedIndexChanged="ddsubhead_SelectedIndexChanged" CssClass="form-control form-control-sm" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <asp:Panel runat="server" ID="pnlcounter" Visible="false">
                            <div class="form-row">
                                <div class="form-group col-md-4">
                                    <label for="ddcounter">
                                        Counter</label>
                                    <asp:DropDownList ID="ddcounter" runat="server" OnSelectedIndexChanged="ddcounter_SelectedIndexChanged" CssClass="form-control form-control-sm" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-4" runat="server" id="empsec">
                                    <label for="ddemp">
                                        Employee</label>
                                    <asp:DropDownList ID="ddemp" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-4" runat="server" id="amtsec">
                                    <label>
                                        Amount</label>
                                    <asp:TextBox runat="server" ID="tbamount" CssClass="form-control form-control-sm" autocomplete="off"
                                        Text="0" MaxLength="8"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,custom"
                                        ValidChars="." TargetControlID="tbamount" />
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="form-group col-md-12" style="text-align: center;">
                                    <br />
                                    <h1 class="text-primary">Total Amount To Pay <span>&#8377;</span>
                                        <asp:Label runat="server" ID="totalpayamt" Text="0"></asp:Label></h1>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlCanteen" Visible="false">
                            <div class="form-row">
                                <div class="form-group col-md-4" id="oldreeiptno" runat="server" visible="false">
                                    <label for="ddcounter">
                                        Old Receipt Number<span class="text-danger">*</span></label>
                                    <asp:TextBox runat="server" ID="txtOldReceiptNumber" CssClass="form-control form-control-sm" MaxLength="15"
                                        autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4" id="maxicab" runat="server" visible="false">
                                    <label for="ddsubhead">
                                        Maxi Cab <span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlmaxicab" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-4" id="busno" runat="server" visible="false">
                                    <label for="ddcounter">
                                        Bus Number<span class="text-danger">*</span></label>
                                    <asp:TextBox runat="server" ID="txtBusNumber" CssClass="form-control form-control-sm" MaxLength="12"
                                        autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4" id="waybillno" runat="server" visible="false">
                                    <label for="ddcounter">
                                        Waybill number<span class="text-danger">*</span></label>
                                    <asp:TextBox runat="server" ID="txtWaybillnumber" CssClass="form-control form-control-sm" MaxLength="15"
                                        autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row" style="padding: 5px; background: #f1f0f0;">
                                <div class="col-md-3">
                                    <span style="font-size: 10pt; color: black; font-weight: bold;">Received From</span>
                                </div>
                                <div class="col-md-9">
                                    <asp:RadioButtonList ID="rbtnEmployee" runat="server" RepeatDirection="Horizontal"
                                        AutoPostBack="true">
                                        <asp:ListItem Value="O" Selected="True">Other</asp:ListItem>
                                        <asp:ListItem Value="U"> Department Employee</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="form-row mt-3">
                                <div class="form-group col-md-4">
                                    <label for="ddcounter">
                                        Name<span class="text-danger">*</span></label>
                                    <asp:TextBox runat="server" ID="txtname" CssClass="form-control form-control-sm" MaxLength="20" autocomplete="off"></asp:TextBox>
                                    <asp:DropDownList ID="ddlutcemp" runat="server" CssClass="form-control form-control-sm" Visible="false"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-4">
                                    <label for="ddemp">
                                        Mobile No.<span class="text-danger">*</span></label>
                                    <asp:TextBox runat="server" ID="txtmobileno" CssClass="form-control form-control-sm" MaxLength="10"
                                        autocomplete="off"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                        TargetControlID="txtmobileno" />
                                </div>
                                <div class="form-group col-md-4">
                                    <label>
                                        <b>Amount(Rs.)</b> <span class="text-danger">*</span></label>
                                    <asp:TextBox runat="server" ID="txtAmount" CssClass="form-control form-control-sm" Text="0" MaxLength="8"
                                        autocomplete="off" Style="text-align: right;"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers,custom"
                                        ValidChars="." TargetControlID="txtAmount" />
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="form-group col-md-4" id="idproof" runat="server" visible="false">
                                    <label>
                                        ID Proof Type<span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlIDProofType" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-4" id="idproofno" runat="server" visible="false">
                                    <label>
                                        ID Proof Number<span class="text-danger">*</span></label>
                                    <asp:TextBox runat="server" ID="txtIDProofNumber" CssClass="form-control form-control-sm" MaxLength="15"
                                        autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label>
                                        Remark</label>
                                    <asp:TextBox runat="server" ID="txtRemark" CssClass="form-control form-control-sm" Text="" TextMode="MultiLine"
                                        MaxLength="200" autocomplete="off" Style="resize: none"></asp:TextBox>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlWaybillCash" Visible="false">
                            <div class="form-row">
                                <div class="form-group col-md-4">
                                    <label for="ddcounter">
                                        Waybill number<span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlWaybillNo" runat="server" CssClass="form-control form-control-sm searchable-dropdown" OnSelectedIndexChanged="ddlWaybillNo_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-4">
                                    <asp:LinkButton runat="server" ID="lbtnViewWaybill" OnClick="lbtnViewWaybill_Click" Visible="false" CssClass="btn btn-primary btn-sm mt-4" ToolTip="View Waybill Details"><i class="fa fa-eye"></i></asp:LinkButton>
                                </div>
                            </div>
                            <div class="form-row" runat="server" id="divWDepot" visible="false">
                                <div class="form-group col-sm-4">
                                    <span style="font-size: 9pt">Bus No</span><br />
                                    <asp:Label runat="server" ID="lblBusNo" CssClass="form-control-label"></asp:Label>
                                </div>
                                <div class="form-group col-sm-4">
                                    <span style="font-size: 9pt">ETM No</span><br />
                                    <asp:Label runat="server" ID="lblETM" CssClass="form-control-label"></asp:Label>
                                </div>
                                <div class="form-group col-md-4">
                                    <span style="font-size: 9pt">Duty Date/Time</span><br />
                                    <asp:Label runat="server" ID="lblDutyTime" CssClass="form-control-label"></asp:Label>
                                </div>
                            </div>
                            <div class="form-row" runat="server" id="divWServices" visible="false">
                                <div class="form-group col-md-12" runat="server" id="divWServCategory" visible="false">

                                    <span style="font-size: 9pt">Service Name</span><br />
                                    <asp:Label runat="server" ID="lblServiceName" CssClass="form-control-label"></asp:Label>
                                </div>
                                <div class="col-md-12 form-row" style="padding: 5px; background: #f1f0f0;">
                                    <div class="col-md-12">
                                        <span style="font-size: 10pt; color: black; font-weight: bold;">Received From</span>
                                    </div>
                                </div>
                                <div class="col-md-12 form-row mt-3">
                                    <div class="form-group col-md-6">
                                        <label for="ddcounter">
                                            Name<span class="text-danger">*</span></label>
                                        <asp:TextBox runat="server" ID="txtWEmpName" CssClass="form-control form-control-sm" MaxLength="20" autocomplete="off"></asp:TextBox>
                                        <asp:DropDownList ID="ddlWEmpName" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" Visible="false">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label for="ddemp">
                                            Mobile No.<span class="text-danger">*</span></label>
                                        <asp:TextBox runat="server" ID="txtWEmpMobile" CssClass="form-control form-control-sm" MaxLength="10"
                                            autocomplete="off"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" FilterType="Numbers"
                                            TargetControlID="txtWEmpMobile" />
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label>
                                            <b>Amount(Rs.)</b> <span class="text-danger">*</span></label>
                                        <asp:TextBox runat="server" ID="txtWaybillTotAmt" CssClass="form-control form-control-sm" Text="0" MaxLength="8" ReadOnly="true"
                                            autocomplete="off"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" FilterType="Numbers,custom"
                                            ValidChars="." TargetControlID="txtWaybillTotAmt" />
                                    </div>
                                </div>
                                <div class="col-md-12 form-row">
                                    <div class="form-group col-md-6">
                                        <label>
                                            Remark</label>
                                        <asp:TextBox runat="server" ID="txtWRemark" CssClass="form-control form-control-sm" Text="" TextMode="MultiLine"
                                            MaxLength="200" autocomplete="off" Style="resize: none"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12 form-row">
                                    <div class="form-group col-md-12" style="text-align: center;">
                                        <h3 class="text-primary text-center">Total Amount To Pay <span>&#8377;</span>
                                            <asp:Label runat="server" ID="lblTotayWaybillAmount" Text="0"></asp:Label></h3>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="col-12 mt-2 text-center">
                            <asp:LinkButton runat="server" ID="btnsave" OnClick="btnsave_Click" CssClass="btn btn-success"> Save</asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lbtnreset" CssClass="btn btn-danger"> Reset</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card shadow" style="min-height: 80vh;">
                    <div class="card-header pb-2">
                        <div class="row">
                            <div class="col-lg-4">
                                <h5>Transaction Summery</h5>
                            </div>
                            <div class="col-lg-4">
                                <asp:TextBox class="form-control form-control-sm" runat="server" ID="txttransdate" MaxLength="10" autocomplete="off"
                                    placeholder="DD/MM/YYYYY" Text=""></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers,Custom"
                                    TargetControlID="txttransdate" ValidChars="/" />

                            </div>
                            <div class="col-lg-2">
                                <asp:LinkButton ID="lbtnsearch" runat="server" OnClick="lbtnsearch_Click" class="btn btn-primary" Style="border-radius: 4px; height: 30px; font-size: 10pt; padding-top: 4px;"
                                    ToolTip="Search Daily Transaction Details"><i class="fa fa-search"></i></asp:LinkButton>
                            </div>
                            <div class="col-lg-2">
                                <asp:LinkButton ID="lbtnDownloadExcel" OnClick="lbtnDownloadExcel_Click" Visible="true" runat="server" CssClass="btn btn-success"
                                    Style="border-radius: 4px; height: 30px; font-size: 10pt; padding-top: 4px;"
                                    ToolTip="Download Daily Transaction Details"><i class="fa fa-download"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row border-bottom">
                            <div class="col-lg-6 border-right">
                                <h4 class="text-primary">Collection <span>&#8377;</span>
                                    <asp:Label runat="server" ID="lbtodayamt" Text="0"></asp:Label></h4>
                            </div>
                            <%--  <div class="col-lg-6">
                                <h4 class="text-primary">Deposit <span>&#8377;</span>
                                    <asp:Label runat="server" ID="Label7" Text="0"></asp:Label></h4>
                            </div>--%>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <asp:GridView ID="gvTodayTransaction" runat="server" EnableModelValidation="True"
                                    AllowPaging="True" Font-Size="12pt" GridLines="None" class="table table-striped table-responsive-sm"
                                    AutoGenerateColumns="False" DataKeyNames="txnid" OnRowCommand="gvTodayTransaction_RowCommand"
                                    PageSize="6" OnPageIndexChanging="gvTodayTransaction_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Ref No." HeaderStyle-Font-Size="12pt" ItemStyle-Font-Size="12pt">
                                            <ItemTemplate>
                                                <asp:Label ID="linkbtnBUSREGISTRATIONNO" runat="server" Text='<%#Bind("txnid") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" HeaderStyle-Font-Size="12pt" ItemStyle-Font-Size="12pt">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblCHECHISNO" Text='<%#Bind("amt") %>' /><i class="fa fa-rupee-sign ml-1"></i>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View" HeaderStyle-Font-Size="12pt">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="view" runat="server" CommandName="viewTrans" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    class="btn btn-primary py-0 px-2" Style="border-radius: 3px;"><i class="fa fa-print" title="Click to Print Receipt"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#eaf4ff" />
                                    <HeaderStyle BackColor="aliceblue" ForeColor="black" VerticalAlign="Top" CssClass="table_head" />
                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                </asp:GridView>
                                <asp:Panel runat="server" ID="pnlNodata" Visible="false">
                                    <h1 class="text-center text-muted mt-8">Transactions
                                       <br />
                                        Not Found
                                    </h1>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpOpenClose" runat="server" PopupControlID="pnlOpenClose"
                CancelControlID="Button6" TargetControlID="Button5" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlOpenClose" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-body" style="min-height: 100px;">
                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align: center;">Welcome
                                    <br />
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trChestAmt" runat="server" visible="false">
                                <td style="text-align: center;">
                                    <asp:Label ID="lblAmt" runat="server" Text="Please Check/Verify Total Amount in your chest"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblCloseBlnc" runat="server" Font-Bold="true" Text="0"></asp:Label>
                                    <i class="fa fa-rupee"></i>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;">
                                    <asp:Label ID="lblmsg1" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;">
                                    <br />
                                    <asp:LinkButton ID="btnYes" runat="server" OnClick="btnYes_Click" CssClass="btn btn-success" Style="height: 30px; width: 90px; padding-top: 4px; font-size: 10pt; border-radius: 4px;"> <i class="fa fa-check"></i> YES </asp:LinkButton>
                                    <asp:LinkButton ID="btnNo" runat="server" OnClick="btnNo_Click" CssClass="btn btn-warning" Style="height: 30px; width: 90px; padding-top: 4px; font-size: 10pt; border-radius: 4px;"> <i class="fa fa-times"></i> No </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button5" runat="server" Text="" />
                    <asp:Button ID="Button6" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
                CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground" BehaviorID="bvConfirm">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
                <div class="card" style="width: 450px;">
                    <div class="card-header">
                        <h2 class="card-title text-left mb-0">Please Confirm
                        </h2>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnYesConfirmation" runat="server" OnClick="lbtnYesConfirmation_Click" OnClientClick="$find('bvConfirm').hide();ShowLoading();" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                            <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button4" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpShowCashslip" runat="server" PopupControlID="Panel1" TargetControlID="Button1"
                CancelControlID="LinkButton2" BackgroundCssClass="ModalPopupBG">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="Panel1" runat="server" Style="position: fixed;">
                <div class="modal-content mt-5" style="width: 50vw;">
                    <div class="card w-100">
                        <div class="card-header py-3">
                            <div class="row">
                                <div class="col">
                                    <h3 class="m-0">Cash Deposit Slip</h3>
                                </div>
                                <div class="col-auto">
                                    <asp:LinkButton ID="LinkButton2" runat="server" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>

                        <div class="card-body p-2">
                            <div class="row">
                                <div class="col-12">

                                    <asp:Literal ID="eSlip" runat="server"></asp:Literal>

                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button1" runat="server" Text="" />
                    <asp:Button ID="Button2" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpExpenditureDetails" runat="server" PopupControlID="pnlExpenditureDetails" TargetControlID="Button8"
                CancelControlID="LinkButton1" BackgroundCssClass="ModalPopupBG">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlExpenditureDetails" runat="server" Style="position: fixed;">
                <div class="modal-content mt-5" style="width: 90vw;">
                    <div class="card w-100">
                        <div class="card-header">
                            <div class="row">
                                <div class="col">
                                    <h3 class="m-0">Waybill Collection & Expenditure Details</h3>
                                </div>
                                <div class="col-auto">
                                    <asp:LinkButton ID="LinkButton1" runat="server" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body p-2">
                            <asp:Literal ID="eJourneyDetails" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button8" runat="server" Text="" />
                    <asp:Button ID="Button9" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
    </div>

</asp:Content>

