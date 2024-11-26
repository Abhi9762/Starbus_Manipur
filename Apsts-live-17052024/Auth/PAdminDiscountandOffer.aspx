<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/PortalAdmmaster.master" AutoEventWireup="true" CodeFile="PAdminDiscountandOffer.aspx.cs" Inherits="Auth_PAdminDiscountandOffer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .headerCss {
            color: #8898aa;
            border-color: #e9ecef;
            background-color: #f6f9fc;
            text-align: center;
            font-weight: bold;
        }

        .rbl input[type="radio"] {
            margin-left: 10px;
            margin-right: 10px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {

            var currDate = new Date().getDate();
            var preDate = new Date(new Date().setDate(currDate - 1));
            var todayDate = new Date(new Date().setDate(currDate));

            $('[id*=tbFrom]').datepicker({
                startDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            })
                .on('changeDate', function (selected) {
                    var minDate = new Date(selected.date.valueOf());
                    $('[id*=tbTo]').datepicker('setStartDate', minDate);
                });
            $('[id*=tbTo]').datepicker({
                startDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });

            $('[id*=tbFromD]').datepicker({
                startDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            })
                .on('changeDate', function (selected) {
                    var minDate = new Date(selected.date.valueOf());
                    $('[id*=tbToD]').datepicker('setStartDate', minDate);
                });
            $('[id*=tbToD]').datepicker({
                startDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });



            $('[id*=tbFromValidity]').datepicker({
                startDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            })
                .on('changeDate', function (selected) {
                    var minDate = new Date(selected.date.valueOf());
                    $('[id*=tbToValidity]').datepicker('setStartDate', minDate);
                });
            $('[id*=tbToValidity]').datepicker({
                startDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
        });
        function UploadImageWeb(fileUpload) {
            if ($('#ImgWebPortal').value != '') {
                document.getElementById("<%=btnUploadWebPortal.ClientID %>").click();
            }
        }
        function UploadImageMob(fileUpload) {
            if ($('#imgMobileApp').value != '') {
                document.getElementById("<%=btnUploadMobileApp.ClientID %>").click();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid mt-1 mb-7">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="row">
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h4 class="mb-1">
                                            <asp:Label ID="lblSummary" runat="server" CssClass="text-capitalize">Summary as on Date 01/12/2021 04:00pm</asp:Label></h4>
                                        <div class="row m-0">
                                            <div class="col-12">
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Total Offers:&nbsp;
                                 <asp:Label ID="lbltotalOffers" runat="server" ToolTip="Total Offers Available" CssClass="h3 font-weight-bold mb-0 text-right float-right"></asp:Label></h5>
                                                    </div>
                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Published: &nbsp;
                                <asp:Label ID="lblPublished" runat="server" ToolTip="Offers Published" CssClass="h3 font-weight-bold mb-0 text-right float-right"></asp:Label></h5>
                                                    </div>

                                                    <div class="col-12">
                                                        <h5 class="card-title text-uppercase text-muted mb-0">Pending:&nbsp;
                                 <asp:Label ID="lblPending" runat="server" ToolTip="Offers Pending" CssClass="h3 font-weight-bold mb-0 text-right float-right"></asp:Label></h5>
                                                    </div>

                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-12">
                                        <div>
                                            <h4 class="mb-1">Generate Offer Report</h4>
                                        </div>
                                        <asp:Label runat="server" CssClass="form-control-label">Validity</asp:Label>
                                        <div class="input-group">
                                            <div class="col-md-5 col-lg-5">
                                                <label class="text-muted form-control-label">From</label>
                                                <asp:TextBox ID="tbFromD" ToolTip="Enter Discount Starting date" runat="server" autocompletee="off" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>
                                            </div>

                                            <div class="col-md-5 col-lg-5">
                                                <label class="text-muted form-control-label">To</label>
                                                <asp:TextBox ID="tbToD" ToolTip="Enter Discount ending date" runat="server" autocompletee="off" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>

                                            </div>

                                            <div class="col-md-2 col-lg-2 mt-4">
                                                <asp:LinkButton ID="lbtndownload" ToolTip="Download" runat="server" CssClass="btn btn bg-gradient-green btn-sm text-white">
                                            <i class="fa fa-download"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </div>



                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="col-4">
                            <div class="card-body">
                                <div class="row mr-0">
                                    <div class="col">
                                        <div>
                                            <h4 class="mb-1">Instructions</h4>
                                        </div>
                                        <asp:Label runat="server" CssClass="form-control-label">1. Offer Code Will be AlphaNumeric.</asp:Label><br />
                                        <asp:Label runat="server" CssClass="form-control-label">2. Offer Code cannot be edited.</asp:Label>
                                    </div>
                                    <div class="col-auto">
                                        <asp:LinkButton ID="lbtnview" OnClick="lbtnview_Click" ToolTip="View Instructions" runat="server" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" class="btn btn bg-gradient-green btn-sm text-white" ToolTip="Download Instruction">
                                            <i class="fa fa-download"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>

        <div class="row m-0">
            <div class="col-md-6 col-lg-6 order-xl-1">
                <div class="card" style="min-height: 700px">
                    <div class="card-header">

                        <div class="row m-0">

                            <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h2>Discount Status </h2></asp:Label>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row m-0">
                            <div class="col-md-12 col-lg-12">
                                <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="12pt">Pending for Publishing</asp:Label>
                            </div>
                        </div>

                        <div class="row m-0">
                            <div class="col-md-12 col-lg-12">
                                <asp:GridView ID="gvOfferDraft" runat="server" PageSize="5" OnRowCommand="gvOfferDraft_RowCommand" AutoGenerateColumns="False" GridLines="None"
                                    OnPageIndexChanging="gvOfferDraft_PageIndexChanging" AllowSorting="true" AllowPaging="true" CssClass="table table-striped table-hover w-100"
                                    HeaderStyle-CssClass="thead-light text-center font-weight-bold" OnRowCreated="gvOfferDraft_RowCreated"
                                    DataKeyNames="couponid,couponcode,coupontitle,discountdescription,discounttype,discounton,discountamount,maxdiscount_amount,validfrom_date,
                                    validto_date,img_web,img_app">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("coupontitle") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="validfrom_date" HeaderText="From" ItemStyle-Width="50"
                                            DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="validto_date" HeaderText="To" ItemStyle-Width="50"
                                            DataFormatString="{0:dd/MM/yyyy}" />


                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" ToolTip="Edit Discount details" CommandName="Modify" runat="server" CssClass="btn btn-sm btn-warning"><asp:Label runat="server" CssClass="form-control-label1"><i class="fa fa-edit">&nbsp;</i> </asp:Label> </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnPublish" ToolTip="Publish Discount" CommandName="Publish" runat="server" CssClass="btn btn-sm btn-success"><asp:Label runat="server" CssClass="form-control-label1"><i class="fa fa-check">&nbsp; </i> </asp:Label> </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnDelete" ToolTip="Delete Discount" CommandName="Remove" runat="server" CssClass="btn btn-sm btn-danger"><asp:Label runat="server" CssClass="form-control-label1"><i class="fa fa-trash">&nbsp;</i> </asp:Label> </asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                </asp:GridView>
                            </div>
                            <div class="col-md-12 col-lg-12">
                                <asp:Label ID="lblnorecord" Visible="false" runat="server" CssClass="text-uppercase text-muted font-weight-bold">
                                    No Draft Offer Found
                                </asp:Label>

                            </div>
                        </div>


                        <div class="row m-0">
                            <div class="col-md-12 col-lg-12">
                                <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="12pt">Currently Active</asp:Label>

                            </div>
                        </div>


                        <div class="row m-0">
                            <div class="col-md-12 col-lg-12">
                                <asp:GridView ID="gvOffer" OnRowCreated="gvOffer_RowCreated" runat="server" PageSize="5" OnRowCommand="gvOffer_RowCommand" AutoGenerateColumns="False" GridLines="None" OnPageIndexChanging="gvOffer_PageIndexChanging" AllowSorting="true" AllowPaging="true" CssClass="table table-striped table-hover"
                                    HeaderStyle-CssClass="thead-light text-center font-weight-bold" DataKeyNames="couponid,couponcode,coupontitle,discountdescription,discounttype,discounton,discountamount,maxdiscount_amount,validfrom_date,validto_date,img_web,img_app">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("coupontitle") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="validfrom_date" HeaderText="From" ItemStyle-Width="70" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="validto_date" HeaderText="To" ItemStyle-Width="70" DataFormatString="{0:dd/MM/yyyy}" />

                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnview" ToolTip="View Discount Details" runat="server" CommandName="viewoffer" CssClass="btn btn-sm btn-primary"><i class="fa fa-eye">&nbsp;</i> </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnChangeValidity" ToolTip="Change Discount Validity" runat="server" CommandName="change_validity" CssClass="btn btn-sm btn-success"><i class="fa fa-edit">&nbsp; </i></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnDiscountinue" ToolTip="Discontinue Discount" runat="server" CommandName="Discontinue" CssClass="btn btn-sm btn-danger"><i class="fa fa-trash">&nbsp;</i></asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                </asp:GridView>
                            </div>
                            <div class="col-md-12 col-lg-12">
                                <asp:Label ID="lblNoActiveoffer" Visible="false" runat="server" CssClass="text-uppercase text-muted font-weight-bold">
                                    No Active Offer Found
                                </asp:Label>
                            </div>

                        </div>


                    </div>
                </div>
            </div>

            <div class="col-lg-6 col-md-6 order-xl-2">
                <asp:Panel ID="pnladd" runat="server">
                    <div class="card" style="min-height: 700px">
                        <div class="card-header">
                            <div class="row m-0">
                                <div class="col-md-6 col-lg-6">
                                    <asp:Label ID="lblCreate" runat="server" CssClass="form-control-label" Font-Bold="true"><h2>Create Discount/Offer</h2></asp:Label>
                                    <asp:Label ID="lblEdit" Visible="false" runat="server" CssClass="form-control-label" Font-Bold="true"><h2>Update Discount/Offer</h2></asp:Label>
                                </div>
                                <div class="col-md-6 col-lg-6 text-right">
                                    <p style="font-size: small; margin: 0px;" class="text-danger font-weight-bold">All marked * Fields are mandatory</p>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-6 col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">Offer Code<span class="text-warning">*</span> </asp:Label>
                                    <asp:TextBox AutoComplete="off" ID="tbOfferCode" ToolTip="Enter Offer Code" runat="server" CssClass="form-control text-uppercase form-control-sm" MaxLength="20" placeholder="max 20 chars"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers, UppercaseLetters,LowercaseLetters" TargetControlID="tbOfferCode" />
                                </div>
                                <div class="col-md-6 col-lg-6">
                                    <asp:Label ID="lblTitle" runat="server" CssClass="form-control-label">Title<span class="text-warning">*</span> </asp:Label>
                                    <asp:TextBox AutoComplete="off" ID="tbTitle" ToolTip="Enter Title" runat="server" CssClass="form-control form-control-sm" MaxLength="20" placeholder="max 20 chars"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbTitle" ValidChars=" " />
                                </div>
                            </div>
                            <div class="row border-bottom pb-2">
                                <div class="col-md-6 col-lg-6">
                                    <asp:Label ID="lblDescription" runat="server" CssClass="form-control-label">Description<span class="text-warning">*</span> </asp:Label>
                                    <asp:TextBox AutoComplete="off" ID="tbDescription" ToolTip="Enter Description" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine" resize="none" MaxLength="200" placeholder="max 200 chars"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12 col-lg-12 pt-2">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="12pt">Discount Details</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">Type<span class="text-warning">*</span> </asp:Label>
                                    <asp:RadioButtonList ID="rbtnDiscountType" runat="server" CssClass="rbl"
                                        RepeatDirection="Horizontal" ToolTip="Select Discount Type" Height="25px">
                                        <asp:ListItem Value="P" Text="Percentage" Selected="False"></asp:ListItem>
                                        <asp:ListItem Value="A" Text="Amount" Selected="False"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-md-6 col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">Applicable to<span class="text-warning">*</span> </asp:Label>
                                    <asp:RadioButtonList ID="rbtnDiscountApplicable" runat="server" CssClass="rbl"
                                        RepeatDirection="Horizontal" ToolTip="Select Discount Applicable to" Height="25px">
                                        <asp:ListItem Value="S" Text="Per Seat" Selected="False"></asp:ListItem>
                                        <asp:ListItem Value="B" Text="Per Bus" Selected="False"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="row border-bottom pb-2">
                                <div class="col-md-6 col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">Discount Amount/Percentage<span class="text-warning">*</span> </asp:Label>
                                    <asp:TextBox AutoComplete="off" ID="tbDiscountAmt" ToolTip="Enter Discount Amount" runat="server" CssClass="form-control form-control-sm" MaxLength="5" placeholder="max 5 digits"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers" TargetControlID="tbDiscountAmt" />

                                </div>
                                <div class="col-md-6 col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">Max Discount Amount<span class="text-warning">*</span> </asp:Label>
                                    <asp:TextBox AutoComplete="off" ID="tbMaxDiscountAmt" ToolTip="Enter Max Discount Amt" runat="server" CssClass="form-control form-control-sm" MaxLength="5" placeholder="max 5 digits"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers" TargetControlID="tbMaxDiscountAmt" />
                                </div>
                            </div>
                            <div class="row pt-2">
                                <div class="col-md-12 col-lg-12 pt-2">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="12pt">Validity</asp:Label>
                                </div>
                            </div>
                            <div class="row border-bottom pb-2">
                                <div class="col-md-6 col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">From<span class="text-warning">*</span> </asp:Label>
                                    <asp:TextBox ID="tbFrom" ToolTip="Enter Discount Starting date" runat="server" autocomplete="off" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>
                                    
                                </div>
                                <div class="col-md-6 col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">To<span class="text-warning">*</span> </asp:Label>
                                    <asp:TextBox ID="tbTo" ToolTip="Enter Discount Ending Date" runat="server" autocomplete="off" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 col-lg-12 pt-2">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="12pt">Images</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4 col-lg-4">
                                    <asp:Label runat="server" CssClass="form-control-label">For Web Portal  </asp:Label>
                                    <span style="color: #ea5d46; font-size: 6pt">(Max 20Kb/ 600*300 Pixel)</span>
                                </div>
                                <div class="col-md-8 col-lg-8">
                                    <asp:Button ToolTip="Upload Web Portal Image" ID="btnUploadWebPortal" OnClick="btnUploadWebPortal_Click" runat="server" CausesValidation="False" CssClass="form-control form-control-sm"
                                        Style="display: none" TabIndex="18" Text="Upload Image" accept=".png,.jpg,.jpeg,.gif" Width="80px" />
                                    <asp:FileUpload ID="FileWebPortal" onchange="UploadImageWeb(this);" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-sm btn-success" TabIndex="9" />
                                    <asp:Image ID="ImgWebPortal" onchange="UploadImageWeb(this);" runat="server" Visible="false" Style="border-width: 0px; height: 50px; width: 50px; border: 2px solid #eaf4ff;" />
                                    <label id="lblwrongimage" runat="server" style="font-size: 7pt; color: Red; line-height: 12px;">
                                        Image size will be less then 20KB<br />
                                        (Only .JPG, .PNG, .JPEG)</label>
                                    <br />
                                </div>
                            </div>
                            <div class="row my-2">
                                <div class="col-md-4 col-lg-4">
                                    <asp:Label runat="server" CssClass="form-control-label">For Mobile App  </asp:Label>
                                    <span style="color: #ea5d46; font-size: 6pt">(Max 10Kb/ 155*18 Pixel)</span>
                                </div>
                                <div class="col-md-8 col-lg-8">
                                    <asp:Button ToolTip="Upload Mobile App image" ID="btnUploadMobileApp" OnClick="btnUploadMobileApp_Click" runat="server"
                                        CausesValidation="False" CssClass="file-upload-inner" accept=".png,.jpg,.jpeg,.gif"
                                        Style="display: none" Text="Upload Image" Width="80px" />
                                    <asp:FileUpload ID="FileMobileApp" onchange="UploadImageMob(this);" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-sm btn-success" TabIndex="9" />
                                    <asp:Image ID="imgMobileApp" onchange="UploadImageMob(this);" runat="server" Visible="false" Style="border-width: 0px; height: 50px; width: 50px; border: 2px solid #eaf4ff;" />
                                    <label id="lblwrongimageMob" runat="server" style="font-size: 7pt; color: Red; line-height: 12px;">
                                        Image size will be less then 10 KB<br />
                                        (Only .JPG, .PNG, .JPEG)</label><br />
                                </div>
                            </div>

                            <div class="col-md-12 col-lg-12 mt-3 text-center">
                                <asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_Click" ToolTip="Save Discount" runat="server" CssClass="btn btn-warning"><i class="fa fa-save"></i>&nbsp;Save as Draft</asp:LinkButton>
                                <asp:LinkButton ID="lbtnUpdate" Visible="false" OnClick="lbtnUpdate_Click" ToolTip="Update Discount" runat="server" CssClass="btn btn-success"><i class="fa fa-save"></i>&nbsp;Update</asp:LinkButton>
                                <asp:LinkButton ID="lbtnSaveandPublish" OnClick="lbtnSaveandPublish_Click" ToolTip="Save and Publish Discount" runat="server" CssClass="btn btn-success"><i class="fa fa-save"></i>&nbsp;Save and Publish </asp:LinkButton>
                                <asp:LinkButton ID="lbtnReset" ToolTip="Reset Discount" runat="server" OnClick="lbtnReset_Click" CssClass="btn btn-danger"><i class="fa fa-undo"></i> &nbsp;Reset </asp:LinkButton>
                                <asp:LinkButton ID="lbtnCancelD" Visible="false" ToolTip="Cancel Discount" runat="server" OnClick="lbtnCancel_Click" CssClass="btn btn-danger"><i class="fa fa-times"></i> &nbsp;Cancel </asp:LinkButton>

                            </div>
                        </div>
                    </div>
                </asp:Panel>
                
                <asp:Panel ID="pnlView" Visible="false" runat="server">
                    <div class="card" style="min-height: 700px">
                        <div class="card-header">
                            <div class="row m-0">
                                <div class="col-md-6 col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true"><h2>View Discount/Offer Details</h2></asp:Label>
                                </div>
                                <div class="col-md-6 col-lg-6 text-right">
                                    <asp:LinkButton ID="lbtnAddNewOffer" runat="server" OnClick="lbtnAddNewOffer_Click" CssClass="btn btn-danger btn-sm"><i class="fa fa-plus"></i> &nbsp;Add New Offer </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-6 col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">Offer Code </asp:Label><br />
                                    <asp:Label ID="lblOffercode" runat="server" CssClass="text-uppercase"></asp:Label>
                                </div>
                                <div class="col-md-6 col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">Title </asp:Label><br />
                                    <asp:Label ID="lblofferTitle" runat="server"></asp:Label>

                                </div>
                            </div>
                            <div class="row border-bottom pb-2">
                                <div class="col-md-6 col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">Description </asp:Label><br />
                                    <asp:Label ID="lblOfferDescription" runat="server"></asp:Label>

                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12 col-lg-12 pt-2">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="12pt">Discount Details</asp:Label><br />
                                    <asp:Label ID="lblDiscountDetails" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">Type </asp:Label><br />
                                    <asp:Label ID="lblofferType" runat="server"></asp:Label>

                                </div>
                                <div class="col-md-6 col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">Applicable to </asp:Label><br />
                                    <asp:Label ID="lblOfferApplicableTo" runat="server"></asp:Label>

                                </div>
                            </div>
                            <div class="row border-bottom pb-2">
                                <div class="col-md-6 col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">Discount Amount/Percentage </asp:Label><br />
                                    <asp:Label ID="lblofferamount" runat="server"></asp:Label>

                                </div>
                                <div class="col-md-6 col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">Max Discount Amount </asp:Label><br />
                                    <asp:Label ID="lblMaxDiscount" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row pt-2">
                                <div class="col-md-12 col-lg-12 pt-2">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="12pt">Validity</asp:Label><br />
                                </div>
                            </div>
                            <div class="row border-bottom pb-2">
                                <div class="col-md-6 col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">From </asp:Label><br />
                                    <asp:Label ID="lblofferFromDate" runat="server"></asp:Label>


                                </div>
                                <div class="col-md-6 col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">To </asp:Label><br />
                                    <asp:Label ID="lblofferToDate" runat="server"></asp:Label>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-12 col-lg-12 pt-2">
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true" Font-Size="12pt">Images</asp:Label><br />
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-6 col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">For Web Portal  </asp:Label><br />
                                    <asp:Image ID="imgWeb" runat="server" Visible="false" Style="height: 200px; width: 200px;" />

                                </div>
                                <div class="col-md-6 col-lg-6">
                                    <asp:Label runat="server" CssClass="form-control-label">For Mobile App  </asp:Label><br />
                                    <asp:Image ID="imgApp" runat="server" Visible="false" Style="height: 200px; width: 200px;" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

    <div class="row">
        <cc1:ModalPopupExtender ID="mpChangeValidity" runat="server" PopupControlID="pnlChangeValidity"
            CancelControlID="lbtnCancelValidity" TargetControlID="btn" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlChangeValidity" runat="server" Style="position: fixed;">
            <div class="card" style="width: 350px;">
                <div class="card-header">
                    <h4 class="card-title text-left mb-0">Change Validity
                    </h4>
                </div>
                <div class="card-body text-left ml-3">
                    <div class="row border-bottom">
                        <div class="col-md-6 col-lg-6 border-right mb-2">
                            <div class="row">
                                <div class="col-md-12 col-lg-12">

                                    <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">New Validity</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-11 col-lg-11">
                                    <asp:Label runat="server" CssClass="form-control-label">From Date</asp:Label>
                                    <asp:TextBox ID="tbFromValidity" ToolTip="Enter Discount Starting date" runat="server" autocompletee="off" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>

                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-11 col-lg-11">
                                    <asp:Label runat="server" CssClass="form-control-label">To Date</asp:Label>
                                    <asp:TextBox ID="tbToValidity" ToolTip="Enter Discount Starting date" runat="server" autocompletee="off" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>


                                </div>
                            </div>

                        </div>

                        <div class="col-md-6 col-lg-6 mb-2">
                            <asp:Label runat="server" CssClass="form-control-label" Font-Bold="true">Current Validity</asp:Label>

                            <div class="row">
                                <div class="col-md-11 col-lg-11 ml-2">
                                    <asp:Label runat="server" CssClass="form-control-label">From Date</asp:Label>
                                    <br />
                                    <asp:Label ID="lblFromDate" runat="server" CssClass="form-control-label">12/1/2022</asp:Label>

                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-11 col-lg-11 ml-2">
                                    <asp:Label runat="server" CssClass="form-control-label">To Date</asp:Label>
                                    <br />
                                    <asp:Label ID="lblToDate" runat="server" CssClass="form-control-label">14/1/2022</asp:Label>

                                </div>
                            </div>
                        </div>
                    </div>

                    <div style="width: 100%; margin-top: 20px; text-align: right;">
                        <asp:LinkButton ID="lbtnSaveValidity" OnClick="lbtnSaveValidity_Click" runat="server" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Save </asp:LinkButton>
                        <asp:LinkButton ID="lbtnCancelValidity" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> Cancel </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="btn" runat="server" Text="" />
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
                    <h4 class="card-title text-left mb-0">Change Validity
                    </h4>
                </div>

                <div class="card-body text-left pt-2" style="min-height: 100px;">

                    <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                    <div style="width: 100%; margin-top: 20px; text-align: right;">
                        <asp:LinkButton ID="lbtnYesConfirmation" OnClick="lbtnYesConfirmation_Click" runat="server" CssClass="btn btn-sm btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                        <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-sm btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button4" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>

</asp:Content>

