<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmMaster.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="SysAdmOfficeMgmt.aspx.cs" Inherits="Auth_SysAdmOfficeMgmt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .checkboxlist label {
            margin-left: 5px;
            font-size: medium;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="header pb-7">
    </div>
    <div class="container-fluid mt--6">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <asp:Panel ID="pnlOffice" runat="server" Visible="true">
            <div class="row align-items-center">
                <div class="col-md-12 ">
                    <div class="card card-stats mb-3">
                        <div class="row m-0">
                            <div class="col-md-4 border-right">
                                <div class="card-body">
                                    <div class="row m-0">
                                        <div class="col-12">
                                            <div class=" text-capitalize" style="font-size: medium; font-weight: bold">
                                                <asp:Label ID="lblDateTime" runat="server" class="h3 font-weight-bold mb-0"> Summary as on Date </asp:Label>
                                            </div>
                                            <div class="row m-0">
                                                <div class="col-lg-6 border-right">
                                                    <div class="row m-0">
                                                        <div class="col-12">
                                                            <label class="form-control-label">Hq Office :</label>&nbsp;&nbsp;
                                                    <asp:Label ID="lblhq" runat="server" CssClass="h3 font-weight-bold mb-0">0</asp:Label>
                                                        </div>
                                                        <div class="col-12">
                                                            <label class="form-control-label">Bus Station:</label>
                                                            <asp:Label ID="lblStation" runat="server" class="h3 font-weight-bold mb-0">0</asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="row m-0">
                                                        <div class="col-12">
                                                            <label class="form-control-label">Depot Office: </label>
                                                            &nbsp;&nbsp;
                                                <asp:Label ID="lblDepot" runat="server" class="h3 font-weight-bold mb-0">0</asp:Label>
                                                        </div>
                                                        <div class="col-12">
                                                            <label class="form-control-label">Division Office:</label>
                                                            <asp:Label ID="lblDivision" runat="server" class="h3 font-weight-bold mb-0">0</asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                </div>
                            </div>

                            <div class="col-md-4 border-right">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col">

                                            <div class="input-group-prepend">
                                                <h4 class="mb-1">Generate Office Report</h4>


                                                <asp:LinkButton ID="lbtnExport" runat="server" ToolTip="Download Office Report" OnClick="lbtnExport_Click"
                                                    CssClass="btn btn bg-gradient-green btn-sm text-white ml-2">
                                             <i class="fa fa-download"></i>
                                                </asp:LinkButton>

                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col">
                                            <div>
                                                <h4 class="mb-1">Instructions</h4>
                                            </div>
                                            <label class="form-control-label">1. Here you can add Office along with its basic details.</label>
                                            <br />
                                            <label class="form-control-label">2. You can also update office details.</label>
                                            <br />
                                            <label class="form-control-label">2. You Can add unit office also under the main office.</label>
                                        </div>
                                        <div class="col-auto">
                                            <asp:LinkButton ID="lbtnViewInstruction" runat="server" ToolTip="View Instructions" OnClick="lbtnview_Click" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-eye"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnDownloadInst" OnClick="lbtnDownloadInst_Click" runat="server" ToolTip="Download Instructions" CssClass="btn btn bg-gradient-green btn-sm text-white">
                                   <i class="fa fa-download"></i>
                                            </asp:LinkButton>

                                        </div>
                                    </div>

                                </div>
                            </div>

                        </div>

                    </div>
                </div>
            </div>

        </asp:Panel>

        <div class="row ">
            <div class="col-lg-4">
                <asp:Panel ID="pnlOfficeList" runat="server" Visible="true">
                    <div class="card mb-3" style="min-height: 600px;">
                        <div class="card-header border-bottom">
                            <h3 class="mb-0">Office List</h3>
                        </div>
                        <div class="card card-stats">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-4 pr-0">
                                                <h6 class="form-control-label text-muted my-0">Office Name</h6>
                                                <asp:TextBox class="form-control" runat="server" ID="tbSOfcName" MaxLength="50" ToolTip="Office Name"
                                                    placeholder="Max 50 Characters" Text="" CssClass="form-control form-control-sm"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <h6 class="form-control-label text-muted my-0">Office Level</h6>
                                                <asp:DropDownList ID="ddlSOfficeLevel" runat="server" CssClass="form-control form-control-sm" ToolTip="Office Level">
                                                    <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-4 mt-3 pt-1">
                                                <asp:LinkButton ID="lbtnsearch" OnClick="lbtnsearch_Click" runat="server" class="btn btn-sm btn-primary" ToolTip="Search"> <i class="fa fa-search"></i></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnResetList" OnClick="lbtnResetList_Click" runat="server" class="btn btn-sm btn-danger" ToolTip="Reset"> <i class="fa fa-undo"></i></asp:LinkButton>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-body p-0">
                            <div class="col-12">
                                <asp:GridView ID="gvOffices" runat="server" CssClass="table table-hover table-striped w-100" ShowHeader="false"
                                    AutoGenerateColumns="false" AllowPaging="true" GridLines="None" HeaderStyle-CssClass="font-weight-bold"
                                    OnPageIndexChanging="grvOffices_PageIndexChanging" PageSize="10" OnRowDataBound="gvOffices_RowDataBound" OnRowCommand="gvOffices_RowCommand"
                                    DataKeyNames="officeid,officename,adrs,statecode,districtcode,oflvlid,landl1,landl2,mob,eml,activest,reportingoofcid,ofclvlnm,stname,distname,km,delete_yn">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblstatus" Visible="false" Text=' <%# Eval("activest") %> '></asp:Label>
                                                <div class="">
                                                    <div class="row px-3 pt-2">
                                                        <div class="col">
                                                            <h3 class="text-uppercase mb-0 font-weight-bold text-sm">
                                                                <%# Eval("officename") %> (<%# Eval("ofclvlnm") %>)</h3>
                                                        </div>
                                                        <div class="col-right">
                                                            <asp:LinkButton ID="ActivateOffice" Visible="false" OnClientClick="return ShowLoading()" CausesValidation="False" runat="server" class="btn btn-sm btn-success"
                                                                CommandName="activateoffice" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                title="Activate Office"> <i class="fa fa-check" ></i></asp:LinkButton>
                                                            <asp:LinkButton ID="DeactivateOffice" Visible="false" OnClientClick="return ShowLoading()" CausesValidation="False" runat="server" class="btn btn-sm btn-danger"
                                                                CommandName="deactivateoffice" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                title="Deactivate Office"> <i class="fa fa-times-circle" ></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnEditdOffice" OnClientClick="return ShowLoading()" CausesValidation="False" runat="server" class="btn btn-sm btn-warning"
                                                                CommandName="editoffice" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                title="Edit Office"> <i class="fa fa-edit" ></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnAddUnit" OnClientClick="return ShowLoading()" CausesValidation="False" runat="server" class="btn btn-sm btn-primary" Visible="true"
                                                                CommandName="addUnit" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                title="Add Unit"> <i class="fa fa-plus-circle" ></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnDeleteOffice" OnClientClick="return ShowLoading()" CausesValidation="False" runat="server" class="btn btn-sm btn-danger"
                                                                CommandName="deleteoffice" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                title="Delete Office"> <i class="fa fa-trash" ></i></asp:LinkButton>

                                                        </div>
                                                    </div>
                                                </div>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                </asp:GridView>
                            </div>
                            <div class="col-12 my-4" id="imgNorecord" visible="false" runat="server" style="margin-top: 10px;">
                                <div class="col-md-12 p-5" style="text-align: center; text-transform: uppercase;">
                                    <div class="col-md-12 busListBox pt-3" style="color: #e3e3e3; font-size: 24pt; font-weight: bold;">
                                        No Record Available<br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </asp:Panel>
                <asp:Panel ID="pnlUnitInstruction" runat="server" Visible="false">
                    <div class="card mb-3" style="min-height: 600px;">
                        <div class="card-header border-bottom">
                            <h3 class="mb-0">Instruction For Unit Creation</h3>
                        </div>
                        <div class="card-body p-0">
                            <div class="col-12">
                                <label class="form-control-label">1. Here you can add Unit Office along with its basic details.</label>
                                <br />
                                <label class="form-control-label">2. You can also update Unit office details.</label>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>

            <div class="col-lg-8">
                <asp:Panel ID="PnlRegistration" runat="server" Visible="true">

                    <div class="card mb-3" style="min-height: 600px;">

                        <div class="card-header border-bottom">
                            <div class="row m-0">
                                <div class="col-md-6 ">
                                    <h3 class="mb-1">
                                        <asp:Label ID="lbladdnewofficeHeading" runat="server" Visible="true" Text="Add New Office"></asp:Label>
                                        <asp:Label ID="lblUpdateofficeHeading" runat="server" Visible="false" Text="Update Office"></asp:Label>
                                    </h3>
                                </div>
                                <div class="col-md-6 ">
                                    <h4>
                                        <label class="form-control-label" style="font-size: small; color: red; float: right">* Fields are mandatory  </label>
                                    </h4>
                                </div>
                            </div>
                        </div>
                        <div class="card-body ">
                            <div class="clearfix"></div>
                            <div class="text-left">
                                <div class="col-lg-12">

                                    <p style="margin-bottom: 1px; font-weight: bold; font-size: medium; margin-bottom: 5px;">
                                        1. General Details
                                    </p>
                                    <div class="row m-0">
                                        <div class="col-md-4">
                                            <label class="form-control-label">Name <span style="color: Red;">*</span></label>

                                            <asp:TextBox runat="server" ID="tbofficename" MaxLength="100" autocomplete="off" ToolTip="Office Name" CssClass=" form-control form-control-sm"
                                                placeholder="Office Name"></asp:TextBox>

                                        </div>
                                        <div class="col-md-4">
                                            <label class="form-control-label">Address</label>

                                            <asp:TextBox runat="server" ID="tbaddress" MaxLength="100" ToolTip=" Office Address" autocomplete="off" CssClass=" form-control form-control-sm"
                                                placeholder="Office Address"></asp:TextBox>

                                        </div>
                                        <div class="col-md-4">
                                            <label class="form-control-label">State <span style="color: Red;">*</span></label>

                                            <asp:DropDownList runat="server" ID="ddlstate" OnClientClick="return ShowLoading()" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged" ToolTip=" Office State" CssClass=" form-control form-control-sm" AutoPostBack="true">
                                                <asp:ListItem Value="0">Select </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12 mt-2">
                                    <div class="row m-0">

                                        <div class="col-md-4">
                                            <label class="form-control-label">District <span style="color: Red;">*</span></label>

                                            <asp:DropDownList runat="server" ID="ddldistrict" OnClientClick="return ShowLoading()" ToolTip=" Office District" CssClass=" form-control form-control-sm" AutoPostBack="false">
                                                <asp:ListItem Value="0"> Select </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <label class="form-control-label">Level <span style="color: Red;">*</span></label>


                                            <asp:DropDownList runat="server" ID="ddllofficeLevel" OnClientClick="return ShowLoading()" ToolTip="Office Level" OnSelectedIndexChanged="ddllofficeLevel_SelectedIndexChanged" CssClass=" form-control form-control-sm" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <label class="form-control-label">Reporting Office <span style="color: Red;">*</span></label>


                                            <asp:DropDownList runat="server" ID="ddlreportingofc" OnClientClick="return ShowLoading()" ToolTip="Reporting Office" CssClass=" form-control form-control-sm" AutoPostBack="false">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12 mt-2">
                                    <div class="row m-0">
                                    </div>
                                </div>
                            </div>

                            <div class="text-left">
                                <div class="col-lg-12 mt-3">
                                    <p style="margin-bottom: 1px; font-weight: bold; font-size: medium; margin-bottom: 5px;">
                                        2. Contact Details
                                    </p>
                                    <div class="row m-0">
                                        <div class="col-md-4">
                                            <label class="form-control-label">Landline 1</label>


                                            <asp:TextBox runat="server" ID="tblandline1" MaxLength="15" ToolTip="Landline 1" autocomplete="off" CssClass=" form-control form-control-sm"
                                                placeholder="Max 15 Characters"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom"
                                                ValidChars="-" TargetControlID="tblandline1" />
                                        </div>
                                        <div class="col-md-4">
                                            <label class="form-control-label">Landline 2</label>


                                            <asp:TextBox runat="server" ID="tblandline2" MaxLength="15" ToolTip="Landline 2" autocomplete="off" CssClass=" form-control form-control-sm"
                                                placeholder="Max 15 Characters"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, Custom"
                                                ValidChars="-" TargetControlID="tblandline2" />
                                        </div>
                                        <div class="col-md-4">
                                            <label class="form-control-label">Mobile Number</label>

                                            <asp:TextBox runat="server" ID="tbmobileno" ToolTip="Mobile Number" MaxLength="10" autocomplete="off" CssClass=" form-control form-control-sm"
                                                placeholder="Office Mobile Number"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExten" runat="server" FilterType="Numbers, Custom"
                                                TargetControlID="tbmobileno" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12 mt-2">
                                    <div class="row m-0">

                                        <div class="col-md-4">

                                            <label class="form-control-label">Email</label>
                                            <asp:TextBox runat="server" ID="tbemail" MaxLength="100" ToolTip="Email" autocomplete="off" CssClass=" form-control form-control-sm"
                                                placeholder="Office Email"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, LowercaseLetters, Custom"
                                                ValidChars="-.@" TargetControlID="tbemail" />

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="text-left">
                                <div class="col-lg-12 mt-3">
                                    <p style="margin-bottom: 1px; font-weight: bold; font-size: medium; margin-bottom: 5px;">
                                        3. Other Details
                                    </p>
                                    <div class="row m-0">
                                        <div class="col-md-4">
                                            <label class="form-control-label">Dead KM<span style="color: Red;">*</span>(In case of Depot only)</label>


                                            <asp:TextBox runat="server" ID="tbDeadKm" MaxLength="15" ToolTip="Landline 1" autocomplete="off" CssClass=" form-control form-control-sm"
                                                placeholder="Depot Dead Km"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers"
                                                TargetControlID="tbDeadKm" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="form-row justify-content-center">
                                <div class="text-center" style="margin-top: 40px;">
                                    <asp:LinkButton ID="lbtnsaveoffice" runat="server" ToolTip="Save Office" OnClientClick="return ShowLoading()" OnClick="lbtnsaveoffice_Click" class="btn btn-success" Style="border-radius: 5px;"><i class="fa fa-save" ></i> Save</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnupdateoffice" OnClick="lbtnupdateoffice_Click" runat="server" ToolTip="Update Office" Visible="false" class="btn btn-success"
                                        Style="border-radius: 5px;" OnClientClick="return ShowLoading()"><i class="fa fa-save" ></i> Update</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnresetbutton" runat="server" ToolTip="Reset Office" OnClientClick="return ShowLoading()" OnClick="resetbutton_Click"
                                        CssClass="resetForm btn btn-warning"><i class="fa fa-undo" ></i> Reset</asp:LinkButton>

                                    <asp:LinkButton ID="lbtnCancel" Visible="false" ToolTip="Cancel Office" runat="server" OnClick="lbtnCancel_Click" class="btn mr-4 btn-danger" Style="border-radius: 4px;">
                                <i class="fa fa-times"></i> Cancel</asp:LinkButton>
                                </div>
                            </div>
                        </div>

                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlUnitCreation" runat="server" Visible="false">

                    <div class="card mb-3" style="min-height: 600px;">
                        <div class="card-header border-bottom">
                            <div class="row m-0">
                                <div class="col-md-6 ">
                                    <h3 class="mb-1">
                                        <asp:Label ID="lblAddUnitOffice" runat="server" Visible="true" Text="Add Unit Office"></asp:Label>
                                    </h3>
                                </div>
                                <div class="col-md-6 ">
                                    <h4>
                                        <asp:LinkButton ID="lbtnGoback" Font-Size="Small" OnClick="lbtnGoback_Click" runat="server" ToolTip="Go Back To Office Creation" OnClientClick="return ShowLoading()" class="btn btn-danger float-right"><i class="fa fa-arrow-left" ></i> Go Back To Office Creation</asp:LinkButton>

                                        <label class="form-control-label mr-2" style="font-size: small; color: red; float: right">* Fields are mandatory  </label>


                                    </h4>
                                </div>
                            </div>
                        </div>
                        <div class="card-body ">
                            <div class="row text-left">
                                <div class="col-lg-12 mt-2">
                                    <p style="margin-bottom: 1px; font-weight: bold; font-size: medium; margin-bottom: 5px;">
                                        1. General Details
                                    </p>
                                </div>
                                <div class="col-lg-12 mt-2">
                                    <div class="row m-0">
                                        <div class="col-md-4">
                                            <label class="form-control-label">Unit Type<span style="color: Red;">*</span> </label>
                                            <asp:DropDownList runat="server" ID="ddlUnitType" OnSelectedIndexChanged="ddlUnitType_SelectedIndexChanged" OnClientClick="return ShowLoading()" ToolTip="Unit Type" CssClass="form-control form-control-sm" AutoPostBack="True">
                                                <asp:ListItem Value="">Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <label class="form-control-label">Name <span style="color: Red;">*</span></label>
                                            <asp:TextBox runat="server" ID="tbUnitofficename" MaxLength="100" autocomplete="off" ToolTip="Name" CssClass="form-control form-control-sm"
                                                placeholder="Office Name"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Numbers, LowercaseLetters, Custom"
                                                TargetControlID="tbofficename" />
                                        </div>
                                        <div class="col-md-4">
                                            <label class="form-control-label">Address </label>
                                            <asp:TextBox runat="server" ID="tbUnitaddress" MaxLength="100" autocomplete="off" ToolTip="Address" CssClass="form-control form-control-sm"
                                                placeholder="Office Address"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12 mt-2">
                                    <div class="row m-0">
                                        <div class="col-md-4">
                                            <label class="form-control-label">State<span style="color: Red;">*</span> </label>
                                            <asp:DropDownList runat="server" ID="ddlUnitstate" OnClientClick="return ShowLoading()" ToolTip="State" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlUnitstate_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <label class="form-control-label">District <span style="color: Red;">*</span> </label>
                                            <asp:DropDownList runat="server" ID="ddlUnitdistrict" OnClientClick="return ShowLoading()" ToolTip="District" CssClass="form-control form-control-sm">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4" id="dvstation" runat="server" visible="false">
                                            <label class="form-control-label">Bind Bus Station <span style="color: Red;">*</span> </label>
                                            <asp:DropDownList runat="server" ID="ddlbusStation" OnClientClick="return ShowLoading()" ToolTip="District" CssClass="form-control form-control-sm">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row mt-2">
                                        <div class="col-md-4" id="dvcounterlogintype" runat="server" visible="false">
                                            <label class="form-control-label">Counter Login Type <span style="color: Red;">*</span> </label>

                                            <asp:DropDownList runat="server" ID="ddllogintype" OnClientClick="return ShowLoading()" CssClass="form-control">
                                                <asp:ListItem Value="W" Text="Web"></asp:ListItem>
                                                <asp:ListItem Value="E" Text="ETM"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4" id="dvcounterSpecialbooking" runat="server" visible="false">
                                            <label class="form-control-label">Special Booking Allowed <span style="color: Red;">*</span> </label>

                                            <asp:DropDownList runat="server" ID="ddlSpecialBooking" OnClientClick="return ShowLoading()" CssClass="form-control">
                                                
                                                <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                                <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                                
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <asp:Panel ID="pnlStoreTypes" runat="server" Visible="false">
                                    <div class="col-lg-12 mt-2">
                                        <p style="margin-bottom: 1px; font-weight: bold; font-size: medium; margin-bottom: 5px;">
                                            Store Types<span style="color: Red;">*</span>
                                        </p>
                                        <div class="row m-0">
                                            <div class="col-md-12">
                                                <asp:CheckBoxList ID="cbl_storeTypes" runat="server" CssClass="table border-0 checkboxlist" RepeatDirection="Horizontal">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>



                                <div class="col-lg-12 mt-2">
                                    <p style="margin-bottom: 1px; font-weight: bold; font-size: medium; margin-bottom: 5px;">
                                        2.Contact Details
                                    </p>
                                    <div class="row m-0">
                                        <div class="col-md-4">
                                            <label class="form-control-label">Landline 1 </label>
                                            <asp:TextBox runat="server" ID="tbUnitlandline1" MaxLength="15" autocomplete="off" ToolTip="Landline1" CssClass="form-control form-control-sm"
                                                placeholder="Max 15 Characters"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers, Custom"
                                                ValidChars="-" TargetControlID="tbUnitlandline1" />
                                        </div>
                                        <div class="col-md-4">
                                            <label class="form-control-label">Landline 2</label>
                                            <asp:TextBox runat="server" ID="tbUnitlandline2" MaxLength="15" autocomplete="off" ToolTip="Landline2" CssClass="form-control form-control-sm"
                                                placeholder="Max 15 Characters"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers, Custom"
                                                ValidChars="-" TargetControlID="tbUnitlandline2" />
                                        </div>
                                        <div class="col-md-4">
                                            <label class="form-control-label">Mobile Number</label>
                                            <asp:TextBox runat="server" ID="tbUnitmobileno" MaxLength="10" autocomplete="off" ToolTip="Mobile Number" CssClass="form-control form-control-sm"
                                                placeholder="Office Mobile Number"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers, Custom"
                                                TargetControlID="tbUnitmobileno" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12 mt-2">
                                    <div class="row m-0">
                                        <div class="col-md-4">
                                            <label class="form-control-label">Email </label>
                                            <asp:TextBox runat="server" ID="tbUnitemail" MaxLength="100" autocomplete="off" ToolTip="Email" CssClass="form-control form-control-sm"
                                                placeholder="Office Email"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers, LowercaseLetters, Custom"
                                                ValidChars=".@" TargetControlID="tbUnitemail" />
                                        </div>
                                        
                                    </div>
                                </div>
                            </div>
                            <div class="form-row justify-content-center mt-2">
                                <div class="text-center">
                                    <asp:LinkButton ID="lbtnUnitSaveOffice" OnClick="lbtnUnitSaveOffice_Click" runat="server" ToolTip="Save Unit Office" OnClientClick="return ShowLoading()" class="btn btn-success" Style="border-radius: 5px;"><i class="fa fa-save" ></i> Save</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnUnitUpdateOffice" OnClick="lbtnUnitUpdateOffice_Click" runat="server" ToolTip="Update Unit Office" Visible="false" class="btn btn-success"
                                        Style="border-radius: 5px;" OnClientClick="return ShowLoading()"><i class="fa fa-edit" ></i> Update</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnUnitReset" ToolTip="Reset Unit Office" OnClick="lbtnUnitReset_Click" runat="server" OnClientClick="return ShowLoading()" CssClass="resetForm btn btn-warning"><i class="fa fa-undo" ></i> Reset</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnUnitBack" runat="server" ToolTip="Close Unit Office" OnClientClick="return ShowLoading()" OnClick="lbtnUnitBack_Click" CssClass="resetForm btn btn-danger"><i class="fa fa-times" ></i> Cancel</asp:LinkButton>

                                </div>
                            </div>
                        </div>

                        <div class="card-header border-bottom  mt-5">
                            <div class="row m-0">
                                <div class="col-md-4 ">
                                    <h3 class="mb-1">


                                        <asp:Label ID="Label2" runat="server" Visible="true" Text="List Of Unit Offices"></asp:Label>

                                    </h3>
                                </div>
                                <div class="col-md-4">
                                    <h4>
                                        <label class="form-control-label" style="margin-top: 7px; float: right">Unit Type</label>

                                    </h4>

                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList runat="server" ID="ddlSUnitOficeType" OnSelectedIndexChanged="ddlSUnitOficeType_SelectedIndexChanged" CssClass="form-control form-control-sm" AutoPostBack="true" Style="width: 80%;">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">

                            <div class="row m-0">
                                <div class="col-lg-12 mt-2 text-left">
                                    <asp:GridView ID="gvUnitOffices" runat="server" CssClass="table table-hover table-striped"
                                        OnRowCommand="gvUnitOffices_RowCommand" OnPageIndexChanging="gvUnitOffices_PageIndexChanging"
                                        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnRowDataBound="gvUnitOffices_RowDataBound"
                                        DataKeyNames="office_id, office_name, address, stateid, districtid, officelevelid, landline1, landline2, mobile, email, active,
                                         reportingofficeid, actiontypeid, unitid, mainofclvl, unitname, levelname, district,delete_yn,station_id,
                                        store_type_id,store_type_status,specialbooking,logintype">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Office Unit" HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("UNITNAME") %>'></asp:Label>
                                                    <asp:Label ID="lblact" runat="server" Visible="false" Text='<%# Eval("active") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub Office Name" HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUnit2" runat="server" Text='<%# Eval("office_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="District" HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("DISTRICT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="75px" HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnactivateUnitOffice" OnClientClick="return ShowLoading()" CausesValidation="False" runat="server" class="btn btn-sm btn-success" CommandName="activateUnitOffice" Style="font-size: 10pt;" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' title="Activate Office"> <i class="fa fa-check" ></i></asp:LinkButton>

                                                    <asp:LinkButton ID="lbtndeactivateUnitOffice" OnClientClick="return ShowLoading()" CausesValidation="False" runat="server" class="btn btn-sm btn-warning" CommandName="deactivateUnitOffice" Style="font-size: 10pt;" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' title="Deactivate Office"> <i class="fa fa-times-circle" ></i></asp:LinkButton>

                                                    <asp:LinkButton ID="lbtnEditdOffice" OnClientClick="return ShowLoading()" CausesValidation="False" runat="server" class="btn btn-sm btn-primary" CommandName="updateOffice" Style="font-size: 10pt;" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' title="Edit Office"> <i class="fa fa-edit" ></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDeleteOffice" OnClientClick="return ShowLoading()" CausesValidation="False" runat="server" class="btn btn-sm btn-danger" CommandName="deleteUnitOffice" Style="font-size: 10pt;" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' title="Delete Office"> <i class="fa fa-trash" ></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Panel ID="pnlNoUnitOffice" runat="server" Width="100%" Visible="true">
                                        <div class="col-md-12 p-3" style="text-align: center; text-transform: uppercase;">
                                            <div class="col-md-12 busListBox pt-3" style="color: #e3e3e3; font-size: 18pt; font-weight: bold;">
                                                No Record Available<br />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>

                        </div>
                    </div>
                </asp:Panel>
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
                    <h4 class="card-title text-left mb-0">Please Check
                    </h4>
                </div>
                <div class="card-body text-left pt-2" style="min-height: 100px;">
                    <asp:Label ID="lblerrormsg" runat="server" Text="Please Check entered values."></asp:Label>
                    <div style="width: 100%; margin-top: 20px; text-align: right;">
                        <asp:LinkButton ID="lbtnclose1" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
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

    <div class="modal" id="notice">
        <div class="modal-dialog">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Please Note</h4>
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                </div>
                <!-- Modal body -->
                <div class="modal-body">
                    <div class="card-body text-left" style="font-size: 10pt; padding: 10px;">
                        <b>Here You can perform following actions </b>
                        <br />
                        1. Add New Office<br />
                        2. Edit existing Office<br />
                        3. Update Office<br />
                        4. Create sub office of office
                        <hr />
                        <b>Please remember</b>
                        <br />
                        1. You can add Offices of all Level<br />
                        2. First you have to create Office of Higher level.<br />
                        3. All fields <span style="color: Red;">*</span> are mandatory.
                    </div>
                </div>
                <!-- Modal footer -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

