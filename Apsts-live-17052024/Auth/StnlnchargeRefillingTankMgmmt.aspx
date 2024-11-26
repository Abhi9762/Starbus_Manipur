<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/StnInchgemaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="StnlnchargeRefillingTankMgmmt.aspx.cs" Inherits="Auth_StnlnchargeRefillingTankMgmmt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
	<script type="text/javascript">       

		$(document).ready(function () {

			var currDate = new Date().getDate();
			var preDate = new Date(new Date().setDate(currDate - 1));
			var todayDate = new Date(new Date().setDate(currDate));

			$('[id*=tbRefuelledDate]').datepicker({
				endDate: todayDate,
				changeMonth: true,
				changeYear: false,
				format: "dd/mm/yyyy",
				autoclose: true
			});
			$('[id*=tbReceiptDate]').datepicker({
				endDate: todayDate,
				changeMonth: true,
				changeYear: false,
				format: "dd/mm/yyyy",
				autoclose: true
			});

		});
		function UploadPDF(fileUpload) {
			//alert(1);
			if ($('#fudocfile').value != '') {
				document.getElementById("<%=btnUploadpdf.ClientID %>").click();
			}
		}
	</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<div class="header pb-7 ">
	</div>
	<div class="container-fluid mt--6 mb-2">
		<div class="row align-items-center">
			<div class="col-md-12 ">
				<div class="card card-stats mb-3">
					<div class="row m-0">
						<div class="col-md-4 border-right">
							<div class="card-body">
								<div class="row m-0">
									<div class="col-12">
										<div class=" text-capitalize" style="font-size: medium; font-weight: bold">
											<asp:Label ID="lblDateTime" runat="server" class="h3 font-weight-bold mb-0"> Summary as on Date </asp:Label>
										</div>
										<div class="row m-0">
											<div class="col-12">
												<label class="form-control-label">Filling Stations</label>&nbsp;&nbsp;
                                                    <asp:Label ID="lblTotalFillingSt" runat="server" CssClass="h3 font-weight-bold mb-0 float-right">0</asp:Label>
											</div>
										</div>
										<div class="row m-0">
											<div class="col-12">
												<label class="form-control-label">Tanks</label>
												<asp:Label ID="lblTotalTanks" runat="server" class="h3  font-weight-bold mb-0 float-right">0</asp:Label>
											</div>
										</div>
										<div class="row m-0">
											<div class="col-12">
												<label class="form-control-label">Pumps</label>
												&nbsp;&nbsp;
                                                <asp:Label ID="lblTotalPumps" runat="server" class="h3 font-weight-bold mb-0 float-right">0</asp:Label>
											</div>
										</div>
									</div>

								</div>

							</div>
						</div>
						<div class="col-3 border-right">
							<div class="card-body">
								<div class="row">
									<div class="col">
										<div>
											<h4 class="mb-1">Generate Refueling Tank Report</h4>
										</div>
										<div class="input-group mb-3">
											<div class="input-group-prepend pr-2" style="width: 80%">
												<asp:DropDownList ID="ddlRefuelingTankReport" data-toggle="tooltip" data-placement="bottom" title="Refueling Tank" CssClass="form-control form-control-sm" runat="server">
													<asp:ListItem Value="0" Text="Select"></asp:ListItem>
												</asp:DropDownList>
											</div>
											<asp:LinkButton ID="lbtnDownloadRefuelingRpt" data-toggle="tooltip" data-placement="bottom" title="Download" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white">
                                            <i class="fa fa-download"></i>
											</asp:LinkButton>
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
											<li class="form-control-label">
												Tank refueling details are entered here.
											</li>
											<li class="form-control-label">
												Refueling quantity shoundn't be greater than tha tank capacity or available quantity.
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
			<div class="col-lg-6 col-md-6 col-sm-12">
				<div class="card mb-3" style="min-height: 500px;">
					<div class="card-body">
						<div class="card-header border-bottom">
							<div class="row m-0">
								<div class="col-md-12">
									<asp:Label runat="server" Font-Bold="true" CssClass="form-control-label"><h3>Refueling Tank Entry Details</h3></asp:Label>
								</div>

							</div>
						</div>
						<div class="row m-0">
							<div class="col-md-12">
								<asp:GridView ID="gvFillingStation" runat="server" OnRowCommand="gvFillingStation_RowCommand" AutoGenerateColumns="False" PageSize="7" GridLines="None" AllowPaging="true"
									CssClass="table table-striped table-hover" HeaderStyle-CssClass="thead-light font-weight-bold" PagerStyle-CssClass="GridPager" Font-Size="10pt" Width="100%" OnPageIndexChanging="gvFillingStation_PageIndexChanging"
									DataKeyNames="refuelingid,capacity,availableqty,refuelledqty,refuelleddate,receiptnumber,receiptdate,tankno,fillingstationid,tankernumber,temperature,actualdensity,checkeddensity,uploadedinvoice">
									<Columns>
										<asp:TemplateField HeaderText="Tank No">
											<ItemTemplate>
												<asp:Label ID="lblTank" runat="server" Text='<%#Eval("tankno")%>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Capacity">
											<ItemTemplate>
												<asp:Label ID="lblCapacity" runat="server" Text='<%#Eval("capacity")%>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Available </br> Qty (at Time</br>of Refuel)">
											<ItemTemplate>
												<asp:Label ID="lblAvQty" runat="server" Text='<%#Eval("availableqty")%>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Refueling <br> Qty.">
											<ItemTemplate>
												<asp:Label ID="lblgvQty" runat="server" Text='<%#Eval("refuelledqty")%>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Refueling <br> Date">
											<ItemTemplate>
												<asp:Label ID="lblgvRefillingDate" runat="server" Text='<%#Eval("refuelleddate")%>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Receipt No">
											<ItemTemplate>
												<asp:Label ID="lblgvReceiptNumber" runat="server" Text='<%#Eval("receiptnumber")%>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField>
											<ItemTemplate>
												<asp:LinkButton ID="gvlnkbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Click here for edit" CssClass="btn btn-sm btn-warning" CommandName="gvEdit"><i class="fa fa-edit"></i></asp:LinkButton>
												<asp:LinkButton ID="gvlnkbtndelete" runat="server" Visible="false" ToolTip="Click here for delete" CssClass="btn btn-sm btn-danger" CommandName="gvDelete"><i class="fa fa-times"></i></asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateField>
									</Columns>
									<PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
								</asp:GridView>
								<asp:Panel ID="pnlNoRecord" runat="server" Width="100%" Visible="true">
									<div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
										<div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
											No Record Available<br />
										</div>
									</div>
								</asp:Panel>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div class="col-lg-6 col-md-6 col-sm-12">
				<div class="card mb-3" style="min-height: 500px;">
					<div class="card-body pt-2">
						<div class="card-header border-bottom">
							<div class="row m-0">
								<div class="col-md-12">
									<asp:Label runat="server" ID="lblAddRefuelingDetails" Font-Bold="true" CssClass="form-control-label"><h3> Add Refueling Details</h3></asp:Label>
									<asp:Label runat="server" ID="lblUpdateRefuelingDetails" Visible="false" Font-Bold="true" CssClass="form-control-label"><h3> Update Refueling Details</h3></asp:Label>
								</div>

							</div>
						</div>
						<div class="row m-0 ml-3 mt-2">
							<div class="col-4">
								<asp:Label runat="server" CssClass="form-control-label">Filling Station<span style="color: red">*</span></asp:Label>
								<asp:DropDownList ID="ddlFillingStation" OnSelectedIndexChanged="ddlFillingStation_SelectedIndexChanged" runat="server" ToolTip="Select Filling Station" AutoPostBack="true"
									class="form-control form-control-sm">
								</asp:DropDownList>
							</div>
							<div class="col-4">
								<asp:Label runat="server" CssClass="form-control-label">Tank<span style="color: red">*</span></asp:Label>
								<asp:DropDownList ID="ddlTank" runat="server" ToolTip="Select Filling Station" AutoPostBack="true" OnSelectedIndexChanged="ddlTank_SelectedIndexChanged"
									class="form-control form-control-sm">
								</asp:DropDownList>
							</div>
							<div class="col-4">
								<asp:Label runat="server" CssClass="form-control-label">Available Qty</asp:Label>
								<asp:TextBox ID="tbAvailableQty" class="form-control form-control-sm" ReadOnly="true" Text="0" ToolTip="Available Qty." placeholder="Max Length 5 (In litres)" autocomplete="off" MaxLength="5" runat="server"></asp:TextBox>
								<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom,Numbers"
									TargetControlID="tbAvailableQty" ValidChars="." />
							</div>
						</div>
						<div class="row m-0 ml-3 mt-2">
							<div class="col-4">
								<asp:Label runat="server" CssClass="form-control-label">Refueled Qty<span style="color: red">*</span></asp:Label>
								<asp:TextBox ID="tbRefuelledQuantity" class="form-control form-control-sm" ToolTip="Enter Refuelled Quantity" MaxLength="5" placeholder="Max Length 5 (In litres)" runat="server" autocomplete="off"></asp:TextBox>
								<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom,Numbers"
									TargetControlID="tbRefuelledQuantity" ValidChars="." />
								<asp:HiddenField ID="hdnReFillingSTNID" runat="server" />
							</div>
							<div class="col-4">
								<asp:Label runat="server" CssClass="form-control-label">Refueled on Date<span style="color: red">*</span></asp:Label>
								<asp:TextBox class="form-control form-control-sm" runat="server" ID="tbRefuelledDate" MaxLength="10" placeholder="DD/MM/YYYY"
									Text="" autocomplete="off"></asp:TextBox>
								<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,Custom"
									TargetControlID="tbRefuelledDate" ValidChars="/" />
							</div>
						</div>
						<div class="row m-0 ml-3 mt-1">
							<div class="col-4">
								<asp:Label runat="server" CssClass="form-control-label">Receipt Number<span style="color: red">*</span></asp:Label>
								<asp:TextBox ID="tbReceiptNumber" class="form-control form-control-sm" MaxLength="15" ToolTip="Enter Receipt Number" placeholder="Max 15 Characters" runat="server" autocomplete="off"></asp:TextBox>

							</div>
							<div class="col-4">
								<asp:Label runat="server" CssClass="form-control-label">Receipt on Date<span style="color: red"> (if any)</span></asp:Label>
								<asp:TextBox class="form-control form-control-sm" runat="server" ID="tbReceiptDate" MaxLength="10" placeholder="DD/MM/YYYY"
									Text="" autocomplete="off"></asp:TextBox>
								<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom"
									TargetControlID="tbReceiptDate" ValidChars="/" />
							</div>
						</div>
						<div class="col-md-12 ml-3">
							<asp:Label runat="server" Font-Bold="true" CssClass="form-control-label"><h3>Invoice Details</h3></asp:Label>
						</div>
						<hr style="margin: 0px;" />
						<div class="row ml-3 mt-1 m-0">
							<div class="col-4">
								<asp:Label runat="server" CssClass="form-control-label">Tanker Number</asp:Label>
								<asp:TextBox ID="tbTankerNo" class="form-control form-control-sm" MaxLength="15" ToolTip="Enter Tanker Number" placeholder="Max 15 Characters" runat="server" autocomplete="off"></asp:TextBox>

							</div>
							<div class="col-4">
								<asp:Label runat="server" CssClass="form-control-label">Actual Density(As per invoice)</asp:Label>
								<asp:TextBox ID="tbActualDensity" class="form-control form-control-sm" MaxLength="5" ToolTip="Enter Actual Density" placeholder="Max Length 5 (In litres)" runat="server" autocomplete="off"></asp:TextBox>
								<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom,Numbers"
									TargetControlID="tbActualDensity" ValidChars="." />

							</div>
							<div class="col-4">
								<asp:Label runat="server" CssClass="form-control-label">Checked Density</asp:Label>
								<asp:TextBox ID="tbCheckedDensity" class="form-control form-control-sm" MaxLength="5" ToolTip="Enter Checked Density" placeholder="Max Length 5 (In litres)" runat="server" autocomplete="off"></asp:TextBox>
								<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom,Numbers"
									TargetControlID="tbCheckedDensity" ValidChars="." />
							</div>
						</div>
						<div class="row m-0 ml-3 mt-2">
							<div class="col-4">
								<asp:Label runat="server" CssClass="form-control-label">Temperature</asp:Label>
								<asp:TextBox ID="tbTemperature" class="form-control form-control-sm" MaxLength="5" ToolTip="Enter Temperature" placeholder="Max Length 5" runat="server" autocomplete="off"></asp:TextBox>
								<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom,Numbers"
									TargetControlID="tbTemperature" ValidChars="." />
							</div>
							<div class="col-8">
								<asp:Label runat="server" CssClass="form-control-label">Upload Invoice</asp:Label><br />
								<asp:Button ID="btnUploadpdf" OnClick="btnUploadpdf_Click" runat="server" CausesValidation="False" CssClass="button1"
									Style="display: none" TabIndex="18" Text="Upload Image" Width="80px" />
								<asp:FileUpload ID="fudocfile" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-success btn-sm"
									onchange="UploadPDF(this);" TabIndex="9" accept="application/pdf" />
								<asp:Label runat="server" ID="lblPDF" CssClass="col-form-label control-label " Style="font-size: 12px; color: red; font-weight: normal;"></asp:Label>
								<asp:LinkButton runat="server" ID="lbtnInvoice" Font-Underline="true" title="View Uploaded Invoice" Style="font-size: 12px; color: red; font-weight: normal;" OnClick="viewdoc_click"
									Visible="false"></asp:LinkButton><br />
								<span style="font-size: 7pt; color: Red; line-height: 12px;">Only PDF Allowed. (File size max 2MB)</span>
							</div>
						</div>
						<div class="row m-0 mt-3 ml-3">
							<div class="col-12 text-left">
								<asp:LinkButton ID="lbtnUpdate" Visible="false" runat="server" CommandName="Update" OnClick="lbtnUpdate_Click" ToolTip="Click here for update" CssClass="btn  btn-primary"><i class="fa fa-save"></i>&nbsp;Update</asp:LinkButton>
								<asp:LinkButton ID="lbtnSave" runat="server" OnClick="lbtnSave_Click" CommandName="Save" ToolTip="Click here for save" CssClass="btn btn-success"><i class="fa fa-save"></i>&nbsp;Save</asp:LinkButton>
								<asp:LinkButton ID="lbtnReset" runat="server" OnClick="lbtnReset_Click" CommandName="Reset" ToolTip="Click here for reset" CssClass="btn  btn-danger"><i class="fa fa-undo"></i>&nbsp;Reset</asp:LinkButton>
							</div>
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

