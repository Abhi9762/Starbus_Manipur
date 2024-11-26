<%@ Page Title="" Language="C#" MasterPageFile="~/traveller/trvlrMaster.master" AutoEventWireup="true" CodeFile="ticketDetail.aspx.cs" Inherits="traveller_ticketDetail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                                    <div class="col-xl-3 col-md-3 pl-3 py-2 collapse">
                                        <div class="form-group mb-0">
                                            <div class="input-group input-group-sm">
                                                <asp:TextBox ID="tbTicketSearch" runat="server" type="text" class="form-control" placeholder="Ticket No." MaxLength="20"></asp:TextBox>
                                                <div class="input-group-append">
                                                    <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="input-group-text" OnClick="lbtnSearch_Click">
                                                           <i class="fa fa-search"></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xl-12 pl-3">
                                        <h4 class="text-white-50 mb-1 mt-1 text-uppercase">Transaction/PNR Number Number 
                                            <asp:Label ID="lblTicketNo" runat="server" CssClass="text-white" Text=""></asp:Label>
                                            status
                                            <asp:Label ID="lblTicketStatus" runat="server" CssClass="" Text="NA"></asp:Label>
                                        </h4>
                                        <h4 class="text-white-50 mb-1 mt-1 text-uppercase">Transaction was for booking from
                                            <asp:Label ID="lblFromStation" runat="server" CssClass="text-white" Text="NA"></asp:Label>
                                            to 
                                            <asp:Label ID="lblToStation" runat="server" CssClass="text-white" Text="NA"></asp:Label>
                                            for date
                                            <asp:Label ID="lblDate" runat="server" CssClass="text-white" Text="NA"></asp:Label>
                                            departure 
                                            <asp:Label ID="lblDeparture" runat="server" CssClass="text-white" Text="NA"></asp:Label>
                                        </h4>
                                        <h4 class="text-white-50 mb-1 mt-1 text-uppercase">Your service type
                                            <asp:Label ID="lblServiceType" runat="server" CssClass="text-white" Text="NA"></asp:Label>
                                        </h4>
                                        <h4 class="text-white-50 mb-1 mt-1 text-uppercase">Your boarding station is
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
            <div class="col-xl-5">
                <div class="card">
                    <div class="card-header bg-transparent">
                        <div class="row align-items-center">
                            <div class="col">
                                <h5 class="h3 text-black-50 mb-0">Passengers</h5>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <asp:Panel ID="pnlDetail" runat="server">
                            <asp:ListView ID="gvPassengers" runat="server">
                                <ItemTemplate>
                                    <div class="card">
                                        <div class="card-body px-2 py-1">
                                            <span class="text-md"><%# Eval("travellername") %>, <%# Eval("travellergender") %>, <%# Eval("travellerage") %> Year</span>
                                            <span class="font-weight-600 text-md float-right">Seat No. <%# Eval("seatno") %></span>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                            <div class="row px-2 pt-4">
                                <div class="col-xl-3">
                                    <asp:LinkButton ID="lbtnSMS" runat="server" CssClass="btn btn-success btn-sm w-100"  OnClick="lbtnSMS_Click">
                                    <i class="fa fa-sms"></i> Resend SMS
                                    </asp:LinkButton>
                                </div>
                                <div class="col-xl-3">
                                    <asp:LinkButton ID="lbtnEmail" runat="server" CssClass="btn btn-info btn-sm w-100">
                                    <i class="fa fa-envelope"></i> Resend Email
                                    </asp:LinkButton>
                                </div>
                                <div class="col-xl-3">
                                    <asp:LinkButton ID="lbtnTrack" runat="server" CssClass="btn btn-warning btn-sm w-100">
                                    <i class="fa fa-map-marked"></i> Track Bus
                                    </asp:LinkButton>
                                </div>
                                <div class="col-xl-3">
                                    <asp:LinkButton ID="lbtnPrintTicket" runat="server" CssClass="btn btn-primary btn-sm w-100" OnClick="lbtnPrintTicket_Click">
                                    <i class="fa fa-ticket-alt"></i> e-Ticket
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlNoRecordDetail" runat="server">
                            <div class="row">
                                <div class="col-lg-12 text-center mt-4">
                                    <i class="fa fa-ticket-alt fa-4x text-muted"></i>
                                </div>
                                <div class="col-lg-12 text-center mt-4">
                                    <span class="h2 font-weight-bold mb-0">Passengers detail not available.<br />
                                        Please contact to helpdesk.
                                    </span>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <div class="col-xl-3">
                <div class="card">
                    <div class="card-header bg-transparent">
                        <div class="row align-items-center">
                            <div class="col">
                                <h5 class="h3 text-black-50 mb-0">Fare</h5>
                            </div>
                        </div>
                    </div>
                    <div class="card-body" style="min-height: 50vh !important">
                        <div class="row px-3">
                            <div class="col-xl-12">
                                <span class="text-sm text-default mb-0 font-weight-300">Fare
                                </span>
                                <span class="text-md float-right font-weight-bold mb-0">
                                    <asp:Label ID="lblFareAmt" runat="server" Text="NA"></asp:Label>
                                    <i class="fa fa-rupee-sign"></i>
                                </span>
                            </div>
                            <div class="col-xl-12 mt-1">
                                <span class="text-sm text-default mb-0 font-weight-300">Reservation
                                </span>
                                <span class="text-md float-right font-weight-bold mb-0">
                                    <asp:Label ID="lblReservationCharge" runat="server" Text="NA"></asp:Label>
                                    <i class="fa fa-rupee-sign"></i>
                                </span>
                            </div>
                            <div class="col-xl-12 p-0">
                                <asp:GridView ID="grdtax" runat="server" ShowHeader="false" GridLines="None" Width="100%" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="col-xl-12 mt-1">
                                                    <span class="text-sm text-default mb-0 font-weight-300"><%# Eval("taxname")%>
                                                    </span>
                                                    <span class="text-md float-right font-weight-bold mb-0">
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("taxamt")%>'></asp:Label>
                                                        <i class="fa fa-rupee-sign"></i>
                                                    </span>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-xl-12 mt-1 pt-1" style="border-top: 0.5px dotted gray;">
                                <span class="text-sm text-default mb-0 font-weight-300">Discount
                                </span>
                                <span class="text-md float-right font-weight-bold mb-0">
                                    <asp:Label ID="lblDiscountAmnt" runat="server" Text="0"></asp:Label>
                                    <i class="fa fa-rupee-sign"></i>
                                </span>
                            </div>

                            <div class="col-xl-12 text-center py-3">
                                <span class="h3 text-danger mb-0 font-weight-600">Total Amount
                                </span>
                                <span class="h3 font-weight-bold mb-0">
                                    <asp:Label ID="lblTotal" runat="server" Text="NA"></asp:Label>
                                    <i class="fa fa-rupee-sign"></i>
                                </span>
                            </div>
                            <div class="col-xl-12">
                                <asp:Label ID="fareYN" runat="server" Text="(Fare will be collected by conductor in the bus)"
                                    Style="color: Red; font-size: 8pt;" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-xl-4">
                <div class="card">
                    <div class="card-header bg-transparent">
                        <div class="row align-items-center">
                            <div class="col">
                                <h5 class="h3 text-black-50 mb-0">Transaction Log of Ticket</h5>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="gvticketlog" runat="server" AutoGenerateColumns="false" GridLines="None"
                            ShowHeader="false" Width="100%">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div class="row py-1 border-bottom">
                                            <div class="col">
                                                <span class="text-sm text-default"><%# Eval("ticket_status_name")%>
                                                </span>
                                            </div>
                                            <div class="col-auto text-right">
                                                <span class="text-xs">
                                                    <%# Eval("current_status_datetime")%>
                                                </span>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                        </asp:GridView>
                        <asp:Panel ID="pnlNoTicketLog" runat="server">
                            <div class="row">
                                <div class="col-lg-12 text-center mt-4">
                                    <p class="text-center" style="font-size: 32px; font-weight: bold; color: #e3e3e3;">
                                        There is no cancelled seat in this ticket
                                    </p>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="card">
                    <div class="card-header bg-transparent">
                        <div class="row align-items-center">
                            <div class="col">
                                <h5 class="h3 text-black-50 mb-0">Cancellations</h5>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row px-2">
                            <div class="col-xl-12 py-1">
                                <asp:GridView ID="grdcancelledtkt" runat="server" Visible="false" ShowHeader="false" Width="100%" OnRowDataBound="grdcancelledtkt_RowDataBound"
                                    AutoGenerateColumns="False" DataKeyNames="cancellation_ref_no,ticket_no,cancellation_date,refund_ref_no"
                                    OnRowCommand="grdcancelledtkt_RowCommand" GridLines="None">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="row pb-1 mb-1 border-bottom">
                                                    <div class="col">
                                                        <asp:Label ID="lblcancelRefno" runat="server" Text='<%# Eval("cancellation_ref_no") %>'></asp:Label>&nbsp;<span
                                                            style="color: #a29e9e; font-size: 12px;">Cancellation Ref. No.</span><br />
                                                        <asp:Label ID="lblcanceldate" runat="server" Text='<%# Eval("cancellation_date") %>'></asp:Label>&nbsp;<span
                                                            style="color: #a29e9e; font-size: 12px;">Cancel Date/Time</span>
                                                    </div>
                                                    <div class="col-auto text-right">
                                                        <asp:UpdatePanel runat="server" ID="UpdatePanelall" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="lbtnprintvoucher" runat="server" CssClass="btn btn-success btn-sm" CommandName="PrintVoucher" data-toggle="tooltip" data-placement="bottom" title="Print Cancellaion Voucher"
                                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'> 
                                                                            <i class="fa fa-file" ></i> </asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnrefund" runat="server" CssClass="btn btn-warning btn-sm" CommandName="Refund" data-toggle="tooltip" data-placement="bottom" title="Refund Status"
                                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'> <i class="fa fa-rupee-sign"></i> </asp:LinkButton>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="lbtnprintvoucher" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:Panel ID="pnlNocancelledtkt" runat="server">
                                    <div class="row">
                                        <div class="col-lg-12 text-center mt-4">
                                            <p class="text-center" style="font-size: 32px; font-weight: bold; color: #e3e3e3;">
                                                There is no cancelled seat in this ticket
                                            </p>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>

    </div>
    
    <cc1:ModalPopupExtender ID="mpePage" runat="server" PopupControlID="pnlPage" TargetControlID="Button21"
        CancelControlID="LinkButton71" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPage" runat="server" Style="position: fixed;">
        <div class="modal-content mt-5" style="width: 98%; margin-left: 2%; text-align:center">
            <div class="modal-header" >
                <div class="col-md-7">
                    <h3 id="lblTitle" runat="server" class="m-0 text-left"> </h3>
                </div>
                <div class="col-md-4 text-right" style="text-align: end;">
                </div>
                <div class="col-md-1 text-right" style="text-align: end;">
                    <asp:LinkButton ID="LinkButton71" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="24px">X</asp:LinkButton>
                </div>
            </div>
            <div class="modal-body" style="background-color: #ffffff; overflow-y: auto; padding: 1px; text-align:center">
                <embed src="" style="height: 90vh; width: 55vw;" runat="server" id="embedPage" />
            </div>
        </div>
        <br />
        <div style="visibility: hidden;">
            <asp:Button ID="Button21" runat="server" Text="" />
        </div>
    </asp:Panel>
</asp:Content>

