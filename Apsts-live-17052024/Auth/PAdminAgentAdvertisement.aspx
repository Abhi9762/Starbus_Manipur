<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/PortalAdmmaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="PAdminAgentAdvertisement.aspx.cs" Inherits="Auth_PAdminAgentAdvertisement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {

            loaddatepicker();
        });
        function loaddatepicker() {
            var currDate = new Date().getDate();
            var preDate = new Date(new Date().setDate(currDate - 1));
            var todayDate = new Date(new Date().setDate(currDate));
            $('[id*=tbRegStartDate]').datepicker({
                startDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            })
                .on('changeDate', function (selected) {
                    var minDate = new Date(selected.date.valueOf());
                    $('[id*=tbRegEndDate]').datepicker('setStartDate', minDate);
                });
            $('[id*=tbRegEndDate]').datepicker({
                startDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
        

            $('[id*=tbProStartDate]').datepicker({
                startDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            })
                .on('changeDate', function (selected) {
                    var minDate = new Date(selected.date.valueOf());
                    $('[id*=tbProEndDate]').datepicker('setStartDate', minDate);
                });
            $('[id*=tbProEndDate]').datepicker({
                startDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
           
      
            $('[id*=tbAdvDetailsOrderDate]').datepicker({
                startDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
        }


        function UploadPDF(fileUpload) {
            if ($('#fudocfile').value != '') {
                document.getElementById("<%=btnUploadpdf.ClientID %>").click();
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="header pb-3">
    </div>
    <div class="container-fluid">

        <div class="row">
            <div class="col-lg-3 col-md-3">
                <div class="row mt-2">
                    <div class="col-lg-12 col-md-12  stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Agent Advertisement Date</div>
                                <div class="total-tx mb-2">Online Agent Request and Application Process Date</div>
                                <div class="col text-right">
                                    <a href="PAdminAgentAdvertisement.aspx" class="btn btn-sm btn-primary">Explore <i class="fa fa-arrow-circle-o-right"></i></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row mt-2">
                    <div class="col-lg-12 col-md-12  stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Agent Topup Limit</div>
                                <div class="total-tx mb-2">Recharge Top Up Limit</div>
                                <div class="col text-right">
                                    <a href="PAdminPolicy.aspx" class="btn btn-sm btn-primary">Explore <i class="fa fa-arrow-circle-o-right"></i></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row mt-2">
                    <div class="col-lg-12 col-md-12 stretch-card transparent">
                        <div class="card card-dark-blue">
                            <div class="card-body">
                                <div class="card-heading">Agent Security Fee</div>
                                <div class="total-tx mb-2">Security Fee</div>
                                <div class="col text-right">
                                    <a href="PAdminPolicy.aspx" class="btn btn-sm btn-primary">Explore <i class="fa fa-arrow-circle-o-right"></i></a>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>

            </div>
            <div class="col-lg-9 col-md-9">

                <div class="row m-0 p-0">
                    <div class="col-md-12 col-lg-12">
                        <div class="card" style="min-height: 470px">
                            <div class="card-header">
                                <div class="row">
                                    <div class="col-md-9">
                                        <h3 class="mb-0">Agent Advertisement Publishing</h3>
                                    </div>
                                    <div class="col-md-3 text-right">
                                        <h4>
                                            <asp:LinkButton ID="lbtnViewHelp" runat="server" OnClick="lbtnViewHelp_Click" ToolTip="View Instructions" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                            </asp:LinkButton>

                                            <asp:LinkButton ID="lbtnAgentTopUpLimitHistory" OnClick="lbtnHistory_Click" runat="server" ToolTip="Click here to View Agent Top Up Limit History" CssClass="btn btn bg-gradient-blue btn-sm text-white">
                                    <i class="fa fa-history"></i>
                                            </asp:LinkButton>
                                        </h4>
                                    </div>
                                </div>

                            </div>
                            <div class="card-body">

                                <div class="row">
                                    <div class="col-md-6 col-lg-6 border-right">
                                        <div class="border-bottom">


                                            <div class="form-control-label my-2">1. Advertisement Details</div>
                                            <div class="form-row align-items-center mt-3" autocomplete="off">
                                                <div class="form-group col-lg-3 mb-0 text-right">
                                                    <asp:Label AssociatedControlID="tbRegStartDate" CssClass="form-control-label" Font-Bold="true" ID="lblRegistration" runat="server">Registration<span class="text-warning">*</span></asp:Label>
                                                </div>
                                                <div class="form-group col-lg-4">
                                                    <asp:Label AssociatedControlID="tbRegStartDate" CssClass="form-control-label" Font-Bold="true" ID="lblRegStartDate" runat="server" Text="">Start Date</asp:Label>
                                                    <div class="input-group ">
                                                        <asp:TextBox ID="tbRegStartDate" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="DD/MM/YYYY" runat="server" MaxLength="10" ToolTip="Enter Start Date"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-4">
                                                    <asp:Label AssociatedControlID="tbRegEndDate" CssClass="form-control-label" ID="lblRegEndDate" runat="server" Font-Bold="true" Text="">End Date</asp:Label>
                                                    <div class="input-group ">
                                                        <asp:TextBox ID="tbRegEndDate" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="DD/MM/YYYY" runat="server" MaxLength="10" ToolTip="Enter End Date"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-row align-items-center my-2" autocomplete="off">
                                                <div class="form-group col-lg-3 mb-0 text-right">

                                                    <asp:Label AssociatedControlID="tbProStartDate" CssClass="form-control-label" Font-Bold="true" ID="lblProcessing" runat="server">Processing<span class="text-warning">*</span></asp:Label>
                                                </div>
                                                <div class="form-group col-lg-4">
                                                    <asp:Label AssociatedControlID="tbProStartDate" CssClass="form-control-label" ID="lblProStartDate" Font-Bold="true" runat="server" Text="">Start Date</asp:Label>
                                                    <div class="input-group ">
                                                        <asp:TextBox ID="tbProStartDate" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="DD/MM/YYYY" runat="server" MaxLength="10" ToolTip="Enter Start Date"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-4">
                                                    <asp:Label AssociatedControlID="tbProEndDate" CssClass="form-control-label" ID="lblProEndDate" Font-Bold="true" runat="server" Text="">End Date</asp:Label>
                                                    <div class="input-group ">
                                                        <asp:TextBox ID="tbProEndDate" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="DD/MM/YYYY" runat="server" MaxLength="10" ToolTip="Enter End Date"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>


                                        <div class="border-bottom">
                                            <div class="form-control-label my-2">2. Office Details</div>

                                            <div class="form-row align-items-center my-2">
                                                <div class="form-group col-lg-3 text-right">
                                                    <asp:Label AssociatedControlID="tbAdvDetailsOfficeOrderID" Autocomplete="off" CssClass="form-control-label" Font-Bold="true" ID="lblAdvDetailsOfficeOrderID" runat="server" Text="">Office Order ID</asp:Label>
                                                </div>
                                                <div class="form-group col-lg-4 text-right">
                                                    <div class="input-group ">
                                                        <asp:TextBox ID="tbAdvDetailsOfficeOrderID" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="Order ID" runat="server" MaxLength="10" ToolTip=""></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-row align-items-center my-2">
                                                <div class="form-group col-lg-3 text-right">
                                                    <asp:Label AssociatedControlID="tbAdvDetailsOrderDate" CssClass="form-control-label" Font-Bold="true" ID="lblAdvDetailsOrderDate" runat="server" Text="">Order Date</asp:Label>
                                                </div>
                                                <div class="form-group col-lg-4 text-right">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="tbAdvDetailsOrderDate" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="DD/MM/YYYY" runat="server" MaxLength="10" ToolTip="Enter Order Date"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-row align-items-center my-2">
                                                <div class="form-group col-lg-3 text-right">
                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" ID="lblUploadFile">Upload File</asp:Label>

                                                </div>

                                                <div class="form-group col-lg-6 text-left">
                                                    <asp:Button ID="btnUploadpdf" OnClick="btnUploadpdf_Click" runat="server" CausesValidation="False" CssClass="button1"
                                                        Style="display: none" TabIndex="18" Text="Upload Image" Width="80px" />
                                                    <asp:FileUpload ID="fudocfile" runat="server" Style="color: black; background-color: #eaf4ff; border: none; width: 190px;" CssClass="btn btn-success btn-sm"
                                                        onchange="UploadPDF(this);" TabIndex="9" accept="application/pdf" />
                                                    <asp:Label ID="lblPDF" runat="server" CssClass="form-control-label"></asp:Label>
                                                </div>
                                                <div class="form-group col-lg-3 text-left">
                                                    <asp:LinkButton ID="lbtnfileUpload" Visible="false" runat="server" ToolTip="Click here to Download" CssClass="btn btn bg-gradient-green btn-sm text-white"><i class="fa fa-download" title="Click here to Download"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtndeletefileUpload" Visible="false" runat="server" ToolTip="Click here to Delete" class="btn btn-danger btn-sm"><i class="fa fa-trash" title="Click here to Delete"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="form-row align-items-center py-2" autocomplete="off">
                                                <div class="form-group col-lg-3 text-right">

                                                    <asp:Label AssociatedControlID="tbRemarks" CssClass="form-control-label" Font-Bold="true" ID="lblRemarks" runat="server" Text="">Description</asp:Label>
                                                </div>
                                                <div class="form-group col-lg-9 text-right">
                                                    <div class="input-group ">
                                                        <asp:TextBox ID="tbRemarks" Rows="5" TextMode="MultiLine" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="Maximum 1000 characters" runat="server" MaxLength="1000" ToolTip="Enter Description" Style="resize: none;"></asp:TextBox>

                                                    </div>

                                                </div>


                                            </div>
                                            <div class="form-row left">
                                                <div class="form-group col-lg-11 text-center">
                                                    <asp:LinkButton ID="lbtnSaveAgentAdvertisementRegistration" OnClick="lbtnSaveAgentAdvertisementRegistration_Click" runat="server" class="btn btn-success">
                                    <i class="fa fa-save"></i> Save</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnResetAgentAdvertisementRegistration" OnClick="lbtnResetAgentAdvertisementRegistration_Click" runat="server" CssClass="btn btn-danger">
                                    <i class="fa fa-undo"></i> Reset</asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>

                                    </div>
                                    <div class="col-md-6 col-lg-6">
                                          <div class="row m-0 mt-2">
                                                <div class="col-lg-12  pr-0">

                                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h3> Last advertising publishing details</h3></asp:Label>

                                                </div>
                                            </div>
                                       
                                         <div class="row m-0 align-items-center">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="gvAgentRegistrationdate" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                    Visible="true" CssClass="table noborder" ShowHeader="false" OnPageIndexChanging="gvAgentRegistrationdate_PageIndexChanging" DataKeyNames="" AllowPaging="true" PageSize="10">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <span style="font-weight: bold;">Registration Start Date</span>
                                                                <asp:Label runat="server" Text='<%# Eval("reg_from_dt") %>' Style="color: Red;"></asp:Label>
                                                                <br />
                                                                <br />
                                                                <span style="font-weight: bold;">End Date</span>
                                                                <asp:Label runat="server" Text='<%# Eval("reg_to_date") %>' Style="color: Red;"></asp:Label>
                                                                <i class="fa fa-rupee"></i>
                                                                <br />
                                                                <br />
                                                                <span style="font-weight: bold;">Processing Start Date</span>
                                                                <asp:Label runat="server" Text='<%# Eval("proc_from_dt") %>'
                                                                    Style="color: Red;"></asp:Label>
                                                                <br />
                                                                <br />
                                                                <span style="font-weight: bold;">Processing End Date</span>
                                                                <asp:Label runat="server" Text='<%# Eval("proc_to_dt") %>'
                                                                    Style="color: Red;"></asp:Label>
                                                                <br />
                                                                <br />
                                                                <span style="font-weight: bold;">Updated On</span>
                                                                <asp:Label runat="server" Text='<%# Eval("actiondate") %>'
                                                                    Style="color: Red;"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Panel runat="server" ID="PanelNoRecordCurrentAdvertisement" Visible="true" CssClass="text-center" Width="100%">
                                                    <p class="text-center" style="font-size: 32px; font-weight: bold; color: #e3e3e3; margin-top: 50px;">
                                                        Advertisement Not Available
                                                    </p>
                                                </asp:Panel>
                                            </div>
                                        </div>
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
        <cc1:ModalPopupExtender ID="mpAgentAdverisementHistory" runat="server" PopupControlID="pnlAgentAdvertisementsHistory"
            TargetControlID="btn1" CancelControlID="lbtn1" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlAgentAdvertisementsHistory" runat="server" Style="position: fixed; top: 48.5px; width: 800px;">
            <div class="card">
                <div class="card-header">
                    <div class="row m-0 align-items-center">
                        <div class="col-md-10 ">
                            <h3 class="mb-1">
                                <strong class="card-title">Agent Advertisement History</strong>
                            </h3>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="lbtnAgentAdvertisementDownload" OnClick="lbtnAgentAdvertisementDownload_Click" Visible="false" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white" ToolTip="Download Advance days booking History">  <i class="fa fa-download"></i> </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="card-body" style="padding: 15px !important;">

                    <div class="col-md-12 col-lg-12">
                        <asp:GridView ID="gvAgentRegDatesHistory" runat="server" AutoGenerateColumns="False" GridLines="None"
                            Visible="true" ShowHeader="true" CssClass="table text-center table-striped" OnPageIndexChanging="gvAgentRegDatesHistory_PageIndexChanging" DataKeyNames="" AllowPaging="true" PageSize="3">
                            <Columns>
                                <asp:TemplateField HeaderText="Registartion Date">
                                    <ItemTemplate>

                                        <asp:Label runat="server" Text='<%# Eval("reg_from_dt") %>'></asp:Label>
                                        <br />
                                        To<br />
                                        <asp:Label runat="server" Text='<%# Eval("reg_to_date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Processing Date">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("proc_from_dt") %>' ></asp:Label>
                                        <br />
                                        To<br />
                                        <asp:Label runat="server" Text='<%# Eval("proc_to_dt") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Updation Date/Time">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("actiondate") %>' ></asp:Label>
                                      
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField  HeaderText="Updated By">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Eval("actionby") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Panel runat="server" ID="pnlNoRecordAgentAdvertisementsHistory" Visible="true" CssClass="text-center" Width="100%">
                            <p class="text-center" style="font-size: 32px; font-weight: bold; color: #e3e3e3; margin-top: 50px;">
                                Advertisement History Not Available
                            </p>
                        </asp:Panel>
                    </div>
                </div>
                <div class="card-footer">
                    <asp:LinkButton ID="lbtn1" runat="server" CssClass="btn btn-success" Style="font-size: 11pt; float: right; border-radius: 4px;"><i class="fa fa-times"></i> OK</asp:LinkButton>

                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="btn1" runat="server" Text="" />
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

</asp:Content>

