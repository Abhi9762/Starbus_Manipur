<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/TimeKeeperMaster.master" AutoEventWireup="true" CodeFile="TimeKeeperCatalogue.aspx.cs" Inherits="TimeKeeperCatalogue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<div class="container-fluid pt-top pb-5">
		<div class="row mt-4">
			<div class="col-md-4  stretch-card transparent">
				<div class="card card-dark-blue">
					<div class="card-body">
						<div class="card-heading">Attendance Management</div>
						<div class="total-tx mb-4">Mark Attendance of Driver and Conductor</div>
						<div class="col text-right">
							<a href="../Auth/UserManuals/Time Keeper/Help Document for TimeKeeper Attendance Management.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="tkAttendanceManagement.aspx" class="btn btn-sm btn-primary">Explore</a>
						</div>
					</div>
				</div>
			</div>
			<div class="col-md-4  stretch-card transparent">
				<div class="card card-dark-blue">
					<div class="card-body">
						<div class="card-heading">Duty Allotment</div>
						<div class="total-tx mb-4">Provisional duty allotment of crew</div>
						<div class="col text-right">
							<a href="../Auth/UserManuals/Time Keeper/Help Document for TimeKeeper Duty Allotment.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="tkDutyAllocation.aspx" class="btn btn-sm btn-primary">Explore</a>
						</div>
					</div>
				</div>
			</div>
			<div class="col-md-4  stretch-card transparent">
				<div class="card card-dark-blue">
					<div class="card-body">
						<div class="card-heading">Duty Slip</div>
						<div class="total-tx mb-4">Generate duty slip</div>
						<div class="col text-right">
							<a href="../Auth/UserManuals/Time Keeper/Help Document for TimeKeeper Duty Slip.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="tkDutySlip.aspx" class="btn btn-sm btn-primary">Explore</a>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</asp:Content>

