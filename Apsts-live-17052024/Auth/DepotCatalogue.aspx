<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Depotmaster.master" AutoEventWireup="true" CodeFile="DepotCatalogue.aspx.cs" Inherits="Auth_DepotCatalogue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<div class="container-fluid pt-top ">
		<div class="row mt-4">
			<div class="col-md-4  stretch-card transparent">
				<div class="card card-dark-blue">
					<div class="card-body">
						<div class="card-heading">Filling Station</div>
						<div class="total-tx mb-4">Configure Filling Statio</div>
						<div class="col text-right">
							<a href="#!" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><%--<a href="DepAdminFillingStationMgmt.aspx" class="btn btn-sm btn-primary">Explore</a>--%>
						</div>
					</div>
				</div>
			</div>
			<div class="col-md-4  stretch-card transparent">
				<div class="card card-dark-blue">
					<div class="card-body">
						<div class="card-heading">Tank Management</div>
						<div class="total-tx mb-4">Configure Tank Management</div>
						<div class="col text-right">
							<a href="#!" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><%--<a href="DepAdminTankMgmt.aspx" class="btn btn-sm btn-primary">Explore</a>--%>
						</div>
					</div>
				</div>
			</div>
			<div class="col-md-4 stretch-card transparent">
				<div class="card card-dark-blue">
					<div class="card-body">
						<div class="card-heading">Pump Management</div>
						<div class="total-tx mb-4">
							Configure Pump Management

						</div>
						<div class="col text-right">
							<a href="#!" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a>
                            <%--<a href="DepAdminPumpMgmt.aspx" class="btn btn-sm btn-primary">Explore</a>--%>
						</div>

					</div>
				</div>
			</div>
			<div class="col-md-4  stretch-card transparent">
				<div class="card card-dark-blue">
					<div class="card-body">
						<div class="card-heading">ETM Management</div>
						<div class="total-tx mb-4">Issue ETM to other office</div>
						<div class="col text-right">
							<a href="#!" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop ">

							                                            </i></a>
<%--                            <a href="AdmETMManagement.aspx" class="btn btn-sm btn-primary">Explore</a>--%>
						</div>

					</div>
				</div>
			</div>
			<div class="col-md-4  stretch-card transparent">
				<div class="card card-dark-blue">
					<div class="card-body">
						<div class="card-heading">Service-Bus Mapping</div>
						<div class="total-tx mb-4">Map Service with bus</div>
						<div class="col text-right">
							<a href="#!" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a>
                            <%--<a href="DepBusServiceMapping.aspx" class="btn btn-sm btn-primary">Explore</a>--%>
						</div>

					</div>
				</div>
			</div>
			<div class="col-md-4  stretch-card transparent">
				<div class="card card-dark-blue">
					<div class="card-body">
						<div class="card-heading">Bus-Crew Mapping</div>
						<div class="total-tx mb-4">Map Crew with bus</div>
						<div class="col text-right">
							<a href="#!" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><%--<a href="DepBusCrewMapping.aspx" class="btn btn-sm btn-primary">Explore</a>--%>
						</div>

					</div>
				</div>
			</div>
            <div class="col-md-4  stretch-card transparent">
				<div class="card card-dark-blue">
					<div class="card-body">
						<div class="card-heading"> Service Cancel/Block</div>
						<div class="total-tx mb-4"> Cancel/Block of online services</div>
						<div class="col text-right">
							<a href="#!" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="DepoServiceCancel.aspx" class="btn btn-sm btn-primary">Explore</a>
						</div>

					</div>
				</div>
			</div>
             <div class="col-md-4  stretch-card transparent">
				<div class="card card-dark-blue">
					<div class="card-body">
						<div class="card-heading"> Trip Chart</div>
						<div class="total-tx mb-4"> Generate Trip Chart</div>
						<div class="col text-right">
							<a href="#!" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="tripchartdash.aspx" class="btn btn-sm btn-primary">Explore</a>
						</div>

					</div>
				</div>
			</div>
		</div>
	</div>
</asp:Content>

