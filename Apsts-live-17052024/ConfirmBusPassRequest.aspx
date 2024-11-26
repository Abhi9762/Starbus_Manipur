<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mainMasterPage.master" CodeFile="ConfirmBusPassRequest.aspx.cs" Inherits="ConfirmBusPassRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <link href="../assets/css/travelllerStepProgressBar.css" rel="stylesheet" />
    <style>
        body {
            font-size: 0.8rem;
        }

        h6 {
            font-size: 1.0rem;
        }

        .border-right {
            border-right: 1px solid;
        }

        .lbld {
            font-weight: bold;
        }

        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .cd {
            min-height: 22vh !important;
        }

        #progressbar li {
            width: 25% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="container">
            <div class="row my-3 mb-2">
                <div class="col-12">
                    <ul id="progressbar">
                        <li class="text-center active" id="apply_one"><strong>Apply for New Pass</strong></li>
                        <li class="text-center active" id="confirm_two"><strong>Confirm Details</strong></li>
                        <li class="text-center" id="confirm_three"><strong>Payment</strong></li>
                        <li class="text-center" id="confirm_four"><strong>Download</strong></li>
                    </ul>
                </div>
            </div>
            <div class="row my-3" style="padding-bottom: 64px;">
                <div class="col-12">
                    <div class="card">
                        <div class="card-header pt-0 pb-0"></div>
                        <div class="card-body">
                            <div class="row mt-2">
                                <div class="col-lg-6 border-right" style="line-height: 26px;">
                                    <h6>
                                        <asp:Label ID="lblpassrequest" runat="server" Text="" Font-Bold="True"></asp:Label></h6>
                                    <br />

                                    <h6 class="heading-small my-0">Personal Details</h6>
                                    <div class="row p-2">
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">Name</span>
                                            <asp:Label ID="lblname" runat="server" CssClass="lbld" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">Father Name</span>
                                            <asp:Label ID="lblfname" CssClass="lbld" runat="server" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">Gender</span>
                                            <asp:Label ID="lblgender" CssClass="lbld" runat="server" Text="NA"></asp:Label>

                                        </div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">Date of Birth</span>
                                            <asp:Label ID="lbldob" CssClass="lbld" runat="server" Text="NA"></asp:Label>

                                        </div>
                                    </div>
                                    <h6 class="heading-small my-0">Contact Details</h6>
                                    <div class="row p-2">
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">Mobile No.</span>
                                            <asp:Label ID="lblmobileno" runat="server" CssClass="lbld" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">Email</span>
                                            <asp:Label ID="lblemail" CssClass="lbld" runat="server" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">State</span>
                                            <asp:Label ID="lblstate" CssClass="lbld" runat="server" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">District</span>
                                            <asp:Label ID="lbldistrict" CssClass="lbld" runat="server" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-8">
                                            <span class="form-control-label text-muted font-weight-normal">Address</span>
                                            <asp:Label ID="lbladdress" CssClass="lbld" runat="server" Text="NA"></asp:Label>

                                        </div>
                                    </div>
                                    <h6 class="heading-small my-0">Journey Details</h6>
                                    <div class="row p-2">
                                        <div class="col-lg-8">
                                            <span class="form-control-label text-muted font-weight-normal">Route</span>
                                            <asp:Label ID="lblRoute" runat="server" CssClass="lbld" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-4"></div>
                                        <div class="col-lg-8">
                                            <span class="form-control-label text-muted font-weight-normal">Stations</span>
                                            <asp:Label ID="lblstations" CssClass="lbld" runat="server" Text="NA"></asp:Label>

                                        </div>
                                        <div class="col-lg-4"></div>
                                        <div class="col-lg-8">
                                            <span class="form-control-label text-muted font-weight-normal">Service Type</span>
                                            <asp:Label ID="lblservicetype" CssClass="lbld" runat="server" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-4"></div>
                                        <div class="col-lg-6">
                                            <span class="form-control-label text-muted font-weight-normal">Validity</span>
                                            <asp:Label ID="lblvalidity" CssClass="lbld" runat="server" Text="NA"></asp:Label>
                                        </div>

                                    </div>

                                    <h6 class="heading-small my-0">Applicable Charges</h6>
                                    <div class="row p-2">
                                        <div class="col-lg-4">
                                            <span class="form-control-label text-muted font-weight-normal">Pass Amount</span>
                                            <asp:Label ID="lblPassamount" runat="server" CssClass="lbld" Text="NA"></asp:Label>
                                        </div>
                                        <div class="col-lg-4">
                                            <span class="form-control-label text-muted font-weight-normal" style="line-height: 4px;">
                                                <asp:Label ID="lblExtra_Charges" runat="server"></asp:Label></span>
                                            <asp:Label ID="lblExtrachrge" CssClass="lbld" runat="server" Text="NA"></asp:Label>

                                        </div>
                                        <div class="col-lg-4">
                                            <span class="form-control-label text-muted font-weight-normal">Tax Amount</span>
                                            <asp:Label ID="lbltaxamt" CssClass="lbld" runat="server" Text="NA"></asp:Label>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">

                                        <div class="col-md-12" style="text-align: center;">
                                            <h3 class="text-danger font-weight-bold">Total Amount To Pay
                                            <asp:Label ID="lblAmountToPay" runat="server" Font-Bold="True" Text="NA"></asp:Label>
                                                <i class="fa fa-rupee"></i>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <h6 class="card-title text-left">Please e-verify yourself</h6>
                                        </div>
                                    </div>
                                    <h6>
                                        <asp:Label ID="lblMobileOTP" runat="server" Text="Enter One Time Password(OTP) sent to your Mobile Number *******741" Font-Bold="True" ForeColor="#339933"></asp:Label></h6>
                                    <div class="row mt-3">
                                        <div class="col-lg-3"></div>
                                        <div class="col-lg-6">
                                            <asp:Label runat="server" Text="Enter OTP" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>

                                            <asp:TextBox ID="tbOTP" runat="server" CssClass="form-control" placeholder="Max 6 digits" MaxLength="6" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Enter OTP" Style="height: 42px; text-transform: uppercase;"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers" TargetControlID="tbOTP" ValidChars=" " />

                                        </div>
                                        <div class="col-lg-3"></div>
                                    </div>
                                    <asp:Panel ID="pnlotpcaptcha" runat="server" Visible="false">
                                        <div class="row mt-2">
                                            <div class="col-lg-3"></div>
                                            <div class="col-lg-6">
                                                <asp:Label runat="server" Text="Security Captcha" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>
                                                 <div class="input-group">
                                       <img alt="Visual verification" title="Please enter the security code as shown in the image."
                                    src="CaptchaImage.aspx" style="width: 70%; border: 1px solid #e6e6e6; height: 35px; border-radius: 5px;" />
                                        <asp:LinkButton runat="server" ID="lbtnRefresh" CssClass=" btn btn-primary" OnClick="lbtnRefresh_Click" Style="height: 36px;">
                                                            <i class="fa fa-sync-alt" ></i></asp:LinkButton>
                                    </div>

                                            </div>
                                            <div class="col-lg-3"></div>
                                        </div>
                                        <div class="row mt-2">
                                            <div class="col-lg-3"></div>
                                            <div class="col-lg-6">
                                                <asp:Label runat="server" Text="Security Text" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>
                                                <div class="input-group">
                                                    <asp:TextBox ID="tbcaptchOTP" runat="server" placeholder="Enter Text" autocomplete="off" class="form-control" MaxLength="6" Style="height: 42px; text-transform: uppercase;"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-3"></div>
                                        </div>
                                    </asp:Panel>
                                    <div class="row mt-3">
                                        <div class="col-lg-12 text-center">
                                            <asp:LinkButton ID="lbtnProceedOTP" runat="server" OnClick="lbtnProceedOTP_Click" CssClass="btn btn-success"> <i class="fa fa-check"></i> Verify & Proceed</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="row mt-2">
                                        <div class="col-lg-3"></div>
                                        <div class="col-lg-6" style="text-align: right;">
                                            <asp:LinkButton ID="lbtnResendOTP" runat="server" OnClick="lbtnResendOTP_Click" CssClass="text-primary"> <i class="fa fa-mobile"></i> Resend OTP</asp:LinkButton>
                                        </div>
                                        <div class="col-lg-3"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation" CancelControlID="lbtnNoConfirmation"
                    TargetControlID="Button3" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlConfirmation" runat="server" Style="width: 500px !important">
                    <div class="card cd">
                        <div class="card-header">
                            <h4 class="card-title text-left mb-0">Please Confirm
                            </h4>
                        </div>
                        <div class="card-body text-left pt-2" style="min-height: 100px;">
                            <asp:Label ID="lblConfirmation" runat="server" Style="font-size: 12pt;"></asp:Label>

                            <div style="margin-top: 20px; text-align: right;">
                                <asp:LinkButton ID="lbtnYesConfirmation" runat="server" OnClick="lbtnYesConfirmation_Click" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                                <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                            </div>
                            <div style="visibility: hidden;">
                                <asp:Button ID="Button3" runat="server" Text="" />
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
                    <div class="card cd">
                        <div class="card-header">
                            <h4 class="card-title text-left mb-0">Please Check
                            </h4>
                        </div>
                        <div class="card-body text-left">
                            <asp:Label ID="lblerrormsg" runat="server" Style="font-size: 12pt;"></asp:Label>
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

        </div>
    </div>
</asp:Content>


