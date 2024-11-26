<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="AgentRegistration.aspx.cs" Inherits="AgentRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .form-control, .textbox {
            height: 35px !IMPORTANT;
        }

        .rbl input[type="radio"] {
            margin-left: 10px;
            margin-right: 10px;
        }

        label {
            margin-left: 6px;
        }

        .midd-bg {
            background-color: white !important;
        }

        hr {
            margin-top: 12px;
            margin-bottom: 0px;
        }
    </style>
    <script type="text/javascript">
        function UploadPDF(fileUpload) {
            //alert(1);
            if ($('#fudocfile').value != '') {
                document.getElementById("<%=btnUploadpdf.ClientID %>").click();
            }
        }
        function UploadPDFRegCopy(fileUpload) {
            //alert(1);
            if ($('#fudocfileRegCopy').value != '') {
                document.getElementById("<%=btnUploadpdfRegCopy.ClientID %>").click();
            }
        }
        function UploadPDFAddressproof(fileUpload) {
            //alert(1);
            if ($('#fudocfileAddressproof').value != '') {
                document.getElementById("<%=btnUploadpdfAddressproof.ClientID %>").click();
            }
        }
        function UploadPDFIdproof(fileUpload) {
            //alert(1);
            if ($('#fudocfileIdproof').value != '') {
                document.getElementById("<%=btnUploadpdfIDproof.ClientID %>").click();
            }
        }

        function Point1() {
            $("#mm").modal('show');
            document.getElementById('lblMsg').innerHTML = 'Enter the name of Agency/Agent.';
        }
        function Point4() {
            $("#mm").modal('show');
            document.getElementById('lblMsg').innerHTML = 'Select the Legal status of Agency/Agent.';
        }
        function Point5() {
            $("#mm").modal('show');
            document.getElementById('lblMsg').innerHTML = 'Give details about your Bus Ticket Booking Experience.';
        }
        function Point6() {
            $("#mm").modal('show');
            document.getElementById('lblMsg').innerHTML = 'Upload Address and Identity Proof Document here.';
        }
        function Point7() {
            $("#mm").modal('show');
            document.getElementById('lblMsg').innerHTML = 'Select the type of booking facility you want to avail.';
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container" style="background-color: white">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <asp:HiddenField ID="hidHashC" runat="server" />
        <asp:HiddenField ID="hidHashpwd" runat="server" />
        <asp:HiddenField ID="hidHashcpwd" runat="server" />


        <div class="row">
            <div class="col-md">
                <h3 class="mb-0">Apply here for becoming our authorized agent</h3>
            </div>
        </div>

        <div class="row">
            <div class="col-md">
                <p class="mb-0 font-weight-bold">Please Note</p>
                <p class="mb-0">1. All fields marked <span class="text-danger">*</span> are Mendatory.</p>
                <p class="mb-0">2. If you are applying as an agency (ref point 4), please upload relevent document.</p>
                <p class="mb-0">3. After completion of registration process, you will be entitiled for <b>ONLINE TICKET BOOKING.</b></p>
                <p class="mb-0">4. Approval is subjected to availability and policies of Uttarakhand Transport corporation.</p>
                <p class="mb-0" id="point3" runat="server">5. If you are having setup/office at our bus stand, you can also apply for <b>CURRENT TICKET BOOKING</b> (Ref Point 7).</p>
                <hr />
            </div>
        </div>
        <div class="row">

            <div class="col-md-12 text text-dark px-2 py-3">

                <div class="col-md-12">
                    <div class="row">
                        <div class="col-lg-12">
                            <p class="mb-0 font-weight-bold">
                                1. Agency/Agent Details
                                 <a id="lbtnInfoPoint1" onclick="Point1()" class="pl-2" style="font-size: 20px; line-height: 20px; cursor: pointer;">
                                     <i class="fa fa-info-circle pt-1"></i>
                                 </a>
                            </p>
                        </div>
                        <div class="col-lg-4">
                            <asp:Label ID="lblNameHeader" runat="server" Text="Name" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                            <span style="color: Red;">*</span>
                            <div class="form-group mb-2">
                                <asp:TextBox ID="txtname" runat="server" CssClass="form-control" placeholder="Max 50 Characters" TabIndex="1" autocomplete="off" MaxLength="50"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Custom"
                                    TargetControlID="txtname" ValidChars=". " />
                            </div>
                        </div>

                        <div class="col-lg-4">
                            <asp:Label ID="lblPanNoHeader" runat="server" Text="PAN No" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                            <span style="color: Red;">*</span>
                            <div class="form-group mb-2">
                                <asp:TextBox ID="txtPanNo" runat="server" CssClass="form-control" placeholder="Max 10 Characters" TabIndex="10" autocomplete="off" MaxLength="10"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Numbers"
                                    TargetControlID="txtPanNo" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-12 mt-3">
                            <p class="mb-0 font-weight-bold">2. Contact Person</p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <asp:Label ID="lblContactPersonNameHeader" runat="server" Text="Name" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                            <span style="color: Red;">*</span>
                            <div class="form-group mb-2">
                                <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control" placeholder="Max 50 Characters"
                                    TabIndex="2" autocomplete="off" MaxLength="50"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Custom"
                                    TargetControlID="txtContactName" ValidChars=". " />
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <asp:Label ID="lblMobileHeader" runat="server" Text="Mobile No" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                            <span style="color: Red;">*</span>
                            <div class="form-group mb-2">
                                <asp:TextBox ID="txtmobileno" runat="server" CssClass="form-control" placeholder="Max 10 Characters"
                                    TabIndex="3" autocomplete="off" MaxLength="10"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                    TargetControlID="txtmobileno" />
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <asp:Label ID="lblEmailHeader" runat="server" Text="Email Id" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                            <span style="color: Red;">*</span>
                            <div class="form-group mb-2">
                                <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" placeholder="Max 100 Characters"
                                    TabIndex="4" autocomplete="off" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-12 mt-3">
                            <p class="mb-0 font-weight-bold">3. Address</p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <asp:Label ID="lblStateHeader" runat="server" Text="State" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                            <span style="color: Red;">*</span>
                            <div class="form-group mb-2" style="border: 1px solid #eeeaea; border-radius: 4px;">
                                <asp:DropDownList ID="ddlstate" runat="server" TabIndex="5" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged" Style="border: none;">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <asp:Label ID="lblDistrictHeader" runat="server" Text="District" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                            <span style="color: Red;">*</span>
                            <div class="form-group mb-2" style="border: 1px solid #eeeaea; border-radius: 4px;">
                                <asp:DropDownList ID="ddlDistrict" runat="server" TabIndex="6" CssClass="form-control" Style="border: none;" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <asp:Label ID="lblCityHeader" runat="server" Text="City" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                            <span style="color: Red;">*</span>
                            <div class="form-group mb-2" style="border: 1px solid #eeeaea; border-radius: 4px;">
                                <asp:DropDownList ID="ddlcity" runat="server" TabIndex="7" CssClass="form-control" Style="border: none;">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-8">
                            <asp:Label ID="lblAddressHeader" runat="server" Text="Address" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                            <span style="color: Red;">*</span>
                            <div class="form-group mb-2">
                                <asp:TextBox ID="txtaddress" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Max 100 Characters"
                                    TabIndex="8" autocomplete="off" MaxLength="100" Style="resize: none;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <asp:Label ID="lblPinCodeHeader" runat="server" Text="Pin Code" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                            <span style="color: Red;">*</span>
                            <div class="form-group mb-2">
                                <asp:TextBox ID="txtPinCode" runat="server" CssClass="form-control" placeholder="Max 6 Characters"
                                    TabIndex="9" autocomplete="off" MaxLength="6" Style="resize: none;"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Numbers"
                                    TargetControlID="txtPinCode" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 mt-3">
                            <p class="mb-0 font-weight-bold">
                                4. Legal status of Agency/Agent
                        <a id="lbtnInfoPoint4" onclick="Point4()" class="pl-2" style="font-size: 20px; line-height: 20px; cursor: pointer;">
                            <i class="fa fa-info-circle pt-1"></i>
                        </a>
                            </p>
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-lg-3">
                            <asp:Label ID="Label4" runat="server" Text="Status" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                            <span style="color: Red;">*</span>
                            <div class="form-group mb-2" style="border: 1px solid #eeeaea; border-radius: 4px;">
                                <asp:DropDownList ID="ddlstatus" runat="server" TabIndex="11" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged" Style="border: none;">
                                    <asp:ListItem Value="I" Text="Individual" Selected="True"> </asp:ListItem>
                                    <asp:ListItem Value="P" Text="Partnership"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-4" id="dvcopy" runat="server" visible="false">
                            <asp:Label ID="Label5" runat="server" Text="Attach certified copy" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                            <asp:Button ID="btnUploadpdf" runat="server" OnClick="btnUploadpdf_Click" CausesValidation="False" CssClass="button1"
                                Style="display: none" TabIndex="12" Text="Upload PDF" Width="80px" />
                            <asp:FileUpload ID="fudocfile" runat="server" Style="color: #ffffff; border: none;" onchange="UploadPDF(this);" accept="application/pdf" />
                            <br />
                            <asp:Label runat="server" ID="lblPDF" CssClass="col-form-label control-label" Style="font-size: 12px; font-weight: normal; color: green;"></asp:Label>
                            <span style="font-size: 9pt; color: Red; line-height: 12px;">Only PDF Allowed. (File size max 2MB)</span>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-12 mt-3">
                            <p class="mb-0 font-weight-bold">
                                5. Bus Ticket Booking Experience
                        <a id="lbtnInfoPoint5" onclick="Point5()" class="pl-2" style="font-size: 20px; line-height: 20px; cursor: pointer;">
                            <i class="fa fa-info-circle pt-1"></i>
                        </a>

                            </p>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-lg-12">
                            <asp:Label ID="Label6" runat="server" Text="Past booking experience ?" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                            <span style="color: Red;">*</span>
                            <div class="form-group mb-2">
                                <asp:RadioButtonList ID="rbtexperience" Visible="true" OnSelectedIndexChanged="rbtexperience_SelectedIndexChanged" GroupName="radio" TabIndex="13" CssClass="rb1" RepeatDirection="Horizontal" runat="server" AutoPostBack="true">
                                    <asp:ListItem Text="Yes" class="pr-2 pl-2 form-control-label" Value="Y"></asp:ListItem>
                                    <asp:ListItem Text="No" class="pr-2 pl-2 form-control-label" Value="N" Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>


                    </div>
                    <div class="row" id="dvexperience" runat="server" visible="false">
                        <div class="col-lg-2">
                            <asp:Label ID="Label9" runat="server" Text="Number Of year" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                            <span style="color: Red;">*</span>
                            <div class="form-group mb-2">
                                <asp:TextBox ID="txtnoofyear" runat="server" CssClass="form-control" Text="0" placeholder="Only Number"
                                    TabIndex="14" autocomplete="off" MaxLength="2"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Numbers"
                                    TargetControlID="txtnoofyear" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <asp:Label ID="Label12" runat="server" Text="Copy of registration for proof" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                            <span style="color: Red;">*</span>
                            <br />
                            <asp:Button ID="btnUploadpdfRegCopy" runat="server" OnClick="btnUploadpdfRegCopy_Click" CausesValidation="False" CssClass="button1"
                                Style="display: none" TabIndex="15" Text="Upload PDF" Width="80px" />
                            <asp:FileUpload ID="fudocfileRegCopy" runat="server" Style="color: #ffffff; border: none;" onchange="UploadPDFRegCopy(this);" accept="application/pdf" />
                            <br />
                            <asp:Label runat="server" ID="lblPDFRegCopy" CssClass="col-form-label control-label" Style="font-size: 12px; font-weight: normal; color: green;"></asp:Label>
                            <span style="font-size: 9pt; color: Red; line-height: 12px;">Only PDF Allowed. (File size max 2MB)</span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 mt-3">
                            <p class="mb-0 font-weight-bold">
                                6. Upload Documents
                        <a id="lbtnInfoPoint6" onclick="Point6()" class="pl-2" style="font-size: 20px; line-height: 20px; cursor: pointer;">
                            <i class="fa fa-info-circle pt-1"></i>
                        </a>
                            </p>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-lg-6 border-right">
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label ID="Label1" runat="server" Text="Address Proof Type" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                                    <span style="color: Red;">*</span>
                                    <div class="form-group mb-2" style="border: 1px solid #eeeaea; border-radius: 4px;">
                                        <asp:DropDownList ID="ddlAddressProofType" runat="server" TabIndex="16" AutoPostBack="true" CssClass="form-control" Style="border: none;">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <asp:Label ID="Label3" runat="server" Text="Copy of Address for proof" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                                    <span style="color: Red;">*</span>
                                    <asp:Button ID="btnUploadpdfAddressproof" runat="server" OnClick="btnUploadpdfAddressproof_Click" CausesValidation="False" CssClass="button1"
                                        Style="display: none" TabIndex="17" Text="Upload Copy of Address for proof" Width="80px" />
                                    <asp:FileUpload ID="fudocfileAddressproof" runat="server" Style="color: #ffffff; border: none;" onchange="UploadPDFAddressproof(this);" accept="application/pdf" />
                                    <asp:Label runat="server" ID="lblPDFAddressproof" CssClass="col-form-label control-label" Style="font-size: 12px; font-weight: normal; color: green;"></asp:Label>
                                    <span style="font-size: 9pt; color: Red; line-height: 12px;">Only PDF Allowed. (File size max 2MB)</span>

                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label ID="Label10" runat="server" Text="ID Proof Type" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                                    <span style="color: Red;">*</span>
                                    <div class="form-group mb-2" style="border: 1px solid #eeeaea; border-radius: 4px;">
                                        <asp:DropDownList ID="ddlIdProofType" runat="server" TabIndex="18" AutoPostBack="true" CssClass="form-control" Style="border: none;">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <asp:Label ID="Label11" runat="server" Text="Copy of ID for proof" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                                    <span style="color: Red;">*</span>
                                    <asp:Button ID="btnUploadpdfIDproof" runat="server" OnClick="btnUploadpdfIDproof_Click" CausesValidation="False" CssClass="button1"
                                        Style="display: none" TabIndex="19" Text="Upload PDF" Width="80px" />
                                    <asp:FileUpload ID="fudocfileIdproof" runat="server" Style="color: #ffffff; border: none;" onchange="UploadPDFIdproof(this);" accept="application/pdf" />
                                    <asp:Label runat="server" ID="lblPDFIdproof" CssClass="col-form-label control-label" Style="font-size: 12px; font-weight: normal; color: green;"></asp:Label>
                                    <span style="font-size: 9pt; color: Red; line-height: 12px;">Only PDF Allowed. (File size max 2MB)</span>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 mt-3">
                            <p class="mb-0 font-weight-bold" id="point7" runat="server">
                                7. You want to avail Current Booking ?
                        <a id="lbtnInfoPoint7" onclick="Point7()" class="pl-2" style="font-size: 20px; line-height: 20px; cursor: pointer;">
                            <i class="fa fa-info-circle pt-1"></i>
                        </a>
                            </p>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-lg-12 border-right">
                            <div class="form-group mb-2">
                                <asp:RadioButton runat="server" ID="rbtyes" Text="Yes" OnCheckedChanged="rbtyes_CheckedChanged" GroupName="rbtfacilities" AutoPostBack="true" />
                                <asp:RadioButton runat="server" ID="rbtno" Text="No" OnCheckedChanged="rbtno_CheckedChanged" GroupName="rbtfacilities" AutoPostBack="true" />
                            </div>
                        </div>
                        <div class="col-lg-4" id="dvstation" runat="server" visible="false">
                            <asp:Label ID="Label2" runat="server" Text="SELECT STATION (BUS STAND) " CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                            <span style="color: Red;">*</span>
                            <div class="form-group mb-2" style="border: 1px solid #eeeaea; border-radius: 4px;">
                                <asp:DropDownList ID="ddlstation" runat="server" TabIndex="22" CssClass="form-control" AutoPostBack="true" Style="border: none;">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>


                    <hr />
                    <div class="row mt-2">
                        <div class="col-lg-2"></div>
                        <div class="col-lg-4 text-cente">
                            <asp:Label ID="lblSecurityImageHeader" runat="server" Text="Security Image" CssClass="mb-0" Style="font-size: 12px;"></asp:Label>
                            <div class="form-group mb-2">
                                <img alt="Visual verification" title="Please enter the security code as shown in the image."
                                    src="CaptchaImage.aspx" style="width: 80%; border: 1px solid #e6e6e6; height: 35px; border-radius: 5px;" />
                                <asp:LinkButton runat="server" ID="lbtnRefresh" OnClick="lbtnRefresh_Click" TabIndex="23" CssClass="btn btn-primary" Style="width: 17%;">
                                                            <i class="fa fa-sync-alt"></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-lg-4 text-cente">
                            <asp:Label ID="lblSecurityTextHeader" runat="server" Text="Security Text" CssClass="mb-0" Style="font-size: 12px;"></asp:Label>
                            <div class="form-group mb-2">
                                <asp:TextBox ID="tbcaptchacode" runat="server" CssClass="form-control" placeholder="Enter Text" TabIndex="24" Style="text-transform: uppercase;" autocomplete="off" MaxLength="6"></asp:TextBox>
                            </div>

                        </div>
                        <div class="col-lg-2"></div>
                    </div>

                    <div class="row mb-2 mt-2">
                        <div class="col-lg-4 offset-4 text-center">

                            <span style="color: #0d558d;">&nbsp;&nbsp;
                                                                <asp:CheckBox ID="chkTOC" runat="server" Text="&nbsp; I Accept" /></span>
                            <a id="tocLink" class="btn btn-link p-1 text-primary" style="font-size: 16px;" data-toggle="modal" tabindex="25" data-target="#TCModal">Terms & Conditions</a>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12 py-1 mb-5  text-center">
                            <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" OnClientClick="return ShowLoading();" CssClass="btn btn-primary" Text="Submit" TabIndex="26" Style="cursor: pointer;" />
                            <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" OnClientClick="return ShowLoading();" CssClass="btn btn-warning" Text="Reset" TabIndex="27" Style="cursor: pointer;" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="TCModal" tabindex="-1" role="dialog" aria-labelledby="TCModalLabel"
            aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="col-md-10">
                            <h3 class="m-0 text-left">Terms and Conditions</h3>
                        </div>
                        <div class="col-md-2 text-right">
                            <a usesubmitbehavior="false" data-dismiss="modal" style="color: Red; cursor: pointer;">
                                <i class="fa fa-times fa-2x"></i></a>
                        </div>
                    </div>
                    <div class="modal-body">
                        <asp:Label ID="lblTC" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <cc1:ModalPopupExtender ID="mpError" runat="server" PopupControlID="pnlError" CancelControlID="LinkButton1"
                TargetControlID="Button3" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlError" runat="server" Style="position: fixed;">
                <div class="card" style="width: 400px; border-radius: 4px;">
                    <div class="card-header">
                        <h5 class="mt-1 mb-2">Information
                        </h5>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">

                        <asp:Label ID="lblerrmsg" runat="server" ForeColor="Black" Style="font-size: 17px; line-height: 23px;"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-success" Style="height: 30px; width: 90px; padding-top: 4px; font-size: 10pt; border-radius: 4px;"> <b>OK</b></asp:LinkButton>
                        </div>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button3" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpconfirm" runat="server" PopupControlID="pnlconfirm"
                CancelControlID="LinkButton3" TargetControlID="Button1" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlconfirm" runat="server" Style="position: fixed;">
                <div class="card" style="width: 400px; background: white; padding: 17px; border-radius: 4px;">

                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <h3 class="mt-1 mb-2" style="font-size: 21px;">Congratulations
                        </h3>
                        <asp:Label ID="lblsucessmsg" runat="server" ForeColor="Black" Style="font-size: 17px; line-height: 23px;"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-success" Style="height: 30px; width: 90px; padding-top: 4px; font-size: 10pt; border-radius: 4px;"> <b>OK</b></asp:LinkButton>
                        </div>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button1" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
                CancelControlID="lbtnNoConfirmation" TargetControlID="Button2" BackgroundCssClass="modalBackground" BehaviorID="bvConfirm">
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
                            <asp:LinkButton ID="lbtnYesConfirmation" runat="server" OnClick="lbtnYesConfirmation_Click" OnClientClick="$find('bvConfirm').hide();ShowLoading();" CssClass="btn btn-sm btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                            <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-sm btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button2" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>


        <div class="modal fade" id="mm" tabindex="-1" role="dialog" aria-labelledby="TCModalLabel"
            aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h3 class="m-0 text-left">Information</h3>
                    </div>
                    <div class="modal-body">
                        <label id="lblMsg">bhjgk</label>
                    </div>

                    <div class="modal-footer">
                        <button usesubmitbehavior="false" data-dismiss="modal" class="btn btn-success">Ok </button>
                    </div>


                </div>
            </div>
        </div>
    </div>


</asp:Content>





