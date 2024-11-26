<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Reports/SysAdmReportmaster.master" AutoEventWireup="true" CodeFile="rpt_TE1.aspx.cs" Inherits="Auth_Reports_rpt_TE1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <style>
        .truncate-text {
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
}
    </style>
    <style>
        .rr {
            text-align:right !important;
        }
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
    <script type="text/javascript">

        $(document).ready(function () {

            var currDate = new Date().getDate();
            var preDate = new Date(new Date().setDate(currDate - 1));
            var todayDate = new Date(new Date().setDate(currDate));

            $('[id*=txtfrmdate]').datepicker({
                endDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            })



            $('[id*=txttodate]').datepicker({
                endDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            })

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid" style="padding-top: 20px;">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row mb-2">
            <div class="col-lg-12">
                <div class="card">
                    <div class="row card-header m-0 p-1">
                        <div class="col-lg-12">
                            <div class="row">
                                <div class="col-lg-2 form-group">
                                    <label for="ddlReportType">
                                        Report Type</label>
                                    <asp:DropDownList ID="ddlReportType" runat="server" AutoPostBack="true" OnClientClick="return ShowLoading()" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-2 form-group">
                                    <label for="ddlReport">
                                        Report Name</label>
                                    <asp:DropDownList ID="ddlReport" O runat="server" AutoPostBack="true" OnClientClick="return ShowLoading()" CssClass="form-control form-control-sm"
                                        OnSelectedIndexChanged="ddlReport_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>


                                <div class="col-lg-2 form-group" id="fromdate" runat="server" visible="false">
                                    <label>From Date</label>
                                    <div class="input-group date">
                                        <asp:TextBox ID="txtfrmdate" ToolTip="Enter Invoice Date" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="txtfrmdate" ValidChars="/" />
                                    </div>
                                </div>
                                <div class="col-lg-2 form-group" id="todate" runat="server" visible="false">
                                    <label>To Date</label>
                                    <div class="input-group date">
                                        <asp:TextBox ID="txttodate" ToolTip="Enter Invoice Date" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="txttodate" ValidChars="/" />
                                    </div>
                                </div>

                                <div class="col-lg-2 form-group">
                                    <label id="lbldepot" runat="server" visible="false">
                                        Depot</label>
                                    <asp:DropDownList ID="ddldepot" runat="server" Visible="false" CssClass="form-control form-control-sm">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-lg-1 form-group">
                                    <label id="lblpayment" runat="server" visible="false">
                                        Payment Mode</label>
                                    <asp:DropDownList ID="ddlpaymentmode" runat="server" Visible="false" CssClass="form-control form-control-sm">
                                        <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                        <asp:ListItem Value="QR" Text="QR"></asp:ListItem>
                                        <asp:ListItem Value="CASH" Text="CASH"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1 pt-3 mt-2">
                                    <asp:LinkButton ID="lbtnsearch" runat="server" Visible="false" OnClientClick="return ShowLoading()" OnClick="lbtnSearch_Click" CssClass="btn btn-warning btn-sm">
                                    <i class="fa fa-search"> Search</i></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel runat="server" ID="pnlMsg" Visible="True">
            <div class="row">
                <div class="col-12 mt-5">
                    <center>
                        <asp:Label runat="server" Text="To generate report Click on Search Button." Font-Bold="true" Font-Size="40px" ForeColor="LightGray"></asp:Label>
                    </center>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlReport" Visible="False">
            <div class="row">
                <div class="col-sm-12 flex-column d-flex stretch-card">
                    <div class="card">
                        <div class="card-header bg-white">
                            <div class="row">
                                <div class="col-lg-8">
                                    <b>
                                        <asp:Label ID="lblReportName" runat="server"></asp:Label></b>
                                    <br />
                                    <span style="font-size: 14pt;"><b>Summary | </b>
                                        <asp:Label ID="lblsmry" runat="server" Text=""></asp:Label>
                                    </span>
                                </div>
                                <div class="col-lg-4 text-right">
                                    <asp:LinkButton ID="lbtnPDF" runat="server" OnClick="lbtnPDF_Click" CssClass="btn btn-primary btn-sm"><i class="fa fa-file-pdf" style="font-size: 18px; margin-right: 8px;"></i> PDF</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnEXCEL" runat="server" OnClick="lbtnEXCEL_Click" CssClass="btn btn-danger btn-sm"><i class="fa fa-file-excel" style="font-size: 18px; margin-right: 8px;"></i> EXCEL</asp:LinkButton>
                                </div>
                            </div>
                        </div>

                        <div class="card-body table-responsive" style="min-height: 320px;">
                            <asp:GridView ID="gvEtm" runat="server" GridLines="None" CssClass="table" AllowPaging="true" OnPageIndexChanging="gvEtm_PageIndexChanging"
                                PageSize="15" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <div class="header-container">
                                                <div class="main-heading text-center"></div>
                                                <div class="row">
                                                    <span class="sub-heading mt-4 ml-3">#</span>
                                                </div>
                                            </div>
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <div class="header-container">
                                                <div class="main-heading text-center"></div>
                                                <div class="row">
                                                    <span class="sub-heading mt-4">Waybill No.</span>
                                                </div>
                                            </div>
                                            </div>
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Eval("waybillrefno") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Start Date<br>End Date">
                                        <HeaderTemplate>
                                            <div class="header-container">
                                                <div class="main-heading text-center"></div>
                                                <div class="row">
                                                    <span class="sub-heading mt-4">Start Date<br>End Date</span>
                                                </div>
                                            </div>
                                            </div>
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <%#Eval("start_date") %><br /><%#Eval("end_date") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                     <asp:TemplateField HeaderText="Service Name">
                                         <ItemStyle HorizontalAlign="Left" CssClass="truncate-text" />
                                        <ItemTemplate>
                                            <%#Eval("service_name") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount <br>Collected(A)" HeaderStyle-CssClass="rr">
                                        <HeaderTemplate>
                                            <div class="header-container">
                                                <div class="main-heading text-center">Amount Collected(A)</div>
                                                <div class="row">
                                                    <div class="col-6 text-center">
                                                        <span class="sub-heading  mt-1">Cash</span>
                                                    </div>
                                                    <div class="col-6 text-center">
                                                        <span class="sub-heading  mt-1">Qr</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <div class="row">
                                                <div class="col-6 text-center">
                                                    <%#Eval("cash_") %>
                                                </div>
                                                <div class="col-6 text-center">
                                                    <%#Eval("qr_") %> 
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Refund Amount due<br> to Breakdown<br> (in Rupees) (B)" HeaderStyle-CssClass="rr">
                                       
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <%#Eval("breakdown_") %> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Expense Amount<br>(in Rupees) (C)" HeaderStyle-CssClass="rr">
                                       
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <%#Eval("expense_") %> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Amount<br>(in Rupees) <br> D = A-(B+C)" HeaderStyle-CssClass="rr">
                                       
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <%#Eval("total_") %> 
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <PagerStyle CssClass="pagination-ys" />
                               <%-- <HeaderStyle HorizontalAlign="Right" Font-Size="XX-Large" />--%>
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


</asp:Content>


















