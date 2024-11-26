<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Depotmaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="EmployeeManagement.aspx.cs" Inherits="Auth_EmployeeManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <style type="text/css">
        .divWaiting {
            position: absolute;
            background-color: #FAFAFA;
            opacity: 0.6;
            z-index: 2147483647 !important;
            overflow: hidden;
            text-align: center;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            padding-top: 20%;
        }

        .lblMessage {
            color: red;
        }
        /*.textbox {
            height: 24px !important;
        }*/

        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        textarea {
            margin: 0;
            font-family: inherit;
            font-size: inherit;
            line-height: 1.5em;
        }

        .table_grid {
            width: 100%;
            /**   /** font-family: Verdana;**/
        }

        .table {
            font-size: 10pt !important;
        }

        .table_grid tr {
            padding: 0;
            width: 100%;
        }

        .table_grid th {
            padding: 5px;
            text-align: left;
            font-weight: 500;
            font-size: 10pt;
            /**   /** font-family: Verdana;**/
        }

        .table_grid td {
            padding-top: 8px;
            text-align: left;
            vertical-align: top;
            font-weight: 300;
            font-size: 10pt;
            /**  /** font-family: Verdana;**/
            padding-left: 5PX;
        }

        .groupHead {
            background: #436794;
            color: white;
            padding: 5px 10px;
            font-size: 12pt;
        }

        .textboxUpper {
            text-align: left;
            font-size: 10pt;
            /**   /** font-family: Verdana;**/
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            border-radius: .25rem;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }

        /*.textbox {
            border-right: black 1px groove;
            border-top: black 1px groove;
            font-size: 9pt;
            /**  /** font-family: Verdana; border-left: black 1px groove;**/
        /* border-bottom: black 1px groove;
            background-color: #ffffff;
            text-decoration: none;
            height: 24px;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            border-radius: .25rem;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }*/

        .btnsavecss {
            width: 80px;
            height: 22px;
            font-size: 10pt;
            /**  /**  /** font-family: Verdana;**/
            padding: 0;
            font-weight: bold;
            color: #fff;
            background-color: #1e7e34;
            border-color: #1c7430;
        }

        .table td, .table th {
            padding: 5px;
        }

        .btnaddcss {
            width: 44px;
            height: 22px;
            font-size: 10pt;
            /** font-family: Verdana;**/
            padding: 0;
            font-weight: bold;
            color: #fff;
            background-color: #007bff;
            border-color: #007bff;
        }

        .btncancelcss {
            width: 80px;
            height: 22px;
            font-size: 10pt;
            /** font-family: Verdana;**/
            padding: 0;
            font-weight: bold;
            color: #fff;
            background-color: #fd7e14;
            border-color: #fd7e14;
        }

        .btnupdatecss {
            width: 80px;
            height: 22px;
            font-size: 10pt;
            /** font-family: Verdana;**/
            padding: 0;
            font-weight: bold;
            color: #fff;
            background-color: #ffc107;
            border-color: #ffc107;
        }

        .btndeletecss {
            width: 80px;
            height: 22px;
            font-size: 10pt;
            /** font-family: Verdana;**/
            padding: 0;
            font-weight: bold;
            color: #fff;
            background-color: #1e7e34;
            border-color: #1c7430;
        }

        .btnIcon {
            border-radius: 4px;
            padding: 7px 6px;
            font-size: 10pt;
        }

        select.form-control:not([size]):not([multiple]) {
            font-size: 10pt;
            /** font-family: Verdana;**/
        }

        .table_bus_service_type {
            width: 100%;
            /** font-family: Verdana;**/
        }

        .table1 td, .table th {
            border-top: none;
        }

        .card {
            border: none;
        }

        .table_bus_service_type tr {
            padding: 0;
            width: 100%;
        }

        .table_bus_service_type th {
            padding: 0px 6px;
            text-align: left;
            font-weight: 500;
            font-size: 10pt;
            /** font-family: Verdana;**/
            font-weight: bold;
            text-transform: uppercase;
        }

        .table_bus_service_type td {
            padding-top: 8px;
            padding-left: 8px;
            text-align: left;
            vertical-align: middle;
            font-weight: 300;
            font-size: 10pt;
            /** font-family: Verdana;**/
            color: #000;
        }

        #style-4::-webkit-scrollbar-track {
            -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.3);
            background-color: #F5F5F5;
        }

        #style-4::-webkit-scrollbar {
            width: 10px;
            background-color: #F5F5F5;
        }

        #style-4::-webkit-scrollbar-thumb {
            background-color: #70d1f4;
            border: 2px solid #555555;
        }

        select.form-control:not([size]):not([multiple]) {
            height: 35px;
        }

        .form-control {
            height: 35px;
            display: block;
            width: 100%;
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

        .textbox {
            width: 100%;
            height: 32px;
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

        .GridPager td {
            padding-top: 2px;
            padding-left: 3px;
        }

        .GridPager a, .GridPager span {
            display: block;
            height: 22px;
            min-width: 17px;
            font-weight: bold;
            text-align: center;
            text-decoration: none;
        }

        .GridPager a {
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #969696;
            border-radius: 0px;
        }

            .GridPager a .hover {
                background-color: red;
                color: #969696;
                border: 1px solid #969696;
            }

        .tableSearch td, .tableSearch th {
            padding: 3px;
            vertical-align: top;
            border-top: none;
            text-align: left;
        }

        .GridPager span {
            background-color: #f3eded;
            color: #000;
            border: 1px solid #f3eded;
            border-radius: 0px;
        }

        .radiobutton {
            padding: 15px 20px;
            background: #ffeaea;
            margin-top: 16px;
            text-align: center;
        }
    </style>
    <style>
        /* The container */
        .containerRadio {
            display: block;
            position: relative;
            padding-left: 35px;
            margin-bottom: 12px;
            cursor: pointer;
            font-size: 20px;
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
            top: 5px;
            left: 0;
            height: 25px;
            width: 25px;
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

        .cardSidebar {
            margin-bottom: 10px !important;
        }
        /* Show the indicator (dot/circle) when checked */
        .containerRadio input:checked ~ .checkmarkRadio:after {
            display: block;
        }

        /* Style the indicator (dot/circle) */
        .containerRadio .checkmarkRadio:after {
            top: 9px;
            left: 9px;
            width: 8px;
            height: 8px;
            border-radius: 50%;
            background: white;
        }

        .pnlBackground {
            padding: 30px;
            background: #d4cece;
        }

        .boxshadow {
            background: white;
            box-shadow: 0px 0px 30px rgba(127, 137, 161, 0.25);
        }

        .spanMendatory {
            color: red;
        }
    </style>
     <script>
         $(document).ready(function () {

             var currDate = new Date().getDate();
             var preDate = new Date(new Date().setDate(currDate - 1));
             var todayDate = new Date(new Date().setDate(currDate));
             var FutureDate = new Date(new Date().setDate(currDate + 30000));

             $('[id*=txtDob1]').datepicker({
                 endDate: todayDate,
                 changeMonth: true,
                 changeYear: false,
                 format: "dd/mm/yyyy",
                 autoclose: true
             });
             $('[id*=txtPostingDate1]').datepicker({
                 endDate: todayDate,
                 changeMonth: true,
                 changeYear: false,
                 format: "dd/mm/yyyy",
                 autoclose: true
             });
             $('[id*=txtDateOfJoining1]').datepicker({
                 endDate: todayDate,
                 changeMonth: true,
                 changeYear: false,
                 format: "dd/mm/yyyy",
                 autoclose: true
             })
                 .on('changeDate', function (selected) {
                     var minDate = new Date(selected.date.valueOf());
                     $('[id*=txtDateOfJoining1]').datepicker('setStartDate', minDate);
                 });
             $('[id*=txtDepotJoiningDate1]').datepicker({
                 endDate: todayDate,
                 changeMonth: true,
                 changeYear: false,
                 format: "dd/mm/yyyy",
                 autoclose: true
             });
             $('[id*=tbDOPoffice]').datepicker({
                 endDate: todayDate,
                 changeMonth: true,
                 changeYear: false,
                 format: "dd/mm/yyyy",
                 autoclose: true
             });
             $('[id*= txtLicenceDt2]').datepicker({
                 endDate: FutureDate,
                 changeMonth: true,
                 changeYear: false,
                 format: "dd/mm/yyyy",
                 autoclose: true
             });


             
           
             
             $('[id*=txtorderdate]').datepicker({
                 changeMonth: true,
                 changeYear: false,
                 format: "dd/mm/yyyy",
                 autoclose: true
             });
            
         });

         function UploadImage1(fileUpload) {
             if (fileUpload.value != '') {
                 document.getElementById("<%=btnUploadImage1.ClientID %>").click();
            }
        }
        function UploadImageUpdate(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=ImageUpdate.ClientID %>").click();
            }
        }
     
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid" style="padding-top: 20px;">
      
        <div class="row">
            <div class="col-lg-9">
                <div class="card boxshadow">
                    <div class="card-body p-2 mt-1" style="font-size: 10pt; /** font-family: Verdana; **/ min-height: 645px;">
                       
                        <asp:Panel ID="pnlView" runat="server" Visible="true">
                            <div class="row text-left" style="background-color: #c0c0c052; margin-left: 0px; margin-right: 0px; padding-top: 5px; padding-bottom: 5px;">
                                <table class="table">
                                    <tr>
                                        <td>
                                            <label><b>Office Level</b></label>
                                            <br />
                                            <asp:DropDownList ID="ddlOfficeLvl2" runat="server" CssClass="form-control" TabIndex="1" AutoPostBack="true">
                                            </asp:DropDownList></td>
                                        <td style="width: 180px;">
                                            <label><b>Office</b></label>
                                            <br />
                                            <asp:DropDownList ID="ddlOffice2" runat="server" CssClass="form-control" TabIndex="1" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlDepots" runat="server" CssClass="form-control" TabIndex="1" Visible="false"
                                                AutoPostBack="true">
                                            </asp:DropDownList></td>
                                        <td>
                                            <label><b>Designation</b></label>
                                            <br />
                                            <asp:DropDownList ID="ddlDepotWiseDesig" runat="server" CssClass="form-control" TabIndex="2"
                                                AutoPostBack="true">
                                            </asp:DropDownList></td>
                                        <td runat="server" id="tdLicense" visible="false">
                                            <label><b>License Staus</b></label>
                                            <br />
                                            <asp:DropDownList ID="ddlLicenseStatus" runat="server" CssClass="form-control" TabIndex="2"
                                                AutoPostBack="true">
                                                <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                                <asp:ListItem Value="P" Text="Pending"></asp:ListItem>
                                                <asp:ListItem Value="E" Text="Expiring Soon"></asp:ListItem>
                                                <asp:ListItem Value="O" Text="Expired"></asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td>
                                            <label><b>Employee Code/Name</b></label>
                                            <br />
                                            <asp:TextBox ID="TextBoxSearch" runat="server" CssClass="form-control" MaxLength="15" Placeholder="Min. 3 char (Optional)"></asp:TextBox></td>
                                        <td style="padding-top: 22px;">
                                            <asp:LinkButton ID="lbtnserch" runat="server" OnClick="lbtnserch_Click" CssClass="btn btnIcon btn-success" Style="padding: 7px; font-size: 10pt;" ToolTip="Search"> <span><i class ="fa fa-search"></i></span></asp:LinkButton>
                                            <asp:LinkButton ID="btnRefresh" runat="server" CssClass="btn btnIcon btn-warning" OnClick="btnRefresh_Click" Style="padding: 7px; font-size: 10pt;" ToolTip="Clear Filters"> <span><i class ="fa fa-sync-alt"></i></span></asp:LinkButton>
                                            <asp:LinkButton ID="lbtnEmpReports" runat="server" CssClass="btn btnIcon btn-danger" OnClick="lbtnEmpReports_Click" Style="padding: 7px; font-size: 10pt;" ToolTip="Download Employee List"> <i class="fa fa-download"></i></asp:LinkButton></td>
                                    </tr>
                                </table>
                            </div>
                            <div class="row" style="margin-top: 19px; margin-left: 0px; margin-right: 0px; width: 100%;">
                                <div class="col-lg-12 p-0">

                                    <asp:Label ID="lblNoEmpData" CssClass="p-5" runat="server" Text="Sorry! No employee found. Search employee again" Font-Bold="true" ForeColor="#b6b8b8" Style="font-size: 26pt;"></asp:Label>
                                    <div class="row" runat="server" id="divLabel" visible="false">
                                        <div class="col-lg-12 pb-2">
                                            <div class="col-md-12 text-right" style="font-size: 9pt">
                                                <asp:Label Text="text" runat="server" CssClass="btn btn-sm btn-success btn-label">P</asp:Label>
                                                Personal Details
                    <asp:Label Text="text" runat="server" CssClass="btn btn-sm btn-info btn-label">C</asp:Label>
                                                Contact Details
                    <asp:Label Text="text" runat="server" CssClass="btn btn-sm btn-danger btn-label">O</asp:Label>
                                                Office Details
                                                                    <asp:Label Text="text" runat="server" CssClass="btn btn-sm btn-warning btn-label">L</asp:Label>
                                                License Details
                    <asp:Label Text="text" runat="server" CssClass="btn btn-sm btn-info btn-label">R</asp:Label>
                                                Duty Rest Details
                                                <asp:Label Text="text" runat="server" CssClass="btn btn-sm btn-primary btn-label">D</asp:Label>
                                                Duty Type
                                                 <asp:Label Text="text" runat="server" CssClass="btn btn-sm btn-success btn-label">S</asp:Label>
                                                Service Status
                                            </div>
                                        </div>
                                    </div>
                                     
                                    <asp:GridView ID="grvVerifiedEmployees" runat="server" AlternatingRowStyle-BackColor="#CFE2D9"
                                        AllowPaging="true" PageSize="20" AutoGenerateColumns="False" ForeColor="#333333" OnPageIndexChanging="grvVerifiedEmployees_PageIndexChanging"
                                        GridLines="None" Font-Bold="false"  CssClass="table table-responsive table-bordered" OnRowCommand="grvVerifiedEmployees_RowCommand" Width="100%" Style="display: inline-table;" 
                                       DataKeyNames="e_empcode,e_fname,e_mname,e_lname,e_gender,e_blood_group_code,e_weekrestday,
                                                            e_fathername,e_address,e_dob,e_date_of_joining,e_date_of_assigned_depot,e_mobile_number,e_landline,e_email_id, e_office_name,
                                                            e_dutytype,e_city,e_statename,e_pin_code,e_designation_code,e_designation_name,e_ofclvl_id,e_officeid,e_photo,e_emergency_number,e_posting_ofc,e_date_of_posting,e_emp_type,e_state_code,e_licenseno,e_licensedate,e_empclass,e_service_status,e_orderno,e_orderdate,e_orderremark">
                                      <%-- restDay,empPhoto,employeenumber,LICENSENO,LICENSEDATE,,EMERGENCYNO,EMPTYPE,
                                           POSTING_OFC,POSTING_DATE,EMP_CLASS,EMP_DUTYTYPE,SERVICESTATUSCODE,ORDERNO,ORDERDATE,REMARK"--%>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEMPFNAME" runat="server" Text='<%# Eval("e_fname") %>'></asp:Label>
                                                    <asp:Label ID="lblEMPMNAME" runat="server" Text='<%# Eval("e_mname") %>'></asp:Label>
                                                    <asp:Label ID="lblEMPLNAME" runat="server" Text='<%# Eval("e_lname") %>'></asp:Label>
                                                    (<asp:Label ID="lblEMPCODE" runat="server" Text='<%# Eval("e_empcode") %>'></asp:Label>)<br />
                                                    <i class="fa fa-mobile"></i>
                                                    <asp:Label ID="lblEMPMOBILENUMBER" runat="server" Text='<%# Eval("e_mobile_number") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Office Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEMPoffice" runat="server" Text='<%# Eval("e_office_name") %>'></asp:Label><br />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Posting Office">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("e_posting_ofc") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEMPDESIGNATION" runat="server" Text='<%# Eval("e_designation_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnUpdateEmp" Visible="true" runat="server" CommandName="UpdatePersonal" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                        class="btn btnIcon btn-success" Width="25px" ToolTip="Update Personal Details">P</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnVerifyEmp" Visible="true" runat="server" CommandName="UpdateContact" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                        class="btn btnIcon btn-info" Width="25px" ToolTip="Update Contact Details">C</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDeleteEmp" Visible="true" runat="server" CommandName="UpdateOfficial" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                        class="btn btnIcon btn-danger" Width="25px" ToolTip="Update Official Details">O</asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton10" Visible="true" runat="server" CommandName="UpdateLicense" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                        class="btn btnIcon btn-warning" Width="25px" ToolTip="Update License Details">L</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnUpdateWeeklyRest" Visible="true" runat="server" CommandName="UpdateWeeklyRest" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                        class="btn btnIcon btn-info" Width="25px" ToolTip="Update Weekly Rest">R</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtndutytype" Visible="true" runat="server" CommandName="updateDutyType" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                        class="btn btnIcon btn-primary" Width="25px" ToolTip="Update Duty Type">D</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnservicestatus" Visible="true" runat="server" CommandName="updateServiceStatus" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                        class="btn btnIcon btn-success" Width="25px" ToolTip="Update Service Status">S</asp:LinkButton>
                                                    <%-- <asp:LinkButton ID="lbtnProfile" Visible="true" runat="server" CommandName="ViewProfile" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                        class="btn btnIcon btn-warning" Width="25px" ToolTip="View Employee Profile"> <i class="fa fa-user"></i>   </asp:LinkButton>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                                        <AlternatingRowStyle BackColor="#eaf4ff" />
                                        <HeaderStyle BackColor="#bcd7f3" ForeColor="Black" VerticalAlign="Top" />
                                    </asp:GridView>


                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlupdateVerified" runat="server" Visible="false" Style="box-shadow: 2px 4px 25px 20px #596166c2; width: 90%; margin-left: auto; margin-right: auto;">
                            <asp:LinkButton runat="server" ID="closePnl" BackColor="red" OnClick="closePnl_Click" ForeColor="White" Style="color: White; background-color: Red; font-size: 25px; float: right; margin: -24px; padding: 2px 5px; position: relative; border-radius: 4px; margin-top: -18px; margin-right: -7px;"><i class="fa fa-times"></i></asp:LinkButton>
                            <div class="clearfix"></div>
                            <div class="card boxshadow" runat="server" id="dvPersonalDetails" style="position: inherit">
                                <div class="card-header groupHead">
                                    <asp:Label runat="server" ID="lbtnPersonalCollapse" class="card-link" Font-Bold="true" Font-Size="14pt" Style="color: white">Personal details</asp:Label>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-4">
                                            Employee Code <span class="spanMendatory">*</span>
                                            <br />
                                            <asp:TextBox ID="txtEmpcode1" runat="server" MaxLength="10" CssClass="form-control" Width="100px"
                                                TabIndex="1" Placeholder="Max. 10 char">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-lg-4" runat="server" visible="false">
                                            Employee Number (Optional)
                                            <br />
                                            <asp:TextBox ID="txtempnumber1" runat="server" MaxLength="10" CssClass="textbox" Width="100px"
                                                Placeholder="Max. 10 char">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-lg-3">
                                            First Name <span class="spanMendatory">*</span>
                                            <br />
                                            <asp:TextBox ID="txtFname1" runat="server" MaxLength="20" CssClass="form-control" TabIndex="2" Placeholder="Max. 20 char"></asp:TextBox>
                                            <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Custom" ValidChars=" "
                                                TargetControlID="txtFname1" />
                                        </div>
                                        <div class="col-lg-3">
                                            Middle Name
                                                    <br />
                                            <asp:TextBox ID="txtMName1" runat="server" CssClass="form-control" TabIndex="3" MaxLength="20" Placeholder="Max. 20 char"></asp:TextBox>
                                            <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="UppercaseLetters, LowercaseLetters"
                                                TargetControlID="txtMName1" />
                                        </div>
                                        <div class="col-lg-3">
                                            Last Name
                                                <br />
                                            <asp:TextBox ID="txtlastname1" runat="server" MaxLength="20" CssClass="form-control" TabIndex="4" Placeholder="Max. 20 char"></asp:TextBox>
                                            <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="UppercaseLetters, LowercaseLetters"
                                                TargetControlID="txtlastname1" />
                                        </div>
                                        <div class="col-lg-3">
                                            Father Name
                                            <br />
                                            <asp:TextBox ID="txtFatherNAme1" runat="server" MaxLength="45" CssClass="form-control" Placeholder="Max. 45 char"
                                                TabIndex="7"></asp:TextBox>
                                            <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="UppercaseLetters, LowercaseLetters"
                                                TargetControlID="txtFatherNAme1" />
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-lg-3">
                                            Gender <span class="spanMendatory">*</span><br />
                                            <asp:DropDownList runat="server" ID="ddlVerGender" CssClass="form-control">
                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                <asp:ListItem Value="M" Text="Male"></asp:ListItem>
                                                <asp:ListItem Value="F" Text="Female"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-3">
                                            Blood Group <span class="spanMendatory">*</span>
                                            <br />
                                            <asp:DropDownList runat="server" ID="ddlbloodgrp2" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-lg-4 p-0">
                                            <div class="col-lg-7">
                                                Date Of Birth <span class="spanMendatory">*</span>
                                                <br />
                                                <asp:TextBox ID="txtDob1" runat="server" CssClass="textbox" Width="110px" TabIndex="5" Placeholder="DD/MM/YYYY"></asp:TextBox>
                                                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom"
                                                ValidChars="/" TargetControlID="txtDob1" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-lg-12 mb-2">
                                            Photo<br />
                                            <asp:Button ID="btnUploadImage1" runat="server" OnClick="btnUploadImage1_Click" CausesValidation="False" CssClass="button1"
                                                Style="display: none" TabIndex="18" Text="Upload Image" Width="80px" />
                                            <asp:FileUpload ID="ImageUpdate" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-success btn-sm"
                                                onchange="UploadImage1(this);" Width="200px" TabIndex="9" />
                                            <asp:Image ID="imgImageUpdate" runat="server" Visible="false" Style="border-width: 0px; height: 50px; width: 50px; border: 2px solid #eaf4ff;" /><br />
                                            <span style="font-size: 7pt; color: Red; line-height: 12px;">Image size will be less then 100 KB<br />
                                                (Only .JPG, .PNG, .JPEG)</span>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-lg-12 text-center">
                                            <asp:LinkButton ID="lbtnVerifyPersonal" runat="server" OnClick="lbtnVerifyPersonal_Click" CssClass="btn btn-warning"> 
                                                                    <i class="fa fa-lock"></i> Verify & Lock</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card boxshadow" runat="server" id="dvContactDetails" visible="false" style="position: inherit">
                                <div class="card-header groupHead">
                                    <asp:Label runat="server" ID="lbtnContactDetails" class="card-link" Font-Bold="true" Font-Size="14pt" Style="color: white">Contact details</asp:Label>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-3">
                                            Email Id 
                                            <br />
                                            <asp:TextBox ID="txtEmailId1" runat="server" CssClass="form-control" MaxLength="30" TabIndex="21" Placeholder="Max.30 char"></asp:TextBox>
                                        </div>

                                        <div class="col-lg-3">
                                            Mobile No  <span class="spanMendatory">*</span>
                                            <br />
                                            <span class="input-group-addon" style="padding: 4px 5px; font-size: 8pt;">+91</span>
                                            <asp:TextBox ID="txtMobileNo1" runat="server" CssClass="textbox" MaxLength="10" TabIndex="8" Placeholder="Max. 10 char"
                                                Width="75%"></asp:TextBox>
                                            <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Numbers"
                                                TargetControlID="txtMobileNo1" />
                                        </div>
                                        <div class="col-lg-3">
                                            Landline No
                                                <br />
                                            <asp:TextBox ID="txtLandlineno1" runat="server" MaxLength="12" CssClass="form-control" Placeholder="Max. 12 char"
                                                TabIndex="9"></asp:TextBox>
                                            <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterType="Numbers"
                                                TargetControlID="txtLandlineno1" />
                                        </div>
                                        <div class="col-lg-3">
                                            Emergency No <span class="spanMendatory">*</span>
                                            <br />
                                            <asp:TextBox ID="txtEmergencyNo2" runat="server" MaxLength="10" CssClass="textbox" Placeholder="Max. 10 char"
                                                TabIndex="10"></asp:TextBox>
                                            <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender26" runat="server" FilterType="Numbers"
                                                TargetControlID="txtEmergencyNo2" />
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-lg-3">
                                            City 
                                            <br />
                                            <asp:TextBox ID="txtCity1" runat="server" CssClass="form-control" MaxLength="30" TabIndex="12" Placeholder="Max. 30 char"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server" FilterType="UppercaseLetters, LowercaseLetters" TargetControlID="txtCity1" />
                                        </div>
                                        <div class="col-lg-3">
                                            Pin Code<br />
                                            <asp:TextBox ID="txtpin1" runat="server" CssClass="form-control" MaxLength="6" TabIndex="11" Placeholder="Max. 6 char"></asp:TextBox>
                                            <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" FilterType="Numbers"
                                                TargetControlID="txtpin1" />
                                        </div>
                                        <div class="col-lg-3">
                                            State
                                                <br />
                                            <asp:DropDownList ID="ddlStates1" runat="server" CssClass="form-control" TabIndex="13">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-lg-6">
                                            Address<br />
                                            <asp:TextBox ID="txtAddress1" runat="server" MaxLength="200" CssClass="form-control" TabIndex="10" Placeholder="Max. 200 char"
                                                TextMode="MultiLine" Style="height: 60px!Important; resize: none;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-lg-12 text-center ">
                                            <asp:LinkButton ID="lbtnVerifyContact" runat="server" OnClick="lbtnVerifyContact_Click" CssClass="btn btn-warning"> 
                                                                    <i class="fa fa-lock"></i> Verify & Lock</asp:LinkButton>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card boxshadow" runat="server" id="dvOfficeDetails" visible="false" style="position: inherit">
                                <div class="card-header groupHead">
                                    <asp:Label runat="server" ID="lbtnofficialdetails" class="card-link" Font-Bold="true" Font-Size="14pt" Style="color: white">Official details</asp:Label>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-3">
                                            Office Level <span class="spanMendatory">*</span><br />
                                            <asp:DropDownList ID="ddlOfcLvl1" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlOfcLvl1_SelectedIndexChanged" TabIndex="13" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-3">
                                            Office <span class="spanMendatory">*</span><br />
                                            <asp:DropDownList ID="ddlOffice1" runat="server" OnSelectedIndexChanged="ddlOffice1_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true" TabIndex="14">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-3">
                                            Posting Office <span class="spanMendatory">*</span><br />
                                            <asp:DropDownList runat="server" ID="ddlpostingofc1" CssClass="form-control" Height="35px" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-3">
                                            Posting Date<br />
                                            <asp:TextBox ID="txtPostingDate1" runat="server" CssClass="textbox" Width="68%" Placeholder="DD/MM/YYYY" AutoCompleteType="Disabled"
                                                TabIndex="18"></asp:TextBox>
                                                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom"
                                                ValidChars="/" TargetControlID="txtPostingDate1" />                               
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-lg-3">
                                            Employee Type  <span class="spanMendatory">*</span>
                                            <br />
                                            <asp:DropDownList ID="ddlEmpType2" runat="server" CssClass="textbox" TabIndex="14" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-3">
                                            Designation
                                            <br />
                                            <asp:DropDownList ID="drpDesignation1" runat="server" CssClass="form-control" TabIndex="14">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-3">
                                            Date Of Joining<br />
                                            <asp:TextBox ID="txtDateOfJoining1" runat="server" CssClass="textbox" Width="68%" Placeholder="DD/MM/YYYY"
                                                TabIndex="16"></asp:TextBox>
                                              <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers, Custom"
                                                ValidChars="/" TargetControlID="txtDateOfJoining1" /> 
                                        </div>
                                        <div class="col-lg-3">
                                            Date of Joining at Office<br />
                                            <asp:TextBox ID="txtDepotJoiningDate1" runat="server" CssClass="textbox" Width="68%" Placeholder="DD/MM/YYYY"
                                                TabIndex="18"></asp:TextBox>
                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers, Custom"
                                                ValidChars="/" TargetControlID="txtDepotJoiningDate1" /> 
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-lg-12 text-center ">
                                            <asp:LinkButton ID="lbtnVerifyOfficial" runat="server" OnClick="lbtnVerifyOfficial_Click" CssClass="btn btn-warning"> 
                                                                    <i class="fa fa-lock"></i> Verify & Lock</asp:LinkButton>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card boxshadow" runat="server" id="dvLicenseDetails" visible="false" style="position: inherit">
                                <div class="card-header groupHead">
                                    <asp:Label runat="server" ID="LinkButton11" class="card-link" Font-Bold="true" Font-Size="14pt" Style="color: white">License details</asp:Label>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-4">
                                            License No<br />
                                            <asp:TextBox ID="txtLicenseNo2" runat="server" CssClass="textbox" MaxLength="20" TabIndex="11" Placeholder="Max. 20 char"></asp:TextBox>
                                            <AjaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" FilterType="UppercaseLetters,LowercaseLetters,Numbers,Custom"
                                                TargetControlID="txtLicenseNo2" ValidChars=" " />
                                        </div>
                                        <div class="col-lg-4">
                                            License Valid Upto<br />
                                            <asp:TextBox ID="txtLicenceDt2" runat="server" CssClass="textbox" Width="68%" Placeholder="Max. 10 char"
                                                TabIndex="18"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Numbers, Custom"
                                                ValidChars="/" TargetControlID="txtLicenceDt2" />                                          
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-lg-12 text-center ">
                                            <asp:LinkButton ID="lbtnSaveLicenseDetails" runat="server" OnClick="lbtnSaveLicenseDetails_Click" CssClass="btn btn-warning"> 
                                                                    <i class="fa fa-lock"></i> Verify & Lock</asp:LinkButton>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card boxshadow" runat="server" id="dvWeekrestDeails" visible="false" style="position: inherit">
                                <div class="card-header groupHead">
                                    <asp:Label runat="server" ID="lbl2" class="card-link" Style="color: white">Update Weekly Rest</asp:Label>
                                </div>
                                <div class="card-body" style="height: 500px;">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="row">
                                                <div class="col-lg-4"></div>
                                                <div class="col-lg-4">
                                                    Select Weekly Rest Day<br />
                                                    <asp:DropDownList runat="server" ID="ddlWeekDays" CssClass="form-control" Height="35px">
                                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                        <asp:ListItem Value="Monday" Text="Monday"></asp:ListItem>
                                                        <asp:ListItem Value="Tuesday" Text="Tuesday"></asp:ListItem>
                                                        <asp:ListItem Value="Wednesday" Text="Wednesday"></asp:ListItem>
                                                        <asp:ListItem Value="Thursday" Text="Thursday"></asp:ListItem>
                                                        <asp:ListItem Value="Friday" Text="Friday"></asp:ListItem>
                                                        <asp:ListItem Value="Saturday" Text="Saturday"></asp:ListItem>
                                                        <asp:ListItem Value="Sunday" Text="Sunday"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-lg-12 text-center ">
                                            <asp:LinkButton ID="lbtnSaveWeeklyRest" runat="server" OnClick="lbtnSaveWeeklyRest_Click" CssClass="btn btn-warning"> 
                                                                    <i class="fa fa-lock"></i> Verify & Lock</asp:LinkButton>


                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card boxshadow" runat="server" id="dvDutyTypeDetails" visible="false" style="position: inherit">
                                <div class="card-header groupHead">
                                    <asp:Label runat="server" ID="Label6" class="card-link" Style="color: white">Update Class and Duty Type Details</asp:Label>
                                </div>
                                <div class="card-body" style="height: 500px;">

                                    <div class="row">
                                        <div class="col-lg-3">
                                            Employee Class <span class="spanMendatory">*</span>
                                            <br />
                                            <asp:DropDownList ID="ddlempclass1" runat="server" CssClass="textbox" TabIndex="13" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-3">
                                            Duty Type <span class="spanMendatory">*</span>
                                            <br />
                                            <asp:DropDownList ID="ddldutytype1" runat="server" CssClass="textbox" TabIndex="13" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-lg-12 text-center ">

                                            <asp:LinkButton ID="lbtnUpdatedDutyType" runat="server" OnClick="lbtnUpdatedDutyType_Click" CssClass="btn btn-warning"> 
                                                                    <i class="fa fa-lock"></i> Verify & Lock</asp:LinkButton>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card boxshadow" runat="server" id="dvServiceStatus" visible="false" style="position: inherit">
                                <div class="card-header groupHead">
                                    <asp:Label runat="server" ID="Label8" class="card-link" Style="color: white">Update Service Status</asp:Label>
                                </div>
                                <div class="card-body" style="height: 500px;">

                                    <div class="row">
                                        <div class="col-lg-3">
                                            Service Status <span class="spanMendatory">*</span>
                                            <br />
                                            <asp:DropDownList ID="ddlservicestatus" runat="server" CssClass="textbox" TabIndex="13" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-3">
                                            Office Order No. <span class="spanMendatory">*</span>
                                            <br />
                                            <asp:TextBox ID="txtofcorderno" runat="server" MaxLength="20" CssClass="form-control" TabIndex="2" Placeholder="Max. 20 char"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-3">
                                            Order Date <span class="spanMendatory">*</span>
                                            <br />
                                            <asp:TextBox ID="txtorderdate" CssClass="form-control form-control-sm"  runat="server"  Width="68%" Placeholder="DD/MM/YYYY" AutoCompleteType="Disabled"
                                                TabIndex="18"></asp:TextBox>

                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender27" runat="server" FilterType="Numbers, Custom"
                                                ValidChars="/" TargetControlID="txtorderdate" />
                                            <%--<span class="input-group-addon" style="padding: 7px 10px; border-radius: .25rem; border: 1px solid #ced4da;">
                                                <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbOrderDateUpdate" ValidChars="/" />
                                                <asp:ImageButton ID="ImageButton8" runat="server" CausesValidation="false" ImageUrl="~/images/title_marker.gif"
                                                    TabIndex="19" /></span>
                                            <AjaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" CssClass="black"
                                                Format="dd/MM/yyyy" PopupButtonID="ImageButton8" PopupPosition="BottomRight" TargetControlID="txtorderdate"></AjaxToolkit:CalendarExtender>--%>
                                        </div>
                                        <div class="col-lg-3">
                                            Remark 
                                            <br />
                                            <asp:TextBox ID="txtremark" TextMode="MultiLine" runat="server" MaxLength="20" CssClass="form-control" TabIndex="2" Placeholder="Max. 20 char"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-lg-12 text-center ">

                                            <asp:LinkButton ID="lbtnUpdatedServieStatus" runat="server" OnClick="lbtnUpdatedServieStatus_Click" CssClass="btn btn-warning"> 
                                                                    <i class="fa fa-lock"></i> Verify & Lock</asp:LinkButton>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <div class="col-lg-3">

                <div class="card cardSidebar boxshadow">
                    <div class="card-body p-2">
                        <div class="row text-left">
                            <div class="col-lg-2 text-center pr-0 pt-1">
                                <i class="fa fa-user fa-3x" style="color: #eaecf0;"></i>
                            </div>
                            <div class="col-lg-10 text-center p-0">
                                <h4 class="text-danger">Total Employees-
                                    <asp:Label ID="lblTotalEmp" runat="server" Text="0"></asp:Label>
                                </h4>
                            </div>
                            <div class="col-lg-12">
                                <div class="row">
                                    <div class="col-lg-6 text-center p-0" style="border-right: 2px solid #eee">
                                        <p class="mb-1">
                                            Verified
                                        </p>
                                        <h4>
                                            <asp:Label ID="lblTotalVEmp" runat="server" Text="0"></asp:Label></h4>
                                    </div>
                                    <div class="col-lg-6 text-center p-0">
                                        <p class="mb-1">
                                            Not-Verified
                                        </p>
                                        <h4>
                                            <asp:Label ID="lblTotalNVEmp" runat="server" Text="0"></asp:Label></h4>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card cardSidebar boxshadow">
                    <div class="card-header bg-primary" style="padding: 3px;">
                        <h4 style="font-size: 13pt; color: White; font-weight: bold; text-align: center; margin-bottom: 0px;">Pending License Details                             
                        </h4>
                        <p class="text-white text-center" style="font-size: 10pt; margin: 0px;">(Only Verified Employees)</p>
                    </div>
                    <div class="card-body p-2">
                        <div class="row text-left pt-2 pb-2">
                            <div class="col-lg-6 text-center p-0" style="border-right: 2px solid #eee">
                                <p class="mb-1">
                                    Driver
                                </p>
                                <h4>
                                    <asp:LinkButton ID="lbtnPendingDriver" runat="server" Font-Underline="true" ForeColor="Black" Text="0" CommandName="15" CommandArgument="P" ToolTip="Click Here for Info"></asp:LinkButton></h4>
                            </div>
                            <div class="col-lg-6 text-center p-0">
                                <p class="mb-1">
                                    Conductor
                                </p>
                                <h4>
                                    <asp:LinkButton ID="lbtnPendingConductor" runat="server" Text="0" Font-Underline="true" ForeColor="Black" CommandName="14" CommandArgument="P" ToolTip="Click Here for Info"></asp:LinkButton></h4>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card cardSidebar boxshadow" runat="server" id="divToExpLicense" visible="false">
                    <div class="card-header bg-warning" style="padding: 3px;">
                        <h4 style="font-size: 13pt; color: White; font-weight: bold; text-align: center; margin-bottom: 0px;">License Expiring Soon
                        </h4>
                        <p class="text-white text-center" style="font-size: 10pt; margin: 0px;">(Only Verified Employees)</p>
                    </div>
                    <div class="card-body p-2">
                        <div class="row text-left pt-2 pb-2">
                            <div class="col-lg-6 text-center p-0" style="border-right: 2px solid #eee">
                                <p class="mb-1">
                                    Driver
                                </p>
                                <h4>
                                    <asp:LinkButton ID="lbtnExpSoonDrv" runat="server" Text="0" Font-Underline="true" ForeColor="Black" CommandName="15" CommandArgument="E" ToolTip="Click Here for Info"></asp:LinkButton></h4>
                            </div>
                            <div class="col-lg-6 text-center p-0">
                                <p class="mb-1">
                                    Conductor
                                </p>
                                <h4>
                                    <asp:LinkButton ID="lbtnExpSoonCond" runat="server" Text="0" Font-Underline="true" ForeColor="Black" CommandName="14" CommandArgument="E" ToolTip="Click Here for Info"></asp:LinkButton></h4>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card cardSidebar boxshadow" runat="server" id="divExpiredLicense" visible="false">
                    <div class="card-header bg-danger" style="padding: 3px;">
                        <h4 style="font-size: 13pt; color: White; font-weight: bold; text-align: center; margin-bottom: 0px;">Expired License                              
                        </h4>
                        <p class="text-white text-center" style="font-size: 10pt; margin: 0px;">(Only Verified Employees)</p>
                    </div>
                    <div class="card-body p-2">
                        <div class="row text-left pt-2 pb-2">
                            <div class="col-lg-6 text-center p-0" style="border-right: 2px solid #eee">
                                <p class="mb-1">
                                    Driver
                                </p>
                                <h4>
                                    <asp:LinkButton ID="lbtnExpDriver" runat="server" Text="0" Font-Underline="true" ForeColor="Black" CommandName="15" CommandArgument="O" ToolTip="Click Here for Info"></asp:LinkButton></h4>
                            </div>
                            <div class="col-lg-6 text-center p-0">
                                <p class="mb-1">
                                    Conductor
                                </p>
                                <h4>
                                    <asp:LinkButton ID="lbtnExpConductor" runat="server" Text="0" Font-Underline="true" ForeColor="Black" CommandName="14" CommandArgument="O" ToolTip="Click Here for Info"></asp:LinkButton></h4>
                            </div>
                        </div>
                    </div>
                </div>
                
            </div>
        </div>


           <%-- ******************************error Pop Up--%>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpError" runat="server" PopupControlID="pnlError" CancelControlID="Button6"
                TargetControlID="Button3" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlError" runat="server" Style="position: fixed;">
                <div class="card" style="min-width: 350px;">
                    <div class="card-header" style="background-color: #e4273b; color: White;">
                        <h4 class="card-title">
                            <span style="font-size: 11pt;">Please Check & Correct</span>
                            <asp:LinkButton ID="lbtnerrorclose1" runat="server" ToolTip="Close" Style="float: right; color: white; padding: 0px;"> <i class="fa fa-times"></i>  </asp:LinkButton>
                        </h4>
                    </div>
                    <div class="card-body" style="min-height: 100px;">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblerrmsg" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:LinkButton ID="lbtnerrorclose" runat="server" CssClass="btn btn-warning" Style="height: 30px; width: 90px; padding-top: 4px; font-size: 10pt; border-radius: 4px;"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button3" runat="server" Text="" />
                     <asp:Button ID="Button6" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

         <%-- ******************************Success Pop Up--%>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpconfirm" runat="server" PopupControlID="pnlconfirm"
                CancelControlID="LinkButton3" TargetControlID="Button1" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlconfirm" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-body" style="min-height: 100px;">
                        <table class="table" style="width: 100%;">
                            <tr>
                                <td style="text-align: center; border: none;">
                                    <asp:Label ID="lblsucessmsg" runat="server" ForeColor="Red" Font-Size="12pt" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; border: none;">
                                    <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-warning" Style="height: 30px; width: 90px; padding-top: 4px; font-size: 10pt; border-radius: 4px;"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button1" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

         <%-- ******************************Confirmation Pop Up--%>
        <div class="row">
        <cc1:ModalPopupExtender ID="mpVConfirm" runat="server" PopupControlID="pnlVeriConfirmation" CancelControlID="Button7"
            TargetControlID="Button5" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlVeriConfirmation" runat="server" Style="position: fixed; display: none;">
            <div class="card" style="width: 350px;">
                <div class="card-header" style="height: 50px;padding: 7px;">
                    <h5 class="card-title pl-2 pt-2 pb-0">
                        <span>Please Confirm</span>
                        <asp:LinkButton ID="lbtnclose1" runat="server" ToolTip="Close" Style="float: right; padding: 0px;"> <i class="fa fa-times"></i>  </asp:LinkButton>
                    </h5>
                </div>
                <div class="card-body" style="min-height: 100px;">
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: center;">
                                <asp:Label ID="lblVMessage" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <asp:LinkButton ID="lbtnOkVerification" runat="server" OnClick="lbtnOkVerification_Click" CssClass="btn btn-warning" Style="height: 30px; width: 90px; padding-top: 4px; font-size: 10pt; border-radius: 4px;"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                                <asp:LinkButton ID="lbtnclose" runat="server" CssClass="btn btn-success" Style="height: 30px; width: 90px; padding-top: 4px; font-size: 10pt; border-radius: 4px;"> <i class="fa fa-times"></i> No </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button5" runat="server" Text="" />
                 <asp:Button ID="Button7" runat="server" Text="" />
            </div>

        </asp:Panel>
    </div>

          <%-- ******************************Employee Profile Pop Up--%>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpEmpProfile" runat="server" PopupControlID="pnlEmpProfile" TargetControlID="Button21"
                CancelControlID="LinkButton71" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlEmpProfile" runat="server" Style="position: fixed;">
                <div class="modal-content mt-5">
                    <div class="modal-header">
                        <div class="col-md-10">
                            <h3 class="m-0">Employee Information</h3>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="LinkButton71" runat="server" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="modal-body p-0">
                        <embed src="EmployeeInfo.aspx" style="min-height: 60vh; width: 65vw" />
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button21" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
       
        
    </div>
    
    
</asp:Content>


