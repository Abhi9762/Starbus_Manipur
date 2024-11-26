<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Cntrmaster.master" AutoEventWireup="true" CodeFile="CntrTicketQuery.aspx.cs" Inherits="Auth_CntrTicketQuery" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
  
    <div class="container-fluid mt-2 pb-5">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row align-items-center">
            <div class="col-lg-12 col-md-12">
                <div class="card card-stats mb-3">
                    <div class="card-header">
                        <div class="row px-2">
                            <div class="col-lg-4"><span>Ticket Query</span></div>
                            <div class="col-lg-2">
                                <label>Booking Date</label>
                                <asp:TextBox ID="tbbookingdate" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="DD/MM/YYYY" runat="server" MaxLength="10"></asp:TextBox>
                            </div>
                            <div class="col-lg-2">
                                <label>Journey Date</label>
                                <asp:TextBox ID="tbjourneydate" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="DD/MM/YYYY" runat="server" MaxLength="10"></asp:TextBox>
                            </div>

                            <div class="col-lg-2">
                                <label>PNR/Mobile/Name/Email</label>
                                <asp:TextBox ID="tbvalue" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="Ticket No/Name of Traveller" runat="server" MaxLength="20"></asp:TextBox>
                            </div>
                            <div class="col-lg-2 pt-4">
                                <asp:LinkButton ID="lbtnsearch" runat="server" OnClick="lbtnsearch_Click" CssClass="btn btn-success btn-sm"> <i class="fa fa-eye"></i></asp:LinkButton>
                                <asp:LinkButton ID="lbtnreset" runat="server" OnClick="lbtnreset_Click" CssClass="btn btn-warning btn-sm"> <i class="fa fa-undo"></i></asp:LinkButton>
                            </div>
                        </div>

                    </div>
                    <div class="card-body">
                        <div class="row px-2">
                            <div class="col-md-5 col-lg-5 pr-4 border-right">
                                <asp:GridView ID="gvtickets" runat="server" AutoGenerateColumns="false" GridLines="None" ShowHeader="false" Visible="false"
                                    OnPageIndexChanging="gvtickets_PageIndexChanging" AllowPaging="true" PageSize="20" OnRowDataBound="gvtickets_RowDataBound" OnRowCommand="gvtickets_RowCommand"
                                    ShowFooter="false" HeaderStyle-CssClass="thead-light font-weight-bold" CssClass="table text-sm" DataKeyNames="ticket_no,booked_by_type">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <div class="row pl-lg-3 pr-lg-3">
                                                    <div class="col-lg-10">
                                                        <div class="row">
                                                            <div class="col-lg-6">
                                                                <span class="form-control-label text-muted font-weight-normal">Ticket No</span>
                                                                <asp:Label ID="lblticket_no" runat="server" CssClass="  mb-0 font-weight-bold text-xs" Text='<%# Eval("ticket_no")%>'></asp:Label>
                                                                <br />
                                                                <span class="form-control-label text-muted font-weight-normal">Status</span>
                                                                <asp:Label ID="lblticket_status" runat="server" CssClass="  mb-0 font-weight-bold text-xs" Text='<%# Eval("ticket_status")%>'></asp:Label>
                                                            </div>
                                                            <div class="col-lg-6">
                                                                <span class="form-control-label text-muted font-weight-normal">Booking Date</span>
                                                                <asp:Label ID="lblbooking_datetime" runat="server" CssClass="  mb-0 font-weight-bold text-xs" Text='<%# Eval("booking_datetime")%>'></asp:Label>
                                                                <br />
                                                                <span class="form-control-label text-muted font-weight-normal">Journey Date</span>
                                                                <asp:Label ID="lbljourney_date" runat="server" CssClass="  mb-0 font-weight-bold text-xs" Text='<%# Eval("journey_date")%>'></asp:Label>
                                                            </div>
                                                            <div class="col-lg-6">
                                                                <span class="form-control-label text-muted font-weight-normal">Booked By</span>
                                                                <asp:Label ID="lblbooked_by_type" runat="server" CssClass="  mb-0 font-weight-bold text-xs" Text='<%# Eval("booked_by_type")%>'></asp:Label>
                                                                (<asp:Label ID="lblbooked_by_user" runat="server" CssClass="  mb-0 font-weight-bold text-xs" Text='<%# Eval("booked_by_user")%>'></asp:Label>)
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-2 text-right">
                                                        <asp:LinkButton ID="lbtnview" runat="server" CssClass="btn btn-success btn-sm" CommandName="viewTicket" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'> Select</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                </asp:GridView>
                                <asp:Panel ID="pnlnotickets" runat="server" Width="100%" Visible="true">
                                    <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                        <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 35px; padding-bottom: 35px; font-size: 25px; font-weight: bold;">
                                            No Ticket Details available<br />
                                            Please select another combinaton of Perameter
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="col-md-7 col-lg-7 pr-4">
                                <asp:Panel ID="pnlticketdetails" runat="server" Visible="false">
                                    <div class="row px-3">
                                        <div class="col-lg-6 border-bottom pb-2">
                                            <h6 class="heading-small my-0">Booking Details</h6>
                                        </div>
                                        <div class="col-lg-6 border-bottom text-right">
                                            <asp:LinkButton ID="lbtnlog" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbtnlog_Click"> <i class="fa fa-eye"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="row row px-3">
                                        <div class="col-lg-8">
                                            <span class="form-control-label text-muted font-weight-normal">Service</span>
                                            <asp:Label ID="lblservice" runat="server" CssClass="  mb-0 font-weight-bold text-xs" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-4"></div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">Servie Type</span>
                                            <asp:Label ID="lblservicetype" CssClass="  mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>

                                        </div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">Journey Date/Time</span>
                                            <asp:Label ID="lbljourneydatetime" CssClass="  mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>

                                        </div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">Departure Time</span>
                                            <asp:Label ID="lbldepaturetime" CssClass="  mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>

                                        </div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">Arrival Time</span>
                                            <asp:Label ID="lblarrivaltime" CssClass="  mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>

                                        </div>
                                        <div class="col-lg-8">
                                            <span class="form-control-label text-muted font-weight-normal">Stations</span>
                                            <asp:Label ID="lblstations" CssClass="  mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>

                                        </div>
                                        <div class="col-lg-4"></div>
                                        <div class="col-lg-8">
                                            <span class="form-control-label text-muted font-weight-normal">Booking Date/Time</span>
                                            <asp:Label ID="lblbookingdatetime" CssClass="  mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>

                                        </div>
                                        <div class="col-lg-4"></div>
                                        <div class="col-lg-8">
                                            <span class="form-control-label text-muted font-weight-normal">Boarding Station</span>
                                            <asp:Label ID="lblboardingstation" CssClass="  mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>

                                        </div>
                                        <div class="col-lg-4"></div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">Traveller Mobile No.</span>
                                            <asp:Label ID="lblmobileno" CssClass="  mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>

                                        </div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">Traveller EmailId</span>
                                            <asp:Label ID="lblemail" CssClass="  mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>

                                        </div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">Total Fare </span>
                                            <asp:Label ID="lbltotamount" CssClass="  mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>
                                            <i class="fa fa-rupee-sign"></i>

                                        </div>

                                    </div>
                                    <hr />
                                    <div class="row px-3">
                                        <div class="col-lg-6 border-bottom pb-2">
                                            <h6 class="heading-small my-0">Passenger Details</h6>
                                        </div>
                                        <div class="col-lg-6 border-bottom text-right">
                                        </div>
                                    </div>
                                    <div class="row px-3">
                                        <div class="col-md-12 col-lg-12">
                                            <asp:GridView ID="gvpassengerdetails" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="thead-light font-weight-bold"
                                                GridLines="None" CssClass="table text-sm" DataKeyNames="">
                                                <Columns>

                                                    <asp:BoundField DataField="seatno" HeaderText="Seat No."></asp:BoundField>
                                                    <asp:BoundField DataField="travellername" HeaderText="Passenger Name"></asp:BoundField>
                                                    <asp:BoundField DataField="travellerage" HeaderText="Passenger Age (Years)"></asp:BoundField>
                                                    <asp:BoundField DataField="travellergender" HeaderText="Gender"></asp:BoundField>
                                                    <asp:TemplateField ShowHeader="false">
                                                        <ItemTemplate>
                                                            <%# Eval("current_status")%><br />
                                                            <span class="text-muted text-xs"></span><%# (String.IsNullOrEmpty(Eval("cncellation_ref_no").ToString()) ? String.Empty : String.Format("Ref No: {0}", Eval("cncellation_ref_no"))) %>
                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                

                                                </Columns>
                                                <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                            </asp:GridView>
                                            <hr style="margin-bottom: 2px; margin-top: 10px;" />
                                        </div>

                                    </div>
                                    <div class="row pl-2 pt-3">
                                        <div class="col-lg-12 text-center">
                                            <asp:LinkButton ID="lbtnprint" runat="server" OnClick="lbtnprint_Click" CssClass="btn btn-success btn-md">Print Ticket</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnresendsms" runat="server" OnClick="lbtnresendsms_Click"  CssClass="btn btn-warning btn-md">Resend SMS</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnresendemail" runat="server" CssClass="btn btn-primary btn-md">Resend Email</asp:LinkButton>
                                            <asp:LinkButton ID="lbtncancelticket" runat="server" OnClick="lbtncancelticket_Click" CssClass="btn btn-danger btn-md">Cancel Ticket/Seats</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnprintcancelvoucher" runat="server" OnClick="lbtnprintcancelvoucher_Click" CssClass="btn btn-warning btn-md">Print Cancel Voucher</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnspecialrefund" runat="server" OnClick="lbtnspecialrefund_Click" CssClass="btn btn-info btn-md">Special Refund</asp:LinkButton>

                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlnoticketdetails" runat="server" Width="100%" Visible="true">
                                    <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                        <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 35px; padding-bottom: 35px; font-size: 25px; font-weight: bold;">
                                            Please select Ticket for details<br />
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
            <cc1:ModalPopupExtender ID="mpticketlog" runat="server" PopupControlID="pnlticketlog"
                CancelControlID="lbtnok" TargetControlID="Button4" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlticketlog" runat="server" Style="position: fixed;">
                <div class="card" style="min-width: 350px; max-width: 850px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">
                            <asp:Label ID="lblticktloghd" runat="server"></asp:Label>
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:GridView ID="gvticketlog" runat="server" AutoGenerateColumns="false" GridLines="None" HeaderStyle-CssClass="thead-light font-weight-bold" CssClass="table text-sm" DataKeyNames="">
                            <Columns>
                                <asp:BoundField DataField="ticket_status_name" HeaderText="Status"></asp:BoundField>
                                <asp:BoundField DataField="current_status_datetime" HeaderText="Updated Date/Time"></asp:BoundField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                        </asp:GridView>
                    </div>
                    <div class="card-footer text-right">
                        <asp:LinkButton ID="lbtnok" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> Close </asp:LinkButton>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button4" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>


    </div>

</asp:Content>

