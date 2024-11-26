<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/AgentMaster.master" AutoEventWireup="true" CodeFile="AgTripAssignment.aspx.cs" Inherits="Auth_AgTripAssignment" %>

   

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <style>
        .modalBackground {
            background: #000000;
            opacity: 0.4;
        }

        .table_grid {
            width: 100%;
            font-family: Verdana;
            border: #f1f2f7;
            line-height: 20px;
        }

            .table_grid tr {
                border-bottom: 1px solid #70d1f4;
            }

            .table_grid th {
                padding: 0px 2px;
                text-align: left;
                font-weight: 500;
                font-size: 8pt;
                font-weight: bold;
            }

            .table_grid td {
                padding-top: 8px;
                text-align: left;
                vertical-align: top;
                font-weight: 300;
                font-size: 9pt;
                color: #000;
                border-bottom: 1px dotted;
            }

        .gridview {
            padding: 2px;
            margin: 2% auto;
        }

            .gridview table td {
                border-top: none;
                padding: 2px;
                background-color: transparent;
            }

            .gridview a {
                margin: auto 1%;
                border-radius: 5px;
                border: 1px solid #444;
                padding: 3px 8px 3px 8px;
                color: #000;
                font-weight: bold;
                text-decoration: none;
                -o-box-shadow: 1px 1px 1px #111;
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
            }

                .gridview a:hover {
                    background-color: #1e8d12;
                    color: #fff;
                }

            .gridview span {
                background-color: #ae2676;
                color: #fff;
                -o-box-shadow: 1px 1px 1px #111;
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
                border-radius: 5px;
                padding: 5px 10px 5px 10px;
            }

            .gridview .table-striped tbody tr:nth-of-type(odd) {
                background-color: rgba(0,0,0,0);
            }
    </style>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content mt-3">
        
        <asp:HiddenField ID="hidtoken" runat="server" />
        <asp:Panel ID="pnltripY" runat="server">
            <div class="row">
                <div class="col-lg-6">
                    <p style="font-size: 21px; color: Black;">
                        Today Trip Assignment
                    </p>
                </div>
                
            </div>
            <div class="row">
                <div class="col-lg-4">
                    <div class="card">
                        <div class="card-header">
                            <p class="card-title m-0 text-dark">
                                Prepared<span class="badge badge-success">
                                    <asp:Label ID="LabelPreparedCountP" runat="server" Text="0"></asp:Label></span>
                            </p>
                        </div>
                        <div class="card-body p-0" style="min-height: 250px;">
                            <p class="text-center mb-0">
                                <asp:Label ID="lblprtriperror" runat="server" Text="No Prepared Trip" CssClass="font-weight-bold"
                                    Visible="false" Style="font-size: 25px; color: #d6d6d6;"></asp:Label>
                            </p>
                            <asp:GridView ID="grdpreparedtrip" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                GridLines="None" DataKeyNames="arr_time,j_duration,depo_code,trip_code,dept_time" OnRowCommand="grdpreparedtrip_RowCommand" OnPageIndexChanging="grdpreparedtrip_PageIndexChanging"
                                CssClass="table table-hover mb-0" PageSize="6" Font-Size="13px"
                                ShowHeader="false">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <p style="font-size: 12px; line-height: 15px; margin-bottom: 0px;">
                                                <asp:Label ID="lblDepot" runat="server" Text='<%#  Eval("depo_name") %>'></asp:Label><br />
                                                <asp:Label ID="lblTripCode" runat="server" Text='<%#  Eval("trip_code") %>' Font-Bold="true"
                                                    Font-Size="14px" ForeColor="Black"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblFrom" runat="server" Text='<%#  Eval("fstation_abb") %>'></asp:Label>&nbsp;-&nbsp;
                                                <asp:Label ID="lblTo" runat="server" Text='<%#  Eval("tstation_abb") %>'></asp:Label>
                                            </p>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lblServiceCode" runat="server" Text='<%#  Eval("servicetype_name") %>'
                                                Font-Size="11px"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblDepartureTime" runat="server" Text='<%#  Eval("dept_time") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnprint1" runat="server" CssClass="btn btn-primary btn-sm mt-2" CommandName="Print"
                                                CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 5px;">
                                            <i class="fa fa-download"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Left" CssClass="gridview" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="col-lg-8">
                    <div class="card">
                        <div class="card-header">
                            <div class="row">
                                <div class="col-lg-3">
                                    <p class="card-title m-0 text-dark">
                                        Ready To Prepare <span class="badge badge-success">
                                            <asp:Label ID="LabelReadyToPrintCountUP" runat="server" Text="0"></asp:Label></span>
                                    </p>
                                </div>
                                <div class="col-lg-4 text-right pt-1">
                                    <p style="font-size: 13px; color: Black; margin-bottom: 0px;">
                                        Trip Code/Service Code
                                    </p>
                                </div>
                                <div class="col-lg-3 p-0">
                                    <asp:TextBox ID="TextBoxSearchTripCode" runat="server" MaxLength="20" CssClass="form-control"
                                        Style="padding: 2px; font-size: 13px;"></asp:TextBox>
                                </div>
                                <div class="col-lg-2">
                                    <asp:Button ID="ButtonSearch" CssClass="btn btn-success btn-sm" OnClick="ButtonSearch_Click" runat="server" Text="Search"
                                        Style="padding: 2px; font-size: 12px;" />
                                    <asp:Button ID="ButtonAllTripReadyToPrepared" CssClass="btn btn-warning btn-sm" runat="server" OnClick="ButtonAllTripReadyToPrepared_Click" Text="All"
                                        Style="padding: 2px; font-size: 12px; width: 40px;" />
                                </div>
                            </div>
                        </div>
                        <div class="card-body p-0" style="min-height: 250px;">
                            <p class="text-center mb-0">
                                <asp:Label ID="lbluntriperror" runat="server" Text="No Active Trip for Print" CssClass="font-weight-bold"
                                    Visible="false" Style="font-size: 25px; color: #d6d6d6;"></asp:Label>
                            </p>

                            <asp:GridView ID="grdUnpreparedtrip" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                GridLines="None" DataKeyNames="arr_time,j_duration,depo_code,trip_code,dept_time,fstation_name,tstation_name,busservicetype_code,fstation_abb,tstation_abb" OnRowCommand="grdUnpreparedtrip_RowCommand" OnPageIndexChanging="grdUnpreparedtrip_PageIndexChanging"
                                class="table table-hover mb-0" PageSize="11" Font-Size="13px">
                                <Columns>
                                    <asp:TemplateField HeaderText="Depot">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepot" runat="server" Text='<%#  Eval("depo_name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Service Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblServiceCode" runat="server" Text='<%#  Eval("servicetype_name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Trip Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTripCode" runat="server" Text='<%#  Eval("trip_code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Station(s)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFrom" runat="server" Text='<%#  Eval("fstation_name") %>'></asp:Label>
                                            -
                                            <asp:Label ID="lblTo" runat="server" Text='<%#  Eval("tstation_name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Departure">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepartureTime" runat="server" Text='<%#  Eval("dept_time") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnproceed" runat="server" CssClass="btn btn-success btn-sm"
                                                CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="proceed"
                                                Style="padding: 1px 6px; border-radius: 5px; font-size: 12px;">Generate</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Left" CssClass="gridview" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="card">
                        <div class="card-header">
                            <p class="card-title m-0 text-dark">
                                Time Over Unprepared Trip<span class="badge badge-success">
                                    <asp:Label ID="LabelTimeOverCountUP" runat="server" Text="0"></asp:Label></span>
                            </p>
                        </div>
                        <div class="card-body p-0" style="min-height: 250px;">
                            <p class="text-center mb-0">
                                <asp:Label ID="lblTimeOverTripError" runat="server" Text="No TimeOver Trip" CssClass="font-weight-bold"
                                    Visible="false" Style="font-size: 25px; color: #d6d6d6;"></asp:Label>
                            </p>
                            <asp:GridView ID="grdTimeoverTrip" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                ShowHeader="false" GridLines="None" DataKeyNames="arr_time,j_duration,depo_code,trip_code,dept_time,fstation_name,tstation_name" OnRowCommand="grdTimeoverTrip_RowCommand" OnPageIndexChanging="grdTimeoverTrip_PageIndexChanging"
                                class="table table-hover mb-0" PageSize="5" Font-Size="13px">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <p style="font-size: 12px; line-height: 15px; margin-bottom: 0px;">
                                                <asp:Label ID="lblDepot" runat="server" Text='<%#  Eval("depo_name") %>'></asp:Label><br />
                                                <asp:Label ID="lblTripCode" runat="server" Text='<%#  Eval("trip_code") %>' Font-Bold="true"
                                                    Font-Size="14px" ForeColor="Black"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblFrom" runat="server" Text='<%#  Eval("fstation_abb") %>'></asp:Label>&nbsp;-&nbsp;
                                                <asp:Label ID="lblTo" runat="server" Text='<%#  Eval("tstation_abb") %>'></asp:Label>
                                            </p>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lblServiceCode" runat="server" Text='<%#  Eval("servicetype_name") %>'
                                                Font-Size="11px"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblDepartureTime" runat="server" Text='<%#  Eval("dept_time") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnproceed" runat="server" CssClass="btn btn-success btn-sm mt-2"
                                                CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="print"
                                                Style="border-radius: 5px;"> <i class="fa fa-download"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Left" CssClass="gridview" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="card">
                        <div class="card-header">
                            <p class="card-title m-0 text-dark">
                                Upcoming Unprepared Trip <span class="badge badge-danger">
                                    <asp:Label ID="LabelUpcomingCountUP" runat="server" Text="0"></asp:Label></span>
                            </p>
                        </div>
                        <div class="card-body p-0" style="min-height: 250px;">

                            <p class="text-center mb-0">
                                <asp:Label ID="lblUpcomingTripError" runat="server" Text="No Upcoming Trip" CssClass="font-weight-bold"
                                    Visible="false" Style="font-size: 25px; color: #d6d6d6;"></asp:Label>
                            </p>
                            <asp:GridView ID="grdUpcomingTrip" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                ShowHeader="false" GridLines="None" DataKeyNames="arr_time,j_duration,depo_code,trip_code,dept_time,fstation_name,tstation_name" OnPageIndexChanging="grdUpcomingTrip_PageIndexChanging"
                                class="table table-hover mb-0" PageSize="5">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <p style="font-size: 12px; line-height: 15px; margin-bottom: 0px;">
                                                <asp:Label ID="lblDepot" runat="server" Text='<%#  Eval("depo_name") %>'></asp:Label><br />
                                                <asp:Label ID="lblTripCode" runat="server" Text='<%#  Eval("trip_code") %>' Font-Bold="true"
                                                    Font-Size="14px" ForeColor="Black"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblFrom" runat="server" Text='<%#  Eval("fstation_name") %>'></asp:Label>&nbsp;-&nbsp;
                                                <asp:Label ID="lblTo" runat="server" Text='<%#  Eval("tstation_name") %>'></asp:Label>
                                            </p>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lblServiceCode" runat="server" Text='<%#  Eval("servicetype_name") %>'
                                                Font-Size="11px"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblDepartureTime" runat="server" Text='<%#  Eval("dept_time") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Left" CssClass="gridview" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <%--******************************************Prepared Trip Chart****************--%>
            <div class="row">
                <cc1:ModalPopupExtender ID="mpTripAssign" runat="server" PopupControlID="pnlTripAssign"
                    TargetControlID="btnTripAssign" CancelControlID="btnCancelTripAssign" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlTripAssign" runat="server" Style="min-width: 50%; max-width: 90%;">
                    <div class="card">
                        <div class="card-header text-left">
                            Bus/Driver/Conductor Assignment for Trip Code:
                                            <b>
                                                <asp:Label ID="lblSvCode" runat="server"></asp:Label></b>
                        </div>
                        <div class="card-body">
                            <div class="row pt-1 pb-1">
                                <div class="col-lg-4">
                                    <span style="font-size: 12px;">Waybill Reference No</span><span style="color: red;">*</span>
                                    <asp:DropDownList ID="ddlWaybills" runat="server" CssClass="form-control form-control-sm" Width="80%" Visible="true" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtwaybills" runat="server" MaxLength="15" Width="80%" CssClass="form-control form-control-sm" AutoComplete="Off" Visible="false"
                                        placeholder="Waybill Number"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExten" runat="server" ValidChars="0123456789" FilterType="Custom" TargetControlID="txtwaybills" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4 pt-2">
                                    <span style="font-size: 12px;">Select Bus</span><span style="color: red;">*</span>
                                    <asp:DropDownList ID="drpBuss" runat="server" Width="80%" CssClass="form-control form-control-sm">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-4 pt-2">
                                    <span style="font-size: 12px;">Select Driver</span><span style="color: red;">*</span>
                                    <asp:DropDownList ID="drpDriver" runat="server" Width="80%" CssClass="form-control form-control-sm">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-4 pt-2">
                                    <span style="font-size: 12px;">Select Conductor</span><span style="color: red;">*</span>
                                    <asp:DropDownList ID="drpConductor" runat="server" Width="80%" CssClass="form-control form-control-sm">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-lg-12 text-right pt-4">
                                    <asp:LinkButton ID="btnCancelTripAssign" runat="server" CssClass="btn btn-danger" Style="border-radius: 4px;"> <i class="fa fa-times" ></i>  Cancel</asp:LinkButton>
                                    <asp:LinkButton ID="btnSaveTripAssign" runat="server" OnClick="btnSaveTripAssign_Click" CssClass="btn btn-success" Style="border-radius: 4px;"> <i class="fa fa-check"></i>  Save</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div style="visibility: hidden;">
                        <asp:Button ID="btnTripAssign" runat="server" Text="" />
                    </div>
                </asp:Panel>
            </div>
            <div class="row">
                <cc1:ModalPopupExtender ID="mpDownloadPrepared" runat="server" PopupControlID="pnlmpDownloadPrepared"
                    TargetControlID="btnOpenmpDownloadPrepared" CancelControlID="lbtnClosempDownloadPrepared" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlmpDownloadPrepared" runat="server" Style="min-width: 50%; max-width: 90%;">
                    <div class="card">
                        <div class="card-body">
                            <h3 class="mb-4">Trip chart generated successfully.</h3>
                            <h3>Download Trip Chart ?</h3>
                        </div>
                        <div class="card-footer text-right">
                            <asp:LinkButton ID="lbtnClosempDownloadPrepared" runat="server" CssClass="btn btn-danger" Style="border-radius: 4px;"> <i class="fa fa-times" ></i>  Cancel</asp:LinkButton>
                            <asp:LinkButton ID="lbtDownloadmpDownloadPrepared" runat="server" CssClass="btn btn-success" Style="border-radius: 4px;"> <i class="fa fa-download"></i>  Download</asp:LinkButton>
                        </div>
                    </div>
                    <br />
                    <div style="visibility: hidden;">
                        <asp:Button ID="btnOpenmpDownloadPrepared" runat="server" Text="" />
                    </div>
                </asp:Panel>
            </div>

            <div class="row">
                <cc1:ModalPopupExtender ID="mpError" runat="server" PopupControlID="pnlError" CancelControlID="Button2"
                    TargetControlID="Button3" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlError" runat="server" Style="position: fixed;">
                    <div class="card" style="min-width: 350px;">
                        <div class="card-header" style="background-color: #e4273b; color: White;">
                            <h4 class="card-title">
                                <span style="font-size: 11pt;">Please Check & Correct</span>
                                <asp:LinkButton ID="lbtnerrorclose1" runat="server" ToolTip="Close" Style="float: right; color: white; padding: 0px;"> <i class="fa fa-times"></i>  </asp:LinkButton>
                            </h4>
                        </div>
                        <div class="card-body" style="min-height: 100px;">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblerrmsg" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:LinkButton ID="lbtnerrorclose" runat="server" CssClass="btn btn-warning" Style="height: 30px; width: 90px; padding-top: 4px; font-size: 10pt; border-radius: 4px;"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <br />
                    <div style="visibility: hidden;">
                        <asp:Button ID="Button3" runat="server" Text="" />
                        <asp:Button ID="Button2" runat="server" Text="" />
                    </div>
                </asp:Panel>
            </div>


                   <div class="row">
            <cc1:ModalPopupExtender ID="mpTripchart" runat="server" PopupControlID="pnlticket"
                CancelControlID="lbtnclose" TargetControlID="Button8" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlticket" runat="server" Style="position: fixed;">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-lg-6">
                                <h5 class="card-title text-left mb-0">Trip Chart
                                </h5>
                            </div>
                            <div class="col-lg-6">
                                <asp:LinkButton ID="lbtnclose" runat="server" CssClass="text-danger float-right"> <i class="fa fa-times"></i> </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body text-left pt-2" style="overflow: auto;">
                        <embed id="tkt" runat="server" src="" style="height: 70vh; width: 65vw;" />
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button8" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        </asp:Panel>
        <asp:Panel ID="pnltripN" runat="server" Visible="false">
            <div class="row">
                <div class="col-lg-12">
                    <center>
                        <div class="card" style="width: 50%; min-height: 250px;">
                            <div class="card-body text-center p-1" style="color: red; font-family: verdana; font-size: 15pt;">
                                <br />
                                <br />
                                <span>You are not available trip chart generation facilities</span><br />
                                <span>Please Conatact APSTS helpdesk for generate trip chart </span>
                                <br />
                                <br />
                                <asp:LinkButton ID="btnback" runat="server" Text="Back to dashboad" CssClass="btn btn-primary"></asp:LinkButton>
                            </div>
                        </div>
                    </center>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>


