<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/TimeKeeperMaster.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" EnableEventValidation="false" CodeFile="tkDutyAllocation.aspx.cs" Inherits="Auth_tkDutyAllocation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .ModalPopupBG {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
    </style>
    <script type="text/javascript">
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }

        $(document).ready(function () {

            var currDate = new Date().getDate();
            var nextDate = new Date(new Date().setDate(currDate + 1));
            var prevDate = new Date(new Date().setDate(currDate - 1));
            var todayDate = new Date(new Date().setDate(currDate));

            $('[id*=tbDutyDate]').datepicker({
                startDate: todayDate,
                endDate: nextDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
            $('[id*=tbToDate]').datepicker({
                endDate: nextDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
            $('[id*=tbFromDate]').datepicker({
                endDate: nextDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid pt-3 pb-5">
        <asp:HiddenField runat="server" ID="hdnRowIndex" />
        <asp:HiddenField runat="server" ID="hdnDriver" />
        <asp:HiddenField runat="server" ID="hdnConducotr" />
        <asp:HiddenField runat="server" ID="hdnDutyDays" />
        <div class="row">
            <div class="col-lg-12">
                <div class="card card-stats mb-3 m-0">
                    <div class="row m-0 py-1 text-center">
                        <div class="col-lg-2 mt-2 border-right">
                            <div class="row m-0 text-center">
                                <div class="col-md-12 pl-0">
                                    <h4 class="mb-0" runat="server" visible="true">Duty Allotments</h4>
                                    <div class="row">
                                        <div class="col-lg-6 border-right">
                                            <asp:Label ID="lblTotalAllocations" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                            <br />
                                            <asp:Label runat="server" CssClass="form-control-label text-sm"> Total</asp:Label>
                                        </div>
                                        <div class="col-lg-6">
                                            <asp:Label ID="lblProvisionalAllocations" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                            <br />
                                            <asp:Label runat="server" CssClass="form-control-label text-sm"> Provisional</asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-9 pt-2 border-right">
                            <div class="row m-0 text-center">
                                <div class="col-md-3 pl-0 border-right">

                                    <h4 class="mb-0" runat="server" visible="true">Buses</h4>
                                    <div class="row">
                                        <div class="col-lg-6 border-right">
                                            <asp:Label ID="lblTotBus" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                            <br />
                                            <asp:Label runat="server" CssClass="form-control-label text-sm"> Total Buses</asp:Label>
                                        </div>
                                        <div class="col-lg-6">
                                            <asp:Label ID="lblBusonDuty" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                            <br />
                                            <asp:Label runat="server" CssClass="form-control-label text-sm"> Allocated to Duty</asp:Label>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-3 pl-0 border-right">
                                    <h4 class="mb-0" runat="server" visible="true">Drivers</h4>
                                    <div class="row">
                                        <div class="col-lg-6 border-right">
                                            <asp:Label ID="lbltotDrivers" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                            <br />
                                            <asp:Label runat="server" CssClass="form-control-label text-sm"> Total </asp:Label>
                                        </div>
                                        <div class="col-lg-6">
                                            <asp:Label ID="lblOnDutyDriver" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                            <br />
                                            <asp:Label runat="server" CssClass="form-control-label text-sm"> Allocated to Duty </asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3 pl-0 border-right">

                                    <h4 class="mb-0" runat="server" visible="true">Conductors</h4>
                                    <div class="row">
                                        <div class="col-lg-6" style="border-right: 1px solid #eee;">
                                            <asp:Label ID="lbltotConductors" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                            <br />
                                            <asp:Label runat="server" CssClass="form-control-label text-sm">Total </asp:Label>

                                        </div>
                                        <div class="col-lg-6">
                                            <asp:Label ID="lblOnDutyConductor" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                            <br />
                                            <asp:Label runat="server" CssClass="form-control-label text-sm">Allocated to Duty </asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3 pl-0 ">


                                    <div class="row">
                                        <div class="col-lg-12">
                                            <h4 class="mb-0" runat="server" visible="true">Services</h4>


                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6" style="border-right: 1px solid #eee;">
                                            <asp:Label ID="lblTotService" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                            <br />
                                            <asp:Label runat="server" CssClass="form-control-label text-sm">Total </asp:Label>
                                        </div>
                                        <div class="col-lg-6">
                                            <asp:Label ID="lblPendingService" runat="server" Text="0" CssClass="form-control-label"></asp:Label>
                                            <br />
                                            <asp:Label runat="server" CssClass="form-control-label text-sm"> Pending for Target Diesel entry </asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-1 pt-2">
                            <asp:LinkButton runat="server" OnClick="LinkButton1_Click" CssClass="btn btn-sm bg-gradient-orange text-white">Archive</asp:LinkButton>

                        </div>
                    </div>
                </div>
                <div class="card card-stats mb-3 m-0" style="min-height: 600px">
                    <div class="card-header p-2">
                        <div class="row m-0 ml-1">

                            <div class="col-lg-12">
                                <div class="row">
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label ID="lblDate" runat="server" CssClass="form-control-label">Duty Date</asp:Label>
                                        <br />
                                        <asp:TextBox CssClass="form-control form-control-sm" runat="server" ID="tbDutyDate" MaxLength="10" placeholder="DD/MM/YYYY"
                                            Text="" Style="font-size: 9pt; padding: 2px 5px; height: 30px; width: 100%; margin-right: -10px;" autocomplete="off"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="tbDutyDate" ValidChars="/" />

                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label runat="server" CssClass="form-control-label">Bus Type</asp:Label>
                                        <asp:DropDownList ID="ddlBusType" runat="server" Style="height: 30px; font-size: 9pt;" ToolTip="Bus Type"
                                            CssClass="form-control form-control-sm">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label runat="server" CssClass="form-control-label"> Service Type</asp:Label>
                                        <asp:DropDownList ID="ddlServiceType" runat="server" Style="height: 30px; font-size: 9pt;" ToolTip="Service Type"
                                            CssClass="form-control form-control-sm">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label runat="server" CssClass="form-control-label"> Route</asp:Label>
                                        <asp:DropDownList ID="ddlRoutes" runat="server" Style="height: 30px; font-size: 9pt;" ToolTip="Route"
                                            CssClass="form-control form-control-sm">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 pl-0 mt-2 pt-3 pr-0">
                                        <asp:LinkButton ID="lbtnsearch" runat="server" OnClick="lbtnsearch_Click" class="btn btn-sm btn-primary" Style="border-radius: 4px; height: 30px; font-size: 10pt; padding-top: 4px;"> <i class="fa fa-search"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnResetFilter" runat="server" OnClick="lbtnResetFilter_Click" class="btn btn-sm btn-warning" Style="border-radius: 4px; height: 30px; font-size: 10pt; padding-top: 4px;"> <i class="fa fa-undo"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnview" ToolTip="View Instructions" OnClick="lbtnview_Click" runat="server" Width="30px" CssClass="btn btn bg-gradient-orange btn-sm  text-white">
                                    <i class="fa fa-info"></i>
                                        </asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-body p-2">
                        <div class="row mt-2">
                            <div class="col-lg-6 border-right">
                                <h3 class="mb-2" runat="server" id="hdnGrdHeading">List of services for provisional duty allotment
                                          <asp:LinkButton ID="lbtnDownloadExcel" runat="server" OnClick="lbtnDownloadExcel_Click" class="btn btn bg-gradient-green btn-sm text-white" Style="border-radius: 4px; height: 30px; font-size: 10pt; padding-top: 4px;" ToolTip="Download Excel"> <i class="fa fa-download"></i></asp:LinkButton>

                                </h3>
                                <asp:Panel runat="server" ID="pnlAllotDuty" Visible="true">
                                    <asp:GridView ID="gvServicesList" runat="server" ShowHeader="false" AutoGenerateColumns="false" GridLines="None"
                                        AllowPaging="true" PageSize="10" CssClass="table table-striped table-bordered table-hover" OnRowCommand="gvServicesList_RowCommand"
                                         OnRowDataBound="gvServices_RowDataBound" OnPageIndexChanging="gvServicesList_PageIndexChanging" DataKeyNames="dsvc_id,Rout_Id,SERVICE_NAME_EN,Service_Type_Name_En,deptTime,bustype,totalcrew,dutyDays,dutyRestDays,route_name,no_of_driver,no_of_conductor">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col">
                                                            <span style="font-weight: bold; padding-left: 10px;"><%# Eval("dsvc_id") %></span> | 
                                                         
                                                            <asp:Label runat="server" Style="white-space: normal;font-size: 12px" CssClass="text-uppercase" Text='<%# Eval("SERVICE_NAME_EN") %>'></asp:Label>
                                                            
                                                            <br />
                                                            <span style="font-weight: bold; padding-left: 10px;">(<%# Eval("BUSTYPE") %>)</span> | 
                                                    <span style="font-size: 12px;"><%# Eval("Service_Type_Name_En") %></span>
                                                        </div>
                                                        <div class="col-auto">
                                                            <asp:LinkButton ID="LinkButton3" CommandName="updateDuty" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                Visible="true" runat="server" CssClass="btn btn-sm btn-warning" Style="border-radius: 4px; font-size: 12px;"
                                                                ToolTip="Add Crew">
                                                      <i class="fa fa-edit"></i>

                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnAllotDuty" CommandName="allotDuty" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                Visible="true" runat="server" CssClass="btn btn-sm btn-success" Style="border-radius: 4px; font-size: 12px;"
                                                                ToolTip="Allot Duty"> 
                                                        <i class="fa fa-check"></i>
                                                       
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="row" runat="server" visible="true" id="divcrew1">
                                                        <div class="col-3">
                                                            <span style="font-weight: bold; padding-left: 10px;">Bus-</span>
                                                            <asp:label ID="lblBus" runat="server" CssClass="textbox textboxDDL">
                                                            </asp:label>
                                                        </div>
                                                        <div class="col-4">
                                                            <span style="font-weight: bold; padding-left: 10px;">Driver-</span>
                                                            <asp:label ID="lblDriver" runat="server" CssClass="textbox textboxDDL">
                                                            </asp:label>
                                                        </div>
                                                        <div class="col-4">
                                                            <span style="font-weight: bold; padding-left: 10px;">Conductor-</span>
                                                            <asp:label ID="lblConductor" runat="server" CssClass="textbox textboxDDL">
                                                            </asp:label>
                                                        </div>
                                                    </div>
                                                    <div class="row" runat="server" visible="true" id="divcrew2">
                                                        <div class="col-3">
                                                        </div>
                                                        <div class="col-4">
                                                            <span style="font-weight: bold; padding-left: 10px;">D2-</span>
                                                            <asp:label ID="lblDriver2" runat="server" CssClass="textbox textboxDDL">
                                                            </asp:label>
                                                              </div>
                                                        <div class="col-4">
                                                            <span style="font-weight: bold; padding-left: 10px;">C2-</span>
                                                            <asp:label ID="lblConductor2" runat="server" CssClass="textbox textboxDDL">
                                                            </asp:label>
                                                            </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Right" CssClass="GridPager" ForeColor="Black" />
                                    </asp:GridView>
                                </asp:Panel>
                                <asp:Panel ID="pnlNoRecord1" runat="server" Width="100%" Visible="true">
                                    <div class="col-md-12 p-0" style="text-align: center;">
                                        <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
                                            No Record Available<br />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="col-lg-6">
                                <h3 class="mb-2" runat="server" id="h1">List of generated provisional duty allotment
                                    <asp:LinkButton ID="lbtndownloadexcelalloted" runat="server" OnClick="lbtndownloadexcelalloted_Click" class="btn btn bg-gradient-green btn-sm text-white" Style="border-radius: 4px; height: 30px; font-size: 10pt; padding-top: 4px;" ToolTip="Download Excel"> <i class="fa fa-download"></i></asp:LinkButton>

                                </h3>
                                <asp:Panel runat="server" ID="pnlViewDuty" Visible="true">
                                    <div>
                                        <asp:GridView ID="gvAllotedDuties" runat="server" AutoGenerateColumns="false" GridLines="None" ShowHeader="false"
                                            AllowPaging="true" PageSize="10" OnRowCommand="gvAllotedDuties_RowCommand" OnPageIndexChanging="gvAllotedDuties_PageIndexChanging" CssClass="table table-hover" DataKeyNames="dutyrefno">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Service">
                                                    <ItemTemplate>
                                                        <div class="row px-2">
                                                            <div class="col">
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                                        <span style="font-weight: bold;"><%# Eval("dsvc_id") %></span> | 
                                                                        <asp:Label runat="server" ID="Label2" Style="white-space: normal" CssClass="text-uppercase" Font-Size="12px"><%# Eval("service_name_en") %></asp:Label><br />
                                                                        Duty Date/Time:
																	<asp:Label runat="server" Font-Bold="true" ID="Label3" CssClass=""><%# Eval("departuretime") %></asp:Label>

                                                                    </div>
                                                                    <div runat="server" visible="false">
                                                                        <div class="col-sm-2">
                                                                            <h5>Bus:
																<asp:Label runat="server" ID="lblBusNo" CssClass="form-control-label text-sm"><%# Eval("busno") %></asp:Label>
                                                                            </h5>
                                                                        </div>
                                                                        <div class="col-sm-2">
                                                                            <h5>Driver:
																<asp:Label runat="server" ID="lblDriver1" CssClass="form-control-label  text-sm"><%# Eval("empdriver1") %></asp:Label>
                                                                                <br />
                                                                                <asp:Label runat="server" ID="lblDriver2" CssClass="form-control-label text-sm"><%# Eval("empdriver2") %></asp:Label>
                                                                            </h5>
                                                                        </div>
                                                                        <div class="col-sm-2">
                                                                            <h5>Conductor:
																<asp:Label runat="server" ID="lblConductor1" CssClass="form-control-label text-sm"><%# Eval("empconductor1") %></asp:Label><br />
                                                                                <asp:Label runat="server" ID="lblConductor2" CssClass="form-control-label  text-sm"><%# Eval("empconductor2") %></asp:Label></h5>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-auto text-right">
                                                                <asp:LinkButton ID="lbtnViewDuty" CommandName="viewDuty" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                    Visible="true" runat="server" CssClass="btn btn-sm btn-warning"
                                                                    ToolTip="View"> <i class="fa fa-eye"></i></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlNoRecord2" runat="server" Width="100%" Visible="true">
                                    <div class="col-md-12 p-0" style="text-align: center;">
                                        <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
                                            No Record Available<br />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <cc1:ModalPopupExtender ID="mpShowStatus" runat="server" PopupControlID="pnlShowStatus" CancelControlID="Button7"
                        TargetControlID="Button8" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlShowStatus" runat="server" Style="position: fixed;">
                        <div class="card" style="width: 350px;">
                            <div class="card-body text-center pt-2" style="min-height: 100px;">
                                <h4>Please Note</h4>
                                <asp:Label ID="lblAttendanceStatus" runat="server" Text="" Font-Size="13pt"></asp:Label>
                                <div style="width: 100%; margin-top: 20px; text-align: right;">
                                    <asp:LinkButton ID="lbtnAttendance" PostBackUrl="~/auth/tkAttendanceManagement.aspx" Visible="false" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnFreeBusCrew" OnClick="lbtnFreeBusCrew_Click" Visible="false" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button7" runat="server" Text="" />
                            <asp:Button ID="Button8" runat="server" Text="" />
                        </div>
                    </asp:Panel>
                </div>
                <div class="row m-0">
                    <cc1:ModalPopupExtender ID="mpUpdateDuty" runat="server" PopupControlID="pnlMarkLeave"
                        CancelControlID="Button5" TargetControlID="Button3" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlMarkLeave" runat="server">
                        <div class="card" style="min-width: 550px;">
                            <div class="card-header">
                                <h3 class="mb-0"><span style="font-size: 11pt;" class="text-muted">Update Crew for Depot Service</span><br />
                                    <span runat="server" id="lblServiceName"></span>

                                </h3>
                                <asp:Label Visible="false" runat="server" ID="lblRoute" Text="N/A"></asp:Label>

                                <asp:Label Visible="false" runat="server" ID="lblServiceType" Text="N/A"></asp:Label>
                                <asp:Label runat="server" Visible="false" ID="lblBusType" Text="N/A"></asp:Label>
                                <asp:Label runat="server" Visible="false" ID="lblCrewNo" Text="N/A"></asp:Label>
                            </div>
                            <div class="card-body text-left pt-2 row" style="min-height: 100px;">
                                <div class="col-lg-12">
                                    <div class="row m-0">
                                  
                                        <div class="col-lg-12 mb-3">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <table class="table table2 mb-2 table-borderless">
                                                        <tr>
                                                            <td style="width: 30%;"><b>Bus</b></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlUBus" runat="server" CssClass="form-control form-control-sm" ToolTip="Bus" AutoPostBack="true">
                                                                </asp:DropDownList></td>
                                                        </tr>
                                                        <tr runat="server" id="trDriver1">
                                                            <td><b>Driver</b> </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlUDriver" runat="server" CssClass="form-control form-control-sm" ToolTip="Driver" AutoPostBack="true">
                                                                </asp:DropDownList></td>
                                                        </tr>
                                                        <tr runat="server" id="trConductor1">
                                                            <td><b>Conductor</b></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlUConductor" runat="server" CssClass="form-control form-control-sm" ToolTip="Conductor" AutoPostBack="true">
                                                                </asp:DropDownList></td>
                                                        </tr>
                                                        <tr runat="server" id="trDriver2">
                                                            <td><b>Alternate Driver</b></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlUAltDriver" runat="server" CssClass="form-control form-control-sm" ToolTip="Second Driver">
                                                                </asp:DropDownList></td>
                                                        </tr>
                                                        <tr runat="server" id="trConductor2">
                                                            <td><b>Alternate Conductor</b></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlUAltConductor" runat="server" CssClass="form-control form-control-sm" ToolTip="Second Conductor">
                                                                </asp:DropDownList></td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="text-center" style="width: 100%; margin-top: 20px;">
                                                <asp:LinkButton ID="lbtnUpdateDuty" runat="server" OnClick="lbtnUpdateDuty_Click" CssClass="btn btn-success ml-2"> <i class="fa fa-save"></i> Update </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="btn btn-danger ml-2"> <i class="fa fa-times"></i> Cancel </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button3" runat="server" Text="" />
                            <asp:Button ID="Button5" runat="server" Text="" />
                        </div>
                    </asp:Panel>
                </div>
                <div class="row">
                    <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
                        CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
                        <div class="card" style="width: 350px;">
                            <div class="card-header">
                                <h4 class="card-title text-left mb-0">Please Confirm
                                </h4>
                            </div>
                            <div class="card-body text-left pt-2" style="min-height: 100px;">
                                <asp:Label ID="lblConfirmErrorMsg" Font-Size="11pt" ForeColor="Red" runat="server" Text=""></asp:Label>
                                <br />
                                <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to generate provisional duty allotment ?"></asp:Label>
                                <div style="width: 100%; margin-top: 20px; text-align: right;">
                                    <asp:LinkButton ID="lbtnYesConfirmation" OnClick="lbtnYesConfirmation_Click" OnClientClick="return ShowLoading()" runat="server" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button4" runat="server" Text="" />
                        </div>
                    </asp:Panel>
                </div>
                <div class="row">
                    <cc1:ModalPopupExtender ID="mpDuty" runat="server" PopupControlID="pnlDuty" TargetControlID="btnOpen"
                        CancelControlID="lbtnClose" BackgroundCssClass="ModalPopupBG">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlDuty" runat="server" Style="position: fixed;">
                        <div class="modal-content mt-5" style="width: 90vw;">
                            <div class="card w-100">
                                <div class="card-header py-3">
                                    <div class="row">
                                        <div class="col">
                                            <h3 class="m-0">List of generated provisional duty allotment</h3>
                                        </div>
                                        <div class="col-auto">
                                            <asp:LinkButton ID="lbtnClose" runat="server" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body p-2">

                                    <asp:Literal ID="eDash" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div style="visibility: hidden;">
                            <asp:Button ID="btnOpen" runat="server" Text="" />
                            <asp:Button ID="lbtnClosemp" runat="server" Text="" />
                        </div>
                    </asp:Panel>
                </div>

                <div class="row">
                    <cc1:ModalPopupExtender ID="mpShowDuty" runat="server" PopupControlID="Panel1" TargetControlID="Button1"
                        CancelControlID="LinkButton2" BackgroundCssClass="ModalPopupBG">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="Panel1" runat="server" Style="position: fixed;">
                        <div class="modal-content mt-5" style="width: 65vw;">
                            <div class="card w-100">
                                <div class="card-header py-3">
                                    <div class="row">
                                        <div class="col">
                                            <h3 class="m-0">Duty Allotment Slip</h3>
                                        </div>
                                        <div class="col-auto">
                                            <asp:LinkButton ID="LinkButton2" runat="server" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body p-2">

                                    <asp:Literal ID="eSlip" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button1" runat="server" Text="" />
                            <asp:Button ID="Button2" runat="server" Text="" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

