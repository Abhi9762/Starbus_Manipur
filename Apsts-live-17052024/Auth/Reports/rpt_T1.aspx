﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Reports/SysAdmReportmaster.master" AutoEventWireup="true" CodeFile="rpt_T1.aspx.cs" Inherits="Auth_Reports_rpt_T1" %>

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
     <script type="text/javascript">

         $(document).ready(function () {

             var currDate = new Date().getDate();
             var preDate = new Date(new Date().setDate(currDate - 1));
             var todayDate = new Date(new Date().setDate(currDate));

             $('[id*=txtbookingdate]').datepicker({
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
                        <div class="col-lg-3">
                            <div class="row">
                                <div class="col-lg-6 form-group">
                                    <label for="ddlReportType">
                                        Report Type</label>
                                    <asp:DropDownList ID="ddlReportType"  runat="server" AutoPostBack="true" OnClientClick="return ShowLoading()" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
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
                               
                           <div class="col-lg-3">
                            <div class="row">
                                <div class="col-lg-6 form-group" id="bookingdate" runat="server" visible="false">
                                    
                                  <label>Booking Date</label>
                             <div class="input-group date">
                                            <asp:TextBox ID="txtbookingdate" ToolTip="Enter Invoice Date" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="txtbookingdate" ValidChars="/" />
                                        </div>
                                </div>
                            
                                 <div class="col-lg-6 form-group">
                                    <label id="lblservicetype" runat="server" visible ="false"  >
                                       Service Type</label>
                                     <asp:DropDownList ID="ddlservicetype"  runat="server" Visible="false" CssClass="form-control">
                                                </asp:DropDownList>
                                </div>
                                </div>
                                </div>
                             



                         <div class="col-lg-3">
                            <div class="row">
                              
                            
                                 <div class="col-lg-6 form-group">
                                    <label id="lbldepot" runat="server" visible ="false"  >
                                       Depot</label>
                                     <asp:DropDownList ID="ddldepot"  runat="server" Visible="false" CssClass="form-control">
                                                </asp:DropDownList>
                                </div>

                                  <div class="col-lg-6 form-group">
                                    <label id="lblbookingmode" runat="server" visible ="false"  >
                                       Booking Mode</label>
                                     <asp:DropDownList ID="ddlbookingmode"  runat="server" Visible="false" CssClass="form-control">
                                               <asp:ListItem runat="server" Value="0" Text="All"></asp:ListItem>
                                        <asp:ListItem runat="server" Value="A" Text="Agent/CSC"></asp:ListItem>
                                        <asp:ListItem runat="server" Value="C" Text="Counter"></asp:ListItem>
                                        <asp:ListItem runat="server" Value="T" Text="Online(Web)"></asp:ListItem>
                                     <asp:ListItem runat="server" Value="M" Text="Online(App)"></asp:ListItem>
                                         </asp:DropDownList>
                                </div>
                                </div>
                                </div>
                                  <div class="col-lg-3">
                            <div class="row">

                                <div class="col-sm-6 pt-3 mt-2">
                                    <asp:LinkButton ID="lbtnsearch" runat="server"  visible ="false" Onclick="lbtnSearch_Click" OnClientClick="return ShowLoading()" CssClass ="btn btn-warning btn-sm">
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
							<asp:GridView ID="gvonlineBooking" runat="server" GridLines="None" CssClass="table" AllowPaging="true"  OnPageIndexChanging="gvonlineBooking_PageIndexChanging"
								PageSize="25" AutoGenerateColumns="false">
								<Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PNR">
										<ItemTemplate>
									   <%#Eval("val_pnr") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="JOURNEY DATE">
										<ItemTemplate>
									   <%#Eval("journey_date") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="NO. OF SEATS">
										<ItemTemplate>
										 <%#Eval("noofseats") %>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="BOOKED BY">
										<ItemTemplate>
										 <%#Eval("bookedby") %>
										</ItemTemplate>
									</asp:TemplateField>
                                    <asp:TemplateField HeaderText="PAYMENT MODE(PG/WALLET/CASH)">
										<ItemTemplate>
										 <%#Eval("paymentmode") %>
										</ItemTemplate>
									</asp:TemplateField>
                                    <asp:TemplateField HeaderText="FARE">
										<ItemTemplate>
										 <%#Eval("val_fare") %>
										</ItemTemplate>
									</asp:TemplateField>
                                    <asp:TemplateField HeaderText="RESERVATION">
										<ItemTemplate>
										 <%#Eval("val_reservation") %>
										</ItemTemplate>
									</asp:TemplateField>
                                    <asp:TemplateField HeaderText="CONCESSION">
										<ItemTemplate>
										 <%#Eval("val_concession") %>
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


















