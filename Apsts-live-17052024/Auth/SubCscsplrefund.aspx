<%@ Page Language="C#" MasterPageFile="~/Auth/subCscMaster.Master" AutoEventWireup="true" CodeFile="SubCscsplrefund.aspx.cs" Inherits="Auth_SubCscsplrefund" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../style.css" rel="stylesheet" />
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
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


     <script type="text/javascript">
         $(document).ready(function () {
             var todayDate = new Date().getDate();
             var endD = new Date(new Date().setDate(todayDate));

             var currDate = new Date();


             $('[id*=txtcancellationdate]').datepicker({
                 //startDate: "dateToday",
                 endDate: endD,
                 changeMonth: true,
                 changeYear: false,
                 format: "dd/mm/yyyy",
                 autoclose: true
             });



         });
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-panel">
        <div class="content">
            <div class="panel-header bg-primary-gradient">
                <div class="row" style="height: 65px;">
                </div>
            </div>
            <div class="page-inner mt--5" style="min-height: 80vh;">
                <div class="row mt--2">
                    <div class="col-md-5">
                        <div class="card ml-3 mt-2">
                            <div class="card-header">
                                <div class="card-title font-weight-bold">
                                    Cancellation Policy
                                </div>
                            </div>
                            <div class="card-body" style="padding: 5px 10px">
                                <div class="row">
                                    <div class="col-md-12">
                                        <p>
                                            1. Partial cancellation is allowed for which cancellation terms & conditions will
                                                    apply .
                                        </p>
                                        <p>
                                            2. Cancellation/Refund/Rescheduling Ticket booked through Online, refund will be
                                                    done to their respective Credit Cards/Debit Cards/Bank Accounts according to the
                                                    Bank procedure. No refund will be done at UTC ticket booking counters. <a href="#"
                                                        class="text-primary " data-toggle="modal" data-target="#CPModal">Read more</a>
                                        </p>

                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="card ml-3 mt-2">
                            <div class="card-header">
                                <div class="card-title font-weight-bold">
                                    <span class="form-label">Print Cancellation/Refund Voucher <i class="fa fa-info-circle d-none"
                                        style="font-size: 15px; color: #c6c2c2;" data-toggle="tooltip" data-placement="top"
                                        title="Details are available
                                                        for cancellation done in last 30 days. For details please contact UTC helpdesk"></i></span>
                                </div>
                            </div>
                            <div class="card-body" style="padding: 7px 16px; height: 50vh; overflow: auto;">
                                <div class="row">
                                    <div class="col-md-9 col-8">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <span>Refund Date</span>
                                                <asp:TextBox ID="txtcancellationdate" autocomplete="off" 
                                                placeholder="DD/MM/YYYY" ToolTip="Select Booking Date" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                                <span>Ticket No.</span>
                                                <asp:TextBox ID="txtcancelledtktno" runat="server" CssClass="form-control " Style="text-transform: uppercase" MaxLength="20"
                                                    placeholder="Enter Ticket No"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-4 p-0">
                                        <div class="input-group pt-4">
                                            <asp:LinkButton class="btn btn-success btn-sm" Style="margin-bottom: 8px;" ID="btnsearchcancelledtkt" runat="server" OnClick="btnsearchcancelledtkt_Click" OnClientClick="return ShowLoading();"
                                                ToolTip="Search Cancelled ticket">
                                                                         <i class="fa fa-search" title="Search Cancelled ticket"></i> 
                                            </asp:LinkButton>
                                            <asp:LinkButton class="btn btn-warning btn-sm ml-2" Style="margin-bottom: 8px;" ID="btnresetcancelledtkt" runat="server" OnClick="btnresetcancelledtkt_Click" OnClientClick="return ShowLoading();"
                                                ToolTip="Search All Cancelled ticket">
                                                                         <i class="fa fa-sync" title="Reset All Cancelled ticket"></i> 
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 10px;">
                                    <div class="col-md-12">
                                        <asp:GridView ID="grdcancelledtkt" runat="server" AllowPaging="True" Width="100%" CssClass="table" Font-Size="9pt"
                                            AutoGenerateColumns="False" PageSize="5" DataKeyNames="ticketno,cancel_ref_no,cancel_amt,cancel_date " OnPageIndexChanging="grdcancelledtkt_PageIndexChanging" OnRowCommand="grdcancelledtkt_RowCommand"
                                            GridLines="None">
                                            <Columns>
                                                <asp:TemplateField HeaderText="PNR No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUTCTRANSACTIONREFNO" runat="server" Text='<%# Eval("ticketno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Refund Ref. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrefundRefno" runat="server" Text='<%# Eval("refund_refno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Refund Date/Time">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrefunddate" runat="server" Text='<%# Eval("cancel_date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Refund Amt.(Rs.)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcancelamt" runat="server" Text='<%# Eval("cancel_amt") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnprintvoucher" runat="server" CssClass="btn btn-success" CommandName="PrintVoucher"
                                                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="padding: 6px; margin-left: 1px;"
                                                            ToolTip="Print Cancellaion Voucher"> <i class="fa fa-file-alt" title="Print Cancellaion Voucher"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle Font-Size="10pt" />

                                        </asp:GridView>
                                    </div>
                                    <div class="col-lg-12">
                                        <center>
                                                    <asp:Label ID="lblcancelledtktmsg" runat="server" Visible="False" Style="color: Red;" Text="Sorry , No Cancelled Ticket available"></asp:Label></center>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                    <div class="col-md-7">
                        <div class="card mt-2 mr-3">
                            <div class="card-header pb-0">
                                <div class="card-title">
                                    <div class="row">
                                        <div class="col-md-6 font-weight-bold">
                                            Search and Refund
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtticketno" runat="server" CssClass="form-control " Style="text-transform: uppercase" AutoComplete="off" MaxLength="20"
                                                    placeholder="Enter Ticket No"></asp:TextBox>
                                                <asp:LinkButton class="btn btn-success btn-sm ml-2" Style="margin-bottom: 8px;" ID="lbtnsearchticket" runat="server" OnClick="lbtnsearchticket_Click" OnClientClick="return ShowLoading();"
                                                    ToolTip="Search Cancelled ticket">
                                                                         <i class="fa fa-search" title="Search Cancelled ticket"></i> 
                                                </asp:LinkButton>
                                                <asp:LinkButton class="btn btn-warning btn-sm ml-2" Style="margin-bottom: 8px;" ID="lbtnresetticket" runat="server" OnClick="lbtnresetticket_Click" OnClientClick="return ShowLoading();"
                                                    ToolTip="Search All Cancelled ticket">
                                                                         <i class="fa fa-sync" title="Search All Cancelled ticket"></i> 
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="card-body" style="height: 72vh;">
                                <asp:Panel ID="pnlticketdetails" runat="server" Visible="false" Width="100%">

                                    <div class="row mt-2">
                                        <div class="col-md-12">
                                            <h5>1. Ticket Details</h5>
                                            <hr />
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <p class="text-muted m-0">
                                                        PNR No
                                                    </p>
                                                    <p class="m-0">
                                                        <asp:Label ID="lblTicketNo" runat="server" Text=""></asp:Label>
                                                    </p>
                                                    <p class="text-muted m-0 mt-2">
                                                        Source
                                                    </p>
                                                    <p class="m-0">
                                                        <asp:Label ID="lblSource" runat="server" Text=""></asp:Label>
                                                    </p>
                                                    <p class="text-muted m-0 mt-2">
                                                        Destination
                                                    </p>
                                                    <p class="m-0">
                                                        <asp:Label ID="lblDestination" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                                <div class="col-md-6">
                                                    <p class="text-muted m-0">
                                                        Journey Date and Time
                                                    </p>
                                                    <p class="m-0">
                                                        <asp:Label ID="lblJourneyDate" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="lblJourneyTime" runat="server" Text=""></asp:Label>
                                                    </p>
                                                    <p class="text-muted m-0 mt-2">
                                                        Service
                                                    </p>
                                                    <p class="m-0">
                                                        <asp:Label ID="lblServiceType" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="row mt-2 mb-3">
                                                <div class="col-md-6">
                                                    <p class="text-muted m-0">
                                                        Cancellation Date/Time
                                                    </p>
                                                    <p class="m-0">
                                                        <asp:Label ID="lblcancellationdt" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                                <div class="col-md-6">
                                                    <p class="text-muted m-0" style="line-height: 11px; color: orange;">
                                                        Cancelled by
                                                    </p>
                                                    <p class="m-0">
                                                        <asp:Label ID="lblcancelledby" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </div>
                                            </div>


                                            <h5>2. Passenger Details</h5>
                                            <div class="row mt-2">
                                                <div class="col-md-12">
                                                    <asp:GridView ID="grdticketpassenger" runat="server" Width="100%" GridLines="None" AutoGenerateColumns="false"
                                                        CssClass="table border-0">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Seat No.">
                                                                <ItemTemplate>
                                                                    <%# Eval("seatno") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name">
                                                                <ItemTemplate>
                                                                    <%# Eval("travellername") %>                                                                                                                                     
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Gender">
                                                                <ItemTemplate>
                                                                    <%# Eval("travellergender") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Age">
                                                                <ItemTemplate>
                                                                    <%# Eval("travellerage") %> Years
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Fare">
                                                                <ItemTemplate>
                                                                    <%# Eval("amounttotal") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <hr />
                                                </div>
                                            </div>
                                            <div class="row mt-2">
                                                <div class="col-md-12 text-center">
                                                    <h4 class="m-0">Total Refund Amount
                                                        <br />
                                                        <i class="fa fa-rupee"></i>
                                                        <asp:Label ID="lblrefundamt" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                    </h4>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-md-12 text-center">
                                                    <asp:Button ID="btnRefundTicket" runat="server" OnClick="btnRefundTicket_Click" Text="Refund" OnClientClick="return ShowLoading();" CssClass="btn btn-danger" />
                                                    &nbsp;&nbsp;
                                                                        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back " OnClientClick="return ShowLoading();" CssClass="btn btn-success" Width="80px" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlnoticketdetails" runat="server" Style="padding-top: 90px; padding-bottom: 50px; font-size: 22px; width: 100%; font-weight: bold; text-align: center;">
                                    <asp:Label ID="lblValidationMsg" CssClass="text-muted" runat="server" Text="Enter cancelled ticket number for special Refund  "></asp:Label>

                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
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
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mperror" runat="server" PopupControlID="pnlerror" CancelControlID="LinkButton5"
                TargetControlID="Button1" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlerror" runat="server" Style="width: 500px !important">
                <div class="card">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Please Check
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblerrormsg" runat="server"></asp:Label>
                        <div style="margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="LinkButton5" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                        </div>
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button1" runat="server" Text="" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpticket" runat="server" PopupControlID="pnlticketpop"
                CancelControlID="lbtnclose" TargetControlID="Button4" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlticketpop" runat="server" Style="position: fixed;">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-lg-6">
                                <h5 class="card-title text-left mb-0">Cancellaiton Voucher
                                </h5>
                            </div>
                            <div class="col-lg-6">
                                <asp:LinkButton ID="lbtnclose" runat="server" CssClass="text-danger float-right"> <i class="fa fa-times"></i> </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body text-left pt-2" style="overflow: auto;">
                        <embed id="tkt" runat="server" src="" style="height: 85vh; width: 45vw" />
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button4" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

        <div class="row">
            <cc1:ModalPopupExtender ID="mpconfirmation" runat="server" PopupControlID="pnlconfirmation" TargetControlID="Button5"
                CancelControlID="lbtnno" BackgroundCssClass="modalBackground" BehaviorID="bvConfirm">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlconfirmation" runat="server" Style="position: fixed;">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <div class="col-md-12">
                                <h3 class="m-0">
                                    <asp:Label ID="lblconfrimmsg" runat="server" Font-Size="15px" Text="Do you want to proceed for special Refund ?"></asp:Label>
                                </h3>
                            </div>
                        </div>
                        <div class="modal-body text-center">
                            <asp:Button ID="lbtnyes" runat="server" OnClick="lbtnyes_Click" Text="Yes" OnClientClick="$find('bvConfirm').hide();ShowLoading();" CssClass="btn btn-danger" />
                            &nbsp; &nbsp;
                            <asp:Button ID="lbtnno" runat="server" Text="No" CssClass="btn btn-success" />
                        </div>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button5" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>


