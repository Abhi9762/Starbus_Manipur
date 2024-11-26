<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="sysadmbustracking.aspx.cs" Inherits="Auth_sysadmbustracking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .dataTables_length {
            display: none;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidtoken" runat="server" />
    <div class="container-fluid pr-1 pl-2" style="padding-top: 20px; padding-bottom: 30px;">
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="row">
                        <div class="col-4 border-right text-center ">
                            <div class="row mt-4">
                                <div class="col-12">
                                    <asp:Label runat="server" Style="font-size: 20px; color: #011b42; font-weight: bold;" Text="Track a Bus"></asp:Label>
                                </div>
                            </div>
                            <span style="font-size: 10pt; color: Red;">(for busses fitted with GPS devices)</span>
                        </div>
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h4 class="mb-1">
                                            <asp:Label ID="lblSummary" runat="server" CssClass="text-capitalize">Summary as on Date 01/12/2021 04:00pm</asp:Label></h4>
                                        <div class="row m-0">
                                            <div class="col-12">
                                                <div class="row m-0">
                                                    <div class="col-6">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Total Bus:&nbsp;
                                 <asp:Label ID="lblTotalBus" runat="server" ToolTip="Total States Available" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-6">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">GPS Configured: &nbsp;
                                <asp:Label ID="lblConfiguared" runat="server" ToolTip="Configured State" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <div class="row m-0">
                                                    <div class="col-6">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Not Configured: &nbsp;
                                <asp:Label ID="lblNotConfigured" runat="server" ToolTip="Configured State" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="col-4">
                            <div class="card-body">
                                <div class="row mr-0">
                                    <div class="col">
                                        <div>
                                            <h4 class="mb-1">Instructions</h4>
                                        </div>
                                        <asp:Label runat="server" CssClass="form-control-label">1. Here you can search bus location.</asp:Label><br />
                                        <asp:Label runat="server" CssClass="form-control-label">2. You can also view other details of Bus</asp:Label>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row text-center mt-7" runat="server" id="divNoBuses">
            <div class="col-lg-12">
                <asp:Label ID="lblmapmassage" CssClass="" ForeColor="LightGray" Font-Size="40px" Font-Bold="true" runat="server" Text="Please Check Either Buses Are Not Registered Or</br> GPS Updation Status Is Pending."></asp:Label>
            </div>
        </div>
        <div class="row" runat="server" id="divBusDetails">
            <div class="col-3">
                <div class="card shadow">
                    <div class="card-body table-responsive" style="min-height: 320px;">
                        <asp:GridView ID="gvBusList" PageSize="10" ShowHeader="true" AllowPaging="false" CssClass="table"
                            runat="server" GridLines="None" DataKeyNames="gps_yn,busno,depot,servicetype" AutoGenerateColumns="false" ClientIDMode="Static" OnRowCommand="gvBusList_RowCommand">
                            <Columns>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <div></div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="row">
                                            <div class="col-2">
                                                <%#Container.DataItemIndex + 1 %>
                                            </div>
                                            <div class="col-6">
                                                <asp:Label ID="busno" runat="server" Text='<%#Eval("busno") %>'></asp:Label>
                                            </div>
                                            <div class="col-3">
                                                <asp:LinkButton runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="viewBus" CssClass="btn btn-primary btn-sm" ToolTip="View Bus Location"><i class="fa fa-map-marker-alt"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <PagerStyle HorizontalAlign="Left" CssClass="gridview" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="col-9">
                <div runat="server" id="divtrackbusno" visible="false">
                    <div class="card shadow text-center" style="min-height: 630px">
                        <div class="row mt-9">
                            <div class="col-12">
                                <asp:Label ID="Label3" CssClass="" ForeColor="LightGray" Font-Size="40px" Font-Bold="true" runat="server" Text="To Track A Bus"></asp:Label>
                            </div>
                            <div class="col-12">
                                <asp:Label ID="Label1" CssClass="" ForeColor="LightGray" Font-Size="40px" Font-Bold="true" runat="server" Text=" Click On "></asp:Label>
                                <i class="btn btn-primary btn-sm fa fa-map-marker-alt mb-3 ml-2"></i>

                            </div>
                        </div>
                    </div>
                </div>
                <div runat="server" id="divtrackbus" visible="false">
                    <div class="card shadow">
                        <div class="row" style="font-size: 12px;">
                            <div class="col-4 pt-2 pl-3 border-right pb-2" ">
                                <span class="font-weight-900">Bus Details</span><br />
                                <asp:Label runat="server" Text="Bus No."></asp:Label>
                                -
                                <asp:Label ID="lblbusno" runat="server" Text="N/A"></asp:Label>
                                <br />
                                <asp:Label runat="server" CssClass="" Text="Depot"></asp:Label>
                                -
                                <asp:Label ID="lblDepot" runat="server" Text="N/A"></asp:Label><br />
                                <asp:Label runat="server" CssClass="" Text="Service Type"></asp:Label>
                                -
                                <asp:Label ID="lblServiceType" runat="server" Text="N/A"></asp:Label>
                            </div>
                            <div class="col-7 pt-2 pl-3 pb-2">
                                <span class="font-weight-900">Duty Details</span><br />
                                <div id="divDutyDetails" runat="server" visible="false">
                                    <asp:Label runat="server" Text="Duty Slip No."></asp:Label>
                                    -
                                <asp:Label ID="lblDutySlipno" Font-Bold="true" runat="server" Text="N/A"></asp:Label>
                                    
                                    <asp:Label runat="server" CssClass="ml-5" Text="Duty Date"></asp:Label>
                                    -
                                <asp:Label ID="lblDutyDate" runat="server" Text="N/A"></asp:Label><br />
                                    <asp:Label runat="server" CssClass="" Text="Driver"></asp:Label>
                                    -
                                <asp:Label ID="lblDriver" runat="server" Text="N/A"></asp:Label>
                                    <asp:Label runat="server" CssClass="ml-5" Text="Conductor"></asp:Label>
                                    -
                                <asp:Label ID="lblConductor" runat="server" Text="N/A"></asp:Label><br />
                                    <asp:Label runat="server" CssClass="" Text="Route"></asp:Label>
                                    -
                                <asp:Label ID="lblRoute" Font-Bold="true" runat="server" Text="N/A"></asp:Label>
                                </div>
                                <div id="divNoDuty" runat="server" visible="false">
                                    <asp:Label runat="server" Text="Current Duty Details Not Available." CssClass="ml-9 mt-3"></asp:Label>

                                </div>
                            </div>
                            <div class="col-1">
                                <asp:LinkButton runat="server" ID="lbtndutydetails" OnClick="lbtndutydetails_Click" CssClass="float-right mr-3 mt-2" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                            </div>

                        </div>
                    </div>
                   
                        <div id="div1" visible="false" class="card shadow p-1" runat="server" style="min-height: 580px">
                        </div>
                    
                </div>
            </div>
        </div>
        
    </div>



    <script type="text/javascript">
        $(document).ready(function () {
            $('#gvBusList').DataTable({
                "pageLength": 15,
                //dom: 'Bfrtip',
                "bSort": false,
                buttons: [
                    'csv', 'excel', 'pdf', 'print'
                ]
            });
        });
    </script>
</asp:Content>

