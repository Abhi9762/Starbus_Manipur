<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="sysAdmSpecialTicketCancellation.aspx.cs" Inherits="Auth_sysAdmSpecialTicketCancellation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .sp {
            font-size: 9pt;
            color: #a39d9d;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:HiddenField ID="hidtoken" runat="server" />
    <asp:HiddenField ID="hfSeatList" runat="server" />
    <asp:HiddenField ID="hfNoOfSeat" runat="server" />
     <div class="container-fluid" style="padding-top: 20px;">
        <div class="row">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-header pb-0">
                        <div class="card-title">
                            Search and Cancel
                                <div class="row">
                                    <div class="col-lg-5">
                                        <asp:TextBox ID="txtticketno" runat="server" CssClass="form-control" MaxLength="20" Style="text-transform: uppercase"
                                            placeholder="Enter Ticket No"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <asp:LinkButton class="btn btn-success p-2" OnClick="lbtnsearchticket_Click" ID="lbtnsearchticket" runat="server"
                                                ToolTip="Search Ticket">
                                                                         <i class="fa fa-search" title="Search Cancelled ticket"></i> 
                                            </asp:LinkButton>
                                            <asp:LinkButton class="btn btn-warning p-2 ml-1" OnClick="lbtnresetticket_Click" ID="lbtnresetticket" runat="server" 
                                                ToolTip="Reset ticket">
                                                                         <i class="fa fa-times-circle" title="Reset All Cancelled ticket"></i> 
                                            </asp:LinkButton>

                                        </div>
                                    </div>
                                    <div class="col-lg-5 offset-1">
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
                                                     <p class="m-0 mt-2" style="line-height: 11px; color: orange;">
                                                         Booked By
                                                     </p>
                                                     <p class="m-0">
                                                         <asp:Label ID="lblbookedby" runat="server" Font-Bold="false" Text=""></asp:Label>
                                                     </p>
                                                 </div>
                                             </div>
                                             <div class="row mt-2">
                                                 <div class="col-md-12">
                                                     <asp:GridView ID="grdticketpassenger" runat="server" Width="100%" GridLines="None" AutoGenerateColumns="false"
                                                         class="table" Style="border: 1px solid #ece7e7;"  DataKeyNames="fare_res">
                                                         <Columns>
                                                             <asp:TemplateField ShowHeader="False">
                                                                 <ItemTemplate>
                                                                     <asp:CheckBox runat="server" ID="ChkSelect" AutoPostBack="true" OnCheckedChanged="ChkSelect_CheckedChanged"></asp:CheckBox>
                                                                 </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField ShowHeader="true" HeaderText="Seat">
                                                                 <ItemTemplate>
                                                                     <p class="m-0" style="line-height: 17px;">
                                                                         <asp:Label ID="lblseatno" runat="server" Text='<%# Eval("seatno") %>'></asp:Label>
                                                                     </p>
                                                                 </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField ShowHeader="true" HeaderText="Passenger">
                                                                 <ItemTemplate>
                                                                     <p class="m-0" style="line-height: 17px;">
                                                                         <asp:Label ID="lblpassengername" runat="server" Text='<%# Eval("travellername") %>'></asp:Label>
                                                                         <br />
                                                                         <asp:Label ID="lblgender" runat="server" Font-Size="13px" Text='<%# Eval("travellergender") %>'></asp:Label>,&nbsp;
                                                                                            <asp:Label ID="lblage" runat="server" Font-Size="13px" Text='<%# Eval("travellerage") %>'></asp:Label>
                                                                         Years
                                                                     </p>
                                                                 </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField ShowHeader="true" HeaderText="Fare">
                                                                 <ItemTemplate>
                                                                     <p class="m-0" style="line-height: 17px;">
                                                                         <asp:Label ID="lblAMOUNT_FARE" runat="server" Text='<%# Eval("amountfare") %>'></asp:Label>&nbsp;<i
                                                                             class="fa fa-rupee"></i>
                                                                     </p>
                                                                 </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField ShowHeader="true" HeaderText="Reservation">
                                                                 <ItemTemplate>
                                                                     <p class="m-0" style="line-height: 17px;">
                                                                         <asp:Label ID="lblAMOUNT_ONL_RESERVATION" runat="server" Text='<%# Eval("reservation") %>'></asp:Label>&nbsp;<i
                                                                             class="fa fa-rupee"></i>
                                                                     </p>
                                                                 </ItemTemplate>
                                                             </asp:TemplateField>

                                                             <asp:TemplateField ShowHeader="true" HeaderText="Max Refund">
                                                                 <ItemTemplate>
                                                                     <p class="m-0" style="line-height: 17px;">
                                                                         <asp:Label ID="lblAMOUNT_RESERVATION" runat="server" Text='<%# Eval("fare_res") %>'></asp:Label>&nbsp;<i class="fa fa-rupee"></i>

                                                                     </p>
                                                                 </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Refund Amount">
                                                                 <ItemTemplate>
                                                                     <asp:TextBox ID="txtRefAmt" runat="server" Visible="false" CssClass="form-control" MaxLength="4" Style="text-align: right;"></asp:TextBox>
                                                                     <cc1:FilteredTextBoxExtender ID="FE_RefAmt" runat="server" FilterType="Numbers, Custom"
                                                                         ValidChars="." TargetControlID="txtRefAmt" />
                                                                 </ItemTemplate>
                                                             </asp:TemplateField>
                                                         </Columns>
                                                     </asp:GridView>
                                                 </div>
                                                 <div class="col-md-6 text-right mt-3">

                                                     <asp:Label ID="Label76" runat="server" Text="Enter reason of ticket cancellation"
                                                         Font-Names="Verdana" Font-Size="10pt"></asp:Label><span class="text-danger">*</span>
                                                 </div>
                                                 <div class="col-md-6 text-left mt-3">
                                                     <asp:TextBox ID="txtReson" runat="server" CssClass="form-control" placeholder="Max 50 Char." Width="300px" MaxLength="50"></asp:TextBox>
                                                 </div>
                                                 <div class="col-md-12 mt-3 text-center">
                                                     <asp:Button ID="btnCancelTicket" OnClick="btnCancelTicket_Click" runat="server" Text="Cancel Ticket" CssClass="btn btn-danger" OnClientClick="return ShowLoading();" />
                                                     &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btnBack" runat="server" Text="Back " OnClick="btnBack_Click" CssClass="btn btn-success" OnClientClick="return ShowLoading();" Width="80px" />
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
            <div class="col-md-6">
                <div class="card">
                    <div class="card-header pb-0">
                        <div class="card-title">
                            Special Cancelled Ticket Pending For Refund
                        </div>
                    </div>
                    <div class="card-body" style="padding: 27px; min-height: 70vh;">
                        <asp:GridView ID="gvspclcancelledtkt" runat="server" PageSize="15" OnRowCommand="gvspclcancelledtkt_RowCommand" AutoGenerateColumns="False" GridLines="None" ShowHeader="true" AllowSorting="true" AllowPaging="true" CssClass="table table-hover"
                            HeaderStyle-CssClass="thead-light font-weight-bold" DataKeyNames="cancelrefno" OnPageIndexChanging="gvspclcancelledtkt_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Cancellation Details">
                                    <ItemTemplate>
                                        <span class="sp"> Cancellation No.</span><br />
                                        <asp:Label ID="lblCANCELLATION_REF_NO" runat="server" Text='<%# Eval("cancelrefno") %>'></asp:Label><br />
                                        <span class="sp"> Cancellation Date/Time</span><br />
                                        <asp:Label ID="lblCANCELLATION_DATE" runat="server" Text='<%# Eval("canceldate") %>'></asp:Label><br />
                                        <span class="sp"> Cancellation Amount</span><br />
                                         <asp:Label ID="lblCANCELLATION_AMT" runat="server" Text='<%# Eval("cancelamt") %>'></asp:Label> <i class="fa fa-rupee"></i>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ticket Details">
                                    <ItemTemplate>
                                        <span class="sp"> Ticket No.</span><br />
                                        <asp:Label ID="lblTICKET_NO" runat="server" Text='<%# Eval("ticketno") %>'></asp:Label><br />
                                        <span class="sp"> Journey Date</span><br />
                                        <asp:Label ID="lblJOURNEY_DATE" runat="server" Text='<%# Eval("journeydate") %>'></asp:Label><br />
                                        <span class="sp"> Booking Amount</span><br />
                                        <asp:Label ID="lblAMOUNT_TOTAL" runat="server" Text='<%# Eval("amounttotal") %>'></asp:Label> <i class="fa fa-rupee"></i>
                                    </ItemTemplate>
                                </asp:TemplateField>                               
                                <asp:TemplateField HeaderText="Booking Details">
                                    <ItemTemplate>
                                        <span class="sp"> Booking Date/Time</span><br />
                                        <asp:Label ID="lblBOOKING_DATETIME" runat="server" Text='<%# Eval("bookingtime") %>'></asp:Label><br />
                                        <span class="sp"> Booked By</span><br />
                                        <asp:Label ID="lblbooked_by" runat="server" Text='<%# Eval("bookby") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtncncvocher" runat="server" CssClass="btn btn-success" ToolTip="View Cancellation Voucher" CommandName="PrintVoucher" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'> <i class="fa fa-eye" title ="View Cancellation Voucher"></i> View </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                        </asp:GridView>
                        <div class="text-center busListBox" id="dvnospclcancelledtkt" runat="server"
                            style="padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold; color: #e3e3e3;"
                            visible="true">
                            No Special Cancelled Ticket Pending For Refund
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <cc1:ModalPopupExtender ID="mperror" runat="server" PopupControlID="pnlerror" CancelControlID="lbtnclose1"
                TargetControlID="Button1" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlerror" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Please Check
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblerrormsg" runat="server" Text="Please Check entered values."></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnclose1" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button1" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
         <div class="row">
        <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
            CancelControlID="lbtnNoConfirmation" TargetControlID="Button2" BackgroundCssClass="modalBackground" BehaviorID="bvConfirm">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
            <div class="card" style="width: 350px;">
                <div class="card-header">
                    <h4 class="card-title text-left mb-0">Confirmation
                    </h4>
                </div>

                <div class="card-body text-left pt-2" style="min-height: 100px;">

                    <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                    <div style="width: 100%; margin-top: 20px; text-align: right;">
                        <asp:LinkButton ID="lbtnYesConfirmation" runat="server" OnClick="lbtnYesConfirmation_Click" CssClass="btn btn-sm btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                        <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-sm btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button2" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
    </div>
</asp:Content>

