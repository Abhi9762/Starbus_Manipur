<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grievancedetail.aspx.cs" Inherits="pathikwebpage_grievancedetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="Starbus online ticket booking" />
    <meta name="author" content="starbus" />
    <title>Starbus</title>
    <!-- Favicon -->
    <link rel="icon" href="../assets/img/brand/favicon.png" type="image/png" />

    <!-- Icons -->
    <link rel="stylesheet" href="../assets/vendor/nucleo/css/nucleo.css" type="text/css" />
    <link rel="stylesheet" href="../assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" type="text/css" />
    <!-- Page plugins -->
    <!-- Argon CSS -->
    <link rel="stylesheet" href="../assets/css/argon.css?v=1.2.0" type="text/css" />
    <%-- <link href="../assets/vendor/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />--%>
    <link id="pagestyle" href="../Auth/dashAssests/css/material-dashboard.css?v=3.0.4" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="card" style="padding: 10px; margin-top: 20px; box-shadow: 0 2px 20px -2px rgb(0 0 0 / 10%);">
                        <div style="width: 100%; text-align: left;">
                            <asp:Panel runat="server" Visible="false" ID="pnldetails">
                                <div class="row">
                                    <h3>Grievance</h3>
                                </div>
                                <div class="row mt--2">
                                    <asp:Label runat="server" Font-Size="22px" ID="lblgrvtype" Text="During Journey - Bus Speed"></asp:Label>
                                </div>
                                <div class="row mt-1">
                                    <h3>Refrence No.</h3>
                                </div>
                                <div class="row mt--2">
                                    <asp:Label runat="server" Font-Size="22px" ID="lblrefno" Text="202221"></asp:Label>
                                </div>
                                <div class="row mt-1">
                                    <h3>Remark</h3>
                                </div>
                                <div class="row mt--2">
                                    <asp:Label runat="server" Font-Size="22px" ID="lblremark" Text="Bus Speed very Fast.Bus Speed very Fast."></asp:Label>
                                </div>
                                <div class="row mt-1">
                                    <h3>Bus No.</h3>
                                </div>
                                <div class="row mt--2">
                                    <asp:Label runat="server" ID="lblbusno" Font-Size="22px" Text="UK07DY3665"></asp:Label>
                                </div>
                                <div class="row mt-1">
                                    <h3>Date Time</h3>
                                </div>
                                <div class="row mt--2">
                                    <asp:Label runat="server" Font-Size="22px" ID="lbldate" Text="19-10-2022 10:58 AM"></asp:Label>
                                </div>
                                <div class="row mt-1">
                                    <h3>Grievance Images</h3>
                                </div>
                                <div class="row mt--2">
                                    <asp:Image ID="img1" runat="server" ImageUrl="dashAssests/img/home-decor-1.jpg" alt="img-blur-shadow" class="img-fluid shadow border-radius-xl" Style="width: 100%; min-height: 120px;" />
                                    <asp:Image ID="img2" runat="server" ImageUrl="dashAssests/img/home-decor-1.jpg" alt="img-blur-shadow" class="img-fluid shadow border-radius-xl mt-2" Style="width: 100%; min-height: 120px;" />

                                    <asp:Label runat="server" Font-Size="22px" ID="lblnoimages" Visible="false" CssClass="text-muted" Text="No Images"></asp:Label>

                                </div>
                                <div class="row mt-2">
                                    <h3>Transactions</h3>
                                </div>
                                <div class="row mt--2">
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
                                                                <h6 class="text-dark text-sm font-weight-bold mb-0"><%# Eval("status_name") %><span class="float-end text-muted"><%# Eval("updated_by") %></span></h6>

                                                                <p class="text-secondary font-weight-bold text-xs mt-1 mb-0"><%# Eval("update_datetime") %></p>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="card-body" style="text-align: center;">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
