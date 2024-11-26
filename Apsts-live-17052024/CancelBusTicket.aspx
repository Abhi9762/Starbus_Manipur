<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="CancelBusTicket.aspx.cs" Inherits="CancelBusTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="midd-bg">

        <div class="container">
            <div class="row">
                <div class="col-lg-6">
                    <h3 class="mb-1">
                        <div class="tx5">Cancellation <span class="org">Policy</span></div>
                    </h3>
                    <div class="tk-bg">
                        <div class="hd-text text-left">

                            <div style="padding: 15px;">
                                <ul class="list-group">
                                    <li class="list-group-item">Cancellation/Refund/Rescheduling Ticket booked through Online,
                                    refund will be done to their respective Credit Cards/Debit Cards/Bank Accounts according
                                    to the Bank procedure. No refund will be done at our ticket booking counters
                                    
                                    </li>
                                    <li class="list-group-item">
                                        <asp:Label ID="lblcancellationpolicy" runat="server"></asp:Label>
                                    </li>
                                    <%--    <li class="list-group-item" >Cancellation is not allowed after Up to 2 hr before Schedule
                                    service start time at origin of the bus. From the date of journey.</li>--%>
                                    <li class="list-group-item">Reservation Fee is non-refundable except in case of 100%
                                    cancellation of tickets, if we cancel the service for operational or any
                                    other reasons.</li>
                                   
                                       
                                    <li class="list-group-item">Payment Gateway Service charges will not be refunded for
                                    service cancellation/ failure transactions in e-ticketing.</li>
                                    <li class="list-group-item">Partial cancellation is allowed for which cancellation terms
                                    & conditions will apply.</li>
                                </ul>
                            </div>
                        </div>


                    </div>
                </div>
                <div class="col-lg-6">
                    <h3 class="mb-1">
                        <div class="tx5">Cancel <span class="org">Ticket</span></div>
                    </h3>
                    <div class="tk-bg h-100vh">
                        <div class="hd-text text-left">

                            <div style="padding: 15px;">

                                
                        <div class="text-center">
                            <div>To cancel booking made through a registered account, Please login and cancel the ticket</div>
                            <asp:LinkButton ID="lbtnlogin" runat="server" CssClass="btn btn-primary btn-lg mt-2" OnClick="lbtnlogin_Click"> <i class="fa fa-lock"></i> Login Here</asp:LinkButton>

                        </div>
                                 <hr />
                             <p>  All eTicket cancellation refunds may process through payment gateway, i.e. passenger may get refund amount in their bank account/card /wallet through which they book eTicket.
To cancel booking made through a registered account, Please login and cancel the ticket</p> 


                               
                        <p> Passengers will be given normally in one month, after the
                                    cancellation of ticket or receipt of e-mail. If refunds are delayed more than a
                                    month, passengers may contact helpline telephone number at <br />  <i class="fa fa-phone-volume"></i>&nbsp;<asp:Label ID="lblcontact" runat="server" Text="-NA-"></asp:Label>
 <br /><i class="fas fa-envelope"></i>&nbsp;<asp:Label runat="server" ID="lblemail" Text="-NA-"></asp:Label></p> 
</div>
                            
                        </div>
                       

                         
                    </div>

                </div>
            </div>
        </div>
        <br />
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
                                <asp:Label ID="lblcancellationpolicy1" runat="server"></asp:Label>
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
    </div>
</asp:Content>

