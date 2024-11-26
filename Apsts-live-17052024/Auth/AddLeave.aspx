﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" EnableEventValidation="true" AutoEventWireup="true" CodeFile="AddLeave.aspx.cs" Inherits="Auth_AddLeave" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="assets/Charts/chart.js/chart.min.js"></script>
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
        function UploadImage(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnUploadImage.ClientID %>").click();
        }
    }
    function UploadImage2(fileUpload) {
        if (fileUpload.value != '') {
            document.getElementById("<%=btnUploadImage2.ClientID %>").click();
        }
    }
    </script>
    <style type="text/css">
        .form-control {
            font-size: 10pt;
            height: calc(2rem + 2px) !important;
        }

        .table2 td, .table2 th {
            padding: 5px 3px;
            vertical-align: top;
            border-top: none;
            border-bottom: 1px solid #e4e2e2;
        }

        .table2 td, .table2 th {
            border: none !important;
        }

        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .tblCenter {
            width: 70%;
            margin-left: auto;
            margin-right: auto;
            text-align: left;
        }

        .textbox:disabled, .textbox[readonly] {
            background-color: #e9ecef;
            opacity: 1;
        }

        .table td, .table th {
            padding: 3px 5px;
            vertical-align: top;
            border-top: none;
        }

        .textbox {
            height: calc(2rem + 2px);
            padding: .375rem .75rem;
            font-size: 10pt;
            line-height: 1.5;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            border-radius: .25rem;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }

        .custom-tab .nav-tabs > .active > a:focus, .custom-tab .nav-tabs > a.active, .custom-tab .nav-tabs > li.active > a:hover {
            border-color: transparent transparent;
            color: white;
            background: #4884d8;
            position: relative;
            border-top-right-radius: 20px;
        }

        .headerCss {
            font-weight: bold;
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

        .content {
            padding: 0 30px;
        }

        .input-group-addon, .input-group-btn {
            white-space: nowrap;
            /*vertical-align: inherit;*/
        }

        .textbox {
            height: calc(2rem + 2px);
            padding: .375rem .75rem;
            font-size: 10pt;
            line-height: 1.5;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            border-radius: .25rem;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }

        .spanMendatory {
            font-size: 10pt;
            color: #dc3545 !important;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#grvEmployees').DataTable(
                 {
                     bLengthChange: true,
                     lengthMenu: [[15, 20, -1], [15, 20, "All"]],
                     bFilter: true,
                     bSort: true,
                     bPaginate: true
                     //,dom: 'Bfrtip',
                     //buttons: ['pdf', 'excel', 'csv']
                 });
        });
    </script>


      <script type="text/javascript">

          $(document).ready(function () {

              var currDate = new Date().getDate();
              var preDate = new Date(new Date().setDate(currDate - 1));
              var todayDate = new Date(new Date().setDate(currDate));

              $('[id*=txtFromdate]').datepicker({
                  changeMonth: true,
                  changeYear: false,
                  format: "dd/mm/yyyy",
                  autoclose: true
              })



              $('[id*=txtToDate]').datepicker({
                  changeMonth: true,
                  changeYear: false,
                  format: "dd/mm/yyyy",
                  autoclose: true
              })

          });
      </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidtoken" runat="server" />

    <div class="container-fluid" style="padding-top: 20px">
        <div class="row">
            <div class="col-lg-3 p-0" style="min-height: 500px">
                <div class="row ml-0">
                    <div class="col-md-12" style="">
                        <div class="card shadow" style="min-height: 10vh; margin-bottom: 10px;">
                            <asp:LinkButton ID="LinkButton1" runat="server" Style="color: red;" CommandName="P">
                                <div class="card-body" style="padding: 10px; text-align: center">
                                    <div class="chart-container" style="min-height: 180px;">
                                        <asp:Literal ID="ltpieChartBookingMode" runat="server"></asp:Literal>
                                        <asp:Image ID="ImgpieChartBookingModeNOdata" runat="server" src="../citizenAuth/assets/img/no_data.png"
                                            Visible="false" class="mt-3 ml-2" Style="opacity: 0.6;" Width="80%" Height="180px" />
                                    </div>
                                </div>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="col-md-6 pr-1">
                        <div class="card shadow" style="min-height: 10vh; margin-bottom: 10px">
                            <div class="card-body" style="padding: 10px; text-align: center; height: 85px">

                                <div class="col-lg-12">
                                    <asp:LinkButton ID="lbtnTotalEmployee" runat="server" Text="0" OnClick="lbtnTotalEmployee_Click" Style="color: green;" Font-Underline="true" Font-Size="16pt" Font-Bold="true" CommandName="O"></asp:LinkButton>
                                    <br />
                                    <label for="ddhead" style="line-height: 18px;">
                                        Total
                                <br />
                                        <br />
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 pl-1">
                        <div class="card shadow" style="min-height: 10vh; margin-bottom: 10px">
                            <div class="card-body" style="padding: 10px; text-align: center; height: 85px">

                                <div class="col-lg-12">
                                    <asp:LinkButton ID="lbtnLeaveApplied" runat="server" Text="0" Style="color: red;" Font-Underline="true" Font-Size="16pt" Font-Bold="true" CommandName="N"></asp:LinkButton>
                                    <br />
                                    <label for="ddhead" style="line-height: 18px;">
                                        Leave Applied
                                <br />
                                        <br />
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 pr-1">
                        <div class="card shadow" style="min-height: 10vh; margin-bottom: 10px">
                            <div class="card-body" style="padding: 10px; text-align: center; height: 85px">

                                <div class="col-lg-12">
                                    <asp:LinkButton ID="lbtnPendingForApproval" runat="server" Text="0" Style="color: green;" Font-Underline="true" Font-Size="16pt" Font-Bold="true" CommandName="P"></asp:LinkButton>
                                    <br />
                                    <label for="ddhead" style="line-height: 18px;">
                                        Pending For Approval
                                <br />
                                        <br />
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 pl-1">
                        <div class="card shadow" style="min-height: 10vh; margin-bottom: 10px">
                            <div class="card-body" style="padding: 10px; text-align: center; height: 85px">

                                <div class="col-lg-12">
                                    <asp:LinkButton ID="lbtnApprovedLeave" runat="server" Text="0" Style="color: red;" Font-Underline="true" Font-Size="16pt" Font-Bold="true" CommandName="A"></asp:LinkButton>
                                    <br />
                                    <label for="ddhead" style="line-height: 18px;">
                                        Approved Leave
                                <br />
                                        <br />
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-9" style="min-height: 500px">
                <div class="card" style="font-size: 12px; min-height: 500px;">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-md-4 pt-1">
                                <span style="font-size: 13pt; font-weight: 600;">Add / Extend and Cancel
                                    <br />
                                    Employee Leave</span>
                            </div>
                            <div class="col-md-2">
                                <span style="font-size: 12pt; font-weight: 600; text-align: right;">Employee Type</span><br />
                                <asp:DropDownList ID="ddlEmptype" runat="server" OnSelectedIndexChanged="ddlEmptype_SelectedIndexChanged" ToolTip="Role" AutoPostBack="true"
                                    class="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <span style="font-size: 12pt; font-weight: 600; text-align: right;">Duty Type</span><br />
                                <asp:DropDownList ID="ddldutytype" runat="server" OnSelectedIndexChanged="ddldutytype_SelectedIndexChanged" ToolTip="Duty Type" AutoPostBack="true"
                                    class="form-control">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-2 pl-0 pt-3 mt-2">
                                <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click" class="btn btn-success btn-sm" ToolTip="Search" Style="border-radius: 4px; font-size: 10pt;"> <i class="fa fa-search"></i></asp:LinkButton>
                                <asp:LinkButton ID="lbtnResetFilter" runat="server" OnClick="lbtnResetFilter_Click" class="btn btn-warning btn-sm" ToolTip="Reset" Style="border-radius: 4px; font-size: 10pt;"> <i class="fa fa-sync"></i></asp:LinkButton>
                                <asp:LinkButton ID="lbtnDownloadExcel" Visible="true" OnClick="lbtnDownloadExcel_Click" runat="server" CssClass="btn btn-danger btn-sm"
                                    Style="border-radius: 4px; font-size: 10pt;" ToolTip="Download"> <i class="fa fa-download"></i></asp:LinkButton>
                            </div>
                            <div class="col-md-2 pl-0 pt-2 mt-2 text-right">
                                <asp:LinkButton ID="LinkButtonInfo" runat="server" OnClick="LinkButtonInfo_Click" class="btn"
                                    Style="margin-top: 0px; font-size: 20px; margin-bottom: 4px; color: #dc3545; line-height: 19px;" ToolTip="View Instructions">
                                        help <i class="fa fa-info-circle" ></i> </asp:LinkButton>
                            </div>
                        </div>

                    </div>
                    <div class="card-body px-0 pt-0">
                        <div class="custom-tab">
                            <div class="tab-content pl-3 pr-3 pt-2">
                                <div>
                                    <div class="row">
                                        <div class="col-lg-12 pb-2">
                                            <div class="col-md-12 text-right" style="font-size: 9pt">
                                                <asp:Label Text="text" runat="server" CssClass="btn btn-sm btn-primary btn-label">L</asp:Label>
                                                Mark Leave,
                    <asp:Label Text="text" runat="server" CssClass="btn btn-sm btn-warning btn-label">E</asp:Label>
                                                Extent Leave,
                    <asp:Label Text="text" runat="server" CssClass="btn btn-sm btn-danger btn-label">C</asp:Label>
                                                Cancel Leave,
                    <asp:Label Text="text" runat="server" CssClass="btn btn-sm btn-info btn-label">D</asp:Label>
                                                View Monthly Calender
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row text-left">
                                        <div class="col-md-12 mt-3">
                                            <asp:Panel ID="pnlNoRecord" runat="server" Width="100%" Visible="true">
                                                <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                                    <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
                                                        Employee's not available<br />
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:GridView ID="grvEmployees" runat="server" AutoGenerateColumns="false" GridLines="None" Font-Size="10pt"
                                                ClientIDMode="Static" AllowPaging="false" PageSize="20" CssClass="table table-hover table-striped" OnRowCommand="grvEmployees_RowCommand" OnRowDataBound="grvEmployees_RowDataBound"
                                                DataKeyNames="empcode_,empname_,mobilenumber_,empdesignationcode,designation_name_,rolecode_,typename_,dutytypename_,draft_,
                                                approved_,leaverefno_,leavestartdate_,leaveenddate,reason_,attachment_,leavetypeid_,leave_typename_">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="class-on-element">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee" ItemStyle-CssClass="class-on-element">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblempname" runat="server" Text='<%# Eval("empname_") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Role (designation)" ItemStyle-CssClass="class-on-element">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblROLE" runat="server" Text='<%# Eval("rolecode_") %>'></asp:Label>
                                                            (<asp:Label ID="lblDESIGNATIONNAME" runat="server" Text='<%# Eval("designation_name_") %>'></asp:Label>)
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Type (Duty Type)" ItemStyle-CssClass="class-on-element">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTYPENAME" runat="server" Text='<%# Eval("typename_") %>'></asp:Label>
                                                           (<asp:Label ID="lblDUTYTYPE_NAME" runat="server" Text='<%# Eval("dutytypename_") %>'></asp:Label>)
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ItemStyle-CssClass="class-on-element">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnAddLeave" CommandName="markLeave" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                Visible="true" runat="server" CssClass="btn btn-sm btn-primary" Style="border-radius: 4px; font-size: 10pt;"
                                                                ToolTip="Mark Leave">L</asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnUpdateLeave" CommandName="updateLeave" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                Visible="true" runat="server" CssClass="btn btn-sm btn-warning" Style="border-radius: 4px; font-size: 10pt;"
                                                                ToolTip="Extend Leave">E</asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnCancelLeave" CommandName="cancelLeave" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                Visible="true" runat="server" CssClass="btn btn-sm btn-danger" Style="border-radius: 4px; font-size: 10pt;"
                                                                ToolTip="Cancel Leave">C</asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton5" CommandName="viewDashboard" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                Visible="false" runat="server" CssClass="btn btn-sm btn-info" Style="border-radius: 4px; font-size: 10pt;"
                                                                ToolTip="View Dashboard">D</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Right" CssClass="GridPager" ForeColor="Black" />
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

        <%--*******************Popup****************--%>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
                CancelControlID="Button3" TargetControlID="Button4" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Please Confirm
                        </h4>
                    </div>
                    <div class="card-body text-center pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnYesConfirmation" runat="server" OnClick="lbtnYesConfirmation_Click" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                            <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button4" runat="server" Text="" />
                    <asp:Button ID="Button3" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mperror" runat="server" PopupControlID="pnlerror" CancelControlID="Button2"
                TargetControlID="Button1" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlerror" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Please Check & Correct
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblerrormsg" runat="server" Text="Please Check entered values."></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnclose1" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> OK </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button1" runat="server" Text="" />
                    <asp:Button ID="Button2" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpsuccess" runat="server" PopupControlID="pnlsuccess"
                CancelControlID="lbtnsuccessclose1" TargetControlID="Button6" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlsuccess" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Confirm
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblsuccessmsg" runat="server" Text="Do you want to Update ?"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnsuccessclose1" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button6" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpInfo" runat="server" PopupControlID="PanelInfo" CancelControlID="LinkButtonMpInfoClose"
                TargetControlID="LinkButtonInfo" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="PanelInfo" runat="server" Style="position: fixed;">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">About Module</h4>
                        </div>
                        <div class="modal-body">
                            <ol>
                                <li>You can Apply Leave of employee</li>
                                <li>You can also Extend leave of employee and can Cancel Employee leave.</li>
                            </ol>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="LinkButtonMpInfoClose" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>


        <div class="row">
            <cc1:ModalPopupExtender ID="mpMarkLeave" runat="server" PopupControlID="pnlMarkLeave"
                CancelControlID="Button5" TargetControlID="Button3" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlMarkLeave" runat="server" Style="position: fixed; display: none">
                <div class="card" style="width:700px;">
                    <div class="card-header">
                        <h4 class="card-title text-center mb-0" style="font-weight: bold; font-size: 15pt;">Mark Leave
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2 row" style="min-height: 100px;">
                        <div class="col-lg-12" style="font-size: 11pt;">
                            <div class="col-lg-12 mb-3">
                                <table class="table table2 mb-2 ">
                                    <tr>
                                        <td style="width: 45%;"><b>Employee Code</b></td>
                                        <td>
                                            <asp:Label runat="server" ID="lblEmpCode" Text="N/A"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><b>Employee Name</b></td>
                                        <td>
                                            <asp:Label runat="server" ID="lblEmpName" Text="N/A"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><b>Designation</b></td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDesigantion" Text="N/A"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Leave type<span style="color: red">*</span></td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlLeaveType" CssClass="form-control"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>Leave Start Date<span style="color: red">*</span></td>
                                      <td>
                                        <asp:TextBox ID="txtFromdate" ToolTip="Enter Invoice Date" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="txtFromdate" ValidChars="/" />
                                    </td>
                                    </tr>
                                    <tr>
                                        <td>Leave End Date<span style="color: red">*</span></td>
                                         <td>
                                        <asp:TextBox ID="txtToDate" ToolTip="Enter Invoice Date" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="txtToDate" ValidChars="/" />
                                    </td>
                                    </tr>
                                    <tr>
                                        <td>Reason</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtRemark" CssClass="form-control" TextMode="MultiLine" Style="resize: none" MaxLength="100" AutoComplete="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Upload Document (If Any)<br />
                                            <span style="font-size: 7pt; color: Red; line-height: 12px;">(Max. Size 2 MB and PDF only)</span></td>
                                        <td>
                                            <asp:Button ID="btnUploadImage" runat="server" OnClick="btnUploadImage_Click" CausesValidation="False" CssClass="button1" Style="display: none"
                                                TabIndex="18" Text="Upload Image" Width="80px" />
                                            <br />
                                            <asp:FileUpload ID="docfile" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-success btn-sm"
                                                onchange="UploadImage(this);" Width="200px" TabIndex="9" />
                                            <asp:LinkButton runat="server" ID="lbtnPdf" CssClass="col-form-label control-label" Style="font-size: 12px; color: red; font-weight: normal; text-decoration: underline;"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                                <div style="width: 100%; margin-top: 20px;" class="text-center">
                                    <asp:LinkButton ID="lbtnMarkLeave" runat="server" OnClick="lbtnMarkLeave_Click" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-floppy-o"></i> Save </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-refresh"></i> Cancel </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button5" runat="server" Text="" />
                    <asp:Button ID="Button7" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpCancelLeave" runat="server" PopupControlID="pnlCancelLeave"
                CancelControlID="LinkButton6" TargetControlID="Button11" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlCancelLeave" runat="server" Style="position: fixed; display: none">
                <center>
                <div class="card" style="width: 700px;">
                    <div class="card-header">
                        <h4 class="card-title text-center mb-0" style="font-weight: bold; font-size: 15pt;">Cancellation of Leave
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2 row" style="min-height: 100px;">
                        <div class="col-lg-12" style="font-size: 11pt;">
                            <div class="col-lg-12 mb-3">
                                <table class="table table2 mb-2 ">
                                    <tr>
                                        <td style="width: 45%;"><b>Employee</b></td>
                                        <td>
                                            <asp:Label runat="server" ID="lblEmployee" Text="N/A"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Leave type</td>
                                        <td>
                                            <asp:Label runat="server" ID="lblLeaveType" Text="N/A"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Leave Period</td>
                                        <td>
                                            <asp:Label runat="server" ID="lblLeavePeriod" Text="N/A"></asp:Label></td>
                                    </tr>
                                    <%-- <tr>
                                        <td>Balance Leave Pendings</td>
                                        <td>
                                            <asp:Label runat="server" ID="lblPendingLeave" Text="N/A"></asp:Label></td>
                                    </tr>--%>
                                    <tr>
                                        <td>Reason of Cancellation<span class="text-danger">*</span></td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtCancellationReason" CssClass="form-control" TextMode="MultiLine" Style="resize: none" MaxLength="100" AutoComplete="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Upload Document (If Any)<br />
                                            <span style="font-size: 7pt; color: Red; line-height: 12px;">(Max. Size 2 MB and PDF only)</span></td>
                                        <td>
                                            <asp:Button ID="btnUploadImage2" runat="server" OnClick="btnUploadImage2_Click" CausesValidation="False" CssClass="button1"
                                                Style="display: none" TabIndex="18" Text="Upload Image" Width="80px" />
                                          
                                            <asp:FileUpload ID="fileDoc2" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-success btn-sm"
                                                onchange="UploadImage2(this);" Width="200px" TabIndex="9" />
                                            <asp:LinkButton runat="server" ID="lbtnpdf2" CssClass="col-form-label control-label " Style="font-size: 12px; color: red; font-weight: normal; text-decoration: underline;"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                                <center>
                                    <div style="width: 100%; margin-top: 20px;">
                                        <asp:LinkButton ID="lbtnCancelLeave" runat="server" OnClick="lbtnCancelLeave_Click" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> Save </asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton6" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> Cancel </asp:LinkButton>
                                    </div>
                                </center>
                            </div>
                        </div>
                    </div>

                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button11" runat="server" Text="" />
                </div>
            </center>
            </asp:Panel>
        </div>
    </div>
</asp:Content>



