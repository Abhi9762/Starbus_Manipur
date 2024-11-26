<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="SysAdmRouteMgmt.aspx.cs" Inherits="Auth_SysAdmRouteMgmt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid pt-2">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="row">
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h4 class="mb-1">
                                            <asp:Label ID="lblSummary" runat="server" CssClass="text-capitalize">Summary as on Date 01/12/2021 04:00pm</asp:Label></h4>
                                        <div class="row m-0">
                                            <div class="col-6 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Total Route:&nbsp;
                                 <asp:Label ID="lblTotalRoute" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Routes Available" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Active: &nbsp;
                                <asp:Label ID="lblActive" runat="server" data-toggle="tooltip" data-placement="bottom" title="Active Routes" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Discontinue:&nbsp;
                                 <asp:Label ID="lblDeactive" runat="server" data-toggle="tooltip" data-placement="bottom" title="Deactive Routes" class="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col">
                                        <div class="input-group-prepend">
                                            <h4 class="mb-1">Download Route Report</h4>
                                            <asp:LinkButton ID="lbtndownload" data-toggle="tooltip" OnClick="lbtndownload_Click" data-placement="bottom" title="Download" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white ml-1">
                                            <i class="fa fa-download"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="card-body">
                                <div class="row mr-0">
                                    <div class="col">
                                        <div>
                                            <h4 class="mb-1">Instructions</h4>
                                        </div>
                                        <asp:Label runat="server" CssClass="form-control-label">1. Here you can Add/Update route and in between stations</asp:Label>
                                    </div>
                                    <div class="col-auto">
                                        <asp:LinkButton ID="lbtnview" OnClick="lbtnview_Click" data-toggle="tooltip" data-placement="bottom" title="View Instructions" runat="server" Width="30px" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" OnClick="Unnamed_Click" class="btn btn bg-gradient-green btn-sm text-white" Width="30px" data-toggle="tooltip" data-placement="bottom" title="Download Instruction">
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
        <div class="row">
            <div class="col-xl-4 order-xl-1">
                <div class="card" style="min-height: 470px">
                    <div class="card-header border-bottom">
                        <div class="float-left">
                            <h3 class="mb-0">Route List</h3>
                        </div>
                        <div class="float-right">
                            <div class="input-group">
                                <asp:TextBox ID="tbSearch" data-toggle="tooltip" data-placement="bottom" title="Enter Route" CssClass="form-control form-control-sm mr-1" MaxLength="20" runat="server" Width="200px" placeholder="Search Route" autocomplete="off"></asp:TextBox>

                                <asp:LinkButton ID="lbtnSearchRoute" data-toggle="tooltip" data-placement="bottom" title="Search Route" runat="server" CssClass="btn bg-success btn-sm text-white mr-1" OnClick="lbtnSearchRoute_Click">
                                            <i class="fa fa-search"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbtnResetSearchRoute" data-toggle="tooltip" data-placement="bottom" title="Reset Route" runat="server" CssClass="btn bg-warning btn-sm text-white" OnClick="lbtnResetRoute_Click">
                                            <i class="fa fa-undo"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="gvRoutes" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
                        OnRowCommand="gvRoutes_RowCommand" OnRowDataBound="gvRoutes_RowDataBound" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvRoutes_PageIndexChanging"
                        DataKeyNames="routeid, routename, frstateid, frstonid, tostateid, tostonid, viastateid, viastonid, status, fromstate, tostate, viastate, fromstation, tostation, viastation,deleteyn,totalroute,actroute,deactroute">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="card card-stats">
                                        <!-- Card body -->
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col">
                                                    <%--<span class="h3 font-weight-bold mb-0"><%# Eval("routename") %> </span>--%>
                                                    <h5 class="text-uppercase mb-0 font-weight-bold text-xs">
                                                        <%# Eval("routename") %>
                                                      <%--  <%# Eval("fromstation") %> To <%# Eval("tostation") %>
                                                        <span class="text-gray text-xs">Via <%# Eval("viastation") %></span>--%>

                                                    </h5>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-8 pt-2">
                                                    <asp:LinkButton ID="lbtnActivate" Visible="true" runat="server" CommandName="activate" CssClass="btn btn-sm btn-success" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="Activate Route" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-check"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDiscontinue" Visible="true" runat="server" CommandName="deactivate" CssClass="btn btn-sm btn-warning" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" data-toggle="tooltip" data-placement="bottom" title="Deactivate Route"> <i class="fa fa-ban"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnViewRouteStations" Visible="true" runat="server" CommandName="viewRouteStations" CssClass="btn btn-sm btn-default" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="View Route Stations" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-eye"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnAddStation" Visible="true" runat="server" CommandName="AddStation" CssClass="btn btn-sm btn-info" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="Add Station" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-plus"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                    </asp:GridView>
                    <asp:Panel ID="pnlNoRoute" runat="server">
                        <div class="card card-stats">
                            <!-- Card body -->
                            <div class="card-body">
                                <div class="row">

                                    <div class="col-lg-12 text-center">
                                        <i class="fa fa-bus fa-5x"></i>
                                    </div>
                                    <div class="col-lg-12 text-center mt-4" runat="server" id="divNoRecord">
                                        <span class="h2 font-weight-bold mb-0">Start Route Creation </span>
                                        <h5 class="card-title text-uppercase text-muted mb-0">No Route has been created yet</h5>
                                    </div>
                                    <div class="col-lg-12 text-center mt-4" runat="server" id="divNoSearchRecord" visible="false">
                                        <span class="h2 font-weight-bold mb-0">No Record Found </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="col-xl-8 order-xl-2">
                <asp:Panel ID="pnlAddRoute" runat="server" Visible="true">
                    <div class="card" style="min-height: 470px">
                        <div class="card-header">
                            <div class="row align-items-center">
                                <div class="col-8">
                                    <h3 class="mb-0">Add Route</h3>
                                </div>
                                <div class="col-md-4 text-right">
                                    <p style="font-size: small; margin: 0px;" class="text-danger font-weight-bold">All marked * Fields are mandatory</p>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <h6 class="heading-small text-muted my-0">From</h6>
                            <div class="pl-lg-4">
                                <div class="row">
                                    <div class="col-lg-4">
                                        <asp:Label runat="server" CssClass="form-control-label"> State<span style="color: red">*</span></asp:Label>
                                        <asp:DropDownList ID="ddlFromState" CssClass="form-control form-control-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFromState_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:Label runat="server" CssClass="form-control-label"> Station<span style="color: red">*</span></asp:Label>
                                        <asp:DropDownList ID="ddlFromStation" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <h6 class="heading-small text-muted my-0 mt-2">To</h6>
                            <div class="pl-lg-4">
                                <div class="row">
                                    <div class="col-lg-4">
                                        <asp:Label runat="server" CssClass="form-control-label"> State<span style="color: red">*</span></asp:Label>
                                        <asp:DropDownList ID="ddlToState" CssClass="form-control form-control-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlToState_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:Label runat="server" CssClass="form-control-label"> Station<span style="color: red">*</span></asp:Label>
                                        <asp:DropDownList ID="ddlToStation" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <h6 class="heading-small text-muted my-0 mt-2">Via Station</h6>
                            <div class="pl-lg-4">
                                <div class="row">
                                    <div class="col-lg-4">
                                        <asp:Label runat="server" CssClass="form-control-label"> State</asp:Label>
                                        <asp:DropDownList ID="ddlViaState" CssClass="form-control form-control-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlViaState_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:Label runat="server" CssClass="form-control-label"> Station</asp:Label>
                                        <asp:DropDownList ID="ddlViaStation" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>


                            <div class="pl-lg-4 mt-2">
                                <div class="row">
                                    <div class="col-lg-12 text-right">
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" class="btn btn-success" Visible="false" data-toggle="tooltip" data-placement="bottom" title="Update Route" OnClick="updateRoute_Click">
                                    <i class="fa fa-save"></i>&nbsp; Update</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnSave" Visible="true" runat="server" class="btn btn-success" data-toggle="tooltip" data-placement="bottom" title="Save Route" OnClick="saveRoute_Click">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-danger" data-toggle="tooltip" data-placement="bottom" title="Reset Route" OnClick="resetRoute_Click">
                                    <i class="fa fa-undo"></i>&nbsp; Reset</asp:LinkButton>
                                        <asp:LinkButton ID="lbtncancel" Visible="false" runat="server" class="btn btn-warning" data-toggle="tooltip" data-placement="bottom" title="Cancel Add Route" OnClick="cancelRoute_Click">
                                    <i class="fa fa-times"></i>&nbsp; Cancel</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlStation" runat="server" Visible="false">
                    <div class="card" style="min-height: 470px">
                        <div class="card-header">
                            <div class="row m-0 align-items-center">
                                <div class="col-lg-12">
                                    <h3 class="mb-0 float-left">Add stations in Route-
                                        <asp:Label ID="lblRoute" Font-Bold="true" runat="server" Text=""></asp:Label></h3>
                                    <asp:LinkButton ID="lbtnAddStationCancel" runat="server" class="btn btn-sm btn-danger float-right" OnClick="lbtnAddStationCancel_Click"> <i class="fa fa-times" ></i> Close</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="pl-lg-4">
                                <asp:Panel ID="PanelRouteStationAddControls" runat="server" Width="100%" Visible="true">
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <asp:Label runat="server" CssClass="form-control-label">From Station<span style="color: red">*</span></asp:Label>
                                            <asp:DropDownList ID="ddlRouteFromStation" Enabled="false" data-toggle="tooltip" data-placement="bottom" title="From Station" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:Label runat="server" CssClass="form-control-label">To Station<span style="color: red">*</span></asp:Label>
                                            <asp:DropDownList ID="ddlRouteToStation" data-toggle="tooltip" data-placement="bottom" title="To Station" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:LinkButton ID="lbtnAddRouteStation" runat="server" class="btn btn-sm btn-success mt-4" data-toggle="tooltip" data-placement="bottom" title="Add Station" OnClick="addRouteStation_Click">
                                    <i class="fa fa-plus"></i> Add</asp:LinkButton>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="LabelRouteStationDoneInfo" Font-Bold="true" Font-Size="15px" runat="server"
                                            ForeColor="Red" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="PanelNoRouteStation" runat="server" Width="100%" Visible="true">
                                <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                    <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
                                        Stations not available<br />
                                        Please add Station
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-md-12 mt-5 mb-3">
                                <asp:GridView ID="gvRouteStations" runat="server" AutoGenerateColumns="false" OnRowCommand="gvRouteStations_RowCommand"
                                    GridLines="None" CssClass="table table-striped table-hover" DataKeyNames="routeid,stn_seq">
                                    <Columns>
                                        <asp:TemplateField HeaderText="FROM STATION">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGVfromStation" runat="server" Text='<%#Eval("fr_station") %>' />,
                                                                                <asp:Label ID="lblGVfromState" runat="server" Text='<%#Eval("fr_state") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TO STATION">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGVtoStation" runat="server" Text='<%#Eval("to_station") %>' />,
                                                                                <asp:Label ID="lblGVtoState" runat="server" Text='<%#Eval("to_state") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DISTANCE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGVDistance" runat="server" Text='<%#Eval("totdist") %>' />
                                                KM
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnStationDelete" Visible="false" runat="server" CssClass="btn btn-sm btn-danger" Style="border-radius: 4px;" data-toggle="tooltip" data-placement="bottom" title="DELETE" CommandName="deleteStation"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                </asp:GridView>
                                <asp:Label runat="server" CssClass="ml-3" ID="lbltottaldistancekm" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlViewStations" runat="server" Visible="false">
                    <div class="card" style="min-height: 470px">
                        <div class="card-header">
                            <div class="row m-0 align-items-center">
                                <div class="col-lg-12">
                                    <h3 class="mb-0 float-left">Stations List of 
                                        <asp:Label ID="lblStnRouteName" Font-Bold="true" runat="server" Text=""></asp:Label></h3>
                                    <asp:LinkButton ID="lbtnCloseRouteStations" runat="server" class="btn btn-sm btn-danger float-right" OnClick="lbtnAddStationCancel_Click"> <i class="fa fa-times" ></i> Close</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <asp:Panel ID="PanelNoStation" runat="server" Width="100%" Visible="true">
                                <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                    <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
                                        Stations not available<br />
                                        Please add Station
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-md-12">
                                <asp:GridView ID="gvStations" OnPageIndexChanging="gvStations_PageIndexChanging" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" GridLines="None" CssClass="table table-striped table-hover" DataKeyNames="routeid,stn_seq">
                                    <Columns>
                                        <asp:TemplateField HeaderText="FROM STATION">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGVfromStation" runat="server" Text='<%#Eval("fr_station") %>' />,
                                                                                <asp:Label ID="lblGVfromState" runat="server" Text='<%#Eval("fr_state") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TO STATION">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGVtoStation" runat="server" Text='<%#Eval("to_station") %>' />,
                                                                                <asp:Label ID="lblGVtoState" runat="server" Text='<%#Eval("to_state") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DISTANCE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGVDistance" runat="server" Text='<%#Eval("totdist") %>' />
                                                KM
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HALT MIN">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGVDistance" runat="server" Text='<%#Eval("halttime") %>' />
                                                MIN
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                </asp:GridView>
                                <asp:Label runat="server" ID="totkm" CssClass="ml-3" Font-Bold="true"></asp:Label>
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

