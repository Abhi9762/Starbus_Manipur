<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/PortalAdmmaster.master" AutoEventWireup="true" CodeFile="PAdminMentor.aspx.cs" Inherits="Auth_PAdminMentor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .photocard {
            box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
        }

            .photocard img {
                height: 190px;
            }

        .text box {
            margin-top: 7px;
        }
    </style>
    <script type="text/javascript">
        function UploadFilePIOS(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnmentor1.ClientID %>").click();
            }
        }
        function UploadFileManual1(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnmentor2.ClientID %>").click();
            }
        }
        function UploadFileManual2(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnmentor3.ClientID %>").click();
            }
        }
        function UploadFileManual3(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnmentor4.ClientID %>").click();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <div class="header pb-3">
    </div>
    <div class="container-fluid mt-1 pb-3">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row">
            <div class="col-lg-3 col-md-3">
                <div class="row m-0">
                    <div class="card mr-2" style="min-height: 530px">

                        <div class="card-header">
                            <div class="row m-0 align-items-center">
                                <div class="col-md-10 ">
                                    <h3 class="mb-1">
                                        <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h2><span style="color: #ff5e00;">Mentors</span></h2></asp:Label>
                                    </h3>
                                </div>
                                <div class="col-md-2 text-right ">
                                    <h4>
                                        <asp:LinkButton ID="lbtnHelp" OnClick="lbtnHelp_Click" runat="server" ToolTip="View Instructions" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                        </asp:LinkButton>
                                    </h4>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-lg-12">
                            <div class="row mx-2 my-1">
                                <div class="col">

                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="Medium">Actions
                              
                                    </asp:Label>
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="false" Font-Size="Small"><br /> 1. Upload Mentors Image and Information.<br />
                                2. Delete Existing Mentors Image and Information.<br />
                                    </asp:Label>

                                </div>

                            </div>
                            <br />
                            <div class="row mx-2 my-1">
                                <div class="col">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="Medium">Please Note</asp:Label>
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="false" Font-Size="Small"><br />
                               1. You can add Maximum 4 Image at a time.<br />
                               2. Image size must be less than 50 KB.<br />
                                3. Height and Width of Image must be 400 x 504.
                                    </asp:Label>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

            </div>
            <div class="col-lg-9 col-md-9">
                <div class="row">
                    <div class="col-md-6">
                        <div class="card photocard">
                            <div class="card-header bg-primary py-1 text-white text-left">
                                <span style="font-weight: 600;">Mentor - 1 </span><span>(Chief Minister)</span>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-5">
                                        <asp:Label runat="server" Visible="true" CssClass="form-control-label">Name<span class="text-warning">*</span></asp:Label>
                                        <asp:TextBox ID="tbnameCM" runat="server" autocomplete="off" class="form-control form-control-sm" ToolTip="Enter Name" placeholder="Name"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="LowercaseLetters,UppercaseLetters,custom" ValidChars=" " TargetControlID="tbnameCM" />

                                        <asp:Label runat="server" Visible="true" CssClass="form-control-label">Local Language<span class="text-warning">*</span></asp:Label>

                                        <asp:TextBox ID="tbLocalLngCM" runat="server" autocomplete="off" class="form-control form-control-sm text box" Style="margin-top: 7px;" ToolTip="Enter Name" placeholder="Local Lang"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="LowercaseLetters,UppercaseLetters,custom" ValidChars=" " TargetControlID="tbLocalLngCM" />

                                        <asp:Label runat="server" Visible="true" CssClass="form-control-label">Designation<span class="text-warning">*</span></asp:Label>

                                        <asp:TextBox ID="tbdesignationCM" runat="server" autocomplete="off" ToolTip="Enter Designation" Style="margin-top: 7px;" class="form-control form-control-sm text box" placeholder="Designation"
                                            Text=""></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="LowercaseLetters,UppercaseLetters,custom" ValidChars=" " TargetControlID="tbdesignationCM" />

                                        <asp:Label runat="server" Visible="true" CssClass="form-control-label">Info URL<span style="font-size:10px">(Optional)</span></asp:Label>

                                        <asp:TextBox ID="tbCMUrl" runat="server" autocomplete="off" ToolTip="Enter URL" Style="margin-top: 7px;" class="form-control form-control-sm" placeholder="Enter URL"
                                            Text=""></asp:TextBox>
                                        <br />

                                    </div>
                                    <div class="col-md-7">
                                        <asp:Image runat="server" ID="imgmentor1img" class="rounded" Width="100%" />
                                        <asp:Button ID="btnmentor1" runat="server" OnClick="btnmentor1_Click" CausesValidation="False" Style="display: none"
                                            TabIndex="18" Text="Update Image" />
                                        <asp:FileUpload ID="FileUploadMentor1" Visible="false" runat="server"
                                            Style="color: black; background-color: #eaf4ff; margin-top: 17px; margin-bottom: 10px; width: 100%; border: none;" CssClass="btn btn-sm btn-success "
                                            onchange=" UploadFilePIOS(this);" accept="image/*" TabIndex="9" />
                                        <asp:Label runat="server" Visible="true" ID="lblmentor1imgname" CssClass="form-control-label"></asp:Label>
                                        <br />
                                        <asp:LinkButton ID="lbtnremovementor1" OnClick="lbtnremovementor1_Click" runat="server" ToolTip="Click here to Remove Chief Minister Details" class="btn btn-sm btn-danger" Style="float: right; margin-top: 19px;">
                                                <i class="fa fa-trash"  title="Click here to Remove Chief Minister Details"></i> </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnsubmitmentor1" OnClick="lbtnsubmitmentor1_Click" Visible="false" runat="server" ToolTip="Click here to Add/Update Chief Minister Details" class="btn btn-sm btn-success">
                                                <i class="fa fa-save" title="Click here to Add/Update Chief Minister Details"></i> Update</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnResetMentor1" OnClick="lbtnResetMentor1_Click" Visible="false" runat="server" ToolTip="Click here to Reset Chief Minister Details" class="btn btn-sm btn-warning">
                                                <i class="fa fa-recycle" title="Click here to Reset Chief Minister Details"></i> Reset</asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="card photocard">
                            <div class="card-header bg-primary py-1 text-white text-left">
                                <span style="font-weight: 600;">Mentor - 2</span><span> (Transport Minister)</span>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-5">

                                        <asp:Label runat="server" Visible="true" ID="Label5" CssClass="form-control-label">Name<span class="text-warning">*</span></asp:Label>

                                        <asp:TextBox ID="tbTMmentorName" autocomplete="off" runat="server" class="form-control form-control-sm" ToolTip="Enter Name" placeholder="Name"></asp:TextBox>

                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="LowercaseLetters,UppercaseLetters,custom" ValidChars=" " TargetControlID="tbTMmentorName" />
                                        <asp:Label runat="server" Visible="true" ID="Label6" CssClass="form-control-label">Local Language<span class="text-warning">*</span></asp:Label>

                                        <asp:TextBox ID="tbMentorLoacalLang" autocomplete="off" runat="server" Style="margin-top: 7px;" class="form-control form-control-sm" ToolTip="Enter Name" placeholder="Local Lang"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="LowercaseLetters,UppercaseLetters,custom" ValidChars=" " TargetControlID="tbMentorLoacalLang" />

                                        <asp:Label runat="server" Visible="true" ID="Label7" CssClass="form-control-label">Designation<span class="text-warning">*</span></asp:Label>

                                        <asp:TextBox ID="tbTMdesignation" autocomplete="off" runat="server" ToolTip="Enter Designation" Style="margin-top: 7px;" class="form-control form-control-sm" placeholder="Designation"
                                            Text=""></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="LowercaseLetters,UppercaseLetters,custom" ValidChars=" " TargetControlID="tbTMdesignation" />

                                        <asp:Label runat="server" Visible="true" ID="Label8" CssClass="form-control-label">Info <span style="font-size:10px">(Optional)</span></asp:Label>

                                        <asp:TextBox ID="tbMentor2URL" runat="server" autocomplete="off" ToolTip="Enter URL" Style="margin-top: 7px;" class="form-control form-control-sm" placeholder="Enter URL"
                                            Text=""></asp:TextBox>
                                        <br />

                                    </div>
                                    <div class="col-md-7">
                                        <asp:Image runat="server" ID="imgmentor2" class="rounded" Width="100%" />
                                        <asp:Button ID="btnmentor2" runat="server" OnClick="btnmentor2_Click" CausesValidation="False" Style="display: none"
                                            TabIndex="18" Text="Update Image" />
                                        <asp:FileUpload ID="FUmentor2" runat="server" Style="color: black; background-color: #eaf4ff; margin-bottom: 10px; width: 100%; margin-top: 17px; border: none;" CssClass="btn btn-sm btn-success "
                                            onchange="UploadFileManual1(this);" accept="image/*" TabIndex="9" />
                                        <asp:Label runat="server" Visible="true" ID="lblmentor2imgname" CssClass="form-control-label"></asp:Label>
                                        <br />
                                        <asp:LinkButton ID="lbtnsubmitmentor2" OnClick="lbtnsubmitmentor2_Click" runat="server" Visible="false" ToolTip="Click here to Add/Update Transport Minister Details"
                                            class="btn btn-sm btn-success"><i class="fa fa-save" title="Click here to Add/Update Transport Minister Details"></i> Update</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnResetMentor2" OnClick="lbtnResetMentor2_Click" runat="server" Visible="false" ToolTip="Click here to Reset Transport Minister Image"
                                            class="btn btn-sm btn-warning"><i class="fa fa-recycle" title="Click here to Reset Transport Minister Details"></i> Reset</asp:LinkButton>

                                        <asp:LinkButton ID="lbtnremovementor2" runat="server" OnClick="lbtnremovementor2_Click" ToolTip="Click here to Remove Transport Minister Details" class="btn btn-sm btn-danger" Style="float: right; margin-top: 19px;">
                                                <i class="fa fa-trash"  title="Click here to Remove Transport Minister Details"></i> </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="card photocard">
                            <div class="card-header bg-primary py-1 text-white text-left">
                                <span style="font-weight: 600;">Mentor - 3</span><span> (Secretary)</span>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-5">
                                        <asp:Label runat="server" Visible="true" ID="Label9" CssClass="form-control-label">Name<span class="text-warning">*</span></asp:Label>

                                        <asp:TextBox ID="tbSecretaryName" autocomplete="off" runat="server" class="form-control form-control-sm" ToolTip="Enter Name" placeholder="Name"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="LowercaseLetters,UppercaseLetters,custom" ValidChars=" " TargetControlID="tbSecretaryName" />

                                        <asp:Label runat="server" Visible="true" ID="Label10" CssClass="form-control-label">Local Language<span class="text-warning">*</span></asp:Label>

                                        <asp:TextBox ID="tbSecretaryLocalName" autocomplete="off" runat="server" Style="margin-top: 7px;" class="form-control form-control-sm" ToolTip="Enter Name" placeholder="Local Lang"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="LowercaseLetters,UppercaseLetters,custom" ValidChars=" " TargetControlID="tbSecretaryLocalName" />

                                        <asp:Label runat="server" Visible="true" ID="Label11" CssClass="form-control-label">Designation<span class="text-warning">*</span></asp:Label>

                                        <asp:TextBox ID="tbSecretaryDgn" autocomplete="off" runat="server" Style="margin-top: 7px;" ToolTip="Enter Designation" class="form-control form-control-sm" placeholder="Designation"
                                            Text=""></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="LowercaseLetters,UppercaseLetters,custom" ValidChars=" " TargetControlID="tbSecretaryDgn" />

                                        <asp:Label runat="server" Visible="true" ID="Label12" CssClass="form-control-label">Info URL<span style="font-size:10px">(Optional)</span></asp:Label>
                                        <asp:TextBox ID="tbSecURL" runat="server" autocomplete="off" ToolTip="Enter URL" Style="margin-top: 7px;" class="form-control form-control-sm" placeholder="Enter URL"
                                            Text=""></asp:TextBox>
                                        <br />

                                    </div>
                                    <div class="col-md-7">
                                        <asp:Image runat="server" ID="imgMentor3" class="rounded" Width="100%" />
                                        <asp:Button ID="btnmentor3" runat="server" OnClick="btnmentor3_Click" CausesValidation="False" Style="display: none"
                                            TabIndex="18" Text="Update Image" />
                                        <asp:FileUpload ID="FuSecretary" runat="server" Style="color: black; background-color: #eaf4ff; margin-bottom: 10px; margin-top: 17px; width: 100%; border: none;" CssClass="btn btn-sm btn-success "
                                            onchange="UploadFileManual2(this);" accept="image/*" TabIndex="9" />
                                        <asp:Label runat="server" Visible="true" ID="lblmentor3imgname" CssClass="form-control-label"></asp:Label>
                                        <br />
                                        <asp:LinkButton ID="lbtnsubmitmentor3" OnClick="lbtnsubmitmentor3_Click" Visible="false" runat="server" OnClientClick="return ShowLoading()" ToolTip="Click here to Add/Update Secretary Details"
                                            class="btn btn-sm btn-success"><i class="fa fa-save" title="Click here to Add/Update Secretary Details"></i> Update</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnremovementor3" runat="server" OnClick="ltnremovementor3_Click" ToolTip="Click here to Remove Secretary Details" class="btn btn-sm btn-danger" Style="float: right; margin-top: 19px;">
                                                <i class="fa fa-trash"  title="Click here to Remove Secretary Image"></i> </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnResetMentor3" OnClick="lbtnResetMentor3_Click" Visible="false" runat="server" ToolTip="Click here to Reset Secretary Details" class="btn btn-sm btn-warning">
                                                <i class="fa fa-recycle" title="Click here to Reset Secretary Details"></i> Reset</asp:LinkButton>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="card photocard">
                            <div class="card-header bg-primary py-1 text-white text-left">
                                <span style="font-weight: 600;">Mentor - 4</span><span> (Managing Director)</span>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-5">

                                        <asp:Label runat="server" Visible="true" ID="Label13" CssClass="form-control-label">Name<span class="text-warning">*</span></asp:Label>

                                        <asp:TextBox ID="tbMDirectorName" runat="server" class="form-control form-control-sm" autocomplete="off" ToolTip="Enter Name" placeholder="Name"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="LowercaseLetters,UppercaseLetters,custom" ValidChars=" " TargetControlID="tbMDirectorName" />

                                        <asp:Label runat="server" Visible="true" ID="Label14" CssClass="form-control-label">Local Language<span class="text-warning">*</span></asp:Label>

                                        <asp:TextBox ID="tbMDirectorLocalLang" runat="server" class="form-control form-control-sm" Style="margin-top: 7px;" autocomplete="off" ToolTip="Enter Name" placeholder="Local Lang"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="LowercaseLetters,UppercaseLetters,custom" ValidChars=" " TargetControlID="tbMDirectorLocalLang" />

                                        <asp:Label runat="server" Visible="true" ID="Label15" CssClass="form-control-label">Designation<span class="text-warning">*</span></asp:Label>

                                        <asp:TextBox ID="tbMDirectorDgn" autocomplete="off" runat="server" ToolTip="Enter Designation" Style="margin-top: 7px;" class="form-control form-control-sm" placeholder="Designation"
                                            Text=""></asp:TextBox>
                                   <cc1:FilteredTextBoxExtender runat="server" FilterType="LowercaseLetters,UppercaseLetters,custom" ValidChars=" " TargetControlID="tbMDirectorDgn" />

                                        <asp:Label runat="server" Visible="true" ID="Label16" CssClass="form-control-label">Info URL<span style="font-size:10px">(Optional)</span></asp:Label>

                                        <asp:TextBox ID="tbDirectorURL" runat="server" autocomplete="off" ToolTip="Enter URL" Style="margin-top: 7px;" class="form-control form-control-sm" placeholder="Enter URL"
                                            Text=""></asp:TextBox>
                                        <br />

                                    </div>
                                    <div class="col-md-7">
                                        <asp:Image runat="server" ID="imgmentor4" class="rounded" Width="100%" />
                                        <asp:Button ID="btnmentor4" runat="server" OnClick="btnmentor4_Click" CausesValidation="False" Style="display: none"
                                            TabIndex="18" Text="Update Image" />
                                        <asp:FileUpload ID="FuMDirector" runat="server" Style="color: black; margin-top: 17px; margin-bottom: 10px; background-color: #eaf4ff; width: 100%; border: none;" CssClass="btn btn-sm btn-success "
                                            onchange="UploadFileManual3(this);" accept="image/*" TabIndex="9" />
                                        <asp:Label runat="server" Visible="true" ID="lblmentor4imgname" CssClass="form-control-label"></asp:Label>
                                        <br />
                                        <asp:LinkButton ID="lbtnsubmitmentor4" OnClick="lbtnsubmitmentor4_Click" Visible="false" runat="server" ToolTip="Click here to Add/Update Managing Director Image"
                                            class="btn btn-sm btn-success"><i class="fa fa-save" title="Click here to Add/Update Managing Director Image"></i> Update</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnremovementor4" OnClick="ltnremovementor4_Click" runat="server" ToolTip="Click here to Add/Update Secretary Image" class="btn btn-sm btn-danger" Style="float: right; margin-top: 19px;">
                                                <i class="fa fa-trash"  title="Click here to Remove Managing Director Image"></i> </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnResetMentor4" OnClick="lbtnMentor4_Click" Visible="false" runat="server" ToolTip="Click here to Reset Managing Director Details" class="btn btn-sm btn-warning">
                                                <i class="fa fa-recycle" title="Click here to Reset Managing Director Details"></i> Reset</asp:LinkButton>

                                    </div>
                                </div>
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
    </div>

</asp:Content>

