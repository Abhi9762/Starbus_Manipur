<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Reports/SysAdmReportmaster.master" AutoEventWireup="true" CodeFile="rpt_B2.aspx.cs" Inherits="Auth_Reports_rpt_B2" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>

    <style>
        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .table {
            width: 100%;
        }

            .table th, .table td {
                padding: .5rem 0.75rem;
                vertical-align: top;
                border-top: 1px solid #dce1e3;
                font-size: 13px;
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid" style="padding-top: 20px;">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row mb-2">
            <div class="col-lg-12">
                <div class="card">
                    <div class="row card-header m-0 p-1">
                        <div class="col-lg-3">
                            <div class="row">
                                <div class="col-lg-6 form-group">
                                    <label for="ddlReportType">
                                        Report Type</label>
                                    <asp:DropDownList ID="ddlReportType" runat="server" AutoPostBack="true" OnClientClick="return ShowLoading()" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-6 form-group">
                                    <label for="ddlReport">
                                        Report Name</label>
                                    <asp:DropDownList ID="ddlReport" OnSelectedIndexChanged="ddlReport_SelectedIndexChanged" runat="server" AutoPostBack="true" OnClientClick="return ShowLoading()" CssClass="form-control form-control-sm">
                                    </asp:DropDownList>
                                </div>

                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="row">
                                <div class="col-lg-6 form-group">
                                    <asp:Label runat="server" ID="lblCategory" Visible="false" Text="Category"></asp:Label>

                                    <asp:DropDownList ID="ddlBusPassCategory" Visible="false" OnSelectedIndexChanged="ddlBusPassCategory_SelectedIndexChanged" AutoPostBack="true" OnClientClick="return ShowLoading()" runat="server" CssClass="form-control form-control-sm">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-6 form-group">
                                    <asp:Label runat="server" ID="lblPasstype" Visible="false" Text="Type"></asp:Label>
                                    <asp:DropDownList ID="ddlpasstype" runat="server" Visible="false" AutoPostBack="true" OnClientClick="return ShowLoading()" CssClass="form-control form-control-sm">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-5">
                            <div class="row">
                                <div class="col-lg-4 form-group">
                                    <asp:Label runat="server" ID="lblYear" Visible="false" Text="Year"></asp:Label>
                                    <asp:DropDownList ID="ddlYear" runat="server" Visible="false" CssClass="form-control form-control-sm">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-4 form-group">
                                    <asp:Label runat="server" ID="lblMonth" Visible="false" Text="Month"></asp:Label>
                                    <asp:DropDownList ID="ddlMonth" runat="server" Visible="false" AutoPostBack="true" OnClientClick="return ShowLoading()" CssClass="form-control form-control-sm">
                                    </asp:DropDownList>
                                </div>

                                <%--     <div class="col-lg-3 form-group">
                                    <asp:Label runat="server" ID="lblStatus" Visible="false" Text="Status"></asp:Label>
                                    <asp:DropDownList ID="ddlStatus" runat="server" Visible="false" OnClientClick="return ShowLoading()" AutoPostBack="true" CssClass="mt-2 form-control form-control-sm">

                                        <asp:ListItem Value="P">Pending For Verification</asp:ListItem>
                                        <asp:ListItem Value="I">Issue</asp:ListItem>
                                        <asp:ListItem Value="R">Reject</asp:ListItem>
                                    </asp:DropDownList>
                                </div>--%>
                                <div class="col-lg-4 form-group">
                                    <asp:Label runat="server" ID="lblApplyMode" Visible="false" Text="Apply Mode"></asp:Label>
                                    <asp:DropDownList ID="ddlApplyMode" runat="server" Visible="false" CssClass="form-control form-control-sm">
                                        <asp:ListItem Value="T">Traveller</asp:ListItem>
                                        <asp:ListItem Value="C">Counter</asp:ListItem>
                                        <asp:ListItem Value="A">Agent</asp:ListItem>
                                        <asp:ListItem Value="S">CSC</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <div class="row">

                                <div class="col-lg-12 form-group pt-3 mt-2">
                                    <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click" OnClientClick="return ShowLoading()" Visible="false" CssClass="btn btn-warning btn-sm">
                                    <i class="fa fa-search" > Search</i></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel runat="server" ID="pnlMsg" Visible="true">
            <div class="row">
                <div class="col-12 mt-5">
                    <center>
                        <asp:Label runat="server" Text="To generate report Click on Search Button." Font-Bold="true" Font-Size="40px" ForeColor="LightGray"></asp:Label>

                    </center>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlReport" Visible="false">
            <div class="row">
                <div class="col-sm-12 flex-column d-flex stretch-card">
                    <div class="card">
                        <div class="card-header bg-white">
                            <div class="d-flex align-items-center justify-content-between">
                                <b>
                                    <asp:Label ID="lblReportName" runat="server"></asp:Label></b>
                                <%--<div class="dropdown">
											<asp:LinkButton ID="lbtnDownload1_1_depot" runat="server" CssClass="btn btn-primary btn-sm"><i class="fa fa-file-pdf-o" style="font-size: 18px; margin-right: 8px;"></i> DOWNLOAD</asp:LinkButton>
										</div>--%>
                                <asp:LinkButton runat="server" CssClass="btn btn-primary btn-sm ml-1 float-right" Visible="true" ID="lbtnDownload" OnClick="lbtnDownload_Click"><i class="fa fa-file-pdf-o"></i> Download</asp:LinkButton>

                            </div>
                        </div>
                        <div class="card-body table-responsive" style="min-height: 320px;">
                            <asp:GridView ID="gvBuspass" runat="server" GridLines="None" CssClass="table" ClientIDMode="Static" OnPageIndexChanging="gvBuspass_PageIndexChanging"
                                AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PASS TYPE">
                                        <ItemTemplate>
                                            <%#Eval("pass_type") %>
                                            <br />
                                            <b>(Pass No.  <%#Eval("pass_number") %>)</b>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="APPLY MODE">
                                        <ItemTemplate>
                                            <%#Eval("apply_by") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FAX">
                                        <ItemTemplate>
                                            <%#Eval("val_fare") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RESERVATION">
                                        <ItemTemplate>
                                            <%#Eval("val_reservation") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TAX">
                                        <ItemTemplate>
                                            <%#Eval("val_tax") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TOTAL">
                                        <ItemTemplate>
                                            <%#Eval("val_total") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CURRENT STATUS">
                                        <ItemTemplate>
                                            <%#Eval("val_status") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <PagerStyle CssClass="pagination-ys" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>

            </div>
        </asp:Panel>
    </div>

    <div class="row">
        <cc1:ModalPopupExtender ID="mpError" runat="server" PopupControlID="pnlError" CancelControlID="lbtnerrorclose"
            TargetControlID="Button3" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlError" runat="server" Style="position: fixed; display: none">
            <div class="card" style="min-width: 350px; max-width: 650px;">
                <div class="card-header">
                    <h4 class="card-title m-0">Please Check
                    </h4>
                </div>
                <div class="card-body py-2 px-3" style="min-height: 100px; max-height: 70vh; overflow: auto;">
                    <asp:Label ID="lblerrmsg" runat="server" Font-Size="18px"></asp:Label>
                </div>
                <div class="card-footer text-right ">
                    <asp:LinkButton ID="lbtnerrorclose" runat="server" CssClass="btn btn-danger"> OK </asp:LinkButton>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="Button3" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>

    <script type="text/javascript">
        $('#gvEmployee').DataTable({
            dom: 'Bfrtip',
            buttons: [

                'csv', 'excel', 'pdf', 'print'


            ]
        });

    </script>
</asp:Content>



