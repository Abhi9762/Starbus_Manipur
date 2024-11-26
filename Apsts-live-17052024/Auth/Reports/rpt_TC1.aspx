<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Reports/SysAdmReportmaster.master" AutoEventWireup="true" CodeFile="rpt_TC1.aspx.cs" Inherits="Auth_Reports_rpt_TC1" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <script type="text/javascript">
        $(window).on('load', function () {
            HideLoading();
        });
        function ShowLoading() {
            var div = document.getElementById("loader");
            div.style.display = "block";
        }
        function HideLoading() {
            var div = document.getElementById("loader");
            div.style.display = "none";
        }
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

        input[type=radio] {
            box-sizing: border-box;
            padding: 0;
            margin-right: 6px;
        }
    </style>

        <script type="text/javascript">

            $(document).ready(function () {

                var currDate = new Date().getDate();
                var preDate = new Date(new Date().setDate(currDate - 1));
                var todayDate = new Date(new Date().setDate(currDate));

                $('[id*=txtfromdate]').datepicker({
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
                    <div class="card-header m-0 p-1">
                        <div class="row">
                            <div class="col-lg-4">
                                <div class="row">
                                    <div class="col-lg-6 form-group">
                                        <label for="ddlReportType">
                                            Report Type</label>
                                        <asp:DropDownList ID="ddlReportType" OnClientClick="return ShowLoading()" runat="server" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control form-control-sm">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-6 form-group">
                                        <label for="ddlReport">
                                            Report Name</label>
                                        <asp:DropDownList ID="ddlReport" OnClientClick="return ShowLoading()" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm"
                                            OnSelectedIndexChanged="ddlReport_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-8">
                                <div class="row" id="dvperameter" runat="server" visible="false">
                                    <div class="col-lg-2">
                                        <label>Booking Date</label>
                                        <div class="input-group date">
                                        <asp:TextBox ID="txtfromdate" ToolTip="Enter Invoice Date" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="txtfromdate" ValidChars="/" />
                                    </div>
                                    </div>
                                    <div class="col-lg-2">
                                        <label for="ddlServicetype">
                                            Service Type</label>
                                        <asp:DropDownList ID="ddlServicetype" OnClientClick="return ShowLoading()" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-4">
                                        <label for="ddldepot">
                                            Depot</label>
                                        <asp:DropDownList ID="ddldepot" OnClientClick="return ShowLoading()" runat="server" OnSelectedIndexChanged="ddldepot_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control form-control-sm">
                                        </asp:DropDownList>
                                    </div>                                   
                                    <div class="col-lg-2">
                                        <label for="ddlcounter">
                                            Booking Counter</label>
                                        <asp:DropDownList ID="ddlcounter" OnClientClick="return ShowLoading()" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2 pt-3 mt-2">
                                        <asp:LinkButton ID="lbtnsearch" runat="server" OnClick="lbtnsearch_Click" Visible="false" OnClientClick="Showloading();" CssClass="btn btn-warning btn-sm">
                                    <i class="fa fa-search"> Search</i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row d-none">
                                     <div class="col-lg-3">
                                        <label for="ddldepot">
                                            Sevice</label>
                                        <asp:DropDownList ID="ddlservice" OnClientClick="return ShowLoading()" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3">
                                        <label for="ddlroute">
                                            Route</label>
                                        <asp:DropDownList ID="ddlroute" OnClientClick="return ShowLoading()" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="card-body">
                        <div class="row mt-2">
                            <div class="col-lg-12">
                                <asp:Panel runat="server" ID="pnlMsg" Visible="True">
                                    <div class="row">
                                        <div class="col-12 mt-5">
                                            <center>
                          <asp:Label runat="server" Text="To generate report Click on Search Button." Font-Bold="true" Font-Size="40px" ForeColor="LightGray" ></asp:Label>
     
                    </center>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlReport" Visible="False">
                                    <div class="row">
                                        <div class="col-sm-12 flex-column d-flex stretch-card">
                                            <div class="card">
                                                <div class="card-header bg-white">
                                                    <div class="d-flex align-items-center justify-content-between">
                                                        <b>
                                                            <asp:Label ID="lblReportName" runat="server"></asp:Label></b>
                                                        <div class="dropdown">
                                                         	<asp:LinkButton ID="lbtnPDF" runat="server" OnClick="lbtnPDF_Click" CssClass="btn btn-primary btn-sm"><i class="fa fa-file-pdf" style="font-size: 18px; margin-right: 8px;"></i> PDF</asp:LinkButton>
										<asp:LinkButton ID="lbtnEXCEL" runat="server" OnClick="lbtnEXCEL_Click" CssClass="btn btn-danger btn-sm"><i class="fa fa-file-excel" style="font-size: 18px; margin-right: 8px;"></i> EXCEL</asp:LinkButton>
										  </div>
                                                    </div>
                                                </div>
                                                <div class="card-body table-responsive" style="min-height: 320px;">
                                                    <asp:GridView ID="gvcurntbookingreport" runat="server" GridLines="None" CssClass="table" AllowPaging="true"  OnPageIndexChanging="gvcurntbookingreport_PageIndexChanging"
                                                        PageSize="10" AutoGenerateColumns="false" DataKeyNames="trip_code_">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Service Type">
                                                                <ItemTemplate>
                                                                    <%#Eval("service_type_name_") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Depot">
                                                                <ItemTemplate>
                                                                    <%#Eval("office_name_") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Route">
                                                                <ItemTemplate>
                                                                    <%#Eval("route_name_") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Service">
                                                                <ItemTemplate>
                                                                    <%#Eval("service_name_") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Actual Depart Date/Time">
                                                                <ItemTemplate>
                                                                    <%#Eval("trip_date_") %> <%#Eval("trip_time_") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Seat Booked">
                                                                <ItemTemplate>
                                                                    <%#Eval("seats_booked_") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <%#Eval("totalamt_") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Bus Number">
                                                                <ItemTemplate>
                                                                    <%#Eval("bus_registration_no_") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Conductor">
                                                                <ItemTemplate>
                                                                    <%#Eval("conductor_name_") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Special Trip">
                                                                <ItemTemplate>
                                                                    <%#Eval("specialtripyn_") %>
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
                        </div>
                    </div>
                </div>
            </div>
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
    </div>

</asp:Content>



