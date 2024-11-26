<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/DieselMaster.master" AutoEventWireup="true" CodeFile="DieselFillEntry.aspx.cs" Inherits="Auth_DieselFillEntry" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    	<%--<link href="../assets/vendor/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />--%>
    <script type="text/javascript">
        $(document).ready(function () {

            loaddatepicker();
        });
        function loaddatepicker() {
            var currDate = new Date().getDate();
            var preDate = new Date(new Date().setDate(currDate - 1));
            var todayDate = new Date(new Date().setDate(currDate));

            $('[id*=tbFromDate]').datepicker({
                endDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
            $('[id*=tbToDate]').datepicker({
                endDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
        }
    </script>
    <style>
        input[type=checkbox], input[type=radio] {
            margin: 1px 0px 0px;
            box-sizing: border-box;
            padding: 0;
            width: 13px;
            height: 13px;
        }

        hr {
            margin: 5px 0px;
        }

        .table td, .table th {
            padding: 5px 3px !important;
            vertical-align: top;
            border-top: none !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid mt-4">
         <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>--%>
                <asp:HiddenField ID="hidtoken" runat="server" />
                <asp:Panel runat="server" ID="pnlDieselEntry" Visible="true">
                    <div class="row mb-3">
                        <div class="col-lg-5 col-md-5 col-sm-12 pr-1">
                            <div class="card mb-3" style="min-height: 600px;">
                                <div class="card-body pt-0 pl-2 pr-2">
                                    <asp:Label runat="server" class="h4 font-weight-bold mb-0">Tank Wise (Available Qty.)</asp:Label>
  <asp:LinkButton href="../Auth/UserManuals/Diesel/Help Document for diesel.pdf" target="_blank" runat="server" CssClass="btn btn-success btn-sm float-right ml-1" ToolTip="Click here for manual."><i class="fa fa-download"></i></asp:LinkButton>
<asp:Label runat="server" Text="Download Manual" CssClass="float-right"></asp:Label>
                            
                                    <div class="row mt-1 m-0">
                                        <asp:Repeater ID="gvTankAvaibility" runat="server">
                                            <ItemTemplate>
                                                <div class="col-lg-6">
                                                    <asp:Label runat="server" class="h5 font-weight-bold mb-0">Tank No. </asp:Label>
                                                    <asp:Label ID="lbltankno" CssClass="form-control-label" runat="server" Text='<%#Eval("tank_no")%>'></asp:Label>
                                                </div>
                                                <div class="col-lg-6 text-right">
                                                    <asp:Label ID="lblavailableqty" runat="server" CssClass="form-control-label" Font-Bold="true" Text='<%#Eval("available_qty")%>'></asp:Label>
                                                    <%--<asp:Label ID="lblgvRemain" runat="server" CssClass="form-control-label" Font-Bold="true" Text='<%#Eval("available_qty")%>'></asp:Label>--%>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <hr class="mb-1 mt-1" />
                                    <div class="card-header border-bottom px-0">
                                        <div class="row">
                                            <div class="col-lg-12 text-left">
                                                <asp:Label runat="server" class="h4 font-weight-bold mb-0">Last 5 Diesel Filling Entry</asp:Label>
                                                <asp:LinkButton ID="lbtnViewDieselDetails" OnClick="lbtnViewDieselDetails_Click" runat="server" ToolTip="View Diesel Details" Visible="true" CssClass="btn btn bg-gradient-orange btn-sm text-white" Style="font-size: 11px; padding: 3px 8px; float: right"><i class="fa fa-eye"></i> View All</asp:LinkButton>
                                            </div>
                                            <div class="col-lg-12">
                                                <div runat="server" id="divSearchBar" style="display: flex; justify-content: center; padding-top: 5px;">
                                                    <label class="containerRadio">
                                                        <asp:RadioButton runat="server" ID="rbtnRunningBus" OnCheckedChanged="rbtnRunningBus_CheckedChanged" GroupName="radio" Checked="true" AutoPostBack="true" />
                                                        <span class="checkmarkRadio"></span>
                                                        Running Bus
                                                    </label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <label class="containerRadio">
                            <asp:RadioButton runat="server" ID="rbtnNewBus" GroupName="radio" AutoPostBack="true" OnCheckedChanged="rbtnRunningBus_CheckedChanged" />
                            <span class="checkmarkRadio"></span>
                            New Bus
                        </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row m-0">
                                        <div class="col-lg-12 p-0">
                                            <asp:GridView ID="gvLastFilled" runat="server" HeaderStyle-CssClass="thead-light font-weight-bold" AutoGenerateColumns="False" PageSize="2" GridLines="None" AllowPaging="false" OnRowDataBound="gvLastFilled_RowDataBound"
                                                CssClass="table table-sm table-hover table-striped" PagerStyle-CssClass="GridPager" Font-Size="11px" Width="100%" ShowHeader="true" OnRowCommand="gvLastFilled_RowCommand" DataKeyNames="waybillno,buscondition,qtyfilled,dieselrefno,fillingdate,busregno,officename,crew,bustype,driver,conductor,driver2,conductor2,busno,servicename,dutydatetime,totalkm,targetincome,targetdieseavg,odometerreading,lastodometerreading,filledattype,tankno,pumpno,filledbyempid,pvtpumpname,pvtpumpaddress,pvtpumpreceiptno,pvtpumpamtpaid,fillingstationname,pvtfilledqty,pumpreading,depotpump_yn, mappedpump_yn, privatepump_yn, mappedpumpname, mappedreceiptno, mappedamtpaid, mappedqty, qty_filled">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Bus No." ItemStyle-HorizontalAlign="left" ItemStyle-CssClass="border-0" HeaderStyle-CssClass="border-0">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTank" runat="server" Text='<%#Eval("busregno")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Waybill No" ItemStyle-HorizontalAlign="left" ItemStyle-CssClass="border-0" HeaderStyle-CssClass="border-0">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWaybill" runat="server" Text='<%#Eval("waybillno")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="border-0" HeaderStyle-CssClass="border-0">
                                                        <HeaderTemplate>
                                                            <div style="width: 100%;">Diesel (Ltr)</div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDiesel" runat="server" Text='<%#Eval("qtyfilled")%>'></asp:Label>
                                                            Ltr
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="left" ItemStyle-CssClass="border-0" HeaderStyle-CssClass="border-0">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDate" runat="server" Text='<%#Eval("fillingdate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnView" CommandName="viewDetails" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                Visible="true" runat="server" CssClass="btn btn-sm btn-warning" Style="border-radius: 4px; font-size: 8pt; padding: 2px 5px;"
                                                                ToolTip="View Details"> <i class="fa fa-eye"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                            </asp:GridView>
                                            <asp:Panel ID="pnlNoLastFilled" runat="server" Width="100%" Visible="true">
                                                <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                                    <div class="col-md-12 busListBox pt-3" style="color: #e3e3e3; font-size: 12pt; font-weight: bold;">
                                                        No Record Available<br />
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="card-header border-bottom px-0">
                                        <div class="row">
                                            <div class="col">
                                                <asp:Label runat="server" class="h4 font-weight-bold mb-0"> Waybills due for fuel Refilling</asp:Label>
                                            </div>
                                            <div class="col-auto">
                                                <div class="input-group">
                                                    <asp:TextBox CssClass="form-control text-uppercase" runat="server" ID="tbSearchWaybill" MaxLength="11" placeholder="Waybill/Bus No"
                                                        Text="" Style="font-size: 9pt; padding: 2px 5px; height: 30px; margin-right: 5px;" autocomplete="off"></asp:TextBox>
                                                    <div class="input-group-append">
                                                        <asp:LinkButton ID="lbtnSearchPendingWaybill" runat="server" class="btn btn-sm bg-gradient-orange" Style="border-radius: 4px; height: 30px; font-size: 10pt; padding-top: 4px; color: #fff;"> <i class="fa fa-search"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row m-0">
                                        <div class="col-lg-12 p-0">
                                         
 <asp:GridView ID="gvWayBillDue" runat="server" OnRowCommand="gvWayBillDue_RowCommand" HeaderStyle-CssClass="thead-light font-weight-bold" AutoGenerateColumns="False" PageSize="5" GridLines="None" AllowPaging="true" DataKeyNames="dutyrefno"
                                                CssClass="table table-hover table-striped" PagerStyle-CssClass="GridPager" Font-Size="11px" Width="100%" ShowHeader="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="left">
                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="lbtnPendingWaybill" ToolTip="Pending Waybill" runat="server" CommandName="pendingWaybill" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'>

                                                                <div class="row">
                                                                    <div class="col-6">
                                                                        <asp:Label runat="server" class="h5 font-weight-bold mb-0">Waybill No</asp:Label>&nbsp;
                                                                        <asp:Label ID="lblgvName" CssClass="form-control-label" runat="server" Text='<%#Eval("dutyrefno")%>'></asp:Label><br />
                                                                    </div>
                                                                    <div class="col-6 text-right ">
                                                                        <asp:Label ID="lblDutyDateTime" CssClass="form-control-label" runat="server" Text='<%#Eval("dutydatetime")%>'></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <asp:Label ID="lblServiceName" runat="server" class="h5 font-weight-bold mb-0 text-uppercase"  Text='<%#Eval("servicename")%>'></asp:Label><br />
                                                                    </div>
                                                                </div>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                            </asp:GridView>
 
                                            <asp:Panel ID="pnlNoRecord" runat="server" Width="100%" Visible="true">
                                                <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                                    <div class="col-md-12 busListBox pt-3" style="color: #e3e3e3; font-size: 12pt; font-weight: bold;">
                                                        No Record Available<br />
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-7 col-md-7 col-sm-12 pl-1">
                            <div class="card mb-3" style="min-height: 600px">
                                <div class="card-body pt-0">
                                    <asp:Panel runat="server" ID="pnlBusDieselEntry">
                                        <asp:Panel runat="server" ID="pnlFirst" Visible="true">
                                            <div class="col-lg-12 pl-0 text-right" style="padding-top: 10px;">
                                                <asp:LinkButton ID="LinkButtonInfo" Visible="false" OnClick="LinkButtonInfo_Click" runat="server" class="btn btn-sm btn-danger"
                                                    Style="margin-top: 0px; font-size: 15px; margin-bottom: 4px; color: white; line-height: 19px;">help <i class="fa fa-info-circle" ></i></asp:LinkButton>
                                            </div>
                                            <p class="text-center" style="font-size: 22px; font-weight: bold; color: #dcd9d9; margin-top: 15px;">
                                                To start diesel filling details click on pending waybills given on left panel for running bus
												<br />
                                                <br />
                                                Or<br />
                                                <br />
                                                Click on below Button for new bus<br />
                                                <br />
                                                <asp:LinkButton ID="lbtnNewBus" runat="server" OnClick="lbtnNewBus_Click" CssClass="btn btn-primary" ToolTip="Diesel Entry of New Bus" Style="margin-top: -3px;"><i class="fa fa-bus"></i> New Bus</asp:LinkButton>
                                            </p>
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="pnlBusDetails" Visible="false">
                                            <div class="row m-0 mt-2 mb-2" runat="server" id="divWaybillDetails" visible="false" style="background: #e8e7e7; padding: 5px 10px;">
                                                <div class="row m-0" style="font-size: 12px; width: 100%;">
                                                    <div class="col-lg-8">
                                                        <asp:Label runat="server" ID="lblServiceName" Font-Bold="true">Dehradun</asp:Label>
                                                    </div>
                                                    <div class="col-lg-4 text-right">
                                                        <i class="fa fa-building"></i>
                                                        <asp:Label runat="server" ID="lblDepot" CssClass="form-control-label text-danger" Text="Own Depot"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row m-0 mt-1" style="font-size: 11px; width: 100%;">
                                                    <div class="col-lg-3">
                                                        <i class="fa fa-clock-o"></i>
                                                        <asp:Label runat="server" CssClass="form-control-label" ID="lblDutyTime">22/02/2022</asp:Label>
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <i class="fa fa-bus"></i>
                                                        <asp:Label runat="server" ID="lblWaybillBusNo" CssClass="form-control-label">UK07TB-4356</asp:Label>
                                                        (<asp:Label runat="server" ID="lblWaybillBusType" CssClass="form-control-label">Corporation</asp:Label>
                                                        <asp:Label runat="server" ID="lblWaybillBusTypeId" CssClass="form-control-label" Text="" Visible="false"></asp:Label>)
                                                    </div>
                                                    <div class="col-lg-6">

                                                        <i class="fa fa-dashboard"></i>
                                                        <asp:Label runat="server" CssClass="form-control-label"> Odometer</asp:Label>
                                                        Before Journey
                                                <asp:Label runat="server" ID="lblWaybillOdometer" CssClass="form-control-label">3</asp:Label>
                                                        After Journey
                                                <asp:Label runat="server" ID="lblWaybillFinalOdometer" CssClass="form-control-label">3</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row m-0 mt-1" style="font-size: 13px; width: 100%;">
                                                    <div class="col-lg-12">
                                                        <i class="fa fa-user"></i>
                                                        <asp:Label runat="server" ID="lblWaybillDriver1" CssClass="form-control-label">Suraj(UTC0005)</asp:Label>, 
                                                <asp:Label runat="server" ID="lblWaybillConductor1" CssClass="form-control-label">Ashish</asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row  mt-2" runat="server" id="divBusDetails" visible="false">
                                                <div class="col-md-12 ">
                                                    <div class="card-header border-bottom">
                                                        <div class="col-md-12 ">
                                                            <asp:Label runat="server" class="h3 font-weight-bold mb-0"> Bus Details </asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-12 ">
                                                    <div class="row mt-2 ml-3 m-0">
                                                        <div class="col-lg-3 text-left">
                                                            <asp:Label runat="server" CssClass="form-control-label">Bus Registration No</asp:Label>
                                                            <asp:DropDownList ID="ddlBus" CssClass="form-control form-control-sm" ToolTip="Bus Registration No" AutoPostBack="true" OnSelectedIndexChanged="ddlBus_SelectedIndexChanged" runat="server" Style="height: 30px; padding: 2px 5px;">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-lg-3 text-left">
                                                            <asp:Label runat="server" CssClass="form-control-label">Bus Type</asp:Label>
                                                            <asp:DropDownList ID="ddlBusType" CssClass="form-control form-control-sm" ToolTip="Bus Type" runat="server" Style="height: 30px; padding: 2px 5px;">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-lg-3 text-left">
                                                            <asp:Label runat="server" CssClass="form-control-label">Odometer Reading</asp:Label>
                                                            <asp:TextBox ID="tbODOMeterReading" runat="server" Text="0" autocomplete="off" MaxLength="7" ToolTip="Enter ODO Meter Reading" class="form-control form-control-sm"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FTBEtxtODOMeter" runat="server" FilterType="Numbers" TargetControlID="tbODOMeterReading" />
                                                        </div>
                                                        <div class="col-lg-3 text-left" runat="server" id="divTotalKM" visible="false">
                                                            <asp:Label runat="server" CssClass="form-control-label">Total KM</asp:Label>
                                                            <asp:TextBox ID="tbtotalKm" runat="server" Text="0" autocomplete="off" MaxLength="7" ToolTip="Enter ODO Meter Reading" ReadOnly="true" class="form-control form-control-sm"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="tbtotalKm" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row m-0 mt-2" runat="server" id="divOtherDepotDetails" visible="false">
                                                <div class="col-lg-12 text-left">
                                                    <h6 style="color: darkgrey; font-size: 13pt;">Fuel Filled in Other Depots</h6>
                                                    <asp:GridView ID="gvFuelFilledOtherDepots" HeaderStyle-CssClass="thead-light font-weight-bold" runat="server" AutoGenerateColumns="False" PageSize="10" GridLines="None" AllowPaging="true"
                                                        CssClass="table table-hover table-striped" PagerStyle-CssClass="GridPager" Font-Size="9pt" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Depot" ItemStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOfficeName" runat="server" Text='<%#Eval("office_name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Filling Station" ItemStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFllingStationName" runat="server" Text='<%#Eval("fillingstnname")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblqtyFilled" runat="server" Text='<%#Eval("qtyfilled")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Reference No" ItemStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRefrenceNo" runat="server" Text='<%#Eval("dieselrefno")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date & Time" ItemStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDateAndTime" runat="server" Text='<%#Eval("fillingdate")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                                    </asp:GridView>
                                                    <asp:Panel ID="pnlNoOtherDepotRecord" runat="server" Width="100%" Visible="true">
                                                        <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                                            <div class="col-md-12 busListBox pt-3" style="color: #e3e3e3; font-size: 12pt; font-weight: bold;">
                                                                No Record Available<br />
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                                <hr />
                                            </div>
                                            <div class="row" id="divDepotFilling" runat="server" visible="false">
                                                <div class="col-md-12 ">
                                                    <div class="card-header border-bottom">
                                                        <div class="col-md-12 ">
                                                            <asp:Label runat="server" class="h4 font-weight-bold mb-0">  </asp:Label>
                                                            <asp:CheckBox runat="server" ID="chkOwnFilling" OnCheckedChanged="chkOwnFilling_CheckedChanged" AutoPostBack="true" Text="Fuel Filled at Own Station" Font-Bold="true" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row mt-3 ml-3 m-0" runat="server" id="divdepotfillingfield" visible="false">
                                                    <div class="col-lg-8 pr-0">
                                                        <div class="row">
                                                            <div class="col-lg-4 text-left">
                                                                <asp:Label runat="server" CssClass="form-control-label"> Filling Station</asp:Label>
                                                                <asp:DropDownList ID="ddlFillingStation" runat="server" ToolTip="Enter Filling Station" AutoPostBack="true" OnSelectedIndexChanged="ddlFillingStation_SelectedIndexChanged" CssClass="form-control form-control-sm" Style="font-size: 10pt; display: inline; height: 30px;"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-lg-4 text-left">
                                                                <asp:Label runat="server" CssClass="form-control-label"> Tank</asp:Label>
                                                                <asp:DropDownList ID="ddlTank" runat="server" ToolTip="Enter Tank" AutoPostBack="true" OnSelectedIndexChanged="ddlTank_SelectedIndexChanged" CssClass="form-control form-control-sm" Style="font-size: 10pt; display: inline; height: 30px;"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-lg-4 text-left">
                                                                <asp:Label runat="server" CssClass="form-control-label"> Pump Number</asp:Label>
                                                                <asp:DropDownList ID="ddlPump" OnSelectedIndexChanged="ddlPump_SelectedIndexChanged" AutoPostBack="true" runat="server" ToolTip="Enter Pump Number" CssClass="form-control form-control-sm" Style="font-size: 10pt; display: inline; height: 30px;"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-4 pr-0">
                                                        <div class="row">
                                                            <div class="col-lg-6 text-left">
                                                                <asp:Label runat="server" CssClass="form-control-label">Pump Reading</asp:Label>
                                                                <asp:TextBox ID="tbPumpReading" Text="0" runat="server" MaxLength="7" ToolTip="Enter Pump Reading" placeholder="Pump Reading" autocomplete="off" class="form-control form-control-sm"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="tbPumpReading" />
                                                            </div>
                                                            <div class="col-lg-6 text-left">
                                                                <asp:Label runat="server" CssClass="form-control-label">Quantity</asp:Label>
                                                                <asp:TextBox ID="tbFuelQuantity" Text="0" runat="server" ToolTip="Enter Fuel Quantity" MaxLength="6" placeholder="Fuel Quantity" class="form-control form-control-sm" autocomplete="off"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars="." FilterType="Custom,Numbers"
                                                                    TargetControlID="tbFuelQuantity" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                </div>
                                            </div>
                                            <div class="row" id="divPumpMapping" runat="server" visible="false">
                                                <div class="col-lg-12 text-left mt-2">
                                                    <div class="col-md-12 ">
                                                        <div class="card-header border-bottom">
                                                            <div class="col-md-12 ">
                                                                <asp:CheckBox runat="server" ID="chkMappingFilling" OnCheckedChanged="chkMappingFilling_CheckedChanged" AutoPostBack="true" Text="Fuel Filled at Depot Mapping Station" Font-Bold="true" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row m-0 mt-2 pl-3" runat="server" id="divMappingFields" visible="false">
                                                        <div class="col-lg-3 text-left">
                                                            <asp:Label runat="server" CssClass="form-control-label">Receipt No</asp:Label>
                                                            <asp:TextBox ID="tbdreceiptno" runat="server" CssClass="form-control form-control-sm" ToolTip="Enter Reference No" MaxLength="20" placeholder="Max 20 Characters" autocomplete="off"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-3 text-left">
                                                            <asp:Label runat="server" CssClass="form-control-label">Quantity(Ltr)</asp:Label>
                                                            <asp:TextBox ID="tbdquantity" runat="server" ToolTip="Enter Filled Quantity" MaxLength="5" placeholder="Max Length 5" class="form-control form-control-sm" autocomplete="off"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom,Numbers"
                                                                TargetControlID="tbdquantity" ValidChars="." />
                                                        </div>
                                                        <div class="col-lg-3 text-left">
                                                            <asp:Label runat="server" CssClass="form-control-label">Amount Paid (<i class="fa fa-rupee-sign"></i>)</asp:Label>
                                                            <asp:TextBox ID="tbdamtpaid" runat="server" ToolTip="Enter Amount Paid" MaxLength="5" placeholder="Max Length 5" class="form-control form-control-sm" autocomplete="off"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom,Numbers"
                                                                TargetControlID="tbdamtpaid" ValidChars="." />
                                                        </div>
                                                        <div class="col-lg-3 text-left">
                                                            <asp:Label runat="server" CssClass="form-control-label">Pump Name</asp:Label>
                                                            <asp:DropDownList ID="ddlMappingPump" CssClass="form-control form-control-sm" ToolTip="Mapped Pump Name" AutoPostBack="true" OnSelectedIndexChanged="ddlBus_SelectedIndexChanged" runat="server" Style="height: 30px; padding: 2px 5px;">
                                                            </asp:DropDownList>
                                                        </div>

                                                    </div>

                                                </div>

                                            </div>
                                            <div class="row m-0 mt-3 pl-3" runat="server" id="divPvtFilling" visible="false">
                                                <div class="col-lg-12 text-left">
                                                    <asp:CheckBox runat="server" ID="chkPvtStatn" OnCheckedChanged="chkPvtStatn_CheckedChanged" AutoPostBack="true" Text="Fuel Filled at Private Filling Station" Font-Bold="true" />
                                                    <div class="row m-0 mt-2" runat="server" id="divPvtDetails" visible="false">
                                                        <div class="col-lg-3 text-left">
                                                            <asp:Label runat="server" CssClass="form-control-label">Receipt No</asp:Label>
                                                            <asp:TextBox ID="tbPvtPumpReceiptNo" runat="server" CssClass="form-control form-control-sm" ToolTip="Enter Reference No" MaxLength="20" placeholder="Max 20 Characters" autocomplete="off"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-3 text-left">
                                                            <asp:Label runat="server" CssClass="form-control-label">Quantity(Ltr)</asp:Label>
                                                            <asp:TextBox ID="tbPvtFilledQty" runat="server" ToolTip="Enter Filled Quantity" MaxLength="5" placeholder="Max Length 5" class="form-control form-control-sm" autocomplete="off"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom,Numbers"
                                                                TargetControlID="tbPvtFilledQty" ValidChars="." />
                                                        </div>
                                                        <div class="col-lg-3 text-left">
                                                            <asp:Label runat="server" CssClass="form-control-label">Amount Paid (<i class="fa fa-rupee-sign"></i>)</asp:Label>
                                                            <asp:TextBox ID="tbPvtPumpAmtPaid" runat="server" ToolTip="Enter Amount Paid" MaxLength="5" placeholder="Max Length 5" class="form-control form-control-sm" autocomplete="off"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom,Numbers"
                                                                TargetControlID="tbPvtPumpAmtPaid" ValidChars="." />
                                                        </div>
                                                        <div class="col-lg-3 text-left">
                                                            <asp:Label runat="server" CssClass="form-control-label">Pump Name</asp:Label>
                                                            <asp:TextBox ID="tbPvtPumpName" runat="server" ToolTip="Enter Fuel Quantity" MaxLength="100" placeholder="Max 100 Characters" CssClass="form-control form-control-sm" autocomplete="off"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-6 mt-2 text-left">
                                                            <asp:Label runat="server" CssClass="form-control-label">Address</asp:Label>
                                                            <asp:TextBox ID="tbPvtPumpAddress" CssClass="form-control form-control-sm" ToolTip="Enter Address" TextMode="MultiLine" Height="50px" runat="server" MaxLength="200" placeholder="Max 200 Characters" Width="100%"
                                                                Text="" Style="resize: none;" autocomplete="off"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="row m-0 mt-2" runat="server" id="divDefectYN" visible="false">
                                                <div class="col-lg-12 text-left">
                                                    <asp:CheckBox runat="server" ID="chkDefectsYN" OnCheckedChanged="chkDefectsYN_CheckedChanged" AutoPostBack="true" Text="Is there any defect in bus marked by driver?" Font-Bold="true" />
                                                </div>
                                                <div class="col-lg-12 px-5 py-2" runat="server" id="divMarkedDefects" visible="false">
                                                    <h4 style="font-size: 12pt; float: left; color: #584f4f">Marked Defects</h4>
                                                    <asp:LinkButton ID="lbtnAddDefects" OnClick="lbtnAddDefects_Click" runat="server" CssClass="btn btn-success" Style="font-size: 11px; float: right; padding: 3px 5px;"><i class="fa fa-plus"></i> Add More</asp:LinkButton>
                                                    <div class="clearfix"></div>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Repeater ID="rptrMarkedDefects2" runat="server" OnItemCommand="rptrMarkedDefects2_ItemCommand">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblGroupID" runat="server" Text='<%#Eval("groupID") %>' Visible="false" />
                                                                        <asp:Label ID="lblDefectID" runat="server" Text='<%#Eval("DefectID") %>' Visible="false" />
                                                                        <asp:LinkButton ID="lbtnDefect" runat="server" CommandName="deleteDefect" CssClass="btn btn-outline-danger" Style="font-size: 11px; padding: 3px 8px;"><%# Eval("itemName") %> <i class="fa fa-times"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="row m-0 mt-4">
                                                <div class="col-lg-12 text-center">
                                                    <asp:LinkButton ID="lbtnSave" runat="server" OnClick="lbtnSave_Click" ToolTip="Click here for save" CssClass="btn btn-success"><i class="fa fa-save"></i>&nbsp;Save</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnReset" runat="server" OnClick="lbtnReset_Click" ToolTip="Click here for reset" CssClass="btn btn-danger"><i class="fa fa-undo"></i>&nbsp;Reset</asp:LinkButton>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="pnlLastDieselFilled" Visible="false">
                                        <div class="row m-0">
                                            <div class="col-lg-12 col-md-12 p-0">
                                                <div class="card-header border-bottom px-0 pb-2">
                                                    <h3 class="mb-0 pb-0 " style="font-weight: 500; font-size: 14pt; color: black; padding-top: 10px">Diesel Filling Details
															<span style="color: red; font-size: 9pt;">(At a time details of only 30days is available)</span></h3>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row m-0">
                                            <div class="col-md-2 pl-0">
                                                <asp:Label runat="server" ID="Label1" CssClass="form-control-label">Bus Type</asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlBusNewRunning" CssClass="form-control">
                                                    <asp:ListItem Value="R" Text="Running Bus"></asp:ListItem>
                                                    <asp:ListItem Value="N" Text="New Bus"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2 pl-0">
                                                <asp:Label runat="server" ID="lblWaybillNo" CssClass="form-control-label">Waybill No</asp:Label>
                                                <asp:TextBox runat="server" class="form-control form-control-sm" ID="tbWaybillNo" MaxLength="10" placeholder="Enter waybill no"
                                                    Text="" autocomplete="off"></asp:TextBox>

                                            </div>
                                            <div class="col-md-2 pl-0">
                                                <asp:Label runat="server" ID="lblBusNo" CssClass="form-control-label">Bus No.</asp:Label>
                                                <asp:TextBox runat="server" class="form-control form-control-sm" ID="tbBusNo" MaxLength="20" placeholder="Enter Bus no"
                                                    Text="" autocomplete="off"></asp:TextBox>

                                            </div>
                                            <div class="col-md-2 pl-0">
                                                <asp:Label runat="server" ID="lblFromDate" CssClass="form-control-label">From Date</asp:Label>
                                                <asp:TextBox ID="tbFromDate" ToolTip="Enter From Date" runat="server" autocompletee="off" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbFromDate" ValidChars="/" />
                                            </div>
                                            <div class="col-md-2 pl-0">
                                                <asp:Label runat="server" ID="lblToDate" CssClass="form-control-label">To Date</asp:Label>
                                                <asp:TextBox ID="tbToDate" ToolTip="Enter To Date" runat="server" autocompletee="off" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbToDate" ValidChars="/" />
                                            </div>
                                            <div class="col-md-2 pl-0 mt-3 pt-2">
                                                <asp:LinkButton ID="lbtnsearch" OnClick="lbtnsearch_Click" runat="server" class="btn btn-sm btn-primary" Style="border-radius: 4px; height: 30px; font-size: 10pt; padding-top: 4px;"> <i class="fa fa-search"></i></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnResetFilter" OnClick="lbtnResetFilter_Click" runat="server" class="btn btn-sm btn-warning" Style="border-radius: 4px; height: 30px; font-size: 10pt; padding-top: 4px;"> <i class="fa fa-undo"></i></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnDownloadExcel" OnClick="lbtnDownloadExcel_Click" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white" Style="border-radius: 4px; height: 30px; font-size: 10pt; padding-top: 4px;" ToolTip="Download Excel"> <i class="fa fa-download"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row m-0 mt-2">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="gvFilledDieselDtls" runat="server" HeaderStyle-CssClass="thead-light font-weight-bold" AutoGenerateColumns="False" PageSize="20" GridLines="None" AllowPaging="true"
                                                    OnPageIndexChanging="gvFilledDieselDtls_PageIndexChanging" CssClass="table table-hover table-striped" PagerStyle-CssClass="GridPager" ShowHeader="true" OnRowDataBound="gvFilledDieselDtls_RowDataBound" OnRowCommand="gvFilledDieselDtls_RowCommand" 
                                                    DataKeyNames="waybillno,buscondition,qtyfilled,dieselrefno,fillingdate,busregno,officename,crew,bustype,driver,conductor,driver2,conductor2,busno,servicename,dutydatetime,totalkm,targetincome,targetdieseavg,odometerreading,lastodometerreading,filledattype,tankno,pumpno,filledbyempid,pvtpumpname,pvtpumpaddress,pvtpumpreceiptno,pvtpumpamtpaid,fillingstationname,pvtfilledqty,pumpreading,
                                                    depotpump_yn, mappedpump_yn, privatepump_yn, mappedpumpname, mappedreceiptno, mappedamtpaid, mappedqty, qty_filled">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Diesel Date" ItemStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDate" runat="server" Text='<%#Eval("fillingdate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Waybill No" ItemStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblWaybill" runat="server" Text='<%#Eval("buscondition")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bus No." ItemStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTank" runat="server" Text='<%#Eval("busregno")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Depot" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDepot" runat="server" Text='<%#Eval("officename")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Filled Qty." ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDiesel" runat="server" Text='<%#Eval("qtyfilled")%>'></asp:Label>
                                                                Ltr
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnView" CommandName="viewDetails" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Visible="true" runat="server" CssClass="btn btn-sm btn-warning" Style="border-radius: 4px; font-size: 10pt; padding: 2px 5px;" ToolTip="View Details"> <i class="fa fa-eye"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Panel ID="pnlNoLastFilledDtls" runat="server" Width="100%" Visible="true">
                                                    <p class="text-center" style="font-size: 22px; font-weight: bold; color: #dcd9d9; margin-top: 50px;">
                                                        No Record Available
                                                    </p>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlDefectsEntry" Visible="false" Style="box-shadow: 2px 4px 25px 20px #596166c2;">
                    <div class="card" style="font-size: 12px; min-height: 500px; border: none; box-shadow: none; background: #f1f0f0">
                        <div class="card-body p-0">
                            <div class="col-lg-12" style="font-size: 13px;">
                                <div class="col-lg-12 mb-3 pt-2">
                                    <div class="card-header border-bottom">
                                        <div class="row">
                                            <div class="col-md-8 ">
                                                <asp:Label runat="server" class="h4 font-weight-bold mb-0">Mark Defects of Bus
                                                    <asp:Label runat="server" CssClass="form-control-label" ID="lblWaybillBusNo2"></asp:Label>
                                                    (<asp:Label runat="server" CssClass="form-control-label" ID="lblWaybillBusType2" Font-Bold="true"></asp:Label>)
                                                </asp:Label>
                                            </div>
                                            <div class="col-md-4 text-right">
                                                <asp:LinkButton ID="lbtnBack" OnClick="lbtnBack_Click" runat="server" CssClass="btn btn-danger btn-sm ml-2" Style="float: right; font-size: 10pt;"> <i class="fa fa-arrow-left"></i> Back</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12 mt-1 mb-2" style="background: white; padding: 5px 10px;">
                                    <div class="row" style="font-size: 12px; width: 100%;">
                                        <div class="col-lg-8">
                                            <asp:Label runat="server" CssClass="form-control-label" ID="lblServiceName2"></asp:Label>
                                        </div>
                                        <div class="col-lg-2">
                                            <i class="fa fa-clock-o"></i>
                                            <asp:Label runat="server" CssClass="form-control-label" ID="lblDutyTime2"></asp:Label>
                                        </div>
                                        <div class="col-lg-2 text-right">
                                            <i class="fa fa-building"></i>
                                            <asp:Label runat="server" ID="lblDepot2" CssClass="form-control-label text-danger" Text="Own Depot"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12 mb-3 p-0">
                                    <div class="row m-0">
                                        <div class="col-lg-4">
                                            <div style="padding: 5px 10px; background: white; min-height: 440px;">
                                                <asp:GridView ID="gvDefectsGroup" runat="server" OnRowCommand="gvDefectsGroup_RowCommand" AutoGenerateColumns="false" GridLines="None" Font-Size="10pt" ClientIDMode="Static"
                                                    AllowPaging="false" PageSize="10" CssClass="table table-striped pt-2 mt-2" DataKeyNames="RPGP_ID,GROUP_DESC_EN">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Group">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStartTime" runat="server" Text='<%#Eval("group_desc_en") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="30px">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnMarkDefects" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Assign New Role" CommandName="markDefects" CssClass="btn btn-warning btn-sm" Style="padding: 0px 10px;"><i class="fa fa-arrow-right"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="col-lg-12 col-md-12 p-0">
                                                <div class="card-header border-bottom">
                                                    <div class="row">
                                                        <div class="col-md-8 ">
                                                            <asp:Label runat="server" ID="DefectsHead" class="h4 font-weight-bold mb-0"> Select Defects</asp:Label>
                                                        </div>
                                                        <div class="col-md-4 text-right">
                                                            <asp:TextBox ID="tbSearch" Visible="false" runat="server" onkeyup="SearchDefects(this,'#chkDefectItems');" CssClass="textbox" placeholder="Search Defects" Style="width: 140px; float: right; height: 25px; padding: 2px 5px !important;" autocomplete="off">
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="padding: 5px 10px; background: white; min-height: 494px;">
                                                <asp:CheckBoxList ID="chkDefectItems" OnSelectedIndexChanged="chkDefectItems_SelectedIndexChanged" runat="server" ClientIDMode="Static" DataTextField="language" Font-Size="14px" Width="100%" AutoPostBack="true"
                                                    DataValueField="language" RepeatDirection="Horizontal" RepeatColumns="1" CssClass="tableChkBox">
                                                </asp:CheckBoxList>
                                                <div class="clearfix"></div>
                                                <asp:Panel ID="pnlNoDefectForMark" runat="server" Width="100%" Visible="true">
                                                    <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                                        <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
                                                            <asp:Label ID="lblNoDefects" runat="server" Text="Select Defect Group" />
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="col-lg-12 col-md-12 p-0">
                                                <div class="card-header border-bottom">
                                                    <div class="row">
                                                        <div class="col-md-8 ">
                                                            <asp:Label runat="server" class="h4 font-weight-bold mb-0"> Marked Defects</asp:Label>
                                                        </div>
                                                        <div class="col-md-4 text-right">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="padding: 5px 10px; background: white; min-height: 494px;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Repeater ID="rptrMarkedDefects" runat="server">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGroupID" runat="server" Text='<%#Eval("groupID") %>' Visible="false" />
                                                                    <asp:Label ID="lblDefectID" runat="server" Text='<%#Eval("DefectID") %>' Visible="false" />
                                                                    <asp:LinkButton ID="lbtnDefect" runat="server" CommandName="deleteDefect" CssClass="btn btn-primary mt-2" Style="font-size: 9pt; padding: 6px;"><%# Eval("itemName") %> <i class="fa fa-times"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </td>
                                                    </tr>
                                                </table>

                                                <asp:Panel ID="pnlMarkedDefects" runat="server" Width="100%" Visible="true">
                                                    <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                                        <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
                                                            Defects Not Marked
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlViewBusDieselDetails" Visible="false" Style="box-shadow: 2px 4px 25px 20px #a4a8ab; width: 90%; margin-left: auto; margin-right: auto;">
                    <div class="card" style="font-size: 12px; min-height: 100px; border: none; box-shadow: none;">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <h4 runat="server" id="H1" class="text-center" style="font-size: 15pt; font-weight: 700; display: inline">Bus Diesel Filling Entry Details for Bus (<asp:Label runat="server" ID="lblDFBusNo"></asp:Label>)</h4>
                                    <asp:LinkButton ID="lbtnCloseDieselDetails" OnClick="lbtnCloseDieselDetails_Click" runat="server" CssClass="btn btn-danger btn-sm ml-2" Style="margin: -20px -25px; float: right;"> <i class="fa fa-times"></i></asp:LinkButton>
                                </div>
                            </div>
                            <div class="row m-0 mt-1 mb-2" runat="server" id="divDFBusServiceDtls" visible="true" style="background: #e8e7e7; padding: 5px 10px;">

                                <div class="row" style="font-size: 12px; width: 100%;" runat="server" id="divDFWaybillBusDetails" visible="false">
                                    <div class="col-lg-8">
                                        <asp:Label runat="server" ID="lblDFServiceName" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="col-lg-4 text-right">
                                        <i class="fa fa-clock"></i>
                                        <asp:Label runat="server" ID="lblDFDutyDateTime" CssClass="text-danger" Text="Own Depot"></asp:Label>
                                    </div>
                                </div>

                                <div class="row mt-1" runat="server" id="divDFBusDetails" visible="false" style="font-size: 10px; width: 100%;">
                                    <div class="col-lg-12">
                                        <div class="row" style="font-size: 9pt">
                                            <div class="col-lg-4 text-left">
                                                <label style="font-weight: 500;">Bus Registration No</label>
                                                <asp:Label ID="lblDFBusRegNo" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-lg-4 text-left">
                                                <label style="font-weight: 500;">Bus Type</label>
                                                <asp:Label ID="lblDFBusType" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-lg-4 text-left">
                                                <label style="font-weight: 500;">Odometer Reading</label>
                                                <asp:Label ID="lblDFOdometerReading" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" visible="true" id="divdepotfilled">
                                <div class="col-lg-12 text-left">
                                    <h6 style="color: #a9a9a9; font-size: 13pt;">Fuel Filled at Own Station</h6>
                                    <div class="row">
                                        <div class="col-lg-6 pr-0">
                                            <div class="row">
                                                <div class="col-lg-4 pr-0 text-left">
                                                    <label style="font-weight: 500;">Filling Station</label><br />
                                                    <asp:Label ID="lblDFFillingSt" runat="server" Text="N/A"></asp:Label>
                                                </div>
                                                <div class="col-lg-4 pr-0 text-left">
                                                    <label style="font-weight: 500;">Tank</label><br />
                                                    <asp:Label ID="lblDFTank" runat="server" Text="N/A"></asp:Label>
                                                </div>
                                                <div class="col-lg-4 text-left">
                                                    <label style="font-weight: 500;">Pump Number</label><br />
                                                    <asp:Label ID="lblDFPump" runat="server" Text="N/A"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="row">
                                                <div class="col-lg-6 pr-0 text-left">
                                                    <label style="font-weight: 500;">Pump Reading</label><br />
                                                    <asp:Label ID="lblDFPumpReading" runat="server" Text="0"></asp:Label>
                                                </div>
                                                <div class="col-lg-6 text-left">
                                                    <label style="font-weight: 500;">Quantity</label><br />
                                                    <asp:Label ID="lblDFQuantity" runat="server" Text="0"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                    </div>
                                    <hr />
                                </div>
                            </div>
                            <div class="row" runat="server" visible="true" id="divmappedfilled">
                                <div class="col-lg-12 text-left">
                                    <h6 style="color: #a9a9a9; font-size: 13pt;">Fuel Filled at Depot Mapping Station</h6>
                                    <div class="row">
                                        <div class="col-lg-6 pr-0">
                                            <div class="row">
                                                
                                                <div class="col-lg-4 pr-0 text-left">
                                                    <label style="font-weight: 500;">Receipt No.</label><br />
                                                    <asp:Label ID="lblmappedreceiptno" runat="server" Text="N/A"></asp:Label>
                                                </div>
                                                <div class="col-lg-4 text-left">
                                                    <label style="font-weight: 500;">Quantity(Ltr)</label><br />
                                                    <asp:Label ID="lblmappedquqntity" runat="server" Text="0"></asp:Label>
                                                </div>
                                                 <div class="col-lg-4 pr-0 text-left">
                                                    <label style="font-weight: 500;">Amount Paid(<i class="fa fa-rupee-sign"></i>)</label><br />
                                                    <asp:Label ID="lblmappedamt" runat="server" Text="0"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="row">
                                               <div class="col-lg-4 pr-0 text-left">
                                                    <label style="font-weight: 500;">Pump Name</label><br />
                                                    <asp:Label ID="lblmappedname" runat="server" Text="N/A"></asp:Label>
                                                </div>
                                                <%--<div class="col-lg-6 text-left">
                                                    <label style="font-weight: 500;">Quantity</label><br />
                                                    <asp:Label ID="Label6" runat="server"></asp:Label>
                                                </div>--%>
                                            </div>
                                        </div>
                                        <hr />
                                    </div>
                                    <hr />
                                </div>
                            </div>
                            <div class="row mt-2" runat="server" id="divDFPvtFillingDtls" visible="true">
                                <div class="col-lg-12 text-left">
                                    <span style="font-size: 13pt; color: DarkGray; font-weight: bold">Fuel Filled at Private Filling Station</span>
                                    <div class="row">
                                        <div class="col-lg-2 text-left">
                                            <label style="font-weight: 500;">Receipt No</label><br />
                                            <asp:Label ID="lblDFPvtReceiptNo" runat="server" Text="N/A"></asp:Label>
                                        </div>
                                        <div class="col-lg-2 text-left">
                                            <label style="font-weight: 500;">Quantity(Ltr)</label><br />
                                            <asp:Label ID="lblDFPvtQuantity" runat="server" Text="N/A"></asp:Label>
                                        </div>
                                        <div class="col-lg-2 text-left">
                                            <label style="font-weight: 500;">Amount Paid (<i class="fa fa-rupee-sign"></i>)</label><br />
                                            <asp:Label ID="lblDFPvtAmt" runat="server" Text="N/A"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 text-left">
                                            <label style="font-weight: 500;">Pump Name</label><br />
                                            <asp:Label ID="lblDFPvtPump" runat="server" Text="N/A"></asp:Label>
                                        </div>
                                        <div class="col-lg-3 mt-2 text-left">
                                            <label style="font-weight: 500;">Address</label><br />
                                            <asp:Label ID="lblDFPvtAddress" runat="server" Text="N/A"></asp:Label>
                                        </div>
                                    </div>
                                    <hr />
                                </div>
                            </div>
                            <div class="row mt-2" runat="server" id="divDFDefects" visible="false">
                                <div class="col-lg-12">
                                    <h6 style="color: #a9a9a9; font-size: 13pt; font-weight: bold">Defects Marked by Driver</h6>
                                    <div class="clearfix"></div>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Repeater ID="rptrDFDefectsList" runat="server">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbtnDefect" runat="server" CssClass="btn btn-danger" Enabled="false" Style="font-size: 11px; padding: 3px 8px;"><%# Eval("DEFECTNAME") %></asp:Label>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="pnlDFNoDefectsList" runat="server" Width="100%" Visible="true" CssClass="text-center">
                                        <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                            <div class="col-md-12 busListBox pt-3" style="color: #e3e3e3; font-size: 12pt; font-weight: bold;">
                                                No Record Available<br />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <hr />
                                </div>
                            </div>
                            <div class="row mt-2" runat="server" id="divDFOdrDepots" visible="true">
                                <div class="col-lg-12 text-left">
                                    <h6 style="color: #a9a9a9; font-size: 13pt; font-weight: bold">Fuel Filled in Other Depots</h6>
                                    <asp:GridView ID="gvDFFuelFilledOtherDepots" runat="server" AutoGenerateColumns="False" PageSize="10" GridLines="None" AllowPaging="true"
                                        CssClass="table tableOdrDepot table-striped" PagerStyle-CssClass="GridPager" Font-Size="9pt" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Depot" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvName" runat="server" Text='<%#Eval("OFFICENAME")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Filling Station" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvName2" runat="server" Text='<%#Eval("FILLINGSTATIONNAME")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAddress" runat="server" Text='<%#Eval("QTYFILLED")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reference No" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAddress" runat="server" Text='<%#Eval("DIESELREFNO")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date & Time" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvName" runat="server" Text='<%#Eval("fillingDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Panel ID="pnlDFNoOtherDepotRecord" runat="server" Width="100%" Visible="true">
                                        <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                            <div class="col-md-12 busListBox pt-3" style="color: #e3e3e3; font-size: 12pt; font-weight: bold;">
                                                No Record Available<br />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <hr />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
           <%-- </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="chkDefectsYN" />
                <asp:PostBackTrigger ControlID="chkPvtStatn" />
                <asp:PostBackTrigger ControlID="lbtnDownloadExcel" />
                <asp:PostBackTrigger ControlID="gvDefectsGroup" />
                <asp:PostBackTrigger ControlID="lbtnAddDefects" />
                <asp:PostBackTrigger ControlID="chkDefectItems" />
                <asp:PostBackTrigger ControlID="rptrMarkedDefects" />
                <asp:PostBackTrigger ControlID="lbtnSave" />
                <asp:PostBackTrigger ControlID="ddlBus" />
                <asp:PostBackTrigger ControlID="lbtnViewDieselDetails" />
            </Triggers>
        </asp:UpdatePanel>--%>
    </div>
    <div class="row">
        <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
            CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
            <div class="card">
                <div class="card-header">
                    <h4 class="card-title text-left mb-0">Please Confirm
                    </h4>
                </div>
                <div class="card-body text-left pt-2" style="min-height: 100px;">
                    <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                    <div style="width: 100%; margin-top: 20px; text-align: right;">
                        <asp:LinkButton ID="lbtnYesConfirmation" runat="server" OnClick="lbtnYesConfirmation_Click" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                        <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button4" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>

</asp:Content>

