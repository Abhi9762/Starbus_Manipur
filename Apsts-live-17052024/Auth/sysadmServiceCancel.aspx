<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master" AutoEventWireup="true" CodeFile="sysadmServiceCancel.aspx.cs" Inherits="Auth_sysadmServiceCancel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .card-height {
            min-height: 15vh;
            margin-bottom: 10px;
            box-shadow: rgb(0 0 0 / 10%) 5px 5px 15px 3px;
        }

        p {
            font-size: 9pt;
            font-weight: bold;
        }

        .borderless th {
            border: none;
        }

        hr {
            margin: 10px !important;
        }

        .table td, .table th {
            padding: 5px 3px;
            vertical-align: middle;
            border-top: none;
            font-size: 10pt;
        }

        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .text-muted {
            color: #868e96 !important;
            font-size: 9pt;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            var todayDate = new Date().getDate();
            var endD = new Date(new Date().setDate(todayDate + 30));
            var currDate = new Date();

            $('[id*=txtblockdate]').datepicker({
                startDate: "dateToday",
                endDate: endD,
                changeMonth: true,
                changeYear: false,
                format: "dd/mm/yyyy",
                autoclose: true
            });



        });
        function close() {
            alert('ok');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidtoken" runat="server" />
    <div class="container-fluid" style="padding-top: 20px; padding-bottom: 20px;">
        <div class="row">
            <div class="col-lg-4">
                <div class="card card-height" style="min-height: 5vh; margin-bottom: 10px">
                    <div class="card-body" style="padding: 10px; /* text-align: center; */height: 65px; vertical-align: middle;">
                        <div class="row">
                            <div class="col-lg-6 pl-3">
                                Total Service
                            </div>
                            <div class="col-md-6 text-right pr-4">
                                <asp:LinkButton ID="p_totservice" CssClass="text-success" runat="server" CommandArgument="0"
                                    Text="0" Font-Underline="false" Font-Size="16pt" Font-Bold="true" CommandName="O"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="card card-height" style="min-height: 5vh; margin-bottom: 10px">
                    <div class="card-body" style="padding: 10px; /* text-align: center; */height: 65px; vertical-align: middle;">
                        <div class="row">
                            <div class="col-lg-6 pl-3">
                                Blocked Services
                            </div>
                            <div class="col-md-6 text-right pr-4">
                                <asp:LinkButton ID="p_blockservice" CssClass="text-success" runat="server" CommandArgument="0"
                                    Text="0" Font-Underline="false" Font-Size="16pt" Font-Bold="true" CommandName="O"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="card card-height" style="min-height: 5vh; margin-bottom: 10px">
                    <div class="card-body" style="padding: 10px; /* text-align: center; */height: 65px; vertical-align: middle;">
                        <div class="row">
                            <div class="col-lg-6 pl-3">
                                Service Blocked in last 30 days
                            </div>
                            <div class="col-md-6 text-right pr-4">
                                <asp:LinkButton ID="p_blcklast" CssClass="text-success" runat="server" CommandArgument="0"
                                    Text="0" Font-Underline="false" Font-Size="16pt" Font-Bold="true" CommandName="O"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="row">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlsevices" runat="server" Visible="true">
                            <div class="card  card-height">
                                <div class="card-header">
                                    <div class="row">

                                        <div class="col-md-1 text-right">
                                            <b>Block Date</b>
                                        </div>
                                        <div class="col-md-1 pl-2">
                                            <asp:TextBox class="form-control" runat="server" ID="txtblockdate" MaxLength="10"
                                                placeholder="DD/MM/YYYY" Text="" Style="font-size: 9pt; height: 30px; display: inline; width: 100px; padding: 5px;"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom"
                                                TargetControlID="txtblockdate" ValidChars="/" />
                                        </div>
                                        <div class="col-md-1 p-0 text-right">
                                            <b>Service Type</b>
                                        </div>
                                        <div class="col-md-2 pl-2 pr-2">
                                            <asp:DropDownList ID="ddlservicetype" runat="server" Style="height: 30px; font-size: 9pt;"
                                                ToolTip="Service Type" AutoPostBack="false" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:LinkButton ID="btnsearch" OnClick="btnsearch_Click" runat="server" CssClass="btn btn-sm btn-success"> <i class="fa fa-search"></i> Search </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnreset" runat="server" CssClass="btn btn-sm btn-danger"> <i class="fa fa-refresh"></i> Reset </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-4" style="border-right: 1px solid grey;">
                                            <h5 class="text-left pt-2 pb-1 pl-3" style="font-weight: bold; color: #7b7474;">List of Blocked Services</h5>
                                            <div class="row">
                                                <div class="col-md-5 offset-5">
                                                    <asp:TextBox class="form-control" runat="server" ID="txtblockedservicecode" MaxLength="10"
                                                        placeholder="Service Code" Text="" Style="font-size: 9pt; height: 30px; margin-left: 16px;"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:LinkButton ID="lbtnblockedclick" OnClick="lbtnblockedclick_Click" runat="server" CssClass="btn btn-sm btn-success ml-3"> <i class="fa fa-search"></i>  </asp:LinkButton>
                                                </div>
                                                <div class="col-md-12">
                                                    <hr />
                                                    <asp:GridView ID="grdblockedservice" runat="server" AutoGenerateColumns="False" CssClass="table"
                                                        AllowPaging="true" PageSize="7" OnPageIndexChanging="grdblockedservice_PageIndexChanging" DataKeyNames="service_code,servicename,strpid,two_way,journey_type,departure_time,src,des"
                                                        GridLines="None" Font-Names="Verdana" Font-Size="8pt" ShowHeader="false">
                                                        <Columns>
                                                            <asp:TemplateField ShowHeader="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl2" runat="server" Text='<%# Eval("service_code") %>'></asp:Label>/<asp:Label ID="lblSTRP_ID" runat="server" Text='<%# Eval("strpid") %>'></asp:Label>
                                                                    <asp:Label ID="lblJourneytype" runat="server" Text='<%# Eval("journey_type") %>'></asp:Label><br />
                                                                    (<asp:Label ID="lbl122" runat="server" Text='<%# Eval("src") %>'></asp:Label>
                                                                    -
                                                                    <asp:Label ID="lbl1122" runat="server" Text='<%# Eval("des") %>'></asp:Label>)
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ShowHeader="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl22" runat="server" Text='<%# Eval("departure_time") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%-- <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnView" runat="server" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                        CssClass="btn btn-sm btn-warning" Style="font-size: 9pt; border-radius: 6px; margin-top: -6px;"> <i class="fa fa-eye" title="View Tickets"></i> </asp:LinkButton>

                                                                    <asp:LinkButton ID="btnselect" runat="server" CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                        CssClass="btn  btn-sm btn-success" Style="font-size: 9pt; border-radius: 6px; margin-top: -6px;"> <i class="fa fa-check" title="Select For Block/Cancel Service"></i> </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                                                        <AlternatingRowStyle BackColor="#eaf4ff" />
                                                        <HeaderStyle BackColor="#d48584" ForeColor="White" VerticalAlign="Top" />
                                                    </asp:GridView>
                                                    <div class="col-md-12 text-center busListBox" id="lblhaveblockedservicesmsg" runat="server"
                                                        style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;"
                                                        visible="false">
                                                        Services Not Available for Selected Perameter
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4" style="border-right: 1px solid grey;">
                                            <h5 class="text-left pt-2 pb-1 pl-3" style="font-weight: bold; color: #7b7474;">List of [Seat(s) have been Booked] Services</h5>
                                            <div class="row">
                                                <div class="col-md-5 offset-5">
                                                    <asp:TextBox class="form-control" runat="server" ID="txthavebookedservicecode" MaxLength="10"
                                                        placeholder="Service Code" Text="" Style="font-size: 9pt; height: 30px; margin-left: 16px;"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:LinkButton ID="lbtnhavebookedservicesearch" OnClick="lbtnhavebookedservicesearch_Click" runat="server" CssClass="btn btn-sm btn-success ml-3"> <i class="fa fa-search"></i>  </asp:LinkButton>
                                                </div>
                                                <div class="col-md-12">
                                                    <hr />
                                                    <asp:GridView ID="grdbookedservice" runat="server" AutoGenerateColumns="False" CssClass="table"
                                                        AllowPaging="true" PageSize="7" OnPageIndexChanging="grdbookedservice_PageIndexChanging" OnRowCommand="grdbookedservice_RowCommand" DataKeyNames="service_code,servicename,strpid,two_way,journey_type,departure_time,src,des"
                                                        GridLines="None" Font-Names="Verdana" Font-Size="8pt" ShowHeader="false">
                                                        <Columns>
                                                            <asp:TemplateField ShowHeader="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl2" runat="server" Text='<%# Eval("service_code") %>'></asp:Label>/<asp:Label ID="lblSTRP_ID" runat="server" Text='<%# Eval("strpid") %>'></asp:Label>
                                                                    <asp:Label ID="lblJourneytype" runat="server" Text='<%# Eval("journey_type") %>'></asp:Label><br />
                                                                    (<asp:Label ID="lbl122" runat="server" Text='<%# Eval("src") %>'></asp:Label>
                                                                    -
                                                                    <asp:Label ID="lbl1122" runat="server" Text='<%# Eval("des") %>'></asp:Label>)
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ShowHeader="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl22" runat="server" Text='<%# Eval("departure_time") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnView" runat="server" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                        CssClass="btn btn-sm btn-warning" Style="font-size: 9pt; border-radius: 6px; margin-top: -6px;"> <i class="fa fa-eye" title="View Tickets"></i> </asp:LinkButton>
                                                                    <asp:LinkButton ID="btnselect" runat="server" CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                        CssClass="btn btn-sm btn-success" Style="font-size: 9pt; border-radius: 6px; margin-top: -6px;"> <i class="fa fa-check" title="Select For Block/Cancel Service"></i> </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                                                        <AlternatingRowStyle BackColor="#eaf4ff" />
                                                        <HeaderStyle BackColor="#d48584" ForeColor="White" VerticalAlign="Top" />
                                                    </asp:GridView>
                                                    <div class="col-md-12 text-center busListBox" id="lblhavebookedservicesmsg" runat="server"
                                                        style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;"
                                                        visible="false">
                                                        Services Not Available for Selected Perameter
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <h5 class="text-left pt-2 pb-1 pl-3" style="font-weight: bold; color: #7b7474;">List of [Seat(s) have not been Booked] Services</h5>
                                            <div class="row">
                                                <div class="col-md-5 offset-5">
                                                    <asp:TextBox class="form-control" runat="server" ID="txthavenotbookedservicecode"
                                                        MaxLength="10" placeholder="Service Code" Text="" Style="font-size: 9pt; height: 30px; margin-left: 16px;"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:LinkButton ID="lbtnhavenotbookedservicesearch" OnClick="lbtnhavenotbookedservicesearch_Click" runat="server" CssClass="btn btn-sm btn-success ml-3"> <i class="fa fa-search"></i>  </asp:LinkButton>
                                                </div>
                                                <div class="col-md-12">
                                                    <hr />
                                                    <asp:GridView ID="gvServices" OnPageIndexChanging="gvServices_PageIndexChanging" OnRowCommand="gvServices_RowCommand" runat="server" AutoGenerateColumns="False" CssClass="table"
                                                        DataKeyNames="service_code,servicename,strpid,two_way,journey_type,departure_time,src,des"
                                                        AllowPaging="true" PageSize="7" GridLines="None" Font-Names="Verdana" Font-Size="8pt"
                                                        ShowHeader="false">
                                                        <Columns>
                                                            <asp:TemplateField ShowHeader="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl2" runat="server" Text='<%# Eval("service_code") %>'></asp:Label>/<asp:Label ID="lblSTRP_ID" runat="server" Text='<%# Eval("strpid") %>'></asp:Label>
                                                                    <asp:Label ID="lblJourneytype" runat="server" Text='<%# Eval("journey_type") %>'></asp:Label><br />
                                                                    (<asp:Label ID="lbl122" runat="server" Text='<%# Eval("src") %>'></asp:Label>
                                                                    -
                                                                    <asp:Label ID="lbl1122" runat="server" Text='<%# Eval("des") %>'></asp:Label>)
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ShowHeader="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl22" runat="server" Text='<%# Eval("departure_time") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnselect" runat="server" CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                                        CssClass="btn btn-sm btn-success" Style="font-size: 9pt; border-radius: 6px; margin-top: -6px;"> <i class="fa fa-check" title="Select For Block/Cancel Service"></i> </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                                                        <AlternatingRowStyle BackColor="#eaf4ff" />
                                                        <HeaderStyle BackColor="#d48584" ForeColor="White" VerticalAlign="Top" />
                                                    </asp:GridView>
                                                    <div class="col-md-12 text-center busListBox" id="lblhavenotbookedservicesmsg" runat="server"
                                                        style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 20px; font-weight: bold;"
                                                        visible="false">
                                                        Services Not Available for Selected Perameter
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpsevicedetails" runat="server" PopupControlID="pnlsevicedetails"
                CancelControlID="Button2" TargetControlID="Button3" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlsevicedetails" runat="server" Style="position: fixed; display: none;">
                <div class="card card-height ">
                    <div class="card-header">
                        <h5>Services Block/Cancel</h5>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <h5>Service (<asp:Label ID="lblServiceCode" runat="server" Text=""></asp:Label><asp:Label
                                    ID="lbltripdirection" runat="server" Text=""></asp:Label>)</h5>
                                <span style="font-size: 10pt;">
                                    <asp:Label ID="lblSourcess" runat="server"></asp:Label></span>
                                <hr />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-8">
                                <h5>Station</h5>
                                <span style="font-size: 10pt;">
                                    <asp:Label ID="lblSource" runat="server"></asp:Label>
                                    -
                                    <asp:Label ID="lblDestination" runat="server"></asp:Label></span>
                                <hr />
                            </div>
                            <div class="col-md-4">
                                <h5>Dept. Time</h5>
                                <span style="font-size: 10pt;">
                                    <asp:Label ID="lbldeparttime" runat="server"></asp:Label></span>
                                <hr />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 ">
                                <h5>Reason <small class="text-danger">*</small></h5>
                                <asp:TextBox ID="tbReason" runat="server" Style="width: 100%; height: 70px; border: 1px solid #dcdada; resize: none;"
                                    TextMode="MultiLine" MaxLength="200" autocomplete="off"></asp:TextBox>

                            </div>
                        </div>
                        <div class="row m-2 p-2">
                            <div class="col-md-12 align-content-center">

                                <asp:LinkButton ID="btnServiceBlock" OnClick="btnServiceBlock_Click" runat="server" CssClass="btn btn-success "> <i class="fa fa-times"></i> Block Service </asp:LinkButton><br />
                                <asp:LinkButton ID="btnServiceBlkcancel" runat="server" CssClass="btn btn-danger mt-2"> <i class="fa fa-reply-all"></i> Back </asp:LinkButton>

                            </div>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button2" runat="server" Text="" />
                    <asp:Button ID="Button3" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mptktview" runat="server" PopupControlID="pnltktview"
                CancelControlID="Button4" TargetControlID="Button5" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnltktview" runat="server" Style="position: fixed; display: none;">
                <div class="card card-height ">
                    <div class="card-header">
                        <h5>Trip Ticket Details</h5>
                    </div>
                    <div class="card-body" style="width: 1000px;">
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:GridView ID="grdtkt" runat="server" AutoGenerateColumns="False" GridLines="None" ShowHeader="false"
                                    class="w-100">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <span class="text-muted">PNR Number </span>
                                                <asp:Label ID="lblTICKETNO" runat="server" Text='<%# Eval("ticketno") %>'></asp:Label>
                                                <br />
                                                <span class="text-muted">Total Seat(s) </span>
                                                <asp:Label ID="lblTOTALSEATS" runat="server" Text='<%# Eval("total_seats") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <span class="text-muted">Booked By </span>
                                                <asp:Label ID="lblBOOKEDBYTYPE" runat="server" Text='<%# Eval("book_by_type") %>'></asp:Label>
                                                (<asp:Label ID="lblBOOKEDBYUSERID" runat="server" Text='<%# Eval("book_by_user") %>'></asp:Label>)
                                               <br />
                                                <span class="text-muted">Booking Date/Time </span>
                                                <asp:Label ID="lblBOOKINGDATETIME" runat="server" Text='<%# Eval("bookingdatetime") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <span class="text-muted">Fare Amount </span>
                                                <i class="fa fa-rupee"></i>
                                                <asp:Label ID="lblTOTALFAREAMT" runat="server" Text='<%# Eval("total_fare") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle Font-Bold="false" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer text-right">
                        <asp:LinkButton ID="btntktok" runat="server" CssClass="btn btn-danger"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button4" runat="server" Text="" />
                    <asp:Button ID="Button5" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mperror" runat="server" PopupControlID="pnlerror" CancelControlID="lbtnclose1"
                TargetControlID="Button1" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlerror" runat="server" Style="position: fixed; display: none;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Please Check & Correct
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblerrormsg" runat="server" Text="Please Check entered values."></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnclose1" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> OK </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button1" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpsuccess" runat="server" PopupControlID="pnlsuccess"
                CancelControlID="lbtnsuccessclose1" TargetControlID="Button6" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlsuccess" runat="server" Style="position: fixed; display: none;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Confirm
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblsuccessmsg" runat="server" Text="Do you want to Update ?"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnsuccessclose1" runat="server" CssClass="btn btn-success btn-sm ml-2"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button6" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

