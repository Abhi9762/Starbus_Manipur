<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/ProjectAdmmaster.master" AutoEventWireup="true" CodeFile="ProjectAdmGenInfo.aspx.cs" Inherits="Auth_ProjectAdmGenInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function UploadDeptlogo(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnDeotLogo.ClientID %>").click();
            }
        }
        function UploadGovlogo(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnGovLogo.ClientID %>").click();
            }
        }
        function UploadFaviconImage(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnFavicon.ClientID %>").click();
            }
        }


    </script>
     <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid mt-1">

        <div class="row my-2">
            <div class="col-lg-6 col-md-6 order-xl-1">
                <div class="card" style="min-height: 610px">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-md-6 col-lg-6">
                                <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h2 class="text-warning">Please Note</h2></asp:Label>
                            </div>
                            <div class="col-md-6 col-lg-6 text-right">
                                <asp:Label runat="server" class="h4 text-warning text-right">All Marked * Fields are mandatory</asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12 col-lg-12">


                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label runat="server" CssClass="form-control-label" Font-Size="Medium">Department Logo</asp:Label>
                                    </div>
                                    <div class="col-lg-12">
                                        <asp:Label runat="server" CssClass="text-sm">
                                            1. Image must be uploaded  only in .png format with transaprent background.<br />
                                            2. Logo size should not exceed 100 KB<br />
                                3. Resolution of logo must be 60px X 60px </asp:Label>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label runat="server" CssClass="form-control-label " Font-Size="Medium">Government Logo</asp:Label>
                                    </div>
                                    <div class="col-lg-12">
                                        <asp:Label runat="server" CssClass="text-sm">1. Image must be uploaded .png  format with transaprent background.<br />
                                2. Logo size should not exceed 100 KB<br />
                                3. Resolution of logo must be 60px X 60px </asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label runat="server" CssClass="form-control-label " Font-Size="Medium">Favicon</asp:Label>
                                    </div>
                                    <div class="col-lg-12">
                                        <asp:Label runat="server" CssClass="text-sm">1. Image must be uploaded .png  format with transaprent background.<br />
                                2. Favicon size should not exceed 100 KB<br />
                                3. Resolution of logo must be 60px X 60px </asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label runat="server" CssClass="form-control-label " Font-Size="Medium">Department Name</asp:Label>
                                    </div>
                                    <div class="col-lg-12">
                                        <asp:Label runat="server" CssClass="text-sm">1. Department Name will be visible in Home page and other pages<br />
                                2. Department Name length should not exceed 100 characters</asp:Label>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label runat="server" CssClass="form-control-label " Font-Size="Medium">Application Title</asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label runat="server" CssClass="text-sm">1. Application Title will be visible at the top of the browser window.<br />
                                2. Application Title should be of Max. 20 characters.</asp:Label>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <div class="col-md-6 col-lg-6 order-xl-2">
                <div class="card" style="min-height: 610px">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-md-10 col-lg-10">
                                <asp:Label runat="server" CssClass="form-control-label"><h2>Project Configuration</h2></asp:Label>

                            </div>

                            <div class="col-lg-2 col-md-2 float-left m-0 p-0">
                                <asp:LinkButton ID="lbtnHelp" runat="server" ToolTip="View Instructions" OnClick="lbtnHelp_Click" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                </asp:LinkButton>

 <asp:LinkButton ID="lbtnManual" runat="server" ToolTip="View Manual" OnClick="lbtnManual_Click" CssClass="btn btn-sm btn-success">
                                    <i class="ni ni-cloud-download-95 mttop "></i>
                                </asp:LinkButton>




                                <asp:LinkButton ID="lbtnViewHistory" runat="server" ToolTip="View History" OnClick="lbtnViewHistory_Click" Visible="false" CssClass="btn btn bg-gradient-primary btn-sm text-white">
                                    <i class="fa fa-history"></i>
                                </asp:LinkButton>
                            </div>

                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12 col-lg-12">
                                <div class="row mx-2 my-1">
                                    <div class="col-lg-12">
                                        <asp:Label runat="server" CssClass="form-control-label" Font-Size="Medium">1. Logo</asp:Label>
                                    </div>
                                </div>
                                <div class="row mt-2 align-items-center ">
                                    <div class="col-md-4 col-lg-4 text-left">
                                        <asp:Label runat="server" CssClass="form-control-label mx-3">Department Logo <span class="text-warning">*</span></asp:Label>
                                    </div>
                                    <div class="col-md-8 col-lg-8 text-left">
                                        <asp:Button ID="btnDeotLogo" OnClick="btnDeotLogo_Click" runat="server" CausesValidation="False" CssClass="form-control form-control-sm"
                                            Style="display: none" TabIndex="18" Text="Upload Image" Width="80px" />
                                        <asp:Label runat="server" ID="lbldept" CssClass="form-control-label"></asp:Label>
                                        <asp:FileUpload ID="fuDeptLogo" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-sm btn-success "
                                            onchange="UploadDeptlogo(this);" TabIndex="9" />
                                        <asp:Image ID="ImgDepartmentLogo" runat="server" Visible="false" Style="border-width: 0px; height: 50px; width: 50px; border: 2px solid #eaf4ff;" />
                                    </div>
                                </div>
                                <div class="row mt-2 align-items-center">
                                    <div class="col-md-4 col-lg-4 text-left">
                                        <asp:Label runat="server" CssClass="form-control-label mx-3">Government Logo <span class="text-warning">*</span> </asp:Label>

                                    </div>
                                    <div class="col-md-8 col-lg-8 text-left">

                                        <asp:Button ID="btnGovLogo" OnClick="btnGovLogo_Click" runat="server" CausesValidation="False" CssClass="form-control form-control-sm"
                                            Style="display: none" TabIndex="18" Text="Upload Image" Width="80px" />
                                        <asp:Label runat="server" ID="lblgiv" CssClass="form-control-label"></asp:Label>
                                        <asp:FileUpload ID="fuGovtLogo" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-sm btn-success "
                                            onchange="UploadGovlogo(this);" TabIndex="9" />
                                        <asp:Image ID="imgGovLogo" runat="server" Visible="false" Style="border-width: 0px; height: 50px; width: 50px; border: 2px solid #eaf4ff;" />
                                        <br />

                                    </div>
                                </div>
                                <div class="row mt-2 align-items-center">
                                    <div class="col-md-4 col-lg-4 text-left">
                                        <asp:Label runat="server" CssClass="form-control-label mx-3"> Favicon<span class="text-warning">*</span> </asp:Label>

                                    </div>
                                    <div class="col-md-8 col-lg-8 text-left">

                                        <asp:Button ID="btnFavicon" OnClick="btnFavicon_Click" runat="server" CausesValidation="False" CssClass="form-control form-control-sm"
                                            Style="display: none" TabIndex="18" Text="Upload Image" Width="80px" />
                                        <asp:FileUpload ID="fuFavIcon" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-sm btn-success "
                                            onchange="UploadFaviconImage(this);" TabIndex="9" />
                                        <asp:Image ID="imgFavicon" runat="server" Visible="false" Style="border-width: 0px; height: 50px; width: 50px; border: 2px solid #eaf4ff;" />
                                        <br />

                                    </div>
                                </div>


                                <div class="row mx-2 my-1 align-items-center">
                                    <div class="col-md-4 col-lg-4 text-left">
                                        <span>Localization<span class="text-warning">*</span></span>

                                    </div>
                                    <div class="col-md-4 col-lg-4 text-left">
                                         <asp:RadioButtonList ID="rbtnLocalization" runat="server" CssClass="rbl" AutoPostBack="true" OnSelectedIndexChanged="rbtnLocalization_SelectedIndexChanged"
                                        RepeatDirection="Horizontal"  Height="25px" Width="150px">
                                        <asp:ListItem Value="Y" Text="Yes" Selected="False"></asp:ListItem>
                                        <asp:ListItem Value="N" Text="No" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    </div>

                                    <div class="col-md-4 col-lg-4 text-left">
                                        <asp:Label ID="lblLocalLanguage" Visible="false" runat="server">Local Language</asp:Label>
                                         <asp:DropDownList ID="ddllocalLanguage" Visible="false" runat="server" CssClass="form-control form-control-sm">
                                        <asp:ListItem Value="0" Text="Select" Selected="False"></asp:ListItem>
                                        <asp:ListItem Value="Hi" Text="Hindi" Selected="False"></asp:ListItem>
                                        <asp:ListItem Value="En" Text="English" Selected="False"></asp:ListItem>
                                    </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="row mx-2 my-1">
                                    <div class="col-lg-12">
                                        <asp:Label runat="server" CssClass="form-control-label" Font-Size="Medium">2. Department Name</asp:Label>
                                    </div>
                                </div>
                                <div class="row m-0 align-items-center">
                                    <div class="col-md-6 col-lg-6 text-left">
                                        <span>English<span class="text-warning">*</span></span>
                                        <asp:TextBox Autocomplete="off" ID="tbDepartmentNameEn" runat="server" CssClass="form-control form-control-sm" MaxLength="100" ToolTip="Enter Department Name in English" Placeholder="Max 100 Chars"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ajxFtDeptName" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Custom" ValidChars=" "
                                            TargetControlID="tbDepartmentNameEn" />
                                    </div>
                                    <div class="col-md-6 col-lg-6 text-left">
                                        <asp:Label runat="server" ID="lbldeptName" Visible="false">Local<span class="text-warning">*</span></asp:Label>
                                        <asp:TextBox Autocomplete="off" ID="tbDepartmentNameL" Visible="false"  runat="server" CssClass="form-control form-control-sm" MaxLength="100" ToolTip="Enter Department Name in local Language" Placeholder="Max 100 Chars"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row mx-2 my-1">
                                    <div class="col-lg-12">
                                        <asp:Label runat="server" CssClass="form-control-label" Font-Size="Medium">3. Department Abbreviation</asp:Label>
                                    </div>
                                </div>
                                <div class="row m-0 align-items-center ">
                                    <div class="col-md-6 col-lg-6 text-left">
                                        <span>English<span class="text-warning">*</span></span>
                                        <asp:TextBox Autocomplete="off" ID="tbDepartmentAbbrEn" runat="server" CssClass="form-control form-control-sm text-uppercase" MaxLength="100" ToolTip="Enter Department Abbreviation in English" Placeholder="Max 10 Chars"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ajxFtDepartment" runat="server" FilterType="UppercaseLetters,Custom,Lowercaseletters" ValidChars=" " TargetControlID="tbDepartmentAbbrEn" />
                                    </div>
                                    <div class="col-md-6 col-lg-6 text-left">
                                        <asp:Label Visible="false" ID="lblDeptabbrLocal" runat="server">Local<span class="text-warning">*</span></asp:Label>
                                        <asp:TextBox Autocomplete="off" ID="tbDepartmentAbbreviationLocal" Visible="false" runat="server" CssClass="form-control form-control-sm" MaxLength="10" ToolTip="Enter Department Abbreviation" Placeholder="Max 10 Chars"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row mx-2 my-1">
                                    <div class="col-lg-12">
                                        <asp:Label runat="server" CssClass="form-control-label" Font-Size="Medium">4. Application Title</asp:Label>
                                    </div>
                                </div>
                                <div class="row m-0 align-items-center ">
                                    <div class="col-md-6 col-lg-6 text-left">
                                        <span>English<span class="text-warning">*</span></span>
                                        <asp:TextBox Autocomplete="off" ID="tbApplication" runat="server" MaxLength="100" CssClass="form-control form-control-sm" ToolTip="Enter Application Title" Placeholder="Max 50 Chars"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ajxFtApllication" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Custom" ValidChars=" " TargetControlID="tbApplication" />
                                    </div>
                                    <div class="col-md-6 col-lg-6 text-left">
                                        <asp:Label Visible="false" ID="lblAppTitleLocal" runat="server">Local<span class="text-warning">*</span></asp:Label>
                                        <asp:TextBox Autocomplete="off" ID="tbAppliactionLocal"  Visible="false" runat="server" CssClass="form-control form-control-sm" MaxLength="100" ToolTip="Enter Application Title in Local language" Placeholder="Max 50 Chars"></asp:TextBox>
                                    </div>
                                </div>


                                  <div class="row mx-2 my-1">
                                    <div class="col-lg-12">
                                        <asp:Label runat="server" CssClass="form-control-label" Font-Size="Medium">5. Application Version</asp:Label>
                                    </div>
                                </div>
                                <div class="row m-0 align-items-center ">
                                    <div class="col-md-4 col-lg-4 text-left">
                                       
                                        <asp:TextBox Autocomplete="off" ID="tbVersion" runat="server" MaxLength="3" CssClass="form-control form-control-sm" ToolTip="Enter Application Title" Placeholder="X.X"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom" ValidChars="." TargetControlID="tbVersion" />
                                    </div>
                                
                                </div>


                                <div class="row mx-2 my-1">
                                    <div class="col-lg-12">
                                        <asp:Label runat="server" CssClass="form-control-label" Font-Size="Medium">6. Footer Text</asp:Label>
                                    </div>
                                </div>
                                <div class="row m-0 align-items-center ">
                                    <div class="col-md-6 col-lg-6 text-left">
                                        <span>English<span class="text-warning">*</span></span>
                                        <asp:TextBox Autocomplete="off" ID="tbFooterEn" TextMode="MultiLine" Rows="3" Style="resize:none" runat="server" CssClass="form-control form-control-sm" MaxLength="300" ToolTip="Enter Footer " Placeholder="Max 100 Chars"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Custom" ValidChars=" " TargetControlID="tbFooterEn" />
                                    </div>
                                    <div class="col-md-6 col-lg-6 text-left">
                                        <asp:Label Visible="false" ID="lblfooterLocal" runat="server">Local<span class="text-warning">*</span></asp:Label>
                                        <asp:TextBox Autocomplete="off" ID="tbFooterLocal" Visible="false" TextMode="MultiLine" Rows="3" Style="resize:none"  runat="server" CssClass="form-control form-control-sm" MaxLength="50" ToolTip="Enter Footer  in Local language" Placeholder="Max 50 Chars"></asp:TextBox>
                                    </div>
                                </div>




                            </div>
                        </div>
                        <div class="row m-0 align-items-center py-1 px-3">
                            <div class="col-md-12 col-lg-12 text-center pt-3">
                                <asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_Click" runat="server" ToolTip="Click here to Save General Configuration" CssClass="btn btn-success"><i class="fa fa-save"></i> Save</asp:LinkButton>
                                <asp:LinkButton ID="lbtnReset" OnClick="lbtnReset_Click" runat="server" ToolTip="Click here to Reset General Configuration Value" CssClass="btn btn-warning"><i class="fa fa-undo"></i> Reset</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <cc1:ModalPopupExtender ID="mplimithistory" runat="server" PopupControlID="pnllimithistory"
                TargetControlID="Button9" CancelControlID="btnclose" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnllimithistory" runat="server" Style="position: fixed; top: 30.5px; max-width: 800px; min-width: 400px; display: none;">
                <div class="card">
                    <div class="card-header" style="border-color: #2dce89; background-color: #2dce89; color: white">
                        <strong class="card-title">General Configuration History</strong>
                    </div>

                    <div class="card-body" style="padding: 15px !important;">

                        <asp:Label runat="server" ID="noHistoryLbl" Text="No Record Found" Visible="false"></asp:Label>
                        <asp:GridView ID="gvHistory" runat="server" AutoGenerateColumns="False" GridLines="None"
                            AllowSorting="true" AllowPaging="true" PageSize="4" OnPageIndexChanging="gvHistory_PageIndexChanging" CssClass="table table-flush table mar table-responsive"
                            DataKeyNames="title,departmentname,departmentabbr,updateby,actiondate" HeaderStyle-CssClass="thead-light" Font-Size="11pt">
                            <Columns>
                                <asp:TemplateField HeaderText="Title">
                                    <ItemTemplate>
                                        <asp:Label ID="TITLE" runat="server" Text='<%# Eval("title") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department Name">
                                    <ItemTemplate>
                                        <asp:Label ID="DEPARTMENTNAME" runat="server" Text='<%# Eval("departmentname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Managed By">
                                    <ItemTemplate>
                                        <asp:Label ID="MANAGEDBY" runat="server" Text='<%# Eval("departmentabbr") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Updated By">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUPDATEDBY" runat="server" Text='<%# Eval("updateby") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Updation Date/Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUPDATEDON" runat="server" Text='<%# Eval("actiondate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" />
                        </asp:GridView>
                        <asp:Label ID="lblNoHistory" runat="server" Visible="false" CssClass="text-bold text-lg text-capitalize"> NO History Available</asp:Label>

                    </div>
                    <div class="card-footer">
                        <asp:LinkButton ID="btnclose" runat="server" CssClass="btn btn-success" Style="font-size: 11pt; float: right; border-radius: 4px;"><i class="fa fa-times"></i>&nbsp;&nbsp;Close</asp:LinkButton>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button9" runat="server" Text="" />
                </div>
            </asp:Panel>
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
    </div>
</asp:Content>

