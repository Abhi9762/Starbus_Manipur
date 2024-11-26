<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="SysAdmDepotService.aspx.cs" Inherits="Auth_SysAdmDepotService" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <link href="../assets/multiSelect/example-styles.css" rel="stylesheet" />
    <script src="../assets/multiSelect/jquery.multi-select.js"></script>
    <script src="../assets/multiSelect/jquery.multi-select.min.js"></script>
    <script src="../timepicker/timepicker.js"></script>
    <style type="text/css">
        .rbl input[type="radio"] {
            margin-left: 10px;
            margin-right: 10px;
        }

        .multi-select-button {
            width: 200px;
        }

            .multi-select-button:after {
                margin-left: 9.4em;
            }
    </style>
    <style>
        input[type="checkbox"] {
            width: 22px; /*Desired width*/
            height: 18px; /*Desired height*/
        }

        input[type="radio"] {
            width: 22px; /*Desired width*/
            height: 18px; /*Desired height*/
        }

        .input-group-prepend .btn, .input-group-append .btn {
            position: relative;
            z-index: 0;
        }
    </style>
    <style>
        .pagination-ys {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 10px 0;
            border-radius: 4px;
            float: right;
        }

            .pagination-ys table > tbody > tr > td {
                display: inline;
            }

                .pagination-ys table > tbody > tr > td > a,
                .pagination-ys table > tbody > tr > td > span {
                    position: inherit;
                    float: left;
                    padding: 3px 7px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    color: #dd4814;
                    background-color: #ffffff;
                    border: 1px solid #dddddd;
                    margin-left: -1px;
                }

                .pagination-ys table > tbody > tr > td > span {
                    position: inherit;
                    float: left;
                    padding: 3px 7px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    margin-left: -1px;
                    z-index: 2;
                    color: #aea79f;
                    background-color: #f5f5f5;
                    border-color: #dddddd;
                    cursor: default;
                }

                .pagination-ys table > tbody > tr > td:first-child > a,
                .pagination-ys table > tbody > tr > td:first-child > span {
                    margin-left: 0;
                    border-bottom-left-radius: 4px;
                    border-top-left-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td:last-child > a,
                .pagination-ys table > tbody > tr > td:last-child > span {
                    border-bottom-right-radius: 4px;
                    border-top-right-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td > a:hover,
                .pagination-ys table > tbody > tr > td > span:hover,
                .pagination-ys table > tbody > tr > td > a:focus,
                .pagination-ys table > tbody > tr > td > span:focus {
                    color: #97310e;
                    background-color: #eeeeee;
                    border-color: #dddddd;
                }
.pagination-ys a {
                width: auto !important;
                height: auto !important;
            }
            .pagination-ys span {
                width: auto !important;
                height: auto !important;
            }
    </style>
    <script type="text/javascript">
        $(function () {
            $('#<%= ddlDays.ClientID %>').multiSelect();
        });
    </script>
    <link href="../timepicker/timepickeSheet.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {
            $('.timepicker').mdtimepicker({
                timeFormat: 'hh:mm tt'
            });
        });
    </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidtoken" runat="server" />
    <div class="container-fluid pt-2">
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="row">
                        <div class="col-sm-4 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h4 class="mb-1">
                                            <asp:Label ID="lblSummary" runat="server" CssClass="text-capitalize">Summary as on Date 01/12/2021 04:00pm</asp:Label></h4>
                                        <div class="row m-0">
                                            <div class="col-6 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Total:&nbsp;
                                 <asp:Label ID="lblTotalDepotService" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Routes Available" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
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
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Active: &nbsp;
                                <asp:Label ID="lblActive" runat="server" data-toggle="tooltip" data-placement="bottom" title="Active Routes" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>

                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="col-sm-4 border-right">
                            <div class="card-body">
                                <h4 class="mb-1">Generate Depot Service Report</h4>
                                <div class="form-group mb-0">
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddlDepotServiceRpt" CssClass="form-control form-control-sm" runat="server">
                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="lbtndownload" data-toggle="tooltip" data-placement="bottom" title="Download" runat="server"
                                                CssClass="btn btn bg-gradient-green btn-sm text-white">
                                            <i class="fa fa-download"></i>
                                            </asp:LinkButton>
                                        </div>

                                    </div>
                                </div>



                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="card-body">
                                <div class="row mr-0">
                                    <div class="col">
                                        <div>
                                            <h4 class="mb-1">Instructions</h4>
                                        </div>
                                        <asp:Label runat="server" CssClass="form-control-label">Coming Soon</asp:Label>
                                    </div>
                                    <div class="col-auto">
                                        <asp:LinkButton ID="lbtnview" OnClick="lbtnview_Click" data-toggle="tooltip" data-placement="bottom" title="View Instructions" runat="server" Width="30px" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" class="btn btn bg-gradient-green btn-sm text-white" Width="30px" data-toggle="tooltip" data-placement="bottom" title="Download Instruction">
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
                        <div class="row">
                            <div class="col-md-4">
                                <h3 class="mb-0">Depot Services</h3>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group mb-0">
                                    <div class="input-group">
                                        <asp:TextBox ID="tbSearchDepotService" runat="server" AutoComplete="Off" class="form-control form-control-sm text-uppercase" placeholder="Search text" MaxLength="20"></asp:TextBox>
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="lbtnSearchDepotService" runat="server" CssClass="btn btn-success btn-icon-only btn-sm mr-1 " OnClick="lbtnSearchDepotService_Click" strle="z-i">
                                            <i class="fa fa-search mt-2"></i>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="lbtnRestSearchDepotService" runat="server" CssClass="btn btn-danger btn-icon-only btn-sm mr-1" OnClick="lbtnRestSearchDepotService_Click">
                                            <i class="fa fa-undo mt-2"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="gvDepotServices" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
                        OnRowCommand="gvDepotServices_RowCommand" OnRowDataBound="gvDepotServices_RowDataBound" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvRoutes_PageIndexChanging"
                        DataKeyNames="dsvcid,servicename,officeid,srtpid,bustype,routid,nooftrips,depttime,servicedurationdays,noofdutydays,dutyrestapplicable,dutyrestdays,nightallowedyn,nightallowedcat,overtimeallowed,overtimehours,noofdriver,noofconductor,servicestartdate,serviceexpiredate,layout_code,fare_yn,reservation_yn,daily_weekly,weekdaysno,statusyn,officename,servicetypename,bustypename,routename, layoutname">
                        
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="card card-stats mb-0">
                                        <div class="card-body  py-2 px-3">
                                            <div class="row">
                                                <div class="col">
                                                    <h5 class="text-uppercase mb-0 font-weight-bold text-xs">
                                                        <%# Eval("dsvcid") %> - <%# Eval("servicename") %>
                                                        <span class="text-gray text-xs">Trips <%# Eval("nooftrips") %></span>
                                                    </h5>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col pt-2">
                                                    <asp:LinkButton ID="lbtnViewDetails" runat="server" CommandName="VIEWDETAILS" CssClass="btn btn-sm btn-default" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="View Depot Service Details" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-eye"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnUpdateDetails" runat="server" CommandName="UPDATEDETAILS" CssClass="btn btn-sm btn-dribbble" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="Update Depot Service Details" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnAddTrips" runat="server" CommandName="TRIPS" CssClass="btn btn-sm btn-info" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="Add/View Trips" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-plus"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnTimeTable" runat="server" CommandName="TIMETABLE" CssClass="btn btn-sm btn-vimeo" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="Set Station Stops and Generate Time Table in Depot Service" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-building"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnTimeTableView" runat="server" CommandName="VIEWTIMETABLE" CssClass="btn btn-sm btn-dribbble" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="View Time Table" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-clock"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnAddAmenities" runat="server" CommandName="AMENITIES" CssClass="btn btn-sm btn-facebook" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="Set Amenities in Depot Service" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-luggage-cart"></i></asp:LinkButton>

                                                    <asp:LinkButton ID="lbtnActivate" Visible="false" runat="server" CommandName="ACTIVATE" CssClass="btn btn-sm btn-success" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="Activate Depot Service" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-check"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDiscontinue" Visible="false" runat="server" CommandName="DEACTIVATE" CssClass="btn btn-sm btn-warning" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" data-toggle="tooltip" data-placement="bottom" title="Deactivate Depot Service"> <i class="fa fa-ban"></i></asp:LinkButton>

                                                    <asp:LinkButton ID="lbtnTiming" runat="server" CommandName="TIMING" CssClass="btn btn-sm btn-instagram "
                                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                        Style="border-radius: 4px;" data-toggle="tooltip" data-placement="bottom" title="Change Timing"> 
                                                        <i class="fa fa-braille "></i></asp:LinkButton>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                    </asp:GridView>
                    <asp:Panel ID="pnlNoDepotService" runat="server">
                        <div class="card card-stats">
                            <!-- Card body -->
                            <div class="card-body text-center">
                                <i class="fa fa-bus fa-5x text-lighter mt-4"></i>
                                <p class="h2 font-weight-bold text-light mt-3">Depot Services Not Found</p>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>

            <div class="col-xl-8 order-xl-2">

                <asp:Panel ID="pnlAddDepotService" runat="server" Visible="true">
                    <div class="card" style="min-height: 470px">
                        <div class="card-header">
                            <div class="row align-items-center">
                                <div class="col-8">
                                    <h3 class="mb-0">Add Depot Service</h3>
                                </div>
                                <div class="col-md-4 text-right">
                                    <p style="font-size: small; margin: 0px;" class="text-danger font-weight-bold">All marked * Fields are mandatory</p>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <h6 class="heading-small my-0">Service-Related Details</h6>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-lg-3">
                                    <span class="form-control-label">Depot <span class="text-danger">*</span></span>
                                    <asp:DropDownList ID="ddlDepot" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3">
                                    <span class="form-control-label">Service Type <span class="text-danger">*</span></span>
                                    <asp:DropDownList ID="ddlServiceType" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label">Bus Type <span class="text-danger">*</span></span>
                                    <asp:DropDownList ID="ddlBusType" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label">Departure Time <span class="text-danger">*</span></span>
                                    <asp:TextBox ID="tbDeptTime" CssClass="form-control form-control-sm" type="time" runat="server" AutoComplete="Off" MaxLength="8"></asp:TextBox>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label">Trips <span class="text-danger">*</span></span>
                                    <asp:TextBox ID="tbTrips" CssClass="form-control form-control-sm" runat="server" AutoComplete="Off" MaxLength="2"></asp:TextBox>
                                </div>
                                <div class="col-lg-8 pt-2">
                                    <span class="form-control-label">Route <span class="text-danger">*</span></span>
                                    <asp:DropDownList ID="ddlRoute" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                </div>
                            </div>

                            <h6 class="heading-small mb-0 mt-4">Duty-Related Details</h6>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-lg-2">
                                    <span class="form-control-label">Service Duration <span class="text-danger">*</span></span>
                                    <asp:TextBox ID="tbServiceDuration" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="In Days" runat="server" MaxLength="2"></asp:TextBox>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label">Duty Days <span class="text-danger">*</span></span>
                                    <asp:TextBox ID="tbDutyDays" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="Duty Days" runat="server" MaxLength="2"></asp:TextBox>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label">Rest applicable <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="If rest days applicable with this service then check the checkbox below and enter the number of rest days."></i></span>
                                    <div class="form-group mb-0">
                                        <asp:UpdatePanel ID="upnlRestApplicable" runat="server">
                                            <ContentTemplate>
                                                <div class="input-group">
                                                    <div class="input-group-prepend border-right">
                                                        <asp:CheckBox ID="cbRestApplicable" runat="server" AutoPostBack="true" OnCheckedChanged="cbRestApplicable_CheckedChanged" CssClass="input-group-text form-control-sm px-1" />
                                                    </div>
                                                    <asp:TextBox ID="tbRestDays" runat="server" AutoComplete="Off" CssClass="form-control form-control-sm" placeholder="Rest Days" MaxLength="2"></asp:TextBox>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label">Night Allowed <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="If night is allowed with this service then check the checkbox below and select the category."></i></span>
                                    <div class="form-group mb-0">
                                        <asp:UpdatePanel ID="upnlNightAllowed" runat="server">
                                            <ContentTemplate>
                                                <div class="input-group">
                                                    <div class="input-group-prepend border-right">
                                                        <asp:CheckBox ID="cbNightAllowed" runat="server" AutoPostBack="true" OnCheckedChanged="cbNightAllowed_CheckedChanged" CssClass="input-group-text form-control-sm px-1" />
                                                    </div>
                                                    <asp:DropDownList ID="ddlNightAllowedCat" CssClass="form-control form-control-sm px-0" runat="server">
                                                        <asp:ListItem Text="Category"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label">Overtime Allowed <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="If overtime is allowed with this service, check the checkbox below and enter the maximum overtime hours."></i></span>
                                    <div class="form-group mb-0">
                                        <asp:UpdatePanel ID="upnlOvertimeAllowed" runat="server">
                                            <ContentTemplate>
                                                <div class="input-group">
                                                    <div class="input-group-prepend border-right">
                                                        <asp:CheckBox ID="cbOvertimeAllowed" runat="server" AutoPostBack="true" OnCheckedChanged="cbOvertimeAllowed_CheckedChanged" CssClass="input-group-text form-control-sm px-1" />
                                                    </div>
                                                    <asp:TextBox ID="tbOvertimeHours" runat="server" AutoComplete="Off" CssClass="form-control form-control-sm" placeholder="Hours" MaxLength="2"></asp:TextBox>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label">Driver/Conductor <span class="text-danger">*</span></span>
                                    <div class="form-group mb-0">
                                        <div class="input-group">
                                            <div class="input-group-prepend w-50">
                                                <asp:TextBox ID="tbDrivers" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="Driver" runat="server" MaxLength="2"></asp:TextBox>
                                            </div>
                                            <div class="input-group-append w-50">
                                                <asp:TextBox ID="tbConductors" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="Conductor" runat="server" MaxLength="2"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <h6 class="heading-small mb-0 mt-4">Online-Related Details</h6>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-lg-4">
                                    <span class="form-control-label">Layout</span>
                                    <asp:DropDownList ID="ddlLayout" CssClass="form-control form-control-sm" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label">Service Period </span>
                                    <div class="input-group ">
                                        <asp:TextBox ID="tbServiceFrom" CssClass="form-control form-control-sm" AutoComplete="Off" type="search" Placeholder="From Date" runat="server" MaxLength="10"></asp:TextBox>
                                        <div class="input-group-append">
                                            <button class="btn btn-outline-light btn-sm" type="button" id="button-tbServiceFrom" style="border-radius: 0px 3px 3px 0px;"><i class="fa fa-calendar-alt"></i></button>
                                        </div>
                                    </div>
                                    <cc1:CalendarExtender ID="ceServiceFrom" runat="server" Format="dd/MM/yyyy" PopupButtonID="button-tbServiceFrom" PopupPosition="TopRight"
                                        TargetControlID="tbServiceFrom"></cc1:CalendarExtender>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label">&nbsp;</span>
                                    <div class="input-group ">
                                        <asp:TextBox ID="tbServiceTo" CssClass="form-control form-control-sm" AutoComplete="Off" type="search" Placeholder="To Date" runat="server" MaxLength="10"></asp:TextBox>
                                        <div class="input-group-append">
                                            <button class="btn btn-outline-light btn-sm" type="button" id="button-tbServiceTo" style="border-radius: 0px 3px 3px 0px;"><i class="fa fa-calendar-alt"></i></button>
                                        </div>
                                    </div>
                                    <cc1:CalendarExtender ID="ceServiceTo" runat="server" Format="dd/MM/yyyy" PopupButtonID="button-tbServiceTo" PopupPosition="TopRight"
                                        TargetControlID="tbServiceTo"></cc1:CalendarExtender>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label">Applicable Charge<i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Select the applicable charges given below. If both fare and reservation charge are applicable then select both otherwise select any one"></i></span>
                                    <div class="form-group mb-0">
                                        <div class="input-group">
                                            <div class="input-group-prepend border-right">
                                                <asp:CheckBox ID="cbApplicableFare" runat="server" Checked="true" CssClass="input-group-text form-control-sm px-1" />
                                            </div>
                                            <span class="form-control form-control-sm px-0 font-weight-600">Fare</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label">&nbsp;</span>
                                    <div class="form-group mb-0">
                                        <div class="input-group">
                                            <div class="input-group-prepend border-right">
                                                <asp:CheckBox ID="cbApplicableReservation" runat="server" Checked="true" CssClass="input-group-text form-control-sm px-1" />
                                            </div>
                                            <span class="form-control form-control-sm px-0 font-weight-600">Reservation</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-3">

                                <div class="col-lg-2">
                                    <span class="form-control-label">Alert Publish <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Publish new service created alert for online traveller"></i></span>
                                    <div class="form-group mb-0">
                                        <div class="input-group">
                                            <div class="input-group-prepend border-right">
                                                <asp:CheckBox ID="chkpublish" runat="server" CssClass="input-group-text form-control-sm px-1" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <span class="form-control-label">Running As </span>
                                    <div class="form-group mb-0">
                                        <div class="input-group">
                                            <div class="input-group-prepend border-right">
                                                <asp:RadioButton runat="server" AutoPostBack="true" OnCheckedChanged="rbtdaily_CheckedChanged" Checked="true" ID="rbtdaily" Text="Daily" GroupName="running" />
                                                <asp:RadioButton runat="server" AutoPostBack="true" OnCheckedChanged="rbtweekly_CheckedChanged" ID="rbtweekly" CssClass="ml-3" Text="Weekly" GroupName="running" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 mt-3">
                                    <asp:ListBox ID="ddlDays" runat="server" ToolTip="Services Days" Visible="false" SelectionMode="Multiple" CssClass="form-control form-control-sm"></asp:ListBox>
                                </div>
                            </div>
                            <div class="row pl-lg-3 pr-lg-3 mt-4">
                                <div class="col-lg-12 text-right">
                                    <asp:LinkButton ID="lbtnSaveNewDepotService" runat="server" class="btn btn-success" OnClick="lbtnSaveNewDepotService_Click">
                                    <i class="fa fa-save"></i> Save</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnResetNewDepotService" runat="server" CssClass="btn btn-danger" OnClick="lbtnResetNewDepotService_Click">
                                    <i class="fa fa-undo"></i> Reset</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlViewDepotService" runat="server" Visible="false">
                    <div class="card" style="min-height: 470px">
                        <div class="card-header">
                            <div class="row align-items-center">
                                <div class="col-8">
                                    <h3 class="mb-0">View Depot Service</h3>
                                </div>
                                <div class="col-md-4 text-right">
                                    <asp:LinkButton ID="lbtnClose_pnlViewDepotService" runat="server" class="btn btn-sm btn-danger float-right" OnClick="lbtnClose_pnlViewDepotService_Click"> <i class="fa fa-times" ></i> Close</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">

                            <h6 class="heading-small my-0">Service-Related Details</h6>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-lg-12">
                                    <span class="form-control-label text-muted">Name</span>
                                    <asp:Label ID="lblDepotServicevw" runat="server" CssClass="text-uppercase mb-0 font-weight-bold text-xs" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-12">
                                    <span class="form-control-label text-muted">Route</span>
                                    <asp:Label ID="lblRoutevw" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Depot</span>
                                    <asp:Label ID="lblDepotvw" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Service Type </span>
                                    <asp:Label ID="lblServiceTypevw" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Bus Type</span>
                                    <asp:Label ID="lblBusTypevw" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Trips </span>
                                    <asp:Label ID="lblTripsvw" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Departure Time </span>
                                    <asp:Label ID="lblDepttimevw" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Status </span>
                                    <asp:Label ID="lblStatusvw" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                            </div>

                            <h6 class="heading-small mb-0 mt-4">Duty-Related Details</h6>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Service Duration (Days) </span>
                                    <asp:Label ID="lblServiceDurationvw" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Duty Days</span>
                                    <asp:Label ID="lblDutyDaysvw" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label text-muted">Driver</span>
                                    <asp:Label ID="lblDriversvw" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label text-muted">Conductor</span>
                                    <asp:Label ID="lblConductorsvw" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Rest applicable</span>
                                    <asp:Label ID="lblResApplicablevw" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Night Allowed</span>
                                    <asp:Label ID="lblNightAllowedvw" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Overtime Allowed</span>
                                    <asp:Label ID="lblOvertimeAllowedvw" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                            </div>

                            <h6 class="heading-small mb-0 mt-4">Online-Related Details</h6>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Service Period </span>
                                    <asp:Label ID="lblServicePeriodvw" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-8">
                                    <span class="form-control-label text-muted">Layout</span>
                                    <asp:Label ID="lblLayoutvw" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Applicable Fare</span>
                                    <asp:Label ID="lblApplicableFarevw" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Applicable Reservation</span>
                                    <asp:Label ID="lblApplicableReservationvw" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
