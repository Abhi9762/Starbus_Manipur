<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="CSPRegistration.aspx.cs" Inherits="Auth_CSPRegistration" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    </script>
    <link rel="stylesheet" href="../style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:HiddenField ID="hidtoken" runat="server" />
    <div class="container-fluid" style="padding-top: 25px; padding-bottom: 20px;">
        <div class="row">
            <div class="col-lg-6 order-1 order-lg-1">
                <div class="card">
                    <div class="card-header p-2">
                        <div class="row">
                            <div class="col" style="font-size: 13pt; font-weight: 600;">
                                Common Service Provider
                            </div>
                            <div class="col-auto" style="font-size: 10pt; font-weight: 600; color: #f00;">
                                * All feilds are mandatory
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-8 col-sm-6">
                                <div class="form-group text-left">
                                    <label style="font-size: 12px; text-transform: uppercase;">Agency/Agent Name</label><span style="color: red;">*</span>
                                    <asp:TextBox ID="txtAgentName" runat="server" AutoComplete="Off" CssClass="form-control"
                                        Placeholder="Max 20 char" MaxLength="20"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="UppercaseLetters,Numbers,LowercaseLetters,Custom"
                                        TargetControlID="txtAgentName" ValidChars=" " />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4">
                                <asp:Label ID="lblContactPersonNameHeader" runat="server" Text="Contact Person Name" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                                <span style="color: Red;">*</span>
                                <div class="form-group mb-2">
                                    <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control" placeholder="Max 50 Characters"
                                        TabIndex="2" autocomplete="off" MaxLength="50"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Custom"
                                        TargetControlID="txtContactName" ValidChars=". " />
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <asp:Label ID="lblMobileHeader" runat="server" Text="Mobile No" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                                <span style="color: Red;">*</span>
                                <div class="form-group mb-2">
                                    <asp:TextBox ID="txtmobileno" runat="server" CssClass="form-control" placeholder="Max 10 Characters"
                                        TabIndex="3" autocomplete="off" MaxLength="10"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" FilterType="Numbers"
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
                            <div class="col-lg-4">
                                <asp:Label ID="lblStateHeader" runat="server" Text="State" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                                <span style="color: Red;">*</span>
                                <div class="form-group mb-2" style="border: 1px solid #eeeaea; border-radius: 4px;">
                                    <asp:DropDownList ID="ddlstate" runat="server" TabIndex="5" CssClass="form-control" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged" OnClientClick="return ShowLoading();" AutoPostBack="true" Style="border: none;">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <asp:Label ID="lblDistrictHeader" runat="server" Text="District" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                                <span style="color: Red;">*</span>
                                <div class="form-group mb-2" style="border: 1px solid #eeeaea; border-radius: 4px;">
                                    <asp:DropDownList ID="ddlDistrict" runat="server" TabIndex="6" CssClass="form-control" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" OnClientClick="return ShowLoading();" Style="border: none;" AutoPostBack="true">
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
                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Max 100 Characters"
                                        TabIndex="8" autocomplete="off" MaxLength="100" Style="resize: none;"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <asp:Label ID="lblPinCodeHeader" runat="server" Text="Pin Code" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                                <span style="color: Red;">*</span>
                                <div class="form-group mb-2">
                                    <asp:TextBox ID="txtPinCode" runat="server" CssClass="form-control" placeholder="Max 6 Characters"
                                        TabIndex="9" autocomplete="off" MaxLength="6" Style="resize: none;"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" FilterType="Numbers"
                                        TargetControlID="txtPinCode" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-6">
                                <asp:Label ID="lblPanNoHeader" runat="server" Text="PAN No" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                                <span style="color: Red;">*</span>
                                <div class="form-group mb-2">
                                    <asp:TextBox ID="txtPanNo" runat="server" CssClass="form-control" placeholder="Max 10 Characters" TabIndex="10" autocomplete="off" MaxLength="10"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Numbers"
                                        TargetControlID="txtPanNo" />
                                </div>
                            </div>
                        </div>


                        <h3 class="mb-3 mt-4 border-bottom">
                            <asp:CheckBox ID="chksecurity" runat="server" AutoPostBack="true" OnCheckedChanged="chksecurity_CheckedChanged" OnClientClick="return ShowLoading();" />
                            Security Is Applicable ?</h3>
                        <asp:Panel ID="pnlsecurity" runat="server" Visible="false">
                            <div class="row mb-3">
                                <div class="col-md-6 col-sm-6">
                                    <label style="font-size: 12px; text-transform: uppercase;">Service Tax Registration No.</label><span style="color: red; font-size: 9pt;">(Optional)</span>

                                    <asp:TextBox ID="txtServiceTax" runat="server" AutoComplete="Off" CssClass="form-control"
                                        Placeholder="max 20 char" MaxLength="20"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="UppercaseLetters,Numbers,LowercaseLetters,Custom"
                                        TargetControlID="txtServiceTax" ValidChars=" " />
                                </div>
                                <div class="col-md-6 col-sm-6">
                                    <label style="font-size: 12px; text-transform: uppercase;">Service Security Amount</label><span style="color: red;">*</span>
                                    <asp:TextBox ID="txtSecurityAmount" runat="server" AutoComplete="Off" CssClass="form-control"
                                        Placeholder="Only Number" MaxLength="6"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExten" runat="server" FilterType="Numbers, Custom"
                                        TargetControlID="txtSecurityAmount" ValidChars="." />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4 col-sm-6">
                                    <label style="font-size: 12px; text-transform: uppercase;">Deposit Type</label><span style="color: red;">*</span>
                                    <asp:DropDownList ID="ddlDepositType" runat="server" OnSelectedIndexChanged="ddlDepositType_SelectedIndexChanged" CssClass="form-control" AutoPostBack="True">
                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                        <asp:ListItem>DDR</asp:ListItem>
                                        <asp:ListItem>CASH</asp:ListItem>
                                        <asp:ListItem>CHEQUE</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4 col-sm-6">
                                    <label style="font-size: 12px; text-transform: uppercase;">Bank</label><span style="color: red;">*</span>
                                    <asp:TextBox ID="txtBank" runat="server" AutoComplete="Off" CssClass="form-control"
                                        Placeholder="max 20 char" MaxLength="20"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="UppercaseLetters,LowercaseLetters"
                                        TargetControlID="txtBank" />
                                </div>
                                <div class="col-md-4 col-sm-6">
                                    <label style="font-size: 12px; text-transform: uppercase;">Ref No.</label><span style="color: red;">*</span>
                                    <asp:TextBox ID="txtRefNo" runat="server" AutoComplete="Off" CssClass="form-control"
                                        Placeholder="max 20 char" MaxLength="20" Enabled="true"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="UppercaseLetters,Numbers,LowercaseLetters"
                                        TargetControlID="txtRefNo" />
                                </div>
                            </div>
                        </asp:Panel>



                        <h3 class="mb-3 mt-4 border-bottom">Document</h3>
                        <div class="row mb-3">
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
                                        <span style="font-size: 7pt; color: Red; line-height: 12px;">Only PDF Allowed. (File size max 2MB)</span>

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
                                        <span style="font-size: 7pt; color: Red; line-height: 12px;">Only PDF Allowed. (File size max 2MB)</span>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <h3 class="mb-3 mt-4 border-bottom">Facility</h3>
                        <div class="row mb-3">
                            <div class="col-lg-6 border-right">
                                <div class="form-group mb-2">
                                    <asp:CheckBox ID="chkonl" runat="server" TabIndex="20" Text="&nbsp; Online Booking" Checked="true" Enabled="false" /><br />
                                    <asp:CheckBox ID="chkbuspass" runat="server" TabIndex="20" Enabled="false" Text="&nbsp; Bus Pass" /><br />
                                    <asp:CheckBox ID="chkcurnt" runat="server" AutoPostBack="true" Visible="false" TabIndex="21" Text="&nbsp; Make current booking?" OnCheckedChanged="chkcurnt_CheckedChanged" OnClientClick="return ShowLoading();" />

                                </div>
                            </div>
                            <div class="col-lg-6" id="dvstation" runat="server" visible="false">
                                <asp:Label ID="Label2" runat="server" Text="Select Station" CssClass="mb-0" Style="font-size: 12px; text-transform: uppercase;"></asp:Label>
                                <span style="color: Red;">*</span>
                                <div class="form-group mb-2" style="border: 1px solid #eeeaea; border-radius: 4px;">
                                    <asp:DropDownList ID="ddlstation" runat="server" TabIndex="22" CssClass="form-control" AutoPostBack="true" Style="border: none;">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <hr />
                        <div class="row">
                            <div class="col-md-12 text-capitalize text-right">
                                <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" OnClientClick="return ShowLoading()">
                                   Save</asp:LinkButton>
                                <asp:LinkButton ID="lbtnsaveverify" runat="server" OnClick="lbtnsaveverify_Click" CssClass="btn btn-warning" OnClientClick="return ShowLoading()">
                                   Save & Verify</asp:LinkButton>
                                <asp:LinkButton ID="btnUpdate" runat="server" Visible="false" CssClass="btn btn-success" OnClientClick="return ShowLoading()">
                                   Update</asp:LinkButton>
                                <asp:LinkButton ID="btnReset" runat="server" CausesValidation="False" OnClick="btnReset_Click" OnClientClick="return ShowLoading()" CssClass="btn btn-danger">
                                    Reset</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 order-2 order-lg-1">
                <div class="card">
                    <div class="card-header p-2">
                        <div class="row">
                            <div class="col" style="font-size: 13pt; font-weight: 600;">
                                List of common service provider
                                <p>(Include Verified and Not Verified)</p>
                            </div>
                            <div class="col-auto">
                                <p class="mb-0 text-sm">Search for Name / Code/ Email / Mobile </p>
                                <div class="form-group">
                                    <div class="input-group mb-0">
                                        <asp:TextBox ID="txtSearchAgentMIS" runat="server" AutoComplete="Off" CssClass="form-control" type="search"
                                            Placeholder="Type here. . . (Min 3 char)" MaxLength="20"></asp:TextBox>
                                        <span class="input-group-text p-0">
                                            <asp:LinkButton ID="linkbtnSearchAgentMIS" runat="server" OnClick="linkbtnSearchAgentMIS_Click" class="btn btn-success py-1 px-3" OnClientClick="return ShowLoading()" Style="height: 100% !important; width: 100% !important;">
                                                       <i class="fa fa-search"></i></asp:LinkButton>
                                        </span>
                                        <span class="input-group-text p-0">
                                            <asp:LinkButton ID="linkbtnAllAgentMIS" runat="server" OnClick="linkbtnAllAgentMIS_Click" class="btn btn-warning py-1 px-3" OnClientClick="return ShowLoading()" Style="height: 100% !important; width: 100% !important;">
                                                    <i class="fa fa-undo"></i></asp:LinkButton></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-body p-1">

                        <div class="row">
                            <div class="col-12 col-lg-8 col-md-8 col-sm-12 text-right">
                            </div>
                            <div class="col-12 col-lg-4  col-md-4 col-sm-12 text-right">
                            </div>
                        </div>

                        <asp:GridView ID="grvAgents" runat="server" AutoGenerateColumns="False" GridLines="None" AllowSorting="true" AllowPaging="true"
                            PageSize="10" class="table" ShowHeader="false" DataKeyNames="address_proof_type,id_proof_type,mobile_no,val_email,address_proof_file,id_proof_file,val_amount,statuson_datetime,payment_orderno,cancel_refno,agent_code" OnPageIndexChanging="grvAgents_PageIndexChanging" OnRowCommand="grvAgents_RowCommand" OnRowDataBound="grvAgents_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div class="row pb-1">
                                            <div class="col">
                                                <asp:Label ID="lblAGENTNAME" runat="server" Text='<%# Eval("agent_name") %>' Style="text-transform: capitalize;"></asp:Label>
                                                <br />
                                                <span>PAN No</span>
                                                <asp:Label ID="lblPANNO" runat="server" Text='<%# Eval("pan_no") %>' Style="text-transform: capitalize;" Font-Bold="true"></asp:Label>
                                            </div>
                                            <div class="col">
                                                <i class="fa fa-user"></i>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("contact_person") %>' Style="text-transform: capitalize;"></asp:Label>
                                                <asp:LinkButton ID="lbtnViewIDProof" runat="server" CommandName="ViewIDProof" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    class="btn-link" ToolTip="View ID Proof"> View ID Proof </asp:LinkButton>
                                                <br />
                                                <i class="fa fa-mobile"></i>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("mobile_no") %>' Style="text-transform: capitalize;"></asp:Label>
                                                <i class="fa fa-envelope ml-2"></i>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("val_email") %>' Style="text-transform: capitalize;"></asp:Label>

                                            </div>
                                            <div class="col">
                                                <span>Facility</span><br />
                                                <asp:Label ID="lblfacility" runat="server" Text='<%# Eval("val_facility") %>' Style="text-transform: capitalize;" Font-Bold="true"></asp:Label>
                                            </div>
                                            <div class="col">
                                                <span>Agent Code</span>
                                                <asp:Label ID="lblAGENTCODE" runat="server" Text='<%# Eval("agent_code") %>' Style="text-transform: capitalize;" Font-Bold="true"></asp:Label>
                                                <br />
                                                <span>Account Validity</span>
                                                <asp:Label ID="lblVALIDTO" runat="server" Text='<%# Eval("valid_to") %>' Style="text-transform: capitalize;" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row pb-0">
                                            <div class="col">
                                                <i class="fa fa-home"></i><%# Eval("city_name") %> <%# Eval("district_name") %>, <%# Eval("state_name") %> , <%# Eval("val_pincode") %>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="ViewAddressProof" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    class="btn-link" ToolTip="View Address Proof"> View Proof </asp:LinkButton>
                                            </div>
                                            <div class="col-auto">
                                                <span>Current Status</span>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("current_status") %>' Style="text-transform: capitalize;" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row pb-1">
                                            <div class="col text-right">
                                                <asp:LinkButton ID="lbtnverify" Visible="true" runat="server" CommandName="Verify" OnClientClick="return ShowLoading();"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' class="btn btn-success btn-sm"
                                                    ToolTip="Reguest Verification">  <i class="fa fa-check" title="Reguest Verification"></i> Veirfy</asp:LinkButton>
                                                <asp:LinkButton ID="lbtnDeactivate" Visible="true" runat="server" CommandName="Deactivate" OnClientClick="return ShowLoading();"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' class="btn btn-danger btn-sm"
                                                    ToolTip="Deactivate Agent "> <i class="fa fa-times" title="Deactivate Agent "></i> Deactivate</asp:LinkButton>

                                                <asp:LinkButton ID="lbtnValidity" Visible="false" runat="server" CommandName="Validity" OnClientClick="return ShowLoading();"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' class="btn btn-success btn-sm"
                                                    ToolTip="Update Login Validity">  <i class="fa fa-check" title="Update Login Validity"></i> Update Login Validity</asp:LinkButton>

                                                <asp:LinkButton ID="lbtnRefund" runat="server" Visible="false" CommandName="Refund" OnClientClick="return ShowLoading();"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' class="btn btn-primary btn-sm"
                                                    ToolTip="Refund Agent Security and Wallet Balance">  <i class="fa fa-rupee-sign" title="Refund Agent Security and Wallet Balance"></i> Refund</asp:LinkButton>

                                                <asp:LinkButton ID="lbtnpwd" runat="server" Visible="true" CommandName="ChndPwd" OnClientClick="return ShowLoading();"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' class="btn btn-warning btn-sm"
                                                    ToolTip="Change Agnet password">  <i class="fa fa-lock" title="Change Agnet password"></i> Change Password</asp:LinkButton>
                                            </div>
                                        </div>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <asp:Panel ID="pnlNoAgent" runat="server" CssClass="w-100 p-5 text-center text-black-50">
                            <i class="fa fa-user-lock fa-5x"></i>
                            <h2 class="mt-2 text-black-50">
                                <asp:Label ID="lblNoAgent" runat="server"></asp:Label></h2>
                        </asp:Panel>


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
                        <h4 class="card-title text-left mb-0">Please Confirm
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnYesConfirmation" runat="server" OnClick="lbtnYesConfirmation_Click" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
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
            <cc1:ModalPopupExtender ID="mpdocment" runat="server" PopupControlID="pnlviewdocment" CancelControlID="btnclose"
                TargetControlID="Button5" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlviewdocment" runat="server" Style="display: none;">
                <div class="card" style="margin-top: 100px;">
                    <div class="card-header">

                        <div class="row">
                            <div class="col-lg-6">
                                <h4 class="card-title text-left mb-0">View Document
                                </h4>
                            </div>
                            <div class="col-lg-6 text-right">
                                <asp:LinkButton ID="btnclose" runat="server" CssClass="btn btn-danger"> <i class="fa fa-times"></i></asp:LinkButton>

                            </div>
                        </div>

                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <embed id="tkt" runat="server" src="" style="height: 85vh; width: 45vw" />
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button5" runat="server" Text="" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>

        <div class="row">
            <cc1:ModalPopupExtender ID="mpDeactivation" runat="server" PopupControlID="pnlDeactivation" CancelControlID="lbtnNo"
                TargetControlID="Button3" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlDeactivation" runat="server" Style="position: fixed; display: none;">
                <div class="card" style="min-width: 350px; max-width: 90vw; max-height: 90vh;">
                    <div class="card-header">
                        <div class="card-title m-0">
                            <asp:Label ID="Label9" runat="server" Text="Deactivation of agent " CssClass="font-weight-600"></asp:Label>
                        </div>
                    </div>
                    <div class="card-body text-left overflow-auto" style="min-height: 100px;">
                        <p class="font-weight-600">Read the following terms and condition for deactivation</p>
                        <p>1. If you deactivate the agent, you will have to refund the amount (security and wallet) to the agent.</p>
                        <p>2. If you deactivate the agent, you cannot activate the agent again.</p>
                        <p>3. After deactivating the agent, you will have to enter the refund like on which date and from which reference number etc...</p>



                        <p class="font-weight-600 text-danger">Do you want to deactivate agent ?</p>
                    </div>
                    <div class="card-footer text-right">
                        <asp:LinkButton ID="lbtnyes" runat="server" OnClick="lbtnYesConfirmation_Click" CssClass="btn btn-success"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                        <asp:LinkButton ID="lbtnNo" runat="server" CssClass="btn btn-warning"> <i class="fa fa-times"></i> No </asp:LinkButton>
                    </div>

                </div>
                <div style="visibility: hidden; height: 0px;">
                    <asp:Button ID="Button3" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpRefund" runat="server" PopupControlID="pnlRefund" CancelControlID="lbtnrefundno"
                TargetControlID="Button2" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlRefund" runat="server" Style="position: fixed; display: none;">
                <div class="card" style="min-width: 350px; max-width: 90vw; max-height: 90vh;">
                    <div class="card-header">
                        <div class="card-title m-0">
                            <asp:Label ID="Label6" runat="server" Text="Reufnd of agent deactivation" CssClass="font-weight-600"></asp:Label>
                        </div>
                    </div>
                    <div class="card-body text-left overflow-auto" style="min-height: 100px;">
                        <p class="font-weight-600">Agent has been deactivated and generated payment order for Refund amount (security amount and wallet balance)</p>

                        <div class="row">
                            <div class="col-lg-4">
                                <span class="text-muted">Payment Order Number </span>
                                <br />
                                <asp:Label ID="lblponumber" runat="server" Text="" Style="font-weight: 600;"></asp:Label>
                            </div>
                            <div class="col-lg-4">
                                <span class="text-muted">Deactivation Date/Time </span>
                                <br />
                                <asp:Label ID="lbldactivatedt" runat="server" Text="" Style="font-weight: 600;"></asp:Label>
                            </div>
                            <div class="col-lg-4">
                                <span class="text-muted">Refund Amount</span>
                                <br />
                                <asp:Label ID="lblrefundamt" runat="server" Text="" Style="font-weight: 600;"></asp:Label>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-lg-4">
                                <span class="text-muted">Bank <span class="text-danger">*</span></span>
                                <br />
                                <asp:DropDownList ID="ddlbank" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-4">
                                <span class="text-muted">Bank Ref. No. <span class="text-danger">*</span></span>
                                <br />
                                <asp:TextBox ID="txtbankrefno" placeholder="Max 20 Char." MaxLength="20" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-lg-4">
                                <span class="text-muted">Refund Date <span class="text-danger">*</span></span>
                                <br />
                                <asp:TextBox ID="txtrefunddate" placeholder="DD/MM/YYYY" MaxLength="10" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <p class="font-weight-600 text-danger">Do you want to refund agent amount ?</p>



                    </div>
                    <div class="card-footer">
                        <div class="row">
                            <div class="col-lg-6">
                                <asp:Label ID="lblmsg" runat="server" Text="" CssClass="text-danger" Visible="false"></asp:Label>
                            </div>
                            <div class="col-lg-6  text-right">
                                <asp:LinkButton ID="lbtnrefundyes" runat="server" CssClass="btn btn-success"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                                <asp:LinkButton ID="lbtnrefundno" runat="server" CssClass="btn btn-warning"> <i class="fa fa-times"></i> No </asp:LinkButton>
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



