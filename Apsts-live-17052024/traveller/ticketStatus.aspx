<%@ Page Title="" Language="C#" MasterPageFile="~/traveller/trvlrMaster.master" AutoEventWireup="true" CodeFile="ticketStatus.aspx.cs" Inherits="traveller_ticketStatus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/travelllerStepProgressBar.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="header mt-3">
          <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="container-fluid">

            <div class="row shadow mb-2">
                <div class="col-10 offset-1 pt-3">
                    <ul id="progressbar">
                        <li class="text-center active" id="search_pr"><strong>Search</strong></li>
                        <li class="text-center active" id="seat_pr"><strong>Seat Selection</strong></li>
                        <li class="text-center active" id="confirm_pr"><strong>Confirmation</strong></li>
                        <li class="text-center active" id="payment_pr"><strong>Payment</strong></li>
                        <li class="text-center active" id="status_pr"><strong>Finish</strong></li>
                    </ul>
                </div>
            </div>
            <div class="card">
                <div class="row">
                    <div class="col-lg-3 py-3" style="background: #f3fff1;">
                        <h3 class="mb-1 text-center">Booking was for</h3>
                        <p class="mb-0 text-left" style="font-size: 13px;">
                            <span class="text-muted">From </span><br />
                            <asp:Label ID="lblFromStation" runat="server" Text="NA" CssClass="text-uppercase h4"></asp:Label><br />
                            <span class="text-muted">To</span><br />
                            <asp:Label ID="lblToStation" runat="server" Text="NA" CssClass="text-uppercase h4"></asp:Label><br />
                            <span class="text-muted">Service </span><br />
                            <asp:Label ID="lblServiceType" runat="server" Text="NA" CssClass="text-uppercase h4"></asp:Label><br />
                            <span class="text-muted">Date </span><br />
                            <asp:Label ID="lblDate" runat="server" Text="NA" CssClass="text-uppercase h4"></asp:Label><asp:Label ID="lblDeparture" runat="server" CssClass="text-uppercase pl-2" Text="NA"></asp:Label><br />
                            <span class="text-muted">Boarding  </span><br />
                            <asp:Label ID="lblBoardingStation" runat="server" Text="NA" CssClass="text-uppercase h4"></asp:Label>
                        </p>
                        <h3 class="mb-1 mt-2">Passengers</h3>
                        <asp:ListView ID="gvPassengers" runat="server">
                            <ItemTemplate>
                                <p class="mb-0 text-left" style="font-size: 13px;">
                                    Seat No. <%# Eval("seatno") %> : <%# Eval("travellername") %>, <%# Eval("travellergender") %>, <%# Eval("travellerage") %> Year
                                </p>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    <div class="col-lg-9 py-3 pl-3" style="min-height: 50vh;">
                        <asp:Panel ID="pnlSuccess" runat="server">
                            <div class="row justify-content-md-center">
                                <div class="col col-lg-2">
                                </div>
                                <div class="col-md-auto px-5">
                                    <div class="row">
                                        <div class="col-lg-12 text-center mt-4">
                                            <i class="fa fa-ticket-alt fa-4x text-success"></i>
                                        </div>
                                        <div class="col-lg-12 text-center mt-2">
                                            <br />
                                            <span class="h2 font-weight-bold mb-0">Thanks for being here.<br />
                                                Your ticket booking has been done successfully
                                            </span>
                                            <br />
                                            <span class="h2 font-weight-bold mb-0">Your Ticket No is
                                        <asp:Label ID="lblTicketNo" runat="server" CssClass="text-success"></asp:Label>
                                            </span>
                                            <p class="mt-3">
                                                <asp:LinkButton ID="lbtnHome" runat="server" CssClass="btn btn-outline-primary" OnClick="lbtnHome_Click">
                                            <i class="fa fa-home"></i> Traveller Dashboard
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnPrint" runat="server" CssClass="btn btn-outline-warning" OnClick="lbtnPrint_Click">
                                            <i class="fa fa-download"></i> Download Ticket
                                                </asp:LinkButton>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                                <div class="col col-lg-2">
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlFail" runat="server">
                            <div class="row justify-content-md-center">
                                <div class="col col-lg-2">
                                </div>
                                <div class="col-md-auto px-5">
                                    <div class="row">
                                        <div class="col-lg-12 text-center mt-4">
                                            <i class="fa fa-ticket-alt fa-4x text-danger"></i>
                                        </div>
                                        <div class="col-lg-12 text-center mt-4">
                                            <span class="h2 font-weight-bold mb-0">Your ticket status is failed. Please try again.<br />
                                                Feel free to contact the helpdesk if you have any doubts about ticket status
                                            </span>
                                            <p class="mt-3">
                                                <a class="btn btn-outline-primary" href="dashboard.aspx">
                                                    <i class="fa fa-home"></i>Traveller Dashboard
                                                </a>
                                            </p>
                                        </div>
                                        <div class="col-lg-12 text-right mt-4">
                                            <asp:LinkButton ID="Refresh" runat="server" CssClass="btn btn-outline-primary btn-sm" href="dashboard.aspx">
                                                <i class="fa fa-history"></i> Refresh
                                            </asp:LinkButton>
                                            for reconfirm ticket status.
                                        </div>
                                    </div>
                                </div>
                                <div class="col col-lg-2">
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--Model Pop-->

    <cc1:ModalPopupExtender ID="mpePage" runat="server" PopupControlID="pnlPage" TargetControlID="Button21"
        CancelControlID="LinkButton71" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPage" runat="server" Style="position: fixed;">
        <div class="modal-content mt-5" style="width: 98%; margin-left: 2%; text-align:center">
            <div class="modal-header" >
                <div class="col-md-7">
                    <h3 id="lblTitle" runat="server" class="m-0"> </h3>
                </div>
                <div class="col-md-4 text-right" style="text-align: end;">
                </div>
                <div class="col-md-1 text-right" style="text-align: end;">
                    <asp:LinkButton ID="LinkButton71" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="24px">X</asp:LinkButton>
                </div>
            </div>
            <div class="modal-body" style="background-color: #ffffff; overflow-y: auto; padding: 1px; text-align:center">
                <embed src="" style="height: 85vh; width: 55vw;" runat="server" id="embedPage" />
            </div>
        </div>
        <br />
        <div style="visibility: hidden;">
            <asp:Button ID="Button21" runat="server" Text="" />
        </div>
    </asp:Panel>

    <!-- ModalPopupExtender -->
</asp:Content>

