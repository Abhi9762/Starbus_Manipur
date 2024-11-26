<%@ Page Title="" Language="C#" MasterPageFile="~/mainMasterPage.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="NewBusPassRegistration.aspx.cs" Inherits="NewBusPassRegistration" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="assets/css/travelllerStepProgressBar.css" rel="stylesheet" />
    <script src="js/sha1.js"></script>
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
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
        .multi-select-button {
            width: 200px;
            height: 33px;
            padding: 5px;
        }

            .multi-select-button:after {
                margin-left: 9.4em !important;
            }

        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .text-bold {
            font-weight: bold;
        }

        .rbl label {
            margin-left: 7px;
            margin-right: 20px;
            vertical-align: initial;
            font-size: 10pt;
            display: inline;
        }

        body {
            font-size: 0.8rem;
        }

        h6 {
            font-size: 0.8rem;
        }

        ul {
            list-style-type: square;
        }

        .form-control {
            height: 37px !important;
        }

        .car-wrap .img {
            width: 50%;
            height: 50%;
        }

        #progressbar li {
            width: 25% !important;
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
    <%--<script>
        function GFG_Fun() {
            var aadhaar = document.getElementById('<%= tbAdharNumber.ClientID %>');
            var caadhaar = document.getElementById('<%= tbConfirmAdharNumber.ClientID %>');
            if (aadhaar !== null) {
                
              document.getElementById('<%= hdAdharNumber.ClientID %>').value = SHA1(aadhaar.value);
                document.getElementById('<%= hdConfirmAdharNumber.ClientID %>').value = SHA1(caadhaar.value);

                if (aadhaar.value != "" && caadhaar.value != "")
                {
                    document.getElementById('<%= tbAdharNumber.ClientID %>').value = "XXXXXXXXXXXX";
                    document.getElementById('<%= tbConfirmAdharNumber.ClientID %>').value = "XXXXXXXXXXXX";
                }
            } else {
                alert("Element not found or is null.");
            }
        }
    </script>--%>
    <script>
        function checkUID() {

            var checkempty = document.getElementById('<%= tbAdharNumber.ClientID %>');
            if (checkempty.value != "") {
                var uid = document.getElementById('<%= tbAdharNumber.ClientID %>');
                var uidvl = uid.value
                /*console.log(uid);*/
                if (uidvl.length != 12) {
                    alert("Invalid AAdhaar");
                }

                var Verhoeff = {
                    "d": [[0, 1, 2, 3, 4, 5, 6, 7, 8, 9],
                    [1, 2, 3, 4, 0, 6, 7, 8, 9, 5],
                    [2, 3, 4, 0, 1, 7, 8, 9, 5, 6],
                    [3, 4, 0, 1, 2, 8, 9, 5, 6, 7],
                    [4, 0, 1, 2, 3, 9, 5, 6, 7, 8],
                    [5, 9, 8, 7, 6, 0, 4, 3, 2, 1],
                    [6, 5, 9, 8, 7, 1, 0, 4, 3, 2],
                    [7, 6, 5, 9, 8, 2, 1, 0, 4, 3],
                    [8, 7, 6, 5, 9, 3, 2, 1, 0, 4],
                    [9, 8, 7, 6, 5, 4, 3, 2, 1, 0]],
                    "p": [[0, 1, 2, 3, 4, 5, 6, 7, 8, 9],
                    [1, 5, 7, 6, 2, 8, 3, 0, 9, 4],
                    [5, 8, 0, 3, 7, 9, 6, 1, 4, 2],
                    [8, 9, 1, 6, 0, 4, 3, 5, 2, 7],
                    [9, 4, 5, 3, 1, 2, 6, 8, 7, 0],
                    [4, 2, 8, 6, 5, 7, 3, 9, 0, 1],
                    [2, 7, 9, 3, 8, 0, 6, 4, 1, 5],
                    [7, 0, 4, 6, 9, 1, 3, 2, 5, 8]],
                    "j": [0, 4, 3, 2, 1, 5, 6, 7, 8, 9],
                    "check": function (str) {
                        var c = 0;
                        str.replace(/\D+/g, "").split("").reverse().join("").replace(/[\d]/g, function (u, i) {
                            c = Verhoeff.d[c][Verhoeff.p[i % 8][parseInt(u, 10)]];
                        });
                        return c;

                    },
                    "get": function (str) {

                        var c = 0;
                        str.replace(/\D+/g, "").split("").reverse().join("").replace(/[\d]/g, function (u, i) {
                            c = Verhoeff.d[c][Verhoeff.p[(i + 1) % 8][parseInt(u, 10)]];
                        });
                        return Verhoeff.j[c];
                    }
                };

                String.prototype.verhoeffCheck = (function () {
                    var d = [[0, 1, 2, 3, 4, 5, 6, 7, 8, 9],
                    [1, 2, 3, 4, 0, 6, 7, 8, 9, 5],
                    [2, 3, 4, 0, 1, 7, 8, 9, 5, 6],
                    [3, 4, 0, 1, 2, 8, 9, 5, 6, 7],
                    [4, 0, 1, 2, 3, 9, 5, 6, 7, 8],
                    [5, 9, 8, 7, 6, 0, 4, 3, 2, 1],
                    [6, 5, 9, 8, 7, 1, 0, 4, 3, 2],
                    [7, 6, 5, 9, 8, 2, 1, 0, 4, 3],
                    [8, 7, 6, 5, 9, 3, 2, 1, 0, 4],
                    [9, 8, 7, 6, 5, 4, 3, 2, 1, 0]];
                    var p = [[0, 1, 2, 3, 4, 5, 6, 7, 8, 9],
                    [1, 5, 7, 6, 2, 8, 3, 0, 9, 4],
                    [5, 8, 0, 3, 7, 9, 6, 1, 4, 2],
                    [8, 9, 1, 6, 0, 4, 3, 5, 2, 7],
                    [9, 4, 5, 3, 1, 2, 6, 8, 7, 0],
                    [4, 2, 8, 6, 5, 7, 3, 9, 0, 1],
                    [2, 7, 9, 3, 8, 0, 6, 4, 1, 5],
                    [7, 0, 4, 6, 9, 1, 3, 2, 5, 8]];

                    return function () {
                        var c = 0;
                        this.replace(/\D+/g, "").split("").reverse().join("").replace(/[\d]/g, function (u, i) {
                            c = d[c][p[i % 8][parseInt(u, 10)]];
                        });
                        return (c === 0);
                    };
                })();

                if (Verhoeff['check'](uid.value) === 0) {
                    /*return true;*/
                    //alert('Match Found..!');
                    var aadhaar = document.getElementById('<%= tbAdharNumber.ClientID %>');
                var caadhaar = document.getElementById('<%= tbConfirmAdharNumber.ClientID %>');
                if (aadhaar !== null) {

                    document.getElementById('<%= hdAdharNumber.ClientID %>').value = SHA1(aadhaar.value);
                    document.getElementById('<%= hdConfirmAdharNumber.ClientID %>').value = SHA1(caadhaar.value);

                    if (aadhaar.value != "" && caadhaar.value != "") {
                        document.getElementById('<%= tbAdharNumber.ClientID %>').value = "XXXXXXXXXXXX";
                        document.getElementById('<%= tbConfirmAdharNumber.ClientID %>').value = "XXXXXXXXXXXX";
                    }
                } else {
                    alert("Element not found or is null.");
                    document.getElementById('<%= tbAdharNumber.ClientID %>').value = "";
                        document.getElementById('<%= tbConfirmAdharNumber.ClientID %>').value = "";
                }
            } else {
                /*return false;*/
                    alert('Invalid Adhaar No...!');
                    document.getElementById('<%= tbAdharNumber.ClientID %>').value = "";
                        document.getElementById('<%= tbConfirmAdharNumber.ClientID %>').value = "";
            }
        }
            /*e.preventDefault();*/

    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnMinAge" runat="server" />
    <asp:HiddenField ID="hdnMaxAge" runat="server" />
    <asp:HiddenField ID="hdAdharNumber" runat="server" />
    <asp:HiddenField ID="hdConfirmAdharNumber" runat="server" />
    <div class="container">

        <div class="row my-3">
            <div class="col-12">
                <ul id="progressbar">
                    <li class="text-center active" id="apply_one"><strong>Apply for New Pass</strong></li>
                    <li class="text-center" id="confirm_two"><strong>Confirm Details</strong></li>
                    <li class="text-center" id="confirm_three"><strong>Payment</strong></li>
                    <li class="text-center" id="confirm_four"><strong>Download</strong></li>
                </ul>
            </div>

        </div>
        <div class="row pb-5">
            <div class="col-lg-3">
                <div class="card">
                    <div class="card-header pt-0 pb-0">
                        <div class="row">
                            <div class="col-lg-12 pt-2">
                                <h5 class="text-danger" style="font-size: 11pt;">Please Note</h5>
                            </div>
                        </div>

                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12 form-control-label">
                                <asp:Label runat="server">1. You can apply for the following passes:Government Pass,Girl Student Pass.</asp:Label>
                                <br />
                                <asp:Label runat="server">2. Please Keep working 10 digit mobile number handy.</asp:Label>
                                <br />
                                <asp:Label runat="server">3. If Aadhar Number is mandatory,keep it handy</asp:Label>
                                <br />
                                <br />
                            </div>
                        </div>

                        <asp:Panel ID="pnlinstruction" runat="server" Visible="false" Style="line-height: 23px;">
                            <hr />
                            <h5 class="text-danger" style="font-size: 11pt;">Instructions</h5>
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
            <div class="col-lg-9">
                <asp:Panel ID="pnlDetails" runat="server">
                    <div class="card">
                        <div class="card-header">
                            <div class="row">
                                <div class="col-lg-3" style="width: 19%; padding-top: 6px;">
                                    <asp:Label runat="server" Text="Select Bus Pass Type" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>

                                </div>

                                <div class="col-lg-4">

                                    <asp:DropDownList ID="ddlbuspassType" AutoPostBack="true" OnSelectedIndexChanged="ddlbuspassType_SelectedIndexChanged" runat="server" class="form-control form-control-sm" ToolTip="Select Bus Pass Type">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="col-lg-1 pt-1">
                                    <asp:UpdatePanel ID="updatePanelToggle" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="lbtnInfo" Visible="false" runat="server" OnClick="lbtnInfo_Click" CssClass="text-primary">
                                                     <i class="fa fa-info-circle fa-2x"></i></asp:LinkButton>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                                <div class="col-lg-4" style="text-align: right; width: 39%; font-size: 9pt; padding-top: 6px;">
                                    <span class="text-danger">Fields Marked * are Mendatory</span>
                                </div>

                            </div>
                        </div>
                        <div class="card-body">

                            <div class="row">
                                <div class="col-lg-12">
                                    <h6 class="text-bold">Enter Personal Details</h6>
                                    <div class="row my-2">
                                        <div class="col-lg-4">
                                            <asp:Label runat="server" Text="Name" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>

                                            <asp:TextBox ID="tbName" runat="server" CssClass="form-control form-control-sm" placeholder="Max 30 characters" MaxLength="30" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Enter Name"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="UppercaseLetters,LowercaseLetters,Custom" TargetControlID="tbName" ValidChars=" " />

                                        </div>
                                        <div class="col-lg-4">
                                            <asp:Label runat="server" Text="Gender" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>

                                            <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control form-control-sm" data-toggle="tooltip" data-placement="bottom" title="Select Gender">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                                <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                                <asp:ListItem Text="Other" Value="O"></asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-lg-4">
                                            <asp:Label runat="server" Text="Date of Birth" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>

                                            <asp:UpdatePanel ID="up" runat="server">
                                                <ContentTemplate>
                                                    <div class="input-group input-group-alternative">
                                                        <div class="input-group date">
                                                            <asp:TextBox ID="tbDate" ToolTip="Enter D.O.B" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="CalendarExtender1" runat="server" FilterType="Numbers,Custom" TargetControlID="tbDate" ValidChars="/" />
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="row my-2">
                                        <div class="col-lg-4">
                                            <asp:Label runat="server" Text="Father Name" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>
                                            <asp:TextBox ID="tbFatherName" runat="server" CssClass="form-control form-control-sm" placeholder="Max 30 characters" MaxLength="30" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Enter Father's Name"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="UppercaseLetters,LowercaseLetters,Custom" ValidChars=" " TargetControlID="tbFatherName" />
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:Label runat="server" Text="Service Type" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span><br />
                                            <asp:DropDownList ID="ddlServiceType" runat="server" CssClass="form-control form-control-sm" data-toggle="tooltip" data-placement="bottom" title="Select Service Type">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <asp:Panel ID="pnlRoute" runat="server" Visible="false">
                                <h6 class="text-bold">Pass Requested For</h6>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="row my-2">
                                            <div class="col-lg-8">
                                                <asp:Label runat="server" Text="Route" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>

                                                <asp:DropDownList ID="ddlRoute" AutoPostBack="true" OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm" ToolTip="Select Route">
                                                    <asp:ListItem Text="Select Route" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                        <div class="row my-2">
                                            <div class="col-lg-4">
                                                <asp:Label runat="server" Text="From" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>

                                                <asp:DropDownList ID="ddlFrom" AutoPostBack="true" OnSelectedIndexChanged="ddlFrom_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm" data-toggle="tooltip" data-placement="bottom" title="From">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-lg-4">
                                                <asp:Label runat="server" Text="To" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>

                                                <asp:DropDownList ID="ddlTo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTo_SelectedIndexChanged" CssClass="form-control form-control-sm" data-toggle="tooltip" data-placement="bottom" title="To">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <hr />
                            </asp:Panel>

                            <div class="row ">
                                <div class="col-lg-12">
                                    <h6 class="text-bold">Enter Contact Details</h6>
                                    <div class="row my-2">
                                        <div class="col-lg-4">
                                            <asp:Label runat="server" Text="Mobile No." CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>
                                            <asp:TextBox ID="tbMobile" runat="server" CssClass="form-control form-control-sm" placeholder="Max 10 digits" MaxLength="10" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Enter Mobile Number"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers" TargetControlID="tbMobile" />

                                        </div>
                                        <div class="col-lg-4">
                                            <asp:Label runat="server" Text="Email" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>


                                            <asp:TextBox ID="tbEmail" runat="server" CssClass="form-control form-control-sm" placeholder="Max 50 characters" MaxLength="50" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Enter Email"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="lowercaseletters,Numbers,Custom" TargetControlID="tbEmail" ValidChars="@,." />

                                        </div>
                                    </div>
                                    <div class="row my-2">

                                        <div class="col-lg-4">
                                            <asp:Label runat="server" Text="State" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>

                                            <asp:DropDownList ID="ddlState" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" CssClass="form-control form-control-sm" data-toggle="tooltip" data-placement="bottom" title="Select State">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-lg-4">
                                            <asp:Label runat="server" Text="District" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>

                                            <asp:DropDownList ID="ddlDistrict" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" CssClass="form-control form-control-sm" data-toggle="tooltip" data-placement="bottom" title="Select District">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-lg-4">
                                            <asp:Label runat="server" Text="City" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>

                                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control form-control-sm" data-toggle="tooltip" data-placement="bottom" title="Select City">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>


                                    </div>
                                    <div class="row my-2">
                                        <div class="col-lg-8">
                                            <asp:Label runat="server" Text="Address" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>

                                            <asp:TextBox ID="tbAddress" runat="server" Height="50px" CssClass="form-control form-control-sm" placeholder="Max 100 characters" MaxLength="100" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Enter Address"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom,Uppercaseletters,Lowercaseletters" TargetControlID="tbAddress" ValidChars=" ,.,-,/"  />

                                        </div>
                                        <div class="col-lg-4">
                                            <asp:Label runat="server" Text="Pincode" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>

                                            <asp:TextBox ID="tbPincode" runat="server" CssClass="form-control form-control-sm" placeholder="Max 6 digits" MaxLength="6" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Enter Pincode"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers" TargetControlID="tbPincode" ValidChars=", " />

                                        </div>


                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="row d-none ">

                                <div class="col-lg-12">
                                    <h6 class="text-bold">Validity</h6>
                                    <div class="row my-2">
                                        <div class="col-lg-4">
                                            <asp:Label runat="server" Text="From" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>
                                            <div class="input-group input-group-alternative">
                                                <div class="input-group">

                                                    <asp:TextBox ID="tbValidityFrom" ToolTip="Enter Validity From" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom" TargetControlID="tbValidityFrom" ValidChars="/" />




                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:Label runat="server" Text="Upto" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>
                                            <div class="input-group input-group-alternative">


                                                <div class="input-group">

                                                    <asp:TextBox ID="tbValidityTo" ToolTip="Enter Validity To" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,Custom" TargetControlID="tbValidityTo" ValidChars="/" />




                                                </div>
                                            </div>
                                        </div>



                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-lg-12">
                                    <h6 class="text-bold">Enter Aadhar Details</h6>
                                    <div class="row my-2">
                                        <div class="col-lg-4">
                                            <asp:Label runat="server" Text="Aadhar No." CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>
                                            <asp:TextBox ID="tbAdharNumber" runat="server" CssClass="form-control form-control-sm"
                                                placeholder="Max 12 digits" MaxLength="12" AutoComplete="off" data-toggle="tooltip"
                                                data-placement="bottom" title="Enter Aadhar Number"></asp:TextBox>

                                        </div>
                                        <div class="col-lg-4">
                                            <asp:Label runat="server" Text="Confirm Aadhar No." CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>
                                            <asp:TextBox ID="tbConfirmAdharNumber" runat="server" AutoPostBack="true" onblur="return checkUID();"
                                                OnTextChanged="tbConfirmAdharNumber_TextChanged" CssClass="form-control form-control-sm" placeholder="Max 12 digits" MaxLength="12" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Enter Aadhar Number for confirmation"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-8" style="border-right: 1px solid">
                                    <asp:Panel ID="pnlDocument" runat="server" Visible="false">
                                        <h6 class="text-bold">Uploads - <span style="font-size: 9pt;">Document</span><span class="text-danger">*</span>
                                        </h6>
                                        <div class="row">
                                            <div class="col-lg-6" style="border-right: 1px solid #e6e6e6">
                                                <asp:Panel ID="pnlIDProofNew" runat="server">
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <asp:Label runat="server" Text="ID Proof" CssClass="form-control-label"></asp:Label>
                                                        </div>
                                                        <div class="col-lg-12">
                                                            <asp:RadioButtonList ID="rbtnIdProof" AutoPostBack="true" OnSelectedIndexChanged="rbtnIdProof_SelectedIndexChanged" GroupName="radio" CssClass="rbl" RepeatDirection="vertical" runat="server">
                                                            </asp:RadioButtonList>

                                                            <asp:Panel ID="pnlIdProof" runat="server" Visible="false">
                                                                <div class="col-lg-12">
                                                                    <label style="font-weight: normal; color: Red; font-size: 8pt; margin: 0px;">
                                                                        PDF file only(Max. Size 1MB)</label><br />

                                                                    <asp:Button ID="btnUploadDoc" runat="server" OnClick="btnUploadDoc_Click" CausesValidation="False" Style="display: none" Text="" />
                                                                    <asp:FileUpload ID="fileUpload" runat="server" CssClass="file-control" onchange="UploadDoc(this);" />
                                                                    <br />
                                                                    <asp:Label runat="server" ID="lblIDpdf" CssClass="col-form-label control-label" Style="font-size: 10pt; font-weight: normal; color: #08a35b!important;"></asp:Label>

                                                                    <br />
                                                                    <asp:Label runat="server" Text="Document Id Number" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>
                                                                    <br />
                                                                    <asp:TextBox ID="txtidproofno" runat="server" CssClass="form-control form-control-sm" placeholder="Id Proof Number" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Enter Uploaded Document Id Number"></asp:TextBox>

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
                                                            <asp:RadioButtonList ID="rbtnAddressProof" AutoPostBack="true" GroupName="radio" OnSelectedIndexChanged="rbtnAddressProof_SelectedIndexChanged" CssClass="rbl" RepeatDirection="vertical" runat="server">
                                                            </asp:RadioButtonList>
                                                            <asp:Panel ID="pnlAddProof" runat="server" Visible="false">
                                                                <div class="col-lg-12">
                                                                    <label style="font-weight: normal; color: Red; font-size: 8pt; margin: 0px;">
                                                                        PDF file only(Max. Size 1MB)</label><br />

                                                                    <asp:Button ID="btnAddproof" runat="server" OnClick="btnAddproof_Click" CausesValidation="False" Style="display: none" Text="" />
                                                                    <asp:FileUpload ID="fileaddproof" runat="server" CssClass="file-control" onchange="UploadAddDoc(this);" />
                                                                    <br />
                                                                    <asp:Label runat="server" ID="lbladd" CssClass="col-form-label control-label" Style="font-size: 10pt; font-weight: normal; color: #08a35b!important;"></asp:Label>
                                                                    <br />
                                                                    <asp:Label runat="server" Text="Address Proof Id Number" CssClass="form-control-label"></asp:Label><span class="text-danger">*</span>
                                                                    <br />
                                                                    <asp:TextBox ID="txtaddressproofno" runat="server" CssClass="form-control form-control-sm" placeholder="Address Proof Number" AutoComplete="off" data-toggle="tooltip" data-placement="bottom" title="Enter Uploaded Document Id Number"></asp:TextBox>

                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="col-lg-4">
                                    <asp:Panel ID="pnlPhoto" runat="server" Visible="false">
                                        <h6 class="text-bold">Uploads - <span style="font-size: 9pt;">Photo</span><span class="text-danger">*</span>  </h6>
                                        <div class="row">
                                            <div class="col-lg-12">

                                                <div class="row">

                                                    <div class="col-lg-12">
                                                        <label style="font-weight: normal; color: Red; font-size: 8pt; margin: 0px;">
                                                            Image file .JPG/ .PNG/ .JPEG only(Max. Size 1MB)</label>
                                                    </div>
                                                </div>
                                                <div class="row mt-1 m-0">
                                                    <div class="col-lg-5">

                                                        <asp:Button ToolTip="Upload Web Portal Image" ID="btnUploadWebPortal" OnClick="btnUploadWebPortal_Click" runat="server" CausesValidation="False" CssClass="form-control form-control-sm"
                                                            Style="display: none" TabIndex="18" Text="Upload Image" accept=".png,.jpg,.jpeg,.gif" Width="80px" />

                                                        <asp:FileUpload ID="FileWebPortal" onchange="UploadImageWeb(this);" runat="server" TabIndex="9" />
                                                        <asp:Image ID="ImgWebPortal" onchange="UploadImageWeb(this);" runat="server" Visible="false" Style="border-width: 0px; height: 50px; width: 50px; border: 2px solid #eaf4ff;" />
                                                        <asp:LinkButton ID="lbtncloseWebImage" runat="server" OnClick="lbtncloseWebImage_Click" Style="font-size: 5pt; border-radius: 25px; margin-bottom: 22pt" Visible="false" CssClass="btn btn-sm btn-danger"><i class="fa fa-times"></i></asp:LinkButton><br />


                                                        <br />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <img alt="Visual verification" title="Please enter the security code as shown in the image."
                                            src="CaptchaImage.aspx" style="width: 70%; border: 1px solid #e6e6e6; height: 35px; border-radius: 5px;" />
                                        <asp:LinkButton runat="server" ID="lbtnRefresh" CssClass=" btn btn-primary" OnClick="lbtnRefresh_Click" Style="height: 36px;">
                                                            <i class="fa fa-sync-alt" ></i></asp:LinkButton>
                                    </div>
                                </div>

                                <div class="col-lg-2">
                                    <asp:TextBox ID="tbcaptchacode" runat="server" placeholder="Enter Text" autocomplete="off" class="form-control form-control-sm" MaxLength="6" Style="height: 41px; text-transform: uppercase;"></asp:TextBox>
                                </div>
                                <div class="col-lg-6">
                                    <div class="input-group">

                                        <asp:LinkButton ID="lbtnSave" runat="server" OnClick="lbtnSave_Click" CssClass="btn btn-success" data-toggle="tooltip" data-placement="bottom" title="Click to Save" Style="height: 37px;"><i class="fa fa-save"></i> Save & Proceed</asp:LinkButton>

                                        <asp:LinkButton ID="lbtnReset" runat="server" OnClick="lbtnReset_Click" CssClass="btn btn-danger" data-toggle="tooltip" data-placement="bottom" title="Click to Reset" Style="height: 37px;"><i class="fa fa-undo"></i> Reset</asp:LinkButton>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>

        <div class="row">
            <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation" CancelControlID="lbtnNoConfirmation"
                TargetControlID="Button3" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlConfirmation" runat="server" Style="width: 500px !important">
                <div class="card">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Please Confirm
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>

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
            <cc1:ModalPopupExtender ID="mpsuccess" runat="server" PopupControlID="pnlsuccess" CancelControlID="lbtnsuccessclose1"
                TargetControlID="Button2" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlsuccess" runat="server" Style="width: 500px !important">
                <div class="card">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Confirm
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblsuccessmsg" runat="server"></asp:Label>
                        <div style="margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnsuccessclose1" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                        </div>
                        <div style="visibility: hidden;">
                            <asp:Button ID="Button2" runat="server" Text="" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpInfo" runat="server" PopupControlID="PanelInfo" CancelControlID="LinkButtonMpInfoClose"
                TargetControlID="lbtnview" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="PanelInfo" runat="server" Style="width: 500px !important">
                <div class="card">
                    <div class="card-header">
                        <h5 class="card-title text-left mb-0">
                            <asp:Label ID="lblAbout" runat="server"></asp:Label>
                        </h5>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">

                        <asp:Label ID="lblEligibilityInfo" runat="server"></asp:Label>
                        <asp:Label ID="lblIDDocumentsInfo" runat="server"></asp:Label>
                        <asp:Label ID="lbladdDocumentsInfo" runat="server"></asp:Label>

                        <asp:Label ID="lblChargesApplicableInfo" runat="server"></asp:Label>
                        <asp:Label ID="lblStateInfo" runat="server"></asp:Label>
                        <asp:Label ID="lblservicetypeInfo" runat="server"></asp:Label>
                        <asp:Label ID="lblvalidityInfo" runat="server"></asp:Label>





                        <div style="margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="LinkButtonMpInfoClose" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                        </div>
                        <div style="visibility: hidden;">
                            <asp:Button ID="lbtnview" runat="server" Text="" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>



    </div>

</asp:Content>




