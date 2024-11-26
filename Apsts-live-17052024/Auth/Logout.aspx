<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logout.aspx.cs" Inherits="Logout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="Start your development with a Dashboard for Bootstrap 4.">
    <meta name="author" content="Creative Tim">
    <title>APSTS</title>
    <link rel="stylesheet" href="../assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" type="text/css">
    <link rel="stylesheet" href="../assets/css/argon.css?v=1.2.0" type="text/css">
<link rel="icon" href="../Logo/Favicon.png" type="image/png" />
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
            <div class="notfound1">
                <div class="row">
                    <div class="col-lg-12 text-left">

                        <center>
                             <img src="../Logo/DeptLogo.png" style="width: 20%;">
                        </center>
                       
                    </div>
                 
                </div>

                <div class="thank">Thanks for using our services </div>
                <div>
                    <img src="../assets/img/namste.png">
                </div>
              

                <div>
                    <asp:LinkButton ID="lbtnvisitagain" runat="server" CssClass="btn btn-default" OnClick="lbtnvisitagain_Click"> Please Visit us Again </asp:LinkButton>

                </div>
                <div class="nic">Powered by National Informatics Centre </div>
            </div>
        </div>
    </form>
</body>
</html>
