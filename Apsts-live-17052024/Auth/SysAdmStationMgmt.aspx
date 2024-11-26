<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="SysAdmStationMgmt.aspx.cs" Inherits="Auth_SysAdmStationMgmt" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function txtOnKeyPress() {
            txt1 = document.getElementById('<%=tbSDFromHillDist.ClientID%>');
            txt2 = document.getElementById('<%=tbSDFromPlainDist.ClientID%>');
            txt3 = document.getElementById('<%=tbSDToHillDist.ClientID%>');
            txt4 = document.getElementById('<%=tbSDToPlainDist.ClientID%>');
            txt5 = document.getElementById('<%=txtTotalkm.ClientID%>');
            txt5.value = +txt1.value + +txt2.value + +txt3.value + +txt4.value;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="header pb-7">
    </div>
    <div class="container-fluid mt--6">
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="row">
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h4 class="mb-1">
                                            <asp:Label ID="lblSummary" runat="server" CssClass="text-capitalize">Summary as on Date 01/12/2021 04:00pm</asp:Label></h4>
                                        <div class="row m-0">
                                            <div class="col-6 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Total Stations:&nbsp;
                                 <asp:Label ID="lblTotStation" runat="server" ToolTip="Total Services Available" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Active: &nbsp;
                                <asp:Label ID="lblActivateStation" runat="server" ToolTip=" Services discontinued" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Bus Stand:&nbsp;
                                 <asp:Label ID="lblBSStation" runat="server" ToolTip="Active Services" class="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Discontinued: &nbsp;
                                <asp:Label ID="lblDeactStation" runat="server" ToolTip=" Services discontinued" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col">
                                        <div class="input-group-prepend">
                                            <h4 class="mb-1">Generate Station Report</h4>
                                            <asp:LinkButton ID="lbtndownload" OnClick="lbtndownload_Click" ToolTip="Download" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white ml-1">
                                            <i class="fa fa-download"></i>
                                            </asp:LinkButton>
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
                                        <asp:Label runat="server" CssClass="form-control-label">Coming Soon</asp:Label>
                                    </div>
                                    <div class="col-auto">
                                        <asp:LinkButton ID="lbtnview" ToolTip="View Instructions" runat="server" Width="30px" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" class="btn btn bg-gradient-green btn-sm text-white" Width="30px" ToolTip="Download Instruction" OnClick="Unnamed_Click">
                                            <i class="fa fa-download"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xl-4 order-xl-1">
                <asp:Panel ID="pnlStationDetails" runat="server" Visible="true">
                    <div class="card" style="min-height: 470px">
                        <div class="card-header border-bottom row m-0">
                            <h3 class="mb-0">Stations List</h3>
                        </div>
                        <div class="card-body p-0">
                            <div class="row m-0 align-items-center">
                                <div class="card card-stats">
                                    <div class="card-body">
                                        <div class="row m-0">
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="col-md-6 pr-0">
                                                        <h6 class="form-control-label text-muted my-0">State</h6>
                                                        <asp:DropDownList ID="ddlSState" ToolTip="State" CssClass="form-control form-control-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSState_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <h6 class="form-control-label text-muted my-0">District</h6>
                                                        <asp:DropDownList ID="ddlSDistrict" ToolTip="District" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <h6 class="form-control-label text-muted my-0">Station</h6>
                                                        <asp:TextBox ID="tbSearch" ToolTip="Enter Station" CssClass="form-control form-control-sm" MaxLength="20" runat="server" placeholder="Search Station" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6 mt-3 pt-1">
                                                        <asp:LinkButton ID="lbtnSearchStation" ToolTip="Search Station" runat="server" CssClass="btn bg-success btn-sm text-white mr-1" OnClick="lbtnSearchStation_Click">
                                            <i class="fa fa-search"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnResetSearchStation" ToolTip="Reset Search" runat="server" CssClass="btn bg-warning btn-sm text-white" OnClick="lbtnResetSearch_Click">
                                            <i class="fa fa-undo"></i>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <asp:GridView ID="gvStation" runat="server" AutoGenerateColumns="false" CssClass="table table-borderless table-striped table-hover" GridLines="None" AllowPaging="true" PageSize="10"
                                        ShowHeader="false" HeaderStyle-CssClass="thead-light font-weight-bold" OnRowCommand="gvStation_RowCommand" OnRowDataBound="gvStation_RowDataBound" OnPageIndexChanging="gvStation_PageIndexChanging"
                                        DataKeyNames="stonid,stationnameeng,stationnamelocal,statecode,districtcode,citycode,address,busstandflag,busstandstatus,office_id,latitude,longittude,status,office,state,district,city,deleteyn">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>

                                                    <div class="card p-1 mb-0">
                                                        <div class="row p-2">
                                                            <div class="col">
                                                                <h3 class="text-uppercase mb-0 font-weight-bold text-sm"><%# Eval("stationnameeng") %></h3>
                                                                <h5 class="font-weight-normal mb-0">Bus Stand- <%#Eval("busstandstatus") %></h5>
                                                                <h5 class="font-weight-normal">(<%#Eval("state") %>, <%#Eval("district") %>)</h5>
                                                            </div>

                                                            <div class="col-right">
                                                                <asp:LinkButton ID="lbtnUpdateStation" Visible="true" runat="server" CommandName="UpdateStation" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="btn btn-sm btn-default" Style="border-radius: 4px;" ToolTip="Update Station"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnAddStationDistance" Visible="true" runat="server" CommandName="addstationdistance" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="btn btn-sm btn-info" Style="border-radius: 4px;" ToolTip="Add/Update Station Distance"> <i class="fa fa-plus"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnActivate" Visible="false" runat="server" CommandName="activate" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="btn btn-sm btn-success" Style="border-radius: 4px;" ToolTip="Activate Station"> <i class="fa fa-check"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnDiscontinue" Visible="false" runat="server" CommandName="deactivate" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="btn btn-sm btn-warning" Style="border-radius: 4px;" ToolTip="Deactivate Station"> <i class="fa fa-ban"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnDeleteStation" Visible="false" runat="server" CommandName="deleteStation" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="btn btn-sm btn-danger" Style="border-radius: 4px;" ToolTip="Delete Station"> <i class="fa fa-trash"></i></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <p style="color: red; text-align: center; font-weight: bold; font-size: 12pt;">No Records Found.</p>
                                        </EmptyDataTemplate>
                                        <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                    </asp:GridView>
                                    <asp:Panel runat="server" ID="pnlNoRecord" Visible="true" CssClass="text-center" Width="100%">
                                        <p class="text-center" style="font-size: 32px; font-weight: bold; color: #e3e3e3; margin-top: 50px;">
                                            Stations Not Available
                                        </p>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div class="col-xl-8 order-xl-2">
                <asp:Panel runat="server" ID="pnlAddStation" Visible="true">
                    <div class="card" style="min-height: 470px">
                        <div class="card-header border-bottom">
                            <div class="row m-0">
                                <div class="col-md-8 text-left">
                                    <h3 class="mb-0" runat="server" id="lblAddStationHeader" visible="true">Add New Station</h3>
                                </div>
                                <div class="col-md-4 text-right">
                                    <p style="font-size: small; margin: 0px;" class="text-danger font-weight-bold">All marked * Fields are mandatory</p>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row m-0 align-items-center">

                                <div class="row m-0">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblServiceName" runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="Medium">1. Station Name</asp:Label>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="row m-0">
                                            <div class="col-lg-4">
                                                <asp:Label runat="server" CssClass="form-control-label"> English<span style="color: red">*</span></asp:Label>
                                                <asp:TextBox class="form-control form-control-sm" runat="server" ID="tbStationNameEng" MaxLength="50" ToolTip="Station Name in English" autocomplete="off"
                                                    placeholder="Max 50 Characters" Text="" Style=""></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbStationNameEng" ValidChars=" " />
                                            </div>
                                            <div class="col-lg-4">
                                                <asp:Label runat="server" CssClass="form-control-label"> Local Language</asp:Label>
                                                <asp:TextBox class="form-control form-control-sm" runat="server" ID="tbStationNameLocal" MaxLength="50" ToolTip="Station Name in Local Language" autocomplete="off"
                                                    placeholder="Max 50 Characters" Text="" Style=""></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters" TargetControlID="tbStationNameLocal" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-12 mt-2">
                                        <asp:Label ID="Label2" runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="Medium">2. Address</asp:Label>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="row m-0">
                                            <div class="col-lg-4">
                                                <asp:Label runat="server" CssClass="form-control-label"> State<span style="color: red">*</span></asp:Label>
                                                <asp:DropDownList ID="ddlStationState" ToolTip="Station State" CssClass="form-control form-control-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStationState_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-lg-4">
                                                <asp:Label runat="server" CssClass="form-control-label">District<span style="color: red">*</span></asp:Label>
                                                <asp:DropDownList ID="ddlStationDistrict" ToolTip="Station District" CssClass="form-control form-control-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStationDistrict_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-lg-4">
                                                <asp:Label runat="server" CssClass="form-control-label">City<span style="color: red">*</span></asp:Label>
                                                <asp:DropDownList ID="ddlStationCity" ToolTip="Station City" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="row m-0">
                                            <div class="col-lg-8">
                                                <asp:Label runat="server" CssClass="form-control-label"> Address<span style="color: red">*</span></asp:Label>
                                                <asp:TextBox class="form-control form-control-sm" runat="server" ID="tbAddress" MaxLength="200" ToolTip="Address" autocomplete="off" TextMode="MultiLine" Height="60px"
                                                    placeholder="Max 200 Characters" Text="" Style="resize: none"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbAddress" ValidChars=" ./-&" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-12 mt-2">
                                        <asp:Label ID="Label3" runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="Medium">3. Bus Stand</asp:Label>
                                        <asp:CheckBox ID="cbStationBusStandYN" OnCheckedChanged="cbStationBusStandYN_CheckedChange" ToolTip="If Bus Stand Yes/No" runat="server" AutoPostBack="true" />
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="row m-0">
                                            <div class="col-lg-4" id="divOffice" runat="server" visible="false">
                                                <asp:Label runat="server" CssClass="form-control-label">Office<span style="color: red">*</span></asp:Label>
                                                <asp:DropDownList ID="ddlStationOffice" ToolTip="Station Office" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-12 mt-2">
                                        <asp:Label ID="Label4" runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="Medium">4. Other Details</asp:Label>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="row m-0">
                                            <div class="col-lg-4">
                                                <asp:Label runat="server" CssClass="form-control-label"> Latitude</asp:Label>
                                                <asp:TextBox class="form-control form-control-sm" runat="server" ID="tbStationLatitude" MaxLength="10" ToolTip="Station Latitude" autocomplete="off"
                                                    placeholder="Max 10 Digits" Text="" Style=""></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="tbStationLatitude" />
                                            </div>
                                            <div class="col-lg-4">
                                                <asp:Label runat="server" CssClass="form-control-label">Longitude</asp:Label>
                                                <asp:TextBox class="form-control form-control-sm" runat="server" ID="tbStationLongitude" MaxLength="10" ToolTip="Station Longitude" autocomplete="off"
                                                    placeholder="Max 10 Digits" Text="" Style=""></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="tbStationLongitude" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-12 mt-3 text-center">

                                        <asp:LinkButton ID="lbtnUpdate" runat="server" OnClick="updateStation_Click" class="btn btn-success" Visible="false" ToolTip="Update Station">
                                    <i class="fa fa-save"></i>&nbsp; Update</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnSave" Visible="true" OnClick="saveStation_Click" runat="server" class="btn btn-success" ToolTip="Save Station">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>

                                        <asp:LinkButton ID="lbtnReset" OnClick="resetStationData_Click" runat="server" CssClass="btn btn-danger" ToolTip="Reset Station">
                                    <i class="fa fa-undo"></i>&nbsp; Reset</asp:LinkButton>
                                        <asp:LinkButton ID="lbtncancel" OnClick="lbtncancel_Click" Visible="false" runat="server" class="btn btn-warning" ToolTip="Cancel Add Station">
                                    <i class="fa fa-times"></i>&nbsp; Cancel</asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <asp:Panel ID="pnlAddStationDistance" runat="server" Visible="false" Style="box-shadow: 2px 4px 15px 15px #596166c2; width: 98%; margin-left: auto; margin-right: auto;">
                <div class="card card-stats mb-3">
                    <div class="card-header border-bottom text-center">
                        <div class="row col-auto mt--4 float-right" style="margin-right: -2.5rem !important;">
                            <asp:LinkButton ID="lbtnClose" OnClick="lbtnClose_Click" Style="border-radius: 25px" runat="server" CssClass="btn btn-sm  btn-danger"><i class="fa fa-times"></i></asp:LinkButton>
                        </div>
                        <h2 class="mb-0">Station Distance Details for
							<asp:Label runat="server" ID="lblSDStationName"></asp:Label></h2>
                    </div>
                    <div class="card-body pt-0">
                        <div class="row">
                            <div class="col-xl-6 order-xl-1">
                                <div class="card" style="min-height: 470px">
                                    <div class="card-header border-bottom">
                                        <div class="float-left">
                                            <h3 class="mb-0">Station Distance List</h3>
                                        </div>
                                        <div class="float-right">
                                            <div class="input-group">
                                                <asp:TextBox ID="tbSDSearch" ToolTip="Enter Station" CssClass="form-control form-control-sm mr-1" MaxLength="20" runat="server" Width="200px" placeholder="Search Station" autocomplete="off"></asp:TextBox>
                                                <asp:LinkButton ID="lbtnSearchSD" ToolTip="Search Station" runat="server" CssClass="btn bg-success btn-sm text-white mr-1" OnClick="lbtnSearchSD_Click">
                                            <i class="fa fa-search"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnResetSDSearch" ToolTip="Reset Search" runat="server" CssClass="btn bg-warning btn-sm text-white" OnClick="lbtnResetSDSearch_Click">
                                            <i class="fa fa-undo"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-body p-0">
                                        <div class="row m-0 align-items-center">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="gvStationDistance" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover" GridLines="None" OnRowDataBound="gvStationDistance_RowDataBound"
                                                    HeaderStyle-CssClass="thead-light font-weight-bold" OnRowCommand="gvStationDistance_RowCommand" OnPageIndexChanging="gvStationDistance_PageIndexChanging" AllowPaging="true" PageSize="10"
                                                    DataKeyNames="stdtid,frstate_id,frston_id,tostateid,tostonid,frstatehillkm,tostatehillkm,frstateplainkm,tostateplainkm,totdistancekm,halttimemin,tollchargesingle,
									tollchargedouble,parkingchargesingle,parkingchargedouble,dhabahalttime,dhabaid,status,fromstate,tostate,fromstation,tostation">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="From Station" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFromStation" ToolTip="Station" CssClass="form-control-label" runat="server" Text='<%#Eval("fromstation") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To Station" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblToStation" ToolTip="Station" CssClass="form-control-label" runat="server" Text='<%#Eval("tostation") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total KM" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAcSurcharge" ToolTip="AC surcharges" CssClass="form-control-label" runat="server" Text='<%#Eval("totdistancekm") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnUpdateStation" Visible="true" runat="server" CommandName="UpdateStationDistance" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="btn btn-sm btn-default" Style="border-radius: 4px;" ToolTip="Update Station"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnDeleteStation" Visible="true" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="deleteStationDistance" CssClass="btn btn-sm btn-danger" Style="border-radius: 4px;" ToolTip="Delete Station"> <i class="fa fa-trash"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <p style="color: red; text-align: center; font-weight: bold; font-size: 12pt;">No Records Found.</p>
                                                    </EmptyDataTemplate>
                                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                                </asp:GridView>
                                                <asp:Panel runat="server" ID="pnlNoStationDist" Visible="true" CssClass="text-center" Width="100%">
                                                    <p class="text-center" style="font-size: 32px; font-weight: bold; color: #e3e3e3; margin-top: 50px;">
                                                        Station Distance not added for the selected station.
                                                    </p>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-6 order-xl-2">
                                <div class="card" style="min-height: 470px">
                                    <div class="card-header border-bottom">
                                        <div class="row m-0">
                                            <div class="col text-left">
                                                <h3 class="mb-0" runat="server" id="lblAddSDHeader" visible="true">Add Station Distance</h3>
                                            </div>
                                            <div class="col-auto text-right">
                                                <p style="font-size: small; margin: 0px;" class="text-danger font-weight-bold">All marked * Fields are mandatory</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="row m-0 align-items-center">

                                            <div class="row m-0">
                                                <div class="col-lg-12">
                                                    <asp:Label ID="Label5" runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="Medium">1. From </asp:Label>
                                                </div>
                                                <div class="col-lg-12">
                                                    <div class="row m-0">
                                                        <div class="col-lg-6">
                                                            <asp:Label runat="server" CssClass="form-control-label"> State<span style="color: red">*</span></asp:Label>
                                                            <asp:DropDownList ID="ddlSDFromState" ToolTip="From State" Enabled="false" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <asp:Label runat="server" CssClass="form-control-label"> Station<span style="color: red">*</span></asp:Label>
                                                            <asp:DropDownList ID="ddlSDFromStation" ToolTip="From Station" Enabled="false" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12 mt-2">
                                                    <asp:Label ID="Label6" runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="Medium">2. To</asp:Label>
                                                </div>
                                                <div class="col-lg-12">
                                                    <div class="row m-0">
                                                        <div class="col-lg-6">
                                                            <asp:Label runat="server" CssClass="form-control-label"> State<span style="color: red">*</span></asp:Label>
                                                            <asp:DropDownList ID="ddlSDToState" ToolTip="To State" CssClass="form-control form-control-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSDToState_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <asp:Label runat="server" CssClass="form-control-label"> Station<span style="color: red">*</span></asp:Label>
                                                            <asp:DropDownList ID="ddlSDToStation" ToolTip="To Station" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12 mt-2">
                                                    <asp:Label ID="Label7" runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="Medium">3. Distance(KM)</asp:Label>
                                                </div>
                                                <div class="col-lg-12">
                                                    <div class="row m-0">
                                                        <div class="col-lg-6">
                                                            <div class="row m-0">
                                                                <div class="col-lg-12 mt-2">
                                                                    <asp:Label ID="Label9" runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="11pt">From State</asp:Label>
                                                                </div>
                                                                <div class="col-lg-6">
                                                                    <asp:Label runat="server" CssClass="form-control-label"> Hill<span style="color: red">*</span></asp:Label>
                                                                    <asp:TextBox ID="tbSDFromHillDist" onKeyUp="txtOnKeyPress();" ToolTip="From State Hill Distance" CssClass="form-control form-control-sm" MaxLength="5" placeholder="Max 5 Digits" runat="server" autocomplete="off"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="tbSDFromHillDist" />
                                                                </div>
                                                                <div class="col-lg-6">
                                                                    <asp:Label runat="server" CssClass="form-control-label"> Plain<span style="color: red">*</span></asp:Label>
                                                                    <asp:TextBox ID="tbSDFromPlainDist" onKeyUp="txtOnKeyPress();" ToolTip="From State Plain Distance" CssClass="form-control form-control-sm" MaxLength="5" placeholder="Max 5 Digits" runat="server" autocomplete="off"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="tbSDFromPlainDist" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <div class="row m-0">
                                                                <div class="col-lg-12 mt-2">
                                                                    <asp:Label ID="Label10" runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="11pt">To State</asp:Label>
                                                                </div>
                                                                <div class="col-lg-4">
                                                                    <asp:Label runat="server" CssClass="form-control-label"> Hill<span style="color: red">*</span></asp:Label>
                                                                    <asp:TextBox ID="tbSDToHillDist" onKeyUp="txtOnKeyPress();" ToolTip="To State Hill Distance" CssClass="form-control form-control-sm" MaxLength="5" placeholder="Max 5 Digits" runat="server" autocomplete="off"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="tbSDToHillDist" />
                                                                </div>
                                                                <div class="col-lg-4">
                                                                    <asp:Label runat="server" CssClass="form-control-label"> Plain<span style="color: red">*</span></asp:Label>
                                                                    <asp:TextBox ID="tbSDToPlainDist" onKeyUp="txtOnKeyPress();" ToolTip="To State Plain Distance" CssClass="form-control form-control-sm" MaxLength="5" placeholder="Max 5 Digits" runat="server" autocomplete="off"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="tbSDToPlainDist" />
                                                                </div>
                                                                <div class="col-lg-4">
                                                                    <asp:Label runat="server" CssClass="form-control-label"> Total Km</asp:Label>
                                                                    <asp:TextBox ID="txtTotalkm" Enabled="false" onKeyUp="txtOnKeyPress();" ToolTip="To State Plain Distance" CssClass="form-control form-control-sm" MaxLength="5" placeholder="Max 5 Digits" runat="server" autocomplete="off"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12 mt-2">
                                                    <asp:Label ID="Label8" runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="Medium">4. Charges(₹)</asp:Label>
                                                </div>
                                                <div class="col-lg-12">
                                                    <div class="row m-0">
                                                        <div class="col-lg-6">
                                                            <div class="row m-0">
                                                                <div class="col-lg-12 mt-2">
                                                                    <asp:Label ID="Label11" runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="11pt">Toll</asp:Label>
                                                                </div>
                                                                <div class="col-lg-6">
                                                                    <asp:Label runat="server" CssClass="form-control-label"> Single<span style="color: red">*</span></asp:Label>
                                                                    <asp:TextBox class="form-control form-control-sm" runat="server" ID="tbSDTollSingle" MaxLength="6" ToolTip="Toll Charge Single" autocomplete="off"
                                                                        placeholder="Max 6 Digits" Text="" Style=""></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="tbSDTollSingle" />
                                                                </div>
                                                                <div class="col-lg-6">
                                                                    <asp:Label runat="server" CssClass="form-control-label"> Double<span style="color: red">*</span></asp:Label>
                                                                    <asp:TextBox class="form-control form-control-sm" runat="server" ID="tbSDTollDouble" MaxLength="6" ToolTip="Toll Charge Double" autocomplete="off"
                                                                        placeholder="Max 6 Digits" Text="" Style=""></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="tbSDTollDouble" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <div class="row m-0">
                                                                <div class="col-lg-12 mt-2">
                                                                    <asp:Label ID="Label12" runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="11pt">Parking</asp:Label>
                                                                </div>
                                                                <div class="col-lg-6">
                                                                    <asp:Label runat="server" CssClass="form-control-label"> Single<span style="color: red">*</span></asp:Label>
                                                                    <asp:TextBox class="form-control form-control-sm" runat="server" ID="tbSDParkingSingle" MaxLength="6" ToolTip="Parking Charge Single" autocomplete="off"
                                                                        placeholder="Max 6 Digits" Text="" Style=""></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="tbSDParkingSingle" />
                                                                </div>
                                                                <div class="col-lg-6">
                                                                    <asp:Label runat="server" CssClass="form-control-label"> Double<span style="color: red">*</span></asp:Label>
                                                                    <asp:TextBox class="form-control form-control-sm" runat="server" ID="tbSDParkingDouble" MaxLength="6" ToolTip="Parking Charge Double" autocomplete="off"
                                                                        placeholder="Max 6 Digits" Text="" Style=""></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="tbSDParkingDouble" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12 mt-2">
                                                    <asp:Label ID="Label13" runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="Medium">5. Halt Time</asp:Label><span style="color: red">*</span>
                                                </div>
                                                <div class="col-lg-12">
                                                    <div class="row m-0">
                                                        <div class="col-lg-6">
                                                            <div class="row m-0">
                                                                <div class="col-lg-6">
                                                                    <asp:TextBox class="form-control form-control-sm" runat="server" ID="tbSDHaltTime" MaxLength="5" ToolTip="Halt Time" autocomplete="off"
                                                                        placeholder="Max 5 Digits" Text="" Style=""></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="tbSDHaltTime" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12 mt-3 text-center">
                                                    <asp:LinkButton ID="lbtnSDUpdate" runat="server" OnClick="updateStationDistance_Click" class="btn btn-success" Visible="false" ToolTip="Update Station Distance">
                                    <i class="fa fa-save"></i>&nbsp; Update</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnSDSave" Visible="true" OnClick="saveStationDistance_Click" runat="server" class="btn btn-success" ToolTip="Save Station Distance">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnSDReset" OnClick="resetStationDistData_Click" runat="server" CssClass="btn btn-danger" ToolTip="Reset Station Distance">
                                    <i class="fa fa-undo"></i>&nbsp; Reset</asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
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

