<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/PortalAdmmaster.master" AutoEventWireup="true" CodeFile="PAdminPhotoGallery.aspx.cs" Inherits="Auth_PAdminPhotoGallery" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function UploadImage1(fileUpload) {

            if (fileUpload.value != '') {

                document.getElementById("<%=btnUploadImage.ClientID %>").click();

            }
        }
    </script>
    <style>
        .container1 {
            position: relative;
            height: 238px;
        }

        .image {
            opacity: 1;
            display: block;
            width: 100%;
            height: auto;
            transition: .5s ease;
            backface-visibility: hidden;
            border-radius: 4px;
        }

        .middle {
            transition: .5s ease;
            opacity: 0;
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            -ms-transform: translate(-50%, -50%);
            text-align: center;
        }

        .container1:hover .image {
            opacity: 0.3;
        }

        .container1:hover .middle {
            opacity: 1;
        }



        .overlay {
            position: absolute;
            bottom: 0;
            background: rgb(0, 0, 0);
            background: rgba(0, 0, 0, 0.5); /* Black see-through */
            color: #f1f1f1;
            width: 100%;
            transition: .5s ease;
            opacity: 0;
            color: white;
            font-size: 20px;
            padding: 15px;
            text-align: center;
        }

        .container1:hover .overlay {
            opacity: 1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="header pb-3">
        <asp:HiddenField ID="HiddenField1" runat="server" />
    </div>
    <div class="container-fluid">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row">
            <div class="col-lg-3 col-md-3">
                <asp:Panel runat="server" ID="pnlctgryHeading" Style="width: 100%">
                    <div class="row m-0">
                        <div class="card " style="min-height: 500px">

                            <div class="card-header">
                                <asp:LinkButton ID="lbtnCtgry" OnClick="lbtnCtgry_Click" class="btn btn-primary" OnClientClick="return ShowLoading()" ToolTip="Click here to Add/Update Album of photo gallary" Style="width: 100%;"
                                    runat="server">
                    <i class="fa fa-list" title ="Click here to Add/Update Album of photo gallary"></i> Album</asp:LinkButton>
                                <asp:LinkButton ID="lbtnPhotoupload" OnClick="lbtnPhotoupload_Click" class="btn btn-outline-primary mt-2" OnClientClick="return ShowLoading()" ToolTip="Click here to Add/Update Images for photo gallary" Style="width: 100%;" runat="server">
                     <i class="fa fa-image" title ="Click here to Add/Update Images for photo gallary"></i> Photo Upload</asp:LinkButton>
                                <asp:LinkButton visible="false" ID="lbtnVideoUpload" OnClick="lbtnVideoUpload_Click" class="btn btn-outline-primary mt-2" OnClientClick="return ShowLoading()" ToolTip="Click here to Add/Update Video Links" Style="width: 100%;" runat="server">
                     <i class="fa fa-camera" title ="Click here to Add/Update Images for photo gallary"></i> Video Upload</asp:LinkButton>
                            </div>
                            <div class="col-md-12 col-lg-12">
                                <div class="row mx-2 my-1">
                                    <div class="col">

                                        <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="Medium">Actions
                              
                                        </asp:Label>
                                        <asp:Label runat="server" CssClass="form-control-label" Font-Bold="false" Font-Size="Small"><br /> 1. Add New Photos.<br />
                               2. Delete existing Photos.<br />
                                3. Add/Update Album of Image Gallery.
                                        </asp:Label>

                                    </div>

                                </div>
                                <br />
                                <div class="row mx-2 my-1">
                                    <div class="col">
                                        <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="Medium">Please Note</asp:Label>
                                        <asp:Label runat="server" CssClass="form-control-label" Font-Bold="false" Font-Size="Small"><br />
                               1. You can add Maximum 8 photos in a Album.<br />
                               2. First you have to create Album of Image Gallery inside Album Section.<br />
                               3. Then you can add photos for that particular Album inside Photo Upload Section.
                                            4. Phtographs of only dimension 1200X720 are allowed
                                        </asp:Label>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" Visible="false" ID="pnlphotoHeading" Style="width: 100%">
                    <div class="row m-0">
                        <div class="card " style="min-height: 500px">

                            <div class="card-header">
                                <asp:LinkButton ID="lbtnCategoryHeading" OnClick="lbtnCtgry_Click" class="btn btn-outline-primary" OnClientClick="return ShowLoading()" ToolTip="Click here to Add/Update Album of photo gallary" Style="width: 100%;"
                                    runat="server">
                    <i class="fa fa-list" title ="Click here to Add/Update Category of photo gallary"></i> Album</asp:LinkButton>
                                <asp:LinkButton ID="lbtnPhotoHeading" OnClick="lbtnPhotoupload_Click" class="btn btn-primary mt-2" OnClientClick="return ShowLoading()" ToolTip="Click here to Add/Update Images for photo gallary" Style="width: 100%;" runat="server">
                     <i class="fa fa-image" title ="Click here to Add/Update Images for photo gallary"></i> Photo Upload</asp:LinkButton>
                                <asp:LinkButton ID="lbtnVideoHeading" visible="false" OnClick="lbtnVideoUpload_Click" class="btn btn-outline-primary mt-2" OnClientClick="return ShowLoading()" ToolTip="Click here to Add/Update Video Links" Style="width: 100%;" runat="server">
                     <i class="fa fa-camera" title ="Click here to Add/Update Images for photo gallary"></i> Video Upload</asp:LinkButton>

                            </div>
                            <div class="col-md-12 col-lg-12">
                                <div class="row mx-2 my-1">
                                    <div class="col">

                                        <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="Medium">Actions
                              
                                        </asp:Label>
                                        <asp:Label runat="server" CssClass="form-control-label" Font-Bold="false" Font-Size="Small"><br /> 1. Add New Photos.<br />
                               2. Delete existing Photos.<br />
                                3. Add/Update Category of Image Gallery.
                                        </asp:Label>

                                    </div>

                                </div>
                                <br />
                                <div class="row mx-2 my-1">
                                    <div class="col">
                                        <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="Medium">Please Note</asp:Label>
                                        <asp:Label runat="server" CssClass="form-control-label" Font-Bold="false" Font-Size="Small"><br />
                               1. You can add Maximum 8 photos in a Category.<br />
                               2. First you have to create Category of Image Gallery inside Album Section.<br />
                               3. Then you can add photos for that particular Album inside Photo Upload Section.
                                        </asp:Label>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div class="col-lg-9 col-md-9">
                <asp:Panel runat="server" ID="pnlctgry" Style="width: 100%">
                    <div class="row m-0">
                        <div class="col-lg-12 col-md-12 order-xl-1 ">
                            <div class="card" style="min-height: 500px">
                                <div class="col-lg-12 col-md-12">
                                    <div class="card-header">
                                        <div class="row m-0 align-items-center">
                                            <div class="col-md-10 ">
                                                <h3 class="mb-1">
                                                    <asp:Label ID="lblCategory" runat="server" CssClass="form-control-label" Font-Bold="true"><h2>Album</h2></asp:Label>

                                                </h3>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <h4>
                                                    <asp:LinkButton ID="lbtnCategoryhelp" runat="server" OnClick="lbtnCategoryhelp_Click" ToolTip="View Instructions" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                                    </asp:LinkButton>
                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-2 m-0">
                                        <div class="col-lg-6">
                                            <div class="row ml-2 m-0 mt-2">
                                                <div class="col-lg-12  pr-0">

                                                    <asp:Label ID="lblSaveCategory" runat="server" CssClass="form-control-label" Font-Bold="true"><h3>Add New Album</h3></asp:Label>
                                                    <asp:Label ID="lblUpdateCategory" runat="server" Visible="false" Font-Bold="true" CssClass="form-control-label"><h3>Update New Category</h3></asp:Label>

                                                </div>
                                            </div>
                                            <div class="row mt-2">
                                                <div class="col-lg-4 text-right pr-0">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Album Name<span class="text-warning">*</span></asp:Label>
                                                </div>
                                                <div class="col-lg-6">
                                                    <asp:TextBox ID="tbCategoryName" placeholder="Max length 100 chars" ToolTip="English Name" CssClass="form-control form-control-sm" runat="server" autocomplete="off" MaxLength="100"
                                                        Text=""></asp:TextBox>

                                                    <cc1:FilteredTextBoxExtender ID="ajaxFTCategoryName" ValidChars="/ * . " runat="server" FilterType="LowercaseLetters,UppercaseLetters,Numbers,Custom"
                                                        TargetControlID="tbCategoryName" />

                                                </div>

                                            </div>
                                            <div class="row mt-2">
                                                <div class="col-lg-4 text-right pr-0">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Category Name(Local)<span class="text-warning">*</span></asp:Label>
                                                </div>
                                                <div class="col-lg-6">
                                                    <asp:TextBox ID="tbCategoryNameLocal" placeholder="Max length 100 chars" ToolTip="Hindi Name" CssClass="form-control form-control-sm" runat="server" autocomplete="off" MaxLength="100"
                                                        Text=""></asp:TextBox>
                                                    <%--                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" ValidChars="/ * . " runat="server" FilterType="LowercaseLetters,UppercaseLetters,Numbers,Custom"
                                                        TargetControlID="tbCategoryNameLocal" />--%>
                                                </div>
                                            </div>
                                            <%--<div class="row mt-2">
                                                <div class="col-lg-4 text-right pr-0">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Active<span class="text-warning">*</span></asp:Label>
                                                </div>
                                                <div class="col-lg-6">
                                                    <asp:DropDownList ID="ddlstatus" ToolTip="select status" CssClass="form-control form-control-sm" runat="server" AutoPostBack="true" TabIndex="2"
                                                        Style="font-size: 10pt;">
                                                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>--%>
                                            <div class="row mt-4">
                                                <div class="col-lg-12 text-center">
                                                    <asp:LinkButton ID="lbtnSaveCategory" OnClick="lbtnSaveCategory_Click" runat="server" class="btn btn-success"
                                                        ToolTip="Click here to Add Category"> <i class="fa fa-save" ></i> Save</asp:LinkButton>
                                                    <%--<asp:LinkButton ID="lbtnUpdateCategory" OnClick="lbtnUpdateCategory_Click" Visible="false" runat="server" class="btn btn-primary"
                                                        ToolTip="Click here to Update Category"> <i class="fa fa-undo" ></i> Update</asp:LinkButton>--%>
                                                    <asp:LinkButton ID="lbtnPStatusreset" OnClick="lbtnPStatusreset_Click" runat="server" class="btn btn-danger"
                                                        ToolTip="Click here to Reset Category"> <i class="fa fa-undo" ></i> Reset</asp:LinkButton>

                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-lg-6 pl-3" style="border-left: 1px solid;">
                                            <div class="row ml-2 m-0 mt-2">
                                                <div class="col-lg-12  pr-0">

                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h3>Categorys List</h3></asp:Label>

                                                </div>
                                            </div>
                                            <asp:GridView ID="gvCategoryList" OnRowCommand="gvCategoryList_RowCommand" PageSize="5" OnPageIndexChanging="gvCategoryList_PageIndexChanging" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                Visible="true" CssClass="table table-hover table-striped" DataKeyNames="category_id">
                                                <Columns>
                                                   
                                                    <asp:BoundField HeaderStyle-Font-Bold="true" DataField="category_name" HeaderText="Category" />
                                                    <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' runat="server" ToolTip="Click to delete" CssClass="btn btn-sm btn-warning" CommandName="gvEdit"><i class="fa fa-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle BackColor="#ececec" Font-Size="9pt" />
                                                <PagerStyle HorizontalAlign="Left" CssClass="gridview" />
                                            </asp:GridView>
                                            <asp:Panel runat="server" ID="pnlCategoryList" Visible="false" CssClass="text-center" Width="100%">
                                                <p class="text-center" style="font-size: 25px; font-weight: bold; color: #e3e3e3; margin-top: 50px;">
                                                    Please add catgory
                                                </p>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlphoto" Visible="false" Style="width: 100%">
                    <div class="row m-0">
                        <div class="col-lg-12 col-md-12 order-xl-1 ">
                            <div class="card" style="min-height: 500px">
                                <div class="col-lg-12 col-md-12">
                                    <div class="card-header">
                                        <div class="row m-0 align-items-center">
                                            <div class="col-md-10 ">
                                                <h3 class="mb-1">
                                                    <asp:Label ID="lblPhotoUpload" runat="server" CssClass="form-control-label" Font-Bold="true"><h2>Photo Upload</h2></asp:Label>

                                                </h3>
                                            </div>
                                            <div class="col-md-2 text-right">
                                                <h4>
                                                    <asp:LinkButton ID="lblViewInstructions" runat="server" ToolTip="View Instructions" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                                    <i class="fa fa-info"></i>
                                                    </asp:LinkButton>

                                                </h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-2 m-0">
                                        <div class="col-lg-6">
                                            <div class="row ml-2 m-0 mt-2">
                                                <div class="col-lg-12  pr-0">

                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h3>Add New Image</h3></asp:Label>

                                                </div>
                                            </div>
                                            <div class="row m-0 mt-2">
                                                <div class="col-lg-4 text-right pr-0">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Category Name<span class="text-warning">*</span></asp:Label>
                                                </div>
                                                <div class="col-lg-6">
                                                    <asp:DropDownList ID="ddlCategoryName" runat="server" ToolTip=" select Category Name" AutoPostBack="false"
                                                        Style="font-size: 10pt;" CssClass="form-control form-control-sm">
                                                        <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                            <div class="row m-0 mt-2">
                                                <div class="col-lg-4 text-right pr-0">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Title<span class="text-warning">*</span></asp:Label>
                                                </div>
                                                <div class="col-lg-6">
                                                    <asp:TextBox ID="tbTitle" placeholder="Max length 100 char." CssClass="form-control form-control-sm" ToolTip=" Enter a New Title" runat="server" MaxLength="100" autocomplete="off"
                                                        Text=""></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ajaxFttbTitle" ValidChars="/ * . " runat="server" FilterType="LowercaseLetters,UppercaseLetters,Numbers,Custom"
                                                        TargetControlID="tbTitle" />
                                                </div>
                                            </div>
                                            <div class="row m-0 mt-2">
                                                <div class="col-lg-4 text-right pr-0">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Title(Local)<span class="text-warning">*</span></asp:Label>
                                                </div>
                                                <div class="col-lg-6">
                                                    <asp:TextBox ID="tbImageTitleLocal" placeholder="Max length 100 char." CssClass="form-control form-control-sm" ToolTip=" Enter a New Title" runat="server" MaxLength="100" autocomplete="off"
                                                        Text=""></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" ValidChars="/ * . " runat="server" FilterType="LowercaseLetters,UppercaseLetters,Numbers,Custom"
                                                        TargetControlID="tbTitle" />
                                                </div>
                                            </div>
                                            <div class="row m-0 mt-2">
                                                <div class="col-lg-4 text-right pr-0">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Upload Image<span class="text-warning">*</span></asp:Label>
                                                </div>
                                                <div class="col-lg-6">
                                                    <asp:FileUpload ID="FileUploadImage" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-sm btn-success "
                                                        TabIndex="9" />
                                                    <%--  <asp:FileUpload ID="FileUploadImage" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-sm btn-success "
                                                        onchange="UploadImage1(this);" 
                                                        TabIndex="9" />--%>
                                                    <asp:Button ID="btnUploadImage" runat="server" CausesValidation="False" Style="display: none" Text="" Width="80px" />
                                                    <asp:LinkButton ID="lbtnfuAttach1Clear" runat="server" ForeColor="Green" Visible="False" OnClick="lbtnfuAttach1Clear_Click"> <i class="fa fa-times-circle-o" style="color:Red"></i>  </asp:LinkButton>
                                                    <asp:Label ID="lblfuFileName" runat="server" Visible="false"></asp:Label>

                                                </div>
                                            </div>

                                            <div class="row m-0 mt-4">
                                                <div class="col-lg-12 text-center">
                                                    <asp:LinkButton ID="lbtnSaveImage" OnClick="lbtnSaveImage_Click" runat="server" class="btn btn-success"
                                                        CommandName="S" ToolTip="Click here to Add/Update Images for photo gallary"> <i class="fa fa-save" ></i> Save</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnResetImage" OnClick="lbtnResetImage_Click" runat="server" class="btn btn-danger"
                                                        ToolTip="Click here to Reset Images for photo gallary"> <i class="fa fa-undo" ></i> Reset</asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-lg-6" style="border-left: 1px solid;">
                                            <div class="row ml-2 m-0 mt-2">

                                                <div class="col-lg-6  pr-0">

                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h3>Images List</h3></asp:Label>

                                                </div>
                                                <div class="col-lg-6  pr-0">
                                                    <asp:DropDownList ID="ddlImageList" runat="server" ToolTip=" select Image Category" OnSelectedIndexChanged="ddlImageList_SelectedIndexChanged" AutoPostBack="true"
                                                        Style="font-size: 10pt;" CssClass="form-control form-control-sm">
                                                        <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12">
                                                <div class="row">
                                                    <asp:Repeater ID="rptImageList" runat="server" OnItemDataBound="rptImageList_ItemDataBound" OnItemCommand="rptImageList_ItemCommand">
                                                        <ItemTemplate>
                                                            <div class="col-lg-6">
                                                                <div class="container1 mt-2">
                                                                    <asp:Image ID="img" runat="server" Style="height: 100%; width: 100%;" CssClass="image" />
                                                                    <asp:HiddenField runat="server" ID="hdnphotoid" Value='<%#Eval("photo_id")%>' />
                                                                    <asp:HiddenField runat="server" ID="hdcategoryid" Value='<%#Eval("category_id")%>' />
                                                                    <asp:HiddenField runat="server" ID="hdphotourl" Value='<%#Eval("photo_url")%>' />
                                                                    <div class="overlay">
                                                                        <asp:Label ID="photoName" runat="server"><%#Eval("photo_name")%></asp:Label>
                                                                    </div>
                                                                    <div class="middle">
                                                                        <div class="text">
                                                                            <asp:LinkButton ID="lbtndelete" runat="server" CssClass="btn btn-warning btn-sm" CommandName="deletephoto" ToolTip="Click here delete photo"><i class="fa fa-trash" title="Click here delete photo"></i> </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                                <asp:Panel runat="server" ID="pnlPhotoNoRecord" Visible="false" CssClass="text-center" Width="100%">
                                                    <p class="text-center" style="font-size: 32px; font-weight: bold; color: #e3e3e3; margin-top: 50px;">
                                                        No Record Available
                                                    </p>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </asp:Panel>

            </div>
        </div>
    </div>
    <div class="row">
        <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
            CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed; display: none;">
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

