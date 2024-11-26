<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="SysAdmUserMapping.aspx.cs" Inherits="Auth_SysAdmUserMapping" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  

    <script type="text/javascript">
        $(document).ready(function () {
            $('#grdemployee').DataTable(
                 {
                     bLengthChange: true,
                     //lengthMenu: [[10, 20, -1], [10, 20, "All"]],
                     bFilter: true,
                     bSort: true,
                     bPaginate: true
                     //,dom: 'Bfrtip',
                     //buttons: ['pdf', 'excel', 'csv']
                 });
        });
    </script>
    <style>
        table.dataTable tbody th, table.dataTable tbody td {
            padding: 7px 10px 8px 13px;
            font-size: 9pt;
        }

        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="container-fluid" style="padding-top: 27px; padding-bottom: 30px;">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="row m-0">
                        <div class="col-4 border-right">
                            <div class="card-body">
                                  <h4 class="mb-1">
                                        <asp:Label ID="lblSummary" runat="server" CssClass="text-capitalize">Summary as on Date 01/12/2021 04:00pm</asp:Label></h4>
                                <div class="row">                                  
                                    <div class="col-md-4 border-right">
                                       <span class="text-muted">Total Employee </span><br />
                                        <asp:Label ID="lbltotemp" runat="server" Text="0"></asp:Label>
                                    </div>
                                     <div class="col-md-4 border-right">
                                             <span class="text-muted">Total Module</span><br />
                                        <asp:Label ID="lbltotmodule" runat="server" Text="0"></asp:Label>
                                    </div>
                                   <div class="col-md-4">
                                         <span class="text-muted"> Assigned Module</span><br />
                                        <asp:Label ID="lblassignmodule" runat="server" Text="0"></asp:Label>
                                       </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div>
                                    <h4 class="mb-1">Generate Report</h4>
                                </div>
                                <div class="row">
                                   <div class="col">
                                        <asp:LinkButton ID="lbtnmoduleassignment" OnClick="lbtnmoduleassignment_Click" data-toggle="tooltip" data-placement="bottom" title="Download" runat="server" CssClass="btn btn-success text-white">
                                            <i class="fa fa-download"></i> Module Assignment
                                        </asp:LinkButton>
                                   </div>
                                    <div class="col">
                                        <asp:LinkButton ID="lbtnmodulelist" OnClick="lbtnmodulelist_Click" data-toggle="tooltip" data-placement="bottom" title="Download" runat="server" CssClass="btn btn-success text-white">
                                            <i class="fa fa-download"></i> Module List
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="col-4">
                            <div class="card-body">
                                <div class="row mr-0">
                                    <div class="col">
                                        <div>
                                            <h4 class="mb-1">Instructions</h4>
                                        </div>
                                        <ul class="data-list" data-autoscroll>
                                            <li>
                                                <asp:Label runat="server" CssClass="form-control-label"> Before module assignment please check the current module assignment from module assignment report.
                                                   <%-- Download the report before assignment to module any employee, Please check module assignment report.--%></asp:Label><br />
                                            </li>
                                           <%-- <li>
                                                <asp:Label runat="server" CssClass="form-control-label"> Once a service is blocked, that service cannot be unblocked.</asp:Label><br />
                                            </li>
                                            <li>
                                                <asp:Label runat="server" CssClass="form-control-label"> If service tickets are booked online, then their cancellation will be auto.</asp:Label><br />
                                            </li>
                                            <li>
                                                <asp:Label runat="server" CssClass="form-control-label"> If there are booking tickets from the counter or agent, then they will be cancelled only, their refund will be from the respective counter and agent.</asp:Label>
                                            </li>--%>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="card card-stats">
                    <div class="card-header">
                        <asp:Label ID="lblempofc" runat="server" Visible="true"></asp:Label>
                        <asp:LinkButton ID="lbtnback" runat="server" CssClass="btn btn-danger" OnClick="lbtnback_Click" Visible="false" Style="float: right;"> <i class="fa fa-backward"> </i> Back To Employee List</asp:LinkButton>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="grdemployee" runat="server" AutoGenerateColumns="False" GridLines="None"
                            ClientIDMode="Static" AllowPaging="false" class="table table-bordered table-hover table-striped" OnRowCommand="grdemployee_RowCommand" DataKeyNames="emp_code,e_officeid,e_ofclvlid">
                          
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEMPNAME" runat="server" Text='<%# Eval("emp_name") %>'></asp:Label><br />
                                        (<asp:Label ID="lblEMPCODE" runat="server" Text='<%# Eval("emp_code") %>'></asp:Label>)
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date of Birth/Gender">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEMPDOB" runat="server" Text='<%# Eval("emp_dob") %>'></asp:Label>, 
                                        <asp:Label ID="lblGender" runat="server" Text='<%# Eval("e_gender") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDESIGNATIONNAME" runat="server" Text='<%# Eval("e_designationname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Office Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEMPFATHERNAME" runat="server" Text='<%# Eval("e_officename") %>'></asp:Label><br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Posting Office">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpostingofc" runat="server" Text='<%# Eval("e_postingofc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile/Email">
                                    <ItemTemplate>
                                        <i class="fa fa-phone"></i>
                                        <asp:Label ID="lblEMPMOBILENUMBER" runat="server" Text='<%# Eval("e_mobile") %>'></asp:Label><br />
                                        <i class="fa fa-envelope"></i>
                                        <asp:Label ID="lblEMPEMAIL" runat="server" Text='<%# Eval("e_email") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEMPTYPE" runat="server" Text='<%# Eval("e_emp_type") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--    <asp:TemplateField HeaderText="Emergency No.">
                                <ItemTemplate>
                                    <i class="fa fa-phone"></i>
                                    <asp:Label ID="lblEMERGENCYNO" runat="server" Text='<%# Eval("EMERGENCYNO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Module Assigned">
                                    <ItemTemplate>
                                        <asp:Label ID="lblModuleAssigned" runat="server" Text='<%# Eval("module_assigned") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnview" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                            Visible="true" runat="server" CssClass="btn btn-warning" Style="border-radius: 4px;"
                                            ToolTip="View Transaction Details"> <i class="fa fa-forward"></i> Map</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        
                                <div class="col-12" style="text-align: center;" id="dvnoemp" runat ="server">
                                    <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-bottom: 50px; font-size: 40px; font-weight: bold;">
                                        No Employee available for module assignment<br />
                                    </div>
                                </div>
                        <asp:Panel ID="pnldetails" runat="server" Visible="false">

                            <div class="card-body" style="padding: 5px 20px;">
                                <div class="row mt-1">
                                    <div class="col-lg-4">
                                        <span class="text-muted">Employee Name </span>
                                        <asp:Label ID="lblempname" runat="server" Text="NA"></asp:Label><br />
                                        <span class="text-muted">Date Of Birth </span>
                                        <asp:Label ID="lblempdob" runat="server" Text="NA"></asp:Label><br />
                                        <span class="text-muted">Gender </span>
                                        <asp:Label ID="lblempgender" runat="server" Text="NA"></asp:Label><br />
                                    </div>
                                    <div class="col-lg-4">
                                        <span class="text-muted">Office </span>
                                        <asp:Label ID="lblempoffice" runat="server" Text="NA"></asp:Label><br />
                                        <span class="text-muted">Posting Office </span>
                                        <asp:Label ID="lblemppostingofc" runat="server" Text="NA"></asp:Label><br />
                                        <span class="text-muted">Designation </span>
                                        <asp:Label ID="lblempdesignation" runat="server" Text="NA"></asp:Label><br />
                                    </div>
                                    <div class="col-lg-4">
                                        <span class="text-muted">Email </span>
                                        <asp:Label ID="lblempemailid" runat="server" Text="NA"></asp:Label><br />
                                        <span class="text-muted">Mobile  </span>
                                        <asp:Label ID="lblempmobile" runat="server" Text="NA"></asp:Label><br />
                                        <span class="text-muted">Employee Type  </span>
                                        <asp:Label ID="lblemptype" runat="server" Text="NA"></asp:Label><br />
                                    </div>
                                </div>

                                <hr />
                                <div class="row mt-3">
                                    <div class="col-lg-6" style="border-right: 1px solid">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <span style="font-size: 15pt; font-weight: bold; color: gray">Available for Assignment</span>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="grdavailableforassign" runat="server" AutoGenerateColumns="False"
                                                    GridLines="None" Font-Size="10pt" Visible="true" CssClass="table table-striped"
                                                    DataKeyNames="val_moduleid,val_modulename,val_moduleurl" OnRowCommand="grdavailableforassign_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.N.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Module Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMODULENAME" runat="server" Text='<%# Eval("val_modulename") %>'></asp:Label>
                                                                (<asp:Label ID="lblMODULEURL" runat="server" Text='<%# Eval("val_moduleurl") %>'></asp:Label>)
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnassign" runat="server" OnClientClick="return ShowLoading()" CssClass="btn btn-success btn-sm" CommandName="Assign"
                                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Click here to Assign module"> <i class="fa fa-forward" title="Click here to Assign module"></i> Assign</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Panel ID="pnlavailableforassignNoRecord" runat="server" Width="100%" Visible="false">
                                                    <center>
                                                    <i class="fa fa-window-close" style="font-size: 120px; color: #e4e4e6;"></i>
                                                    <p style="color: #e4e4e6; font-size: 25px; font-weight: 500;">
                                                        Module not available for assignment
                                                    </p>
                                                </center>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <span style="font-size: 15pt; font-weight: bold; color: gray">Assigned Module</span>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="grdassignedmodule" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                    Font-Size="10pt" Visible="true" CssClass="table table-striped"  DataKeyNames="val_moduleid,val_modulename,val_moduleurl" OnRowCommand="grdassignedmodule_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.N.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Module Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMODULENAME" runat="server" Text='<%# Eval("val_modulename") %>'></asp:Label><br />
                                                                (<asp:Label ID="lblMODULEURL" runat="server" Text='<%# Eval("val_moduleurl") %>'></asp:Label>)
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnassign" runat="server" OnClientClick="return ShowLoading()" CssClass="btn btn-warning btn-sm" CommandName="Remove"
                                                                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Click here to Remove Assigned Module"> <i class="fa fa-backward" title ="Click here to Remove Assigned Module"></i> Remove</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Panel ID="pnlassignedmoduleNoRecord" runat="server" Width="100%" Visible="false">
                                                    <center>
                                                    <i class="fa fa-window-close" style="font-size: 120px; color: #e4e4e6;"></i>
                                                    <p style="color: #e4e4e6; font-size: 25px; font-weight: 500;">
                                                        Module not assign
                                                    </p>
                                                </center>
                                                </asp:Panel>
                                            </div>
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
                        <asp:LinkButton ID="lbtnNoConfirmation" runat="server" OnClick="lbtnNoConfirmation_Click" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button4" runat="server" Text="" />
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
                    <h4 class="card-title text-left mb-0">Confirm
                    </h4>
                </div>
                <div class="card-body text-left pt-2" style="min-height: 100px;">
                    <asp:Label ID="lblsuccessmsg" runat="server" Text="Do you want to Update ?"></asp:Label>
                    <div style="width: 100%; margin-top: 20px; text-align: right;">
                        <asp:LinkButton ID="lbtnsuccessclose1" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button6" runat="server" Text="" />
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



