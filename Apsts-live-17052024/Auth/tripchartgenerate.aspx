<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tripchartgenerate.aspx.cs" Inherits="Auth_tripchartgenerate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="Start your development with a Dashboard for Bootstrap 4." />
    <meta name="author" content="Creative Tim" />
    <title>StarBus* 4.0 | System Admin</title>
    <!-- Favicon -->
    <link rel="icon" href="../assets/img/brand/favicon.png" type="image/png" />
    <!-- Fonts -->
    <!-- Icons -->
    <link rel="stylesheet" href="../assets/vendor/nucleo/css/nucleo.css" type="text/css" />
    <link rel="stylesheet" href="../assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" type="text/css" />
    <!-- Page plugins -->
    <!-- Argon CSS -->
    <link rel="stylesheet" href="../assets/css/argon.css?v=1.2.0" type="text/css" />
    <link href="../assets/vendor/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <link href="../assets/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../assets/js/jquery-n.js"></script>
    <script src="../assets/vendor/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    <link href="../css/paging.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container-fluid">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-6 border-right border-dark">
                            <div class="row">
                                <div class="col">
                                    <h4>Trip Details</h4>
                                </div>
                                <div class="col-auto pr-4">
                                    <h4>
                                        <asp:Label runat="server" ID="lbltripcode" CssClass="text-right" Text=""></asp:Label>
                                    </h4>
                                </div>
                            </div>
                            <div class="row" style="max-height: 274px; overflow: auto;">
                                <div class="col">
                                    <asp:GridView ID="grdtripdetails" runat="server" AutoGenerateColumns="False"
                                        GridLines="None" DataKeyNames=""
                                        class="table table-hover mb-0" Font-Size="13px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Seat No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepot" runat="server" Text='<%#  Eval("seat_no") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ticket No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblServiceCode" runat="server" Text='<%#  Eval("ticketno") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Passenger Info.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblServiceCode" runat="server" Text='<%#  Eval("traveller_name") %>'></asp:Label>,
                                                <asp:Label ID="Label1" runat="server" Text='<%#  Eval("traveller_gender") %>'></asp:Label>,
                                            <asp:Label ID="Label2" runat="server" Text='<%#  Eval("traveller_age") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" CssClass="gridview" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <asp:Panel ID="pnltripdetails" runat="server" Visible="true">
                                <center>
                                 <h1 class="mt-5" style="color:lightgray;font-size:30px">Trip Details <br />Not <br />Available.</h1>
                            </center>
                            </asp:Panel>

                        </div>
                        <div class="col-6 pl-4">

                            <h4 class="text-danger mb-3">Please Fill All Fields To Generate Trip Chart.</h4>
                            <asp:Panel runat="server" Visible="true" ID="pnlwaybill">
                                <div class="row">
                                    <div class="col-6">
                                        <span>Waybill No.</span>
                                    </div>
                                    <div class="col-6">
                                        <asp:TextBox runat="server" MaxLength="20" CssClass="form-control" ID="txtwaybill" placeholder="Max 20 Chars"></asp:TextBox>
                                        <asp:DropDownList runat="server" ID="ddlwaybill" CssClass="form-control" Visible="false">
                                            <asp:ListItem>Select Waybill No.</asp:ListItem>
                                        </asp:DropDownList>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                            TargetControlID="txtwaybill" ValidChars="" />
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" Visible="false" ID="pnlNoconductor">
                                <div class="row mt-3">
                                    <div class="col-6">
                                        <span>Conductor</span><span class="text-danger">*</span>
                                    </div>
                                    <div class="col-6">
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlconductor" placeholder="Max 20 Chars"></asp:DropDownList>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" Visible="false" ID="pnlconductor">
                                <div class="row mt-3">
                                    <div class="col-6">
                                        <span>Second Conductor</span><span class="text-danger">*</span>
                                    </div>
                                    <div class="col-6">
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlconductor2" placeholder="Max 20 Chars"></asp:DropDownList>
                                    </div>
                                </div>
                            </asp:Panel>


                            <div class="row mt-3">
                                <div class="col-6">
                                    <span>Driver</span><span class="text-danger">*</span>
                                </div>
                                <div class="col-6">
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddldriver" placeholder="Max 20 Chars"></asp:DropDownList>
                                </div>
                            </div>

                            <asp:Panel runat="server" Visible="false" ID="pnldriver">
                                <div class="row mt-3">
                                    <div class="col-6">
                                        <span>Second Driver</span><span class="text-danger">*</span>
                                    </div>
                                    <div class="col-6">
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddldriver2" placeholder="Max 20 Chars"></asp:DropDownList>
                                    </div>
                                </div>
                            </asp:Panel>


                            <div class="row mt-3">
                                <div class="col-6">
                                    <span>Bus No.</span><span class="text-danger">*</span>
                                </div>
                                <div class="col-6">
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlbus" placeholder="Max 20 Chars"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row mt-3 mb-3">
                                <div class="col-12">
                                    <center>
                                    <asp:LinkButton runat="server" ID="lbtngenerate" OnClick="lbtngenerate_Click" CssClass="btn btn-success"><i class="fa fa-fast-forward mr-2"></i>Generate Trip Chart</asp:LinkButton>
                             </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
