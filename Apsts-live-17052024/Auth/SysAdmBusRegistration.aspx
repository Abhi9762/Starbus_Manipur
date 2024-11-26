<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="SysAdmBusRegistration.aspx.cs" Inherits="Auth_SysAdmBusRegistration" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function UploadInvoice(fileUpload) {
            if ($('#ImgInvoice').value != '') {
                document.getElementById("<%=btnUploadInvoice.ClientID %>").click();
            }
		}
		$(document).ready(function () {

			var currDate = new Date().getDate();
			var preDate = new Date(new Date().setDate(currDate - 1));
			var todayDate = new Date(new Date().setDate(currDate));
			//Invoice Dates
			$('[id*=tbInvoiceDate]').datepicker({
				endDate: todayDate,
				changeMonth: true,
				changeYear: false,
				format: "dd/mm/yyyy",
				autoclose: true
			})
				.on('changeDate', function (selected) {
					var minDate = new Date(selected.date.valueOf());
					$('[id*=tbRecDate]').datepicker('setStartDate', minDate);
				});
			$('[id*=tbRecDate]').datepicker({
				endDate: todayDate,
				changeMonth: true,
				changeYear: false,
				format: "dd/mm/yyyy",
				autoclose: true
			});
			//Permit Dates
			$('[id*=tbValidTo]').datepicker({
				changeMonth: true,
				changeYear: false,
				format: "dd/mm/yyyy",
				autoclose: true
			});
			$('[id*=tbValidFrom]').datepicker({
				changeYear: false,
				format: "dd/mm/yyyy",
				autoclose: true
			})
				.on('changeDate', function (selected) {
					var minDate = new Date(selected.date.valueOf());
					$('[id*=tbValidTo]').datepicker('setStartDate', minDate);
				});
			//Insurance Dates
			$('[id*=tbValiduptoInsurance]').datepicker({
				changeMonth: true,
				changeYear: false,
				format: "dd/mm/yyyy",
				autoclose: true
			});
			//Fitness Dates
			$('[id*=tbValidtoFitness]').datepicker({
				changeMonth: true,
				changeYear: false,
				format: "dd/mm/yyyy",
				autoclose: true
			});
			$('[id*=tbValidFromFitness]').datepicker({
				changeYear: false,
				format: "dd/mm/yyyy",
				autoclose: true
			})
				.on('changeDate', function (selected) {
					var minDate = new Date(selected.date.valueOf());
					$('[id*=tbValidtoFitness]').datepicker('setStartDate', minDate);
				});
			//Pollution Dates
			$('[id*=tbValidtoPollution]').datepicker({
				changeMonth: true,
				changeYear: false,
				format: "dd/mm/yyyy",
				autoclose: true
			});
			$('[id*=tbValidFromPollution]').datepicker({
				changeYear: false,
				format: "dd/mm/yyyy",
				autoclose: true
			})
				.on('changeDate', function (selected) {
					var minDate = new Date(selected.date.valueOf());
					$('[id*=tbValidtoPollution]').datepicker('setStartDate', minDate);
				});
		});
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid pt-2">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="row">
                        <div class="col-3 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h4 class="mb-1">
                                            <asp:Label ID="lblSummary" runat="server" CssClass="text-capitalize">Summary as on Date 01/12/2021 04:00pm</asp:Label></h4>
                                        <div class="row m-0">
                                            <div class="col-6 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Total Buses:&nbsp;
                                 <asp:Label ID="lblTotalBus" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Buses" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Active: &nbsp;
                                <asp:Label ID="lblActivateBus" runat="server" data-toggle="tooltip" data-placement="bottom" title="Active Buses" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Discontinue:&nbsp;
                                 <asp:Label ID="lblDeactBus" runat="server" data-toggle="tooltip" data-placement="bottom" title="Deactive Buses" class="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="col-3 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h4 class="mb-1">
                                            <asp:Label ID="Label1" runat="server" CssClass="text-capitalize">Pending Details</asp:Label></h4>
                                        <div class="row m-0">
                                            <div class="col-6 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Permit:&nbsp;
                                 <asp:Label ID="lbtnPermitPending" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Buses" CssClass="h3 font-weight-bold mb-0 text-right float-right text-info" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Fitness:&nbsp;
                                 <asp:Label ID="lbtnFitnessPending" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Buses" CssClass="h3 font-weight-bold mb-0 text-right float-right text-info" Text="0"></asp:Label></h5>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Pollution: &nbsp;
                                <asp:Label ID="lbtnPollutionPending" runat="server" data-toggle="tooltip" data-placement="bottom" title="Active Buses" CssClass="h3 font-weight-bold mb-0 text-right float-right text-info" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Insurance:&nbsp;
                                 <asp:Label ID="lbtnInsurancePending" runat="server" data-toggle="tooltip" data-placement="bottom" title="Deactive Buses" class="h3 font-weight-bold mb-0 text-right float-right text-info" Text="0"></asp:Label></h5>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="col-3 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h4 class="mb-1">
                                            <asp:Label ID="Label5" runat="server" CssClass="text-capitalize">Expired Details</asp:Label></h4>
                                        <div class="row m-0">
                                            <div class="col-6 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Permit:&nbsp;
                                 <asp:Label ID="lbtnPermitExpired" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Buses" CssClass="h3 font-weight-bold mb-0 text-right float-right text-danger" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Fitness:&nbsp;
                                 <asp:Label ID="lbtnFitnessExpired" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Buses" CssClass="h3 font-weight-bold mb-0 text-right float-right text-danger" Text="0"></asp:Label></h5>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Pollution: &nbsp;
                                <asp:Label ID="lbtnPollutionExpired" runat="server" data-toggle="tooltip" data-placement="bottom" title="Active Buses" CssClass="h3 font-weight-bold mb-0 text-right float-right text-danger" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Insurance:&nbsp;
                                 <asp:Label ID="lbtnInsuranceExpired" runat="server" data-toggle="tooltip" data-placement="bottom" title="Deactive Buses" class="h3 font-weight-bold mb-0 text-right float-right text-danger" Text="0"></asp:Label></h5>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="card-body">
                                <div class="row mr-0">
                                    <div class="col">
                                        <div class="row m-0 col-12" style="display: inline-block">
                                            <h4 class="mb-1 float-left">To be Expired (In 1 Month)</h4>
                                            <div class="float-right">
                                                <asp:LinkButton ID="lbtnview" OnClick="lbtnview_Click" data-toggle="tooltip" data-placement="bottom" title="View Instructions" runat="server" Width="30px" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lbtndwnldinst" OnClick="lbtndwnldinst_Click"  class="btn btn bg-gradient-green btn-sm text-white" Width="30px" data-toggle="tooltip" data-placement="bottom" title="Download Instruction">
                                            <i class="fa fa-download"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="row m-0">
                                            <div class="col-6 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Permit:&nbsp;
                                 <asp:Label ID="lbtnPermitToBeExpired" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Buses" CssClass="h3 font-weight-bold mb-0 text-right float-right text-yellow" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Fitness:&nbsp;
                                 <asp:Label ID="lbtnFitnessToBeExpired" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Buses" CssClass="h3 font-weight-bold mb-0 text-right float-right text-yellow" Text="656"></asp:Label></h5>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Pollution: &nbsp;
                                <asp:Label ID="lbtnPollutionToBeExpired" runat="server" data-toggle="tooltip" data-placement="bottom" title="Active Buses" CssClass="h3 font-weight-bold mb-0 text-right float-right text-yellow" Text="5484"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Insurance:&nbsp;
                                 <asp:Label ID="lbtnInsuranceToBeInsurance" runat="server" data-toggle="tooltip" data-placement="bottom" title="Deactive Buses" class="h3 font-weight-bold mb-0 text-right float-right text-yellow" Text="0"></asp:Label></h5>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
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
                <div class="card" style="min-height: 500px">
                    <div class="card-header border-bottom">
                        <div class="float-left">
                            <h3 class="mb-0">Buses List</h3>
                        </div>

                    </div>

                    <div class="card card-stats">
                        <div class="card-body">
                            <div class="row m-0">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-4 pr-0">
                                            <h6 class="form-control-label text-muted my-0">Bus Registration No</h6>
                                            <asp:TextBox class="form-control" runat="server" ID="tbSSerialNo" MaxLength="50" ToolTip="Reg./Chasis No"
                                                placeholder="Max 50 Characters" Text="" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <h6 class="form-control-label text-muted my-0">Service Type</h6>
                                            <asp:DropDownList ID="ddlSServiceType" runat="server" CssClass="form-control form-control-sm" ToolTip="Service Type">
                                                <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <h6 class="form-control-label text-muted my-0">Depot</h6>
                                            <asp:DropDownList ID="ddlSDepot" runat="server" CssClass="form-control form-control-sm" ToolTip="Depot">
                                                <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-4">
                                            <h6 class="form-control-label text-muted my-0">Status Type</h6>
                                            <asp:DropDownList ID="ddlSType" runat="server" CssClass="form-control form-control-sm" ToolTip="Status Type"
                                                class="form-control">
                                                <asp:ListItem Value="O" Text="All"></asp:ListItem>
                                                <asp:ListItem Value="Fitness" Text="Fitness"></asp:ListItem>
                                                <asp:ListItem Value="Pollution" Text="Pollution"></asp:ListItem>
                                                <asp:ListItem Value="Permit" Text="Permit"></asp:ListItem>
                                                <asp:ListItem Value="Insurance" Text="Insurance"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <h6 class="form-control-label text-muted my-0">Status</h6>
                                            <asp:DropDownList ID="ddlSStatus" runat="server" CssClass="form-control form-control-sm" ToolTip="Bus Status"
                                                class="form-control">
                                                <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                                <asp:ListItem Value="P" Text="Pending"></asp:ListItem>
                                                <asp:ListItem Value="E" Text="Expiring Soon"></asp:ListItem>
                                                <asp:ListItem Value="O" Text="Expired"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4 mt-3 pt-1">
                                            <asp:LinkButton ID="lbtnsearch" OnClick="lbtnsearch_Click" runat="server" class="btn btn-sm btn-primary m-0" ToolTip="Search"> <i class="fa fa-search"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lbtnResetList" OnClick="lbtnResetList_Click" runat="server" class="btn btn-sm btn-danger m-0" ToolTip="Reset"> <i class="fa fa-undo"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lbtnDownloadExcel" OnClick="lbtnDownloadExcel_Click" Visible="true" runat="server" class="btn btn bg-gradient-green btn-sm text-white m-0"
                                                ToolTip="Download Excel"> <i class="fa fa-download"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <asp:GridView ID="gvBusDetails" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
                        OnRowCommand="gvBusDetails_RowCommand" OnPageIndexChanging="gvBusDetails_PageIndexChanging" OnRowDataBound="gvBusDetails_RowDataBound"
                        AllowPaging="true" PageSize="4" DataKeyNames="busregistration_no,ownertype,engine_no,chasis_no,srtpid,servicetype_nameen,layoutcode,layoutname,officeid,officename,makeid,make_name,gps_yn,gpscompanyid,gpscompany_name,current_status,bustype,bus_type,init_odometerreading,odometer_reading,wheel_base,store,store_name,receivedon_date,agency,name,invoice_no,invoice_date,invoice_amt">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="card card-stats">
                                        <!-- Card body -->
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col">

                                                    <h5 class="text-uppercase mb-0 font-weight-bold text-xs">
                                                        <%# Eval("busregistration_no") %>(<%# Eval("servicetype_nameen") %>)<br />
                                                        <span class="text-gray text-xs">Depot- 
                                                            <%# Eval("officename") %>
                                                        </span>
                                                    </h5>
                                                </div>
												 <div class="col-auto pt-2">
                                                    <asp:LinkButton ID="lbtnActivate" Visible="true" runat="server" CommandName="activate" CssClass="btn btn-sm btn-success" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Activate bus"> <i class="fa fa-check"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDiscontinue" Visible="true" runat="server" CommandName="deactivate" CssClass="btn btn-sm btn-warning" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Deactivate bus"> <i class="fa fa-ban"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnViewbus" Visible="true" runat="server" CommandName="viewBus" CssClass="btn btn-sm btn-default" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="View bus Details"> <i class="fa fa-eye"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnUpdategency" Visible="true" runat="server" CommandName="updateBus" CssClass="btn btn-sm btn-info" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Update bus Details"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDeletegency" Visible="true" runat="server" CommandName="deleteBus" CssClass="btn btn-sm btn-danger" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Delete bus"> <i class="fa fa-trash"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnPermit" CommandName="Permit" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Visible="true" runat="server" CssClass="btn btn-sm btn-success" ToolTip="Update Permit Details" Text="P"></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnFitness" CommandName="Fitness" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Visible="true" runat="server" CssClass="btn btn-sm btn-warning" ToolTip="Update Fitness Details"> F</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnInsurance" CommandName="Insurance" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Visible="true" runat="server" CssClass="btn btn-sm btn-info" ToolTip="Update Insurance Details"> I</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnPollution" CommandName="Pollution" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Visible="true" runat="server" CssClass="btn btn-sm btn-primary" ToolTip="Update Pollution Details"> PC</asp:LinkButton>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
						<PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                    </asp:GridView>
                    <asp:Panel ID="pnlnoRecordfound" runat="server">
                        <div class="card card-stats">
                            <!-- Card body -->
                            <div class="card-body">
                                <div class="row">

                                    <div class="col-lg-12 text-center">
                                        <i class="fa fa-bus fa-5x"></i>
                                    </div>
                                    <div class="col-lg-12 text-center mt-4" runat="server" id="div1">
                                        <span class="h2 font-weight-bold mb-0">Start Bus Creation </span>
                                        <h5 class="card-title text-uppercase text-muted mb-0">No Bus has been registered yet</h5>
                                    </div>
                                    <div class="col-lg-12 text-center mt-4" runat="server" id="div2" visible="false">
                                        <span class="h2 font-weight-bold mb-0">No Record Found </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="col-xl-8 order-xl-2">
                <asp:Panel ID="pnlAddBus" runat="server" Visible="true">
                    <div class="card" style="min-height: 500px">
                        <div class="card-header">
                            <div class="row align-items-center m-0">
                                <div class="col-8">
                                    <h3 class="mb-0">
                                        <asp:Label runat="server" ID="lblBusHead" Text="Add Bus Details"></asp:Label>
                                        <asp:Label runat="server" ID="lblBusUpdate" Text="Update Bus Details" Visible="false"></asp:Label>

                                    </h3>

                                </div>
                                <div class="col-md-4 text-right">
                                    <p style="font-size: small; margin: 0px;" class="text-danger font-weight-bold">All marked * Fields are mandatory</p>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <h6 class="heading-small my-0">1. Bus Details</h6>
                            <div class="pl-lg-4">
                                <div class="row">
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Registration No.<span style="color: red">*</span></asp:Label>
                                        <div class="input-group ">
                                            <asp:TextBox ID="tbRegistrationNoChar" runat="server" CssClass="form-control form-control-sm text-uppercase" placeholder="Ex- UK07TB" MaxLength="6" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Registration No."></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,UppercaseLetters,LowercaseLetters" TargetControlID="tbRegistrationNoChar" />
                                            <div class="input-group-append" style="width: 45%;">
                                                <span>&nbsp;-&nbsp;</span>
                                                <asp:TextBox ID="tbRegistrationNo" MaxLength="4" autocomplete="off" placeholder="Ex- 1234" data-toggle="tooltip" data-placement="bottom" title="Registration No." CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" FilterType="Numbers" TargetControlID="tbRegistrationNo" />
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Engine No.</asp:Label><br />
                                        <asp:TextBox ID="tbEngineNO" MaxLength="10" autocomplete="off" placeholder="Max Length 10" data-toggle="tooltip" data-placement="bottom" title="Engine No." CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server" FilterType="Numbers" TargetControlID="tbEngineNO" />
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Chassis No.<span style="color: red">*</span></asp:Label><br />
                                        <asp:TextBox ID="tbChasisNo" MaxLength="20" autocomplete="off" placeholder="Max 20 Characters" data-toggle="tooltip" data-placement="bottom" title="Chassis No." CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbChasisNo" ValidChars=" _/." />
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Wheel Base<span style="color: red">*</span></asp:Label><br />
                                        <asp:TextBox ID="tbWheelBase" MaxLength="6" autocomplete="off" placeholder="Max Length 6" data-toggle="tooltip" data-placement="bottom" title="Wheel Base" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server" FilterType="Numbers" TargetControlID="tbWheelBase" />
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Initial Odometer Reading<span style="color: red">*</span></asp:Label>
                                        <asp:TextBox ID="tbOdometerReading" MaxLength="7" autocomplete="off" placeholder="Max Length 7" data-toggle="tooltip" data-placement="bottom" title="Initial Odometer Reading" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="tbOdometerReading" />
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Bus Service Type<span style="color: red">*</span></asp:Label>
                                        <asp:DropDownList ID="ddlBusServiceType" data-toggle="tooltip" data-placement="bottom" title="Bus Service Type" CssClass="form-control form-control-sm" runat="server">
                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Layout<span style="color: red">*</span></asp:Label>
                                        <asp:DropDownList ID="ddlLayout" data-toggle="tooltip" data-placement="bottom" title="Layout" CssClass="form-control form-control-sm" runat="server">
                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Ownership Type<span style="color: red">*</span></asp:Label>
                                        <asp:DropDownList ID="ddlOwnerType" data-toggle="tooltip" data-placement="bottom" title="Ownership Type" CssClass="form-control form-control-sm" runat="server">
                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <h6 class="heading-small my-0 mt-2">2. Other Details</h6>
                            <div class="pl-lg-4">
                                <div class="row">
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Make Model<span style="color: red">*</span></asp:Label>
                                        <asp:DropDownList ID="ddlMakeModel" data-toggle="tooltip" data-placement="bottom" title="Make Model" CssClass="form-control form-control-sm" runat="server">
                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> GPS In Bus<span style="color: red">*</span></asp:Label>
                                        <asp:DropDownList ID="ddlGpsYN" data-toggle="tooltip" AutoPostBack="true" OnSelectedIndexChanged="ddlGpsYN_SelectedIndexChanged" data-placement="bottom" title="GPS In Bus" CssClass="form-control form-control-sm" runat="server">
                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                            <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                            <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblGPSCompany" runat="server" CssClass="text-muted form-control-label" Visible="false"> GPS Company<span style="color: red">*</span></asp:Label>
                                        <asp:DropDownList ID="ddlGPSCompany" data-toggle="tooltip" Visible="false" data-placement="bottom" title="GPS Company" CssClass="form-control form-control-sm" runat="server">
                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <h6 class="heading-small my-0 mt-2">3. Invoice Details</h6>
                            <div class="pl-lg-4">
                                <div class="row">
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted  form-control-label">Agency</asp:Label>
                                        <asp:DropDownList ID="ddlAgency" data-toggle="tooltip" data-placement="bottom" title="Agency" CssClass="form-control form-control-sm" runat="server">
                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted  form-control-label">Invoice No.</asp:Label>
                                        <asp:TextBox ID="tbinvoiceNo" MaxLength="10" autocomplete="off" placeholder="Max Length 10" data-toggle="tooltip" data-placement="bottom" title="Invoice Number" CssClass="form-control form-control-sm text-uppercase" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="UppercaseLetters,LowercaseLetters,Numbers" TargetControlID="tbinvoiceNo" />
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted  form-control-label"> Invoice Date</asp:Label><br />
                                        <div class="input-group date">
                                            <asp:TextBox ID="tbInvoiceDate" ToolTip="Enter Invoice Date" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbInvoiceDate" ValidChars="/" />
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted  form-control-label">Invoice Amount</asp:Label>
                                        <asp:TextBox ID="tbinvoiceamt" MaxLength="10" autocomplete="off" placeholder="Max Length 10" data-toggle="tooltip" data-placement="bottom" title="Invoice Number" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers,Custom" ValidChars="." TargetControlID="tbinvoiceamt" />
                                    </div>


                                </div>
                                <div class="row mt-2">
                                    <div class="col-lg-12">
                                        <asp:Label runat="server" CssClass="text-muted  form-control-label"> Upload Invoice</asp:Label><br />
                                        <asp:Button ToolTip="Upload Invoice" ID="btnUploadInvoice" OnClick="btnUploadInvoice_Click" runat="server" CausesValidation="False" CssClass="form-control form-control-sm"
                                            Style="display: none" TabIndex="18" Text="Upload Image" accept=".png,.jpg,.jpeg,.gif" Width="80px" />
                                        <asp:FileUpload ID="FileInvoice" onchange="UploadInvoice(this);" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-sm btn-success" TabIndex="9" />
                                        <asp:Image ID="ImgInvoice" onchange="UploadInvoice(this);" runat="server" Visible="false" Style="border-width: 0px; height: 50px; width: 50px; border: 2px solid #eaf4ff;" />
                                        <br />
                                        <label id="lblwrongimage" runat="server" style="font-size: 7pt; color: Red; line-height: 12px;">
                                            <br />
                                            Image size will be less then 1 MB<br />
                                            (Only .JPG, .PNG, .JPEG)</label>
                                    </div>
                                </div>
                            </div>
                            <h6 class="heading-small my-0 mt-2">4. Receiving Details</h6>
                            <div class="pl-lg-4">
                                <div class="row">
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Received At Store<span style="color: red">*</span> </asp:Label>
                                        <asp:DropDownList ID="ddlStore" data-toggle="tooltip" data-placement="bottom" title="Receiving Store" CssClass="form-control form-control-sm" runat="server">
                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Received On<span style="color: red">*</span></asp:Label><br />
                                        <div class="input-group date">
                                            <asp:TextBox ID="tbRecDate" ToolTip="Enter Receiving Date" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbRecDate" ValidChars="/" />
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <h6 class="heading-small  my-0 mt-2">5. Issuance Details</h6>
                            <div class="pl-lg-4">
                                <div class="row">
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Issued to Depot<span style="color: red">*</span> </asp:Label>
                                        <asp:DropDownList ID="ddlDepot" data-toggle="tooltip" data-placement="bottom" title="Depot" CssClass="form-control form-control-sm" runat="server">
                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="pl-lg-4 mt-3 mb-2">
                                <div class="row">
                                    <div class="col-lg-12 text-left">
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" class="btn btn-success" OnClick="updateBusDetails_Click" Visible="false" data-toggle="tooltip" data-placement="bottom" title="Update Bus Details">
                                    <i class="fa fa-save"></i>&nbsp; Update</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnSave" Visible="true" runat="server" class="btn btn-success" OnClick="saveBusDetails_Click" data-toggle="tooltip" data-placement="bottom" title="Save Bus Details">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-danger" OnClick="resetDetails_Click" data-toggle="tooltip" data-placement="bottom" title="Reset Bus Details">
                                    <i class="fa fa-undo"></i>&nbsp; Reset</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" Visible="false" runat="server" CssClass="btn btn-danger" OnClick="resetDetails_Click" data-toggle="tooltip" data-placement="bottom" title="Cancel Bus Details">
                                    <i class="fa fa-times"></i>&nbsp; Cancel</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlViewBusDetails" runat="server" Visible="false">
                    <div class="card" style="min-height: 500px">
                        <div class="card-header">
                            <div class="row align-items-center m-0">
                                <div class="col-md-8">
                                    <h3 class="mb-0">
                                        <asp:Label runat="server" Text="View Bus Details" ID="lblViewBusHead"></asp:Label>
                                    </h3>
                                </div>
                                <div class="col-4 float-right">
                                    <asp:LinkButton ID="lbtnCloseViewBus" runat="server" OnClick="lbtnCloseViewBus_Click" class="btn btn-sm btn-danger float-right" Style="border-radius: 25px; left: 23px; margin-top: -36px;"> <i class="fa fa-times" ></i> </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <h6 class="heading-small  my-0">1. Bus Details</h6>
                            <div class="pl-lg-4">
                                <div class="row">
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Engine No.</asp:Label><br />
                                        <asp:Label ID="lblEngineNo" CssClass="form-control-label" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Chassis No.</asp:Label><br />
                                        <asp:Label ID="lblChasisNo" CssClass="form-control-label" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Wheel Base</asp:Label><br />
                                        <asp:Label ID="lblWheelBase" CssClass="form-control-label" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Initial Odometer Reading</asp:Label><br />
                                        <asp:Label ID="lblInitialOdometer" CssClass="form-control-label" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Layout</asp:Label><br />
                                        <asp:Label ID="lblLayout" CssClass="form-control-label" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Ownership Type</asp:Label><br />
                                        <asp:Label ID="lblOwnership" CssClass="form-control-label" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <h6 class="heading-small my-0 mt-2">2. Other Details</h6>
                            <div class="pl-lg-4">
                                <div class="row">
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted  form-control-label">Make Model</asp:Label><br />
                                        <asp:Label ID="lblMakeModel" CssClass="form-control-label" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted  form-control-label"> GPS In Bus</asp:Label><br />
                                        <asp:Label ID="lblGpsYN" CssClass="form-control-label" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3" runat="server" id="divGps" visible="false">
                                        <asp:Label ID="lblGPS" runat="server" CssClass="text-muted  form-control-label"> GPS Company</asp:Label><br />
                                        <asp:Label ID="lblGPScompanyName" CssClass="form-control-label" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <h6 class="heading-small  my-0 mt-2">3. Invoice Details</h6>
                            <div class="pl-lg-4">
                                <div class="row">
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Agency</asp:Label><br />
                                        <asp:Label ID="lblAgency" CssClass="form-control-label" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Invoice No.</asp:Label><br />
                                        <asp:Label ID="lblInvoiceNo" CssClass="form-control-label" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Invoice Date</asp:Label><br />
                                        <asp:Label ID="lblInvoiceDate" CssClass="form-control-label" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Invoice Amount (₹)</asp:Label><br />
                                        <asp:Label ID="lblInvoiceAmt" CssClass="form-control-label" runat="server"></asp:Label>
                                    </div>


                                </div>


                            </div>

                            <h6 class="heading-small  my-0 mt-2">4. Receiving Details</h6>
                            <div class="pl-lg-4">
                                <div class="row">
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Received At Store </asp:Label><br />
                                        <asp:Label ID="lblReceivingStore" CssClass="form-control-label" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label"> Received On</asp:Label><br />
                                        <asp:Label ID="lblReceiveOn" CssClass="form-control-label" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label runat="server" CssClass="text-muted form-control-label">Issued to Depot </asp:Label><br />
                                        <asp:Label ID="lblDepot" CssClass="form-control-label" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlViewBus" runat="server" Visible="false">
                    <div class="card" style="min-height: 500px">
                        <div class="card-header border-bottom">
                            <div class="float-left">
                                <h3 class="mb-0">Buses List</h3>
                            </div>
                            <div class="float-right">
                                <div class="input-group">
                                    <asp:TextBox ID="tbSearch" data-toggle="tooltip" data-placement="bottom" title="Enter Bus Name" CssClass="form-control form-control-sm mr-1" MaxLength="20" runat="server" Width="200px" placeholder="Search Bus" autocomplete="off"></asp:TextBox>

                                    <asp:LinkButton ID="lbtnSearchBus" data-toggle="tooltip" data-placement="bottom" title="Search Bus" runat="server" CssClass="btn bg-success btn-sm text-white mr-1">
                                            <i class="fa fa-search"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnResetSearchBus" OnClick="lbtnResetBus_Click" data-toggle="tooltip" data-placement="bottom" title="Reset Bus" runat="server" CssClass="btn bg-warning btn-sm text-white">
                                            <i class="fa fa-undo"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>

                        <asp:Panel ID="pnlNoBus" runat="server">
                            <div class="card card-stats">
                                <!-- Card body -->
                                <div class="card-body">
                                    <div class="row">

                                        <div class="col-lg-12 text-center">
                                            <i class="fa fa-bus fa-5x"></i>
                                        </div>
                                        <div class="col-lg-12 text-center mt-4" runat="server" id="divNoRecord">
                                            <span class="h2 font-weight-bold mb-0">Start Bus Creation </span>
                                            <h5 class="card-title text-uppercase text-muted mb-0">No Bus has been registered yet</h5>
                                        </div>
                                        <div class="col-lg-12 text-center mt-4" runat="server" id="divNoSearchRecord" visible="false">
                                            <span class="h2 font-weight-bold mb-0">No Record Found </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlUpdatePermit" runat="server" Visible="false">
                    <div class="card" style="min-height: 500px">
                        <div class="card-header">
                            <div class="row align-items-center m-0">
                                <div class="col-8">
                                    <h3>
                                        <asp:Label ID="lblPermit" runat="server" class="mb-0 float-left"></asp:Label></h3>
                                </div>
                                <div class="col-4 float-right">
                                    <asp:LinkButton ID="lbtnClosePermit" runat="server" class="btn btn-sm btn-danger float-right" Style="border-radius: 25px; left: 23px; margin-top: -36px;" OnClick="lbtnCancelPUC_Click"> <i class="fa fa-times" ></i> </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row mx-2">
                                <div class="col-6 border-right">

                                    <div class="col-lg-12">
                                        <asp:GridView ID="gvBusPermit" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover" GridLines="None"
                                            HeaderStyle-CssClass="thead-light font-weight-bold" OnPageIndexChanging="gvBusPermit_PageIndexChanging" PageSize="5"
                                            DataKeyNames="bus_permit, validto_date, actiondate">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Permit No." HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ToolTip="Permit No." CssClass="form-control-label" runat="server" Text='<%#Eval("bus_permit") %>' /><br />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="validfromdate" ItemStyle-CssClass="form-control-label" HeaderText="Valid From"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                 <asp:BoundField DataField="validto_date" ItemStyle-CssClass="form-control-label" HeaderText="Valid Upto"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="actiondate" HeaderText="Updated On" ItemStyle-CssClass="form-control-label"
                                                    DataFormatString="{0:dd/MM/yyyy}" />

                                            </Columns>
                                            <EmptyDataTemplate>
                                                <p style="color: red; text-align: center; font-weight: bold; font-size: 12pt;">No Records Found.</p>
                                            </EmptyDataTemplate>
                                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                        </asp:GridView>
                                        <asp:Panel runat="server" ID="pnlNoPermitRecord" Visible="true" CssClass="text-center" Width="100%">
                                            <p class="text-center" style="font-size: 32px; font-weight: bold; color: #e3e3e3; margin-top: 50px;">
                                                Permit Detail not Available
                                            </p>
                                        </asp:Panel>
                                    </div>
                                </div>

                                <div class="col-6 pl-4">
                                    <div class="row">
                                        <div class="col-6">
                                            <asp:Label runat="server" CssClass="form-control-label"> Permit No.<span style="color: red">*</span></asp:Label>

                                            <asp:TextBox ID="tbPermitNo" runat="server" CssClass="form-control form-control-sm text-uppercase" placeholder="Max 20 chars" MaxLength="20" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Permit No."></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers,Uppercaseletters,LowercaseLetters" TargetControlID="tbPermitNo" />

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-6">
                                            <asp:Label runat="server" CssClass="form-control-label"> Valid From<span style="color: red">*</span></asp:Label><br />
                                            <div class="input-group date">
                                                <asp:TextBox ID="tbValidFrom" ToolTip="Enter Valid from Date" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbValidFrom" ValidChars="/" />
                                            </div>
                                        </div>
                                        <div class="col-6">
                                            <asp:Label runat="server" CssClass="form-control-label"> Valid To<span style="color: red">*</span></asp:Label><br />
                                            <div class="input-group date">
                                                <asp:TextBox ID="tbValidTo" ToolTip="Enter Valid To Date" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                               <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbValidTo" ValidChars="/" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row pt-3 text-center">
                                        <div class="col-md-12">

                                            <asp:LinkButton ID="lbtnSavePermit" Visible="true" runat="server" class="btn btn-success" OnClick="lbtnSavePermit_Click" data-toggle="tooltip" data-placement="bottom" title="Save Permit Deatils">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>
                                            &nbsp;&nbsp;                                    
                                        <asp:LinkButton ID="lbtnCancelPermit" Visible="true" runat="server" CssClass="btn btn-danger" OnClick="lbtnCancelPUC_Click" data-toggle="tooltip" data-placement="bottom" title="Cancel Permit Details">
                                    <i class="fa fa-times"></i>&nbsp; Cancel</asp:LinkButton>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>


                <asp:Panel ID="pnlUpdateFitness" runat="server" Visible="false">
                    <div class="card" style="min-height: 500px">
                        <div class="card-header">
                            <div class="row align-items-center m-0">
                                <div class="col-8">
                                    <h3>
                                        <asp:Label ID="lblfitness" runat="server" class="mb-0 float-left"></asp:Label></h3>


                                </div>
                                <div class="col-4 float-right">
                                    <asp:LinkButton ID="lbtnCloseFitness" runat="server" class="btn btn-sm btn-danger float-right" Style="border-radius: 25px; left: 23px; margin-top: -36px;" OnClick="lbtnCancelPUC_Click"> <i class="fa fa-times" ></i> </asp:LinkButton>

                                </div>



                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row mx-2">
                                <div class="col-6 border-right">
                                    <div class="col-lg-12">
                                        <asp:GridView ID="gvBusFitness" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover" GridLines="None"
                                            HeaderStyle-CssClass="thead-light font-weight-bold" OnPageIndexChanging="gvBusFitness_PageIndexChanging" PageSize="5"
                                            DataKeyNames="certificate_no, validto_date, actiondate">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Certificate No." HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ToolTip="Certificate No." CssClass="form-control-label" runat="server" Text='<%#Eval("certificate_no") %>' /><br />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="validfromdate" ItemStyle-CssClass="form-control-label" HeaderText="Valid From"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                 <asp:BoundField DataField="validto_date" ItemStyle-CssClass="form-control-label" HeaderText="Valid Upto"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="actiondate" HeaderText="Updated On" ItemStyle-CssClass="form-control-label"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <p style="color: red; text-align: center; font-weight: bold; font-size: 12pt;">No Records Found.</p>
                                            </EmptyDataTemplate>
                                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                        </asp:GridView>
                                        <asp:Panel runat="server" ID="pnlNoFitnessRecord" Visible="true" CssClass="text-center" Width="100%">
                                            <p class="text-center" style="font-size: 32px; font-weight: bold; color: #e3e3e3; margin-top: 50px;">
                                                Fitness Details not Available
                                            </p>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="col-6 pl-4">
                                    <div class="row">
                                        <div class="col-6">
                                            <asp:Label runat="server" CssClass="form-control-label"> Certificate No.<span style="color: red">*</span></asp:Label>

                                            <asp:TextBox ID="tbCertificateNoFitness" runat="server" CssClass="form-control form-control-sm text-uppercase" placeholder="Max 20 chars" MaxLength="20" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Fitness No."></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers,uppercaseletters,LowercaseLetters" TargetControlID="tbCertificateNoFitness" />

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-6">
                                            <asp:Label runat="server" CssClass="form-control-label"> Valid From<span style="color: red">*</span></asp:Label><br />
                                            <div class="input-group date">
                                                <asp:TextBox ID="tbValidFromFitness" ToolTip="Enter Valid from Date" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbValidFromFitness" ValidChars="/" />
                                            </div>
                                        </div>
                                        <div class="col-6">
                                            <asp:Label runat="server" CssClass="form-control-label"> Valid To<span style="color: red">*</span></asp:Label><br />
                                            <div class="input-group date">
                                                <asp:TextBox ID="tbValidtoFitness" ToolTip="Enter Valid To Date" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbValidtoFitness" ValidChars="/" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row pt-3 text-center">
                                        <div class="col-12">

                                            <asp:LinkButton ID="lbtnSavFitness" Visible="true" runat="server" class="btn btn-success" OnClick="lbtnSavFitness_Click" data-toggle="tooltip" data-placement="bottom" title="Save Fitness Deatils">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>
                                            &nbsp;&nbsp;                                    
                                        <asp:LinkButton ID="lbtnCancelFitness" Visible="true" runat="server" CssClass="btn btn-danger"  OnClick="lbtnCancelPUC_Click" data-toggle="tooltip" data-placement="bottom" title="Cancel Fitness Details">
                                    <i class="fa fa-times"></i>&nbsp; Cancel</asp:LinkButton>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>


                <asp:Panel ID="pnlUpdateInsurance" runat="server" Visible="false">
                    <div class="card" style="min-height: 500px">
                        <div class="card-header">
                            <div class="row align-items-center m-0">
                                <div class="col-8">
                                    <h3>
                                        <asp:Label ID="lblInsurance" runat="server" class="mb-0 float-left"></asp:Label></h3>

                                </div>
                                <div class="col-4 float-right">
                                    <asp:LinkButton ID="lbtnCloseInsurance" runat="server" class="btn btn-sm btn-danger float-right" Style="border-radius: 25px; left: 23px; margin-top: -36px;" OnClick="lbtnCancelPUC_Click"> <i class="fa fa-times" ></i> </asp:LinkButton>

                                </div>



                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row mx-2">
                                <div class="col-6 border-right">
                                    <div class="col-lg-12">
                                        <asp:GridView ID="gvBusInsurance" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover" GridLines="None"
                                            HeaderStyle-CssClass="thead-light font-weight-bold" OnPageIndexChanging="gvBusInsurance_PageIndexChanging" PageSize="5"
                                            DataKeyNames="insurancepolicy_no, validto_date, actiondate">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Certificate No." HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ToolTip="Certificate No." CssClass="form-control-label" runat="server" Text='<%#Eval("insurancepolicy_no") %>' /><br />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="validto_date" ItemStyle-CssClass="form-control-label" HeaderText="Valid Upto"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="actiondate" HeaderText="Updated On" ItemStyle-CssClass="form-control-label"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <p style="color: red; text-align: center; font-weight: bold; font-size: 12pt;">No Records Found.</p>
                                            </EmptyDataTemplate>
                                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                        </asp:GridView>
                                        <asp:Panel runat="server" ID="pnlNoInsuranceRecord" Visible="true" CssClass="text-center" Width="100%">
                                            <p class="text-center" style="font-size: 32px; font-weight: bold; color: #e3e3e3; margin-top: 50px;">
                                                Insurance Details not Available
                                            </p>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="col-6 pl-4">
                                    <div class="row">
                                        <div class="col-6">
                                            <asp:Label runat="server" CssClass="form-control-label"> Certificate No.<span style="color: red">*</span></asp:Label>

                                            <asp:TextBox ID="tbinsuranceno" runat="server" CssClass="form-control form-control-sm text-uppercase" placeholder="Max 20 chars" MaxLength="20" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Insurance No."></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers,uppercaseletters,LowercaseLetters" TargetControlID="tbinsuranceno" />

                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-6">
                                            <asp:Label runat="server" CssClass="form-control-label"> Valid Upto<span style="color: red">*</span></asp:Label><br />
                                            <div class="input-group date">
                                                <asp:TextBox ID="tbValiduptoInsurance" ToolTip="Enter Valid To Date" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbValiduptoInsurance" ValidChars="/" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row pt-3 text-center">
                                        <div class="col-12">

                                            <asp:LinkButton ID="lbtnSavInsurance" Visible="true" runat="server" class="btn btn-success" OnClick="lbtnSavInsurance_Click" data-toggle="tooltip" data-placement="bottom" title="Save Insurance Deatils">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>
                                            &nbsp;&nbsp;                                    
                                        <asp:LinkButton ID="lbtnCancelInsurance" Visible="true" runat="server" CssClass="btn btn-danger" OnClick="lbtnCancelPUC_Click" data-toggle="tooltip" data-placement="bottom" title="Cancel Insurance Details">
                                    <i class="fa fa-times"></i>&nbsp; Cancel</asp:LinkButton>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>


                <asp:Panel ID="pnlUpdatePollution" runat="server" Visible="false">
                    <div class="card" style="min-height: 500px">
                        <div class="card-header">
                            <div class="row align-items-center m-0">
                                <div class="col-8">
                                    <h3>
                                        <asp:Label ID="lblPollution" runat="server" class="mb-0 float-left"></asp:Label></h3>


                                </div>
                                <div class="col-4 float-right">
                                    <asp:LinkButton ID="lbtnClosePollution" runat="server" class="btn btn-sm btn-danger float-right" Style="border-radius: 25px; left: 23px; margin-top: -36px;" OnClick="lbtnCancelPUC_Click"> <i class="fa fa-times" ></i> </asp:LinkButton>

                                </div>



                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row mx-2">
                                <div class="col-6 border-right">
                                    <div class="col-lg-12">
                                        <asp:GridView ID="gvBusPollution" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover" GridLines="None"
                                            HeaderStyle-CssClass="thead-light font-weight-bold" OnPageIndexChanging="gvBusPollution_PageIndexChanging" PageSize="4"
                                            DataKeyNames="certificate_no, validto_date, actiondate">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Certificate No." HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ToolTip="Certificate No." CssClass="form-control-label" runat="server" Text='<%#Eval("certificate_no") %>' /><br />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="validfromdate" ItemStyle-CssClass="form-control-label" HeaderText="Valid From"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                 <asp:BoundField DataField="validto_date" ItemStyle-CssClass="form-control-label" HeaderText="Valid Upto"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="actiondate" HeaderText="Updated On" ItemStyle-CssClass="form-control-label"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <p style="color: red; text-align: center; font-weight: bold; font-size: 12pt;">No Records Found.</p>
                                            </EmptyDataTemplate>
                                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                        </asp:GridView>
                                        <asp:Panel runat="server" ID="pnlNoPollutionFitness" Visible="true" CssClass="text-center" Width="100%">
                                            <p class="text-center" style="font-size: 32px; font-weight: bold; color: #e3e3e3; margin-top: 50px;">
                                                Pollution Details are not Available
                                            </p>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="col-6 pl-4">
                                    <div class="row">
                                        <div class="col-6">
                                            <asp:Label runat="server" CssClass="form-control-label"> Certificate No.<span style="color: red">*</span></asp:Label>

                                            <asp:TextBox ID="tbPollutionNo" runat="server" CssClass="form-control form-control-sm text-uppercase" placeholder="Max 20 chars" MaxLength="20" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Pollution No."></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers,uppercaseletters,LowercaseLetters" TargetControlID="tbPollutionNo" />

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-6">
                                            <asp:Label runat="server" CssClass="form-control-label"> Valid From<span style="color: red">*</span></asp:Label><br />
                                            <div class="input-group date">
                                                <asp:TextBox ID="tbValidFromPollution" ToolTip="Enter Valid from Date" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbValidFromPollution" ValidChars="/" />
                                            </div>
                                        </div>
                                        <div class="col-6">
                                            <asp:Label runat="server" CssClass="form-control-label"> Valid To<span style="color: red">*</span></asp:Label><br />
                                            <div class="input-group date">
                                                <asp:TextBox ID="tbValidtoPollution" ToolTip="Enter Valid To Date" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbValidtoPollution" ValidChars="/" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row pt-3 text-center">
                                        <div class="col-12">

                                            <asp:LinkButton ID="lbtnSavPollution" Visible="true" runat="server" class="btn btn-success" OnClick="lbtnSavPollution_Click" data-toggle="tooltip" data-placement="bottom" title="Save Pollution Deatils">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>
                                            &nbsp;&nbsp;                                    
                                        <asp:LinkButton ID="lbtnCancelPollution" Visible="true" runat="server" CssClass="btn btn-danger" OnClick="lbtnCancelPUC_Click" data-toggle="tooltip" data-placement="bottom" title="Cancel Pollution Details">
                                    <i class="fa fa-times"></i>&nbsp; Cancel</asp:LinkButton>
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
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Please Confirm
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnYesConfirmation" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbtnYesConfirmation_Click"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
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


</asp:Content>

