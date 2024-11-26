<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/PortalAdmmaster.master" AutoEventWireup="true" CodeFile="PAdminHomePage.aspx.cs" Inherits="Auth_PAdminHomePage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .photocard {
            box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
        }

            .photocard img {
                height: 181px;
            }
    </style>
    <script type="text/javascript">
        function UploadFile1(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnUploadImage1.ClientID %>").click();
            }
        }
        function UploadFile2(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnUploadImage2.ClientID %>").click();
            }
        }
        function UploadFile3(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnUploadImage3.ClientID %>").click();
            }
        }
        function UploadFile4(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnUploadImage4.ClientID %>").click();
            }
        }
        function UploadFile5(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnUploadImage5.ClientID %>").click();
            }
        }
        function UploadFile6(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnUploadImage6.ClientID %>").click();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <div class="header pb-3">
    </div>
    <div class="container-fluid mt-1">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row">
            <div class="col-lg-3 col-md-3">
                <div class="row m-0">
                    <div class="card mr-2" style="min-height: 530px">

                        <div class="card-header">
                            <div class="row m-0 align-items-center">
                                <div class="col-md-10 ">
                                    <h5 class="mb-1">
                                        <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h3><span style="color: #ff5e00;">Background Images</span></h3></asp:Label>

                                    </h5>
                                </div>
                                <div class="col-md-2 text-right ">

                                    <asp:LinkButton ID="lbtnHelp" OnClick="lbtnHelp_Click" runat="server" ToolTip="View Instructions" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                    </asp:LinkButton>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-lg-12">
                            <div class="row mx-2 my-1">
                                <div class="col">

                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="Medium">Actions
                              
                                    </asp:Label>
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="false" Font-Size="Small"><br /> 1. Update Home Page Background Image.<br />
                                2. Delete existing Home Page Background Image.<br />
                                    </asp:Label>

                                </div>

                            </div>
                            <br />
                            <div class="row mx-2 my-1">
                                <div class="col">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="Medium">Please Note</asp:Label>
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="false" Font-Size="Small"><br />
                               1. You can add Maximum 6 Image at a time.<br />
                               2. Image size must be less than 2 MB.<br />
                                3. Height and Width of Image must be 640 x 1350.<br />
                                        4. The Image File  Should be JPG/PNG/JPEG Format.
                                    </asp:Label>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

            </div>
            <div class="col-lg-9 col-md-9">
                <div class="row m-0">
                    <div class="col-lg-12 col-md-12">
                        <div class="row">
                            <div class="col-md-4 pb-3">
                                <div class="card photocard">
                                    <asp:Image runat="server" ID="img1" />
                                    <div class="card-body" style="padding: 5px 5px !important;">
                                        <asp:Button ID="btnUploadImage1" runat="server" OnClick="btnUploadImage1_Click" CausesValidation="False" Style="display: none"
                                            TabIndex="18" Text="Upload Image" />
                                        <asp:FileUpload ID="FileUpload1" runat="server" Style="color: black; background-color: #eaf4ff; border: none; width: 80%;" CssClass="btn btn-sm btn-success "
                                            onchange="UploadFile1(this);" TabIndex="9" />
                                        <br />
                                        <asp:Label runat="server" Visible="true" ID="lblFileUpload1" CssClass="form-control-label">Upload Image</asp:Label>

                                        <asp:LinkButton ID="lbtnDeleteImage1" OnClick="lbtnDeleteImage1_Click" Style="float: right;" runat="server" class="btn btn-danger btn-sm"
                                            ToolTip="Click here to Delete Image"><i class="fa fa-trash"  title ="Click here to Delete Image"></i> </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4 pb-3">
                                <div class="card photocard">
                                    <asp:Image runat="server" ID="Img2" />
                                    <div class="card-body" style="padding: 5px 5px !important;">
                                        <asp:Button ID="btnUploadImage2" OnClick="btnUploadImage2_Click" runat="server" CausesValidation="False" Style="display: none"
                                            TabIndex="18" Text="Upload Image" />
                                        <asp:FileUpload ID="FileUpload2" runat="server" Style="color: black; background-color: #eaf4ff; border: none; width: 80%;" CssClass="btn btn-sm btn-success "
                                            onchange="UploadFile2(this);" TabIndex="9" />

                                        <br />
                                        <asp:Label runat="server" Visible="true" ID="lblFileUpload2" CssClass="form-control-label">Upload Image</asp:Label>

                                        <asp:LinkButton ID="lbtnDeleteImage2" OnClick="lbtnDeleteImage2_Click" Style="float: right;" runat="server" class="btn btn-danger btn-sm"
                                            ToolTip="Click here to Delete Image"><i class="fa fa-trash"  title ="Click here to Delete Image"></i> </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4 pb-3">
                                <div class="card photocard">
                                    <asp:Image runat="server" ID="Img3" />
                                    <div class="card-body" style="padding: 5px 5px !important;">
                                        <asp:Button ID="btnUploadImage3" OnClick="btnUploadImage3_Click" runat="server" CausesValidation="False" Style="display: none"
                                            TabIndex="18" Text="Upload Image" />
                                        <asp:FileUpload ID="FileUpload3" runat="server" Style="color: black; background-color: #eaf4ff; border: none; width: 80%;" CssClass="btn btn-sm btn-success "
                                            onchange="UploadFile3(this);" TabIndex="9" />

                                        <br />
                                        <asp:Label runat="server" ID="lblFileUpload3" CssClass="form-control-label">Upload Image </asp:Label>

                                        <asp:LinkButton ID="lbtnDeleteImage3" OnClick="lbtnDeleteImage3_Click" Style="float: right;" runat="server" class="btn btn-danger btn-sm"
                                            ToolTip="Click here to Delete Image"><i class="fa fa-trash"  title ="Click here to Delete Image"></i> </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 pb-3">
                                <div class="card photocard">
                                    <asp:Image runat="server" ID="Img4" />
                                    <div class="card-body" style="padding: 5px 5px !important;">
                                        <asp:Button ID="btnUploadImage4" OnClick="btnUploadImage4_Click" runat="server" CausesValidation="False" Style="display: none"
                                            TabIndex="18" Text="Upload Image" />
                                        <asp:FileUpload ID="FileUpload4" runat="server" Style="color: black; background-color: #eaf4ff; border: none; width: 80%;" CssClass="btn btn-sm btn-success "
                                            onchange="UploadFile4(this);" TabIndex="9" />

                                        <br />
                                        <asp:Label runat="server" ID="lblFileUpload4" CssClass="form-control-label"> Upload Image</asp:Label>

                                        <asp:LinkButton ID="lbtnDeleteImage4" OnClick="lbtnDeleteImage4_Click" Style="float: right;" runat="server" class="btn btn-danger btn-sm"
                                            ToolTip="Click here to Delete Image"><i class="fa fa-trash"  title ="Click here to Delete Image"></i> </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4 pb-3">
                                <div class="card photocard">
                                    <asp:Image runat="server" ID="Img5" />
                                    <div class="card-body" style="padding: 5px 5px !important;">
                                        <asp:Button ID="btnUploadImage5" OnClick="btnUploadImage5_Click" runat="server" CausesValidation="False" Style="display: none"
                                            TabIndex="18" Text="Upload Image" />
                                        <asp:FileUpload ID="FileUpload5" runat="server" Style="color: black; background-color: #eaf4ff; border: none; width: 80%;" CssClass="btn btn-sm btn-success "
                                            onchange="UploadFile5(this);" TabIndex="9" />

                                        <br />
                                        <asp:Label runat="server" ID="lblFileUpload5" CssClass="form-control-label"> Upload Image</asp:Label>

                                        <asp:LinkButton ID="lbtnDeleteImage5" OnClick="lbtnDeleteImage5_Click" Style="float: right;" runat="server" class="btn btn-danger btn-sm"
                                            ToolTip="Click here to Delete Image"><i class="fa fa-trash"  title ="Click here to Delete Image"></i> </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4 pb-3">
                                <div class="card photocard">
                                    <asp:Image runat="server" ID="Img6" />
                                    <div class="card-body" style="padding: 5px 5px !important;">
                                        <asp:Button ID="btnUploadImage6" OnClick="btnUploadImage6_Click" runat="server" CausesValidation="False" Style="display: none"
                                            TabIndex="18" Text="Upload Image" />
                                        <asp:FileUpload ID="FileUpload6" runat="server" Style="color: black; background-color: #eaf4ff; border: none; width: 80%;" CssClass="btn btn-sm btn-success "
                                            onchange="UploadFile6(this);" TabIndex="9" />

                                        <br />
                                        <asp:Label runat="server" ID="lblFileUpload6" CssClass="form-control-label"> Upload Image</asp:Label>

                                        <asp:LinkButton ID="lbtnDeleteImage6" OnClick="lbtnDeleteImage6_Click" Style="float: right;" runat="server" class="btn btn-danger btn-sm"
                                            ToolTip="Click here to Delete Image"><i class="fa fa-trash"  title ="Click here to Delete Image"></i> </asp:LinkButton>
                                    </div>
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

