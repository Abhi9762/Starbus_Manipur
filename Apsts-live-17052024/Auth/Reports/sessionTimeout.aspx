<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sessionTimeout.aspx.cs" Inherits="sessionTimeout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="Start your development with a Dashboard for Bootstrap 4.">
    <meta name="author" content="Creative Tim">
    <title>StarBus* APSTS</title>
    <!-- Favicon -->
 <link rel="icon" href="../../Logo/Favicon.png" type="image/png" />
   
    <!-- Fonts -->
    <!-- Icons -->
    <link rel="stylesheet" href="../../assets/vendor/nucleo/css/nucleo.css" type="text/css">
    <link rel="stylesheet" href="../../assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" type="text/css">
    <!-- Page plugins -->
    <!-- Argon CSS -->
    <link rel="stylesheet" href="../../assets/css/argon.css?v=1.2.0" type="text/css">
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
</head>
<body class="h-100">
    <form id="form2" runat="server">
        <div id="notfound">
            <div class="notfound-bg"></div>
            <div class="notfound">

                <h2 class="opps">Timeout</h2>
                <div>
                    <i class="ni ni-time-alarm ni-5x"></i>
                </div>
                <div class="er-tx">Unfortunately Your Session has expired!!</div>

                <div>
                    <asp:LinkButton ID="lbtnHome" runat="server" CssClass="btn btn-primary" OnClick="lbtnhome_Click"> Home </asp:LinkButton>

                </div>
                <div class="nic">Powered by National Informatics Centre </div>
            </div>
        </div>
    </form>
</body>
</html>

