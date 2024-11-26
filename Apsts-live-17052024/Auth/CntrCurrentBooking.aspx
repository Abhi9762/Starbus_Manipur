<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Cntrmaster.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="CntrCurrentBooking.aspx.cs" Inherits="Auth_CntrCurrentBooking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .ModalPopupBG {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
    </style>
    <script type="text/javascript" src="../assets/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">

        function TicketLoad() {

            //Prevent keypress
            $('#<%= btnPrintTicket.ClientID %>').attr("disabled", "true");

            $('#<%= txtSeatNumbers.ClientID %>').keypress(function () {
                return false;
            });
            $('#<%= txtTotalFare.ClientID %>').keypress(function () {
                return false;
            });
            <%--$('#<%= txtRefundAmt.ClientID %>').keypress(function () {
                return false;
            });--%>
            $('#<%= txtTotalNoOfSeat.ClientID %>').keypress(function () {
                return false;
            });
            $('#<%= txtFarePerSeat.ClientID %>').keypress(function () {
                return false;
            });

            //Key Press event
            <%--$('#<%= txtReceivedAmt.ClientID %>').keyup(function () {
                try {
                    var RefundAmt = 0

                    var ReceivedAmt = 0;
                    var TotalFare = 0;
                    TotalFare = $('#<%= txtTotalFare.ClientID %>').val();
                    ReceivedAmt = $('#<%= txtReceivedAmt.ClientID %>').val();
                    RefundAmt = ReceivedAmt - TotalFare;

                    $('#<%=txtRefundAmt.ClientID %>').val(RefundAmt);
                }
                catch (err) {
                    alert('Err1');
                }
            });--%>


            //Chesk On Proceed Button Click
            $('#<%=btnPrintTicket.ClientID %>').click(function () {

               <%-- if ($('#<%= txtRefundAmt.ClientID %>').val() < 0) {
                    alert('Please check >> Received Amount is lesser than Total Fare Amount.');

                    return false;
                }

                if ($('#<%= txtReceivedAmt.ClientID %>').val() == '') {
                    alert('Please enter Received Amount');
                    return false;
                }--%>

                if ($('#<%= hfDepartureDate.ClientID %>').val() == '') {
                    alert('Please enter Departure Time');
                    return false;
                }
                if ($('#<%= txtFarePerSeat.ClientID %>').val() == '') {
                    alert('Please Select Route');
                    return false;
                }

                if ($('#<%= txtTotalNoOfSeat.ClientID %>').val() == '') {
                    alert('Please Select Seat from Bus layout');
                    return false;
                }

                var intReceivedAmt = 0;
                var intTotFareAmt = 0;
               <%-- intReceivedAmt = $('#<%= txtReceivedAmt.ClientID %>').val()--%>
                intTotFareAmt = $('#<%= txtTotalFare.ClientID %>').val()
            });
            //Populate Onward BusLayout
            var seatDescUp = $('#<%= hfSeatStructUp.ClientID %>');

            getSeatStruct('up', seatDescUp);
            if ($('#abc').val() == 'true')
                return false;
            $('#abc').val('true');

            //*********SET VALUES******
            //$('.selected').live('click', function () {
            //});


            //check click event on not selected seat
            $('.available,.woman,.mlareporter,.selected').click(function () {

                var className = $(this).attr("class");
                if (className == "selected") {
                    $this = $(this);
                    $this.removeClass();

                    var journeyType = $this.children('input[type=hidden]').attr('id').split('-');
                    var seatNoCat = $this.children('input[type=hidden]').val().split('-');
                    //alert(seatNoCat);
                    if (seatNoCat[1] == 'G')
                        $this.addClass('available');
                    if (seatNoCat[1] == 'M') { $this.addClass('woman'); }
                    if (seatNoCat[1] == 'F')
                        $this.addClass('mlareporter');
                    CalculateFare();
                    var rowClass = $this.children('input[type=hidden]').attr('id');

                    $(".tr" + rowClass).fadeOut(200, function () {
                        $('.tr' + rowClass).remove();
                        var selectedSeat = $('#div-seat-table-' + journeyType[0] + ' .selected').length;
                        if (selectedSeat == 0) {
                            $('#ticket-booking-' + journeyType[0] + ' .seat').hide();
                        }
                    });
                    if ($('#<%= txtSeatNumbers.ClientID %>').val() == '') {
                        $('#<%= btnPrintTicket.ClientID %>').attr("disabled", "true");

                    }
                }
                else {


                    var strFare = '';
                    strFare = $('#<%= txtFarePerSeat.ClientID %>').val();
                    var curdate = new Date()
                    var year = curdate.getFullYear();
                    var month = (1 + curdate.getMonth()).toString();
                    month = month.length > 1 ? month : '0' + month;
                    var day = curdate.getDate().toString();
                    day = day.length > 1 ? day : '0' + day;
                    var date = day + '/' + month + '/' + year;

                    if ($('#<%= hfDepartureDate.ClientID %>').val() != date) {
                        //  alert('Invalid Date time')
                        // return false;
                    }

                    //ARUN  19Jan2017
                    var count = $('table[id*="gvRouteStationsList"] input[type="radio"]:checked').length;
                    if (count <= 0) { alert('Please select station'); return false; }
                    //--ARUN

                    var strTotalSeats = '';
                    strTotalSeats = $('#<%=txtTotalNoOfSeat.ClientID %>').val();

                    var PassangerNo = $('#<%= hfNoOfPassanger.ClientID %>').val();
                    // alert(strTotalSeats);
                    //if (strTotalSeats > PassangerNo ) {
                    //alert("Please select maximum " + PassangerNo + "seats at a time");
                    //return false;
                    //}
                    /////////////
                    var strtime = '';
                    strtime = $('#<%= hfDepartureTime.ClientID %>').val();
                    if (strtime.length == 0) {
                        // alert('Invalid Departure Time');
                        // return false;
                    }

                    // var PassangerNo = 1;
                    $this = $(this);
                    var journeyType = $this.children('input[type=hidden]').attr('id').split('-');
                    var selectedSeat = $('#' + journeyType[0] + '-journey .selected').length;


                    // var PassangerNo = 1;
                    //alert(selectedSeat);
                    if (strTotalSeats < PassangerNo) {

                        $this.removeClass();
                        $this.addClass('selected');

                        var rowClass = $this.children('input[type=hidden]').attr('id');
                        var seatNoCat = $this.children('input[type=hidden]').val().split('-');
                        //----Fare Calculation-----

                        CalculateFare();
                        $('#<%= btnPrintTicket.ClientID %>').removeAttr('disabled');
                    }
                    else {
                        alert("Please select maximum " + PassangerNo + "seats at a time");
                        return false;
                    }
                    if ($('#<%= txtSeatNumbers.ClientID %>').val() == '') {
                        $('#<%= btnPrintTicket.ClientID %>').attr("disabled", "true");
                    }
                }
            });

            //check click event on not selected seat
            <%--$('.selected').click(function () {
                $this = $(this);
                $this.removeClass();

                var journeyType = $this.children('input[type=hidden]').attr('id').split('-');
                var seatNoCat = $this.children('input[type=hidden]').val().split('-');
                //alert(seatNoCat);
                if (seatNoCat[1] == 'G')
                    $this.addClass('available');
                if (seatNoCat[1] == 'M') { $this.addClass('woman'); }
                if (seatNoCat[1] == 'F')
                    $this.addClass('mlareporter');
                CalculateFare();
                var rowClass = $this.children('input[type=hidden]').attr('id');

                $(".tr" + rowClass).fadeOut(200, function () {
                    $('.tr' + rowClass).remove();
                    var selectedSeat = $('#div-seat-table-' + journeyType[0] + ' .selected').length;
                    if (selectedSeat == 0) {
                        $('#ticket-booking-' + journeyType[0] + ' .seat').hide();
                    }
                });
                if ($('#<%= txtSeatNumbers.ClientID %>').val() == '') {
                    $('#<%= btnPrintTicket.ClientID %>').attr("disabled", "true");

                }
            });--%>

            function getSeatStruct(journeyType, SeatDesc) {
                var SeatDetail = $(SeatDesc).val().split(',');
                var Rows = 0; var Cols = 0; Fare = 0.0;
                var rowcol = SeatDetail[0].split('-');
                Rows = parseInt(rowcol[0]);
                Cols = parseInt(rowcol[1]);
                Fare = parseFloat(rowcol[2]);
                if (Rows == 4) {
                    $('#seatpanel').css({ 'height': 175 });
                }
                if (Rows == 5) {
                    $('#seatpanel').css({ 'height': 200 });
                }
                if (Rows == 10) {
                    $('#seatpanel').css({ 'height': 420 });
                }
                if (Rows == 3) {
                    $('#seatpanel').css({ 'height': 170 });
                }
                if (Rows == 11) {
                    $('#seatpanel').css({ 'height': 450 });
                }
                if (Cols == 5) {
                    $('#seatpanel').css({ 'width': 250 });
                }
                if (Cols == 6) {
                    $('#seatpanel').css({ 'width': 280 });
                }

                $('body').append('<input type="hidden" id="' + journeyType + 'Fare" value="' + Fare + '" />')
                $('#' + journeyType + '-' + Rows + '-' + (Cols)).remove();

                //INJECT LAST SEAT TO TABLE
                for (var i = 1; i < SeatDetail.length; i++) {

                    var seatRow = SeatDetail[i].split('-');
                    $('#' + journeyType + '-' + seatRow[0] + '-' + seatRow[1]).val(seatRow[2] + '-' + seatRow[4])
                    $('#' + journeyType + '-' + seatRow[0] + '-' + seatRow[1]).parents('a').removeClass();

                    if (seatRow[3] == 'N') {
                        $('#' + journeyType + '-' + seatRow[0] + '-' + seatRow[1]).parents('a').removeClass();
                    }

                    else if (seatRow[4] == 'D') {
                        $('#' + journeyType + '-' + seatRow[0] + '-' + seatRow[1]).parents('a').addClass('driver');
                    }
                    else if (seatRow[4] == 'G') {
                        $('#' + journeyType + '-' + seatRow[0] + '-' + seatRow[1]).parents('a').addClass('available');
                        $('#' + journeyType + '-' + seatRow[0] + '-' + seatRow[1]).parents('a').append('<span>' + seatRow[2] + '</span>');
                    }
                    else if (seatRow[4] == 'M') {
                        $('#' + journeyType + '-' + seatRow[0] + '-' + seatRow[1]).parents('a').addClass('man');
                        $('#' + journeyType + '-' + seatRow[0] + '-' + seatRow[1]).parents('a').append('<span>' + seatRow[2] + '</span>');
                    }
                    else if (seatRow[4] == 'F') {
                        $('#' + journeyType + '-' + seatRow[0] + '-' + seatRow[1]).parents('a').addClass('woman');
                        $('#' + journeyType + '-' + seatRow[0] + '-' + seatRow[1]).parents('a').append('<span>' + seatRow[2] + '</span>');
                    }
                    else if (seatRow[4] == 'C') {
                        $('#' + journeyType + '-' + seatRow[0] + '-' + seatRow[1]).parents('a').addClass('conductor');
                    }
                    if ((seatRow[6] == 'A') || (seatRow[6] == 'R') || (seatRow[6] == 'B')) {
                        $('#' + journeyType + '-' + seatRow[0] + '-' + seatRow[1]).parents('a').removeClass();
                        $('#' + journeyType + '-' + seatRow[0] + '-' + seatRow[1]).parents('a').addClass('booked');
                    }
                    if (seatRow[5] == 'N') {
                        if (seatRow[4] == 'D') {
                            $('#' + journeyType + '-' + seatRow[0] + '-' + seatRow[1]).parents('a').addClass('driver');

                        }
                        else if (seatRow[4] == 'C') {
                            $('#' + journeyType + '-' + seatRow[0] + '-' + seatRow[1]).parents('a').addClass('conductor');
                            $('#' + journeyType + '-' + seatRow[0] + '-' + seatRow[1]).parents('a').append('<span>' + seatRow[2] + '</span>');
                        }
                        else {
                            $('#' + journeyType + '-' + seatRow[0] + '-' + seatRow[1]).parents('a').removeClass();
                            $('#' + journeyType + '-' + seatRow[0] + '-' + seatRow[1]).parents('a').addClass('booked');
                        }
                    }

                }
                $('#seat-desc-' + journeyType + ' tr > td:nth-child(' + Cols + ')').nextAll().remove();
            }

            //********
            function CalculateFare() {
                try {
                    var FarePerTicket = 0;
                    var selectedUpSeat = 0;
                    var TotalFare = 0;

                    FarePerTicket = $('#<%= txtFarePerSeat.ClientID %>').val();
                    selectedUpSeat = $('#div-seat-table-up .selected').length;
                    FromStation = $('#<%=lblFromStation.ClientID %>').val();
                    toStation = $('#<%=hdnToStation.ClientID %>').val();
                    TotalFare = FarePerTicket * selectedUpSeat;

                    //if (TotalFare != 0 && selectedUpSeat != null) {
                    //    CallDisplayMethod(TotalFare, selectedUpSeat, toStation);
                    //}
                    //else {
                    //    CallDisplayMethod1();
                    //}

                    $('#<%=txtTotalFare.ClientID %>').val(TotalFare);
                    $('#<%=txtTotalNoOfSeat.ClientID %>').val(selectedUpSeat);

                    //Set Seat number             
                    var customerData = "";
                    $('.selected').each(function () {
                        $this = $(this);
                        customerData = customerData + ',' + $this.children('span').text();
                    });

                    $('#<%=txtSeatNumbers.ClientID %>').val(customerData.substr(1, customerData.length - 1));
                    <%--$('#<%= txtReceivedAmt.ClientID %>').val('');
                    $('#<%= txtRefundAmt.ClientID %>').val('');--%>
                }
                catch (err) {
                    alert(err);
                }
            }
        }
    </script>

    <style type="text/css">
        .gvcls {
            margin-top: 12px;
        }

        .spansheet {
            width: 15px;
            height: 18px;
            border: 1px solid #dbeff9;
            background-color: aliceblue;
            padding: 8px; /* border-radius: 50%; */
        }

        .spansheetupr {
            height: 10px;
            width: 10px;
            border: 1px solid #dbeff9;
            background-color: aliceblue;
            padding: 8px;
        }

        .ul-seat-Upr span {
            display: block;
            float: left;
            padding-top: 3px;
            font-size: 11px;
        }


        #mask {
            position: absolute;
            left: 0;
            top: 0;
            z-index: 90000000;
            background-color: #000;
            display: none;
        }

        #box {
            width: 275px;
            margin: 10px auto;
            padding: 20px;
            border: 1px double #00d8ff;
            background: #e9fcfe;
        }

        .td-first {
            width: 350px;
            text-align: left;
            padding-left: 5px;
        }

        .td-second {
            width: 205px;
            text-align: left;
        }

        .td-third {
            width: 145px;
            text-align: left;
            padding-left: 5px;
        }

        h1 {
            text-align: center;
            border-bottom: 1px solid #0094ff;
            padding-bottom: 5px 0;
            width: 95%;
            margin: 2px auto;
        }


        #tbl-from-detail-up td, #tbl-from-detail-down td {
            height: 20px;
        }

        input[type=text], select {
            border: 1px solid #000;
        }

        .bus-desc {
            height: 250px;
            width: 100%;
        }

        #seat-desc-up, #seat-desc-down {
            margin-left: 8px;
            border-collapse: collapse;
        }

            #seat-desc-up td, #seat-desc-down td {
                width: 31px;
                height: 31px;
                padding: 2px;
            }

                #seat-desc-up td a, #seat-desc-down td a {
                    width: 31px;
                    height: 31px;
                    display: block;
                }

        /*.available, .woman, .man, .booked, .selected, .sleeper
        {
            background-image: url('img/seats/NewSeat1.png');
            cursor: pointer;
        }*/
        .available {
            background-image: url('../images/seats/seatwhite.png');
            cursor: pointer; /* background-position: -84px 0;*/
            padding: 5px;
        }
        /* .available:hover{ background-position: -84px 0; }*/
        .woman {
            background-image: url('../images/seats/seatpink.png');
            cursor: pointer; /*  background-position: -28px 0;*/
            padding: 5px;
        }

        .man {
            background-image: url('../images/seats/seatMale.png');
            cursor: pointer; /*background-position: -56px 0;*/
            padding: 5px;
        }

        .sleeper {
            background-image: url('../images/seats/seatsleep.png');
            cursor: pointer; /* background-position: -140px 0;*/
            padding: 5px;
        }

        .booked {
            background-image: url('../images/seats/seatgray.png'); /* background-position: -112px 0;*/
            cursor: default;
            padding: 5px;
        }

        .selected {
            background-image: url('../images/seats/seatblue.png');
            cursor: pointer; /* background-position: 0 0;*/
            padding: 5px;
        }

        .driver {
            background-image: url('../images/seats/seatDriver.png'); /* background-position: -112px 0;*/
            cursor: default;
            padding: 5px;
        }

        .conductor {
            background-image: url('../images/seats/seatConductor.png'); /* background-position: -112px 0;*/
            cursor: default;
            padding: 5px;
        }

        input[type=text], select {
            border: 1px solid #CCC;
        }

        .ticket-tbl-first-row {
            font-size: 9px;
            text-transform: uppercase;
            font-weight: bold;
        }

            .ticket-tbl-first-row span {
                display: block;
                width: 48px;
            }

        #btnProceed.hover {
            background-color: Green;
        }

        /* #div-seat-table-up, #div-seat-table-down
        {
            width: 466px;
            height: 174px;
            border: 0px solid #565656;
            text-align: center;
            margin: 10px 6px 5px 120px;
            padding-top: 3px;
            padding-bottom: 10px;
            background: url('images/bus-layout.png') 0 0; 
            position: relative;
        } */
        #div-seat-table-up {
            margin-left: -40dp;
        }

        .av, .bo, .wo, .mr, .res, .seltd {
            background-image: url('img/seats/ticket-seat-small.png');
            margin-right: 5px;
            height: 18px;
            width: 22px;
        }

        .av {
            background-position: -66px 0;
        }

        .wo {
            background-position: -22px 0;
        }

        .mr {
            background-position: -44px 0;
        }

        .bo {
            background-position: -110px 0;
        }

        .res {
            background-position: -88px 0;
        }

        .seltd {
            background-position: 0 0;
        }

        .ul-seat-desc {
            text-align: center;
            background: white;
            list-style: none;
            padding: 0px 4px 0px 0px;
            overflow: hidden;
            -webkit-border-radius: 10px;
            -moz-border-radius: 10px;
        }

            .ul-seat-desc li {
                background: #fff;
                height: 23px;
                list-style: none;
                float: left;
                font-size: 10px;
                padding: 4px 4px 0px 4px;
                margin: 2px;
                border: 0px solid #4bd5f1;
            }

            .ul-seat-desc span {
                display: block;
                float: left;
                padding-top: 3px;
                font-size: 11px;
            }

        /* #ticket-booking-up, #ticket-booking-down
        {
            width: 460px;
            overflow: hidden;
            margin: 5px 9px 5px 120px;
            background: #F8FAB9;
        }*/
        #ticket-booking-up {
            margin-top: 45px;
        }

            #ticket-booking-up select, #ticket-booking-down select, .seat-entry input[type=text] {
                margin: 0 2px;
                font-size: 10px;
                text-transform: uppercase;
            }

        .seat-entry input[type=text] {
            padding: 1px 0;
            margin: 0;
        }

        .opt-cat {
            width: 70px;
        }

        .opt-gender {
            width: 70px;
        }

        .seat {
            margin: 0;
            display: none;
            overflow: hidden;
        }

        .ticket-tbl-first-row {
            font-size: 10px;
            text-transform: uppercase;
            font-weight: bold;
            color: #0084d9;
        }

        .text-right {
            padding-right: 5px;
            text-align: right;
        }

        .header-text-up, .header-text-down {
            border-style: none;
            border-color: inherit;
            border-width: 0;
            height: 31px;
            padding-left: 0px;
            margin-top: 10px;
            width: 650px;
        }

        #heading-left-up, #heading-mid-up, #heading-right-up {
            /* background: url('img/seats/rj-bg.png');*/
            display: block;
        }

        #heading-left-down, #heading-mid-down, #heading-right-down {
            background: url('img/seats/oj-bg.png');
            display: block;
        }

        #heading-left-up, #heading-left-down {
            width: 15px;
            height: 30px;
            float: left;
        }

        #heading-mid-up, #heading-mid-down {
            height: 18px;
            float: left;
            background-position: 0 -30px;
            overflow: hidden;
            padding: 5px 20px 7px 20px;
            background-repeat: repeat-x;
            font-size: 15px;
            color: #fff;
            font-weight: bold;
        }

        #heading-right-up, #heading-right-down {
            width: 15px;
            height: 30px;
            float: left;
            background-position: 0 -60px;
        }

        #Name {
            height: 50px;
        }

        #Age {
            height: 50px;
            width: 50px;
        }

        .tdgen {
        }


        #div-submit-detail table {
            border-collapse: collapse;
        }

            #div-submit-detail table td {
                border-collapse: collapse;
                padding: 5px;
            }

        input[type=button], input[type=submit] {
            border: none;
            width: 200px;
            height: 25px;
            font-size: 10px;
            text-transform: uppercase;
            padding: 0;
            color: #FFF;
            font-weight: bold;
        }

            input[type=button]:hover, input[type=submit]:active {
                background-position: -200px 0;
                padding-top: 2px;
                padding-left: 2px;
            }

        #seat-desc-up a span, #seat-desc-down a span {
            display: block;
            width: 24px;
            text-align: center;
            font-weight: bold;
            color: #cc6500;
            font-size: 7pt;
            margin-left: -2px;
        }

        .text-right strong {
            float: right;
            text-align: right;
        }

        .panel-info > .panel-heading {
            color: #000000;
            background-color: #ffffff !important;
        }

        .divWaiting {
            position: fixed;
            background-color: White;
            opacity: 0.7;
            z-index: 2147483647 !important;
            overflow: hidden;
            text-align: center;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            padding-top: 20%;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            var todayDate = new Date().getDate();
            var endD = new Date(new Date().setDate(todayDate + 1));
            var currDate = new Date();

            $('[id*=txtdepartdate]').datepicker({

                startDate: "dateToday",
                endDate: endD,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });


        });
    </script>

    <style>
        .dataTables_wrapper .dataTables_filter input {
            margin-left: 0.5em;
            margin-bottom: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanel18" runat="server">
        <ContentTemplate>--%>
    <asp:HiddenField ID="hfSeatStructUp" runat="server" />
    <asp:HiddenField ID="hfTripChart" runat="server" />
    <asp:HiddenField ID="hfServiceTypeId" runat="server" />
    <asp:HiddenField ID="hfRouteId" runat="server" />
    <asp:HiddenField ID="hfDepartureDate" runat="server" />
    <asp:HiddenField ID="hfDepartureTime" runat="server" />
    <asp:HiddenField ID="hfConductorId" runat="server" />
    <asp:HiddenField ID="hfConductorName" runat="server" />
    <asp:HiddenField ID="hfDriverId" runat="server" />
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="hfNoOfPassanger" runat="server" />
    <div class="container-fluid pt-2">
        <div class="row">
            <div class="col-xl-3 order-xl-1">
                <div class="card" style="min-height: 80vh">
                    <div class="card-header border-bottom">
                        <div class="row">
                            <div class="col-md-6">
                                <h3 class="mb-0">Please Note</h3>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row p-2">
                            <div class="col-md-12">
                                <h5 class="mb-1">For start new Trip follow following steps</h5>
                            </div>
                            <div class="col-md-12 p-2">
                                <p>
                                    1. Select Waybill Reference No and search.
                                </p>
                                <p>
                                    2. Select Journey Type (Down or Up).
                                </p>
                                <p>
                                    3. Select Destination Station.
                                </p>
                                <p>
                                    4. Select no of Seats (maximum 6 seats at a time).
                                </p>
                                <p>
                                    5. Enter Fare Detail.
                                </p>
                            </div>
                        </div>


                    </div>
                </div>
            </div>
            <div class="col-xl-9 order-xl-2">

                <div class="card" style="min-height: 80vh">
                    <div class="card-header">
                        <div class="row align-items-center">
                            <div class="col-7">
                                <h3 class="mb-0">Ticket Booking</h3>
                            </div>
                            <div class="col-md-5 text-right">
                                <asp:LinkButton ID="lbtnback" runat="server" CssClass="btn btn-warning btn-sm" OnClick="lbtnback_Click" ToolTip="Back to current booking dashboard"><i class="fa fa-backward"></i> Go To Current Booking Dashboard </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row p-2">
                            <div class="col-md-12">
                                <div class="row pt-1 pb-1 mt-2 d-none" style="background-color: #5778b3;">
                                </div>
                                <div class="row pt-1 pb-1" style="background-color: #e6e6e6;">
                                    <div class="col-lg-3" style="font-size: 15px; font-weight: 600;">
                                        Waybill Reference No
                                                    
                                                <asp:TextBox ID="txtwaybills" runat="server" CssClass="form-control" AutoComplete="Off" Visible="false"
                                                    placeholder="Waybill Number" MaxLength="15"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-5">
                                        <asp:LinkButton ID="lbtnwaybillnosearch" ToolTip="Search Waybill" OnClientClick="return ShowLoading()" runat="server" OnClick="lbtnwaybillnosearch_Click"
                                            CssClass="btn btn-success btn-sm mr-1 mt-4" strle="z-i">
                                            <i class="fa fa-search"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnwaybillnoreset" ToolTip="Reset Waybill Details" runat="server" OnClick="lbtnwaybillnoreset_Click"
                                            CssClass="btn btn-danger btn-sm mr-1 mt-4" strle="z-i" OnClientClick="return ShowLoading()">
                                            <i class="fa fa-undo"></i>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-lg-4 text-right" style="font-size: 15px; font-weight: 600;">
                                        Booking For <span style="font-size: 9pt; font-weight: initial;">(Journey Date)</span>
                                        <br />
                                        <asp:TextBox ID="txtdepartdate" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="DD/MM/YYYY" runat="server" MaxLength="10" Style="width: 260px; float: right;"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:Panel ID="pnlnowaybill" runat="server" Visible="true">
                            <div class="row">
                                <div class="col-md-4">
                                    <span>Service Type <span class="text-danger font-weight-bold">*</span></span>
                                    <asp:DropDownList ID="ddlservicetype" OnClientClick="return ShowLoading()" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlservicetype_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <span>Depot <span class="text-danger font-weight-bold">*</span></span>
                                    <asp:DropDownList ID="ddldepot" OnClientClick="return ShowLoading()" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddldepot_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <span>Bus <span class="text-danger font-weight-bold">*</span></span>
                                    <asp:DropDownList ID="ddlbus" OnClientClick="return ShowLoading()" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlbus_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>

                            </div>
                            <div class="row mt-2">
                                <div class="col-md-4">
                                    <span>Conductor Name <span class="text-danger font-weight-bold">*</span></span>
                                    <asp:DropDownList ID="ddlconductor" OnClientClick="return ShowLoading()" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <span>Bus Route <span class="text-danger font-weight-bold">*</span></span>
                                    <asp:DropDownList ID="ddlroute" OnClientClick="return ShowLoading()" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlroute_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <span>Journey Type <span class="text-danger font-weight-bold">*</span></span><br />
                                    <asp:DropDownList ID="ddljtype" OnClientClick="return ShowLoading()" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddljtype_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-md-4">
                                    <span>Departure Time <span class="text-danger font-weight-bold">*</span></span><br />
                                    <asp:TextBox ID="txtdeparttime" runat="server" CssClass="form-control" AutoComplete="Off" Style="width: 40%;"
                                        placeholder="XX:XX AM"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptAMPM="true" Enabled="true" Mask="99:99" MaskType="Time" TargetControlID="txtdeparttime" />
                                </div>

                                <div class="col-md-4">
                                </div>
                            </div>
                            <div class="row mt-2" id="dvSpecialService" runat="server">
                                <div class="col-md-6">
                                    <asp:CheckBox ID="chkspcl" runat="server" />
                                    Is This Special Service ?
                                </div>
                            </div>
                            <hr style="margin-top: 10px; margin-bottom: 10px" />
                            <div class="row mt-2">
                                <div class="col-md-4">
                                    <span>Ticket Type </span>
                                    <br />
                                    <asp:DropDownList ID="ddlConcession" OnClientClick="return ShowLoading()" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlConcession_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </asp:Panel>
                        <asp:Panel ID="pnlNotrip" runat="server" Visible="true">
                            <div class="row">
                                <div class="col-lg-12 text-center mt-4">
                                    <p class="text-center" style="font-size: 32px; font-weight: bold; color: #e3e3e3;">
                                        For start new Trip please follow steps.
                                    </p>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlSeatLayout" runat="server" Visible="false">
                            <div class="row mt-3">
                                <div class="col-md-5 pl-0">
                                    <div class="col-md-12 pt-1 pb-1 mb-1" style="font-size: 15px; color: white; font-weight: 600; background-color: #5778b3;">
                                        1. Routes Station
                                    </div>
                                    <div class="col-md-12 pt-1 pb-1 mb-1" style="overflow: auto;">
                                        <asp:GridView ID="gvRouteStationsList" runat="server" CssClass="table table-bordered table-hover table-striped table-responsive-sm shadow" AutoGenerateColumns="False" ClientIDMode="Static" AllowPaging="false"
                                            DataKeyNames="route_station_id,rout_id,station_name,total_dist_km,final_fare,ston_id,from_stationname,starttime,farefrmstation"
                                            text-align="center" GridLines="None">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                            <ContentTemplate>
                                                                <asp:RadioButton runat="server" Checked="false" ID="rblYesNo" AutoPostBack="true"
                                                                    OnCheckedChanged="rblYesNo_CheckedChanged" ValidationGroup="selectradio" GroupName="rbselect" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="station_name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true"
                                                    HeaderText="Route Station" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Font-Bold="true" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="total_dist_km" HeaderStyle-HorizontalAlign="Left"
                                                    HeaderText="Distance(KM)" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Font-Size="14px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="farefrmstation" HeaderStyle-HorizontalAlign="Left" HeaderText="Fare (₹)"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Font-Bold="true" Font-Size="14px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                        </asp:GridView>
                                        <asp:HiddenField ID="hdnToStation" runat="server" />
                                    </div>

                                </div>
                                <div class="col-md-3 pl-0">
                                    <div class="col-md-12 pt-1 pb-1 mb-1" style="font-size: 15px; color: white; font-weight: 600; background-color: #5778b3;">
                                        2. Seat(s)
                                    </div>
                                    <div class="col-md-12 pt-1 pb-1 mb-1 text-center">
                                        <div id='div-seat-table-up'>
                                            <div id="seatpanel" style="border-top: 5px solid #0084d7; box-shadow: 0 19px 38px rgba(0,0,0,0.30), 0 15px 12px rgba(0,0,0,0.22); background-color: rgb(218, 238, 248); padding: 4px; border-radius: 4px;"
                                                class="panel-primary">
                                                <table id='seat-desc-up'>
                                                    <tr>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-1-1" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-2-1" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-3-1" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-4-1" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-5-1" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-6-1" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-7-1" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-8-1" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-9-1" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-10-1" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-11-1" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-12-1" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-13-1" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-14-1" value="" /></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-1-2" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-2-2" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-3-2" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-4-2" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-5-2" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-6-2" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-7-2" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-8-2" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-9-2" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-10-2" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-11-2" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-12-2" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-13-2" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-14-2" value="" /></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-1-3" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-2-3" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-3-3" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-4-3" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-5-3" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-6-3" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-7-3" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-8-3" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-9-3" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-10-3" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-11-3" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-12-3" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-13-3" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-14-3" value="" /></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-1-4" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-2-4" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-3-4" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-4-4" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-5-4" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-6-4" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-7-4" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-8-4" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-9-4" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-10-4" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-11-4" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-12-4" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-13-4" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-14-4" value="" /></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-1-5" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-2-5" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-3-5" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-4-5" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-5-5" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-6-5" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-7-5" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-8-5" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-9-5" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-10-5" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-11-5" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-12-5" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-13-5" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-14-5" value="" /></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-1-6" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-2-6" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-3-6" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-4-6" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-5-6" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-6-6" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-7-6" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-8-6" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-9-6" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-10-6" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-11-6" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-12-6" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-13-6" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-14-6" value="" /></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-1-7" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-2-7" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-3-7" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-4-7" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-5-7" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-6-7" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-7-7" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-8-7" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-9-7" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-10-7" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-11-7" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-12-7" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-13-7" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-14-7" value="" /></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-1-8" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-2-8" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-3-8" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-4-8" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-5-8" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-6-8" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-7-8" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-8-8" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-9-8" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-10-8" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-11-8" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-12-8" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-13-8" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-14-8" value="" /></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-1-9" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-2-9" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-3-9" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-4-9" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-5-9" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-6-9" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-7-9" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-8-9" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-9-9" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-10-9" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-11-9" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-12-9" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-13-9" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-14-9" value="" /></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-1-10" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-2-10" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-3-10" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-4-10" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-5-10" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-6-10" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-7-10" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-8-10" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-9-10" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-10-10" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-11-10" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-12-10" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-13-10" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-14-10" value="" /></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-1-11" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-2-11" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-3-11" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-4-11" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-5-11" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-6-11" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-7-11" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-8-11" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-9-11" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-10-11" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-11-11" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-12-11" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-13-11" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-14-11" value="" /></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-1-12" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-2-12" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-3-12" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-4-12" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-5-12" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-6-12" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-7-12" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-8-12" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-9-12" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-10-12" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-11-12" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-12-12" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-13-12" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-14-12" value="" /></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-1-13" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-2-13" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-3-13" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-4-13" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-5-13" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-6-13" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-7-13" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-8-13" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-9-13" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-10-13" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-11-13" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-12-13" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-13-13" value="" /></a>
                                                        </td>
                                                        <td>
                                                            <a>
                                                                <input type="hidden" id="up-14-13" value="" /></a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 pl-0">
                                    <div class="col-md-12 pt-1 pb-1 mb-1" style="font-size: 15px; color: white; font-weight: 600; background-color: #5778b3;">
                                        3. Entry Details
                                    </div>
                                    <div class="col-md-12 p-3" style="line-height: 38px;">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <span>From Station</span>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label ID="lblFromStation" runat="server" Text="NA"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <span>To Sation</span>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblToStation" runat="server" Text="NA"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <span>Seat No.</span>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtSeatNumbers" runat="server" CssClass="form-control form-control-sm" BackColor="#EEEEEE"
                                                            onCopy="return false" onDrag="return false" onDrop="return false"
                                                            onPaste="return false" autocomplete="off"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <span>Fare per Seat ₹</span>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtFarePerSeat" runat="server" CssClass="form-control form-control-sm" BackColor="#EEEEEE"
                                                            onCopy="return false" onDrag="return false" onDrop="return false"
                                                            onPaste="return false" autocomplete="off"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <span>Total No. of Seats</span>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtTotalNoOfSeat" runat="server" CssClass="form-control form-control-sm" BackColor="#EEEEEE"
                                                            onCopy="return false" onDrag="return false" onDrop="return false"
                                                            onPaste="return false" autocomplete="off"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <span>Total Fare ₹</span>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtTotalFare" runat="server" CssClass="form-control form-control-sm" BackColor="#EEEEEE"
                                                            Font-Bold="True" ForeColor="#FF3300" onCopy="return false" onDrag="return false"
                                                            onDrop="return false" onPaste="return false" autocomplete="off"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>


                                        <div class="row mt-2">
                                            <div class="col-md-6" style="line-height: 18px;">
                                                <asp:Label ID="lblverificationmsg" runat="server" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtverificationid" runat="server" Visible="false" CssClass="form-control"
                                                            onCopy="return false" onDrag="return false" onDrop="return false"
                                                            onPaste="return false" autocomplete="off"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <asp:Button ID="btnPrintTicket" runat="server" Height="35px" Text="Print Ticket" OnClick="btnPrintTicket_Click"
                                            CssClass="btn btn-success" AccessKey="p" Font-Bold="True" Style="color: white; width: 100%; margin-top: 10px; font-size: 11pt;"
                                            OnClientClick="this.disabled='true';return true;return ShowLoading()" />
                                        <br />
                                        <asp:Button ID="btnTripChart" runat="server" Height="35px" Text="Generate Trip Chart"
                                            CssClass="btn btn-danger" Width="100%" Style="color: white; margin-top: 10px; font-size: 11pt;"
                                            AccessKey="t" OnClick="btnTripChart_Click"  OnClientClick="return ShowLoading()"/>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                    </div>
                </div>

            </div>
        </div>


        <div class="row">
            <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
                CancelControlID="Button3" TargetControlID="Button4" BackgroundCssClass="modalBackground" BehaviorID="bvConfirm">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Confirmation
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnYesConfirmation" OnClick="lbtnYesConfirmation_Click" OnClientClick="$find('bvConfirm').hide();ShowLoading();" runat="server" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                            <asp:LinkButton ID="lbtnNoConfirmation" OnClick="lbtnNoConfirmation_Click" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button4" runat="server" Text="" />
                    <asp:Button ID="Button3" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpsucsess" runat="server" PopupControlID="pnlsucsess"
                CancelControlID="Button2" TargetControlID="Button1" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlsucsess" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Information
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblsuccess" runat="server" Text="Trip Chart Successfully Generated."></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnok" OnClick="lbtnok_Click" runat="server" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button1" runat="server" Text="" />
                    <asp:Button ID="Button2" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

        <div class="row">
            <cc1:ModalPopupExtender ID="mpPessenger" runat="server" PopupControlID="pnlPessenger"
                CancelControlID="Button6" TargetControlID="Button7" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlPessenger" runat="server" Style="position: fixed;">
                <div class="card">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Enter Passenger Details
                        </h4>
                        <asp:Label ID="lblstations" runat="server"></asp:Label>
                    </div>
                    <div class="card-body text-center pt-2" style="min-height: 100px;">
                        <div class="row">
                            <div class="col-md-6">
                                <label class="text-muted" style="font-size: 10pt;">Total Seat</label>
                                <asp:Label ID="lbltotalseats" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-6">
                                <label class="text-muted" style="font-size: 10pt;">Total Fare</label>
                                <asp:Label ID="lbltotalfare" runat="server"></asp:Label>
                                <i class="fa fa-rupee"></i>
                            </div>
                        </div>

                        <hr />
                        <asp:GridView ID="grvPessenger" runat="server" AutoGenerateColumns="false" OnRowDataBound="grvPessenger_RowDataBound" GridLines="None" Font-Size="10pt"
                            AllowPaging="true" PageSize="6" CssClass="table table-hover table-striped" DataKeyNames="">
                            <Columns>
                                <asp:TemplateField HeaderText="Seat Number" ItemStyle-CssClass="class-on-element">
                                    <ItemTemplate>
                                        <asp:Label ID="lblseatno" runat="server" Text='<%# Eval("seatNO") %>' CssClass="form-control"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" ItemStyle-CssClass="class-on-element">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" FilterType="UppercaseLetters,LowercaseLetters"
                                                            ValidChars=" " TargetControlID="txtname" />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID Type" ItemStyle-CssClass="class-on-element">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlidtype" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID Number" ItemStyle-CssClass="class-on-element">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtidnumber" runat="server" CssClass="form-control"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-CssClass="class-on-element">
                                    <ItemTemplate>
                                        OR
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Special Ref. Number" ItemStyle-CssClass="class-on-element">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSpclRefno" runat="server" CssClass="form-control"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="GridPager" ForeColor="Black" />
                        </asp:GridView>
                        <hr />
                        <div class="row mt-2">
                            <div class="col-md-12 text-center ">
                                <asp:Label ID="lblerrormsg" runat="server" Style="color: red;"></asp:Label>
                            </div>
                        </div>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:Label ID="Label2" runat="server" Text="Do you want to save ?"></asp:Label>
                            <asp:LinkButton ID="lbtnspecialyes" runat="server" OnClick="lbtnspecialyes_Click" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                            <asp:LinkButton ID="lbtnspecialno" runat="server" OnClick="lbtnspecialno_Click" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button6" runat="server" Text="" />
                    <asp:Button ID="Button7" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpShowTicket" runat="server" PopupControlID="Panel1" TargetControlID="Button8"
                CancelControlID="LinkButton2" BackgroundCssClass="ModalPopupBG">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="Panel1" runat="server" Style="position: fixed;">
                <div class="modal-content mt-5" style="width: 30vw;">
                    <div class="card w-100">
                        <div class="card-header py-3">
                            <div class="row">
                                <div class="col">
                                    <h3 class="m-0">Passenger Ticket</h3>
                                </div>
                                <div class="col-auto">
                                    <asp:LinkButton ID="LinkButton2" runat="server" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body p-2">

                            <asp:Literal ID="eSlip" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button5" runat="server" Text="" />
                    <asp:Button ID="Button8" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
    </div>

    <%--</ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPrintTicket" />
            <asp:PostBackTrigger ControlID="btnTripChart" />
            <asp:PostBackTrigger ControlID="lbtnYesConfirmation" />
            <asp:PostBackTrigger ControlID="lbtnNoConfirmation" />
            <asp:PostBackTrigger ControlID="lbtnspecialyes" />
        </Triggers>
    </asp:UpdatePanel>--%>
    <script src="../assets/vendor/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>

    <%-- <script type="text/javascript">
        $(document).ready(function () {
            var todayDate = new Date().getDate();
            var endD = new Date(new Date().setDate(todayDate + 1));
            var currDate = new Date();
            $('[id*=txtdepartdate]').datepicker({
                startDate: "dateToday",
                endDate: endD,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });

        });
    </script>--%>
    <script src="../DataTables/js/jquery.dataTables.min.js"></script>
    <link href="../assets/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('#gvRouteStationsList').DataTable(
                 {
                     bLengthChange: true,
                     lengthMenu: [[100, 200, -1], [100, 200, "All"]],
                     bFilter: true,
                     bSort: true,
                     bPaginate: false
                 });
        });
    </script>
</asp:Content>

