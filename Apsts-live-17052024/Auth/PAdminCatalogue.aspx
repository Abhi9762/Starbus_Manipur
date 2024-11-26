<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Auth/PortalAdmmaster.master" AutoEventWireup="true" CodeFile="PAdminCatalogue.aspx.cs" Inherits="Auth_PAdminCatalogue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="../style.css" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            var todayDate = new Date().getDate();
            var endD = new Date(new Date().setDate(todayDate));
            var currDate = new Date();

            $('[id*=tbDateAuditLog]').datepicker({
                // startDate: "dateToday",
                endDate: endD,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidtoken" runat="server" />
    <div class="container-fluid pt-top ">
        <div class="row">
            <div class="col-lg-6">
                <div class="row mt-4 mb-4">
                    <div class="col-md-6  stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">General Configuration</div>
                                <div class="total-tx mb-4">General Configure like Advance Booking days,max seat booking at a time etc...</div>
                                <div class="col text-right">
                                    <a href="../Auth/UserManuals/Portal_adm/Help Document for General Configuration.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="PAdminGeneralConfig.aspx" class="btn btn-sm btn-primary">Explore</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Contact</div>
                                <div class="total-tx mb-4">
                                    Configure Contact detail for Helpdesk/Support like Email-ID, Contact Numbers and Toll Free Number

                                </div>
                                <div class="col text-right">
                                    <a href="../Auth/UserManuals/Portal_adm/Help Document for portal admin_contact.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="PAdminContDetails.aspx" class="btn btn-sm btn-primary">Explore</a>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="col-md-6  stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">RTI Manuals</div>
                                <div class="total-tx mb-4">Configure RTI Info like RTI Document Manuals and PIOS / APIOS Application Title</div>
                                <div class="col text-right">
                                    <a href="../Auth/UserManuals/Portal_adm/Help Document for portal admin_RTI Manuals.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="PAdminRTI.aspx" class="btn btn-sm btn-primary">Explore</a>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">SMS Configuration</div>
                                <div class="total-tx mb-4">SMS Configuration  </div>
                                <div class="col text-right">
                                    <a href="../Auth/UserManuals/Portal_adm/Help Document for portal admin_sms configuration.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="PAdminSMSConfig.aspx" class="btn btn-sm btn-primary">Explore</a>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="col-md-6  stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Portal Access Control</div>
                                <div class="total-tx mb-4">Rolewise access control</div>
                                <div class="col text-right">
                                    <a href="../Auth/UserManuals/Portal_adm/Help Document for portal admin_portal access control.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop"></i></a><a href="PAdminRoleWiseBlocking.aspx" class="btn btn-sm btn-primary">Explore</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6  stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Cancellation Policy</div>
                                <div class="total-tx mb-4">To define the dynamic cancellation policy for generating refund.</div>
                                <div class="col text-right">
                                    <a href="../Auth/UserManuals/Portal_adm/Help Document for portal admin_portal access control.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop"></i></a><a href="PAdminCancPolicy.aspx" class="btn btn-sm btn-primary">Explore</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6  stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Track My Bus Configuration</div>
                                <div class="total-tx mb-4">To define the Agency GPS URL.</div>
                                <div class="col text-right">
                                    <a href="../Auth/UserManuals/Portal_adm/Help Document for portal admin_track my bus.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop"></i></a><a href="PAdminGPSconfig.aspx" class="btn btn-sm btn-primary">Explore</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6  stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Offer and Discount</div>
                                <div class="total-tx mb-4">To define the Offer and discount for Traveller.</div>
                                <div class="col text-right">
                                    <a href="../Auth/UserManuals/Portal_adm/Help Document for portal admin_offer and discount.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop"></i></a><a href="PAdminDiscountandOffer.aspx" class="btn btn-sm btn-primary">Explore</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6  stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Home Page </div>
                                <div class="total-tx mb-4">Insert Home Page Background Scroll Images </div>
                                <div class="col text-right">
                                    <a href="../Auth/UserManuals/Portal_adm/Help Document for portal admin_homepage.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop"></i></a><a href="PAdminHomePage.aspx" class="btn btn-sm btn-primary">Explore</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6  stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Content Managment System</div>
                                <div class="total-tx mb-4">Terms & Condition | History etc.</div>
                                <div class="col text-right">
                                    <a href="../Auth/UserManuals/Portal_adm/Help Document for portal admin_content management system.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="PAdminTermsNConditions.aspx" class="btn btn-sm btn-primary">Explore</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6  stretch-card transparent" runat="server" id="divconcession">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Concession Policy</div>
                                <div class="total-tx mb-4">Concession Policy on ticket</div>
                                <div class="col text-right">
                                    <%--  <a href="../Auth/UserManuals/Portal_adm/.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="#" class="btn btn-sm btn-primary">Coming Soon</a> --%>
                                    <a href="../Auth/UserManuals/Portal_adm/Help Document for portal admin_concession policy.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="PAdminConcessionPolicy.aspx" class="btn btn-sm btn-primary">Explore</a>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6  stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">FAQ</div>
                                <div class="total-tx mb-4">Frequently Asked Questions</div>
                                <div class="col text-right">
                                    <a href="../Auth/UserManuals/Portal_adm/Help Document for portal admin_FAQs.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="PAdminFAQ.aspx" class="btn btn-sm btn-primary">Explore</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6  stretch-card transparent" runat="server" id="divbuspass">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Bus Pass</div>
                                <div class="total-tx mb-4">Bus Pass Category,Charges,Type</div>
                                <div class="col text-right">
                                    <a href="../Auth/UserManuals/Portal_adm/Help Document for portal admin_Bus Pass.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="PABusPassCharges.aspx" class="btn btn-sm btn-primary">Explore</a>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6  stretch-card transparent" runat="server" id="divagentconf">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Agent Configuration</div>
                                <div class="total-tx mb-4">Agent Configuration like Top Up limit etc...</div>
                                <div class="col text-right">
                                    <a href="../Auth/UserManuals/Portal_adm/Help Document for portal admin_Agent Configurations.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="PAdminPolicy.aspx" class="btn btn-sm btn-primary">Explore</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6  stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Notice News</div>
                                <div class="total-tx mb-4">Publishing News/Notice/Alert/Event</div>
                                <div class="col text-right">
                                    <a href="../Auth/UserManuals/Portal_adm/Help Document for portal admin_Notice News.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="PAdminAlertNoticePublishing.aspx" class="btn btn-sm btn-primary">Explore</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6  stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Photo Gallery</div>
                                <div class="total-tx mb-4">Add photo category and photo </div>
                                <div class="col text-right">
                                    <a href="../Auth/UserManuals/Portal_adm/Help Document for portal admin_Photo Gallery.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="PAdminPhotoGallery.aspx" class="btn btn-sm btn-primary">Explore</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6  stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Mentors</div>
                                <div class="total-tx mb-4">Add photo and information </div>
                                <div class="col text-right">
                                    <a href="../Auth/UserManuals/Portal_adm/Help Document for portal admin_Mentors.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="PAdminMentor.aspx" class="btn btn-sm btn-primary">Explore</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--- <div class="col-md-6  stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">HQ User Module Mapping</div>
                                <div class="total-tx mb-4">Map user module assign and remove module</div>
                                <div class="col text-right">
                                    <a href="../Auth/UserManuals/Portal_adm/.pdf" target="_blank" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="PUsermapping.aspx" class="btn btn-sm btn-primary">Explore</a>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                </div>
            </div>
         <div class="col-lg-6">
                    <div class="card mt-4 mb-4">
                        <div class="card-header">
                            <p class="mb-0" style="color: orange; font-weight: 600;">
                                Audit Trail
                            </p>
                        </div>
                        <div class="card-body">
                            <div class="row">

                                <div class="col-lg-4 input-group-prepend">
                                    <asp:Label runat="server" Text="Date" CssClass="mr-2"></asp:Label>
                                    <asp:TextBox ID="tbDateAuditLog" runat="server" MaxLength="10" CssClass="form-control" placeholder="DD/MM/YYYY" AutoComplete="off"></asp:TextBox>
                                </div>
                                <div class="col-lg-5 input-group-prepend">
                                    <asp:Label runat="server" Text="Type" CssClass="mr-2"></asp:Label>
                                    <asp:DropDownList ID="ddlaudittrailtype" runat="server" CssClass="form-control mr-2">
                                        <asp:ListItem Value="1">Login Log</asp:ListItem>
                                        <asp:ListItem Value="2">Action Log</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:LinkButton ID="lbtnSearchAuditTrail" runat="server" OnClick="lbtnSearchAuditTrail_Click" CssClass="btn btn-warning" ToolTip="Click here to Search Audit Trail"
                                        Style="padding: 5px 15px;">
                                             <i class="fa fa-search" style="font-size:19px;" title="Click here to Search Audit Trail"></i></asp:LinkButton>
                                </div>
                            <div class="col-lg-2">
                                <asp:LinkButton ID="lbtnDownloadAuditTrail" runat="server" Visible="false" OnClick="lbtnDownloadAuditTrail_Click" CssClass="btn btn-primary" ToolTip="Click here to download Audit Trail"
                                    Style="padding: 5px 15px;">
                                             <i class="fa fa-download" style="font-size:19px;" title="Click here to download Audit Trail"></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12 pt-1">
                                <asp:GridView ID="gv1_6_Web" runat="server" AutoGenerateColumns="False" GridLines="None" AllowPaging="true" PageSize="15"
                                    Visible="true" CssClass="table table-striped" DataKeyNames="" OnPageIndexChanging="gv1_6_Web_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.N.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="usercode" HeaderText="User " />
                                        <asp:BoundField DataField="ipaddress" HeaderText="IP Address" />
                                        <%-- <asp:BoundField DataField="logindatetime" HeaderText="IP Address" />--%>
                                      <asp:BoundField DataField="logindatetime"
                                            HeaderText="Login Date &amp; Time" HtmlEncode="False" HtmlEncodeFormatString="False" />
                                        <asp:BoundField DataField="logoutdatetime"
                                            HeaderText="Logout Date &amp; Time" HtmlEncode="False" HtmlEncodeFormatString="False" />
                                        <asp:BoundField DataField="status" HeaderText="Status " />
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView ID="GVAuditLog" runat="server" AutoGenerateColumns="False" GridLines="None" AllowPaging="true" PageSize="15"
                                    Visible="true" CssClass="table table-striped" DataKeyNames="" OnPageIndexChanging="GVAuditLog_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.N.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="actionby" HeaderText="User" />
                                        <asp:BoundField DataField="ipaddress" HeaderText="IP Address" />
                                        <asp:BoundField DataField="datetime"
                                            HeaderText="Login Date &amp; Time" HtmlEncode="False" HtmlEncodeFormatString="False" />
                                       <asp:BoundField DataField="actioncategory" HeaderText="Action Category " />
                                        <asp:BoundField DataField="actionname" HeaderText="Action Type " />
                                    </Columns>
                                </asp:GridView>
                                <asp:Panel ID="pnlNoRecord" runat="server" Width="100%" Visible="false">
                                    <center>
                                            <i class="fa fa-times-circle-o" style="font-size: 120px; color: #e4e4e6;"></i>
                                            <p style="color: #e4e4e6; font-size: 25px; font-weight: 500;">
                                                No Record Found
                                            </p>
                                        </center>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
    </div>

    </div>
</asp:Content>


