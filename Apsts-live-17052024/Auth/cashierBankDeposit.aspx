<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Auth/cashierMaster.master" AutoEventWireup="true" CodeFile="cashierBankDeposit.aspx.cs" Inherits="Auth_cashierBankDeposit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            var todayDate = new Date().getDate();
            var endD = new Date(new Date().setDate(todayDate));
            var currDate = new Date();
            var startDt = $("[id$=hdstartdate]").val();
            $('[id*=txtdate]').datepicker({
                startDate: startDt,
                endDate: endD,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
        });
    </script>
    <script type="text/javascript">
        function UploadPDF(fileUpload) {
            //alert(1);
            if ($('#fudocfile').value != '') {
                document.getElementById("<%=btnUploadpdf.ClientID %>").click();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdstartdate" runat="server" />
      <asp:HiddenField ID="hidtoken" runat="server" />
    <div class="container-fluid pt-2 pb-5">
        <div class="row align-items-center">
            <div class="col-xl-12">
                <h4 class="mb-1">Summary As On
                                            <asp:Label runat="server" ID="lbldatesummary"></asp:Label>
                </h4>
                <div class="card card-stats mb-3">
                    <div class="row">
                        <div class="col-3 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">

                                        <div class="row mt-2">
                                            <div class="col-6 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-muted mb-0">Collection
                                                            <br />
                                                            <asp:Label ID="lbltotalCollection" runat="server" data-toggle="tooltip" data-placement="bottom" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-muted mb-0">deposit<br />
                                                            <asp:Label ID="lbltotaldeposit" runat="server" data-toggle="tooltip" data-placement="bottom" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-6 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h4 class="mb-1">Opening/Closing Summary
                                        </h4>
                                        <div class="row m-0">
                                            <div class="col-6 border-right">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-muted mb-0">Opened By&nbsp;
                                                            <asp:Label ID="lblopenby" runat="server" data-toggle="tooltip" data-placement="bottom" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>

                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-muted mb-0">Opened At&nbsp;
                                                            <asp:Label ID="lblopenat" runat="server" data-toggle="tooltip" data-placement="bottom" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>

                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-muted mb-0">Opening Balance&nbsp;
                                                            <asp:Label ID="lblopenbal" runat="server" data-toggle="tooltip" data-placement="bottom" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>

                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-6">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-muted mb-0">Closed By&nbsp;
                                                            <asp:Label ID="lblcloseby" runat="server" data-toggle="tooltip" data-placement="bottom" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>

                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-muted mb-0">Closed At&nbsp;
                                                            <asp:Label ID="lblcloseat" runat="server" data-toggle="tooltip" data-placement="bottom" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>

                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-muted mb-0">Closed Balance&nbsp;
                                                            <asp:Label ID="lblclosebal" runat="server" data-toggle="tooltip" data-placement="bottom" CssClass="h3 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h5>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-md-12 text-center">
                                        <h4 class="card-title text-muted mb-1">Current Balance<br />
                                            <asp:Label ID="lblcurrentbal" runat="server" data-toggle="tooltip" data-placement="bottom" CssClass="h3 font-weight-bold mb-0" Text="0"></asp:Label>
                                            <i class="fa fa-rupee-sign"></i>
                                        </h4>
                                        <%--  <div class="row m-0">
                                            <div class="col-12">
                                                <asp:LinkButton ID="lbtnbankdeposit" runat="server" OnClick="lbtnbankdeposit_Click" CssClass="btn btn-success" Style="width: 100%; box-shadow: rgb(0 0 0 / 24%) 0px 3px 8px;">
                                    <i class="fa fa-arrow-left"></i> Go To Dashboard
                                                </asp:LinkButton>
                                            </div>
                                        </div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <div class="card shadow" style="min-height: 80vh;">
                    <div class="card-header pb-2 pt-2">
                        <h4>Please Note</h4>
                    </div>
                     <div class="card-body text-danger" >
                        <div class="row">
                            1. Make sure mandatory cash should be deposited to bank time to time.
                        </div>
                         <div class="row">
                           2. After deposit the cash in the bank and submit the cash deposit details here.
                        </div>
                         <div class="row">
                            3. You can see the pending deposits and verified deposits here.
                        </div>
                         
                    </div>
                </div>
            </div>
            <div class="col-md-5">
                <div class="card shadow" style="min-height: 80vh;">
                    <div class="card-header pb-2 pt-2">
                        <div class="row pl-3">
                            <div class="col-md-8">
                                <h4>Cash Bank Deposit<br />
                                    <span class="text-danger text-sm">All Marked * Fields are mandatory</span></h4>
                            </div>
                            <%--  <div class="col-md-4 text-right">
                                <asp:LinkButton ID="LinkButtonInfo" runat="server" class="btn text-danger">help <i class="fa fa-info-circle" ></i></asp:LinkButton>
                            </div>--%>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="form-row mt-3">
                            <div class="form-group col-md-4">
                                <label for="ddhead">
                                    Date <span class="text-danger">*</span></label>
                                <asp:TextBox runat="server" MaxLength="10" ID="txtdate" placeholder="DD/MM/YYYY" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4">
                                <label for="ddhead">
                                    Bank <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlbank" Enabled="true" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-md-4">
                                <label for="ddhead">
                                    Amount(Rs.) <span class="text-danger">*</span></label>
                                <asp:TextBox runat="server" MaxLength="8" ID="txtamt" Text="0" placeholder="Amount" CssClass="form-control"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,custom"
                                    ValidChars="." TargetControlID="txtamt" />
                            </div>
                        </div>
                        <div class="form-row mt-3">
                            <div class="form-group col-md-4">
                                <label for="ddhead">
                                    Receipt No. <span class="text-danger">*</span></label>
                                <asp:TextBox runat="server" ID="txtreceiptno" MaxLength="20" placeholder="Max. 20 Characters" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4">
                                <label for="ddhead">
                                    Upload Receipt </label>

                                  <asp:Button ID="btnUploadpdf" OnClick="btnUploadpdf_Click" runat="server" CausesValidation="False" CssClass="button1"
                                                Style="display: none" TabIndex="18" Text="Upload File" Width="80px" />
                                <asp:FileUpload runat="server" ID="fudocfile" onchange="UploadPDF(this);" /><br />
                                 <asp:Label runat="server" ID="lblFileName" CssClass="col-form-label control-label" Style="font-size: 12px; font-weight: normal;"></asp:Label>
                                <asp:Label runat="server" Text="(jpg, jpeg Only Maximum 1 MB)" ForeColor="Red" Font-Size="Smaller"></asp:Label>
                            </div>

                        </div>
                        <div class="form-row mt-3">
                            <div class="form-group col-md-8">
                                <label for="ddhead">
                                    Remark <span class="text-success">(Optional)</span></label>
                                <asp:TextBox runat="server" ID="txtremark" MaxLength="100" TextMode="MultiLine" Height="50px" placeholder="Max. 100 characters" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-12 mt-2 text-center">
                            <asp:LinkButton runat="server" ID="btnsave" OnClick="btnsave_Click" CssClass="btn btn-success"><i class="fa fa-check"></i> Save</asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lbtnreset" CssClass="btn btn-danger"><i class="fa fa-refresh"></i> Reset</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card shadow" style="min-height: 5vh;">
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-12 text-center">
                                <asp:Label runat="server" ID="Label3" Text="Mandatory Amount To Deposit "></asp:Label>
                                <asp:Label runat="server" ID="lblMandatoryAmountSum" Text="0"></asp:Label><i class="fa fa-rupee-sign ml-1"></i>
                            </div>

                        </div>
                        <div class="row mt--2">
                            <div class="col-12 text-center">
                                <asp:Label runat="server" ID="Label2" ForeColor="Green" Font-Size="Smaller" Text="(Excluding Pending For Verification)"></asp:Label>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="card shadow" style="min-height: 5vh;">
                    <div class="card-body ">
                        <div class="row">
                            <div class="col-12 text-center">
                                <asp:Label runat="server" ID="Label4" Text="Total Amount For Bank Deposit"></asp:Label>
                                <asp:Label runat="server" ID="lblTotalAmountSum" Text="0"></asp:Label><i class="fa fa-rupee-sign ml-1"></i>
                            </div>

                        </div>
                        <div class="row mt--2">
                            <div class="col-12 text-center">
                                <asp:Label runat="server" ID="llbDatetimetotaldeposit" ForeColor="Green" Font-Size="Smaller" Text=""></asp:Label>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="card shadow" style="min-height: 40vh;">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <h4 class="text-primary">Pending Amount For Verification <%--<span>&#8377;</span>--%>
                                    <asp:Label runat="server" ID="lblPendingamt" CssClass="mr-1" Text="0"></asp:Label><span>&#8377;</span></h4>
                            </div>
                            <div class="col-12">

                                <asp:GridView ID="gvPendingTransaction" OnRowCommand="gvPendingTransaction_RowCommand" runat="server" EnableModelValidation="True"
                                    AllowPaging="True" Font-Size="12pt" GridLines="None" class="table table-striped table-responsive-sm"
                                    AutoGenerateColumns="False" DataKeyNames="receipt_no,r_id" OnPageIndexChanging="gvPendingTransaction_PageIndexChanging"
                                    PageSize="4">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Receipt No." HeaderStyle-Font-Size="12pt" ItemStyle-Font-Size="12pt">
                                            <ItemTemplate>
                                                <asp:Label ID="linkbtnBUSREGISTRATIONNO" runat="server" Text='<%#Bind("receipt_no") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" HeaderStyle-Font-Size="12pt" ItemStyle-Font-Size="12pt">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblCHECHISNO2" Text='<%#Bind("amt") %>' /><i class="fa fa-rupee-sign ml-1"></i>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" HeaderStyle-Font-Size="12pt" ItemStyle-Font-Size="12pt">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblCHECHISNO3" Text='<%#Bind("verify_date") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnView" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' runat="server" ToolTip="Click here View Details" CssClass="btn btn-sm btn-info" CommandName="ViewDetails"><i class="fa fa-eye "></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#eaf4ff" />
                                    <HeaderStyle BackColor="aliceblue" ForeColor="black" VerticalAlign="Top" CssClass="table_head" />
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>
                                <asp:Panel runat="server" ID="pnlPendingNodata" Visible="false">
                                    <h1 class="text-center text-muted mt-5">Transactions
                                       <br />
                                        Not Found
                                    </h1>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card shadow" style="min-height: 25vh;">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <h4 class="text-primary">Amount Bank Deposited list </h4>

                            </div>
                            <div class="col-12">

                                <asp:GridView ID="gvVerifyList" OnRowCommand="gvVerifyList_RowCommand" runat="server" EnableModelValidation="True"
                                    AllowPaging="True" Font-Size="12pt" GridLines="None" class="table table-striped table-responsive-sm"
                                    AutoGenerateColumns="False" DataKeyNames="receipt_no,r_id" OnPageIndexChanging="gvVerifyList_PageIndexChanging"
                                    PageSize="5">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Receipt No." HeaderStyle-Font-Size="12pt" ItemStyle-Font-Size="12pt">
                                            <ItemTemplate>
                                                <asp:Label ID="linkbtnBUSREGISTRATIONNO" runat="server" Text='<%#Bind("receipt_no") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" HeaderStyle-Font-Size="12pt" ItemStyle-Font-Size="12pt">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblCHECHISNO1" Text='<%#Bind("amt") %>' /><i class="fa fa-rupee-sign ml-1"></i>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" HeaderStyle-Font-Size="12pt" ItemStyle-Font-Size="12pt">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblCHECHISNO" Text='<%#Bind("verify_date") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnView" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' runat="server" ToolTip="Click here View Details" CssClass="btn btn-sm btn-info" CommandName="ViewDetails"><i class="fa fa-eye "></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>


                                    <AlternatingRowStyle BackColor="#eaf4ff" />
                                    <HeaderStyle BackColor="aliceblue" ForeColor="black" VerticalAlign="Top" CssClass="table_head" />
                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                </asp:GridView>
                                <asp:Panel runat="server" ID="pnlVerifyAmtNodata" Visible="false">
                                    <h1 class="text-center text-muted mt-4">Transactions
                                       <br />
                                        Not Found
                                    </h1>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpdepositdetails" runat="server" PopupControlID="pnldepositdetails"
                CancelControlID="lbtnclose" TargetControlID="Button3" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnldepositdetails" runat="server" Style="position: fixed;">
                <div class="card">
                    <div class="card-header" style="background-color: #ffc107; padding-bottom: 3px;">
                        <h5 class="card-title">
                            <span>Chest Bank Deposit Details</span>
                        </h5>
                    </div>
                    <div class="card-body" style="min-height: 100px; padding-bottom: 0px;">
                        <table style="width: 100%;" class="table">
                            <tr>
                                <td>
                                    <span style="">Chest Name</span>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="plblchtname" ForeColor="black" Text="NA"></asp:Label>
                                </td>
                                <td>
                                    <span style="">Bank Name</span>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="plblbnkname" ForeColor="black" Text="NA"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span style="">Deposit By</span>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="plbldoptby" ForeColor="black" Text="NA"></asp:Label>
                                </td>
                                <td>
                                    <span style="">Bank Deposit Date</span>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="plbldoptdate" ForeColor="black" Text="NA"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span style="">Deposit Amount</span>
                                </td>
                                <td>
                                    <span>&#8377;</span>
                                    <asp:Label runat="server" ID="plblamt" ForeColor="black" Text="NA"></asp:Label>
                                </td>
                                <td>
                                    <span style="">Receipt Number</span>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="plblreceipt" ForeColor="black" Text="NA"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span style="">Deposit Entry Date</span>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="plbldoptentrydate" ForeColor="black" Text="NA"></asp:Label>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <span style="">Verify By</span>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="plblverifyby" ForeColor="black" Text="NA"></asp:Label>
                                </td>
                                <td>
                                    <span style="">Verify Date</span>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="plblverifydate" ForeColor="black" Text="NA"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="card-footer">
                        <asp:LinkButton ID="lbtnclose" runat="server" CssClass="btn btn-warning" Style="height: 30px; width: 90px; padding-top: 4px; font-size: 10pt; border-radius: 4px; float: right;"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button3" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpOpenClose" runat="server" PopupControlID="pnlOpenClose"
                CancelControlID="Button6" TargetControlID="Button5" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlOpenClose" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-body" style="min-height: 100px;">
                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align: center;">Welcome
                                    <br />
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trChestAmt" runat="server" visible="false">
                                <td style="text-align: center;">
                                    <asp:Label ID="lblAmt" runat="server" Text="Please Check/Verify Total Amount in your chest"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblCloseBlnc" runat="server" Font-Bold="true" Text="0"></asp:Label>
                                    <i class="fa fa-rupee"></i>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;">
                                    <asp:Label ID="lblmsg1" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;">
                                    <br />
                                    <asp:LinkButton ID="btnYes" runat="server" OnClick="btnYes_Click" CssClass="btn btn-success" Style="height: 30px; width: 90px; padding-top: 4px; font-size: 10pt; border-radius: 4px;"> <i class="fa fa-check"></i> YES </asp:LinkButton>
                                    <asp:LinkButton ID="btnNo" runat="server" OnClick="btnNo_Click" CssClass="btn btn-warning" Style="height: 30px; width: 90px; padding-top: 4px; font-size: 10pt; border-radius: 4px;"> <i class="fa fa-times"></i> No </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button5" runat="server" Text="" />
                    <asp:Button ID="Button6" runat="server" Text="" />
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

