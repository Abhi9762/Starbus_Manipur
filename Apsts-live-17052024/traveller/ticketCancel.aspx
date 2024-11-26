<%@ Page Title="" Language="C#" MasterPageFile="~/traveller/trvlrMaster.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="ticketCancel.aspx.cs" Inherits="traveller_ticketCancel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <asp:HiddenField ID="hidtoken" runat="server" />
    <div class="container-fluid pt-2">
        <div class="row">
            <div class="col-xl-5 order-xl-1">
                <div class="card">
                    <div class="card-header border-bottom">
                        <div class="row">
                            <div class="col">
                                <h3 class="mb-0 text-primary">Tickets For Cancellation</h3>
                            </div>
                            <div class="col-auto text-right">
                                <i class="fa fa-info-circle text-muted" data-toggle="modal" data-target="#CPModal"></i>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                          <asp:GridView ID="gvTickets" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
                            DataKeyNames="ticket_no,book_datetime,journey_date,src,dest,traveller_mobile_no_"
                            OnRowCommand="gvTickets_RowCommand" OnPageIndexChanging="gvTickets_PageIndexChanging" AllowPaging="true" PageSize="5" OnRowDataBound="gvTickets_RowDataBound">
                            <PagerStyle CssClass="pagination-ys" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div class="row px-2 shadow-lg--hover border-bottom pb-2 mb-2" style="background-color:#f4f5f8;">
                                            <div class="col">
                                                <span class="h3 font-weight-bold mb-0"><%# Eval("ticket_no") %><%# Eval("current_status").ToString() == "A" ? "<i class='fa fa-check-circle ml-2 text-success'></i>" : "<i class='fa fa-times-circle ml-2 text-danger'></i>"  %> </span>
                                                <h5 class="card-title text-xs text-uppercase text-muted mb-0"><%# Eval("src") %> - <%# Eval("dest") %></h5>
                                                <%-- <p class="mb-0 text-xs">Ticket Amount ₹ <%# Eval("Amount") %> </p>--%>
                                            </div>
                                            <div class="col text-left">
                                                <p class="mb-0 text-xs">Booking <%# Eval("book_datetime") %> </p>
                                                <p class="mb-0 text-xs">Journey <%# Eval("journey_date") %>&nbsp;<%# Eval("depart") %></p>
                                            </div>
                                            <div class="col-auto text-right pt-2">
                                                <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="btn btn-icon btn-danger btn-sm"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="CANCELLATION" data-toggle="tooltip" data-placement="bottom" title="Cancel Ticket">
                                                    <i class="fa fa-times-circle"></i>
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
                                    <span class="h2 font-weight-bold mb-0">Thanks for being here</span>
                                    <h5 class="card-title text-uppercase text-muted mb-0">
                                        <asp:Label ID="lblNoTicketsMsg" runat="server"></asp:Label>
                                    </h5>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="card">
                    <div class="card-header border-bottom">
                        <div class="row">
                            <div class="col">
                                <h3 class="mb-0 text-primary">Cancelled Tickets  <i class="fa fa-info-circle"
                                    style="font-size: 15px; color: #c6c2c2;" data-toggle="tooltip" data-placement="top"
                                    title="Details are available, Click On Print Cancellation Voucher"></i></h3>
                            </div>
                            <div class="col-auto text-right">
                                <div class="form-group mb-0">
                                    <div class="input-group">
                                        <asp:TextBox ID="tbcancelledpnr" runat="server" AutoComplete="Off" class="form-control form-control-sm  " placeholder="Search ticket number" MaxLength="20"></asp:TextBox>
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="lbtncacnelledpnrsearch" runat="server" OnClick="lbtncacnelledpnrsearch_Click" CssClass="btn btn-success btn-icon-only btn-sm mr-1 " strle="z-i">
                                            <i class="fa fa-search mt-2"></i>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="lbtncacnelledpnrreset" runat="server" OnClick="lbtncacnelledpnrreset_Click" CssClass="btn btn-danger btn-icon-only btn-sm mr-1">
                                            <i class="fa fa-undo mt-2"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="grdcancelledtkt" runat="server" Visible="false" AllowPaging="true" ShowHeader="false" Width="100%" OnRowDataBound="grdcancelledtkt_RowDataBound"
                            AutoGenerateColumns="False" PageSize="2" DataKeyNames="cancellation_ref_no,ticket_no,cancellation_date,refund_ref_no"
                            OnRowCommand="grdcancelledtkt_RowCommand" OnPageIndexChanging="grdcancelledtkt_PageIndexChanging" GridLines="None">
                            <PagerStyle CssClass="pagination-ys" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div class="row px-2 shadow-lg--hover border-bottom pb-2 mb-2" style="background-color:#f4f5f8;">
                                            <div class="col">
                                                <asp:Label ID="lblcancelRefno" runat="server" Text='<%# Eval("cancellation_ref_no") %>'></asp:Label>&nbsp;<span
                                                    style="color: #a29e9e; font-size: 12px;">Cancellation Ref. No.</span><br />
                                                <asp:Label ID="lblUTCTRANSACTIONREFNO" runat="server" Text='<%# Eval("ticket_no") %>'></asp:Label>
                                                &nbsp;<span style="color: #a29e9e; font-size: 12px;">PNR No.</span><br />
                                                <asp:Label ID="lblcanceldate" runat="server" Text='<%# Eval("cancellation_date") %>'></asp:Label>&nbsp;<span
                                                    style="color: #a29e9e; font-size: 12px;">Cancel Date/Time</span>
                                            </div>
                                            <div class="col-auto text-right pt-2">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanelall" UpdateMode="Always">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="lbtnprintvoucher" runat="server" CssClass="btn btn-success" CommandName="PrintVoucher"
                                                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="padding: 10px;"
                                                            ToolTip="Print Cancellaion Voucher"> <i class="fa fa-file" title="Print Cancellaion Voucher"></i> </asp:LinkButton>
                                                       
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
                                        Details not available for Cancelled Tickets
                                    </p>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <div class="col-xl-7 order-xl-2">
                <div class="card" style="min-height: 80vh">
                    <div class="card-header">
                        <div class="row align-items-center">
                            <div class="col">
                                <h3 class="mb-0 text-primary">Ticket Details For Cancellation</h3>
                            </div>
                            <div class="col">
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <asp:Panel ID="pnltktdetails" runat="server" Visible="false">
                            <h6 class="heading-small my-0">Joueney Details</h6>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted font-weight-normal">Booked By</span>
                                    <asp:Label ID="lblUsr" runat="server" CssClass="  mb-0 font-weight-bold text-xs" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted font-weight-normal">Total Amout (Including Taxes)</span>
                                    <asp:Label ID="lblTotal" CssClass="  mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                    <i class="fa fa-rupee-sign"></i>
                                </div>
                                <div class="col-lg-4"></div>

                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted font-weight-normal">PNR No.</span>
                                    <asp:Label ID="lblPNRNO" runat="server" CssClass="  mb-0 font-weight-bold text-xs" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted font-weight-normal">Service Code/Name</span>
                                    <asp:Label ID="lblService" CssClass="  mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted font-weight-normal">Date of Booking</span>
                                    <asp:Label ID="lblBookingDate" CssClass="  mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted font-weight-normal">Scheduled Departure</span>
                                    <asp:Label ID="lblScheduledDeparture" runat="server" CssClass="  mb-0 font-weight-bold text-xs" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted font-weight-normal">Scheduled Arrival</span>
                                    <asp:Label ID="lblScheduledArrival" CssClass="  mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted font-weight-normal">Distance(in KM)</span>
                                    <asp:Label ID="lblDistance" CssClass="  mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted font-weight-normal">From</span>
                                    <asp:Label ID="lblFrom" runat="server" CssClass="  mb-0 font-weight-bold text-xs" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted font-weight-normal">To</span>
                                    <asp:Label ID="lblTo" runat="server" CssClass="  mb-0 font-weight-bold text-xs" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted font-weight-normal">Date of Journey</span>
                                    <asp:Label ID="lblJourneyDate" CssClass="  mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted font-weight-normal">Fare Amount</span>
                                    <asp:Label ID="lblFare" CssClass="  mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                    <i class="fa fa-rupee-sign"></i>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted font-weight-normal">Reservation Charge</span>
                                    <asp:Label ID="lblRes" CssClass="  mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                    <i class="fa fa-rupee-sign"></i>
                                </div>
                                <div class="col-lg-4">
                                    <span class="form-control-label text-muted font-weight-normal">Tax Amount</span>
                                    <asp:Label ID="lbltottax" CssClass="  mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                    <i class="fa fa-rupee-sign"></i>
                                </div>
                            </div>
                            <h6 class="heading-small mb-0 mt-4">Passenger Details</h6>
                            <div class="row pl-lg-3 pr-lg-3">
                                <div class="col-lg-12">


                                    <asp:GridView ID="gvTicketseatDetails" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvTicketseatDetails_RowDataBound"
                                        GridLines="None" CssClass="table   text-sm" DataKeyNames="seatno,amountrefunded">
                                        <Columns>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkseats" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="seatno" HeaderText="Seat No."></asp:BoundField>
                                            <asp:BoundField DataField="travellername" HeaderText="Passenger"></asp:BoundField>
                                            <asp:BoundField DataField="travellergender" HeaderText="Gender"></asp:BoundField>
                                            <asp:BoundField DataField="travellerage" HeaderText="Age"></asp:BoundField>
                                            <asp:BoundField DataField="amounttotal" HeaderText="Fare Amount"></asp:BoundField>
                                            <asp:BoundField DataField="amountrefunded" HeaderText="Refund Amount"></asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                            <div class="row pl-lg-3 pr-lg-3 mt-4">
                                <div class="col-lg-12 text-center">
                                    <asp:LinkButton ID="lbtncanceltkt" runat="server" OnClick="lbtncanceltkt_Click" CssClass="btn btn-success" Style="border-radius: 4px; font-size: 11pt; font-family: verdana;">  Cancel Ticket</asp:LinkButton>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlNoRecord" Visible="true" CssClass="text-center" Width="100%">
                            <p class="text-center" style="font-size: 32px; font-weight: bold; color: #e3e3e3; margin-top: 50px;">
                                Ticket detail will be here<br />
                                Please select ticket from the list on the left
                            </p>
                        </asp:Panel>
                    </div>
                </div>
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
                            <asp:LinkButton ID="lbtnYesConfirmation" runat="server" OnClientClick="return ShowLoading()" OnClick="lbtnYesConfirmation_Click" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
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
    <div class="modal fade" id="CPModal" tabindex="-1" role="dialog" aria-labelledby="CPModalLabel"
        aria-hidden="true">
        <div class="modal-dialog  modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="col-md-10">
                        <h3 class="m-0">Cancellation Policy</h3>
                    </div>
                    <div class="col-md-2 text-right">
                        <a usesubmitbehavior="false" data-dismiss="modal" tooltip="Close" style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></a>
                    </div>
                </div>
                <div class="modal-body">
                    <ul class="list-group" style="font-size: 13px; font-family: verdana; color: #b10021;">
                        <li class="list-group-item">Cancellation/Refund/Rescheduling Ticket booked through Online,
                                    refund will be done to their respective Credit Cards/Debit Cards/Bank Accounts according
                                    to the Bank procedure. No refund will be done at UTC ticket booking counters
                                    
                        </li>
                        <li class="list-group-item">
                            <asp:Label ID="lblcancellationpolicy" runat="server"></asp:Label>
                        </li>
                        <li class="list-group-item">Cancellation is not allowed after Up to 2 hr before Schedule
                                    service start time at origin of the bus. From the date of journey.</li>
                        <li class="list-group-item">Reservation Fee is non-refundable except in case of 100%
                                    cancellation of tickets, if the service is cancelled by UTC for operational or any
                                    other reasons.</li>
                        <li class="list-group-item">Passengers will be given normally in one month, after the
                                    cancellation of ticket or receipt of e-mail. If refunds are delayed more than a
                                    month, passengers may contact helpline telephone number at 8476007605 E-Mail help[dot]UTConline[at]gmail[dot]com.</li>
                        <li class="list-group-item">Payment Gateway Service charges will not be refunded for
                                    service cancellation/ failure transactions in e-ticketing.</li>
                        <li class="list-group-item">Partial cancellation is allowed for which cancellation terms
                                    & conditions will apply.</li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

