<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/AgentMaster.master" AutoEventWireup="true" CodeFile="agTicketQuery.aspx.cs" Inherits="Auth_agTicketQuery" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

        .border-right {
            border-right: 1px solid #e6e6e6;
        }

        .btn-QuickLinks {
            color: #1d4ab1;
            background-color: transparent;
            background-image: none;
            border-width: 2px;
            border-color: #236bb3;
            font-weight: 600 !important;
            border-radius: 5px !important;
            font-size: 14px;
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

    <script type="text/javascript">
        $(document).ready(function () {
            var todayDate = new Date().getDate();
            var endD = new Date(new Date().setDate(todayDate + 30));
            var currDate = new Date();

            $('[id*=tbjourneydate]').datepicker({
                endDate: endD,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
            $('[id*=tbbookingdate]').datepicker({
                endDate: 'dateToday',
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });

        });
    </script>
       
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:HiddenField ID="hdmaxdate" runat="server" />
    <div class="content mt-3">
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="card-header">
                        <div class="row px-2">
                            <div class="col-lg-3">
                                <h4 class="mb-0">Ticket Query</h4>
                                <span style="font-size: 10pt;">(You can search all ticket for multiple action)</span>
                            </div>
                            <div class="col-lg-2">
                                <span class="form-control-label">Booking Date</span>
                                <asp:TextBox ID="tbbookingdate" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="DD/MM/YYYY" runat="server" MaxLength="10"></asp:TextBox>
                               <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom"
                                                            TargetControlID="tbbookingdate" ValidChars="/" />
                            </div>
                            <div class="col-lg-2">
                                <span class="form-control-label">Journey Date</span>
                                <asp:TextBox ID="tbjourneydate" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="DD/MM/YYYY" runat="server" MaxLength="10"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom"
                                                            TargetControlID="tbjourneydate" ValidChars="/" />
                            </div>

                            <div class="col-lg-3">
                                <span class="form-control-label">Ticket No./Passenger Name/Mobile Number</span>
                                <asp:TextBox ID="tbvalue" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="Ticket No/Name of Traveller" runat="server" MaxLength="20"></asp:TextBox>
                            </div>
                            <div class="col-lg-2 pt-4">
                                <asp:LinkButton ID="lbtnsearch" OnClick="lbtnsearch_Click" OnClientClick="return ShowLoading()" runat="server" Style="padding: 2px;" CssClass="btn btn-success btn-icon-only btn-sm mr-1 ">
                                            <i class="fa fa-search"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbtnRest" OnClick="lbtnreset_Click" OnClientClick="return ShowLoading()" runat="server" Style="padding: 2px;" CssClass="btn btn-danger btn-icon-only btn-sm mr-1">
                                            <i class="fa fa-undo"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row px-2">
                            <div class="col-md-12 col-lg-12">
                                <asp:Panel ID="pnlticketdetails" runat="server" Visible="false">
                                    <div class="row px-3">
                                        <div class="col-md-12 col-lg-12">
                                            <asp:GridView ID="gvTickets" runat="server" class="table table-striped table-advance table-hover"
                                                AllowSorting="True" DataKeyNames="is_print, departure_time,ticket_no" AutoGenerateColumns="False" ShowHeader="false"
                                                OnRowCommand="gvTickets_RowCommand" OnRowDataBound="gvTickets_RowDataBound" GridLines="None">
                                               
                                                <Columns>
                                                    <asp:TemplateField>                                                      
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTICKETNO" runat="server" Text='<%# Eval("ticket_no") %>'></asp:Label><br />
                                                           <span style="font-size: 12px; padding-right: 6px;"><i class="fa fa-bus"></i> </span> <asp:Label ID="lblSERVICE_TYPE_NAME_EN" runat="server" Text='<%# Eval("servicetypename") %>'></asp:Label>
                                                            <br />
                                                            <%#Eval("val_source") %> - <%#Eval("val_destination") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>                                                       
                                                        <ItemTemplate>
                                                            <span style="font-size: 12px; padding-right: 6px;"><i class="fa fa-calendar-check-o"></i>&nbsp;Booking Date </span> 
                                                            <asp:Label ID="lblBOOKINGDATETIME" runat="server" Text='<%# Eval("booking_datetime") %>'></asp:Label>
                                                                <br />
                                                            <span style="font-size: 12px; padding-right: 6px;"><i class="fa fa-calendar-check-o"></i>&nbsp; Journey Date</span>
                                                                <asp:Label ID="lbljouyneydate" runat="server" Text='<%# Eval("journey_date") %>'></asp:Label><br />
                                                            <span style="font-size: 12px; padding-right: 6px;">Boarding Station </span> <%#Eval("val_boarding") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>                                                      
                                                        <ItemTemplate>
                                                             <span style="font-size: 12px; padding-right: 6px; font-weight:bold;">Status</span> 
                                                            <asp:Label ID="lblbookstatus" runat="server" ForeColor="#5cb85c" Font-Bold="true"
                                                                Text='<%# Eval("val_staus") %>'></asp:Label><br />
                                                            <asp:LinkButton ID="lbtnPrint" runat="server" CommandName="print" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                class="btn btn-primary" Style="width: 30px; padding: 5px;" ToolTip="Print Ticket"><i class="fa fa-print" title="Print Ticket"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnview" runat="server" CommandName="view" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                class="btn btn-success" Style="width: 30px; padding: 5px;" ToolTip="View Ticket Details"><i class="fa fa-eye" title="View Ticket Details"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnresendsms" runat="server" CommandName="resendsms" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                class="btn btn-warning" Style="width: 30px; padding: 5px;" ToolTip="Resend Ticket SMS"><i class="fa fa-mobile" title="Resend SMS"></i></asp:LinkButton>
                                                             <asp:LinkButton ID="lbtnresendwhtsup" runat="server" CommandName="whtsup" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                class="btn btn-danger" Style="width: 30px; padding: 5px;" ToolTip="Resend Ticket SMS"><i class="fa fa-whatsapp" title="Resend SMS"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnresendmail" runat="server" CommandName="resendmail" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                class="btn btn-primary" Style="width: 30px; padding: 5px;" ToolTip="Resend Ticket Email"><i class="fa fa-envelope" title="Resend Ticket Email" style="font-size: 9pt;padding: 1px;"></i> </asp:LinkButton>
                                                             <asp:LinkButton ID="lbtncancel" runat="server" CommandName="cancel" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                class="btn btn-danger" Style="width: 30px; padding: 5px;" ToolTip="Cancel Ticket"><i class="fa fa-ticket" title="Resend SMS"></i></asp:LinkButton>
                                                             <asp:LinkButton ID="lbtnspcialcancel" runat="server" CommandName="spclcancel" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                class="btn btn-danger" Style="width: 30px; padding: 5px;" ToolTip="Spceial Cancel Ticket"><i class="fa fa-ticket" title="Resend SMS"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                        <HeaderStyle Height="20px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlnoticketdetails" runat="server" Width="100%" Visible="true">
                                    <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                        <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 35px; padding-bottom: 35px; font-size: 25px; font-weight: bold;">
                                            Please select Perameter for details<br />
                                            and other actions

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
            <cc1:ModalPopupExtender ID="mpError" runat="server" PopupControlID="pnlError" CancelControlID="Button2"
                TargetControlID="Button3" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlError" runat="server" Style="position: fixed;">
                <div class="card" style="min-width: 350px;">
                    <div class="card-header" style="background-color: #e4273b; color: White;">
                        <h4 class="card-title">
                            <span style="font-size: 11pt;">Please Check & Correct</span>
                            <asp:LinkButton ID="lbtnerrorclose1" runat="server" OnClick="lbtnerrorclose_Click" ToolTip="Close" Style="float: right; color: white; padding: 0px;"> <i class="fa fa-times"></i>  </asp:LinkButton>
                        </h4>
                    </div>
                    <div class="card-body" style="min-height: 100px;">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblerrmsg" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:LinkButton ID="lbtnerrorclose" runat="server" OnClick="lbtnerrorclose_Click" CssClass="btn btn-warning" Style="height: 30px; width: 90px; padding-top: 4px; font-size: 10pt; border-radius: 4px;"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button3" runat="server" Text="" />
                    <asp:Button ID="Button2" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <!-- ticket Details -->
        <div class="row">
            <cc1:ModalPopupExtender ID="mpTicketDtls" runat="server" PopupControlID="pnlMPEmail" TargetControlID="Button21"
                CancelControlID="LinkButton71" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlMPEmail" runat="server" Style="position: fixed;">
                <div class="modal-content mt-5">
                    <div class="modal-header">
                        <div class="col-md-10">
                            <h3 class="m-0">Ticket Details</h3>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="LinkButton71" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="modal-body p-0">
                        <embed src="dashticket.aspx" style="height: 85vh; width: 80vw" />
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button21" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

        <div class="row">
            <cc1:ModalPopupExtender ID="mpticket" runat="server" PopupControlID="pnlticket"
                CancelControlID="lbtnclose" TargetControlID="Button4" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlticket" runat="server" Style="position: fixed;">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-lg-6">
                                <h5 class="card-title text-left mb-0">Ticket Details
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
    </div>
</asp:Content>


