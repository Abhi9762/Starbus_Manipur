<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/SysAdmmaster.master"  AutoEventWireup="true" CodeFile="SysAdmHoliday.aspx.cs" Inherits="Auth_SysAdmHoliday" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .btn {
            /* min-height: 60px;*/
            min-width: 100px;
        }

        hr {
            margin-top: 5px;
        }
    </style>
    <script type="text/javascript">
        function Closepopup() {
            $('#mpaddHoliday').modal('close');

        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid" style="background-color: #FAF9F6">
        <div class="row">
            <div class="col-md-12 col-lg-12">
                <h2 class="card-title" style="font-weight: bold;">Add Holiday</h2>
                <hr />
            </div>

        </div>

        <div class="row">
            <div class="col-md-2 col-lg-2 bg-white card p-4">
                <div class="row">

                    <div class="col-lg-12 col-md-12">
                        <asp:Label runat="server" CssClass="form-control-label">Office Level <span class="text-warning">*</span></asp:Label>

                        <asp:DropDownList class="form-control form-control-sm" runat="server" ID="ddlOfficeLevel" OnSelectedIndexChanged="ddlOfficeLevel_SelectedIndexChanged" ToolTip="Select Office Level" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-lg-12 col-md-12">
                        <asp:Label runat="server" CssClass="form-control-label">Office<span class="text-warning">*</span></asp:Label>

                        <asp:DropDownList class="form-control form-control-sm" runat="server" ID="ddlOfficeType" OnSelectedIndexChanged="ddlOfficeType_SelectedIndexChanged" AutoPostBack="true"  ToolTip="Select Select Office"></asp:DropDownList>
                    </div>
                    <div class="col-lg-12 col-md-12">
                        <asp:Label runat="server" CssClass="form-control-label">Year <span class="text-warning">*</span></asp:Label>

                        <asp:DropDownList class="form-control form-control-sm" runat="server" ID="ddlYear" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" ToolTip="Select Year" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-lg-12 col-md-12">
                        <asp:Label runat="server" CssClass="form-control-label">Month <span class="text-warning">*</span></asp:Label>

                        <asp:DropDownList class="form-control form-control-sm" runat="server" ID="ddlMonthNames" OnSelectedIndexChanged="ddlMonthSelectedIndexChangrd" ToolTip="Select Month" AutoPostBack="true"></asp:DropDownList>
                    </div>

                </div>


            </div>
            <div class="col-md-10 col-lg-10 bg-white card p-4">


                <div class="row row-cards">
                    <div class="col-sm-12">

                        <div class="row">
                            <div class="col-2"></div>
                            <div class="col-2 input-group-prepend ">
                                <div style="width: 15px; height: 15px; background-color: orangered; margin-right: 10px">
                                </div>
                                Weekend
                            </div>
                            <div class="col-2 input-group-prepend ">
                                <div style="width: 15px; height: 15px; background-color: Red; margin-right: 10px">
                                </div>
                                Gazetted Holiday
                            </div>
                            <div class="col-2 input-group-prepend ">
                                <div style="width: 15px; height: 15px; background-color: green; margin-right: 10px">
                                </div>
                                Restricted Holiday
                            </div>
                            <div class="col-2 input-group-prepend ">
                                <div style="width: 15px; height: 15px; background-color: #7cfc00; margin-right: 10px">
                                </div>
                                National Holiday
                            </div>
                        </div>
                   

                            <asp:Updatepanel runat="server">
                                <ContentTemplate>
                                    <asp:Calendar ID="Calendar1" runat="server"  BorderColor="#00ffff" BorderWidth="2px"
                                        DayNameFormat="Full" Font-Names="Verdana" Font-Size="10pt" ForeColor="#663399"
                                        ShowGridLines="True" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged"
                                        OnVisibleMonthChanged="Calendar1_VisibleMonthChanged">
                                        <selecteddaystyle backcolor="#1abc9c" font-bold="True" forecolor="White" />
                                        <selectorstyle backcolor="#0df2f2" height="20px" />
                                        <todaydaystyle backcolor="#FFCC66" forecolor="White" />
                                        <othermonthdaystyle forecolor="#CC9966" />
                                        <nextprevstyle font-size="9pt" forecolor="#FFFFCC" />
                                        <dayheaderstyle backcolor="#20dfdf" font-bold="True" height="10px" />
                                        <titlestyle backcolor="#1abc9c" font-bold="True" font-size="20pt" forecolor="White" />
                                    </asp:Calendar>
                                    <div class="row">
                                        <cc1:ModalPopupExtender ID="mpaddHoliday" runat="server" PopupControlID="Pnldutyyy" TargetControlID="Button2"
                                            CancelControlID="lbtncancel" BackgroundCssClass="modalBackground">
                                        </cc1:ModalPopupExtender>
                                        <asp:Panel ID="Pnldutyyy" runat="server" Style="position: fixed;">

                                            <div class="modal-content" style="width: 750px;">
                                                <div class="modal-header">
                                                    <div class="col-md-10">
                                                        <h4 class="m-0 text-left" style="text-align: left; font-size: 20px;">Add Holiday</h4>
                                                    </div>
                                                    <div class="col-md-2 text-right">
                                                        <asp:LinkButton ID="lbtncancel" runat="server" UseSubmitBehavior="false"
                                                            Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="modal-body">
                                                    <div class="row pl-3 pr-3">

                                                        <div class="col-md-4">
                                                            Date
                                     <asp:TextBox ID="txtDate" Enabled="false" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            Day
                                     <asp:TextBox ID="txtHolidayDay" Enabled="false" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            Type
                                     <asp:DropDownList ID="ddl_Holidaytype" runat="server" CssClass="form-control" required="true">
                                         <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                         <asp:ListItem Text="Gazetted Holiday" Value="G"></asp:ListItem>
                                         <asp:ListItem Text="Restricted Holiday" Value="R"></asp:ListItem>
                                         <asp:ListItem Text="National Holiday" Value="N"></asp:ListItem>
                                     </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-4">
                                                            Occassion
                                     <asp:TextBox ID="txt_holidayName" Enabled="false" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            Description
                                     <asp:TextBox ID="txtdesc" Enabled="false" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <%--  <div class="row"><p>&nbsp;</p></div>--%>
                                                    <div class="row">
                                                        <div class="col-4"></div>
                                                        <div class="col-2">
                                                        </div>
                                                        <div class="col-2">
                                                            <asp:LinkButton runat="server" ID="lbtnSaveHoliday" OnClick="lbtnSaveHoliday_Click" CssClass="btn btn-success mt-2" Font-Size="Small"><i class="fa fa-check "></i>&nbsp;Submit</asp:LinkButton>

                                                        </div>
                                                        <div class="col-4">
                                                            <asp:LinkButton runat="server" ID="lbtnReset" OnClick="lbtnReset_Click" CausesValidation="False" CssClass="btn btn-warning mt-2" Font-Size="Small"><i class="fa fa-close "></i>&nbsp;Reset</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <br />
                                        <div style="visibility: hidden;">
                                            <asp:Button ID="Button2" runat="server" Text="" />
                                          
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:Updatepanel>
                    </div>
                </div>
            </div>
        </div>


        <div class="row">
            <cc1:ModalPopupExtender ID="mpConfirmation" runat="server" PopupControlID="pnlConfirmation"
                CancelControlID="lbtnNoConfirmation" TargetControlID="Button4" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlConfirmation" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Please Confirm
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblConfirmation" runat="server" Text="Do you want to save ?"></asp:Label>
                        <div style="width: 100%; margin-top: 20px; text-align: right;">
                            <asp:LinkButton ID="lbtnYesConfirmation" runat="server" OnClick="lbtnYesConfirmation_Click" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
                            <asp:LinkButton ID="lbtnNoConfirmation" runat="server" CssClass="btn btn-danger btn-sm ml-2"> <i class="fa fa-times"></i> No </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div style="visibility: hidden;">
                    <asp:Button ID="Button4" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>

        <div class="row">
            <cc1:ModalPopupExtender ID="mperror" runat="server" PopupControlID="pnlerror" CancelControlID="lbtnclose1"
                TargetControlID="Button1" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlerror" runat="server" Style="position: fixed;">
                <div class="card" style="width: 350px;">
                    <div class="card-header">
                        <h4 class="card-title text-left mb-0">Please Check & Correct
                        </h4>
                    </div>
                    <div class="card-body text-left pt-2" style="min-height: 100px;">
                        <asp:Label ID="lblerrormsg" runat="server" ></asp:Label>
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
            <asp:Panel ID="pnlsuccess" runat="server" Style="position: fixed;">
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




