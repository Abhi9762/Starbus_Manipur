<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/ProjectAdmmaster.master" AutoEventWireup="true" CodeFile="ProjectAdmCatalogue.aspx.cs" Inherits="Auth_ProjectAdmCatalogue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid pt-top ">
        <div class="row mt-4">
            <div class="col-md-6  stretch-card transparent">
                <div class="card">
                    <div class="card-header pb-0 pt-2">
                        <div class="row">
                            <div class="col-md-6 col-lg-6">
                                <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h2 class="text-warning">Please Note</h2></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:Label runat="server" Font-Size="15px">
                                    <h4>In General Configuration</h4>
                                            1. Before configure the project, You have to upload department Logo, government logo, favicon icon and enter the department name and application name.<br/>
                                 <%--  2.  <br/>--%>
                                 <%--    3. If you deactivate the module then related module pages not available for operations.<br/>--%>
                                   <%-- 4. <br/>
                                    5. <br/>--%>
                                            <h4 class="mt-3">In Module Configuration</h4>
                                            1. Start & stop buspass module.<br/>
                                     2. Start & stop agent module.<br/>
                                     3. Start & stop csc module.<br/>
                                
                                </asp:Label>
                            </div>

                        </div>
                    </div>
                </div>
                <%-- <div class="card card-dark-blue">
                    <div class="card-body">
                        <div class="card-heading">General Information</div>
                        <div class="total-tx mb-4">Configure General Info like Department Logo, Government Logo, Department Name and Application Title</div>
                        <div class="col text-right">
                            <a href="#!" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="ProjectAdmGenInfo.aspx" class="btn btn-sm btn-primary">Explore</a>
                        </div>
                    </div>
                </div>--%>
            </div>

            <div class="col-md-6 ">
                <div class="card">
                    <div class="card-header pb-0 pt-2 ">
                        <div class="row">
                            <div class="col-md-6 col-lg-6">
                                <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h2 class="text-primary">Catalogue</h2></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12  stretch-card transparent">
                                <div class="card card-dark-blue">
                                    <div class="card-body">
                                        <div class="card-heading">General Information</div>
                                        <div class="total-tx mb-4">Configure General Info like Department Logo, Government Logo, Department Name and Application Title</div>
                                        <div class="col text-right">
                                            <a href="../Auth/UserManuals/Project_adm/Um_Project_adm_general information.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="ProjectAdmGenInfo.aspx" class="btn btn-sm btn-primary">Explore</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12  stretch-card transparent">
                                <div class="card card-dark-blue">
                                    <div class="card-body">
                                        <div class="card-heading">Module Configuration </div>
                                        <div class="total-tx mb-4">Start & Stop Modules</div>
                                        <div class="col text-right">
                                            <a href="../Auth/UserManuals/Project_adm/Um_Project_adm_module config.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="configurationModule.aspx" class="btn btn-sm btn-primary">Explore</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
</asp:Content>

