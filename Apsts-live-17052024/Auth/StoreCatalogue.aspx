<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/StoreMaster.master" AutoEventWireup="true" CodeFile="StoreCatalogue.aspx.cs" Inherits="StoreCatalogue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<div class="container-fluid pt-top ">
		<div class="row mt-4">
			<div class="col-md-4  stretch-card transparent">
				<div class="card card-dark-blue">
					<div class="card-body">
						<div class="card-heading">ETM Management</div>
						<div class="total-tx mb-4">Issue ETM to ETM Branch</div>
						<div class="col text-right">
							<a href="../Auth/UserManuals/Store Management/Help Document for Store Management.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="SysAdmETMRegistration.aspx" class="btn btn-sm btn-primary">Explore</a>
						</div>
					</div>
				</div>
			</div>
			<%--<div class="col-md-4  stretch-card transparent">
				<div class="card card-dark-blue">
					<div class="card-body">
						<div class="card-heading">Bus Management</div>
						<div class="total-tx mb-4">View and Update Bus PUC Details.</div>
						<div class="col text-right">
							<a href="#!" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="BusManagement.aspx" class="btn btn-sm btn-primary">Explore</a>
						</div>
					</div>
				</div>
			</div>
			<div class="col-md-4  stretch-card transparent">
				<div class="card card-dark-blue">
					<div class="card-body">
						<div class="card-heading">Invoice Dashboard</div>
						<div class="total-tx mb-4">Issue ETM to ETM Branch</div>
						<div class="col text-right">
							<a href="#!" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="AdmETMManagement.aspx" class="btn btn-sm btn-primary">Explore</a>
						</div>
					</div>
				</div>
			</div>--%>
		</div>
		<%--<div class="row mt-4">
			<div class="col-md-4  stretch-card transparent">
				<div class="card card-dark-blue">
					<div class="card-body">
						<div class="card-heading">Invoice Entry</div>
						<div class="total-tx mb-4">Issue ETM to ETM Branch</div>
						<div class="col text-right">
							<a href="#!" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="Invoice.aspx" class="btn btn-sm btn-primary">Explore</a>
						</div>
					</div>
				</div>
			</div>
			<div class="col-md-4  stretch-card transparent">
				<div class="card card-dark-blue">
					<div class="card-body">
						<div class="card-heading">Indent Request</div>
						<div class="total-tx mb-4">Issue ETM to ETM Branch</div>
						<div class="col text-right">
							<a href="#!" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="ItemIndent.aspx" class="btn btn-sm btn-primary">Explore</a>
						</div>
					</div>
				</div>
			</div>
			<div class="col-md-4  stretch-card transparent">
				<div class="card card-dark-blue">
					<div class="card-body">
						<div class="card-heading">Item Issuance</div>
						<div class="total-tx mb-4">Issue ETM to ETM Branch</div>
						<div class="col text-right">
							<a href="#!" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="ItemIssuance.aspx" class="btn btn-sm btn-primary">Explore</a>
						</div>
					</div>
				</div>
			</div>
		</div>--%>
	</div>
</asp:Content>

