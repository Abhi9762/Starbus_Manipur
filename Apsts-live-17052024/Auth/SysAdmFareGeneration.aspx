<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="SysAdmFareGeneration.aspx.cs" Inherits="Auth_SysAdmFareGeneration" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

                .pagination-ys table > tbody > tr > td > a,
                .pagination-ys table > tbody > tr > td > span {
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid pt-2 pb-5">
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
                                            <div class="col-12">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Total State:&nbsp;
                                 <asp:Label ID="lblTotalFare" runat="server" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Per KM: &nbsp;
                                <asp:Label ID="lblFare" runat="server" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Slab:&nbsp;
                                 <asp:Label ID="lblSlab" runat="server" class="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
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
                                        <div>
                                            <h4 class="mb-1">Generate Fare Report</h4>
                                        </div>
                                        <br />
                                        <div class="input-group mt-2">
                                            <div class="input-group-prepend pr-2" style="width: 60%">
                                                <asp:DropDownList ID="ddlFare" data-toggle="tooltip" data-placement="bottom" title="Fare" CssClass="form-control form-control-sm" runat="server">
                                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                    <asp:ListItem Value="P" Text="Slab"></asp:ListItem>
                                                    <asp:ListItem Value="S" Text="Per Km"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <asp:LinkButton ID="lbtndownload" data-toggle="tooltip" data-placement="bottom" title="Download" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white">
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
                        <div class="float-left">
                            <h3 class="mb-0">Fare Generated List</h3>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlSServiceType" runat="server" CssClass="form-control form-control-sm mr-1">
                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="tbSRoute" runat="server" placeholder="Enter Route Name" AutoComplete="Off" CssClass="form-control form-control-sm mr-1"></asp:TextBox>
                                    <asp:LinkButton ID="lbtnSearch" OnClick="lbtnSearch_Click" runat="server" CssClass="btn bg-success btn-sm text-white mr-1">
                                            <i class="fa fa-search"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnResetSearch" OnClick="lbtnResetSearch_Click" runat="server" CssClass="btn bg-warning btn-sm text-white">
                                            <i class="fa fa-undo"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="row pt-3">
                            <div class="col-md-12">
                                <asp:GridView ID="gvFare" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
                                    OnRowCommand="gvFare_RowCommand" OnRowDataBound="gvFare_RowDataBound" OnPageIndexChanging="gvFare_PageIndexChanging"
                                    AllowPaging="true" PageSize="10" DataKeyNames="routeid,routename,srtpid,servicetype_name_en,create_dt">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <!-- Card body -->
                                                <div class="card mb-0">
                                                    <div class="row px-3 pt-2">
                                                        <div class="col">
                                                            <h5 class="text-uppercase mb-0 font-weight-bold text-xs">
                                                                <%# Eval("routename") %> </h5>
                                                        </div>
                                                        <div class="col-right px-1">
                                                            <asp:LinkButton ID="lbtnViewFare" Visible="true" runat="server" CommandName="ViewFare" CssClass="btn btn-sm btn-info" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="View Fare Details" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-eye"></i></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="row px-3">
                                                        <div class="col">
                                                            <h5><span class="text-gray"><%# Eval("servicetype_name_en") %></span></h5>
                                                        </div>
                                                        <div class="col-right px-1">
                                                            <h5><span class="text-gray text-xs font-weight-normal">Created On <%# Eval("create_dt") %></span></h5>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                </asp:GridView>
                                <asp:Panel ID="pnlNoFare" runat="server">
                                    <div class="card card-stats">
                                        <!-- Card body -->
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-lg-12 text-center">
                                                    <i class="fa fa-bus fa-5x"></i>
                                                </div>
                                                <div class="col-lg-12 text-center mt-4" runat="server" id="divNoRecord">
                                                    <span class="h2 font-weight-bold mb-0">Start Fare Creation </span>
                                                    <h5 class="card-title text-uppercase text-muted mb-0">No Fare has been created yet</h5>
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
                    </div>
                </div>
            </div>
            <div class="col-xl-8 order-xl-2">
                <asp:Panel ID="pnlAddFare" runat="server" Visible="true">
                    <div class="card" style="min-height: 470px">
                        <div class="card-header">
                            <div class="float-left">
                                <h3 class="mb-0">Fare Generate</h3>
                            </div>
                            <div class="float-right">
                                <p style="font-size: small; margin: 0px;" class="text-danger font-weight-bold">All marked * Fields are mandatory</p>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row pl-lg-4">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label"> Route <span style="color: red">*</span></asp:Label>
                                    <asp:DropDownList ID="ddlRoutes" CssClass="form-control form-control-sm text-uppercase" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-3">
                                    <asp:Label runat="server" CssClass="form-control-label">Service Type <span style="color: red">*</span></asp:Label><br />
                                    <asp:DropDownList ID="ddlServiceType" CssClass="form-control form-control-sm text-uppercase" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row pt-4 pr-2">
                                <div class="col-lg-12 text-right">
                                    <asp:LinkButton ID="lbtnSave" Visible="true" runat="server" class="btn btn-success" OnClick="lbtnSave_Click" data-toggle="tooltip" data-placement="bottom" title="Save Fare Details">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-danger" OnClick="lbtnReset_Click" data-toggle="tooltip" data-placement="bottom" title="Reset Fare Details">
                                    <i class="fa fa-undo"></i>&nbsp; Reset</asp:LinkButton>
                                </div>

                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlViewDetails" runat="server" Visible="true">
                    <asp:HiddenField ID="hfRouteId" runat="server" />
                    <asp:HiddenField ID="hfServiceTypeId" runat="server" />
                    <div class="card" style="min-height: 470px">
                        <div class="card-header">
                            <div class="float-left">
                                <h3 class="mb-0">Fare Detail</h3>
                            </div>
                            <div class="float-right">
                                <asp:LinkButton ID="lbtnCloseDetail" runat="server" CssClass="btn btn-danger btn-sm" OnClick="lbtnCloseDetail_Click"> <i class="fa fa-times"></i> Cancel </asp:LinkButton>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row px-3">
                                <div class="col-lg-12">

                                    <div class="row pt-2">
                                        <div class="col">
                                            <h5 class="text-uppercase mb-0 font-weight-bold text-sm"><span class="font-weight-normal">Route:</span>
                                                    <asp:Label ID="lblRouteName" runat="server"></asp:Label>
                                            </h5>
                                            <h5 class="text-uppercase mb-0 font-weight-bold text-sm"><span class="font-weight-normal">Service Type:</span>
                                                    <asp:Label ID="lblServiceTypeName" runat="server"></asp:Label>
                                            </h5>
                                        </div>
                                    </div>
                                    <asp:GridView ID="gvFareDetail" runat="server" GridLines="None" OnPageIndexChanging="gvFareDetail_PageIndexChanging"
                                        ShowHeader="true" AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="10">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <!-- Card body -->
                                                    <div class="card mb-0">
                                                        <div class="row px-3 pt-2">
                                                            <div class="col-sm-3">
                                                                <h5 class="text-uppercase mb-0 font-weight-normal text-xs">
                                                                    <%# Eval("from_ston") %> </h5>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <h5 class="text-uppercase mb-0 font-weight-normal text-xs">
                                                                    <%# Eval("to_ston") %> </h5>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <h5 class="text-gray mb-0"><%# Eval("ac_surcharge") %> <span class="font-weight-normal"> AC Surcharge</span></h5>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <h5 class="text-gray mb-0"><%# Eval("heat_surcharge") %> <span class="font-weight-normal"> Heat Surcharge</span></h5>
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <h5 class="text-gray mb-0"><%# Eval("total_distance") %> <span class="font-weight-normal">KM</span></h5>
                                                            </div>
                                                            <div class="col-sm-1 text-right">
                                                                <h5 class="text-gray"><%# Eval("final_fare") %> <span class="font-weight-normal"><i class="fa fa-rupee-sign pl-1"></i></span></h5>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    <PagerStyle CssClass="pagination-ys float-right" />
                                    </asp:GridView>
                                    <asp:Label ID="lblNoDetail" runat="server"></asp:Label>
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
                            <asp:LinkButton ID="lbtnYesConfirmation" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbtnYesConfirmation_Click"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
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

