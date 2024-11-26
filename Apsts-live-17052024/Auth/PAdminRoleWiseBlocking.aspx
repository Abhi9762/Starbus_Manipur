<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/PortalAdmmaster.master" AutoEventWireup="true" CodeFile="PAdminRoleWiseBlocking.aspx.cs" Inherits="Auth_PAdminRoleWiseBlocking" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">       
		$(document).ready(function () {

			var currDate = new Date().getDate();
            var preDate = new Date(new Date().setDate(currDate - 1));
			var todayDate = new Date(new Date().setDate(currDate));


            $('[id*=tbBlockFromDate]').datepicker({
                startDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            })
                .on('changeDate', function (selected) {
                    var minDate = new Date(selected.date.valueOf());
                    $('[id*=tbBlockToDate]').datepicker('setStartDate', minDate);
                });
            $('[id*=tbBlockToDate]').datepicker({
                startDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });

		});

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    	<div class="header pb-4">
	</div>
    <asp:HiddenField ID="HiddenField1" runat="server" />
     <div class="container-fluid mt-1">
                 <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row my-2">
            <div class="col-lg-6 col-md-6 order-xl-1">
                <div class="card" style="min-height: 400px">

                   <div class="card-header">
                        <div class="row m-0 align-items-center">
                              <div class="col-md-6 ">
                            <h3 class="mb-1">
                                 <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h2><span style="color: #ff5e00;">Please Note</span></h2></asp:Label>

                                    </h3>
                                    </div>
                             <div class="col-md-6 ">
                                 <h4 ">
                              <label class="form-control-label" style="font-size: small; color:red; float:right">All Marked * Fields are mandatory  </label>
                                 </h4>
                             </div>
                        </div>
                    </div>
                       <div class="col-md-12 col-lg-12">
                    
                        <div class="row mx-2 my-1">
                            <div class="col">
           
                               <asp:Label runat="server" CssClass="form-control-label" Font-Bold="false" Font-Size="Small"><br />  1. Block User Role for Particular Period. <br />
                                2.  Unblock Blocked User  <br />
                               3.  Blocked user can not access the portal between given block date period.
                              </asp:Label>

                                        </div>
                       
                               </div>
                        <br />
                    </div>

                     </div>

            </div>
            <div class="col-md-6 col-lg-6 order-xl-2">
                <div class="card" style="min-height: 400px">
                      <div class="card-header">

                        <div class="row m-0 align-items-center">
                            <div class="col-md-10 ">
                            <h3 class="mb-1">
                                 <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h2><span >Portal Access Control</span></h2></asp:Label>

                                    </h3>
                                    </div>
                             <div class="col-md-2 text-right ">
                                 <h4 >
                                     
                      <asp:LinkButton ID="lbtnViewInstruction" runat="server" OnClick="lbtnViewInstruction_Click" ToolTip="View Instructions"  CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i> </asp:LinkButton>
                        
                          <asp:LinkButton ID="lbtnViewHistory" runat="server" OnClick="lbtnViewHistory_Click" ToolTip="Click here to View Portal Access Control History" CssClass="btn btn bg-gradient-blue btn-sm text-white">
                                    <i class="fa fa-history"></i>
                        </asp:LinkButton>
                                 </h4>
                             </div>
                        </div>
                    </div>
                     <div class="row m-0 align-items-center px-4">
                         <div class="col-md-12 col-lg-12">
                   <br />
                              <div class="row">
                                                <div class="col-lg-4 text-right">
                                                   
                                           <asp:Label runat="server" CssClass="form-control-label"  Font-Bold="true">Role Type<span class="text-warning">*</span> </asp:Label>

                                                </div>
                                                <div class="col-lg-5">
                                                      <asp:DropDownList runat="server" ID="ddlUserRole" ToolTip="Role Type"
                                                CssClass="form-control form-control-sm" >
                                            </asp:DropDownList>

                                                </div>
                                            </div>
                                <div class="row mt-3">
                                                <div class="col-lg-4 text-right">
                                                <asp:Label runat="server" CssClass="form-control-label"  Font-Bold="true"> From Date<span class="text-warning">*</span> </asp:Label>

                                                </div>
                                                <div class="col-lg-5">
                                                                                                 <asp:TextBox ID="tbBlockFromDate" runat="server" autocomplete="off" CssClass="form-control form-control-sm" ToolTip="Select From Date" TabIndex="5"
                                                        Placeholder="DD/MM/YYYY" Style="font-size: 10pt;"></asp:TextBox>
                                    
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,Custom"
                                                        TargetControlID="tbBlockFromDate" ValidChars="/" />
                                                    
                                                  
                                                </div>
                                            </div>
                                 <div class="row mt-3">
                                                <div class="col-lg-4 text-right">
                                                    <asp:Label runat="server" CssClass="form-control-label"  Font-Bold="true"> To Date<span class="text-warning">*</span></asp:Label>
                                                </div>
                                                <div class="col-lg-5">
                                               <asp:TextBox ID="tbBlockToDate"  runat="server" autocomplete="off" CssClass="form-control form-control-sm" TabIndex="5"
                                                        Placeholder="DD/MM/YYYY" ToolTip="Select To Date" Style="font-size: 10pt;"></asp:TextBox>
                                     

                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers,Custom"
                                                        TargetControlID="tbBlockToDate" ValidChars="/" />
                                                </div>
                                            </div>
                                  <div class="row mt-4">
                                                <div class="col-lg-4">
                                                </div>
                                                <div class="col-lg-5 text-center">
                                                    <asp:LinkButton ID="lbtnUpdateRoleStatus" runat="server" OnClick="lbtnUpdateRoleStatus_Click" OnClientClick="return ShowLoading()" CommandArgument="U" class="btn btn-success"
                                                        Style="font-size: 10pt;" ToolTip="Click here to Save Role Portal Accessing "> <i class="fa fa-save" title ="Click here to Save Role Portal Accessing" ></i> Save</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnResetRole" runat="server" OnClick="lbtnResetRole_Click" ToolTip="Click here to Reset Role Portal Accessing" OnClientClick="return ShowLoading()" class="btn btn-danger" Style="font-size: 10pt;"> <i class="fa fa-undo" title="Click here to Reset Role Portal Accessing"></i> Reset</asp:LinkButton>
                                                </div>
                                            </div>

                         </div>
                         <div class="col-lg-12 mt-3">
                                    <asp:GridView ID="gvBlockedRoles" runat="server" OnPageIndexChanging="gvBlockedRoles_PageIndexChanging" AutoGenerateColumns="False" 
                                        GridLines="None" OnRowCommand="gvBlockedRoles_RowCommand"  AllowPaging="true" PageSize="3" CssClass="table table-striped"
                                        DataKeyNames="roleid,role_name,blockfromdt,blocktodt">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Role">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRole" runat="server" Text='<%# Eval("role_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Block From Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBLOCKFROMDT" runat="server" Text='<%# Eval("blockfromdt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Block To Date">
                                                <ItemTemplate>
                                                      <asp:Label ID="lblBLOCKTODATE" runat="server" Text='<%# Eval("blocktodt") %>'></asp:Label>
                                                     </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnunlock" runat="server"  OnClientClick="return ShowLoading()" CssClass="btn btn-sm btn-danger" CommandName="RoleUpdation"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="height: 25px; padding-top: 5px; font-size: 10pt; border-radius: 4px;"
                                                        ToolTip="Unblock"><i class="fa fa-unlock-alt"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
										<PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                    </asp:GridView>
                                    <asp:Panel ID="noBlockedRoles" runat="server" Width="100%" Visible="false">
                                        <center>
                                            <i class="fa fa-times-circle-o" style="font-size: 120px; color: #e4e4e6;"></i>
                                            <p style="color: #e4e4e6; font-size: 25px; font-weight: 500;">
                                                No Role is Blocked
                                            </p>
                                        </center>
                                    </asp:Panel>
                                </div>
                          </div>
                </div>
            </div>
     
             </div>
      </div>
    <div class="row">
            <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
                CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Please Confirm
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnYesConfirmation" runat="server" OnClick="lbtnYesConfirmation_Click" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                            <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button4" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
    <div class="row">
            <cc1:ModalPopupExtender ID="mpRoleBlockHistory" runat="server" PopupControlID="PanelRoleBlockHistory"
                TargetControlID="Button8" CancelControlID="LinkButton4" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="PanelRoleBlockHistory" runat="server" Style="position: fixed; top: 30.5px; min-width: 400px; max-width: 800px;">
                <div class="card">
                    <div class="card-header">
                        <strong class="card-title">Role Wise Blocking History</strong>
                    </div>
                    <div class="card-body" style="padding: 15px !important;">
                        
                            <asp:GridView ID="gvRoleBlockHistory" OnPageIndexChanging="gvRoleBlockHistory_PageIndexChanging" runat="server" AutoGenerateColumns="False"
                                GridLines="None" AllowSorting="true"  AllowPaging="true" PageSize="3" CssClass="table table-striped"
                                DataKeyNames="roleid,blockfromdt,blocktodt,role_name,actiondate,updateby">
                                <Columns>
                                    <asp:TemplateField HeaderText="Role">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrole" runat="server" Text='<%# Eval("role_name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfromdt" runat="server" Text='<%# Eval("blockfromdt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltodate" runat="server" Text='<%# Eval("blocktodt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Updation Date/Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUPDATIONDATE" runat="server" Text='<%# Eval("actiondate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Updated By">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUPDATEDBY" runat="server" Text='<%# Eval("updateby") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                            </asp:GridView>
                        <asp:Panel ID="pnlNoRecord" runat="server" Width="100%" Visible="true">
									<div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
										<div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
											No Record Available<br />
										</div>
									</div>
								</asp:Panel>
                    </div>
                    <div class="card-footer">
                        <asp:LinkButton ID="LinkButton4" runat="server" CssClass="btn btn-success" Style="font-size: 11pt; float: right; border-radius: 4px;">OK</asp:LinkButton>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button8" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div> 
     <div class="row">
            <cc1:ModalPopupExtender ID="mpsuccess" runat="server" PopupControlID="pnlsuccess"
                CancelControlID="lbtnsuccessclose1" TargetControlID="Button6" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlsuccess" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Information
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblsuccessmsg" runat="server" Text="Do you want to Update ?"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnsuccessclose1" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> Close </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button6" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

</asp:Content>

