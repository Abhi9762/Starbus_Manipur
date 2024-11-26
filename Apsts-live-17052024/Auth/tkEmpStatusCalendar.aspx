<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/TimeKeeperMaster.master" AutoEventWireup="true" CodeFile="tkEmpStatusCalendar.aspx.cs" Inherits="Auth_tkEmpStatusCalendar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
	<%--<script>
		$(document).ready(function () {
			empcode = $(this).val();
			monthname = $("#ContentPlaceHolder1_txtFromDate").val();
			year = $("#ContentPlaceHolder1_txtToDate").val();
			loadEmpSummary(empcode, monthname, year);
		});
		$(window).on('load', function () {
			empcode = $('[id*=hdnempcode]').val();
			monthname = $('[id*=ddlMonthNames]').val();
			year = $('[id*=ddlYear]').val();


			loadEmpSummary(empcode, monthname, year);

			var ctx = document.getElementById('chart-area').getContext('2d');
			window.myPie = new Chart(ctx, config);
		});
		var chartLabel = [];
		var chartData = [];
		function loadEmpSummary(empcode, monthname, year) {
			$.ajax({
				type: "POST",
				url: "tkEmpStatusCalendar.aspx/Getsummary",
				data: "{'empcode':'" + empcode + "', 'monthname':'" + monthname + "','year':'" + year + "'}",
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function OnSuccess(r) {
					var data = JSON.parse(r.d);// alert(data);
					console.log(data);
					$(data).each(function (index, item) {
						//alert(item.status_date+"-"+item.duty_status);
						chartLabel.push(item.status_date);
						chartData.push(item.duty_status);
					});
				},
				failure: function (r) {
					alert("No Data Found");
				},
				error: function (response) {
					alert("No Data Found");
				}
			});
		};
		var config = {
			type: 'pie',
			data: {
				datasets: [{
					data: chartData,
					backgroundColor: [
						"#3e95cd", "#8e5ea2", "#3cba9f"
					],
					label: 'Labels'
				}],
				labels: chartLabel
			},
			options: {
				responsive: true
			}
		};
	</script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<div class="container-fluid pt-2 pb-5">
		<asp:HiddenField runat="server" ID="hdnempcode" />
		<div class="row align-items-center">
			<div class="col-xl-12">
				<div class="card card-body card-stats mb-3">
					<div class="row">
						<div class="col-lg-7">
							<h3 class="card-title  mb-0">Attendance Register</h3>
							<h4 class="card-title text-muted mb-0">
								<asp:Label ID="lblEmpName" runat="server" Text=""></asp:Label>
								<asp:Label ID="lblEmpDesignation" runat="server" Text="Driver"></asp:Label>
							</h4>
						</div>
						<div class="col-lg-5">
							<div class="row">
								<div class="col-md-4">
									<h5 class="card-title text-muted mb-0">Month</h5>
									<asp:DropDownList ID="ddlMonthNames" runat="server" CssClass="form-control form-control-sm">
									</asp:DropDownList>
								</div>
								<div class="col-md-4">
									<h5 class="card-title text-muted mb-0">Year</h5>
									<asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control form-control-sm">
									</asp:DropDownList>
								</div>
								<div class="col-md-4 mt-3 pt-1">
									<asp:LinkButton ID="lbtnSearch" OnClick="lbtnSearch_Click" data-toggle="tooltip" data-placement="bottom" title="Search" runat="server" Width="30px" CssClass="btn btn bg-gradient-green btn-sm text-white">
                                    <i class="fa fa-search"></i>
									</asp:LinkButton>
									<asp:LinkButton ID="lbtnDownload" runat="server" data-toggle="tooltip" data-placement="bottom" title="Download List" class="btn btn bg-gradient-danger btn-sm text-white"> <i class="fa fa-download"></i></asp:LinkButton>
									<asp:LinkButton ID="lbtnBack" OnClick="lbtnBack_Click" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back to Attendance Management" class="btn btn bg-gradient-primary btn-sm text-white"> <i class="fa fa-arrow-left"></i> Back</asp:LinkButton>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
		<div class="row">
			<div class="col-lg-4">
				<div class="card mb-2" style="min-height: 150px;">
					<div class="card-body p-0 text-center">
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
					</div>
				</div>
				<div class="row m-0">
					<div class="col-lg-6 pl-0">
						<div class="card mb-2" style="min-height: 50px;">
							<div class="card-body p-1 text-center">
								<asp:Label ID="lblTotalCount" runat="server" Text="0" Style="font-size: 20px; font-weight: bold; color: Green;"></asp:Label>
								<p class="m-0">
									Total
								</p>
							</div>
						</div>
					</div>
					<div class="col-lg-6 p-0">
						<div class="card mb-2" style="min-height: 50px;">
							<div class="card-body p-1 text-center">
								<asp:Label ID="lblNotMarkedCount" runat="server" Text="0" Style="font-size: 20px; font-weight: bold; color: Red;"></asp:Label>
								<p class="m-0">
									Not Marked
								</p>
							</div>
						</div>
					</div>
				</div>
				<div class="row m-0">
					<div class="col-lg-6 pl-0">
						<div class="card mb-2" style="min-height: 50px;">
							<div class="card-body p-1 text-center">
								<asp:Label ID="lblPresentCount" runat="server" Text="0" Style="font-size: 20px; font-weight: bold; color: Green;"></asp:Label>
								<p class="m-0">
									Present
								</p>
							</div>
						</div>
					</div>
					<div class="col-lg-6 p-0">
						<div class="card mb-2" style="min-height: 50px;">
							<div class="card-body p-1 text-center">
								<asp:Label ID="lblAbsentCount" runat="server" Text="0" Style="font-size: 20px; font-weight: bold; color: Red;"></asp:Label>
								<p class="m-0">
									Absent
								</p>
							</div>
						</div>
					</div>

				</div>
				<div class="row m-0">
					<div class="col-lg-6 pl-0">
						<div class="card mb-2" style="min-height: 50px;">
							<div class="card-body p-1 text-center">
								<asp:Label ID="lblDutyCount" runat="server" Text="0" Style="font-size: 20px; font-weight: bold; color: Green;"></asp:Label>
								<p class="m-0">
									On Duty
								</p>
							</div>
						</div>
					</div>
					<div class="col-lg-6 p-0">
						<div class="card mb-2" style="min-height: 50px;">
							<div class="card-body p-1 text-center">
								<asp:Label ID="lblLeaveCount" runat="server" Text="0" Style="font-size: 20px; font-weight: bold; color: Red;"></asp:Label>
								<p class="m-0">
									On Leave
								</p>
							</div>
						</div>
					</div>

				</div>

			</div>
			<div class="col-lg-8">
				<div class="card">
					<div class="card-body p-1">
						<asp:Calendar ID="Calendar1" runat="server" Enabled="false" DayNameFormat="Full" Width="100%" ShowGridLines="True" OnDayRender="Calendar1_DayRender"
							Style="min-height: 380px;">
							<NextPrevStyle Font-Bold="true" Font-Size="23px" ForeColor="#FFFFFF" />
							<DayHeaderStyle BackColor="#ffffff" Font-Bold="True" HorizontalAlign="Center"
								VerticalAlign="Middle" />
							<TitleStyle Font-Bold="True" Font-Size="20pt" ForeColor="#25426a" BackColor="White" />
						</asp:Calendar>
					</div>
				</div>
			</div>
		</div>
	</div>
</asp:Content>

