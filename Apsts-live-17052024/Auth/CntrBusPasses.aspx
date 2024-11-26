<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Cntrmaster.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="CntrBusPasses.aspx.cs" Inherits="Auth_CntrBusPasses" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="assets/js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="assets/js/main.js" type="text/javascript"></script>
    <link href="../style.css" rel="stylesheet" />

    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <script type="text/javascript">
        $(function () {
            $('#<%= ddlServiceType.ClientID %>').multiSelect();

        });


        function UploadDoc(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnUploadDoc.ClientID %>").click();
            }
        }

        function UploadAddDoc(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnAddproof.ClientID %>").click();


            }
        }
        function UploadDocRenew(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnUploadDocRenew.ClientID %>").click();
            }
        }

        function UploadAddDocRenew(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnAddproofRenew.ClientID %>").click();
            }
        }
    </script>
    <style type="text/css">
        .multi-select-button {
            width: 200px;
            height: 33px;
            padding: 5px;
        }

            .multi-select-button:after {
                margin-left: 9.4em !important;
            }

        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .text-bold {
            font-weight: bold;
        }

        .rbl label {
            margin-left: 4px;
            margin-right: 20px;
            vertical-align: middle;
            font-size: 10pt;
        }

        body {
            font-size: 0.8rem;
        }

        h6 {
            font-size: 0.8rem;
        }

        .border-right {
            border-right: 1px solid #e6e6e6;
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


    <script type="text/javascript">
        $(document).ready(function () {

            var todayDate = new Date().getDate();
            var endDate = $("[id$=hdmaxdate]").val();
            var endD = new Date(new Date().setDate(todayDate + parseInt(endDate - 1)));
            var currDate = new Date();
            $('[id*=tbapplydate]').datepicker({
                endDate: "dateToday",
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
            $('[id*=tbDate]').datepicker({
                endDate: "dateToday",
                changeMonth: true,
                changeYear: true,
                format: "dd/mm/yyyy",
                autoclose: true
            });


            $('[id*=txtfrmdate]').datepicker({
                endDate: "dateToday",
                changeMonth: true,
                changeYear: true,
                format: "dd/mm/yyyy",
                autoclose: true
            });


            $('[id*=txttodate]').datepicker({
                endDate: "dateToday",
                changeMonth: true,
                changeYear: true,
                format: "dd/mm/yyyy",
                autoclose: true
            });
        });
        function close() {
            alert('ok');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:HiddenField ID="hidtoken" runat="server" />
    <asp:HiddenField ID="hdnMinAge" runat="server" />
    <asp:HiddenField ID="hdnMaxAge" runat="server" />
    <asp:HiddenField ID="hdimg" runat="server" />
    <asp:HiddenField ID="hdimg1" runat="server" />
    <div class="content mt-3">
        <div class="row">
            <div class="col-md-4 pr-1">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card mb-2 summary" style="min-height: 1229px !important;">
                            <div class="card-header" style="background-color: #f7f7f7; padding: 3px;">
                                <div class="row no-gutter">
                                    <div class="col-lg-12 pl-3">
                                        <span class="card-title text-dark" style="color: #0d1b3eb3; font-size: 19px;">Pass/Transaction Summary</span>
                                    </div>
                                </div>
                                <div class="row pl-3">
                                    <div class="col-md-5">
                                        <asp:Label runat="server" CssClass=" form-control-label card-title lbl">Transaction Date </asp:Label>
                                        <div class="input-group">
                                            <asp:TextBox ID="tbapplydate" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="DD/MM/YYYY" runat="server" MaxLength="10"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label runat="server" CssClass=" form-control-label card-title lbl">Issuance Type </asp:Label>
                                        <asp:DropDownList ID="ddlIssuanceType" runat="server"
                                            CssClass="form-control form-control-sm form-controlsm">
                                            <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                            <asp:ListItem Value="I">Instant Issue</asp:ListItem>
                                            <asp:ListItem Value="A">After Approval</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3" style="padding-top: 20px;">
                                        <asp:LinkButton ID="lbtnsearchView" ToolTip="Search Pass/Pass Request" runat="server" OnClick="lbtnsearchView_Click" CssClass="btn btn-success btn-sm">
                                            <i class="fa fa-search"></i> 
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnsearchDownload" ToolTip="Download Pass Report" OnClick="lbtnsearchDownload_Click" Visible="false" runat="server" CssClass="btn btn-warning btn-sm">
                                            <i class="fa fa-download"></i> 
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body">
                                <asp:GridView ID="grdsearchpass" runat="server" PageSize="10" AutoGenerateColumns="False" GridLines="None" ShowHeader="false" AllowSorting="true" AllowPaging="true" CssClass="table table-hover"
                                    HeaderStyle-CssClass="thead-light font-weight-bold" DataKeyNames="currtranref_no,total_amount,card_type_name,psngr_type_name,pass_no,current_status,issuence_" OnRowCommand="grdsearchpass_RowCommand" OnRowDataBound="grdsearchpass_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Pass Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBUSPASS_CATEGORY_NAME" runat="server" Text='<%# Eval("card_type_name") %>'></asp:Label>
                                                <br />
                                                <asp:Label ID="lblNAME" runat="server" Text='<%# Eval("psngr_type_name") %>'></asp:Label>
                                                <br />
                                                <span>Ref. No.</span> -   
                                                                    <asp:Label ID="lblCURRTRANREFNO" runat="server" Text='<%# Eval("currtranref_no") %>'></asp:Label>
                                                <br />
                                                <span>Pass No.</span>
                                                <asp:Label ID="lblPASSNUMBER" runat="server" Text='<%# Eval("pass_no") %>'></asp:Label><br />
                                                <asp:Label ID="lblapplytype" runat="server" Text='<%# Eval("apply_type") %>'></asp:Label>,
                                                <asp:Label ID="lblTRANSTYPE" runat="server" Text='<%# Eval("trans_type") %>'></asp:Label><br />
                                                <asp:Label ID="lblISSUENCETYPE" runat="server" Text='<%# Eval("issuence_type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transaction Amt.">
                                            <ItemTemplate>
                                                <span>Amt.Charged</span><br />
                                                <asp:Label ID="lblTOTALAMOUNT" runat="server" Text='<%# Eval("total_amount") %>'></asp:Label>
                                                <i class="fa fa-rupee"></i>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnprintpass" runat="server" CssClass="btn btn-primary btn-sm" CommandName="PrintPass" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Click here to Print Pass"> <i class="fa fa-print" title="Click here to Print Pass"></i> </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnprintreceipt" runat="server" CssClass="btn btn-info btn-sm" CommandName="PrintReceipt" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Click here to Print Pass Payment Receipt"> <i class="fa fa-rupee-sign" title="Click here to Print Pass Payment Receipt"></i> </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                </asp:GridView>
                                <div class="text-center busListBox" id="dvsearchpass" runat="server"
                                    style="padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold; color: #e3e3e3;"
                                    visible="true">
                                    Sorry, No Record Available 
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card mb-2" style="min-height: 1229px !important;">
                            <div class="card-header" style="background-color: #f7f7f7; padding: 3px;">
                                <div class="row no-gutter">
                                    <div class="col-lg-6 pl-4">
                                        <span class="card-title text-dark" style="color: #0d1b3eb3; font-size: 19px;">
                                            <asp:Label ID="lblrequestmsg" runat="server">New Pass Request</asp:Label></span>
                                    </div>
                                    <div class="col-lg-6 text-right">
                                        <div class="btn-group-sm btn-group">
                                            <asp:Button ID="btnPassNew" runat="server" OnClick="btnPassNew_Click" Text="New Pass" CssClass="btn btn-success btn-sm btn-borderradius3"></asp:Button>
                                            <asp:Button ID="btnPassRenew" runat="server" OnClick="btnPassRenew_Click" Text="Renew Pass" CssClass="btn btn-default btn-sm btn-borderradius3"></asp:Button>
                                            <asp:Button ID="btnPassQuery" runat="server" OnClick="btnPassQuery_Click" Text="Query/Report" CssClass="btn btn-default btn-sm btn-borderradius3"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body" style="background-color: #fff;">
                                <asp:Panel ID="pnlPassNew" runat="server" Visible="true">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <asp:Panel ID="pnlDetails" runat="server">
                                                <div class="">
                                                    <div class="">
                                                        <div class="row">
                                                            <div class="col-lg-3" style="width: 19%; padding-top: 6px;">
                                                                <h6>Select Bus Pass Type<span class="text-danger">*</span></h6>
                                                            </div>
                                                            <div class="col-lg-4">
                                                                <asp:DropDownList ID="ddlbuspassType" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlbuspassType_SelectedIndexChanged" CssClass="form-control form-control-sm" ToolTip="Select Bus Pass Type">
                                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-lg-1 pt-1">
                                                                <asp:LinkButton ID="lbtnInfo" Visible="false" runat="server" OnClick="lbtnInfo_Click" CssClass="text-primary">
                                                     <i class="fa fa-info-circle fa-2x"></i></asp:LinkButton>
                                                            </div>
                                                            <div class="col-lg-4" style="text-align: right; width: 39%; font-size: 10pt; padding-top: 6px;">
                                                                <span class="text-danger">Fields Marked * are Mendatory</span>
                                                            </div>

                                                        </div>
                                                        <hr />
                                                    </div>
                                                    <div class="">
                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <h6 class="text-bold">Enter Personal Details</h6>
                                                                <div class="row my-2">
                                                                    <div class="col-lg-4">
                                                                        <asp:Label runat="server" Text="Name" CssClass="form-control-label HeadLabel"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                        <asp:TextBox ID="tbName" runat="server" CssClass="form-control" placeholder="Max 30 characters" MaxLength="30" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Enter Name"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="UppercaseLetters,LowercaseLetters,Custom" TargetControlID="tbName" ValidChars=" " />
                                                                    </div>
                                                                    <div class="col-lg-4">
                                                                        <asp:Label runat="server" Text="Gender" CssClass="form-control-label HeadLabel"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" title="Select Gender">
                                                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                                                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                                                            <asp:ListItem Text="Other" Value="O"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-lg-4">
                                                                        <asp:Label runat="server" Text="Date of Birth" CssClass="form-control-label HeadLabel"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                        <div class="input-group">
                                                                            <asp:TextBox ID="tbDate" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="DD/MM/YYYY" runat="server" MaxLength="10"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row my-2">
                                                                    <div class="col-lg-4">
                                                                        <asp:Label runat="server" Text="Father Name" CssClass="form-control-label HeadLabel"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                        <asp:TextBox ID="tbFatherName" runat="server" CssClass="form-control" placeholder="Max 30 characters" MaxLength="30" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Enter Father's Name"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="UppercaseLetters,LowercaseLetters,Custom" ValidChars=" " TargetControlID="tbFatherName" />
                                                                    </div>
                                                                    <div class="col-lg-4">
                                                                        <asp:Label runat="server" Text="Service Type" CssClass="form-control-label HeadLabel"></asp:Label><span class="text-danger HeadLabel">*</span><br />
                                                                        <asp:DropDownList ID="ddlServiceType" CssClass="form-control" runat="server" ToolTip="Select Service Type"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <hr />
                                                        <asp:Panel ID="pnlRoute" runat="server" Visible="false">
                                                            <h6 class="text-bold">Pass Requested For</h6>
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <div class="row my-2">
                                                                        <div class="col-lg-8">
                                                                            <asp:Label runat="server" Text="Route" CssClass="form-control-label HeadLabel"></asp:Label><span class="text-danger HeadLabel">*</span>

                                                                            <asp:DropDownList ID="ddlRoute" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged" CssClass="form-control" ToolTip="Select Route">
                                                                                <asp:ListItem Text="Select Route" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row my-2">
                                                                        <div class="col-lg-4">
                                                                            <asp:Label runat="server" Text="From" CssClass="form-control-label HeadLabel"></asp:Label><span class="text-danger HeadLabel">*</span>

                                                                            <asp:DropDownList ID="ddlFrom" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlFrom_SelectedIndexChanged" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" title="From">
                                                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>

                                                                        </div>
                                                                        <div class="col-lg-4">
                                                                            <asp:Label runat="server" Text="To" CssClass="form-control-label HeadLabel"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                            <asp:DropDownList ID="ddlTo" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlTo_SelectedIndexChanged" data-toggle="tooltip" data-placement="bottom" title="To">
                                                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <hr />
                                                        </asp:Panel>
                                                        <div class="row ">
                                                            <div class="col-lg-12">
                                                                <h6 class="text-bold">Enter Contact Details</h6>
                                                                <div class="row my-2">
                                                                    <div class="col-lg-4">
                                                                        <asp:Label runat="server" Text="Mobile No." CssClass="form-control-label HeadLabel"></asp:Label><span class="text-danger HeadLabel">*</span>

                                                                        <asp:TextBox ID="tbMobile" runat="server" CssClass="form-control" placeholder="Max 10 digits" MaxLength="10" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Enter Mobile Number"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers" TargetControlID="tbMobile" />
                                                                    </div>
                                                                    <div class="col-lg-4">
                                                                        <asp:Label runat="server" Text="Email" CssClass="form-control-label HeadLabel"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                        <asp:TextBox ID="tbEmail" runat="server" CssClass="form-control form-control-sm" placeholder="Max 50 characters" MaxLength="50" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Enter Email"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="UppercaseLetters,lowercaseletters,Numbers,Custom" TargetControlID="tbEmail" ValidChars="@,." />
                                                                    </div>
                                                                </div>
                                                                <div class="row my-2">
                                                                    <div class="col-lg-4">
                                                                        <asp:Label runat="server" Text="State" CssClass="form-control-label HeadLabel"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                        <asp:DropDownList ID="ddlState" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" title="Select State">
                                                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-lg-4">
                                                                        <asp:Label runat="server" Text="District" CssClass="form-control-label HeadLabel"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                        <asp:DropDownList ID="ddlDistrict" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" title="Select District">
                                                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-lg-4">
                                                                        <asp:Label runat="server" Text="City" CssClass="form-control-label HeadLabel"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                        <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" data-toggle="tooltip" data-placement="bottom" title="Select City">
                                                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="row my-2">
                                                                    <div class="col-lg-8">
                                                                        <asp:Label runat="server" Text="Address" CssClass="form-control-label HeadLabel"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                        <asp:TextBox ID="tbAddress" runat="server" Height="70px" CssClass="form-control" Style="resize: none;" TextMode="MultiLine" placeholder="Max 100 characters" MaxLength="100" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Enter Address"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom,Uppercaseletters,Lowercaseletters" TargetControlID="tbAddress" ValidChars=",-/ " />
                                                                    </div>
                                                                    <div class="col-lg-4">
                                                                        <asp:Label runat="server" Text="Pincode" CssClass="form-control-label HeadLabel"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                        <asp:TextBox ID="tbPincode" runat="server" CssClass="form-control form-control-sm" placeholder="Max 6 digits" MaxLength="6" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Enter Pincode"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers" TargetControlID="tbPincode" ValidChars=", " />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row d-none">
                                                            <h6 class="text-bold" style="margin-left: 15px;">Validity</h6>
                                                            <div class="col-lg-12">
                                                                <div class="row my-2">
                                                                    <div class="col-lg-4">
                                                                        <asp:Label runat="server" Text="From" CssClass="form-control-label HeadLabel"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                        <div class="input-group input-group-alternative">
                                                                            <div class="input-group">
                                                                                <asp:TextBox ID="tbValidityFrom" AutoPostBack="true" runat="server" MaxLength="10" CssClass="form-control" type="Search" placeholder="DD/MM/YYYY" Height="74%" AutoComplete="off"></asp:TextBox>
                                                                                <span class="input-group-text" id="tbfrom"><i class="fa fa-calendar"></i></span>
                                                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" CssClass="black"
                                                                                    PopupButtonID="tbfrom" TargetControlID="tbValidityFrom"></cc1:CalendarExtender>
                                                                                <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbValidityFrom" ValidChars="/" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-4">
                                                                        <asp:Label runat="server" Text="Upto" CssClass="form-control-label HeadLabel"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                        <div class="input-group input-group-alternative">
                                                                            <div class="input-group">
                                                                                <asp:TextBox ID="tbValidityTo" Enabled="false" runat="server" MaxLength="10" CssClass="form-control" type="Search" placeholder="DD/MM/YYYY" Height="74%" AutoComplete="off"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <hr />
                                                        <div class="row">
                                                            <h6 class="text-bold" style="margin-left: 15px;">Aadhar Details</h6>
                                                            <div class="col-lg-12">
                                                                <div class="row my-2">
                                                                    <div class="col-lg-4">
                                                                        <asp:Label runat="server" Text="Aadhar No." CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>
                                                                        <asp:TextBox ID="tbAdharNumber" runat="server" CssClass="form-control form-control-sm" placeholder="Max 12 digits" MaxLength="12" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Enter Aadhar Number"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers" TargetControlID="tbAdharNumber" />
                                                                    </div>
                                                                    <div class="col-lg-4">
                                                                        <asp:Label runat="server" Text="Confirm Aadhar No." CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>
                                                                        <asp:TextBox ID="tbConfirmAdharNumber" runat="server" AutoPostBack="true" OnTextChanged="tbConfirmAdharNumber_TextChanged" CssClass="form-control form-control-sm" placeholder="Max 12 digits" MaxLength="12" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Enter Aadhar Number for confirmation"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers" TargetControlID="tbConfirmAdharNumber" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <hr />
                                                        <div class="row">
                                                            <div class="col-lg-8" style="border-right: 1px solid">
                                                                <asp:Panel ID="pnlDocument" runat="server" Visible="false">
                                                                    <h6 class="text-bold">Uploads - <span style="font-size: 9pt;">Document</span><span class="text-danger">*</span>
                                                                    </h6>
                                                                    <div class="row mt-3">
                                                                        <div class="col-lg-6" style="border-right: 1px solid #e6e6e6">
                                                                            <asp:Panel ID="pnlIDProofNew" runat="server">
                                                                                <div class="row">
                                                                                    <div class="col-lg-12">
                                                                                        <asp:Label runat="server" Text="ID Proof" CssClass="form-control-label"></asp:Label>
                                                                                    </div>
                                                                                    <div class="col-lg-12 mt-2">
                                                                                        <asp:RadioButtonList ID="rbtnIdProof" AutoPostBack="true" OnSelectedIndexChanged="rbtnIdProof_SelectedIndexChanged" GroupName="radio" CssClass="rbl" RepeatDirection="Vertical" runat="server">
                                                                                        </asp:RadioButtonList>

                                                                                        <asp:Panel ID="pnlIdProof" runat="server" Visible="false">
                                                                                            <div class="col-lg-12">
                                                                                                <label style="font-weight: normal; color: Red; font-size: 8pt; margin: 0px;">
                                                                                                    PDF file only(Max. Size 1MB)</label><br />
                                                                                                <asp:Button ID="btnUploadDoc" runat="server" OnClick="btnUploadDoc_Click" CausesValidation="False" Style="display: none" Text="" />
                                                                                                <asp:FileUpload ID="fileUpload" runat="server" CssClass="file-control" onchange="UploadDoc(this);" accept="application/pdf" />
                                                                                                <br />
                                                                                                <asp:Label runat="server" ID="lblIDpdf" CssClass="col-form-label control-label" Style="font-size: 10pt; font-weight: normal; color: #08a35b!important;"></asp:Label>
                                                                                                <br />
                                                                                                <asp:Label runat="server" Text="Document Id Number" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>
                                                                                                <br />
                                                                                                <asp:TextBox ID="txtidproofno" runat="server" CssClass="form-control form-control-sm" placeholder="Id Proof Number" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Enter Uploaded Document Id Number"></asp:TextBox>
                                                                                            </div>
                                                                                        </asp:Panel>
                                                                                    </div>
                                                                                </div>
                                                                            </asp:Panel>
                                                                        </div>
                                                                        <div class="col-lg-6">
                                                                            <asp:Panel ID="pnlAddProofNew" runat="server">
                                                                                <div class="row">
                                                                                    <div class="col-lg-12">
                                                                                        <asp:Label runat="server" Text="Address Proof" CssClass="form-control-label"></asp:Label>
                                                                                    </div>
                                                                                    <div class="col-lg-12 mt-2">
                                                                                        <asp:RadioButtonList ID="rbtnAddressProof" AutoPostBack="true" OnSelectedIndexChanged="rbtnAddressProof_SelectedIndexChanged" GroupName="radio" CssClass="rbl" RepeatDirection="Vertical" runat="server">
                                                                                        </asp:RadioButtonList>
                                                                                        <asp:Panel ID="pnlAddProof" runat="server" Visible="false">
                                                                                            <div class="col-lg-12">
                                                                                                <label style="font-weight: normal; color: Red; font-size: 8pt; margin: 0px;">
                                                                                                    PDF file only(Max. Size 1MB)</label><br />

                                                                                                <asp:Button ID="btnAddproof" runat="server" OnClick="btnAddproof_Click" CausesValidation="False" Style="display: none" Text="" />
                                                                                                <asp:FileUpload ID="fileaddproof" runat="server" CssClass="file-control" onchange="UploadAddDoc(this);" accept="application/pdf" />
                                                                                                <br />
                                                                                                <asp:Label runat="server" ID="lbladd" CssClass="col-form-label control-label" Style="font-size: 10pt; font-weight: normal; color: #08a35b!important;"></asp:Label>
                                                                                                <br />
                                                                                                <asp:Label runat="server" Text="Address Proof Id Number" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>
                                                                                                <br />
                                                                                                <asp:TextBox ID="txtaddressproofno" runat="server" CssClass="form-control form-control-sm" placeholder="Address Proof Number" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Enter Uploaded Document Id Number"></asp:TextBox>
                                                                                            </div>
                                                                                        </asp:Panel>
                                                                                    </div>
                                                                                </div>
                                                                            </asp:Panel>
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                            <div class="col-lg-4">
                                                                <asp:Panel ID="pnlPhoto" runat="server" Visible="false">
                                                                    <h6 class="text-bold">Uploads - <span style="font-size: 9pt;">Photo</span><span class="text-danger">*</span>  </h6>

                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" class="mt-3">
                                                                        <ContentTemplate>
                                                                            <asp:LinkButton ID="btntakephoto" runat="server" OnClick="btntakephoto_Click" TabIndex="21" Style="height: 34px; padding-top: 3px; width: 120px; font-size: 13pt; border-radius: 4px;"
                                                                                CssClass="btn btn-success btn-sm">
                                                                     <i class="fa fa-camera "></i> 
                                                                     Take Photo
                                                                            </asp:LinkButton>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="btntakephoto" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                    <asp:Image ID="ImgWebPortal" runat="server" Visible="false" Style="border-width: 0px; border-width: 0px; height: 80px; width: 80px; border: 2px solid #eaf4ff;" />
                                                                    <asp:LinkButton ID="lbtncloseWebImage" runat="server" OnClick="lbtncloseWebImage_Click" Style="font-size: 10pt; border-radius: 7px; margin-bottom: 52pt;" Visible="false" CssClass="btn btn-sm btn-danger"><i class="fa fa-times"></i></asp:LinkButton><br />
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                        <div class="row mt-5">
                                                            <div class="col-lg-12 text-center">
                                                                <asp:LinkButton ID="lbtnSave" runat="server" OnClick="lbtnSave_Click" CssClass="btn btn-success" data-toggle="tooltip" data-placement="bottom" title="Click to Save" Style="height: 42px;"><i class="fa fa-save"></i> Save & Proceed</asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnReset" runat="server" OnClick="lbtnReset_Click" CssClass="btn btn-danger" data-toggle="tooltip" data-placement="bottom" title="Click to Reset" Style="height: 42px;"><i class="fa fa-undo"></i> Reset</asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlPassRenew" runat="server" Visible="false" Style="min-height: 400px;">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <h6>Enter Old Pass No.<span class="text-danger">*</span></h6>
                                            <div class="row mt-3">
                                                <div class="col-lg-4">
                                                    <asp:TextBox ID="tbPassnNo" runat="server" CssClass="form-control" placeholder="Pass Number" MaxLength="15" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Pass No." Style="text-transform: uppercase;"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender runat="server" FilterType="UppercaseLetters,LowercaseLetters,Custom,Numbers" TargetControlID="tbPassnNo" />
                                                </div>
                                                <div class="col-lg-2">
                                                    <asp:LinkButton ID="lbtnSrchNwPsRqt" runat="server" OnClick="lbtnSrchNwPsRqt_Click" CssClass="btn btn-warning btn-sm"><i class="fa fa-search"></i> Search</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <hr />
                                    <asp:Panel ID="pnlPassDetails" runat="server" Visible="false">
                                        <div class="">
                                            <div class="">
                                            </div>
                                            <div class="">
                                                <div class="row">
                                                    <div class="col-lg-4">
                                                        <asp:Label runat="server" Text="Bus Pass Type" CssClass="form-control-label"></asp:Label><br />
                                                        <asp:Label ID="lblBusPassType" runat="server" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <h6 class="text-bold">Personal Details</h6>
                                                        <div class="row my-2">
                                                            <div class="col-lg-4">
                                                                <asp:Label runat="server" Text="Name" CssClass="form-control-label text-muted font-weight-normal"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                <br />
                                                                <asp:Label ID="lblName" runat="server" CssClass="lbld"></asp:Label>
                                                            </div>
                                                            <div class="col-lg-4">
                                                                <asp:Label runat="server" Text="Gender" CssClass="form-control-label text-muted font-weight-normal"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                <br />
                                                                <asp:Label ID="lblGender" runat="server" CssClass="lbld"></asp:Label>
                                                            </div>
                                                            <div class="col-lg-4">
                                                                <asp:Label runat="server" Text="Date of Birth" CssClass="form-control-label text-muted font-weight-normal"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                <br />
                                                                <asp:Label ID="lblDOB" runat="server" CssClass="lbld"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="row my-2">
                                                            <div class="col-lg-4">
                                                                <asp:Label runat="server" Text="Father Name" CssClass="form-control-label text-muted font-weight-normal"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                <br />
                                                                <asp:Label ID="lblfname" runat="server" CssClass="lbld"></asp:Label>
                                                            </div>
                                                            <div class="col-lg-4">
                                                                <asp:Label runat="server" Text="Service Type" CssClass="form-control-label text-muted font-weight-normal"></asp:Label><span class="text-danger HeadLabel">*</span><br />

                                                                <asp:Label ID="lblServiceTypeName" runat="server" CssClass="lbld"></asp:Label>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <h6 class="text-bold">Pass Requested For</h6>
                                                        <div class="row my-2">
                                                            <div class="col-lg-6">
                                                                <asp:Label runat="server" Text="Route" CssClass="form-control-label text-muted font-weight-normal"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                <br />
                                                                <asp:Label ID="lblRoute" runat="server" CssClass="lbld"></asp:Label>
                                                            </div>
                                                            <div class="col-lg-6">
                                                                <asp:Label runat="server" Text="Stations" CssClass="form-control-label text-muted font-weight-normal"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                <br />
                                                                <asp:Label ID="lblFrom" runat="server" CssClass="lbld"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row ">
                                                    <div class="col-lg-12">
                                                        <h6 class="text-bold">Contact Details</h6>
                                                        <div class="row my-2">
                                                            <div class="col-lg-4">
                                                                <asp:Label runat="server" Text="Mobile No." CssClass="form-control-label text-muted font-weight-normal"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                <br />
                                                                <asp:Label ID="lblMobileNo" runat="server" CssClass="lbld"></asp:Label>
                                                            </div>
                                                            <div class="col-lg-4">
                                                                <asp:Label runat="server" Text="Email" CssClass="form-control-label text-muted font-weight-normal"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                <br />
                                                                <asp:Label ID="lblEmail" runat="server" CssClass="lbld"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="row my-2">
                                                            <div class="col-lg-4">
                                                                <asp:Label runat="server" Text="State" CssClass="form-control-label text-muted font-weight-normal"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                <br />
                                                                <asp:Label ID="lblstateName" runat="server" CssClass="lbld"></asp:Label>

                                                            </div>
                                                            <div class="col-lg-4">
                                                                <asp:Label runat="server" Text="District" CssClass="form-control-label text-muted font-weight-normal"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                <br />
                                                                <asp:Label ID="lblDistrict" runat="server" CssClass="lbld"></asp:Label>
                                                            </div>
                                                            <div class="col-lg-4">
                                                                <asp:Label runat="server" Text="City" CssClass="form-control-label text-muted font-weight-normal"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                <br />
                                                                <asp:Label ID="lblCity" runat="server" CssClass="lbld"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="row my-2">
                                                            <div class="col-lg-8">
                                                                <asp:Label runat="server" Text="Address" CssClass="form-control-label text-muted font-weight-normal"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                <br />
                                                                <asp:Label ID="lblAddress" runat="server" CssClass="lbld" Text="N/A"></asp:Label>
                                                            </div>
                                                            <div class="col-lg-4">
                                                                <asp:Label runat="server" Text="Pincode" CssClass="form-control-label text-muted font-weight-normal"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                <br />
                                                                <asp:Label ID="lblPincode" runat="server" CssClass="lbld" Text="N/A"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row">
                                                    <h6 class="text-bold" style="margin-left: 15px;">Validity</h6>
                                                    <div class="col-lg-12">
                                                        <div class="row my-2">
                                                            <div class="col-lg-4">
                                                                <asp:Label runat="server" Text="From" CssClass="form-control-label text-muted font-weight-normal"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                <br />
                                                                <asp:Label ID="lblValidityFrom" runat="server" CssClass="lbld"></asp:Label>
                                                            </div>
                                                            <div class="col-lg-4">
                                                                <asp:Label runat="server" Text="Upto" CssClass="form-control-label text-muted font-weight-normal"></asp:Label><span class="text-danger HeadLabel">*</span>
                                                                <br />
                                                                <asp:Label ID="lblValidTo" runat="server" CssClass="lbld"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row">
                                                    <div class="col-lg-8" style="border-right: 1px solid">
                                                        <asp:Panel ID="pnlDocumentRenew" runat="server" Visible="true">
                                                            <h6 class="text-bold">Uploads - <span style="font-size: 9pt;">Document</span><span class="text-danger">*</span>
                                                            </h6>
                                                            <div class="row mt-3">
                                                                <div class="col-lg-6" style="border-right: 1px solid #e6e6e6">
                                                                    <asp:Panel ID="pnlIDProofReNew" runat="server">
                                                                        <div class="row">
                                                                            <div class="col-lg-12">
                                                                                <asp:Label runat="server" Text="ID Proof" CssClass="form-control-label"></asp:Label>
                                                                            </div>
                                                                            <div class="col-lg-12 mt-2">
                                                                                <asp:RadioButtonList ID="rbtnIdProofRenew" AutoPostBack="true" OnSelectedIndexChanged="rbtnIdProofRenew_SelectedIndexChanged" GroupName="radio" CssClass="rbl" RepeatDirection="Horizontal" runat="server">
                                                                                </asp:RadioButtonList>

                                                                                <asp:Panel ID="pnlIdProofUploadRenew" runat="server" Visible="false">
                                                                                    <div class="col-lg-12">
                                                                                        <label style="font-weight: normal; color: Red; font-size: 8pt; margin: 0px;">
                                                                                            PDF file only(Max. Size 1MB)</label><br />
                                                                                        <asp:Button ID="btnUploadDocRenew" runat="server" OnClick="btnUploadDocRenew_Click" CausesValidation="False" Style="display: none" Text="" />
                                                                                        <asp:FileUpload ID="fileUploadRenew" runat="server" CssClass="file-control" onchange="UploadDocRenew(this);" accept="application/pdf" />
                                                                                        <br />
                                                                                        <asp:Label runat="server" ID="lblIDpdfRenew" CssClass="col-form-label control-label" Style="font-size: 10pt; font-weight: normal; color: #08a35b!important;" Visible="false"></asp:Label>
                                                                                    </div>
                                                                                </asp:Panel>
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </div>
                                                                <div class="col-lg-6">
                                                                    <asp:Panel ID="pnlAddProofReNew" runat="server">
                                                                        <div class="row">
                                                                            <div class="col-lg-12">
                                                                                <asp:Label runat="server" Text="Address Proof" CssClass="form-control-label"></asp:Label>
                                                                            </div>
                                                                            <div class="col-lg-12 mt-2">
                                                                                <asp:RadioButtonList ID="rbtnAddressProofRenew" AutoPostBack="true" GroupName="radio" OnSelectedIndexChanged="rbtnAddressProofRenew_SelectedIndexChanged" CssClass="rbl" RepeatDirection="Horizontal" runat="server">
                                                                                </asp:RadioButtonList>
                                                                                <asp:Panel ID="pnlAddProofUploadRenew" runat="server" Visible="false">
                                                                                    <div class="col-lg-12">
                                                                                        <label style="font-weight: normal; color: Red; font-size: 8pt; margin: 0px;">
                                                                                            PDF file only(Max. Size 1MB)</label><br />

                                                                                        <asp:Button ID="btnAddproofRenew" runat="server" OnClick="btnAddproofRenew_Click" CausesValidation="False" Style="display: none" Text="" />
                                                                                        <asp:FileUpload ID="fileaddproofRenew" runat="server" CssClass="file-control" onchange="UploadAddDocRenew(this);" accept="application/pdf" />
                                                                                        <br />
                                                                                        <asp:Label runat="server" ID="lbladdpdfRenew" CssClass="col-form-label control-label" Style="font-size: 10pt; font-weight: normal; color: #08a35b!important;" Visible="false"></asp:Label>
                                                                                    </div>
                                                                                </asp:Panel>
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <asp:Panel ID="pnlPhoto1" runat="server" Visible="true">
                                                            <h6 class="text-bold">Uploads - <span style="font-size: 9pt;">Photo</span><span class="text-danger">*</span>  </h6>

                                                            <asp:UpdatePanel ID="UpdatePanel21" runat="server" class="mt-3">
                                                                <ContentTemplate>
                                                                    <asp:LinkButton ID="btntakephoto1" runat="server" OnClick="btntakephoto1_Click" TabIndex="21" Style="height: 34px; padding-top: 3px; width: 120px; font-size: 13pt; border-radius: 4px;"
                                                                        CssClass="btn btn-success btn-sm">
                                                                     <i class="fa fa-camera "></i> 
                                                                     Take Photo
                                                                    </asp:LinkButton>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="btntakephoto1" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                            <asp:Image ID="ImgWebPortal1" runat="server" Visible="false" Style="border-width: 0px; border-width: 0px; height: 80px; width: 80px; border: 2px solid #eaf4ff;" />
                                                            <asp:LinkButton ID="lbtncloseWebImage1" runat="server" Style="font-size: 10pt; border-radius: 7px; margin-bottom: 52pt;" Visible="false" CssClass="btn btn-sm btn-danger"><i class="fa fa-times"></i></asp:LinkButton><br />


                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                                <div class="row mt-5">
                                                    <div class="col-lg-12 text-center">
                                                        <asp:LinkButton ID="lbtnSaveRenew" runat="server" OnClick="lbtnSaveRenew_Click" CssClass="btn btn-success" data-toggle="tooltip" data-placement="bottom" title="Click to Save" Style="height: 42px;"><i class="fa fa-save"></i> Save & Proceed</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlPassNorecord" runat="server" Visible="true">
                                        <div class="row">
                                            <div class="col-md-12 busListBox text-center" style="color: #9d9797; padding-top: 50px; padding-bottom: 50px; font-weight: bold; font-size: 20px;">
                                                <asp:Label ID="lblmsg" runat="server" Text="Please Enter Valid Pass Number for Renew Your Pass"></asp:Label>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </asp:Panel>
                                <asp:Panel ID="pnlPassQuery" runat="server" Visible="false" Style="min-height: 200px;">
                                    <div class="row">
                                        <div class="col-lg-7 border-right">
                                            <div class=" text-capitalize" style="line-height: 16px;">
                                                <asp:Label ID="Label6" runat="server" class="h6">Generate Report</asp:Label><br />
                                                <span class="text-danger" style="font-size: 10pt;">Please select parameters to generate report (At a time for only 15 days the report can be generated)</span>
                                            </div>
                                            <div class="row m-0 mt-2">
                                                <div class="col-md-4">
                                                    <asp:Label runat="server" CssClass=" form-control-label card-title lbl">Request Type </asp:Label>
                                                    <asp:DropDownList ID="ddlrequesttype" runat="server" TabIndex="5" 
                                                        CssClass="form-control form-control-sm form-controlsm">
                                                        <asp:ListItem Value="A" Selected="True">All</asp:ListItem>
                                                        <asp:ListItem Value="N">New Pass</asp:ListItem>
                                                        <asp:ListItem Value="R">Renew Pass</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label runat="server" CssClass=" form-control-label card-title lbl">Pass Category</asp:Label>
                                                    <asp:DropDownList ID="ddlPassCategory" runat="server" TabIndex="5" 
                                                        CssClass="form-control form-control-sm form-controlsm">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label runat="server" CssClass=" form-control-label card-title lbl">Pass Type</asp:Label>
                                                    <asp:DropDownList ID="ddlPassType" runat="server" TabIndex="5" 
                                                        CssClass="form-control form-control-sm form-controlsm">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row m-0 mt-1">
                                                <div class="col-md-4">
                                                    <asp:Label runat="server" CssClass=" form-control-label card-title lbl">Status</asp:Label>
                                                    <asp:DropDownList ID="ddlstatus" runat="server" TabIndex="5" 
                                                        CssClass="form-control form-control-sm form-controlsm">
                                                        <asp:ListItem Value="0" Text="All" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="S" Text="Payment/Pending For Approved"></asp:ListItem>
                                                        <asp:ListItem Value="A" Text="Approved/Pass Issuance"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label runat="server" CssClass=" form-control-label card-title lbl">From Date</asp:Label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtfrmdate" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="DD/MM/YYYY" runat="server" MaxLength="10"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label runat="server" CssClass=" form-control-label card-title lbl">To Date</asp:Label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txttodate" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="DD/MM/YYYY" runat="server" MaxLength="10"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row m-0 mt-2">
                                                <div class="col-md-12 text-center">
                                                    <asp:LinkButton ID="lbtnReportview" ToolTip="View Pass Report" runat="server" OnClick="lbtnReportview_Click" CssClass="btn btn-success btn-sm">
                                            <i class="fa fa-search"></i> Search
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnReportdownload" ToolTip="Download Pass Report" OnClick="lbtnReportdownload_Click" Visible="false" runat="server" CssClass="btn btn-warning btn-sm">
                                            <i class="fa fa-download"></i> Download
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <hr />
                                            <asp:GridView ID="grvreport1" runat="server" PageSize="5" AutoGenerateColumns="False" GridLines="None" AllowSorting="true" AllowPaging="true" CssClass="table table-hover" OnPageIndexChanging="grvreport1_PageIndexChanging"
                                                HeaderStyle-CssClass="thead-light font-weight-bold" DataKeyNames="mst_number,first_name,source_stn,dest_stn,period_from,Period_to,apply_date,val_status,psngr_typename,fare_amount,val_route,total_fare_amount,total_tax,buspasscategoryname,apply_type,mobile_no,email_id">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Pass Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBUSPASS_CATEGORY_NAME" runat="server" Text='<%# Eval("buspasscategoryname") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblpsngrtypename" runat="server" Text='<%# Eval("psngr_typename") %>'></asp:Label>
                                                            <br />
                                                            <span>Ref. No.</span> -  
                                                            <asp:Label ID="lblMSTNUMBER" runat="server" Text='<%# Eval("mst_number") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblapplytype" runat="server" Text='<%# Eval("apply_type") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("val_status") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Traveller Info.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFIRSTNAME" runat="server" Text='<%# Eval("first_name") %>'></asp:Label><br />
                                                            <i class="fa fa-mobile"></i>
                                                            <asp:Label ID="lblMobileno" runat="server" Text='<%# Eval("mobile_no") %>'></asp:Label><br />
                                                            <i class="fa fa-envelope-o"></i>
                                                            <asp:Label ID="lblEmailid" runat="server" Text='<%# Eval("email_id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                            </asp:GridView>
                                            <div class="text-center busListBox" id="dvreport1" runat="server"
                                                style="padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold; color: #e3e3e3;"
                                                visible="true">
                                                Sorry, No Record Available 
                                            </div>
                                        </div>
                                        <div class="col-lg-5">
                                            <div class=" text-capitalize" style="line-height: 16px;">
                                                <asp:Label ID="Label4" runat="server" class="h6">Pass Transaction List (Instant Issue/After Approval) for print pass and print payment receipt</asp:Label>
                                            </div>
                                            <div class="row mt-2">

                                                <div class="col-lg-9">
                                                    <asp:Label runat="server" CssClass=" form-control-label card-title lbl">Name/Mobile/PassNumber/Ref. Number/Apply Date</asp:Label>
                                                    <asp:TextBox ID="tbvalue" runat="server" CssClass="form-control" placeholder="Name/Mobile/PassNumber/Ref. Number/Apply Date" MaxLength="30" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Enter Name"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-3" style="padding-top: 20px;">
                                                    <asp:LinkButton ID="lbtnsrchsumry" ToolTip="Search Pass/Pass Request" runat="server" OnClick="lbtnsrchsumry_Click" CssClass="btn btn-success btn-sm">
                                            <i class="fa fa-search"></i> 
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <hr />
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <asp:GridView ID="gvsmry" runat="server" PageSize="5" AutoGenerateColumns="False" GridLines="None" ShowHeader="false" AllowSorting="true" AllowPaging="true" CssClass="table table-hover table-responsive"
                                                        HeaderStyle-CssClass="thead-light font-weight-bold" DataKeyNames="currtnrefno,totalamount,cardtypename,passengertype,passnumber,currentstatus">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Pass Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNAME" runat="server" Text='<%# Eval("passengertype") %>'></asp:Label><br />
                                                                    <span>Ref. No.</span> -   
                                                <asp:Label ID="lblCURRTRANREFNO" runat="server" Text='<%# Eval("currtnrefno") %>'></asp:Label><br />
                                                                    <span>Pass No.</span>
                                                                    <asp:Label ID="lblPASSNUMBER" runat="server" Text='<%# Eval("passnumber") %>'></asp:Label><br />
                                                                    <span>Amt.Charged</span> - 
                                                <asp:Label ID="lblTOTALAMOUNT" runat="server" Text='<%# Eval("totalamount") %>'></asp:Label>
                                                                    <i class="fa fa-rupee"></i>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Traveller Info.">
                                                                <ItemTemplate>
                                                                    <span>Apply Date</span><br />
                                                                    <asp:Label ID="lblAPPLYONDATE" runat="server" Text='<%# Eval("applyon_date") %>'></asp:Label><br />
                                                                    <span>Validity</span><br />
                                                                    <asp:Label ID="lblPeriodFrom" runat="server" Text='<%# Eval("period_from") %>'></asp:Label>
                                                                    - 
                                                                    <asp:Label ID="lblPeriodTo" runat="server" Text='<%# Eval("period_to") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Traveller Info.">
                                                                <ItemTemplate>
                                                                    <span>Traveller Info.</span><br />
                                                                    <asp:Label ID="lblctzname" runat="server" Text='<%# Eval("ctz_name") %>'></asp:Label><br />
                                                                    <asp:Label ID="lblMobileno" runat="server" Text='<%# Eval("mobileno") %>'></asp:Label><br />
                                                                    <asp:Label ID="lblEmailid" runat="server" Text='<%# Eval("emailid") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                                    </asp:GridView>
                                                    <div class="text-center busListBox" id="dvsmry" runat="server"
                                                        style="padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold; color: #e3e3e3;"
                                                        visible="true">
                                                        Sorry, No Record Available 
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
                <%------------------------------------------------Start popup--%>
                <div class="row">
                    <cc1:ModalPopupExtender ID="mpInfo" runat="server" PopupControlID="PanelInfo" CancelControlID="LinkButtonMpInfoClose"
                        TargetControlID="Button6" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="PanelInfo" runat="server" Style="width: 500px !important">
                        <div class="card">
                            <div class="card-header">
                                <h4 class="card-title text-left mb-0">About Pass Type
                                </h4>
                            </div>
                            <div class="card-body text-left pt-2" style="min-height: 100px;">
                                <asp:Label ID="lblEligibilityInfo" runat="server"></asp:Label>
                                <asp:Label ID="lblIDDocumentsInfo" runat="server"></asp:Label>
                                <asp:Label ID="lbladdDocumentsInfo" runat="server"></asp:Label>
                                <asp:Label ID="lblChargesApplicableInfo" runat="server"></asp:Label>
                                <asp:Label ID="lblStateInfo" runat="server"></asp:Label>
                                <asp:Label ID="lblservicetypeInfo" runat="server"></asp:Label>
                                <asp:Label ID="lblvalidityInfo" runat="server"></asp:Label>
                                <div style="margin-top: 20px; text-align: right;">
                                    <asp:LinkButton ID="LinkButtonMpInfoClose" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                                </div>
                                <div style="visibility: hidden;">
                                    <asp:Button ID="Button6" runat="server" Text="" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="row">
                    <cc1:ModalPopupExtender ID="Mptakephoto" runat="server" CancelControlID="LinkButton2"
                        TargetControlID="Button5" PopupControlID="pnltakephoto" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnltakephoto" Style="display: none" runat="server">
                        <div class="card">
                            <div class="card-header">
                                <span>Take Photo</span>
                                <asp:LinkButton ID="LinkButton2" runat="server" UseSubmitBehavior="false" data-dismiss="modal"
                                    ToolTip="Close" Style="float: right; color: red; padding: 4px;">
                                <i class="fa fa-times fa-2x"></i></asp:LinkButton>
                            </div>
                            <div class="card-body" style="background: gainsboro;">
                                <table border="0" cellpadding="0" cellspacing="0">

                                    <tr>
                                        <td>
                                            <div id="webcam">
                                            </div>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td id="tdimg" style="display: none;">
                                            <asp:Image ID="imgCapture" runat="server" Style="visibility: hidden;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="padding-top: 10px;">
                                            <input type="button" id="btnCapture" value="Capture" cssclass="btn btn-success" style="width: 200px; font-size: 11pt; border-radius: 6px; margin: auto; display: block" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td align="center" id="tdimgbtn" style="display: none; padding-top: 10px;">
                                            <asp:Button ID="btnpic" Text="Upload" runat="server" OnClick="btnpic_Click" CssClass="btn btn-success" Style="width: 200px; visibility: hidden; font-size: 11pt; border-radius: 6px; margin: auto; display: block" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </asp:Panel>
                    <div style="visibility: hidden;">
                        <asp:Button ID="Button5" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <cc1:ModalPopupExtender ID="Mptakephoto1" runat="server" CancelControlID="LinkButton3"
                        TargetControlID="Button7" PopupControlID="pnltakephoto1" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnltakephoto1" Style="display: none" runat="server">
                        <div class="card">
                            <div class="card-header">
                                <span>Take Photo</span>
                                <asp:LinkButton ID="LinkButton3" runat="server" UseSubmitBehavior="false" data-dismiss="modal"
                                    ToolTip="Close" Style="float: right; color: red; padding: 4px;">
                                <i class="fa fa-times fa-2x"></i></asp:LinkButton>
                            </div>
                            <div class="card-body" style="background: gainsboro;">
                                <table border="0" cellpadding="0" cellspacing="0">

                                    <tr>
                                        <td>
                                            <div id="webcam1">
                                            </div>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td id="tdimg1" style="display: none;">
                                            <asp:Image ID="imgCapture1" runat="server" Style="visibility: hidden;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="padding-top: 10px;">
                                            <input type="button" id="btnCapture1" value="Capture" cssclass="btn btn-success" style="width: 200px; font-size: 11pt; border-radius: 6px; margin: auto; display: block" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td align="center" id="tdimgbtn1" style="display: none; padding-top: 10px;">
                                            <asp:Button ID="btnpic1" Text="Upload" runat="server" OnClick="btnpic1_Click" CssClass="btn btn-success" Style="width: 200px; visibility: hidden; font-size: 11pt; border-radius: 6px; margin: auto; display: block" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </asp:Panel>
                    <div style="visibility: hidden;">
                        <asp:Button ID="Button7" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation" CancelControlID="lbtnNoConfirmation"
                        TargetControlID="Button3" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlConfirmation" runat="server" Style="width: 500px !important">
                        <div class="card">
                            <div class="card-header">
                                <h4 class="card-title text-left mb-0">Please Confirm
                                </h4>
                            </div>
                            <div class="card-body text-left pt-2" style="min-height: 100px;">
                                <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>

                                <div style="margin-top: 20px; text-align: right;">
                                    <asp:LinkButton ID="lbtnYesConfirmation" runat="server" OnClick="lbtnYesConfirmation_Click" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                                </div>
                                <div style="visibility: hidden;">
                                    <asp:Button ID="Button3" runat="server" Text="" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="row">
                    <cc1:ModalPopupExtender ID="mperror" runat="server" PopupControlID="pnlerror" CancelControlID="LinkButton1"
                        TargetControlID="Button1" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlerror" runat="server" Style="width: 500px !important">
                        <div class="card">
                            <div class="card-header">
                                <h4 class="card-title text-left mb-0">Please Check
                                </h4>
                            </div>
                            <div class="card-body text-left pt-2" style="min-height: 100px;">
                                <asp:Label ID="lblerrormsg" runat="server"></asp:Label>
                                <div style="margin-top: 20px; text-align: right;">
                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                                </div>
                                <div style="visibility: hidden;">
                                    <asp:Button ID="Button1" runat="server" Text="" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="row">
                    <cc1:ModalPopupExtender ID="mpsuccess" runat="server" PopupControlID="pnlsuccess" CancelControlID="lbtnsuccessclose1"
                        TargetControlID="Button2" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlsuccess" runat="server" Style="width: 500px !important">
                        <div class="card">
                            <div class="card-header">
                                <h4 class="card-title text-left mb-0">Confirm
                                </h4>
                            </div>
                            <div class="card-body text-left pt-2" style="min-height: 100px;">
                                <asp:Label ID="lblsuccessmsg" runat="server"></asp:Label>
                                <div style="margin-top: 20px; text-align: right;">
                                    <asp:LinkButton ID="lbtnsuccessclose1" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                                </div>
                                <div style="visibility: hidden;">
                                    <asp:Button ID="Button2" runat="server" Text="" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <script src="../assets/js/WebCam.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            Webcam.set({
                width: 320,
                height: 240,
                image_format: 'jpeg',
                jpeg_quality: 90
            });
            Webcam.attach('#webcam');

            $("#btnCapture").click(function () {

                Webcam.snap(function (data_uri) {
                    $("[id*=imgCapture]").css("visibility", "visible")
                    $("[id*=btnpic]").css("visibility", "visible")
                    $("[id*=imgCapture]")[0].src = data_uri;
                    $("[id*=hdimg]").val(data_uri)
                    $("[id*=btnpic]").removeAttr("disabled");
                    $('#tdimg').show();
                    $('#tdimgbtn').show();
                });
            });
            Webcam.attach('#webcam1');
            $("#btnCapture1").click(function () {

                Webcam.snap(function (data_uri) {
                    $("[id*=imgCapture1]").css("visibility", "visible")
                    $("[id*=btnpic1]").css("visibility", "visible")
                    $("[id*=imgCapture1]")[0].src = data_uri;
                    $("[id*=hdimg1]").val(data_uri)
                    $("[id*=btnpic1]").removeAttr("disabled");
                    $('#tdimg1').show();
                    $('#tdimgbtn1').show();
                });
            });

        });
    </script>
</asp:Content>


