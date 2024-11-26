<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/PortalAdmmaster.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="PAdminConcessionPolicy.aspx.cs" Inherits="Auth_PAdminConcessionPolicy" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/multiSelect/example-styles.css" rel="stylesheet" />
    <script src="../assets/multiSelect/jquery.multi-select.js"></script>
    <script src="../assets/multiSelect/jquery.multi-select.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#<%= ddlGender.ClientID %>').multiSelect();
            $('#<%= ddlServiceType.ClientID %>').multiSelect();
            $('#<%= ddlidverification.ClientID %>').multiSelect();
            $('#<%= ddldocumentverification.ClientID %>').multiSelect();
        });

        function validateFloatKeyPress(el, evt) {
            rbtapplicableonline

            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57)) {
                return false;
            }

            if (charCode == 46 && el.value.indexOf(".") !== -1) {
                return false;
            }

            return true;
        }

    </script>
    <style type="text/css">
        .rbl label {
            margin-left: 5px;
            color: black;
        }

        .multi-select-button {
            width: 200px;
        }

            .multi-select-button:after {
                margin-left: 9.4em;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <asp:HiddenField ID="hidtoken" runat="server" />
    <div class="container-fluid mb-5">
        <div class="card card-stats mb-3">
            <div class="row">
                <div class="col-lg-4 border-right">
                    <div class="card-body">
                        <div class="row m-0">
                            <div class="col-12">
                                <h5 class="mb-1">
                                    <asp:Label ID="lblSummary" runat="server" CssClass="text-capitalize">Summary as on Date 01/12/2021 04:00pm</asp:Label></h5>
                                <div class="col-12">
                                    <asp:Label runat="server" CssClass="form-control-label">Total</asp:Label>
                                    <asp:Label ID="lblTotalConcession" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Concession" CssClass="font-weight-bold float-right" Text="0"></asp:Label>
                                </div>
                                <div class="col-12">
                                    <asp:Label runat="server" CssClass="form-control-label">Active</asp:Label>
                                    <asp:Label ID="lblActivateConcession" runat="server" data-toggle="tooltip" data-placement="bottom" title="Active Concession" CssClass="font-weight-bold float-right" Text="0"></asp:Label>
                                </div>
                                <div class="col-12">
                                    <asp:Label runat="server" CssClass="form-control-label">Discontinue</asp:Label>
                                    <asp:Label ID="lblDeactConcession" runat="server" data-toggle="tooltip" data-placement="bottom" title="Deactive Concession" class="font-weight-bold float-right" Text="0"></asp:Label>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
                <div class="col-lg-4 border-right">
                    <div class="card-body">
                        <div class="row m-0">
                            <div class="col-12">
                                <h5 class="mb-1">
                                    <asp:Label runat="server" CssClass="text-capitalize">Generate Report</asp:Label></h5>
                                <div class="mt-3">
                                    <label class="form-control-label">Select Concession Type</label>
                                    <div class="input-group">
                                        <div class="input-group-prepend pr-2" style="width: 80%">

                                            <asp:DropDownList ID="ddlConcession" ToolTip="Concession Available" CssClass="form-control form-control-sm" Width="100%" runat="server"></asp:DropDownList>
                                        </div>
                                        <asp:LinkButton ID="lbtndownloadReport" runat="server" OnClientClick="return ShowLoading()" ToolTip="Download Office Report"
                                            CssClass="btn btn-success btn-sm">
                                             <i class="fa fa-download"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="card-body">
                        <div class="row mr-0">
                            <div class="row m-0 col-12" style="display: inline-block">
                                <h5 class="mb-1 float-left">Instructions</h5>

                            </div>
                            <div class="row m-0">
                                <div class="col-12">
                                    <div class="row m-0">
                                        <asp:Label ID="lblinstruction" runat="server" CssClass="form-control-label">Concession Name cannot be edited after creation</asp:Label><br />
                                        <asp:Label ID="Label3" runat="server" CssClass="form-control-label">Bus Pass Type Mandatory for Concession Creation</asp:Label><br />
                                        <asp:Label ID="Label2" runat="server" CssClass="form-control-label">Concession Condition Allowed Bus Pass Type</asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xl-4 order-xl-1">
                <div class="card" style="min-height: 50vh">
                    <div class="card-header border-bottom">
                        <div class="float-left">
                            <h5 class="mb-0">Concession List</h5>
                        </div>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="gvConcession" OnPageIndexChanging="gvConcession_PageIndexChanging" OnRowCommand="gvConcession_RowCommand" OnRowDataBound="gvConcession_RowDataBound" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
                            AllowPaging="true" PageSize="10" DataKeyNames="ebtmprintyn,abbrevation,concessionid,concessionname, concession_namelocal , remark , buspass_type , concessionper_fare , concessionper_tax , genderyn , gendertype , noofkmsyn , noofkms , agegroupyn , minage ,maxage ,servicetypeyn , service_typecode , stateyn , within_statyn , otherstateyn , additionalattendentyn , onlineverificationyn , idverificationyn , id_verificationyn , documentverificationyn , document_verification  , current_status ,status_ ,passapplicable ,otherconcession_yn ">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div class="row" style="font-size: 10pt;">
                                            <div class="col-lg-4">
                                                <%# Eval("concessionname") %><br />
                                                <span>Status - <%# Eval("status_") %> </span>
                                                <br />
                                                <span><%# Eval("passapplicable") %></span>
                                            </div>
                                            <div class="col-lg-4">
                                                <span>Concession</span><br />
                                                <span>On Fare </span>- <b><%# Eval("concessionper_fare") %> %</b><br />
                                                <span>On Tax </span>- <b><%# Eval("concessionper_tax") %> %</b>
                                            </div>
                                            <div class="col-lg-4 text-right">
                                                <asp:LinkButton ID="lbtnactivate" Visible="false" runat="server" CommandName="activate" CssClass="btn btn-sm btn-success" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Activate Concession"> <i class="fa fa-check "></i></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnDeactivate" Visible="false" runat="server" CommandName="deactivate" CssClass="btn btn-sm btn-danger" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Deactivate Concession"> <i class="fa fa-ban"></i></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnUpdateConcession" Visible="true" runat="server" CommandName="updateconcession" CssClass="btn btn-sm btn-primary" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Update Concession Details"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                <asp:LinkButton ID="lbtndelete" Visible="false" runat="server" CommandName="deleted" CssClass="btn btn-sm btn-warning" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Delete Concession"> <i class="fa fa-trash"></i></asp:LinkButton>

                                            </div>
                                        </div>
                                        <hr />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                        </asp:GridView>
                        <asp:Panel ID="pnlnoRecordfound" runat="server">
                            <div class="row">
                                <div class="col-lg-12 text-center mt-4" runat="server" id="div1">
                                    <h5 class="card-title text-uppercase text-muted mb-0">No Concession has been created yet</h5>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>

            </div>
            <div class="col-xl-8 order-xl-2">
                <asp:Panel ID="pnlAddConcession" runat="server" Visible="true">
                    <div class="card" style="min-height: 50vh">
                        <div class="card-header">
                            <div class="row align-items-center m-0">
                                <div class="col-8">
                                    <h5 class="mb-0">
                                        <asp:Label runat="server" ID="lblConcessionHead" Text="Add Concession Details"></asp:Label>
                                    </h5>
                                </div>
                                <div class="col-md-4 text-right">
                                    <p style="font-size: small; margin: 0px;" class="text-danger font-weight-bold">All marked * Fields are mandatory</p>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row mb-3">
                                <div class="col-lg-12">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="font-weight-bold text-capitalize text-sm text-gray-dark mb-2">1. Bus Pass Type</div>
                                            <asp:DropDownList ID="ddlBusPass" OnSelectedIndexChanged="ddlBusPass_SelectedIndexChanged" AutoPostBack="true" data-toggle="tooltip" data-placement="bottom" title="Bus Pass Type" CssClass="form-control form-control-sm" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 mb-3">
                                    <div class="font-weight-bold text-capitalize text-sm text-gray-dark mb-2">2. Concession Name</div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <asp:Label runat="server" CssClass="form-control-label">English<span style="color: red">*</span></asp:Label>
                                            <asp:TextBox ID="tbConsessionName" runat="server" CssClass="form-control form-control-sm" placeholder="Max 50 chars" MaxLength="50" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Consession Name"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,UppercaseLetters,LowercaseLetters,Custom" TargetControlID="tbConsessionName" ValidChars=" " />
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Label runat="server" CssClass="form-control-label">Local</asp:Label>
                                            <asp:TextBox ID="tbConsessionNameLocal" runat="server" CssClass="form-control form-control-sm" placeholder="Max 50 chars" MaxLength="50" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Consession Name(Local)"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Label runat="server" CssClass="form-control-label">Abbreviation<span style="color: red">*</span></asp:Label>
                                            <asp:TextBox ID="tbabbr" runat="server" CssClass="form-control form-control-sm" placeholder="Max 50 chars" MaxLength="50" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Consession Name(Local)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-5">
                                    <div class="font-weight-bold text-capitalize text-sm text-gray-dark">3. Remark <span style="color: red">*</span></div>

                                    <div class="row mt-3">
                                        <div class="col-lg-12">
                                            <asp:TextBox ID="tbRemark" MaxLength="200" TextMode="MultiLine" autocomplete="off" placeholder="Max 200 chars" data-toggle="tooltip" data-placement="bottom" title="Remark" CssClass="form-control form-control-sm" runat="server" Style="resize: none;"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbRemark" ValidChars=" " />
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="font-weight-bold text-capitalize text-sm text-gray-dark mt-3">4. Concession</div>
                            <div class="row mt-2">
                                <div class="col-lg-2">
                                    <asp:Label runat="server" CssClass="form-control-label"> On Fare</asp:Label><span style="color: red">*</span>
                                    <div class="input-group input-group-alternative">
                                        <div class="input-group">
                                            <asp:TextBox ID="tbconcessionfare" onkeypress="return validateFloatKeyPress(this, event)" runat="server" CssClass="form-control form-control-sm" placeholder="Max 3 digits" MaxLength="3" Text="100" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Concession On Fare"></asp:TextBox>
                                            <span class="input-group-text" id="tb"><i class="fa fa-percent"></i></span>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Numbers" TargetControlID="tbconcessionfare" ValidChars=" " />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <asp:Label runat="server" CssClass="form-control-label"> On Tax</asp:Label><span style="color: red">*</span>
                                    <div class="input-group input-group-alternative">
                                        <div class="input-group">
                                            <asp:TextBox ID="tbconcessiontax" onkeypress="return validateFloatKeyPress(this, event)" runat="server" CssClass="form-control form-control-sm" placeholder="Max 3 digits" MaxLength="3" Text="100" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Concession On Tax"></asp:TextBox>
                                            <span class="input-group-text" id="tb1"><i class="fa fa-percent"></i></span>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Numbers" TargetControlID="tbconcessiontax" ValidChars=" " />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="font-weight-bold text-capitalize text-sm text-gray-dark mt-3">5. EBTM Ticket Print</div>
                            <div class="row mt-2">
                                <div class="col-lg-5">
                                    <div class="input-group">
                                        <asp:RadioButtonList ID="rbtEBTMprintyn" Visible="true" GroupName="radio" CssClass="rb1" RepeatDirection="Horizontal" runat="server" AutoPostBack="true">
                                            <asp:ListItem Text="Yes"  Value="Y" style="margin-right:30px" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="No"  Value="N"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="font-weight-bold text-capitalize text-sm text-gray-dark mt-3">6. Concession Applicable To Online Booking ?</div>
                            <div class="row mt-2">
                                <div class="col-lg-5">
                                    <div class="input-group">
                                        <asp:RadioButtonList ID="rbtapplicableonline" Visible="true" GroupName="radio" CssClass="rb1" RepeatDirection="Horizontal" runat="server" AutoPostBack="true">
                                            <asp:ListItem Text="Yes"  Value="Y" style="margin-right:30px"></asp:ListItem>
                                            <asp:ListItem Text="No"  Value="N"  Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="font-weight-bold text-capitalize text-sm text-gray-dark mt-3">7. Condition</div>
                            <div class="row mt-2">
                                <div class="col-lg-6">
                                    <div class="ml-3">
                                        <asp:CheckBox runat="server" ID="chkGender" OnCheckedChanged="chkGender_CheckedChanged" CssClass="form-check-input" AutoPostBack="true" />
                                        <asp:Label runat="server" CssClass="form-control-label">Restricted to specific Gender</asp:Label><br />
                                        <asp:ListBox ID="ddlGender" Visible="false" runat="server" ToolTip="Select gender" SelectionMode="Multiple" CssClass="form-control form-control-sm">
                                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                            <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                        </asp:ListBox>
                                        <asp:Label runat="server" CssClass="form-control-label" ID="lblgender" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="ml-3">
                                        <asp:CheckBox runat="server" ID="chkKms" OnCheckedChanged="chkKms_CheckedChanged" CssClass="form-check-input" AutoPostBack="true" />
                                        <asp:Label runat="server" CssClass="form-control-label">Restricted to number of KMs</asp:Label><br />
                                        <asp:TextBox ID="tbKms" runat="server" Width="15%" Visible="false" CssClass="form-control form-control-sm" AutoComplete="off" MaxLength="3" placeholder="Max 3 digit" data-toggle="tooltip" data-placement="bottom" title="Number of KMs"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers" TargetControlID="tbKms" />
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-lg-6">
                                    <div class="ml-3">
                                        <asp:CheckBox runat="server" ID="chkAgeGroup" OnCheckedChanged="chkAgeGroup_CheckedChanged" CssClass="form-check-input" AutoPostBack="true" />
                                        <asp:Label runat="server" CssClass="form-control-label">Restricted to specific Age Group</asp:Label><br />
                                        <div class="row">
                                            <asp:Panel ID="pnlAge" Visible="false" runat="server">
                                                <div class="row">
                                                    <div class="col-3 pr-0">
                                                        <asp:TextBox ID="tbMinAge" runat="server" CssClass="form-control form-control-sm" AutoComplete="off" MaxLength="2" placeholder="Minimum" data-toggle="tooltip" data-placement="bottom" title="Minimum Age"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers" TargetControlID="tbMinAge" />
                                                    </div>
                                                    <div class="col-1" style="padding-right: 0px; padding-left: 8px; font-size: 15pt;">
                                                        <
                                                    </div>
                                                    <div class="col-3 pl-0">
                                                        <asp:TextBox ID="tbMaxAge" runat="server" CssClass="form-control form-control-sm" AutoComplete="off" MaxLength="3" placeholder="Maximum" data-toggle="tooltip" data-placement="bottom" title="Maximum Age"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers" TargetControlID="tbMaxAge" />
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="ml-3">
                                        <asp:CheckBox runat="server" ID="chkServiceType" OnCheckedChanged="chkServiceType_CheckedChanged" CssClass="form-check-input" AutoPostBack="true" />
                                        <asp:Label runat="server" CssClass="form-control-label">Applicable to Specific Bus Service Type</asp:Label><br />
                                        <asp:ListBox ID="ddlServiceType" Visible="false" runat="server" ToolTip="Select Service Type" SelectionMode="Multiple" CssClass="form-control form-control-sm"></asp:ListBox>
                                        <asp:Label runat="server" CssClass="form-control-label" ID="lblservicetype" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-lg-6">
                                    <div class="ml-3">
                                        <asp:CheckBox runat="server" ID="chkState" OnCheckedChanged="chkState_CheckedChanged" CssClass="form-check-input" AutoPostBack="true" />
                                        <asp:Label runat="server" CssClass="form-control-label">Journey Allowed in</asp:Label><br />
                                        <div class="ml-3">
                                            <asp:Panel ID="pnlState" runat="server" Visible="false">
                                                <asp:CheckBox runat="server" ID="chkWithinState" CssClass="form-check-input" />
                                                <asp:Label runat="server" CssClass="form-control-label">Within State</asp:Label><br />
                                                <asp:CheckBox runat="server" ID="chkOutsideState" CssClass="form-check-input" />
                                                <asp:Label runat="server" CssClass="form-control-label">Outside State</asp:Label>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="ml-3">
                                        <asp:CheckBox runat="server" ID="chkAttendant" CssClass="form-check-input rbl" />
                                        <asp:Label runat="server" CssClass="form-control-label">Additional Attendant  Allowed</asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-lg-6">
                                    <div class="ml-3">
                                        <asp:CheckBox runat="server" ID="chkallowotherconcession" CssClass="form-check-input rbl" />
                                        <asp:Label runat="server" CssClass="form-control-label">Allowed with other concession in same ticket</asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="font-weight-bold text-capitalize text-sm text-gray-dark mt-3">8. Verification Condition<span style="color: red">*</span></div>
                            <div class="row mt-2">
                                <div class="col-lg-12">
                                    <div class="ml-3">
                                        <asp:CheckBox runat="server" ID="chkonlineverification" OnCheckedChanged="chkonlineverification_CheckedChanged" CssClass="form-check-input rbl" AutoPostBack="true" OnClientClick="return ShowLoading()" />
                                        <asp:Label runat="server" CssClass="form-control-label">Require online Verification</asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-lg-12">
                                    <div class="ml-3">
                                        <asp:CheckBox runat="server" ID="chkidverification" OnCheckedChanged="chkidverification_CheckedChanged" CssClass="form-check-input rbl" AutoPostBack="true" OnClientClick="return ShowLoading()" />
                                        <asp:Label runat="server" CssClass="form-control-label">ID Require During Booking</asp:Label><br />
                                        <asp:ListBox ID="ddlidverification" runat="server" Visible="false" ToolTip="Select Documents ID Entered during Ticket Booking" SelectionMode="Multiple" CssClass="form-control"></asp:ListBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-lg-12">
                                    <div class="ml-3">
                                        <asp:CheckBox runat="server" ID="chkdocumentverification" OnCheckedChanged="chkdocumentverification_CheckedChanged" CssClass="form-check-input rbl" AutoPostBack="true" OnClientClick="return ShowLoading()" />
                                        <asp:Label runat="server" CssClass="form-control-label">Documents Require During journey</asp:Label><br />
                                        <asp:ListBox ID="ddldocumentverification" runat="server" Visible="false" ToolTip="Select Documents to be checked during journey" SelectionMode="Multiple" CssClass="form-control"></asp:ListBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row mt-3">
                                <div class="col-lg-12 text-right">
                                    <asp:LinkButton ID="lbtnUpdate" OnClick="lbtnUpdate_Click" runat="server" OnClientClick="return ShowLoading()" class="btn btn-success" Visible="false" data-toggle="tooltip" data-placement="bottom" title="Update Concession Details">
                                    <i class="fa fa-save"></i>&nbsp; Update</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_Click" Visible="true" runat="server" OnClientClick="return ShowLoading()" class="btn btn-success" data-toggle="tooltip" data-placement="bottom" title="Save Concession Details">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnReset" OnClick="lbtnReset_Click" runat="server" OnClientClick="return ShowLoading()" CssClass="btn btn-danger" data-toggle="tooltip" data-placement="bottom" title="Reset Concession Details">
                                    <i class="fa fa-undo"></i>&nbsp; Reset</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
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
                            <asp:LinkButton ID="lbtnYesConfirmation" OnClick="lbtnYesConfirmation_Click" runat="server" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                            <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button4" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
    </div>

</asp:Content>

