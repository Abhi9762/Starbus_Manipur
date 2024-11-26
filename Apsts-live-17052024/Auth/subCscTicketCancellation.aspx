<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/subCscMaster.Master" AutoEventWireup="true" CodeFile="subCscTicketCancellation.aspx.cs" Inherits="Auth_subCscTicketCancellation" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/download_jquery-confirm.min.css" rel="stylesheet" type="text/css" />
    <script src="../assets/download_jquery-confirm.min.js" type="text/javascript"></script>
    <link href="../style.css" rel="stylesheet" />
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidtoken" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hfTicketNo" runat="server" />
    <asp:HiddenField ID="hfJournetDate" runat="server" />
    <asp:HiddenField ID="hfJourneyTime" runat="server" />
    <asp:HiddenField ID="hfSource" runat="server" />
    <asp:HiddenField ID="hfDestination" runat="server" />
    <asp:HiddenField ID="hfSeatList" runat="server" />
    <asp:HiddenField ID="hfNoOfSeat" runat="server" />
   
    <div class="main-panel">
        <div class="content">
            <div class="panel-header bg-primary-gradient">
                <div class="row" style="height: 65px;">
                </div>
            </div>
            <div class="page-inner mt--5" style="min-height:80vh;">             
                <div class="row mt--2">
                    <div class="col-md-5">
                        <div class="card ml-3 mt-2">
                            <div class="card-header">
                                <div class="card-title">
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

                                        <div class="row">
                                            <div class="col-md-9 col-8">
                                                <span class="form-label">Print Cancellation Voucher <i class="fa fa-info-circle d-none"
                                                    style="font-size: 15px; color: #c6c2c2;" data-toggle="tooltip" data-placement="top"
                                                    title="Details are available
                                                        for cancellation done in last 30 days. For details please contact UTC helpdesk"></i></span>
                                                <div class="row">
                                                    <%--<div class="col-md-6">
                                                        <asp:TextBox ID="txtcancellationdate" runat="server" CssClass="form-control " MaxLength="10" Style="text-transform: uppercase"
                                                            placeholder="dd/MM/yyyy"></asp:TextBox>
                                                        
                                                    </div>--%>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtcancelledtktno" runat="server" CssClass="form-control " Style="text-transform: uppercase"
                                                            placeholder="Enter Ticket No" MaxLength="20"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-3 col-4 p-0">
                                                <div class="input-group pt-4">
                                                    <asp:LinkButton class="btn btn-success p-2" ID="btnsearchcancelledtkt" OnClick="btnsearchcancelledtkt_Click" runat="server"
                                                        ToolTip="Search Cancelled ticket">
                                                                         <i class="fa fa-search" title="Search Cancelled ticket"></i> 
                                                    </asp:LinkButton>
                                                    <asp:LinkButton class="btn btn-warning p-2 ml-1" ID="btnresetcancelledtkt" runat="server" OnClick="btnresetcancelledtkt_Click"
                                                        ToolTip="Search All Cancelled ticket">
                                                                         <i class="fa fa-undo" title="Search All Cancelled ticket"></i> 
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 10px;">
                                            <div class="col-md-12">
                                                <asp:GridView ID="grdcancelledtkt" runat="server" ShowHeader="false" AllowPaging="True" Width="100%"
                                                    AutoGenerateColumns="False" PageSize="5" DataKeyNames="ticket_no,cancellation_ref_no,cancellation_date " OnRowCommand="grdcancelledtkt_RowCommand"
                                                    GridLines="None">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <div class="card mb-2">
                                                                    <div class="card-body" style="padding: 10px 6px;">
                                                                        <div class="row">
                                                                            <div class="col-md-10">
                                                                                <asp:Label ID="lblcancelRefno" runat="server" Text='<%# Eval("cancellation_ref_no") %>'></asp:Label>&nbsp;<span
                                                                                    style="color: #a29e9e; font-size: 12px;">Cancellation Ref. No.</span><br />
                                                                                <asp:Label ID="lblUTCTRANSACTIONREFNO" runat="server" Text='<%# Eval("ticket_no") %>'></asp:Label>
                                                                                &nbsp;<span style="color: #a29e9e; font-size: 12px;">PNR No.</span><br />
                                                                                <asp:Label ID="lblcanceldate" runat="server" Text='<%# Eval("cancellation_date") %>'></asp:Label>&nbsp;<span
                                                                                    style="color: #a29e9e; font-size: 12px;">Cancel Date/Time</span>
                                                                            </div>
                                                                            <div class="col-md-2 text-right">
                                                                                <asp:UpdatePanel runat="server" ID="UpdatePanelall" UpdateMode="Always">
                                                                                    <ContentTemplate>
                                                                                        <asp:LinkButton ID="lbtnprintvoucher" runat="server" CssClass="btn btn-success" CommandName="PrintVoucher"
                                                                                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="width: 100%; padding: 6px; margin-left: 1px;"
                                                                                            ToolTip="Print Cancellaion Voucher"> <i class="fa fa-file-alt" title="Print Cancellaion Voucher"></i> </asp:LinkButton>
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
                                                </asp:GridView>
                                            </div>
                                            <div class="col-lg-12">
                                                <center>
                                                    <asp:Label ID="lblcancelledtktmsg" runat="server" Visible="False" Style="color: Red;"></asp:Label></center>
                                            </div>
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-7">
                        <div class="card mt-2 mr-3">
                            <div class="card-header">
                                <div class="card-title">
                                    Tickets Available for Cancellation
                                            <asp:Label ID="listcntkt" runat="server" Text="" Style="font-size: 12px;"></asp:Label>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <asp:Panel ID="pnlticket" runat="server" Width="100%">
                                        <div class="row">
                                            <div class="col-md-12 pl-4 pr-4">
                                                <asp:GridView ID="gvTicketCancelList" runat="server" ShowHeader="false" AllowPaging="True"  Width="100%"
                                                    AutoGenerateColumns="False" PageSize="10" DataKeyNames="ticket_no,src,dest,journey_date,depart,busservice_name,traveller_mobile_no_" OnRowCommand="gvTicketCancelList_RowCommand"
                                                    GridLines="None">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <div class="card mb-2">
                                                                    <div class="card-body">
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <asp:Label ID="lblSOURCE" runat="server" Font-Bold="true" Text='<%# Eval("src") %>'></asp:Label>
                                                                                <b>&nbsp;-&nbsp;</b>
                                                                                <asp:Label ID="lblDESTINATION" runat="server" Font-Bold="true" Text='<%# Eval("dest") %>'></asp:Label>
                                                                            </div>
                                                                            <div class="col-md-8">
                                                                                <asp:Label ID="lblTICKETNO" runat="server" Text='<%# Eval("ticket_no") %>'></asp:Label>&nbsp;<span
                                                                                    style="color: #a29e9e; font-size: 12px;">PNR No.</span><br />
                                                                                <asp:Label ID="lblTRIPDATE" runat="server" Text='<%# Eval("journey_date") %>'></asp:Label>
                                                                                <asp:Label ID="lblDEPARTURETIMEA" runat="server" Text='<%# Eval("depart") %>'></asp:Label>&nbsp;<span
                                                                                    style="color: #a29e9e; font-size: 12px;">Journey Date</span>
                                                                                <asp:Label ID="lblTRIPDATE1" runat="server" Visible="false" Text='<%# Eval("trip_date") %>'></asp:Label>
                                                                            </div>
                                                                            <div class="col-md-4 text-right">
                                                                                <asp:LinkButton ID="lbtnCancel" runat="server" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="btn btn-warning">Cancel</asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <br />
                                                <asp:Label ID="lblValidationMsg" runat="server" Visible="False" Style="width: 100%; font-size: 17px; color: #cecdd0; text-align: center;"></asp:Label>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlticketdetails" runat="server" Visible="false" Width="100%">
                                        <div class="row">
                                            <div class="col-md-12">
                                               
                                                   
                                                    <div class="card-body">
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
                                                        <div class="row">
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
                                                                        <asp:TemplateField ShowHeader="true" HeaderText="Fare">
                                                                            <ItemTemplate>
                                                                                <p class="m-0" style="line-height: 17px;">
                                                                                    <asp:Label ID="grdLabelFare" runat="server" Text='<%# Eval("amounttotal") %>'></asp:Label>&nbsp;<i
                                                                                        class="fa fa-rupee"></i>
                                                                                </p>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ShowHeader="true" HeaderText="Reservation">
                                                                            <ItemTemplate>
                                                                                <p class="m-0" style="line-height: 17px;">
                                                                                    <asp:Label ID="grdLabelReservation" runat="server" Text='<%# Eval("amt_onl_reservation") %>'></asp:Label>&nbsp;<i
                                                                                        class="fa fa-rupee"></i>
                                                                                </p>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ShowHeader="true" HeaderText="Refunded Amount">
                                                                            <ItemTemplate>
                                                                                <p class="m-0" style="line-height: 17px;">
                                                                                    <asp:Label ID="grdLabelrefundedamt" runat="server" Text='<%# Eval("amountrefunded") %>'></asp:Label>&nbsp;<i
                                                                                        class="fa fa-rupee"></i>
                                                                                </p>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                            <div class="col-md-12 text-center">
                                                                <asp:Button ID="btnCancelTicket" runat="server" OnClick="btnCancelTicket_Click1" Text="Cancel Ticket" CssClass="btn btn-danger" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btnBack" runat="server" Text="Back " OnClick="btnBack_Click" CssClass="btn btn-success" Width="80px" />
                                                            </div>
                                                        </div>
                                                    </div>
                                               
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
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
            <!-- Modal -->
            <div>

            
            <cc1:ModalPopupExtender ID="ModalPopupError" runat="server" PopupControlID="PanelModalError"
                TargetControlID="ButtonOpenModalError" CancelControlID="LinkButtonCloseModalError"
                BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="PanelModalError" runat="server" Style="position: fixed;">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <div class="col-md-10">
                                <h3 class="m-0">
                                    <asp:Label ID="LabelModalErrorHeader" runat="server" ForeColor="Red"></asp:Label>
                                </h3>
                            </div>
                            <div class="col-md-2 text-right">
                                <asp:LinkButton ID="LinkButtonCloseModalError" runat="server" UseSubmitBehavior="false"
                                    Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-body">
                            <p class="full-width-separator text-center" style="font-size: 17px;">
                                <asp:Label ID="LabelModalErrorMessage" runat="server"></asp:Label>
                            </p>
                        </div>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="ButtonOpenModalError" runat="server" Text="" />
                </div>
            </asp:Panel>

                </div>

            <div class="row">
            <cc1:ModalPopupExtender ID="mp2" runat="server" PopupControlID="Panel1" TargetControlID="Button5"
                CancelControlID="Button6" BackgroundCssClass="modalBackground" BehaviorID="bvConfirm">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="Panel1" runat="server" Style="position: fixed;">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <div class="row">
                                <div class="col-md-12">
                                    <h3 class="m-0">
                                        <asp:Label ID="lblOtpMsg" runat="server" Font-Size="13px" Text="Do you want to proceed with cancellation of selected ticket(s) ?"></asp:Label>
                                    </h3>
                                </div>
                            </div>
                        </div>
                        <div class="modal-body">

                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="tbOTP" runat="server" MaxLength="6" CssClass="form-control text-lg text-uppercase mb-3" type="Search" placeholder="Enter OTP" AutoComplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="tbcaptchacode" runat="server" MaxLength="6" CssClass="form-control text-lg text-uppercase" type="Search" placeholder="Enter Image Text" AutoComplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-md-2"></div>
                                <div class="col-md-8">
                                    <div class="form-group w-100">
                         

                                        <div class="input-group">
                                            <img alt="Visual verification" title="Please enter the security code as shown in the image."
                                                src="../CaptchaImage.aspx" style="width: 88%; border: 1px solid #b2f0be; border-radius: 5px 0px 0px 5px;" />
                                            <div class="input-group-append">
                                                <asp:LinkButton runat="server" ID="lbtnRefresh" OnClick="lbtnRefresh_Click" CssClass=" btn btn-outline-primary p-2 btn-refresh"><i class="fa fa-recycle" ></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <h3 class="m-0">
                                        <asp:Label ID="lblerror" runat="server" CssClass="text-danger" Font-Size="15px" Text=""></asp:Label>
                                    </h3>
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-md-12 mt-2 text-center">
                                    <asp:Button ID="yes" runat="server" OnClick="yes_Click1" Text="Verify & Proceed" OnClientClick="$find('bvConfirm').hide();ShowLoading();" CssClass="btn btn-danger" />
                                    &nbsp; &nbsp;
                                        <asp:Button ID="no" runat="server" OnClick="no_Click1" Text="Cancel" CssClass="btn btn-success" />
                                </div>
                            </div>
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

            <div>
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
        </div>
    </div>
    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
</asp:Content>




