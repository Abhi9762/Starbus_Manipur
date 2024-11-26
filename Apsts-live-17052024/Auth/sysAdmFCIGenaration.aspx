<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="sysAdmFCIGenaration.aspx.cs" Inherits="Auth_sysAdmFCIGenaration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid pb-5">
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
                                            <div class="col-6">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Total FCI Generated:&nbsp;
                                 <asp:Label ID="lblTotalState" runat="server" ToolTip="Total States Available" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                              <%--      <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Configured State: &nbsp;
                                <asp:Label ID="lblConfiguared" runat="server" ToolTip="Configured State" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>--%>


                                                </div>
                                            </div>
                                            <%--<div class="col-6">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Discontinue: &nbsp;
                                <asp:Label ID="lblDiscontinue" runat="server" ToolTip="Discontinue" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Active: &nbsp;
                                <asp:Label ID="lblActive" runat="server" ToolTip="Active State" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                </div>

                                            </div>--%>
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

                                            <h4>Generate FCI Report</h4>
                                            <asp:LinkButton ID="lbtndownload" OnClick="lbtndownload_Click" ToolTip="Download Report" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white ml-2">
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
                                        <asp:Label runat="server" CssClass="form-control-label">Coming Soon.</asp:Label><br />

                                    </div>
                                    <div class="col-auto">

                                        <asp:LinkButton ID="lbtnview" OnClick="lbtnview_Click" ToolTip="View Instructions" runat="server" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" OnClick="lbtndwnldinst_Click" ID="lbtndwnldinst" class="btn btn bg-gradient-green btn-sm text-white" ToolTip="Download Instructions">
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
            <div class="col-xl-6 order-xl-1">
                <asp:Panel ID="pnlStationDetails" runat="server" Visible="true">
                    <div class="card" style="min-height: 470px">
                        <div class="card-header border-bottom row m-0">
                            <h3 class="mb-0">FCI List</h3>
                        </div>
                        <div class="card-body p-0">
                            <div class="row m-0 align-items-center">

                                <div class="col-lg-4 pr-0 mt-2">
                                    <h6 class="form-control-label text-muted my-0">Route</h6>
                                    <asp:DropDownList ID="ddlsRoute" ToolTip="State" CssClass="form-control form-control-sm" runat="server" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-lg-4 mt-2">
                                    <h6 class="form-control-label text-muted my-0">Service Type</h6>
                                    <asp:DropDownList ID="ddlsServiceTYpe" ToolTip="District" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-lg-4 mt-4">
                                    <asp:LinkButton runat="server" CssClass="btn btn-success btn-sm"><i class="fa fa-search"></i> Search</asp:LinkButton>
                                    <asp:LinkButton runat="server" CssClass="btn btn-danger btn-sm"><i class="fa fa-times"></i> Reset</asp:LinkButton>
                                </div>
                                <div class="col-lg-12">

                                    <asp:GridView runat="server"></asp:GridView>
                                    <asp:Panel runat="server" ID="pnlNoRecord" Visible="true" CssClass="text-center" Width="100%">
                                        <p class="text-center" style="font-size: 32px; font-weight: bold; color: #e3e3e3; margin-top: 50px;">
                                            FCI List Not Available
                                        </p>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div class="col-xl-6 order-xl-2">
                <asp:Panel runat="server" ID="pnlAddStation" Visible="true">
                    <div class="card" style="min-height: 470px">
                        <div class="card-header border-bottom">
                            <div class="row m-0">
                                <div class="col-md-8 text-left">
                                    <h3 class="mb-0" runat="server" id="lblAddStationHeader" visible="true">Add New FCI</h3>
                                </div>
                                <%--  <div class="col-md-4 text-right">
                                    <p style="font-size: small; margin: 0px;" class="text-danger font-weight-bold">All marked * Fields are mandatory</p>
                                </div>--%>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row m-0 align-items-center">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label"> Depot<span style="color: red">*</span></asp:Label>
                                    <asp:DropDownList ID="ddlDepot" ToolTip="Station State" CssClass="form-control form-control-sm" runat="server" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">Service Type<span style="color: red">*</span></asp:Label>
                                    <asp:DropDownList ID="ddlServicetype" ToolTip="Station District" CssClass="form-control form-control-sm" runat="server" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row m-0 align-items-center">
                                <div class="col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label"> Route<span style="color: red">*</span></asp:Label>
                                    <asp:DropDownList ID="ddlRoute" OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged" ToolTip="Station State" CssClass="form-control form-control-sm" runat="server" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">Depot Service<span style="color: red">*</span></asp:Label>
                                    <asp:DropDownList ID="ddldepotservice" ToolTip="Station District" CssClass="form-control form-control-sm" runat="server" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row m-0 align-items-center mt-2">
                                <div class="col-lg-12">
                                    
                                    <asp:LinkButton runat="server" ID="lbtnSave" OnClick="lbtnSave_Click" CssClass="btn btn-danger btn-sm float-right ml-2"><i class="fa fa-times"> </i> Reset</asp:LinkButton>
                            <asp:LinkButton runat="server" OnClick="Unnamed_Click" CssClass="btn btn-success btn-sm float-right ml-2"><i class="fa fa-rupee-sign"> </i> Genearate</asp:LinkButton>
                                  
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

