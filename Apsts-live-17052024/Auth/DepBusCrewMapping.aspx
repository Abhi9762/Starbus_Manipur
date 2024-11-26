<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Depotmaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="DepBusCrewMapping.aspx.cs" Inherits="Auth_DepBusBusCrewMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function SearchEmployees(txtSearch, chkBoxDrivers) {
            if ($(txtSearch).val() != "") {
                var count = 0;
                $(chkBoxDrivers).children('tbody').children('tr').each(function () {
                    var match = false;
                    $(this).children('td').children('label').each(function () {
                        if ($(this).text().toUpperCase().indexOf($(txtSearch).val().toUpperCase()) > -1)
                            match = true;
                    });
                    if (match) {
                        $(this).show();
                        count++;
                    }
                    else { $(this).hide(); }
                });
                $('#spnCount').html((count) + ' match');
            }
            else {
                $(chkBoxDrivers).children('tbody').children('tr').each(function () {
                    $(this).show();
                });
                $('#spnCount').html('');
            }
        }
    </script>
    <style>
        input[type=checkbox], input[type=radio] {
            margin: 5px 10px 0px;
            box-sizing: border-box;
            padding: 0;
            width: 15px;
            height: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="header pb-7">
    </div>
    <div class="container-fluid mt--6">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="row">
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-md-12">
                                        <h4 class="mb-1">
                                            <asp:Label ID="lblSummary" runat="server" CssClass="text-capitalize">Summary as on Date 01/12/2021 04:00pm</asp:Label></h4>
                                        <div class="row m-0">
                                            <div class="col-md-6 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Total Buses
                                              <asp:LinkButton runat="server" ID="lbtnDownloadBuses" OnClick="lbtnDownloadBuses_Click" Font-Underline="true" CssClass="mt-1 pl-1 text-warning text-right float-right"><i class="fa fa-download"></i></asp:LinkButton>

                                                            <asp:Label ID="lblTotalBuses" runat="server" data-toggle="tooltip" data-placement="bottom" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Total Driver
                                                           <asp:LinkButton runat="server" ID="lbtnDownloadDriver" OnClick="lbtnDownloadDriver_Click" Font-Underline="true" CssClass="mt-1 pl-1 text-warning text-right float-right"><i class="fa fa-download"></i></asp:LinkButton>

                                                            <asp:Label ID="lblTotalDriver" runat="server" data-toggle="tooltip" data-placement="bottom" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6">
                                                <div class="row m-0">
                                                    <div class="col-md-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Total Conductor &nbsp;
                                                                            <asp:LinkButton runat="server" ID="lbtnDownloadConductor" OnClick="lbtnDownloadConductor_Click" Font-Underline="true" CssClass="mt-1 pl-1 text-warning text-right float-right"><i class="fa fa-download"></i></asp:LinkButton>

                                                            <asp:Label ID="lblTotalConductor" runat="server" data-toggle="tooltip" data-placement="bottom" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Total Mapped Driver 
                                                                       <asp:LinkButton runat="server" ID="lbtnDownloadMappedDriver" OnClick="lbtnDownloadMappedDriver_Click" Font-Underline="true" CssClass="mt-1 pl-1 text-warning text-right float-right"><i class="fa fa-download"></i></asp:LinkButton>

                                                            <asp:Label ID="lblTotalMappedDriver" runat="server" data-toggle="tooltip" data-placement="bottom" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 border-right">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col">
                                        <div>
                                            <h4 class="mb-1">Depot</h4>
                                        </div>

                                        <div class="input-group mt-2">
                                            <div class="input-group-prepend pr-2" style="width: 60%">
                                                <asp:DropDownList ID="ddlDepot" data-toggle="tooltip" OnSelectedIndexChanged="ddlDepot_SelectedIndexChanged" data-placement="bottom" CssClass="form-control form-control-sm" runat="server">
                                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                            <asp:LinkButton ID="lbtndownload" data-toggle="tooltip" data-placement="bottom" title="Download" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white">
                                            <i class="fa fa-download"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="card-body">
                                <div class="row mr-0">
                                    <div class="col">
                                        <div>
                                            <h4 class="mb-1">Instructions</h4>
                                        </div>
                                        <ul class="data-list" data-autoscroll>
                                            <li>
                                                <asp:Label runat="server" CssClass="form-control-label">• Select depot for which you want to map crew with bus.</asp:Label><br />
                                            </li>
                                            <li>
                                                <asp:Label runat="server" CssClass="form-control-label">• Crew can be mapped with only one bus at a time.</asp:Label><br />
                                            </li>
                                            <li>
                                                <asp:Label runat="server" CssClass="form-control-label">• To map crew click on view/update button, select driver or conductor you want to map and click on yes button.</asp:Label><br />
                                            </li>
                                            <li>
                                                <asp:Label runat="server" CssClass="form-control-label">• To remove mapped crew ,click on delete button and crew will delete from the mapped crew list.</asp:Label>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-auto">
                                        <asp:LinkButton ID="lbtnview" OnClick="lbtnview_Click" data-toggle="tooltip" data-placement="bottom" title="View Instructions" runat="server" Width="30px" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" class="btn btn bg-gradient-green btn-sm text-white" Width="30px" data-toggle="tooltip" data-placement="bottom" title="Click To View Help Document">
                                            <i class="ni ni-archive-2"></i>
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
            <div class="col-lg-12">
                <div class="card mb-3" style="min-height: 400px;">

                    <asp:Panel ID="pnlAddDepotService" runat="server">
                        <div class="col-lg-12">
                            <asp:Panel ID="pnlNoRecord" runat="server" Width="100%" Visible="true">
                                <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                    <div class="col-md-12" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
                                        Bus not available<br />
                                        Please add Bus in Bus Registration Module
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:GridView ID="gvBuslist" runat="server" HeaderStyle-CssClass="thead-light font-weight-bold" OnRowCommand="gvBuslist_RowCommand" AutoGenerateColumns="false" GridLines="None" ClientIDMode="Static"
                                AllowPaging="false" PageSize="10" CssClass="table table-hover table-striped" DataKeyNames="busno,depotcode,driver,conductor,totaldriver,totalconductor">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">

                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BUS No.">
                                        <ItemTemplate>
                                            <%#Eval("busno") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mapped Driver">
                                        <ItemTemplate>
                                            <%#Eval("driver") %>
                                            <asp:Label ID="lblgvLabrdlDriver" runat="server" Text='<%#Eval("totaldriver") %>' Style="border: 1px solid gray; border-radius: 5px; padding: 1px 8px; font-weight: bold;"></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mapped Conductor">
                                        <ItemTemplate>
                                            <%#Eval("conductor") %>

                                            <asp:Label ID="lblgvLabrdlConductor" runat="server" Text='<%#Eval("totalconductor") %>' Style="border: 1px solid gray; border-radius: 5px; padding: 1px 8px; font-weight: bold;"></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnmapBus" CommandName="mapBus" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                Visible="true" runat="server" CssClass="btn btn-sm btn-success btn-block" Style="border-radius: 4px; font-size: 10pt; width: 125px;"
                                                ToolTip="View/Update Crew"><i class="fa fa-check"></i>   View/Update</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                            </asp:GridView>
                        </div>

                    </asp:Panel>
                    <asp:Panel ID="pnlMapBus" Visible="false" Style="box-shadow: 2px 4px 15px 15px #596166c2;" runat="server">
                        <div class="card-header border-bottom text-center">
                            <div class="row col-auto mt--4 float-right" style="margin-right: -2.5rem !important;">
                                <asp:LinkButton ID="lbtnCancel" OnClick="lbtnCancel_Click" Style="border-radius: 25px" runat="server" CssClass="btn btn-sm  btn-danger"><i class="fa fa-times"></i></asp:LinkButton>
                            </div>
                            <h2 class="mb-0">Tentative Mapping of Crew with Bus No "<asp:Label runat="server" ID="lblRouteName"></asp:Label>"</h2>
                        </div>
                        <div class="row m-0">
                            <div class="col-lg-8 mb-3">
                                <div class="card">
                                    <div class="card-body px-0 pt-0" style="border: 2px solid #edecec; border-radius: 4px; min-height: 800px; background: #faf9f9;">
                                        <div class="row m-0 mt-2">
                                            <div class="col-md-6 ml-2">
                                                <div class="row m-0">
                                                    <div class="col-md-2 mt-2 ml-2">
                                                        <h4 class="mb-1">List of</h4>
                                                    </div>
                                                    <div class="col-md-4 mt-1 text-left">
                                                        <asp:DropDownList ID="ddlEmpType" runat="server" OnSelectedIndexChanged="ddlEmpType_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control form-control-sm">
                                                            <asp:ListItem Text="Driver" Value="2" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Conductor" Value="3"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-5 mt-1 text-right">
                                                <asp:TextBox ID="tbSearch" runat="server" onkeyup="SearchEmployees(this,'#chkBoxDrivers');" CssClass="form-control form-control-sm"
                                                    placeholder="Search Employee" Style="width: 200px; float: left;" autocomplete="off">
                                                </asp:TextBox>
                                                <span id="spnCount"></span>
                                            </div>
                                        </div>
                                        <div class="row ml-4 m-0">
                                            <div class="col-md-12  mt-3">
                                                <div class="row">
                                                    <asp:Repeater runat="server" ID="rptemplist" OnItemCommand="rptemplist_ItemCommand">
                                                        <ItemTemplate>
                                                            <div class="col-6 mt-2 ">
                                                                <asp:LinkButton runat="server" ToolTip="Add Bus" CommandName="ADDDRIVER" CommandArgument='<%# Eval("empcode") %>' CssClass="btn-sm btn-success"><i class="fa fa-plus"></i></asp:LinkButton>

                                                                <asp:Label runat="server" CssClass="text-uppercase" Text='<%# Eval("empname") %>'></asp:Label>

                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                                
                                                <h4 runat="server" id="lblNoCrew" visible="false" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 26px; font-weight: bold; text-align: center">No Crew is available for mapping with this Bus</h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4 mb-3">
                                <div class="card" style="font-size: 12px; min-height: 800px; background: #faf9f9;">
                                    <div class="card-body px-0 pt-0" style="border: 2px solid #edecec; border-radius: 4px;">
                                        <div class="row m-0 mt-2 ml-2">
                                            <div class="col-md-12 mt-2 ml-2">
                                                <h4 class="mb-1">List of Mapped Crew</h4>

                                            </div>
                                            <div class="col-md-12 mt-3">
                                                <asp:GridView ID="gvMappedDrivers" HeaderStyle-CssClass="thead-light font-weight-bold" OnRowCommand="gvMappedDrivers_RowCommand" runat="server" AutoGenerateColumns="false" ShowHeader="false" GridLines="None" Font-Size="14px"
                                                    AllowPaging="false" PageSize="10" CssClass="table table-hover table-striped" DataKeyNames="empcode,empname,emptype,busno">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <span style="font-size: 12px; font-weight: bold;"><%# Eval("emptype").ToString()=="C" ? "CONDUCTOR":"DRIVER" %>
                                                                </span>
                                                                <br />
                                                                <%#Eval("empname") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnmapBus" CommandName="deleteBus" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                    Visible="true" runat="server" CssClass="btn btn-sm btn-danger" Style="border-radius: 4px; font-size: 10pt;"
                                                                    ToolTip="Remove Mapped Crew"> <i class="fa fa-minus"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                                </asp:GridView>
                                                <h4 runat="server" id="lblNoData" visible="false" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 26px; font-weight: bold; text-align: center">Crew Not Mapped with this BUS</h4>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
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
                        <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnYesConfirmation" OnClick="lbtnYesConfirmation_Click" runat="server" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                            <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button4" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id*=gvBuslist]").DataTable(
                {
                    dom: 'lBfrtip<"actions">',
                    bLengthChange: true,
                    lengthMenu: [[20, 30, -1], [20, 30, "All"]],
                    bFilter: true,
                    bSort: true,
                    bPaginate: true,
                    buttons: [
                        'excel', 'pdf', 'print'
                    ]
                });
        });
    </script>

</asp:Content>

