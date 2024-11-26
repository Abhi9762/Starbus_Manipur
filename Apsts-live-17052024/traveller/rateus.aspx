<%@ Page Title="" Language="C#" MasterPageFile="~/traveller/trvlrMaster.master" AutoEventWireup="true" CodeFile="rateus.aspx.cs" Inherits="traveller_rateus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style>
        .input-group-prepend .btn, .input-group-append .btn {
            position: relative;
            z-index: 0;
        }
    </style>
    <style type="text/css">
        .ratingEmpty {
            background-image: url(ratingStars/staroutline.png);
            width: 25px;
            height: 25px;
        }

        .ratingFilled {
            background-image: url(ratingStars/starFilled.png);
            width: 25px;
            height: 25px;
        }

        .ratingSaved {
            background-image: url(ratingStars/staroutline.png);
            width: 25px;
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="main-content" id="panel">
        <asp:HiddenField ID="hidtoken" runat="server" />  
            <div class="container-fluid mt-4">
                <div class="row">
                    <div class="col-xl-4">
                        <div class="card">
                            <div class="card-header bg-transparent border-0">
                                <div class="row align-items-center">
                                    <div class="col">
                                        <h5 class="h3 text-black-50 mb-0">Rate us</h5>
                                    </div>
                                    <div class="col-auto text-right">
                                        <h5 class="h3 text-black-50 mb-0"></h5>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body" style="min-height: 50vh !important">
                                <p class="mb-0">You are here to help us.</p>
                                <p class="mb-0">We are working on improving our services.</p>
                                <p class="mb-0">kindly give us rating for your previous journey. it helps us to improve our services.</p>
                                <p class="mb-0">Rating is mandatory after complation your journey.</p>
                                <p class="mb-0">Your rating is for the servious depending on staff (Driver/Conductor), bus condition and booking portal.</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-8">
                        <div class="card">
                            <div class="card-header bg-transparent border-0">
                                <div class="row align-items-center">
                                    <div class="col">
                                        <h5 class="h3 text-black-50 mb-0">Journey Completed Tickets</h5>
                                    </div>
                                    <div class="col-auto text-right">
                                        <h5 class="h3 text-black-50 mb-0"></h5>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body" style="min-height: 50vh !important">
                                <asp:GridView ID="gvRatingTickets" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
                                    OnRowCommand="gvRatingTickets_RowCommand" DataKeyNames="ticket_no,booked_by">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="row px-4 py-2 border-top">
                                                    <div class="col">
                                                        <span class="h3 font-weight-bold mb-0"><%# Eval("ticket_no") %> <span class="mb-0 text-xs text-muted">( Journey <%# Eval("journey_date") %>)</span></span>
                                                        <h5 class="card-title text-xs text-uppercase text-muted mb-0"><%# Eval("from_station") %> - <%# Eval("to_station") %></h5>
                                                    </div>
                                                    <div class="col-auto text-right ">
                                                        <asp:LinkButton ID="lbtnRate" runat="server" CssClass="btn btn-icon btn-primary btn-sm"
                                                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="VIEWDETAIL">
                                                    <i class="fa fa-star-half-alt"></i> Rate Us
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <asp:Panel ID="pnlRate" runat="server" Visible="false" Style="border-bottom: 4px solid #eedada;">
                                                    <div class="row px-4 py-1">
                                                        <div class="col" style="min-width: 200px;">
                                                            <h5 class="mb-0">Driver/Conductor</h5>
                                                            <cc1:Rating ID="rcStaff" AutoPostBack="true" OnChanged="rcStaff_Changed" runat="server" StarCssClass="ratingEmpty" WaitingStarCssClass="ratingSaved" EmptyStarCssClass="ratingEmpty" FilledStarCssClass="ratingFilled">
                                                            </cc1:Rating>
                                                        </div>
                                                        <div class="col" style="min-width: 200px;">
                                                            <asp:TextBox ID="tbRateStaff" runat="server" Visible="false" CssClass="form-control p-1" TextMode="MultiLine" placeholder="Enter Staff related feedback here.. (Max 100 chars)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row px-4 py-1">
                                                        <div class="col" style="min-width: 200px;">
                                                            <h5 class="mb-0">Bus</h5>
                                                            <cc1:Rating ID="rcBus" AutoPostBack="true" OnChanged="rcBus_Changed" runat="server" StarCssClass="ratingEmpty" WaitingStarCssClass="ratingSaved" EmptyStarCssClass="ratingEmpty" FilledStarCssClass="ratingFilled">
                                                            </cc1:Rating>
                                                        </div>
                                                        <div class="col">
                                                            <asp:TextBox ID="tbRateBus" runat="server" Visible="false" CssClass="form-control p-1" TextMode="MultiLine" placeholder="Enter Bus related feedback here.. (Max 100 chars)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row px-4 py-1">
                                                        <div class="col" style="min-width: 200px;">
                                                            <h5 class="mb-0">Booking Portal</h5>
                                                            <cc1:Rating ID="rcPortal" AutoPostBack="true" OnChanged="rcPortal_Changed" runat="server" StarCssClass="ratingEmpty" WaitingStarCssClass="ratingSaved" EmptyStarCssClass="ratingEmpty" FilledStarCssClass="ratingFilled">
                                                            </cc1:Rating>
                                                        </div>
                                                        <div class="col" style="min-width: 200px;">
                                                            <asp:TextBox ID="tbRatePortal" runat="server" Visible="false" CssClass="form-control p-1" TextMode="MultiLine" placeholder="Enter Portal related feedback here.. (Max 100 chars)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row px-4 py-2 pb-5">
                                                        <div class="col" style="min-width: 200px;">
                                                            <asp:LinkButton ID="lbtnSaveRate" runat="server" CssClass="btn btn-icon btn-success"
                                                                CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="SAVERATING">
                                                    <i class="fa fa-check"></i> Submit
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
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
                                                No ticket available for rating.
                                            </p>
                                            <a href="dashboard.aspx" class="btn btn-outline-primary my-5"><i class="fa fa-home"></i>Home</a>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>

                <%--   <div class="alert alert-default alert-dismissible fade show" role="alert">
                    <span class="alert-icon"><i class="ni ni-like-2"></i></span>
                    <span class="alert-text"><strong>Default!</strong> This is a default alert—check it out!</span>
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>--%>
            </div>
        </div>
</asp:Content>

