
<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Auth/PortalAdmmaster.master" AutoEventWireup="true" CodeFile="PABusPassTypes.aspx.cs" Inherits="Auth_PABusPassTypes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/multiSelect/example-styles.css" rel="stylesheet" />
    <script src="../assets/multiSelect/jquery.multi-select.js"></script>
    <script src="../assets/multiSelect/jquery.multi-select.min.js"></script>
    <style type="text/css">
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
        .rbl input[type="radio"] {
            margin-left: 10px;
            margin-right: 10px;
        }

        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .multi-select-button {
            width: 200px;
        }

            .multi-select-button:after {
                margin-left: 9.4em;
            }
    </style>
    <script type="text/javascript">
        $(function () {
            $('#<%= ddlgsttype.ClientID %>').multiSelect();
            $('#<%= ddlGender.ClientID %>').multiSelect();
            $('#<%= ddlServiceType.ClientID %>').multiSelect();
            $('#<%= ddlDocumentIdProof.ClientID %>').multiSelect();
            $('#<%= ddlDocumentAddProof.ClientID %>').multiSelect();
            $('#<%= ddlApplicableCharges.ClientID %>').multiSelect();

        });

        function validateFloatKeyPress(el, evt) {

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid py-2">
        <div class="row align-items-center" style="padding-top: 20px">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="row">
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h6 class="mb-1">
                                            <asp:Label ID="lblSummary" runat="server" CssClass="text-capitalize">Summary as on Date 01/12/2021 04:00pm</asp:Label></h6>
                                        <div class="col-12">
                                            <asp:Label runat="server" CssClass=" card-title text-uppercase">Total</asp:Label>
                                            <asp:Label ID="lblTotal" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Bus Pass Types" CssClass="font-weight-bold float-right" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-12">
                                            <asp:Label runat="server" class=" card-title text-uppercase">Active</asp:Label>
                                            <asp:Label ID="lblActivate" runat="server" data-toggle="tooltip" data-placement="bottom" title="Active Bus Pass Types" CssClass="font-weight-bold float-right" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-12">
                                            <asp:Label runat="server" CssClass=" card-title text-uppercase">Discontinue</asp:Label>
                                            <asp:Label ID="lblDeactive" runat="server" data-toggle="tooltip" data-placement="bottom" title="Deactive Bus Pass Types" class="font-weight-bold float-right" Text="0"></asp:Label>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h6 class="mb-1">
                                            <asp:Label runat="server" CssClass="text-capitalize">Generate Report</asp:Label></h6>
                                        <div class="mt-3">
                                            <label class="form-control-label">Select Bus Pass Type</label>
                                            <div class="input-group">
                                                <div class="input-group-prepend pr-2" style="width: 80%">

                                                    <asp:DropDownList ID="ddlBusPassType" ToolTip="Bus Pass Type Available" CssClass="form-control form-control-sm" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <asp:LinkButton ID="lbtndownloadReport" runat="server" Visible="false" ToolTip="Download Office Report"
                                                    CssClass="btn btn bg-gradient-green btn-sm text-white">
                                             <i class="fa fa-download"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="card-body">
                                <div class="row mr-0">
                                    <div class="row m-0 col-12" style="display: inline-block">
                                        <h6 class="mb-1 float-left">Instructions</h6>
                                        <div class="float-right">
                                            <asp:LinkButton ID="lbtnview" data-toggle="tooltip" OnClick="lbtnview_Click" data-placement="bottom" title="View Instructions" runat="server" Width="30px" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnHistory" runat="server" Visible="false" class="btn btn bg-gradient-green btn-sm text-white" Width="30px" data-toggle="tooltip" data-placement="bottom" title="History of Bus Pass Type">
                                            <i class="fa fa-history"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="row m-0">
                                        <div class="col-12">
                                            <div class="row m-0">
                                                <asp:Label ID="lblinstruction" runat="server" CssClass="form-control-label">Bus Pass Type Name cannot be edited after creation</asp:Label>
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
            <div class="col-xl-4 order-xl-1">
                <div class="card" style="min-height: 500px">

                    <div class="card-header">
                        <div class="row border-bottom m-0 p-0">
                            <div class="col-lg-3" style="text-align: right; padding-top: 14px;">
                                <p class="text-sm font-weight-bold text-primary">Action</p>
                            </div>
                            <div class="col-lg-3  m-0 p-0">
                                <a href="PABusPassCharges.aspx" class="btn btn-sm btn-secondary w-100">Bus Pass
                                    <br />
                                    Charges</a>
                            </div>
                            <div class="col-lg-3  m-0 p-0">
                                <h6>
                                    <a href="PABusPassCategory.aspx" class="btn btn-sm btn-secondary w-100">Bus Pass
                                        <br />
                                        Categories</a>
                                </h6>
                            </div>
                            <div class="col-lg-3  m-0 p-0">
                                <a href="PABusPassTypes.aspx" class="btn btn-sm btn-primary w-100">Bus Pass
                                    <br />
                                    Types</a>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:GridView ID="gvBusPassTypes" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
                                    AllowPaging="true" PageSize="10" OnPageIndexChanging="gvBusPassTypes_PageIndexChanging" OnRowCommand="gvBusPassTypes_RowCommand" OnRowDataBound="gvBusPassTypes_RowDataBound"
                                    DataKeyNames="buspass_type_id,buspass_type_name,buspass_type_name_local,bus_pass_category_id,val_description,val_description_local,CONCESSION_TYPE,RESTRICTED_TOGENDER_YN,
  RESTRICTED_TOGENDER_TYPE,RESTRICTED_TOAGE_YN,RESTRICTED_AGE_MAX,RESTRICTED_AGE_MIN,RESTRICTED_KMS_YN,RESTRICTED_NO_OF_KMS,BUS_SERVICE_TYPES_YN,
  TO_BUS_SERVICE_TYPES,ALLOWED_INSTATE_YN,ALLOWED_OUTSIDE_STATE_YN,RENEW_YN,NO_OF_DAYS,ISSUENCE_YN,ISSUENCE_TYPE,dbt_yn,DOCID_NEW_YN,DOCID_NEW_TYPES,
  DOCID_RENEW_YN,DOCID_RENEW_TYPES,DOCID_DUPLICATE_YN,DOCID_DUPLICATE_TYPES,DOCADD_NEW_YN,DOCADD_NEW_TYPES,DOCADD_RENEW_YN,
  doc_add_renew_types,DOCADD_DUPLICATE_YN,DOCADD_DUPLICATE_TYPES,VALIDITY_INDAYS,PHOTO_NEWYN,PHOTO_RENEWYN,PHOTO_DUPLICATEYN,
  BUSPASS_CHARGESID,ADVANCE_FAREYN,DUPLICATE_YN,DUPLICATE_TYPES,current_status,busspass_categoryname,bus_pass_abbr,concession_per_fare,concession_per_tax,
                                    validity_month,applicable_online_concession_yn,enroutecheck_document,buspass_abbrold,gst_yn,gst_type">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="row pr-3 pl-3">
                                                    <div class="col-lg-7">

                                                        <p class="mb-0 text-xs">
                                                           <span><%# Eval("busspass_categoryname") %></span> <br />                                                           
                                                            <b><%# Eval("buspass_type_name") %>(<%# Eval("bus_pass_abbr") %>)</b>
                                                        </p>
                                                    </div>

                                                    <div class="col-lg-5 text-right m-0 p-0">
                                                        <asp:LinkButton ID="lbtnDeactivate" Visible="true" runat="server" CommandName="Deactivate" CssClass="btn btn-sm btn-danger" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Deactivate Bus Pass Types"> <i class="fa fa-ban"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnActivate" Visible="true" runat="server" CommandName="Activate" CssClass="btn btn-sm btn-success" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Activate Bus Pass Types"> <i class="fa fa-check"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnUpdateBusPassTypes" Visible="true" runat="server" CommandName="updateBusPassTypes" CssClass="btn btn-sm btn-primary" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Update Bus Pass Types Details"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                                <hr />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                                </asp:GridView>
                                <asp:Panel ID="pnlnoRecordfound" runat="server">
                                    <div class="card card-stats">
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-lg-12 text-center mt-4" runat="server" id="div1">
                                                    <span class="h6 font-weight-bold mb-0">Start Bus Pass Types Creation </span>
                                                    <h5 class="card-title text-uppercase text-muted mb-0">No Bus Pass Types has been created yet</h5>
                                                </div>
                                                <div class="col-lg-12 text-center mt-4" runat="server" id="div2" visible="false">
                                                    <span class="h6 font-weight-bold mb-0">No Record Found </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-xl-8 order-xl-2">

                <div class="card" style="min-height: 500px">
                    <div class="card-header border-bottom">
                        <div class="float-left">
                            <h4 class="mb-0">New Bus Pass Types</h4>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="row mt-3">
                                    <div class="col-lg-8">
                                        <h5>1. Category<span style="color: red">*</span></h5>
                                    </div>
                                </div>
                                <div class="row mb-2">
                                    <div class="col-lg-4">
                                        <asp:DropDownList ID="ddlPassCategory" runat="server" CssClass="form-control form-control-sm" data-toggle="tooltip" data-placement="bottom" title="Select Pass Category"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row mt-3">
                                    <div class="col-lg-8">
                                        <h5>2. Name</h5>
                                    </div>
                                </div>
                                <div class="row border-bottom">
                                    <div class="col-lg-4 my-1">
                                        <asp:Label runat="server" CssClass="form-control-label">English<span style="color: red">*</span></asp:Label>
                                        <asp:TextBox ID="tbName" runat="server" CssClass="form-control form-control-sm" placeholder="Max 50 character" MaxLength="50" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Pass Type Name"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,UppercaseLetters,LowercaseLetters,Custom" TargetControlID="tbName" ValidChars=" " />
                                    </div>
                                    <div class="col-lg-2 my-1">
                                        <asp:Label runat="server" CssClass="form-control-label"> Abbreviation<span style="color: red">*</span></asp:Label>
                                        <asp:TextBox ID="tbabbr" runat="server" CssClass="form-control form-control-sm" placeholder="Max 2 character" MaxLength="2" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Pass Type  Abbreviation Name"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="UppercaseLetters" TargetControlID="tbabbr" />
                                    </div>
                                    <div class="col-lg-2 my-1">
                                        <asp:Label runat="server" CssClass="form-control-label">Old Abbreviation</asp:Label>
                                        <asp:TextBox ID="tbabbrold" runat="server" CssClass="form-control form-control-sm" placeholder="Max 3 character" MaxLength="5" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" Enabled ="false" title="Pass Type Old Abbreviation Name"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="UppercaseLetters" TargetControlID="tbabbrold" />
                                    </div>
                                    <div class="col-lg-4 my-1">
                                        <asp:Label runat="server" CssClass="form-control-label">Local</asp:Label>
                                        <asp:TextBox ID="tbNameLocal" runat="server" CssClass="form-control form-control-sm" placeholder="Max 50 character" MaxLength="50" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Pass Type Name(Local)"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row mt-3">
                                    <div class="col-lg-8">
                                        <h5>3. Description</h5>
                                    </div>
                                </div>
                                <div class="row border-bottom">

                                    <div class="col-lg-6 my-1">
                                        <asp:Label runat="server" CssClass="form-control-label">English<span style="color: red">*</span></asp:Label>

                                        <asp:TextBox ID="tbDescriptionEn" runat="server" Height="60px" TextMode="MultiLine" Style="resize: none" CssClass="form-control form-control-sm" placeholder="Max 200 character" MaxLength="200" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Pass Type Description"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers,UppercaseLetters,LowercaseLetters,Custom" TargetControlID="tbDescriptionEn" ValidChars=" " />
                                    </div>
                                    <div class="col-lg-6 my-1">
                                        <asp:Label runat="server" CssClass="form-control-label">Local</asp:Label>

                                        <asp:TextBox ID="tbDescriptionLocal" TextMode="MultiLine" Height="60px" Style="resize: none" runat="server" CssClass="form-control form-control-sm" placeholder="Max 200 character" MaxLength="200" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Pass Type Description(Local)"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row mt-3">
                                    <div class="col-lg-8">
                                        <h5>4. Issuance Mode<span style="color: red">*</span></h5>
                                    </div>
                                </div>
                                <div class="row border-bottom">
                                    <div class="col-lg-8 my-1">
                                        <asp:RadioButtonList ID="rbtIssuance" Visible="true" GroupName="radio" CssClass="rb1" RepeatDirection="Horizontal" runat="server">
                                            <asp:ListItem Text="Instant Issuance" class="pr-2 pl-2 form-control-label" Value="I"></asp:ListItem>
                                            <asp:ListItem Text="After Approval" class="pr-2 pl-2 form-control-label" Value="A"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="row mt-3">
                                    <div class="col-lg-8">
                                        <h5>5. Advance Fare Applicable</h5>
                                    </div>
                                </div>
                                <div class="row border-bottom">
                                    <div class="col-lg-8 my-1">
                                        <asp:RadioButtonList ID="rbtadvancefare" Visible="true" OnSelectedIndexChanged="rbtadvancefare_SelectedIndexChanged" GroupName="radio" CssClass="rb1" RepeatDirection="Horizontal" runat="server" AutoPostBack="true">
                                            <asp:ListItem Text="Yes" class="pr-2 pl-2 form-control-label" Value="Y"></asp:ListItem>
                                            <asp:ListItem Text="No" class="pr-2 pl-2 form-control-label" Value="N" Selected="True" ></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="row border-bottom" id="dvadvancefare" runat="server" visible="false">
                                    <div class="col-lg-2">
                                        <asp:Label runat="server" CssClass="form-control-label">Concession On Fare</asp:Label><span style="color: red">*</span>
                                        <div class="input-group input-group-alternative">
                                            <div class="input-group">
                                                <asp:TextBox ID="tbconcessionfare" onkeypress="return validateFloatKeyPress(this, event)" runat="server" CssClass="form-control form-control-sm" placeholder="Max 3 digits" MaxLength="3" Text="100" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Pass Type Description"></asp:TextBox>
                                                <span class="input-group-text" id="tb"><i class="fa fa-percent"></i></span>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Numbers" TargetControlID="tbconcessionfare" ValidChars=" " />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:Label runat="server" CssClass="form-control-label">Concession On Tax</asp:Label><span style="color: red">*</span>
                                        <div class="input-group input-group-alternative">
                                            <div class="input-group">
                                                <asp:TextBox ID="tbconcessiontax" onkeypress="return validateFloatKeyPress(this, event)" runat="server" CssClass="form-control form-control-sm" placeholder="Max 3 digits" MaxLength="3" Text="100" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Pass Type Description"></asp:TextBox>
                                                <span class="input-group-text" id="tb1"><i class="fa fa-percent"></i></span>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Numbers" TargetControlID="tbconcessiontax" ValidChars=" " />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row mt-3">
                                    <div class="col-lg-8">
                                        <h5>6. Bus Pass Applicable to Online Booking Concession</h5>
                                    </div>
                                </div>
                                <div class="row border-bottom">
                                    <div class="col-lg-8 my-1">
                                        <asp:RadioButtonList ID="rbtapplicableonline" Visible="true" GroupName="radio1" CssClass="rb1" RepeatDirection="Horizontal" runat="server" AutoPostBack="true">
                                            <asp:ListItem Text="Yes" class="pr-2 pl-2 form-control-label" Value="Y"></asp:ListItem>
                                            <asp:ListItem Text="No" class="pr-2 pl-2 form-control-label" Value="N" Selected="True" ></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                
                                <%-- <div class="row border-bottom">
                                    <div class="col-lg-4 my-1">
                                        <asp:RadioButtonList ID="rbtConcession" AutoPostBack="true" GroupName="radio" CssClass="rb1" RepeatDirection="Horizontal" runat="server">
                                            <asp:ListItem Text="Free" class="pr-2 pl-2 form-control-label" Value="F"></asp:ListItem>
                                            <asp:ListItem Text="Concession" class="pr-2 pl-2 form-control-label" Value="C"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-lg-4 my-1">
                                        <asp:Panel ID="pnlConcession" Visible="false" runat="server">
                                            <div class="row m-0">
                                                <div class="col-lg-6">
                                                    <asp:TextBox ID="tbConcessionPer" onkeypress="return validateFloatKeyPress(this, event)" runat="server" CssClass="form-control form-control-sm" placeholder="Max 5 digits" MaxLength="5" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Pass Type Description"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="tbConcessionPer" ValidChars=" " />
                                                </div>
                                                <div class="col-lg-2 pr-0 text-left">
                                                    %
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>

                                </div>--%>
                                <div class="row my-3">
                                    <div class="col-lg-12">
                                        <h5>6. Other Configurations</h5>
                                    </div>
                                </div>
                                <div class="row ml-3 border-bottom">
                                    <div class="col-lg-6">
                                        <asp:CheckBox runat="server" ID="chkgst" OnCheckedChanged="chkgst_CheckedChanged" CssClass="form-check-input" AutoPostBack="true" />
                                        <asp:Label runat="server" CssClass="form-control-label">Applicable to GST</asp:Label><br />
                                        <asp:ListBox ID="ddlgsttype" Visible="false" runat="server" ToolTip="Select GST" SelectionMode="Multiple" CssClass="form-control form-control-sm"></asp:ListBox>
                                    </div>
                                     <div class="col-lg-6">
                                         </div>
                                    <div class="col-lg-6">
                                        <asp:CheckBox runat="server" ID="chkGender" OnCheckedChanged="chkGender_CheckedChanged" CssClass="form-check-input" AutoPostBack="true" />
                                        <asp:Label runat="server" CssClass="form-control-label">Restricted to specific Gender</asp:Label><br />
                                        <asp:ListBox ID="ddlGender" Visible="false" runat="server" ToolTip="Select gender" SelectionMode="Multiple" CssClass="form-control form-control-sm">
                                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                            <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                            <asp:ListItem Text="Other" Value="O"></asp:ListItem>
                                        </asp:ListBox>

                                    </div>
                                    <div class="col-lg-6">
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
                                                        
                                                    </div>
                                                    <div class="col-3 pl-0">
                                                        <asp:TextBox ID="tbMaxAge" runat="server" CssClass="form-control form-control-sm" AutoComplete="off" MaxLength="3" placeholder="Maximum" data-toggle="tooltip" data-placement="bottom" title="Minimum Age"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers" TargetControlID="tbMaxAge" />
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:CheckBox runat="server" ID="chkServiceType" OnCheckedChanged="chkServiceType_CheckedChanged" CssClass="form-check-input" AutoPostBack="true" />
                                        <asp:Label runat="server" CssClass="form-control-label">Applicable to Specific Bus Service Type</asp:Label><br />
                                        <asp:ListBox ID="ddlServiceType" Visible="false" runat="server" ToolTip="Select Service Type" SelectionMode="Multiple" CssClass="form-control form-control-sm"></asp:ListBox>

                                    </div>
                                    <div class="col-lg-6">
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
                                    <div class="col-lg-6">
                                        <asp:CheckBox runat="server" ID="chkKms" OnCheckedChanged="chkKms_CheckedChanged" CssClass="form-check-input" AutoPostBack="true" />
                                        <asp:Label runat="server" CssClass="form-control-label">Restricted to number of KMs</asp:Label><br />
                                        <asp:Panel ID="pnlKm" Visible="false" runat="server">
                                            <div class="row">
                                                <div class="col-lg-5">
                                                    <asp:TextBox ID="tbKms" runat="server" Width="100%" CssClass="form-control form-control-sm" AutoComplete="off" MaxLength="3" placeholder="Max 3 digit" data-toggle="tooltip" data-placement="bottom" title="Minimum Age"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers" TargetControlID="tbKms" />
                                                </div>
                                                <div class="col-lg-2 pr-0 text-left">
                                                    KMs
                                                </div>
                                            </div>
                                        </asp:Panel>

                                    </div>
                                    <div class="col-lg-6 pb-1">
                                        <asp:CheckBox runat="server" ID="chkRenewYN" OnCheckedChanged="chkRenewYN_CheckedChanged" AutoPostBack="true" CssClass="form-check-input" />
                                        <asp:Label runat="server" CssClass="form-control-label">Renew Allowed</asp:Label><br />
                                        <asp:Panel ID="pnlRenewDays" runat="server" Visible="false">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <span class="text-danger" style="font-size: 8pt">(Advance Days before expire)</span><br />
                                                </div>
                                                <div class="col-lg-4">

                                                    <asp:TextBox ID="tbRenewNoofdays" runat="server" Width="100%" CssClass="form-control form-control-sm" AutoComplete="off" MaxLength="1" placeholder="No of Days" data-toggle="tooltip" data-placement="bottom" title="No of days Before Expiry renewal request can be done"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers" TargetControlID="tbRenewNoofdays" />

                                                </div>
                                                <div class="col-lg-2 pr-0 text-left">
                                                    Days
                                                </div>
                                            </div>

                                        </asp:Panel>



                                    </div>
                                    <div class="col-lg-6 pb-1">
                                        <asp:CheckBox runat="server" ID="chkDBTid" CssClass="form-check-input" />
                                        <asp:Label runat="server" CssClass="form-control-label">Allow Direct Beneficary Transfer</asp:Label><br />

                                    </div>                                   
                                </div>
                                <div class="row my-3">
                                    <div class="col-lg-3">
                                        <h5>7. Applicable Charges</h5>
                                        <asp:ListBox ID="ddlApplicableCharges" runat="server" ToolTip="Select Applicable Charges" SelectionMode="Multiple" CssClass="form-control form-control-sm"></asp:ListBox>
                                    </div>

                                    <div class="col-lg-3">
                                        <h5>8. Photo Required For<span style="color: red">*</span></h5>

                                        <div class="ml-3">
                                            <asp:CheckBox runat="server" ID="chkphotoNew" CssClass="form-check-input" />
                                            <asp:Label runat="server" CssClass="form-control-label">New Pass</asp:Label><br />
                                            <asp:CheckBox runat="server" ID="chkphotoRenew" CssClass="form-check-input" />
                                            <asp:Label runat="server" CssClass="form-control-label">Renew Pass</asp:Label><br />

                                        </div>
                                    </div>

                                    <div class="col-lg-2">
                                        <h5>9. Validity in Days <span style="color: red">*</span></h5>
                                        <asp:TextBox ID="tbValidity" runat="server" CssClass="form-control form-control-sm w-50" placeholder="Max 3 digits" MaxLength="3" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Validity"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers" TargetControlID="tbValidity" ValidChars=" " />

                                    </div>
                                    <div class="col-lg-3">
                                        <h5>10. Validity Restricted On</h5>
                                        <asp:DropDownList ID="ddlmonths" runat="server" CssClass="form-control form-control-sm w-50" data-toggle="tooltip" data-placement="bottom" title="Select Restricted Month">
                                            <asp:ListItem Value="0">None</asp:ListItem>
                                            <asp:ListItem Value="01">January</asp:ListItem>
                                            <asp:ListItem Value="02">February</asp:ListItem>
                                            <asp:ListItem Value="03">March</asp:ListItem>
                                            <asp:ListItem Value="04">April</asp:ListItem>
                                            <asp:ListItem Value="05">May</asp:ListItem>
                                            <asp:ListItem Value="06">June</asp:ListItem>
                                            <asp:ListItem Value="07">July</asp:ListItem>
                                            <asp:ListItem Value="08">August</asp:ListItem>
                                            <asp:ListItem Value="09">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">

                                        <h5>11. Document As ID Proof</h5>
                                        <asp:ListBox ID="ddlDocumentIdProof" AutoPostBack="true" OnSelectedIndexChanged="ddlDocumentIdProof_SelectedIndexChanged" runat="server" ToolTip="Select Document As ID Proof" SelectionMode="Multiple" CssClass="form-control form-control-sm"></asp:ListBox>


                                        <div class="ml-6">
                                            <asp:Panel ID="pnlDocumentAsIDProof" Visible="false" runat="server">
                                                <asp:CheckBox runat="server" ID="chkNewPass" CssClass="form-check-input" />
                                                <asp:Label runat="server" CssClass="form-control-label">New Pass</asp:Label><br />
                                                <asp:CheckBox runat="server" ID="chkRenew" CssClass="form-check-input" />
                                                <asp:Label runat="server" CssClass="form-control-label">Renew</asp:Label>


                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 py-1">
                                        <h5>12. Document As Address Proof</h5>
                                        <asp:ListBox ID="ddlDocumentAddProof" AutoPostBack="true" OnSelectedIndexChanged="ddlDocumentAddProof_SelectedIndexChanged" runat="server" ToolTip="Select Document As Address Proof" SelectionMode="Multiple" CssClass="form-control form-control-sm"></asp:ListBox>

                                        <div class="ml-6">


                                            <asp:Panel ID="pnlAddress" Visible="false" runat="server">
                                                <asp:CheckBox runat="server" ID="chkAddNew" CssClass="form-check-input" />
                                                <asp:Label runat="server" CssClass="form-control-label">New Pass</asp:Label><br />
                                                <asp:CheckBox runat="server" ID="chkAddRenew" CssClass="form-check-input" />
                                                <asp:Label runat="server" CssClass="form-control-label">Renew</asp:Label>

                                            </asp:Panel>
                                        </div>


                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <h5>13. Enroute Check Document</h5>
                                        <asp:DropDownList ID="ddlenroutechkdoc" runat="server" CssClass="form-control form-control-sm w-50" data-toggle="tooltip" data-placement="bottom" title="Enroute Check Document">
                                            <asp:ListItem Value="N">None</asp:ListItem>
                                            <asp:ListItem Value="B">Both(Pass/Aadhar)</asp:ListItem>
                                            <asp:ListItem Value="P">Pass</asp:ListItem>
                                            <asp:ListItem Value="A">Aadhar</asp:ListItem>
                                        </asp:DropDownList>
                                        </div>
                                    </div>
                                <div class="row py-2">
                                    <div class="col-lg-12 text-center">
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" class="btn btn-success" OnClick="lbtnUpdate_Click" Visible="false" data-toggle="tooltip" data-placement="bottom" title="Update Concession Details">
                                    <i class="fa fa-save"></i>&nbsp; Update</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnSave" Visible="true" runat="server" OnClick="lbtnSave_Click" class="btn btn-success" data-toggle="tooltip" data-placement="bottom" title="Save Concession Details">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-danger" OnClick="lbtnReset_Click" data-toggle="tooltip" data-placement="bottom" title="Reset Concession Details">
                                    <i class="fa fa-undo"></i>&nbsp; Reset</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" Visible="false" runat="server" OnClick="lbtnCancel_Click" CssClass="btn btn-danger" data-toggle="tooltip" data-placement="bottom" title="Cancel Concession Details">
                                    <i class="fa fa-times"></i>&nbsp; Cancel</asp:LinkButton>
                                    </div>
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
                TargetControlID="Button2" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlerror" runat="server" Style="position: fixed;">
                <div class="card" style="min-width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">
                            <asp:Label ID="Label1" runat="server" Text="Please Check & Correct"></asp:Label>
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblerrormsg" runat="server" Text="Do you want to save Route ?"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnclose1" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> OK </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button2" runat="server" Text="" />
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
                        <h4 class="card-title">Confirm
                        </h4>
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
            <cc1:ModalPopupExtender ID="mpInfo" runat="server" PopupControlID="PanelInfo" CancelControlID="LinkButtonMpInfoClose"
                TargetControlID="lbtnview" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="PanelInfo" runat="server" Style="position: fixed;">
                <div class="card" style="min-width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">About Module
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <ol>
                            <li>Here you can Add/Update Bus Service Type and its other details like charges and speed limit.</li>
                            <li>Service Type Fare type wise details will be added here along with effective date.</li>
                            <li>We can discontinue service type if it's currently not in use.</li>
                        </ol>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="LinkButtonMpInfoClose" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>


