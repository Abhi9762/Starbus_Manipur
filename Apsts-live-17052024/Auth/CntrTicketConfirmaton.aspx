<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Cntrmaster.master" AutoEventWireup="true" CodeFile="CntrTicketConfirmaton.aspx.cs" Inherits="Auth_CntrTicketConfirmaton" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid mt-1">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row my-2">
            <div class="col-md-12 col-lg-12">
                <div class="card" style="min-height: 460px">
                    <div class="card-body">
                        <div class="row px-3">
                            <div class="col-md-6 border-right">
                                <h6 class="heading-small my-0">Journey Details</h6>
                                <div class="row pl-lg-3 pr-lg-3">
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
                                </div>
                                <br />
                                <h6 class="heading-small my-0">Amount Details </h6>
                                <div class="row pl-lg-3 pr-lg-3">
                                    <div class="col-md-3">
                                        <label class="text-sm">Booking  </label>
                                        <br />
                                        <asp:Label ID="lblbookingAmt" runat="server" CssClass="text-muted pl-2" Text="0"></asp:Label>
                                        <i class="fa fa-rupee-sign text-sm"></i>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="text-sm">Reservation  </label>
                                        <br />
                                        <asp:Label ID="lblReservationCharge" runat="server" CssClass="text-muted pl-2" Text="0"></asp:Label>
                                        <i class="fa fa-rupee-sign text-sm"></i>
                                    </div>
                                    <div class="col-md-6">
                                        <label class="text-sm">Other <span>(Commission+Concession+offer)</span> </label>
                                        <br />
                                        <asp:Label ID="lblother" runat="server" CssClass="text-muted pl-2" Text="0"></asp:Label>
                                        <i class="fa fa-rupee-sign text-sm"></i>
                                    </div>
                                </div>
                                <br />
                                <h6 class="heading-small my-0">Tax Details</h6>
                                <div class="row pl-lg-3 pr-lg-3">
                                    <div class="col-md-12 col-lg-12">
                                        <asp:GridView ID="grdtax" runat="server" AutoGenerateColumns="false" GridLines="None" OnRowDataBound="grdtax_RowDataBound"
                                            HeaderStyle-CssClass="thead-light font-weight-bold" CssClass="table text-sm">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Tax">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltaxname" runat="server" Text='<%# Eval("taxname")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <span style="display: block; padding: 0; font-weight: bold;">Total Tax</span>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount (<i class='fa fa-rupee-sign'></i>)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltaxamt" runat="server" Text='<%# Eval("taxamt")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <span style="display: block; padding: 0; font-weight: bold;">
                                                            <asp:Label ID="lblFTotalTax" runat="server"></asp:Label></span>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <h6 class="heading-small my-0">Passenger's Details</h6>
                                <div class="row pl-lg-3 pr-lg-3">
                                    <div class="col-md-12 col-lg-12">
                                        <asp:GridView ID="gvpassengerdetails" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="thead-light font-weight-bold"
                                            GridLines="None" CssClass="table text-sm" DataKeyNames="">
                                            <Columns>

                                                <asp:BoundField DataField="seatno" HeaderText="Seat No."></asp:BoundField>
                                                <asp:BoundField DataField="travellername" HeaderText="Passenger Name"></asp:BoundField>
                                                <asp:BoundField DataField="travellerage" HeaderText="Passenger Age (Years)"></asp:BoundField>
                                                <asp:BoundField DataField="travellergender" HeaderText="Gender"></asp:BoundField>

                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                        </asp:GridView>
                                        <hr style="margin-bottom: 2px; margin-top: 10px;" />
                                    </div>

                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12 col-lg-12 text-center">
                                        <strong style="color: #507cd1; font-size: 12pt; font-family: verdana;">Total Amount
                            to Pay </strong>
                                        <span style="font-size: 16pt; font-family: verdana; font-weight: bold; color: black; line-height: 1.8;">
                                            <asp:Label ID="lblTotal" runat="server"
                                                ForeColor="black"></asp:Label>
                                            <i class="fa fa-rupee-sign"></i></span>
                                        <br />
                                        <asp:Button ID="btnproceed" runat="server" Text="Confirm and Proceed For Printing" OnClick="btnproceed_Click" OnClientClick="return ShowLoading()" CssClass="btn btn-warning" />
                                        <asp:Button ID="btnprint" runat="server" Text="Print Again" OnClick="btnprint_Click" Visible="false" CssClass="btn btn-warning" />

                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
        CancelControlID="Button1" TargetControlID="Button4" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
        <div class="card" style="min-width: 350px;">
            <div class="card-header">
                <h4 class="card-title">Please Confirm
                </h4>
            </div>
            <div class="card-body" style="min-height: 100px;">
                <asp:Label ID="lblConfirmation" runat="server" Text="Ticket Print Successfully ?"></asp:Label>
                <div style="width: 100%; margin-top: 20px; text-align: right;">
                    <asp:LinkButton ID="lbtnYesConfirmation" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbtnYesConfirmation_Click"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                    <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-3" OnClick="lbtnNoConfirmation_Click"> <i class="fa fa-times"></i> No </asp:LinkButton>
                </div>
            </div>
        </div>
        <div style="visibility: hidden;">
            <asp:Button ID="Button4" runat="server" Text="" />
            <asp:Button ID="Button1" runat="server" Text="" />
        </div>
    </asp:Panel>



    <!--Model Pop-->

    <cc1:ModalPopupExtender ID="mpePage" runat="server" PopupControlID="pnlPage" TargetControlID="Button21"
        CancelControlID="LinkButton71" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPage" runat="server" Style="position: fixed;">
        <div class="modal-content mt-5" style="width: 100%; margin-left: 2%; text-align: center">
            <div class="modal-header">
                <div class="col-md-7">
                    <h3 id="lblTitle" runat="server" class="m-0"></h3>
                </div>
                <div class="col-md-4 text-right" style="text-align: end;">
                </div>
                <div class="col-md-1 text-right" style="text-align: end;">
                    <asp:LinkButton ID="LinkButton71" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="24px">X</asp:LinkButton>
                </div>
            </div>
            <div class="modal-body" style="background-color: #ffffff; overflow-y: auto; padding: 1px; text-align: center">
                <embed src="" style="height: 85vh; width: 70vw;" runat="server" id="embedPage" />
            </div>
        </div>
        <br />
        <div style="visibility: hidden;">
            <asp:Button ID="Button21" runat="server" Text="" />
        </div>
    </asp:Panel>

    <!-- ModalPopupExtender -->
</asp:Content>

