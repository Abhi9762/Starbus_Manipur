<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/CscMasterPage.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="SubCscMgmt.aspx.cs" Inherits="Auth_SubCscMgmt" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/css/jquery.dataTables.min.css" rel="stylesheet" />

    <script src="../assets/js/jquery.dataTables.min.js"></script>

    <link href="../style.css" rel="stylesheet" />
  
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <style>
        .table td, .table th {
            padding: 0.25rem;
            vertical-align: top;
            border-top: 1px solid #dee2e6;
        }

        label {
            margin-bottom: .0rem;
        }

        .font-weight-bolder {
            font-weight: 600 !important;
        }

        .info {
            overflow: hidden;
        }

            .info .card-body .rotate {
                z-index: 8;
                float: right;
                height: 100%;
            }

                .info .card-body .rotate i {
                    color: rgba(20, 20, 20, 0.15);
                    position: absolute;
                    left: auto;
                    right: -5px;
                    bottom: -5px;
                    display: block;
                    -webkit-transform: rotate(-44deg);
                    -moz-transform: rotate(-44deg);
                    -o-transform: rotate(-44deg);
                    -ms-transform: rotate(-44deg);
                    transform: rotate(-44deg);
                }

        .table_head th {
            padding-top: 5px;
            padding-bottom: 5px;
        }
    </style>
    <style type="text/css">
        .grRowStyle {
            border-bottom: solid 1px #BBD9EE;
        }
    </style>
    <style>
        .pagination-ys {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }

            .pagination-ys table > tbody > tr > td {
                display: inline;
            }

                .pagination-ys table > tbody > tr > td > a, .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    color: #dd4814;
                    background-color: #ffffff;
                    border: 1px solid #dddddd;
                    margin-left: -1px;
                }

                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    margin-left: -1px;
                    z-index: 2;
                    color: #aea79f;
                    background-color: #f5f5f5;
                    border-color: #dddddd;
                    cursor: default;
                }

                .pagination-ys table > tbody > tr > td:first-child > a, .pagination-ys table > tbody > tr > td:first-child > span {
                    margin-left: 0;
                    border-bottom-left-radius: 4px;
                    border-top-left-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td:last-child > a, .pagination-ys table > tbody > tr > td:last-child > span {
                    border-bottom-right-radius: 4px;
                    border-top-right-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td > a:hover, .pagination-ys table > tbody > tr > td > span:hover, .pagination-ys table > tbody > tr > td > a:focus, .pagination-ys table > tbody > tr > td > span:focus {
                    color: #97310e;
                    background-color: #eeeeee;
                    border-color: #dddddd;
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-6">
                <div class="card mt-2 shadow">
                    <div class="card-header" style="height: 48px;">
                        <strong>Sub CSC Registration</strong>

                    </div>
                    <div class="card-body" style="min-height: 80vh;">
                        <div class="col">
                            <div class="nav nav-tabs">
                                <asp:LinkButton runat="server" ID="lbtnManual" OnClick="lbtnManual_Click"  CssClass="nav-item nav-link active" Font-Size="14px" Font-Bold="true">Manual Registration</asp:LinkButton>
                                <asp:LinkButton runat="server" ID="lbtnAuto" OnClick="lbtnAuto_Click"  CssClass="nav-item nav-link" Font-Size="14px" Font-Bold="true">Automatic Registration</asp:LinkButton>

                            </div>
                        </div>
                        <asp:Panel ID="pnlManualRegistraion" Visible="false" runat="server" Style="padding-top: 10px;">


                            <div class="row mt-3">
                                <div class="col-lg-12">
                                    <h4 class="">Please Note</h4>
                                    <hr style="margin-top:2px;"/>
                                    <ol>
                                        <li>1. All fields marked with <span style="color: red">*</span>are mandatory.</li>
                                        <li>2. Before uploading please ensure all entries are correct.</li>
                                        <%--  <li>For better performance, it is advised to upload the small size files.</li>--%>
                                    </ol>
                                </div>
                            </div>
                            <hr />
                            <div class="row p-2">
                                <div class="col-lg-6">
                                    <p style="margin-bottom: 1px; color: #a8a8a8; font-size: 14px;">
                                        CSC Id<span style="color: red">*</span>
                                    </p>
                                    <asp:TextBox ID="txtcscid" runat="server" autocomplete="off" MaxLength="12" CssClass="form-control "
                                        placeholder="Max. 12 Chararcters" ToolTip="Enter CSC Id/Code"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="UppercaseLetters, Numbers,Custom"
                                        TargetControlID="txtcscid" ValidChars=" " />
                                </div>
                            </div>
                            <div class="row p-2">
                                <div class="col-lg-6">
                                    <p style="margin-bottom: 1px; color: #a8a8a8; font-size: 14px;">
                                        Name<span style="color: red">*</span>
                                    </p>
                                    <asp:TextBox ID="txtcscname" runat="server" autocomplete="off" MaxLength="20" CssClass="form-control "
                                        placeholder="Max. 20 Chararcters" ToolTip="Enter Name"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExten" runat="server" FilterType="UppercaseLetters, LowercaseLetters ,Custom" ValidChars=" " TargetControlID="txtcscname" />

                                </div>
                                <div class="col-lg-6">
                                    <p style="margin-bottom: 1px; color: #a8a8a8; font-size: 14px;">
                                        Contact Person Name<span style="color: red">*</span>
                                    </p>
                                    <asp:TextBox ID="txtcscpersonname" runat="server" autocomplete="off" MaxLength="20" CssClass="form-control "
                                        placeholder="Max. 20 Chararcters" ToolTip="Enter Contact Person Name"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="UppercaseLetters, LowercaseLetters ,Custom" ValidChars=" " TargetControlID="txtcscpersonname" />
                                </div>

                            </div>
                            <div class="row " style="margin-top: 10px;">
                                <div class="col-auto">
                                    <p style="margin-bottom: 1px; color: black; font-size: 18px;">
                                        Contact Details
                                    </p>
                                </div>
                            </div>
                            <div class="row  p-2">
                                <div class="col-lg-6">
                                    <p style="margin-bottom: 1px; color: #a8a8a8; font-size: 14px;">
                                        Mobile<span style="color: red">*</span>
                                    </p>
                                    <asp:TextBox ID="txtmobileno" runat="server" autocomplete="off" MaxLength="10" CssClass="form-control"
                                        placeholder="XXXXX67890" ToolTip="Enter Mobile No."></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                        TargetControlID="txtmobileno" />
                                </div>
                                <div class="col-lg-6">
                                    <p style="margin-bottom: 1px; color: #a8a8a8; font-size: 14px;">
                                        Email<span style="color: red">*</span>
                                    </p>
                                    <asp:TextBox ID="txtemail" runat="server" autocomplete="off" MaxLength="50" CssClass="form-control "
                                        placeholder="XXXXX@gmail.com" ToolTip="Enter Email"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="LowercaseLetters ,Numbers,Custom" ValidChars="@ ." TargetControlID="txtemail" />
                                </div>
                            </div>

                            <div class="row " style="margin-top: 10px;">
                                <div class="col-auto">
                                    <p style="margin-bottom: 1px; color: black; font-size: 18px;">
                                        Address Details
                                                 
                                    </p>
                                </div>
                            </div>
                            <div class="row p-2">
                                <div class="col-lg-12">
                                    <p style="margin-bottom: 1px; color: #a8a8a8; font-size: 14px;">
                                        Address<span style="color: red">*</span>
                                    </p>
                                    <asp:TextBox ID="txtAddress" runat="server" autocomplete="off" MaxLength="200" CssClass="form-control " Style="height: 80px;"
                                        placeholder="Max. 200 Characters" ToolTip="Enter Address"></asp:TextBox>
                                </div>



                            </div>

                            <div class="row pr-2 pl-2 pb-2">


                                <div class="col-md-6">
                                    <p style="margin-bottom: 1px; color: #a8a8a8; font-size: 14px;">
                                        State<span style="color: red">*</span>
                                    </p>
                                    <asp:DropDownList ID="ddlStates" ToolTip="Select State" OnSelectedIndexChanged="ddlStates_SelectedIndexChanged"  AutoPostBack="true" CssClass="form-control  " runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <p style="margin-bottom: 1px; color: #a8a8a8; font-size: 14px;">
                                        District<span style="color: red">*</span>
                                    </p>
                                    <asp:DropDownList ID="ddlDistricts" ToolTip="Select District" CssClass="form-control " runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row pr-2 pl-2 pb-2">
                                <div class="col-lg-6">
                                    <p style="margin-bottom: 1px; color: #a8a8a8; font-size: 14px;">
                                        Pincode<span style="color: red">*</span>
                                    </p>
                                    <asp:TextBox ID="txtpincode" runat="server" autocomplete="off" MaxLength="6" CssClass="form-control"
                                        placeholder="XXXX90" ToolTip="Enter Pincode"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers"
                                        TargetControlID="txtpincode" />
                                </div>

                            </div>
                            <div class="row p-2">
                                <div class="col-lg-6">
                                    <div class="row" style="font-size: 10pt; padding-top: 0px; padding-bottom: 10px;">
                                        <div class="col-lg-12">
                                            <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="btn btn-success"> <i class="fa fa-check"></i> Save </asp:LinkButton>
                                            <asp:LinkButton ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Visible="false" CssClass="btn btn-success"> <i class="fa fa-check"></i> Update </asp:LinkButton>
                                            <asp:LinkButton ID="btnReset" runat="server" OnClick="btnReset_Click" CssClass="btn btn-warning"> <i class="fa fa-times"></i> Reset </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlAutoRegistraion" Visible="false" runat="server">

                            <div class="row mt-3" style="padding-left: 34px">

                                <h6 class="">Please Note</h6>
                                <hr />
                                <ol>
                                    <li>Sub CSC Data can be uploaded here.You can download the sample Excel file from
                                        <asp:LinkButton ID="LinkButton1"  runat="server" OnClick="LinkButton1_Click">here.</asp:LinkButton>
                                    </li>
                                    <li>Before uploading please ensure all entries are correct.</li>
                                    <li>For better performance, it is advised to upload the small size files.</li>
                                </ol>
                            </div>

                            <div class="row mt-3" style="padding-left: 20px">



                                <div class="col-sm-12 mt-1 ">
                                    <h6 class="">Uploaded New File</h6>
                                    <hr />
                                    <%-- <asp:Label ID="Label1" CssClass="mr-2 font-weight-bold "  runat="server" Text="Upload Excel"></asp:Label>--%>
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                    <asp:LinkButton ID="lbtnUploadExcel" OnClientClick="return ShowLoading();"  runat="server" OnClick="lbtnUploadExcel_Click" CssClass="btn btnIcon btn-warning"> <i class="fa fa-upload "></i> Upload Excel</asp:LinkButton>

                                </div>

                                <div class="col-sm-12  mt-5 ">
                                    <asp:Panel runat="server" Visible="false" ID="pnlExcelWithData" CssClass="w-100">


                                        <h6 class="">Summary of Last Uploaded File</h6>
                                        <hr />

                                        <table>
                                            <tr>
                                                <td>
                                                    <div class="w-100 mt-2">
                                                        <label>File Name - </label>

                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="w-100 mt-2">
                                                        <asp:Label runat="server" ID="lblFileName" Font-Bold="true" Text=""></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="w-100  mt-2">
                                                        <asp:Label ID="Label6" runat="server" Text="Correct Records - "></asp:Label>

                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="w-100  mt-2">
                                                        <asp:Label runat="server" ID="lblCorrect" Font-Bold="true" ForeColor="Green" Text=""></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="w-100  mt-2">


                                                        <asp:Label runat="server" Text="Incorrect Records -"></asp:Label>

                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="w-100  mt-2">


                                                        <asp:LinkButton ID="lbtnIncorrect" OnClientClick="return ShowLoading();" ForeColor="RED" Font-Bold="true"  runat="server" OnClick="lbtnIncorrect_Click"></asp:LinkButton>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="w-100  mt-2">

                                                        <asp:Label runat="server" Text="Duplicate Records - "></asp:Label>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="w-100  mt-2">
                                                        <asp:LinkButton ID="lbtnDuplicate" OnClientClick="return ShowLoading();" ForeColor="RED" Font-Bold="true"  runat="server" OnClick="lbtnDuplicate_Click"></asp:LinkButton>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="w-100  mt-2">

                                                        <asp:Label runat="server" Text="Already Exist Records - "></asp:Label>

                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="w-100  mt-2">
                                                        <asp:LinkButton ID="lbtnAlready" OnClientClick="return ShowLoading();" ForeColor="RED" Font-Bold="true"  runat="server" OnClick="lbtnAlready_Click"></asp:LinkButton>

                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="w-100  mt-2">
                                                        <asp:Label ID="lbldraftid" Visible="false" runat="server" Text="Label"></asp:Label>
                                                        <asp:Label runat="server" Text="Upload Date - "></asp:Label>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="w-100  mt-2">
                                                        <asp:Label runat="server" ID="lblDate" Font-Bold="true" Text=""></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel runat="server" Visible="false" ID="pnlExcelNoData">
                                        <asp:Label runat="server" Style="color: #a8a8a8; font-weight: bold; font-size: 16px;" Text="No Uploaded Excel File"></asp:Label>
                                    </asp:Panel>

                                </div>
                                <%--    <div class="col-sm-12  mt-2 py-4">
                                    <asp:Panel runat="server" Visible="false" ID="pnlExcelWithData" CssClass="w-100">


                                        <h3 class="text-center">Last Uploaded File</h3>

                                        <div class="w-100 text-center">
                                            <i class="fa fa-file-o fa-5x"></i>
                                        </div>
                                        <div class="w-100 text-center mt-2">
                                            <asp:Label runat="server" ID="lblFileName" Font-Bold="true" Style="font-size: 20px;" Text=""></asp:Label>
                                        </div>



                                        <div class="w-100 text-center mt-2">
                                            <asp:Label ID="Label6" runat="server" Style="font-size: 20px; color: #a8a8a8;" Text="Correct Records - "></asp:Label>
                                            <asp:Label runat="server" ID="lblCorrect" Font-Bold="true" ForeColor="Green" Style="font-size: 20px;" Text=""></asp:Label>
                                        </div>



                                        <div class="w-100 text-center mt-2">


                                            <asp:Label runat="server" Style="color: #a8a8a8; font-size: 20px;" Text="Incorrect Records -"></asp:Label>
                                            <asp:LinkButton ID="lbtnIncorrect" ForeColor="RED" Font-Bold="true" Style="font-size: 20px;" OnClick="lbtnIncorrect_Click" runat="server"></asp:LinkButton>


                                            <asp:Label runat="server" Style="color: #a8a8a8; font-size: 20px; margin-left: 20px" Text="Duplicate Records - "></asp:Label>
                                            <asp:LinkButton ID="lbtnDuplicate" ForeColor="RED" Font-Bold="true" Style="font-size: 20px;" OnClick="lbtnDuplicate_Click" runat="server"></asp:LinkButton>
                                        </div>


                                        <div class="w-100 text-center mt-2">

                                            <asp:Label runat="server" Style="color: #a8a8a8; font-size: 20px;" Text="Already Exist Records - "></asp:Label>
                                            <asp:LinkButton ID="lbtnAlready" ForeColor="RED" Font-Bold="true" Style="font-size: 20px;" OnClick="lbtnAlready_Click" runat="server"></asp:LinkButton>

                                        </div>

                                        <div class="w-100 text-center mt-2">
                                            <asp:Label ID="lbldraftid" Visible="false" runat="server" Text="Label"></asp:Label>
                                            <asp:Label runat="server" Style="color: #a8a8a8; font-size: 20px;" Text="Upload Date - "></asp:Label>
                                            <asp:Label runat="server" ID="lblDate" Font-Bold="true" Style="font-size: 20px;" Text=""></asp:Label>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel runat="server" Visible="false" ID="pnlExcelNoData">
                                        <asp:Label runat="server" Style="color: #a8a8a8; font-weight: bold; font-size: 16px;" Text="No Uploaded Excel File"></asp:Label>
                                    </asp:Panel>

                                </div>--%>
                            </div>

                        </asp:Panel>
                    </div>
                </div>
            </div>

            <div class="col-md-6 ">
                <div class="card mt-2 shadow">
                    <div class="card-header" style="height: 48px;">
                        <div class="row ">
                            <div class="col">
                                <strong>Sub CSC Details</strong>
                            </div>

                            <div class="col-auto input-group float-right" style="width: auto;">
                                <asp:TextBox ID="txtSearch" placeholder="Agent Code/Name/Mobile/Email" runat="server" CssClass="form-control " Style="max-width: 370px; min-width: 220px; margin-top: -5px; font-size: smaller;"></asp:TextBox>


                                <asp:LinkButton ID="lbtnSearch" OnClientClick="return ShowLoading();" ToolTip="Click here for Search"  runat="server" OnClick="lbtnSearch_Click" Style="margin-top: -5px" CssClass="btn btn-warning ml-1">
                                                                    <i class="fa fa-search" ></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body" style="min-height: 80vh;">
                        <asp:GridView ID="grvCSCDeatails1" OnRowDataBound="grvCSCDeatails1_RowDataBound" runat="server" OnRowCommand="grvCSCDeatails1_RowCommand"
                            OnPageIndexChanging="grvCSCDeatails1_PageIndexChanging" AllowPaging="true" PageSize="15"
                            ClientIDMode="Static" AutoGenerateColumns="False" ForeColor="#333333" Font-Size="14px"
                            GridLines="None" Font-Bold="false" DataKeyNames="agent_id,val_agent_code,val_address,state_code,
                                    district_code,email_id,contact_person,mobile_no,val_status,status_reason,registration_date,
                            update_date, update_by,pan_no,service_taxno,securoty_amount,deposit_type,
                                    val_bank,registration_refno,valid_from,valid_to,agent_type,depot_code,station_code,trip_yn,csp_agent,ag_name,val_pincode,csc_id,current_balance"
                            Width="100%">
                            <Columns>

                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAGENTNAME1" runat="server" Text='<%# Eval("agent_name") %>'></asp:Label>
                                        <br />
                                        <span class="text-muted">Currnet Balance</span><b> <%# Eval("current_balance") %> <i class="fa fa-rupee"></i></b>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                          
                                <asp:TemplateField HeaderText="Mobile/Email">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMOBILENO1" runat="server" Text='<%# Eval("mobile_no") %>'></asp:Label>
                                        <br />
                                        <asp:Label ID="lblEMAIL1" runat="server" Text='<%# Eval("email_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                               <div class="btn-group">
                                                    <asp:LinkButton ID="lbtnViewDetails" OnClientClick="return ShowLoading();" runat="server" CommandName="UpdateDetails" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    class="btn btnIcon btn-success p-2" ToolTip="Update Details"><i class="fa fa-edit  "></i> </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnActive" runat="server" OnClientClick="return ShowLoading();" CommandName="Active" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    class="btn btnIcon btn-info p-2" ToolTip="Active Agent"><i class="fa fa-check"></i> </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnDeactive" runat="server" OnClientClick="return ShowLoading();" CommandName="Deactive" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    class="btn btnIcon btn-info p-2"  ToolTip="Deactive Agent"><i class="fa fa-times"></i> </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnChngePwd" OnClientClick="return ShowLoading();" runat="server" Enabled="true" CommandName="ChngePwd" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    class="btn btnIcon btn-danger p-2" ToolTip="ChangevAgent Password"><i class="fa fa-key"></i> </asp:LinkButton>
                                                <asp:LinkButton ID="LbtnSetLimit" OnClientClick="return ShowLoading();" runat="server" CommandName="Limit" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    class="btn btnIcon btn-warning p-2" ToolTip="Set Agent Limit"><i class="fa fa-rupee-sign "></i> </asp:LinkButton>
                                          
                                               </div>
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
            <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
                CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Confirmation
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnYesConfirmation" runat="server" OnClick="lbtnYesConfirmation_Click" OnClientClick="return ShowLoading();" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                            <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2" > <i class="fa fa-times"></i> No </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button4" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

            <div class="row">
            <cc1:ModalPopupExtender ID="mperror" runat="server" PopupControlID="pnlerror" CancelControlID="lbtnclose1"
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
                            <asp:LinkButton ID="lbtnsuccessclose1" runat="server" CssClass="btn btn-success btn-sm ml-2" > <i class="fa fa-check"></i> OK </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button6" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>


        <div class="row">
            <cc1:ModalPopupExtender ID="mpDraftData" runat="server" PopupControlID="pnlDraftData" TargetControlID="Button146456"
                CancelControlID="LinkButton19898" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlDraftData" runat="server" Style="position: fixed;">
                <div class="modal-content mt-1">
                    <div class="modal-header">
                        <div class="col-md-10">
                            <h3 class="m-0">
                                <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></h3>
                            <h6 class="text-danger">
                                <asp:Label ID="lblsumrymsg" runat="server" Text=""></asp:Label></h6>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="LinkButton19898" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="modal-body p-0">
                        <div class="card" style="font-size: 12px; min-height: 100px; min-width: 800px">
                            <div class="row mt-0">
                                <div class="col-sm-12 flex-column d-flex stretch-card ">
                                    <div class="card-body table table-responsive">
                                        <asp:GridView ID="gvDraftData" runat="server"
                                            AllowPaging="false"
                                            ClientIDMode="Static" AutoGenerateColumns="False" ForeColor="#333333" Font-Size="14px"
                                            GridLines="None" Font-Bold="false"
                                            Width="100%">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Agent Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAGENTNAME1" runat="server" Text='<%# Eval("cscname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAGENTNAME1" runat="server" Text='<%# Eval("val_email") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAGENTNAME1" runat="server" Text='<%# Eval("val_mobile") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="State">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEMAIL1" runat="server" Text='<%# Eval("val_state") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="District">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEMAIL1" runat="server" Text='<%# Eval("val_district") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <PagerStyle CssClass="pagination-ys" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button146456" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

        <div class="row">
            <cc1:ModalPopupExtender ID="MpAgentLimit" runat="server" PopupControlID="pnlAgentLimit" TargetControlID="Button7666" BehaviorID="bvConfirm"
                CancelControlID="LinkButton1984548" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlAgentLimit" runat="server" Style="position: fixed;">
                <div class="modal-content mt-1">
                    <div class="modal-header">
                        <div class="col-md-10">
                            <h6 class="m-0">
                                <asp:Label ID="lblAgmimitmsg" runat="server" Text="Set Agent Limit" Font-Size="Large"></asp:Label></h6>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="LinkButton1984548" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="modal-body p-0">
                        <div class="card" style="font-size: 12px; min-height: 100px; min-width: 450px">
                            <div class="row mt-0">
                                <div class="col-sm-12 flex-column d-flex stretch-card ">
                                    <div class="card-body table table-responsive">
                                         <div class="row p-2" style="margin-top: -20px;margin-bottom: -11px;">
                                                    <div class="col-lg-12 text-center">
                                                        <asp:Label ID="lblbalance" runat="server" Style="margin-bottom: 1px; color: #a8a8a8; font-weight: bold; font-size: 25px;" Text="0"></asp:Label>
                                                        <hr />
                                                    </div>
                                                </div>
                                                <div class="row p-2" style="margin-bottom: 7px;">
                                                    <div class="col-lg-6 text-right" style="padding-top: 6px;">
                                                        <asp:Label ID="Label4" runat="server" Style="margin-bottom: 1px; color: black; font-size: 16px;" Text="Amount to be allocated"></asp:Label>
                                                        <span class="text-danger">*</span>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <asp:TextBox ID="txtmaximum"  runat="server" autocomplete="off" MaxLength="10" CssClass="form-control mt-1 "
                                                            placeholder="Enter Amount ₹"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtmaximum" />
                                                    </div>
                                                </div>
                                         <div class="row p-2" style="margin-bottom: 7px;">
                                            <div class="col-lg-12 text-center">
                                                <asp:Label ID="lbllimitmsg" runat="server" Text="" Style="color: red;"></asp:Label>
                                            </div>
                                        </div>
                                                <div class="row p-2">
                                                    <div class="col-lg-12 text-center">
                                                        <asp:LinkButton ID="lbtnLimitSave" runat="server" OnClick="lbtnLimitSave_Click" OnClientClick="$find('bvConfirm').hide();ShowLoading();" Visible="true"  CssClass="btn btn-success ml-2"> <i class="fa fa-check"></i> Save </asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnLimitReset" runat="server" OnClick="lbtnLimitReset_Click"  CssClass="btn btn-warning ml-2" OnClientClick="return ShowLoading();"> <i class="fa fa-times"></i> Reset </asp:LinkButton>
                                                    </div>
                                                </div>
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button7666" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

        <div class="row">
            <cc1:ModalPopupExtender ID="MPChangePass" runat="server" PopupControlID="Panel1" TargetControlID="Button1323"
                CancelControlID="LinkButton198983" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="Panel1" runat="server" Style="position: fixed;">
                <div class="modal-content mt-1">
                    <div class="modal-header">
                        <div class="col-md-10">
                            <h3 class="m-0">
                                <asp:Label ID="Label1" runat="server" Text="Change Password"></asp:Label></h3>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="LinkButton198983" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="modal-body p-0">
                        <div class="card" style="font-size: 12px; min-height: 100px; min-width: 450px">
                            <div class="row mt-0">
                                <div class="col-sm-12 flex-column d-flex stretch-card ">
                                    <div class="card-body table table-responsive">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12 input-group-prepend ">
                                                    <asp:Label ID="Label2" runat="server" Text="Enter New Password"></asp:Label>
                                                    <asp:TextBox ID="txtnewpass" autocomplete="off" placeholder="Enter Password" CssClass="form-control " runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-12 input-group-prepend mt-3 text-right ">
                                                    <asp:LinkButton ID="lbtnChangepass"  runat="server" OnClick="lbtnChangepass_Click" OnClientClick="return ShowLoading();" Visible="true" CssClass="btn btn-success ml-2"> <i class="fa fa-check"></i> Change Password </asp:LinkButton>

                                                    <asp:LinkButton ID="lbtnResetChangepass" runat="server" OnClick="lbtnResetChangepass_Click" OnClientClick="return ShowLoading();" CssClass="btn btn-warning ml-2"> <i class="fa fa-times"></i> Reset </asp:LinkButton>

                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button1323" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

            <div class="row">
            <cc1:ModalPopupExtender ID="mpDeactivation" runat="server" PopupControlID="pnlDeactivation" CancelControlID="lbtnNo"
                TargetControlID="Button2" BackgroundCssClass="modalBackground" BehaviorID="bvvConfirm">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlDeactivation" runat="server" Style="position: fixed; display: none;">
                <div class="card" style="width: 620px; max-width: 90vw; max-height: 90vh;">
                    <div class="card-header">
                        <div class="card-title m-0">
                            <asp:Label ID="Label9" runat="server" Text="Please Note" CssClass="font-weight-600"></asp:Label>
                        </div>
                    </div>
                    <div class="card-body text-left overflow-auto" style="min-height: 100px;">                       
                        <p> After deactivation of <asp:Label ID="lblcscname" runat ="server" CssClass="font-weight-bold"></asp:Label>, wallet balance of csc will become zero and will be credited in your wallet.<br />
                            If you are sure please give reason of deactivation
                        </p>
                        <p>
                            <asp:TextBox ID="txtdeactivatereason" runat="server" TextMode="MultiLine" placeholder="Max. 50 Characters" CssClass="form-control" Visible="true" Style="resize: none; height: 65px;"></asp:TextBox>
                        </p>
                        
                    </div>
                    <div class="card-footer ">
                        <div class="row">
                            <div class="col-lg-5">
                                <p class="font-weight-600 text-danger">Do you want to deactivate csc ?</p>
                            </div>
                            <div class="col-lg-7 text-right">
                                <asp:LinkButton ID="lbtnyes" runat="server" Onclick="lbtnyes_Click"  CssClass="btn btn-success" OnClientClick="$find('bvvConfirm').hide();ShowLoading();"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                                <asp:LinkButton ID="lbtnNo" runat="server" CssClass="btn btn-warning" > <i class="fa fa-times"></i> No </asp:LinkButton>
                            </div>
                        </div>


                    </div>

                </div>
                <div style="visibility: hidden; height: 0px;">
                    <asp:Button ID="Button2" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

    </div>
</asp:Content>



