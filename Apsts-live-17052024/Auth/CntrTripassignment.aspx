<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Cntrmaster.master" AutoEventWireup="true" CodeFile="CntrTripassignment.aspx.cs" Inherits="Auth_CntrTripassignment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="header pb-7">
    </div>
    <div class="container-fluid mt--6">
        <asp:Panel ID="pnltripdetails" runat="server" Visible="true">
            <div class="row align-items-center">
                <div class="col-lg-12 col-md-12">
                    <div class="card card-stats mb-3">
                        <div class="card-header">
                            <div class="row">
                                <div class="col-md-4">
                                    Today Trip Summary
                                </div>
                                <div class="col-md-8 text-right">
                                    <asp:LinkButton ID="lbtnHelp" runat="server" ToolTip="View Instructions" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnarchive" runat="server" CssClass="btn btn-success btn-sm"> <i class="fa fa-file"></i> Archive Trip</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 col-lg-3 border-right">
                                <div class="card-body">
                                    <div class="row mx-1">
                                        <div class="col-lg-12 col-md-12">
                                            <span>Ready To prepare</span>
                                        </div>
                                        <div class="col-md-12 col-lg-12 text-right">
                                            <asp:LinkButton ID="lbtnReadyToprepare" OnClick="lbtnReadyToprepare_Click" runat="server" ToolTip="Ready To prepare" class="font-weight-bold mb-0 text-right" Text="0"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 border-right">
                                <div class="card-body">
                                    <div class="row mx-1">
                                        <div class="col-lg-12 col-md-12">
                                            <span>Upcomming Trip</span>
                                        </div>
                                        <div class="col-md-12 col-lg-12 text-right">
                                            <asp:LinkButton ID="lbtnUpcommingTrip" OnClick="lbtnUpcommingTrip_Click" runat="server" ToolTip="Upcomming Trip" class="font-weight-bold mb-0 text-right" Text="0"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-3 border-right">
                                <div class="card-body">
                                    <div class="row mx-1">
                                        <div class="col-lg-12 col-md-12">
                                            <span>Prepared Trip</span>
                                        </div>
                                        <div class="col-md-12 col-lg-12 text-right">
                                            <asp:LinkButton ID="lbtnPreparedTrip" OnClick="lbtnPreparedTrip_Click" runat="server" ToolTip="Prepared Trip" class="font-weight-bold mb-0 text-right" Text="0"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-lg-3">
                                <div class="card-body">
                                    <div class="row mx-1">
                                        <div class="col-lg-12 col-md-12">
                                            <span>Time Over Trip</span>
                                        </div>
                                        <div class="col-md-12 col-lg-12 text-right">
                                            <asp:LinkButton ID="lbtnTimeOverTrip" OnClick="lbtnTimeOverTrip_Click" runat="server" ToolTip="Time Over Trip" class="font-weight-bold mb-0 text-right" Text="0"></asp:LinkButton>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row align-items-center">
                <div class="col-lg-12 col-md-12">
                    <div class="card card-stats mb-3">
                        <div class="card-header">
                            <div class="row">
                                <div class="col-md-9">
                                    <asp:Label ID="lbltriplist" runat="server" CssClass="text-muted pl-2" Text="Ready To prepare Trip List"></asp:Label>
                                </div>
                                <div class="col-md-3 text-right">
                                    <div class="form-group mb-0">
                                        <div class="input-group">
                                            <asp:TextBox ID="tbtripcode" runat="server" AutoComplete="Off" class="form-control form-control-sm  " placeholder="Search trip code" MaxLength="20"></asp:TextBox>
                                            <div class="input-group-append">
                                                <asp:LinkButton ID="lbtnSearchtrip" runat="server" CssClass="btn btn-success btn-icon-only btn-sm mr-1 " strle="z-i">
                                            <i class="fa fa-search mt-2"></i>
                                                </asp:LinkButton>
                                            </div>
                                            <div class="input-group-append">
                                                <asp:LinkButton ID="lbtnResttrip" runat="server" CssClass="btn btn-danger btn-icon-only btn-sm mr-1">
                                            <i class="fa fa-undo mt-2"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="row pl-2">
                            <div class="col-md-12 col-lg-12 p-4 pr-4">
                                <asp:GridView ID="gvtrip" runat="server" CssClass="table table-hover table-striped " GridLines="None"
                                    ShowHeader="true" BorderStyle="None" AllowPaging="true" PageSize="10" OnRowDataBound="gvtrip_RowDataBound" OnRowCommand="gvtrip_RowCommand"
                                    AutoGenerateColumns="false" HeaderStyle-Font-Size="10pt" HeaderStyle-ForeColor="Black" Font-Size="10pt" DataKeyNames="trip_code,trip_type,trip_date,trip_time,total_seats,total_seats_blocked,total_seats_booked,service_type_name,sp_action">
                                    <Columns>
                                        <asp:BoundField DataField="trip_code" HeaderStyle-Font-Bold="true" HeaderText="Trip Code" />
                                        <asp:BoundField DataField="trip_type" HeaderStyle-Font-Bold="true" HeaderText="Trip Type" />
                                        <asp:BoundField DataField="service_type_name" HeaderStyle-Font-Bold="true" HeaderText="Service Type" />
                                        <asp:BoundField DataField="trip_time" HeaderStyle-Font-Bold="true" HeaderText="Departure Time" />
                                        <asp:BoundField DataField="total_seats" HeaderStyle-Font-Bold="true" HeaderText="Total Seats" />
                                        <asp:BoundField DataField="total_seats_blocked" HeaderStyle-Font-Bold="true" HeaderText="Blocked Seats" />
                                        <asp:BoundField DataField="total_seats_booked" HeaderStyle-Font-Bold="true" HeaderText="Booked Seats" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="left" ItemStyle-Font-Bold="false" HeaderText="Action" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:LinkButton ID="lbtnview" runat="server" CommandName="viewtrip" CssClass="btn btn-primary btn-sm" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                        ToolTip="View Trip Details"> <i class="fa fa-eye"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtngeneratetrip" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Generate Trip"
                                                        CommandName="generatetrip" CssClass="btn btn-success btn-sm"><i class="fa fa-file"></i> Generate</asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                </asp:GridView>
                                <asp:Panel runat="server" ID="pnlNoRecord" Visible="true" CssClass="text-center" Width="100%">
                                    <p class="text-center" style="font-size: 32px; font-weight: bold; color: #e3e3e3; margin-top: 50px;">
                                        Trip Details Not Available
                                    </p>
                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlassigntrip"  runat="server" Visible="false">
            <div class="row col-auto mt--3 float-right">
                <asp:LinkButton ID="lbtnCancel" OnClick="lbtnCancel_Click" Style="border-radius: 25px;margin-right: 10px;z-index: 1;" runat="server" CssClass="btn btn-sm  btn-danger"><i class="fa fa-times"></i></asp:LinkButton>
            </div>
            <div class="row align-items-center">
                <div class="col-lg-12 col-md-12">
                    <div class="card card-stats mb-3" Style="box-shadow: 2px 4px 15px 15px #596166c2;">
                        <div class="card-header">
                            <div class="row">
                                <div class="col-md-9">
                                    <asp:Label ID="lbltrip" runat="server" CssClass="text-muted pl-2" Text="N/A"></asp:Label>
                                </div>

                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row pl-2">
                                <div class="col-md-5 col-lg-5 pr-4 border-right">
                                    <h6 class="heading-small my-0">Trip Details</h6>
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

                                        <div class="col-lg-8">
                                            <span class="form-control-label text-muted font-weight-normal">Stations</span>
                                            <asp:Label ID="lblstations" CssClass="  mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>

                                        </div>
                                        <div class="col-lg-4"></div>
                                        <div class="col-lg-8">
                                            <span class="form-control-label text-muted font-weight-normal">Depot</span>
                                            <asp:Label ID="lbldepot" CssClass="  mb-0 font-weight-bold text-xs" runat="server" Text="NA"></asp:Label>

                                        </div>
                                        <div class="col-lg-4"></div>
                                    </div>
                                    <br />
                                    <h6 class="heading-small my-0">Passenger Details</h6>
                                    <div class="row pl-lg-3 pr-lg-3">
                                        <div class="col-md-12 col-lg-12">
                                            <asp:GridView ID="gvTicketseatDetails" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvTicketseatDetails_RowDataBound"
                                                GridLines="None" CssClass="table text-sm" DataKeyNames="amount_total">
                                                <Columns>

                                                    <asp:BoundField DataField="seat_no" HeaderText="Seat No."></asp:BoundField>
                                                    <asp:TemplateField HeaderText="Passenger">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPassengername" runat="server" Text='<%# Eval("traveller_name") %>'></asp:Label><br />
                                                            <asp:Label ID="lblage" runat="server" Text='<%# Eval("traveller_age") %>'></asp:Label>&nbsp;<span
                                                                style="color: #a29e9e; font-size: 12px;">years</span>,
                                                            <asp:Label ID="lblgender" runat="server" Text='<%# Eval("traveller_gender") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="boarding_station" HeaderText="Bording Station"></asp:BoundField>
                                                    <asp:BoundField DataField="amount_total" HeaderText="Fare(Rs.)"></asp:BoundField>
                                                    <asp:BoundField DataField="bookingmode" HeaderText="Booking Mode"></asp:BoundField>
                                                </Columns>
												<PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                            </asp:GridView>
                                            <hr style="margin-bottom: 2px; margin-top: 10px;" />
                                        </div>

                                        <div class="col-md-5 col-lg-5 text-right">
                                        </div>
                                        <div class="col-md-3 col-lg-3">
                                            <h6 class="heading-small my-0">Total Seats
                                                <asp:Label ID="lbltotseats" runat="server" CssClass="text-muted pl-2" Text="0"></asp:Label></h6>

                                        </div>
                                        <div class="col-md-4 col-lg-4">
                                            <h6 class="heading-small my-0">Total Fare 
                                                <asp:Label ID="lbltotfare" runat="server" CssClass="text-muted pl-2" Text="0"></asp:Label>
                                                <i class="fa fa-rupee-sign"></i></h6>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-7 col-lg-7 pr-4">
                                    <div class="card-header">

                                        <div class="row">
                                            <div class="col-md-4 text-left">
                                                <span class="font-weight-bold text-xs">Waybill Number <span class="text-danger">*</span></span>
                                                <asp:DropDownList ID="ddlwaybill" CssClass="form-control form-control-sm" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-8 text-right">
                                                <asp:LinkButton ID="lbtnviewinstruction" runat="server" ToolTip="View Instructions" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnaboutmodule" runat="server" ToolTip="Abount Module" CssClass="btn btn bg-gradient-primary btn-sm text-white">
                                    <i class="fa fa-history"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>

</asp:Content>

