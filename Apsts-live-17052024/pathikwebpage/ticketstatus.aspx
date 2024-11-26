<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ticketstatus.aspx.cs" Inherits="pathikwebpage_ticketstatus" %>

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
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="card" style="padding: 20px; margin-top: 20px; box-shadow: 0 2px 20px -2px rgb(0 0 0 / 10%);">
                        <div style="width: 100%; text-align: center;">
                            <i class="fa fa-ticket text-primary" style="font-size: 150px;"></i>
                        </div>
                        <div class="card-body" style="text-align: center;">
                            <hr />
                            <h3>
                                <asp:Label ID="lbltktno" runat="server" Text="" Style="font-weight: bold;"></asp:Label>
                                <span style="font-size: 20px; color: gray; padding-right: 5px;">&nbsp;Ticket No.</span>
                            </h3>
                            <h4>Thanks for using Our Online Booking Service.
                            </h4>
                            <h4>Your Transaction has been completed successfully
                            </h4>
                            <p>Please check ticket history</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
