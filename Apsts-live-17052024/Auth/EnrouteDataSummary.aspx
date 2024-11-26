<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="EnrouteDataSummary.aspx.cs" Inherits="Auth_EnrouteDataSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        html,
        body,
        #map {
            margin: 0;
            padding: 0;
            width: 100%;
            height: 100vh;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid watermark" id="maindiv" style="margin-top: 10px">
        <div class="card">
            <div class="card-header">
                <div class="row">
                    <div class="col-lg-11 text-center">
                        <h4>
                            <asp:Label runat="server" class="" Style="font-size: 13pt;" Text="Waybill Details" ID="lblHeading"></asp:Label>
                        </h4>

                    </div>
                    <div class="col-lg-1">
                        <asp:LinkButton ID="lbtnGoback" runat="server" CssClass="btn btn-danger btn-sm float-right" OnClick="lbtnGoback_Click"><i class="fa fa-backward"></i>Go Back</asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="card-body">
                <div class="row mt-2">
                    <div class="col-sm-3">
                        <i class="fa fa-road"></i>
                        <asp:Label runat="server" Text="Waybill No" Font-Bold="true" CssClass="form-control-label"></asp:Label>
                        -
                            <asp:Label runat="server" ID="tbWaybillNo" CssClass="form-control-label"></asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <i class="fa fa-bus mr-1"></i>
                        <asp:Label runat="server" Text="Bus No" Font-Bold="true" CssClass="form-control-label"></asp:Label>
                        -
                            <asp:Label runat="server" ID="lblBusNo" CssClass="form-control-label text-uppercase"></asp:Label>
                    </div>
                    <div class="col-sm-6">
                        <i class="fa fa-server mr-1"></i>
                        <asp:Label runat="server" Text="Service Name" Font-Bold="true" CssClass="form-control-label"></asp:Label>
                        -
                            <asp:Label runat="server" ID="lblServiceName" CssClass="form-control-label text-uppercase"></asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <i class="fa fa-user mr-1"></i>
                        <asp:Label runat="server" Text="Driver" Font-Bold="true" CssClass="form-control-label"></asp:Label>
                        -
                            <asp:Label runat="server" CssClass="form-control-label text-uppercase" ID="lblDriver"></asp:Label>
                    </div>
                    <div class="col-sm-4">
                        <i class="fa fa-user mr-1"></i>
                        <asp:Label runat="server" Text="Conductor" Font-Bold="true" CssClass="form-control-label"></asp:Label>
                        -
                            <asp:Label runat="server" CssClass="form-control-label text-uppercase" ID="lblConductor"></asp:Label>
                    </div>

                </div>

                <hr style="margin: 10px" />
                <div class="row">
                    <div class="col-sm-12">
                        <h4>
                            <asp:Label runat="server">1. Collection Details</asp:Label></h4>
                    </div>
                    <div class="col-sm">
                        <asp:Label runat="server" Text="Online Ticketsaa " CssClass="form-control-label">Online Tickets</asp:Label>
                        <asp:Label runat="server" Font-Size="16px" Font-Bold="true" Text="0 ₹" ID="tbOnlineTktamt" CssClass="form-control-label"></asp:Label>
                    </div>
                    <div class="col-sm">
                        <asp:Label runat="server" Text="Enroute Tickets" CssClass="form-control-label">Enroute Tickets</asp:Label>
                        <asp:Label runat="server" Font-Size="16px" Font-Bold="true" ID="tbEnrouteTktamt" Text="0 ₹" CssClass="form-control-label"></asp:Label>
                        <hr style="margin-top: 0px; margin-bottom: 0px" />
                        <asp:Label runat="server" Font-Size="10px" Text="Cash" CssClass="form-control-label"></asp:Label>
                        <asp:Label runat="server" Font-Size="12px" Font-Bold="true" ID="tbenroutecash" Text="0 ₹" CssClass="form-control-label"></asp:Label>
                        <asp:Label runat="server" Font-Size="10px" Text="QR" CssClass="form-control-label ml-3"></asp:Label>
                        <asp:Label runat="server" Font-Size="12px" Font-Bold="true" ID="tbenrouteqr" Text="0 ₹" CssClass="form-control-label"></asp:Label>

                    </div>
                    <div class="col-sm">
                        <asp:Label runat="server" Text="Luggage" CssClass="form-control-label">Luggage</asp:Label>
                        <asp:Label runat="server" Font-Size="16px" ID="tbLuggageAmt" Font-Bold="true" Text="0 ₹" CssClass="form-control-label"></asp:Label>
                        <hr style="margin-top: 0px; margin-bottom: 0px" />
                        <asp:Label runat="server" Font-Size="10px" Text="Cash" CssClass="form-control-label"></asp:Label>
                        <asp:Label runat="server" Font-Size="12px" Font-Bold="true" ID="tbluggagecash" Text="0 ₹" CssClass="form-control-label"></asp:Label>
                        <asp:Label runat="server" Font-Size="10px" Text="QR" CssClass="form-control-label ml-3"></asp:Label>
                        <asp:Label runat="server" Font-Size="12px" Font-Bold="true" ID="tbluggageqr" Text="0 ₹" CssClass="form-control-label"></asp:Label>

                    </div>
                    <div class="col-sm">
                        <asp:Label runat="server" Text="Dhaba" CssClass="form-control-label"></asp:Label>
                        <asp:Label runat="server" Font-Size="16px" ID="tbDhabaAmt" Font-Bold="true" Text="0 ₹" CssClass="form-control-label"></asp:Label>
                    </div>
                    <div class="col-sm">
                        <asp:Label runat="server" Text="Other Earning" CssClass="form-control-label"></asp:Label>
                        <asp:Label runat="server" Font-Size="16px" ID="tbOtherEarningAmt" Font-Bold="true" Text="0 ₹" CssClass="form-control-label"></asp:Label>
                        <hr style="margin-top: 0px; margin-bottom: 0px" />
                        <asp:Label runat="server" Font-Size="10px" Text="Cash" CssClass="form-control-label"></asp:Label>
                        <asp:Label runat="server" Font-Size="12px" Font-Bold="true" ID="tbotherEarningcash" Text="0 ₹" CssClass="form-control-label"></asp:Label>
                        <asp:Label runat="server" Font-Size="10px" Text="QR" CssClass="form-control-label ml-3"></asp:Label>
                        <asp:Label runat="server" Font-Size="12px" Font-Bold="true" ID="tbotherEarningqr" Text="0 ₹" CssClass="form-control-label"></asp:Label>

                    </div>
                    <div class="col-sm">
                        <asp:Label runat="server" Text="Refunded Tickets" Font-Bold="true" CssClass="form-control-label">Refund Ticket</asp:Label>
                        <asp:Label runat="server" Font-Size="16px" ID="tbRefundedTickets" Font-Bold="true" Text="0 ₹" CssClass="form-control-label"></asp:Label>
                    </div>
                    <div class="col-sm">
                        <asp:Label runat="server" Text="Total " CssClass="form-control-label"></asp:Label>
                        <asp:Label runat="server" Font-Size="16px" Font-Bold="true" ID="tbTotalEarningAmt" Text="0 ₹" CssClass="form-control-label"></asp:Label>
                        <hr style="margin-top: 0px; margin-bottom: 0px" />
                        <asp:Label runat="server" Font-Size="10px" Text="Cash" CssClass="form-control-label"></asp:Label>
                        <asp:Label runat="server" Font-Size="12px" Font-Bold="true" ID="tbtotalcollectioncash" Text="0 ₹" CssClass="form-control-label"></asp:Label>
                        <asp:Label runat="server" Font-Size="10px" Text="QR" CssClass="form-control-label ml-3"></asp:Label>
                        <asp:Label runat="server" Font-Size="12px" Font-Bold="true" ID="tbtotalcollectionqr" Text="0 ₹" CssClass="form-control-label"></asp:Label>

                    </div>
                </div>


                <hr style="margin: 10px" />
                <div class="row">
                    <div class="col-sm-7">
                        <div class="row mt-4">
                            <div class="col-sm-12">
                                <h4>
                                    <asp:Label runat="server">2. Expenditure Details</asp:Label></h4>
                            </div>
                            <div class="col-sm">
                                <asp:Label runat="server" Text="Toll Paid" CssClass="form-control-label">Toll Paid </asp:Label>

                                <asp:Label class="form-control-label" Font-Size="16px" runat="server" ID="tbTollpaid" Text="0 ₹"
                                    placeholder="Max 6 Digit"></asp:Label>
                            </div>
                            <div class="col-sm">
                                <asp:Label runat="server" Text="Parking" CssClass="form-control-label">Parking</asp:Label>

                                <asp:Label class="form-control-label" runat="server" Font-Bold="true" ID="tbParking" Text="0 ₹" Font-Size="16px"></asp:Label>
                            </div>
                            <div class="col-sm">
                                <asp:Label runat="server" Text="Other Expense" CssClass="form-control-label">Other Expense</asp:Label>

                                <asp:Label class="form-control-label" runat="server" Font-Bold="true" ID="tbOtherExp" Text="0 ₹" Font-Size="16px"></asp:Label>
                            </div>
                            <div class="col-sm">
                                <asp:Label runat="server" Text="Total " CssClass="form-control-label"> Total</asp:Label>

                                <asp:Label class="form-control-label" runat="server" Font-Bold="true" ID="tbTotalExpenses" Text="0 ₹" Font-Size="16px"
                                    placeholder="Max 6 Digit"></asp:Label>
                            </div>

                        </div>

                        <hr style="margin: 10px" />
                        <div class="row mt-4" runat="server" id="div1">
                            <div class="col-lg-12">
                                <h4>
                                    <asp:Label runat="server">3. Inspection Details</asp:Label></h4>
                            </div>
                            <div class="col-lg-12">
                                <div class="row">
                                    <div class="col-sm">
                                        <asp:Label runat="server" Text="" Font-Bold="true" CssClass="form-control-label">Total Inspections</asp:Label>
                                        <asp:Label class="form-control-label" Font-Size="16px" runat="server" ID="lblNoofInspection" Text="0"
                                            placeholder="Max 6 Digit"></asp:Label>
                                    </div>
                                    <div class="col-sm">
                                        <asp:Label runat="server" Text="Parking" Font-Bold="true" CssClass="form-control-label">OK Inspections</asp:Label>

                                        <asp:Label class="form-control-label" runat="server" Font-Bold="true" ID="lblokOnspection" Text="0" Font-Size="16px"></asp:Label>
                                    </div>
                                    <div class="col-sm">
                                        <asp:Label runat="server" Text="Other Expense" Font-Bold="true" CssClass="form-control-label">WT Inspections</asp:Label>

                                        <asp:Label class="form-control-label" runat="server" Font-Bold="true" ID="lblwtInspections" Text="0" Font-Size="16px"></asp:Label>

                                    </div>

                                </div>
                            </div>
                        </div>

                        <hr style="margin: 10px" />
                        <div class="row mt-4" runat="server" id="div3">
                            <div class="col-lg-12">
                                <h4>
                                    <asp:Label runat="server">4. Concessional Journey Details</asp:Label></h4>
                            </div>
                            <div class="col-lg-12">
                                <div class="row">
                                    <div class="col-sm">
                                        <asp:Label runat="server" Text="" Font-Bold="true" CssClass="form-control-label">Total Concessional Tickets</asp:Label>
                                        <asp:Label class="form-control-label" Font-Size="16px" runat="server" ID="lbltotalConcession" Text="0"
                                            placeholder="Max 6 Digit"></asp:Label>
                                    </div>
                                    <div class="col-sm">
                                        <asp:Label runat="server" Text="Parking" Font-Bold="true" CssClass="form-control-label">Concessional Amount</asp:Label>

                                        <asp:Label class="form-control-label" runat="server" Font-Bold="true" ID="lblConcessiontktamt" Text="0 ₹" Font-Size="16px"></asp:Label>
                                    </div>
                                    <div class="col-sm">
                                        <asp:Label runat="server" Text="Other Expense" Font-Bold="true" CssClass="form-control-label">Discount Amount</asp:Label>

                                        <asp:Label class="form-control-label" runat="server" Font-Bold="true" ID="lblconcessiondiscountamt" Text="0 ₹" Font-Size="16px"></asp:Label>

                                    </div>

                                </div>
                            </div>
                        </div>

                        <hr style="margin: 10px" />
                        <div class="row mt-4">
                            <div class="col-sm-12">
                                <h4>
                                    <asp:Label runat="server">5. Trip Details</asp:Label>(<i class="fa fa-rupee-sign"></i>)</h4>
                            </div>
                            <div class="col-lg-12">
                                <asp:GridView ID="gvServiceTrips" runat="server" AutoGenerateColumns="false" GridLines="None"
                                    AllowPaging="true" CssClass="table" DataKeyNames="tripno,waybill" ShowHeader="false" OnRowDataBound="gvServiceTrips_RowDataBound"
                                    OnRowCommand="gvServiceTrips_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="card">
                                                    <div class="row p-2">
                                                        <div class="col-sm-12 pb-1">
                                                            Trip No - 
                                                    <asp:Label class="form-control-label" runat="server" Font-Bold="true" Text='<%# Eval("tripno") %>' Font-Size="13px"></asp:Label>,
                                                      <asp:Label class="form-control-label" runat="server" Font-Bold="true" Text='<%# Eval("fromstationtostation") %>' Font-Size="13px"></asp:Label>
                                                            (<asp:Label class="" runat="server" Text='<%# Eval("directions") %>' Font-Size="13px"></asp:Label>),
                                                    
                                                    <asp:Label class="form-control-label" runat="server" Font-Bold="true" Text='<%# Eval("totaldistance") %>' Font-Size="13px"></asp:Label>km,
                                                    In Waybill -
                                                    <asp:Label class="form-control-label" runat="server" Font-Bold="true" Text='<%# Eval("inwaybill") %>' Font-Size="13px"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            Enroute Tickets - 
                                                    <asp:Label runat="server" Font-Size="13px" Font-Bold="true" ID="tbEnrouteTktamt" CssClass="form-control-label"></asp:Label>
                                                            <hr style="margin-top: 0px; margin-bottom: 0px" />
                                                            Cash - 
                                                    <asp:Label runat="server" Font-Size="13px" Font-Bold="true" ID="tbenroutecasht" CssClass="form-control-label mr-2"></asp:Label>
                                                            QR - 
                                                    <asp:Label runat="server" Font-Size="13px" Font-Bold="true" ID="tbenrouteqrt" CssClass="form-control-label"></asp:Label>

                                                        </div>
                                                        <div class="col-sm-4">
                                                            Luggage - 
                                                    <asp:Label runat="server" Font-Size="13px" Font-Bold="true" ID="lbltotluggaget" CssClass="form-control-label"></asp:Label>
                                                            <hr style="margin-top: 0px; margin-bottom: 0px" />
                                                            Cash - 
                                                    <asp:Label runat="server" Font-Size="13px" Font-Bold="true" ID="lblcashloggaget" CssClass="form-control-label mr-2"></asp:Label>
                                                            QR - 
                                                    <asp:Label runat="server" Font-Size="13px" Font-Bold="true" ID="lblqrluggaget" CssClass="form-control-label"></asp:Label>

                                                        </div>
                                                        <div class="col-sm-4">
                                                            Other Earning - 
                                                    <asp:Label runat="server" Font-Size="13px" Font-Bold="true" ID="lblotherearningt" CssClass="form-control-label"></asp:Label>
                                                            <hr style="margin-top: 0px; margin-bottom: 0px" />
                                                            Cash - 
                                                    <asp:Label runat="server" Font-Size="13px" Font-Bold="true" ID="lblcashearningt" CssClass="form-control-label mr-2"></asp:Label>
                                                            QR - 
                                                    <asp:Label runat="server" Font-Size="13px" Font-Bold="true" ID="lblqrearningt" CssClass="form-control-label"></asp:Label>

                                                        </div>
                                                        <div class="col-sm-3 pt-1">
                                                            Online Tickets - 
                                                    <asp:Label runat="server" Font-Size="13px" Font-Bold="true" ID="lblonlinetickett" CssClass="form-control-label"></asp:Label>
                                                        </div>

                                                        <div class="col-sm-3 pt-1">
                                                            Refunded Tickets - 
                                                    <asp:Label runat="server" Font-Size="13px" Font-Bold="true" ID="lblrefundedt" CssClass="form-control-label"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-3 pt-1">
                                                            Total Expenses - 
                                                    <asp:Label runat="server" Font-Size="13px" Font-Bold="true" ID="lbltotexpenset" CssClass="form-control-label"></asp:Label>
                                                        </div>
                                                        <div class="col-3">
                                                            <asp:Label runat="server" Text="Parking" CssClass="form-control-label">Total Passengers - </asp:Label>
                                                            <asp:Label class="form-control-label" runat="server" Font-Bold="true"
                                                                Text='<%# Eval("totaltickets") %>' Font-Size="16px"></asp:Label>
                                                            <asp:LinkButton runat="server" CommandName="view" ToolTip="View Tickets On Map"
                                                                CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' OnClientClick="return ShowLoading()"
                                                                CssClass="btn btn-primary btn-sm float-right"><i class="fa fa-map-marker-alt"></i></asp:LinkButton>
                                                        </div>
                                                    </div>

                                                </div>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="GridPager" ForeColor="Black" />
                                </asp:GridView>
                                <asp:Panel ID="pnlNoRecordTrip" runat="server" Width="100%" Visible="true">
                                    <div class="col-md-12 p-0" style="text-align: center;">
                                        <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
                                            Trips Not Available<br />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-5 mt-4">
                        <h4>
                            <asp:Label runat="server">6. Tickets On Map</asp:Label>(<i class="fa fa-map-marker"></i>)</h4>
                        <div class="row ">
                            <div class="col-sm-12">

                                <div class="card shadow p-2" style="min-height: 200px;" id="dvmap" runat="server" visible="false">
                                    <div id="map" class="card shadow p-2" visible="true"></div>
                                </div>
                                <div id="dvnomap" class="card shadow p-2" style="min-height: 200px;" runat="server" visible="true">
                                    <asp:Label runat="server" CssClass="text-center mt-5" Font-Bold="true" ForeColor="LightGray" Font-Size="25px" ID="lblticketmap" Text="Please Select Trip <br>For Tickets On Map"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

