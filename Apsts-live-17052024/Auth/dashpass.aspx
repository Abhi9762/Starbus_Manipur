<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dashpass.aspx.cs" Inherits="Auth_dashpass" %>


<!DOCTYPE html>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>StarBus* | APSTS Online 2.0</title>
    <meta name="description" content="Sufee Admin - HTML5 Admin Template" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="../citizenAuth/assets/css/bootstrap.min.css" />
    <link id="pagestyle" href="dashAssests/css/material-dashboard.css?v=3.0.4" rel="stylesheet" />
    <link href="assets/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <style>
        *, *::after {
            box-sizing: border-box;
            margin: 0;
        }

        body {
            background-color: #f4f5f6;
        }

        .ticket {
            display: grid;
            grid-template-rows: auto 1fr auto;
        }

        .ticket__header, .ticket__body, .ticket__footer {
            padding: 1.00rem;
            background-color: #fff;
            border: 1px solid #abb5ba;
            box-shadow: 0 2px 4px rgba(41, 54, 61, 0.25);
        }

        .ticket__header {
            font-size: 1.5rem;
            border-top: 0.25rem solid #dc143c;
            border-bottom: none;
            box-shadow: none;
        }

        .ticket__wrapper {
            box-shadow: 0 2px 4px rgba(41, 54, 61, 0.25);
            border-radius: 0.375em 0.375em 0 0;
            overflow: hidden;
        }

        .ticket__divider {
            background-color: #fff;
        }

            .ticket__divider::after {
                content: '';
                position: absolute;
                height: 50%;
                width: 100%;
                top: 0;
                border-bottom: 2px dashed #e9ebed;
            }

        .ticket__notch {
            position: absolute;
            left: -0.5rem;
            width: 1rem;
            height: 1rem;
            overflow: hidden;
        }

            .ticket__notch::after {
                content: '';
                position: relative;
                display: block;
                width: 2rem;
                height: 2rem;
                right: 100%;
                top: -50%;
                border: 0.5rem solid #fff;
                border-radius: 50%;
                box-shadow: inset 0 2px 4px rgba(41, 54, 61, 0.25);
            }

        .ticket__notch--right {
            left: auto;
            right: -0.5rem;
        }

            .ticket__notch--right::after {
                right: 0;
            }

        .ticket__body {
            border-bottom: none;
            border-top: none;
        }

            .ticket__body > * + * {
                margin-top: 1rem;
            }

        .ticket__section > * + * {
            margin-top: 0.25rem;
        }

        .ticket__section > h3 {
            font-size: 1.125rem;
            margin-bottom: 0.5rem;
        }

        .ticket__header, .ticket__footer {
            font-weight: bold;
            font-size: 1.25rem;
            display: flex;
            justify-content: space-between;
        }

        .ticket__footer {
            border-top: 2px dashed #e9ebed;
            border-radius: 0 0 0.325rem 0.325rem;
        }

        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server">
        </asp:ScriptManager>
        <div class="container-fluid pb-5">
            <div class="row">
                <div class="col-xl-8 col-lg-8 col-md-12 col-sm-12">
                    <article class="ticket mt-3">
                        <header class="ticket__wrapper">
                            <div class="ticket__header">
                                <asp:Label ID="lblhd" runat="server" Text="About Transaction" Style="line-height: 20px;"></asp:Label>
                            </div>
                        </header>

                        <div class="ticket__body" style="min-height: 150vh;">
                            <div class="row">
                                <div class="col-6">
                                    <section class="ticket__section">
                                        <h3>Personal Details</h3>
                                    </section>
                                    <section class="ticket__section">
                                        <div class="row">
                                            <div class="col">
                                                <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Name </span><asp:Label ID="lblName" runat="server" Text="NA"></asp:Label></p>
                                            </div>
                                            <div class="col-auto">
                                                <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Father Name </span><asp:Label ID="lblfname" runat="server" Text="NA"></asp:Label></p>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col">
                                                <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Gender </span><asp:Label ID="lblGender" runat="server" Text="NA"></asp:Label></p>
                                            </div>
                                            <div class="col-auto">
                                                <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Date of Birth </span><asp:Label ID="lblDOB" runat="server" Text="NA"></asp:Label></p>
                                            </div>

                                        </div>
                                    </section>
                                </div>
                                <div class="col-6">
                                    <section class="ticket__section">
                                        <h3>Contact Details</h3>
                                    </section>
                                    <section class="ticket__section">
                                        <div class="row">
                                            <div class="col">
                                                <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Mobile Number </span><asp:Label ID="lblMobno" runat="server" Text="NA"></asp:Label></p>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col">
                                                <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Email </span><asp:Label ID="lblemail" runat="server" Text="NA"></asp:Label></p>
                                            </div>
                                        </div>
                                    </section>
                                </div>
                                <div class="col-12">
                                    <section class="ticket__section">
                                        <div class="row">
                                            <div class="col">
                                                <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Address </span><asp:Label ID="lbladdress" runat="server" Text="NA"></asp:Label></p>
                                            </div>
                                        </div>
                                    </section>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-6">
                                    <section class="ticket__section">
                                        <h3>Pass Requested For</h3>
                                    </section>
                                    <section class="ticket__section">
                                        <div class="row">
                                            <div class="col">
                                                <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Route </span><asp:Label ID="lblroute" runat="server" Text="NA"></asp:Label></p>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col">
                                                <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Service Type </span><asp:Label ID="lblservicetype" runat="server" Text="NA"></asp:Label></p>
                                            </div>
                                            <div class="col-auto">
                                                <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Station </span><asp:Label ID="lblstation" runat="server" Text="NA"></asp:Label></p>
                                            </div>
                                        </div>

                                    </section>
                                </div>
                                <div class="col-6">
                                    <section class="ticket__section">
                                        <h3>Pass Validity</h3>
                                    </section>
                                    <section class="ticket__section">
                                        <div class="row">
                                            <div class="col">
                                                <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">From </span><asp:Label ID="lblfrom" runat="server" Text="NA"></asp:Label></p>
                                            </div>
                                            <div class="col">
                                                <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">To </span><asp:Label ID="lblto" runat="server" Text="NA"></asp:Label></p>
                                            </div>
                                        </div>
                                    </section>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-12">
                                    <section class="ticket__section">
                                        <h3>Fare Details</h3>
                                    </section>
                                    <section class="ticket__section">
                                        <div class="row">
                                            <div class="col">
                                                <p class="mb-0">
                                                    <span class="pr-1 text-muted" style="font-size: 14px;">Pass Amout </span><asp:Label ID="lblpassamt" runat="server" Text="0"></asp:Label>
                                                    <span><i class="fa fa-rupee"></i></span>
                                                </p>
                                            </div>
                                            <div class="col">
                                                <p class="mb-0">
                                                    <span class="pr-1 text-muted" style="font-size: 14px;">Extra Charge </span><asp:Label ID="lblextracharge" runat="server" Text="0"></asp:Label>
                                                    <span><i class="fa fa-rupee"></i></span>
                                                </p>
                                            </div>
                                            <div class="col-auto">
                                                <p class="mb-0">
                                                    <span class="pr-1 text-muted" style="font-size: 14px;">Tax Amount </span><asp:Label ID="lbltaxamount" runat="server" Text="0"></asp:Label>
                                                    <span><i class="fa fa-rupee"></i></span>
                                                </p>
                                            </div>
                                        </div>
                                    </section>
                                    <section class="ticket__section pt-2 mt-2" style="border-top: 1px dashed #e6e6e6;">
                                        <span>Total Amount Paid </span>
                                        <span>
                                            <asp:Label ID="lblAmountTotal" runat="server" Text="0"></asp:Label>
                                            <i class="fa fa-rupee"></i></span>
                                    </section>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-6">
                                    <section class="ticket__section">
                                        <h3>Uploaded Photo</h3>
                                    </section>
                                    <section class="ticket__section">
                                        <div class="row">
                                            <div class="col">
                                                <asp:Image ID="img" class="mt-2" runat="server" Style="border-width: 0px; width: 80px; border: 1px solid;" />
                                            </div>
                                        </div>
                                    </section>
                                </div>
                                <div class="col-6">
                                    <section class="ticket__section">
                                        <h3>Uploaded Document</h3>
                                    </section>
                                    <section class="ticket__section">
                                        <div class="row">
                                            <div class="col">
                                                <p class="mb-0">
                                                    <span class="pr-1 text-muted" style="font-size: 14px;">
                                                        <asp:Label ID="lblidproof" runat="server" Text="Id Proof"></asp:Label></span>

                                                </p>
                                                <asp:HiddenField ID="hdidproof" runat="server" />
                                                <asp:LinkButton ID="lbtnviewidproof" runat="server" OnClick="lbtnviewidproof_Click" CssClass="btn btn-success btn-sm"> View </asp:LinkButton>
                                                <asp:LinkButton ID="lbtndownloidproof" runat="server" OnClick="lbtndownloidproof_Click" CssClass="btn btn-warning btn-sm"> Download </asp:LinkButton>
                                            </div>
                                            <div class="col">
                                                <p class="mb-0">
                                                    <span class="pr-1 text-muted" style="font-size: 14px;">
                                                        <asp:Label ID="lbladdressproof" runat="server" Text="Address Proof"></asp:Label></span>
                                                </p>
                                                <asp:HiddenField ID="hdaddressproof" runat="server" />
                                                <asp:LinkButton ID="lbtnviewaddressproof" runat="server" OnClick="lbtnviewaddressproof_Click" CssClass="btn btn-success btn-sm"> View </asp:LinkButton>
                                                <asp:LinkButton ID="lbtndownloadaddressproof" runat="server" OnClick="lbtndownloadaddressproof_Click" CssClass="btn btn-warning btn-sm"> Download </asp:LinkButton>
                                            </div>
                                        </div>
                                    </section>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-6" style="border-right: 1px solid #e6e6e6;">
                                    <section class="ticket__section">
                                        <h3>Transaction  log</h3>
                                    </section>
                                    <section class="ticket__section">
                                        <div class="row">
                                            <div class="col">
                                                <asp:Repeater ID="rptTxnLog" runat="server" Visible="false">
                                                    <ItemTemplate>
                                                        <div class="row mb-0">
                                                            <div class="col-md-6">
                                                                <p class="mb-2" style="font-size: 13px; line-height: 14px;">
                                                                    <%# Eval("txt_statusname")%>
                                                                </p>
                                                                <p style="font-size: 13px; line-height: 14px; text-transform: initial;">
                                                                    <%# Eval("updateon")%>
                                                                </p>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <p style="font-size: 13px; line-height: 14px;">
                                                                    <%# Eval("updateby")%>
                                                                </p>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <p style="font-size: 13px; line-height: 14px;">
                                                                    <%# Eval("ipaddress")%>
                                                                </p>
                                                            </div>
                                                        </div>
                                                        <hr style="margin-top: 0; margin-bottom: 7px;" />
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:Panel ID="pnlnoTxnLog" Visible="true" runat="server">
                                                    <div class="row">
                                                        <div class="col-lg-12 text-center mt-4" runat="server">
                                                            <h6 class="card-title text-muted mb-0">No Record Found </h6>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </section>
                                </div>
                                <div class="col-6">
                                    <section class="ticket__section">
                                        <h3>Pass log</h3>
                                    </section>
                                    <section class="ticket__section">
                                        <div class="row">
                                            <div class="col">
                                                <asp:Repeater ID="rptpassLog" runat="server" Visible="false">
                                                    <ItemTemplate>
                                                        <div class="row mb-0">
                                                            <div class="col-md-6">
                                                                <p class="mb-2" style="font-size: 13px; line-height: 14px;">
                                                                    <%# Eval("txt_statusname")%>
                                                                </p>
                                                                <p style="font-size: 13px; line-height: 14px; text-transform: initial;">
                                                                    <%# Eval("updateon")%>
                                                                </p>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <p style="font-size: 13px; line-height: 14px;">
                                                                    <%# Eval("updateby")%>
                                                                </p>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <p style="font-size: 13px; line-height: 14px;">
                                                                    <%# Eval("ipaddress")%>
                                                                </p>
                                                            </div>
                                                        </div>
                                                        <hr style="margin-top: 0; margin-bottom: 7px;" />
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:Panel ID="pnlnopassLog" Visible="true" runat="server">
                                                    <div class="row">
                                                        <div class="col-lg-12 text-center mt-4" runat="server">
                                                            <h6 class="card-title text-muted mb-0">No Record Found </h6>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </section>
                                </div>
                            </div>

                        </div>
                    </article>
                </div>
                <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12">
                    <article class="ticket mt-3">
                        <header class="ticket__wrapper">
                            <div class="ticket__header">
                                <div class="col">
                                    <asp:Label ID="Label2" runat="server" Text="Pass Journey Details"></asp:Label>
                                </div>
                            </div>
                        </header>
                        <div class="ticket__body" style="min-height: 150vh;">
                            <asp:Panel ID="pnldrnydtls" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col">
                                        <p class="mb-0">
                                            <span class="pr-1 text-muted" style="font-size: 14px;">Total Traval KM.</span>
                                            <asp:Label ID="lbltotalkm" runat="server" Text="0"></asp:Label>
                                        </p>
                                    </div>
                                    <div class="col">
                                        <p class="mb-0">
                                            <span class="pr-1 text-muted" style="font-size: 14px;">Total Fare</span>
                                            <asp:Label ID="lbltotfare" runat="server" Text="0"></asp:Label>
                                            <i class="fa fa-rupee"></i>
                                        </p>
                                    </div>
                                    <div class="col">
                                        <p class="mb-0">
                                            <span class="pr-1 text-muted" style="font-size: 14px;">Concession</span>
                                            <asp:Label ID="lblconcession" runat="server" Text="0"></asp:Label>
                                            <i class="fa fa-rupee"></i>
                                        </p>
                                    </div>
                                    <%-- <div class="col">
                                    <p class="mb-0">
                                        <span class="pr-1 text-muted" style="font-size: 14px;">Panelty(C)</span>
                                        <asp:Label ID="lblpanelty" runat="server" Text="0"></asp:Label>
                                    </p>
                                </div>
                                  <div class="col">
                                    <p class="mb-0">
                                        <span class="pr-1 text-muted" style="font-size: 14px;">Amount Charged((A-B)+C)</span>
                                        <asp:Label ID="lbltotamtcharged" runat="server" Text="0"></asp:Label>
                                    </p>
                                </div>--%>
                                </div>
                                <hr />
                                <div class="row mb-2">
                                    <div class="col-lg-8">
                                    </div>
                                    <div class="col">
                                        <asp:DropDownList ID="ddlyear" runat="server" OnSelectedIndexChanged="ddlyear_SelectedIndexChanged" AutoPostBack="true" CssClass="form form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col">
                                        <asp:Repeater ID="rptjournydtlsmonth" runat="server">
                                            <ItemTemplate>
                                                <div class="row mb-0">
                                                    <div class="col-md-2">
                                                        <p class="mb-2" style="font-size: 13px; font-weight: bold; margin-top: 3px;">
                                                            <%# Eval("mon_")%>
                                                        </p>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <span class="pr-1 text-muted">Traval Km</span>
                                                        <p style="font-size: 13px; line-height: 14px;">
                                                            <%# Eval("total_km")%>
                                                        </p>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <span class="pr-1 text-muted">Fare</span>
                                                        <p style="font-size: 13px; line-height: 14px;">
                                                            <%# Eval("fare_")%> <i class="fa fa-rupee"></i>
                                                        </p>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <span class="pr-1 text-muted">Concession</span>
                                                        <p style="font-size: 13px; line-height: 14px;">
                                                            <%# Eval("concession_")%> <i class="fa fa-rupee"></i>
                                                        </p>
                                                    </div>
                                                    <%--                                              
                                              
                                                <div class="col-md-2">
                                                    <span class="pr-1 text-muted">Panelty</span> 
                                                    <p style="font-size: 13px; line-height: 14px;">
                                                        <%# Eval("PANELTY")%>
                                                    </p>
                                                </div>
                                                <div class="col-md-3">
                                                    <span class="pr-1 text-muted">Charged Amt.</span> 
                                                    <p style="font-size: 13px; line-height: 14px;">
                                                        <%# Eval("CHARGED")%>
                                                    </p>
                                                </div>--%>
                                                </div>
                                                <hr style="margin-top: 0; margin-bottom: 7px;" />
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlNoRecord" runat="server" Width="100%" Visible="true">
                                <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                    <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
                                        <asp:Label ID="lblmsg" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </article>
                </div>
            </div>
            <div class="row">
                <cc1:ModalPopupExtender ID="mpviewdocment" runat="server" PopupControlID="pnlviewdocment" CancelControlID="btnclose"
                    TargetControlID="Button5" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="pnlviewdocment" runat="server">
                    <div class="card" style="margin-top: 100px;">
                        <div class="card-header">
                            <div class="row">
                                <div class="col-lg-6">
                                    <h4 class="card-title text-left mb-0">View Document
                                    </h4>
                                </div>
                                <div class="col-lg-5  float-end">
                                    </div>
                                <div class="col-lg-1  float-end">
                                    <asp:LinkButton ID="btnclose" runat="server" CssClass="btn btn-danger"> <i class="fa fa-times">Close</i></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body text-left pt-2" style="min-height: 100px;">
                            <embed src="../Pass/ViewDocument.aspx" style="height: 75vh; width: 70vw" />
                            <div style="visibility: hidden;">
                                <asp:Button ID="Button5" runat="server" Text="" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </form>
</body>
</html>

