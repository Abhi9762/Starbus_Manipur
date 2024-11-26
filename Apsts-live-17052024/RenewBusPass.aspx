<%@ Page Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="RenewBusPass.aspx.cs" Inherits="RenewBusPass" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../assets/js/jquery-n.js"></script>
    <link href="../assets/css/travelllerStepProgressBar.css" rel="stylesheet" />
    <link href="../assets/css/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript">



        function UploadDoc(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnUploadDoc.ClientID %>").click();
            }
        }

        function UploadAddDoc(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnAddproof.ClientID %>").click();


            }
        }

    </script>

    <script type="text/javascript">
        function UploadImageWeb(fileUpload) {
            if ($('#ImgWebPortal').value != '') {
                document.getElementById("<%=btnUploadWebPortal.ClientID %>").click();

            }
        }

    </script>
    <style type="text/css">
        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }
    </style>
    <style type="text/css">
        .rbl label {
            margin-left: 7px;
            margin-right: 20px;
            vertical-align: bottom;
            font-size: 10pt;
        }

        .text-bold {
            font-weight: bold;
        }
    </style>


    <script type="text/javascript">

        $(document).ready(function () {

            var currDate = new Date().getDate();
            var preDate = new Date(new Date().setDate(currDate - 1));
            var todayDate = new Date(new Date().setDate(currDate));

            $('[id*=tbDate]').datepicker({
                endDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            })

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container" style="margin-top: 20px; min-height: 100vh;">
        <div class="row">
            <div class="col-lg-12">

                <div class="row">
                    <div class="col-lg-2">
                        <div class="card">
                            <div class="card-header">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <h5 class="text-danger">Please Note</h5>
                                    </div>
                                </div>

                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-lg-12 form-control-label">
                                        <asp:Label runat="server">1. Search your pass to check if it is renewable or not.</asp:Label>
                                        <br />
                                        <asp:Label runat="server">2. If is it renewable then check if you can you are eligible or not.</asp:Label>
                                        <br />
                                        <asp:Label runat="server">3. Please Keep working 10 digit mobile number handy</asp:Label>
                                        <br />
                                        <br />


                                        <asp:Panel ID="pnlinstruction" runat="server" Visible="false">


                                            <asp:Label runat="server" CssClass="text-bold h4">Instructions</asp:Label>
                                            <br />


                                            <asp:Label ID="lblEligibility" runat="server"></asp:Label>
                                            <asp:Label ID="lblIDDocuments" runat="server"></asp:Label>
                                            <asp:Label ID="lbladdDocuments" runat="server"></asp:Label>
                                            <asp:Label ID="lblChargesApplicable" runat="server"></asp:Label>
                                            <asp:Label ID="lblState" runat="server"></asp:Label>
                                            <asp:Label ID="lblservicetype" runat="server"></asp:Label>
                                            <asp:Label ID="lblvalidity" runat="server"></asp:Label>
                                        </asp:Panel>

                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="col-lg-10">
                        <asp:Panel ID="pnlPassRenewDetails" runat="server" Visible="true">
                            <div class="card" style="min-height: 50vh">
                                <div class="card-header">
                                    <asp:Panel ID="pnlentry" Visible="true" runat="server">
                                        <div class="row">
                                            <div class="col-lg-3">
                                                <asp:Label runat="server" Text="Pass Number" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>

                                                <asp:TextBox ID="tbPassnNo" runat="server" CssClass="form-control" placeholder="Pass Number" MaxLength="15" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Transaction Ref. No." Style="height: 42px; text-transform: uppercase;"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender runat="server" FilterType="UppercaseLetters,LowercaseLetters,Custom,Numbers" TargetControlID="tbPassnNo" />

                                            </div>
                                            <div class="col-lg-3">
                                                <asp:Label runat="server" Text="Date Of Birth" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>
                                                 <div class="input-group input-group-alternative">
                                                            <div class="input-group date">
                                        <asp:TextBox ID="tbDate" ToolTip="Enter D.O.B" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="CalendarExtender1" runat="server" FilterType="Numbers,Custom" TargetControlID="tbDate" ValidChars="/" />
                                    </div>
                                                    </div>

                                            </div>
                                            <div class="col-lg-3">
                                                <asp:Label runat="server" Text="Security Image" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>
                                                <div class="input-group">
                                       <img alt="Visual verification" title="Please enter the security code as shown in the image."
                                    src="CaptchaImage.aspx" style="width: 70%; border: 1px solid #e6e6e6; height: 35px; border-radius: 5px;" />
                                        <asp:LinkButton runat="server" ID="lbtnRefresh" OnClick="lbtnRefresh_Click" CssClass=" btn btn-primary"  Style="height: 36px;">
                                                            <i class="fa fa-sync-alt" ></i></asp:LinkButton>
                                    </div>

                                            </div>
                                            <div class="col-lg-3">
                                                <asp:Label runat="server" Text="Security Text" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>
                                                <div class="input-group">
                                                    <asp:TextBox ID="tbcaptchacode" runat="server" placeholder="Enter Text" autocomplete="off" class="form-control" MaxLength="6" Style="height: 42px; text-transform: uppercase;"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 text-center" style="padding-top: 26px;">
                                                <asp:LinkButton ID="lbtnSearch" runat="server" OnClick="lbtnSearch_Click" CssClass="btn btn-warning"><i class="fa fa-search"></i> Search</asp:LinkButton>
                                                <asp:LinkButton ID="lbtnrest" runat="server" OnClick="lbtnrest_Click" CssClass="btn btn-danger"><i class="fa fa-undo"></i> Reset</asp:LinkButton>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="card-body pt-2">
                                    <asp:Panel ID="pnlPassDetail" Visible="false" runat="server">
                                        <div class="row">
                                            <div class="col-lg-4">
                                                <asp:Label runat="server" Text="Bus Pass Type" CssClass="form-control-label"></asp:Label><br />

                                                <asp:Label ID="lblBusPassType" runat="server" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                            </div>

                                        </div>
                                        <hr />
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <h6 class="text-bold">Personal Details</h6>
                                                <div class="row my-2">
                                                    <div class="col-lg-4">
                                                        <asp:Label runat="server" Text="Name" CssClass="form-control-label text-muted font-weight-normal"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblName" runat="server" CssClass="lbld"></asp:Label>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <asp:Label runat="server" Text="Gender" CssClass="form-control-label text-muted font-weight-normal"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblGender" runat="server" CssClass="lbld"></asp:Label>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <asp:Label runat="server" Text="Date of Birth" CssClass="form-control-label text-muted font-weight-normal"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblDOB" runat="server" CssClass="lbld"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row my-2">
                                                    <div class="col-lg-4">
                                                        <asp:Label runat="server" Text="Father Name" CssClass="form-control-label text-muted font-weight-normal"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblfname" runat="server" CssClass="lbld"></asp:Label>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <asp:Label runat="server" Text="Service Type" CssClass="form-control-label text-muted font-weight-normal"></asp:Label><br />

                                                        <asp:Label ID="lblServiceTypeName" runat="server" CssClass="lbld"></asp:Label>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <h6 class="text-bold">Pass Requested For</h6>
                                                <div class="row my-2">
                                                    <div class="col-lg-6">
                                                        <asp:Label runat="server" Text="Route" CssClass="form-control-label text-muted font-weight-normal"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblRoute" runat="server" CssClass="lbld"></asp:Label>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <asp:Label runat="server" Text="Stations" CssClass="form-control-label text-muted font-weight-normal"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblFrom" runat="server" CssClass="lbld"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row ">
                                            <div class="col-lg-12">
                                                <h6 class="text-bold">Contact Details</h6>
                                                <div class="row my-2">
                                                    <div class="col-lg-4">
                                                        <asp:Label runat="server" Text="Mobile No." CssClass="form-control-label text-muted font-weight-normal"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblMobileNo" runat="server" CssClass="lbld"></asp:Label>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <asp:Label runat="server" Text="Email" CssClass="form-control-label text-muted font-weight-normal"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblEmail" runat="server" CssClass="lbld"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row my-2">
                                                    <div class="col-lg-4">
                                                        <asp:Label runat="server" Text="State" CssClass="form-control-label text-muted font-weight-normal"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblstateName" runat="server" CssClass="lbld"></asp:Label>

                                                    </div>
                                                    <div class="col-lg-4">
                                                        <asp:Label runat="server" Text="District" CssClass="form-control-label text-muted font-weight-normal"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblDistrict" runat="server" CssClass="lbld"></asp:Label>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <asp:Label runat="server" Text="City" CssClass="form-control-label text-muted font-weight-normal"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblCity" runat="server" CssClass="lbld"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row my-2">
                                                    <div class="col-lg-8">
                                                        <asp:Label runat="server" Text="Address" CssClass="form-control-label text-muted font-weight-normal"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblAddress" runat="server" CssClass="lbld" Text="N/A"></asp:Label>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <asp:Label runat="server" Text="Pincode" CssClass="form-control-label text-muted font-weight-normal"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblPincode" runat="server" CssClass="lbld" Text="N/A"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row">
                                            <h6 class="text-bold">Validity</h6>
                                            <div class="col-lg-12">
                                                <div class="row my-2">
                                                    <div class="col-lg-4">
                                                        <asp:Label runat="server" Text="From" CssClass="form-control-label text-muted font-weight-normal"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblValidityFrom" runat="server" CssClass="lbld"></asp:Label>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <asp:Label runat="server" Text="Upto" CssClass="form-control-label text-muted font-weight-normal"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblValidTo" runat="server" CssClass="lbld"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row my-2">
                                            <div class="col-lg-6" style="border-right: 1px solid;">
                                                <asp:Panel ID="pnlDocument" runat="server" Visible="false">
                                                    <div class="row py-2">
                                                        <div class="col-lg-12">
                                                            <h6 class="text-bold">Document</h6>
                                                            <div class="row my-2">
                                                                <div class="col-lg-6" style="border-right: 1px solid">
                                                                    <asp:Panel ID="pnlIDProofNew" runat="server">
                                                                        <div class="row">
                                                                            <div class="col-lg-12">
                                                                                <asp:Label runat="server" Text="ID Proof" CssClass="form-control-label"></asp:Label>
                                                                            </div>
                                                                            <div class="col-lg-12">
                                                                                <asp:RadioButtonList ID="rbtnIdProof" AutoPostBack="true" OnSelectedIndexChanged="rbtnIdProof_SelectedIndexChanged" GroupName="radio" CssClass="rbl" RepeatDirection="Horizontal" runat="server">
                                                                                </asp:RadioButtonList>
                                                                                <asp:Panel ID="pnlIdProof" runat="server" Visible="false">
                                                                                    <div class="col-lg-12">
                                                                                        <label style="font-weight: normal; color: Red; font-size: 8pt; margin: 0px;">
                                                                                            PDF file only(Max. Size 1MB)</label><br />
                                                                                        <asp:Button ID="btnUploadDoc" runat="server" OnClick="btnUploadDoc_Click" CausesValidation="False" Style="display: none" Text="" />
                                                                                        <asp:FileUpload ID="fileUpload" runat="server" CssClass="file-control" onchange="UploadDoc(this);" accept="application/pdf" />
                                                                                        <asp:Label runat="server" ID="lblIDpdf" CssClass="col-form-label control-label" Style="font-size: 12px; font-weight: normal;"></asp:Label>
                                                                                    </div>
                                                                                </asp:Panel>
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </div>
                                                                <div class="col-lg-6">
                                                                    <asp:Panel ID="pnlAddProofNew" runat="server">
                                                                        <div class="row">
                                                                            <div class="col-lg-12">
                                                                                <asp:Label runat="server" Text="Address Proof" CssClass="form-control-label"></asp:Label>
                                                                            </div>
                                                                            <div class="col-lg-12">
                                                                                <asp:RadioButtonList ID="rbtnAddressProof" AutoPostBack="true" OnSelectedIndexChanged="rbtnAddressProof_SelectedIndexChanged" GroupName="radio" CssClass="rbl" RepeatDirection="Horizontal" runat="server">
                                                                                </asp:RadioButtonList>
                                                                                <asp:Panel ID="pnlAddProof" runat="server" Visible="false">
                                                                                    <div class="col-lg-12">
                                                                                        <label style="font-weight: normal; color: Red; font-size: 8pt; margin: 0px;">
                                                                                            PDF file only(Max. Size 1MB)</label><br />
                                                                                        <asp:Button ID="btnAddproof" runat="server" OnClick="btnAddproof_Click" CausesValidation="False" Style="display: none" Text="" />
                                                                                        <asp:FileUpload ID="fileaddproof" runat="server" CssClass="file-control" onchange="UploadAddDoc(this);" accept="application/pdf" />
                                                                                        <asp:Label runat="server" ID="lbladd" CssClass="col-form-label control-label" Style="font-size: 12px; font-weight: normal;"></asp:Label>

                                                                                    </div>
                                                                                </asp:Panel>
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                            <div class="col-lg-6">
                                                <asp:Panel ID="pnlPhoto" runat="server" Visible="false">
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <h6 class="text-bold">Photo</h6>
                                                            <div class="row mt-1 m-0">
                                                                <div class="col-lg-5">
                                                                    <asp:Button ToolTip="Upload Web Portal Image" ID="btnUploadWebPortal" OnClick="btnUploadWebPortal_Click" runat="server" CausesValidation="False" CssClass="form-control form-control-sm"
                                                                        Style="display: none" TabIndex="18" Text="Upload Image" accept=".png,.jpg,.jpeg,.gif" Width="80px" />
                                                                    <asp:FileUpload ID="FileWebPortal" onchange="UploadImageWeb(this);" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-sm btn-success" TabIndex="9" />
                                                                    <asp:Image ID="ImgWebPortal" onchange="UploadImageWeb(this);" runat="server" Visible="false" Style="border-width: 0px; height: 50px; width: 50px; border: 2px solid #eaf4ff;" />
                                                                    <asp:LinkButton ID="lbtncloseWebImage" runat="server" OnClick="lbtncloseWebImage_Click" Style="font-size: 5pt; border-radius: 25px; margin-bottom: 22pt" Visible="false" CssClass="btn btn-sm btn-danger"><i class="fa fa-times"></i></asp:LinkButton><br />

                                                                    <label id="lblwrongimage" runat="server" style="font-size: 7pt; color: Red; line-height: 12px;">
                                                                        Image size will be less then 1 MB<br />
                                                                        (Only .JPG, .PNG, .JPEG)</label>
                                                                    <br />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>

                                        <div class="row my-3">
                                            <div class="col-lg-3 offset-9 text-right">
                                                <asp:LinkButton ID="lbtnSave" runat="server" OnClick="lbtnSave_Click" CssClass="btn btn-success" data-toggle="tooltip" data-placement="bottom" title="Click to Proceed"><i class="fa fa-save"></i> Submit & Proceed</asp:LinkButton>

                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlPassNorecord" runat="server" Visible="true">
                                        <div class="row">
                                            <div class="col-md-12 busListBox text-center" style="color: #9d9797; padding-top: 50px; padding-bottom: 50px; font-weight: bold; font-size: 20px;">
                                                <asp:Label ID="lblmsg" runat="server" Text="Enter Valid Pass Number, Date of Birth and security text.<br /> For Renew Your Pass"></asp:Label>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation" CancelControlID="lbtnNoConfirmation"
                TargetControlID="Button4" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlConfirmation" runat="server" Style="width: 500px !important">
                <div class="card">
                    <div class="card-header">
                        <h5 class="card-title text-left mb-0">Please Confirm
                        </h5>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                        <div style="margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnYesConfirmation" runat="server" OnClick="lbtnYesConfirmation_Click" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                            <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                        </div>
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button4" runat="server" Text="" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mperror" runat="server" PopupControlID="pnlerror" CancelControlID="LinkButton1"
                TargetControlID="Button1" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlerror" runat="server" Style="width: 500px !important">
                <div class="card">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Please Check
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblerrormsg" runat="server"></asp:Label>
                        <div style="margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                        </div>
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button1" runat="server" Text="" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpsuccess" runat="server" PopupControlID="pnlsuccess"
                CancelControlID="lbtnsuccessclose1" TargetControlID="Button6" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlsuccess" runat="server" Style="position: inherit;">
                <div class="card" style="min-width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title">Confirm
                        </h4>
                    </div>
                    <div class="card-body" style="min-height: 100px;">
                        <asp:Label ID="lblsuccessmsg" runat="server"></asp:Label>
                        <div style="margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnsuccessclose1" runat="server" CssClass="btn btn-success"> <i class="fa fa-check"></i> OK </asp:LinkButton>
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


