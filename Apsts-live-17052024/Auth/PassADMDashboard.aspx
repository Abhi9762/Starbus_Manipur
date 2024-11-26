<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/PassAdminmaster.master" AutoEventWireup="true" CodeFile="PassADMDashboard.aspx.cs" Inherits="Auth_PassADMDashboard" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <style type="text/css">
        .table th {
            padding: 0.25rem;
            font-size: 10pt;
        }

        .table td {
            padding: 0.25rem;
            font-size: 9pt;
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
            border-right: 1px solid #e6e6e6;
        }

        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .lbl {
            font-size: 10pt;
        }

        .form-controlsm {
            height: 28px !important;
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

            var currDate = new Date().getDate();
            var preDate = new Date(new Date().setDate(currDate - 1));
            var todayDate = new Date(new Date().setDate(currDate));

            $('[id*=txtfromdate]').datepicker({
                endDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            })



            $('[id*=txttodate]').datepicker({
                endDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            })

            $('[id*=txtapplydate]').datepicker({
                endDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            })


            $('[id*=txtissunacedate]').datepicker({
                endDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            })
            

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="container-fluid" style="padding-top: 20px; padding-bottom: 30px;">
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="row">
                        <div class="col-lg-5 border-right">
                            <div class="card-body pt-1">
                                <div class=" text-capitalize">
                                    <asp:Label ID="Label1" runat="server" class="h6">Today's Summary </asp:Label>
                                </div>

                                <div class="row m-0">
                                    <div class="col-md-4">
                                        <asp:Label runat="server" CssClass=" form-control-label card-title lbl">Request Type </asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label runat="server" CssClass=" form-control-label card-title float-right  lbl">Total </asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label runat="server" CssClass=" form-control-label card-title float-right  lbl">Pending </asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label runat="server" CssClass=" form-control-label card-title float-right  lbl">Approved </asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label runat="server" CssClass=" form-control-label card-title float-right lbl">Reject </asp:Label>
                                    </div>
                                </div>
                                <div class="row m-0">
                                    <div class="col-md-4">
                                        <asp:Label runat="server" CssClass=" form-control-label card-title  lbl">New </asp:Label>
                                    </div>
                                    <div class="col-md-2 border-right">
                                        <asp:Label ID="lbltotalnew" runat="server" CssClass="form-control-label float-right  lbl" data-toggle="tooltip" data-placement="bottom" title="Total New Bus Pass Request" Text="0"></asp:Label>
                                    </div>
                                    <div class="col-md-2 border-right">
                                        <asp:Label ID="lblpendingnew" runat="server" CssClass="form-control-label float-right  lbl" data-toggle="tooltip" data-placement="bottom" title="Total New Bus Pass Request Pending For Verification" Text="0"></asp:Label>
                                    </div>
                                    <div class="col-md-2 border-right">
                                        <asp:Label ID="lblacceptNew" runat="server" CssClass="form-control-label float-right lbl" data-toggle="tooltip" data-placement="bottom" title="Total Accept New Bus Pass Request" Text="0"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="lblrejectNew" runat="server" CssClass="form-control-label float-right lbl" data-toggle="tooltip" data-placement="bottom" title="Total Reject New Bus Pass Request" Text="0"></asp:Label>
                                    </div>
                                </div>
                                <div class="row m-0">
                                    <div class="col-md-4">
                                        <asp:Label runat="server" CssClass=" form-control-label card-title lbl">Renew </asp:Label>
                                    </div>
                                    <div class="col-md-2 border-right">
                                        <asp:Label ID="lbltotalrenew" runat="server" CssClass="form-control-label float-right lbl" data-toggle="tooltip" data-placement="bottom" title="Total Renew Bus Pass Request" Text="0"></asp:Label>
                                    </div>
                                    <div class="col-md-2 border-right">
                                        <asp:Label ID="lblpendingrenew" runat="server" CssClass="form-control-label float-right lbl" data-toggle="tooltip" data-placement="bottom" title="Total Renew Bus Pass Request Pending For Verification" Text="0"></asp:Label>
                                    </div>
                                    <div class="col-md-2 border-right">
                                        <asp:Label ID="lblacceptRenew" runat="server" CssClass="form-control-label float-right lbl" data-toggle="tooltip" data-placement="bottom" title="Total Accept Renew Bus Pass Request" Text="0"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="lblrejectRenew" runat="server" CssClass="form-control-label float-right lbl" data-toggle="tooltip" data-placement="bottom" title="Total Accept Renew Bus Pass Request" Text="0"></asp:Label>
                                    </div>
                                </div>
                                <div class="row m-0 mt-1 pt-1" style="border-top: 1px solid #e6e6e6;">
                                    <div class="col-md-4">
                                        <asp:Label runat="server" CssClass=" form-control-label card-title lbl">Instant Issuance</asp:Label>
                                    </div>
                                    <div class="col-md-2 border-right">
                                        <asp:Label ID="lblintantissue" runat="server" CssClass="form-control-label float-right lbl" data-toggle="tooltip" data-placement="bottom" title="Total Alert/Notice" Text="0"></asp:Label>
                                    </div>
                                    <div class="col-md-4 border-right">
                                        <asp:Label runat="server" CssClass=" form-control-label card-title lbl">Duplicate Download</asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="lblduplicatedwnload" runat="server" CssClass="form-control-label float-right lbl" data-toggle="tooltip" data-placement="bottom" title="Total Alert/Notice" Text="0"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-7">
                            <div class="card-body  pt-1">
                                <div class=" text-capitalize" style="line-height: 16px;">
                                    <asp:Label ID="Label6" runat="server" class="h6">Generate Report</asp:Label><br />
                                    <span class="text-danger" style="font-size: 10pt;">Please select parameters to generate report (At a time for only 15 days the report can be generated)</span>
                                </div>
                                <div class="row m-0 mt-1">
                                    <div class="col-md-3">
                                        <asp:Label runat="server" CssClass=" form-control-label card-title lbl">Request Type </asp:Label>
                                        <asp:DropDownList ID="ddlrequesttype" runat="server" TabIndex="5" AutoPostBack="true"
                                            CssClass="form-control form-control-sm form-controlsm">
                                            <asp:ListItem Value="A" Selected="True">All</asp:ListItem>
                                            <asp:ListItem Value="N">New Pass</asp:ListItem>
                                            <asp:ListItem Value="R">Renew Pass</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label runat="server" CssClass=" form-control-label card-title lbl">Pass Category</asp:Label>
                                        <asp:DropDownList ID="ddlPassCategory" runat="server" TabIndex="5" AutoPostBack="true"
                                            CssClass="form-control form-control-sm form-controlsm">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label runat="server" CssClass=" form-control-label card-title lbl">Pass Type</asp:Label>
                                        <asp:DropDownList ID="ddlPassType" runat="server" TabIndex="5" AutoPostBack="true"
                                            CssClass="form-control form-control-sm form-controlsm">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label runat="server" CssClass=" form-control-label card-title lbl">Status</asp:Label>
                                        <asp:DropDownList ID="ddlstatus" runat="server" TabIndex="5" AutoPostBack="true"
                                            CssClass="form-control form-control-sm form-controlsm">
                                            <asp:ListItem Value="0" Text="All" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="S" Text="Payment/Pending For Approved"></asp:ListItem>
                                            <asp:ListItem Value="A" Text="Approved/Pass Issuance"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row m-0 mt-1">
                                    <div class="col-md-3">
                                        <asp:Label runat="server" CssClass=" form-control-label card-title lbl">From Date</asp:Label>
                                        <div class="input-group date">
                                            <asp:TextBox ID="txtfromdate" ToolTip="Enter From Date" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="txtfromdate" ValidChars="/" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label runat="server" CssClass=" form-control-label card-title lbl">To Date</asp:Label>
                                        <div class="input-group date">
                                            <asp:TextBox ID="txttodate" ToolTip="Enter To Date" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="txttodate" ValidChars="/" />
                                        </div>
                                    </div>
                                    <div class="col-md-3" style="padding-top: 23px;">
                                        <asp:LinkButton ID="lbtndownload" ToolTip="Download" runat="server" OnClick="lbtndownload_Click" CssClass="btn btn-success btn-sm">
                                            <i class="fa fa-download"></i> Download
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-6 col-md-6">
                <div class="card" style="min-height: 100vh">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row mt-1" style="border-bottom: 1px solid #e6e6e6;">
                                    <div class="col-md-4 text-left">
                                        <h5>Pending Pass Request </h5>
                                    </div>
                                    <div class="col-md-8 text-right">
                                        <div class="row">
                                            <div class="col-lg-4 border-right">
                                                <span class=" form-control-label card-title lbl pr-1">Total</span>
                                                <asp:Label ID="lbltotalrequest" runat="server" Text="0"
                                                    ToolTip="Total Pass Request" CssClass="font-weight-bold mb-0 text-right lbl"></asp:Label>
                                            </div>
                                            <div class="col-lg-4 border-right">
                                                <span class=" form-control-label card-title lbl pr-1">New </span>
                                                <asp:Label ID="lblnewrequest" runat="server" Text="0"
                                                    ToolTip="New Pass Request" CssClass="font-weight-bold mb-0 text-right lbl"></asp:Label>
                                            </div>
                                            <div class="col-lg-4">
                                                <span class=" form-control-label card-title lbl pr-1">Renew </span>
                                                <asp:Label ID="lblrenewrequest" runat="server" Text="0"
                                                    ToolTip="Renew Pass Request" CssClass="font-weight-bold mb-0 text-right lbl"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="row mt-1">
                                    <div class="col-md-2">
                                        <asp:Label runat="server" CssClass=" form-control-label card-title lbl">Request Type </asp:Label>
                                        <asp:DropDownList ID="ddlPendingRequesttype" runat="server" AutoPostBack="true"
                                            CssClass="form-control form-control-sm form-controlsm">
                                            <asp:ListItem Value="N" Selected="True">New Pass</asp:ListItem>
                                            <asp:ListItem Value="R">Renew Pass</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label runat="server" CssClass=" form-control-label card-title lbl">Pass Type </asp:Label>
                                        <asp:DropDownList ID="ddlPendingPasstype" runat="server" AutoPostBack="true"
                                            CssClass="form-control form-control-sm form-controlsm">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label runat="server" CssClass=" form-control-label card-title lbl">Apply Date </asp:Label>
                                        <div class="input-group date">
                                            <asp:TextBox ID="txtapplydate" ToolTip="Enter From Date" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="txtapplydate" ValidChars="/" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label runat="server" CssClass=" form-control-label card-title lbl">Refrence No. </asp:Label>
                                        <div class="input-group">
                                            <asp:TextBox CssClass="form-control form-controlsm" runat="server" ID="txtRefNo" MaxLength="30" ToolTip="Enter Pass Ref. No."
                                                placeholder="Ref No." Text="" Style="display: inline;"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="UppercaseLetters,Numbers,Custom"
                                                TargetControlID="txtRefNo" ValidChars="_" />
                                        </div>
                                    </div>
                                    <div class="col-md-2" style="padding-top: 23px;">
                                        <asp:LinkButton ID="lbtnapplytxn" ToolTip="Search Pass Request For Approval"  runat="server" OnClick="lbtnapplytxn_Click" CssClass="btn btn-success btn-sm">
                                            <i class="fa fa-search"></i> 
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnresetapplytxn" ToolTip="Reset Pass Request For Approval" runat="server" OnClick="lbtnresetapplytxn_Click" CssClass="btn btn-warning btn-sm">
                                            <i class="fa fa-undo"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:GridView ID="gvPassRequest" runat="server" PageSize="5"  AutoGenerateColumns="False" GridLines="None" AllowSorting="true" ClientIDMode="Static" AllowPaging="false" CssClass="table table-hover table-responsive"
                                    HeaderStyle-CssClass="thead-light font-weight-bold" DataKeyNames="currtranrefno,totalamount,buspass_category_name,buspasstype_name,passnumber" OnRowCommand="gvPassRequest_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Request Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTRANSTYPE" runat="server" Text='<%# Eval("transtype_") %>'></asp:Label>
                                                <br />
                                                <asp:Label ID="lblapplytype" runat="server" Text='<%# Eval("aaplytype") %>'></asp:Label>
                                                <br />
                                                <asp:Label ID="lblapplydatetime" runat="server" Text='<%# Eval("apply_date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pass Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBUSPASS_CATEGORY_NAME" runat="server" Text='<%# Eval("buspass_category_name") %>'></asp:Label>
                                                <br />
                                                <asp:Label ID="lblNAME" runat="server" Text='<%# Eval("buspasstype_name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Traveller Info.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTraveller_NAME" runat="server" Text='<%# Eval("travellername") %>'></asp:Label><br />
                                                <i class="fa fa-mobile"></i>
                                                <asp:Label ID="lbllblTraveller_Mobile" runat="server" Text='<%# Eval("mobileno") %>'></asp:Label><br />
                                                <i class="fa fa-envelope"></i>
                                                <asp:Label ID="lblTraveller_Email" runat="server" Text='<%# Eval("emailid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ref. Number">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCURRTRANREFNO" runat="server" Text='<%# Eval("currtranrefno") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amt. Charged">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTOTALAMOUNT" runat="server" Text='<%# Eval("totalamount") %>'></asp:Label>
                                                <i class="fa fa-rupee"></i>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnView" ToolTip="View Request Details For Approved and Reject" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' runat="server" CssClass="btn btn-success btn-sm" Style="padding: 4px;"><i class="fa fa-eye" title="View Request Details For Approved and Reject">&nbsp;</i></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnprintreceipt" runat="server" CssClass="btn btn-warning btn-sm" CommandName="PrintReceipt" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Click here to Print Pass Payment Receipt"> <i class="fa fa-rupee-sign" title="Click here to Print Pass Payment Receipt"></i> </asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                </asp:GridView>
                                <div class="text-center busListBox" id="dvPassRequest" runat="server"
                                    style="padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold; color: #e3e3e3;"
                                    visible="true">
                                    No Pass Request Transaction Pending for Verification 
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="card" style="min-height: 100vh">
                    <div class="card-header">
                        <div class=" text-capitalize" style="line-height: 16px;">
                            <asp:Label ID="Label4" runat="server" class="h6">Pass Transaction List (Instant Issue/After Approval) for print pass and print payment receipt</asp:Label>

                        </div>

                        <div class="row mt-1">
                            <div class="col-md-2">
                                <asp:Label runat="server" CssClass=" form-control-label card-title lbl">Transaction Date </asp:Label>
                                 <div class="input-group date">
                                            <asp:TextBox ID="txtissunacedate" ToolTip="Enter From Date" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="txtissunacedate" ValidChars="/" />
                                        </div>
                            </div>
                            <div class="col-md-2">
                                <asp:Label runat="server" CssClass=" form-control-label card-title lbl">Issuance Type </asp:Label>
                                <asp:DropDownList ID="ddlIssuanceType" runat="server"
                                    CssClass="form-control form-control-sm form-controlsm">
                                    <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                    <asp:ListItem Value="I">Instant Issue</asp:ListItem>
                                    <asp:ListItem Value="A">After Approval</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <asp:Label runat="server" CssClass=" form-control-label card-title lbl">Pass Type </asp:Label>
                                <asp:DropDownList ID="ddlsearchpasstype" runat="server" AutoPostBack="true"
                                    CssClass="form-control form-control-sm form-controlsm">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <asp:Label runat="server" CssClass=" form-control-label card-title lbl">Refrence No./Name/Mobile/Email</asp:Label>
                                <div class="input-group">
                                    <asp:TextBox CssClass="form-control form-controlsm" runat="server" ID="txtapprovalrefno" MaxLength="30" ToolTip="Enter Pass Ref. No."
                                        placeholder="Refrence No./Name/Mobile/Email" Text="" Style="display: inline;"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="UppercaseLetters,LowercaseLetters,Numbers,Custom"
                                        TargetControlID="txtapprovalrefno" ValidChars="_ @ ." />
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-top: 23px;">
                                <asp:LinkButton ID="btnsearch" ToolTip="Search Pass/Pass Request" runat="server" OnClick="btnsearch_Click" CssClass="btn btn-success btn-sm">
                                            <i class="fa fa-search"></i> 
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbtnsearchDownload" ToolTip="Download Pass Report" OnClick="lbtnsearchDownload_Click" Visible="false" runat="server" CssClass="btn btn-warning btn-sm">
                                            <i class="fa fa-download"></i> 
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbtnresetsearch" ToolTip="Reset Search Pass/Pass Request" runat="server" OnClick="lbtnresetsearch_Click" CssClass="btn btn-danger btn-sm">
                                            <i class="fa fa-undo"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body pt-2">

                        <div class="row mt-3">
                            <div class="col-lg-12">
                                <asp:GridView ID="grdsearchpass" runat="server" PageSize="5" AutoGenerateColumns="False" GridLines="None" AllowSorting="true" ClientIDMode="Static" AllowPaging="false" ShowHeader="true" CssClass="table table-hover"
                                    HeaderStyle-CssClass="thead-light font-weight-bold" OnRowCommand="grdsearchpass_RowCommand" DataKeyNames="currtranref_no,total_amount,card_type_name,psngr_type_name,pass_number,current_status,pass_no">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Pass Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBUSPASS_CATEGORY_NAME" runat="server" Text='<%# Eval("card_type_name") %>'></asp:Label>
                                                <br />
                                                <asp:Label ID="lblNAME" runat="server" Text='<%# Eval("psngr_type_name") %>'></asp:Label>
                                                <br />
                                                <span>Ref. No.</span>
                                                <asp:Label ID="lblCURRTRANREFNO" runat="server" Text='<%# Eval("currtranref_no") %>'></asp:Label>
                                                <br />
                                                <asp:Label ID="lblPASSNUMBER" runat="server" Text='<%# Eval("pass_number") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Request Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTRANSTYPE" runat="server" Text='<%# Eval("trans_type") %>'></asp:Label>
                                                <br />
                                                <asp:Label ID="lblISSUENCETYPE" runat="server" Text='<%# Eval("issuence_type") %>'></asp:Label><br />
                                                <asp:Label ID="lblapplytype" runat="server" Text='<%# Eval("apply_type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pass Amt.">
                                            <ItemTemplate>
                                                <span>Amt.Charged</span><br />
                                                <asp:Label ID="lblTOTALAMOUNT" runat="server" Text='<%# Eval("total_amount") %>'></asp:Label>
                                                <i class="fa fa-rupee"></i>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Traveller Info.">
                                            <ItemTemplate>
                                                <span>Traveller Info.</span><br />
                                                <asp:Label ID="lblTraveller_NAME" runat="server" Text='<%# Eval("ctz_name") %>'></asp:Label><br />
                                                <i class="fa fa-mobile"></i>
                                                <asp:Label ID="lbllblTraveller_Mobile" runat="server" Text='<%# Eval("mobile_no") %>'></asp:Label><br />
                                                <i class="fa fa-envelope-o"></i>
                                                <asp:Label ID="lblTraveller_Email" runat="server" Text='<%# Eval("email_id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnprintpass" runat="server" CssClass="btn btn-success btn-sm" CommandName="PrintPass" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Click here to Print Pass"> <i class="fa fa-print" title="Click here to Print Pass"></i> </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnprintreceipt" runat="server" CssClass="btn btn-warning btn-sm" CommandName="PrintReceipt" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Click here to Print Pass Payment Receipt"> <i class="fa fa-rupee-sign" title="Click here to Print Pass Payment Receipt"></i> </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnview" runat="server" CssClass="btn btn-primary btn-sm" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Click here to View Pass/Transaction"> <i class="fa fa-info" title="Click here to View Pass/Transaction "></i> </asp:LinkButton>
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
        </div>

        <div class="row">
            <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
                CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h6 class="card-title text-left mb-0">Please Confirm
                        </h6>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnYesConfirmation" runat="server" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
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
            <cc1:ModalPopupExtender ID="mpsuccess" runat="server" PopupControlID="pnlsuccess"
                CancelControlID="lbtnsuccessclose1" TargetControlID="Button6" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlsuccess" runat="server" Style="position: fixed;">
                <div class="card" style="min-width: 350px;">
                    <div class="card-header">
                        <h6 class="card-title">Confirm
                        </h6>
                    </div>
                    <div class="card-body" style="min-height: 100px;">
                        <asp:Label ID="lblsuccessmsg" runat="server"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnsuccessclose1" runat="server" CssClass="btn btn-success"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button6" runat="server" Text="" />
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
            <cc1:ModalPopupExtender ID="mpPass" runat="server" PopupControlID="pnlPass" TargetControlID="Button22"
                CancelControlID="lbtnClose" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlPass" runat="server" Style="position: fixed;">
                <div class="modal-content mt-5">
                    <div class="modal-header">
                        <div class="col-md-10">
                            <h3 class="m-0">Bus Pass</h3>
                        </div>


                         <div class="col-md-2 text-right">
                            <asp:LinkButton ID="lbtnClose" runat="server" Style="color: Red; cursor: pointer;"><i class="fa fa-times" style="font-size:26pt;" ></i></asp:LinkButton>
                        </div>
                       
                    </div>
                    <div class="modal-body p-0">
                        <embed src="../Bus_Pass.aspx" style="height: 70vh; width: 63vw" />
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button22" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpPassInfo" runat="server" PopupControlID="pnlPassInfo" TargetControlID="Button9"
                CancelControlID="LinkButton75" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlPassInfo" runat="server" Style="position: fixed;">
                <div class="modal-content mt-5">
                    <div class="modal-header">
                        <div class="col-md-10">
                            <h3 class="m-0">Bus Pass/Transaction Detail</h3>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="LinkButton2" runat="server" Style="color: Red; cursor: pointer;"><i class="fa fa-times" style="font-size:26pt;" ></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="modal-body p-0">
                        <embed src="../Auth/dashpass.aspx" style="height: 85vh; width: 80vw" />
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button9" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mppassreceipt" runat="server" PopupControlID="pnlpassreceipt" TargetControlID="Button3"
                CancelControlID="lbtnreiptclose" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlpassreceipt" runat="server" Style="position: fixed;">
                <div class="modal-content mt-5">
                    <div class="modal-header">
                        <div class="col-md-10">
                            <h3 class="m-0">Bus Pass Apply Receipt</h3>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="lbtnreiptclose" runat="server" Style="color: Red; cursor: pointer;"><i class="fa fa-times" style="font-size:26pt;" ></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="modal-body p-0">
                        <embed src="../Pass_reciept.aspx" style="height: 190px; width: 50vw;" />
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button3" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
    </div>

    <script type="text/javascript">
        $("[id*=gvPassRequest]").DataTable(
            {
                //dom: 'lBfrtip<"actions">',
                bLengthChange: true,
                lengthMenu: [[10, 15, -1], [10, 15, "All"]],
                bFilter: true,
                bSort: true,
                bPaginate: true
                //,                buttons: [
                //    'excel', 'pdf', 'print'
                //]
            });
        $("[id*=grdsearchpass]").DataTable(
            {
                //dom: 'lBfrtip<"actions">',
                bLengthChange: true,
                lengthMenu: [[10, 15, -1], [10, 15, "All"]],
                bFilter: true,
                bSort: true,
                bPaginate: true
                //,                buttons: [
                //    'excel', 'pdf', 'print'
                //]
            });
    </script>
</asp:Content>



