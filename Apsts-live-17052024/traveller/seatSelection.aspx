<%@ Page Title="" Language="C#" MasterPageFile="~/traveller/trvlrMaster.master" AutoEventWireup="true" CodeFile="seatSelection.aspx.cs" Inherits="traveller_seatSelection" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <script type="text/javascript" src="../assets/js/jquery-1.8.3.min.js"></script>--%>
    <link href="../css/travelllerStepProgressBar.css" rel="stylesheet" />
    <script src="../assets/js/jquery-n.js"></script>
    <style>
        input[type="checkbox"] {
            width: 22px; /*Desired width*/
            height: 18px; /*Desired height*/
        }

        input[type="radio"] {
            width: 22px; /*Desired width*/
            height: 18px; /*Desired height*/
        }

        .input-group-prepend .btn, .input-group-append .btn {
            position: relative;
            z-index: 0;
        }
    </style>
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
            background-image: url('../images/NewSeat1.png');
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
            background: url('images/bus-layout.png') 0 0; 
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
            /* background: url('../images/rj-bg.png');*/
            display: block;
        }

        #heading-left-down, #heading-mid-down, #heading-right-down {
            background: url('../images/oj-bg.png');
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
            padding-top: 9px;
            width: 24px;
            text-align: center;
            font-weight: bold;
            color: #cc6500;
            font-size: 7px;
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
   <script>
        function onlyNumberKey(evt) {
            // Only ASCII character in that range allowed
            var ASCIICode = (evt.which) ? evt.which : evt.keyCode
            if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
                return false;
            return true;
        };

        function onlyNumberMobile(e) {
            e.target.value = e.target.value.replace(/[^\d]/g, '');
            return false;
        };

        function onlyName(evt) {
            var ASCIICode = (evt.which) ? evt.which : evt.keyCode
            if ((ASCIICode == 32) || (ASCIICode > 64 && ASCIICode < 91) || (ASCIICode > 96 && ASCIICode < 123))
                return true;
            return false;
        };

        function onlyNameMobile(evt) {
            var key = evt.key;

            // Check if the pressed key is a space or an alphabetical character
            if (key === " " || /^[a-zA-Z]$/.test(key)) {
                // Allow the input for space or alphabetical characters
                return true;
            }

            // Prevent input for other characters
            evt.preventDefault();
            return false;
        };


    </script>
    <script type="text/javascript">
        $(document).ready(function () {
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

            //RoundTrip Check  
            var isRoundTrip = $('#<%= hfIsRoundTrip.ClientID %>').val();

            //Populate Onward BusLayout
            var seatDescUp = $('#<%= hfSeatStructUp.ClientID %>').val();
            // alert(isRoundTrip);
            getSeatStruct('up', seatDescUp);

            //For Single Trip Journey Remove Return Journey BusLayout & Fix controls to page Layout
            $("#down-journey").parent('td').remove()
            $("#up-journey").css({ 'width': '100%' });
            $("#up-journey").children('table').css({ 'margin': '5px auto' });
            $(".ul-seat-desc").css({ 'left': '235px' });
            $(".header-text-up").css({ 'margin-left': '360px' });

            var idCount = 0;
            //check click event 
            $(".available, .woman, .man, .sleeper, .selected").click(function () {

                var className = $(this).attr("class");
                var $this = $(this);

                if (className == "selected") {
                    $this.removeClass();
                    var journeyType = $this.children('input[type=hidden]').attr('id').split('-');
                    var seatNoCat = $this.children('input[type=hidden]').val().split('-');
                    if (seatNoCat[1] == 'G')
                        $this.addClass('available');
                    if (seatNoCat[1] == 'F') { $this.addClass('woman'); }
                    if (seatNoCat[1] == 'M')
                        $this.addClass('man');
                    if (seatNoCat[1] == 'S')
                        $this.addClass('sleeper');
                    var rowClass = $this.children('input[type=hidden]').attr('id');
                    $(".tr" + rowClass).fadeOut(200, function () {
                        $('.tr' + rowClass).remove();

                        var selectedSeat = $('#div-seat-table-' + journeyType[0] + ' .selected').length;
                        //---------------------------
                        var chkpassengertype1 = $('#<%=hfPassangertype.ClientID %>').val();
                        var chkvalue = $('input[name=inp]').val();
                        if ((chkpassengertype1 == 'F') && (selectedSeat == 0)) {
                            $('#<%= hdncheck.ClientID %>').val("check");
                        }
                        else if ((chkpassengertype1 == 'J') && (selectedSeat == 0)) {
                            $('#<%= hdncheck.ClientID %>').val("check");
                        }
                        else {
                            $('#<%= hdncheck.ClientID %>').val("");
                        }

                        if (selectedSeat == 0) {
                            $('#ticket-booking-' + journeyType[0] + ' .seat').hide(1000);
                            $('#dvNoRecord').show();
                        }
                    });
            }
            else {
                var PassangerNo = $('#<%= hfNoOfPassanger.ClientID %>').val();
                   // var PassangerNo = 6;
                    var seatNoCat = $this.children('input[type=hidden]').val().split('-');

                    //----------------------FreedomFighter/Journalist Logic
                    var selectGender
                    var name = $this.children('input[type=hidden]').attr('id');

                    var gender = $("#<%= hdngender.ClientID %>").val();

                    var chk = $('#<%= hdncheck.ClientID %>').val();
                    var seatNoCat = $this.children('input[type=hidden]').val().split('-');
                    if (chk == 'check') {
                        var Gender = $('#<%= hdngender1.ClientID %>').val();
                        var selectGender = "<select class='opt-gender'  id='gender' style='width:90px; height: 29px; border-radius: 3px; font-size: 12px;'><option value='M'>Male</option><option value='F'>Female</option></select>";
                        if (Gender == 'F')
                            selectGender = "<select disabled='disabled' class='opt-gender'  style='width:90px; height: 29px; border-radius: 3px; font-size: 12px;'><option value='M'>Male</option><option value='F' selected>Female</option></select>";
                        if (Gender == 'M')
                            selectGender = "<select disabled='disabled' class='opt-gender'  style='width:90px; height: 29px; border-radius: 3px; font-size: 12px;'><option value='M' selected >Male</option><option value='F' >Male</option></select>";
                        $('input[name=inp]').val(seatNoCat[0]);

                        var gender1 = $("#<%= hdngender1.ClientID %>").val();
                        if (gender1 == 'M' && seatNoCat[1] == 'F') {
                            alert("InValid seat");
                            return;
                        }
                        if (gender1 == 'F' && seatNoCat[1] == 'M') {
                            alert("InValid seat");
                            return;
                        }
                    }
                    else {
                        var selectGender = "<select class='opt-gender'  id='gender' style='width:100%; height: 29px; border-radius: 3px; font-size: 12px;'><option value='M'>Male</option><option value='F'>Female</option></select>";
                        if (seatNoCat[1] == 'F')
                            selectGender = "<select disabled='disabled' class='opt-gender'  style='width:90px; height: 29px; border-radius: 3px; font-size: 12px;'><option value='M'>Male</option><option value='F' selected>Female</option></select>";
                        if (seatNoCat[1] == 'M')
                            selectGender = "<select disabled='disabled' class='opt-gender'  style='width:90px; height: 29px; border-radius: 3px; font-size: 12px;'><option value='M' selected >Male</option><option value='F' >Female</option></select>";
                    }

                    var journeyType = $this.children('input[type=hidden]').attr('id').split('-');
                    var selectedSeat = $('#' + journeyType[0] + '-journey .selected').length;
                    if (selectedSeat < PassangerNo) {
                        $this.removeClass();
                        $this.addClass('selected');
                        ///== Fare And Tax Calulation
                        var farePerTicket = parseFloat($('#' + journeyType[0] + 'Fare').val());
                        var rowClass = $this.children('input[type=hidden]').attr('id');

                        //----category-----
                        var categoryList = $('#<%= hfCategoryList.ClientID %>').val().split(',');
                        var selectCategory = "<select class='opt-cat' Id='ddloptcat" + idCount.toString() + "' style='width:100%; height: 29px; border-radius: 3px; font-size: 12px;'>";
                        for (var i = 0; i < categoryList.length; i++) {
                            var categoryArr = categoryList[i].split('-');
                            selectCategory = selectCategory + '<option value="' + categoryArr[0] + '">' + categoryArr[1] + '</option>';
                        }
                        selectCategory = selectCategory + '</select>';


                        $testHtml = "<tr class='row mb-2 seat-entry tr" + rowClass + "'><td class='col-md-2 col-6 cus-seat-no pl-4'><blink><span class='csn' style='font-size: 15px; font-weight: bold;'>" + seatNoCat[0] + "</span></blink></td><td class='col-md-3 col-6'><input class='Name pl-1' placeholder='Min 2 & Max 25 ' onkeypress='return onlyName(event)' onkeyup='onlyNameMobile(event);' autocomplete='off' style='width:100%; height: 29px; border-radius: 3px; font-size: 12px;' type='text' Id='d1' maxlength='25' /></td><td class='col-md-2 col-6'>" + selectGender + "</td><td class='col-md-2 col-6'><input class='Age pl-1' placeholder='Age' onkeypress='return onlyNumberKey(event)' autocomplete='off' style='width:100%;  height: 29px; border-radius: 3px; font-size: 12px;' type='text' Id='tbAge" + idCount.toString() + "' maxlength='2' /></td><td class='col-md-3 col-6'>" + selectCategory + "</td></tr>";
                        $('#' + journeyType[0] + 'TotalFare').parents('.totStatRow').before($testHtml); //" + farePerTicket+ "</td></tr>");
                        //  alert($testHtml);
                        $('.csn').ajBlink();

                        //--Concession Default---------------//
                        var upTicketList = $('#up-ticket-table .seat-entry');
                        $('.tr' + rowClass).slideDown(0, function () { });
                        $('#ticket-booking-' + journeyType[0] + ' .seat').slideDown(0, function () { });
                        $('#dvNoRecord').hide(0);

                        idCount++;
                    }
                    else {
                        alert('You can book maximum ' + PassangerNo + ' seats');
                    }


                    //-----------------------------
                    var PassangerName = $('#<%= hdnname.ClientID %>').val();
                    $("#d1").val(PassangerName)
                    var Passangerage = $('#<%= hdnage.ClientID %>').val();
                    $("#age1").val(Passangerage)
                    var chkpassengertype = $('#<%=hfPassangertype.ClientID %>').val();
                    if (chkpassengertype == "F") {
                        $("#d1").val(PassangerName).add().prop("disabled", true)
                        $("#age1").val(Passangerage).add().prop("disabled", true)
                    }
                    if (chkpassengertype == "J") {
                        $("#d1").val(PassangerName).add().prop("disabled", true)
                        $("#age1").val(Passangerage).add().prop("disabled", true)
                    }

                    $('#<%= hdncheck.ClientID %>').val("");

                    //-----------------------------

                }
            });




            //Chesk On Proceed Button Click
            $('#<%= lbtnProceed.ClientID %>').click(function () {
                var fare = $("#<%= hdfare.ClientID %>").val();
                customerData = "";
                $('.seat-entry').each(function () {
                    $this = $(this);
                    // alert(upFarePerTicketf);
                    var jaourneyTypeId = $this.parents('table').attr('id').split('-');
                    customerData = customerData + '|' + $this.children('.cus-seat-no').text() + ',' + $this.find('.Name').val() + ',' + $this.find('.opt-gender').val() + ',' + $this.find('.Age').val() + ',' + $this.find('.opt-cat').val() + ',' + jaourneyTypeId[0] + ',' + fare;
                });
                $('#<%= hfCustomerData.ClientID %>').val(customerData.substr(1, customerData.length - 1));
                var PassangerNo = $('#<%= hfNoOfPassanger.ClientID %>').val();
                var selectedUpSeat = $('#div-seat-table-up .selected').length;
                //if (selectedUpSeat < PassangerNo) {
                if (selectedUpSeat == 0) {
                    alert('Please select seat(s).\nYou can select maximum ' + PassangerNo + ' seat.');
                    return false;
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
                    alert('Please fill all Passanger\'s name/age correctly!');
                    return false;
                }
            });
            //Check On Form Submit
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
            // alert(SeatDetail);
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
                $('#seatpanel').css({ 'width': 215 });
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
            //  $('#seat-desc-' + journeyType + ' tr > td:nth-child(' + Cols + ')').nextAll().remove();
            //  $('#seat-desc-' + journeyType + ' tr > td:nth-child(' + Cols + ')').nextAll().remove();
        }


        function popUp() {
            popupWindow = window.open('../TermsAndCondotions.htm', 'name', 'width=850,height=900');
        }

    </script>
    <script type="text/javascript">

        $(document).ready(function () {
            // $('[data-toggle="tooltip"]').tooltip();
            $('#ticket-booking-up' + ' .seat').hide();
            $('#dvNoRecord').show();

            $('#tblrow').hide();
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hfIsRoundTrip" runat="server" />
    <asp:HiddenField ID="hfSeatStructUp" runat="server" />
    <asp:HiddenField ID="hfSeatStructDown" runat="server" />
    <asp:HiddenField ID="hfCustomerData" runat="server" />
    <asp:HiddenField ID="hfCategoryList" runat="server" />
    <asp:HiddenField ID="hfNoOfPassanger" runat="server" />
    <asp:HiddenField ID="hidtoken" runat="server" />
    <asp:HiddenField ID="hdnname" runat="server" />
    <asp:HiddenField ID="hdnage" runat="server" />
    <asp:HiddenField ID="hdngender" runat="server" />
    <asp:HiddenField ID="hdngender1" runat="server" />
    <asp:HiddenField ID="hdnchkvalue" runat="server" />
    <asp:HiddenField ID="hdncheck" runat="server" />
    <asp:HiddenField ID="hfPassangertype" runat="server" />
    <asp:HiddenField ID="hfTotalFareDown" Value="0" runat="server" />
    <asp:HiddenField ID="hdfare" Value="0" runat="server" />
    <asp:Panel ID="pnlLayout" runat="server" Visible="true">
        <div class="container-fluid py-3">
            <div class="row shadow mb-2">
                <div class="col-10 offset-1 pt-3">
                    <ul id="progressbar">
                        <li class="text-center active" id="search_pr"><strong>Search</strong></li>
                        <li class="text-center active" id="seat_pr"><strong>Seat Selection</strong></li>
                        <li class="text-center" id="confirm_pr"><strong>Confirmation</strong></li>
                        <li class="text-center" id="payment_pr"><strong>Payment</strong></li>
                        <li class="text-center" id="status_pr"><strong>Finish</strong></li>
                    </ul>
                </div>
            </div>
            <div class="card">
                <div class="row">
                    <div class="col-lg-3 py-3" style="background: #f3fff1;">
                        <h4 class="mb-1">You are booking for</h4>
                        <p class="mb-0 text-left" style="font-size: 13px;">
                            <span class="text-muted">From</span>
                            <asp:Label ID="lblFromStation" runat="server" CssClass="text-uppercase" Text="NA"></asp:Label><br />
                            <span class="text-muted">To</span>
                            <asp:Label ID="lblToStation" runat="server" CssClass="text-uppercase" Text="NA"></asp:Label><br />
                            <span class="text-muted">Service</span>
                            <asp:Label ID="lblServiceType" runat="server" CssClass="text-uppercase" Text="NA"></asp:Label><br />
                            <span class="text-muted">Date</span>
                            <asp:Label ID="lblDate" runat="server" CssClass="text-uppercase" Text="NA"></asp:Label>
                            <asp:Label ID="lblDeparture" runat="server" CssClass="text-uppercase pl-1" Text="NA"></asp:Label>
                        </p>
                    </div>
                    <div class="col-lg-9" style="min-height: 50vh;">
                        <div class="row py-3">
                            <div class="col-xl-4">
                                <div id='up-journey' style="width: 100%;">
                                    <table cellpadding='0' cellspacing='0' style="margin-left: 120px; padding: 0;">
                                        <tr>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id='div-seat-table-up' style="background: #f4f5f8; border: solid 1px #e2e7f8;">
                                                    <div id="seatpanel" style="/* border: 1px solid rgb(0, 132, 215); */padding: 9px; border-radius: 4px; /* height: 281px; */width: 176px;"
                                                        class="panel-primary pl-0">
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

                                <div class="col-xl-10 offset-1">
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
                                </div>
                            </div>
                            <div class="col-xl-8">
                                <div class="row align-items-center pl-3 pr-3">
                                    <div class="col-xl-12">
                                        <div id="ticket-booking-up">
                                            <div class="seat" style="background: #f4f5f8; border: solid 1px #e2e7f8;">
                                                <table id='up-ticket-table'>
                                                    <tr class="row ticket-tbl-first-row" style="padding: 6px;">
                                                        <td class="col-md-2 col-6 pl-3">Seat No
                                                        </td>
                                                        <td class="col-md-3 col-6">Passenger Name

                                                        </td>
                                                        <td class="col-md-2 col-6">Gender

                                                        </td>
                                                        <td class="col-md-2 col-6">Age

                                                        </td>
                                                        <td id="Td1" runat="server" class="col-md-3 col-6">Concession </td>
                                                    </tr>
                                                    <tr class="totStatRow" style="font-family: Verdana; font-size: 9pt">
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td class="text-right" style="font-family: Verdana; font-size: 9pt"></td>
                                                        <td id="Td2" runat="server">
                                                            <strong><span id="upTotalFare" class="text-right" style="font-size: 9pt; font-family: Verdana"></span></strong>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div id="dvNoRecord">
                                            <div class="col-md-12 col-lg-12 py-5">
                                                <h2 class="text-muted border-0 text-center">Click on an Available seat to proceed for booking
                                                </h2>
                                            </div>
                                        </div>

                                        <div class="row py-3 mt-2" style="background: #f4f5f8; border: solid 1px #e2e7f8;">
                                            <div class="col-md-4">
                                                <div class="form-group p-0">
                                                    <span class="form-label">Boarding From</span>
                                                    <asp:DropDownList ID="ddlBoardingfrom" runat="server" CssClass="form-control form-control-sm">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group p-0">
                                                    <span class="form-label">Mobile No</span>
                                                    <asp:TextBox ID="txtMobileNo" runat="server" onkeypress="return onlyNumberKey(event)" CssClass="form-control form-control-sm" Style="text-transform: uppercase"
                                                        placeholder="Mobile No" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group p-0">
                                                    <span class="form-label">Email Id<span style="color: #c6c2c2;"> (Optional)</span></span>
                                                    <asp:TextBox ID="txtemialid" runat="server" CssClass="form-control form-control-sm" Style="text-transform: uppercase"
                                                        placeholder="Email Id" MaxLength="100"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="row align-items-center pl-3 pr-3 pt-4">
                                    <div class="col-xl-12 col-md-12 text-right">
                                        <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="btn btn-icon btn-danger mr-2" OnClick="lbtnclose_Click">
                                            <i class="fa fa-times"></i> Cancel
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnProceed" runat="server" CssClass="btn btn-icon btn-success" OnClick="lbtnProceed_Click">
                                            <i class="fa fa-check"></i> Proceed
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlConcession" runat="server" Visible="false">
        <div class="container-fluid my-3">
            <div class="row shadow mb-2">
                <div class="col-10 offset-1 pt-3">
                    <ul id="progressbar">
                        <li class="text-center active" id="search_pr"><strong>Search</strong></li>
                        <li class="text-center active" id="seat_pr"><strong>Seat Selection</strong></li>
                        <li class="text-center" id="confirm_pr"><strong>Confirmation</strong></li>
                        <li class="text-center" id="payment_pr"><strong>Payment</strong></li>
                        <li class="text-center" id="status_pr"><strong>Finish</strong></li>
                    </ul>
                </div>
            </div>
            <div class="card">
                <div class="row">
                    <div class="col-lg-3 py-3" style="background: #f3fff1;">
                        <h3 class="mb-1 text-left">You are booking for</h3>
                        <p class="mb-0 text-left" style="font-size: 15px;">
                            <span class="text-muted">From </span>
                            <asp:Label ID="lblFromStationName2" runat="server" Text="NA"></asp:Label><br />
                            <span class="text-muted">To</span>
                            <asp:Label ID="lblToStationName2" runat="server" Text="NA"></asp:Label><br />
                            <span class="text-muted">Service </span>
                            <asp:Label ID="lblServiceType2" runat="server" Text="NA"></asp:Label><br />
                            <span class="text-muted">Date </span>
                            <asp:Label ID="lblDateTime2" runat="server" Text="NA"></asp:Label>
                        </p>
                    </div>
                    <div class="col-lg-9 py-3 px-5" style="min-height: 50vh;">
                        <h3 class="mb-1 text-left">
                            <asp:Label ID="lblAbout" runat="server" Text="You have selected seat(s) with following details."></asp:Label></h3>
                        <asp:GridView ID="gvSeats" runat="server" GridLines="None" AutoGenerateColumns="false" OnRowDataBound="gvSeats_RowDataBound" ShowHeader="false" ForeColor="Black" Width="100%"
                            AllowPaging="true" DataKeyNames="SeatNo,Name,Age,Gender,Concession,JourneyType,Fare,OnlineVerificationYN,IdVerificationYN,Idverification,DocumentVerificationYN,DocumentVerification,ConcessionName">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div class="row py-4 border-bottom">
                                            <div class="col-lg-6">
                                                <b>Seat No.   <%# Eval("SeatNo") %> :</b> <%# Eval("Name") %>, <%# Eval("Gender") %>, <%# Eval("Age") %> Years
                                                <br />
                                                Concession - <%# Eval("ConcessionName") %>
                                            </div>
                                            <div class="col-lg-6">
                                                <p style="font-size: 12px;" class="mb-0">
                                                    <asp:Label ID="lblheading" runat="server" Text="" Visible="false"></asp:Label>
                                                </p>
                                                <asp:TextBox ID="tbpass_docid" runat="server" CssClass="form-control" Style="text-transform: uppercase; font-size: 15px; height: 36px !important; width: 250px;" Text="" Visible="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblseaterror" runat="server" Style="color: red; font-size: 10pt;" Visible="false"></asp:Label>
                        <div class="col text-right">
                            <asp:LinkButton ID="lbtnclose" runat="server" CssClass="btn btn-danger" OnClick="lbtnclose_Click"> <i class="fa fa-times "></i> Cancel </asp:LinkButton>
                            <asp:LinkButton ID="lbtnConfirmConcession" runat="server" CssClass="btn btn-success" OnClick="lbtnConfirmConcession_Click"> <i class="fa fa-check"></i> Confirm & Proceed  </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
<div class="row">
            <cc1:ModalPopupExtender ID="mpTripOver" runat="server" PopupControlID="pnlTripOver"
                CancelControlID="Button3" TargetControlID="Button5" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlTripOver" runat="server" Style="position: fixed;">
                <div class="modal-dialog" style="min-width: 360px">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-body text-left">
                            <asp:Label runat="server" ID="lblMsg">Sorry, Booking has been closed for selected service. Please change your selection and try again.</asp:Label>
                            <div style="width: 100%; margin-top: 20px; text-align: right;">
                                <asp:LinkButton ID="lbtnTripOver" runat="server" PostBackUrl="~/Traveller/dashboard.aspx" CssClass="btn btn-danger btn-sm"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button3" runat="server" Text="" />
                    <asp:Button ID="Button5" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
</asp:Content>

