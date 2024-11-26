<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tripChartGenerateSuccess.aspx.cs" Inherits="Auth_tripChartGenerateSuccess" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="Start your development with a Dashboard for Bootstrap 4.">
    <meta name="author" content="Creative Tim">
    <title>OIOB-StarBus ver 4.0</title>
    <link rel="stylesheet" href="../assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" type="text/css">
    <link rel="stylesheet" href="../assets/css/argon.css?v=1.2.0" type="text/css">
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="page page-center">
            <div class="container-fluid py-4 bg-white">
                <div class="empty text-center">
                    <div class="empty-header" style="font-size: 30px">Trip Chart Generated Successfully.</div>
                    <asp:LinkButton runat="server" CssClass="btn btn-success btn-sm" OnClick="Unnamed_Click"><i class="fa fa-download"> Download Trip Chart</i></asp:LinkButton>
                    <p style="font-size: 20px">Please Close This Window For Further Actions.</p>

                </div>
            </div>
        </div>
        <div class="row">
            <cc1:modalpopupextender id="mpTripchart" runat="server" popupcontrolid="pnlticket"
                cancelcontrolid="lbtnclose" targetcontrolid="Button8" backgroundcssclass="modalBackground">
            </cc1:modalpopupextender>
            <asp:Panel ID="pnlticket" runat="server" Style="position: fixed;">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-lg-6">
                                <h5 class="card-title text-left mb-0">Trip Chart
                                </h5>
                            </div>
                            <div class="col-lg-6">
                                <asp:LinkButton ID="lbtnclose" runat="server" CssClass="text-danger float-right"> <i class="fa fa-times"></i> </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body text-left pt-2" style="overflow: auto;">
                        <embed id="tkt" runat="server" src="" style="height: 60vh; width: 85vw;" />
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button8" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
