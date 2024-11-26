<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Auth/sysAdmmaster.master" CodeFile="Thirdpartylogoconfig.aspx.cs" Inherits="Auth_Thirdpartylogoconfig" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        hr {
            margin-top: 0rem;
            margin-bottom: 5px;
            border: 0;
            border-top: 1px solid rgba(0,0,0,.1);
        }

        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .msg {
            display: none;
        }

        .error {
            color: red;
        }

        .success {
            color: green;
        }

        .form-control {
            padding: 2px 5px !important;
            height: 30px !important;
        }

        td, th {
            padding: 5px !important;
            vertical-align: top;
        }

        .wrapper {
            position: absolute;
            width: 100%;
            min-height: 100vh;
            background: #f1f1f1;
        }

        .divWaiting {
            position: fixed;
            background-color: White;
            opacity: 0.6;
            z-index: 2147483647 !important;
            overflow: auto;
            text-align: center;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            padding-top: 20%;
        }

        .grdtable {
            line-height: 20px;
            font-size: 8pt;
        }

        .card {
            border: 0;
            background: white;
            transition: 0.3s;
            position: relative;
        }

        .gridview {
            background-color: #fff;
            padding: 2px;
            margin: 2% auto;
            border: none;
        }

        .table2 td, .table2 th {
            padding: 3px 5px !important;
        }

        .gridview table td {
            border-top: none;
            padding: 2px;
            background-color: transparent;
        }

        .gridview a {
            margin: auto 0%;
            border-radius: 5px;
            border: 1px solid #444;
            padding: 3px 8px 3px 8px;
            color: #000;
            font-weight: bold;
            text-decoration: none;
            -o-box-shadow: 1px 1px 1px #111;
            -moz-box-shadow: 1px 1px 1px #111;
            -webkit-box-shadow: 1px 1px 1px #111;
            box-shadow: 1px 1px 1px #111;
        }

            .gridview a:hover {
                background-color: #1e8d12;
                color: #fff;
            }

        .gridview span {
            background-color: #ae2676;
            color: #fff;
            -o-box-shadow: 1px 1px 1px #111;
            -moz-box-shadow: 1px 1px 1px #111;
            -webkit-box-shadow: 1px 1px 1px #111;
            box-shadow: 1px 1px 1px #111;
            border-radius: 5px;
            padding: 5px 10px 5px 10px;
        }

        .gridview .table-striped tbody tr:nth-of-type(odd) {
            background-color: rgba(0,0,0,0);
        }

        .table td, .table th {
            padding: 10px;
            vertical-align: top;
            border-top: none;
        }

        .btn-orange {
            background: white;
            border: 1px solid orangered;
            color: orangered !important;
        }

        .bg-head {
            margin-bottom: 0px;
            font-size: 13pt;
        }

        .pForSide {
            text-align: left;
            font-size: 14px;
            line-height: 18px;
            margin-top: 4px;
        }

        a:hover {
            text-decoration: none;
        }
    </style>
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
        function UploadDeptlogo(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnDeotLogo.ClientID %>").click();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid" style="padding-top: 20px; padding-bottom: 30px;">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row">
            <div class="col-lg-12">
                <div class="card" style="min-height: 400px">
                    <div class="card-body" style="padding: 20px !important;">
                        <div class="row">
                            <div class="col-lg-5" style="padding: 0px 40px;">
                                <span style="font-size: 20pt; color: #ff5e00; font-weight: bold;">Please Note</span>
                                <hr />
                                <p>All  marked <span style="color: red; font-weight: bold;">*</span>  fields are mandatory</p>
                                <h4 class="bg-head" style="text-align: left">Third Party Logo</h4>
                                <p class="pForSide">
                                    1. Image to be uploaded should be only in <span style="font-weight: bold;">.png</span>
                                    format with transaprent background.<br />
                                    2.  Logo size should not exceed <span style="font-weight: bold;">100 KB</span><br />
                                    3.  resolution of logo must be <span style="font-weight: bold;">60px X 60px</span>
                                </p>
                            </div>
                            <div class="col-lg-7" style="border-left: 1px solid orange;padding: 0px 40px;line-height: 45px;">
                                <div class="row mx-2 my-1">
                                    <div class="col-lg-6 text-right">
                                        <asp:Label runat="server" CssClass="form-control-label mx-3">Agent Type <span class="text-warning">*</span></asp:Label>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:DropDownList ID="ddlagtype" runat="server" CssClass="form-control mt-2">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row mx-2 my-1">
                                    <div class="col-md-6 text-right">
                                        <asp:Label runat="server" CssClass="form-control-label mx-3">Logo <span class="text-warning">*</span></asp:Label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnDeotLogo" runat="server" OnClick="btnDeotLogo_Click" CausesValidation="False" CssClass="form-control form-control-sm"
                                            Style="display: none" TabIndex="18" Text="Upload Image" Width="80px" />
                                        <asp:Label runat="server" ID="lbldept" CssClass="form-control-label"></asp:Label>
                                        <asp:FileUpload ID="fuDeptLogo" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-sm btn-success "
                                            onchange="UploadDeptlogo(this);" TabIndex="9" />
                                        <asp:Image ID="ImgDepartmentLogo" runat="server" Visible="false" Style="border-width: 0px; height: 50px; width: 50px; border: 2px solid #eaf4ff;" />
                                    </div>
                                </div>
                                <div class="row m-0 align-items-center py-1 px-3">
                                    <div class="col-md-12 col-lg-12 text-center pt-3">
                                        <asp:LinkButton ID="lbtnSave" runat="server" OnClick="lbtnSave_Click" ToolTip="Click here to Save Third Party Logo Configuration" OnClientClick="return ShowLoading()" CssClass="btn btn-success"><i class="fa fa-save"></i> Save</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnReset" runat="server" OnClick="lbtnReset_Click" ToolTip="Click here to Reset Third Party Logo Configuration" OnClientClick="return ShowLoading()" CssClass="btn btn-danger"><i class="fa fa-undo"></i> Reset</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <hr />
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
        <div class="row">
            <cc1:ModalPopupExtender ID="mperror" runat="server" PopupControlID="pnlerror" CancelControlID="lbtnclose1"
                TargetControlID="Button1" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlerror" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Please Check
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblerrormsg" runat="server" Text="Please Check entered values."></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnclose1" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> OK </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
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

    </div>

</asp:Content>


