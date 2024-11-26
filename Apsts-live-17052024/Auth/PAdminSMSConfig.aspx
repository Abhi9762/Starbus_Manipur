<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/PortalAdmmaster.master" AutoEventWireup="true" CodeFile="PAdminSMSConfig.aspx.cs" Inherits="Auth_PAdminSMSConfig" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(function () {           

            //Enable Disable TextBoxes in a Row when the Row CheckBox is checked.
            $("[id*=cbSMS]").bind("click", function () {

                //Find and reference the GridView.
                var grid = $(this).closest("table");

                

                //If the CheckBox is Checked then enable the TextBoxes in thr Row.
                if (!$(this).is(":checked")) {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#FFF" });
                    $("input[type=text]", td).attr("disabled", "disabled");
                } else {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#D8EBF2" });
                    $("input[type=text]", td).removeAttr("disabled");
                }

                //Enable Header Row CheckBox if all the Row CheckBoxes are checked and vice versa.
                if ($("[id*=cbSMS]", grid).length == $("[id*=cbSMS]:checked", grid).length) {
                    chkHeader.attr("checked", "checked");
                } else {
                    chkHeader.removeAttr("checked");
                }
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidtoken" runat="server" />
    <div class="container-fluid mt-1 pb-5">

        <div class="row my-3">
            <div class="col-md-4 col-lg-4 order-xl-1">
                <div class="card" style="min-height: 500px">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-md-11 col-lg-11">
                                <asp:Label runat="server" CssClass="form-control-label"><h2>SMS Configuration</h2></asp:Label>
                            </div>
                            <div class="col-lg-1 col-md-1 float-right m-0 p-0">
                                <asp:LinkButton ID="lbtnHelpTemplate" runat="server" OnClick="lbtnHelpTemplate_Click" ToolTip="View Instructions" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                </asp:LinkButton>
                              
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="pnlSMS" runat="server">
                        <div class="row align-items-center py-4 px-2">
                            <div class="col-md-12 col-lg-12 mt-2">
                                <div class="row pb-3">
                                    <div class="col-md-4 col-lg-4 text-left">
                                        <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Sender ID<span class="text-warning">*</span></asp:Label>

                                    </div>

                                    <div class="col-md-7 col-lg-7 text-left">

                                        <asp:TextBox Autocomplete="off" ID="tbSenderID" runat="server" CssClass="form-control form-control-sm" ToolTip="Enter Sender ID" MaxLength="10" Placeholder="Max 10 Chars"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ajxFtSenderID" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="tbSenderID" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 col-lg-12">
                                <div class="row pb-3">
                                    <div class="col-md-4 col-lg-4 text-left">
                                        <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Key<span class="text-warning">*</span></asp:Label>
                                    </div>
                                    <div class="col-md-7 col-lg-7 text-left">
                                        <asp:TextBox Autocomplete="off" ID="tbUserName" runat="server" CssClass="form-control form-control-sm" ToolTip="Enter User Name" MaxLength="10" Placeholder="Max 10 Chars"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ajxFtUserName" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="tbUserName" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 col-lg-12">
                                <div class="row pb-3">
                                    <div class="col-md-4 col-lg-4 text-left">
                                        <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Value<span class="text-warning">*</span></asp:Label>
                                    </div>
                                    <div class="col-md-7 col-lg-7 text-left">
                                        <asp:TextBox Autocomplete="off" ID="tbPassword" MaxLength="20" runat="server" CssClass="form-control form-control-sm" ToolTip="Enter Password" Placeholder="Max 20 Chars"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 col-lg-12">
                                <div class="row pb-3">
                                    <div class="col-md-4 col-lg-4 text-left">
                                        <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">DLT ID<span class="text-warning">*</span></asp:Label>
                                    </div>
                                    <div class="col-md-7 col-lg-7 text-left">
                                        <asp:TextBox Autocomplete="off" ID="tbDLTid" runat="server" CssClass="form-control form-control-sm" ToolTip="Enter DLT ID" Placeholder="Max 50 Chars"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ajxFtDLTid" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="tbDLTid" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 col-lg-12 text-center py-2">
                                <asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_Click" runat="server" CssClass="btn btn-success"><asp:Label runat="server" CssClass="form-control-label1"><i class="fa fa-save"></i> Save</asp:Label> </asp:LinkButton>
                                <asp:LinkButton ID="lbtnReset" runat="server" OnClick="lbtnReset_Click" CssClass="btn btn-warning"><asp:Label runat="server" CssClass="form-control-label1"><i class="fa fa-undo"></i> Reset </asp:Label> </asp:LinkButton>
                            </div>

                        </div>

                    </asp:Panel>
                </div>
            </div>
            <div class="col-lg-8 col-md-8 order-xl-2">
                <div class="card" style="min-height: 500px">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-md-11 col-lg-11">
                                <asp:Label runat="server" CssClass="form-control-label"><h2>SMS Template List</h2></asp:Label>
                            </div>
                            <div class="col-lg-1 col-md-1 text-right">
                                <asp:LinkButton ID="lbtnViewConfig" OnClick="lbtnViewConfig_Click" runat="server" ToolTip="View Instructions" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                </asp:LinkButton>
                           
                            </div>

                        </div>
                    </div>
                    <div class="card-body">

                        <asp:GridView ID="gvSMS" runat="server" AutoGenerateColumns="False"
                            GridLines="None" ShowHeader="true" DataKeyNames="moduleid,templatei,templatetext,purpos">
                            <Columns>                                
                                <asp:TemplateField HeaderText="Message Text">
                                    <HeaderTemplate>
                                         <div class="row py-2  px-4 border-bottom">    
                                        <asp:CheckBox ID="cbAll" AutoPostBack="true" type="checkbox" OnCheckedChanged="cbAll_CheckedChanged" runat="server" /> &nbsp;&nbsp; Select All</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="row py-2 border-bottom">                                           
                                         
                                               
                                                   <asp:HiddenField ID="hdmoduleid" runat="server" Value='<%#Eval("moduleid") %>' />
                                                    <div class="col-lg-1 text-center">
                                                        <asp:CheckBox ID="cbSMS" type="checkbox" runat="server" ClientIDMode="Static" Style="height:10px; width:10px" /></div>
                                                         <div class="col-lg-8">
                                                        <asp:Label ID="Label1" runat="server" CssClass="form-control-label text-center" Text='<%# Eval("templatetext") %>'></asp:Label><br />
                                                    
                                                       (<asp:Label ID="lblPurpose" runat="server" CssClass="form-control-label text-center" Text='<%# Eval("purpos") %>'></asp:Label>)
                                                    </div>
                                              
                                                    <div class="col-lg-3">                                                
                                                        <asp:TextBox ID="tbEntityid" Enabled="false" AutoComplete="off" ToolTip="Enter Entity ID" Text='<%# Eval("templatei") %>' placeholder="max 20 chars" MaxLength="20" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers" TargetControlID="tbEntityid" />
                                 
                                                    </div>
                                                </div>
                                        
                                        </div>

                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                        </asp:GridView>
                        <br />

                        <asp:LinkButton ID="lbtnSaveSMS" OnClick="lbtnSaveSMS_Click" runat="server" CssClass="btn btn-success"><asp:Label runat="server" CssClass="form-control-label1"><i class="fa fa-save"></i> Save </asp:Label> </asp:LinkButton>


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
                    <h4 class="card-title text-left mb-0">Confirmation
                    </h4>
                </div>
                <div class="card-body text-left pt-2" style="min-height: 100px;">
                    <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                    <div style="width: 100%; margin-top: 20px; text-align: right;">
                        <asp:LinkButton ID="lbtnYesConfirmation" OnClick="lbtnYesConfirmation_Click" runat="server" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                        <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button4" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>

</asp:Content>
