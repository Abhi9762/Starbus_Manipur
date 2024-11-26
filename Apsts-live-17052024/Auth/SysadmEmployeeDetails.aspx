<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="SysadmEmployeeDetails.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="Auth_SysadmEmployeeDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .groupHead {
            background: #e9e4e4;
            color: black !important;
            padding: 5px 10px;
            font-size: 12pt;
        }

        input[type='radio'], input[type='checkbox'] {
            height: 16px;
            width: 17px;
        }

        .form-control-label1 {
            font-size: .75rem;
            font-weight: 600;
            color: white;
        }
    </style>
    <script>
        $(document).ready(function () {

            var currDate = new Date().getDate();
            var preDate = new Date(new Date().setDate(currDate - 1));
            var todayDate = new Date(new Date().setDate(currDate));
            var FutureDate = new Date(new Date().setDate(currDate + 30000));

            $('[id*=tbDob]').datepicker({
                endDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
            $('[id*=tbDateOfJoining]').datepicker({
                endDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            })
                .on('changeDate', function (selected) {
                    var minDate = new Date(selected.date.valueOf());
                    $('[id*=tbDOJOffice]').datepicker('setStartDate', minDate);
                });
            $('[id*=tbDOJOffice]').datepicker({
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
            $('[id*= tblicensevalidupto]').datepicker({
                endDate: FutureDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });

            
             $('[id*=tbOrderDateUpdate]').datepicker({
                endDate: FutureDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
            $('[id*=tbPostingDate]').datepicker({
                endDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
            $('[id*=tbDobUpdate]').datepicker({
                endDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
            $('[id*=tbDoJupdate]').datepicker({
                endDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            }).on('changeDate', function (selected) {
                var minDate = new Date(selected.date.valueOf());
                $('[id*=tbDoJupdateOf]').datepicker('setStartDate', minDate);
            });;
            $('[id*=tbDoJupdateOf]').datepicker({
                endDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
            $('[id*=tbDateOfLicense]').datepicker({
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
            $('[id*=tbPostingDate]').datepicker({
                endDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
        });
        function UploadImage(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnUploadImage.ClientID %>").click();
            }
        }
        function UploadImageUpdate(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnImageUpdate.ClientID %>").click();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="header pb-7">
        <asp:HiddenField ID="hidtoken" runat="server" />
    </div>
    <div class="container-fluid mt--6">
        <div class="row align-items-center">
            <div class="col-lg-12 col-md-12">
                <div class="card card-stats mb-3">
                    <div class="row">
                        <div class="col-md-3 col-lg-3 border-right">
                            <div class="card-body">
                                <div class="row mx-1">
                                    <div class="col-lg-12 col-md-12">
                                        <asp:Label ID="lblTotalEmp" runat="server" CssClass="ml--2 text-capitalize" Style="font-size: small; font-weight: bold" Text="Total Employees- 0"></asp:Label>

                                        <div class="row mt-4">
                                            <div class="col-lg-6 col-md-6 border-right">
                                                <h5 class="card-title text-uppercase mb-0">Verified:&nbsp;
                                 <asp:Label ID="lblVerified" runat="server" ToolTip="Verified Employees" CssClass="font-weight-bold mb-0 text-right" Text="0"></asp:Label></h5>
                                            </div>
                                            <div class="col-md-6 col-lg-6 ">
                                                <h5 class="card-title text-uppercase mb-0">Not-Verified:&nbsp;
                                 <asp:Label ID="lblNotVerified" runat="server" ToolTip="Not-Verified Employees" class="font-weight-bold mb-0 text-right" Text="0"></asp:Label></h5>
                                            </div>



                                        </div>

                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 border-right">
                            <div class="card-body">
                                <div class="row mx-1">
                                    <div class="col-lg-12 col-md-12">
                                        <asp:Label ID="lblPending" runat="server" CssClass="ml--4 text-capitalize" Style="font-size: small; font-weight: bold">Pending License Details</asp:Label>
                                        <br />
                                        <asp:Label runat="server" CssClass="ml--4 text-capitalize" Style="font-size: small;">(Only Verified Employees)</asp:Label>




                                        <div class="row ml--4">
                                            <div class="col-md-6 col-lg-6 border-right">
                                                <h5 class="card-title text-uppercase mb-0">Driver:&nbsp;
                                 <asp:Label ID="lblDriverP" runat="server" ToolTip="Driver Pending License" CssClass="font-weight-bold mb-0 text-right" Text="0"></asp:Label></h5>
                                            </div>
                                            <div class="col-md-6 col-lg-6">
                                                <h5 class="card-title text-uppercase mb-0">Conductor:&nbsp;
                                 <asp:Label ID="lblConductorP" runat="server" ToolTip="Conductor Pending License" class="font-weight-bold mb-0 text-right" Text="0"></asp:Label></h5>
                                            </div>



                                        </div>

                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 border-right">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col">
                                        <div class="text-capitalize ml--2" style="font-size: small; font-weight: bold">License Expiring Soon</div>
                                        <label class="text-capitalize ml--2" style="font-size: small;">(Only Verified Employees)</label>


                                        <div class="row">
                                            <div class="col-md-6 col-lg-6 border-right">
                                                <h5 class="card-title text-uppercase mb-0">Driver:&nbsp;
                                 <asp:Label ID="lblDriverExpSoon" runat="server" ToolTip="Driver License Expiring Soon" CssClass="font-weight-bold mb-0 text-right" Text="0"></asp:Label></h5>
                                            </div>
                                            <div class="col-md-6 col-lg-6">
                                                <h5 class="card-title text-uppercase mb-0">Conductor:&nbsp;
                                 <asp:Label ID="lblConductorExpSoon" runat="server" ToolTip="Conductor License Expiring Soon" class="font-weight-bold mb-0 text-right" Text="0"></asp:Label></h5>
                                            </div>



                                        </div>

                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-lg-3 col-lg-3">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col">
                                        <div class="text-capitalize" style="font-size: small; font-weight: bold">Expired License</div>
                                        <label class="text-capitalize" style="font-size: small;">(Only Verified Employees)</label>



                                    </div>
                                    <div class="col-auto">

                                        <asp:LinkButton ID="lbtnview" runat="server" ToolTip="View Instructions" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-eye"></i>
                                        </asp:LinkButton>

                                    </div>

                                </div>
                                <div class="row">

                                    <div class="col-lg-6 col-md-6 border-right">
                                        <h5 class="card-title text-uppercase mb-0">Driver:&nbsp;
                                 <asp:Label ID="lblDriverExp" runat="server" ToolTip="Driver Expired License" CssClass="font-weight-bold mb-0 text-right" Text="0"></asp:Label></h5>
                                    </div>
                                    <div class="col-md-6 col-lg-6">
                                        <h5 class="card-title text-uppercase mb-0">Conductor:&nbsp;
                                 <asp:Label ID="lblConductorExp" runat="server" ToolTip="Conductor Expired License" class="font-weight-bold mb-0 text-right" Text="15"></asp:Label></h5>
                                    </div>



                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-3">
            </div>
            <div class="col-9">
                <asp:Panel ID="pnlButton" runat="server">
                    <div class="row mb-3">

                        <div class="col-lg-12 col-md-12 text-center form-check">
                            <asp:RadioButton runat="server" ID="rbtnAdd" CssClass="form-check-input pr-2" OnCheckedChanged="rbtnAdd_CheckedChanged" GroupName="radio" Checked="true" AutoPostBack="true" />
                            <span class="checkmarkRadio"></span>
                            <asp:Label runat="server" For="rbtnAdd" Font-Bold="true" ToolTip="Add New Employee" CssClass="ml-2" Font-Size="small">Add New Employee &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:Label>

                            <asp:RadioButton runat="server" ID="rbtnUpdate" CssClass="form-check-input" OnCheckedChanged="rbtnUpdate_CheckedChanged" GroupName="radio" AutoPostBack="true" />
                            <span class="checkmarkRadio"></span>
                            <asp:Label runat="server" For="rbtnUpdate" Font-Bold="true" ToolTip="Update Employee" CssClass="ml-2" Font-Size="small">Update Employee&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:Label>

                            <asp:RadioButton runat="server" ID="rbtnVerUpdate" CssClass="form-check-input" ToolTip="Update Verified Employee" OnCheckedChanged="rbtnVerUpdate_CheckedChanged" GroupName="radio" AutoPostBack="true" />
                            <span class="checkmarkRadio"></span>
                            <asp:Label runat="server" For="rbtnVerUpdate" CssClass="ml-2" Font-Bold="true" Font-Size="small">Update Verified Employee&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:Label>
                        </div>
                    </div>

                </asp:Panel>

                <div class="row align-items-center">
                    <div class="col-lg-12 col-md-12">
                        <div class="card card-stats mb-3">

                            <asp:Panel ID="pnlAddNewEmployee" runat="server">
                                <div class="row mx-3">

                                    <div class="col-lg-12 col-md-12">

                                        <div class="row align-items-center">
                                            <div class="col-md-6 col-lg-6 card-header text-left!important">
                                                <asp:Label ID="LabelHeader" runat="server" Text="Add New Employee" Font-Size="medium"></asp:Label>
                                                <asp:Label ID="LabelHeaderUpdate" runat="server" Visible="false" Text="Update Employee Details" Font-Size="medium"></asp:Label>
                                            </div>
                                            <div class="col-md-6 col-lg-6 text-right">
                                                <span class="text-warning">* &nbsp; Fields are mandatory</span>
                                            </div>



                                        </div>

                                        <div class="row text-left py-2">

                                            <div class="col-lg-12 col-md-12">
                                                <div class="card-header groupHead">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="small" ForeColor="black" Text="1. Personal Details"></asp:Label>
                                                </div>



                                            </div>

                                        </div>

                                        <div class="col-lg-12 col-md-12">
                                            <div class="row">
                                                <div class="col-lg-3 col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label"> First Name<span class="text-warning">*</span></asp:Label>

                                                    <asp:TextBox AutoComplete="true" CssClass="form-control form-control-sm" runat="server" ID="tbFirstName" MaxLength="50" ToolTip="Enter First Name"
                                                        placeholder="Max 50 char" Text="" Style=""></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajxFtFirstName" runat="server" ValidChars=" " FilterType="UppercaseLetters, LowercaseLetters,Custom  "
                                                        TargetControlID="tbFirstName" />
                                                </div>
                                                <div class="col-lg-3 col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label">Middle Name</asp:Label>

                                                    <asp:TextBox AutoComplete="true" class="form-control form-control-sm" runat="server" ID="tbMiddleName" MaxLength="50" ToolTip="Enter Middle Name"
                                                        placeholder="Max 50 char" Text="" Style=""></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajxFtMiddleName" runat="server" ValidChars=" " FilterType="UppercaseLetters, LowercaseLetters,Custom"
                                                        TargetControlID="tbMiddleName" />
                                                </div>
                                                <div class="col-lg-3 col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label">Last Name</asp:Label>

                                                    <asp:TextBox AutoComplete="true" class="form-control form-control-sm" runat="server" ID="tbLastName" MaxLength="50" ToolTip="Enter Last Name"
                                                        placeholder="Max 50 char" Text="" Style=""></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajxFtLastName" ValidChars=" " runat="server" FilterType="UppercaseLetters, LowercaseLetters,Custom"
                                                        TargetControlID="tbLastName" />
                                                </div>
                                                <div class="col-lg-3 col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label">  Date Of Birth<span class="text-warning">*</span></asp:Label>
                                                    <div class="input-group">
                                                        <asp:TextBox AutoComplete="true" ID="tbDob" runat="server" CssClass="form-control form-control-sm" Width="68%" TabIndex="5" Placeholder="DD/MM/YYYY"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbDob" ValidChars="/" />
                                                    </div>


                                                </div>

                                            </div>
                                        </div>


                                        <div class="col-lg-12 col-md-12 mb-2">
                                            <div class="row">
                                                <div class="col-lg-3 col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label"> Father Name</asp:Label>

                                                    <asp:TextBox AutoComplete="true" class="form-control form-control-sm" runat="server" ID="tbFatherName" MaxLength="50" ToolTip="Father Name"
                                                        placeholder="Max 50 char" Text="" Style=""></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajxFtFatherName" runat="server" ValidChars=" " FilterType="UppercaseLetters, LowercaseLetters,Custom"
                                                        TargetControlID="tbFatherName" />
                                                </div>
                                                <div class="col-lg-3 col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label">Gender<span class="text-warning">*</span></asp:Label>

                                                    <asp:DropDownList ID="ddlGender" CssClass="form-control form-control-sm" runat="server" ToolTip="Select Gender">
                                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                        <asp:ListItem Value="M" Text="Male"></asp:ListItem>
                                                        <asp:ListItem Value="F" Text="Female"></asp:ListItem>

                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-lg-3 col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label">Blood Group</asp:Label>

                                                    <asp:DropDownList CssClass="form-control form-control-sm" runat="server" ID="ddlBloodGroup" ToolTip="Select Blood Group"></asp:DropDownList>
                                                </div>


                                            </div>
                                        </div>

                                        <div class="row text-left py-2">

                                            <div class="col-lg-12 col-md-12">
                                                <div class="card-header groupHead">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="small" ForeColor="black" Text="2. Contact Details"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-lg-12 col-md-12">
                                            <div class="row">
                                                <div class="col-lg-3 col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label">Email ID</asp:Label>

                                                    <asp:TextBox AutoComplete="true" class="form-control form-control-sm" runat="server" ID="tbEmail" MaxLength="50" ToolTip="Enter Email Id"
                                                        placeholder="Max 50 char" Text="" Style=""></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajxFtEmailID" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters, Custom" ValidChars=".@"
                                                        TargetControlID="tbEmail" />
                                                </div>
                                                <div class="col-lg-3 col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label">Mobile Number<span class="text-warning">*</span></asp:Label>

                                                    <asp:TextBox AutoComplete="true" class="form-control form-control-sm" runat="server" ID="tbMobile" MaxLength="10" ToolTip="Enter Mobile Number"
                                                        placeholder="Max 10 char" Text="" Style=""></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajxFtMobileNumber" runat="server" FilterType="Numbers" ValidChars=""
                                                        TargetControlID="tbMobile" />
                                                </div>
                                                <div class="col-lg-3 col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label">Landline Number</asp:Label>

                                                    <asp:TextBox AutoComplete="true" class="form-control form-control-sm" runat="server" ID="tbLandline" MaxLength="11" ToolTip="Enter Landline Number"
                                                        placeholder="Max 11 char" Text="" Style=""></asp:TextBox>
                                                </div>
                                                <div class="col-lg-3 col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label">Emergency No<span class="text-warning">*</span></asp:Label>


                                                    <asp:TextBox AutoComplete="true" ID="tbEmergency" runat="server" CssClass="form-control form-control-sm" ToolTip="Enter Emergency No." MaxLength="10" Placeholder="Max. 10 char"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajxFtEmergencyNo" runat="server" FilterType="Numbers" ValidChars=""
                                                        TargetControlID="tbEmergency" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-12 col-md-12">
                                            <div class="row">
                                                <div class="col-lg-3 col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label">Address</asp:Label>

                                                    <asp:TextBox AutoComplete="true" class="form-control form-control-sm" runat="server" ID="tbAddress" MaxLength="200" TextMode="MultiLine" Height="30px" Resize="None" ToolTip="Enter Address"
                                                        placeholder="Max 200 char" Text="" Style=""></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajxFtAddress" runat="server" FilterType="UppercaseLetters,Numbers,LowercaseLetters,Custom" ValidChars=",/ -"
                                                        TargetControlID="tbAddress" />

                                                </div>
                                                <div class="col-lg-3 col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label">Pin Code</asp:Label>

                                                    <asp:TextBox AutoComplete="true" ID="tbPinCode" MaxLength="6" placeholder="Max 6 char" CssClass="form-control form-control-sm" runat="server" ToolTip="Enter Pincode"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajxFtPincode" runat="server" FilterType="Numbers" ValidChars=""
                                                        TargetControlID="tbPinCode" />
                                                </div>
                                                <div class="col-lg-3 col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label">City <span class="text-warning">*</span></asp:Label>

                                                    <asp:TextBox AutoComplete="true" CssClass="form-control form-control-sm" MaxLength="20" placeholder="Max 20 char" runat="server" ID="tbCity" ToolTip="Enter City"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajxFtCity" runat="server" FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=" "
                                                        TargetControlID="tbCity" />
                                                </div>
                                                <div class="col-lg-3 col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label">State <span class="text-warning">*</span></asp:Label>

                                                    <asp:DropDownList CssClass="form-control form-control-sm" runat="server" ID="ddlState" ToolTip="Select State"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row text-left py-2">
                                            <div class="col-lg-12 col-md-12 py-2">
                                                <div class="card-header groupHead">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="small" ForeColor="black" Text="3. Official Details"></asp:Label>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-lg-12 col-md-12">
                                            <div class="row">
                                                <div class="col-lg-3 col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label">Office Level <span class="text-warning">*</span></asp:Label>

                                                    <asp:DropDownList class="form-control form-control-sm" runat="server" ID="ddlOfficeLevel" OnSelectedIndexChanged="ddlOfficeLevel_SelectedIndexChanged" ToolTip="Select Office Level" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                                <div class="col-lg-3 col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label">Office<span class="text-warning">*</span></asp:Label>

                                                    <asp:DropDownList class="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlOfficeType_SelectedIndexChanged" runat="server" ID="ddlOfficeType" ToolTip="Select Select Office"></asp:DropDownList>
                                                </div>

                                                <div class="col-lg-3 col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label">  Posting Office<span class="text-warning">*</span></asp:Label>


                                                    <asp:DropDownList class="form-control form-control-sm" runat="server" ID="ddlPostingoffice" ToolTip="Select Select Office"></asp:DropDownList>


                                                </div>
                                                <div class="col-lg-3 col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label">Date of Posting</asp:Label>
                                                    <div class="input-group">
                                                        <asp:TextBox AutoComplete="true" ID="tbDOPoffice" runat="server" CssClass="form-control form-control-sm" Width="68%" TabIndex="5" Placeholder="DD/MM/YYYY"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbDOPoffice" ValidChars="/" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-lg-12 col-md-12">
                                            <div class="row">
                                                <div class="col-lg-3 col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label">Employee Class <span class="text-warning">*</span></asp:Label>

                                                    <asp:DropDownList class="form-control form-control-sm" runat="server" ID="ddlEmpClass" ToolTip="Select Employee Class"></asp:DropDownList>
                                                </div>
                                                <div class="col-lg-3 col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label">Duty Type<span class="text-warning">*</span></asp:Label>

                                                    <asp:DropDownList class="form-control form-control-sm" runat="server" ID="ddlEmpdutyType" ToolTip="Select Employee type"></asp:DropDownList>
                                                </div>
                                                <div class="col-lg-3 col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label">  Date Of Joining</asp:Label>
                                                    <div class="input-group">
                                                        <asp:TextBox AutoComplete="true" ID="tbDateOfJoining" runat="server" CssClass="form-control form-control-sm" Width="68%" TabIndex="5" Placeholder="DD/MM/YYYY"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbDateOfJoining" ValidChars="/" />
                                                    </div>


                                                </div>
                                                <div class="col-lg-3 col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label">Date of Joining at Office</asp:Label>
                                                    <div class="input-group">
                                                        <asp:TextBox AutoComplete="true" ID="tbDOJOffice" runat="server" CssClass="form-control form-control-sm" Width="68%" TabIndex="5" Placeholder="DD/MM/YYYY"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbDOJOffice" ValidChars="/" />
                                                    </div>


                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-lg-12 col-md-12">
                                            <div class="row">
                                                <div class="col-lg-3  col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label">Employee Type<span class="text-warning">*</span></asp:Label>
                                                    <asp:DropDownList class="form-control form-control-sm" runat="server" ID="ddlEmployeetype" ToolTip="select employee type">
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-lg-3 col-md-3 mb-2">
                                                    <asp:Label runat="server" CssClass="form-control-label">Designation<span class="text-warning">*</span></asp:Label>

                                                    <asp:DropDownList class="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddldesignation_SelectedIndexChanged" runat="server" ID="ddldesignation" ToolTip="select Designation"></asp:DropDownList>
                                                </div>

                                                <div class="col-lg-3 col-md-3 mb-2" id="divlicense" visible="false" runat="server">
                                                    <asp:Label runat="server" CssClass="form-control-label">License No.</asp:Label>

                                                    <div class="input-group">
                                                        <asp:TextBox AutoComplete="true" ID="tblicenseNoS" runat="server" CssClass="form-control form-control-sm" Width="68%" TabIndex="5" Placeholder="License No."></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="col-lg-3  col-md-3" id="divlicensevlaid" runat="server" visible="false">
                                                    <asp:Label runat="server" CssClass="form-control-label">License Valid Upto</asp:Label>

                                                    <div class="input-group">
                                                        <asp:TextBox AutoComplete="true" ID="tblicensevalidupto" runat="server" CssClass="form-control form-control-sm" Width="68%" TabIndex="5" Placeholder="DD/MM/YYYY"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tblicensevalidupto" ValidChars="/" />
                                                    </div>

                                                </div>

                                            </div>
                                        </div>

                                        <div class="row text-left py-2">

                                            <div class="col-lg-12 col-md-12">
                                                <div class="card-header groupHead">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="small" ForeColor="black" Text="4. Weekly Rest Day"></asp:Label>
                                                </div>
                                            </div>
                                        </div>



                                        <div class="col-lg-12 col-md-12">
                                            <div class="row mb-2">
                                                <div class="col-lg-3 col-md-3">
                                                    <asp:Label runat="server" CssClass="form-control-label">Weekly Rest Day</asp:Label>

                                                    <asp:DropDownList class="form-control form-control-sm" runat="server" ID="ddlWeeklyRestDay" ToolTip="select Weekly Rest Day">
                                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                        <asp:ListItem Value="S" Text="Sunday"></asp:ListItem>
                                                        <asp:ListItem Value="M" Text="Monday"></asp:ListItem>
                                                        <asp:ListItem Value="T" Text="Tuesday"></asp:ListItem>
                                                        <asp:ListItem Value="W" Text="Wednesday"></asp:ListItem>
                                                        <asp:ListItem Value="Th" Text="Thursday"></asp:ListItem>
                                                        <asp:ListItem Value="F" Text="Friday"></asp:ListItem>
                                                        <asp:ListItem Value="Sat" Text="Saturday"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>





                                            </div>
                                        </div>
                                        <div class="row text-left py-2">

                                            <div class="col-lg-12 col-md-3">
                                                <div class="card-header groupHead">

                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" ForeColor="black" Font-Size="small" Text="5. Upload Photo"></asp:Label>

                                                </div>


                                            </div>
                                        </div>

                                        <div class="col-lg-12 col-md-12 py-2">
                                            <div class="row">
                                                <div class="col-lg-12 mb-2">
                                                    <asp:Button ID="btnUploadImage" runat="server" CausesValidation="False" CssClass="form-control form-control-sm" OnClick="btnUploadImage_Click"
                                                        Style="display: none" TabIndex="18" Text="Upload Image" Width="80px" />
                                                    <asp:FileUpload ID="fuImage" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-sm btn-success "
                                                        onchange="UploadImage(this);" TabIndex="9" />
                                                    <asp:Image ID="imgPhoto" runat="server" Visible="false" Style="border-width: 0px; height: 50px; width: 50px; border: 2px solid #eaf4ff;" />
                                                    <br />
                                                    <span class="text-warning" style="font-size: 7pt; line-height: 12px;">Image size will be less then 100 KB
                                        (Only .JPG, .PNG, .JPEG)</span>
                                                </div>
                                            </div>

                                        </div>


                                        <div class="col-lg-12 col-md-12 text-center pb-3">



                                            <asp:LinkButton ID="lbtnSave" runat="server" OnClick="lbtnSave_Click" ToolTip="Click to save" CssClass="btn btn-success">
                            <i class="fa fa-save">&nbsp;Save</i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnUpdate" OnClick="lbtnUpdate_Click" Visible="false" runat="server" ToolTip="Click to save" CssClass="btn btn-success">
                            <i class="fa fa-save">&nbsp;Update</i>
                                            </asp:LinkButton>

                                            <asp:LinkButton ID="lbtnRest" runat="server" OnClick="lbtnRest_Click" ToolTip="Click to Reset" CssClass="btn btn-warning">
                            <i class="fa fa-undo">&nbsp;Reset</i>
                                            </asp:LinkButton>
                                        </div>






                                    </div>

                                </div>

                            </asp:Panel>
                            <asp:Panel ID="pnlupdate" runat="server" Visible="false">
                                <div class="row mx-3">
                                    <div class="col-lg-2 col-md-2">

                                        <asp:Label runat="server" CssClass="form-control-label form-control-md" Font-Bold="true">Office Level</asp:Label>

                                        <asp:DropDownList ID="ddlOfficeLvl2" runat="server" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlOfficeLvl2_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-lg-2 col-md-2">
                                        <asp:Label runat="server" CssClass="form-control-label form-control-md" Font-Bold="true">Office</asp:Label>

                                        <asp:DropDownList ID="ddlOffice2" runat="server" CssClass="form-control form-control-sm">
                                        </asp:DropDownList>


                                    </div>
                                    <div class="col-lg-2 col-md-2">
                                        <asp:Label runat="server" CssClass="form-control-label form-control-md" Font-Bold="true">Designation</asp:Label>

                                        <asp:DropDownList ID="ddlDepotWiseDesig" runat="server" CssClass="form-control form-control-sm" TabIndex="2">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-lg-2 col-md-2">

                                        <asp:Label runat="server" Font-Bold="true" CssClass="form-control-label form-control-md">License Staus</asp:Label>

                                        <asp:DropDownList ID="ddlLicenseStatus" runat="server" CssClass="form-control form-control-sm" TabIndex="2">
                                            <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                            <asp:ListItem Value="P" Text="Pending"></asp:ListItem>
                                            <asp:ListItem Value="E" Text="Expiring Soon"></asp:ListItem>
                                            <asp:ListItem Value="O" Text="Expired"></asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-lg-2 col-md-2">

                                        <asp:Label runat="server" Text="Employee Name" Font-Bold="true" CssClass="form-control-label  form-control-md"></asp:Label>

                                        <asp:TextBox AutoComplete="true" ID="tbEmloyeeName" runat="server" CssClass="form-control form-control-sm" MaxLength="15" Placeholder="Min. 3 char (Optional)"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ajxFtEmployeeName" runat="server" FilterType="UppercaseLetters,LowercaseLetters" ValidChars=""
                                            TargetControlID="tbEmloyeeName" />

                                    </div>
                                    <div class="col-lg-2 col-md-2 my-4">
                                        <asp:LinkButton ID="lbtnsearch" runat="server" CssClass="btn btn-sm  btn-success" OnClick="lbtnsearch_Click" Style="font-size: 10pt;" ToolTip="Search"> <span><i class ="fa fa-search"></i></span></asp:LinkButton>
                                        <asp:LinkButton ID="btnRefresh" runat="server" CssClass="btn btn-sm  btn-warning" OnClick="btnRefresh_Click" Style="font-size: 10pt;" ToolTip="Clear Filters"> <span><i class ="fa fa-undo"></i></span></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnEmpReports" runat="server" CssClass="btn btn-sm  btn-danger" OnClick="lbtnEmpReports_Click" Style="font-size: 10pt;" ToolTip="Download Employee List"> <i class="fa fa-download"></i></asp:LinkButton>


                                    </div>
                                </div>
                                <div class="row mx-3">
                                    <div class="col-lg-12 col-md-12">
                                        <asp:GridView ID="grvEmployees" runat="server" Width="100%" OnRowCommand="grvEmployees_RowCommand"
                                            AllowPaging="true" PageSize="12" AutoGenerateColumns="False"
                                            GridLines="None" DataKeyNames="e_id,e_code,e_fname,e_mname,e_lname,e_fathername,e_dob,e_gender,e_gender_desc,e_designation_code,e_state_code,e_mobile_number,e_email_id,e_address,e_pin_code,e_ofclvl_id,e_officeid,e_blood_group_code,e_emergency_number,e_emp_type,e_weekrestday,e_date_of_joining,e_date_of_assigned_depot,e_photo,e_designation_name,e_statename,e_ofclvlname,e_office_name,e_city,e_landline,e_licenseno,e_licensedate,e_posting_ofc,e_date_of_posting,e_empclass,e_dutytype,e_empcode"
                                            CssClass="table table-striped w-100">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEMPFNAME" runat="server" Text='<%# Eval("e_fname") %>'></asp:Label>
                                                        <asp:Label ID="lblEMPMNAME" runat="server" Text='<%# Eval("e_mname") %>'></asp:Label>
                                                        <asp:Label ID="lblEMPLNAME" runat="server" Text='<%# Eval("e_lname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEMPDESIGNATION" runat="server" Text='<%# Eval("e_designation_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Mobile No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEMPMOBILENUMBER" runat="server" Text='<%# Eval("e_mobile_number") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblofclvlname" runat="server" Text='<%# Eval("e_ofclvlname") %>'></asp:Label>,
                                                      <asp:Label ID="lblofficename" runat="server" Text='<%# Eval("e_office_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnUpdateEmp" runat="server" CommandName="UpdateEmployee" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                            class="btn btn-success btn-sm" ToolTip="Update"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnVerifyEmp" runat="server" CommandName="VerifyEmployee" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                            class="btn btn-info btn-sm" ToolTip="Verify" Style="font-size: 14px;"><i class="fa fa-check"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnDeleteEmp" runat="server" CommandName="DeleteEmployee" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                            class="btn btn-danger btn-sm" ToolTip="Delete"><i class="fa fa-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlVerifiedEmp" CssClass="px-2" runat="server" Visible="false">
                                    <div class="row py-2 pr-5">
                                        <div class="col-md-12 col-lg-12 text-right">
                                            <asp:Label Text="text" runat="server" CssClass="btn btn-sm  btn-success btn-label">P</asp:Label>
                                            <asp:Label runat="server" CssClass="form-control-label">Personal Details</asp:Label>
                                            <asp:Label Text="text" runat="server" CssClass="btn btn-sm  btn-info btn-label">C</asp:Label>
                                            <asp:Label runat="server" CssClass="form-control-label"> Contact Details</asp:Label>

                                            <asp:Label Text="text" runat="server" CssClass="btn btn-sm  btn-danger btn-label">O</asp:Label>
                                            <asp:Label runat="server" CssClass="form-control-label">Office Details</asp:Label>


                                            <asp:Label Text="text" runat="server" CssClass="btn btn-sm  btn-success btn-label">I</asp:Label>
                                            <asp:Label runat="server" CssClass="form-control-label"> Update Image</asp:Label>

                                            <asp:Label Text="text" runat="server" CssClass="btn btn-sm  btn-warning btn-label">L</asp:Label>
                                            <asp:Label runat="server" CssClass="form-control-label">License Details</asp:Label>


                                            <asp:Label Text="text" runat="server" CssClass="btn btn-sm  btn-info btn-label">R</asp:Label>
                                            <asp:Label runat="server" CssClass="form-control-label"> Duty Rest Details</asp:Label>

                                            <asp:Label Text="text" runat="server" CssClass="btn btn-sm  btn-primary btn-label">D</asp:Label>
                                            <asp:Label runat="server" CssClass="form-control-label"> Duty Type</asp:Label>

                                             <asp:Label Text="text" runat="server" CssClass="btn btn-sm  btn-success btn-label">S</asp:Label>
                                            <asp:Label runat="server" CssClass="form-control-label">Service Status</asp:Label>
                                        </div>
                                    </div>
                                    <div class="row mx-3">
                                        <div class="col-lg-12 col-md-12">
                                            <asp:GridView ID="gvVerifiedEmployees" runat="server" AllowPaging="true" PageSize="20" AutoGenerateColumns="False" GridLines="None" Font-Bold="false" DataKeyNames="e_id,e_code,e_fname,
                                    e_mname,e_lname,e_fathername,e_dob,e_gender,e_gender_desc,e_designation_code,e_state_code,e_mobile_number,e_email_id,e_address,e_pin_code,e_ofclvl_id,e_officeid,e_blood_group_code,e_emergency_number,e_emp_type,e_weekrestday,e_date_of_joining,e_date_of_assigned_depot,
                                    e_photo,e_designation_name,e_statename,e_ofclvlname,e_office_name,e_city,e_landline,e_licenseno,e_licensedate,e_posting_ofc,e_date_of_posting,e_empclass,e_dutytype,e_empcode,e_service_status,e_orderno,e_orderdate,e_orderremark"
                                                OnPageIndexChanging="gvVerifiedEmployees_PageIndexChanging"
                                                OnRowCommand="gvVerifiedEmployees_RowCommand" CssClass="table table-flush  table mar" HeaderStyle-CssClass="thead-light font-weight-bold" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Emp Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEMPCODE" CssClass="form-check-label" runat="server" Text='<%# Eval("e_code") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEMPFNAME" runat="server" Text='<%# Eval("e_fname") %>'></asp:Label>
                                                            <asp:Label ID="lblEMPMNAME" runat="server" Text='<%# Eval("e_mname") %>'></asp:Label>
                                                            <asp:Label ID="lblEMPLNAME" runat="server" Text='<%# Eval("e_lname") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Office Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblofclvlname" CssClass="form-control-label" runat="server" Text='<%# Eval("e_ofclvlname") %>'></asp:Label>,
                                                 <asp:Label ID="lbloffice_name" CssClass="form-control-label" runat="server" Text='<%# Eval("e_office_name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Designation">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldesignation_name" CssClass="form-control-label" runat="server" Text='<%# Eval("e_designation_name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
<asp:TemplateField HeaderText="Current Role">
                                                        <ItemTemplate>
                                                            <asp:Label CssClass="form-control-label" runat="server" Text='<%# Eval("currentrole") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mobile No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmobile_number" CssClass="form-control-label" runat="server" Text='<%# Eval("e_mobile_number") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnPersonal" runat="server" CommandName="UpdatePersonal" CssClass="btn btn-sm  btn-success" ToolTip="Update Personal Details">
                                                    P</asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnConatct" runat="server" CommandName="UpdateContact" CssClass="btn btn-sm  btn-info" ToolTip="Update Contact Details">
                                                    C</asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnOfficial" runat="server" CommandName="UpdateOfficial" CssClass="btn btn-sm  btn-danger" ToolTip="Update Official Details">
                                                    O</asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnUpdatephoto" runat="server" CommandName="UpdatePhoto" CssClass="btn btn-sm  btn-success" ToolTip="Update Photo">
                                                    I</asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnUpdateLicence" runat="server" CommandName="UpdateLicense" CssClass="btn btn-sm  btn-warning" ToolTip="Update License Details">
                                                    L</asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnUpdateWeeklyRest" runat="server" CommandName="UpdateWeeklyRest" CssClass="btn btn-sm btn-info" ToolTip="Update Weekly Rest">
                                                    R</asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnUpdatePosting" runat="server" CommandName="updateDutyType" CssClass="btn btn-sm  btn-primary" ToolTip="Update Duty Type">
                                                    D</asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnUpdateService" runat="server" CommandName="UpdateServiceStatus" CssClass="btn btn-sm  btn-success" ToolTip="Update Service Status">
                                                    S</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Label ID="lblNoEmpData" CssClass="p-5 form-control text-capitalize text-center" Visible="false" runat="server" Font-Size="medium" Font-Bold="true">
                                    Sorry! No employee found. Search employee again
                                </asp:Label>

                            </asp:Panel>

                        </div>
                    </div>
                </div>


                <div class="row align-items-center">
                    <div class="col-lg-12 col-md-12">

                        <div class="card card-stats mb-3 shadow">

                            <asp:Panel ID="pnlPersonal" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col">
                                        <div class="row px-4 py-4">
                                            <div class="col-md-12 text-left">
                                                <div class="col-12 groupHead">
                                                    <asp:Label ID="lblPersonalDetail" runat="server" Font-Bold="true" CssClass="form-check-label" Font-Size="small">Personal Details</asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 py-2">
                                                <div class="row px-4">
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">Employee Code</asp:Label><span class="text-warning">*</span>

                                                        <asp:TextBox AutoComplete="true" class="form-control form-control-sm" runat="server" ID="tbEmployeeCode" MaxLength="50" ToolTip="Employee Code"
                                                            Text="" Style="">
                                                        </asp:TextBox>

                                                    </div>
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label"> First Name<span class="text-warning">*</span></asp:Label>

                                                        <asp:TextBox AutoComplete="true" class="form-control form-control-sm" runat="server" ID="tbFirstNameUpdate" MaxLength="50" ToolTip="First Name"
                                                            placeholder="Max 50 char" Text="" Style=""></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="AjxFtFirstNameUpdate" runat="server" FilterType="UppercaseLetters,LowercaseLetters" ValidChars=""
                                                            TargetControlID="tbFirstNameUpdate" />

                                                    </div>
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">Middle Name</asp:Label>

                                                        <asp:TextBox AutoComplete="true" class="form-control form-control-sm" runat="server" ID="tbMiddleNameUpdate" MaxLength="50" ToolTip="Middle Name"
                                                            placeholder="Max 50 char" Text="" Style=""></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="AjxFtMiddleNameUpdate" runat="server" FilterType="UppercaseLetters,LowercaseLetters" ValidChars=""
                                                            TargetControlID="tbMiddleNameUpdate" />

                                                    </div>
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">Last Name</asp:Label>

                                                        <asp:TextBox AutoComplete="true" class="form-control form-control-sm" runat="server" ID="tbLastNameUpdate" MaxLength="50" ToolTip="Last Name"
                                                            placeholder="Max 50 char" Text="" Style=""></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="AjxFtLastNameUpdate" runat="server" FilterType="UppercaseLetters,LowercaseLetters" ValidChars=""
                                                            TargetControlID="tbLastNameUpdate" />

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-12 col-md-12 py-2">
                                                <div class="row px-4">
                                                    <div class="col-lg-3 col-md-12">
                                                        <asp:Label runat="server" CssClass="form-control-label"> Father Name</asp:Label>

                                                        <asp:TextBox AutoComplete="true" class="form-control form-control-sm" runat="server" ID="tbFatherNameUpdate" MaxLength="50" ToolTip="First Name"
                                                            placeholder="Max 50 char" Text="" Style=""></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="AjxFtFatherNameUpdate" runat="server" FilterType="UppercaseLetters,LowercaseLetters" ValidChars=""
                                                            TargetControlID="tbFatherNameUpdate" />
                                                    </div>
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">Gender<span class="text-warning">*</span></asp:Label>

                                                        <asp:DropDownList ID="ddlGenderUpdate" CssClass="form-control form-control-sm" runat="server" ToolTip="Select Gender">
                                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                            <asp:ListItem Value="M" Text="Male"></asp:ListItem>
                                                            <asp:ListItem Value="F" Text="Female"></asp:ListItem>

                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">Blood Group</asp:Label>

                                                        <asp:DropDownList CssClass="form-control form-control-sm" runat="server" ID="ddlBloodGroupUpdate" ToolTip="Last Name"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">  Date Of Birth<span class="text-warning">*</span></asp:Label>
                                                        <div class="input-group">
                                                            <asp:TextBox AutoComplete="true" ID="tbDobUpdate" runat="server" CssClass="form-control form-control-sm" Width="68%" TabIndex="5" Placeholder="DD/MM/YYYY"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbDobUpdate" ValidChars="/" />
                                                        </div>


                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12 col-lg-12 text-center">
                                                <asp:LinkButton ID="lbtnPdraft" OnClick="lbtnPdraft_Click" Visible="false" runat="server" CssClass="btn btn-success" ToolTip="Update Personal Details"><i class="fa fa-save">&nbsp; &nbsp;Save as Draft </i></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnPlock" OnClick="lbtnPlock_Click" runat="server" CssClass="btn btn-warning" ToolTip="Verify and Lock"><i class="fa fa-lock">&nbsp; &nbsp;Verify & Lock</i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-auto mr--2 mt--2">
                                        <asp:LinkButton ID="lbtnClose" OnClick="lbtnClose_Click" Style="border-radius: 25px" runat="server" CssClass="btn btn-sm  btn-danger"><i class="fa fa-times"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlContact" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col">
                                        <div class="row px-4 py-4">
                                            <div class="col-lg-12 col-md-12">
                                                <div class="card-header groupHead">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="small" ForeColor="black" Text="Contact Details"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12">
                                                <div class="row px-4">
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">Email Id</asp:Label>
                                                        <asp:TextBox AutoComplete="true" class="form-control form-control-sm" runat="server" ID="tbEmailUpdate" MaxLength="50" ToolTip="Email Id"
                                                            placeholder="Max 50 char" Text="" Style=""></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="AjxFtEmailUpdate" runat="server" FilterType="LowercaseLetters,Numbers,Custom" ValidChars=".@"
                                                            TargetControlID="tbEmailUpdate" />
                                                    </div>
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">Mobile Number<span class="text-warning">*</span></asp:Label>

                                                        <asp:TextBox AutoComplete="true" class="form-control form-control-sm" runat="server" ID="tbMobileNumberUpdate" MaxLength="10" ToolTip="Mobile Number"
                                                            placeholder="Max 10 char" Text="" Style=""></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="AjxFtMobileNumberUpdate" runat="server" FilterType="Numbers" ValidChars=""
                                                            TargetControlID="tbMobileNumberUpdate" />
                                                    </div>
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">Landline Number</asp:Label>

                                                        <asp:TextBox AutoComplete="true" class="form-control form-control-sm" runat="server" ID="tbLandlineNumberUpdate" MaxLength="11" ToolTip="Landline Number"
                                                            placeholder="Max 11 char" Text="" Style=""></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="AjxFtLandline" runat="server" FilterType="Numbers" ValidChars=""
                                                            TargetControlID="tbLandlineNumberUpdate" />

                                                    </div>
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">Emergency No<span class="text-warning">*</span></asp:Label>


                                                        <asp:TextBox AutoComplete="true" ID="tbEmergencyNoUpdate" runat="server" CssClass="form-control form-control-sm" ToolTip="Emergency No." Placeholder="Max. 10 char" MaxLength="10"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="AjxFtEmergencyNoUpdate" runat="server" FilterType="Numbers" ValidChars=""
                                                            TargetControlID="tbEmergencyNoUpdate" />
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12">
                                                <div class="row px-4">
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">Address</asp:Label>

                                                        <asp:TextBox AutoComplete="true" class="form-control form-control-sm" runat="server" ID="tbAddressUpdate" MaxLength="200" TextMode="MultiLine" Resize="None" ToolTip="Address"
                                                            placeholder="Max 200 char" Text="" Style=""></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="AjxFtAddressUpdate" runat="server" FilterType="Numbers,UppercaseLetters,LowercaseLetters,Custom" ValidChars=",/"
                                                            TargetControlID="tbAddressUpdate" />
                                                    </div>
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">Pin Code</asp:Label>

                                                        <asp:TextBox AutoComplete="true" ID="tbPinCodeUpdate" MaxLength="6" placeholder="Max 6 char" CssClass="form-control form-control-sm" runat="server" ToolTip="Pincode"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="AjxFtPincodeUpdate" runat="server" FilterType="Numbers" ValidChars=""
                                                            TargetControlID="tbPinCodeUpdate" />
                                                    </div>
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">City <span class="text-warning">*</span></asp:Label>

                                                        <asp:TextBox AutoComplete="true" CssClass="form-control form-control-sm" MaxLength="20" placeholder="Max 20 char" runat="server" ID="tbCityUpdate" ToolTip="City"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="AjxFtCityUpdate" runat="server" FilterType="LowercaseLetters,UppercaseLetters" ValidChars=""
                                                            TargetControlID="tbCityUpdate" />
                                                    </div>
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">State <span class="text-warning">*</span></asp:Label>

                                                        <asp:DropDownList CssClass="form-control form-control-sm" runat="server" ID="ddlStateUpdate" ToolTip="State"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12 col-lg-12 text-center">
                                                <asp:LinkButton ID="lbtnCdraft" OnClick="lbtnCdraft_Click" Visible="false" runat="server" CssClass="btn btn-success" ToolTip="Update Contact Details"><i class="fa fa-save">&nbsp; &nbsp; Save as Draft</i></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnClock" OnClick="lbtnClock_Click" runat="server" CssClass="btn btn-warning" ToolTip="Verify and Lock"><i class="fa fa-lock">&nbsp; &nbsp; Verify & Lock</i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-auto mr--2 mt--2">
                                        <asp:LinkButton ID="lbtnCloseContact" OnClick="lbtnClose_Click" Style="border-radius: 25px" runat="server" CssClass="btn btn-sm  btn-danger"><i class="fa fa-times"></i></asp:LinkButton>
                                    </div>
                                </div>

                            </asp:Panel>

                            <asp:Panel ID="pnlOfficeDetails" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col">
                                        <div class="row px-4 py-4">
                                            <div class="col-lg-12 col-md-12 py-2">
                                                <div class="card-header groupHead">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="small" ForeColor="black" Text="Official Details"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 py-2">
                                                <div class="row px-4">
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">Office Level <span class="text-warning">*</span></asp:Label>

                                                        <asp:DropDownList class="form-control form-control-sm" runat="server" ID="ddlOfficeLevelUpdate" AutoPostBack="true" OnSelectedIndexChanged="ddlOfficeLevelUpdate_SelectedIndexChanged" ToolTip="Select Office Level"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">Office<span class="text-warning">*</span></asp:Label>

                                                        <asp:DropDownList class="form-control form-control-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged" ID="ddlOffice" ToolTip="Select Office"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">Posting Office<span class="text-warning">*</span></asp:Label>

                                                        <asp:DropDownList class="form-control form-control-sm" runat="server" ID="ddlpostingOfficeUpdate" ToolTip="Select Office"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">Posting Date</asp:Label>
                                                        <div class="input-group">
                                                            <asp:TextBox AutoComplete="true" ID="tbPostingDateUpdate" runat="server" CssClass="form-control form-control-sm" Width="68%" TabIndex="5" Placeholder="DD/MM/YYYY"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbPostingDateUpdate" ValidChars="/" />
                                                        </div>


                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 pb-2">
                                                <div class="row  px-4">
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">Employee Type</asp:Label>

                                                        <asp:DropDownList class="form-control form-control-sm" runat="server" ID="ddlEmployeeTypeUpdate" ToolTip="select employee type">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">Designation<span class="text-warning">*</span></asp:Label>

                                                        <asp:DropDownList class="form-control form-control-sm" runat="server" ID="ddlDesignationUpdate" ToolTip="select Designation"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">Date Of Joining</asp:Label>
                                                        <div class="input-group">
                                                            <asp:TextBox AutoComplete="true" ID="tbDoJupdate" runat="server" CssClass="form-control form-control-sm" Width="68%" TabIndex="5" Placeholder="DD/MM/YYYY"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbDoJupdate" ValidChars="/" />
                                                        </div>


                                                    </div>
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">Date of Joining at Office</asp:Label>
                                                        <div class="input-group">
                                                            <asp:TextBox AutoComplete="true" ID="tbDoJupdateOf" runat="server" CssClass="form-control form-control-sm" Width="68%" TabIndex="5" Placeholder="DD/MM/YYYY"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbDoJupdateOf" ValidChars="/" />
                                                        </div>


                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12 col-lg-12 text-center">
                                                <asp:LinkButton ID="lbtnOdraft" OnClick="lbtnOdraft_Click" runat="server" Visible="false" CssClass="btn btn-success" ToolTip="Update Official Details"><i class="fa fa-save">&nbsp; &nbsp;<label class="form-control-label1"> Save as Draft</label></i></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnOlock" OnClick="lbtnOlock_Click" runat="server" CssClass="btn btn-warning" ToolTip="Verify and Lock"><i class="fa fa-lock">&nbsp; &nbsp; Verify & Lock</i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-auto mr--2 mt--2">
                                        <asp:LinkButton ID="lbtnCloseOfficial" OnClick="lbtnClose_Click" Style="border-radius: 25px" runat="server" CssClass="btn btn-sm  btn-danger"><i class="fa fa-times"></i></asp:LinkButton>
                                    </div>
                                </div>


                            </asp:Panel>

                            <asp:Panel ID="pnlUpdatePhoto" runat="server" Visible="false">

                                <div class="row">
                                    <div class="col">
                                        <div class="row py-4 px-2">
                                            <div class="col-lg-12 col-md-12 py-4 px-4">
                                                <div class="card-header groupHead">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" ForeColor="black" Font-Size="small" Text="Upload Photo"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12">
                                                <div class="row px-4">
                                                    <div class="col-lg-12 mb-2 px-4">
                                                        <asp:Button ID="btnImageUpdate" runat="server" OnClick="btnImageUpdate_Click" CausesValidation="False" CssClass="form-control form-control-sm"
                                                            Style="display: none" TabIndex="18" Text="Upload Image" Width="80px" />
                                                        <asp:FileUpload ID="fuImageUpdate" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-sm btn-success "
                                                            onchange="UploadImageUpdate(this);" TabIndex="9" />
                                                        <asp:Image ID="imgImageUpdate" runat="server" Visible="false" Style="border-width: 0px; height: 50px; width: 50px; border: 2px solid #eaf4ff;" />
                                                        <br />
                                                        <span class="text-warning" style="font-size: 7pt; line-height: 12px;">Image size will be less then 100 KB
                                            (Only .JPG, .PNG, .JPEG)</span>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="col-md-12 col-lg-12 text-center">
                                                <asp:LinkButton ID="lbtnIdraft" Visible="false" runat="server" OnClick="lbtnIdraft_Click" CssClass="btn btn-success" ToolTip="Update Photo"><i class="fa fa-save">&nbsp; &nbsp;<label class="form-control-label1"> Save as Draft</label></i></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnIlock" runat="server" OnClick="lbtnIlock_Click" CssClass="btn btn-warning" ToolTip="Verify and Lock"><i class="fa fa-lock">&nbsp; &nbsp; Verify & Lock</i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-auto mr--2 mt--2">
                                        <asp:LinkButton ID="lbtnCloseUpdatePhoto" OnClick="lbtnClose_Click" Style="border-radius: 25px" runat="server" CssClass="btn btn-sm  btn-danger"><i class="fa fa-times"></i></asp:LinkButton>
                                    </div>
                                </div>

                            </asp:Panel>

                            <asp:Panel ID="pnlLicense" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col">
                                        <div class="row px-4 py-4">
                                            <div class="col-lg-12 col-md-12 py-2">
                                                <div class="card-header groupHead">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="small" ForeColor="black" Text="License Details"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 py-2">
                                                <div class="row  px-4">
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">License No <span class="text-warning">*</span></asp:Label>
                                                        <asp:TextBox ID="tbLicenseNo" runat="server" CssClass="form-control form-control-sm" MaxLength="12" autocomplete="off" Placeholder="max 12 digit"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="ajxFtLicenseNo" runat="server" FilterType="Numbers,UppercaseLetters,LowercaseLetters" ValidChars="/"
                                                            TargetControlID="tbLicenseNo" />
                                                    </div>
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">  License Valid Upto</asp:Label>
                                                        <div class="input-group">
                                                            <asp:TextBox autocomplete="off" ID="tbDateOfLicense" runat="server" CssClass="form-control form-control-sm" Width="68%" TabIndex="5" Placeholder="DD/MM/YYYY"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbDateOfLicense" ValidChars="/" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12">
                                                <div class="row">
                                                    <div class="col-md-12 col-lg-12 text-center">
                                                        <asp:LinkButton ID="lbtnLdraft" OnClick="lbtnLdraft_Click" Visible="false" runat="server" CssClass="btn btn-success" ToolTip="Update Document"><i class="fa fa-save">&nbsp; &nbsp; Save as Draft</i></asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnLlock" OnClick="lbtnLlock_Click" runat="server" CssClass="btn btn-warning" ToolTip="Verify and Lock"><i class="fa fa-lock">&nbsp; &nbsp; Verify & Lock</i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-auto mr--2 mt--2">
                                        <asp:LinkButton ID="lbtnCloseLicense" OnClick="lbtnClose_Click" Style="border-radius: 25px" runat="server" CssClass="btn btn-sm  btn-danger"><i class="fa fa-times"></i></asp:LinkButton>
                                    </div>
                                </div>

                            </asp:Panel>

                            <asp:Panel ID="pnlWeeklyRest" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col">
                                        <div class="row px-4 py-4">
                                            <div class="col-lg-12 col-md-12">
                                                <div class="card-header groupHead">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="small" ForeColor="black" Text="Weekly Rest Day"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12">
                                                <div class="row">
                                                    <div class="col-lg-3 col-md-3 py-2 px-4">
                                                        <asp:Label runat="server" CssClass="form-control-label">Weekly Rest Day</asp:Label>
                                                        <asp:DropDownList class="form-control form-control-sm" runat="server" ID="ddlweekrestdays" ToolTip="select Weekly Rest Day">
                                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                            <asp:ListItem Value="S" Text="Sunday"></asp:ListItem>
                                                            <asp:ListItem Value="M" Text="Monday"></asp:ListItem>
                                                            <asp:ListItem Value="T" Text="Tuesday"></asp:ListItem>
                                                            <asp:ListItem Value="W" Text="Wednesday"></asp:ListItem>
                                                            <asp:ListItem Value="Th" Text="Thursday"></asp:ListItem>
                                                            <asp:ListItem Value="F" Text="Friday"></asp:ListItem>
                                                            <asp:ListItem Value="Sat" Text="Saturday"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12 col-lg-12 text-center">
                                                <asp:LinkButton ID="lbtnRdraft" OnClick="lbtnRdraft_Click" Visible="false" runat="server" CssClass="btn btn-success" ToolTip="Update Weekly Rest"><i class="fa fa-save">&nbsp; &nbsp;<label class="form-control-label1"> Save as Draft</label></i></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnRlock" OnClick="lbtnRlock_Click" runat="server" CssClass="btn btn-warning" ToolTip="Verify and Lock"><i class="fa fa-lock">&nbsp; &nbsp; Verify & Lock</i></asp:LinkButton>
                                            </div>



                                        </div>
                                    </div>
                                    <div class="col-auto mr--2 mt--2">
                                        <asp:LinkButton ID="lbtnCloseWeeklyRest" OnClick="lbtnClose_Click" Style="border-radius: 25px" runat="server" CssClass="btn btn-sm  btn-danger"><i class="fa fa-times"></i></asp:LinkButton>
                                    </div>
                                </div>

                            </asp:Panel>
                            <asp:Panel ID="pnlDutyType" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col">
                                        <div class="row px-4 py-4">
                                            <div class="col-lg-12 col-md-12">
                                                <div class="card-header groupHead">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="small" ForeColor="black" Text="Update Duty Type"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12">
                                                <div class="row">
                                                    <div class="col-lg-3 col-md-3 py-2 px-4">
                                                        <asp:Label runat="server" CssClass="form-control-label">Employee Class<span class="text-warning"></span></asp:Label>
                                                        <asp:DropDownList class="form-control form-control-sm" runat="server" ID="ddlempclassupdate" ToolTip="select Weekly Rest Day">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 py-2 px-4">
                                                        <asp:Label runat="server" CssClass="form-control-label">Duty Type<span class="text-warning"></span></asp:Label>
                                                        <asp:DropDownList class="form-control form-control-sm" runat="server" ID="ddldutytypeupdate" ToolTip="select Weekly Rest Day">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12 col-lg-12 text-center">
                                                <asp:LinkButton ID="lbtnDlock" OnClick="lbtnDlock_Click" runat="server" CssClass="btn btn-warning" ToolTip="Verify and Lock"><i class="fa fa-lock">&nbsp; &nbsp; Verify & Lock</i></asp:LinkButton>
                                            </div>



                                        </div>
                                    </div>
                                    <div class="col-auto mr--2 mt--2">
                                        <asp:LinkButton ID="LinkButton3" OnClick="lbtnClose_Click" Style="border-radius: 25px" runat="server" CssClass="btn btn-sm  btn-danger"><i class="fa fa-times"></i></asp:LinkButton>
                                    </div>
                                </div>

                            </asp:Panel>
                            <asp:Panel ID="pnlServiceStatus" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col">
                                        <div class="row px-4 py-4">
                                            <div class="col-lg-12 col-md-12">
                                                <div class="card-header groupHead">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="small" ForeColor="black" Text="Update Service Status"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12">
                                                <div class="row">
                                                    <div class="col-lg-3 col-md-3 ">
                                                        <asp:Label runat="server" CssClass="form-control-label">Service Status<span class="text-warning">*</span></asp:Label>
                                                        <asp:DropDownList class="form-control form-control-sm" runat="server" ID="ddlservicestatusUpdate" ToolTip="select Weekly Rest Day">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">  Office Order No.<span class="text-warning">*</span></asp:Label>
                                                        <div class="input-group">
                                                            <asp:TextBox autocomplete="off" ID="tbofficeorderUpdate" runat="server" CssClass="form-control form-control-sm" Width="68%" TabIndex="5" Placeholder="Max 20 chars"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">  Office Date<span class="text-warning">*</span></asp:Label>
                                                        <div class="input-group">
                                                            <asp:TextBox autocomplete="off" ID="tbOrderDateUpdate" runat="server" CssClass="form-control form-control-sm" Width="68%" TabIndex="5" Placeholder="DD/MM/YYYY"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbOrderDateUpdate" ValidChars="/" />
                                                   
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3 col-md-3">
                                                        <asp:Label runat="server" CssClass="form-control-label">  Remark</asp:Label>
                                                        <div class="input-group">
                                                            <asp:TextBox autocomplete="off" ID="tbremarkupdate" TextMode="MultiLine" runat="server" CssClass="form-control form-control-sm" Width="68%" TabIndex="5" Placeholder="Enter Remark"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12 col-lg-12 text-center">
                                                     <asp:LinkButton ID="lbtnServicestatusUpdate" OnClick="lbtnServicestatusUpdate_Click" runat="server" CssClass="btn btn-warning" ToolTip="Verify and Lock"><i class="fa fa-lock">&nbsp; &nbsp; Verify & Lock</i></asp:LinkButton>
                                            </div>



                                        </div>
                                    </div>
                                    <div class="col-auto mr--2 mt--2">
                                        <asp:LinkButton ID="LinkButton6" OnClick="lbtnClose_Click" Style="border-radius: 25px" runat="server" CssClass="btn btn-sm  btn-danger"><i class="fa fa-times"></i></asp:LinkButton>
                                    </div>
                                </div>

                            </asp:Panel>
                        </div>
                    </div>
                </div>

            </div>
        </div>

    </div>

    <%-- //PopUp--%>
    <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
        CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed; display: none;">
        <div class="card" style="min-width: 350px;">
            <div class="card-header">
                <h4 class="card-title">Please Confirm
                </h4>
            </div>
            <div class="card-body" style="min-height: 100px;">
                <asp:Label ID="lblConfirmation" runat="server"></asp:Label>
                <div style="width: 100%; margin-top: 20px; text-align: right;">
                    <asp:LinkButton ID="lbtnYesConfirmation" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbtnYesConfirmation_Click"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                    <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-3"> <i class="fa fa-times"></i> No </asp:LinkButton>
                </div>
            </div>
        </div>
        <div style="visibility: hidden;">
            <asp:Button ID="Button4" runat="server" Text="" />
        </div>
    </asp:Panel>

</asp:Content>

