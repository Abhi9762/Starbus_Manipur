<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Reports/SysAdmReportmaster.master" AutoEventWireup="true" CodeFile="rpt_P4.aspx.cs" Inherits="Auth_Reports_rpt_P4" %>

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
                                    <asp:DropDownList ID="ddlReport"  runat="server" AutoPostBack="true" OnClientClick="return ShowLoading()" CssClass="form-control form-control-sm"
                                        OnSelectedIndexChanged="ddlReport_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                               

                                 <div class="col-lg-2 form-group">
                                    <label id="lblMode" runat="server" visible ="false"  >
                                        Mode of Cancellation</label>
                                     <asp:DropDownList ID="ddlmode"  runat="server" Visible="false" CssClass="form-control">
                                                  
                                         </asp:DropDownList>
                                </div>
                                <div class="col-sm-1 pt-3 mt-2">
                                    <asp:LinkButton ID="lbtnsearch" runat="server" OnClick="lbtnSearch_Click" OnClientClick="return ShowLoading()"  visible ="false"  CssClass ="btn btn-warning btn-sm">
                                    <i class="fa fa-search" > Search</i></asp:LinkButton>
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
                            <div class="row">
                                <div class="col-lg-8"><b>
                                    <asp:Label ID="lblReportName" runat="server"></asp:Label></b>
                                    <br />
                                    <span style="font-size:10pt;"><b>Summary | </b> <asp:Label ID="lblsmry" runat="server" Text=""></asp:Label> </span> 
                                </div>
                                <div class="col-lg-4 text-right">
                                    <asp:LinkButton ID="lbtnPDF" runat="server" OnClick="lbtnPDF_Click" CssClass="btn btn-primary btn-sm"><i class="fa fa-file-pdf" style="font-size: 18px; margin-right: 8px;"></i> PDF</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnEXCEL" runat="server" OnClick="lbtnEXCEL_Click" CssClass="btn btn-danger btn-sm"><i class="fa fa-file-excel" style="font-size: 18px; margin-right: 8px;"></i> EXCEL</asp:LinkButton>
                                </div>
                            </div>  
                          
                        </div>

                     	<div class="card-body table-responsive" style="min-height: 320px;">
							<asp:GridView ID="gvPendingRefund" runat="server" GridLines="None" CssClass="table" AllowPaging="true"  OnPageIndexChanging="gvPendingRefund_PageIndexChanging"
								PageSize="15" AutoGenerateColumns="false">
								<Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PAYMENT GATEWAY">
										<ItemTemplate>
									   <%#Eval("gatewayname") %>
										</ItemTemplate>
									</asp:TemplateField>
                                    <asp:TemplateField HeaderText="REFERENCE NO.">
										<ItemTemplate>
									   <%#Eval("ref_no") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="BANK REFERENCE NO.">
										<ItemTemplate>
									   <%#Eval("bank_refno") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="TRANSACTION DATE">
										<ItemTemplate>
										 <%#Eval("trans_date") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="CANCELLATION DATE">
										<ItemTemplate>
										 <%#Eval("cancellation_date") %>
										</ItemTemplate>
									</asp:TemplateField>
                                    <asp:TemplateField HeaderText="TRANSACTION AMOUNT">
										<ItemTemplate>
										 <%#Eval("trans_amount") %>
										</ItemTemplate>
									</asp:TemplateField>
                                    <asp:TemplateField HeaderText="CANCELLATION AMOUNT">
										<ItemTemplate>
										 <%#Eval("cancellation_amount") %>
										</ItemTemplate>
									</asp:TemplateField>
                                    <asp:TemplateField HeaderText="TRANSACTION TYPE">
										<ItemTemplate>
										 <%#Eval("trans_type") %>
										</ItemTemplate>
									</asp:TemplateField>
                                	<asp:TemplateField HeaderText="PENDING SINCE">
										<ItemTemplate>
										 <%#Eval("pending_since") %>
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

    
</asp:Content>

















