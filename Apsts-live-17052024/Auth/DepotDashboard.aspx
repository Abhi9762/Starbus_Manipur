<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Depotmaster.master" AutoEventWireup="true" CodeFile="DepotDashboard.aspx.cs" Inherits="Auth_DepotDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<meta http-equiv="refresh" content="30" />--%>

    <style>
        .card-heading {
            font-size: 15px;
        }

        .total-tx {
            font-size: 12px;
        }
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid pt-top ">
        <div class="row">

            <div class="col-md-4">
                <div class="card shadow">
                    <div class="card-body">
                        <div class="row">
                            <asp:Label runat="server" Text="Summary As On" Font-Size="Larger"></asp:Label>&nbsp;
                            <asp:Label runat="server" ID="lblsummarydate" Font-Bold="true" Font-Size="Larger" Text=""></asp:Label>
                        </div>


                        <div class="row">
                            <div class="col-md-11">
                                <asp:Label runat="server" Text=" 1. Trip Chart (Online Booking)" Font-Size="Medium"></asp:Label>
                            </div>
                            <div class="col-md-1 float-right">
                                <asp:LinkButton ID="lbtntripchart" OnClick="tricpchart_Click" runat="server"><i class="fa fa-sync-alt"></i></asp:LinkButton>
                            </div>
                        </div>




                        <asp:UpdatePanel ID="updatePanel" runat="server">
                            <ContentTemplate>
                                <div class="row mt-1">
                                    <div class="col-md-4 stretch-card transparent">
                                        <div class="card card-dark-blue" style="height: 65px">
                                            <div class="card-body text-center">
                                                <asp:Label runat="server" Text="Issued" Font-Size="Smaller"></asp:Label>
                                                <br>
                                                </>
                                                <asp:Label runat="server" ID="lblpreparedcount" Font-Size="Smaller"></asp:Label>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 stretch-card transparent">
                                        <div class="card card-dark-blue" style="height: 65px;">
                                            <div class="card-body text-center">
                                                <asp:Label runat="server" Text="Upcoming" Font-Size="Smaller"></asp:Label>
                                                <br>
                                                </>
                                                <asp:Label runat="server" ID="lblUpcomingCount" Font-Size="Smaller"></asp:Label>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 stretch-card transparent">
                                        <div class="card card-dark-blue" style="height: 65px;">
                                            <div class="card-body text-center">
                                                <asp:Label runat="server" Text="Ready To Print" Font-Size="Smaller"></asp:Label>
                                                <br />
                                                <asp:Label runat="server" ID="lblReadycount" Font-Size="Smaller"></asp:Label>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <hr />
                        <div class="row">
                            <div class="col-md-11">
                                <asp:Label runat="server" Text=" 2. Current Booking" Font-Size="Medium"></asp:Label>
                            </div>
                            <div class="col-md-1 float-right">
                                <asp:LinkButton ID="LinkButton1" runat="server"><i class="fa fa-sync-alt"></i></asp:LinkButton>
                            </div>
                        </div>

                        <asp:UpdatePanel ID="updatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="row mt-1">
                                    <div class="col-md-6  stretch-card transparent">
                                        <div class="card card-dark-blue" style="height: 105px;">
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-md-6"><span style="font-size: smaller">Total Trip</span> </div>
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblttrip" Text="0" Font-Size="Smaller"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <span style="font-size: smaller">Total Passenger</span>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lbltpsngr" Text="0" Font-Size="Smaller"></asp:Label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <span style="font-size: smaller">Total Amount</span>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lbltamt" Text="0" Font-Size="Smaller"></asp:Label>
                                                        <i class="fa fa-rupee-sign" style="font-size: smaller;"></i>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4  stretch-card transparent">
                                        <div class="card card-dark-blue" style="height: 105px;">
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-md-12 text-center">
                                                        <span style="font-size: smaller">Trip Under Process  </span>
                                                        <br />
                                                        <asp:Label runat="server" ID="lbltunderprocess" Text="0" Font-Size="Smaller"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <hr />

                        <div class="row">
                            <div class="col-md-11">
                                <asp:Label runat="server" Text=" 3. Online Booking" Font-Size="Medium"></asp:Label>
                            </div>
                            <div class="col-md-1 float-right">
                                <asp:LinkButton ID="LinkButton2" runat="server"><i class="fa fa-sync-alt"></i></asp:LinkButton>
                            </div>
                        </div>



                        <asp:UpdatePanel ID="updatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="row mt-1">

                                    <div class="col-md-4  stretch-card transparent">
                                        <div class="card card-dark-blue" style="height: 105px;">
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-md-12 text-center"><span style="font-size: smaller">Total</span> </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6"><span style="font-size: smaller">Passenger</span> </div>
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblonltpsngr" Text="0" Font-Size="Smaller"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6"><span style="font-size: smaller">Amount</span> </div>
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblonltamt" Text="0" Font-Size="Smaller"></asp:Label>
                                                        <i class="fa fa-rupee-sign" style="font-size: smaller;"></i>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4  stretch-card transparent">
                                        <div class="card card-dark-blue" style="height: 105px;">
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-md-12 text-center"><span style="font-size: smaller">Online</span> </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <span style="font-size: smaller">Passenger</span>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblonlpsngr" Text="0" Font-Size="Smaller"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <span style="font-size: smaller">Amount</span>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblonlamt" Text="0" Font-Size="Smaller"></asp:Label>
                                                        <i class="fa fa-rupee-sign" style="font-size: smaller;"></i>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4  stretch-card transparent">
                                        <div class="card card-dark-blue" style="height: 105px;">
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-md-12 text-center"><span style="font-size: smaller">Counter</span> </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <span style="font-size: smaller">Passenger</span>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblcntrpsngr" Text="0" Font-Size="Smaller"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <span style="font-size: smaller">Amount</span>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblcntramt" Text="0" Font-Size="Smaller"></asp:Label>
                                                        <i class="fa fa-rupee-sign" style="font-size: smaller;"></i>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4  stretch-card transparent">
                                        <div class="card card-dark-blue" style="height: 105px;">
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-md-12 text-center"><span style="font-size: smaller">Agent</span> </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <span style="font-size: smaller">Passenger</span>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblagntpsngr" Text="0" Font-Size="Smaller"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <span style="font-size: smaller">Amount</span>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblagntamt" Text="0" Font-Size="Smaller"></asp:Label>
                                                        <i class="fa fa-rupee-sign" style="font-size: smaller;"></i>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>



                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <hr />

                        <div class="row">
                            <div class="col-md-11">
                                <asp:Label runat="server" Text=" 4. Inventory" Font-Size="Medium"></asp:Label>
                            </div>
                            <div class="col-md-1 float-right">
                                <asp:LinkButton ID="LinkButton3" runat="server"><i class="fa fa-sync-alt"></i></asp:LinkButton>
                            </div>
                        </div>



                        <asp:UpdatePanel ID="updatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="row mt-1">

                                    <div class="col-md-12 stretch-card transparent text-center">
                                        <asp:Label runat="server" Text="Coming Soon" Font-Size="Medium"></asp:Label>
                                        <%-- <div class="card card-dark-blue">
                                    <div class="card-body">
                                        <div class="card-heading">Coming Soon</div>



                                    </div>
                                </div>--%>
                                    </div>


                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <hr />

                        <div class="row">
                            <div class="col-md-11">
                                <asp:Label runat="server" Text=" 5. Crew" Font-Size="Medium"></asp:Label>
                            </div>
                            <div class="col-md-1 float-right">
                                <asp:LinkButton ID="LinkButton4" runat="server"><i class="fa fa-sync-alt"></i></asp:LinkButton>
                            </div>
                        </div>


                        <asp:UpdatePanel ID="updatePanel4" runat="server">
                            <ContentTemplate>
                                <div class="row mt-1">

                                    <div class="col-md-12  stretch-card transparent text-center">

                                        <asp:Label runat="server" Text="Coming Soon" Font-Size="Medium"></asp:Label>
                                        <%--   <div class="card card-dark-blue">
                                  <center></center> 
                                   <div class="card-body">
                                        <div class="card-heading">Coming Soon</div>



                                    </div>
                                </div>--%>
                                    </div>


                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <div class="card shadow">
                    <div class="card-body">
                        <i class="fa fa-file-alt text-muted"></i>
                        <asp:Label runat="server" Text="Master Entry" Font-Bold="true" Font-Size="Large"></asp:Label>
                        <hr style="margin-top: 2px; margin-bottom: 10px;" />
                        <div class="row mt-1">
                            <div class="col-md-3  stretch-card transparent">
                                <div class="card card-dark-blue" style="height: 125px;">
                                    <div class="card-body">

                                        <div class="card-heading" style="font-size: 15px;">Filling Station</div>
                                        <div class="total-tx mb-4" style="font-size: 12px;">Configure Filling Station</div>
                                        <div class="col-md-12  stretch-card transparent text-center">

                                            <asp:Label runat="server" Text="Coming Soon" Visible="false" ForeColor="Red" Font-Size="Medium"></asp:Label>

                                        </div>
                                        <div class="col text-right" runat="server">
                                            <a href="../Auth/UserManuals/Depot Manager/Help Document for Filling Station.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="DepAdminFillingStationMgmt.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3  stretch-card transparent">
                                <div class="card card-dark-blue" style="height: 125px;">
                                    <div class="card-body">
                                        <div class="card-heading">Tank Management</div>
                                        <div class="total-tx mb-4">Configure Tank Management</div>
                                        <div class="col-md-12  stretch-card transparent text-center">

                                            <asp:Label runat="server" Text="Coming Soon" Visible="false" ForeColor="Red" Font-Size="Medium"></asp:Label>

                                        </div>
                                        <div class="col text-right" runat="server">
                                            <a href="../Auth/UserManuals/Depot Manager/Help Document for Tank Management.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="DepAdminTankMgmt.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 stretch-card transparent">
                                <div class="card card-dark-blue" style="height: 125px;">
                                    <div class="card-body">
                                        <div class="card-heading" runat="server">Pump Management</div>
                                        <div class="total-tx mb-4">
                                            Configure Pump Management

                                        </div>
                                        <div class="col-md-12  stretch-card transparent text-center">

                                            <asp:Label runat="server" Text="Coming Soon" Visible="false" ForeColor="Red" Font-Size="Medium"></asp:Label>

                                        </div>
                                        <div class="col text-right" runat="server">
                                            <a href="../Auth/UserManuals/Depot Manager/Help Document for Pump Management.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="DepAdminPumpMgmt.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                        </div>

                                    </div>
                                </div>
                            </div>

                        </div>


                        <i class="fa fa-cogs text-muted"></i>
                        <asp:Label runat="server" Text="Configuration/Management" Font-Bold="true" Font-Size="Large"></asp:Label>
                        <hr style="margin-top: 2px; margin-bottom: 10px;" />


                        <div class="row mt-1">
                            <div class="col-md-3  stretch-card transparent">
                                <div class="card card-dark-blue" style="height: 125px;">
                                    <div class="card-body">

                                        <div class="card-heading">Employee Management</div>
                                        <div class="total-tx mb-4">Configure Employee Management</div>
                                        <div class="col text-right">
                                            <a href="../Auth/UserManuals/Depot Manager/Help Document for Employee Management.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="EmployeeManagement.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3  stretch-card transparent">
                                <div class="card card-dark-blue" style="height: 125px;">
                                    <div class="card-body">
                                        <div class="card-heading">User Management</div>
                                        <div class="total-tx mb-4">Configure User Management</div>
                                        <div class="col text-right" runat="server" visible="true">
                                            <a href="../Auth/UserManuals/Depot Manager/Help Document for User Management.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="Sysadmusermanagement.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 stretch-card transparent">
                                <div class="card card-dark-blue" style="height: 125px;">
                                    <div class="card-body">
                                        <div class="card-heading">Module Assignment </div>
                                        <div class="total-tx mb-4">
                                            Configure Module Assignment

                                        </div>

                                        <div class="col text-right" runat="server" visible="true">
                                            <a href="../Auth/UserManuals/Depot Manager/Help Document for Module Assignment.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="moduleassignment.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 stretch-card transparent" runat="server" visible="false">
                                <div class="card card-dark-blue" style="height: 125px;">
                                    <div class="card-body">
                                        <div class="card-heading">Bus Management </div>
                                        <div class="total-tx mb-4">
                                            Configure Bus Management

                                        </div>
                                        <asp:Label runat="server" Text="Coming Soon" ForeColor="Red" Font-Size="Medium"></asp:Label>

                                        <div class="col text-right" runat="server" visible="false">
                                            <a href="../Auth/UserManuals/Depot Manager/2-City_Management.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="DepAdminPumpMgmt.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 stretch-card transparent">
                                <div class="card card-dark-blue" style="height: 125px;">
                                    <div class="card-body">
                                        <div class="card-heading">Bus Route Mapping </div>
                                        <div class="total-tx mb-4">
                                            Configure Bus Route

                                        </div>

                                        <div class="col text-right" runat="server" visible="true">
                                            <a href="../Auth/UserManuals/Depot Manager/Help Document for Bus Route Mapping.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="depbusservicemapping.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 stretch-card transparent">
                                <div class="card card-dark-blue" style="height: 125px;">
                                    <div class="card-body">
                                        <div class="card-heading">Bus Crew Mapping </div>
                                        <div class="total-tx mb-4">
                                            Configure Bus Crew

                                        </div>

                                        <div class="col text-right" runat="server" visible="true">
                                            <a href="../Auth/UserManuals/Depot Manager/Help Document for Bus Crew Mapping.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="depbuscrewmapping.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <i class=" fa fa-cogs text-muted"></i>
                        <asp:Label runat="server" Text="Operation Management" Font-Bold="true" Font-Size="Large"></asp:Label>
                        <hr style="margin-top: 2px; margin-bottom: 10px;" />

                        <div class="row mt-1">
                            <div class="col-md-3  stretch-card transparent">
                                <div class="card card-dark-blue" style="height: 125px;">
                                    <div class="card-body">

                                        <div class="card-heading">Trip Chart</div>
                                        <div class="total-tx mb-4">Configure Trip Chart</div>
                                        <div class="col text-right" runat="server" visible="true">
                                            <a href="../Auth/UserManuals/Depot Manager/Help Document for Trip Chart.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="tripchartdash.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3  stretch-card transparent">
                                <div class="card card-dark-blue" style="height: 125px;">
                                    <div class="card-body">
                                        <div class="card-heading">Service Block/Unblock</div>
                                        <div class="total-tx mb-4">Configure Service Block/Unblock</div>
                                        <div class="col text-right" runat="server" visible="true">
                                            <a href="../Auth/UserManuals/Depot Manager/Help Document for Service Block_Unblock.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="Deposervicecancel.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 stretch-card transparent">
                                <div class="card card-dark-blue" style="height: 125px;">
                                    <div class="card-body">
                                        <div class="card-heading">Complaint Management</div>
                                        <div class="total-tx mb-4">
                                            Configure Complaint Management

                                        </div>


                                        <div class="col text-right" runat="server" visible="true">
                                            <a href="../Auth/UserManuals/Depot Manager/Help Document for Complaint Management.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="complaintDashboard.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 stretch-card transparent">
                                <div class="card card-dark-blue" style="height: 125px;">
                                    <div class="card-body">
                                        <div class="card-heading">Leave Entry</div>
                                        <div class="total-tx mb-4">
                                            Configure Leave Entry

                                        </div>

                                        <div class="col text-right" runat="server">
                                            <a href="../Auth/UserManuals/Depot Manager/Help Document for Leave Entry.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="addleave.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 stretch-card transparent">
                                <div class="card card-dark-blue" style="height: 125px;">
                                    <div class="card-body">
                                        <div class="card-heading">Leave Approval</div>
                                        <div class="total-tx mb-4">
                                            Configure Leave Entry

                                        </div>

                                        <div class="col text-right" runat="server">
                                            <a href="../Auth/UserManuals/Depot Manager/Help Document for Leave Approval.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="leaveapproval.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 stretch-card transparent">
                                <div class="card card-dark-blue" style="height: 125px;">
                                    <div class="card-body">
                                        <div class="card-heading">Add Holiday</div>
                                        <div class="total-tx mb-4">
                                            Add/Update Holiday

                                        </div>
                                        <div class="col-md-12  stretch-card transparent text-center">

                                            <%--<asp:Label runat="server" Text="Coming Soon" ForeColor="Red" Font-Size="Medium"></asp:Label>--%>
                                            <a href="../Auth/UserManuals/Depot Manager/Help Document for Add Holiday.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="SysAdmHoliday.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                        </div>
                                        <div class="col text-right" runat="server" visible="false">
                                            <a href="../Auth/UserManuals/Depot Manager/Help Document for Add Holiday.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="DepAdminPumpMgmt.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="col-md-3 stretch-card transparent">
                                <div class="card card-dark-blue" style="height: 125px;">
                                    <div class="card-body">
                                        <div class="card-heading">Shift Allocation</div>
                                        <div class="total-tx mb-4">
                                            Configure Shift Allocation
                                        </div>

                                        <div class="col text-right" runat="server">
                                            <a href="../Auth/UserManuals/Depot Manager/Help Document for Shift Allocation.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="adddutyshift.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 stretch-card transparent">
                                <div class="card card-dark-blue" style="height: 125px;">
                                    <div class="card-body">
                                        <div class="card-heading">Station Incharge</div>
                                        <div class="total-tx mb-4">
                                            Verify Cash Deposit And Waybill Extra Km
                                        </div>
                                        <%-- <div class="col-md-12  stretch-card transparent text-center">

                                            <asp:Label runat="server" Text="Coming Soon" ForeColor="Red" Font-Size="Medium"></asp:Label>

                                        </div>--%>
                                        <div class="col text-right" runat="server" visible="true">
                                            <a href="../Auth/UserManuals/Depot Manager/Help Document for Station_Incharge.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="StInchargeCatalogue.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>



