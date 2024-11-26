<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dashTicket.aspx.cs" Inherits="Auth_dashTicket" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="icon" href="../assets/img/brand/favicon.png" type="image/png" />

    <!-- Icons -->
    <link rel="stylesheet" href="../assets/vendor/nucleo/css/nucleo.css" type="text/css" />
    <link rel="stylesheet" href="../assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" type="text/css" />
    <!-- Page plugins -->
    <!-- Argon CSS -->
    <link rel="stylesheet" href="../assets/css/argon.css?v=1.2.0" type="text/css" />
    <link href="../assets/vendor/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />
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
            padding: 1.25rem;
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
            position: relative;
            height: 1rem;
            background-color: #fff;
            margin-left: 0.5rem;
            margin-right: 0.5rem;
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
                padding-top: 1rem;
                border-top: 1px solid #e9ebed;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid pb-5">
            <div class="row">
                <div class="col-xl-4 col-lg-4 col-md-6 col-sm-12">
                    <article class="ticket mt-3">
                        <header class="ticket__wrapper">
                            <div class="ticket__header">
                                <asp:Label ID="lblTicketNo" runat="server" Text="1F2202209120001"></asp:Label><asp:Label ID="lblTicketStatus" runat="server" Text="(Confirmed)"></asp:Label>
                            </div>
                        </header>
                        <div class="ticket__divider">
                            <div class="ticket__notch"></div>
                            <div class="ticket__notch ticket__notch--right"></div>
                        </div>
                        <div class="ticket__body">
                            <section class="ticket__section">
                                <div class="row">
                                    <div class="col">
                                        <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Journey</span><asp:Label ID="lblJourneyDT" runat="server" Text="NA"></asp:Label></p>
                                    </div>

                                    <div class="col-auto">
                                        <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Booking</span><asp:Label ID="lblBookingDT" runat="server" Text="NA"></asp:Label></p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col">
                                        <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">From</span><asp:Label ID="lblFrom" runat="server" Text="NA"></asp:Label></p>
                                    </div>
                                    <div class="col-auto">
                                        <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">To</span><asp:Label ID="lblTo" runat="server" Text="NA"></asp:Label></p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col">
                                        <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Boarding</span><asp:Label ID="lblBoarding" runat="server" Text="NA"></asp:Label></p>
                                    </div>
                                </div>
                            </section>
                            <section class="ticket__section">
                                <div class="row">
                                    <div class="col">
                                        <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Bus</span><asp:Label ID="lblBus" runat="server" Text="NA"></asp:Label></p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col">
                                        <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Service</span><asp:Label ID="lblService" runat="server" Text="NA"></asp:Label></p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col">
                                        <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Booking Mode</span><asp:Label ID="lblBookingMode" runat="server" Text="NA"></asp:Label></p>
                                    </div>
                                </div>
                            </section>
                            <section class="ticket__section">
                                <div class="row">
                                    <div class="col">
                                        <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">User Id</span><asp:Label ID="lblUserId" runat="server" Text="NA"></asp:Label></p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col">
                                        <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Mobile No.</span><asp:Label ID="lblMobileNo" runat="server" Text="NA"></asp:Label></p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col">
                                        <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Email</span><asp:Label ID="lblEmail" runat="server" Text="NA"></asp:Label></p>
                                    </div>
                                </div>
                            </section>
                            <section class="ticket__section">
                                <h3>Payment</h3>
                                <div class="row">
                                    <div class="col">
                                        <p class="mb-0">
                                            <span class="pr-1 text-muted" style="font-size: 14px;">Fare</span><asp:Label ID="lblAmountFare" runat="server" Text="NA"></asp:Label>
                                            <i class="fa fa-rupee"></i>
                                        </p>
                                    </div>
                                    <div class="col-auto">
                                        <p class="mb-0">
                                            <span class="pr-1 text-muted" style="font-size: 14px;">Reservation</span><asp:Label ID="lblAmountReservation" runat="server" Text="NA"></asp:Label>
                                            <i class="fa fa-rupee"></i>
                                        </p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col">
                                        <p class="mb-0">
                                            <span class="pr-1 text-muted" style="font-size: 14px;">Discount</span><asp:Label ID="lblAmountDiscount" runat="server" Text="NA"></asp:Label>
                                            <i class="fa fa-rupee"></i>
                                        </p>
                                    </div>
                                    <div class="col-auto">
                                        <p class="mb-0">
                                            <span class="pr-1 text-muted" style="font-size: 14px;">Concession</span><asp:Label ID="lblAmountConssion" runat="server" Text="NA"></asp:Label>
                                            <i class="fa fa-rupee"></i>
                                        </p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col">
                                        <p class="mb-0">
                                            <span class="pr-1 text-muted" style="font-size: 14px;">Commission</span><asp:Label ID="lblAmountCommission" runat="server" Text="NA"></asp:Label>
                                            <i class="fa fa-rupee"></i>
                                        </p>
                                    </div>
                                    <div class="col-auto">
                                        <p class="mb-0">
                                            <span class="pr-1 text-muted" style="font-size: 14px;">Tax</span><asp:Label ID="lblAmountTax" runat="server" Text="NA"></asp:Label>
                                            <i class="fa fa-rupee"></i>
                                        </p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col">
                                        <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Payment Mode</span><asp:Label ID="lblPaymentMode" runat="server" Text="NA"></asp:Label></p>
                                    </div>
                                </div>
                            </section>
                        </div>
                        <footer class="ticket__footer">
                            <span>Total Paid</span>
                            <span>
                                <asp:Label ID="lblAmountTotal" runat="server" Text="NA"></asp:Label>
                                <i class="fa fa-rupee"></i></span>
                        </footer>
                    </article>
                </div>
                <div class="col-xl-4 col-lg-4 col-md-6 col-sm-12">
                    <div class="card border-0 mt-3">
                        <div class="card-body p-3 ticket__section">
                            <h3>Passangers</h3>

                            <asp:GridView ID="gvPassangers" runat="server" AutoGenerateColumns="false" CssClass="w-100" GridLines="None" ShowHeader="false">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div class="row">
                                                <div class="col">
                                                    <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Seat No. <%# Eval("seat_no") %></span><%# Eval("traveller_name") %>, <%# Eval("traveller_gender") %>, <%# Eval("traveller_age") %>Y<span class="float-right"><%# Eval("current_status") %></span> </p>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xl">
                                                    <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Concession</span><%# Eval("concession_name") %></p>
                                                </div>
                                            </div>
                                            <hr class="mt-2 mb-2" style="border-top: 1px solid rgb(205 198 198 / 10%);" />

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Label ID="lblNoPassanger" runat="server" Text="No passanger available"></asp:Label>

                        </div>
                    </div>
                    <div class="card border-0 mt-3">
                        <div class="card-body p-3 ticket__section">
                            <h3>Transaction Log</h3>
                            <asp:GridView ID="gvTicketTransactions" runat="server" AutoGenerateColumns="false" CssClass="w-100" GridLines="None" ShowHeader="false">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div class="row">
                                                <div class="col">
                                                    <p class="mb-0"><%# Eval("ticket_status_name") %></p>
                                                </div>
                                                <div class="col-auto">
                                                    <p class="mb-0"><%# Eval("current_status_datetime") %></p>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Label ID="lblNoTransaction" runat="server" Text="No transaction available"></asp:Label>

                        </div>
                    </div>
                    <div class="card border-0 mt-3">
                        <div class="card-body p-3 ticket__section">
                            <h3>Cancellation</h3>
                            <asp:GridView ID="gvCancellation" runat="server" AutoGenerateColumns="false" CssClass="w-100" GridLines="None" ShowHeader="false">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div class="row">
                                                <div class="col-xl">
                                                    <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Cancellaction Ref No.</span><%# Eval("cancellation_ref_no") %></p>
                                                </div>
                                                <div class="col-xl-auto">
                                                    <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Refund</span><%# Eval("cancellation_amt") %> <i class="fa fa-rupee"></i></p>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xl">
                                                    <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Cancellaction Date</span><%# Eval("cancellation_date") %></p>
                                                </div>
                                                <div class="col-xl-auto">
                                                    <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Refund Ref No</span><%# Eval("refund_ref_no") %></p>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xl">
                                                    <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Cancelled By</span><%# Eval("cancellation_by_type") %>(<%# Eval("cancelled_by_refno") %>)</p>
                                                </div>
                                                <div class="col-xl-auto">
                                                    <asp:LinkButton ID="lbtnView" runat="server" Visible="false" CssClass="btn btn-primary btn-sm" Style="padding: 2px 12px;"><i class="fa fa-eye"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                            <hr class="mt-2 mb-2" style="border-top: 1px solid rgb(205 198 198 / 10%);" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Label ID="lblNoCancellation" runat="server" Text="No cancellation available"></asp:Label>
                        </div>
                    </div>

                </div>
                <div class="col-xl-4 col-lg-4 col-md-12 col-sm-12">
                    <div class="card border-0 mt-3">
                        <div class="card-body p-3 ticket__section">
                            <h3>Journey Details</h3>
                            <div class="row">
                                <div class="col">
                                    <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Waybill</span><asp:Label ID="Label1" runat="server" Text="NA"></asp:Label></p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Driver</span><asp:Label ID="Label2" runat="server" Text="NA"></asp:Label></p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Conductor</span><asp:Label ID="Label3" runat="server" Text="NA"></asp:Label></p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card border-0 mt-3">
                        <div class="card-body p-3 ticket__section">
                            <h3>Alarms (raised by traveller during journey)</h3>
                            <asp:GridView ID="gvAlarms" runat="server" AutoGenerateColumns="false" CssClass="w-100" GridLines="None" ShowHeader="false">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div class="row">
                                                <div class="col-sm">
                                                    <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Alarm</span><%# Eval("alarmtype") %></p>
                                                </div>
                                                <div class="col-sm-auto">
                                                    <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Alarm at</span><%# Eval("reportdatetime") %></p>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm">
                                                    <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Status</span><%# Eval("status") %></p>
                                                </div>
                                                <div class="col-sm-auto">
                                                    <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Last Update</span><%# Eval("lastupdatedatetime") %></p>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm">
                                                    <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Remark</span><%# Eval("remark") %></p>
                                                </div>
                                                <div class="col-sm-auto">
                                                    <asp:LinkButton ID="lbtnView" runat="server" Visible ="false"  CssClass="btn btn-primary btn-sm" Style="padding: 2px 12px;"><i class="fa fa-eye"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                            <hr class="mt-2 mb-2" style="border-top: 1px solid rgb(205 198 198 / 10%);" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Label ID="lblNoAlarm" runat="server" Text="No alarm available"></asp:Label>
                        </div>
                    </div>
                    <div class="card border-0 mt-3">
                        <div class="card-body p-3 ticket__section">
                            <h3>Grievances (reported during journey)</h3>
                            <asp:GridView ID="gvGrievance" runat="server" AutoGenerateColumns="false" CssClass="w-100" GridLines="None" ShowHeader="false">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div class="row">
                                                <div class="col-sm">
                                                    <p class="mb-0"><%# Eval("categoryname") %> - <%# Eval("subcategoryname") %> </p>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm">
                                                    <p class="mb-0"><span class="pr-1 text-muted" style="font-size: 14px;">Date & Time</span><%# Eval("gdatetime") %></p>
                                                </div>
                                                <div class="col-sm-auto">
                                                    <asp:LinkButton ID="lbtnView" runat="server" Visible ="false" CssClass="btn btn-primary btn-sm" Style="padding: 2px 12px;"><i class="fa fa-eye"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                            <hr class="mt-2 mb-2" style="border-top: 1px solid rgb(205 198 198 / 10%);" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Label ID="lblNoGrievance" runat="server" Text="No grievance available"></asp:Label>
                        </div>
                    </div>
                    <div class="card border-0 mt-3" style="display: none">
                        <div class="card-body p-3 ticket__section">
                            <h3 class="w-100 text-left">Actions</h3>
                            <asp:LinkButton ID="lbtnPrint" runat="server" CssClass="btn btn-success mt-3" Style="text-transform: none;"><i class="fa fa-print"></i> Print</asp:LinkButton>
                            <asp:LinkButton ID="lbtnSMS" runat="server" CssClass="btn btn-primary mt-3" Style="text-transform: none;"><i class="fa fa-send"></i> Resend SMS</asp:LinkButton>
                            <asp:LinkButton ID="lbtnEmail" runat="server" CssClass="btn btn-info mt-3" Style="text-transform: none;"><i class="fa fa-envelope"></i> Resend Email</asp:LinkButton>
                            <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="btn btn-warning mt-3" Style="text-transform: none;"><i class="fa fa-times"></i> Cancel</asp:LinkButton>
                            <asp:LinkButton ID="lbtnSpecialCancel" runat="server" CssClass="btn btn-danger mt-3" Style="text-transform: none;"><i class="fa fa-times-circle"></i> Special Cancel</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
