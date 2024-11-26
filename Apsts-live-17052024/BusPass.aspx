<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" AutoEventWireup="true" CodeFile="busPass.aspx.cs" Inherits="busPass" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .form-control, .textbox {
            height: 35px !IMPORTANT;
            color: black;
        }

        ul li {
            display: inline;
            color: #1b4372;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="pt-4" style="min-height: 70vh;">
        <div class="container">
            <div class="row no-gutters">
                <div class="col-md-12">
                    <div class="section pl-md-5">
                        <div class="block-23 mb-3 ">
                            <ul>
                                <li>
                                    <span class="text">We offer various concessional bus passes . To make the process of bus passes issuance easy now you can apply online and can download the file from portal itself. While applying please check your eligibility and keep related documents  ready for upload 
                                    </span></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row mt-1">
                <div class="col-md-12 text-center">
                    <asp:Label ID="Label3" runat="server" CssClass="h4 text text-black-50 font-weight-bold" Text="Bus Pass Types"></asp:Label>
                    <ul class="mb-0">
                        <asp:Repeater ID="rptrservicetypecount" runat="server">
                            <ItemTemplate>
                                <li><%#Eval("busspass_categoryname")%> - <%#Eval("buspass_categorycount")%>  </li>,
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>

                </div>
            </div>
           <%--     <div class="row">
                <div id="demo" class="carousel slide" data-ride="carousel">
                    <!-- Indicators -->
                    <!-- The slideshow -->
                    <div class="carousel-inner">
                        
                        <asp:Repeater ID="rptBusPassType" runat="server" Visible="true">
                            <ItemTemplate>
                                <div class="carousel-item active">
                                    <div class="card mt-2" style="min-height: 350px;">
                                        <div class="card-header" style="background-color: #428BCA; height: 60px; line-height: 20px;">
                                            <div class="card-title  mb-0" style="font-size: 11pt;"><%#Eval("buspass_type_name")%></div>
                                        </div>
                                        <div class="card-body p-2">
                                            <span class="text-black-50"><%#Eval("val_description")%></span><br />
                                            <p class="mb-0 text-dark">Validity Period</p>
                                            <p class="text-black-50"><%#Eval("val_validity")%></p>
                                            <p class="mb-0 text-dark">Document Required</p>
                                            <p class="text-black-50"><%#Eval("document_required")%></p>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <!-- Left and right controls -->
                    <a class="carousel-control-prev" href="#demo" data-slide="prev">
                        <span class="carousel-control-prev-icon"></span>
                    </a>
                    <a class="carousel-control-next" href="#demo" data-slide="next">
                        <span class="carousel-control-next-icon"></span>
                    </a>
                </div>
            </div>--%>




                    <div class="row">
                          
                    <asp:Repeater ID="rptBusPassType" runat="server">
                        <ItemTemplate>
                            <div class="col-md-3">
                            <div class="item">
                                <div class="card mt-2" style="min-height: 350px;">
                                    <div class="card-header" style="background-color: #428BCA; height: 60px; line-height: 20px;">
                                        <div class="card-title text-white mb-0" style="font-size: 11pt;"><%#Eval("buspass_type_name")%></div>
                                    </div>
                                    <div class="card-body p-2">
                                        <span class="text-black-50"><%#Eval("val_description")%></span><br />
                                        <p class="mb-0 text-dark">Validity Period</p>
                                        <p class="text-black-50"><%#Eval("val_validity")%></p>
                                        <p class="mb-0 text-dark">Document Required</p>
                                        <p class="text-black-50"><%#Eval("document_required")%></p>
                                    </div>
                                </div>

                            </div>
                                </div>
                        </ItemTemplate>
                    </asp:Repeater>
                              
                        </div>   
          
            <div class="row mt-4 pb-4">
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="Label7" runat="server" CssClass="h4 text text-black-50 font-weight-bold" Text="Apply For New Pass"></asp:Label>
                            <div class="p-3" style="box-shadow: 0px 5px 12px -1px rgb(0 0 0 / 6%);">
                                <p class="text-dark">If you want to buy a bus pass then you can apply for the new pass to avail additional benefits.</p>
                                <div class="form-group m-0">
                                    <asp:LinkButton ID="lbtnNewPass" runat="server" CssClass="btn btn-primary" PostBackUrl="NewBusPassRegistration.aspx"> Click here </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col-md-12">
                            <asp:Label ID="Label1" runat="server" CssClass="h4 text text-black-50 font-weight-bold" Text="Apply For Renew Pass"></asp:Label>
                            <div class="p-3" style="box-shadow: 0px 5px 12px -1px rgb(0 0 0 / 6%);">
                                <p class="text-dark">If your current bus pass is not expire, then you can renew your current pass for that you should have Pass No.</p>
                                <div class="form-group m-0">
                                    <asp:LinkButton ID="lbtnRenewPass" runat="server" CssClass="btn btn-primary" PostBackUrl="renewbuspass.aspx"> Click here </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-8">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="Label2" runat="server" CssClass="h4 text text-black-50 font-weight-bold" Text="Other Services"></asp:Label>
                            <div class="p-3 pb-5" style="box-shadow: 0px 5px 12px -1px rgb(0 0 0 / 6%);">
                                <p class="text-dark">You can use Other service like pass application request status, pass validity and download e_pass.</p>
                                <div class="row mt-3" style="font-size: 11pt; padding-left: 8px;">
                                    <div class="col-lg-4">
                                        <asp:RadioButton GroupName="rbservices" ID="rbtchkstatus" runat="server" />
                                        <label>Check Application Status</label>
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:RadioButton GroupName="rbservices" ID="rbtchkvalidity" runat="server" />
                                        <label>Check Pass Validity</label>
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:RadioButton GroupName="rbservices" ID="rbtdownload" runat="server" />
                                        <label>Download Pass</label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblPassRefNoHeader" runat="server" Text="Pass Ref. Number" CssClass="mb-0" Style="font-size: 12px;"></asp:Label>
                                        <div class="form-group mb-2">
                                            <asp:TextBox ID="tbPassRefno" runat="server" CssClass="form-control" placeholder="Max 50 Characters" TabIndex="1" autocomplete="off" MaxLength="50"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="UppercaseLetters,LowercaseLetters,Numbers,Custom" TargetControlID="tbPassRefno" ValidChars="_" />
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblDateOfBirthHeader" runat="server" Text="Date Of Birth" CssClass="mb-0" Style="font-size: 12px;"></asp:Label>
                                        <div class="form-group mb-2">
                                            <asp:TextBox ID="tbdob" runat="server" CssClass="form-control" placeholder="DD/MM/YYYY" TabIndex="2" autocomplete="off" MaxLength="50"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" CssClass="black" PopupButtonID="tbdob" TargetControlID="tbdob"></cc1:CalendarExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="row mt-3">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblSecurityImageHeader" runat="server" Text="Security Image" CssClass="mb-0" Style="font-size: 12px;"></asp:Label>
                                        <div class="form-group mb-2">
                                           <img alt="Visual verification" title="Please enter the security code as shown in the image."
                                    src="CaptchaImage.aspx" style="width: 80%; border: 1px solid #e6e6e6; height: 35px; border-radius: 5px;" />
                                            <asp:LinkButton runat="server" ID="lbtnRefresh" OnClick="lbtnRefresh_Click" CssClass=" btn btn-primary" Style="width: 17%;">
                                                            <i class="fa fa-sync-alt"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblSecurityTextHeader" runat="server" Text="Security Text" CssClass="mb-0" Style="font-size: 12px;"></asp:Label>
                                        <div class="form-group mb-2">
                                            <asp:TextBox ID="tbcaptchacode" runat="server" CssClass="form-control" placeholder="Enter Text" TabIndex="3" autocomplete="off" MaxLength="6"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="row" style="margin-top: 25px;">
                                    <div class="col-lg-12 text-center">
                                        <asp:LinkButton ID="lbtnproceed" runat="server" OnClick="lbtnproceed_Click" class="btn btn-success mt-2"><i class="fa fa-search"></i> Search</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnreset" runat="server" OnClick="lbtnreset_Click" class="btn btn-danger mt-2"><i class="fa fa-undo"></i> Reset</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>


            <div class="row">
                <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation" CancelControlID="lbtncancel"
                    TargetControlID="Button3" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlConfirmation" runat="server" Style="width: 500px !important">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="panel-title text-left mb-0" style="padding-top: 5px;">
                                <asp:Label ID="lblhd" runat="server"></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body text-left" style="min-height: 100px;">
                            <h5 style="line-height: 25px;">
                                <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></h5>
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                    <asp:LinkButton ID="lbtndownload" runat="server" OnClick="lbtndownload_Click" Visible="false" CssClass="btn btn-success"> <i class="fa fa-print"></i> Download E-Pass </asp:LinkButton>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                    <p class="text-dark">
                                        For any query feel free to contact helpdesk
                                        <br />
                                        <i class="fa fa-envelope"></i>help[dot]utconline[at]gmail[dot]com
                                    </p>
                                </div>
                            </div>
                            <div class="modal-footer text-right">
                                <asp:LinkButton ID="lbtncancel" runat="server" CssClass="btn btn-danger"> Close </asp:LinkButton>
                            </div>
                            <div style="visibility: hidden;">
                                <asp:Button ID="Button3" runat="server" Text="" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div class="row">
                <cc1:ModalPopupExtender ID="mperror" runat="server" PopupControlID="pnlerror" CancelControlID="LinkButton4"
                    TargetControlID="Button1" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlerror" runat="server" Style="min-width: 300px !important">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="card-title text-left mb-0">Please Check
                            </h4>
                        </div>
                        <div class="modal-body text-left">
                            <p class="text-dark">
                                <asp:Label ID="lblerrormsg" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div class="modal-footer text-right">
                            <asp:LinkButton ID="LinkButton4" runat="server" CssClass="btn btn-success"> OK </asp:LinkButton>
                            <div style="visibility: hidden; height: 0; display: none;">
                                <asp:Button ID="Button1" runat="server" Text="" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>

            <div class="row">
            <cc1:ModalPopupExtender ID="mpPass" runat="server" PopupControlID="pnlPass" TargetControlID="Button22"
                CancelControlID="lbtnClose" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlPass" runat="server" Style="position: fixed;">
                <div class="modal-content mt-5">
                    <div class="modal-header">
                        <div class="col-md-10">
                            <h3 class="m-0">Bus Pass</h3>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="lbtnClose" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="icon icon-close" style="font-size:26pt;" ></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="modal-body p-0">
                        <embed src="PassUTC/Bus_Pass.aspx" style="height: 35vh; width: 63vw" />
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button22" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>



        </div>
    </section>
</asp:Content>