<div class="col-lg-12">
                                    <span class="form-control-label text-muted">Running As</span>
                                    <asp:Label ID="lblRunningAsVw" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                            </div>

                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlUpdateDepotService" runat="server" Visible="false">
                    <div class="card" style="min-height: 470px">
                        <div class="card-header">
                            <div class="row align-items-center">
                                <div class="col-8">
                                    <h3 class="mb-0">Update Depot Service</h3>
                                </div>
                                <div class="col-md-4 text-right">
                                    <asp:LinkButton ID="lbtnClose_pnlUpdateDepotService" runat="server" class="btn btn-sm btn-danger float-right" OnClick="lbtnClose_pnlUpdateDepotService_Click"> <i class="fa fa-times" ></i> Close</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <h6 class="heading-small my-0">Service-Related Details</h6>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-lg-12">
                                    <span class="form-control-label text-muted">Name</span>
                                    <asp:Label ID="lblDepotServiceUp" runat="server" CssClass="text-uppercase mb-0 font-weight-bold text-xs" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-12">
                                    <span class="form-control-label text-muted">Route</span>
                                    <asp:Label ID="lblRouteUp" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Depot</span>
                                    <asp:Label ID="lblDepotUp" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Service Type </span>
                                    <asp:Label ID="lblServiceTypeUp" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Bus Type</span>
                                    <asp:Label ID="lblBusTypeUp" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <div class="row pl-2 pr-2">
                                        <div class="col-lg-2">
                                            <span class="form-control-label text-muted pl-1">Trips </span>
                                        </div>
                                        <div class="col-lg-3 ">
                                            <asp:TextBox ID="tbTripsUp" CssClass="form-control form-control-sm" runat="server" AutoComplete="Off" MaxLength="2"></asp:TextBox>
                                            <asp:Label ID="lblTripsUp" CssClass="collapse" runat="server" Text="NA"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Departure Time </span>
                                    <asp:Label ID="lblDepttimeUp" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Status </span>
                                    <asp:Label ID="lblStatusUp" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                            </div>
                            <h6 class="heading-small mb-0 mt-4">Duty-Related Details</h6>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-lg-2">
                                    <span class="form-control-label">Service Duration <span class="text-danger">*</span></span>
                                    <asp:TextBox ID="tbServiceDurationUp" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="In Days" runat="server" MaxLength="2"></asp:TextBox>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label">Duty Days <span class="text-danger">*</span></span>
                                    <asp:TextBox ID="tbDutyDaysUp" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="Duty Days" runat="server" MaxLength="2"></asp:TextBox>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label">Rest applicable <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="If rest days applicable with this service then check the checkbox below and enter the number of rest days."></i></span>
                                    <div class="form-group mb-0">
                                        <asp:UpdatePanel ID="upnlRestApplicableUp" runat="server">
                                            <ContentTemplate>
                                                <div class="input-group">
                                                    <div class="input-group-prepend border-right">
                                                        <asp:CheckBox ID="cbRestApplicableUp" runat="server" AutoPostBack="true" OnCheckedChanged="cbRestApplicableUp_CheckedChanged" CssClass="input-group-text form-control-sm px-1" />
                                                    </div>
                                                    <asp:TextBox ID="tbRestDaysUp" runat="server" AutoComplete="Off" CssClass="form-control form-control-sm" placeholder="Rest Days" MaxLength="2"></asp:TextBox>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label">Night Allowed <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="If night is allowed with this service then check the checkbox below and select the category."></i></span>
                                    <div class="form-group mb-0">
                                        <asp:UpdatePanel ID="upnlNightAllowedUp" runat="server">
                                            <ContentTemplate>
                                                <div class="input-group">
                                                    <div class="input-group-prepend border-right">
                                                        <asp:CheckBox ID="cbNightAllowedUp" runat="server" AutoPostBack="true" OnCheckedChanged="cbNightAllowedUp_CheckedChanged" CssClass="input-group-text form-control-sm px-1" />
                                                    </div>
                                                    <asp:DropDownList ID="ddlNightAllowedCatUp" CssClass="form-control form-control-sm px-0" runat="server">
                                                        <asp:ListItem Text="Category"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label">Overtime Allowed <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="If overtime is allowed with this service, check the checkbox below and enter the maximum overtime hours."></i></span>
                                    <div class="form-group mb-0">
                                        <asp:UpdatePanel ID="upnlOvertimeAllowedUp" runat="server">
                                            <ContentTemplate>
                                                <div class="input-group">
                                                    <div class="input-group-prepend border-right">
                                                        <asp:CheckBox ID="cbOvertimeAllowedUp" runat="server" AutoPostBack="true" OnCheckedChanged="cbOvertimeAllowedUp_CheckedChanged" CssClass="input-group-text form-control-sm px-1" />
                                                    </div>
                                                    <asp:TextBox ID="tbOvertimeHoursUp" runat="server" AutoComplete="Off" CssClass="form-control form-control-sm" placeholder="Hours" MaxLength="2"></asp:TextBox>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label">Driver/Conductor <span class="text-danger">*</span></span>
                                    <div class="form-group mb-0">
                                        <div class="input-group">
                                            <div class="input-group-prepend w-50">
                                                <asp:TextBox ID="tbDriversUp" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="Driver" runat="server" MaxLength="2"></asp:TextBox>
                                            </div>
                                            <div class="input-group-append w-50">
                                                <asp:TextBox ID="tbConductorsUp" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="Conductor" runat="server" MaxLength="2"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <h6 class="heading-small mb-0 mt-4">Online-Related Details</h6>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-lg-4">
                                    <span class="form-control-label">Layout</span>
                                    <asp:DropDownList ID="ddlLayoutUp" CssClass="form-control form-control-sm" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label">Service Period </span>
                                    <div class="input-group ">
                                        <asp:TextBox ID="tbServiceFromUp" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="From Date" runat="server" MaxLength="10"></asp:TextBox>
                                        <div class="input-group-append">
                                            <button class="btn btn-outline-light btn-sm" type="button" id="button-tbServiceFromUp" style="border-radius: 0px 3px 3px 0px;"><i class="fa fa-calendar-alt"></i></button>
                                        </div>
                                    </div>
                                    <cc1:CalendarExtender ID="ceServiceFromUp" runat="server" Format="dd/MM/yyyy" PopupButtonID="button-tbServiceFromUp" PopupPosition="TopRight"
                                        TargetControlID="tbServiceFromUp"></cc1:CalendarExtender>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label">&nbsp;</span>
                                    <div class="input-group ">
                                        <asp:TextBox ID="tbServiceToUp" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="To Date" runat="server" MaxLength="10"></asp:TextBox>
                                        <div class="input-group-append">
                                            <button class="btn btn-outline-light btn-sm" type="button" id="button-tbServiceToUp" style="border-radius: 0px 3px 3px 0px;"><i class="fa fa-calendar-alt"></i></button>
                                        </div>
                                    </div>
                                    <cc1:CalendarExtender ID="ceServiceToUp" runat="server" Format="dd/MM/yyyy" PopupButtonID="button-tbServiceToUp" PopupPosition="TopRight"
                                        TargetControlID="tbServiceToUp"></cc1:CalendarExtender>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label">Applicable Charge<i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Select the applicable charges given below. If both fare and reservation charge are applicable then select both otherwise select any one"></i></span>
                                    <div class="form-group mb-0">
                                        <div class="input-group">
                                            <div class="input-group-prepend border-right">
                                                <asp:CheckBox ID="cbApplicableFareUp" runat="server" Checked="true" CssClass="input-group-text form-control-sm px-1" />
                                            </div>
                                            <span class="form-control form-control-sm px-0 font-weight-600">Fare</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label">&nbsp;</span>
                                    <div class="form-group mb-0">
                                        <div class="input-group">
                                            <div class="input-group-prepend border-right">
                                                <asp:CheckBox ID="cbApplicableReservationUp" runat="server" Checked="true" CssClass="input-group-text form-control-sm px-1" />
                                            </div>
                                            <span class="form-control form-control-sm px-0 font-weight-600">Reservation</span>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row pl-lg-3 pr-lg-3 mt-4">
                                <div class="col-lg-12 text-right">
                                    <asp:LinkButton ID="lbtnUpdateDepotService" runat="server" class="btn btn-success" OnClick="lbtnUpdateDepotService_Click">
                                    <i class="fa fa-upload"></i> Update</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlTripsDepotService" runat="server" Visible="false">
                    <div class="card" style="min-height: 470px">
                        <div class="card-header">
                            <div class="row align-items-center">
                                <div class="col-8">
                                    <h3 class="mb-0">Trips with Depot Service</h3>
                                </div>
                                <div class="col-md-4 text-right">
                                    <asp:LinkButton ID="lbtnClose_pnlTripsDepotService" runat="server" class="btn btn-sm btn-danger float-right" OnClick="lbtnClose_pnlTripsDepotService_Click"> <i class="fa fa-times" ></i> Close</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <h6 class="heading-small my-0">Service-Related Details</h6>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-lg-12">
                                    <span class="form-control-label text-muted">Name</span>
                                    <asp:Label ID="lblDepotServiceTrip" runat="server" CssClass="text-uppercase mb-0 font-weight-bold text-xs" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label text-muted">Trips</span>
                                    <asp:Label ID="lblTripsTrip" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-3">
                                    <span class="form-control-label text-muted">Departure Time </span>
                                    <asp:Label ID="lblDepttimeTrip" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label text-muted">Status </span>
                                    <asp:Label ID="lblStatusTrip" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-5">
                                    <span class="form-control-label text-muted">Depot</span>
                                    <asp:Label ID="lblDepotTrip" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <asp:HiddenField ID="hfLayoutYNTrip" runat="server" Value="0" />
                            </div>

                            <h6 class="heading-small mb-0 mt-4">Add Trip</h6>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-lg-3">
                                    <span class="form-control-label">Direction <span class="text-danger">*</span></span>
                                    <asp:DropDownList ID="ddlDirectTrip" AutoPostBack="true" OnSelectedIndexChanged="ddlDirectTrip_SelectedIndexChanged" CssClass="form-control form-control-sm" runat="server">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Forward" Value="F"></asp:ListItem>
                                        <asp:ListItem Text="Inward" Value="I"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-3">
                                    <span class="form-control-label">From Station <span class="text-danger">*</span></span>
                                    <asp:DropDownList ID="ddlFromStationTrip" AutoPostBack="true" OnSelectedIndexChanged="ddlFromStationTrip_SelectedIndexChanged" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3">
                                    <span class="form-control-label">To Station <span class="text-danger">*</span></span>
                                    <asp:DropDownList ID="ddlToStationTrip" AutoPostBack="true" OnSelectedIndexChanged="ddlToStationTrip_SelectedIndexChanged" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3">
                                    <span class="form-control-label">Via Station</span>
                                    <asp:DropDownList ID="ddlviastation" CssClass="form-control form-control-sm" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label">Start Time <span class="text-danger">*</span></span>
                                    <asp:TextBox ID="tbStartTimeTrip" CssClass="form-control form-control-sm" type="time" runat="server" AutoComplete="Off"></asp:TextBox>
                                </div>
                                <div class="col-lg-2">
                                    <span class="form-control-label">End Time <span class="text-danger">*</span></span>
                                    <asp:TextBox ID="tbEndTimeTrip" CssClass="form-control form-control-sm" type="time" runat="server" AutoComplete="Off"></asp:TextBox>
                                </div>


                                <div class="col-lg-2">
                                    <span class="form-control-label">Online <span class="text-danger">*</span></span>
                                    <asp:DropDownList ID="ddlOnlineTrip" CssClass="form-control form-control-sm" runat="server">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-3">
                                    <span class="form-control-label">&nbsp;</span>
                                    <asp:CheckBox ID="cbMiddleStationOnline" runat="server" Visible="true" CssClass="form-control form-control-sm" Text="Middle Stations Available for Online" Style="border: none; color: black; font-size: 15px;" />
                                </div>
                                <asp:Panel ID="pnlTripDays" runat="server" CssClass="col-lg-3">
                                    <span class="form-control-label">Week Days</span><br />
                                    <asp:DropDownList ID="ddlTripDays" runat="server" ToolTip="Services Days" Visible="true" CssClass="form-control form-control-sm">                                        
                                    </asp:DropDownList>
                                </asp:Panel>


                                <div class="col-lg-2 ">
                                    <span class="form-control-label">&nbsp;</span><br />
                                    <asp:LinkButton ID="lbtnSaveNewTrip" runat="server" class="btn btn-success btn-sm text-sm" OnClick="lbtnSaveNewTrip_Click">
                                    <i class="fa fa-save"></i> Save Trip</asp:LinkButton>
                                </div>

                            </div>

                            <h6 class="heading-small mb-0 mt-4">Trip Details</h6>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-lg-12">
                                    <asp:GridView ID="gvTrips" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvTrips_RowDataBound" OnRowCommand="gvTrips_RowCommand"
                                        GridLines="None" CssClass="table text-uppercase" DataKeyNames="strpid,dsvcid,fr_stonid,fr_stonname,to_stonid,to_stonname,starttime,endtime,direction,pln_km,hill_km,onlineyn,midstation_onlineyn,statusyn,weekday">
                                        <Columns>
                                            <asp:TemplateField HeaderText="FROM">
                                                <ItemTemplate>
                                                    <%#Eval("fr_stonname") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TO">
                                                <ItemTemplate>
                                                    <%#Eval("to_stonname") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="START TIME">
                                                <ItemTemplate>
                                                    <%#Eval("starttime") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="END TIME">
                                                <ItemTemplate>
                                                    <%#Eval("endtime") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAY">
                                                <ItemTemplate>
                                                    <%#Eval("weekday") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DIRECTION">
                                                <ItemTemplate>
                                                    <%# Eval("direction").ToString()=="I"?"Inward":"Forward" %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Online">
                                                <ItemTemplate>
                                                    <label class="custom-toggle">
                                                        <asp:CheckBox ID="cbOnlineYN" runat="server" Checked="true" />
                                                        <span class="custom-toggle-slider rounded-circle" data-label-off="No" data-label-on="Yes"></span>
                                                    </label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mid Station">
                                                <ItemTemplate>
                                                    <label class="custom-toggle">
                                                        <asp:CheckBox ID="cbMidStationYN" runat="server" Checked="true" />
                                                        <span class="custom-toggle-slider rounded-circle" data-label-off="No" data-label-on="Yes"></span>
                                                    </label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Active">
                                                <ItemTemplate>
                                                    <label class="custom-toggle">
                                                        <asp:CheckBox ID="cbActiveYN" runat="server" Checked="true" />
                                                        <span class="custom-toggle-slider rounded-circle" data-label-off="No" data-label-on="Yes"></span>
                                                    </label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnupdate" runat="server" CssClass="btn btn-sm btn-success" Style="border-radius: 4px;" CommandName="UPDATETRIP" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                        OnClientClick="return ShowLoading();" ToolTip="Update Trip Details" data-toggle="tooltip" data-placement="bottom"> 
                                                        <i class="fa fa-check"></i> 
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDhaba" runat="server" CssClass="btn btn-sm btn-default ml-1" Style="border-radius: 4px;" CommandName="DHABATRIP" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                        OnClientClick="return ShowLoading();" ToolTip="Dhaba with Trip" data-toggle="tooltip" data-placement="bottom"> 
                                                        <i class="fa fa-hotel"></i> 
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                    </asp:GridView>
                                    <asp:Label ID="lblNoTripMsg" runat="server" CssClass="h2 text-muted" Text=""></asp:Label>
                                </div>
                            </div>

                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlTimeTableDepotService" runat="server" Visible="false">
                    <div class="card" style="min-height: 470px">
                        <div class="card-header">
                            <div class="row align-items-center">
                                <div class="col-8">
                                    <h3 class="mb-0">Stops and Timetable Depot Service</h3>
                                </div>
                                <div class="col-md-4 text-right">
                                    <asp:LinkButton ID="lbtnClose_pnlTimeTableDepotService" runat="server" class="btn btn-sm btn-danger float-right" OnClick="lbtnClose_pnlTimeTableDepotService_Click"> <i class="fa fa-times" ></i> Close</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <h6 class="heading-small my-0">Service-Related Details</h6>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-lg-12">
                                    <span class="form-control-label text-muted">Name</span>
                                    <asp:Label ID="lblDepotServiceTT" runat="server" CssClass="text-uppercase mb-0 font-weight-bold text-xs" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-12">
                                    <span class="form-control-label text-muted">Route</span>
                                    <asp:Label ID="lblRouteTT" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Depot</span>
                                    <asp:Label ID="lblDepotTT" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Service Type </span>
                                    <asp:Label ID="lblServiceTypeTT" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Bus Type</span>
                                    <asp:Label ID="lblBusTypeTT" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Trips </span>
                                    <asp:Label ID="lblTripsTT" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Departure Time </span>
                                    <asp:Label ID="lblDepttimeTT" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Status </span>
                                    <asp:Label ID="lblStatusTT" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                            </div>

                            <h6 class="heading-small mb-0 mt-4">Station-Related Details</h6>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-lg-12">
                                    <asp:GridView ID="gvTimeTable" runat="server" AutoGenerateColumns="false" GridLines="None" OnRowDataBound="gvTimeTable_RowDataBound"
                                        CssClass="table text-uppercase" DataKeyNames="tssmid,dsvcid,stationid" Style="width: 100%;">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                <HeaderTemplate>
                                                    <span>S.No.</span>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmpGVTssmId" runat="server" Visible="false" Text='<%#Eval("tssmid") %>' />
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    <asp:Label ID="lblmpGVFromStationId" runat="server" Visible="false" Text='<%#Eval("from_ston_id") %>' />
                                                    <asp:Label ID="lblmpGVToStationId" runat="server" Visible="false" Text='<%#Eval("to_ston_id") %>' />
                                                    <asp:Label ID="lblmpGVDsvcId" runat="server" Visible="false" Text='<%#Eval("dsvcid") %>' />
                                                    <asp:Label ID="lblmpGVStrpId" runat="server" Visible="false" Text='<%#Eval("strpid") %>' />
                                                    <asp:Label ID="lblmpGVAlreadyYN" runat="server" Visible="false" Text='<%#Eval("already_yn") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                <HeaderTemplate>
                                                    <span>Trip No.</span>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmpGVTripNo" runat="server" Text='<%#Eval("tripno") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Station Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmpGVStationName" runat="server" Text='<%#Eval("ston_name") %>' />
                                                    <asp:Label ID="lblmpGVStationId" runat="server" Visible="false" Text='<%#Eval("stationid") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                <HeaderTemplate>
                                                    <span>Add Station</span>
                                                    <asp:CheckBox ID="CheckBoxHeaderAll" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBoxAll_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBoxAddStation" runat="server" />
                                                    <asp:Label ID="lblmpGVAddStation" runat="server" Visible="false" Text='<%#Eval("ston_stop_yn") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                <HeaderTemplate>
                                                    <span>Mid Station</span>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBoxMidStation" runat="server" />
                                                    <asp:Label ID="lblmpGVMidStation" runat="server" Visible="false" Text='<%#Eval("onl_ston") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                <HeaderTemplate>
                                                    <span>Boarding Station</span>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBoxBoardingStation" runat="server" />
                                                    <asp:Label ID="lblmpGVBoardingStation" runat="server" Visible="false" Text='<%#Eval("onl_boarding") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                <HeaderTemplate>
                                                    <span>Change Crew</span>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBoxChangeCrew" runat="server" />
                                                    <asp:Label ID="lblmpGVChangeCrew" runat="server" Visible="false" Text='<%#Eval("crew_change_yn") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Panel ID="PanelNoRecordTimeTable" runat="server" Width="100%" Visible="true">
                                        <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                            <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 35px; padding-bottom: 35px; font-size: 25px; font-weight: bold;">
                                                No Record available<br />
                                                Please Add Trip First
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-md-12 mt-2 mb-2 text-right">
                                    <asp:LinkButton ID="lbtnSaveTimeTable" runat="server" class="btn btn-success"
                                        Style="border-radius: 4px;" OnClientClick="return ShowLoading();" OnClick="lbtnSaveTimeTable_Click"> 
                                        <i class="fa fa-save"></i> Save</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlTimeTableViewDepotService" runat="server" Visible="false">
                    <div class="card" style="min-height: 470px">
                        <div class="card-header">
                            <div class="row align-items-center">
                                <div class="col-8">
                                    <h3 class="mb-0">View Timetable</h3>
                                </div>
                                <div class="col-md-4 text-right">
                                    <asp:LinkButton ID="lbtnClose_pnlTimeTableViewDepotService" runat="server" class="btn btn-sm btn-danger float-right" OnClick="lbtnClose_pnlTimeTableViewDepotService_Click"> <i class="fa fa-times" ></i> Close</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <h6 class="heading-small my-0">Service-Related Details</h6>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-lg-12">
                                    <span class="form-control-label text-muted">Name</span>
                                    <asp:Label ID="lblDepotServiceTTV" runat="server" CssClass="text-uppercase mb-0 font-weight-bold text-xs" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-12">
                                    <span class="form-control-label text-muted">Route</span>
                                    <asp:Label ID="lblRouteTTV" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Depot</span>
                                    <asp:Label ID="lblDepotTTV" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Service Type </span>
                                    <asp:Label ID="lblServiceTypeTTV" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Bus Type</span>
                                    <asp:Label ID="lblBusTypeTTV" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Trips </span>
                                    <asp:Label ID="lblTripsTTV" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Departure Time </span>
                                    <asp:Label ID="lblDepttimeTTV" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Status </span>
                                    <asp:Label ID="lblStatusTTV" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                            </div>

                            <h6 class="heading-small mb-0 mt-4">Timetable</h6>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-lg-12">
                                    <div class="row">
                                        <asp:ListView ID="lvTimetableView" runat="server" OnItemDataBound="lvTimetableView_ItemDataBound">
                                            <ItemTemplate>
                                                <div class="col-lg-12">
                                                    <div class="card card-stats shadow-lg mb-1">
                                                        <div class="card-body px-4 py-2">
                                                            <div class="row">
                                                                <div class="col-lg-1 text-left">
                                                                    <span class="h4 font-weight-bold text-muted"><%# Container.DataItemIndex + 1 %> </span>
                                                                </div>
                                                                <div class="col-lg-6">
                                                                    <span class="h5 font-weight-bold mb-0 text-uppercase"><%# Eval("ston_name") %></span>
                                                                </div>
                                                                <div class="col-lg-2 text-left">
                                                                    <asp:Label ID="lblInTime" runat="server" CssClass="text-sm text-uppercase" Text='<%#Eval("intime") %>'></asp:Label>
                                                                </div>
                                                                <div class="col-lg-2 text-right">
                                                                    <span class="font-weight-600" style="font-size: .5rem !important">OUT</span>
                                                                    <asp:Label ID="lblOutTime" runat="server" CssClass="text-sm text-uppercase" Text='<%#Eval("outtime") %>'></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                    <asp:Panel ID="PanelNoRecordTimeTableView" runat="server" Width="100%" Visible="true">
                                        <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                            <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 35px; padding-bottom: 35px; font-size: 25px; font-weight: bold;">
                                                No Record available<br />
                                                Please Set Station Stops and Generate Time Table
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlAmenitiesDepotService" runat="server" Visible="false">
                    <div class="card" style="min-height: 470px">
                        <div class="card-header">
                            <div class="row align-items-center">
                                <div class="col-8">
                                    <h3 class="mb-0">Amenities</h3>
                                </div>
                                <div class="col-md-4 text-right">
                                    <asp:LinkButton ID="lbtnClose_pnlAmenitiesDepotService" runat="server" class="btn btn-sm btn-danger float-right" OnClick="lbtnClose_pnlAmenitiesDepotService_Click"> <i class="fa fa-times" ></i> Close</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <h6 class="heading-small my-0">Service-Related Details</h6>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-lg-12">
                                    <span class="form-control-label text-muted">Name</span>
                                    <asp:Label ID="lblDepotServiceA" runat="server" CssClass="text-uppercase mb-0 font-weight-bold text-xs" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-12">
                                    <span class="form-control-label text-muted">Route</span>
                                    <asp:Label ID="lblRouteA" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Depot</span>
                                    <asp:Label ID="lblDepotA" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Service Type </span>
                                    <asp:Label ID="lblServiceTypeA" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Bus Type</span>
                                    <asp:Label ID="lblBusTypeA" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Trips </span>
                                    <asp:Label ID="lblTripsA" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Departure Time </span>
                                    <asp:Label ID="lblDepttimeA" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Status </span>
                                    <asp:Label ID="lblStatusA" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                            </div>

                            <h6 class="heading-small mb-0 mt-4">Amenities</h6>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-lg-12">
                                    <div class="row">
                                        <asp:ListView ID="lvAmenities" runat="server" DataKeyNames="amenity_yn" OnItemDataBound="lvAmenities_ItemDataBound">
                                            <ItemTemplate>
                                                <div class="col-lg-4">
                                                    <div class="card card-stats shadow-lg">
                                                        <div class="card-body px-4 py-2">
                                                            <div class="row">
                                                                <div class="col-auto pt-1">
                                                                    <asp:CheckBox ID="cbAmenity" CssClass="ChkBoxClass" runat="server" />
                                                                </div>
                                                                <div class="col-auto">
                                                                    <asp:Label ID="lblamenity_yn" runat="server" Visible="false" Text='<%# Eval("amenity_yn") %>'></asp:Label>
                                                                    <asp:Label ID="lblAmenityId" runat="server" Visible="false" Text='<%# Eval("amenityid") %>'></asp:Label>
                                                                    <span class="h4 font-weight-bold mb-0"><%# Eval("amenityname") %></h5>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                    <asp:Panel ID="PanelNoRecordAmenities" runat="server" Width="100%" Visible="true">
                                        <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                            <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 35px; padding-bottom: 35px; font-size: 25px; font-weight: bold;">
                                                Amenities are not available
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-md-12 mt-2 mb-2 text-right">
                                    <asp:LinkButton ID="lbtnSaveAmenities" runat="server" class="btn btn-success"
                                        Style="border-radius: 4px;" OnClientClick="return ShowLoading();" OnClick="lbtnSaveAmenities_Click"> 
                                        <i class="fa fa-save"></i> Save</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlTimingDepotService" runat="server" Visible="false">
                    <div class="card" style="min-height: 470px">
                        <div class="card-header">
                            <div class="row align-items-center">
                                <div class="col-8">
                                    <h3 class="mb-0">Depot Service Timing</h3>
                                </div>
                                <div class="col-md-4 text-right">
                                    <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-sm btn-danger float-right" OnClick="lbtnClose_pnlTripsDepotService_Click"> <i class="fa fa-times" ></i> Close</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <h6 class="heading-small my-0">Service-Related Details</h6>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-lg-12">
                                    <span class="form-control-label text-muted">Name</span>
                                    <asp:Label ID="lblTimingName" runat="server" CssClass="text-uppercase mb-0 font-weight-bold text-xs" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-12">
                                    <span class="form-control-label text-muted">Route</span>
                                    <asp:Label ID="lblTimingRoute" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Depot</span>
                                    <asp:Label ID="lblTimingDepot" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Service Type </span>
                                    <asp:Label ID="lblTimingServiceType" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Bus Type</span>
                                    <asp:Label ID="lblTimingBusType" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Trips </span>
                                    <asp:Label ID="lblTimingtrips" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted">Status </span>
                                    <asp:Label ID="lblTimingStatus" CssClass="text-uppercase mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                            </div>

                            <h6 class="heading-small mb-0 mt-4">STEP 1. Update Depot service Departure Timing</h6>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-sm-5">
                                    Current Departure Time 
                                    <asp:Label ID="lblTimingDepartureTime" CssClass="text-uppercase mb-0 font-weight-bold" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-sm-5">
                                    New Departure Time 
                                    <asp:TextBox ID="tbNewDepartureTime" CssClass="form-control form-control-sm timepicker" runat="server" AutoComplete="Off" Style="width: 120px; display: inline; background-color: transparent;"></asp:TextBox>                                    
                                </div>
                                <div class="col-sm-2">
                                    <asp:LinkButton ID="lbtnTimingProceed" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbtnTimingProceed_Click">Proceed</asp:LinkButton>
                                </div>
                            </div>

                            <h6 class="heading-small mb-0 mt-4">
                                <asp:Label ID="lblTimingStep2Heading" runat="server" Visible="false" Text="Step 2. Update Trip(s) Timing"></asp:Label>
                            </h6>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-lg-12">
                                    <asp:GridView ID="gvTimingTrips" runat="server" AutoGenerateColumns="false" GridLines="None" CssClass="table text-uppercase"
                                        DataKeyNames="strpid,dsvcid,fr_stonid,fr_stonname,to_stonid,to_stonname,starttime,endtime,direction,pln_km,hill_km,onlineyn,midstation_onlineyn,statusyn">
                                        <Columns>
                                            <asp:TemplateField HeaderText="FROM">
                                                <ItemTemplate>
                                                    <%#Eval("fr_stonname") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TO">
                                                <ItemTemplate>
                                                    <%#Eval("to_stonname") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="START TIME">
                                                <ItemTemplate>
                                                    <%#Eval("starttime") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="END TIME">
                                                <ItemTemplate>
                                                    <%#Eval("endtime") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DIRECTION">
                                                <ItemTemplate>
                                                    <%# Eval("direction").ToString()=="I"?"Inward":"Forward" %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="New START TIME">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbStartTimeTrip" CssClass="form-control form-control-sm timepicker" runat="server" AutoComplete="Off" Style="width: 120px; background-color: transparent;"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="New END TIME">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbEndTimeTrip" CssClass="form-control form-control-sm timepicker" runat="server" AutoComplete="Off" Style="width: 120px; background-color: transparent;"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblNoTripMsg1" runat="server" CssClass="h2 text-muted" Text=""></asp:Label>
                                </div>
                            </div>

                            <div class="row pl-lg-3 pr-lg-3 mt-4">
                                <div class="col-lg-12 text-right">
                                    <asp:LinkButton ID="lbtnTimingUpdate" runat="server" class="btn btn-success" Visible="false" OnClick="lbtnTimingUpdate_Click">
                                    <i class="fa fa-upload"></i> Update</asp:LinkButton>
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
                <div class="card" style="min-width: 350px; max-width: 850px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Please Confirm
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblConfirmation" runat="server" CssClass="text-uppercase" Text="Do you want to save ?"></asp:Label>
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
        <div class="row">

            <cc1:ModalPopupExtender ID="mpAddDhaba" runat="server" PopupControlID="pnlDhaba"
                CancelControlID="lbtnCancempAddDhaba" TargetControlID="btnOpenmpAddDhaba" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlDhaba" runat="server" Style="position: fixed;">
                <div class="card" style="height: 85vh; min-width: 350px; max-width: 850px;">
                    <div class="card-header">
                        <h4 class="card-title text-uppercase mb-0">
                            <asp:Label ID="lblDhabaHeaderTripDetails" runat="server"></asp:Label>
                            <asp:Label ID="lblDhabaStrpID" runat="server" Visible="false"></asp:Label>
                        </h4>
                    </div>
                    <div class="card-body text-left pt-0 overflow-auto">
                        <h6 class="text-uppercase text-dark mb-0 border-bottom text-sm py-1">Dhaba List</h6>
                        <asp:ListView ID="lvDhabas" runat="server">
                            <ItemTemplate>
                                <div class="row pl-2 py-1 border-bottom">
                                    <div class="col-auto pt-2">
                                        <asp:CheckBox ID="cbDhaba" CssClass="ChkBoxClass" runat="server" />
                                    </div>
                                    <div class="col-auto">
                                        <asp:Label ID="lblDhabaId" runat="server" Visible="false" Text='<%# Eval("id") %>'></asp:Label>
                                        <span class="text-xs font-weight-bold text-uppercase mb-0"><%# Eval("name") %></span>
                                        <h6 class="text-uppercase text-muted mb-0"><%# Eval("address") %></h6>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                        <asp:Label ID="lblNoDhabaMsg" runat="server" CssClass="text-muted font-weight-800 text-sm"></asp:Label>
                    </div>
                    <div class="card-footer text-right p-2">
                        <asp:LinkButton ID="lbtnSaveDhaba" OnClick="lbtnSaveDhaba_Click" runat="server" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Save </asp:LinkButton>
                        <asp:LinkButton ID="lbtnCancempAddDhaba" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> Cancel </asp:LinkButton>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="btnOpenmpAddDhaba" runat="server" Text="" />
                </div>
            </asp:Panel>

        </div>
    </div>

</asp:Content>

