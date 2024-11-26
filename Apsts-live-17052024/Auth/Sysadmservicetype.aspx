<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="Sysadmservicetype.aspx.cs" Inherits="Auth_Sysadmservicetype" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../assets/multiSelect/example-styles.css" rel="stylesheet" />

    <script src="../assets/multiSelect/jquery.multi-select.js"></script>
    <script src="../assets/multiSelect/jquery.multi-select.min.js"></script>
    <style>
        .headerCss {
            color: #8898aa;
            border-color: #e9ecef;
            background-color: #f6f9fc;
            text-align: center;
            font-weight: bold;
        }

        .table mar {
            margin-left: 40px;
        }

        .form-control-label1 {
            font-size: .75rem;
            font-weight: 600;
            color: white;
        }
    </style>
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
    <script type="text/javascript">
        $(function () {
            $('#<%= ddlAmenities.ClientID %>').multiSelect();
        });
    </script>



    <script>
        $(document).ready(function () {

            var currDate = new Date().getDate();
            var preDate = new Date(new Date().setDate(currDate - 1));
            var todayDate = new Date(new Date().setDate(currDate));
            var FutureDate = new Date(new Date().setDate(currDate + 30000));


            $('[id*=txtSTOCEffectiveDate]').datepicker({
                endDate: FutureDate,

                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            })

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="header pb-7">
    </div>
    <div class="container-fluid mt--6">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <asp:Panel ID="pnlService" runat="server" Visible="true">
            <div class="row align-items-center">
                <div class="col-md-12 ">
                    <div class="card card-stats mb-3">
                        <div class="row m-0">
                            <div class="col-md-4 border-right">
                                <div class="card-body">
                                    <div class="row m-0">
                                        <div class="col-12">
                                            <div class=" text-capitalize" style="font-size: medium; font-weight: bold">
                                                <asp:Label ID="lblSummary" runat="server" class="h4 font-weight-bold mb-0"> Summary as on Date </asp:Label>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col-md-4 col-lg-4 border-right">
                                                    <h5 class="card-title text-uppercase mb-0">Total Service:&nbsp;
								 <asp:Label ID="lbltotalservice" runat="server" ToolTip="Total Services Available" CssClass="font-weight-bold mb-0 text-right"></asp:Label></h5>
                                                </div>
                                                <div class="col-lg-4 col-md-4 border-right">
                                                    <h5 class="card-title text-uppercase mb-0">Active:&nbsp;
								 <asp:Label ID="lblactiveservice" runat="server" ToolTip="Active Services" class="font-weight-bold mb-0 text-right"></asp:Label></h5>
                                                </div>
                                                <div class="col-lg-4 col-md-4">
                                                    <h5 class="card-title text-uppercase mb-0">Discontinued: &nbsp;
								<asp:Label ID="lbldiscontinuedservice" runat="server" ToolTip=" Services discontinued" CssClass="font-weight-bold mb-0 text-right"></asp:Label></h5>
                                                </div>


                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-4 border-right">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col">

                                            <div class="input-group-prepend">
                                                <h4 class="mb-1">Download Bus Service Type Report</h4>
                                                  <asp:LinkButton ID="lbtndownloadReport" runat="server" ToolTip="Download Office Report" OnClick="lbtndownloadReport_Click"
                                                    CssClass="btn btn bg-gradient-green btn-sm text-white ml-1">
											 <i class="fa fa-download "></i>
                                                </asp:LinkButton>
                                            </div>
                                           
                                          
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col">
                                            <div>
                                                <h4 class="mb-1">Instructions</h4>
                                            </div>
                                            <label class="form-control-label">Instruction 1 </label>
                                            <br />
                                            <label class="form-control-label">Instruction 2</label>


                                        </div>
                                        <div class="col-auto">
                                            <asp:LinkButton ID="lbtnview" runat="server" ToolTip="View Instructions" OnClick="lbtnview_Click" CssClass="btn btn bg-gradient-orange btn-sm text-white">
									<i class="fa fa-eye"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbtndownload" runat="server" OnClick="lbtndownload_Click" ToolTip="Download user manual" CssClass="btn btn bg-gradient-green btn-sm text-white">
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
        </asp:Panel>

        <div class="row">
            <div class="col-lg-4 col-md-6 order-xl-1">
                <div class="card mb-3" style="min-height: 500px;">
                    <div class="card-header border-bottom">
                        <asp:Label runat="server" Visible="true" Text="Service Type List" Font-Size="Large" CssClass="text-left"></asp:Label>
                    </div>
                    <div class="card-body ">
                        <div class="col-lg-12 col-md-12 order-xl-2">
                            <asp:GridView ID="gvServiceType" runat="server" OnRowDataBound="gvServiceType_RowDataBound" AutoGenerateColumns="false" CssClass="w-100"
                                GridLines="None" ShowHeader="false" DataKeyNames="srtpid,servicetype_name_en,servicetype_name_hi,acscharge_pkm,heatscharge_pkm,speedhill_kmh,speedplain_kmh,statusa,luggagerate,luggmul_unit,luggwith_psngr,incentivedriver,incentiveconductor,servicetax,statusflag,luggwithpsngr_flag,onlinereservationchargea,amenitiesa,des_cription,img_web,img_app,s_gst,c_gst,i_gst,ag_comm"
                                OnRowCommand="gvServiceType_RowCommand" HeaderStyle-CssClass="thead-light font-weight-bold" OnPageIndexChanging="gvServiceType_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>

                                            <div class="card p-1 mb-0">
                                                <div class="row px-3 pt-2">
                                                    <div class="col-2">
                                                        <asp:Image ID="img" runat="server" CssClass="img-thumbnail rounded-circle w-75" />
                                                    </div>
                                                    <div class="col">
                                                        <h3 class="text-uppercase mb-0 font-weight-bold text-sm">
                                                            <%# Eval("servicetype_name_en") %></h3>
                                                        <h5 class="font-weight-normal"><span class="text-gray">Hill <%# Eval("speedhill_kmh") %> KM/Hour</span> <span class="text-gray ml-3">Plain <%# Eval("speedplain_kmh") %> KM/Hour</span></h5>

                                                    </div>

                                                    <div class="col-right">
                                                        <asp:LinkButton ID="lbtnUpdateServiceType" Visible="true" runat="server" CommandName="UpdateServiceType" CssClass="btn btn-sm btn-warning" Style="border-radius: 4px;" ToolTip="Update Service Type"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnActiveDeactive" Visible="true" runat="server" CommandName="ActiveYN" CssClass="btn btn-sm btn-success" Style="border-radius: 4px;" ToolTip="Activate Service Type"> <i class="fa fa-toggle-on"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnDeactivate" Visible="false" runat="server" CommandName="ActiveYN" CssClass="btn btn-sm btn-danger" Style="border-radius: 4px;" ToolTip="Deactivate Service Type"> <i class="fa fa-toggle-off"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnAddCharges" Visible="true" runat="server" CommandName="AddCharges" CssClass="btn btn-sm btn-primary" Style="border-radius: 4px;" ToolTip="Add Other Charges"> <i class="fa fa-plus"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Columns>
                                <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-8 col-md-6 order-xl-2">
                <div class="card" style="min-height: 500px">

                    <asp:Panel runat="server" ID="pnlAddServiceType" Visible="false">
                        <div class="row  card-header">
                            <div class="col-md-6 col-lg-6 text-left!important">
                                <asp:Label ID="LabelHeader" runat="server" Visible="true" Text="Add New Service Type" Font-Size="Large" CssClass="text-left"></asp:Label>
                                <asp:Label ID="LabelHeaderUpdate" runat="server" Visible="false" Text="Update Service Type" Font-Size="large" CssClass="text-left"></asp:Label>
                            </div>
                            <div class="col-md-6 text-right">
                                <span style="color: red">All Fields are mandatory   *

                                </span>
                            </div>
                        </div>


                        <div class="row mx-2">
                            <div class="col-md-12 col-lg-12">

                                <asp:Label ID="lblServiceName" runat="server" CssClass="form-control-label ml--2" Font-Bold="true" Font-Size="small">1. Service Type Name</asp:Label>
                                <br />

                            </div>


                            <div class="col-lg-12 col-md-12">
                                <div class="row">
                                    <div class="col-lg-6 col-md-6">
                                        <asp:Label runat="server" CssClass="form-control-label"> English</asp:Label>
                                        <span class="text-warning">*</span>
                                        <br />
                                        <asp:TextBox Autocomplete="off" CssClass="form-control form-control-sm" runat="server" ID="tbServiceTypeNameEn" MaxLength="50" ToolTip="Service Type Name in English"
                                            placeholder="Max 50 Characters" Text="" Style=""></asp:TextBox>
                                    </div>
                                    <div class="col-lg-6 col-md-6">
                                        <asp:Label runat="server" CssClass="form-control-label"> Local Language</asp:Label>
                                        <br />
                                        <asp:TextBox Autocomplete="off" class="form-control form-control-sm" runat="server" ID="tbServiceTypeNameHn" MaxLength="50" ToolTip="Service type in Local Language"
                                            placeholder="Max 50 Characters" Text="" Style=""></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row mx-2 mt-2">
                            <div class="col-lg-6 col-md-6">

                                <asp:Label ID="lblSpeed" runat="server" CssClass="form-control-label ml--2" Font-Bold="true" Font-Size="small">2. Speed</asp:Label>

                            </div>
                            <div class="col-lg-6 col-md-6">

                                <asp:Label ID="lblSurcharge" runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="small">3. Surcharge (Per Km)</asp:Label>
                                <br />

                            </div>

                            <div class="col-lg-12 col-md-12">
                                <div class="row">
                                    <div class="col-lg-3 col-lg-3">
                                        <asp:Label runat="server" CssClass="form-control-label">Hill (Km/hr)</asp:Label><span class="text-warning">*</span>
                                        <br />
                                        <asp:TextBox Autocomplete="off" CssClass="form-control form-control-sm" runat="server" ID="tbServiceTypeSpeedHill" MaxLength="2" ToolTip="Speed Hill in Kilometer per Hour"
                                            placeholder="Only number" Text=""></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ajxFtSpeedHill" runat="server" FilterType="Numbers"
                                            TargetControlID="tbServiceTypeSpeedHill" />
                                    </div>
                                    <div class="col-lg-3 col-md-3">
                                        <asp:Label runat="server" CssClass="form-control-label">
										Plain (Km/hr)</asp:Label><span class="text-warning">*</span>
                                        <br />
                                        <asp:TextBox Autocomplete="off" CssClass="form-control form-control-sm" runat="server" ID="tbServiceTypeSpeedPlain" MaxLength="2" ToolTip="Speed Plain in Kilometer per Hour"
                                            placeholder="Only number" Text=""></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ajxFtSpeedPlain" runat="server" FilterType="Numbers"
                                            TargetControlID="tbServiceTypeSpeedPlain" />
                                    </div>
                                    <div class="col-lg-3 col-md-3">
                                        <asp:Label runat="server" CssClass="form-control-label"> 
										Heating (₹)</asp:Label><span class="text-warning">*</span>
                                        <br />
                                        <asp:TextBox Autocomplete="off" CssClass="form-control form-control-sm" runat="server" ID="tbServiceTypeHeatingSurcharge" ToolTip="Service Type Heating Surcharges"
                                            MaxLength="5" placeholder="Only number" Text=""></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ajxFtHeating" runat="server" FilterType="Numbers,custom"
                                            ValidChars="." TargetControlID="tbServiceTypeHeatingSurcharge" />
                                    </div>
                                    <div class="col-lg-3 col-md-3">
                                        <asp:Label runat="server" CssClass="form-control-label">AC (₹)</asp:Label><span class="text-warning">*</span>
                                        <br />
                                        <asp:TextBox Autocomplete="off" CssClass="form-control form-control-sm" runat="server" ID="tbServiceTypeACSurchargeperkm" ToolTip="Service Type AC Surcharges"
                                            MaxLength="5" placeholder="Only number" Text=""></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ajxFtAC" runat="server" FilterType="Numbers,custom"
                                            ValidChars="." TargetControlID="tbServiceTypeACSurchargeperkm" />
                                    </div>
                                </div>

                            </div>




                        </div>

                        <div class="row mx-2 mt-2">
                            <div class="col-lg-12 col-md-12">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12">
                                        <asp:Label ID="Label1" runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="small">4. For Online</asp:Label>
                                        <br />

                                    </div>
                                    <div class="col-lg-4 col-md-4">
                                        <asp:Label runat="server" CssClass="form-control-label"> 
									   Online Reservation Charge(₹)</asp:Label>
                                        <br />
                                        <asp:TextBox Autocomplete="off" CssClass="form-control form-control-sm" runat="server" ID="tbOnlineReservationcharge" ToolTip="Online Reservation Charges"
                                            MaxLength="5" placeholder="Only number" Text=""></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ajxFtOnlineReservation" runat="server" FilterType="Numbers,custom"
                                            ValidChars="." TargetControlID="tbOnlineReservationcharge" />
                                    </div>
                                    <div class="col-lg-4 col-md-4">
                                        <asp:Label runat="server" CssClass="form-control-label"> 
									   Agent Commission(₹)</asp:Label>
                                        <br />
                                        <asp:TextBox Autocomplete="off" CssClass="form-control form-control-sm" runat="server" ID="tbagentcommission" ToolTip="Online Reservation Charges"
                                            MaxLength="5" placeholder="Only number" Text="0"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers,custom"
                                            ValidChars="." TargetControlID="tbagentcommission" />
                                    </div>
                                    <div class="col-lg-4 col-md-4">
                                        <asp:Label runat="server" CssClass="form-control-label">Amenities</asp:Label>
                                        <br />
                                        <asp:ListBox ID="ddlAmenities" runat="server" ToolTip="Select amenities" Style="width: 50%;" SelectionMode="Multiple" CssClass="form-control"></asp:ListBox>

                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="row mx-2 mt-2">
                            <div class="col-lg-12 col-md-12">
                                <div class="row">
                                    <div class="col-lg-4 col-md-4">
                                        <asp:Label ID="lblOther" runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="small">5. Other</asp:Label>

                                        <br />


                                        <asp:Label runat="server" CssClass="form-control-label">
										Service Tax (%)</asp:Label>
                                        <br />
                                        <asp:TextBox Autocomplete="off" CssClass="form-control form-control-sm" runat="server" ID="tbServiceTypeservicetax" MaxLength="5" ToolTip="Service Tax"
                                            placeholder="Only number" Text=""></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ajxFtServiceTax" runat="server" FilterType="Numbers,custom"
                                            ValidChars="." TargetControlID="tbServiceTypeservicetax" />
                                    </div>
                                    <div class="col-lg-6 col-md-6">
                                        <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="small">6. Luggage
													Allowed</asp:Label>
                                        <asp:CheckBox ID="cbServiceTypeLuggageWithPassenger" ToolTip="If luggage allowed" OnCheckedChanged="chkServiceTypeLuggageWithPassenger_CheckedChanged" runat="server" AutoPostBack="true" />&nbsp;&nbsp;
									 
								
								<br />
                                        <asp:Panel ID="PanelLuggage" runat="server" CssClass="col-lg-12" Visible="false">
                                            <div class="row">
                                                <div class="col-lg-6 col-md-6">
                                                    <asp:Label runat="server" CssClass="form-control-label">  Luggage Rate</asp:Label>
                                                    <br />
                                                    <asp:TextBox Autocomplete="off" CssClass="form-control form-control-sm" runat="server" ID="tbServiceTypeLuggageRate" MaxLength="2" ToolTip="Luggage Rate"
                                                        placeholder="Only Number" Text="0"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajxFtLuggageRate" runat="server" FilterType="Numbers"
                                                        TargetControlID="tbServiceTypeLuggageRate" />
                                                </div>
                                                <div class="col-lg-6 col-md-6 p-0">
                                                    <asp:Label runat="server" CssClass="form-control-label">
												Luggage Multiple Unit</asp:Label>
                                                    <br />
                                                    <asp:TextBox Autocomplete="off" CssClass="form-control form-control-sm" runat="server" ID="tbServiceTypeLuggageMultipleUnit" ToolTip="Luggage Multiple Unit"
                                                        MaxLength="2" placeholder="Only Number" Text="0"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajxFtLuggageMulti" runat="server" FilterType="Numbers"
                                                        TargetControlID="tbServiceTypeLuggageMultipleUnit" />
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mx-2 mt-2">
                            <div class="col-lg-6 col-md-6">
                                <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="small">7. Tax (%)</asp:Label>

                            </div>
                        </div>
                        <div class="row mx-2">


                            <div class="col-lg-3 col-md-3">
                                <asp:Label runat="server" CssClass="form-control-label"> 
										SGST</asp:Label>
                                <br />
                                <asp:TextBox Autocomplete="off" CssClass="form-control form-control-sm" runat="server" ID="tbSgst" ToolTip="State Goods and Services Tax"
                                    MaxLength="2" placeholder="Only number" Text=""></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                    ValidChars="." TargetControlID="tbSgst" />
                            </div>

                            <div class="col-lg-3 col-md-3">
                                <asp:Label runat="server" CssClass="form-control-label"> 
										CGST</asp:Label>
                                <br />
                                <asp:TextBox Autocomplete="off" CssClass="form-control form-control-sm" runat="server" ID="tbCGST" ToolTip="Central Goods and Service Tax"
                                    MaxLength="2" placeholder="Only number" Text=""></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" ValidChars="." TargetControlID="tbCGST" />
                            </div>


                            <div class="col-lg-3 col-md-3">
                                <asp:Label runat="server" CssClass="form-control-label"> 
										IGST</asp:Label>
                                <br />
                                <asp:TextBox Autocomplete="off" CssClass="form-control form-control-sm" runat="server" ID="tbigst" ToolTip="Integrated goods and services tax"
                                    MaxLength="2" placeholder="Only number" Text=""></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" ValidChars="." TargetControlID="tbigst" />
                            </div>


                        </div>

                        <div class="row mx-2 mt-2">
                            <div class="col-lg-6 col-md-6">
                                <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="small">8. Incentives (%)</asp:Label>
                                <br />

                            </div>
                            <div class="col-lg-6 col-md-6">

                                <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="small">9. ETM max seat</asp:Label>
                                <br />

                            </div>


                            <div class="col-lg-3 col-md-3">
                                <asp:Label runat="server" CssClass="form-control-label"> 
										Driver</asp:Label>
                                <br />
                                <asp:TextBox Autocomplete="off" CssClass="form-control form-control-sm" runat="server" ID="tbDriverIncentives" ToolTip="Driver Incentive in Percentage"
                                    MaxLength="5" placeholder="Only number" Text=""></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="ajxFtDriverIncentives" runat="server" FilterType="Numbers,custom"
                                    ValidChars="." TargetControlID="tbDriverIncentives" />
                            </div>
                            <div class="col-lg-3 col-md-3">
                                <asp:Label runat="server" CssClass="form-control-label"> 
									   Conductor</asp:Label>
                                <br />
                                <asp:TextBox Autocomplete="off" CssClass="form-control form-control-sm" runat="server" ID="tbConductorIncentives" ToolTip="Conductor Incentive in Percentage"
                                    MaxLength="5" placeholder="Only number" Text=""></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="ajxFtConductorIncentives" runat="server" FilterType="Numbers,custom"
                                    ValidChars="." TargetControlID="tbConductorIncentives" />
                            </div>
                            <div class="col-lg-3 col-md-3">
                                <asp:Label runat="server" CssClass="form-control-label"> 
										Adult</asp:Label>
                                <br />
                                <asp:TextBox Autocomplete="off" CssClass="form-control form-control-sm" runat="server" ID="tbAdult" ToolTip="Adult Maximum Seat"
                                    MaxLength="5" placeholder="Only number" Text=""></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="ajxFtAdult" runat="server" FilterType="Numbers,custom"
                                    ValidChars="." TargetControlID="tbAdult" />
                            </div>
                            <div class="col-lg-3 col-md-3">
                                <asp:Label runat="server" CssClass="form-control-label"> 
									   Child</asp:Label>
                                <br />
                                <asp:TextBox Autocomplete="off" CssClass="form-control form-control-sm" runat="server" ID="tbChild" ToolTip="Child Maximum Seat"
                                    MaxLength="5" placeholder="Only number" Text=""></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="ajxFtChild" runat="server" FilterType="Numbers,custom"
                                    ValidChars="." TargetControlID="tbChild" />
                            </div>
                        </div>

                        <div class="row mx-2  mt-2">
                            <div class="col-lg-12 col-md-12">
                                <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="small">10. Images</asp:Label>
                                <br />
                                <div class="row mt-2 m-0">

                                    <div class="col-md-6 col-lg-6">
                                        <asp:Label runat="server" CssClass="form-control-label">For Web Portal<span class="text-warning">*</span>  </asp:Label><br />

                                        <asp:Button ToolTip="Upload Web Portal Image" ID="btnUploadWebPortal" OnClick="btnUploadWebPortal_Click" runat="server" CausesValidation="False" CssClass="form-control form-control-sm"
                                            Style="display: none" TabIndex="18" Text="Upload Image" accept=".png,.jpg,.jpeg,.gif" Width="80px" />
                                        <asp:FileUpload ID="FileWebPortal" onchange="UploadImageWeb(this);" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-sm btn-success" TabIndex="9" />
                                        <asp:Image ID="ImgWebPortal" onchange="UploadImageWeb(this);" runat="server" Visible="false" Style="border-width: 0px; height: 50px; width: 50px; border: 2px solid #eaf4ff;" />
                                        <asp:LinkButton ID="lbtncloseWebImage" runat="server" OnClick="lbtncloseWebImage_Click" Style="font-size: 5pt; border-radius: 25px; margin-bottom: 22pt" Visible="false" CssClass="btn btn-sm btn-danger"><i class="fa fa-times"></i></asp:LinkButton>
                                        <br />
                                        <label id="lblwrongimage" runat="server" style="font-size: 7pt; color: Red; line-height: 15px;">
                                            Image size will be less then 1 MB<span style="color: #ea5d46; font-size: 7pt">(Max 20Kb/ 600*300 Pixel)</span><br />
                                            (Only .JPG, .PNG, .JPEG)</label>
                                        <br />
                                    </div>
                                    <div class="col-md-6 col-lg-6">
                                        <asp:Label runat="server" CssClass="form-control-label">For Mobile App<span class="text-warning">*</span>  </asp:Label><br />
                                        <asp:Button ToolTip="Upload Mobile App image" ID="btnUploadMobileApp" OnClick="btnUploadMobileApp_Click" runat="server"
                                            CausesValidation="False" CssClass="file-upload-inner" accept=".png,.jpg,.jpeg,.gif"
                                            Style="display: none" Text="Upload Image" Width="80px" />
                                        <asp:FileUpload ID="FileMobileApp" onchange="UploadImageMob(this);" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-sm btn-success" TabIndex="9" />
                                        <asp:Image ID="imgMobileApp" onchange="UploadImageMob(this);" runat="server" Visible="false" Style="border-width: 0px; height: 50px; width: 50px; border: 2px solid #eaf4ff;" />
                                        <asp:LinkButton ID="lbtncloseMobileImage" OnClick="lbtncloseMobileImage_Click" runat="server" Style="font-size: 5pt; border-radius: 25px; margin-bottom: 22pt" Visible="false" CssClass="btn btn-sm btn-danger"><i class="fa fa-times"></i></asp:LinkButton>
                                        <br />
                                        <label id="lblwrongimageMob" runat="server" style="font-size: 7pt; color: Red; line-height: 15px;">
                                            Image size will be less then 1 MB<span style="color: #ea5d46; font-size: 7pt">(Max 10Kb/ 155*18 Pixel)</span><br />
                                            (Only .JPG, .PNG, .JPEG)</label><br />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-1 mx-2  mt-2">
                            <div class="col-lg-12 col-md-12">
                                <%--                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="small">10. Description<span style="color: red">*</span></asp:Label>--%>
                                <div class="col-md-12 col-lg-12">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="small">11. Description</asp:Label>
                                    <asp:TextBox ID="tbDescription" class="form-control form-control-sm" TextMode="MultiLine" runat="server" MaxLength="200" ToolTip="Enter Description here" autocomplete="off"
                                        placeholder="Max 200 Characters.." Text="" Style=""></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row mb-3 mt-4">
                            <div class="col-lg-12 col-md-12 text-center pb-5">

                                <asp:LinkButton ID="lbtnUpdate" runat="server" class="btn btn-primary" Visible="false" OnClick="lbtnUpdate_Click" ToolTip="Update Service Type">
									<i class="fa fa-save"></i> Update</asp:LinkButton>
                                <asp:LinkButton ID="lbtnSave" Visible="true" runat="server" class="btn btn-success" OnClick="lbtnSave_Click">
									<i class="fa fa-save"></i> Save</asp:LinkButton>

                                <asp:LinkButton ID="lbtnReset" runat="server" OnClick="lbtnReset_Click" CssClass="btn btn-danger" ToolTip="Save Service Type">
									<i class="fa fa-undo"></i> Reset</asp:LinkButton>
                                <asp:LinkButton ID="lbtncancel" Visible="false" runat="server" class="btn btn-warning" ToolTip="Reset Service type" OnClick="lbtnCancel_Click">
									<i class="fa fa-times"></i> Cancel</asp:LinkButton>

                            </div>
                        </div>

                    </asp:Panel>
                    <asp:Panel ID="PanelAddOtherCharges" runat="server" Visible="false" Style="box-shadow: 2px 4px 30px 40px #596166c2; padding: 10px;">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="row mt-2">
                                    <div class="col-lg-8">
                                        <span style="font-size: 16px;">Add Other Charges in below Service Type<br />
                                            <asp:Label ID="LabelOtherChargesServiceTypeName" Font-Bold="true" runat="server"
                                                Text=""></asp:Label></span>
                                        <asp:Label ID="LabelOtherChargesServiceTypeId" runat="server" Text="" Visible="false"></asp:Label>
                                    </div>
                                    <div class="col-lg-4 text-right">
                                        <asp:LinkButton ID="LinkButtonPanelAddOtherChargesCancel" OnClick="LinkButtonPanelAddOtherChargesCancel_Click" runat="server" class="btn btn-danger"
                                            Style="border-radius: 4px; height: 32px; padding-top: 2px;"> <i class="fa fa-times" ></i> Close</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row mt-3">
                                    <div class="col-lg-12">
                                        <div class="row">
                                            <div class="col-lg-3">
                                                Charge
                                                                <br />
                                                <asp:DropDownList ID="ddlSTOCCharge" runat="server" class="form-control" Style="padding: 2px 5px;">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-2">
                                                Fare Type<br />
                                                <asp:DropDownList ID="ddlSTOCFareType" runat="server" class="form-control" Style="padding: 2px 5px;">
                                                    <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Per Km" Value="P"></asp:ListItem>
                                                    <asp:ListItem Text="Slab" Value="S"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-2">
                                                From (km)<br />
                                                <asp:TextBox class="form-control" runat="server" ID="txtSTOCFromKM" MaxLength="4"
                                                    autocomplete="off" placeholder="Max 4 Char" Text=""></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtSTOCFromKM" />
                                            </div>
                                            <div class="col-lg-2">
                                                To (km)<br />
                                                <asp:TextBox class="form-control" runat="server" ID="txtSTOCToKM" MaxLength="4" autocomplete="off"
                                                    placeholder="Max 4 Char" Text=""></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtSTOCToKM" />
                                            </div>
                                            <div class="col-lg-2">
                                                Charge Amount (<i class="fa fa-rupee"></i>)<br />
                                                <asp:TextBox class="form-control" runat="server" ID="txtSTOCChargeAmount" MaxLength="4"
                                                    autocomplete="off" placeholder="Max 4 Char" Text=""></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" runat="server" FilterType="Numbers,custom"
                                                    ValidChars="." TargetControlID="txtSTOCChargeAmount" />
                                            </div>
                                        </div>
                                        <div class="row mt-4">
                                            <div class="col-lg-3">
                                                Effective Date<br />
                                                <div class="input-group clockpicker-with-callbacks">
                                                    <asp:TextBox class="form-control" runat="server" ID="txtSTOCEffectiveDate" MaxLength="10"
                                                        autocomplete="off" placeholder="DD/MM/YYYY" Text=""></asp:TextBox>
                                                 
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row mt-4">
                                            <div class="col-lg-12 pb-2">
                                                <center>
                                                                    <asp:LinkButton ID="lbtnSaveOtherCharges" runat="server" class="btn btn-success"
                                                                        Style="border-radius: 4px;" OnClick="lbtnSaveOtherCharges_Click" > <i class="fa fa-floppy-o" ></i> Save</asp:LinkButton>
                                                                    <asp:LinkButton ID="lbtnUpdateOtherCharges" runat="server" Visible="false" class="btn btn-success" Style="border-radius: 4px;"
                                                                      OnClick="lbtnUpdateOtherCharges_Click"> <i class="fa fa-edit" ></i> Update</asp:LinkButton>
                                                                    <asp:LinkButton ID="lbtnResetOtherCharges" runat="server" class="btn btn-danger ml-3"
                                                                        Style="border-radius: 4px;" OnClick="lbtnResetOtherCharges_Click"> <i class="fa fa-refresh" ></i> Reset</asp:LinkButton>
                                                                </center>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row text-left">
                                    <div class="col-md-12 mt-4 text-left">
                                        <asp:Panel ID="pnlNoRecord_otherCharge" runat="server" Width="100%" Visible="true">
                                            <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                                <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
                                                    Charges List not available<br />
                                                    Please add Other Charge
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:GridView ID="grdServiceTypeOtherCharge" runat="server" AutoGenerateColumns="false" OnRowCommand="grdServiceTypeOtherCharge_RowCommand"
                                            GridLines="None" CssClass="table table-bordered" DataKeyNames="stocid,chargeid,faretype,fromkm,tokm,amt,effdate,srtpid,chargename,fare,servicename">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Charge">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcharge_name" runat="server" Text='<%#Eval("chargename") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblservice_type_name_en" runat="server" Text='<%#Eval("servicename") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fare Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFARE_TYPE_FLAG" runat="server" Text='<%#Eval("fare") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="From (KM)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFR_KM" runat="server" Text='<%#Eval("fromkm") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To (Km)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTO_KM" runat="server" Text='<%#Eval("tokm") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Charge Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCHARGE_AMT" runat="server" Text='<%#Eval("amt") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Effective Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEFF_FROM_DT" runat="server" Text='<%#Eval("effdate") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton2" CommandName="updatecharge" 
                                                            Visible="true" runat="server" CssClass="btn btn-sm btn-warning" Style="border-radius: 4px;"
                                                            ToolTip="Update State"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" CssClass="GridPager" ForeColor="Black" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </asp:Panel>


                </div>
            </div>
        </div>
    </div>



    <%-- //PopUp--%>
    <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
        CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
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




</asp:Content>

