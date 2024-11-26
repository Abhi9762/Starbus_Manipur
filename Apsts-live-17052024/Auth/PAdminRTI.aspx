<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/PortalAdmmaster.master" AutoEventWireup="true" CodeFile="PAdminRTI.aspx.cs" Inherits="Auth_PAdminRTI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function UploadFilePIOS(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnFilePIOS.ClientID %>").click();
            }
        }
        function UploadFileManual1(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnFileManual1.ClientID %>").click();
            }
        }
        function UploadFileManual2(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnFileManual2.ClientID %>").click();
            }
        }
        function UploadFileManual3(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnFileManual3.ClientID %>").click();
            }
        }
    </script>
    <style type="text/css">
        .form-control-label1 {
            font-size: .75rem;
            font-weight: 600;
            color: white;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="HiddenField1" runat="server" />
   	<div class="header pb-4">
	</div>
     <div class="container-fluid mt-1">
      <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row my-2">
            <div class="col-lg-5 col-md-5 order-xl-1">
                <div class="card" style="min-height: 400px">

                   <div class="card-header">
                        <div class="row m-0 align-items-center">
                              <div class="col-md-5">
                            <h3 class="mb-1">
                                 <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h2><span style="color: #ff5e00;">Please Note</span></h2></asp:Label>

                                    </h3>
                                    </div>
                             <div class="col-md-7 text-right">
                                 <h4>
                              <label class="form-control-label text-warning" >All * Fields are mandatory  </label>
                                 </h4>
                             </div>
                        </div>
                    </div>

                 
                    <div class="col-md-12 col-lg-12">
                
                           <br />
                        <div class="row mx-2 my-1">
                            <div class="col">
                                            
                                     <asp:Label runat="server"  CssClass="form-control-label" Font-Size="Medium">PIOS Document Manuals
                              
                            </asp:Label>              
                               <asp:Label runat="server" CssClass="form-control-label" Font-Bold="false" Font-Size="Small"><br /> 1. File to be uploaded should be only in<b>.pdf</b> format.<br />
                                2. pdf size should not exceed  <b>5 MB</b><br />
                              </asp:Label>

                                        </div>
                       
                               </div>
                        <br />
                        <div class="row mx-2 my-1">
                             <div class="col">
                            <asp:Label runat="server" CssClass="form-control-label" Font-Size="Medium">RTI Document Manuals</asp:Label>
                            <asp:Label runat="server" CssClass="form-control-label" Font-Bold="false" Font-Size="Small"><br />
                                1. File to be uploaded should be only in<b>.pdf</b>format.<br />
                                2. pdf size should not exceed <b>5 MB</b><br />
                                </asp:Label>
                                 </div>
                        </div>

                    </div>
                </div>

            </div>
            <div class="col-md-7 col-lg-7 order-xl-2">
                <div class="card" style="min-height: 400px">
                       <div class="card-header">

                        <div class="row m-0 align-items-center">
                            <div class="col-md-9">
                            <h3 class="mb-1">
                                 <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h2><span >RTI Manual</span></h2></asp:Label>

                                    </h3>
                                    </div>
                             <div class="col-md-3 text-right">
                                 <h4 >
                      <asp:LinkButton ID="lbtnHelp" runat="server" ToolTip="View Instructions" OnClick="lbtnHelp_Click"  CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                        </asp:LinkButton>
                                     
                          <asp:LinkButton ID="lbtnRTIPIOSViewHistory" Visible="false" runat="server"  OnClick="lbtnRTIPIOSViewHistory_Click" ToolTip="Click here to View RTI/ PIOS  History" CssClass="btn btn bg-gradient-blue btn-sm text-white">
                                    <i class="fa fa-history"></i>
                        </asp:LinkButton>
                                 </h4>
                             </div>
                        </div>
                    </div>

                    <div class="row m-0 align-items-center ">
                        <div class="col-md-12 col-lg-12 ml-3">
                   <br />
                             <div class="col-md-12 col-lg-12">
                            <div class="row pb-3">
                                <div class="col-md-2 col-lg-2 pl-0">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"> PIOS/APIOS <span class="text-warning">*</span></asp:Label>

                                </div>

                                <div class="col-md-10 col-lg-10 text-left">
                                    <asp:FileUpload ID="FilePIOS" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-sm btn-success "
                                        onchange="UploadFilePIOS(this);" TabIndex="9" />
                            
                            <asp:Button ID="btnFilePIOS" runat="server" OnClick="btnFilePIOS_Click" CausesValidation="False" Style="display: none"
                                Text="" Width="80px" />
                            <asp:Label runat="server" ID="lblPIOS" CssClass="form-control-label"></asp:Label>
                            <asp:LinkButton ID="lbtndownlaodPIOS" runat="server" ToolTip="Click here to Download PIOS Manuals" OnClick="lbtndownlaodPIOS_Click"  CssClass="btn btn bg-gradient-green btn-sm text-white"><i class="fa fa-download" title ="Click here to Download PIOS Manuals"></i></asp:LinkButton>
                            <asp:LinkButton ID="lbtndeletePIOS" runat="server" OnClick="lbtndeletePIOS_Click" ToolTip="Click here to Delete PIOS Manuals" Visible="false" OnClientClick="return ShowLoading()" class="btn btn-danger btn-sm"><i class="fa fa-trash" title ="Click here to Delete PIOS Manuals"></i></asp:LinkButton>
                                    <br />

                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-lg-12">
                            <div class="row pb-3">
                                <div class="col-md-2 col-lg-2 pl-0">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"> RTI Maual 1<span class="text-warning">*</span> </asp:Label>

                                </div>

                                <div class="col-md-10 col-lg-10 text-left">
                                     <asp:FileUpload ID="FileManual1" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-sm btn-success "
                                        onchange="UploadFileManual1(this);" TabIndex="9" />
                            
                                   
                            <asp:Button ID="btnFileManual1" runat="server" OnClick="btnFileManual1_Click" CausesValidation="False" Style="display: none"
                                Text="" Width="80px" />
                            <asp:Label runat="server" ID="lblManual1" CssClass="form-control-label"></asp:Label>
                            <asp:LinkButton ID="lbtndownlaodManual1" runat="server" OnClick="lbtndownlaodManual1_Click" ToolTip="Click here to Download RTI Maual 1"  CssClass="btn btn bg-gradient-green btn-sm text-white"><i class="fa fa-download" title ="Click here to Download RTI Maual 1"></i></asp:LinkButton>
                            <asp:LinkButton ID="lbtndeleteManual1" runat="server" OnClick="lbtndeleteManual1_Click" Visible="false" OnClientClick="return ShowLoading()" ToolTip="Click here to Delete RTI Maual 1" class="btn btn-danger btn-sm"><i class="fa fa-trash" title ="Click here to Delete RTI Maual 1"></i></asp:LinkButton>
                                    <br />

                                </div>
                            </div>
                        </div>


                        <div class="col-md-12 col-lg-12">
                            <div class="row pb-3">
                                <div class="col-md-2 col-lg-2 pl-0">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"> RTI Manual 2</asp:Label>

                                </div>

                                <div class="col-md-10 col-lg-10 text-left">
                                     <asp:FileUpload ID="FileManual2"  runat="server"  Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-sm btn-success "
                                        onchange="UploadFileManual2(this);" TabIndex="9" />
                            
                                  
                            <asp:Button ID="btnFileManual2" runat="server" OnClick="btnFileManual2_Click" CausesValidation="False" Style="display: none"
                                Text="" Width="80px" />
                            <asp:Label ID="lblManual2" runat="server" CssClass="form-control-label"></asp:Label>
                            <asp:LinkButton ID="lbtndownlaodManual2" runat="server" OnClick="lbtndownlaodManual2_Click" Visible="false"  ToolTip="Click here to Download RTI Maual 2"  CssClass="btn btn bg-gradient-green btn-sm text-white"><i class="fa fa-download" title="Click here to Download RTI Maual 2"></i></asp:LinkButton>
                            <asp:LinkButton ID="lbtndeleteManual2" runat="server" OnClick="lbtndeleteManual2_Click" Visible="false" OnClientClick="return ShowLoading()" ToolTip="Click here to Delete RTI Maual 1" class="btn btn-danger btn-sm"><i class="fa fa-trash" title="Click here to Delete RTI Maual 1"></i></asp:LinkButton>
                                    

                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 col-lg-12">
                            <div class="row pb-3">
                                <div class="col-md-2 col-lg-2 pl-0">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"> RTI Manual 3 </asp:Label>

                                </div>

                                <div class="col-md-10 col-lg-10 text-left">
                                     <asp:FileUpload ID="FileManual3" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-sm btn-success "
                                        onchange="UploadFileManual3(this);" TabIndex="9" />
 
                            <asp:Button ID="btnFileManual3" runat="server" OnClick="btnFileManual3_Click" CausesValidation="False" Style="display: none"
                                Text="" Width="80px" />
                            <asp:Label ID="lblManual3" runat="server" CssClass="form-control-label"></asp:Label>
                            <asp:LinkButton ID="lbtndownlaodManual3" runat="server" OnClick="lbtndownlaodManual3_Click" Visible="false" ToolTip="Click here to Download RTI Maual 3"  CssClass="btn btn bg-gradient-green btn-sm text-white"><i class="fa fa-download" title ="Click here to Download RTI Maual 3"></i></asp:LinkButton>
                            <asp:LinkButton ID="lbtndeleteManual3" runat="server" OnClick="lbtndeleteManual3_Click" Visible="false" ToolTip="Click here to Delete RTI Maual 1" OnClientClick="return ShowLoading()" class="btn btn-danger btn-sm"><i class="fa fa-trash" title="Click here to Delete RTI Maual 1"></i></asp:LinkButton>
                              
                                    </div>
                            </div>
                        </div>

                        <div class="col-md-12 col-lg-12 text-center pt-3">
                     
                             <asp:LinkButton ID="lbtnSaveRTIData" runat="server" OnClick="lbtnSaveRTIData_Click" OnClientClick="return ShowLoading()" CausesValidation="False"
                                class="btn btn-success" Style="margin-top: 9px; font-size: 10pt;" ToolTip="Click here to Add/Update PIOS/RTI Manuals"> <i class="fa fa-save" title="Click here to Add/Update PIOS/RTI Manuals"></i> Update</asp:LinkButton>
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
        <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;display:none">
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
            <cc1:ModalPopupExtender ID="mpsuccess" runat="server" PopupControlID="pnlsuccess"
                CancelControlID="lbtnsuccessclose1" TargetControlID="Button6" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlsuccess" runat="server" Style="position: fixed;display:none;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Information
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblsuccessmsg" runat="server" Text="Do you want to Update ?"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnsuccessclose1" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> Close </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button6" runat="server" Text="" />
                </div>
            </asp:Panel>
    </div>

</asp:Content>

