<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Cntrmaster.master" AutoEventWireup="true" CodeFile="CntrBusPassesStatus.aspx.cs" Inherits="Auth_CntrBusPassesStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        body {
            font-size: 0.8rem;
        }

        h6 {
            font-size: 0.8rem;
            font-weight: bold;
        }

        .border-right {
            border-right: 1px solid;
        }

        .lbld {
            font-weight: bold;
        }

        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .cd {
            min-height: 22vh !important;
        }
    </style>
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidtoken" runat="server"></asp:HiddenField>
    <div class="content mt-3">
        <div class="animated fadeIn">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <div class="row">
                                <div class="col-lg-6">
                                    <h5>
                                        <asp:Label ID="lblcardhd" runat="server" Text="New Pass - Confirmation"></asp:Label></h5>
                                </div>
                                <div class="col-lg-6 text-right">
                                    <asp:LinkButton ID="lbtnback" OnClick="lbtnback_Click" runat="server" CssClass="btn btn-warning btn-sm sm-4"> <i class="fa fa-backward"></i> Back To Pass Dashboard</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row mt-2">
                                <div class="col-lg-12 text-center">
                                    <asp:Label ID="lblmsg" runat="server" Text="N/A" Style="font-weight: bold; font-size: 17pt;"></asp:Label><br />
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-lg-12 text-center">
                                    <asp:LinkButton ID="btnprintpass" OnClick="btnprintpass_Click" runat="server" CssClass="submit-btn btn btn-success"><i class="fa fa-print"></i> Click Here to Print Pass</asp:LinkButton>
                                    <asp:LinkButton ID="btnprintrecipt" OnClick="btnprintrecipt_Click" runat="server" CssClass="submit-btn btn btn-warning"><i class="fa fa-comment"></i> Click Here to Print Reciept</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    
</asp:Content>


