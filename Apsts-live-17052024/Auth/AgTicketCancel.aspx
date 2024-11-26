<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/AgentMaster.master" AutoEventWireup="true" CodeFile="AgTicketCancel.aspx.cs" Inherits="Auth_AgTicketCancel" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <script type="text/javascript">
        function SuccessMessage(msg) {
            $.confirm({
                icon: 'fa fa-check',
                title: 'Information',
                content: msg,
                animation: 'zoom',
                closeAnimation: 'scale',
                type: 'red',
                typeAnimated: true,
                buttons: {
                    tryAgain: {
                        text: 'OK',
                        btnClass: 'btn-red',
                        action: function () {
                        }
                    },

                }
            });
        }
        function ErrorMessage(msg) {
            $.confirm({
                title: 'Warning!',
                content: msg,
                animation: 'zoom',
                closeAnimation: 'scale',
                type: 'red',
                typeAnimated: true,
                buttons: {
                    tryAgain: {
                        text: 'try again',
                        btnClass: 'btn-red',
                        action: function () {
                        }
                    },
                }
            });
        }
    </script>
    <script>
        $(document).ready(function () {

            var currDate = new Date().getDate();
            var todayDate = new Date(new Date().setDate(currDate));

            $('[id*=txtcancellationdate]').datepicker({
                endDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });




        });
    </script>
    <style>
        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .divWaiting {
            position: fixed;
            background-color: White;
            opacity: 0.6;
            z-index: 2147483647 !important;
            overflow: hidden;
            text-align: center;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            padding-top: 20%;
        }

        .footable {
            border: solid #ccc 0px !important;
        }

            .footable > tbody > tr > td, .footable > thead > tr > th {
                border-left: 0px solid #ccc !important;
            }

        @media only screen and (max-width: 700px) {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidtoken" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hfSeatList" runat="server" />
    <asp:HiddenField ID="hfNoOfSeat" runat="server" />
    <div class="content mt-3">
        <div class="animated fadeIn">
            <div class="row mt--2">
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-header pb-0">
                            <div class="card-title">
                                Ticket Cancellation of 
                                <div class="row">
                                   <%-- <div class="col-lg-4">
                                        <asp:TextBox ID="txtcancellationdate" runat="server" CssClass="form-control " MaxLength="10" Style="text-transform: uppercase"
                                            placeholder="dd/MM/yyyy"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom"
                                            TargetControlID="txtcancellationdate" ValidChars="/" />
                                    </div>--%>
                                    <div class="col-lg-5">
                                        <asp:TextBox ID="txtcancelledtktno" runat="server" CssClass="form-control " MaxLength="20" Style="text-transform: uppercase"
                                            placeholder="Enter Ticket No"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <asp:LinkButton class="btn btn-success p-2" OnClientClick="return ShowLoading();" ID="btnsearchcancelledtkt" runat="server" OnClick="btnsearchcancelledtkt_Click"
                                                ToolTip="Search Cancelled ticket">
                                                                         <i class="fa fa-search" title="Search Cancelled ticket"></i> 
                                            </asp:LinkButton>
                                            <asp:LinkButton class="btn btn-warning p-2 ml-1" OnClientClick="return ShowLoading();" ID="btnresetcancelledtkt" runat="server" OnClick="btnresetcancelledtkt_Click"
                                                ToolTip="Search All Cancelled ticket">
                                                                         <i class="fa fa-undo" title="Search All Cancelled ticket"></i> 
                                            </asp:LinkButton>
                                            <asp:LinkButton class="btn btn-danger p-2 ml-1" OnClientClick="return ShowLoading();" ID="btninfo" runat="server" OnClick="btninfo_Click"
                                                ToolTip="Click here to View Cancellation policy">
                                                                         <i class="fa fa-info" title="Click here to View Cancellation policy"></i> 
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="card-body" style="padding: 5px 10px; min-height: 70vh;">
                            <div class="row" style="margin-top: 10px;">
                                <div class="col-md-12">
                                    <asp:GridView ID="grdcancelledtkt" runat="server" AllowPaging="True" Width="100%" CssClass="table" Font-Size="9pt"
                                        AutoGenerateColumns="False" PageSize="5" DataKeyNames="ticket_no,cancellation_ref_no,cancellation_date " OnRowCommand="grdcancelledtkt_RowCommand" OnRowDataBound="grdcancelledtkt_RowDataBound" OnPageIndexChanging="grdcancelledtkt_PageIndexChanging"
                                        GridLines="None">
                                        <Columns>
                                            <asp:TemplateField HeaderText="PNR No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUTCTRANSACTIONREFNO" runat="server" Text='<%# Eval("ticket_no") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cancellation Ref. No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcancelRefno" runat="server" Text='<%# Eval("cancellation_ref_no") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cancel Date/Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcanceldate" runat="server" Text='<%# Eval("cancellation_date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Refund Amt.(Rs.)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcancelamt" runat="server" Text='<%# Eval("CANCELLATION_AMT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnprintvoucher" runat="server" OnClientClick="return ShowLoading();" CssClass="btn btn-success" CommandName="PrintVoucher"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="padding: 6px; margin-left: 1px;"
                                                        ToolTip="Print Cancellaion Voucher"> <i class="fa fa-print" title="Print Cancellaion Voucher"></i> </asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnRefund" OnClientClick="return ShowLoading();" runat="server" CssClass="btn btn-warning" CommandName="Refund"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="padding: 6px; margin-left: 1px;"
                                                        ToolTip="Refund Transaction"> <i class="fa fa-rupee" title="Refund Transaction"></i> </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle Font-Size="10pt" />

                                    </asp:GridView>
                                    <asp:Panel ID="pnlancelledtktmsg" runat="server" Style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold; text-align: center;">
                                        !! Sorry , No Cancelled Ticket available !!
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-header pb-0">
                            <div class="card-title">
                                Search and Cancel
                                <div class="row">
                                    <div class="col-lg-5">
                                        <asp:TextBox ID="txtticketno" runat="server" CssClass="form-control " MaxLength="20" Style="text-transform: uppercase"
                                            placeholder="Enter Ticket No"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <asp:LinkButton class="btn btn-success p-2" OnClientClick="return ShowLoading();" ID="lbtnsearchticket" runat="server" OnClick="lbtnsearchticket_Click"
                                                ToolTip="Search Ticket">
                                                                         <i class="fa fa-search" title="Search Cancelled ticket"></i> 
                                            </asp:LinkButton>
                                            <asp:LinkButton class="btn btn-warning p-2 ml-1" ID="lbtnresetticket" OnClientClick="return ShowLoading();" runat="server" OnClick="lbtnresetticket_Click"
                                                ToolTip="Reset ticket">
                                                                         <i class="fa fa-undo" title="Search All Cancelled ticket"></i> 
                                            </asp:LinkButton>

                                        </div>
                                    </div>
                                    <div class="col-lg-4 offset-1">
                                    </div>


                                </div>

                            </div>
                        </div>
                        <div class="card-body" style="padding: 27px; min-height: 70vh;">
                            <div class="row">
                                <asp:Panel ID="pnlticketdetails" runat="server" Visible="false" Width="100%">
                                    Please Select Seat to cancel
                                          
                                     <div class="row mt-2">
                                         <div class="col-md-12">

                                             <div class="row">
                                                 <div class="col-md-6">
                                                     <p class="m-0" style="line-height: 11px; color: orange;">
                                                         PNR No
                                                     </p>
                                                     <p class="m-0">
                                                         <asp:Label ID="lblTicketNo" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                     </p>
                                                     <p class="m-0 mt-2" style="line-height: 11px; color: orange;">
                                                         Source
                                                     </p>
                                                     <p class="m-0">
                                                         <asp:Label ID="lblSource" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                     </p>
                                                     <p class="m-0 mt-2" style="line-height: 11px; color: orange;">
                                                         Destination
                                                     </p>
                                                     <p class="m-0">
                                                         <asp:Label ID="lblDestination" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                     </p>
                                                 </div>
                                                 <div class="col-md-6">
                                                     <p class="m-0" style="line-height: 11px; color: orange;">
                                                         Journey Date and Time
                                                     </p>
                                                     <p class="m-0">
                                                         <asp:Label ID="lblJourneyDate" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                         <asp:Label ID="lblJourneyTime" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                     </p>
                                                     <p class="m-0 mt-2" style="line-height: 11px; color: orange;">
                                                         Service
                                                     </p>
                                                     <p class="m-0">
                                                         <asp:Label ID="lblServiceType" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                     </p>
                                                 </div>
                                             </div>
                                             <div class="row mt-2">
                                                 <div class="col-md-12">
                                                     <asp:GridView ID="grdticketpassenger" runat="server" Width="100%" GridLines="None" AutoGenerateColumns="false" DataKeyNames="seatno,amountrefunded"
                                                         class="table" Style="border: 1px solid #ece7e7;">
                                                         <Columns>
                                                             <asp:TemplateField ShowHeader="False">
                                                                 <ItemTemplate>
                                                                     <asp:CheckBox ID="CheckBox1" runat="server"></asp:CheckBox>
                                                                 </ItemTemplate>
                                                             </asp:TemplateField>
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
                                                              <asp:TemplateField ShowHeader="true" HeaderText="Fare Amount">
                                                                 <ItemTemplate>
                                                                     <p class="m-0" style="line-height: 17px;">
                                                                         <asp:Label ID="grdLabelFare" runat="server" Text='<%# Eval("amounttotal") %>'></asp:Label>&nbsp;<i
                                                                             class="fa fa-rupee"></i>
                                                                     </p>
                                                                 </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField ShowHeader="true" HeaderText="Refunded Amount">
                                                                 <ItemTemplate>
                                                                     <p class="m-0" style="line-height: 17px;">
                                                                         <asp:Label ID="grdLabelrefFare" runat="server" Text='<%# Eval("amountrefunded") %>'></asp:Label>&nbsp;<i
                                                                             class="fa fa-rupee"></i>
                                                                     </p>
                                                                 </ItemTemplate>
                                                             </asp:TemplateField>
                                                             
                                                         </Columns>
                                                     </asp:GridView>
                                                 </div>
                                                 <div class="col-md-12 text-center">
                                                     <asp:Button ID="btnCancelTicket" runat="server" OnClientClick="retrun ShowLoading();" OnClick="btnCancelTicket_Click" Text="Cancel Ticket" CssClass="btn btn-danger" />
                                                     &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back " CssClass="btn btn-success" Width="80px" />
                                                 </div>
                                             </div>
                                         </div>
                                     </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlnoticketdetails" runat="server" Style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; width: 100%; font-weight: bold; text-align: center;">
                                    <asp:Label ID="lblValidationMsg" runat="server" Text="!! Search and Cancel Ticket !!"></asp:Label>

                                </asp:Panel>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <cc1:ModalPopupExtender ID="mp2" runat="server" PopupControlID="Panel1" TargetControlID="Button5" BehaviorID="bvConfirm"
                    CancelControlID="Button6" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="Panel1" runat="server" Style="position: fixed;">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <div class="col-md-12">
                                    <h3 class="m-0">
                                        <asp:Label ID="Label1" runat="server" Font-Size="15px" Text="Do you want to proceed with cancellation of selected ticket(s) ?"></asp:Label>
                                    </h3>
                                </div>
                            </div>
                            <div class="modal-body">
                                <center>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel11" UpdateMode="Always">
                                    <ContentTemplate>
                                        <asp:Button ID="yes" runat="server" OnClick="lbtnYesConfirmation_Click" OnClientClick="$find('bvConfirm').hide();ShowLoading();" Text="Yes" CssClass="btn btn-danger" />
                                        &nbsp; &nbsp;
                                        <asp:Button ID="no" runat="server" Text="No" CssClass="btn btn-success" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="yes" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                </center>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div style="visibility: hidden;">
                        <asp:Button ID="Button5" runat="server" Text="" />
                        <asp:Button ID="Button6" runat="server" Text="" />
                    </div>
                </asp:Panel>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <cc1:ModalPopupExtender ID="mpcancellationpolicy" runat="server" PopupControlID="pnlcancellationpolicy" TargetControlID="Button3"
                        CancelControlID="btnclose" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlcancellationpolicy" runat="server" Style="position: fixed;">
                        <div class="modal-dialog modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <div class="col-md-12">
                                        <h3 class="m-0">
                                            <asp:Label ID="Label4" runat="server" Font-Size="15px" Text="Cancellation Policy"></asp:Label>
                                        </h3>
                                    </div>
                                </div>
                                <div class="modal-body" style="max-height: 70vh; overflow: auto;">
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
                                <div class="modal-footer">
                                    <asp:LinkButton ID="btnclose" runat="server" CssClass="btn btn-success"> OK</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button3" runat="server" Text="" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <cc1:ModalPopupExtender ID="mperror" runat="server" CancelControlID="btnerrcl"
                        TargetControlID="Button1" PopupControlID="pnlerror" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlerror" Style="display: none; max-width: 85%;" runat="server">
                        <div class="card" style="min-width: 250px;">
                            <div class="card-header">

                                <strong class="card-title">Please Check</strong>

                            </div>
                            <div class="card-body" style="max-height: 70vh; overflow: auto;">
                                <asp:Label ID="lblerro" runat="server" Text="" Style="font-size: 10pt; font-family: verdana; color: red; font-weight: bold;"></asp:Label>

                            </div>
                            <div class="card-footer" style="text-align: right;">
                                <asp:LinkButton ID="btnerrcl" runat="server" CssClass="btn btn-warning btn-sm" Style="border-radius: 4px;"> <i class="fa fa-times-circle-o"></i> Close </asp:LinkButton>
                            </div>
                        </div>
                        <br />
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button1" runat="server" Text="" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


