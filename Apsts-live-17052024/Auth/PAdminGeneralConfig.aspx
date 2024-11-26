<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/PortalAdmmaster.master" AutoEventWireup="true" CodeFile="PAdminGeneralConfig.aspx.cs" Inherits="Auth_PAdminGeneralConfig" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=gvtravelerseatavailable] input[type=checkbox]").click(function () {
                if ($(this).is(":checked")) {
                    // $("[id*=gvtravelerseatavailable] input[type=checkbox]").removeAttr("checked");
                    $(this).attr("checked", "checked");
                }
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            //Enable Disable TextBoxes in a Row when the Row CheckBox is checked.
            $("[id*=cbMaxSeat]").bind("click", function () {

                //Find and reference the GridView.
                var grid = $(this).closest("table");

                //Find and reference the Header CheckBox.
                var chkHeader = $("[id*=chkHeader]", grid);

                //If the CheckBox is Checked then enable the TextBoxes in thr Row.
                if (!$(this).is(":checked")) {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#FFF" });
                    $("input[type=text]", td).attr("disabled", "disabled");
                } else {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#D8EBF2" });
                    $("input[type=text]", td).removeAttr("disabled");
                }

                //Enable Header Row CheckBox if all the Row CheckBoxes are checked and vice versa.
                if ($("[id*=chkRow]", grid).length == $("[id*=chkRow]:checked", grid).length) {
                    chkHeader.attr("checked", "checked");
                } else {
                    chkHeader.removeAttr("checked");
                }
            });
        });
    </script>
    <script type="text/javascript">
        function UploadImageWeb(fileUpload) {
            if ($('#ImgWebPortal').value != '') {
                document.getElementById("<%=btnUploadWebPortal.ClientID %>").click();
            }
        }
        function UploadImageMob(fileUpload) {
            if ($('#imgMobileApp').value != '') {
                document.getElementById("<%=btnUploadMobileApp.ClientID %>").click();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="header pb-3">
    </div>
    <div class="container-fluid mb-2 pb-5">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row">
            <div class="col-lg-3 col-md-3">
                <div class="row mt-1">
                    <div class="col-lg-12 col-md-12  stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Advance Booking Days </div>
                                <div class="total-tx mb-2">Online Advance Booking Days </div>
                                <div class="col text-right">
                                    <asp:LinkButton ID="lbtnAdvancedaysbooking" OnClick="lbtnAdvancedaysbooking_Click" runat="server" class="btn btn-sm btn-primary">Explore <i class="fa fa-arrow-circle-o-right"></i> </asp:LinkButton>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>
                <div class="row mt-2">
                    <div class="col-lg-12 col-md-12 stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Trip Chart Generation/ Booking Closing Time </div>
                                <div class="total-tx mb-2">Advance Booking Closed Time And Generate Trip Chart Time </div>
                                <div class="col text-right">
                                    <asp:LinkButton ID="lbtnAdvanceBookingTripChartTime" OnClick="lbtnAdvanceBookingTripChartTime_Click" runat="server" class="btn btn-sm btn-primary">Explore <i class="fa fa-arrow-circle-o-right"></i> </asp:LinkButton>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>
                <div class="row mt-2">
                    <div class="col-lg-12 col-md-12 stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Extra Seats Payment </div>
                                <div class="total-tx mb-2">Extra Payment Of Particular Like Prime Seats </div>
                                <div class="col text-right">
                                    <asp:LinkButton ID="lbtnSeatsExtraPayment" OnClick="lbtnSeatsExtraPayment_Click" runat="server" class="btn btn-sm btn-primary">Explore <i class="fa fa-arrow-circle-o-right"></i> </asp:LinkButton>
                                </div>

                            </div>
                        </div>

                    </div>

                </div>
                <div class="row mt-2">
                    <div class="col-lg-12 col-md-12 stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Payment Gateway Management</div>
                                <div class="total-tx mb-2">Payment Gateway  Activate And Deactivate Configuration </div>
                                <div class="col text-right">
                                    <asp:LinkButton ID="lbtnPaymentGatewayStatus" OnClick="lbtnPaymentGatewayStatus_Click" runat="server" class="btn btn-sm btn-primary">Explore <i class="fa fa-arrow-circle-o-right"></i> </asp:LinkButton>
                                </div>

                            </div>
                        </div>

                    </div>

                </div>
                <div class="row mt-2">
                    <div class="col-lg-12 col-md-12 stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Maximum Seat Booking at a Time</div>
                                <div class="total-tx mb-2">Service Type Wise Maximum Seats Available For Booking</div>
                                <div class="col text-right">
                                    <asp:LinkButton ID="lbtnTravelerSeatAvailability" OnClick="lbtnTravelerSeatAvailability_Click" runat="server" class="btn btn-sm btn-primary">Explore <i class="fa fa-arrow-circle-o-right"></i> </asp:LinkButton>
                                </div>

                            </div>
                        </div>

                    </div>

                </div>
                <div class="row mt-2">

                    <div class="col-lg-12 col-md-12 stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Advertisement On Ticket</div>
                                <div class="total-tx mb-2">Print Specific Text For Tickets</div>
                                <div class="col text-right">
                                    <asp:LinkButton ID="lbtnTicketExtraText" OnClick="lbtnTicketExtraText_Click" runat="server" class="btn btn-sm btn-primary">Explore <i class="fa fa-arrow-circle-o-right"></i> </asp:LinkButton>
                                </div>

                            </div>
                        </div>

                    </div>

                </div>
                <div class="row mt-2">
                    <div class="col-lg-12 col-md-12 stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Ticket Type /Mode</div>
                                <div class="total-tx mb-2">Set ETM/Manual Ticket Active And Deactive</div>
                                <div class="col text-right">
                                    <asp:LinkButton I ID="lbtnBusTicketingType" OnClick="lbtnBusTicketingType_Click" runat="server" class="btn btn-sm btn-primary">Explore <i class="fa fa-arrow-circle-o-right"></i> </asp:LinkButton>
                                </div>

                            </div>
                        </div>

                    </div>

                </div>
            </div>
            <div class="col-lg-9 col-md-9">
                <asp:Panel ID="pnlAdvancedaysbooking" runat="server" Visible="false">
                    <div class="row mt-1 ml-2 m-0">
                        <div class="col-lg-12 col-md-12 order-xl-1 ">
                            <div class="card" style="min-height: 400px">
                                <div class="col-lg-12 col-md-12">
                                    <div class="card-header">
                                        <div class="row m-0 align-items-center">
                                            <div class="col-md-9 ">
                                                <h3 class="mb-1">
                                                    <asp:Label ID="lblGeneralsHeading" runat="server" CssClass="form-control-label" Font-Bold="true"><h2> Advance Booking Days </h2></asp:Label>

                                                </h3>
                                            </div>
                                            <div class="col-md-3 text-right">
                                                <h4>
                                                    <asp:LinkButton ID="lbtnViewHelp" runat="server" OnClick="lbtnViewHelp_Click" ToolTip="View Instructions" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton ID="lbtnAdvanceDayBViewHistory" OnClick="lbtnAdvanceDayBViewHistory_Click" runat="server" ToolTip="Click here to View Advance Days Booking History" CssClass="btn btn bg-gradient-blue btn-sm text-white">
                                    <i class="fa fa-history"></i>
                                                    </asp:LinkButton>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-2 ml-2 m-0">
                                        <div class="col-lg-6">

                                            <div class="row m-0 mt-2">
                                                <div class="col-lg-4  pr-0">

                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h3>New Value</h3></asp:Label>

                                                </div>
                                            </div>
                                            <div class="row m-0 mt-3">

                                                <div class="col-lg-3 pr-0">
                                                </div>
                                                <div class="col-lg-6">
                                                    <asp:TextBox ID="tbadvancedays" ToolTip="Enter a new value" CssClass="form-control form-control-sm" runat="server" autocomplete="off" MaxLength="2" placeholder="Min. 1 Days"
                                                        Text=""></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajaxFtadvancedays" runat="server" FilterType="Numbers, Custom"
                                                        TargetControlID="tbadvancedays" />

                                                </div>
                                                <div class="col-lg-2 pl-0">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Days</asp:Label>
                                                </div>

                                            </div>

                                            <div class="row m-0 mt-3">
                                                <div class="col-lg-12 text-center">
                                                    <asp:LinkButton ID="lbtnsaveadvancedays" runat="server" OnClick="lbtnsaveadvancedays_Click" class="btn btn-success"
                                                        ToolTip="Click here to Save Advance days"> <i class="fa fa-save" ></i> Save</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnresetadvancedays" runat="server" OnClick="lbtnresetadvancedays_Click" class="btn btn-danger"
                                                        ToolTip="Click here to reset Advance days"> <i class="fa fa-undo" ></i> Reset</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6 pl-3" style="border-left: 1px solid;">
                                            <div class="row m-0 mt-2">
                                                <div class="col-lg-12  pr-0">

                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h3>Current Value</h3></asp:Label>

                                                </div>
                                            </div>
                                            <div class="row m-0 mt-3">
                                                <div class="col-lg-12 col-md-12">
                                                    <asp:GridView ID="gvAdvancebookingdays" runat="server" AutoGenerateColumns="False"
                                                        GridLines="None" AllowSorting="true" PageSize="5"
                                                        DataKeyNames="days,actiondate,actionby" ShowHeader="false" Font-Size="10pt">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Value
                                         <asp:Label ID="lblDAYS" runat="server" Text='<%# Bind("days")%>' Style="color: Red;"></asp:Label>
                                                                        Days
                                                                    </asp:Label>
                                                                    <br />
                                                                    <br />
                                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Updation On
                                        
                                           <asp:Label ID="lblupdataiondatetime" runat="server" Text='<%# Bind("actiondate")%>'
                                               Style="color: Red;"></asp:Label>
                                                                    </asp:Label>
                                                                    <br />
                                                                    <br />
                                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Updated By
                                        
                                         
                                            <asp:Label ID="lblUPDATEDBY" runat="server" Text='<%# Bind("actionby")%>' Style="color: Red;"></asp:Label>
                                                                    </asp:Label>
                                                                    <br />
                                                                    <br />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                                    </asp:GridView>
                                                    <asp:Panel ID="pnladvancenostatusrecord" runat="server" Width="100%" Visible="true">
                                                        <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                                            <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;">
                                                                Sorry No Record Found
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

                </asp:Panel>
                <asp:Panel ID="pnlAdvanceBookingTripChartTime" runat="server" Visible="false">
                    <div class="row mt-1 ml-2 m-0">
                        <div class="col-lg-12 col-md-12 order-xl-1 ">
                            <div class="card" style="min-height: 400px">
                                <div class="col-lg-12 col-md-12">
                                    <div class="card-header">
                                        <div class="row m-0 align-items-center">
                                            <div class="col-md-9 ">
                                                <h3 class="mb-1">
                                                    <asp:Label ID="lblTripChart" runat="server" CssClass="form-control-label" Font-Bold="true"><h2> Trip Chart Generation/ Booking Closing Time </h2></asp:Label>

                                                </h3>
                                            </div>
                                            <div class="col-md-3 text-right">
                                                <h4>
                                                    <asp:LinkButton ID="lbtnhelp" runat="server" OnClick="lbtnhelp_Click" ToolTip="View Instructions" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton ID="lbtnTripChartGenerater" OnClick="lbtnTripChartGenerater_Click" runat="server" ToolTip="Click here to View Trip Chart Generater History" CssClass="btn btn bg-gradient-blue btn-sm text-white">
                                    <i class="fa fa-history"></i>
                                                    </asp:LinkButton>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-2 ml-2 m-0">
                                        <div class="col-lg-6">
                                            <div class="row m-0 mt-2">
                                                <div class="col-lg-4  pr-0">

                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h3>New Value</h3></asp:Label>

                                                </div>
                                            </div>

                                            <div class="row m-0 mt-3">
                                                <div class="col-lg-3 text-right pr-0">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Booking<span class="text-warning">*</span></asp:Label>
                                                </div>
                                                <div class="col-lg-6 ">
                                                    <asp:TextBox ID="tbTripChartGeneraterBooking" CssClass="form-control form-control-sm" placeholder="max 3 num..." runat="server" ToolTip="Enter a New Booking Minute" MaxLength="3" autocomplete="off"
                                                        Text=""></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajaxFtTripChartGeneraterBooking" runat="server" FilterType="Numbers, Custom"
                                                        TargetControlID="tbTripChartGeneraterBooking" />
                                                </div>
                                                <div class="col-lg-3 pl-0">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Minutes</asp:Label>
                                                </div>
                                            </div>
                                            <div class="row m-0 mt-2">
                                                <div class="col-lg-3 text-right pr-0">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Trip Chart<span class="text-warning">*</span></asp:Label>
                                                </div>
                                                <div class="col-lg-6 ">
                                                    <asp:TextBox ID="tbTripChartGeneraterTripC" CssClass="form-control form-control-sm" placeholder="max 3 num..." ToolTip="Enter a New Trip Chart Minute" runat="server" MaxLength="3" autocomplete="off"
                                                        Text=""></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajaxFtTripChart" runat="server" FilterType="Numbers, Custom"
                                                        TargetControlID="tbTripChartGeneraterTripC" />

                                                </div>
                                                <div class="col-lg-3 pl-0">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Minutes</asp:Label>
                                                </div>
                                            </div>
                                            <div class="row m-0 mt-3">
                                                <div class="col-lg-12 text-center">
                                                    <asp:LinkButton ID="lbtnSaveTripChartGenerater" OnClick="lbtnSaveTripChartGenerater_Click" runat="server" class="btn btn-success"
                                                        ToolTip="Click here to save Trip Chart Generater/ Booking Closing Time"> <i class="fa fa-save" ></i> Save</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnResetTripChartGenerater" OnClick="lbtnResetTripChartGenerater_Click" runat="server" class="btn btn-danger"
                                                        ToolTip="Click here to reset Trip Chart Generater/ Booking Closing Time"> <i class="fa fa-undo" ></i> Reset</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 pl-3" style="border-left: 1px solid;">
                                            <div class="row m-0 mt-2">
                                                <div class="col-lg-4  pr-0">

                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h3>Current Value</h3></asp:Label>

                                                </div>
                                            </div>
                                            <div class="col-lg-12 mt-3 pr-0">
                                                <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Online Booking
                                                <asp:Label ID="lblbookingmin" runat="server" CssClass="form-control-label" Text="0" Style="color: red;"
                                                    Visible="false"></asp:Label>
                                                    Min.
                                                </asp:Label>

                                            </div>
                                            <br />
                                            <div class="col-lg-12  pr-0">
                                                <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Trip Chart
                                                <asp:Label ID="lbltripchartmin" runat="server" Text="0" Style="color: red" CssClass="form-control-label"
                                                    Visible="false"></asp:Label>
                                                    Min.
                                                </asp:Label>
                                            </div>
                                            <br />
                                            <div class="col-lg-12  pr-0">
                                                <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Updation On

                                                    <asp:Label ID="lblAdupdataiondatetime" CssClass="form-control-label" runat="server" Text='<%# Eval("actiondate") %>'
                                                        Style="color: Red;"></asp:Label>
                                                </asp:Label>
                                            </div>
                                            <br />
                                            <div class="col-lg-12  pr-0">
                                                <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Updated By
                                                    <asp:Label ID="lblAdUPDATEDBY" CssClass="form-control-label" runat="server" Text='<%# Eval("actionby") %>' Style="color: Red;"></asp:Label>
                                                </asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlSeatsExtraPayment" runat="server" Visible="false">
                    <div class="row mt-1 ml-2 m-0">
                        <div class="col-lg-12 col-md-12 order-xl-1 ">
                            <div class="card" style="min-height: 400px">
                                <div class="col-lg-12 col-md-12">
                                    <div class="card-header">
                                        <div class="row m-0 align-items-center">
                                            <div class="col-md-9">
                                                <h3 class="mb-1">
                                                    <asp:Label ID="lblExtraSeatPay" runat="server" CssClass="form-control-label" Font-Bold="true"><h2>Extra Seats Payment</h2></asp:Label>

                                                </h3>
                                            </div>
                                            <div class="col-md-3 text-right">
                                                <h4>
                                                    <asp:LinkButton ID="lbtnseatshelp" runat="server" OnClick="lbtnseatshelp_Click" ToolTip="View Instructions" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton ID="lbtnseatsExtraPaymentHistory" OnClick="lbtnseatsExtraPaymentHistory_Click" runat="server" ToolTip="Click here to View Extra Seats Payment History" CssClass="btn btn bg-gradient-blue btn-sm text-white">
                                    <i class="fa fa-history"></i>
                                                    </asp:LinkButton>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-2 m-0">
                                        <div class="col-lg-6">
                                            <div class="row m-0 mt-2">
                                                <div class="col-lg-12">

                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h3>New Value</h3></asp:Label>

                                                </div>
                                            </div>
                                            <div class="row m-0 mt-2">
                                                <div class="col-lg-3 text-right">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Seat Type<span class="text-warning">*</span> </asp:Label>
                                                </div>
                                                <div class="col-lg-6 pl-0">
                                                    <asp:DropDownList ID="ddlseattype" runat="server" ToolTip=" select Extra Seat Type"
                                                        Style="font-size: 10pt;" CssClass="form-control form-control-sm">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>


                                            <div class="row m-0 mt-2">
                                                <div class="col-lg-3 text-right">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Value<span class="text-warning">*</span> </asp:Label>
                                                </div>
                                                <div class="col-lg-6 pl-0">
                                                    <asp:TextBox CssClass="form-control form-control-sm" placeholder="max length 5" ToolTip=" Enter a New Seat Value" runat="server" ID="tbseatvalue" MaxLength="5" autocomplete="off"
                                                        Text=""></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajaxFtseatvalue" runat="server" FilterType="Numbers, Custom"
                                                        TargetControlID="tbseatvalue" />


                                                </div>
                                                <div class="col-lg-3 pl-0">
                                                    <asp:Label runat="server" CssClass="form-control-label" Text="%" Font-Bold="true"></asp:Label>

                                                </div>
                                            </div>
                                            <div class="row m-0 mt-3">
                                                <div class="col-lg-12 text-center pl-0">
                                                    <asp:LinkButton ID="lbtnsaveseatpmtvalue" OnClick="lbtnsaveseatpmtvalue_Click" runat="server" class="btn btn-success"
                                                        ToolTip="Click here to Save Extra Seats payment"> <i class="fa fa-save" ></i> Save</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnresetseatpmtvalue" OnClick="lbtnresetseatpmtvalue_Click" runat="server" class="btn btn-danger"
                                                        ToolTip="Click here to reset extra seats payment"> <i class="fa fa-undo" ></i> Reset</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 pl-3" style="border-left: 1px solid;">
                                            <div class="row  m-0 mt-2">
                                                <div class="col-lg-12 ">

                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h3>Current Value</h3></asp:Label>

                                                </div>
                                            </div>
                                            <div class="row ml-3 m-0 mt-2">
                                                <asp:GridView ID="gvseatextrapmt" runat="server" AutoGenerateColumns="False"
                                                    GridLines="None" AllowSorting="true" AllowPaging="true" PageSize="5"
                                                    DataKeyNames="" ShowHeader="false" Font-Size="10pt">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpaymentcategoryname" CssClass="form-control-label" Font-Bold="true" runat="server" Text='<%# Eval("paymentcategory_name") %>'></asp:Label>
                                                                <asp:Label ID="lblpaymentper" CssClass="form-control-label" Font-Bold="true" runat="server" Text='<%# Eval("amountpercentage") %>' Style="color: Red;"></asp:Label>
                                                                %
                                                                <br />
                                                                <br />

                                                                <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Updation On
                                                   
                                                                    <asp:Label ID="lblsepupdataiondatetime" runat="server" Text='<%# Eval("actiondate") %>'
                                                                        Style="color: Red;"></asp:Label>
                                                                    <br />
                                                                    <br />
                                                                    Updated By
                                                                    <asp:Label ID="lblsepUPDATEDBY" runat="server" Text='<%# Eval("actionby") %>' Style="color: Red;"></asp:Label>
                                                                </asp:Label>
                                                                <br />
                                                                <br />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                                </asp:GridView>
                                                <asp:Panel ID="pnlnoExtraNoRecord" runat="server" Width="100%" Visible="true">
                                                    <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                                        <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;">
                                                            Sorry No Record Found
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
                <asp:Panel ID="pnlPmntGatewayStatus" runat="server" Visible="false">
                    <div class="row mt-1 ml-2 m-0">
                        <div class="col-lg-12 col-md-12 order-xl-1 ">
                            <div class="card" style="min-height: 485px">
                                <div class="col-lg-12 col-md-12">
                                    <div class="card-header">
                                        <div class="row m-0 align-items-center">
                                            <div class="col-md-9">
                                                <h3 class="mb-1">
                                                    <asp:Label ID="lblPaymentGatewayAddit" runat="server" CssClass="form-control-label" Font-Bold="true"><h2>Payment Gateway Management</h2></asp:Label>

                                                </h3>
                                            </div>
                                            <div class="col-md-3 text-right">
                                                <h4>
                                                    <asp:LinkButton ID="lbtnpmtgatewayhelp" runat="server" OnClick="lbtnpmtgatewayhelp_Click" ToolTip="View Instructions" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton ID="lbtnPaymentGatewayStatusHistory" OnClick="lbtnPaymentGatewayStatusHistory_Click" runat="server" ToolTip="Click here to View Payment Gateway Addition History" CssClass="btn btn bg-gradient-blue btn-sm text-white">
                                    <i class="fa fa-history"></i>
                                                    </asp:LinkButton>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row  mt-2 m-0">
                                        <div class="col-lg-6">
                                            <div class="row m-0 mt-2">
                                                <div class="col-lg-12">

                                                    <asp:Label runat="server" ID="lblAddPmt" CssClass="form-control-label" Font-Bold="true"><h3>Add New Payment Gateway</h3></asp:Label>
                                                    <asp:Label runat="server" ID="lblUpdatePmt" Visible="false" CssClass="form-control-label" Font-Bold="true"><h3>Update Payment Gateway</h3></asp:Label>

                                                </div>
                                            </div>

                                            <div class="row  m-0 mt-2">
                                                <div class="col-lg-6 text-left pr-0">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">1. Name<span class="text-warning">*</span></asp:Label>
                                                </div>
                                                 <div class="col-lg-6 text-left pr-0">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">2. Description<span class="text-warning">*</span></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row m-0 mt-2">
                                                <div class="col-lg-6 text-left">
                                                    <asp:TextBox ID="tbPaymentName" placeholder="Max Length 20 characters " ToolTip="Enter a New Payment Gateway" CssClass="form-control form-control-sm" runat="server" autocomplete="off" MaxLength="20"
                                                        Text=""></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajaxFTPAymentName" runat="server" FilterType="Custom,LowercaseLetters, UppercaseLetters" ValidChars="_ "
                                                        TargetControlID="tbPaymentName" />
                                                </div>
                                                 <div class="col-lg-6 text-left">
                                                    <asp:TextBox ID="tbDescription" TextMode="MultiLine" placeholder="Max Length 200 characters " ToolTip="Enter Description" CssClass="form-control form-control-sm" runat="server" autocomplete="off" MaxLength="200"
                                                        Text=""></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom,LowercaseLetters, UppercaseLetters" ValidChars="_ "
                                                        TargetControlID="tbDescription" />
                                                </div>
                                            </div>
                                              <div class="row  m-0 mt-2">
                                                <div class="col-lg-8 col-md-8 text-left">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="small">3. Request URL<span class="text-warning">*</span></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row m-0 mt-2">
                                                <div class="col-lg-6 text-left">
                                                    <asp:TextBox ID="tbRequestUrl" placeholder="Max Length 50 characters " ToolTip="Enter Request URL" CssClass="form-control form-control-sm" runat="server" autocomplete="off" MaxLength="20"
                                                        Text=""></asp:TextBox>
                                                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom,LowercaseLetters, UppercaseLetters" ValidChars="_."
                                                        TargetControlID="tbRequestUrl" />
                                                </div>
                                               
                                            </div>
                                            <div class="row  m-0 mt-2">
                                                <div class="col-lg-8 col-md-8 text-left">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="small">4. Images</asp:Label>
                                                </div>
                                            </div>
                                            <div class="row  mt-2 m-0">
                                                <div class="col-lg-6 pl-0">
                                                    <div class="col-md-12 col-lg-12 text-left">
                                                        <asp:Label runat="server" CssClass="form-control-label">For Web Portal <span class="text-warning">*</span> </asp:Label><br />
                                                        <span style="color: #ea5d46; font-size: 7pt">(Max 20Kb/ 200*100 Pixel)</span>
                                                    </div>


                                                    <div class="row mt-1 m-0">
                                                        <div class="col-md-12 col-lg-12 text-left">
                                                            <asp:Button ToolTip="Upload Web Portal Image" ID="btnUploadWebPortal" OnClick="btnUploadWebPortal_Click" runat="server" CausesValidation="False" CssClass="form-control form-control-sm"
                                                                Style="display: none" TabIndex="18" Text="Upload Image" accept=".png,.jpg,.jpeg,.gif" Width="80px" />
                                                            <asp:FileUpload ID="FileWebPortal" onchange="UploadImageWeb(this);" runat="server" Style="color: black; background-color: #eaf4ff; border: none; width: 191px;" CssClass="btn btn-sm btn-success" TabIndex="9" />
                                                            <asp:Image ID="ImgWebPortal" onchange="UploadImageWeb(this);" runat="server" Visible="false" Style="border-width: 0px; height: 50px; width: 50px; border: 2px solid #eaf4ff;" />
                                                            <asp:LinkButton ID="lbtncloseWebImage" runat="server" OnClick="lbtncloseWebImage_Click" Style="font-size: 5pt; border-radius: 25px; margin-bottom: 22pt" Visible="false" CssClass="btn btn-sm btn-danger"><i class="fa fa-times"></i></asp:LinkButton><br />
                                                            <label id="lblwrongimage" runat="server" style="font-size: 7pt; color: Red; line-height: 12px;">
                                                                Image size will be less then 1 MB<br />
                                                                (Only .JPG, .PNG, .JPEG)</label>
                                                            <br />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6 pl-0">

                                                    <div class="col-md-12 col-lg-12 text-left">
                                                        <asp:Label runat="server" CssClass="form-control-label">For Mobile App<span class="text-warning">*</span>  </asp:Label><br />
                                                        <span style="color: #ea5d46; font-size: 7pt">(Max 10Kb/ 155*18 Pixel)</span>
                                                    </div>
                                                    <div class="row mt-1 m-0">
                                                        <div class="col-md-12 col-lg-12 text-left">
                                                            <asp:Button ToolTip="Upload Mobile App image" ID="btnUploadMobileApp" OnClick="btnUploadMobileApp_Click" runat="server"
                                                                CausesValidation="False" CssClass="file-upload-inner" accept=".png,.jpg,.jpeg,.gif"
                                                                Style="display: none" Text="Upload Image" Width="80px" />
                                                            <asp:FileUpload ID="FileMobileApp" onchange="UploadImageMob(this);" runat="server" Style="color: black; background-color: #eaf4ff; border: none; width: 191px;" CssClass="btn btn-sm btn-success" TabIndex="9" />
                                                            <asp:Image ID="imgMobileApp" onchange="UploadImageMob(this);" runat="server" Visible="false" Style="border-width: 0px; height: 50px; width: 50px; border: 2px solid #eaf4ff;" />
                                                            <asp:LinkButton ID="lbtncloseMobileImage" OnClick="lbtncloseMobileImage_Click" runat="server" Style="font-size: 5pt; border-radius: 25px; margin-bottom: 22pt" Visible="false" CssClass="btn btn-sm btn-danger"><i class="fa fa-times"></i></asp:LinkButton><br />
                                                            <label id="lblwrongimageMob" runat="server" style="font-size: 7pt; color: Red; line-height: 12px;">
                                                                Image size will be less then 1 MB<br />
                                                                (Only .JPG, .PNG, .JPEG)</label><br />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row mt-3">
                                                <div class="col-lg-12 text-center">
                                                    <asp:LinkButton ID="lbtnPStatusupdate" OnClick="lbtnPStatusupdate_Click" runat="server" class="btn btn-success"
                                                        ToolTip="Click here to Update payment gateway status"> <i class="fa fa-save" ></i> Save</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnUpdatePmt" OnClick="lbtnUpdatePmt_Click" Visible="false" runat="server" class="btn btn-primary"
                                                        ToolTip="Click here to Update payment gateway status"> <i class="fa fa-save" ></i> Update</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnPmtCancel" Visible="false" OnClick="lbtnPmtCancel_Click" runat="server" class="btn btn-danger"
                                                        ToolTip="Click here to Reset payment gateway status"> <i class="fa fa-times" ></i> Cancel</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnPStatusreset" OnClick="lbtnPStatusreset_Click" runat="server" class="btn btn-danger"
                                                        ToolTip="Click here to Reset payment gateway status"> <i class="fa fa-times" ></i> Cancel</asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-lg-6 pl-3" style="border-left: 1px solid;">
                                            <div class="row m-0 mt-2">
                                                <div class="col-lg-12">

                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h3>Payment Gateway List</h3></asp:Label>

                                                </div>
                                            </div>
                                            <asp:GridView ID="gvPmtgatewaystatus" runat="server" OnRowDataBound="gvPmtgatewaystatus_RowDataBound" OnRowCommand="gvPmtgatewaystatus_RowCommand" OnPageIndexChanging="gvPmtgatewaystatus_PageIndexChanging" AutoGenerateColumns="False" GridLines="None"
                                                Visible="true" CssClass="table table-hover table-striped" DataKeyNames="gatewayid,gatewayname,img_web,img_app,stname,statusname,descr,url">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Gateway">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnPaymentId" runat="server" Value='<%#Eval("gatewayid") %>' />
                                                            <asp:Label ID="lblPMTGATEWAYNAME" CssClass="form-control-label" runat="server" Text='<%# Eval("gatewayname") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSTATUSNAME" CssClass="form-control-label" runat="server" Text='<%# Eval("stname") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnUpdatepmtStatus" runat="server" CssClass="btn btn-sm btn-primary"
                                                                CommandName="Updation" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                ToolTip=" click here to Edit payment Gateway"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnpmtActiveDeactive" runat="server" CssClass="btn btn-sm btn-success" Visible="true"
                                                                CommandName="ActiveDeactive" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                ToolTip="click here to Activate  payment Gateway"> <i class="fa fa-toggle-on"></</asp:LinkButton>
                                                            <asp:LinkButton ID="lnkbtnDecative" runat="server" CssClass="btn btn-sm btn-danger" Visible="false"
                                                                CommandName="ActiveDeactive" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                ToolTip="click here to Deactivate   payment Gateway"> <i class="fa fa-toggle-off"></</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle BackColor="#ececec" Font-Size="9pt" />
                                                <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                            </asp:GridView>
                                            <asp:Panel ID="pnlPmtgatewayNodata" runat="server" Width="100%" Visible="true">
                                                <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                                    <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;">
                                                        Sorry No Record Found
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlTravelerSeatAvailability" runat="server" Visible="false">
                    <div class="row mt-1 ml-2 m-0">
                        <div class="col-lg-12 col-md-12 order-xl-1 ">
                            <div class="card" style="min-height: 400px">
                                <div class="col-lg-12 col-md-12">
                                    <div class="card-header">
                                        <div class="row m-0 align-items-center">
                                            <div class="col-md-9">
                                                <h3 class="mb-1">
                                                    <asp:Label ID="lblMaximumSeatBooking" runat="server" CssClass="form-control-label" Font-Bold="true"><h2>Maximum Seat Booking at a Time</h2></asp:Label>

                                                </h3>
                                            </div>
                                            <div class="col-md-3 text-right">
                                                <h4>
                                                    <asp:LinkButton ID="lbtntravelerhelp" runat="server" OnClick="lbtntravelerhelp_Click" ToolTip="View Instructions" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton ID="lbtnTravelerSeatAvailabilityHistory" OnClick="lbtnTravelerSeatAvailabilityHistory_Click" runat="server" ToolTip="Click here to View Maximum Seat Booking at a Time History" CssClass="btn btn bg-gradient-blue btn-sm text-white">
                                    <i class="fa fa-history"></i>
                                                    </asp:LinkButton>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-2 m-0">
                                        <div class="col-lg-8" style="border-right: 1px solid;">

                                            <asp:GridView ID="gvtravelerseatavailable" runat="server" OnPageIndexChanging="gvtravelerseatavailable_PageIndexChanging" PageSize="8" AutoGenerateColumns="False"
                                                GridLines="None" AllowSorting="true" class="table "
                                                DataKeyNames="servicetypename_en,servicecode,currentseats" Font-Size="10pt">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbMaxSeat" runat="server" CssClass="form-check"></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Bus Service">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBusService" CssClass="form-control-label" runat="server" Text='<%# Eval("servicetypename_en") %>'></asp:Label>
                                                            <asp:HiddenField ID="serviceid" runat="server" Value='<%# Eval("servicecode") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Current Seat(s) Available">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSTATUSNAME" CssClass="form-control-label" runat="server" Text='<%# Eval("currentseats") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Update Seat(s) Available" ControlStyle-Width="80px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="tbnewseat" runat="server" CssClass="form-control form-control-sm w-50" Enabled="false" placeholder="max 1 Num.." ToolTip="Enter Seat(s)" autocomplete="off" MaxLength="1"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="ajaxFTBoxExten" runat="server" FilterType="Numbers"
                                                                TargetControlID="tbnewseat" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle BackColor="#ececec" Font-Size="9pt" />
                                                <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                            </asp:GridView>
                                            <asp:Panel ID="pnlNoRecord" runat="server" Width="100%" Visible="true">
                                                <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                                    <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
                                                        Service Type List not available<br />
                                                        Please add Service Type
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <div class="row mt-2">
                                                <div class="col-lg-12 text-center">
                                                    <asp:LinkButton ID="lbtnsavetravelerseat" OnClick="lbtsavetravelerseat_Click" runat="server" Visible="false" class="btn  btn-success"
                                                        ToolTip="Click here to update traveler seat availablity"> <i class="fa fa-save" ></i> Save</asp:LinkButton>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 pl-3">
                                            <h4 class="text-warning mb-1 font-weight-bold">Please Note:</h4>
                                            <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">(Min, Max) Value for update seat is (1, 6)</asp:Label><br />
                                            <br />
                                            <asp:Label runat="server" CssClass="form-control-label " Font-Bold="true"> Last Updated</asp:Label>


                                            <asp:Label runat="server" ID="lbllastupdated" CssClass="form-control-label text-warning" Font-Bold="true"></asp:Label>
                                            <br />
                                            <br />

                                            <asp:Label runat="server" CssClass="form-control-label " Font-Bold="true"> On</asp:Label>

                                            <asp:Label ID="lblseatupdataiondatetime" runat="server" CssClass="form-control-label text-warning" Font-Bold="true"></asp:Label>

                                            &nbsp;
                                                 <br />
                                            <br />

                                            <asp:Label runat="server" CssClass="form-control-label " Font-Bold="true">By</asp:Label>

                                            <asp:Label ID="lblseatUPDATEDBY" runat="server" CssClass="form-control-label text-warning " Font-Bold="true"></asp:Label>

                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlTicketExtraText" runat="server" Visible="false">
                    <div class="row mt-1 ml-2 m-0">
                        <div class="col-lg-12 col-md-12 order-xl-1 ">
                            <div class="card" style="min-height: 400px">
                                <div class="col-lg-12 col-md-12">
                                    <div class="card-header">
                                        <div class="row m-0 align-items-center">
                                            <div class="col-md-9">
                                                <h3 class="mb-1">
                                                    <asp:Label ID="lblAdvOnTicket" runat="server" CssClass="form-control-label" Font-Bold="true"><h2>Advertisement On Ticket</h2></asp:Label>

                                                </h3>
                                            </div>
                                            <div class="col-md-3 text-right">
                                                <h4>
                                                    <asp:LinkButton ID="lbtntickethelp" runat="server" OnClick="lbtntickethelp_Click" ToolTip="View Instructions" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton ID="lbtnTicketExtraTextHistory" OnClick="lbtnTicketExtraTextHistory_Click" runat="server" ToolTip="Click here to View Advertisement On Ticket History" CssClass="btn btn bg-gradient-blue btn-sm text-white">
                                    <i class="fa fa-history"></i>
                                                    </asp:LinkButton>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-2 m-0">
                                        <div class="col-lg-6">
                                            <div class="row  m-0 mt-2">
                                                <div class="col-lg-12 ">

                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h3>New Text</h3></asp:Label>

                                                </div>
                                            </div>

                                            <div class="row m-0 mt-2">
                                                <div class="col-lg-12">
                                                    <asp:TextBox CssClass="form-control form-control-sm" runat="server" ID="tbTicketExtraText" MaxLength="200" autocomplete="off"
                                                        TextMode="MultiLine" ToolTip="Ticket Extra Text" placeholder="Max 200 Characters"
                                                        Text="" Style="font-size: 10pt; resize: none; height: 125px;"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajaxFTTicketExtra" runat="server" FilterType="Numbers, LowercaseLetters, UppercaseLetters, Custom"
                                                        ValidChars=".@!*#(/,+-- " TargetControlID="tbTicketExtraText" />
                                                </div>
                                            </div>
                                            <div class="row m-0 mt-2">
                                                <div class="col-lg-12 text-center">
                                                    <asp:LinkButton ID="lbtnTicketExtraTextSave" OnClick="lbtnTicketExtraTextSave_Click" runat="server" class="btn btn-success"
                                                        ToolTip="click here to Save Advertisement On Ticket text"> <i class="fa fa-save"></i> Save</asp:LinkButton>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 pl-3" style="border-left: 1px solid;">
                                            <div class="row  m-0 mt-2">
                                                <div class="col-lg-12 ">

                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h3>Current Text</h3></asp:Label>

                                                </div>
                                            </div>
                                            <div class="row m-0 mt-2">
                                                <div class="col-lg-12">
                                                    <asp:Label runat="server" ID="lblTicketExtraTextCurrent" Text=""></asp:Label>
                                                    <asp:Panel ID="pnlnoTicketExtraText" runat="server" Width="100%" Visible="true">
                                                        <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                                            <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;">
                                                                No Extra text available for ticket
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                            <div class="row m-0 mt-2">
                                                <div class="col-lg-12 text-center">
                                                    <asp:LinkButton ID="lbtnTicketExtraTextRemove" OnClick="lbtnTicketExtraTextRemove_Click" Visible="false" runat="server" Style="margin-top: 23%;" class="btn btn-warning"
                                                        ToolTip="click here to Remove Advertisement On Ticket Text"> <i class="fa fa-times" ></i> Remove Text</asp:LinkButton>

                                                </div>
                                                <div class="row m-0 mt-3 ml-3">
                                                    <asp:Label runat="server" CssClass="form-control-label text-center" Font-Bold="true"><h3>This Message will Appear At The Bottom Of Ticket</h3></asp:Label>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlWaybillTicketingType" runat="server" Visible="false">
                    <div class="row mt-1 ml-2 m-0">
                        <div class="col-lg-12 col-md-12 order-xl-1 ">
                            <div class="card" style="min-height: 400px">
                                <div class="col-lg-12 col-md-12">
                                    <div class="card-header">
                                        <div class="row m-0 align-items-center">
                                            <div class="col-md-9">
                                                <h3 class="mb-1">
                                                    <asp:Label ID="lblTicketIssueMode" runat="server" CssClass="form-control-label" Font-Bold="true"><h2>Ticket Type/Mode</h2></asp:Label>

                                                </h3>
                                            </div>
                                            <div class="col-md-3 text-right">
                                                <h4>
                                                    <asp:LinkButton ID="lbtnbilltickethelp" runat="server" OnClick="lbtnbilltickethelp_Click" ToolTip="View Instructions" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                                    </asp:LinkButton>

                                                    <asp:LinkButton ID="lbtnbusbillTicketingType" OnClick="lbtnbusbillTicketingType_Click" runat="server" ToolTip="Click here to View Ticket Type /Mode History" CssClass="btn btn bg-gradient-blue btn-sm text-white">
                                    <i class="fa fa-history"></i>
                                                    </asp:LinkButton>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-2 m-0">
                                        <div class="col-lg-6">
                                            <div class="row m-0 mt-2">
                                                <div class="col-lg-12">

                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h3>Current List</h3></asp:Label>

                                                </div>
                                            </div>
                                            <asp:GridView ID="gvTicketTypes" runat="server" AutoGenerateColumns="False"
                                                GridLines="None" AllowSorting="true" OnRowCommand="gvTicketTypes_RowCommand" AllowPaging="true" PageSize="3" class="table"
                                                DataKeyNames="modeid,modename,p_status,statusname" Font-Size="10pt">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Ticketing Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTicketTypeMode" CssClass="form-control-label" runat="server" Text='<%# Eval("modename") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSTATUSNAME" CssClass="form-control-label" runat="server" Text='<%# Eval("statusname") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Action">
                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="lbtnActiveDeactiveTicket" runat="server" CssClass="btn btn-sm btn-info"
                                                                CommandName="ActiveDeactive" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                ToolTip="click here to Active/Deactive Ticketing Type"> <i class="fa fa-toggle-on"></</asp:LinkButton>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle BackColor="#ececec" Font-Size="9pt" />
                                                <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                            </asp:GridView>
                                            <asp:Panel ID="pnlTicketNoRecord" runat="server" Width="100%" Visible="True">
                                                <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                                    <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;">
                                                        Sorry No Record Found
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>

                                        <div class="col-lg-6 " style="border-left: 1px solid;">
                                            <div class="row m-0 mt-2">
                                                <div class="col-lg-12 text-left">

                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h3>Current Value</h3></asp:Label>

                                                </div>
                                            </div>
                                            <div class="row  m-0 mt-2">
                                                <div class="col-lg-12 col-md-12">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Last Updated</asp:Label>

                                                    <asp:Label runat="server" ID="lblTicketType" CssClass="form-control-label text-warning" Font-Bold="true"></asp:Label>
                                                    <br />
                                                    <br />
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">On</asp:Label>

                                                    <asp:Label ID="lblLastUpdationOn" runat="server" CssClass="form-control-label text-warning" Font-Bold="true"></asp:Label>

                                                    &nbsp;
                                                 <br />
                                                    <br />
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"> By</asp:Label>

                                                    <asp:Label ID="lbllastUpdatedBy" runat="server" CssClass="form-control-label text-warning " Font-Bold="true"></asp:Label>

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
    </div>
    <%--Grid View With Popup Box--%>
    <div class="row">
        <cc1:ModalPopupExtender ID="mpAdvDayBHistory" runat="server" PopupControlID="PnlAdvDaysBokking"
            TargetControlID="btn1" CancelControlID="lbtn1" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="PnlAdvDaysBokking" runat="server" Style="position: fixed; margin-top: 42px!important; width: 800px;">
            <div class="card">
                <div class="card-header">
                    <div class="row m-0 align-items-center">
                        <div class="col-md-10 ">
                            <h3 class="mb-1">
                                <strong class="card-title">Advance Booking Days History</strong>
                            </h3>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="lbtnAdvancedaysbookingHistoryDownload" OnClick="lbtnAdvancedaysbookingHistoryDownload_Click" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white" Visible="false" ToolTip="Download Advance days booking History">  <i class="fa fa-download"></i> </asp:LinkButton>

                        </div>
                    </div>
                </div>
                <div class="card-body" style="padding: 15px !important;">

                    <div class="col-md-12 col-lg-12">
                        <asp:GridView ID="gvAdvDayHistory" runat="server" OnPageIndexChanging="gvAdvDayHistory_PageIndexChanging" AutoGenerateColumns="False" GridLines="None"
                            Visible="true" CssClass="table table-hover table-striped" DataKeyNames="olddays,actiondate,actionby " AllowPaging="true" PageSize="5">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Old Days">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOLDDAYS" runat="server" Text='<%# Bind("olddays") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Updation Date/Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUPDATIONDATETIME" runat="server" Text='<%# Bind("actiondate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Updated By">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUPDATEBY" runat="server" Text='<%# Bind("actionby") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#ececec" Font-Size="9pt" />
                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                        </asp:GridView>
                        <asp:Panel ID="pnlAdvDayNoRecord" runat="server" Width="100%" Visible="true">
                            <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;">
                                    Sorry No Record Found
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="card-footer">
                    <asp:LinkButton ID="lbtn1" runat="server" CssClass="btn btn-success" Style="font-size: 11pt; float: right; border-radius: 4px;"><i class="fa fa-times"></i> OK</asp:LinkButton>

                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="btn1" runat="server" Text="" />
            </div>
        </asp:Panel>

    </div>
    <div class="row">
        <cc1:ModalPopupExtender ID="mpTripChartGenerater" runat="server" PopupControlID="pnlTripChartGeneraterHistory"
            TargetControlID="btn3" CancelControlID="lbtn3" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlTripChartGeneraterHistory" runat="server" Style="position: fixed; margin-top: 42px; width: 800px;">
            <div class="card">

                <div class="card-header">
                    <div class="row m-0 align-items-center">
                        <div class="col-md-10 ">
                            <h3 class="mb-1">
                                <strong class="card-title">Trip Chart Generater/ Booking Closing Time History</strong>
                            </h3>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="lbtnDownloadTripChartGenerater" Visible="false" OnClick="lbtnDownloadTripChartGenerater_Click" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white" ToolTip="Download Trip Chart Generater History"> <i class="fa fa-download"></i></asp:LinkButton>

                        </div>
                    </div>
                </div>
                <div class="card-body" style="padding: 15px !important;">

                    <div class="col-lg-12">
                        <asp:GridView ID="gvTripChartGeneraterHistory" runat="server" OnPageIndexChanging="gvAdvBookingTimeHistory_PageIndexChanging" AutoGenerateColumns="False" GridLines="None"
                            Visible="true" CssClass="table table-striped" DataKeyNames="" AllowPaging="true" PageSize="5">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Online Booking(min.)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblADVANCEBOOKINGHRS" runat="server" Text='<%# Eval("bookingclosing_time") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Trip Chart(min.)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTRIPCHARTGENHRS" runat="server" Text='<%# Eval("trip_chart_genratetime") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Updation Date/Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUPDATIONDATE" runat="server" Text='<%# Eval("actiondate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Updated By">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUPDATEDBY" runat="server" Text='<%# Eval("actionby") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#ececec" Font-Size="9pt" />
                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                        </asp:GridView>
                        <asp:Panel ID="pnlTripChartGeneraterNoRecord" runat="server" Width="100%" Visible="true">
                            <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;">
                                    Sorry No Record Found
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="card-footer">
                    <asp:LinkButton ID="lbtn3" runat="server" CssClass="btn btn-success" Style="font-size: 11pt; float: right; border-radius: 4px;"><i class="fa fa-times"></i> OK</asp:LinkButton>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="btn3" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
    <div class="row">
        <cc1:ModalPopupExtender ID="mpSeatsExtraPaymentHistory" runat="server" PopupControlID="pnlSeatsExtraPaymentHistory"
            TargetControlID="btn4" CancelControlID="lbtn4" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlSeatsExtraPaymentHistory" runat="server" Style="position: fixed; margin-top: 42px; width: 800px;">
            <div class="card">

                <div class="card-header">
                    <div class="row m-0 align-items-center">
                        <div class="col-md-10 ">
                            <h3 class="mb-1">
                                <strong class="card-title">Extra Seats Payment History</strong>
                            </h3>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="lbtnDownloadseatsExtraPaymenthistory" Visible="false" OnClick="lbtnDownloadseatsExtraPaymenthistory_Click" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white" ToolTip="Download Extra Seats Payment History"> <i class="fa fa-download" ></i> </asp:LinkButton>

                        </div>
                    </div>
                </div>

                <div class="card-body" style="padding: 15px !important;">
                    <div class="col-lg-12">
                        <asp:GridView ID="gvSeatsExtraPaymenthistory" runat="server" OnPageIndexChanging="gvSeatsExtraPaymenthistory_PageIndexChanging" AutoGenerateColumns="False" GridLines="None"
                            Visible="true" CssClass="table table-striped" DataKeyNames="" AllowPaging="true" PageSize="5">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Seat Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpaymentcategoryname" runat="server" Text='<%# Eval("paymentcategory_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Value(%)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpaymentper" runat="server" Text='<%# Eval("amountpercentage") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Updation Date/Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblupdationdatetime" runat="server" Text='<%# Eval("actiondate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Updated By">
                                    <ItemTemplate>
                                        <asp:Label ID="lblupdatedby" runat="server" Text='<%# Eval("actionby") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#ececec" Font-Size="9pt" />
                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                        </asp:GridView>
                        <asp:Panel ID="pnlExtraSeatNoRecord" runat="server" Width="100%" Visible="true">
                            <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;">
                                    Sorry No Record Found
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="card-footer">
                    <asp:LinkButton ID="lbtn4" runat="server" CssClass="btn btn-success" Style="font-size: 11pt; float: right; border-radius: 4px;"><i class="fa fa-times"></i> OK</asp:LinkButton>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="btn4" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
    <div class="row">
        <cc1:ModalPopupExtender ID="mpPaymentGateway" runat="server" PopupControlID="pnlPmtGatewayHistory"
            TargetControlID="btn2" CancelControlID="lbtn2" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlPmtGatewayHistory" runat="server" Style="position: fixed; margin-top: 42px; width: 800px;">
            <div class="card">
                <div class="card-header">
                    <div class="row m-0 align-items-center">
                        <div class="col-md-10 ">
                            <h3 class="mb-1">
                                <strong class="card-title">Payment Gateway Addition History</strong>
                            </h3>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="lbtnPaymentGatewayStatusHistoryDownload" OnClick="lbtnPaymentGatewayStatusHistoryDownload_Click" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white" Visible="false" ToolTip="Download Payment Gateway Addition History"> <i class="fa fa-download" ></i> </asp:LinkButton>

                        </div>
                    </div>
                </div>

                <div class="card-body" style="padding: 15px !important;">

                    <div class="col-lg-12">
                        <asp:GridView ID="gvPmtstatushistory" runat="server" OnPageIndexChanging="gvPmtstatushistory_PageIndexChanging" AutoGenerateColumns="False" GridLines="None"
                            Visible="true" CssClass="table table-striped" DataKeyNames="" AllowPaging="true" PageSize="5">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Old Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOLDSTATUSNAME" runat="server" Text='<%# Eval("oldstatusname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Payment Gateway">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPMTGATEWAYNAME" runat="server" Text='<%# Eval("gatewayname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Updation Date/Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUPDATIONDATETIME" runat="server" Text='<%# Eval("actiondate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Updated By">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUPDATEBY" runat="server" Text='<%# Eval("actionby") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#ececec" Font-Size="9pt" />
                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                        </asp:GridView>
                        <asp:Panel ID="pnlPmtstatusNoRecord" runat="server" Width="100%" Visible="true">
                            <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;">
                                    Sorry No Record Found
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="card-footer">
                    <asp:LinkButton ID="lbtn2" runat="server" CssClass="btn btn-success" Style="font-size: 11pt; float: right; border-radius: 4px;">OK</asp:LinkButton>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="btn2" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
    <div class="row">
        <cc1:ModalPopupExtender ID="mpTravelerSeatAvailabilityHistory" runat="server" PopupControlID="pnlTravelerSeatAvailabilityHistory"
            TargetControlID="btn5" CancelControlID="lbtn5" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlTravelerSeatAvailabilityHistory" runat="server" Style="position: fixed; margin-top: 42px; width: 800px;">
            <div class="card">

                <div class="card-header">
                    <div class="row m-0 align-items-center">
                        <div class="col-md-10 ">
                            <h3 class="mb-1">
                                <strong class="card-title">Maximum Seat Booking at a Time History</strong>
                            </h3>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="lbtnDownloadTravelerSeatAvailability" OnClick="lbtnDownloadTravelerSeatAvailability_Click" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white" Visible="false" ToolTip="Download Maximum Seat Booking at a Time History"> <i class="fa fa-download"></i> </asp:LinkButton>

                        </div>
                    </div>
                </div>
                <div class="card-body" style="padding: 15px !important;">
                    <div class="col-lg-12">
                        <asp:GridView ID="gvTravelerSeatAvailabilityHistory" runat="server" OnPageIndexChanging="gvTravelerSeatAvailabilityHistory_PageIndexChanging" AutoGenerateColumns="False" GridLines="None"
                            Visible="true" CssClass="table table-striped" DataKeyNames="" AllowPaging="true" PageSize="5">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Service Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOLDSTATUSNAME" runat="server" Text='<%# Eval("servicetypename_en") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Old Seats">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNEWSTATUSNAME" runat="server" Text='<%# Eval("oldseats") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Updation Date/Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUPDATIONDATETIME" runat="server" Text='<%# Eval("actiondate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Updated By">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUPDATEBY" runat="server" Text='<%# Eval("actionby") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#ececec" Font-Size="9pt" />
                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                        </asp:GridView>
                        <asp:Panel ID="pnlTravelerSeatNoRecord" runat="server" Width="100%" Visible="true">
                            <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                <div class="col-md-12 busListBox" style="color: #e3e3e3; margin-top: 42px; font-size: 20px; font-weight: bold;">
                                    Sorry No Record Found
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="card-footer">
                    <asp:LinkButton ID="lbtn5" runat="server" CssClass="btn btn-success" Style="font-size: 11pt; float: right; border-radius: 4px;"><i class="fa fa-times"></i> OK</asp:LinkButton>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="btn5" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
    <div class="row">
        <cc1:ModalPopupExtender ID="mpgvHistoryTicketExtraText" runat="server" PopupControlID="pnlgvHistoryTicketExtraText"
            TargetControlID="btn6" CancelControlID="lbtn6" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlgvHistoryTicketExtraText" runat="server" Style="position: fixed; margin-top: 42px; width: 800px;">
            <div class="card">

                <div class="card-header">
                    <div class="row m-0 align-items-center">
                        <div class="col-md-10 ">
                            <h3 class="mb-1">
                                <strong class="card-title">Advertisement On Ticket History</strong>
                            </h3>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="lbtnDownloadTicketExtraText" OnClick="lbtnDownloadTicketExtraText_Click" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white" Visible="false" ToolTip="Download Advertisement On Ticket History"> <i class="fa fa-download" ></i> </asp:LinkButton>

                        </div>
                    </div>
                </div>
                <div class="card-body" style="padding: 15px !important;">
                    <div class="col-lg-12">
                        <asp:GridView ID="gvHistoryTicketExtraText" runat="server" OnPageIndexChanging="gvHistoryTicketExtraText_PageIndexChanging" AutoGenerateColumns="False" GridLines="None"
                            Visible="true" CssClass="table table-striped" DataKeyNames="adverton_ticket,actiondate,actionby" AllowPaging="true" PageSize="5">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="S.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Text" ItemStyle-Width="350px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNEWSTATUSNAME" runat="server" Text='<%# Eval("adverton_ticket") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Updation at">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUPDATIONDATETIME" runat="server" Text='<%# Eval("actiondate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Updated By">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUPDATEBY" runat="server" Text='<%# Eval("actionby") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#ececec" Font-Size="9pt" />
                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                        </asp:GridView>
                        <asp:Panel ID="pnlAdveNoRecord" runat="server" Width="100%" Visible="true">
                            <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;">
                                    Sorry No Record Found
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="card-footer">
                    <asp:LinkButton ID="lbtn6" runat="server" CssClass="btn btn-success" Style="font-size: 11pt; float: right; border-radius: 4px;"><i class="fa fa-times"></i> OK</asp:LinkButton>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="btn6" runat="server" Text="" />
            </div>
        </asp:Panel>

    </div>
    <div class="row">
        <cc1:ModalPopupExtender ID="mpTicketTypeHistory" runat="server" PopupControlID="pnlTicketTypeHistory"
            TargetControlID="btn7" CancelControlID="lbtn7" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlTicketTypeHistory" runat="server" Style="position: fixed; margin-top: 42px; width: 800px;">
            <div class="card">

                <div class="card-header">
                    <div class="row m-0 align-items-center">
                        <div class="col-md-10 ">
                            <h3 class="mb-1">
                                <strong class="card-title">Ticket Type /Mode History</strong>
                            </h3>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="lbtnDownloadWaybillTicketingType" OnClick="lbtnDownloadWaybillTicketingType_Click" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white" Visible="false" ToolTip="Download Ticket Type /Mode History"> <i class="fa fa-download" ></i> </asp:LinkButton>

                        </div>
                    </div>
                </div>
                <div class="card-body" style="padding: 15px !important;">
                    <div class="col-lg-12">
                        <asp:GridView ID="gvTicketTypeHistory" OnPageIndexChanging="gvTicketTypeHistory_PageIndexChanging" runat="server" AutoGenerateColumns="False"
                            GridLines="None" AllowSorting="true" AllowPaging="true" PageSize="5" class="table"
                            DataKeyNames="" Font-Size="8pt">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Ticketing Type">
                                    <ItemTemplate>

                                        <asp:Label ID="lblPMTGATEWAYNAME" runat="server" Text='<%# Eval("modename") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOLDSTATUSNAME" runat="server" Text='<%# Eval("statusname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Updation Date/Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUPDATIONDATETIME" runat="server" Text='<%# Eval("actiondate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Updated By">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUPDATEBY" runat="server" Text='<%# Eval("actionby") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#ececec" Font-Size="9pt" />
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                        <asp:Panel ID="pnlTicketIssueNoRecord" runat="server" Width="100%" Visible="true">
                            <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;">
                                    Sorry No Record Found
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="card-footer">
                    <asp:LinkButton ID="lbtn7" runat="server" CssClass="btn btn-success" Style="font-size: 11pt; float: right; border-radius: 4px;"><i class="fa fa-times"></i> OK</asp:LinkButton>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="btn7" runat="server" Text="" />
            </div>
        </asp:Panel>

    </div>
    <%--Confirmation box--%>
    <div class="row">
        <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
            CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed; display: none;">
            <div class="card" style="min-width: 350px;">
                <div class="card-header">
                    <h4 class="card-title">Please Confirm
                    </h4>
                </div>
                <div class="card-body" style="min-height: 100px;">
                    <asp:Label ID="lblConfirmation" runat="server"></asp:Label>
                    <div style="width: 100%; margin-top: 20px; text-align: right;">
                        <asp:LinkButton ID="lbtnYesConfirmation" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbtnYesConfirmation_Click"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                        <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-3"> <i class="fa fa-times"></i> No </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button4" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>

