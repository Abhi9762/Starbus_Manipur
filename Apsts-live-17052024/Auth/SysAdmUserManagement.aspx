<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="SysAdmUserManagement.aspx.cs" Inherits="Auth_SysUserManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<div class="header pb-7">
        <asp:HiddenField ID="hidtoken" runat="server" />
	</div>
	<div class="container-fluid mt--6">
		<div class="row">
			<div class="col-lg-3">
				<asp:Panel ID="LeftPanel" CssClass="scrollbar" runat="server" min-Height="800px">
					<div class="force-overflow">
						<div class="card" style="min-height: 300px;">
							<div class="card-header">
								<div class="card-title" style="margin-bottom: 0px;">
									<table width="100%">
										<tr>
											<td style="width: 43%;">
												<i style="font-size: 14px;" class="fa fa-lock"></i>&nbsp;<span style="font-weight: bold; font-size: 15px;">Locked User(s)</span>
											</td>
											<td style="width: 17%;">
												<asp:LinkButton ID="lbtnUnlockAllUser" OnClick="lbtnUnlockAllUser_Click" runat="server" CssClass="btn btn-primary btn-sm"
													ToolTip="Unlock All User" Style="padding: 3px 5px; font-size: 11px;"><i class="fa fa-unlock" style="color:White;"></i>&nbsp;Unlock All</asp:LinkButton>
											</td>
										</tr>
									</table>
								</div>
							</div>
							<div class="card-body text-center">
								<asp:GridView ID="gvLockedUser" runat="server" CssClass="table table-hover table-striped " GridLines="None"
									ShowHeader="false" BorderStyle="None" OnPageIndexChanging="gvLockedUser_PageIndexChanging" OnRowCommand="gvLockedUser_RowCommand" AllowPaging="true" PageSize="10"
									AutoGenerateColumns="false" HeaderStyle-Font-Size="10pt" HeaderStyle-ForeColor="Black" Font-Size="10pt"
									DataKeyNames="user_code">
									<Columns>
										<asp:TemplateField ItemStyle-HorizontalAlign="left" ItemStyle-Font-Bold="false">
											<ItemTemplate>
												<asp:Label ID="lbluser_code" runat="server" Text='<%# Eval("user_code") %>'></asp:Label>
												<br />
												<asp:Label ID="lblrole_name" runat="server" Text='<%# Eval("role_name") %>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField>
											<ItemStyle Width="25%" HorizontalAlign="Right" />
											<ItemTemplate>
												<asp:LinkButton ID="lbtnUnlockSingleUser" runat="server" Style="padding: 3px 5px; font-size: 11px;"
													CommandName="UnlockUser" CssClass="btn btn-primary btn-sm" ToolTip="Unlock User"><i class="fa fa-unlock" style="color:White;"></i>&nbsp;Unlock</asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateField>
									</Columns>
									<PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
								</asp:GridView>
								<asp:Label ID="lblLockedUserNodate" runat="server" Text="No user to unlock"
									Visible="true" Style="color: #d4cece; margin-top: 15px; font-size: 20px; font-weight: bold;"></asp:Label>
							</div>
						</div>
						<div class="card" style="min-height: 335px;">
							<div class="card-header">
								<div class="card-title" style="margin-bottom: 0px;">
									<table width="100%">
										<tr>
											<td colspan="2">
												<i style="font-size: 14px;" class="fa fa-user"></i>&nbsp;<span style="font-weight: bold; font-size: 15px;">Rolewise User(s)</span>
											</td>
										</tr>
										<tr style="padding-top: 10px;">
											<td>
												<div class="form-group" style="margin-bottom: 0px; margin-top: 0px;">
													<asp:DropDownList ID="ddlDesignationLP" OnSelectedIndexChanged="ddlDesignationLP_SelectedIndexChanged" ToolTip="Designation" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true">
														<asp:ListItem Text="Select Designation" Value="0"></asp:ListItem>
													</asp:DropDownList>
												</div>
											</td>
											<td style="width: 50%; padding-left: 5px;">
												<div class="form-group" style="margin-bottom: 0px; margin-top: 0px;">
													<asp:DropDownList ID="ddlRoleLP" OnSelectedIndexChanged="ddlRoleLP_SelectedIndexChanged" ToolTip="Role" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true">
														<asp:ListItem Text="All Role" Value="0"></asp:ListItem>
													</asp:DropDownList>
												</div>
											</td>
										</tr>
									</table>
								</div>
							</div>
							<div class="card-body">
								<asp:GridView ID="gvRolewiseUser" runat="server" CssClass="table text-left table-hover table-striped" Style="margin-bottom: 0rem; color: black;"
									GridLines="None" BorderStyle="None" ShowHeader="false" OnPageIndexChanging="gvRolewiseUser_PageIndexChanging" AllowPaging="true" Font-Size="10pt"
									AutoGenerateColumns="false" HeaderStyle-Font-Size="11pt" DataKeyNames="role_code" OnRowCommand="gvRolewiseUser_RowCommand">
									<Columns>

										<asp:BoundField DataField="role_name" />
										<asp:TemplateField>
											<ItemStyle Width="50%" HorizontalAlign="Right" />

											<ItemTemplate>
												<asp:LinkButton ID="lbtnRoleWiseUser" runat="server" Style="padding: 3px 5px; padding: 1px; font-weight: bold; font-size: 16px;"
													CommandName="RoleWiseAllUser" CssClass="text-info" ToolTip="User Count" Text='<%#Eval("rolecount")%>'></asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField>
											<ItemTemplate>
												<asp:LinkButton ID="lbtnRoleWiseUserPrint" runat="server" Style="padding: 3px 5px; padding: 1px; font-weight: bold; font-size: 16px;"
													CommandName="RoleWisePrint"
													CssClass="text-success" ToolTip="Print"><i class="fa fa-print" style="padding-top:2px;"></i></asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateField>
									</Columns>
									<PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
								</asp:GridView>

								<asp:Label ID="lblRolewiseUserNodata" runat="server" Text="No user Available"
									Visible="true" Style="color: #d4cece; margin-top: 15px; font-size: 20px; font-weight: bold;"></asp:Label>

								<h6 style="margin: 0px; padding: 0px; text-align: right; padding-top: 5px; border-top: 1px solid #efefef;">
									<asp:LinkButton ID="lbtnRoleWiseUserPrintAll" Visible="false"  runat="server" Style="padding: 3px 5px; font-weight: bold; font-size: 12px;"
										CssClass="btn btn-success btn-sm"
										ToolTip="Print All"><i class="fa fa-print"></i>&nbsp;Print All</asp:LinkButton>
								</h6>

							</div>
						</div>
					</div>
				</asp:Panel>
			</div>
			<div class="col-lg-9">
				<asp:Panel ID="pnluserlist" runat="server" Visible="true">
					<div class="card" style="min-height: 600px">
						<div class="card-header">
							<div class="row m-0 text-left">
								<div class="col-lg-2">
									<div class="form-group" style="margin-bottom: 0px; margin-top: 0px;">
										<label class="form-control-label">Office Level</label>

										<asp:DropDownList ID="ddlOfficeLevel" OnSelectedIndexChanged="ddlOfficeLevel_SelectedIndexChanged" runat="server" ToolTip="Office Level" CssClass="form-control form-control-sm" AutoPostBack="true">
											<asp:ListItem Text="select" Value="0"></asp:ListItem>
										</asp:DropDownList>
									</div>
								</div>
								<div class="col-lg-2">
									<div class="form-group" style="margin-bottom: 0px; margin-top: 0px;">
										<label class="form-control-label">Office</label>
										<asp:DropDownList ID="ddlOffice" runat="server" ToolTip="Office" CssClass="form-control form-control-sm">
											<asp:ListItem Text="select" Value="0"></asp:ListItem>
										</asp:DropDownList>
									</div>
								</div>
								<div class="col-lg-2">
									<div class="form-group" style="margin-bottom: 0px; margin-top: 0px;">
										<label class="form-control-label">Designation</label>
										<asp:DropDownList ID="ddlDesignation" runat="server" ToolTip="Designation" CssClass="form-control form-control-sm">
											<asp:ListItem Text="All" Value="0"></asp:ListItem>
										</asp:DropDownList>
									</div>
								</div>
								<div class="col-lg-2">
									<div class="form-group" style="margin-bottom: 0px; margin-top: 0px;">
										<label class="form-control-label">
											Role</label>
										<asp:DropDownList ID="ddlRole" runat="server" ToolTip="Role" CssClass="form-control form-control-sm">
											<asp:ListItem Text="All" Value="0"></asp:ListItem>
										</asp:DropDownList>
									</div>
								</div>
								<div class="col-lg-2">
									<div class="form-group" style="margin-bottom: 0px; margin-top: 0px;">
										<label class="form-control-label">
											Search</label>
										<asp:TextBox ID="tbSearchEmployee" runat="server" ToolTip="Search" placeholder="Enter Employee Code" CssClass="form-control form-control-sm" MaxLength="50">
										</asp:TextBox>
									</div>
								</div>
								<div class="col-lg-2">
									<div class="btn-group mt-4" style="margin-bottom: 0px;">
										<asp:LinkButton ID="lbtnSearchEmployee" OnClick="lbtnSearchEmployee_Click" runat="server" CssClass="btn btn-sm btn-primary" ToolTip="Search"><i class="fa fa-search"></i></asp:LinkButton>
										&nbsp;<asp:LinkButton ID="lbtnLoadAllEmployee" OnClick="lbtnLoadAllEmployee_Click" runat="server" CssClass="btn btn-sm btn-warning" ToolTip="Reset Filters"><i class="fa fa-undo"></i></asp:LinkButton>
										&nbsp;<asp:LinkButton ID="lbtnDownloadExcel" runat="server" OnClick="lbtnDownloadExcel_Click" CssClass="btn btn-sm btn-success" ToolTip="Download"><i class="fa fa-download"></i></asp:LinkButton>
									</div>
								</div>
							</div>
						</div>
						<div class="card-body">
							<div class="col-lg-12 float-right" style="text-align: right; padding-bottom: 20px; font-weight: bold;">

								<asp:Label runat="server" ID="lblTotEmployees" Text="" Style="margin-bottom: 0px;"></asp:Label>
							</div>
							<asp:GridView ID="gvEmployee" runat="server" CssClass="table table-hover table-striped " GridLines="None"
								ShowHeader="true" BorderStyle="None" OnPageIndexChanging="gvEmployee_PageIndexChanging" OnRowCommand="gvEmployee_RowCommand" AllowPaging="true" PageSize="10"
								AutoGenerateColumns="false" OnRowDataBound="gvEmployee_RowDataBound" HeaderStyle-Font-Size="10pt" HeaderStyle-ForeColor="Black" Font-Size="10pt" DataKeyNames="e_code,e_fname,e_designation_name,e_mobile_number,e_ofclvlname,e_office_name, e_role,e_status,ofcid,attachedofc,rolecode,unit_office,e_empndesignation,e_empname,ofclvlid,unitofficename">
								<Columns>

									<asp:BoundField DataField="e_code" HeaderStyle-Font-Bold="true" HeaderText="Employee Code" />
									<asp:TemplateField HeaderText="Name/Designation" HeaderStyle-Font-Bold="true">
										<ItemTemplate>
                                            <asp:Label runat="server" ID="lbblstatus" Visible="false" Text='<%#Eval("e_status") %>'></asp:Label>
											<asp:Label ID="lblefname" runat="server" Text='<%#Eval("e_fname") %>' />
											(<asp:Label ID="lbledesignation_name" runat="server" Text='<%#Eval("e_designation_name") %>' />)<br />
											<b><i class="fa fa-mobile-phone"></i>&nbsp<asp:Label ID="lblemobilenumber" runat="server" Text='<%#Eval("e_mobile_number") %>' /></b>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Office" HeaderStyle-Font-Bold="true">
										<ItemTemplate>
											<asp:Label ID="lbleofclvlname" runat="server" Text='<%#Eval("e_ofclvlname") %>' />
											(<asp:Label ID="lbleofficename" runat="server" Text='<%#Eval("e_office_name") %>' />)
                                       
										</ItemTemplate>
									</asp:TemplateField>
									<asp:BoundField DataField="e_role" HeaderStyle-Font-Bold="true" HeaderText="Current Role" />
									<asp:TemplateField ItemStyle-HorizontalAlign="left" ItemStyle-Font-Bold="false" HeaderText="ACTION" HeaderStyle-Font-Bold="true">
										<ItemTemplate>
											<div>
												<asp:LinkButton ID="lbtnActiveYN" runat="server" CommandName="activeyn" CssClass="btn btn-danger btn-sm"
													ToolTip="Change User Status"> <i class="fa fa-toggle-off"></i></asp:LinkButton>
												<asp:LinkButton ID="lbtnChangePasswd" runat="server" ToolTip="Change Password"
													CommandName="changepasswd" CssClass="btn btn-warning btn-sm"><i class="fa fa-key"></i></asp:LinkButton>
												<asp:LinkButton ID="lbtnAssignRole" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Assign New Role"
													CommandName="assignrole" CssClass="btn btn-primary btn-sm"><i class="fa fa-tasks"></i></asp:LinkButton>
											</div>
										</ItemTemplate>
									</asp:TemplateField>
								</Columns>
								<PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
							</asp:GridView>
							<asp:Label ID="lblNoEmpData" CssClass="p-5 form-control text-capitalize text-center" Visible="false" runat="server" Font-Size="medium" Font-Bold="true">
                                    Sorry! No employee found. Search employee again
							</asp:Label>

						</div>
					</div>
				</asp:Panel>
			
			</div>
		</div>

		<cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
			CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
		</cc1:ModalPopupExtender>
		<asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed; display: none;">
			<div class="card" style="min-width: 350px;">
				<div class="card-header">
					<h4 class="card-title">Please Confirm
					</h4>
				</div>
				<div class="card-body" style="min-height: 100px;">
					<asp:Label ID="lblConfirmation" runat="server"></asp:Label>
					<div style="width: 100%; margin-top: 20px; text-align: right;">
						<asp:LinkButton ID="lbtnYesConfirmation" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbtnYesConfirmation_Click"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
						<asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-3"> <i class="fa fa-times"></i> No </asp:LinkButton>
					</div>
				</div>
			</div>
			<div style="visibility: hidden;">
				<asp:Button ID="Button4" runat="server" Text="" />
			</div>
		</asp:Panel>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpRoleWiseUserLP" runat="server" PopupControlID="pnlRoleWiseUserLP"
                CancelControlID="Button8" TargetControlID="Button9" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlRoleWiseUserLP" runat="server" Style="position: fixed; margin-left: 4%; top: 1%; display: none;"
                class="modal-dialog">
                <div class="modal-content" style="min-width: 600px;">
                    <div class="modal-header " style="font-size: 15pt;">
                        <div class="row w-100">
                            <div class="col-lg-8">
                                <h4 class="modal-title" style="color: gray;">
                                    <asp:Label ID="Label8" runat="server" Text="Rolewise User Detail"></asp:Label></h4>
                            </div>
                            <div class="col-lg-4 text-right">
                                <asp:LinkButton ID="LinkButton3" runat="server"><i class="fa fa-times-circle" style="color:Red; font-size:20px;"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="modal-body" style="font-size: 11pt">
                        <asp:GridView ID="gvUserByRoleLP" runat="server" CssClass="table text-left table-striped" GridLines="None"
                            BorderStyle="None" ShowHeader="false" AllowPaging="true" PageSize="10" PagerSettings-Mode="NextPreviousFirstLast"
                            PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-NextPageText="Next"
                            PagerSettings-PreviousPageText="Previous" AutoGenerateColumns="false" HeaderStyle-Font-Size="10pt" Font-Size="10pt"
                            DataKeyNames="e_rolecode, e_rolename">
                            <Columns>
                               
                                <asp:BoundField DataField="emp_name" HeaderText="Name" />
                                <asp:BoundField DataField="designationname" HeaderText="Designation" />
                                <asp:BoundField DataField="e_rolename" HeaderText="Role" />
                                <asp:BoundField DataField="code" HeaderText="Employee Code" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div style="visibility: hidden; height: 0px;">
                    <asp:Button ID="Button8" runat="server" Text="" />
                    <asp:Button ID="Button9" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

        <div class="row">
            <cc1:ModalPopupExtender ID="mpAssignRole" runat="server" PopupControlID="pnlmpAssignRole"
                CancelControlID="Button6" TargetControlID="Button7" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlmpAssignRole" runat="server" Style="position: fixed; margin-left: 4%; top: 1%; display: none;"
                class="modal-dialog modal-lg">

                <div class="modal-content" style="width: 800px">
                    <div class="modal-header" style="font-size: 10pt; padding: 5px;">
                        <div class="modal-title" style="color: gray; font-size: 12pt; width: 100%">
                            <div class="row">
                                <div class="col-lg-8 pl-3">
                                    <b>
                                        <asp:Label runat="server" ID="lblAssignRoleName_Designation"></asp:Label></b>
                                </div>
                                <div class="col-lg-4">
                                    <asp:LinkButton ID="lnkAssignRoleCancel1" runat="server" ToolTip="Close" Style="float: right; padding: 0px;"> <i class="fa fa-times"></i>  </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-body" style="font-size: 12pt">
                        <div class="text-center">
                            <asp:Label runat="server" ID="lblpostingnotice" Visible="false"></asp:Label><br />
                            <asp:Label runat="server" ID="lblCurrRole"></asp:Label>
                            <asp:HiddenField runat="server" ID="hdn_currRole" />
                            <asp:Label runat="server" Visible="false"  ID="lblmsg" Style="color: red; font-size: 13pt; font-weight: bold;"></asp:Label>
                        </div>
                        <asp:Panel ID="pnlassign" runat="server" Visible="false">
                            <hr />
                            <div class="row">
                                <div class="col-lg-6" style="border-right: 1px solid #e6e6e6;">
                                    <h4>Assign New Role</h4>
                                     <asp:DropDownList ID="ddlassignedRole" OnSelectedIndexChanged="ddlassignedRole_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <div class="row mt-3">
                                        <div class="col-lg-12 text-center">
                                            <span style="font-size: 14pt;">Do you want to change role?</span><br />
                                            <br />
                                            <asp:LinkButton ID="lnkAssignRole" OnClick="lnkAssignRole_Click" runat="server" class="btn btn-success"><i class="fa fa-check"></i> &nbsp;Yes</asp:LinkButton>
                                            <asp:LinkButton ID="lnkAssignRoleCancel" runat="server" class="btn btn-danger"><i class="fa fa-times"></i> &nbsp;No</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <h4>Other Attached Employees</h4>
                                    <asp:GridView ID="grdassignedroleusr" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="table" ShowHeader="true" Font-Size="10pt" Visible="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Employee">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblemName" runat="server" Text=''><%# Eval("e_name") %></asp:Label>(<asp:Label ID="lblemusercode" runat="server" Text='<%# Eval("empcode") %>'></asp:Label>)<br />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attached Office">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblemofc" runat="server" Text=''><%# Eval("officename") + " (" + Eval("ofc_id") + ")" %></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Panel ID="pnlNorecord" runat="server" Width="100%" Visible="true">
                                        <div class="col-md-12" style="text-align: center;">
                                            <div class="col-md-12 busListBox pt-4" style="color: #e3e3e3; font-size: 10pt; font-weight: bold;">
                                                No Other Employee Attached in this office with selected Roles
                                                <br />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>


                        </asp:Panel>
                    </div>
                </div>

                <div style="visibility: hidden; height: 0px;">
                    <asp:Button ID="Button6" runat="server" Text="" />
                    <asp:Button ID="Button7" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>



	</div>
</asp:Content>

