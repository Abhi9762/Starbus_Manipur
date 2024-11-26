<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/AgentMaster.master" AutoEventWireup="true" CodeFile="AgCurrentBookingDashboard.aspx.cs" Inherits="Auth_AgCurrentBookingDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../assets/vendor/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var todayDate = new Date().getDate();
            var endD = new Date(new Date().setDate(todayDate));
            var currDate = new Date();
            $('[id*=txtFromDate]').datepicker({
                //startDate: "dateToday",
                endDate: endD,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
            $('[id*=txtToDate]').datepicker({
                //startDate: "dateToday",
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
    <div class="container-fluid pt-2">
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="row">
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h4 class="mb-1">TRIP SUMMARY</h4>
                                        <div class="row m-0">
                                            <div class="col-6 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Today &nbsp;
                                                            <br />
                                                         <%--   <asp:Label ID="lbltTodaytrip" runat="server" data-toggle="tooltip" data-placement="bottom" title="Today Trip" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0 ( 0 ₹ )"></asp:Label></h5>
                                        --%>             <asp:Label ID="lbltodayttkt" runat="server" Text="0"></asp:Label>
                                    (
                                    <asp:Label ID="lbltodaytktamt" runat="server" Text="0"></asp:Label>
                                    ₹ )
                                                   </h5>         
                                                            </div>
                                                    <div class="col-12">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Till Date&nbsp;<br />
                                                          <%--  <asp:Label ID="lblTillDate" runat="server" data-toggle="tooltip" data-placement="bottom" title="Till Date Trip" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0 ( 0 ₹ )"></asp:Label>
                                           --%>        

                                                              <asp:Label ID="lbltotalttkt" runat="server" Text="0"></asp:Label>
                                    (
                                    <asp:Label ID="lbltotaltktamt" runat="server" Text="0"></asp:Label>
                                    ₹ )
                                                        </h5> </div>
                                                    <div class="col-12">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr style="margin-bottom: 0;" />
                                <div class="row mt-1">
                                    <div class="col-12 text-center">
                                        <asp:LinkButton ID="lbtnnewtrip" runat="server" OnClick="lbtnnewtrip_Click" CssClass="btn btn-success w-75 text-lg"><i class="fa fa-bus"></i> Start New Trip</asp:LinkButton>
                                        <asp:Label ID="lblnotripmsg" runat="server" CssClass="text-uppercase text-muted mb-0"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-8">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h4 class="mb-1">Pending/Expired Trip</h4>
                                        <div class="row">
                                            <asp:Repeater ID="rptopentrip" runat="server" OnItemDataBound="rptopentrip_ItemDataBound" OnItemCommand="rptopentrip_ItemCommand">
                                                <ItemTemplate>
                                                    <div class="col-md-3 border-right p-1">
                                                        <div class="card shadow" style="font-size: 10pt;">
                                                            <div class="card-body" style="min-height: 165px;">
                                                                <asp:HiddenField ID="hdtripcode" runat="server" Value='<%# Eval("trip_code") %>' />
                                                                <asp:HiddenField ID="hdtripmin" runat="server" Value='<%# Eval("Tripmin") %>' />
                                                                <asp:HiddenField ID="hdSERVICE_CODE" runat="server" Value='<%# Eval("srtpid") %>' />
                                                                <asp:HiddenField ID="hdREGISTRATIONNUMBER" runat="server" Value='<%# Eval("bus_registration_no") %>' />
                                                                <asp:HiddenField ID="hdCONDUCTOREMPID" runat="server" Value='<%# Eval("primary_conductor_empcode") %>' />
                                                                <asp:HiddenField ID="hdWaybillnumber" runat="server" Value='<%# Eval("waybill_no") %>' />
                                                                <asp:HiddenField ID="hdjourneyType" runat="server" Value='<%# Eval("trip_type") %>' />
                                                                <asp:HiddenField ID="hdSPECIALTRIPYN" runat="server" Value='<%# Eval("specialtrip") %>' />
                                                                <b>
                                                                    <asp:Label ID="lblROUTE_NAME_EN" runat="server" Text='<%# Eval("route_name") %>'></asp:Label>
                                                                    (<asp:Label ID="lblStatus" runat="server" Text='' Style="text-transform: uppercase;"></asp:Label>)</b>
                                                                <br />
                                                                <span>Bus </span>
                                                                <asp:Label ID="lblREGISTRATIONNUMBER" runat="server" Text='<%# Eval("bus_registration_no") %>'></asp:Label>
                                                                (<asp:Label ID="lblSERVICE_TYPE_NAME_EN" runat="server" Text='<%# Eval("service_type_name") %>'></asp:Label>)<br />
                                                                <span>Departure Date </span>
                                                                <asp:Label ID="lblTRIPDATE" runat="server" Text='<%# Eval("trip_date") %>'></asp:Label><br />
                                                                <span>Departure Time </span>
                                                                <asp:Label ID="lblTRIPDEPARTURETIME" runat="server" Text='<%# Eval("trip_time") %>'></asp:Label>
                                                                <div class="row mt-2">
                                                                    <div class="col-md-7 pr-0">
                                                                        <asp:LinkButton ID="lbtnCloseTrip" runat="server" CommandName="CloseTrip" CommandArgument='<%# Eval("trip_id") %>'
                                                                            CssClass="btn btn-danger btn-sm" Style="border-radius: 4px; padding: 4px 10px;"
                                                                            ToolTip="Close Trip"> Generate Trip Chart</asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-md-5 pl-0 text-right">
                                                                        <asp:LinkButton ID="lbtnBookTrip" runat="server" Visible="false" CommandName="OpenTrip"
                                                                            CommandArgument='<%# Eval("trip_id") %>' CssClass="btn btn-primary btn-sm ml-1"
                                                                            Style="border-radius: 4px; padding: 4px 10px;" ToolTip="Book More Tickets"> Book Ticket </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <asp:Panel ID="pnlNotrip" runat="server">
                                            <div class="row">
                                                <div class="col-lg-12 text-center mt-4">
                                                    <p class="text-center" style="font-size: 32px; font-weight: bold; color: #e3e3e3;">
                                                        To start a trip Please click on Start New Trip button
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
            </div>
        </div>
        <div class="row align-items-center mt-4">
            <div class="col-xl-12">
                <div class="card card-stats">
                    <div class="row">
                        <div class="col-4">
                            <div class="card-body">
                                <h4 class="mb-1">TRIP LIST As On Date 
                                    <asp:Label runat="server" ID="lbltriplistdate"></asp:Label></h4>
                            </div>
                        </div>
                        <div class="col-3">
                        </div>
                        <div class="col-5">
                            <div class="card-body input-group-prepend">
                                <asp:Label runat="server" Text="From Date"></asp:Label>
                                <asp:TextBox ID="txtFromDate" AutoComplete="off" runat="server" placeholder="DD/MM/YYYY" CssClass="form-control ml-1" Width="120px"></asp:TextBox>

                                <asp:Label runat="server" Text="To Date" CssClass="ml-3"></asp:Label>
                                <asp:TextBox ID="txtToDate" AutoComplete="off" runat="server" CssClass="form-control ml-1" placeholder="DD/MM/YYYY" Width="120px"></asp:TextBox>
                                <asp:LinkButton runat="server" CssClass="btn btn-success ml-2 btn-sm" ToolTip="Search Trip"><i class="fa fa-search"></i></asp:LinkButton>
                                <asp:LinkButton runat="server" CssClass="btn btn-warning ml-1 btn-sm" ToolTip="Download Excel" Visible="false" ><i class="fa fa-download"></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-12">
                            <hr />
                            <asp:GridView ID="grdTripDetails" runat="server" AllowPaging="true" PageSize="10"
                                CssClass="table" ClientIDMode="Static" AutoGenerateColumns="False" ForeColor="#333333"
                                Font-Size="14px" GridLines="None" Font-Bold="false" OnRowCommand="grdTripDetails_RowCommand" OnPageIndexChanging="grdTripDetails_PageIndexChanging" DataKeyNames="tripcode,specialtrip_yn" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Waybill (Trip)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWaybillNo" runat="server" Text='<%# Eval("waybillno") %>'></asp:Label>
                                            (<asp:Label ID="lblTRIPID" runat="server" Text='<%# Eval("tripcode") %>'></asp:Label>)
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Route">
                                        <ItemTemplate>
                                            <asp:Label ID="lblROUTE_NAME_EN" runat="server" Text='<%# Eval("route_name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bus">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSERVICE_TYPE_NAME_EN" runat="server" Text='<%# Eval("bus_servicetypename") %>'></asp:Label>
                                            (<asp:Label ID="lblREGISTRATIONNUMBER" runat="server" Text='<%# Eval("busno") %>'></asp:Label>)
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Seats Booked">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTOTALSEATSBOOKED" runat="server" Text='<%# Eval("seat_booked") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Trip Date/Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTRIPDATE" runat="server" Text='<%# Eval("trip_date") %>'></asp:Label>
                                            <asp:Label ID="lblTRIPDEPARTURETIME" runat="server" Text='<%# Eval("trip_departuretime") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Special Trip">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsTripSpecial" runat="server" Text='<%# Eval("is_tripspecial") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnViewDetails" runat="server" CommandName="ViewDetails" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                class="btn btn-warning btn-sm" ToolTip="View Trip Details" Style="border-radius: 4px;"><i class="fa fa-eye "></i> </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                            </asp:GridView>
                            <center>
                        <h4 runat="server" id="NoDataLabel" visible="true" style="padding: 50px; font-size: 40px; font-weight: bold; color: #d4d0d0;">No Data Found</h4>
                    </center>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
                CancelControlID="Button6" TargetControlID="Button5" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Please Confirm
                        </h4>
                    </div>
                    <div class="card-body text-center pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnYesConfirmation" runat="server" OnClick="lbtnYesConfirmation_Click" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                            <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button5" runat="server" Text="" />
                    <asp:Button ID="Button6" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="ModalPopupExtenderFirst" runat="server" CancelControlID="LinkButton9"
                TargetControlID="Button2" PopupControlID="Panel2" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="Panel2" Style="display: none" runat="server">
                <div class="card" style="width: 360px;">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-lg-12">
                                <strong class="card-title">Important</strong>
                                <asp:LinkButton ID="LinkButton9" runat="server" UseSubmitBehavior="false" data-dismiss="modal"
                                    ToolTip="Close" Style="float: right; color: red; padding: 4px;">
                                <i class="fa fa-times fa-2x"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="col-12">
                            <center>
                                    <i class="fa fa-rupee" style="font-size: 100px;"></i>
                                    <br />
                                    <asp:Label ID="Label1" runat="server" Text="" Style="font-size: 10pt; font-family: verdana; color: red; font-weight: bold;"></asp:Label>
                                </center>
                        </div>
                    </div>
                    <div class="card-footer" style="text-align: right;">
                        <asp:LinkButton ID="btnamtok" runat="server" CssClass="btn btn-warning btn-sm"> <i class="fa fa-times-circle-o"></i> OK </asp:LinkButton>
                    </div>
                </div>
            </asp:Panel>
            <div style="visibility: hidden; height: 0px;">
                <asp:Button ID="Button2" runat="server" />
            </div>
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
                        <embed id="tkt" runat="server" src="" style="height: 85vh; width: 46vw;" />
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button8" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

