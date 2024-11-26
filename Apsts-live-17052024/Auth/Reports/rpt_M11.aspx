<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Reports/SysAdmReportmaster.master" AutoEventWireup="true" CodeFile="rpt_M11.aspx.cs" Inherits="Auth_Reports_rpt_M11" %>



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
                        <div class="col-lg-4">
                            <div class="row">
                                <div class="col-lg-6 form-group">
                                    <label for="ddlReportType">
                                        Report Type</label>
                                    <asp:DropDownList ID="ddlReportType"  runat="server" OnClientClick="return ShowLoading()" AutoPostBack="true" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-6 form-group">
                                    <label for="ddlReport">
                                        Report Name</label>
                                    <asp:DropDownList ID="ddlReport"  runat="server" AutoPostBack="true" OnClientClick="return ShowLoading()" CssClass="form-control form-control-sm"
                                        OnSelectedIndexChanged="ddlReport_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>

                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="row">
                                <div class="col-lg-6 form-group">
                                    <asp:Label runat="server" ID="lblofc" Visible="false" Text="ETM Type"></asp:Label>
                                    <asp:DropDownList ID="ddlsETMType" runat="server"  Visible="false" CssClass=" form-control form-control-sm">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-6 form-group">
                                    <asp:Label runat="server" ID="lblStatus" Visible="false" Text="Status"></asp:Label>
                                    <asp:DropDownList ID="ddlStatus" runat="server"  Visible="false" CssClass="form-control form-control-sm">
                                    </asp:DropDownList>
                                </div>

                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="row">

                                <div class="col-lg-6 form-group pt-3 mt-2">
                                    <asp:LinkButton ID="lbtnSearch" runat="server"  OnClick="lbtnSearch_Click" OnClientClick="return ShowLoading()" Visible="false" CssClass="btn btn-warning btn-sm">
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
                          <asp:Label runat="server" Text="To generate report Click on Search Button." Font-Bold="true" Font-Size="40px" ForeColor="LightGray" ></asp:Label>
     
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
                                <h4 class="card-title mb-0" style="font-size: 15pt;">
                                    <asp:Label ID="lblReportName" runat="server"></asp:Label></h4>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-10" style="border-right: 2px solid #eee">

                                <h4 class=" ml-4 mt-2 mb-0">Details</h4>
                                <div class="card-body table-responsive" style="min-height: 320px;">
                                    <asp:GridView ID="gvEtm" runat="server" GridLines="None" CssClass="table" ClientIDMode="Static"
                                        AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ETM Registration No">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label21" runat="server" Text='<%#Eval("etm_refno") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label22" runat="server" Text='<%#Eval("type_name") %>' Style="font-size: 9pt" />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IMEI No">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label25" runat="server" Text='<%#Eval("imei_no1") %>' />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allocation Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblETMStatus" runat="server" Text='<%#Eval("stayus_name") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Duty Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="IMEI1" runat="server" Text='<%#Eval("duty_staus") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current Office">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label23" runat="server" Text='<%#Eval("issued_office") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerStyle CssClass="pagination-ys" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <h4 class="mt-2">Summary</h4>
                                <hr />
                                <h5 class="mt-2 mb-0">Status Count</h5>
                                <div class="row">
                                    <div class="col-sm-6" style="border-right: 2px solid #eee">
                                        Total
                                        <br />
                                        <b>
                                            <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label></b>
                                    </div>
                                    <div class="col-sm-6" style="border-right: 2px solid #eee">
                                        Issued To ETM Branch
                                            <br />
                                        <b>
                                            <asp:Label ID="lblTotalIssued" runat="server" Text=""></asp:Label></b>
                                    </div>
                                </div>
                                <h5 class="mt-2">In Progress</h5>
                                <div class="row">
                                    <div class="col-sm-6" style="border-right: 2px solid #eee">
                                        Pending For Issue
                                            <br />
                                        <b>
                                            <asp:Label ID="lblPendingIssue" runat="server" Text=""></asp:Label></b>
                                    </div>
                                    <div class="col-sm-6" style="border-right: 2px solid #eee">
                                        Pending For Receiving
                                            <br />
                                        <b>
                                            <asp:Label ID="lblPendingReceiving" runat="server" Text=""></asp:Label></b>
                                    </div>
                                </div>
                                <h5 class="mt-2">Not In Use</h5>
                                <div class="row">
                                    <div class="col-sm-6" style="border-right: 2px solid #eee">
                                        Faulty ETM
                                            <br />
                                        <b>
                                            <asp:Label ID="lblFaulty" runat="server" Text=""></asp:Label></b>
                                    </div>
                                    <div class="col-sm-6" style="border-right: 2px solid #eee">
                                        Obsolete ETM
                                            <br />
                                        <b>
                                            <asp:Label ID="lblObsolete" runat="server" Text=""></asp:Label></b>
                                    </div>
                                </div>
                                <h5 class="mt-2">Master Summary</h5>
                                <div class="row">
                                    <div class="col-sm-6" style="border-right: 2px solid #eee">
                                        Locked ETM
                                            <br />
                                        <b>
                                            <asp:Label ID="lblLocked" runat="server" Text=""></asp:Label></b>
                                    </div>
                                    <div class="col-sm-6" style="border-right: 2px solid #eee">
                                        Draft ETM
                                            <br />
                                        <b>
                                            <asp:Label ID="lblDraft" runat="server" Text=""></asp:Label></b>
                                    </div>
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
        $('#gvEtm').DataTable({
            "pageLength": 15,
            dom: 'Bfrtip',
            "bSort": false,
            buttons: [
                'copy', 'csv', 'pdf'
            ]
        });

    </script>
</asp:Content>

