<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/subCscMaster.Master" AutoEventWireup="true" CodeFile="subCscTicketDetails.aspx.cs" Inherits="Auth_subCscTicketDetails" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/DataTables/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="assets/DataTables/js/jquery.dataTables.min.js"></script>
    <link href="assets/css/download_jquery-confirm.min.css" rel="stylesheet" type="text/css" />
    <script src="assets/js/download_jquery-confirm.min.js" type="text/javascript"></script>
    <script src="../NewAssets/js/jquery-3.3.1.min.js"></script>
    <script src="../assets/js/jqueryuimin.js"></script>
    <link href="../assets/css/UIMin.css" rel="stylesheet" />
    <script src="../assets/download_jquery-confirm.min.js"></script>
    <link href="../assets/download_jquery-confirm.min.css" rel="stylesheet" />

    <style>
        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .pagination-ys {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }

            .pagination-ys table > tbody > tr > td {
                display: inline;
            }

                .pagination-ys table > tbody > tr > td > a, .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    color: #dd4814;
                    background-color: #ffffff;
                    border: 1px solid #dddddd;
                    margin-left: -1px;
                }

                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    margin-left: -1px;
                    z-index: 2;
                    color: #aea79f;
                    background-color: #f5f5f5;
                    border-color: #dddddd;
                    cursor: default;
                }

                .pagination-ys table > tbody > tr > td:first-child > a, .pagination-ys table > tbody > tr > td:first-child > span {
                    margin-left: 0;
                    border-bottom-left-radius: 4px;
                    border-top-left-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td:last-child > a, .pagination-ys table > tbody > tr > td:last-child > span {
                    border-bottom-right-radius: 4px;
                    border-top-right-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td > a:hover, .pagination-ys table > tbody > tr > td > span:hover, .pagination-ys table > tbody > tr > td > a:focus, .pagination-ys table > tbody > tr > td > span:focus {
                    color: #97310e;
                    background-color: #eeeeee;
                    border-color: #dddddd;
                }
    </style>
    <script type="text/javascript">
        $(window).on('load', function () {
            HideLoading();
        });
        function ShowLoading() {
            var div = document.getElementById("loader");
            div.style.display = "block";
        }
        function HideLoading() {
            var div = document.getElementById("loader");
            div.style.display = "none";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid" style="padding-top: 75px;">

        <div class="row">
            <div class="col-3 ">
                <div class="card pt-3 pl-3" style="min-height: 700px">
                    <div class="row">
                        <div class="col-12 mt-2">
                            <asp:Label ID="Label3" Font-Bold="true" runat="server" Text="You can perform following actions with tickets:"></asp:Label>
                        </div>
                        <div class="col-12 mt-2">
                            <asp:LinkButton ID="lbtnPrint" runat="server"
                                class="btn btn-primary" Enabled="false" Style="width: 25px; padding: 5px;">P</asp:LinkButton>
                            <i class="fa fa-arrow-right mr-2" style="color: lightgray"></i>
                            <asp:Label ID="Label6" Font-Size="Small" runat="server" Text="You can print the confirmed ticket."></asp:Label>
                        </div>
                        <div class="col-12 mt-2">
                            <asp:LinkButton ID="lbtnCancel" runat="server"
                                class="btn btn-danger " Enabled="false" Style="width: 25px; padding: 5px;">C</asp:LinkButton>
                            <i class="fa fa-arrow-right mr-2 " style="color: lightgray"></i>
                            <asp:Label ID="Label5" Font-Size="Small" runat="server" Text="You can cancel the confirmed ticket."></asp:Label>

                        </div>
                        <div class="col-12 mt-2">
                            <asp:LinkButton ID="lbtnresendwhatsapp" runat="server"
                                class="btn btn-success" Enabled="false" Style="width: 25px; padding: 5px;">W</asp:LinkButton>
                            <i class="fa fa-arrow-right mr-2 " style="color: lightgray"></i>
                            <asp:Label ID="Label4" runat="server" Font-Size="Small" Text="Resend Whatsapp SMS for Ticket Information is available only for confirmed tickets."></asp:Label>
                        </div>
                        <div class="col-12 mt-2">
                            <asp:LinkButton ID="lbtnresendsms" runat="server"
                                class="btn btn-warning" Enabled="false" Style="width: 25px; padding: 5px;">S</asp:LinkButton>
                            <i class="fa fa-arrow-right mr-2 " style="color: lightgray"></i>
                            <asp:Label ID="Label8" Font-Size="Small" runat="server" Text="Resend SMS for Ticket Information is available only for confirmed tickets.."></asp:Label>
                        </div>
                        <div class="col-12 mt-2">
                            <asp:LinkButton ID="lbtnresendmail" runat="server"
                                class="btn btn-danger" Enabled="false" Style="width: 25px; padding: 5px;">E</asp:LinkButton>
                            <i class="fa fa-arrow-right mr-2 " style="color: lightgray"></i>
                            <asp:Label ID="Label9" Font-Size="Small" runat="server" Text="Resend Email for Ticket Information is available only for confirmed tickets.."></asp:Label>
                        </div>
                        <div class="col-12 mt-2">
                            <asp:LinkButton ID="lbtnInfo" runat="server"
                                class="btn btn-warning" Enabled="false" Style="width: 25px; padding: 5px;">I</asp:LinkButton>
                            <i class="fa fa-arrow-right mr-2 " style="color: lightgray"></i>
                            <asp:Label ID="Label10" Font-Size="Small" runat="server" Text="You can view the Ticket Information of all tickets."></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-9 ">
                <div class="card" style="min-height: 700px">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-3">
                                <strong>Ticket Details</strong><br />
                                <asp:Label ID="Label11" runat="server" ForeColor="Green" Font-Size="small" Font-Bold="false" Text="(Details Available Seat wise)"></asp:Label>
                            </div>
                            <div class="col">
                                <asp:Label ID="Label1" runat="server" Font-Size="small" Font-Bold="false" Text="Enter Ticket No./Name/Mobile/Email Id"></asp:Label><br />
                                <asp:TextBox ID="txtSearch" placeholder="Ticket No./Name/Mobile/Email Id" AutoComplete="off" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                            <div class="col">
                                <asp:Label ID="Label12" runat="server" Font-Size="small" Font-Bold="false" Text="Booking Date"></asp:Label><br />
                                <asp:TextBox ID="txtBookingDT" placeholder="DD/MM/YYYY" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtenderBookingDT" runat="server" CssClass="black" PopupPosition="BottomRight"
                                    Format="dd/MM/yyyy" PopupButtonID="txtBookingDT" TargetControlID="txtBookingDT" BehaviorID="CalendarExtenderBookingDT"></cc1:CalendarExtender>
                            </div>
                            <div class="col">
                                <asp:Label ID="Label13" runat="server" Font-Size="small" Font-Bold="false" Text="Journey Date"></asp:Label><br />
                                <asp:TextBox ID="txtJourneyDt" placeholder="DD/MM/YYYY" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtenderJourneyDt" runat="server" CssClass="black" PopupPosition="BottomRight"
                                    Format="dd/MM/yyyy" PopupButtonID="txtJourneyDt" TargetControlID="txtJourneyDt" BehaviorID="CalendarExtenderJourneyDt"></cc1:CalendarExtender>
                            </div>
                            <div class="col" style="padding: 0px; padding-top: 20px;">
                                <asp:LinkButton ID="lbtnSearch" OnClick="lbtnSearch_Click" ToolTip="Click here for Search" Style="padding: 7px;" runat="server" CssClass="btn btn-warning btn-sm">
                                                                    <i class="fa fa-search" ></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 flex-column d-flex stretch-card ">
                            <div class="card-body table table-responsive">
                                <asp:GridView ID="gvTicketDetails" runat="server" GridLines="None" CssClass="w-100" AllowPaging="true"
                                    PageSize="6" OnPageIndexChanging="gvTicketDetails_PageIndexChanging" OnRowCommand="gvTicketDetails_RowCommand" OnRowDataBound="gvTicketDetails_RowDataBound" AutoGenerateColumns="false" ShowHeader="false"
                                    DataKeyNames="ticket_no,is_print, dep_time,trvlr_emailid,trip_status,val_current_status_code">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="row pb-2">
                                                    <%--style="border-bottom: 1px solid #f3eaea;"--%>
                                                    <div class="col">
                                                        <div class="row">
                                                            <div class="col">
                                                                <h5 class="mb-0">
                                                                    <asp:Label ID="lblbookstatus" Visible="false" Font-Bold="true" runat="server" Text='<%# Eval("val_status") %>'></asp:Label>
                                                                    <%# Eval("val_status").ToString() == "CONFIRMED" ? "<i class='fa fa-check-circle-o text-success'></i>" : "<i class='fa fa-times-circle-o text-danger'></i>" %>
                                                                    <asp:Label ID="lblticket" runat="server" Text='<%# Eval("ticket_no") %>'></asp:Label>

                                                                </h5>

                                                                <p class="mb-0">

                                                                    <i class="fa fa-bus" aria-hidden="true"></i>&nbsp;
                                                                    <%#Eval("service_type_name") %>


                                                                    <asp:Label ID="lblservicecode" runat="server" Text='<%#Eval("devcid") %>' /><asp:Label ID="lblserviceRorJ" runat="server" Text='<%#Eval("tripdirection") %>' /><%#Eval("service_trip_code") %><br />


                                                                </p>
                                                            </div>
                                                            <div class="col-auto">
                                                                <p class="mb-0 ">
                                                                    <asp:Label ID="Label1" runat="server" Text='  <%#Eval("trvlr_name") %> '></asp:Label>

                                                                    , <%#Eval("trvlr_gender") %>, <%#Eval("trvlr_age") %>Y
                                                                </p>
                                                                <p class="mb-0">
                                                                    <i class="fa fa-mobile mr-1"></i><%#Eval("trvlr_mobileno") %>,
                                                                      <i class="fa fa-envelope mr-1"></i><%#Eval("trvlr_emailid") %>
                                                                </p>
                                                            </div>
                                                            <div class="col text-right">
                                                                <p class="mb-0">
                                                                    <asp:Label ID="Label7" runat="server" Text="Booking Date-"></asp:Label><%#Eval("booking_datetime") %>
                                                                </p>
                                                                <p class="mb-0">
                                                                    <asp:Label ID="Label2" runat="server" Text="Journey Date-"></asp:Label>
                                                                    <asp:Label ID="lbljouyneydate" runat="server" Text=' <%#Eval("journey_date") %>'></asp:Label>
                                                                </p>
                                                            </div>
                                                        </div>
                                                        <%--   <div class="row">
                                                            <div class="col">
                                                                <p class="mb-0 mt-1 text-success font-weight-bold "><%#Eval("SOURCE") %> - <%#Eval("DESTINATION") %></p>
                                                            </div>
                                                        </div>--%>
                                                    </div>
                                                    <div class="col-auto">
                                                        <p class="text-right mb-1">
                                                            <%# (Eval("val_current_status").ToString() == "CONFIRMED") ? 
        "<span class='text-success font-weight-bold'>Confirmed</span>" : 
        "<span class='text-danger font-weight-bold'>Cancelled</span>" %>
                                                        </p>

                                                        <asp:LinkButton ID="lbtnPrint" runat="server"
                                                            class="btn btn-primary" Style="width: 25px; padding: 5px;" CommandName="print" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Print Ticket"> P</asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnCancel" runat="server"
                                                            class="btn btn-danger " Style="width: 25px; padding: 5px;" CommandName="Cancel" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Cancel Ticket">C</asp:LinkButton>

                                                        <asp:LinkButton ID="lbtnresendwhatsapp" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="resendwhatsapp"
                                                            class="btn btn-success" Style="width: 25px; padding: 5px;" ToolTip="Resend Whatsapp SMS">W</asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnresendsms" runat="server" CommandName="resendsms" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                            class="btn btn-warning" Style="width: 25px; padding: 5px;" ToolTip="Resend Ticket SMS">S</asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnresendmail" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="resendemail"
                                                            class="btn btn-danger" Style="width: 25px; padding: 5px;" ToolTip="Resend Ticket Email">E</asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnInfo" runat="server"
                                                            class="btn btn-warning" Style="width: 25px; padding: 5px;" CommandName="view" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="View Detail">I </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>

                            </div>
                            <div class="col text-center mt-5">
                                <asp:Label ID="lblmsg" Style="font-size: 40px;" Font-Bold="true" ForeColor="LightGray" runat="server" Text="Ticket Details Not Available"></asp:Label>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <!-- Ticket -->
        <div class="row">
            <cc1:ModalPopupExtender ID="mpEtkt" runat="server" PopupControlID="pnlEtkt"
                CancelControlID="lbtnclose" TargetControlID="Button4" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlEtkt" runat="server" Style="display: none; position: fixed;">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-lg-6">
                                <h5 class="card-title text-left mb-0">E-Ticket
                                </h5>
                            </div>
                            <div class="col-lg-6">
                                <asp:LinkButton ID="lbtnclose" runat="server" CssClass="text-danger float-right"> <i class="fa fa-times"></i> </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body text-left pt-2" style="overflow: auto;">
                        <embed id="tkt" runat="server" src="" style="height: 85vh; width: 80vw" />
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button4" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <!-- Send Whatsapp -->
        <div class="row">
            <cc1:ModalPopupExtender ID="mpresendWhatsapp" runat="server" PopupControlID="mppnlwhatsapp"
                TargetControlID="Button1" CancelControlID="LinkButton1" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="mppnlwhatsapp" runat="server" Style="display: none; position: fixed;">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <div class="col-md-10">
                                <h3 class="m-0">Whatsapp Sms</h3>
                            </div>
                            <div class="col-md-2 text-right">
                                <asp:LinkButton ID="LinkButton1" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-body">
                            <p class="full-width-separator text-center" style="font-size: 17px;">
                                Ticket booking Confirmation Whatsapp SMS has been sent again to your mobile number.<br />
                                Please check inbox of your Whatsapp Account.
                            </p>
                        </div>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button1" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <!-- Send SMS -->
        <div class="row">
            <cc1:ModalPopupExtender ID="mpsmssend" runat="server" PopupControlID="pnlsmssend"
                TargetControlID="Button2" CancelControlID="LinkButton2" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlsmssend" runat="server" Style="display: none; position: fixed;">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <div class="col-md-10">
                                <h3 class="m-0">Sms</h3>
                            </div>
                            <div class="col-md-2 text-right">
                                <asp:LinkButton ID="LinkButton2" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-body">
                            <p class="full-width-separator text-center" style="font-size: 17px;">
                                Ticket booking Confirmation SMS has been sent again to your mobile number.<br />
                                Please check inbox of your mobile.
                            </p>
                        </div>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button2" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <!-- Send Email -->
        <div class="row">
            <cc1:ModalPopupExtender ID="mpresendEmail" runat="server" PopupControlID="mppanelEmail"
                TargetControlID="Button3" CancelControlID="LinkButton3" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="mppanelEmail" runat="server" Style="display: none; position: fixed;">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <div class="col-md-10">
                                <h3 class="m-0">Email</h3>
                            </div>
                            <div class="col-md-2 text-right">
                                <asp:LinkButton ID="LinkButton3" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-body">
                            <p class="full-width-separator text-center" style="font-size: 17px;">
                                Ticket booking Confirmation Email and Copy Ticket has been sent again to eMail ID
                                given at the time of booking. Please check your mailbox.
                            </p>
                        </div>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button3" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <!-- ticket Details -->
        <div class="row">
            <cc1:ModalPopupExtender ID="mpTicket" runat="server" PopupControlID="pnlMPEmail" TargetControlID="Button5"
                CancelControlID="LinkButton4" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlMPEmail" runat="server" Style="display: none; position: fixed;">
                <div class="modal-content mt-5">
                    <div class="modal-header">
                        <div class="col-md-10">
                            <h3 class="m-0">Ticket Details</h3>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="LinkButton4" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="modal-body p-0">
                        <embed src="dashticket.aspx" style="height: 85vh; width: 80vw" />
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button5" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <!-- Error Popup  -->
        <div class="row">
            <cc1:ModalPopupExtender ID="ModalPopupError" runat="server" PopupControlID="PanelModalError"
                TargetControlID="ButtonOpenModalError" CancelControlID="LinkButtonCloseModalError"
                BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="PanelModalError" runat="server" Style="display: none; position: fixed;">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <div class="col-md-10">
                                <h3 class="m-0">
                                    <asp:Label ID="LabelModalErrorHeader" runat="server" ForeColor="Black"></asp:Label>
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

    </div>










</asp:Content>




