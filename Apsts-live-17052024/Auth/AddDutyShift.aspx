<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="AddDutyShift.aspx.cs" Inherits="Auth_AddDutyShift" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <script type="text/javascript">
        $(document).ready(function () {
            $('#grdemployee').DataTable(
                 {
                     bLengthChange: true,
                     lengthMenu: [[14, 20, -1], [14, 20, "All"]],
                     bFilter: true,
                     bSort: true,
                     bPaginate: true
                     //,dom: 'Bfrtip',
                     //buttons: ['pdf', 'excel', 'csv']
                 });
        });
    </script>
    <style>
        .table td, .table th {
            padding: 3px 5px;
            vertical-align: top;
            border-top: none;
        }

        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .btn-label {
            font-size: 12pt;
            padding: 0px 5px;
            color: black;
            font-weight: bold;
        }

        hr {
            margin-top: 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid" style="padding-top: 20px">

        <div class="row">
            <div class="col-lg-3 p-0" style="min-height: 500px">
                <div class="row ml-0">
                    <div class="col-md-4 pr-1">
                        <div class="card shadow" style="min-height: 10vh; margin-bottom: 10px">
                            <div class="card-body" style="padding: 10px; text-align: center; height: 85px">
                                <div class="col-lg-12">
                                    <asp:LinkButton ID="lbtntotalemp" runat="server" Text="0" Style="color: green;" Font-Underline="true" Font-Size="16pt" Font-Bold="true" CommandName="O"></asp:LinkButton>
                                    <br />
                                    <label for="ddhead" style="line-height: 18px;">
                                        Total
                                <br />
                                        <br />
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                     <div class="col-md-4 pr-1">
                        <div class="card shadow" style="min-height: 10vh; margin-bottom: 10px">
                            <div class="card-body" style="padding: 10px; text-align: center; height: 85px">
                                <div class="col-lg-12">
                                    <asp:LinkButton ID="lbtnShifted" runat="server" Text="0" Style="color: green;" Font-Underline="true" Font-Size="16pt" Font-Bold="true" CommandName="O"></asp:LinkButton>
                                    <br />
                                    <label for="ddhead" style="line-height: 18px;">
                                        Shifted 
                                <br />
                                        <br />
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 pl-1">
                        <div class="card shadow" style="min-height: 10vh; margin-bottom: 10px">
                            <div class="card-body" style="padding: 10px; text-align: center; height: 85px">

                                <div class="col-lg-12">
                                    <asp:LinkButton ID="lbtnpendingshift" runat="server" Text="0" Style="color: red;" Font-Underline="true" Font-Size="16pt" Font-Bold="true" CommandName="N"></asp:LinkButton>
                                    <br />
                                    <label for="ddhead" style="line-height: 18px;">
                                        Not Marked
                                <br />
                                        <br />
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row ml-0">
                    <div class="col-md-12" style="">
                        <div class="card shadow" style="min-height: 10vh; margin-bottom: 10px">
                            <div class="card-body">
                                <table class="table">
                                    <tr>
                                        <td>SHIFT 1<br />
                                            (06 AM - 02 PM)
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lbtn1shift" runat="server" CssClass="btn-label" Text="0"></asp:LinkButton></td>
                                    </tr>
                                    <tr style ="border-top:1px solid;">
                                        <td colspan="2"> </td>
                                    </tr>
                                     <tr>
                                        <td>SHIFT 2<br />
                                            (02 PM - 10 PM)
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lbtn2shift" runat="server" CssClass="btn-label" Text="0"></asp:LinkButton></td>
                                    </tr>
                                      <tr style ="border-top:1px solid;">
                                        <td colspan="2"> </td>
                                    </tr>
                                    <tr>
                                        <td>SHIFT 3<br />
                                            (10 PM - 06 AM)
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lbtn3shift" runat="server" CssClass="btn-label" Text="0"></asp:LinkButton></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
           <div class="col-lg-9" style="min-height: 500px">
                <div class="card card-stats">
                    <div class="card-header">
                       <div class="row">
                             <div class="col-md-8">
                                 <asp:Label ID="lblmsg" runat ="server" Text ="Select Duty Before Proceding" ></asp:Label>
                                 </div>
                            <div class="col-md-2">
                                <span style="font-size: 12pt; font-weight: 600; text-align: right;">Year</span><br />
                                <asp:DropDownList ID="ddlYear" runat ="server" class="form-control" AutoPostBack ="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>                               
                            </div>
                            <div class="col-md-2">
                                <span style="font-size: 12pt; font-weight: 600; text-align: right;">Month</span><br />
                                 <asp:DropDownList ID="ddlMonth" runat ="server" class="form-control" AutoPostBack ="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"></asp:DropDownList> 
                            </div>
                           </div>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="grdemployee" runat="server" AutoGenerateColumns="False" GridLines="None" Font-Size="10pt"
                            ClientIDMode="Static"  class="table table-hover table-striped" OnRowDataBound="grdemployee_RowDataBound"
                            DataKeyNames="emp_name,emp_code_,designation_name_,emptype_,classname_,officeid_,ofclvlid_" OnRowCommand="grdemployee_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name/Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEMPNAME" runat="server" Text='<%# Eval("emp_name") %>'></asp:Label>
                                        (<asp:Label ID="lblEMPCODE" runat="server" Text='<%# Eval("emp_code_") %>'></asp:Label>)<br />
                                        <asp:Label ID="lblDESIGNATIONNAME" runat="server" Text='<%# Eval("designation_name_") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Posting Office">
                                    <ItemTemplate>
                                       <asp:Label ID="lblpostingofc" runat="server" Text='<%# Eval("posting_office") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile/Email">
                                    <ItemTemplate>
                                        <i class="fa fa-phone"></i>
                                        <asp:Label ID="lblEMPMOBILENUMBER" runat="server" Text='<%# Eval("mobile_number_") %>'></asp:Label><br />
                                        <i class="fa fa-envelope"></i>
                                        <asp:Label ID="lblEMPEMAIL" runat="server" Text='<%# Eval("empemail_") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Type/Class">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEMPTYPE" runat="server" Text='<%# Eval("emptype_") %>'></asp:Label>
                                        / <asp:Label ID="lblCLASS_NAME" runat="server" Text='<%# Eval("classname_") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Duty Shift">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlshifttype" runat ="server" CssClass="form-control form-control-sm">

                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="row mt-3 pt-2" style="border-top:1px solid #e6e6e6;">
                            <div class="col-md-12 text-right">
                                 <asp:LinkButton ID="lbtnSaveNProceed" runat="server" OnClick="lbtnSaveNProceed_Click" CssClass="btn btn-success"  OnClientClick ="return ShowLoading()"> <i class="fa fa-forward"></i> Save & Proceed</asp:LinkButton>
                            </div>
                        </div>
                        <asp:Label ID="lblNoEmp" CssClass="p-5" runat="server" Text="Sorry! No employee Available for Shift Allocation" Font-Bold="true" ForeColor="#b6b8b8" Style="font-size: 26pt;"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <cc1:ModalPopupExtender ID="mpShift" runat="server" PopupControlID="pnlShift"
                CancelControlID="lbtnCancel" TargetControlID="Button4" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlShift" runat="server" Style="position: fixed;">
                <div class="card">
                    <div class="card-header">
                        <h5 class="card-title text-left mb-0">Please Confirm Duty Shift
                             <asp:Button ID="lbtnCancel" runat="server" Text="Close" CssClass ="btn btn-danger" style="float:right;" />
                        </h5>
                    </div>
                    <div class="card-body text-left pt-2" style="height: 500px; overflow:auto; width:1000px;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" GridLines="None" Font-Size="10pt"
                            class="table table-hover table-striped">
                            
                        </asp:GridView>
                        
                    </div>
                    <div class="card-footer text-right">
                        <asp:LinkButton id="lbtnsave" runat ="server" OnClick="lbtnsave_Click" CssClass="btn btn-warning" > <i class="fa fa-floppy-o"></i> Confirm & Save</asp:LinkButton>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button4" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

        <div class="row">
            <cc1:ModalPopupExtender ID="mpError" runat="server" PopupControlID="pnlError" CancelControlID="Button1"
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
                    <asp:Button ID="Button1" runat="server" Text="" />
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
                        <asp:Label ID="lblsuccessmsg" runat="server" Text=""></asp:Label>
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
    </div>

 
</asp:Content>



