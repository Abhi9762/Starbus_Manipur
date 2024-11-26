<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Reports/cntrMasterPage.master" AutoEventWireup="true" CodeFile="rpt_CB2.aspx.cs" Inherits="Auth_Reports_rpt_CB2" %>

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
    <script src="../assets/vendor/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var todayDate = new Date().getDate();
            var endD = new Date(new Date().setDate(todayDate));
            var currDate = new Date();
            $('[id*=txtBookingdate]').datepicker({
                //startDate: "dateToday",
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
     <asp:HiddenField ID="hidtoken" runat="server"  />
    <div class="container-fluid" style="padding-top: 10px;">
        <div class="row mb-2">
            <div class="col-lg-12">
                <div class="card">
                    <div class="row card-header m-0 p-1">
                        <div class="col-lg-4">
                            <div class="row">
                                <div class="col-lg-6 form-group">
                                    <label for="ddlReportType">
                                        Report Type</label>
                                    <asp:DropDownList ID="ddlReportType" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-6 form-group">
                                    <label for="ddlReport">
                                        Report Name</label>
                                    <asp:DropDownList ID="ddlReport" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm"
                                        OnSelectedIndexChanged="ddlReport_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-2">
                            <div class="row">
                                <div class="col-lg-12 form-group" id="fromdate" runat="server" visible="false">

                                    <label>Booking Date</label>
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" AutoComplete="off" runat="server" ID="txtBookingdate" MaxLength="10" ToolTip="Enter Date"
                                            placeholder="DD/MM/YYYY" Text="" Style="display: inline;"></asp:TextBox>
                                      </div>
                                </div>



                               
                            </div>
                        </div>



                        <div class="col-lg-4">
                            <div class="row">

                                <div class="col-sm-1 pt-3 mt-2">
                                    <asp:LinkButton ID="lbtnsearch" OnClick="lbtnsearch_Click" runat="server" Visible="false" CssClass="btn btn-warning btn-sm">
                                    <i class="fa fa-search"> </i></asp:LinkButton>
                                </div>
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
        <div class="row pl-4 pr-4">
            <div class="col-sm-12 flex-column d-flex stretch-card">
                <div class="card">
                    <div class="card-header bg-white">
                        <div class="d-flex align-items-center justify-content-between">
                            <b>
                                <asp:Label ID="lblReportName" runat="server"></asp:Label></b>
                            <div class="dropdown">
                                <asp:LinkButton ID="lbtnDownload" OnClick="lbtnDownload_Click" runat="server" CssClass="btn btn-primary btn-sm"><i class="fa fa-file-pdf" style="font-size: 18px; margin-right: 8px;"></i> DOWNLOAD</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body table-responsive" style="min-height: 320px;">
                    	   <asp:GridView ID="gvOnlineBooking" runat="server" CssClass="table text-left table-striped" GridLines="None"
                            ShowHeader="true" BorderStyle="None" AllowPaging="true" PageSize="15"
                            AutoGenerateColumns="false" HeaderStyle-Font-Size="10pt" Font-Size="10pt" OnPageIndexChanging="gvOnlineBooking_PageIndexChanging">
						
							<Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="CANCELLATION DATETIME">
										<ItemTemplate>
									   <%#Eval("CANCELLATION_DATETIME") %>
										</ItemTemplate>
									</asp:TemplateField>
                                    <asp:TemplateField HeaderText="CANCELLATION REFERENCE NO.">
										<ItemTemplate>
									   <%#Eval("TRANS_CANCEL_REFNO") %>
										</ItemTemplate>
									</asp:TemplateField>
									
								
								<asp:TemplateField HeaderText="TRANSACTION AMOUNT">
										<ItemTemplate>
										 <%#Eval("TRANS_AMOUNT") %>
										</ItemTemplate>
									</asp:TemplateField>
                                    
                            <asp:TemplateField HeaderText="REFUNDED AMOUNT">
										<ItemTemplate>
										 <%#Eval("Trans_Refunded") %>
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

