<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/StnInchgemaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="StInchargeBusServTargetDiesel.aspx.cs" Inherits="Auth_StInchargeBusServTargetDiesel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
	<script type="text/javascript">
		$(document).ready(function () {

			var currDate = new Date().getDate();
			var preDate = new Date(new Date().setDate(currDate - 1));
			var todayDate = new Date(new Date().setDate(currDate));

			$('[id*=tbValidTo]').datepicker({
			    startDate: todayDate,
				changeMonth: true,
				changeYear: false,
				format: "dd/mm/yyyy",
				autoclose: true
			});
		    $('[id*=tbValidFrom]').datepicker({
		        startDate: todayDate,
				changeYear: false,
				format: "dd/mm/yyyy",
				autoclose: true
			})
				.on('changeDate', function (selected) {
					var minDate = new Date(selected.date.valueOf());
					$('[id*=tbValidTo]').datepicker('setStartDate', minDate);
				});
		});
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<div class="container-fluid pt-2 pb-5">
		<div class="row align-items-center">
			<div class="col-md-12 ">
				<div class="card card-stats mb-3">
					<div class="row m-0">
						<div class="col-7 border-right">
							<div class="card-body">
								<div class="row m-0">
									<div class="col-12">
										<h4 class="mb-1">
											<asp:Label ID="lblSummary" runat="server" CssClass="text-capitalize">Summary as on Date 01/12/2021 04:00pm</asp:Label></h4>
										<div class="row m-0">
											<div class="col-5 border-right">
												<div class="row m-0">
													<div class="col-12">
														<h5 class="card-title text-uppercase text-muted mb-0">Total Services:&nbsp;
                                <asp:LinkButton ID="lbtnTotalService" runat="server" OnClick="lbtnTotalExpireEntries_Click" ForeColor="gray" Font-Bold="true" Enabled="false" Text="0" Font-Size="14pt" CssClass="h3 font-weight-bold mb-0 text-right float-right"></asp:LinkButton></h5>
													</div>
													<div class="col-12">
														<h5 class="card-title text-uppercase text-muted mb-0">Total active entries: &nbsp;
                                <asp:LinkButton ID="lbtnTotalActiveEntries" runat="server" OnClick="lbtnTotalExpireEntries_Click" ForeColor="gray" Font-Bold="true" Font-Underline="true" CommandArgument="O" Text="0" Font-Size="14pt" CssClass="h3 font-weight-bold mb-0 text-right float-right"></asp:LinkButton></h5>
													</div>
												</div>
											</div>
											<div class="col-7">
												<div class="row m-0">
													<div class="col-12">
														<h5 class="card-title text-uppercase text-muted mb-0">Entries going to expire (in next 2 days):&nbsp;
                                 <asp:LinkButton ID="lbtnToExpireEntries" runat="server" OnClick="lbtnTotalExpireEntries_Click" ForeColor="gray" Font-Bold="true" Font-Underline="true" CommandArgument="T" Text="0" Font-Size="14pt" CssClass="h3 font-weight-bold mb-0 text-right float-right"></asp:LinkButton></h5>
													</div>
													<div class="col-12">
														<h5 class="card-title text-uppercase text-muted mb-0">Entries Expired: &nbsp;
															<asp:LinkButton ID="lbtnTotalExpireEntries" OnClick="lbtnTotalExpireEntries_Click" runat="server" ForeColor="gray" Font-Bold="true" Font-Underline="true" CommandArgument="E" Text="0" CssClass="h3 font-weight-bold mb-0 text-right float-right"></asp:LinkButton></h5>
													</div>
												</div>

											</div>
										</div>
									</div>

								</div>

							</div>
						</div>
						<div class="col-md-5">
							<div class="card-body">
								<div class="row">
									<div class="col">
										<div>
											<h4 class="mb-1">Instructions</h4>
										</div>
										<ul class="data-list" data-autoscroll>
											<li class="form-control-label">Add Target Diesel Income.
											</li>
											
										</ul>
									</div>
									<div class="col-auto">
										<asp:LinkButton ID="lbtnViewInstruction" runat="server" OnClick="lbtnViewInstruction_Click" ToolTip="View Instructions" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-eye"></i>
										</asp:LinkButton>
										<asp:LinkButton ID="lbtnDownloadUserManual" runat="server" ToolTip="Download user manual" CssClass="btn btn bg-gradient-green btn-sm text-white">
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
				<div class="card" style="min-height: 800px">
					<div class="card-header border-bottom">
						<div class="row">
                            <div class="col-md-4">
                                <h3 class="mb-0">Depot Services</h3>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group mb-0">
                                    <div class="input-group">
                                        <asp:TextBox ID="tbSearchDepotService" runat="server" AutoComplete="Off" class="form-control form-control-sm text-uppercase" placeholder="Search text" MaxLength="20"></asp:TextBox>
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="lbtnSearchDepotService" runat="server" CssClass="btn btn-success btn-icon-only btn-sm mr-1 " OnClick="lbtnSearchDepotService_Click" strle="z-i">
                                            <i class="fa fa-search mt-2"></i>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="lbtnRestSearchDepotService" runat="server" CssClass="btn btn-danger btn-icon-only btn-sm mr-1" OnClick="lbtnRestSearchDepotService_Click">
                                            <i class="fa fa-undo mt-2"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
					</div>
					<asp:GridView ID="gvFillingStation" OnRowCommand="gvFillingStation_RowCommand" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
						AllowPaging="true" PageSize="4" DataKeyNames="officeid,routeid,serviceownertype,dsvc_id,srtp_id,buslayoutcode,buswheelbase,busloadfactor,busseatingcapacity,targetincomeperkm,targetincome,tdieselaverage_hill,tdieselaverage_plain,service_name_en,validityfrom,validityto,layout_name,servicetype,loadfactor,bustype,wheelbase ">
						<Columns>
							<asp:TemplateField>
								<ItemTemplate>
									<div class="card card-stats">
										<!-- Card body -->
										<div class="card-body">
											<div class="row">
												<div class="col">

													<h5 class="text-uppercase mb-0 font-weight-bold text-xs">
														<%# Eval("service_name_en") %><br />
														<span class="text-gray text-xs">Valid Upto- 
                                                            <%# Eval("validityto") %>
														</span>
													</h5>
												</div>
											</div>
											<div class="row">
												<div class="col-lg-8 pt-2">
													<asp:LinkButton ID="lbtnView" Visible="true" runat="server" CommandName="viewTD" CssClass="btn btn-sm btn-warning" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="View bus Details"> <i class="fa fa-eye"></i></asp:LinkButton>
													<asp:LinkButton ID="lbtnUpdate" Visible="true" runat="server" CommandName="updateTD" CssClass="btn btn-sm btn-info" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Update bus Details"> <i class="fa fa-edit"></i></asp:LinkButton>

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
									<div class="col-lg-12 text-center mt-4" runat="server" id="divNoEntry">
										<span class="h2 font-weight-bold mb-0">Start Bus Creation </span>
										<h5 class="card-title text-uppercase text-muted mb-0">No Bus has been registered yet</h5>
									</div>
									<div class="col-lg-12 text-center mt-4" runat="server" id="divnosearchrecord" visible="false">
										<span class="h2 font-weight-bold mb-0">No Record Found </span>
									</div>
								</div>
							</div>
						</div>
					</asp:Panel>
				</div>
			</div>
			<div class="col-xl-8 order-xl-2">
				<asp:Panel runat="server" ID="pnlAddDetails">
					<div class="card" style="min-height: 800px">
						<div class="card-header">
							<div class="row align-items-center m-0">
								<div class="col-8">
									<h3 class="mb-0">
										<asp:Label runat="server" ID="lblHead" Text="Add New Entry"></asp:Label>
									</h3>
								</div>
								<div class="col-md-4 text-right">
									<p style="font-size: small; margin: 0px;" class="text-danger font-weight-bold">All Fields are mandatory</p>
								</div>
							</div>
						</div>
						<div class="card-body">
							<h6 class="heading-small my-0">1. Bus Service Details</h6>
							<div class="pl-lg-4">
								<div class="row">
									<div class="col-lg-6">
										<asp:Label runat="server" CssClass="text-muted form-control-label">Route </asp:Label>
										<asp:DropDownList ID="ddlRoute" AutoPostBack="true" OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged" data-toggle="tooltip" data-placement="bottom" title="Bus Service Type" CssClass="form-control form-control-sm" runat="server">
											<asp:ListItem Value="0" Text="Select"></asp:ListItem>
										</asp:DropDownList>
									</div>
									<div class="col-lg-3">
										<asp:Label runat="server" CssClass="text-muted form-control-label">Service type</asp:Label><br />
										<asp:DropDownList ID="ddlServiceType" AutoPostBack="true" OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged" data-toggle="tooltip" data-placement="bottom" title="Bus Service Type" CssClass="form-control form-control-sm" runat="server">
											<asp:ListItem Value="0" Text="Select"></asp:ListItem>
										</asp:DropDownList>
									</div>
									<div class="col-lg-3">
										<asp:Label runat="server" CssClass="text-muted form-control-label">Bus Owner type</asp:Label><br />
										<asp:DropDownList ID="ddlBusOwnerType" AutoPostBack="true" OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged" data-toggle="tooltip" data-placement="bottom" title="Bus Service Type" CssClass="form-control form-control-sm" runat="server">
											<asp:ListItem Value="0" Text="Select"></asp:ListItem>
										</asp:DropDownList>
									</div>
								</div>
								<div class="row mt-1">
									<div class="col-lg-9">
										<asp:Label runat="server" CssClass="text-muted form-control-label">Depot Service</asp:Label>
										<asp:DropDownList ID="ddlDepotService" data-toggle="tooltip" data-placement="bottom" title="Bus Service Type" CssClass="form-control form-control-sm" runat="server">
											<asp:ListItem Value="0" Text="Select"></asp:ListItem>
										</asp:DropDownList>
									</div>
								</div>
								<div class="row mt-1">
									<div class="col-lg-3">
										<asp:Label runat="server" CssClass="text-muted form-control-label">Layout</asp:Label><br />
										<asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="ddlLayout_SelectedIndexChanged" ID="ddlLayout" data-toggle="tooltip" data-placement="bottom" title="Bus Service Type" CssClass="form-control form-control-sm" runat="server">
											<asp:ListItem Value="0" Text="Select"></asp:ListItem>
										</asp:DropDownList>
									</div>
									<div class="col-lg-3">
										<asp:Label runat="server" CssClass="text-muted form-control-label"> Seating Capacity</asp:Label><br />
										<asp:TextBox ID="tbSeatingCapacity" MaxLength="20" autocomplete="off" placeholder="Max 20 Characters" data-toggle="tooltip" data-placement="bottom" title="Seating Capacity" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
										<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbSeatingCapacity" ValidChars=" _/." />
									</div>
									<div class="col-lg-3">
										<asp:Label runat="server" CssClass="text-muted form-control-label"> Load Factor</asp:Label><br />
										<asp:DropDownList ID="ddlLoadFactor" data-toggle="tooltip" data-placement="bottom" title="Bus Service Type" CssClass="form-control form-control-sm" runat="server">
											<asp:ListItem Value="0" Text="Select"></asp:ListItem>
										</asp:DropDownList>
									</div>
									<div class="col-lg-3">
										<asp:Label runat="server" CssClass="text-muted form-control-label">Wheelbase</asp:Label>
										<asp:DropDownList ID="ddlWheelBase" data-toggle="tooltip" data-placement="bottom" title="Bus Service Type" CssClass="form-control form-control-sm" runat="server">
											<asp:ListItem Value="0" Text="Select"></asp:ListItem>
										</asp:DropDownList>
									</div>
								</div>
							</div>

							<h6 class="heading-small my-0 mt-2">2. Target Details</h6>
							<div class="pl-lg-4">
								<div class="row">
									<div class="col-lg-3">
										<asp:Label runat="server" CssClass="text-muted form-control-label">Target Income Per KM (₹)</asp:Label>
										<asp:TextBox ID="tbTargetIncomePerKm" ToolTip="Enter Target Income Per Km" runat="server" CssClass="form-control form-control-sm" MaxLength="6" placeholder="Max 6 Digit" autocomplete="off"></asp:TextBox>
										<cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbTargetIncomePerKm" ValidChars="." />
									</div>
									<div class="col-lg-3">
										<asp:Label runat="server" CssClass="text-muted form-control-label"> Target Income (₹)</asp:Label>
										<asp:TextBox ID="tbTargetIncome" ToolTip="Enter Target Income" runat="server" CssClass="form-control form-control-sm" MaxLength="6" placeholder="Max 6 Digit" autocomplete="off"></asp:TextBox>
										<cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbTargetIncome" ValidChars="." />
									</div>
									<div class="col-lg-3">
										<asp:Label ID="lblGPSCompany" runat="server" CssClass="text-muted form-control-label"> Diesel Average (Hill)</asp:Label>
										<asp:TextBox ID="tbDieselAvgHill" ToolTip="Enter Diesel Average (Hill)" runat="server" CssClass="form-control form-control-sm" MaxLength="6" placeholder="Max 6 Digit" autocomplete="off"></asp:TextBox>
										<cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbDieselAvgHill" ValidChars="." />
									</div>
									<div class="col-lg-3">
										<asp:Label ID="Label2" runat="server" CssClass="text-muted form-control-label"> Diesel Average (Plain)</asp:Label>
										<asp:TextBox ID="tbDieselAvgPlain" ToolTip="Enter Diesel Average (Plain)" runat="server" CssClass="form-control form-control-sm" MaxLength="6" placeholder="Max 6 Digit" autocomplete="off"></asp:TextBox>
										<cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbDieselAvgPlain" ValidChars="." />
									</div>
								</div>
							</div>

							<h6 class="heading-small my-0 mt-2">3. Validity</h6>
							<div class="pl-lg-4">
								<div class="row">
									<div class="col-lg-3">
										<asp:Label runat="server" CssClass="text-muted  form-control-label">From Date</asp:Label>
										<div class="input-group date">
											<asp:TextBox ID="tbValidFrom" ToolTip="Enter Valid from Date" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
											<cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbValidFrom" ValidChars="/" />
										</div>
									</div>
									<div class="col-lg-3">
										<asp:Label runat="server" CssClass="text-muted  form-control-label">To Date</asp:Label>
										<div class="input-group date">
											<asp:TextBox ID="tbValidTo" ToolTip="Enter Valid To Date" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
											<cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbValidTo" ValidChars="/" />
										</div>
									</div>
								</div>
							</div>
							<div class="pl-lg-4 mt-3 mb-2">
								<div class="row">
									<div class="col-lg-12 text-left">
										<asp:LinkButton ID="lbtnUpdate" runat="server" class="btn btn-success" OnClick="updateDetails_Click" Visible="false" data-toggle="tooltip" data-placement="bottom" title="Update Bus Details">
                                    <i class="fa fa-save"></i>&nbsp; Update</asp:LinkButton>
										<asp:LinkButton ID="lbtnSave" Visible="true" runat="server" class="btn btn-success" OnClick="saveDetails_Click" data-toggle="tooltip" data-placement="bottom" title="Save Bus Details">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>
										<asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-danger" OnClick="resetDetails_Click" data-toggle="tooltip" data-placement="bottom" title="Reset Bus Details">
                                    <i class="fa fa-undo"></i>&nbsp; Reset</asp:LinkButton>
										<asp:LinkButton ID="lbtnCancel" Visible="false" runat="server" OnClick="resetDetails_Click" CssClass="btn btn-danger" data-toggle="tooltip" data-placement="bottom" title="Cancel Bus Details">
                                    <i class="fa fa-times"></i>&nbsp; Cancel</asp:LinkButton>
									</div>
								</div>
							</div>
						</div>
					</div>
				</asp:Panel>
				<asp:Panel runat="server" ID="pnlViewDetails" Visible="false">
					<div class="card" style="min-height: 800px">
						<div class="card-header">
							<div class="row align-items-center m-0">
								<div class="col-11">
									<h3 class="mb-0">
										<asp:Label runat="server" ID="lblViewServicehead" Text="View Details"></asp:Label>
									</h3>
								</div>
								<div class="col-1 float-right">
                                    <asp:LinkButton ID="lbtnCloseViewBus" runat="server" OnClick="resetDetails_Click"  class="btn btn-sm btn-danger float-right" Style="border-radius: 25px; left: 23px; margin-top: -36px;"> <i class="fa fa-times" ></i> </asp:LinkButton>
                                </div>
							</div>
						</div>
						<div class="card-body">
							<h6 class="heading-small my-0">1. Bus Service Details</h6>
							<div class="pl-lg-4">
								<div class="row mt-1">
									<div class="col-lg-3">
										<asp:Label runat="server" CssClass="text-muted form-control-label">Layout</asp:Label><br />
										<asp:Label runat="server" ID="lblLayout" CssClass="form-control-label font-weight-bold text-sm"></asp:Label>
									</div>
									<div class="col-lg-3">
										<asp:Label runat="server" CssClass="text-muted form-control-label"> Seating Capacity</asp:Label><br />
										<asp:Label runat="server" ID="lblSeatingCapacity" CssClass="form-control-label font-weight-bold text-sm"></asp:Label>
									</div>
									<div class="col-lg-3">
										<asp:Label runat="server" CssClass="text-muted form-control-label"> Load Factor</asp:Label><br />
										<asp:Label runat="server" ID="lblLoadFactor" CssClass="form-control-label font-weight-bold text-sm"></asp:Label>
									</div>
									<div class="col-lg-3">
										<asp:Label runat="server" CssClass="text-muted form-control-label">Wheelbase</asp:Label><br />
										<asp:Label runat="server" ID="lblWheelbase" CssClass="form-control-label font-weight-bold text-sm"></asp:Label>
									</div>
								</div>
							</div>

							<h6 class="heading-small my-0 mt-2">2. Target Details</h6>
							<div class="pl-lg-4">
								<div class="row">
									<div class="col-lg-3">
										<asp:Label runat="server" CssClass="text-muted form-control-label">Target Income Per KM (₹)</asp:Label><br />
										<asp:Label runat="server" ID="lblTargetIncomePerKm" CssClass="form-control-label font-weight-bold text-sm"></asp:Label>
									</div>
									<div class="col-lg-3">
										<asp:Label runat="server" CssClass="text-muted form-control-label"> Target Income (₹)</asp:Label><br />
									<asp:Label runat="server" ID="lblTargetIncome" CssClass="form-control-label font-weight-bold text-sm"></asp:Label>
									</div>
									<div class="col-lg-3">
										<asp:Label ID="Label4" runat="server" CssClass="text-muted form-control-label"> Diesel Average (Hill)</asp:Label><br />
										<asp:Label runat="server" ID="lblDieselAvgHill" CssClass="form-control-label font-weight-bold text-sm"></asp:Label>
									</div>
									<div class="col-lg-3">
										<asp:Label ID="Label5" runat="server" CssClass="text-muted form-control-label"> Diesel Average (Plain)</asp:Label><br />
										<asp:Label runat="server" ID="lblDieselAvgPlain" CssClass="form-control-label font-weight-bold text-sm"></asp:Label>
									</div>
								</div>
							</div>

							<h6 class="heading-small my-0 mt-2">3. Validity</h6>
							<div class="pl-lg-4">
								<div class="row">
									<div class="col-lg-3">
										<asp:Label runat="server" CssClass="text-muted  form-control-label">From Date</asp:Label><br />
										<asp:Label runat="server" id="lblValidFromDate" CssClass="form-control-label font-weight-bold text-sm"></asp:Label>
									</div>
									<div class="col-lg-3">
										<asp:Label runat="server" CssClass="text-muted  form-control-label">To Date</asp:Label><br />
										<asp:Label runat="server" id="lblValidToDate" CssClass="form-control-label font-weight-bold text-sm"></asp:Label>
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

