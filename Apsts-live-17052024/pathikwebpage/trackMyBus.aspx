<%@ Page Language="C#" AutoEventWireup="true" CodeFile="trackMyBus.aspx.cs" Inherits="pathikwebpage_trackMyBus" %>

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
    <link href="../assets/vendor/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12">
                    <div id="div1" runat="server">
                    </div>

                </div>
                <div class="col-lg-12 text-center">
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />

                    <asp:Label ID="lblgpsyn" runat="server" Text="Sorry, Bus Details Not Available"
                        Style="color: red; font-size: 12pt;" Visible="false"></asp:Label>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
