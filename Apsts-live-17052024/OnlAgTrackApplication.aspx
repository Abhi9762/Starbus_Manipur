<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="OnlAgTrackApplication.aspx.cs" Inherits="OnlAgTrackApplication" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="travellerBookingAssets/css/travelllerStepProgressBar.css" rel="stylesheet" />
    <style>
        #progressbar li {
            list-style-type: none;
            font-size: 15px;
            width: 33%;
            float: left;
            position: relative;
            font-weight: 400;
        }

        .table th, .table td {
            padding: 0.5rem !important;
        }

        .table th {
            background: #e6e6e6;
        }

        .stepper-wrapper {
            margin-top: auto;
            display: flex;
            justify-content: space-between;
            margin-bottom: 20px;
        }

        .stepper-item {
            position: relative;
            display: flex;
            flex-direction: column;
            align-items: center;
            flex: 1;

            @media (max-width: 768px) {
                font-size: 12px;
            }
        }

            .stepper-item::before {
                position: absolute;
                content: "";
                border-bottom: 2px solid #ccc;
                width: 100%;
                top: 20px;
                left: -50%;
                z-index: 2;
            }

            .stepper-item::after {
                position: absolute;
                content: "";
                border-bottom: 2px solid #ccc;
                width: 100%;
                top: 20px;
                left: 50%;
                z-index: 2;
            }

            .stepper-item .step-counter {
                position: relative;
                z-index: 5;
                display: flex;
                justify-content: center;
                align-items: center;
                width: 40px;
                height: 40px;
                border-radius: 50%;
                background: #ccc;
                margin-bottom: 6px;
            }

            .stepper-item.active {
                font-weight: bold;
            }

            .stepper-item.completed .step-counter {
                background-color: #4bb543;
            }

            .stepper-item.completed::after {
                position: absolute;
                content: "";
                border-bottom: 2px solid #4bb543;
                width: 100%;
                top: 20px;
                left: 50%;
                z-index: 3;
            }

            .stepper-item:first-child::before {
                content: none;
            }

            .stepper-item:last-child::after {
                content: none;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="ftco-section" style="min-height: 60vh; padding-top: 1em;">

        
        <div class="container">  
            <asp:HiddenField ID="hidtoken" runat="server" />

            <div class="row">

                <div class="col-md-12 text text-dark px-4 py-3">
                    <div class="row">
                        <div class="col-md">
                            <h2 class="mb-0">AGENT APPLICATION STATUS</h2>
                            <p><b>To become Manipur State Transport Agent</b></p>
                        </div>

                    </div>
                    <div class="col-md-12">
                        <asp:Panel runat="server" ID="pnlDetails" Width="100%" Visible="false">
                            <div class="row">
                                <div class="col-md-12 pt-3">
                                    <h4>Applicant Details</h4>
                                    <span style="font-size: 12px;">Reference No. </span>
                                    <asp:Label ID="lblReferenceNo" runat="server" Style="font-size: 12px;" Font-Bold="true" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 ">
                                    <asp:GridView ID="grvRequest" runat="server" AutoGenerateColumns="False" GridLines="None" AllowSorting="true" AllowPaging="true" ShowHeader="true"
                                        Font-Size="10pt" class="table table-bordered table-hover">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAGENTNAME" runat="server" Text='<%# Eval("agent_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PAN No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("pan_no") %>' Style="text-transform: capitalize;" Font-Bold="true"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile">
                                                <ItemTemplate>
                                                    <i class="fa fa-mobile"></i>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("mobile_no") %>' Style="text-transform: capitalize;"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email">
                                                <ItemTemplate>
                                                    <i class="fa fa-envelope ml-2"></i>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("val_email") %>' Style="text-transform: capitalize;"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblADDRESS" runat="server" Font-Bold="true" Text='<%# Eval("val_address") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                            <div class="row mt-4" id="dvprogressbar" runat="server">
                                <div class="col-md-12">
                                    <div class="stepper-wrapper">
                                        <div class="stepper-item completed" id="step1" runat="server">
                                            <div class="step-counter">1</div>
                                            <div class="step-name">Requested</div>
                                        </div>
                                        <div class="stepper-item completed" id="step2" runat="server">
                                            <div class="step-counter">2</div>
                                            <div class="step-name">Verified</div>
                                        </div>
                                        <div class="stepper-item active" id="step3" runat="server">
                                            <div class="step-counter">3</div>
                                            <div class="step-name">Approved</div>
                                        </div>
                                        <div class="stepper-item active" id="step4" runat="server" visible="false">
                                            <div class="step-counter">2</div>
                                            <div class="step-name">Rejected</div>
                                        </div>
                                        <div class="stepper-item active" id="step5" runat="server" visible="false">
                                            <div class="step-counter">2</div>
                                            <div class="step-name">Cancelled</div>
                                        </div>
                                        <div class="stepper-item active" id="step6" runat="server" visible="false">
                                            <div class="step-counter">4</div>
                                            <div class="step-name">Deactivated</div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row mt-4" style="line-height: 40px; font-size: 10pt;">
                                <div class="col-md-4" style="border: 1px solid #e6e6e6;">
                                    <asp:Label ID="Label8" runat="server" Text="Status" CssClass="text-uppercase"></asp:Label>
                                </div>
                                <div class="col-md-8" style="border: 1px solid #e6e6e6;">
                                    <asp:Label ID="lblpstatus" runat="server" Text="" CssClass="text-uppercase"></asp:Label>
                                </div>
                                <div class="col-md-4" style="border: 1px solid #e6e6e6;">
                                    <asp:Label ID="lblpending" runat="server" Text="Pending Since" CssClass="text-uppercase"></asp:Label>
                                </div>
                                <div class="col-md-8" style="border: 1px solid #e6e6e6;">
                                    <asp:Label ID="lblpendingsince" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-md-4" style="border: 1px solid #e6e6e6;">
                                    <asp:Label ID="Label11" runat="server" Text="Remark" CssClass="text-uppercase"></asp:Label>
                                </div>
                                <div class="col-md-8" style="border: 1px solid #e6e6e6;">
                                    <span class="text-left" style="color: red;">
                                        <asp:Label ID="lblStatus" runat="server"></asp:Label></span>
                                    <asp:Label runat="server" ID="lblRejectReason" ForeColor="Red" Style="font-size: 13pt"></asp:Label>
                                    <asp:LinkButton ID="lbtngetpwd" runat="server" OnClick="lbtngetpwd_Click" Text=""> <i class='icon icon-external-link'></i> </asp:LinkButton>

                                </div>
                            </div>
                            <div class="row" runat="server" id="feeLabel" visible="false" style="line-height: 40px; font-size: 10pt;">
                                <div class="col-md-4" style="border: 1px solid #e6e6e6;">
                                    <asp:Label ID="Label1" runat="server" Text="Amount to Pay" CssClass="text-uppercase"></asp:Label>
                                </div>
                                <div class="col-md-8" style="border: 1px solid #e6e6e6;">
                                    <span runat="server" class="text-left" style="color: red;">
                                        <asp:Label ID="lblAmount" runat="server">
                                        </asp:Label>&nbsp;<i class="fa fa-rupee-sign"></i></span>
                                </div>
                            </div>
                            <div class="row" runat="server" id="paymentDiv" visible="false" style="line-height: 50px; font-size: 10pt;">
                                <div class="col-md-12 text-right" style="border: 1px solid #e6e6e6;">
                                    <asp:LinkButton runat="server" ID="lbtnProceed" OnClick="lbtnProceed_Click" CssClass="btn btn-primary" Text="Click Here to Proceed for Payment"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lbtnCancel" OnClick="lbtnCancel_Click" CssClass="btn btn-warning ml-2" Text="Cancel"></asp:LinkButton>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlForm" Width="100%">


                            <div class="row">
                                <div class="col-lg-4"></div>
                                <div class="col-lg-4">
                                    <asp:Label ID="Label6" runat="server" Text="Enter Reference No." CssClass="mb-0" Style="font-size: 15px;"></asp:Label>
                                    <span style="color: Red;">*</span>
                                    <asp:TextBox ID="txtReferenceNo" runat="server" CssClass="form-control" placeholder="Enter Ref. No."
                                        TabIndex="1" autocomplete="off" MaxLength="20"></asp:TextBox>
                                </div>
                                <div class="col-lg-4"></div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-lg-4"></div>
                                <div class="col-lg-4">
                                    <asp:Label ID="Label7" runat="server" Text="Enter Mobile No." CssClass="mb-0" Style="font-size: 15px;"></asp:Label>
                                    <span style="color: Red;">*</span>
                                    <asp:TextBox ID="txtmobileno" runat="server" CssClass="form-control" placeholder="10 Digit Mobile Number"
                                        TabIndex="2" autocomplete="off" MaxLength="10"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtmobileno" />
                                </div>
                                <div class="col-lg-4"></div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-lg-4"></div>
                                <div class="col-lg-4">
                                    <asp:Label ID="lblSecurityImageHeader" runat="server" Text="Security Image" CssClass="mb-0" Style="font-size: 15px;"></asp:Label>
                                  <div class="form-group mb-2">
                                <img alt="Visual verification" title="Please enter the security code as shown in the image."
                                    src="CaptchaImage.aspx" style="width: 80%; border: 1px solid #e6e6e6; height: 35px; border-radius: 5px;" />
                                <asp:LinkButton runat="server" ID="lbtnRefresh" OnClick="lbtnRefresh_Click" TabIndex="23" CssClass="btn btn-primary" Style="width: 17%;">
                                                            <i class="fa fa-sync-alt"></i></asp:LinkButton>
                            </div>
                                </div>
                                <div class="col-lg-4"></div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-lg-4"></div>
                                <div class="col-lg-4">
                                    <asp:Label ID="lblSecurityTextHeader" runat="server" Text="Security Text" CssClass="mb-0" Style="font-size: 15px;"></asp:Label>
                                    <span style="color: Red;">*</span>
                                    <div class="form-group mb-2">
                                        <asp:TextBox ID="tbcaptchacode" runat="server" CssClass="form-control" placeholder="Enter Text" Style="text-transform: uppercase;" TabIndex="3" autocomplete="off" MaxLength="6"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="col-lg-4"></div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-lg-12 py-1  text-center">
                                    <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" OnClientClick="return ShowLoading();" CssClass="btn btn-primary" Text="Submit" TabIndex="13" Style="cursor: pointer;" />
                                    <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" OnClientClick="return ShowLoading();" CssClass="btn btn-warning" Text="Reset" TabIndex="13" Style="cursor: pointer;" />
                                </div>
                            </div>


                        </asp:Panel>
                    </div>
                </div>
            </div>

            <div class="row">
                <div style="visibility: hidden;">
                    <asp:Button ID="Button3" runat="server" Text="" />
                </div>
                <cc1:ModalPopupExtender ID="mpError" runat="server" PopupControlID="pnlError" CancelControlID="LinkButton1"
                    TargetControlID="Button3" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlError" runat="server" Style="position: fixed;">
                    <div class="modal-content" style="min-width: 400px; background: white; border-radius: 4px;">
                        <div class="modal-header">
                            <h4 class="mb-0">Please Check & Correct</h4>
                        </div>
                        <div class="modal-body text-left" style="min-height: 100px;">
                            <asp:Label ID="lblerrmsg" runat="server" ForeColor="Black" Style="font-size: 17px; line-height: 23px;"></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-success"> OK </asp:LinkButton>
                        </div>
                    </div>

                </asp:Panel>
            </div>
            <div class="row">
                <cc1:ModalPopupExtender ID="mpconfirm" runat="server" PopupControlID="pnlconfirm"
                    CancelControlID="LinkButton3" TargetControlID="Button1" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlconfirm" runat="server" Style="position: fixed;">
                    <div class="modal-content" style="width: 400px; background: white; border-radius: 4px;">
                        <div class="modal-header">
                            <h4 class="mb-0">Congratulations</h4>
                        </div>
                        <div class="modal-body text-left pt-2" style="min-height: 100px;">
                            <asp:Label ID="lblsucessmsg" runat="server" ForeColor="Black" Style="font-size: 17px; line-height: 23px;"></asp:Label>

                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-success"> OK </asp:LinkButton>
                        </div>
                    </div>
                    <br />
                    <div style="visibility: hidden;">
                        <asp:Button ID="Button1" runat="server" Text="" />
                    </div>
                </asp:Panel>
            </div>

        </div>
    </section>
</asp:Content>



