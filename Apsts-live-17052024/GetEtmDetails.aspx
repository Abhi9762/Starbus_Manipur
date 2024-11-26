<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetEtmDetails.aspx.cs" Inherits="Auth_GetEtmDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

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


<body>
    <form runat="server">
   <div class="container-fluid">
    <div class="row">
        <div class="col-md-6" style="border-right: 1px solid black; height:500px;">
            <h3 class="mb-0 float-left">
               </h3>
            <div class="card-body">
                <div class="card-body">
                    <h6 class="heading-small  my-0">1. ETM Details (<asp:Label ID="lblETMName" Font-Bold="true" runat="server" Text=""></asp:Label>)</h6>
                    <div class="pl-lg-4">
                        <div class="row">
                            <div class="col-lg-3">
                                <asp:Label runat="server" CssClass="text-muted form-control-label"> Make Model</asp:Label><br />
                                <asp:Label runat="server" ID="lblmakemodel" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-lg-3">
                                <asp:Label runat="server" CssClass="text-muted form-control-label"> Purchase Mode</asp:Label><br />
                                <asp:Label runat="server" ID="lblPurchaseMode" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-lg-3">
                                <asp:Label runat="server" CssClass="text-muted form-control-label"> IMEI No.1</asp:Label><br />
                                <asp:Label runat="server" ID="lblIMEI1" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-lg-3">
                                <asp:Label runat="server" CssClass="text-muted form-control-label">IMEI No.2</asp:Label><br />
                                <asp:Label runat="server" ID="lblIMEI2" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <h6 class="heading-small  my-0 mt-2">2. Invoice Details</h6>
                    <div class="pl-lg-4">
                        <div class="row">
                            <div class="col-lg-3">
                                <asp:Label runat="server" CssClass="text-muted form-control-label">Agency</asp:Label><br />
                                <asp:Label runat="server" ID="lblAgency" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-lg-3">
                                <asp:Label runat="server" CssClass="text-muted form-control-label">Invoice No.</asp:Label><br />
                                <asp:Label runat="server" ID="lblInvoiceNo" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-lg-3">
                                <asp:Label runat="server" CssClass="text-muted form-control-label"> Invoice Date</asp:Label><br />
                                <asp:Label runat="server" ID="lblInvoiceDate" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-lg-3">
                                <asp:Label runat="server" CssClass="text-muted form-control-label"> Amount (<i class="fa fa-rupee-sign"></i>)</asp:Label><br />
                                <asp:Label runat="server" ID="lblAmount" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-lg-12">
                                <asp:Label runat="server" CssClass="text-muted form-control-label"> Uploaded Invoice</asp:Label><br />
                                <asp:Label runat="server" ID="lblUploadedInvoice" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                            </div>


                        </div>

                    </div>

                    <h6 class="heading-small  my-0 mt-2">3. Receiving Details</h6>
                    <div class="pl-lg-4">
                        <div class="row">
                            <div class="col-lg-3">
                                <asp:Label runat="server" CssClass="text-muted form-control-label">Received At Store</asp:Label><br />
                                <asp:Label runat="server" ID="lblRecStore" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-lg-3">
                                <asp:Label runat="server" CssClass="text-muted form-control-label">Receive By</asp:Label><br />
                                <asp:Label runat="server" ID="lblReceivedBy" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-lg-3">
                                <asp:Label runat="server" CssClass="text-muted form-control-label"> Receive On</asp:Label><br />
                                <asp:Label runat="server" ID="lblReceiveOn" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                            </div>

                        </div>

                    </div>

                    <hr />
                                          <h3 class="heading-small  my-0 mt-2">1. Issuance Details</h3>
                                <div class="pl-lg-4">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <asp:Label runat="server" CssClass="text-muted form-control-label">Issued to ETM Branch/Store</asp:Label><br />
                                            <asp:Label runat="server" ID="lblissuestore" Text="N/A" CssClass="form-control-label" Font-Bold="true"></asp:Label>
                                        </div>
                                        

                                    </div>

                                </div>
                </div>
            </div>

          
        </div>

        <div class="col-md-6">
                
  
            <h3>Log Details</h3>
            
        </div>

        
    </div>
       </div>
</form>
</body>
</html>
