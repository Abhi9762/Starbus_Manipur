<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="sysAdmBlockSeat.aspx.cs" Inherits="Auth_sysAdmBlockSeat" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<script src="../images/loading/jquery-1.8.2.js"></script>--%>
    <script src="../NewAssets/js/jquery-3.3.1.min.js"></script>
    <script src="../assets/js/jqueryuimin.js"></script>
    <style type="text/css">
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
            }

                #seat-desc-up td a, #seat-desc-down td a {
                    width: 31px;
                    height: 31px;
                    display: block;
                }

        /*.available, .woman, .man, .booked, .selected, .sleeper
        {
            background-image: url('../../images/NewSeat1.png');
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

        dvAcOnly.man {
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

        .Reserved {
            background-image: url('../images/seats/seatred.png'); /* background-position: -112px 0;*/
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
            font-size: 10pt;
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
            background: url('../images/bus-layout.png') 0 0; 
            position: relative;
        } */
        #div-seat-table-up {
            margin-left: -40dp;
        }

        .av, .bo, .wo, .mr, .res, .seltd {
            background-image: url('../images/ticket-seat-small.png');
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
            margin-left: 60px;
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
            width: 100%;
            margin: 0;
            display: none;
            overflow: hidden;
        }

        #up-ticket-table {
            width: 100%;
        }

        .ticket-tbl-first-row {
            font-size: 10px;
            text-transform: uppercase;
            font-weight: bold;
            color: #0084d9;
        }
        /* .seat table
        {
            width: 460px;
            border: 1px solid #647aff;
            background-color: #d9defb;
        }
        .seat table tr
        {
            padding-bottom: 4px;
            border: 1px solid #9eacfc;
            width: 458px;
        }
        .seat table, .seat table td
        {
            border-collapse: collapse;
        }
        .seat table td
        {
            height: 21px;
            text-align: center;
            border: 1px dotted #9eacfc;
            padding: 0;
        } */
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
            /* background: url('../../images/rj-bg.png');*/
            display: block;
        }

        #heading-left-down, #heading-mid-down, #heading-right-down {
            background: url('../../images/oj-bg.png');
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

        h3 {
            text-align: center;
        }

        #div-submit-detail table {
            border-collapse: collapse;
        }

            #div-submit-detail table td {
                border-collapse: collapse;
                padding: 5px;
            }

        #seat-desc-up a span, #seat-desc-down a span {
            display: block;
            text-align: center;
            font-weight: bold;
            color: #cc6500;
            font-size: 10px;
        }

        .text-right strong {
            float: right;
            text-align: right;
        }

        .panel-info > .panel-heading {
            color: #34aadc;
            background-color: #f9f9f9 !important;
            border-color: #f9f9f9;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input:checkbox').change(function () {
                var seatNo = "$";
                $('input:checkbox').each(function () {
                    if ($(this).is(':checked')) {
                        $td = $(this).parents('tr').children('td');
                        seatNo = seatNo + ',' + $($td[1]).html();
                    }
                });
                $('#<%= hfSeatList.ClientID %>').val(seatNo.replace("$,", ""));
                $('#<%= hfNoOfSeat.ClientID %>').val($("input:checked").length);
            });

            //blink
            (function ($) {
                $.fn.ajBlink = function () {
                    return this.each(function (i, obj) {
                        $(this).animate({ opacity: 0 }, 200, "linear", function () {
                            $(this).animate({ opacity: 1 }, 200);
                        });
                    });
                };
            })(jQuery);

            var isRoundTrip = $('#hfIsRoundTrip').val();

            //Populate Onward BusLayout
            var seatDescUp = $('#hfSeatStructUp').val();
            // getSeatStruct('up', seatDescUp);


            //For Single Trip Journey Remove Return Journey BusLayout & Fix controls to page Layout
            $("#down-journey").parent('td').remove()

            $("#up-journey").children('table').css({ 'margin': '5px auto' });
            $(".ul-seat-desc").css({ 'left': '235px' });
            $(".header-text-up").css({ 'margin-left': '343px' });

            //check click event on not selected seat
            $('.available,.woman,.man,.sleeper, .selected').click(function () {


                var className = $(this).attr("class");
                var $this = $(this);

                if (className == "selected") {

                    $(this).attr("class", "");

                    var journeyType = $this.children('input[type=hidden]').attr('id').split('-');
                    var seatNoCat = $this.children('input[type=hidden]').val().split('-');

                    if (seatNoCat[1] == 'G')
                        $this.addClass('available');
                    if (seatNoCat[1] == 'F')
                    { $this.addClass('woman'); }
                    if (seatNoCat[1] == 'M')
                        $this.addClass('man');
                    if (seatNoCat[1] == 'S')
                        $this.addClass('sleeper');
                    if (seatNoCat[1] == 'U')
                        $this.addClass('available');
                    var rowClass = $this.children('input[type=hidden]').attr('id');
                    $(".tr" + rowClass).fadeOut(200, function () {
                        $('.tr' + rowClass).remove();
                        var selectedSeat = $('#div-seat-table-' + journeyType[0] + ' .selected').length;
                        if (selectedSeat == 0) {
                            $('#ticket-booking-' + journeyType[0] + ' .seat').hide();
                        }
                    });
                }
                else {
                    var journeyType = $this.children('input[type=hidden]').attr('id').split('-');
                    var selectedSeat = $('#' + journeyType[0] + '-journey .selected').length;
                    if (selectedSeat < 6) {     //PassangerNo
                        $(this).attr("class", "");
                        $this.addClass('selected');
                        ///== Fare And Tax Calulation
                        var farePerTicket = parseFloat($('#' + journeyType[0] + 'Fare').val());

                        var rowClass = $this.children('input[type=hidden]').attr('id');
                        var seatNoCat = $this.children('input[type=hidden]').val().split('-');
                        var selectGender = "<select class='opt-gender'><option value='Male'>Male</option><option value='Female'>Female</option></select>";
                        if (seatNoCat[1] == 'W')
                            selectGender = "<select disabled='disabled' class='opt-gender'><option value='Male'>Male</option><option value='Female' selected>Female</option></select>";
                        //----category-----                       
                        //$('#' + journeyType[0] + 'TotalFare').parents('.totStatRow').before("<tr class='seat-entry tr" + rowClass + "'><td class='cus-seat-no'><blink><span class='csn'>" + seatNoCat[0] + "</span></blink></td><td><input class='Name' type='text' maxlength='25' /></td><td>" + selectGender + "</td><td><input class='Age' type='text' maxlength='2' /></td><td>" + selectCategory + "</td><td class='text-right'>" + farePerTicket + "</td></tr>");
                        $('#' + journeyType[0] + 'TotalFare').parents('.totStatRow').before("<tr class='seat-entry tr" + rowClass + "'><td class='cus-seat-no'><blink><span class='csn'>" + seatNoCat[0] + "</span></blink></td></tr>");
                        // $('.csn').ajBlink();
                        $(".tr" + rowClass).fadeIn(300);
                        $('#ticket-booking-' + journeyType[0] + ' .seat').show();
                        //CalculateFare();
                    }
                    else {
                        alert('You can book maximum 6 seats');
                    }
                }
            });
            $('.opt-cat').change('on', function () {
                // CalculateFare();
            });
            //check click event on not selected seat
            //$('.selected').click(function () {

            //    $this = $(this);
            //    $this.removeClass();


            //    var journeyType = $this.children('input[type=hidden]').attr('id').split('-');
            //    var seatNoCat = $this.children('input[type=hidden]').val().split('-');

            //    if (seatNoCat[1] == 'G')
            //        $this.addClass('available');
            //    if (seatNoCat[1] == 'W')
            //    { $this.addClass('woman'); }
            //    if (seatNoCat[1] == 'M')
            //        $this.addClass('man');
            //    if (seatNoCat[1] == 'S')
            //        $this.addClass('sleeper');
            //    if (seatNoCat[1] == 'U')
            //        $this.addClass('available');
            //    var rowClass = $this.children('input[type=hidden]').attr('id');
            //    $(".tr" + rowClass).fadeOut(200, function () {
            //        $('.tr' + rowClass).remove();

            //        var selectedSeat = $('#div-seat-table-' + journeyType[0] + ' .selected').length;
            //        if (selectedSeat == 0) {
            //            $('#ticket-booking-' + journeyType[0] + ' .seat').hide();
            //        }
            //    });
            //});

            //Chesk On Proceed Button Click
            $('#<%= btnProceed.ClientID %>').click(function () {
                customerData = "";
                $('.seat-entry').each(function () {
                    $this = $(this);
                    var jaourneyTypeId = $this.parents('table').attr('id').split('-');

                    customerData = customerData + '|' + $this.children('.cus-seat-no').text() + ',' + jaourneyTypeId[0] + ',' + $this.children("td :nth-child(6)").text();
                });
                $('#<%= hfCustomerData.ClientID %>').val(customerData.substr(1, customerData.length - 1));

                //alert(customerData);
            });


            //Check On Form Submit
            $('form').submit(function () {
                //var PassangerNo = $('#<%= hfNoOfPassanger.ClientID %>').val();
                var selectedUpSeat = $('#div-seat-table-up .selected').length;
                // if (selectedUpSeat < PassangerNo) {
                //    alert('Please select ' + PassangerNo + ' seat(s) in onward journey list.');
                //   return false;
                // }

                var isRoundTrip = $('#<%= hfIsRoundTrip.ClientID %>').val();
                if (isRoundTrip == 'YES') {
                    var selectedDownSeat = $('#div-seat-table-down .selected').length;
                    if (selectedDownSeat < PassangerNo) {
                        alert('Please select ' + PassangerNo + ' seat(s) in return journey list.');
                        return false;
                    }
                }
                /////==checktextbox
                var isTxtBoxBlank = false;
                $('.seat input[type=text]').each(function () {
                    if ($(this).val().trim().length == 0) {
                        isTxtBoxBlank = true;
                        $(this).css({ 'border': '1px solid red', 'background': '#ffd4d4' })
                    }
                    else {
                        $(this).css({ 'border': '1px solid #CCC', 'background': '#fff' })
                    }
                });

                if (isTxtBoxBlank) {
                    alert('Please fill all passanger\'s Name/Age correctly!');
                    return false;
                }
            });

            $('#btnCancel').click(function (e) {
                e.preventDefault();
                $('#mask').hide();
                $('#confirm').hide();
            });
            $(window).resize(function () {
                var box = $('#box');
                var maskHeight = $(document).height();
                var maskWidth = $(window).width();
                $('#mask').css({ 'width': maskWidth, 'height': maskHeight });
                var winH = $(window).height();
                var winW = $(window).width();
                box.css('top', winH / 2 - box.height());
                box.css('left', winW / 2 - box.width() / 2);
            });
        });
        function getSeatStruct(journeyType, SeatDesc) {

            // $('#seat-desc-' + journeyType + ' td a').addClass('booked');
            var SeatDetail = SeatDesc.split(',');//$(SeatDesc).val().split(',');          
            //alert(SeatDetail);
            var Rows = 0; var Cols = 0; Fare = 0.0;
            var rowcol = SeatDetail[0].split('-');
            Rows = parseInt(rowcol[0]);
            Cols = parseInt(rowcol[1]);
            Fare = parseFloat(rowcol[2]);
            //  alert("Rows:- "+Rows);
            // alert("Cols:- "+Cols);
            if (Rows == 8) {
                $('#seatpanel').css({ 'height': 281 });
            }
            if (Cols == 6) {
                $('#seatpanel').css({ 'width': 235 });
            }

            $('body').append('<input type="hidden" id="' + journeyType + 'Fare" value="' + Fare + '" />')
            $('#' + journeyType + '-' + Rows + '-' + (Cols)).remove();

            //INJECT LAST SEAT TO TABLE
            // $('#seat-desc-' + journeyType + ' tr:nth-child(4) td:nth-child(' + Rows + ')').html('<a class="booked"><input id="' + journeyType + '-' + Rows + '-' + (Cols + 1) + '" type="hidden" /></a>');
            //alert(SeatDetail.length);
            for (var i = 1; i < SeatDetail.length; i++) {
                var seatRow = SeatDetail[i].split('-');
                //alert(seatRow);
                //alert('Col-' + seatRow[0] + 'Row-' + seatRow[1] + 'seatno-' + seatRow[2] + 'seatyn' + seatRow[3] + 'travellertype' + seatRow[4] + 'AvalBook' + seatRow[5]);
                $('#' + journeyType + '-' + seatRow[0] + '-' + seatRow[1]).val(seatRow[2] + '-' + seatRow[4])
                $('#' + journeyType + '-' + seatRow[0] + '-' + seatRow[1]).parents('a').removeClass();
                //  alert("journeyType:- " + journeyType);
                // alert("seatRow[0]:-" + seatRow[0]);
                // alert("seatRow[1]:-" + seatRow[1]);
                // alert("seatRow[2]:-" + seatRow[2]);
                // alert("seatRow[3]:-" + seatRow[3]);
                // alert("seatRow[4]:-" + seatRow[4]);
                if (seatRow[3] == 'N') {
                    $('#' + journeyType + '-' + seatRow[0] + '-' + seatRow[1]).parents('a').removeClass();
                }

                else if (seatRow[4] == 'D') {
                    $('#' + journeyType + '-' + seatRow[0] + '-' + seatRow[1]).parents('a').addClass('driver');
                }
                else if (seatRow[4] == 'G') {
                    //  alert(seatRow[2]);
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

        }



    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var todayDate = new Date().getDate();
            var endDate = $("[id$=hdmaxdate]").val();
            var endD = new Date(new Date().setDate(todayDate + parseInt(endDate - 1)));
            var currDate = new Date();
            $('[id*=txtdate]').datepicker({
                startDate: "dateToday",
                endDate: endD,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });
        });
    </script>


    <script type="text/javascript">
        $(document).ready(function () {
            $(".searchable-dropdown").select2(); // Assuming you're using Select2 library
        });
    </script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/js/select2.min.js"></script>
    <style>
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #444;
            line-height: 11px;
            font-size: 14px;
        }

        .select2-container--default .select2-selection--single {
            background-color: #fff;
            border: 1px solid #aaa;
            border-radius: 4px;
            height: 37px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="hidtoken" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hfSeatList" runat="server" />
    <asp:HiddenField ID="hfIsRoundTrip" runat="server" />
    <asp:HiddenField ID="hfSeatStructUp" runat="server" />
    <asp:HiddenField ID="hfSeatStructDown" runat="server" />
    <asp:HiddenField ID="hfCustomerData" runat="server" />
    <asp:HiddenField ID="hfNoOfPassanger" runat="server" />
    <asp:HiddenField ID="hfCategoryList" runat="server" />
    <asp:HiddenField ID="hfContacID" runat="server" />
    <asp:HiddenField ID="hfNoOfSeat" runat="server" />
    <asp:HiddenField ID="hfRefundAmt" runat="server" />
    <asp:HiddenField ID="hdmaxdate" runat="server" />
    <div class="container-fluid" style="padding-top: 20px; padding-bottom: 20px;">
        <div class="row">
            <div class="col-lg-12">
                <div class="card" style="font-size: 10pt; min-height: 90vh;">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-2">
                                <span>Service Type</span>
                                <asp:DropDownList ID="ddlBusServiceTypes" OnSelectedIndexChanged="ddlBusServiceTypes_SelectedIndexChanged" runat="server" Style="height: 38px;color: #444;"
                                    ToolTip="Service Type" AutoPostBack="true" class="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <span>Departure From</span>
                                <asp:DropDownList ID="ddlPlaceFrom" CssClass=" searchable-dropdown form-control" OnSelectedIndexChanged="ddlPlaceFrom_SelectedIndexChanged" runat="server" Style="height: 40px; font-size: 12px;"
                                    ToolTip="From Station" AutoPostBack="true" class="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <span>Arrival To</span>
                                <asp:DropDownList ID="ddlPlaceTo" CssClass=" searchable-dropdown form-control" runat="server" Style="height: 40px; font-size: 12px;"
                                    ToolTip="To Station" AutoPostBack="true" class="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                                <span>Journey Date</span><br />
                                <asp:TextBox class="form-control" runat="server" ID="txtdate" AutoComplete="off" MaxLength="10" placeholder="DD/MM/YYYY"
                                    Text="" Style="height: 38px;color: #444;"></asp:TextBox>

                            </div>
                            <div class="col-md-2" style="padding-top: 20px; padding-left: 0px;">
                                <asp:LinkButton ID="btnSearch" OnClientClick="return ShowLoading();" Height="40px" runat="server" OnClick="btnSearch_Click" CssClass="btn btn-success"> <i class="fa fa-search"></i> Search </asp:LinkButton>
                                <asp:LinkButton ID="btnReset" runat="server" OnClientClick="return ShowLoading();" Height="40px" OnClick="btnReset_Click" CssClass="btn btn-warning"> <i class="fa fa-times "></i> Reset </asp:LinkButton>

                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="pnldetails" runat="server" Visible="false">
                        <div class="card-body p-2">
                            <div class="row">
                                <div class="col-md-6">
                                    <h3 class="text-left pt-2 pb-1">
                                        <asp:Label ID="lblSearchResult" runat="server" Text="Search Results for"></asp:Label>
                                    </h3>
                                    <p>
                                        <asp:Label ID="lblServTPNameData" runat="server" Style="font-weight: 600;"></asp:Label>,
                                                            <asp:Label ID="lblDepSNameData" runat="server" Style="font-weight: 600;"></asp:Label>
                                        to
                                                            <asp:Label ID="lblArvlSNameData" runat="server" Style="font-weight: 600;"></asp:Label>
                                        for
                                                            <asp:Label ID="lblJrnyDateData" runat="server" Style="font-weight: 600;"></asp:Label>
                                    </p>
                                    <hr style="margin-top: -5px; margin-bottom: 15px" />
                                    <asp:GridView ID="grvJDetails" runat="server" AutoGenerateColumns="false" Font-Size="Larger"
                                        DataKeyNames="dsvcid,tripdirection,depttime,arrtime,fare,totalavailablseats,strpid"
                                        Width="100%" class="table table-striped" OnRowCommand="grvJDetails_RowCommand" GridLines="None" OnSelectedIndexChanged="grvJDetails_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Service Code
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblservicecode" runat="server" Font-Size="Larger" Text='<%#Eval("dsvcid") %>'></asp:Label><asp:Label
                                                        ID="Label2" runat="server" Text='<%#Eval("tripdirection") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Departure
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlbldt" runat="server" Font-Size="Larger" Text='<%#Eval("depttime") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Arrival
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblat" runat="server" Font-Size="Larger" Text='<%#Eval("arrtime") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Fare
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlbltf" runat="server" Font-Size="Larger" Text='<%#Eval("fare") %>'></asp:Label>&nbsp;
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Distance
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlbltd" runat="server" Font-Size="Larger" Text='<%#Eval("distance") %>'></asp:Label>&nbsp;KM
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Availables
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblfare" runat="server" Font-Size="Larger" Text='<%#Eval("totalavailablseats") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnselect" runat="server" CssClass="btn btn-sm btn-success" CommandName="Select" OnClientClick="return ShowLoading();"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' ToolTip="Click Here to Block Seat a trip"
                                                        Style="width: 32px; padding: 2px;">  <i class="fa fa-forward" title="click here to show bus layout"></i>  </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                       
                                    </asp:GridView>
                                </div>
                                <div class="col-md-6">
                                    <asp:Panel ID="pnlseatlayout" runat="server" Visible="false">

                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-md-6 offset-1 pl-5">
                                                        <h3 class="">Select Seat</h3>
                                                    </div>
                                                </div>

                                                <div id='up-journey'>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <div id='div-seat-table-up' style="background: #f4f5f8; border: solid 1px #e2e7f8; margin-top: 20px;">
                                                                    <div id="seatpanel" style="padding: 9px; border-radius: 4px; width: 195px;"
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
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <h3 class="text-left">Traveller Detail</h3>
                                                <div id='div-submit-detail'>
                                                    <table id="tblseatblck" runat="server" style="text-align: center;">
                                                        <tr>
                                                            <td>
                                                                <div id="ticket-booking-up" class="d-none">
                                                                    <div class="seat">
                                                                        <table id='up-ticket-table'>
                                                                            <tr class="ticket-tbl-first-row">
                                                                                <td style="width: 100%;">SELECTED SEAT NO.
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="totStatRow">
                                                                                <td>
                                                                                    <strong><span id="upTotalFare" class="text-right"></span></strong>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left;font-size:larger"> Name<span class="text-danger">*</span>
                                                                                    <asp:TextBox ID="txtReason" autocomplete="off" runat="server" class="form-control" MaxLength="20" placeholder="Enter Name"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="ajxFtMiddleName" runat="server" ValidChars=" " FilterType="UppercaseLetters, LowercaseLetters,Custom"
                                                                    TargetControlID="txtReason" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left;font-size:larger">Mobile Number<span class="text-danger">*</span>
                                                                                    <asp:TextBox ID="txtvipmobileno" autocomplete="off" runat="server" class="form-control" MaxLength="10"
                                                                                        placeholder="10 Digit Mobile Number"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="ajxFtMobileNumber" runat="server" FilterType="Numbers" ValidChars=""
                                                                    TargetControlID="txtvipmobileno" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="btnSeatReset" OnClick="btnSeatReset_Click" OnClientClick="return ShowLoading();" class="btn btn-success btn-sm mt-3" runat="server"
                                                                    Text=""><i class="fa fa-times"></i>  Reset </asp:LinkButton>
                                                                <asp:LinkButton ID="btnProceed" OnClick="btnProceed_Click" OnClientClick="return ShowLoading();" class="btn btn-danger btn-sm mt-3" runat="server"
                                                                    Text=""> <i class="fa fa-check"></i> Block Seat </asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">

                                                <center>
                                                        <table class="w-100 text-center mt-3">
                                            <tr>
                                                <td>
                                                    <img src="../images/seats/seatgray.png" style="height: 20px;" />
                                                    <p style="font-size: 10px;">Booked</p>
                                                </td>
                                                <td>
                                                    <img src="../images/seats/seatblue.png" style="height: 20px;" />
                                                    <p style="font-size: 10px;">Selected</p>
                                                </td>
                                                <td>
                                                    <img src="../images/seats/seatwhite.png" style="height: 20px;" />
                                                    <p style="font-size: 10px;">Available</p>
                                                </td>
                                                <td>
                                                    <img src="../images/seats/seatMale.png" style="height: 20px;" />
                                                    <p style="font-size: 10px;">Male</p>
                                                </td>
                                                <td>
                                                    <img src="../images/seats/seatpink.png" style="height: 20px;" />
                                                    <p style="font-size: 10px;">Female</p>
                                                </td>
                                                <td>
                                                    <img src="../images/seats/seatConductor.png" style="height: 20px;" />
                                                    <p style="font-size: 10px;">Conductor</p>
                                                </td>
                                            </tr>
                                        </table>
                                                                            <%--<table class="ul-seat-Upr" style="width: 100%;">
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class="spansheetupr" style="background-color: #ffffff;"></span><span style="margin-left: 8px;
                                                                                            margin-top: -5px;">Available</span>
                                                                                    </td>
                                                                                    <td>
                                                                                        <span class="spansheetupr" style="background-color: #2f6ec3;"></span><span style="margin-left: 8px;
                                                                                            margin-top: -5px;">Selected</span>
                                                                                    </td>
                                                                                    <td>
                                                                                        <span class="spansheetupr" style="background-color: #868998;"></span><span style="margin-left: 8px;
                                                                                            margin-top: -5px;">Reserved</span>
                                                                                    </td>
                                                                                    <td>
                                                                                        <span class="spansheetupr" style="background-color: #52cab4;"></span><span style="margin-left: 8px;
                                                                                            margin-top: -5px;">Male</span>
                                                                                    </td>
                                                                                    <td>
                                                                                        <span class="spansheetupr" style="background-color: #d84ec1;"></span><span style="margin-left: 8px;
                                                                                            margin-top: -5px;">Female</span>
                                                                                    </td>
                                                                                    <td>
                                                                                        <span class="spansheetupr" style="background-color: #bfd1e5;"></span><span style="margin-left: 8px;
                                                                                            margin-top: -5px;">Sleeper</span>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>--%>
                                                                        </center>

                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>

                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Label ID="lblMessageJ" runat="server" CssClass="lblMessage text-center mt-5 text-muted" Font-Bold="True"
                        Visible="false" Font-Size="20pt" Text="Sorry No Servie is Available for this selection."></asp:Label>
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
                    <h4 class="card-title text-left mb-0">Please Confirm
                    </h4>
                </div>
                <div class="card-body text-center pt-2" style="min-height: 100px;">
                    <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                    <div style="width: 100%; margin-top: 20px; text-align: right;">
                        <asp:LinkButton ID="lbtnYesConfirmation" OnClick="lbtnYesConfirmation_Click" OnClientClick="$find('bvConfirm').hide();ShowLoading();" runat="server" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                        <asp:LinkButton ID="lbtnNoConfirmation" OnClick="lbtnNoConfirmation_Click" OnClientClick="return ShowLoading();" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button1" runat="server" Text="" />
                <asp:Button ID="Button3" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>


