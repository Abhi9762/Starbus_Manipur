<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Errorpage.aspx.cs" Inherits="Errorpage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="Start your development with a Dashboard for Bootstrap 4.">
    <meta name="author" content="Creative Tim">
    <title>StarBus* APSTS</title>
    <!-- Favicon -->
    <link rel="icon" href="assets/img/brand/favicon.png" type="image/png">
    <!-- Icons -->
    <link rel="stylesheet" href="../assets/vendor/nucleo/css/nucleo.css" type="text/css">
    <link rel="stylesheet" href="../assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" type="text/css">
    <!-- Page plugins -->
    <!-- Argon CSS -->
    <link rel="stylesheet" href="../assets/css/argon.css?v=1.2.0" type="text/css">
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
</head>
<body class="h-100">
    <form id="form1" runat="server">
        <div id="notfound">
            <div class="notfound-bg"></div>
            <div class="notfound">

                <h2 class="opps">oops!</h2>
                <div>
                    <img src="../assets/img/sad.png">
                </div>
                <div class="er-tx">Error - You are here because of an error!!</div>
                <div>
                    <asp:LinkButton ID="lbtnhome" runat="server" CssClass="btn btn-default" OnClick="lbtnhome_Click"> Go Home </asp:LinkButton>
                    <asp:LinkButton ID="lbtnlogin" runat="server" CssClass="btn btn-primary" OnClick="lbtnlogin_Click"> Login Again </asp:LinkButton>
                </div>
                <div class="nic">Powered by National Informatics Centre </div>
            </div>
        </div>
    </form>

</body>
</html>
