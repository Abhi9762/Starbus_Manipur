<%@ Page Title="" Language="C#" MasterPageFile="~/traveller/trvlrMaster.master" AutoEventWireup="true" CodeFile="trackBus.aspx.cs" Inherits="traveller_trackBus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid mt-4">
        <div class="row">
            <div class="col-xl-5">
                <div class="card">
                    <div class="card-header bg-transparent border-0">
                        <div class="row align-items-center">
                            <div class="col">
                                <h5 class="h3 text-black-50 mb-0">Live Tickets</h5>
                            </div>
                            <div class="col-auto text-right">
                                <h5 class="h3 text-black-50 mb-0"></h5>
                            </div>
                        </div>
                    </div>
                    <div class="card-body" style="min-height: 50vh !important">
                        <asp:GridView ID="gvLiveTickets" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
                            DataKeyNames="_ticketno,booked_by">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div class="row px-4 py-2 border-top">
                                            <div class="col">
                                                <span class="h3 font-weight-bold mb-0"><%# Eval("_ticketno") %> <span class="mb-0 text-xs text-muted">( Journey <%# Eval("journeydate") %>)</span></span>
                                                <h5 class="card-title text-xs text-uppercase text-muted mb-0"><%# Eval("fromstn_name") %> - <%# Eval("tostn_name") %></h5>
                                            </div>
                                            <div class="col-auto text-right ">
                                                <asp:LinkButton ID="lbtnRate" runat="server" CssClass="btn btn-icon btn-primary btn-sm"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="VIEWDETAIL">
                                                    <i class="fa fa-star-half-alt"></i> Rate Us
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Panel ID="pnlNoTickets" runat="server">
                            <div class="row">
                                <div class="col-lg-12 text-center mt-4">
                                    <i class="ni ni-bus-front-12 ni-5x text-muted"></i>
                                </div>
                                <div class="col-lg-12 text-center mt-4">
                                    <p class="h2 font-weight-bold mb-0">
                                        Thanks for being here.<br />
                                        No ticket available for bus tracking.
                                    </p>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <div class="col-xl-7">
                <div class="card">
                    <div class="card-header bg-transparent border-0">
                        <div class="row align-items-center">
                            <div class="col">
                                <h5 class="h3 text-black-50 mb-0"></h5>
                            </div>
                            <div class="col-auto text-right">
                                <h5 class="h3 text-black-50 mb-0"></h5>
                            </div>
                        </div>
                    </div>
                    <div class="card-body" style="min-height: 50vh !important">
                        <asp:Panel ID="Panel1" runat="server">
                            <div class="row">
                                <div class="col-lg-12 text-center mt-4">
                                    <i class="fa fa-map fa-6x"></i>
                                </div>
                                <div class="col-lg-12 text-center mt-4">
                                    <p class="h2 font-weight-bold mb-0 mt-2">
                                       Map will be here.<br />
                                        If you are viewing the ticket list in the left panel then select the ticket for the track.
                                    </p>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

