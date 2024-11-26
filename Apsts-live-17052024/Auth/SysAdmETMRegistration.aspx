<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/StoreMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="SysAdmETMRegistration.aspx.cs" Inherits="Auth_SysAdmETMRegistration" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <style>
        .form-control {
            font-size: 10pt;
        }

        .table td, .table th {
            padding: 5px 3px;
            vertical-align: middle;
            border-top: none;
        }

        .shadow {
            box-shadow: rgb(0 0 0 / 20%) 5px 5px 15px 3px;
        }

        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .tblCenter {
            width: 70%;
            margin-left: auto;
            margin-right: auto;
            text-align: left;
        }

        .custom-tab .nav-tabs > .active > a:focus, .custom-tab .nav-tabs > a.active, .custom-tab .nav-tabs > li.active > a:hover {
            border-color: transparent transparent;
            color: white;
            background: #4884d8;
            position: relative;
            border-top-right-radius: 20px;
        }

        .headerCss {
            font-weight: bold;
        }

        .form-control {
            display: block;
            width: 100%;
            height: 30px;
            padding: 5px 8px;
        }

        .GridPager a, .GridPager span {
            display: inline-block;
            padding: 0px 9px;
            border-radius: 2px;
            border: solid 1px #f3eded;
            background: #f3eded;
            box-shadow: inset 0px 1px 0px rgba(255,255,255, .8), 0px 1px 3px rgba(0,0,0, .1);
            font-size: .875em;
            font-weight: bold;
            text-decoration: none;
            color: #717171;
            text-shadow: 0px 1px 0px rgba(255,255,255, 1);
        }

        .GridPager a {
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #b0aeae;
        }

        .GridPager span {
            background: #f3eded;
            box-shadow: inset 0px 0px 8px rgba(0,0,0, .5), 0px 1px 0px rgba(255,255,255, .8);
            color: #000;
            text-shadow: 0px 0px 3px rgba(0,0,0, .5);
            border: 1px solid #f3eded;
        }

        .content {
            padding: 0 30px;
        }

        .input-group-addon, .input-group-btn {
            white-space: nowrap;
            vertical-align: inherit;
        }

        .textbox {
            height: 30px;
            padding: .375rem .75rem;
            font-size: 10pt;
            line-height: 1.5;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            border-radius: .25rem;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }

        .spanMendatory {
            font-size: 10pt;
            color: #dc3545 !important;
            font-weight: bold;
        }



        .table {
            width: 100%;
        }

            .table th, .table td {
                padding: .5rem 0.75rem;
                vertical-align: top;
                border-top: 1px solid #dce1e3;
                font-size: 13px;
            }

        table.dataTable {
            text-transform: uppercase;
        }

        .horizontal-radio-list {
            display: flex;
            align-items: center;
        }

            .horizontal-radio-list label {
                margin-right: 20px; /* Adjust as needed */
            }

        /*.card {
            box-shadow: rgb(0 0 0 / 20%) 5px 5px 15px 3px;
        }*/
    </style>

        <script>
            $(document).ready(function () {

                var currDate = new Date().getDate();

                var todayDate = new Date(new Date().setDate(currDate));


                $('[id*=tbInvoiceDate]').datepicker({
                    endDate: todayDate,
                    changeMonth: true,
                    changeYear: false,
                    format: "dd/mm/yyyy",
                    autoclose: true
                });
                $('[id*=tbReceivedOnDate]').datepicker({
                    endDate: todayDate,
                    changeMonth: true,
                    changeYear: false,
                    format: "dd/mm/yyyy",
                    autoclose: true
                });






            });



        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid" style="padding-top: 20px;">
        <asp:HiddenField ID="hidtoken" runat="server" />

        <div class="row">
           
                  <div class="col-lg-3">
                       <asp:Panel ID="pnletmcount" runat="server">
                <div class="row ml-0">
                    <div class="card shadow mb-2" style="font-size: 11pt; width: 100%; min-height: 600px">
                        <div class="card-body px-3 py-0">
                            
                            <div class="mb-2" style="font-size: 11pt; width: 100%">
                                <div class="card-body p-0">
                                    <h4 class="text-left pt-0 pb-0 pl-3" font-size: 13pt; color: #7b7474;"><asp:Label ID="lblSummary" runat="server" Text=""></asp:Label></h4>
                                    
                                    <div class="row">

                                                            <div class="col-md-12 pl-1">
                                            <div class="card" style="min-height: 10vh; margin-bottom: 10px">
                                                <div class="card-body" style="padding: 10px; text-align: center; height: 80px">

                                                    <div class="col-lg-12 p-0">
                                                        <asp:linkbutton ID="lbtnPendingforlock" runat="server" OnClick="lbtnPendingforlock_Click"  Text="0" CssClass="text-success" Font-Size="16pt" Font-Bold="true" ></asp:linkbutton>
                                                        <br />
                                                        <label for="ddhead" style="line-height: 18px;">
                                                           Pending For Lock
                                <br />
                                                            <br />
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 pr-1">
                                            <div class="card" style="min-height: 10vh; margin-bottom: 10px">
                                                <div class="card-body" style="padding: 10px; text-align: center; height: 80px">
                                                    <div class="col-lg-12 p-0">
                                                        <asp:linkbutton ID="lbtnAvailableforissuance" OnClick="lbtnAvailableforissuance_Click" CssClass="text-success" runat="server"  Text="0" Font-Size="16pt" Font-Bold="true" ></asp:linkbutton>
                                                        <br />
                                                        <label for="ddhead" style="line-height: 18px;">
                                                           Available For Issuance
                                <br />
                                                            <br />
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                    

                                                 <div class="col-md-12 pl-1">
                                            <div class="card" style="min-height: 10vh; margin-bottom: 10px">
                                                <div class="card-body" style="padding: 10px; text-align: center; height: 80px">

                                                    <div class="col-lg-12 p-0">
                                                        <asp:linkbutton ID="lbtnissuedetm" runat="server" OnClick="lbtnissuedetm_Click"  Text="0" CssClass="text-success" Font-Size="16pt" Font-Bold="true" ></asp:linkbutton>
                                                        <br />
                                                        <label for="ddhead" style="line-height: 18px;">
                                                            Issued to ETM Branch/Store
                                <br />
                                                            <br />
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 pl-1">
                                            <div class="card" style="min-height: 10vh; margin-bottom: 10px">
                                                <div class="card-body" style="padding: 10px; text-align: center; height: 80px">

                                                    <div class="col-lg-12 p-0">
                                                        <asp:linkbutton ID="lbtncounter" runat="server" OnClick="lbtncounter_Click"  Text="0" CssClass="text-success" Font-Size="16pt" Font-Bold="true" ></asp:linkbutton>
                                                        <br />
                                                        <label for="ddhead" style="line-height: 18px;">
                                                            Issued to Counter
                                <br />
                                                            <br />
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 pl-1">
                                            <div class="card" style="min-height: 10vh; margin-bottom: 10px">
                                                <div class="card-body" style="padding: 10px; text-align: center; height: 80px">

                                                    <div class="col-lg-12 p-0">
                                                        <asp:linkbutton ID="lbtnagent" runat="server" OnClick="lbtnagent_Click"  Text="0" CssClass="text-success" Font-Size="16pt" Font-Bold="true" ></asp:linkbutton>
                                                        <br />
                                                        <label for="ddhead" style="line-height: 18px;">
                                                            Issued to Agent
                                <br />
                                                            <br />
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                                 <div class="col-md-12 pl-1">
                                            <div class="card" style="min-height: 10vh; margin-bottom: 10px">
                                                <div class="card-body" style="padding: 10px; text-align: center; height: 80px">

                                                    <div class="col-lg-12 p-0">
                                                        <asp:linkbutton ID="lbtnfaulty" runat="server" CommandArgument="3" Text="0" CssClass="text-success" Font-Size="16pt" Font-Bold="true" CommandName="N"></asp:linkbutton>
                                                        <br />
                                                        <label for="ddhead" style="line-height: 18px;">
                                                          Faulty & Sent for Repair
                                <br />
                                                            <br />
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 pl-1">
                                            <div class="card" style="min-height: 10vh; margin-bottom: 10px">
                                                <div class="card-body" style="padding: 10px; text-align: center; height: 80px">

                                                    <div class="col-lg-12 p-0">
                                                        <asp:linkbutton ID="lbtnobsolate" runat="server" CommandArgument="3" Text="0" CssClass="text-success" Font-Size="16pt" Font-Bold="true" CommandName="N"></asp:linkbutton>
                                                        <br />
                                                        <label for="ddhead" style="line-height: 18px;">
                                                          Obsolate ETM
                                <br />
                                                            <br />
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                                 
                                                 <div class="col-md-12 pl-1">
                                            <div class="card" style="min-height: 10vh; margin-bottom: 10px">
                                                <div class="card-body" style="padding: 10px; text-align: center; height: 80px">

                                                    <div class="col-lg-12 p-0">
                                                        <asp:linkbutton ID="lblreturnbacktostore" OnClick="lblreturnbacktostore_Click" runat="server" CommandArgument="3" Text="0" CssClass="text-success" Font-Size="16pt" Font-Bold="true" CommandName="N"></asp:linkbutton>
                                                        <br />
                                                        <label for="ddhead" style="line-height: 18px;">
                                                            Return Back to Store
                                <br />
                                                            <br />
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                           
                            <div class="col-lg-12 mb-3">
                                <asp:LinkButton runat="server" ID="lbtnAddETM" OnClick="lbtnAddETM_Click"  CssClass="btn btn-primary btn-block text-center p-2" ToolTip="Add ETM"><i class="fa fa-plus"></i> ADD ETM</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>

           
            </asp:Panel>
          


            <asp:Panel runat="server" ID="instr" Visible="false">
               
                <div class="row ml-0">
                    <div class="card shadow mb-2" style="font-size: 11pt; width: 100%; min-height: 600px">
                        <div class="card-body px-3 py-0">
                            
                      <h4 class="text-left pt-0 pb-0 pl-3 pt-2" font-size: 18pt; color: #7b7474;">Instructions</h4>
                    <div class="modal-body">
                        <ol>
                            <li> 1. Add ETM details along with its receiving office.</li>
                            <li> 2. If ETM is issued to ETM Branch then it will be available for waybill issuance but if ETM is issued to Store then Store will first receive it then issue it to another store or ETM Branch.</li>
                            <li> 3. View/Update/Delete ETM Details </li>
                            <li> 4. Store can only be deleted if it is not assigned to any employee.</li>
                            <li> 5. Only Drafts ETM can be updated. Once Locked you cannot update or delete the ETM</li>
                            <li> 6. Lock drafts ETM</li>
                        </ol>
                    </div>
                    
                </div>
           
                    </div>
                </div>

          
            </asp:Panel>
                      </div>
            <div class="col-lg-9" style="min-height: 700px">
               

                                <asp:Panel runat="server" ID="pnlAddETM" Visible="false">
                          
                      <div class="card" style="min-height: 490px">
                        <div class="card-header">
                            <div class="row m-0 align-items-center">
                                <div class="col-lg-12">
                                    <h3 class="mb-0 float-left">
                                        <asp:Label ID="lblETMHead" Font-Bold="true" runat="server" Text="Add ETM Details"></asp:Label></h3>
                                    <asp:LinkButton ID="lbtncanceladdetm" runat="server" OnClick="lbtncanceladdetm_Click" class="btn btn-sm btn-danger float-right" Style="border-radius: 25px; left: 31px; margin-top: -22px;" > <i class="fa fa-times" ></i> </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                          
                    

                        <div class="card-body">
                            <div class="card-body">
                                <h6 class="heading-small my-0">1. ETM Details</h6>
                                <div class="pl-lg-4">
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass=" text-muted form-control-label"> ETM Type<span style="color: red">*</span></asp:Label>
                                            <asp:DropDownList ID="ddlETMType" CssClass="form-control form-control-sm" ToolTip="Select ETM type" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlETMType_SelectedIndexChanged" >
                                             <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label">Serial No.<span style="color: red">*</span></asp:Label><br />
                                            <asp:TextBox ID="tbSerial" MaxLength="20" autocomplete="off" placeholder="Max 20 Characters" CssClass="form-control form-control-sm text-uppercase" runat="server" ToolTip="Serial No."></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers, UppercaseLetters,LowercaseLetters" TargetControlID="tbSerial" />
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label"> Make Model<span style="color: red">*</span></asp:Label><br />
                                            <asp:DropDownList ID="ddlMakeModel" CssClass="form-control form-control-sm" ToolTip="Select Make Model" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMakeModel_SelectedIndexChanged" >
                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-3" runat="server" id="divMakeModelName" visible="false">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label">Enter Make Model<span style="color: red">*</span></asp:Label><br />
                                            <asp:TextBox ID="tbMakeModel" MaxLength="50" autocomplete="off" placeholder="Max 50 Characters" CssClass="form-control form-control-sm text-uppercase" runat="server" ToolTip="Serial No."></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers, UppercaseLetters,LowercaseLetters,Custom" TargetControlID="tbMakeModel" ValidChars=" .-_/" />
                                        </div>
                                    </div>
                                    <div class="row mt-1">
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label"> Purchase Mode<span style="color: red">*</span></asp:Label><br />
                                            <asp:DropDownList ID="ddlPurchaseMode" CssClass="form-control form-control-sm" ToolTip="Select Purchase Mode" runat="server">
                                                <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Rent" Value="R"></asp:ListItem>
                                                <asp:ListItem Text="Purchase" Value="P"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-3" runat="server" id="divIMEI1" visible="false">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label"> IMEI No.1<span style="color: red">*</span></asp:Label>
                                            <asp:TextBox ID="tbIMEI1" MaxLength="30" autocomplete="off" placeholder="Max 15 Characters" CssClass="form-control form-control-sm text-uppercase" runat="server" ToolTip="IMEI No.1"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers, UppercaseLetters,LowercaseLetters" TargetControlID="tbIMEI1" />
                                        </div>
                                        <div class="col-lg-3" runat="server" id="divIMEI2" visible="false">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label">IMEI No.2</asp:Label><br />
                                            <asp:TextBox ID="tbIMEI2" MaxLength="30" autocomplete="off" placeholder="Max 15 Characters" CssClass="form-control form-control-sm text-uppercase" runat="server" ToolTip="IMEI No.2"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers, UppercaseLetters,LowercaseLetters" TargetControlID="tbIMEI2" />
                                        </div>
                                    </div>
                                </div>

                                <h6 class="heading-small my-0 mt-2">2. Invoice Details</h6>
                                <div class="pl-lg-4">
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label">Agency<span style="color: red">*</span></asp:Label>
                                            <asp:DropDownList ID="ddlAgency" CssClass="form-control form-control-sm" ToolTip="Select Agency" runat="server">
                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label">Invoice No.</asp:Label><br />
                                            <asp:TextBox ID="tbInvoiceNo" MaxLength="50" autocomplete="off" placeholder="Max 50 Characters" CssClass="form-control form-control-sm text-uppercase" runat="server" ToolTip="Invoice No."></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers, UppercaseLetters,LowercaseLetters" TargetControlID="tbInvoiceNo" />
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label"> Invoice Date</asp:Label><br />
                                            <div class="input-group date">
                                                <asp:TextBox ID="tbInvoiceDate" ToolTip="Enter Invoice Date" runat="server" autocomplete="off" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom"
                                                ValidChars="/" TargetControlID="tbInvoiceDate" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label"> Amount (<i class="fa fa-rupee-sign"></i>)</asp:Label><br />
                                            <asp:TextBox ID="tbAmount" MaxLength="6" autocomplete="off" placeholder="Max 6 Digits" CssClass="form-control form-control-sm" runat="server" ToolTip="Enter Amount"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="tbAmount" ValidChars="." />
                                        </div>
                                    </div>
                                    <div class="row mt-1">
                                      <div class="col-lg-12">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label"> Upload Invoice</asp:Label><br />
                                            <asp:Button ToolTip="Upload Invoice" ID="btnUploadInvoice" OnClick="btnUploadInvoice_Click" runat="server" CausesValidation="False" CssClass="form-control form-control-sm"
                                                Style="display: none" TabIndex="18" Text="Upload Image" accept=".png,.jpg,.jpeg,.gif" Width="80px" />
                                            <asp:FileUpload ID="FileInvoice" onchange="UploadInvoice(this);" runat="server" Style="color: black; background-color: #eaf4ff; border: none;" CssClass="btn btn-sm btn-success" TabIndex="9" />
                                            <asp:Image ID="ImgInvoice" onchange="UploadInvoice(this);" runat="server" Visible="false" Style="border-width: 0px; height: 50px; width: 50px; border: 2px solid #eaf4ff;" />
                                            <br />
                                            <label id="lblwrongimage" runat="server" style="font-size: 7pt; color: Red; line-height: 12px;">
                                                <br />
                                                Image size will be less then 1 MB<br />
                                                (Only .JPG, .PNG, .JPEG)</label>

                                        </div>


                                    </div>

                                </div>

                                <h6 class="heading-small  my-0 mt-2">3. Receiving Details</h6>
                                <div class="pl-lg-4">
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label">Received At Store<span style="color: red">*</span></asp:Label>
                                            <asp:DropDownList ID="ddlRecStore" CssClass="form-control form-control-sm" ToolTip="Select Receive At from list" runat="server">
                                                
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label">Receive By<span style="color: red">*</span></asp:Label><br />
                                            <asp:TextBox ID="tbReceiveBy" MaxLength="50" autocomplete="off" placeholder="Max 50 Characters" CssClass="form-control form-control-sm" runat="server" ToolTip="Receive By"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="UppercaseLetters, Lowercaseletters,Custom" TargetControlID="tbReceiveBy" ValidChars=" " />
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label"> Receive On<span style="color: red">*</span></asp:Label><br />
                                            <div class="input-group date">
                                                <asp:TextBox ID="tbReceivedOnDate" ToolTip="Enter Receive on" runat="server" autocomplete="off" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY"></asp:TextBox>
                                           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, Custom"
                                                ValidChars="/" TargetControlID="tbReceivedOnDate" />
                                                </div>
                                        </div>

                                    </div>

                                </div>



                              <%--  <h6 class="heading-small my-0 mt-2">4. Issuance Details</h6>
                                <div class="pl-lg-4">
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label">Issue To<span style="color: red">*</span></asp:Label>
                                            <asp:DropDownList ID="ddlIssueToOfc" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlIssueTo_SelectedIndexChanged" ToolTip="Select Issue to from list" runat="server">
                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                <asp:ListItem Value="S" Text="Store"></asp:ListItem>
                                                <asp:ListItem Value="E" Text="ETM Branch"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-lg-3" runat="server" id="divIssueOfc" visible="false">
                                            <asp:Label runat="server" ID="lblIssueOffice" CssClass="text-muted form-control-label" Text="Store"><span style="color: red">*</span></asp:Label><br />
                                            <asp:DropDownList ID="ddlStore" runat="server" Visible="false" CssClass="form-control form-control-sm" ToolTip="Store">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlETMBranch" runat="server" Visible="false" CssClass="form-control form-control-sm" ToolTip="ETM Branch">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>--%>
                                <div class="pl-lg-4 mt-3 mb-2">
                                    <div class="row">
                                        <div class="col-lg-12 text-left">
                                            <asp:LinkButton ID="lbtnSave" runat="server"  OnClick="lbtnSave_Click" CssClass="btn btn-success" Visible="true" ToolTip="Save As Draft">
                                    <i class="fa fa-save"></i>&nbsp; Save As Draft</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnSaveLock" OnClick="lbtnSaveLock_Click"  Visible="true" runat="server" class="btn btn-primary" ToolTip="Save And Lock">
                                    <i class="fa fa-lock"></i>&nbsp; Save and Lock</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnUpdate" OnClick="lbtnUpdate_Click" Visible="false" runat="server" class="btn btn-success" ToolTip="Save And Lock">
                                    <i class="fa fa-edit"></i>&nbsp; Update</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnCancel"  Visible="false" runat="server" OnClick="lbtnCancel_Click" class="btn btn-danger" ToolTip="Save And Lock">
                                    <i class="fa fa-times"></i>&nbsp; Cancel</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnReset"  OnClick="lbtnReset_Click" runat="server" CssClass="btn btn-danger" ToolTip="Reset" >
                                    <i class="fa fa-undo"></i>&nbsp; Reset</asp:LinkButton>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                                 
                                </asp:Panel>
                                    
                       
                                    <div class="row text-left" id="grdpara" runat="server">
                                        <div class="col-md-12 mt-3">
                                            <div class="card-header p-2">
                                                <div class="row">
                                                    
                                                          <div class="col-lg-9"></div>
                                                            <div class="col-lg-2">
                                                                <h6 class="form-control-label text-muted my-0">ETM Status</h6>
                                                                <asp:DropDownList ID="ddlStatus" runat="server" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control form-control-sm" ToolTip="Select Status">
                                                                  
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-lg-1 mt-3 float-left!important">
                                <asp:LinkButton ID="lbtnSearch"  OnClick="lbtnSearch_Click" data-toggle="tooltip" data-placement="bottom" title="Search ETM" runat="server" CssClass="btn bg-success btn-sm text-white">
                                            <i class="fa fa-search">Search</i>
                                </asp:LinkButton>
                              
                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                          
                 <asp:Panel runat="server" ID="pnlMsg" Visible="True">
            <div class="row">
                <div class="col-12 mt-5">
                    <center>
                        <asp:Label runat="server" Text="No ETM Available " Font-Bold="true" Font-Size="40px" ForeColor="LightGray"></asp:Label>

                    </center>
                </div>
            </div>
        </asp:Panel>
                                            <asp:Panel ID="GetEtmDtails" runat="server">
                                             
                                                <asp:GridView ID="gvETMDetails" runat="server" CssClass="table" ClientIDMode="Static" GridLines="None" AutoGenerateColumns="false" 
                        OnRowCommand="gvETMDetails_RowCommand" OnRowDataBound="gvETMDetails_RowDataBound" OnPageIndexChanging="gvETMDetails_PageIndexChanging"
                        DataKeyNames="etmid , etmrefno , etmtype , etmmake , etmserialno, purchasemode , agencyid , imeino1 , imeino2 , receivingdate , receivedby , statuslogid , statustypeid ,rec_officename ,rec_office,issued_office, invoice_no , invoice_date , invoice_amount , invoice_doc , actionlogid , etmtypename , makemodel , agency , etmstatus,receiving_store,current_store ">
                        <Columns>

                                        <asp:TemplateField HeaderText="IMEI/Serial No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbletmserialno" runat="server" Text='<%# Eval("etmserialno") %>'></asp:Label><br />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ETM Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbletmtypename" runat="server" Text='<%# Eval("etmtypename") %>'></asp:Label><br />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MakeModel">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmakemodel" runat="server" Text='<%# Eval("makemodel") %>'></asp:Label><br />

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Received By">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrecofficename" runat="server" Text='<%# Eval("rec_officename") %>'></asp:Label><br />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Issued To">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl" runat="server" Text='<%# Eval("issueto") %>'></asp:Label><br />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                         <asp:TemplateField>
                                <ItemTemplate>
                                    
                                                <div class="col-auto text-right">
                                                    <asp:LinkButton ID="lbtnviewETMs" Visible="true" runat="server" CommandName="viewETM" CssClass="btn btn-sm btn-default" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="View ETM Details" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-eye"></i></asp:LinkButton>
                                                  
                                                    <asp:LinkButton ID="lbtnissueETM" Visible="true" runat="server" CommandName="issueETM" CssClass="btn btn-sm btn-success" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="Issue ETM" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-external-link-alt"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnUpdateETM" Visible="true" runat="server" CommandName="updateETM" CssClass="btn btn-sm btn-info" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="Update ETM Details" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnLockETM" Visible="true" runat="server" CommandName="LockETM" CssClass="btn btn-sm btn-primary" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="Lock ETM Details" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-lock"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDeleteETM" Visible="true" runat="server" CommandName="deleteETM" CssClass="btn btn-sm btn-danger" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Style="border-radius: 4px;" title="Delete ETM" data-toggle="tooltip" data-placement="bottom"> <i class="fa fa-trash"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
						<PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                    </asp:GridView>
                     </asp:Panel>
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
                        <asp:LinkButton ID="lbtnNoConfirmation" runat="server"  CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button4" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
    <div class="row">
        <cc1:ModalPopupExtender ID="mperror" runat="server" PopupControlID="pnlerror" CancelControlID="Button2"
            TargetControlID="Button1" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlerror" runat="server" Style="position: fixed;">
            <div class="card" style="width: 350px;">
                <div class="card-header">
                    <h4 class="card-title text-left mb-0">Please Check & Correct
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
                <asp:Button ID="Button2" runat="server" Text="" />
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
    <div class="row">
        <cc1:ModalPopupExtender ID="mpETMAllot" runat="server" PopupControlID="pnlETMAllot"
            CancelControlID="Button5" TargetControlID="Button3" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlETMAllot" runat="server" Style="position: fixed; display: none">
            <center>

                <div class="card" style="width: 800px; height: 350px">
                    <div class="card-body text-left pt-2 row" style="min-height: 100px;">
                        <div class="col-lg-12" style="font-size: 11pt;">
                            <div class="row">
                                <div class="col-lg-6 mb-3" style="border-right: 2px solid #eee">
                                    <table class="table mb-2">
                                        <tr>
                                            <td colspan="2">
                                                <h4 class="card-title text-center mb-0" style="font-weight: bold; font-size: 15pt; color: #8e8b8b;">ETM Details
                                                </h4>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><b>Refernce No</b></td>
                                            <td>
                                                <asp:Label runat="server" ID="lblRefNo" Text="N/A"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>ETM Type</b></td>
                                            <td>
                                                <asp:Label runat="server" ID="lblType" Text="N/A"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>Make Model</b></td>
                                            <td>
                                                <asp:Label runat="server" ID="lblMakeModel" Text="N/A"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>Serial No</b></td>
                                            <td>
                                                <asp:Label runat="server" ID="lblSerialNo" Text="N/A"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>Status</b></td>
                                            <td>
                                                <asp:Label runat="server" ID="lblStatus" Text="N/A"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>Status Date</b></td>
                                            <td>
                                                <asp:Label runat="server" ID="lblStatusDate" Text="N/A"></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-lg-6 mb-3">
                                    <div runat="server" id="divAllotment" visible="true">
                                        <center>
                                            <h4 class="card-title mb-2" style="font-weight: bold; font-size: 15pt; color: #8e8b8b;">Update ETM Status</h4>
                                        </center>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <table class="table">
                                                    <tr>
                                                        <td style="width: 25%;">Status</td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlETMStatus" runat="server" Style="height: 32px; font-size: 10pt;" ToolTip="Office Level" AutoPostBack="true"
                                                                class="form-control">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr runat="server" id="trOfcDetails" visible="false">
                                                        <td colspan="2">
                                                            <table class="table">
                                                                <tr>
                                                                    <td style="width: 25%;">Office</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlIssueOffice2" runat="server" Style="font-size: 10pt;" ToolTip="Office (Store/ETM Branch)"
                                                                            class="form-control">
                                                                        </asp:DropDownList></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Status Date<span class="spanMendatory">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox class="textbox" runat="server" ID="txtStatusDate" MaxLength="10" placeholder="DD/MM/YYYY"
                                                                Text="" Style="font-size: 10pt; height: 30px; width: 120px; margin-right: -10px;" autocomplete="off"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,Custom"
                                                                TargetControlID="txtStatusDate" ValidChars="/" />
                                                            <span class="input-group-addon" style="padding: 4px 7px;">
                                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" ImageUrl="~/images/title_marker.gif"
                                                                    TabIndex="6" />
                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" CssClass="black"
                                                                    Format="dd/MM/yyyy" PopupButtonID="ImageButton1" PopupPosition="Right" TargetControlID="txtStatusDate"></cc1:CalendarExtender>
                                                            </span></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Remark</td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtRemark" CssClass="form-control" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <center>
                                            <div style="width: 100%; margin-top: 20px;">
                                                <asp:LinkButton ID="lbtnAllotETM" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> Save </asp:LinkButton>
                                                <asp:LinkButton ID="lbtnAllotCancel" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> Cancel </asp:LinkButton>
                                            </div>
                                        </center>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

                <div style="visibility: hidden;">
                    <asp:Button ID="Button3" runat="server" Text="" />
                    <asp:Button ID="Button5" runat="server" Text="" />
                </div>
            </center>
        </asp:Panel>
    </div>

      <div class="row">
        <cc1:ModalPopupExtender ID="mptransfetm" runat="server" PopupControlID="pnltransferetm"
            CancelControlID="Button13" TargetControlID="Button12" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnltransferetm" runat="server" Style="position: fixed; display: none">
            <center>

                <div class="card" style="width: 400px; height: 200px">
                  
                        <div class="col-lg-12" style="font-size: 11pt;">
                            <div class="row">
                      
                                <div class="col-lg-12 mb-3" style="border-right: 2px solid #eee">
                                  
                                                <h4 class="card-title text-center mb-0" style="font-weight: bold; font-size: 15pt; color: #8e8b8b;">Transfer Details
                                                </h4>
                                
                         <br />
                   

                               <div class="row" runat="server" >
                                  
                                   <div class="col-lg-12">
                                      <label>Other Store</label>
                                       <br />
                                            <asp:DropDownList ID="ddlotherStore" runat="server" CssClass="form-control form-control-sm" ToolTip="Store">
                                            </asp:DropDownList>
                                   </div>
                                            
                                           
                                        </div>

                    
                                            <div  style="width: 100%; margin-top: 20px;">
                                                <asp:LinkButton ID="lbtntransferetm" runat="server" OnClick="lbtntransferetm_Click" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> Save </asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> Cancel </asp:LinkButton>
                                            </div>
                                  
                        </div>  </div>

                    </div>
                </div>
          
                <div style="visibility: hidden;">
                    <asp:Button ID="Button12" runat="server" Text="" />
                    <asp:Button ID="Button13" runat="server" Text="" />
                </div>
            </center>
        </asp:Panel>
    </div>

     <div class="row">
        <cc1:ModalPopupExtender ID="mpissuetm" runat="server" PopupControlID="pnlissueetm"
            CancelControlID="Button10" TargetControlID="Button15" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlissueetm" runat="server" Style="position: fixed; display: none">
                <div class="card" style="width: 400px;min-height: 200px">
                    
                        <div class="col-lg-12" style="font-size: 11pt;">
                            <div class="row">
                                
                                <div class="col-lg-12 mb-3" style="border-right: 2px solid #eee">
                                  
                                                <h4 class="card-title text-center mb-0" style="font-weight: bold; font-size: 15pt; color: #8e8b8b;">Issuance Details
                                                </h4>
                         <br />

                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                    <div class="row" runat="server" >
                                   <div class="col-lg-12 mb-2">
                                       <label>Action</label>
                                       <br />
                                        <asp:DropDownList ID="ddlaction" AutoPostBack="true" OnSelectedIndexChanged="ddlaction_SelectedIndexChanged" runat="server"  CssClass="form-control form-control-sm" ToolTip="Select Action">
                                            </asp:DropDownList>
                                   </div>  
                                        </div>
                               <div class="row" runat="server" id="divoffice" visible="false" >
                                   <div class="col-lg-12">
                                      <asp:Label runat="server" ID="lblissueofficename"></asp:Label>
                                       <br />
                                        <asp:DropDownList ID="ddlETMBranch"  runat="server"  CssClass="form-control form-control-sm" >
                                            </asp:DropDownList>
                                   </div>  
                                        </div>
                                              </ContentTemplate>
                                    </asp:UpdatePanel>
                                            <div  style="width: 100%; margin-top: 20px;">
                                                <asp:LinkButton ID="lbtnsaveissueetm" runat="server" OnClick="lbtnsaveissueetm_Click" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> Save </asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> Cancel </asp:LinkButton>
                                            </div>
                                          

                        </div> 
                                             </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button15" runat="server" Text="" />
                    <asp:Button ID="Button10" runat="server" Text="" />
                    
                </div>
        </asp:Panel>
    </div>
    <div class="row">
        <cc1:ModalPopupExtender ID="mpInfo" runat="server" PopupControlID="PanelInfo" CancelControlID="Button8"
            TargetControlID="Button7" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="PanelInfo" runat="server" Style="position: fixed; display: none">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">About Module
                        </h4>
                    </div>
                    <div class="modal-body">
                        <ol>
                            <li>Add ETM details along with its receiving office.</li>
                            <li>If ETM is issued to ETM Branch then it will be available for waybill issuance but if ETM is issued to Store then Store will first receive it then issue it to another store or ETM Branch.</li>
                            <li>View/Update/Delete ETM Details </li>
                            <li>Store can only be deleted if it is not assigned to any employee.</li>
                            <li>Only Drafts ETM can be updated. Once Locked you cannot update or delete the ETM</li>
                            <li>Lock drafts ETM</li>
                        </ol>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="LinkButtonMpInfoClose" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                    </div>
                </div>
            </div>

            <div style="visibility: hidden;">
                <asp:Button ID="Button7" runat="server" Text="" />
                <asp:Button ID="Button8" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>

      <div class="row">
        <cc1:ModalPopupExtender ID="mpview" runat="server" PopupControlID="pnlMPview" TargetControlID="Button21"
            CancelControlID="LinkButton71" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlMPview" runat="server" Style="position: fixed;">
            <div class="modal-content mt-5">
                <div class="modal-header">
                    <div class="col-md-10">
                        <h3 class="m-0">ETM Details</h3>
                    </div>
                    <div class="col-md-2 text-right">
                        <asp:LinkButton ID="LinkButton71" runat="server" UseSubmitBehavior="false" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                    </div>
                </div>
                <div class="modal-body p-0">
                    <embed src="GetEtmDetails.aspx" style="height: 70vh; width: 70vw" />
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="Button21" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>


     <div class="row">
        <cc1:ModalPopupExtender ID="mpviewetm" runat="server" PopupControlID="pnlviewetm" CancelControlID="Button9"
            TargetControlID="Button11" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlviewetm" runat="server" Style="position: fixed; display: none">
           
                    <div class="card" style="min-height:600px;width:1200px;">
                        <div class="card-header">
                            
                            <div class="row m-0 align-items-center">
                                <div class="col-lg-12">
                                    <h3 class="mb-0 float-left">
                                        <asp:Label ID="lblETMName" Font-Bold="true" runat="server" Text=""></asp:Label></h3>
                                    <asp:LinkButton ID="lbtnViewETMCancel" runat="server" class="btn btn-sm btn-danger float-right" Style="border-radius: 25px; left: 31px; margin-top: -22px;" > <i class="fa fa-times" ></i> </asp:LinkButton>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                                <div class="col-lg-6" style="border-right:1px solid black;height:600px;">
                                   
                                     <div class="card-body">
                            <div class="card-body">
                                <h6 class="heading-small  my-0">1. ETM Details</h6>
                                <div class="pl-lg-4">
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label"> Make Model</asp:Label><br />
                                            <asp:Label runat="server" ID="lblmakemodelname" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label"> Purchase Mode</asp:Label><br />
                                            <asp:Label runat="server" ID="lblPurchaseMode" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label"> IMEI No.1</asp:Label><br />
                                            <asp:Label runat="server" ID="lblIMEI1" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label">IMEI No.2</asp:Label><br />
                                            <asp:Label runat="server" ID="lblIMEI2" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <h6 class="heading-small  my-0 mt-2">2. Invoice Details</h6>
                                <div class="pl-lg-4">
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label">Agency</asp:Label><br />
                                            <asp:Label runat="server" ID="lblAgency" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label">Invoice No.</asp:Label><br />
                                            <asp:Label runat="server" ID="lblInvoiceNo" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label"> Invoice Date</asp:Label><br />
                                            <asp:Label runat="server" ID="lblInvoiceDate" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label"> Amount (<i class="fa fa-rupee-sign"></i>)</asp:Label><br />
                                            <asp:Label runat="server" ID="lblAmount" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row mt-1">
                                        <div class="col-lg-12">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label"> Uploaded Invoice</asp:Label><br />
                                            <asp:Label runat="server" ID="lblUploadedInvoice" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                        </div>


                                    </div>

                                </div>

                                <h6 class="heading-small  my-0 mt-2">3. Receiving Details</h6>
                                <div class="pl-lg-4">
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label">Received At Store</asp:Label><br />
                                            <asp:Label runat="server" ID="lblRecStore" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label">Receive By</asp:Label><br />
                                            <asp:Label runat="server" ID="lblReceivedBy" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label"> Receive On</asp:Label><br />
                                            <asp:Label runat="server" ID="lblReceiveOn" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                        </div>

                                    </div>

                                </div>


                                <hr />
                                          
                                             <h3 class="heading-small  my-0 mt-2">1. Issuance Details</h3>
                                <div class="pl-lg-4">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label">Issued At ETM Branch/Store</asp:Label><br />
                                            <asp:Label runat="server" ID="lblissuestore" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                        </div>
                                        

                                    </div>

                                </div>
                            </div>

                            

                        </div>
                                </div>
                         
                            <div class="col-lg-6">
                               <h6 class="heading-small  my-0"> Log Detailsss</h6>
                            </div>
                            </div>
                    
                    </div>
                </asp:Panel>

            <div style="visibility: hidden;">
                <asp:Button ID="Button9" runat="server" Text="" />
                <asp:Button ID="Button11" runat="server" Text="" />
            </div>
     
    </div>

    <script type="text/javascript">
        $('#gvETMDetails').DataTable({
            "pageLength": 10,


        });

    </script>
</asp:Content>





