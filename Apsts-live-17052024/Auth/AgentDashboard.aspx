<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/AgentMaster.master" AutoEventWireup="true" CodeFile="AgentDashboard.aspx.cs" Inherits="Auth_AgentDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
    <style>
        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .border-right {
            border-right: 1px solid #e6e6e6;
        }

        .btn-QuickLinks {
            color: #1d4ab1;
            background-color: transparent;
            background-image: none;
            border-width: 2px;
            border-color: #236bb3;
            font-weight: 600 !important;
            border-radius: 5px !important;
            font-size: 14px;
        }

        .card h4 {
            font-size: 0.9rem;
        }

        .h6, h6 {
            font-size: 0.8rem;
        }

      
    </style>

    <link rel="stylesheet" href="../assets/css/jquery-ui.css" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$=tbfromstation]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "AgentDashboard.aspx/source_destination",
                        data: "{'stationText':'" + request.term + "','fromTo':'F'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {

                        }
                    });
                }
            });
            $("[id$=tbtostation]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "AgentDashboard.aspx/source_destination",
                        data: "{'stationText':'" + request.term + "','fromTo':'T'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {

                        }
                    });
                }
            });
            

        });
        function close() {
            alert('ok');
        }
    </script>
  <script>
      $(document).ready(function () {
          var todayDate = new Date().getDate();
          //var lastDayOfMonth = new Date(today.getFullYear(), today.getMonth() , 0);
          var endDate = $("[id$=hdmaxdate]").val();
          var endD = new Date(new Date().setDate(todayDate + parseInt(endDate - 1)));
          $('[id*=tbjourneydate]').datepicker({
              startDate: "dateToday",
              endDate: endD,
              changeMonth: true,
              changeYear: false,
              format: "dd/mm/yyyy",
              autoclose: true
          });
      });
  </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdmaxdate" runat="server" />
  
    <div class="content mt-3">
        <div class="row">
            <div class="col-4">
                <div class="card card-stats mb-3">
                    <div class="row">
                        <div class="col-lg-3 col-sm-3 profile-widget-name" style="padding: 30px;">
                            <div class="follow-ava" style="margin-top: -5px;">
                              <img src="../images/person-icon-blue.png"  alt="" style="width: 50px; height: 50px;"/>
                            </div>
                        </div>
                        <div class="col-lg-9 col-sm-9 follow-info" style="line-height: 25px; padding-top: 11px; font-size: 10pt;">
                            <asp:Label ID="lblagname" runat="server" Text="N/A"></asp:Label>
                            <br />
                            <i class="fa fa-envelope"></i>
                            <asp:Label ID="lblagemail" runat="server" Text="N/A"></asp:Label>
                            <br />
                            <i class="fa fa-mobile"></i>
                            <asp:Label ID="lblagmob" runat="server" Text="N/A"></asp:Label>
                            <br />
                            <i class="fa fa-clock-o"></i> Valid upto
                                    <asp:Label ID="lblvalidto" runat="server" Text=""></asp:Label>
                            <asp:LinkButton ID="lbtnRenew1" Visible ="false"  runat ="server"  OnClientClick="return ShowLoading()" CssClass="btn-link"> Renew Now</asp:LinkButton>
                        
                        </div>
                    </div>

                    <%--   

                    <div class="row" style="padding-left: 45px; padding-bottom: 19px;">
                        <div class="col-4 border-right">
                            <h4>
                                <asp:Label ID="lbltotbooking" runat="server" Text="0"></asp:Label></h4>
                            <i class="icon_check_alt2"></i>Total Booking
                        </div>
                        <div class="col-4 border-right">
                            <h4>
                                <asp:Label ID="lbltotcancel" runat="server" Text="0"></asp:Label></h4>
                            <i class="icon_close_alt2"></i>Total Cancel 
                        </div>
                        <div class="col-4">
                            <h4>
                                <asp:Label ID="lbltotvisit" runat="server" Text="0"></asp:Label></h4>
                            <i class="icon_plus_alt2"></i>Total Visit
                        </div>
                    </div>
                         <div class="row pb-3">
                       
                    </div>
                    --%>
                   
                </div>
                <div class="card card-stats mb-3">
                    <div class="card-body text-center">
                        <h4>
                            <asp:Label ID="lblWalletBalance" runat="server" Text="0"></asp:Label>
                            <i class="fa fa-rupee"></i>
                        </h4>
                        <div>
                            <asp:Label ID="lblWalletLastUpdate" runat="server" Text="0"></asp:Label>
                        </div>
                        <asp:LinkButton ID="lbtnonlineRecharge" OnClientClick="return ShowLoading();" runat="server" OnClick="lbtnonlineRecharge_Click" CssClass="btn btn-success"> <i class="fa fa-rupee"></i> Recharge your wallet </asp:LinkButton>
                    </div>
                </div>
                <div class="card card-stats mb-3">
                    <div class="card-body">
                        <i class="fa fa-list-ul text-muted "></i>
                        <asp:Label runat="server" Text="Quick Actions" Font-Bold="true" Font-Size="Large"></asp:Label>
 <asp:LinkButton href="../Auth/UserManuals/Agent/Help Document for Agent.pdf" target="_blank" runat="server" CssClass="btn btn-success btn-sm" ToolTip="Click here for manual."><i class="fa fa-download"></i></asp:LinkButton>
                        
                        <hr />
                        <div class="row">
                            <div class="col-lg-6">
                                <asp:LinkButton ID="lbtnTripChart"  OnClientClick="return ShowLoading()" runat="server" OnClick="lbtnTripChart_Click" CssClass="btn btn-QuickLinks" Style="width: 100%; box-shadow: rgb(0 0 0 / 24%) 0px 3px 8px;">
                                    Trip Chart &nbsp; <i class="fa fa-forward"></i>
                                </asp:LinkButton>
                            </div>
                            <div class="col-lg-6">
                                <asp:LinkButton ID="lbtnCurrentBooking"  OnClientClick="return ShowLoading()" runat="server" OnClick="lbtnCurrentBooking_Click" CssClass="btn btn-QuickLinks" Style="width: 100%; box-shadow: rgb(0 0 0 / 24%) 0px 3px 8px;">
                                    Current Booking &nbsp; <i class="fa fa-forward"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-lg-6">
                                <asp:LinkButton ID="lbtnCancellation"  OnClientClick="return ShowLoading()" runat="server" OnClick="lbtnCancellation_Click" CssClass="btn btn-QuickLinks" Style="width: 100%; box-shadow: rgb(0 0 0 / 24%) 0px 3px 8px;">
                                    Cancel Tickets &nbsp; <i class="fa fa-forward"></i>
                                </asp:LinkButton>
                            </div>
                            <div class="col-lg-6">
                                <asp:LinkButton ID="lbtnspcialcancel"  OnClientClick="return ShowLoading()" runat="server" OnClick="lbtnspcialcancel_Click" CssClass="btn btn-QuickLinks" Style="width: 100%; box-shadow: rgb(0 0 0 / 24%) 0px 3px 8px;">
                                    Special Cancellation &nbsp; <i class="fa fa-forward"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-lg-6">
                                <asp:LinkButton ID="lbtnquery"  OnClientClick="return ShowLoading()" runat="server" OnClick="lbtnquery_Click" CssClass="btn btn-QuickLinks" Style="width: 100%; box-shadow: rgb(0 0 0 / 24%) 0px 3px 8px;">
                                    Ticket query &nbsp; <i class="fa fa-forward"></i>
                                </asp:LinkButton>
                            </div>
                            <div class="col-lg-6">
                                <asp:LinkButton ID="lbtnReport"  OnClientClick="return ShowLoading()" runat="server" OnClick="lbtnReport_Click" CssClass="btn btn-QuickLinks" Style="width: 100%; box-shadow: rgb(0 0 0 / 24%) 0px 3px 8px;">
                                    Transaction Report &nbsp; <i class="fa fa-forward"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="row mt-2 mb-4">
                           <%-- <div class="col-lg-6">
                                <asp:LinkButton ID="lnkChngePwd"  OnClientClick="return ShowLoading()" runat="server" OnClick="lnkChngePwd_Click" CssClass="btn btn-QuickLinks" Style="width: 100%; box-shadow: rgb(0 0 0 / 24%) 0px 3px 8px;">
                                    Change Password &nbsp; <i class="fa fa-forward"></i>
                                </asp:LinkButton>
                            </div>--%>
                            <div class="col-lg-6">
                                <asp:LinkButton ID="lbtndailyaccount"  OnClientClick="return ShowLoading()" runat="server" OnClick="lbtndailyaccount_Click" CssClass="btn btn-QuickLinks" Style="width: 100%; box-shadow: rgb(0 0 0 / 24%) 0px 3px 8px;">
                                    Daily Account &nbsp; <i class="fa fa-forward"></i>
                                </asp:LinkButton>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="col-8">
                <div class="card card-stats mb-3">
                    <div class="card-body">
                        <div class="row m-0">
                            <div class="col-12">
                                <h6 class="mb-1">
                                    <asp:Label ID="lbltodaysummary" runat="server"></asp:Label>
                                </h6>
                                <div class="row">
                                    <div class="col-4 border-right">
                                        <div class="row m-0">
                                            <div class="col-12">
                                                <h4 class="mb-1 text-muted">Online Booking</h4>
                                                <div class="row m-0">
                                                    <div class="col-6 border-right">
                                                        <h6 class="card-title  mb-0">Ticket&nbsp;<br />
                                                            <asp:Label ID="lbltodaybooking" runat="server" data-toggle="tooltip" data-placement="bottom" title="tatal online ticket" CssClass="h5 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label>

                                                        </h6>

                                                    </div>
                                                    <div class="col-6 border-right">
                                                        <h6 class="card-title  mb-0">Amount &nbsp;<br />
                                                            <asp:Label ID="lbltodaybookingamt" runat="server" data-toggle="tooltip" data-placement="bottom" title="total online ticket amount" CssClass="h5 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label>

                                                        </h6>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-2 border-right">
                                        <div class="row m-0">
                                            <div class="col-12">
                                                <h4 class="mb-1 text-muted">Commission</h4>
                                                <div class="row m-0">
                                                    <div class="col-12">
                                                        <h6 class="card-title  mb-0">Amount&nbsp;<br />
                                                            <asp:Label ID="lbltodaycomamt" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Commission" CssClass="h5 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label>

                                                        </h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-2 border-right">
                                        <div class="row m-0">
                                            <div class="col-12">
                                                <h4 class="mb-1 text-muted">Cancellation </h4>
                                                <div class="row m-0">
                                                    <div class="col-12 ">
                                                        <h6 class="card-title  mb-0">Amount&nbsp;<br />
                                                            <asp:Label ID="lbltodayrefamt" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Cancellation" CssClass="h5 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label>

                                                        </h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-4 border-right">
                                        <div class="row m-0">
                                            <div class="col-12">
                                                <h4 class="mb-1 text-muted">Current Booking</h4>
                                                <div class="row m-0">
                                                    <div class="col-6 border-right">
                                                        <h6 class="card-title mb-0">Ticket&nbsp;<br />
                                                            <asp:Label ID="lblcurrticket" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Current Booking Tickets" CssClass="h5 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label></h6>

                                                    </div>
                                                    <div class="col-6">
                                                        <h6 class="card-title mb-0">Amount &nbsp;<br />
                                                            <asp:Label ID="lblcurrtripamt" runat="server" data-toggle="tooltip" data-placement="bottom" title="Total Current Booking Amount" CssClass="h5 font-weight-bold mb-0 text-right float-right" Text="0"></asp:Label>

                                                        </h6>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12">
                                            </div>
                                        </div>
                                    </div>
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card card-stats mb-3">
                    <div class="card-body" style="min-height: 535px;">
                        <asp:Panel ID="pnlbook" runat="server" Visible="true">
                            <i class="fa fa-ticket text-muted "></i>
                            <asp:Label runat="server" Text="Online Ticket Booking" Font-Bold="true" Font-Size="Large"></asp:Label>
                            <p style="font-size: 15px; margin: 0px; float: right;" class="text-danger font-weight-bold"><span class="text-danger">* &nbsp; Fields are mandatory</span></p>
                            <hr />

                               <div class="row pl-lg-3 pr-lg-3">
                            <div class="col-lg-3">
                                <span class="form-control-label">From Station <span class="text-danger">*</span></span>
                                <asp:TextBox ID="tbfromstation" CssClass="form-control form-control-sm search-box" AutoComplete="Off" Placeholder="From Station" runat="server" MaxLength="20"></asp:TextBox>

                                <cc1:FilteredTextBoxExtender ID="ft_tbfromstation" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbfromstation" ValidChars=" ()" />

                            </div>
                            <div class="col-lg-3">
                                <span class="form-control-label">To Station <span class="text-danger">*</span></span>
                                <asp:TextBox ID="tbtostation" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="To Station" runat="server" MaxLength="20"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="ft_tbtostation" runat="server" FilterType="UppercaseLetters, LowercaseLetters,Custom" TargetControlID="tbtostation" ValidChars=" ()" />
                            </div>
                            <div class="col-lg-2">
                                <span class="form-control-label">Journey Date </span>
                                <asp:TextBox ID="tbjourneydate" CssClass="form-control form-control-sm" AutoComplete="Off" Placeholder="DD/MM/YYYY" runat="server" MaxLength="10"></asp:TextBox>
                                  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom"
                                                ValidChars="/" TargetControlID="tbjourneydate" />

                            </div>
                            <div class="col-lg-3">
                                <span class="form-control-label">Service Type</span>
                                <asp:DropDownList ID="ddlServiceType" CssClass="form-control form-control-sm" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-1 pt-4">
                                <asp:LinkButton ID="lbtnSearchService" runat="server" data-toggle="tooltip" data-placement="top"
                                     OnClick="lbtnSearchService_Click" ToolTip="Click here to search services"
                                     CssClass="btn btn-success btn-icon-only btn-sm" strle="z-i" OnClientClick="return ShowLoading();">
       
                         <i class="fa fa-search mt-2"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbtnRestSearchService" runat="server" data-toggle="tooltip"
                                     data-placement="top" ToolTip="Click here to reset search values"
                                    OnClientClick="return ShowLoading();" CssClass="btn btn-danger btn-icon-only btn-sm float-right">
                                            <i class="fa fa-undo mt-2"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                            <hr />
                            <div class="row px-3">
                                <div class="col-lg-12">
                                     <asp:GridView ID="gvService" runat="server" GridLines="None" ShowHeader="false" AutoGenerateColumns="false" Width="100%" OnRowCommand="gvService_RowCommand" OnRowDataBound="gvService_RowDataBound"
                                    DataKeyNames="openclose,dsvcid,servicename,strpid,frstonid,tostonid,depttime,arrtime,tripdirection,srtpid,servicetypename,layout,totalseat,routeid,routename,s_code,distance,fare,midstations,from_station_name,to_station_name">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="card p-2 border ">
                                                    <div class="row px-3 pt-1">
                                                        <div class="col">
                                                            <span class="h3 font-weight-bold mb-0"><%# Eval("dsvcid") %><%# Eval("tripdirection") %><%# Eval("strpid") %> | <%# Eval("servicetypename") %>  </span>
                                                        </div>
                                                        <div class="col-auto text-right ">
                                                            <span class="h5 font-weight-bold mb-0"><span class="h6 text-muted">Departure </span><%# Eval("depttime") %></span>
                                                        </div>
                                                    </div>
                                                    <div class="row px-3 pb-2">
                                                        <div class="col">
                                                            <span class="h5 font-weight-bold mb-0"><%# Eval("totalavailablseats") %> <span class="h6 text-muted">seats available out of </span><%# Eval("totalseat") %><span class="h6 text-muted"> seats</span> </span>
                                                        </div>
                                                        <div class="col-auto text-right ">
                                                            <span class="h5 font-weight-bold mb-0"><span class="h6 text-muted">Arrival </span><%# Eval("arrtime") %></span>
                                                        </div>
                                                    </div>
                                                    <div class="row px-3 pb-1">
                                                        <div class="col">
                                                            <button type="button" class="btn btn-sm" data-container="body" data-toggle="popover" data-color="default" data-placement="top" data-content='<%# Eval("midstations") %>'>
                                                                <i class="fa fa-building mr-1"></i>Mid Stations
                                                            </button>
                                                            <button type="button" class="btn btn-sm" data-container="body" data-toggle="popover" data-color="default" data-placement="top" data-content='<%# Eval("amenity") %>'>
                                                                <i class="fa fa-beer mr-1"></i>Amenities
                                                            </button>
                                                        </div>
                                                        <div class="col-auto text-right ">
                                                            <span class="card-title text-default mr-3 font-weight-bold"><i class="fa fa-rupee-sign mr-1"></i><%# Eval("fare") %> Per Seat</span>
                                                            <asp:LinkButton ID="lbtnOrgView" OnClientClick="return ShowLoading();" runat="server" CssClass="btn btn-icon btn-primary btn-sm"
                                                                CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="BOOKTICKET" data-toggle="tooltip" data-placement="bottom" title="Proceed to booking">
                                                    <i class="fa fa-check"></i> BOOK
                                                            </asp:LinkButton>

                                                            
                                                            <asp:LinkButton ID="lbtnclose" runat="server" Enabled="false" CssClass="btn btn-icon btn-secondary btn-sm mb-2"
                                                                 CommandName="Booking Closed"> Closed</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                    <asp:Panel ID="pnlNoService" runat="server">
                                        <div class="row">
                                            <div class="col-lg-12 text-center mt-4">
                                                <i class="ni ni-bus-front-12 ni-5x text-muted"></i>
                                            </div>
                                            <div class="col-lg-12 text-center mt-4">
                                                <span class="h2 font-weight-bold text-muted mb-0">Thanks for being here</span>
                                                <h5 class="card-title text-muted mb-0">
                                                    <asp:Label ID="lblNoServiceMsg" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlvalidityExpire" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-lg-12 text-center mt-4">
                                    <i class="ni ni-bus-front-12 ni-5x text-muted"></i>
                                </div>
                                <div class="col-lg-12 text-center mt-4">
                                    <span class="h2 font-weight-bold text-muted mb-0">Account Validity</span>
                                    <h5 class="card-title text-muted mb-4">
                                        <asp:Label ID="lblaccvalidity" runat="server"></asp:Label>
                                        <br />

                                    </h5>
                                                <asp:Label ID="lblReferenceNo" CssClass="text-success" runat="server" Style="font-size: 18px;" Font-Bold="true" Text=""></asp:Label>
                                    <asp:LinkButton ID="lbtnRenew" OnClientClick="return ShowLoading()" OnClick="lbtnRenew_Click"  runat="server" data-toggle="tooltip" data-placement="top" Style="padding: 8px;" ToolTip="Click here to Renew Account Validity" CssClass="btn btn-success">
                                            <i class="fa fa-undo"></i> Renew Validity
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">

            <cc1:ModalPopupExtender ID="mpFirst" runat="server" CancelControlID="btnMPfirstClose"
                TargetControlID="Button1" PopupControlID="pnlfirst" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlfirst" Style="display: none" runat="server">
                <div class="card">
                    <div class="card-header">
                        <h4 class="card-title m-0" style="font-size: 20px;">Information
                        </h4>
                    </div>
                    <div class="card-body">
                        <div class="col-lg-12 text-center">
                            <i class="fa fa-rupee" style="font-size: 100px;"></i>
                            <p style="font-size: 14pt; color: #dc3535; font-weight: bold;">
                                <asp:Label ID="lblfirstmsg" runat="server" Text=""></asp:Label>
                            </p>
                        </div>
                    </div>
                    <div class="card-footer" style="text-align: right;">
                        <asp:LinkButton ID="btnMPfirstClose" runat="server" CssClass="btn btn-danger btn-sm font-weight-bold px-3" Style="border-radius: 4px;"> <i class="fa fa-times"></i> OK </asp:LinkButton>
                    </div>
                </div>
                <div style="visibility: hidden; height: 0px;">
                    <asp:Button ID="Button1" runat="server" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpError" runat="server" PopupControlID="pnlError" CancelControlID="Button2"
                TargetControlID="Button3" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlError" runat="server" Style="position: fixed;">
                <div class="card" style="min-width: 350px;">
                    <div class="card-header" style="background-color: #e4273b; color: White;">
                        <h4 class="card-title">
                            <span style="font-size: 11pt;">Please Check & Correct</span>
                            <asp:LinkButton ID="lbtnerrorclose1" runat="server"  ToolTip="Close" Style="float: right; color: white; padding: 0px;"> <i class="fa fa-times"></i>  </asp:LinkButton>
                        </h4>
                    </div>
                    <div class="card-body" style="min-height: 100px;">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblerrmsg" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:LinkButton ID="lbtnerrorclose" runat="server"  CssClass="btn btn-warning" Style="height: 30px; width: 90px; padding-top: 4px; font-size: 10pt; border-radius: 4px;"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button3" runat="server" Text="" />
                    <asp:Button ID="Button2" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpAgentVerification" runat="server" PopupControlID="PanelAgentVerification"
                CancelControlID="lbtnVerificationNo" TargetControlID="Button1" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="PanelAgentVerification" runat="server" Style="position: fixed;">
                <div class="card" style="min-width: 300px; max-width: 90vw; max-height: 90vh;">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-md-12 text-left" style="font-size: 18px; font-weight: 600;">
                                Please Confirm
                            </div>
                        </div>
                    </div>

                    <div class="card-body text-left overflow-auto" style="min-height: 100px; max-width: 600px;">
                        <div class="row pb-3 mt-2">
                            <div class="col-lg-2"></div>
                            <div class="col-lg-8 text-center">
                                <asp:Label ID="lblconfirmmsg" runat="server" ForeColor="Red" Text=""
                                    Style="font-weight: 600;"></asp:Label>
                            </div>
                            <div class="col-lg-2"></div>
                            <div class="col-lg-2"></div>
                            <div class="col-lg-8 mt-2 text-center">
                                <asp:LinkButton ID="lbtnVerificationNo" runat="server" CssClass="btn btn-danger"> <i class="fa fa-times"></i> No </asp:LinkButton>
                                <asp:LinkButton ID="lbtnVerificationYes" runat="server" OnClick="lbtnVerificationYes_Click" CssClass="btn btn-success"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                            </div>
                            <div class="col-lg-2"></div>
                        </div>
                    </div>

                </div>
                <br />
                <div style="visibility: hidden; height: 0px;">
                    <asp:Button ID="Button4" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpconfirm" runat="server" PopupControlID="pnlconfirm"
                CancelControlID="LinkButton3" TargetControlID="Button5" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlconfirm" runat="server" Style="position: fixed;">
                <div class="card" style="width: 400px; background: white; padding: 17px; border-radius: 4px;">

                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <h3 class="mt-1 mb-2" style="font-size: 21px;">Congratulations
                        </h3>
                        <asp:Label ID="lblsucessmsg" runat="server" ForeColor="Black" Style="font-size: 17px; line-height: 23px;"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-success" Style="height: 30px; width: 90px; padding-top: 4px; font-size: 10pt; border-radius: 4px;"> <b>OK</b></asp:LinkButton>
                        </div>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="Button5" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>


