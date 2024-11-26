<%@ Page Title="" Language="C#" MasterPageFile="~/traveller/trvlrMaster.master" AutoEventWireup="true" CodeFile="ticketSuccess.aspx.cs" Inherits="traveller_ticketSuccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="header bg-primary pb-5">
        <div class="container-fluid">
            <div class="header-body">
                <div class="row ">
                    <div class="col-xl-12 col-md-12 text-white">
                        <div class="card bg-transparent">
                            <div class="card-body bg-transparent">
                                <div class="row">
                                    <div class="col pl-3">
                                        <h4 class="text-white-50 mb-1 mt-1 text-uppercase">you are booking a ticket from
                                            <asp:Label ID="lblFromStation" runat="server" CssClass="text-white" Text="NA"></asp:Label>
                                            to 
                                            <asp:Label ID="lblToStation" runat="server" CssClass="text-white" Text="NA"></asp:Label>
                                            for date
                                            <asp:Label ID="lblDate" runat="server" CssClass="text-white" Text="NA"></asp:Label>
                                            departure 
                                            <asp:Label ID="lblDeparture" runat="server" CssClass="text-white" Text="NA"></asp:Label>
                                        </h4>
                                        <h4 class="text-white-50 mb-1 mt-1 text-uppercase">your service type
                                            <asp:Label ID="lblServiceType" runat="server" CssClass="text-white" Text="NA"></asp:Label>
                                        </h4>
                                        <h4 class="text-white-50 mb-1 mt-1 text-uppercase">your boarding station is
                                            <asp:Label ID="lblBoardingStation" runat="server" CssClass="text-white" Text="NA"></asp:Label>
                                        </h4>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid mt--5">
        <div class="row">
            <div class="col-xl-12">
                <div class="card">                    
                    <div class="card-body" style="min-height: 50vh !important">
                        <div class="row">
                            <div class="col-lg-12 text-center mt-4">
                                <i class="fa fa-ticket-alt fa-4x text-muted"></i>
                            </div>
                            <div class="col-lg-12 text-center mt-4">
                                <span class="h2 font-weight-bold mb-0">Thanks for being here.<br />
                                    Passengers detail not available.<br />
                                    Press the Refresh button given below.
                                </span>
                                <p class="mt-3">
                                    <asp:LinkButton ID="lbtnRefersh" runat="server" CssClass="btn btn-outline-primary">
                                                <i class="fa fa-history"></i> Refresh Here
                                    </asp:LinkButton>
                                </p>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </div>

    </div>

</asp:Content>

