<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="tripchartdash.aspx.cs" Inherits="Auth_tripchartdash" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            var todayDate = new Date().getDate();
            var endD = new Date(new Date().setDate(todayDate));
            var currDate = new Date();

            $('[id*=txtdate]').datepicker({
                // startDate: "dateToday",
                endDate: endD,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid pt-top ">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="card-header">
            <div class="row">
                <div class="col-12 input-group-prepend">
                    <span>Depot
                    </span>
                    <asp:DropDownList ID="ddldepot" runat="server" Style="height: 30px; font-size: 9pt;"
                        ToolTip="Depot Name" OnSelectedIndexChanged="ddldepot_SelectedIndexChanged" Width="150px" AutoPostBack="true" class="form-control ml-2 mr-4">
                    </asp:DropDownList>
                    <span>Counter
                    </span>
                    <asp:DropDownList ID="ddlcounter" runat="server" Style="height: 30px; font-size: 9pt;"
                        ToolTip="Counter Name" Width="150px" AutoPostBack="false" class="form-control ml-2 mr-4">
                    </asp:DropDownList>
                    <span>Date
                    </span>
                    <asp:TextBox class="form-control  ml-2 mr-4" runat="server" ID="txtdate" MaxLength="10"
                        placeholder="DD/MM/YYYY" Text="" Style="font-size: 9pt; height: 30px; display: inline; width: 100px; padding: 5px;"></asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom"
                        TargetControlID="txtdate" ValidChars="/-" />
                    <asp:LinkButton ID="btnsearch" OnClick="btnsearch_Click" runat="server" CssClass="btn btn-sm btn-success"> <i class="fa fa-search"></i> Search </asp:LinkButton>
                    <asp:LinkButton ID="lbtnreset" OnClick="lbtnreset_Click" runat="server" CssClass="btn btn-sm btn-danger"> <i class="fa fa-refresh"></i> Reset </asp:LinkButton>
                </div>
            </div>
        </div>
        <div class="row mt-3">
            <div class="col-12">
                <asp:Panel runat="server" ID="pnltodayDate" Visible="false">
                    <div class="row">
                        <div class="col-6">
                            <div class="card shadow" style="min-height: 350px">
                                <div class="card-body">

                                    <div class="row">
                                        <div class="col">
                                            <h3>Ready To Generate</h3>
                                        </div>
                                        <div class="col-auto">
                                            <h4>
                                                <asp:Label runat="server" ID="lblreadyTotalcount" CssClass="text-right" Text=""></asp:Label>
                                            </h4>
                                        </div>
                                    </div>

                                    <asp:GridView ID="grdready" ShowHeader="false" OnPageIndexChanging="grdready_PageIndexChanging" OnRowDataBound="grdready_RowDataBound" OnRowCommand="grdready_RowCommand" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                        GridLines="None" DataKeyNames="depo_code,trip_code,busservicetype_code,service_code"
                                        class="table table-hover mb-0" PageSize="4" Font-Size="13px">
                                        <Columns>
                                             <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnproceed" runat="server" CssClass="btn btn-primary btn-sm"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="proceed"
                                                        Style="padding: 1px 6px; border-radius: 5px; font-size: 12px;"><i class="fa fa-arrow-right"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="bntView" runat="server" CssClass="btn btn-success btn-sm"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="view"
                                                        Style="padding: 1px 6px; border-radius: 5px; font-size: 12px;"><i class="fa fa-eye"></i></asp:LinkButton>


                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Depot">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" Font-Bold="true" runat="server" Text="Trip Code"></asp:Label>
                                                    <asp:Label ID="lblTripCode" runat="server" Text='<%#  Eval("trip_code") %>'></asp:Label>

                                                    <asp:Label ID="Label4" Font-Bold="true" CssClass="ml-3" runat="server" Text="Departure"></asp:Label>
                                                    <asp:Label ID="lblDepartureTime" runat="server" Text='<%#  Eval("dept_time") %>'></asp:Label>

                                                    <asp:Label ID="Label5" Font-Bold="true" CssClass="ml-3" runat="server" Text="Service"></asp:Label>
                                                    <asp:Label ID="lblServiceCode" runat="server" Text='<%#  Eval("servicetype_name") %>'></asp:Label>
                                                    <br />
                                                    <asp:Label ID="Label2" Font-Bold="true" runat="server" Text="Station(s)"></asp:Label>


                                                    <asp:Label ID="lblFrom" runat="server" Text='<%#  Eval("fstation_name") %>'></asp:Label>
                                                    -
                                                    <asp:Label ID="lblTo" runat="server" Text='<%#  Eval("tstation_name") %>'></asp:Label>
                                                    <asp:Label ID="Label1" Font-Bold="true" CssClass="ml-3" runat="server" Text="Depot"></asp:Label>

                                                    <asp:Label ID="lblDepot" runat="server" Text='<%#  Eval("depo_name") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" CssClass="gridview" />
                                    </asp:GridView>
                                    <center> <asp:Panel runat="server" ID="pnlReady">
                                 <h1 class="mt-6" style="color:lightgray;font-size:30px">Trip Chart Details <br />Not <br />Available.</h1>
                            </asp:Panel></center>
                                </div>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="card shadow" style="min-height: 350px">
                                <div class="card-body">

                                    <div class="row">
                                        <div class="col">
                                            <h3>Upcoming Trip Chart</h3>
                                        </div>
                                        <div class="col-auto">
                                            <h4>
                                                <asp:Label runat="server" ID="lbltotalCountupcoming" CssClass="text-right" Text=""></asp:Label>
                                            </h4>
                                        </div>
                                    </div>
                                    <asp:GridView ID="grdUpcoming" ShowHeader="false" runat="server" OnPageIndexChanging="grdUpcoming_PageIndexChanging" AutoGenerateColumns="False" AllowPaging="True"
                                        GridLines="None" DataKeyNames="trip_code" OnRowDataBound="grdUpcoming_RowDataBound" OnRowCommand="grdUpcoming_RowCommand"
                                        class="table table-hover mb-0" PageSize="4" Font-Size="13px">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="bntView" runat="server" CssClass="btn btn-success btn-sm"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="view"
                                                        Style="padding: 1px 6px; border-radius: 5px; font-size: 12px;"><i class="fa fa-eye"></i></asp:LinkButton>


                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Depot">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" Font-Bold="true" runat="server" Text="Trip Code"></asp:Label>
                                                    <asp:Label ID="lblTripCode" runat="server" Text='<%#  Eval("trip_code") %>'></asp:Label>

                                                    <asp:Label ID="Label4" Font-Bold="true" CssClass="ml-3" runat="server" Text="Departure"></asp:Label>
                                                    <asp:Label ID="lblDepartureTime" runat="server" Text='<%#  Eval("dept_time") %>'></asp:Label>

                                                    <asp:Label ID="Label5" Font-Bold="true" CssClass="ml-3" runat="server" Text="Service"></asp:Label>
                                                    <asp:Label ID="lblServiceCode" runat="server" Text='<%#  Eval("servicetype_name") %>'></asp:Label>
                                                    <br />
                                                    <asp:Label ID="Label2" Font-Bold="true" runat="server" Text="Station(s)"></asp:Label>


                                                    <asp:Label ID="lblFrom" runat="server" Text='<%#  Eval("fstation_name") %>'></asp:Label>
                                                    -
                                                    <asp:Label ID="lblTo" runat="server" Text='<%#  Eval("tstation_name") %>'></asp:Label>
                                                    <asp:Label ID="Label1" Font-Bold="true" CssClass="ml-3" runat="server" Text="Depot"></asp:Label>

                                                    <asp:Label ID="lblDepot" runat="server" Text='<%#  Eval("depo_name") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" CssClass="gridview" />
                                    </asp:GridView>
                                    <center> <asp:Panel runat="server" ID="pnlUpcoming">
                                 <h1 class="mt-6" style="color:lightgray;font-size:30px">Trip Chart Details <br />Not <br />Available.</h1>
                            </asp:Panel></center>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>

            <div class="col-6">
                <div class="card shadow" style="min-height: 350px">
                    <div class="card-body">

                        <div class="row">
                            <div class="col">
                                <h3>Prepared Trip Chart</h3>
                            </div>
                            <div class="col-auto">
                                <h4>
                                    <asp:Label runat="server" ID="lbltotalcountprepared" CssClass="text-right" Text=""></asp:Label>
                                </h4>
                            </div>
                        </div>
                        <asp:GridView ID="grdPepared" ShowHeader="false" OnPageIndexChanging="grdPepared_PageIndexChanging" OnRowCommand="grdPepared_RowCommand" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            GridLines="None" DataKeyNames="trip_code"
                            class="table table-hover mb-0" PageSize="4" Font-Size="13px">
                            <Columns>
                                   <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnproceed" runat="server" CssClass="btn btn-success btn-sm"
                                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="VIEWDETAIL"
                                            Style="padding: 1px 6px; border-radius: 5px; font-size: 12px;"><i class="fa fa-eye"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Depot">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" Font-Bold="true" runat="server" Text="Trip Code"></asp:Label>
                                        <asp:Label ID="lblTripCode" runat="server" Text='<%#  Eval("trip_code") %>'></asp:Label>

                                        <asp:Label ID="Label4" Font-Bold="true" CssClass="ml-3" runat="server" Text="Departure"></asp:Label>
                                        <asp:Label ID="lblDepartureTime" runat="server" Text='<%#  Eval("dept_time") %>'></asp:Label>

                                        <asp:Label ID="Label5" Font-Bold="true" CssClass="ml-3" runat="server" Text="Service"></asp:Label>
                                        <asp:Label ID="lblServiceCode" runat="server" Text='<%#  Eval("servicetype_name") %>'></asp:Label>
                                        <br />
                                        <asp:Label ID="Label2" Font-Bold="true" runat="server" Text="Station(s)"></asp:Label>


                                        <asp:Label ID="lblFrom" runat="server" Text='<%#  Eval("fstation_name") %>'></asp:Label>
                                        -
                                                    <asp:Label ID="lblTo" runat="server" Text='<%#  Eval("tstation_name") %>'></asp:Label>
                                        <asp:Label ID="Label1" Font-Bold="true" CssClass="ml-3" runat="server" Text="Depot"></asp:Label>

                                        <asp:Label ID="lblDepot" runat="server" Text='<%#  Eval("depo_name") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                             
                            </Columns>
                            <PagerStyle HorizontalAlign="Left" CssClass="gridview" />
                        </asp:GridView>
                        <center>
                              <asp:Panel runat="server" ID="pnlPrepared">
                                 <h1 class="mt-6" style="color:lightgray;font-size:30px">Trip Chart Details <br />Not <br />Available.</h1>
                            </asp:Panel></center>
                    </div>
                </div>
            </div>
            <div class="col-6">
                <div class="card shadow" style="min-height: 350px">
                    <div class="card-body">

                        <div class="row">
                            <div class="col">
                                <h3>Unprepared Trip Chart</h3>
                            </div>
                            <div class="col-auto">
                                <h4>
                                    <asp:Label runat="server" ID="lbltotalcountunprepared" CssClass="text-right" Text=""></asp:Label>
                                </h4>
                            </div>
                        </div>
                        <asp:GridView ID="grdUnprepared" ShowHeader="false" OnPageIndexChanging="grdUnprepared_PageIndexChanging" OnRowCommand="grdUnprepared_RowCommand" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            GridLines="None" DataKeyNames="depo_code,trip_code,busservicetype_code,service_code"
                            class="table table-hover mb-0" PageSize="4" Font-Size="13px">
                            <Columns>
                                 <asp:TemplateField>
                                    <ItemTemplate>

                                        <asp:LinkButton ID="btnproceed" runat="server" CssClass="btn btn-success btn-sm"
                                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="VIEWDETAIL"
                                            Style="padding: 1px 6px; border-radius: 5px; font-size: 12px;"><i class="fa fa-eye"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Depot">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" Font-Bold="true" runat="server" Text="Trip Code"></asp:Label>
                                        <asp:Label ID="lblTripCode" runat="server" Text='<%#  Eval("trip_code") %>'></asp:Label>

                                        <asp:Label ID="Label4" Font-Bold="true" CssClass="ml-3" runat="server" Text="Departure"></asp:Label>
                                        <asp:Label ID="lblDepartureTime" runat="server" Text='<%#  Eval("dept_time") %>'></asp:Label>

                                        <asp:Label ID="Label5" Font-Bold="true" CssClass="ml-3" runat="server" Text="Service"></asp:Label>
                                        <asp:Label ID="lblServiceCode" runat="server" Text='<%#  Eval("servicetype_name") %>'></asp:Label>
                                        <br />
                                        <asp:Label ID="Label2" Font-Bold="true" runat="server" Text="Station(s)"></asp:Label>


                                        <asp:Label ID="lblFrom" runat="server" Text='<%#  Eval("fstation_name") %>'></asp:Label>
                                        -
                                                    <asp:Label ID="lblTo" runat="server" Text='<%#  Eval("tstation_name") %>'></asp:Label>
                                        <asp:Label ID="Label1" Font-Bold="true" CssClass="ml-3" runat="server" Text="Depot"></asp:Label>

                                        <asp:Label ID="lblDepot" runat="server" Text='<%#  Eval("depo_name") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                            </Columns>
                            <PagerStyle HorizontalAlign="Left" CssClass="gridview" />
                        </asp:GridView>
                        <center>
                                <asp:Panel runat="server" ID="pnlUnprepared">
                                 <h1 class="mt-6" style="color:lightgray;font-size:30px">Trip Chart Details <br />Not <br />Available.</h1>
                            </asp:Panel></center>
                    </div>
                </div>
            </div>
        </div>
        <asp:LinkButton runat="server" Visible="false" OnClick="Unnamed_Click">Generate Trip Chart</asp:LinkButton>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpgeneratechart" runat="server" PopupControlID="pnlchart" TargetControlID="Button9"
                CancelControlID="Button1" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlchart" runat="server" Style="position: fixed;">
                <div class="modal-content mt-5">
                    <div class="modal-header">
                        <div class="col-md-10">
                            <h3 class="m-0">Generate Trip Chart</h3>

                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="lbtnclosechart" runat="server" OnClick="lbtnclosechart_Click" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>

                        </div>

                    </div>
                    <div class="modal-body p-0">
                        <embed src="tripchartgenerate.aspx" style="min-height: 335px; width: 70vw" />
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button9" runat="server" Text="" />
                    <asp:Button ID="Button1" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpTripchart" runat="server" PopupControlID="pnlticket"
                CancelControlID="lbtnclose" TargetControlID="Button8" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlticket" runat="server" Style="position: fixed;">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-lg-6">
                                <h5 class="card-title text-left mb-0">Trip Chart
                                </h5>
                            </div>
                            <div class="col-lg-6">
                                <asp:LinkButton ID="lbtnclose" runat="server" CssClass="text-danger float-right"> <i class="fa fa-times"></i> </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body text-left pt-2" style="overflow: auto;">
                        <embed id="tkt" runat="server" src="" style="height: 70vh; width: 65vw;" />
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button8" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

