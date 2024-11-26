<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changedpasswordsuccess.aspx.cs" Inherits="Auth_changedpasswordsuccess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="Start your development with a Dashboard for Bootstrap 4.">
    <meta name="author" content="Creative Tim">
    <title>StarBus* | APSTS ver 4.0</title>
    <!-- Favicon -->
    <link rel="icon" href="../assets/img/brand/favicon.png" type="image/png">
    <!-- Icons -->
    <link rel="stylesheet" href="../assets/vendor/nucleo/css/nucleo.css" type="text/css">
    <link rel="stylesheet" href="../assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" type="text/css">
    <!-- Page plugins -->
    <!-- Argon CSS -->
    <link rel="stylesheet" href="../assets/css/argon.css?v=1.2.0" type="text/css">
     <script src="../js/sha1.js"></script>
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <style>
    </style>
    <style>
        .center1 {
            margin-top: 8%;
            width: 100%;
            padding: 20px;
        }
    </style>
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
</head>
<body style="background-color: darkblue;">
    
    <form id="form1" runat="server">
        
    <div id="container-fluid" style="width: 99%;">
        <div class="row">
            <div class="col-2"></div>
            <div class="col-8">
                <div class="card shadow center1">
                    <div class="row mb-1">
                        <div class="col-md-8 text-center">
                            <table>
                                <tr>
                                    <td>
                                        <a href="../home.aspx">
                                            <asp:Image runat="server" ID="ImgDepartmentLogo" Height="50px" ImageUrl="" /></a>
                                    </td>
                                    <td style="padding-left: 5px;">
                                        <a href="../home.aspx">
                                            <asp:Label runat="server" ID="lblDeptName1" CssClass="deptName" Text="" Font-Size="18px"> </asp:Label>
                                            <br />

                                            <span style="font-size: 15px; float: left;" class="text-left">Version
                                        <asp:Label runat="server" ID="lblversion" Text=""></asp:Label></span></a>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="row mt-2">
                        <div class="col-lg-12 text-center">
                            Your Password Successfull Changed<br />
                            <i class="fa fa-check-square"></i>
                            <br />
                            <span class="login100-form-title" style="padding-bottom: 5px;">Kindly Login Again On
                                                                New Passsword </span><span style="font-size: 15px;">In case of any Query/Help Please
                                                                    Feel Free to Contact <a href="../HelpDesk.aspx" style="text-decoration: none; color: Red;">Help Desk</a></span>
                        </div>
                    </div>
                     <div class="row">
                                                    <div class="col-lg-12 text-center">
                                                         <a href="../Login.aspx" class="btn btn-success" style="width: 50%; height: 100%; padding: 14px;
                                                                text-align: center;">Click here Login </a>
                                                    </div>
                                                </div>
                    <div class="row" style="width: 100%;">
                        <div style="width: 100%; border-top: 1px solid #0169b7; font-size: 9pt; margin-top: 10px; padding-top: 5px; text-align: center;">

                            <strong>Contents Published ,Managed and Maintained </strong>
                            <asp:Label runat="server" ID="lbldeptname" Text=""></asp:Label>
                            <br />
                            <strong>Design & Developed by </strong>
                            <asp:Label runat="server" ID="lbldevelopedby" Text=""></asp:Label>


                        </div>
                    </div>
                </div>
            </div>
            <div class="col-2"></div>
        </div>
    </div>

    </form>
</body>
</html>
