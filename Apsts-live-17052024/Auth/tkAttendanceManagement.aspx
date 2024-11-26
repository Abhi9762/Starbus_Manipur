<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/TimeKeeperMaster.master" AutoEventWireup="true" CodeFile="tkAttendanceManagement.aspx.cs" Inherits="Auth_tkAttendanceManagement" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
	<script type="text/javascript">       

		$(document).ready(function () {

			var currDate = new Date().getDate();
			var preDate = new Date(new Date().setDate(currDate - 1));
			var todayDate = new Date(new Date().setDate(currDate));

			$('[id*=tbAttendanceDate]').datepicker({
				//startDate: preDate,
				endDate: todayDate,
				changeMonth: true,
				changeYear: false,
				format: "dd/mm/yyyy",
				autoclose: true
			});
			$('[id*=tbLeaveStartDate]').datepicker({
				//startDate: todayDate,
				changeMonth: true,
				changeYear: false,
				format: "dd/mm/yyyy",
				autoclose: true
			})
				.on('changeDate', function (selected) {
					var minDate = new Date(selected.date.valueOf());
					$('[id*=tbLeaveEndDate]').datepicker('setStartDate', minDate);
				});
			$('[id*=tbLeaveEndDate]').datepicker({
				//startDate: todayDate,
				changeMonth: true,
				changeYear: false,
				format: "dd/mm/yyyy",
				autoclose: true
			});
		});

		function UploadImage(fileUpload) {
			if (fileUpload.value != '') {
				document.getElementById("<%=btnUploadImage.ClientID %>").click();
			}
		}
		function UploadImage2(fileUpload) {
			if (fileUpload.value != '') {
				document.getElementById("<%=btnUploadImage2.ClientID %>").click();
			}
		}

	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<div class="container-fluid pt-2 pb-5">
		<div class="row align-items-center">
			<div class="col-xl-12">
				<div class="card card-body card-stats mb-3">
					<div class="row">
						<div class="col-md-3">
							<h5 class="card-title text-muted mb-0">Designation</h5>
							<asp:DropDownList ID="ddlRole" runat="server" ToolTip="Designation" class="form-control form-control-sm">
							</asp:DropDownList>
						</div>
						<div class="col-md-2">
							<h5 class="card-title text-muted mb-0">Status</h5>
							<asp:DropDownList ID="ddlStatusType" runat="server" ToolTip="Attendance Status"
								class="form-control form-control-sm">
								<asp:ListItem Value="O" Text="All"></asp:ListItem>
								<asp:ListItem Value="N" Text="Not Marked" Selected="True"></asp:ListItem>
								<asp:ListItem Value="L" Text="Leave"></asp:ListItem>
								<asp:ListItem Value="P" Text="Present"></asp:ListItem>
								<asp:ListItem Value="A" Text="Absent"></asp:ListItem>
								<asp:ListItem Value="D" Text="Present & On Duty"></asp:ListItem>
							</asp:DropDownList>
						</div>
						<div class="col-md-2">
							<h5 class="card-title text-muted mb-0">Emp Code/Name</h5>
							<asp:TextBox ID="tbSearchText" runat="server" CssClass="form-control form-control-sm" placeholder="Search Text" AutoComplete="Off"
								MaxLength="10" Style=""></asp:TextBox>
						</div>
						<div class="col-md-2">
							<h5 class="card-title text-muted mb-0">Attendance Date</h5>
							<asp:TextBox ID="tbAttendanceDate" ToolTip="Enter Status Date" runat="server" autocompletee="off" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>
							<cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbAttendanceDate" ValidChars="/" />
						</div>
						<div class="col-md-2 pl-0 pt-3 mt-1">
							<asp:LinkButton ID="lbtnSearch" OnClick="lbtnSearch_Click" data-toggle="tooltip" data-placement="bottom" title="Search Employee" runat="server" Width="30px" CssClass="btn btn bg-gradient-green btn-sm text-white">
                                    <i class="fa fa-search"></i>
							</asp:LinkButton>
							<asp:LinkButton ID="lbtnResetList" OnClick="lbtnResetList_Click" runat="server" data-toggle="tooltip" data-placement="bottom" title="Reset Filter" class="btn btn bg-gradient-orange btn-sm text-white"> <i class="fa fa-undo"></i></asp:LinkButton>
							<asp:LinkButton ID="lbtnDownload" OnClick="lbtnDownload_Click" runat="server" data-toggle="tooltip" data-placement="bottom" title="Download List" class="btn btn bg-gradient-danger btn-sm text-white"> <i class="fa fa-download"></i></asp:LinkButton>
						</div>
						<div class="col-md-1 pl-0 pt-3 mt-2 text-right">
							<asp:LinkButton ID="lbtnview" data-toggle="tooltip" data-placement="bottom" title="View Instructions" runat="server" Width="30px" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
							</asp:LinkButton>
							<asp:LinkButton runat="server" class="btn btn bg-gradient-green btn-sm text-white" Width="30px" data-toggle="tooltip" data-placement="bottom" title="Download Instruction">
                                            <i class="fa fa-download"></i>
							</asp:LinkButton>
						</div>
					</div>
				</div>
			</div>
		</div>
		<div class="row">
			<div class="col-lg-3" style="min-height: 500px">
				<div class="row ml-0 pr-2">
					<div class="col-md-12" style="">
						<div class="card shadow" style="min-height: 10vh; margin-bottom: 10px;">
							<asp:LinkButton ID="LinkButton1" runat="server" Style="color: red;" CommandName="P">
								<div class="card-body" style="padding: 10px; text-align: center">
									<div class="chart-container" style="min-height: 180px;">
										<asp:Literal ID="ltpieChartBookingMode" runat="server"></asp:Literal>
										<asp:Image ID="ImgpieChartBookingModeNOdata" runat="server" src="../citizenAuth/assets/img/no_data.png"
											Visible="false" class="mt-3 ml-2" Style="opacity: 0.6;" Width="80%" Height="180px" />
									</div>
								</div>
							</asp:LinkButton>
						</div>
					</div>
					<div class="col-md-6 pr-1">
						<div class="card shadow" style="min-height: 10vh; margin-bottom: 10px">
							<div class="card-body" style="padding: 10px; text-align: center; height: 85px">

								<div class="col-lg-12">
									<asp:LinkButton ID="lbtnTotalEmployee" OnClick="lbtnViewEmp_Click" runat="server" Text="0" Style="color: green;" Font-Underline="true" Font-Bold="true" CommandName="O"></asp:LinkButton>
									<br />
									<label for="ddhead" style="line-height: 18px;">
										Total
                                <br />
										<br />
									</label>
								</div>
							</div>
						</div>
					</div>
					<div class="col-md-6 pl-1">
						<div class="card shadow" style="min-height: 10vh; margin-bottom: 10px">
							<div class="card-body" style="padding: 10px; text-align: center; height: 85px">

								<div class="col-lg-12">
									<asp:LinkButton ID="lbtnNotMarked" OnClick="lbtnViewEmp_Click" runat="server" Text="0" Style="color: red;" Font-Underline="true" Font-Bold="true" CommandName="N"></asp:LinkButton>
									<br />
									<label for="ddhead" style="line-height: 18px;">
										Not Marked
                                <br />
										<br />
									</label>
								</div>
							</div>
						</div>
					</div>
					<div class="col-md-6 pr-1">
						<div class="card shadow" style="min-height: 10vh; margin-bottom: 10px">
							<div class="card-body" style="padding: 10px; text-align: center; height: 85px">

								<div class="col-lg-12">
									<asp:LinkButton ID="lbtnPresent" OnClick="lbtnViewEmp_Click" runat="server" Text="0" Style="color: green;" Font-Underline="true" Font-Bold="true" CommandName="P"></asp:LinkButton>
									<br />
									<label for="ddhead" style="line-height: 18px;">
										Present
                                <br />
										<br />
									</label>
								</div>
							</div>
						</div>
					</div>
					<div class="col-md-6 pl-1">
						<div class="card shadow" style="min-height: 10vh; margin-bottom: 10px">
							<div class="card-body" style="padding: 10px; text-align: center; height: 85px">

								<div class="col-lg-12">
									<asp:LinkButton ID="lbtnAbsent" OnClick="lbtnViewEmp_Click" runat="server" Text="0" Style="color: red;" Font-Underline="true" Font-Bold="true" CommandName="A"></asp:LinkButton>
									<br />
									<label for="ddhead" style="line-height: 18px;">
										Absent
                                <br />
										<br />
									</label>
								</div>
							</div>
						</div>
					</div>
					<div class="col-md-6 pr-1">
						<div class="card shadow" style="min-height: 10vh; margin-bottom: 10px">
							<div class="card-body" style="padding: 10px; text-align: center; height: 85px">

								<div class="col-lg-12">
									<asp:LinkButton ID="lbtnOnDuty" OnClick="lbtnViewEmp_Click" runat="server" Text="0" Style="color: green;" Font-Underline="true" Font-Bold="true" CommandName="D"></asp:LinkButton>
									<br />
									<label for="ddhead" style="line-height: 18px;">
										on Duty
                                <br />
										<br />
									</label>
								</div>
							</div>
						</div>
					</div>
					<div class="col-md-6 pl-1">
						<div class="card shadow" style="min-height: 10vh; margin-bottom: 10px">
							<div class="card-body" style="padding: 10px; text-align: center; height: 85px">

								<div class="col-lg-12">
									<asp:LinkButton ID="lbtnOnLeave" OnClick="lbtnViewEmp_Click" runat="server" Text="0" Style="color: red;" Font-Underline="true" Font-Bold="true" CommandName="L"></asp:LinkButton>
									<br />
									<label for="ddhead" style="line-height: 18px;">
										On Leave
                                <br />
										<br />
									</label>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div class="col-lg-9" style="min-height: 500px">
				<div class="card" style="min-height: 500px;">
					<div class="card-body px-0 pt-0">
						<div class="custom-tab">
							<div class="tab-content pl-3 pr-3 pt-2">
								<asp:Panel runat="server" ID="pnlViewETM" Visible="true">
									<div>
										<div class="row">
											<div class="col-lg-12 pb-2">
												<div class="col-md-12 text-right card-title text-muted mb-0">
													<h5>
														<asp:Label Text="text" runat="server" CssClass="btn btn-sm btn-success btn-label">A</asp:Label>
														Mark Attendance
                    <asp:Label Text="text" runat="server" CssClass="btn btn-sm btn-danger btn-label ml-2">L</asp:Label>
														Mark Leave
                    <asp:Label Text="text" runat="server" CssClass="btn btn-sm btn-warning btn-label ml-2">E</asp:Label>
														Extend Leave
                    <asp:Label Text="text" runat="server" CssClass="btn btn-sm btn-danger btn-label ml-2">C</asp:Label>
														Cancel Leave
                    <asp:Label Text="text" runat="server" CssClass="btn btn-sm btn-info btn-label ml-2">D</asp:Label>
														View Monthly Calender</h5>
												</div>
											</div>
										</div>
										<div class="row text-left">
											<div class="col-md-12 mt-3">
												<asp:Panel ID="pnlNoRecord" runat="server" Width="100%" Visible="true">
													<div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
														<div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
															Employee's not available<br />
														</div>
													</div>
												</asp:Panel>
												<asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="false" GridLines="None" OnRowCommand="gvEmployees_RowCommand" OnRowDataBound="gvEmployees_RowDataBound" OnPageIndexChanging="gvEmployees_PageIndexChanging"
													AllowPaging="true" PageSize="20" CssClass="table table-hover table-striped" DataKeyNames="empcode, empname, mobileno, designationcode, designation, status, dutystatus,
