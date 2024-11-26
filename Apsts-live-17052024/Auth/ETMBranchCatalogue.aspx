<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/ETMBranchMaster.master" AutoEventWireup="true" CodeFile="ETMBranchCatalogue.aspx.cs" Inherits="ETMBranchCatalogue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<div class="container-fluid pt-top ">
		<div class="row mt-4">
			<div class="col-md-4 stretch-card transparent">
				<div class="card card-dark-blue">
					<div class="card-body">
						<div class="card-heading">ETM Management</div>
						<div class="total-tx mb-4">Issue ETM to other office</div>
						<div class="col text-right">
							<a href="../Auth/UserManuals/ETM Branch/Help Document for ETM_Management.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop"></i></a>
							<a href="etmBranchEtmManagement.aspx" class="btn btn-sm btn-primary">Explore</a>
						</div>
					</div>
				</div>
			</div>
			<div class="col-md-4 stretch-card transparent">
				<div class="card card-dark-blue">
					<div class="card-body">
						<div class="card-heading">Waybill Management</div>
						<div class="total-tx mb-4">Issue ETM & Generate Waybill</div>
						<div class="col text-right">
							<a href="../Auth/UserManuals/ETM Branch/Help Document for Waybill Management.pdf" target="_blank" class="btn btn-sm btn-success mr-1"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="ETMBranchWayBillGenerate.aspx" class="btn btn-sm btn-primary">Explore</a>
						</div>
					</div>
				</div>
			</div>
			<div class="col-md-4 stretch-card transparent">
				<div class="card card-dark-blue">
					<div class="card-body">
						<div class="card-heading">ETM Submission</div>
						<div class="total-tx mb-4">Submit ETM</div>
						<div class="col text-right">
							<a href="../Auth/UserManuals/ETM Branch/Help Document for ETM_Submission.pdf" target="_blank" class="btn btn-sm btn-success mr-1"><i class="ni ni-cloud-download-95 mttop "></i>
</a><a href="etmSubmission.aspx" class="btn btn-sm btn-primary">Explore</a>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</asp:Content>

