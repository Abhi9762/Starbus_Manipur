<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Depotmaster.master" AutoEventWireup="true" CodeFile="DepotServiceCancel.aspx.cs" Inherits="Auth_DepotServiceCancel" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            var todayDate = new Date().getDate();
            var endD = new Date(new Date().setDate(todayDate + 30));
            var currDate = new Date();

            $('[id*=txtblockdate]').datepicker({
                startDate: "dateToday",
                endDate: endD,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
 $('[id*=txtrptblockdate]').datepicker({
                endDate: endD,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });


        });
        function close() {
            alert('ok');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="header pb-3">
    </div>
    <div class="container-fluid mt-1">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="row m-0">
                        <div class="col-3 border-right">
                            <div class="card-body">
                                <div class="row">
                                    <h4 class="mb-1">
                                        <asp:Label ID="lblSummary" runat="server" CssClass="text-capitalize">Summary as on Date 01/12/2021 04:00pm</asp:Label></h4>
                                    <div class="col-md-6">
                                        <asp:Label runat="server" CssClass="form-control-label">Total Services </asp:Label>

                                    </div>
                                    <div class="col-md-3 text-right">
                                        <asp:Label ID="lbltotservices" runat="server" CssClass="form-control-label" Font-Bold="true" Style="color: red;"><span style="color: red"></span></asp:Label>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col">
                                        <div>
                                            <h4 class="mb-1">Generate Block Services Report</h4>
                                        </div>
                                        <div class="input-group mb-3">
                                            <div class="input-group pr-2">
                                                <asp:TextBox class="form-control form-control-sm" runat="server" ID="txtrptblockdate" MaxLength="10" autocomplete="off"
                                                    placeholder="DD/MM/YYYYY" Text=""></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom"
                                                    TargetControlID="txtrptblockdate" ValidChars="/" />
                                                <asp:DropDownList ID="ddlrptservicetype" CssClass="form-control form-control-sm" runat="server">
                                                </asp:DropDownList>

                                                <asp:LinkButton ID="lbtnDownloadBlockServiceRpt" data-toggle="tooltip" data-placement="bottom" title="Download" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white">
                                            <i class="fa fa-download"></i>
                                                </asp:LinkButton>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-5">
                            <div class="card-body">
                                <div class="row mr-0">
                                    <div class="col">
                                        <div>
                                            <h4 class="mb-1">Instructions</h4>
                                        </div>
                                        <ul class="data-list" data-autoscroll>
                                            <li>
                                                <asp:Label runat="server" CssClass="form-control-label"> Check date and service type before blocking.</asp:Label><br />
                                            </li>
                                            <li>
                                                <asp:Label runat="server" CssClass="form-control-label"> Once a service is blocked, that service cannot be unblocked.</asp:Label><br />
                                            </li>
                                            <li>
                                                <asp:Label runat="server" CssClass="form-control-label"> If service tickets are booked online, then their cancellation will be auto.</asp:Label><br />
                                            </li>
                                            <li>
                                                <asp:Label runat="server" CssClass="form-control-label"> If there are booking tickets from the counter or agent, then they will be cancelled only, their refund will be from the respective counter and agent.</asp:Label>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-auto">
                                        <asp:LinkButton ID="lbtnview" OnClick="lbtnview_Click" ToolTip="View Instructions" runat="server" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-eye"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDownload" runat="server" class="btn btn bg-gradient-green btn-sm text-white" ToolTip="Download Instruction">
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
            <div class="col-md-12">
                <div class="card mr-2" style="min-height: 530px">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-lg-2 offset-4">
                                <label>Block Date</label>
                                <asp:TextBox class="form-control form-control-sm" runat="server" ID="txtblockdate" MaxLength="10" autocomplete="off"
                                    placeholder="DD/MM/YYYYY" Text=""></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers,Custom"
                                    TargetControlID="txtblockdate" ValidChars="/" />

                            </div>
                            <div class="col-lg-2">
                                <label>Service Type</label>
                                <asp:DropDownList ID="ddlservicetype" CssClass="form-control form-control-sm" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-3">
                                <label>Service Code</label>
                                <asp:TextBox class="form-control form-control-sm" runat="server" ID="txtservicecode" MaxLength="50"
                                    placeholder="Max 50 Characters" Text=""></asp:TextBox>
                            </div>
                            <div class="col-lg-1 pt-4">
                                <asp:LinkButton ID="lbtnsearch" runat="server" class="btn btn-primary btn-sm" Style="height: 30px;" OnClick="lbtnsearch_Click"
                                    ToolTip="Search Services"><i class="fa fa-search"></i></asp:LinkButton>
                                <asp:LinkButton ID="lbtnDownloadExcel" Visible="true" runat="server" CssClass="btn btn-success btn-sm"
                                    Style="height: 30px;"
                                    ToolTip="Download Services"><i class="fa fa-download"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-4 border-right">
                                <asp:GridView ID="gvDepotServices" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
                                    AllowPaging="true" PageSize="10" OnPageIndexChanging="gvDepotServices_PageIndexChanging" OnRowCommand="gvDepotServices_RowCommand"
                                    DataKeyNames="dsvc_id">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="mb-0">
                                                    <div class="card-body  py-2 px-3">
                                                        <div class="row">
                                                            <div class="col-lg-10">
                                                                <h5 class="text-uppercase mb-0 font-weight-bold text-xs">
                                                                    <%# Eval("service_name") %>
                                                                    <br />
                                                                    <span class="text-gray text-xs">No of Trips <%# Eval("no_of_trips") %></span> |
                                                                    <span class="text-gray text-xs">Online Trips <%# Eval("onlinetrip") %></span>
                                                                </h5>
                                                            </div>
                                                            <div class="col-lg-2">
                                                                <asp:LinkButton ID="lbtnViewTrip" runat="server" CommandName="viewtrips" CssClass="btn btn-sm btn-success" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="View Depot Service Trips" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-eye"></i> Select</asp:LinkButton>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                </asp:GridView>
                                <asp:Panel ID="pnlNoDepotService" runat="server">
                                    <div class="">
                                        <!-- Card body -->
                                        <div class="card-body text-center">
                                            <p class="h2 font-weight-bold text-light mt-4">
                                                Depot Services Not Found
                                                <br />
                                                for selected perameter
                                            </p>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="col-md-7">
                                <asp:GridView ID="gvtrips" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="7" CssClass="table table-hover table-striped"
                                    DataKeyNames="strp_id,dsvc_id,fromstaion,tostation,departtime,arrivaltime,trip_type,total_bookedseats,tripcode,trip_direction" OnRowCommand="gvtrips_RowCommand"
                                    GridLines="None">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Stations">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfromstaion" runat="server" Text='<%# Eval("fromstaion") %>'></asp:Label>
                                                -
                                                 <asp:Label ID="lbltostation" runat="server" Text='<%# Eval("tostation") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Trip Direction">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltrip_type" runat="server" Text='<%# Eval("trip_type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Departure/Arrival Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldeparttime" runat="server" Text='<%# Eval("departtime") %>'></asp:Label>
                                                -
                                                 <asp:Label ID="lblarrivaltime" runat="server" Text='<%# Eval("arrivaltime") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No of Booked Tickets">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltotal_bookedseats" runat="server" Text='<%# Eval("total_bookedseats") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnView" runat="server" CommandName="View" Visible="false" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    CssClass="btn btn-sm btn-warning" Style="font-size: 9pt; border-radius: 6px; margin-top: -6px;"> <i class="fa fa-eye" title="View Tickets"></i> </asp:LinkButton>

                                                <asp:LinkButton ID="btnselect" runat="server" CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    CssClass="btn btn-sm btn-success" Style="font-size: 9pt; border-radius: 6px; margin-top: -6px;"> <i class="fa fa-check" title="Select For Block/Cancel Service"></i> </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                </asp:GridView>
                                <asp:Panel ID="pnlNoTrips" runat="server">
                                    <div class="">
                                        <!-- Card body -->
                                        <div class="card-body text-center">
                                            <p class="h2 font-weight-bold text-light mt-4">
                                                No Trip Available<br />
                                                Please Select Depot service for block/Cancel trip
                                            </p>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
                CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
                <div class="card " style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Please Confirm
                        </h4>
                        <span class="text-muted text-sm"><asp:Label ID="trip" runat="server"></asp:Label></span>
                    </div>
                    <div class="card-body text-left p-4" style="min-height: 100px;">
                        <div class="row">
                            <div class="col-lg-12">
                                <label>Block Reason<span class="text-danger">*</span> </label>
                                <asp:DropDownList ID="ddlreason" CssClass="form-control form-control-sm" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                 <label>Remark <span class="text-danger">*</span> </label>
                                <asp:TextBox class="form-control form-control-sm" runat="server" ID="txtremark" MaxLength="50" TextMode="MultiLine" Height="100px"
                                    placeholder="Max 50 Characters" Text=""></asp:TextBox>
                            </div>
                        </div>
                        <div style="width: 100%; margin-top: 20px; text-align: center;">
                             <asp:Label ID="lblConfirmation" runat="server" Text="Do you want Block/Cancel Trip ?"></asp:Label>
                       <br /><br />
                            <asp:LinkButton ID="lbtnYesConfirmation" OnClick="lbtnYesConfirmation_Click" runat="server" CssClass="btn btn-success"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                            <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
				<asp:Button ID="Button4" runat="server" Text="" />
			</div>
            </asp:Panel>
        </div>

    </div>
</asp:Content>

