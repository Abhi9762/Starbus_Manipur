<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Reports/SysAdmReportmaster.master" AutoEventWireup="true" CodeFile="rpt_M21.aspx.cs" Inherits="Auth_Reports_rpt_M21" %>

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

        table.dataTable {
            text-transform: uppercase;
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
                        <div class="col-lg-3 form-group">
                            <label for="ddlReportType">
                                Report Type</label>
                            <asp:DropDownList ID="ddlReportType" runat="server" AutoPostBack="true" OnClientClick="return ShowLoading()" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3 form-group">
                            <label for="ddlReport">
                                Report Name</label>
                            <asp:DropDownList ID="ddlReport" runat="server" AutoPostBack="true" OnClientClick="return ShowLoading()" CssClass="form-control form-control-sm"
                                OnSelectedIndexChanged="ddlReport_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3">
                            <asp:Label runat="server" ID="lbldepo" Visible="false" Text="Depot"></asp:Label>
                            <asp:DropDownList ID="ddldepo" runat="server" AutoPostBack="true" OnClientClick="return ShowLoading()" Visible="false" CssClass=" form-control form-control-sm">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3  pt-3 mt-2">
                            <asp:LinkButton ID="lbtnSearch" runat="server" Visible="false" OnClientClick="return ShowLoading()" OnClick="lbtnSearch_Click" CssClass="btn btn-warning btn-sm">
                                    <i class="fa fa-search"> Search</i></asp:LinkButton>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel ID="pnlmsg" Visible="true" runat="server">
            <div class="row">
                <div class="col-12 mt-5 ml-5">
                    <center>
                        <asp:Label runat="server" Text="To generate report Click on Search Button." font-Bold="true" font-size="40px" Forecolor="LightGrey"></asp:Label>
                    </center>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlreport" runat="server" Visible="false">
            <div class="row">
                <div class="col-sm-12 flex-column d-flex stretch-card">
                    <div class="card">
                        <div class="card-header bg-white">
                            <div class="d-flex align-items-center justify-content-between">
                                <h4 class="card-title mb-0" style="font-size: 15pt;">
                                    <asp:Label ID="lblReportName" runat="server"></asp:Label>
                                </h4>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">

                                <h4 class=" ml-4 mt-2 mb-0">Details</h4>
                                <div class="card-body table-responsive" style="min-height: 320px;">
                                    <asp:GridView ID="gvDepoIncome" runat="server" GridLines="None" CssClass="table" ClientIDMode="Static"
                                        AutoGenerateColumns="false" OnRowDataBound="gvDepoIncome_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Depot">
                                                <ItemTemplate>
                                                    <%#Eval("val_depot") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Service">
                                                <ItemTemplate>
                                                    <%#Eval("servcie_type") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Route">
                                                <ItemTemplate>
                                                    <%#Eval("val_route") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Depot Service">
                                                <ItemTemplate>
                                                    <%#Eval("depot_service") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Target Income/km">
                                                <ItemTemplate>
                                                    <%#Eval("income_km") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Target Income">
                                                <ItemTemplate>
                                                    <%#Eval("val_income") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Diesel Avg(Hill/Plain)">
                                                <ItemTemplate>
                                                    <%#Eval("val_average") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Validity">
                                                <ItemTemplate>
                                                    <%#Eval("val_validity") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="From -To Date">
                                                <ItemTemplate>
                                                    <%#Eval("from_todate") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerStyle CssClass="pagination-ys" />
                                    </asp:GridView>
                                </div>
                            </div>
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
        $('#gvDepoIncome').DataTable({
            "pageLength": 15,
            dom: 'Bfrtip',
            "bSort": false,
            buttons: [
                'copy', 'csv', 'pdf'
            ]
        });

    </script>
</asp:Content>



