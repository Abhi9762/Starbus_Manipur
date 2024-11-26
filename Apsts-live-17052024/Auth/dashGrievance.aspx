<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="dashGrievance.aspx.cs" Inherits="Auth_dashGrievance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Grievance</title>
    <link href="dashAssests/css/nucleo-icons.css" rel="stylesheet" />
    <link href="dashAssests/css/nucleo-svg.css" rel="stylesheet" />
    <link id="pagestyle" href="dashAssests/css/material-dashboard.css?v=3.0.4" rel="stylesheet" />

    <link rel="stylesheet" href="../assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" type="text/css" />
    <script type="text/javascript">
        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("btnFile").click();
            }
        }
    </script>
    <style>
        .ModalPopupBG {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
    </style>
</head>
<body class="g-sidenav-show  bg-gray-200">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <main class="main-content position-relative max-height-vh-100 h-100 border-radius-lg ">
            <div class="container-fluid py-4">
                <div class="row">
                    <div class="col-xl-7 col-sm-12 mb-xl-0 pb-4">
                        <div class="row">
                            <h5>About Grievance</h5>
                            <div class="col-xl-12 col-sm-12 mb-xl-0 pb-4">
                                <div class="card">
                                    <div class="card-body p-3">
                                        <h6 class="mb-0">
                                            <asp:Label ID="lblCategory_subCategory" runat="server" Text="DURING JOURNEY - BEHAVIOUR OF CONDUCTOR/DRIVER"></asp:Label>
                                            <span class="float-end">Ref No
                                                <asp:Label ID="lblRefNo" runat="server" Text="20228"></asp:Label>
                                            </span>
                                        </h6>
                                        <p class="mb-0">
                                            <asp:Label ID="lblGrievanceRemark" runat="server" Text=""></asp:Label>
                                        </p>
                                        <p class="mb-0">
                                            <span class="text-dark">Ticket No</span>
                                            <asp:Label ID="lblTicketNo" runat="server" Text=""></asp:Label>
                                        </p>
                                        <p class="mb-0">
                                            <span class="text-dark">Bus No</span>
                                            <asp:Label ID="lblBusNo" runat="server" Text=""></asp:Label>
                                        </p>
                                        <p class="mb-0">
                                            <span class="text-dark">Latitude and Longitude</span>
                                            <asp:Label ID="lblLatt" runat="server" Text=""></asp:Label>,
                                            <asp:Label ID="lblLongg" runat="server" Text=""></asp:Label>
                                        </p>
                                        <p class="mb-0">
                                            <span class="text-dark">Reported at</span>
                                            <asp:Label ID="lblDateTime" runat="server" Text="21-09-2022 12:12 AM"></asp:Label>
                                        </p>
                                    </div>
                                    <hr class="dark horizontal my-0" />
                                    <div class="card-footer p-3">
                                        <h6 class="mb-0">Reported By</h6>
                                        <p class="mb-0">
                                            <span class="text-dark">Name</span>
                                            <asp:Label ID="lblReportedByName" runat="server" Text=""></asp:Label>
                                        </p>
                                        <p class="mb-0">
                                            <span class="text-dark">Mobile</span>
                                            <asp:Label ID="lblReportedByMobile" runat="server" Text=""></asp:Label>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <h5>Grievance Images</h5>
                            <div class="col-xl-6 col-sm-12 mb-xl-0 pb-4">
                                <asp:Image ID="img1" runat="server" ImageUrl="dashAssests/img/home-decor-1.jpg" alt="img-blur-shadow" class="img-fluid shadow border-radius-xl" Style="width: 100%; min-height: 120px;" />
                            </div>
                            <div class="col-xl-6 col-sm-12 mb-xl-0 pb-4">
                                <asp:Image ID="img2" runat="server" ImageUrl="dashAssests/img/home-decor-1.jpg" alt="img-blur-shadow" class="img-fluid shadow border-radius-xl" Style="width: 100%; min-height: 120px;" />
                            </div>

                        </div>
                    </div>
                    <div class="col-xl-5 col-sm-12 mb-xl-0 mb-4">
                        <asp:Panel ID="pnlAction" runat="server" Width="100%">
                            <div class="row">
                                <h5>Action</h5>
                                <div class="col-xl-12 col-sm-12 mb-xl-0 pb-4">
                                    <div class="card">
                                        <hr class="dark horizontal my-0" />
                                        <div class="card-footer p-3">
                                            <div class="row" id="traction" runat="server" visible="true">
                                                <div class="col-lg-6 col-md-8 col-sm-12">
                                                    <div class="row">
                                                        <div class="col-auto" style="text-align: right;">
                                                            Action <span style="color: Red;">*</span>
                                                        </div>
                                                        <div class="col">
                                                            <asp:DropDownList runat="server" ID="ddAcceptReject1" AutoPostBack="True" CssClass="input-group-text text-start" Style="width: 100%;" OnSelectedIndexChanged="ddAcceptReject1_SelectedIndexChanged">
                                                                <asp:ListItem Value="0"> Select </asp:ListItem>
                                                                <asp:ListItem Value="1">Assign</asp:ListItem>
                                                                <asp:ListItem Value="2">Reject</asp:ListItem>
                                                                <asp:ListItem Value="3">Dispose</asp:ListItem>
                                                                <asp:ListItem Value="4">Instruction</asp:ListItem>
                                                                <asp:ListItem Value="5">Return</asp:ListItem>
                                                            </asp:DropDownList>
                                                          <%--  <asp:DropDownList ID="ddAcceptReject" runat="server" OnSelectedIndexChanged="ddAcceptReject_SelectedIndexChanged" AutoPostBack="True" CssClass="input-group-text text-start" Style="width: 100%;">
                                                                <asp:ListItem Value="0"> Select </asp:ListItem>
                                                                <asp:ListItem Value="1">Assign</asp:ListItem>
                                                                <asp:ListItem Value="2">Reject</asp:ListItem>
                                                                <asp:ListItem Value="3">Dispose</asp:ListItem>
                                                                <asp:ListItem Value="4">Instruction</asp:ListItem>
                                                            </asp:DropDownList>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row mt-4" id="tr1" runat="server" visible="true">
                                                <div class="col-lg-6 col-md-8 col-sm-12">
                                                    <div class="row">
                                                        <div class="col-auto" style="text-align: right;">
                                                            Depot <span style="color: Red;">*</span>
                                                        </div>
                                                        <div class="col">
                                                            <asp:DropDownList ID="ddDepot" runat="server" AutoPostBack="True" CssClass="input-group-text text-start" Style="width: 100%;">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="row mt-4" id="trremark" runat="server" visible="true">
                                                <div class="col-lg-6 col-md-8 col-sm-12">
                                                    <div class="row">
                                                        <div class="col-auto" style="text-align: right;">
                                                            <asp:Label ID="lblRemark" runat="server" Text="Description "></asp:Label><span style="color: Red;">*</span>
                                                        </div>
                                                        <div class="col">
                                                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="input-group-text text-lg-start p-1"
                                                                Style="width: 100%; resize: none;"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row mt-4" id="trattachfile" runat="server" visible="false">
                                                <div class="col-lg-6 col-md-8 col-sm-12">
                                                    <div class="row">
                                                        <div class="col-auto" style="text-align: right;">
                                                            Attachment<span style="color: Red;">(Optional)</span>
                                                        </div>
                                                        <div class="col">
                                                            <asp:Button ID="btnFile" OnClick="btnFile_Click" runat="server" CausesValidation="False" CssClass="button1"
                                                                Style="display: none" TabIndex="18" Text="" Width="80px" />
                                                            <asp:FileUpload ID="FileUpload1" runat="server" Style="color: transparent; width: 100%;" />
                                                            <br />
                                                            <asp:Label ID="lblfileUp1" runat="server" ForeColor="Green" Visible="False" />
                                                            <asp:Label ID="lblfileUpMsg1" runat="server" ForeColor="Red" Visible="true" Font-Size="12px">jpg/png, max. 1 MB</asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" id="trbutton" runat="server" visible="true" style="margin-top: 20px;">
                                                <div class="col-lg-12 text-center">

                                                    <asp:LinkButton ID="btnassign" runat="server" OnClick="btnassign_Click" CssClass="btn btn-success" Style="border-radius: 4px;"> Assign</asp:LinkButton>
                                                    <asp:LinkButton ID="btnReset" runat="server" OnClick="btnReset_Click" CssClass="btn btn-warning" Style="border-radius: 4px;"> Reset</asp:LinkButton>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <div class="row">
                            <h5>Transactions</h5>
                            <div class="col-xl-12 col-sm-12 mb-xl-0 pb-4">
                                <div class="card">
                                    <hr class="dark horizontal my-0" />
                                    <div class="card-footer p-3">
                                        <div class="timeline timeline-one-side">
                                            <asp:GridView ID="grTransactions" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="w-100" ShowHeader="false">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <div class="timeline-block mb-3">
                                                                <span class="timeline-step">
                                                                    <i class="ni ni-check-bold text-success text-gradient"></i>
                                                                </span>
                                                                <div class="timeline-content">
                                                                    <h6 class="text-dark text-sm font-weight-bold mb-0"><%# Eval("status_name") %><span class="float-end text-lighter"><%# Eval("updated_by") %></span></h6>
                                                                    <p class="text-secondary font-weight-bold text-xs mt-1 mb-0"><%# Eval("update_datetime") %></p>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <cc1:ModalPopupExtender ID="mp" runat="server" CancelControlID="idClose" TargetControlID="Button6"
                        PopupControlID="pnlImg" BackgroundCssClass="ModalPopupBG">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlImg" Style="display: none" runat="server">
                        <div class="modal-content modal-dialog modal-lg">
                            <div class="card">
                                <div class="card-header">
                                    <span class="me-auto font-weight-bold">
                                        <asp:Label ID="lblmpHeaderText" runat="server"></asp:Label></span>
                                    <asp:LinkButton ID="idClose" runat="server" CssClass="float-end">
                                   <i class="fa fa-times" style="font-size:22px;"></i>
                                    </asp:LinkButton>

                                </div>
                                <hr class="horizontal dark m-0" />
                                <div class="card-body">
                                    <asp:Label ID="lblmpMessageText" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div style="visibility: hidden;">
                        <asp:Button ID="Button6" runat="server" />
                    </div>
                </div>
            </div>
        </main>
    </form>
    <script src="dashAssests/js/core/popper.min.js"></script>
    <script src="dashAssests/js/core/bootstrap.min.js"></script>
    <script src="dashAssests/js/plugins/perfect-scrollbar.min.js"></script>
    <script src="dashAssests/js/plugins/smooth-scrollbar.min.js"></script>

    <script src="dashAssests/js/material-dashboard.min.js?v=3.0.4"></script>
</body>
</html>