attendancerefno, statusdate, leaverefno, waybillrefno, attendancestatus, leavestartdate,leaveenddate, leavereason, attached_doc, leavetype, leavetypeid">
													<Columns>
														<asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="class-on-element">
															<ItemTemplate>
																<asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Employee" ItemStyle-CssClass="class-on-element">
															<ItemTemplate>
																<asp:Label ID="lblEMPCODE" runat="server" Text='<%# Eval("empname") %>'></asp:Label>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Designation" ItemStyle-CssClass="class-on-element">
															<ItemTemplate>
																<asp:Label ID="lblEMPDESIGNATION" runat="server" Text='<%# Eval("designation") %>'></asp:Label>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField HeaderText="Status" ItemStyle-CssClass="class-on-element">
															<ItemTemplate>
																<asp:Label runat="server" ID="tbAttendenceStatus" Text='<%# Eval("attendancestatus") %>'></asp:Label> -
                                                                <asp:Label runat="server" ID="Label1" Font-Bold="true" Text='<%# Eval("duty_status") %>'></asp:Label>
															</ItemTemplate>
														</asp:TemplateField>
														<asp:TemplateField ItemStyle-CssClass="class-on-element">
															<ItemTemplate>
																<asp:LinkButton ID="lbtnAttendance" CommandName="markAttendence" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
																	Visible="true" runat="server" CssClass="btn btn-sm btn-success" Style="border-radius: 4px;"
																	ToolTip="Mark Attendence">A</asp:LinkButton>
																<asp:LinkButton ID="lbtnAddLeave" CommandName="markLeave" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
																	Visible="true" runat="server" CssClass="btn btn-sm btn-danger" Style="border-radius: 4px;"
																	ToolTip="Mark Leave">L</asp:LinkButton>
																<asp:LinkButton ID="lbtnUpdateLeave" CommandName="updateLeave" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
																	Visible="false" runat="server" CssClass="btn btn-sm btn-warning" Style="border-radius: 4px;"
																	ToolTip="Extend Leave">E</asp:LinkButton>
																<asp:LinkButton ID="lbtnCancelLeave" CommandName="cancelLeave" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
																	Visible="false" runat="server" CssClass="btn btn-sm btn-danger" Style="border-radius: 4px;"
																	ToolTip="Cancel Leave">C</asp:LinkButton>
																<asp:LinkButton ID="LinkButton5" CommandName="viewDashboard" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
																	Visible="true" runat="server" CssClass="btn btn-sm btn-info" Style="border-radius: 4px;"
																	ToolTip="View Dashboard">D</asp:LinkButton>
															</ItemTemplate>
														</asp:TemplateField>
													</Columns>
													<PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
												</asp:GridView>
											</div>
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
	<div class="row">
		<cc1:ModalPopupExtender ID="mpMarkAttendance" runat="server" PopupControlID="pnlAttendance"
			CancelControlID="Button8" TargetControlID="Button9" BackgroundCssClass="modalBackground">
		</cc1:ModalPopupExtender>
		<asp:Panel ID="pnlAttendance" runat="server" Style="position: fixed;">
			<div class="card" style="width: 480px;">
				<div class="card-body text-left pt-2 row" style="min-height: 100px;">
					<div class="col-lg-12" style="font-size: 12pt;">
						<div class="col-lg-12 p-4">
							<center>
                                <h4 style="font-weight: 500; font-size: 14pt; color: #474343;">Mark attendance of
                                    <asp:Label ID="lblAttendance" runat="server" Text=""></asp:Label>?
                                </h4>
                                <br />
                                <asp:DropDownList runat="server" ID="ddlAttendanceType" CssClass="form-control" Width="200px" Style="padding: 0px 5px; display: inline;">
                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                    <asp:ListItem Value="P" Text="Present"></asp:ListItem>
                                    <asp:ListItem Value="A" Text="Absent"></asp:ListItem>
                                </asp:DropDownList>
                                <div style="width: 100%; margin-top: 20px;">
                                    <asp:LinkButton ID="lbtnSaveAttendance" runat="server" OnClick="lbtnSaveAttendance_Click" CssClass="btn btn-success"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-danger ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                                </div>
                            </center>
						</div>
					</div>
				</div>
			</div>
			<div style="visibility: hidden;">
				<asp:Button ID="Button8" runat="server" Text="" />
				<asp:Button ID="Button9" runat="server" Text="" />
			</div>
		</asp:Panel>
	</div>
	<div class="row">
		<cc1:ModalPopupExtender ID="mpMarkLeave" runat="server" PopupControlID="pnlMarkLeave"
			CancelControlID="Button5" TargetControlID="Button7" BackgroundCssClass="modalBackground">
		</cc1:ModalPopupExtender>
		<asp:Panel ID="pnlMarkLeave" runat="server" Style="position: fixed; display: none">
			<center>
                <div class="card" style="">
                    <div class="card-header">
						<h4 style="font-weight: 500; font-size: 14pt; color: #474343;">Mark Leave of
                                    <asp:Label ID="lblEmp" runat="server" Text=""></asp:Label>?
                                </h4>
                    </div>
                    <div class="card-body text-left pt-2 row" style="min-height: 100px;">
                        <div class="col-lg-12" style="font-size: 11pt;">
                            <div class="col-lg-12 mb-3">
                                <table class="table table-borderless mb-2 ">
                                    <tr>
                                        <td>Leave type</td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlLeaveType" CssClass="form-control form-control-sm"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>Leave Start Date<span style="color: red">*</span></td>
                                        <td style="vertical-align: baseline;">
											<asp:TextBox ID="tbLeaveStartDate" ToolTip="Leave Start Date" runat="server" autocompletee="off" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>
							<cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbLeaveStartDate" ValidChars="/" />
											</td>
                                    </tr>
                                    <tr>
                                        <td>Leave End Date<span style="color: red">*</span></td>
                                        <td style="vertical-align: baseline;">
											<asp:TextBox ID="tbLeaveEndDate" ToolTip="Leave End Date" runat="server" autocompletee="off" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>
							<cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbLeaveEndDate" ValidChars="/" />
											</td>
                                    </tr>
                                    <tr>
                                        <td>Leave Reason</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="tbRemark" CssClass="form-control" TextMode="MultiLine" Style="resize: none" MaxLength="100" AutoComplete="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Upload Document (If Any)<br />
                                            <span style="font-size: 7pt; color: Red; line-height: 12px;">(Max. Size 2 MB and PDF only)</span></td>
                                        <td>
                                            <asp:Button ID="btnUploadImage" runat="server" CausesValidation="False" CssClass="button1" OnClick="btnUploadImage_Click"
                                                Style="display: none" TabIndex="18" Text="Upload Image" Width="80px" />
                                            <asp:FileUpload ID="docfile" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-success btn-sm"
                                                onchange="UploadImage(this);" Width="200px" TabIndex="9" />
											<br />
                                            <asp:LinkButton runat="server" ID="lbtnPdf" OnClick="lbtnPdf_Click" CssClass="col-form-label control-label " Style="font-size: 12px; color: red; font-weight: normal; text-decoration: underline;"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                                <center>
                                    <div style="width: 100%; margin-top: 20px;">
                                        <asp:LinkButton ID="lbtnMarkLeave" OnClick="lbtnMarkLeave_Click" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-floppy-o"></i> Save </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-refresh"></i> Cancel </asp:LinkButton>
                                    </div>
                                </center>
                            </div>
                        </div>
                    </div>

                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button5" runat="server" Text="" />
                    <asp:Button ID="Button7" runat="server" Text="" />
                </div>
            </center>
		</asp:Panel>
	</div>
	<div class="row">
		<cc1:ModalPopupExtender ID="mpCancelLeave" runat="server" PopupControlID="pnlCancelLeave"
			CancelControlID="Button11" TargetControlID="Button12" BackgroundCssClass="modalBackground">
		</cc1:ModalPopupExtender>
		<asp:Panel ID="pnlCancelLeave" runat="server" Style="position: fixed; display: none">
			<center>
                <div class="card" style="width: 480px;">
                    <div class="card-header">
						<h4 style="font-weight: 500; font-size: 14pt; color: #474343;">Cancel Leave of
                                    <asp:Label ID="lblLeaveEmp" runat="server" Text=""></asp:Label>?
                                </h4>
                    </div>
                    <div class="card-body text-left pt-2 row" style="min-height: 100px;">
                        <div class="col-lg-12" style="font-size: 11pt;">
                            <div class="col-lg-12 mb-3">
                                <table class="table table-borderless mb-2 ">
                                    <tr>
                                        <td>Leave type</td>
                                        <td>
                                            <asp:Label runat="server" ID="lblLeaveType" Text="N/A"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Leave Period</td>
                                        <td>
                                            <asp:Label runat="server" ID="lblLeavePeriod" Text="N/A"></asp:Label></td>
                                    </tr>
                                    <%-- <tr>
                                        <td>Balance Leave Pendings</td>
                                        <td>
                                            <asp:Label runat="server" ID="lblPendingLeave" Text="N/A"></asp:Label></td>
                                    </tr>--%>
                                    <tr>
                                        <td>Reason of Cancellation<span class="text-danger">*</span></td>
                                        <td>
                                            <asp:TextBox runat="server" ID="tbCancellationReason" CssClass="form-control" TextMode="MultiLine" Style="resize: none" MaxLength="100" AutoComplete="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Upload Document (If Any)<br />
                                            <span style="font-size: 7pt; color: Red; line-height: 12px;">(Max. Size 2 MB and PDF only)</span></td>
                                        <td>
                                            <asp:Button ID="btnUploadImage2" runat="server" CausesValidation="False" CssClass="button1" OnClick="btnUploadImage2_Click"
                                                Style="display: none" TabIndex="18" Text="Upload Image" Width="80px" />
                                            <asp:FileUpload ID="fileDoc2" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-success btn-sm"
                                                onchange="UploadImage2(this);" Width="200px" TabIndex="9" />
											<br />
                                            <asp:LinkButton runat="server" ID="lbtnpdf2" CssClass="col-form-label control-label " Style="font-size: 12px; color: red; font-weight: normal; text-decoration: underline;"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                                <center>
                                    <div style="width: 100%; margin-top: 20px;">
                                        <asp:LinkButton ID="lbtnCancelLeave" runat="server" OnClick="lbtnCancelLeave_Click" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> Save </asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton6" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> Cancel </asp:LinkButton>
                                    </div>
                                </center>
                            </div>
                        </div>
                    </div>

                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button11" runat="server" Text="" />
                    <asp:Button ID="Button12" runat="server" Text="" />
                </div>
            </center>
		</asp:Panel>
	</div>
</asp:Content>

