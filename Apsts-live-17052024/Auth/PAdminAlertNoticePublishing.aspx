<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/PortalAdmmaster.master" AutoEventWireup="true" CodeFile="PAdminAlertNoticePublishing.aspx.cs" Inherits="Auth_PAdminAlertNoticePublishing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function UploadImage1(fileUpload) {
            if ($('#Img1').value != '') {
                document.getElementById("<%=btnUploadimage1.ClientID %>").click();
            }
        }
        function UploadImage2(fileUpload) {
            if ($('#Img2').value != '') {
                document.getElementById("<%=btnUploadimage2.ClientID %>").click();
            }
        }
        function UploadPDF(fileUpload) {
            //alert(1);
            if ($('#fudocfile').value != '') {
                document.getElementById("<%=btnUploadpdf.ClientID %>").click();
            }
        }
        $(document).ready(function () {

            var currDate = new Date().getDate();
            var preDate = new Date(new Date().setDate(currDate - 1));
            var todayDate = new Date(new Date().setDate(currDate));


            $('[id*=tbValidityFromDate]').datepicker({
                startDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            })
                .on('changeDate', function (selected) {
                    var minDate = new Date(selected.date.valueOf());
                    $('[id*=tbValidityTo]').datepicker('setStartDate', minDate);
                });
            $('[id*=tbValidityTo]').datepicker({
                startDate: todayDate,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidtoken" runat="server" />
    <div class="container-fluid pt-2">
        <div class="row align-items-center">
            <div class="col-xl-12">
                <div class="card card-stats mb-3">
                    <div class="row">
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-md-12">
                                        <div class=" text-capitalize" style="font-size: medium; font-weight: bold">
                                            <asp:Label ID="lblSummary" runat="server" class="h4 font-weight-bold mb-0">Summary as on </asp:Label>
                                        </div>
                                        <div class="row m-0">
                                            <div class="col-md-12">
                                                <asp:Label runat="server" CssClass=" form-control-label card-title text-uppercase">Total </asp:Label>
                                                <asp:Label ID="lblTotalAlertNotice" runat="server" CssClass="form-control-label float-right" data-toggle="tooltip" data-placement="bottom" title="Total Alert/Notice" Text="0"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row m-0">
                                            <div class="col-md-12">
                                                <asp:Label runat="server" CssClass=" form-control-label card-title text-uppercase">Event</asp:Label>
                                                <asp:Label ID="lblActiveEvents" runat="server" data-toggle="tooltip" data-placement="bottom" title="Deactive Alert/Notice" CssClass="form-control-label float-right" Text="0"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row m-0">
                                            <div class="col-md-12">
                                                <asp:Label runat="server" CssClass=" form-control-label card-title text-uppercase">Alert</asp:Label>
                                                <asp:Label ID="lblActiveAlert" runat="server" data-toggle="tooltip" data-placement="bottom" title="Active Alert/Notice" CssClass="form-control-label float-right" Text="0"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row m-0">
                                            <div class="col-md-12">
                                                <asp:Label runat="server" CssClass=" form-control-label card-title text-uppercase">Notice</asp:Label>
                                                <asp:Label ID="lblActiveNotice" runat="server" data-toggle="tooltip" data-placement="bottom" title="Deactive Alert/Notice" CssClass="form-control-label float-right" Text="0"></asp:Label>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-4 border-right">
                            <div class="card-body">
                                <div class="row m-0">
                                    <div class="col-12">
                                        <h4 class="mb-1">
                                            <asp:Label runat="server" CssClass="text-capitalize">Generate Report</asp:Label></h4>
                                        <div class="mt-3">
                                            <label class="form-control-label">Select Alert/Notice Type</label>
                                            <div class="input-group">
                                                <div class="input-group-prepend pr-2" style="width: 80%">

                                                    <asp:DropDownList ID="ddlAlertNotice" ToolTip="Concession Available" CssClass="form-control form-control-sm" Width="100%" runat="server"></asp:DropDownList>
                                                </div>
                                                <asp:LinkButton ID="lbtndownloadReport" runat="server" ToolTip="Download Office Report" OnClick="lbtndownloadReport_Click"
                                                    CssClass="btn btn bg-gradient-green btn-sm text-white">
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
                                    <div class="row m-0 col-12" style="display: inline-block">
                                        <h4 class="mb-1 float-left">Instructions</h4>
                                        <div class="float-right">
                                            <asp:LinkButton ID="lbtnview" OnClick="lbtnview_Click" data-toggle="tooltip" data-placement="bottom" title="View Instructions" runat="server" Width="30px" CssClass="btn btn bg-gradient-orange btn-sm text-white">
                                    <i class="fa fa-info"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnViewHistory" OnClick="lbtnViewHistory_Click" runat="server" ToolTip="Click here to View Advance Days Booking History" CssClass="btn btn bg-gradient-blue btn-sm text-white">
                                    <i class="fa fa-history"></i>
                                            </asp:LinkButton>

                                        </div>
                                    </div>
                                    <div class="row m-0">
                                        <div class="col-12">
                                            <div class="row m-0">
                                                <ul class="data-list" data-autoscroll>
                                                    <li>
                                                        <asp:Label runat="server" CssClass=" <h6> form-control-label">Events</asp:Label><br />
                                                    </li>
                                                    <li>
                                                        <asp:Label runat="server" CssClass="form-control-label">URL/Pdf are Not Mandatory.</asp:Label><br />
                                                    </li>
                                                    <li>
                                                        <asp:Label runat="server" CssClass="form-control-label">Alert</asp:Label><br />
                                                    </li>
                                                    <li>
                                                        <asp:Label runat="server" CssClass="form-control-label">Subject & Image is Mandatory.</asp:Label>
                                                    </li>
                                                    <li>
                                                        <asp:Label runat="server" CssClass="form-control-label">URL/Pdf and Description are Not Mandatory.</asp:Label>
                                                    </li>
                                                    <li>
                                                        <asp:Label runat="server" CssClass="form-control-label">Notice</asp:Label>
                                                    </li>
                                                    <li>
                                                        <asp:Label runat="server" CssClass="form-control-label">Image Field will not be Visible.</asp:Label>
                                                    </li>
                                                    <li>
                                                        <asp:Label runat="server" CssClass="form-control-label">URL/Document will not be Mandatory.</asp:Label>
                                                    </li>
                                                </ul>
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
            <div class="col-xl-4 order-xl-1">
                <div class="row ">
                    <div class="col-md-12">
                        <div class="card" style="min-height: 500px">
                            <div class="row m-0">
                                <div class="col-md-6">
                                    <div class="card-header border-bottom">
                                        <div class="float-left">
                                            <h4 class="mb-0">Pending For Publishing</h4>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <asp:GridView ID="gvPengingPublishing" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
                                    AllowPaging="true" PageSize="5" OnRowCommand="gvPengingPublishing_RowCommand"
                                    OnPageIndexChanging="gvPengingPublishing_PageIndexChanging" DataKeyNames="categorycode,tempnotice_id,noticecategory_name,sub_ject,subjectlocal,description,description_local,image1,image2,documt,urllink,valid_fromdt,valid_todt">

                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="">
                                                    <div class="row px-3 pt-2">
                                                        <div class="col-md-9">

                                                            <b>
                                                                <asp:Label CssClass="form-control-label text-warning" Font-Bold="true" runat="server"><%#Eval("noticecategory_name") %> </asp:Label></b>
                                                            <asp:Label CssClass="form-control-label" Font-Bold="true" runat="server"> <%# Eval("sub_ject") %> </asp:Label>

                                                        </div>
                                                        <div class="col-md-3 text-right">
                                                            <asp:LinkButton ID="lbtnUpdate" Visible="true" runat="server" CommandName="PendingUpdate" CssClass="btn btn-sm btn-primary" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Update Pending Alert Notice"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnPublish" Visible="true" runat="server" CommandName="PendingPublish" CssClass="btn btn-sm btn-warning" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Publish Pending Alert Notice"> <i class="fa fa-check"></i></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                </div>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                </asp:GridView>
                                <asp:Panel ID="pnlnoRecordfound" Visible="true" runat="server">
                                    <div class="card card-stats">
                                        <div class="card-body">
                                            <div class="row">

                                                <div class="col-lg-12 text-center">
                                                    <i class="fa fa-Concession fa-5x"></i>
                                                </div>
                                                <div class="col-lg-12 text-center mt-4" runat="server" id="div1">
                                                    <span class="h4 font-weight-bold mb-0">Start Pending For Publishing Alert Notice  Creation </span>
                                                    <h4 class="card-title text-uppercase text-muted mb-0">No Pending For Publishing Alert Notice has been registered yet</h4>
                                                </div>
                                                <div class="col-lg-12 text-center mt-4" runat="server" id="div2" visible="false">
                                                    <span class="h2 font-weight-bold mb-0">No Record Found </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </asp:Panel>
                            </div>
                            <div class="card-header border-bottom mt-4">
                                <div class="float-left">
                                    <h4 class="mb-0">Currently Active</h4>
                                </div>
                            </div>
                            <asp:GridView ID="gvCurrentlyActive" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%"
                                AllowPaging="true" PageSize="5" OnPageIndexChanging="gvCurrentlyActive_PageIndexChanging" OnRowCommand="gvCurrentlyActive_RowCommand" DataKeyNames="noticeid,categorycode,noticecategory_name,sub_ject,subjectlocal,description,description_local,image1,image2,documt,urllink,valid_fromdt,valid_todt">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div class="">
                                                <div class="row px-3 pt-2">
                                                    <div class="col-md-9">
                                                        <b>
                                                            <asp:Label CssClass="form-control-label text-warning" Font-Bold="true" runat="server"><%#Eval("noticecategory_name") %> </asp:Label></b>
                                                        <asp:Label CssClass="form-control-label" Font-Bold="true" runat="server"><%# Eval("sub_ject") %> </asp:Label>
                                                    </div>
                                                    <div class="col-md-3 text-right">
                                                        <asp:LinkButton ID="lbtnDiscountinu" Visible="true" runat="server" CommandName="Discontinuenotice" CssClass="btn btn-sm btn-warning" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Discontinue Alert Notice Publishing"> <i class="fa fa-ban"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                                <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                            </asp:GridView>
                            <asp:Panel ID="pnlActivenoRecordfound" Visible="true" runat="server">
                                <div class="card card-stats">
                                    <div class="card-body">
                                        <div class="row">

                                            <div class="col-lg-12 text-center">
                                                <i class="fa fa-Concession fa-5x"></i>
                                            </div>
                                            <div class="col-lg-12 text-center mt-4" runat="server" id="div3">
                                                <span class="h4 font-weight-bold mb-0">Start Currently Active Alert Notice Creation </span>
                                                <h5 class="card-title text-uppercase text-muted mb-0">No Currently Active Alert Notice has been registered yet</h5>
                                            </div>
                                            <div class="col-lg-12 text-center mt-4" runat="server" id="div4" visible="false">
                                                <span class="h2 font-weight-bold mb-0">No Record Found </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xl-8 order-xl-2">

                <asp:Panel ID="pnlAddConcession" runat="server" Visible="true">
                    <div class="card" style="min-height: 500px">
                        <div class="card-header">
                            <div class="row align-items-center ">
                                <div class="col-8">
                                    <h4 class="mb-0">
                                        <asp:Label runat="server" ID="lblAlertNoticeHead" Text="Add Events/Alert/Notice"></asp:Label>
                                        <asp:Label runat="server" ID="lblAlertNoticeUpdate" Text="Update Events/Alert/Notice" Visible="false"></asp:Label>
                                    </h4>
                                </div>
                                <div class="col-md-4 text-right">
                                    <p style="font-size: small; margin: 0px;" class="text-danger font-weight-bold">All marked * Fields are mandatory</p>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="form-row align-items-center mt-3" autocomplete="off">
                                <div class="form-group col-lg-2 mb-0 text-left">
                                    <asp:Label CssClass="form-control-label" Font-Bold="true" runat="server" Text="">1. Type<span class="text-warning">*</span></asp:Label>
                                </div>
                                <div class="form-group col-lg-6">
                                    <div class="input-group ">
                                        <asp:DropDownList ID="ddlTypes" ToolTip="Select Alert Notice Types" OnSelectedIndexChanged="ddlTypes_SelectedIndexChanged" AutoPostBack="" CssClass="form-control form-control-sm" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row align-items-center mt-3" autocomplete="off">
                                <div class="form-group col-lg-2 mb-0 text-left">
                                    <asp:Label CssClass="form-control-label" Font-Bold="true" ID="lblSubject" runat="server" Text="">2. Subject<span class="text-warning">*</span></asp:Label>
                                </div>
                                <div class="form-group col-lg-5">
                                    <asp:Label CssClass="form-control-label" Font-Bold="true" runat="server" Text="English"></asp:Label>
                                    <div class="input-group ">
                                        <asp:TextBox ID="tbEnglishSub" MaxLength="1000" TextMode="MultiLine" resize="none" autocomplete="off" placeholder="Max 1000 chars" data-toggle="tooltip" data-placement="bottom" title="Subject English" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ajaxfttbEnglishSub" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbEnglishSub" ValidChars=" " />
                                    </div>
                                </div>
                                <div class="form-group col-lg-5">
                                    <asp:Label CssClass="form-control-label" ID="lblRegEndDate" runat="server" Font-Bold="true" Text="Local Language"></asp:Label>
                                    <div class="input-group ">
                                        <asp:TextBox ID="tbLocalLangSub" MaxLength="1000" TextMode="MultiLine" resize="none" autocomplete="off" placeholder="Max 1000 chars" data-toggle="tooltip" data-placement="bottom" title="Subject Local Language" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row align-items-center mt-3" autocomplete="off">
                                <div class="form-group col-lg-2 mb-0 text-left">
                                    <asp:Label CssClass="form-control-label" Font-Bold="true" ID="lblDescription" runat="server" Text="">3. Description<span class="text-warning">*</span></asp:Label>
                                    <asp:Label CssClass="form-control-label" Font-Bold="true" ID="lblDescriptionNotMan" runat="server" Visible="false" Text="">3. Description</asp:Label>

                                </div>
                                <div class="form-group col-lg-5">
                                    <asp:Label CssClass="form-control-label" ID="lblDesEng" Font-Bold="true" runat="server" Text="English"></asp:Label>
                                    <div class="input-group ">
                                        <asp:TextBox ID="tbDescriEng" MaxLength="1000" TextMode="MultiLine" resize="none" autocomplete="off" placeholder="Max 1000 chars" data-toggle="tooltip" data-placement="bottom" title="Description English" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ajaxfttbDescriEng" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbDescriEng" ValidChars=" " />
                                    </div>
                                </div>
                                <div class="form-group col-lg-5">
                                    <asp:Label CssClass="form-control-label" ID="lblLocalLang" runat="server" Font-Bold="true" Text="Local Language"></asp:Label>
                                    <div class="input-group ">
                                        <asp:TextBox ID="tbDescrLocal" MaxLength="1000" TextMode="MultiLine" resize="none" autocomplete="off" placeholder="Max 1000 chars" data-toggle="tooltip" data-placement="bottom" title="Description Local Language" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row mt-3" id="divImage" runat="server">
                                <div class="col-md-6 col-lg-6">
                                    <asp:Label runat="server" ID="lblimage1" CssClass="form-control-label">4. Image 1(For Web Portal)<span class="text-warning">*</span>  </asp:Label><br />
                                    <asp:Label runat="server" ID="lblimage1NotMan" Visible="false" CssClass="form-control-label">4. Image 1 </asp:Label><br />

                                    <asp:Button ToolTip="Upload  Image 1" ID="btnUploadimage1" OnClick="btnUploadimage1_Click" runat="server" CausesValidation="False" CssClass="form-control form-control-sm"
                                        Style="display: none" TabIndex="18" Text="Upload Image" accept=".png" Width="80px" />
                                    <asp:FileUpload ID="fuimage1" onchange="UploadImage1(this);" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-sm btn-success" TabIndex="9" />
                                    <asp:Image ID="Img1" onchange="UploadImage1(this);" runat="server" Visible="false" Style="border-width: 0px; height: 50px; width: 50px; border: 2px solid #eaf4ff;" />
                                    <asp:LinkButton ID="lbtncloseImage1" runat="server" OnClick="lbtncloseImage1_Click" Style="font-size: 5pt; border-radius: 25px; margin-bottom: 22pt" Visible="false" CssClass="btn btn-sm btn-danger"><i class="fa fa-times"></i></asp:LinkButton><br />
                                    <label id="lblwrongimage1" runat="server" style="font-size: 7pt; color: Red; line-height: 15px;">
                                        Image size will be less then 1 MB is Allow
                                        (Only .PNG)</label>
                                    <br />
                                </div>
                                <div class="col-md-6 col-lg-6">
                                    <asp:Label runat="server" ID="lblimage2" CssClass="form-control-label">5. Image 2(For Mobile App)<span class="text-warning">*</span></asp:Label><br />
                                    <asp:Label runat="server" ID="lblImage2NotMan" Visible="false" CssClass="form-control-label">5. Image 2</asp:Label><br />

                                    <asp:Button ToolTip="Upload image 2" ID="btnUploadimage2" OnClick="btnUploadimage2_Click" runat="server"
                                        CausesValidation="False" CssClass="file-upload-inner" accept=".png"
                                        Style="display: none" Text="Upload Image" Width="80px" />
                                    <asp:FileUpload ID="fuimage2" onchange="UploadImage2(this);" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-sm btn-success" TabIndex="9" />
                                    <asp:Image ID="Img2" onchange="UploadImage2(this);" runat="server" Visible="false" Style="border-width: 0px; height: 50px; width: 50px; border: 2px solid #eaf4ff;" />
                                    <asp:LinkButton ID="lbtncloseImage2" OnClick="lbtncloseImage2_Click" runat="server" Style="font-size: 5pt; border-radius: 25px; margin-bottom: 22pt" Visible="false" CssClass="btn btn-sm btn-danger"><i class="fa fa-times"></i></asp:LinkButton><br />
                                    <label id="lblwrongimage2" runat="server" style="font-size: 7pt; color: Red; line-height: 15px;">
                                        Image size will be less then 1 MB is Allow
                                        (Only .PNG)</label><br />
                                </div>
                            </div>
                            <div class="form-row align-items-center mt-3" autocomplete="off">
                                <div class="form-group col-lg-2 mb-4 text-left">
                                    <asp:Label CssClass="form-control-label" ID="lbluploadDocument" Font-Bold="true" runat="server" Text="">6. Upload Document<span class="text-warning">*</span></asp:Label>
                                    <asp:Label CssClass="form-control-label" ID="lbluploadDocumentnotMan" Font-Bold="true" Visible="false" runat="server" Text="">6. Upload Document</asp:Label>

                                </div>
                                <div class="form-group col-lg-10">
                                    <asp:Button ID="btnUploadpdf" OnClick="btnUploadpdf_Click" runat="server" CausesValidation="False" CssClass="button1"
                                        Style="display: none" TabIndex="18" Text="Upload Image" Width="80px" />
                                    <asp:FileUpload ID="fudocfile" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-success btn-sm"
                                        onchange="UploadPDF(this);" TabIndex="9" />
                                    <asp:Label runat="server" ID="lblPDF" CssClass="col-form-label control-label" Style="font-size: 12px; font-weight: normal;"></asp:Label>
                                    <asp:LinkButton runat="server" ID="lbtnDocument" Font-Underline="true" title="View Uploaded Document" Style="font-size: 12px; color: red; font-weight: normal;"
                                        Visible="false"></asp:LinkButton><br />
                                    <span style="font-size: 7pt; color: Red; line-height: 12px;">Only PDF Allowed. (File size max 2MB)</span>
                                </div>
                            </div>
                            <div class="form-row align-items-center mt-3" autocomplete="off">
                                <div class="form-group col-lg-2  text-left">
                                    <asp:Label CssClass="form-control-label" ID="lblurl" Font-Bold="true" runat="server" Text="">7. URL<span class="text-warning">*</span></asp:Label>
                                    <asp:Label CssClass="form-control-label" ID="lblUrlnotMandatory" Font-Bold="true" runat="server" Visible="false" Text="">7. URL</asp:Label>

                                </div>
                                <div class="form-group col-lg-6">
                                    <asp:TextBox ID="tbUrl" Autocomplete="off" runat="server" CssClass="form-control form-control-sm" MaxLength="100" ToolTip="Enter Link" Placeholder="http://xyz.com"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row align-items-center mt-3" autocomplete="off">
                                <div class="form-group col-lg-2 text-left">
                                    <asp:Label CssClass="form-control-label" ID="lblvalidityfrom" Font-Bold="true" runat="server" Text="Registration">8. Validity From<span class="text-warning">*</span></asp:Label>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-5">
                                        <asp:TextBox ID="tbValidityFromDate" runat="server" autocomplete="off" MaxLength="10" CssClass="form-control form-control-sm" ToolTip="Select From Date" TabIndex="5"
                                            Placeholder="DD/MM/YYYY" Style="font-size: 10pt;"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ajaxftValidityFromDate" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="tbValidityFromDate" ValidChars="/" />
                                    </div>
                                    <div class="form-group col-lg-2 text-right">
                                        <asp:Label CssClass="form-control-label" Font-Bold="true" ID="lblvalidityto" runat="server" Text="Registration">To <span class="text-warning">*</span></asp:Label>

                                    </div>
                                    <div class="form-group col-lg-5">
                                        <asp:TextBox ID="tbValidityTo" runat="server" autocomplete="off" MaxLength="10" CssClass="form-control form-control-sm" ToolTip="Select From Date" TabIndex="5"
                                            Placeholder="DD/MM/YYYY" Style="font-size: 10pt;"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ajaxfttbValidityTo" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="tbValidityTo" ValidChars="/" />
                                    </div>
                                </div>
                            </div>

                            <div class="pl-lg-4 mt-4 mb-2">
                                <div class="row">
                                    <div class="col-lg-12 text-left">
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" OnClick="lbtnUpdate_Click" class="btn btn-primary" Visible="false" data-toggle="tooltip" data-placement="bottom" title="Update Alert Notice Details">
                                    <i class="fa fa-save"></i>&nbsp; Update</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnSaveDraft" Visible="true" OnClick="lbtnSaveDraft_Click" runat="server" class="btn btn-warning" data-toggle="tooltip" data-placement="bottom" title="Save Alert Notice Details">
                                    <i class="fa fa-save"></i>&nbsp; Save as Draft</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnSavePublish" Visible="true" runat="server" OnClick="lbtnSavePublish_Click" class="btn btn-success" data-toggle="tooltip" data-placement="bottom" title="Save Alert Notice Details">
                                    <i class="fa fa-save"></i>&nbsp; Save & Publish</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-danger" OnClick="lbtnReset_Click" data-toggle="tooltip" data-placement="bottom" title="Reset Alert Notice Details">
                                    <i class="fa fa-undo"></i>&nbsp; Reset</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" Visible="false" runat="server" OnClick="lbtnCancel_Click" CssClass="btn btn-danger" data-toggle="tooltip" data-placement="bottom" title="Cancel Alert Notice Details">
                                    <i class="fa fa-times"></i>&nbsp; Cancel</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>

        </div>
        <%--Confirmation box--%>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
                CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed; display: none;">
                <div class="card" style="min-width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title">Please Confirm
                        </h4>
                    </div>
                    <div class="card-body" style="min-height: 100px;">
                        <asp:Label ID="lblConfirmation" runat="server"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnYesConfirmation" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbtnYesConfirmation_Click"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                            <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-3"> <i class="fa fa-times"></i> No </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button4" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpViewHistory" runat="server" PopupControlID="pnlNoticenewshistory"
                TargetControlID="Button8" CancelControlID="LinkButton4" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlNoticenewshistory" runat="server" Style="position: fixed; margin-top: 42px; min-width: 400px; max-width: 800px;">
                <div class="card">
                    <div class="card-header">
                        <strong class="card-title">Notice/News History</strong>
                    </div>
                    <div class="card-body" style="padding: 15px !important;">

                        <asp:GridView ID="gvHistory" OnPageIndexChanging="gvHistory_PageIndexChanging" runat="server" AutoGenerateColumns="False"
                            GridLines="None" AllowSorting="true" AllowPaging="true" PageSize="5" CssClass="table table-striped" DataKeyNames="noticeid, 
                        categorycode, noticecategory_name, sub_ject, subjectlocal, description, description_local, image1, image2, documt, urllink, valid_fromdt, valid_todt">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="100">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category Name">
                                    <ItemTemplate>
                                        <%# Eval("noticecategory_name") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valid From Date">
                                    <ItemTemplate>
                                        <%# Eval("valid_fromdt") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valid To Date">
                                    <ItemTemplate>
                                        <%# Eval("valid_todt") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <PagerStyle HorizontalAlign="Left" CssClass="gridview" />
                        </asp:GridView>
                        <asp:Panel ID="pnlNoRecord" runat="server" Width="100%" Visible="true">
                            <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;">
                                    Sorry No Record Found
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="card-footer">
                        <asp:LinkButton ID="LinkButton4" runat="server" CssClass="btn btn-success" Style="font-size: 11pt; float: right; border-radius: 4px;">OK</asp:LinkButton>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button8" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
    </div>

</asp:Content>

