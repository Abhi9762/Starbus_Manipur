<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="SysAdminCity.aspx.cs" Inherits="Auth_SysAdminCity" MaintainScrollPositionOnPostback="true" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<div class="header pb-7">
	</div>
    <asp:HiddenField ID="hidtoken" runat="server" />
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
														<h5 class="card-title text-uppercase text-muted mb-0">Total City:&nbsp;
                                 <asp:Label ID="lblTotalCity" runat="server" ToolTip="Total Cities Available" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
													</div>
													<div class="col-12">
														<h5 class="card-title text-uppercase text-muted mb-0">Active Cities: &nbsp;
                                <asp:Label ID="lblActive" runat="server" ToolTip="Active Cities" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
													</div>
												</div>
											</div>
											<div class="col-6">
												<div class="row m-0">
													<div class="col-12">
														<h5 class="card-title text-uppercase text-muted mb-0">Deactive Cities:&nbsp;
                                 <asp:Label ID="lblDeactive" runat="server" ToolTip="Deactive Cities" class="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
													</div>
													<div class="col-12">
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
											<h4 class="mb-1">Generate City Report</h4>
										
										
											<asp:LinkButton ID="lbtndownload" ToolTip="Download Report" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white ml-2">
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
										<asp:Label runat="server" CssClass="form-control-label">1. Here you can Add/Update state wise city.</asp:Label>
									</div>
									<div class="col-auto">
										<asp:LinkButton ID="lbtnview" OnClick="lbtnview_Click" ToolTip="View Instructions" runat="server" Width="30px" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
										</asp:LinkButton>
										<asp:LinkButton runat="server" ID="lbtndwnlinst" OnClick="lbtndwnlinst_Click" class="btn btn bg-gradient-green btn-sm text-white" Width="30px" ToolTip="Download Instructions">
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
				<asp:Panel ID="pnlCityDetails" runat="server" Visible="true">
					<div class="card" style="min-height: 470px">
						<div class="card-header border-bottom">
							<div class="float-left">
								<h3 class="mb-0">City List</h3>
							</div>
							<div class="float-right">
								<div class="input-group">
									<asp:TextBox ID="tbSearch" ToolTip="Enter City" CssClass="form-control form-control-sm mr-1" MaxLength="20" runat="server" Width="200px" placeholder="Search City" autocomplete="off"></asp:TextBox>

									<asp:LinkButton ID="lbtnSearchCity" ToolTip="Search City" runat="server" CssClass="btn bg-success btn-sm text-white mr-1" OnClick="lbtnSearchCity_Click">
                                            <i class="fa fa-search"></i>
									</asp:LinkButton>
									<asp:LinkButton ID="lbtnResetSearchCity" ToolTip="Reset City" runat="server" CssClass="btn bg-warning btn-sm text-white" OnClick="lbtnResetCity_Click">
                                            <i class="fa fa-undo"></i>
									</asp:LinkButton>
								</div>
							</div>
						</div>
						<div class="card-body p-0">
							<div class="row m-0 align-items-center">
								<div class="col-lg-12">
									<asp:GridView ID="gvCity" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover" GridLines="None"
										AllowPaging="true" PageSize="10" OnPageIndexChanging="gvCity_PageIndexChanging" ShowHeader="false"
										HeaderStyle-CssClass="thead-light font-weight-bold" OnRowCommand="gvCity_RowCommand" Width="100%" OnRowDataBound="gvCity_RowDataBound"
										DataKeyNames="state_code, district_code, city_code, city, citylocal, status, state, district, totalcity, deleteyn, actcity, deactcity">
										<Columns>
											<asp:TemplateField HeaderText="City" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
												<ItemTemplate>
													<div class="row px-3 pt-2">
														<div class="col">
															<h3 class="text-uppercase mb-0 font-weight-bold text-sm">
																<%# Eval("city") %></h3>
															<h5 class="font-weight-normal"><span class="text-gray"><%# Eval("district") %> , <%# Eval("state") %></span></h5>

														</div>
														<div class="col-right">
															<asp:LinkButton ID="lbtnActivate" Visible="true" runat="server" CommandName="activate" CssClass="btn btn-sm btn-success" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" ToolTip="Activate City"> <i class="fa fa-check"></i></asp:LinkButton>
															<asp:LinkButton ID="lbtnDiscontinue" Visible="true" runat="server" CommandName="deactivate" CssClass="btn btn-sm btn-warning" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" ToolTip="Deactivate City"> <i class="fa fa-ban"></i></asp:LinkButton>
															<asp:LinkButton ID="lbtnUpdateCity" Visible="true" runat="server" CommandName="UpdateCity" CssClass="btn btn-sm btn-default" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" ToolTip="Update City"> <i class="fa fa-edit"></i></asp:LinkButton>
															<asp:LinkButton ID="lbtnDeleteCity" Visible="true" runat="server" CommandName="deleteCity" CssClass="btn btn-sm btn-danger" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" ToolTip="Delete City"> <i class="fa fa-trash"></i></asp:LinkButton>
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
											City Not Available
										</p>
									</asp:Panel>
								</div>
							</div>
						</div>
					</div>
				</asp:Panel>
			</div>
			<div class="col-xl-8 order-xl-2">				
				<asp:Panel runat="server" ID="pnlAddCity" Visible="true">
					<div class="card" style="min-height: 470px">
						<div class="card-header border-bottom">
							<div class="row m-0">
								<div class="col-md-6 text-left">
									<h3 class="mb-0" runat="server" id="lblAddCityHeader" visible="true">Add New City</h3>
									<h3 class="mb-0" runat="server" id="lblUpdateCityHeader" visible="false">Update City</h3>
								</div>
								<div class="col-md-6 text-right">
									<p style="font-size: small; margin: 0px;" class="text-danger font-weight-bold">All marked * Fields are mandatory</p>
								</div>
							</div>
						</div>
						<div class="card-body">
							<div class="row m-0 align-items-center">

								<div class="row m-0">
									<div class="col-lg-12">
										<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="Medium">1. City Name</asp:Label>
									</div>
									<div class="col-lg-12">
										<div class="row m-0">
											<div class="col-lg-4">
												<asp:Label runat="server" CssClass="form-control-label"> English<span style="color: red">*</span></asp:Label>
												<asp:TextBox class="form-control form-control-sm" runat="server" ID="tbCityNameEn" MaxLength="50" ToolTip="City Name in English" autocomplete="off"
													placeholder="Max 50 Characters" Text="" Style=""></asp:TextBox>
												<cc1:FilteredTextBoxExtender ID="ajxFtCityName" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbCityNameEn" ValidChars=" " />
											</div>
											<div class="col-lg-4">
												<asp:Label runat="server" CssClass="form-control-label"> Local Language</asp:Label>
												<asp:TextBox class="form-control form-control-sm" runat="server" ID="tbCityNameLocal" MaxLength="50" ToolTip="City Name in Local Language" autocomplete="off"
													placeholder="Max 50 Characters" Text="" Style=""></asp:TextBox>
												<cc1:FilteredTextBoxExtender ID="ajxFtLocal" runat="server" FilterType="UppercaseLetters, LowercaseLetters" TargetControlID="tbCityNameLocal" />
											</div>
										</div>
									</div>
									<div class="col-lg-12 mt-2">
										<asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="Medium">2. Address</asp:Label>
									</div>
									<div class="col-lg-12">
										<div class="row m-0">
											<div class="col-lg-4">
												<asp:Label runat="server" CssClass="form-control-label"> State<span style="color: red">*</span></asp:Label>
												<asp:DropDownList ID="ddlCityState" OnSelectedIndexChanged="ddlCityState_SelectedIndexChanged" ToolTip="City State" CssClass="form-control form-control-sm" runat="server" AutoPostBack="true"></asp:DropDownList>
											</div>
											<div class="col-lg-4">
												<asp:Label runat="server" CssClass="form-control-label">District<span style="color: red">*</span></asp:Label>
												<asp:DropDownList ID="ddlCityDistrict" ToolTip="City District" CssClass="form-control form-control-sm" runat="server" AutoPostBack="true"></asp:DropDownList>
											</div>

										</div>
									</div>

									<div class="col-lg-12 mt-3 text-center">

										<asp:LinkButton ID="lbtnUpdate" runat="server" class="btn btn-success" Visible="false" ToolTip="Update City" OnClick="updateCity_Click">
                                    <i class="fa fa-save"></i>&nbsp; Update</asp:LinkButton>
										<asp:LinkButton ID="lbtnSave" Visible="true" runat="server" class="btn btn-success" ToolTip="Save City" OnClick="saveCity_Click">
                                    <i class="fa fa-save"></i>&nbsp; Save</asp:LinkButton>
										<asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-danger" ToolTip="Reset City" OnClick="resetCity_Click">
                                    <i class="fa fa-undo"></i>&nbsp; Reset</asp:LinkButton>
										<asp:LinkButton ID="lbtncancel" Visible="false" runat="server" class="btn btn-warning" ToolTip="Cancel Add City" OnClick="cancelCity_Click">
                                    <i class="fa fa-times"></i>&nbsp; Cancel</asp:LinkButton>

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
</asp:Content>

