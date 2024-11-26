<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/PortalAdmmaster.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="PAOrganizationRegistration.aspx.cs" Inherits="Auth_PAOrganizationRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        function uploadGovtLogo(fileUpload) {
            if ($('#fuGovtLogo').value != '') {
                document.getElementById("<%=btnfuGovtLogo.ClientID %>").click();
            }
        }
        function uploadDeprtLogo(fileUpload) {
            if ($('#fuDepartmentLogo').value != '') {
                document.getElementById("<%=btnfuDepartmentLogo.ClientID %>").click();
            }
        }
        function uploadFavLogo(fileUpload) {
            if ($('#fuFavIcon').value != '') {
                document.getElementById("<%=btnFavIcon.ClientID %>").click();
            }
        }
    </script>
    <style>
        .ChkBoxClass input {
            width: 25px;
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid pt-2">
        <div class="row">
            <div class="col-xl-4 order-xl-1">
                <asp:GridView ID="gvOrg" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
                    DataKeyNames="orcode,ortypecode,ortype,orstcode,orstname,orname,orabbr,orappname,orofcaddress,orofcstate,orofcaddstname,orofcdistrict,orofcdistrictname,orofcpin,n1name,n1desig,n1mobileno,no1landlineno,n1email,n2name,n2desig,n2mobileno,n2landlineno,n2email,orregdate,orpsbllivedate,psblurl,ofcorderdate,ofcorderno,orlogo,orgovtlogo,orfavicon" OnRowCommand="gvOrg_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="card card-stats">
                                    <!-- Card body -->
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col">
                                                <span class="h2 font-weight-bold mb-0"><%# Eval("orappname") %> </span>
                                                <h5 class="card-title text-uppercase text-muted mb-0"><%# Eval("orname") %></h5>
                                            </div>
                                            <div class="col-auto">
                                                <img src='../<%#Eval("orlogo") %>' style="height: 50px;" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-8 pt-3">
                                                <asp:LinkButton ID="lbtnOrgView" runat="server" CssClass="btn btn-icon btn-primary btn-sm"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="VIEWORG" data-toggle="tooltip" data-placement="bottom" title="View Organization Detail">
                                                    <i class="fa fa-eye"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnOrgMapService" runat="server" CssClass="btn btn-icon btn-warning btn-sm"
                                                    CommandArgument='<%# Container.DataItemIndex %>' CommandName="MAPSERVICE" data-toggle="tooltip" data-placement="bottom" title="Map Organization Services">
                                                    <i class="fa fa-plus"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnOrgUpdate" runat="server" CssClass="btn btn-icon btn-info btn-sm" Visible="false"
                                                    CommandArgument='<%# Container.DataItemIndex %>' CommandName="UPDATEORG" data-toggle="tooltip" data-placement="bottom" title="Update Organization Detail">
                                                    <i class="fa fa-edit"></i>
                                                </asp:LinkButton>
                                            </div>
                                            <%--<div class="col-lg-4">
                                                <p class="mt-2 mb-0 text-sm">
                                                    <span class="text-success">Registered On </span>
                                                    <span class="text-nowrap"><%# Eval("orregdate") %></span>
                                                </p>
                                            </div>--%>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <asp:Panel ID="pnlNoOrg" runat="server">
                    <div class="card card-stats">
                        <!-- Card body -->
                        <div class="card-body">
                            <div class="row">

                                <div class="col-lg-12 text-center">
                                    <i class="fa fa-bus fa-5x"></i>
                                </div>
                                <div class="col-lg-12 text-center mt-4">
                                    <span class="h2 font-weight-bold mb-0">Start Organization Registration </span>
                                    <h5 class="card-title text-uppercase text-muted mb-0">No Organization has been registered yet</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

            </div>
            <div class="col-xl-8 order-xl-2">
                <asp:Panel ID="pnlOrgReg" runat="server" Visible="true">
                    <div class="card">
                        <div class="card-header">
                            <div class="row align-items-center">
                                <div class="col-8">
                                    <h3 class="mb-0">Organization Registration</h3>
                                </div>
                                <div class="col-4 text-right">
                                    <asp:LinkButton ID="lbtnHelp" runat="server" OnClick="lbtnHelp_Click">
                                    <span class="btn-inner--icon"><i class="fa fa-info-circle"></i></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <h6 class="heading-small text-muted mb-2">Organization information</h6>
                            <div class="pl-lg-4">
                                <div class="row">
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <label class="form-control-label" for="input-last-name">Type <span class="text-danger text-lg-left">*</span></label>
                                            <asp:DropDownList ID="ddlOrgType" runat="server" CssClass="form-control form-control-sm">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <label class="form-control-label" for="input-first-name">State <span class="text-danger text-lg-left">*</span></label>
                                            <asp:DropDownList ID="ddlOrgState" runat="server" CssClass="form-control form-control-sm">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <label class="form-control-label" for="input-username">Name <span class="text-danger text-lg-left">*</span></label>
                                            <asp:TextBox ID="tbOrgName" runat="server" CssClass="form-control form-control-sm text-uppercase" placeholder="Name" MaxLength="100" AutoComplete="off"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-lg-2">
                                        <div class="form-group">
                                            <label class="form-control-label" for="input-email">Abbr <span class="text-danger text-lg-left">*</span></label>
                                            <asp:TextBox ID="tbAbbr" runat="server" CssClass="form-control form-control-sm text-uppercase" placeholder="Abbreviation" MaxLength="8" AutoComplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <label class="form-control-label" for="input-username">App Name <span class="text-danger text-lg-left">*</span></label>
                                            <asp:TextBox ID="tbAppName" runat="server" CssClass="form-control form-control-sm text-uppercase" placeholder="App Name" MaxLength="25" AutoComplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <h6 class="heading-small text-muted mb-2">Office Address</h6>
                            <div class="pl-lg-4">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="form-control-label" for="input-address">Address <span class="text-danger text-lg-left">*</span></label>
                                            <asp:TextBox ID="tbOrgAddr" runat="server" CssClass="form-control form-control-sm text-uppercase" placeholder="Address" MaxLength="100" AutoComplete="off"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <label class="form-control-label" for="input-first-name">State <span class="text-danger text-lg-left">*</span></label>
                                            <asp:DropDownList ID="ddlOrgAddrState" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlOrgAddrState_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <label class="form-control-label" for="input-first-name">District <span class="text-danger text-lg-left">*</span></label>
                                            <asp:DropDownList ID="ddlOrgAddrDistrict" runat="server" CssClass="form-control form-control-sm">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-2">
                                        <div class="form-group">
                                            <label class="form-control-label" for="input-city">PIN <span class="text-danger text-lg-left">*</span></label>
                                            <asp:TextBox ID="tbOrgPin" runat="server" CssClass="form-control form-control-sm text-uppercase" placeholder="PIN" MaxLength="6" AutoComplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <h6 class="heading-small text-muted mb-2">First Nodal Officer</h6>
                            <div class="pl-lg-4">
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="form-control-label" for="input-address">Name <span class="text-danger text-lg-left">*</span></label>
                                            <asp:TextBox ID="tbNodal1Name" runat="server" CssClass="form-control form-control-sm text-uppercase" placeholder="Name" MaxLength="100" AutoComplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="form-control-label" for="input-address">Designation <span class="text-danger text-lg-left">*</span></label>
                                            <asp:TextBox ID="tbNodal1Desig" runat="server" CssClass="form-control form-control-sm text-uppercase" placeholder="Designation" MaxLength="100" AutoComplete="off"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-lg-2">
                                        <div class="form-group">
                                            <label class="form-control-label" for="input-city">Mobile No. <span class="text-danger text-lg-left">*</span></label>
                                            <asp:TextBox ID="tbNodal1MobileNo" runat="server" CssClass="form-control form-control-sm" placeholder="Mobile No." MaxLength="10" AutoComplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-2">
                                        <div class="form-group">
                                            <label class="form-control-label" for="input-city">Landline No.</label>
                                            <asp:TextBox ID="tbNodal1LandlineNo" runat="server" CssClass="form-control form-control-sm" placeholder="Landline No." MaxLength="15" AutoComplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <label class="form-control-label" for="input-city">Email Id <span class="text-danger text-lg-left">*</span></label>
                                            <asp:TextBox ID="tbNodal1EmailId" runat="server" CssClass="form-control form-control-sm text-uppercase" placeholder="Email Id" MaxLength="50" AutoComplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <h6 class="heading-small text-muted mb-2">Second Nodal Officer</h6>
                            <div class="pl-lg-4">
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="form-control-label" for="input-address">Name</label>
                                            <asp:TextBox ID="tbNodal2Name" runat="server" CssClass="form-control form-control-sm text-uppercase" placeholder="Name"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="form-control-label" for="input-address">Designation</label>
                                            <asp:TextBox ID="tbNodal2Desig" runat="server" CssClass="form-control form-control-sm text-uppercase" placeholder="Designation"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-lg-2">
                                        <div class="form-group">
                                            <label class="form-control-label" for="input-city">Mobile No.</label>
                                            <asp:TextBox ID="tbNodal2MobileNo" runat="server" CssClass="form-control form-control-sm" placeholder="Mobile No."></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-2">
                                        <div class="form-group">
                                            <label class="form-control-label" for="input-city">Telephone No.</label>
                                            <asp:TextBox ID="tbNodal2LandlineNo" runat="server" CssClass="form-control form-control-sm" placeholder="Telephone No."></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <label class="form-control-label" for="input-city">Email Id</label>
                                            <asp:TextBox ID="tbNodal2EmailId" runat="server" CssClass="form-control form-control-sm text-uppercase" placeholder="Email Id"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Logos -->
                            <h6 class="heading-small text-muted mb-2">Important Logo</h6>
                            <div class="pl-lg-4">
                                <div class="row">
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <label class="form-control-label" for="input-city">Govt Logo <span class="text-danger text-lg-left">*</span></label>
                                            <asp:FileUpload ID="fuGovtLogo" runat="server" CssClass="form-control" onchange="uploadGovtLogo(this);" accept="image/*" />
                                            <asp:Button ID="btnfuGovtLogo" runat="server" OnClick="btnfuGovtLogo_Click" CausesValidation="False" Style="display: none"
                                                Text="Upload Image" />
                                        </div>
                                        <asp:Image ID="imgGovtLogo" runat="server" Visible="false" Style="width: 100px; height: 100px; border: 1px solid;" />
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <label class="form-control-label" for="input-city">Department Logo <span class="text-danger text-lg-left">*</span></label>
                                            <asp:FileUpload ID="fuDepartmentLogo" runat="server" CssClass="form-control" onchange="uploadDeprtLogo(this);" accept="image/*" />
                                            <asp:Button ID="btnfuDepartmentLogo" runat="server" OnClick="btnfuDepartmentLogo_Click" CausesValidation="False" Style="display: none"
                                                Text="Upload Image" />
                                        </div>
                                        <asp:Image ID="imgDepartmentLogo" runat="server" Visible="false" Style="width: 100px; height: 100px; border: 1px solid;" />
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <label class="form-control-label" for="input-city">Fav Icon <span class="text-danger text-lg-left">*</span></label>
                                            <asp:FileUpload ID="fuFavIcon" runat="server" CssClass="form-control" onchange="uploadFavLogo(this);" accept="image/*" />
                                            <asp:Button ID="btnFavIcon" runat="server" OnClick="btnFavIcon_Click" CausesValidation="False" Style="display: none"
                                                Text="Upload Image" />
                                        </div>
                                        <asp:Image ID="imgFavIcon" runat="server" Visible="false" Style="width: 100px; height: 100px; border: 1px solid;" />
                                    </div>
                                </div>
                            </div>

                            <h6 class="heading-small text-muted mb-2">Other</h6>
                            <div class="pl-lg-4">
                                <div class="row">
                                    <div class="col-lg-3">
                                        <label class="form-control-label" for="input-last-name">Office Order Date <span class="text-danger text-lg-left">*</span></label>
                                        <div class="input-group ">
                                            <asp:TextBox ID="tbOrderDate" runat="server" CssClass="form-control form-control-sm" placeholder="Date" MaxLength="10" AutoComplete="off" aria-label="Date" aria-describedby="button-tbOrderDate"></asp:TextBox>
                                            <div class="input-group-append">
                                                <button class="btn btn-outline-primary btn-sm" type="button" id="button-tbOrderDate" style="border-radius: 0px 3px 3px 0px;"><i class="fa fa-calendar"></i></button>
                                            </div>
                                        </div>
                                        <cc1:CalendarExtender ID="ceOrderDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="button-tbOrderDate" PopupPosition="TopRight"
                                            TargetControlID="tbOrderDate"></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <label class="form-control-label" for="input-first-name">Office Order No.</label>
                                            <asp:TextBox ID="tbOrderNo" runat="server" CssClass="form-control form-control-sm text-uppercase" placeholder="Order No." MaxLength="20" AutoComplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <label class="form-control-label" for="input-last-name">Possible Go Live Date <span class="text-danger text-lg-left">*</span></label>
                                        <div class="input-group ">
                                            <asp:TextBox ID="tbGoLiveDate" runat="server" CssClass="form-control form-control-sm" placeholder="Date" MaxLength="10" AutoComplete="off" aria-label="Date" aria-describedby="button-tbGoLiveDate"></asp:TextBox>
                                            <div class="input-group-append">
                                                <button class="btn btn-outline-primary btn-sm" type="button" id="button-tbGoLiveDate" style="border-radius: 0px 3px 3px 0px;"><i class="fa fa-calendar"></i></button>
                                            </div>
                                        </div>
                                        <cc1:CalendarExtender ID="ceGoLiveDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="button-tbGoLiveDate" PopupPosition="TopRight"
                                            TargetControlID="tbGoLiveDate"></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <label class="form-control-label" for="input-first-name">Possible Go Live URL</label>
                                            <asp:TextBox ID="tbGoLiveURL" runat="server" CssClass="form-control form-control-sm text-uppercase" placeholder="Go Live URL" MaxLength="100" AutoComplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="pl-lg-4">
                                <div class="row">
                                    <div class="col-lg-12 text-right">
                                        <asp:LinkButton ID="lbtnSave" runat="server" CssClass="btn btn-icon btn-success" OnClick="lbtnSave_Click">
                                        <span class="btn-inner--icon"><i class="ni ni-check-bold"></i></span>
                                            <span class="btn-inner--text">Save</span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-icon btn-warning" OnClick="lbtnReset_Click">
                                         <span class="btn-inner--icon"><i class="ni ni-fat-remove"></i></span>
                                            <span class="btn-inner--text">Reset</span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlOrgServiceMap" runat="server" Visible="false">
                    <div class="card">
                        <div class="card-header">
                            <div class="row align-items-center">
                                <div class="col-12">
                                    <h3 class="mb-0">Map Services with
                                        <asp:Label ID="lblHeaderOrgName" runat="server" Text="Organization"></asp:Label>
                                        <asp:HiddenField ID="hfOrgCodeForSerMap" runat="server" Value="0" />
                                    </h3>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <asp:Panel ID="pnlAllService" runat="server">

                                <div class="pl-lg-4">
                                    <div class="row">
                                        <asp:ListView ID="lvAllServices" runat="server">
                                            <ItemTemplate>

                                                <div class="col-lg-4">
                                                    <div class="card card-stats shadow-lg">
                                                        <div class="card-body px-4 py-2">
                                                            <div class="row">
                                                                <div class="col">
                                                                    <asp:Label ID="lblServiceCatCode" runat="server" Visible="false" Text='<%# Eval("orsercatcode") %>'></asp:Label>
                                                                    <asp:Label ID="lblServiceCode" runat="server" Visible="false" Text='<%# Eval("orsercode") %>'></asp:Label>
                                                                    <span class="h4 font-weight-bold mb-0"><%# Eval("orsername") %> </span>
                                                                    <h5 class="text-uppercase text-muted mb-0 text-xs"><%# Eval("orsercatname") %></h5>
                                                                </div>
                                                                <div class="col-auto">
                                                                    <asp:CheckBox ID="cbService" CssClass="ChkBoxClass" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                                <div class="pl-lg-4">
                                    <div class="row">
                                        <div class="col-lg-12 text-right">
                                            <asp:LinkButton ID="lbtnSaveMapService" runat="server" CssClass="btn btn-icon btn-success" OnClick="lbtnSaveMapService_Click">
                                        <span class="btn-inner--icon"><i class="ni ni-check-bold"></i></span>
                                            <span class="btn-inner--text">Save</span>
                                            </asp:LinkButton>

                                            <asp:LinkButton ID="lbtnCancelMapService" runat="server" CssClass="btn btn-icon btn-danger" OnClick="lbtnCancelMapService_Click">
                                         <span class="btn-inner--icon"><i class="ni ni-fat-remove"></i></span>
                                            <span class="btn-inner--text">Cancel</span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlNoAllService" runat="server">
                                <div class="card card-stats">
                                    <!-- Card body -->
                                    <div class="card-body">
                                        <div class="row">

                                            <div class="col-lg-12 text-center">
                                                <i class="fa fa-bookmark fa-5x"></i>
                                            </div>
                                            <div class="col-lg-12 text-center mt-4">
                                                <span class="h2 font-weight-bold mb-0">Services are not available for mapping</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlOrgDetailView" runat="server" Visible="false">
                    <div class="card">
                        <div class="card-header">
                            <div class="row align-items-center">
                                <div class="col-12">
                                    <h3 class="mb-0">Organization Details
                                    </h3>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <asp:Panel ID="pnlOrgDetail" runat="server" Visible="true">

                                <div class="card card-stats shadow-lg">
                                    <div class="card-body px-4 py-2">
                                        <div class="row">
                                            <div class="col-lg-1">
                                                <asp:Image ID="imgODGovtLogo" runat="server" Height="50px" />
                                            </div>
                                            <div class="col-lg-1">
                                                <asp:Image ID="imgODLogo" runat="server" Height="50px" />
                                            </div>
                                            <div class="col-lg-8">
                                                <span class="h2 font-weight-bold mb-0">
                                                    <asp:Label ID="lblODAppName" runat="server" Text="NA"></asp:Label>
                                                </span>
                                                <h5 class="card-title text-uppercase text-muted mb-0">
                                                    <asp:Label ID="lblODDepartment" runat="server" Text="NA"></asp:Label></h5>
                                            </div>
                                            <div class="col-lg-2 text-right">
                                                <span class="h2 font-weight-bold mb-0">
                                                    <asp:Label ID="lblODRegDate" runat="server" Text="NA"></asp:Label>
                                                </span>
                                                <h5 class="card-title text-uppercase text-muted mb-0">
                                                    <asp:Label ID="Label2" runat="server" Text="Registration On"></asp:Label></h5>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                                <div class="card card-stats shadow-lg">
                                    <div class="card-body px-4 py-2">
                                        Office Address
                                        <h5 class="card-title text-uppercase text-muted mb-0">
                                            <asp:Label ID="lblODAddress" runat="server" Text="UTC Online"></asp:Label></h5>
                                    </div>
                                </div>
                                <div class="card card-stats shadow-lg">
                                    <div class="card-body px-4 py-2">
                                        First Nodal Officer
                                        <div class="row">
                                            <div class="col-lg-6 pl-3">
                                                <h5 class="card-title text-uppercase text-muted mb-0">
                                                    <asp:Label ID="lblODNO1NameDesig" runat="server" Text="NA"></asp:Label></h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5 class="card-title text-uppercase text-muted mb-0">
                                                    <i class="fa fa-mobile-alt"></i>
                                                    <asp:Label ID="lblODNO1MobileLandline" runat="server" Text="NA"></asp:Label></h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5 class="card-title text-uppercase text-muted mb-0">
                                                    <i class="fa fa-envelope-square"></i>
                                                    <asp:Label ID="lblODNO1Email" runat="server" Text="NA"></asp:Label></h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card card-stats shadow-lg">
                                    <div class="card-body px-4 py-2">
                                        Second Nodal Officer
                                        <div class="row">
                                            <div class="col-lg-6 pl-3">
                                                <h5 class="card-title text-uppercase text-muted mb-0">
                                                    <asp:Label ID="lblODNO2NameDesig" runat="server" Text="NA"></asp:Label></h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5 class="card-title text-uppercase text-muted mb-0">
                                                    <i class="fa fa-mobile-alt"></i>
                                                    <asp:Label ID="lblODNO2MobileLandline" runat="server" Text="NA"></asp:Label></h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5 class="card-title text-uppercase text-muted mb-0">
                                                    <i class="fa fa-envelope-square"></i>
                                                    <asp:Label ID="lblODNO2Email" runat="server" Text="NA"></asp:Label></h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="card card-stats shadow-lg">
                                    <div class="card-body px-4 py-2">
                                        <div class="row">
                                            <div class="col-lg-3 pl-3">
                                                Order Date
                                            <h5 class="card-title text-uppercase text-muted mb-0">
                                                <asp:Label ID="lblODOderDate" runat="server" Text="NA"></asp:Label></h5>
                                            </div>
                                            <div class="col-lg-3">
                                                Order No
                                            <h5 class="card-title text-uppercase text-muted mb-0">
                                                <asp:Label ID="lblODOderNo" runat="server" Text="NA"></asp:Label></h5>
                                            </div>
                                            <div class="col-lg-3">
                                                Possible Live Date
                                            <h5 class="card-title text-uppercase text-muted mb-0">
                                                <asp:Label ID="lblODPossibleLiveDate" runat="server" Text="NA"></asp:Label></h5>
                                            </div>
                                            <div class="col-lg-3">
                                                Possible Live URL
                                            <h5 class="card-title text-uppercase text-muted mb-0">
                                                <asp:Label ID="lblODPossibleLiveURL" runat="server" Text="NA"></asp:Label></h5>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-12 pl-4 pr-4">
                                        <h3 class="card-title text-uppercase text-muted mb-3 mt-3">
                                            <asp:Label ID="lblODServiceListHeader" runat="server" Text="Mapped Services"></asp:Label></h3>
                                        <div class="row">
                                            <asp:ListView ID="lvODServices" runat="server">
                                                <ItemTemplate>
                                                    <div class="col-lg-4">
                                                        <div class="card card-stats shadow-lg">
                                                            <div class="card-body px-4 py-2">
                                                                <span class="h4 font-weight-bold mb-0"><%# Eval("orsername") %> </span>
                                                                <h5 class="text-uppercase text-muted mb-0 text-xs"><%# Eval("orsercatname") %></h5>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                    <div class="col-lg-12 pl-3">
                                        <h3 class="card-title text-uppercase text-muted mb-3 mt-3">
                                            <asp:Label ID="lblODNoService" runat="server" Text="NA"></asp:Label></h3>
                                    </div>
                                </div>


                            </asp:Panel>

                            <asp:Panel ID="pnlNoOrgDetail" runat="server" Visible="false">
                                <div class="card card-stats">
                                    <!-- Card body -->
                                    <div class="card-body">
                                        <div class="row">

                                            <div class="col-lg-12 text-center">
                                                <i class="fa fa-times fa-5x"></i>
                                            </div>
                                            <div class="col-lg-12 text-center mt-4">
                                                <span class="h2 font-weight-bold mb-0">Organization details are not available.</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="pl-lg-4">
                                <div class="row">
                                    <div class="col-lg-12 text-right">
                                        <asp:LinkButton ID="lbtnCloseOrgDetail" runat="server" CssClass="btn btn-icon btn-danger" OnClick="lbtnCloseOrgDetail_Click">
                                         <span class="btn-inner--icon"><i class="ni ni-fat-remove"></i></span>
                                            <span class="btn-inner--text">Close</span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpConfirm" runat="server" CancelControlID="btnClosempConfirm"
                TargetControlID="btnOpenmpConfirm" PopupControlID="pnlConfirm" BackgroundCssClass="ModalPopupBackground">
            </cc1:ModalPopupExtender>

            <asp:Panel ID="pnlConfirm" Style="display: none;" runat="server">

                <div class="modal-dialog modal- modal-dialog-centered modal-" role="document">
                    <div class="modal-content">

                        <div class="modal-header">
                            <h6 class="modal-title" id="modal-title-default">Confirmation</h6>
                        </div>

                        <div class="modal-body">
                            <p>
                                <asp:Label ID="lblConfirmMsg" runat="server" Text="Do you want to save records ?"></asp:Label>
                            </p>
                        </div>

                        <div class="modal-footer">
                            <asp:LinkButton ID="lbtnSavempConfirm" runat="server" CssClass="btn btn-primary" OnClick="lbtnSavempConfirm_Click">Save
                            </asp:LinkButton>
                            <button type="button" id="btnClosempConfirm" class="btn btn-link  ml-auto" data-dismiss="modal">Close</button>
                        </div>

                    </div>
                </div>

            </asp:Panel>

            <div style="visibility: hidden;">
                <asp:Button ID="btnOpenmpConfirm" runat="server" />
            </div>

        </div>
    </div>
</asp:Content>

