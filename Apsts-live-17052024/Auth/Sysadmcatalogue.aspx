<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="Sysadmcatalogue.aspx.cs" Inherits="CitizenAuth_Sysadmcatalogue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <style>
        .card {
            position: relative;
            min-height: 120px;
            display: flex;
            flex-direction: column;
        }

        .card-body {
            flex-grow: 1;
            padding-bottom: 50px;
        }

        .card-actions {
            position: absolute;
            bottom: 10px;
            right: 10px;
        }

        .total-tx {
            overflow: hidden;
            text-overflow: ellipsis;
            display: -webkit-box;
            -webkit-line-clamp: 3;
            -webkit-box-orient: vertical;
        }

        hr {
            margin-top: 6px;
            margin-bottom: 1rem;
        }

        .card {
            border: 1px solid #e6e6e6;
            border-radius: 8px;
            transition: all 0.3s ease;
            height: 100%;
        }

            .card:hover {
                border-color: rgba(13, 110, 253, 0.7);
                box-shadow: 0px 0px 15px 3px rgba(13, 110, 253, 0.4);
                transform: translateY(-3px);
            }

        .card-dark-blue {
            background: linear-gradient(145deg, #f0f8ff 0%, #e6f2ff 100%);
            color: #074886;
        }

        .card-heading {
            font-size: 1.1rem;
            font-weight: bold;
            margin-bottom: 0.5rem;
        }

        .total-tx {
            font-size: 0.9rem;
            color: #555;
            margin-bottom: 1rem;
        }

        .btn-primary {
            background-color: #074886;
            border: none;
            transition: background-color 0.3s ease;
        }

            .btn-primary:hover {
                background-color: #0056b3;
            }

        .btn-sm {
            padding: 0.25rem 0.75rem;
            font-size: 0.875rem;
            border-radius: 0.2rem;
        }

        .btn-primary i {
            margin-right: 5px;
        }

        .row-cards {
            display: flex;
            flex-wrap: wrap;
            margin-right: -15px;
            margin-left: -15px;
        }

        .stretch-card {
            padding-right: 15px;
            padding-left: 15px;
            margin-bottom: 30px;
        }

        .card-body {
            display: flex;
            flex-direction: column;
            height: 100%;
        }

        .total-tx {
            flex-grow: 1;
        }

        .mt-auto {
            margin-top: auto;
        }

        .cards-per-row-6 .stretch-card {
            width: 16.66%;
        }

        .card-body {
            display: flex;
            flex-direction: column;
            height: 100%;
            position: relative;
            padding-bottom: 10px;
        }

        .card-actions {
            position: absolute;
            bottom: 10px;
            right: 10px;
            display: flex;
        }

            .card-actions .btn {
                padding: 0.25rem;
                width: 30px;
                height: 30px;
                display: flex;
                align-items: center;
                justify-content: center;
                margin-left: 5px;
            }

                .card-actions .btn i {
                    font-size: 1rem;
                }

        .btn-sm {
            padding: 0.25rem 0.5rem;
            font-size: 0.75rem;
        }

        @media (max-width: 1199px) {
            .cards-per-row-6 .stretch-card {
                width: 33.333%;
            }
        }

        @media (max-width: 991px) {
            .cards-per-row-6 .stretch-card {
                width: 50%;
            }
        }

        @media (max-width: 575px) {
            .cards-per-row-6 .stretch-card {
                width: 100%;
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid pt-top ">
        <asp:UpdatePanel runat="server" ID="UpdatePanelall" UpdateMode="Always">
            <ContentTemplate>
                <%--Master Entry--%>
                <div class="row mt-2">
                    <div class="col-lg-12">
                        <i class="fa fa-file-alt text-muted"></i>
                        <asp:Label runat="server" Text="Master Entry" Font-Size="Large" Font-Bold="true"></asp:Label>
                        <hr />
                    </div>
                </div>
                <div class="row-cards cards-per-row-6">
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                 <div class="card-content">
                                    <div class="card-heading">State Management</div>
                                    <div class="total-tx">
                                        Update State with fare type and other surcharge
                                    </div>
                                </div>
                                <div class="card-actions">
                                    <a href="../Auth/UserManuals/Sys_adm/1-State_Management.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="SysAdmState.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>
                        </div>
                    </div>
                        </div>
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">City Management</div>
                                <div class="total-tx" >Insert, Update City Details</div>
                                <div class="card-actions">
                                    <a href="../Auth/UserManuals/Sys_adm/2-City_Management.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="SysAdminCity.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>
                            </div>
                        </div>
                        </div>
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Office Management</div>
                                <div class="total-tx" >Insert, Update Office and Unit</div>
                                <div class="card-actions">
                                    <a href="../Auth/UserManuals/Sys_adm/3-Office_Management.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="SysAdmOfficeMgmt.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Bus Layout</div>
                                <div class="total-tx" >Create Layout with no. of rows and column</div>
                                <div class="card-actions">
                                    <a href="../Auth/UserManuals/Sys_adm/4-Bus_Layout.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="SysAdmLayoutMain.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Agency Registration</div>
                                <div class="total-tx" >Insert and update Agency Details</div>
                                <div class="card-actions">
                                    <a href="../Auth/UserManuals/Sys_adm/5-agency_registration.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="SysAdmAgencyRegistration.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Bus Registration</div>
                                <div class="total-tx" >Add, Update and Delete Bus Details</div>
                                <div class="card-actions">
                                    <a href="../Auth/UserManuals/Sys_adm/6-Bus_Registration.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="SysAdmBusRegistration.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
<%--                  <div class="row-cards cards-per-row-6">
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Dhaba Management</div>
                                <div class="total-tx" >Add & Update</div>
                                    </div>
                                <div class="card-actions">
                                    <%--  <asp:Label runat="server" Text="Coming Soon" ForeColor="Red" Font-Size="Medium"></asp:Label>--%>
<%--                                    <a href="#!" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="sysAdmDhaba.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>--%>
                                <%--</div>
                            </div>
                        </div>
                    </div>--%>

                <%-- Master Entry end--%>
                <%--Employee--%>
                <div class="row mt-2">
                    <div class="col-lg-12">
                        <i class="fa fa-user-alt text-muted"></i>
                        <asp:Label runat="server" Text="Employee" Font-Size="Large" Font-Bold="true"></asp:Label>
                        <hr />
                    </div>
                </div>
                <div class="row-cards cards-per-row-6">
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Employee Registration</div>
                                <div class="total-tx" >Add, Verify and update employee details</div>
                                    </div>
                                <div class="card-actions">
                                    <a href="../Auth/UserManuals/Sys_adm/7-Employee_Registration.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="SysadmEmployeeDetails.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>
                            </div>
                        </div>
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">User Management</div>
                                <div class="total-tx" >Assign role</div>
                                <div class="card-actions">
                                    <a href="../Auth/UserManuals/Sys_adm/8-User_Management.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="SysAdmUserManagement.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>
                            </div>
                        </div>
                    </div>
<%--                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Attendance Marking</div>
                                <div class="total-tx" >Add Employee Attendance</div>
                                <div class="card-actions">
                                    <asp:Label runat="server" Text="Coming Soon" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                    <%--<a href="#!" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="EmployeeManagement.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>--%>
                              <%--  </div>
                            </div>
                        </div>
                    </div>--%>
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Leave Entry</div>
                                <div class="total-tx" >Mark Employee Leave</div>
                                <div class="card-actions">
                                    <%--  <asp:Label runat="server" Text="Coming Soon" ForeColor="Red" Font-Size="Medium"></asp:Label>--%>
                                    <a href="../Auth/UserManuals/Sys_adm/30-Leave_Entry.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="AddLeave.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>

                            </div>
                        </div>
                    </div>
                <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Leave Approval</div>
                                <div class="total-tx" >Approve Employee Leave</div>
                                <div class="card-actions">
                                    <%--    <asp:Label runat="server" Text="Coming Soon" ForeColor="Red" Font-Size="Medium"></asp:Label>--%>
                                    <a href="../Auth/UserManuals/Sys_adm/29-Leave_Approval.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="LeaveApproval.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>
                            </div>
                        </div>
                    </div>
                <div class="stretch-card transparent">
                        <div class="card card-dark-blue ">
                            <div class="card-body">
                                <div class="card-heading">Shift Allocation</div>
                                <div class="total-tx" >Add Employee Shift</div>
                                <div class="card-actions">
                                    <%--  <asp:Label runat="server" Text="Coming Soon" ForeColor="Red" Font-Size="Medium"></asp:Label>--%>
                                    <a href="../Auth/UserManuals/Sys_adm/28-Shift_Allocation.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="AddDutyShift.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>
                            </div>
                        </div>
                    </div>
                      <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">

                                <div class="card-heading">Add Holiday</div>
                                <div class="total-tx" >Add/Update Holiday</div>
                                <div class="card-actions">
                                    <%--    <asp:Label runat="server" Text="Coming Soon" ForeColor="Red" Font-Size="Medium"></asp:Label>--%>
                                    <a href="../Auth/UserManuals/Sys_adm/31-Add_Holiday.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="SysAdmHoliday.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="row-cards cards-per-row-6">
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">

                                <div class="card-heading">Module Assignment</div>
                                <div class="total-tx" >Assign Module to Employee</div>
                                <div class="card-actions">
                                    <a href="../Auth/UserManuals/Sys_adm/9-Module_mapping.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="SysAdmUserMapping.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>

                            </div>
                        </div>
                    </div>

                </div>
                <%--Employee End--%>
                <%--Route & Fare--%>
                <div class="row mt-2">
                    <div class="col-lg-12">
                        <i class="fas fa-route text-muted"></i>
                        <asp:Label runat="server" Text="Route & Fare" Font-Bold="true" Font-Size="Large"></asp:Label>
                        <hr style="margin-top: 2px; margin-bottom: 10px;" />
                    </div>
                </div>
                <div class="row-cards cards-per-row-6">
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">

                                <div class="card-heading">Bus Service Type</div>
                                <div class="total-tx" >Insert, Update, Active, Deactive</div>
                                <div class="card-actions">
                                    <a href="../Auth/UserManuals/Sys_adm/10-Bus_Service_Type.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="Sysadmservicetype.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">

                                <div class="card-heading">State Service Wise Fare</div>
                                <div class="total-tx" >Insert, Update, Active, Deactive</div>
                                <div class="card-actions">
                                    <a href="../Auth/UserManuals/Sys_adm/14-State_Service_Wise_Fare.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="SysAdmStateServiceWiseFare.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">

                                <div class="card-heading">Station</div>
                                <div class="total-tx" >Add & Update</div>
                                <div class="card-actions">
                                    <a href="../Auth/UserManuals/Sys_adm/13-Station_StationDistance.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="SysAdmStationMgmt.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>

                            </div>
                        </div>
                    </div>
                     <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Route</div>
                                <div class="total-tx" >Add & Update</div>
                                <div class="card-actions">
                                    <a href="../Auth/UserManuals/Sys_adm/15-Route.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="SysAdmRouteMgmt.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">

                                <div class="card-heading">Depot Service</div>
                                <div class="total-tx" >Add & Update</div>
                                <div class="card-actions">
                                    <a href="../Auth/UserManuals/Sys_adm/17-Depot_service.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="SysAdmDepotService.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">

                                <div class="card-heading">Fare Generation</div>
                                <div class="total-tx" >Add & Update</div>
                                <div class="card-actions">
                                    <a href="../Auth/UserManuals/Sys_adm/16-Fare Generation.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="SysAdmFareGeneration.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>

                            </div>
                        </div>
                    </div>
                    </div>
<%--                <div class="row cards cards-per-row-6">
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">State Service Wise Charge</div>
                                <div class="total-tx" >Add & Update</div>
                                <div class="card-actions">
                                    <asp:Label runat="server" Text="Coming Soon" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                    <%--         <a href="#!" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="SysAdmStateServiceWiseCharge.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                    --%>
                                <%--</div>
                            </div>
                        </div>
                        </div>
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">

                                <div class="card-heading">Service Wise Charge Map</div>
                                <div class="total-tx" >Add & Update</div>
                                <div class="card-actions">
                                    <asp:Label runat="server" Text="Coming Soon" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                    <%-- <a href="#!" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="SysAdmStateServiceWiseChargeMap.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>--%>
                                <%--</div>

                            </div>
                        </div>
                    </div>--%>
                    <%--<div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">

                                <div class="card-heading">State Wise Tax Rate</div>
                                <div class="total-tx" >Add & Update</div>
                                <div class="card-actions">
                                    <asp:Label runat="server" Text="Coming Soon" ForeColor="Red" Font-Size="Medium"></asp:Label>--%>
                                    <%--<a href="#!" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="SysAdmStateServiceWiseTax.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>--%>
                                <%--</div>

                            </div>
                        </div>
                    </div>
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">FCI Generation</div>
                                <div class="total-tx" >Add & Update</div>
                                <div class="card-actions">
                                    <asp:Label runat="server" Text="Coming Soon" ForeColor="Red" Font-Size="Medium"></asp:Label>--%>
                                    <%-- <a href="#!" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="EmployeeManagement.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>--%>
                                <%--</div>

                            </div>
                        </div>
                    </div>
                </div>--%>
                <%-- Routr & Fare end--%>
                <%--Online Ticket Booking--%>
                <div class="row mt-2">
                    <div class="col-lg-12">
                        <i class="fa fa-ticket-alt text-muted"></i>
                        <asp:Label runat="server" Text="Online Ticket Booking" Font-Bold="true" Font-Size="Large"></asp:Label>
                        <hr style="margin-top: 2px; margin-bottom: 10px;" />
                    </div>
                </div>
                <div class="row-cards cards-per-row-6">
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">

                                <div class="card-heading">Special Ticket Cancellation</div>
                                <div class="total-tx" >Add & Update</div>
                                <div class="card-actions">
                                    <a href="../Auth/UserManuals/Sys_adm/20-Special_Ticket_Cancellation.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="sysAdmSpecialTicketCancellation.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">

                                <div class="card-heading">Service Block & Cancel</div>
                                <div class="total-tx" >Add & Update</div>
                                <div class="card-actions">

                                    <a href="../Auth/UserManuals/Sys_adm/24-Service_Block&Cancel.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="sysadmservicecancel.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">

                                <div class="card-heading">Block Seat's</div>
                                <div class="total-tx" >Add & Update</div>
                                <div class="card-actions">
                                     <a href="../Auth/UserManuals/Sys_adm/23-Block_Seats.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="sysAdmBlockSeat.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>

                            </div>
                        </div>
                    </div>

                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">

                                <div class="card-heading">Trip Chart</div>
                                <div class="total-tx" >Generate Trip Chart & View</div>
                                <div class="card-actions">
                                    <a href="../Auth/UserManuals/Sys_adm/21-Trip_Chart.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="tripchartdash.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <%-- Online Ticket Booking end--%>
                <%--MISC--%>

                <div class="row mt-2">
                    <div class="col-lg-12">
                        <i class="fa fa-info-circle text-muted"></i>
                        <asp:Label runat="server" Text="MISC" Font-Bold="true" Font-Size="Large"></asp:Label>
                        <hr style="margin-top: 2px; margin-bottom: 10px;" />
                    </div>
                </div>
                <div class="row-cards cards-per-row-6">
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">PG MIS</div>
                                <div class="total-tx" >View Payment Gateway Mis</div>
                                <div class="card-actions">
                                    <a href="../Auth/UserManuals/Sys_adm/18-PGMIS.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="sysAdmPmtGateway.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">

                                <div class="card-heading">PG Refund Transaction</div>
                                <div class="total-tx" >View Pending Refund Transaction</div>
                                <div class="card-actions">
                                    <a href="../Auth/UserManuals/Sys_adm/35-PG_Refund_Transaction.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="sysAdmpgrefundtransaction.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">

                                <div class="card-heading">PNR Help</div>
                                <div class="total-tx" >Add & Update</div>
                                <div class="card-actions">
                                    <a href="../Auth/UserManuals/Sys_adm/19-PNR_Help.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="helpdesk.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">

                                <div class="card-heading">Rating</div>
                                <div class="total-tx" >View Rating</div>
                                <div class="card-actions">
                                    <a href="../Auth/UserManuals/Sys_adm/36-Rating.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="SysAdmFeedbackRating1.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">

                                <div class="card-heading">Complaint</div>
                                <div class="total-tx" >View Complaint Details</div>
                                <div class="card-actions">
                                    <a href="../Auth/UserManuals/Sys_adm/22-Complaint_management.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="complaintDashboard.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="stretch-card transparent" runat="server" id="divcsclogo">
                        <div class="card card-dark-blue">
                            <div class="card-body">

                                <div class="card-heading">Third Party Logo</div>
                                <div class="total-tx" >Add & Update Third Pary Logo</div>
                                <div class="card-actions">

                                    <a href="../Auth/UserManuals/Sys_adm/34-Third_Party_Logo.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="thirdpartylogoconfig.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

                <div class="row-cards cards-per-row-6">
                    <div class="stretch-card transparent" runat="server" id="divcsc">
                        <div class="card card-dark-blue">
                            <div class="card-body">

                                <div class="card-heading">Common Service Provider</div>
                                <div class="total-tx" >Common Service Provider</div>
                                <div class="card-actions">

                                    <a href="../Auth/UserManuals/Sys_adm/27-Common_Service_Provider.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="CSPRegistration.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>

                            </div>
                        </div>
                    </div>

                    <div class="stretch-card transparent" runat="server" id="divagents">
                        <div class="card card-dark-blue">
                            <div class="card-body">

                                <div class="card-heading">Online Agent Verification</div>
                                <div class="total-tx" >Agent Verification</div>
                                <div class="card-actions">

                                    <a href="../Auth/UserManuals/Sys_adm/32-Online_Agent_Verification.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="OnlagentVerification.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">

                                <div class="card-heading">Bus tracking</div>
                                <div class="total-tx" >View Bus Location </div>
                                <div class="card-actions">
                                    <a href="../Auth/UserManuals/Sys_adm/26-Bus_Tracking.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to track bus"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="sysadmbustracking.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>


                <%--MISC End--%>
                <%--Dashboard--%>
               <%--  <div class="row mt-2">
                    <div class="col-lg-12">
                        <i class="fa fa-tachometer-alt text-muted"></i>
                        <asp:Label runat="server" Text="Dashboard" Font-Bold="true" Font-Size="Large"></asp:Label>
                        <hr style="margin-top: 2px; margin-bottom: 10px;" />
                    </div>
                </div>
                <div class="row-cards cards-per-row-6">
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">

                                <div class="card-heading">PG Performance</div>
                                <div class="total-tx" >View Payment Gateway Performance</div>
                                <div class="card-actions">
                                    <asp:Label runat="server" Text="Coming Soon" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                    <%-- <a href="#!" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="EmployeeManagement.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>--%>
                               <%-- </div>

                            </div>
                        </div>
                    </div>
                    <div class="stretch-card transparent" runat="server" id="divpassPerformance">
                        <div class="card card-dark-blue">
                            <div class="card-body">

                                <div class="card-heading">Pass Performance</div>
                                <div class="total-tx" >Add & Update</div>
                                <div class="card-actions">
                                    <asp:Label runat="server" Text="Coming Soon" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                    <%-- <a href="#!" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="EmployeeManagement.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>--%>
                               <%-- </div>
                            </div>
                        </div>
                    </div>
                </div> --%>
                <%-- Dashboard end--%>
                <%--Reports--%>
                <div class="row mt-2">
                    <div class="col-lg-12">
                        <i class="fa fa-file-invoice text-muted"></i>
                        <asp:Label runat="server" Text="Reports" Font-Bold="true" Font-Size="Large"></asp:Label>
                        <hr style="margin-top: 2px; margin-bottom: 10px;" />
                    </div>
                </div>
                <div class="row-cards cards-per-row-6">
                    <div class="stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">

                                <div class="card-heading">Reports</div>
                                <div class="total-tx" >View Reports</div>
                                <div class="card-actions">
                                    <a href="../Auth/UserManuals/Sys_adm/33-Reports.pdf" target="_blank" class="btn btn-sm btn-success" data-toggle="tooltip" title="Click to view help document"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="reports/rpt_m1.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" title="Click to view"><i class="fa fa-external-link-alt"></i></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%-- Reports end--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

