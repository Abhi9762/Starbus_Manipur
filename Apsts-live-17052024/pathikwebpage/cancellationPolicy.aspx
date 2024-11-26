<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cancellationPolicy.aspx.cs" Inherits="pathikwebpage_cancellationPolicy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
     <link rel="stylesheet" href="../assets/vendor/nucleo/css/nucleo.css" type="text/css" />
    <link rel="stylesheet" href="../assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" type="text/css" />
    <!-- Page plugins -->
    <!-- Argon CSS -->
    <link rel="stylesheet" href="../assets/css/argon.css?v=1.2.0" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="row">
                <div class="col-12 text-center mt-2">
                     <h1>Cancellation Policy</h1>
                </div>
            </div>
           
            <ul class="list-group" style="font-size: 13px; font-family: verdana; color: #b10021;">
                <li class="list-group-item">Cancellation/Refund/Rescheduling Ticket booked through Online,
                                    refund will be done to their respective Credit Cards/Debit Cards/Bank Accounts according
                                    to the Bank procedure. No refund will be done at UTC ticket booking counters
                                    
                </li>
                <li class="list-group-item">
                    <asp:Label ID="lblcancellationpolicy" runat="server"></asp:Label>
                </li>
                <li class="list-group-item">Cancellation is not allowed after Up to 2 hr before Schedule
                                    service start time at origin of the bus. From the date of journey.</li>
                <li class="list-group-item">Reservation Fee is non-refundable except in case of 100%
                                    cancellation of tickets, if the service is cancelled by UTC for operational or any
                                    other reasons.</li>
                <li class="list-group-item">Passengers will be given normally in one month, after the
                                    cancellation of ticket or receipt of e-mail. If refunds are delayed more than a
                                    month, passengers may contact helpline telephone number at 8476007605 E-Mail help[dot]UTConline[at]gmail[dot]com.</li>
                <li class="list-group-item">Payment Gateway Service charges will not be refunded for
                                    service cancellation/ failure transactions in e-ticketing.</li>
                <li class="list-group-item">Partial cancellation is allowed for which cancellation terms
                                    & conditions will apply.</li>
            </ul>
        </div>
    </form>
</body>
</html>
