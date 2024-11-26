<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Cntrmaster.master" AutoEventWireup="true" CodeFile="CntrSpclTicketCancellation.aspx.cs" Inherits="Auth_CntrSpclTicketCancellation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

<style>
 .table td, .table th{
    font-size: 16px;
    white-space: nowrap;
    padding: 6px;
}


</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidtoken" runat="server" />
    <div class="container-fluid pt-2">
        <div class="row">
            <div class="col-xl-5 order-xl-1">
                <div class="card" style="min-height: 80vh">
                    <div class="card-header border-bottom">
                        <div class="row">
                            <div class="col-md-6">
                                <h3 class="mb-0">Cancellation Policy</h3>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row p-2">
                            <div class="col-md-12">
                                <p>
                                    1. Partial cancellation is allowed for which cancellation terms & conditions will
                                                    apply .
                                </p>
                                <p>
                                    2. Cancellation/Refund/Rescheduling Ticket booked through Online, refund will be
                                                    done to their respective Credit Cards/Debit Cards/Bank Accounts according to the
                                                    Bank procedure. No refund will be done at UTC ticket booking counters. <a href="#"
                                                        class="text-danger " data-toggle="modal" data-target="#CPModal">Read more</a>
                                </p>
                            </div>
                        </div>
                        <hr style="margin-top: 0; margin-bottom: 0;" />
                        <div class="row p-2">
                            <div class="col-6">
                                <h3>Cancelled Tickets  <i class="fa fa-info-circle"
                                    style="font-size: 15px; color: #c6c2c2;" data-toggle="tooltip" data-placement="top"
                                    title="Details are available, Click On Print Cancellation Voucher"></i></h3>

                            </div>
                            <div class="col-md-6 text-right">
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

                        <div class="row p-2">
                            <div class="col-12">
                                <asp:GridView ID="grdcancelledtkt" runat="server" Visible="false" AllowPaging="True" Width="100%" OnRowDataBound="grdcancelledtkt_RowDataBound"
                                    AutoGenerateColumns="False" PageSize="2" DataKeyNames="ticketno,cancel_ref_no" OnRowCommand="grdcancelledtkt_RowCommand" OnPageIndexChanging="grdcancelledtkt_PageIndexChanging"
                                    GridLines="None">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="card mb-2">
                                                    <div class="card-body" style="padding: 10px 15px;">
                                                        <div class="row">
                                                            <div class="col-md-10">
                                                                <asp:Label ID="lblcancelRefno" runat="server" Text='<%# Eval("cancel_ref_no") %>'></asp:Label>&nbsp;<span
                                                                    style="color: #a29e9e; font-size: 12px;">Cancellation Ref. No.</span><br />
                                                                <asp:Label ID="lblUTCTRANSACTIONREFNO" runat="server" Text='<%# Eval("ticketno") %>'></asp:Label>
                                                                &nbsp;<span style="color: #a29e9e; font-size: 12px;">PNR No.</span><br />
                                                                <asp:Label ID="lblcanceldate" runat="server" Text='<%# Eval("cancel_date") %>'></asp:Label>&nbsp;<span
                                                                    style="color: #a29e9e; font-size: 12px;">Cancel Date/Time</span>
                                                            </div>
                                                            <div class="col-md-2 text-right">
                                                                <asp:UpdatePanel runat="server" ID="UpdatePanelall" UpdateMode="Always">
                                                                    <ContentTemplate>
                                                                        <asp:LinkButton ID="lbtnprintvoucher" runat="server" CssClass="btn btn-success" CommandName="PrintVoucher"
                                                                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="padding: 10px;"
                                                                            ToolTip="Print Cancellaion Voucher"> <i class="fa fa-file" title="Print Cancellaion Voucher"></i> </asp:LinkButton>
                                                                        <asp:LinkButton ID="lbtnrefund" runat="server" CssClass="btn btn-warning" CommandName="Refund"
                                                                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="padding: 10px;"
                                                                            ToolTip="Refund Transaction"> <i class="fa fa-rupee-sign" title="Refund Transaction"></i> </asp:LinkButton>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="lbtnprintvoucher" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                </asp:GridView>
                                <asp:Panel ID="pnlNocancelledtkt" runat="server">
                                    <div class="row">
                                        <div class="col-lg-12 text-center mt-4">
                                            <p class="text-center" style="font-size: 32px; font-weight: bold; color: #e3e3e3;">
                                                Details not available of Cancelled Tickets
                                            </p>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="col-xl-7 order-xl-2">
                <div class="card" style="min-height: 80vh">
                    <div class="card-header">
                        <div class="row align-items-center">
                            <div class="col-7">
                                <h3 class="mb-0">Special Ticket Refund</h3>
                            </div>
                            <div class="col-md-5 text-right">
                                <div class="form-group mb-0">
                                    <div class="input-group">
                                        <asp:TextBox ID="tbticketno" runat="server" AutoComplete="Off" class="form-control form-control-sm  " placeholder="Search ticket number" MaxLength="20"></asp:TextBox>
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="lbtnSearchTicketNo" OnClick="lbtnSearchTicketNo_Click" runat="server" CssClass="btn btn-success btn-icon-only btn-sm mr-1 " strle="z-i">
                                            <i class="fa fa-search mt-2"></i>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="lbtnRestTicketNo" runat="server" OnClick="lbtnRestTicketNo_Click" CssClass="btn btn-danger btn-icon-only btn-sm mr-1">
                                            <i class="fa fa-undo mt-2"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-body p-4">


                        <asp:Panel ID="pnlticketdetails" runat="server" Visible="false" Width="100%">
                            <h4 style="text-decoration: underline">1. Ticket/PNR Details </h4>
                                          
                                     <div class="row mt-2">
                                         <div class="col-md-12">
                                             <div class="row">
                                                 <div class="col-md-6">
                                                     <p class="m-0" style="line-height: 11px; color: orange;">
                                                         PNR No
                                                     </p>
                                                     <p class="m-0">
                                                         <asp:Label ID="lblTicketNo" runat="server" Font-Bold="false" Text=""></asp:Label>
                                                     </p>
                                                     <p class="m-0 mt-2" style="line-height: 11px; color: orange;">
                                                         Source
                                                     </p>
                                                     <p class="m-0">
                                                         <asp:Label ID="lblSource" runat="server" Font-Bold="false" Text=""></asp:Label>
                                                     </p>
                                                     <p class="m-0 mt-2" style="line-height: 11px; color: orange;">
                                                         Destination
                                                     </p>
                                                     <p class="m-0">
                                                         <asp:Label ID="lblDestination" runat="server" Font-Bold="false" Text=""></asp:Label>
                                                     </p>
                                                 </div>
                                                 <div class="col-md-6">
                                                     <p class="m-0" style="line-height: 11px; color: orange;">
                                                         Journey Date and Time
                                                     </p>
                                                     <p class="m-0">
                                                         <asp:Label ID="lblJourneyDate" runat="server" Font-Bold="false" Text=""></asp:Label>
                                                         <asp:Label ID="lblJourneyTime" runat="server" Font-Bold="false" Text=""></asp:Label>
                                                     </p>
                                                     <p class="m-0 mt-2" style="line-height: 11px; color: orange;">
                                                         Service
                                                     </p>
                                                     <p class="m-0">
                                                         <asp:Label ID="lblServiceType" runat="server" Font-Bold="false" Text=""></asp:Label>
                                                     </p>
                                                 </div>
                                             </div>
                                             <div class="row mt-2">
                                                 <div class="col-md-6">
                                                     <p class="m-0" style="line-height: 11px; color: orange;">
                                                         Cancellation Date/Time
                                                     </p>
                                                     <p class="m-0">
                                                         <asp:Label ID="lblcancellationdt" runat="server" Font-Bold="false" Text=""></asp:Label>
                                                     </p>
                                                 </div>
                                                 <div class="col-md-6">
                                                     <p class="m-0" style="line-height: 11px; color: orange;">
                                                         Cancelled by
                                                     </p>
                                                     <p class="m-0">
                                                         <asp:Label ID="lblcancelledby" runat="server" Font-Bold="false" Text=""></asp:Label>
                                                     </p>
                                                 </div>
                                             </div>
						<h4 style="text-decoration: underline;margin-top: 14px;">2. Details of cancelled seats </h4>
                                             <div class="row mt-2">
                                                 <div class="col-md-12">
                                                     <asp:GridView ID="grdticketpassenger" runat="server" Width="100%" GridLines="None" AutoGenerateColumns="false"
                                                         class="table" Style="border: 1px solid #ece7e7;">
                                                         <Columns>

                                                             <asp:TemplateField ShowHeader="true" HeaderText="Seat">
                                                                 <ItemTemplate>
                                                                     <p class="m-0" style="line-height: 17px;">
                                                                         <asp:Label ID="grdLabelSeat" runat="server" Text='<%# Eval("seatno") %>'></asp:Label>
                                                                     </p>
                                                                 </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField ShowHeader="true" HeaderText="Passenger">
                                                                 <ItemTemplate>
                                                                     <p class="m-0" style="line-height: 17px;">
                                                                         <asp:Label ID="grdLabelName" runat="server" Text='<%# Eval("travellername") %>'></asp:Label>
                                                                         <br />
                                                                         <asp:Label ID="Label2" runat="server" Font-Size="13px" Text='<%# Eval("travellergender") %>'></asp:Label>,&nbsp;
                                                                                            <asp:Label ID="Label3" runat="server" Font-Size="13px" Text='<%# Eval("travellerage") %>'></asp:Label>
                                                                         Years
                                                                     </p>
                                                                 </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField ShowHeader="true" HeaderText="Fare">
                                                                 <ItemTemplate>
                                                                     <p class="m-0" style="line-height: 17px;">
                                                                         <asp:Label ID="grdLabelFare" runat="server" Text='<%# Eval("amounttotal") %>'></asp:Label>&nbsp;<i
                                                                             class="fa fa-rupee"></i>
                                                                     </p>
                                                                 </ItemTemplate>
                                                             </asp:TemplateField>
                                                         </Columns>
                                                     </asp:GridView>
                                                 </div>
                                             </div>

                                             <div class="row mt-2">
                                                 <div class="col-md-12 text-center">
						<br />
                                                     <h1 class="m-0 text-danger">Please check total amount to be Refunded is 
                                                         <br />
                                                         <i class="fa fa-rupee"></i>
                                                         <asp:Label ID="lblrefundamt" runat="server" Font-Bold="true" Text=""></asp:Label> ₹
                                                     </h1>
                                                 </div>
                                             </div>
                                             <div class="row mt-3">
                                                 <div class="col-md-12 text-center">
                                                     <asp:Button ID="btnRefundTicket" OnClick="btnRefundTicket_Click" OnClientClick="return ShowLoading()" runat="server" Text="Refund" CssClass="btn btn-danger" />
                                                     &nbsp;&nbsp;
                                                                        <asp:Button ID="btnBack" runat="server" Text="Back " CssClass="btn btn-success" Width="80px" />
                                                 </div>
                                             </div>
                                         </div>
                                     </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlNoRecord" Visible="true" CssClass="text-center" Width="100%">
                            <p class="text-center" style="font-size: 32px; font-weight: bold; color: #e3e3e3; margin-top: 50px;">
                                Given PNR is invalid or already refunded
                            </p>
                        </asp:Panel>
                    </div>
                </div>
            </div>

        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
                CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground"  BehaviorID="bvConfirm">
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
                            <asp:LinkButton ID="lbtnYesConfirmation" runat="server" OnClientClick="$find('bvConfirm').hide();ShowLoading();" OnClick="lbtnYesConfirmation_Click" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
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
            <cc1:ModalPopupExtender ID="mpsucsess" runat="server" PopupControlID="pnlsucsess"
                CancelControlID="Button2" TargetControlID="Button1" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlsucsess" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Information
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblsuccess" runat="server" Text="Trip Chart Successfully Generated."></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnok" OnClick="lbtnok_Click" runat="server" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button1" runat="server" Text="" />
                    <asp:Button ID="Button2" runat="server" Text="" />
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

