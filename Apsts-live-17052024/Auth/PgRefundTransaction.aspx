<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/pgmaster.master" AutoEventWireup="true" CodeFile="PgRefundTransaction.aspx.cs" EnableViewState="true" Inherits="Auth_PgRefundTransaction" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <style>
        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .table {
            width: 100%;
        }

            .table th, .table td {
                padding: .5rem 0.75rem;
                vertical-align: top;
                border-top: 1px solid #dce1e3;
                font-size: 13px;
            }

        table.dataTable {
            text-transform: uppercase;
        }
    </style>
    <script src="../assets/vendor/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var todayDate = new Date().getDate();
            var endD = new Date(new Date().setDate(todayDate - 1));
            var currDate = new Date();
            $('[id*=txtrefunddate]').datepicker({
                //startDate: "dateToday",
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

    <div class="container-fluid" style="padding-top: 20px;">
        <asp:HiddenField ID="hidtoken" runat="server" />
        <div class="row mb-2">
            <div class="col-lg-12">
                <div class="card">
                    <div class="row card-header m-0 p-1">

                        <div class="col-lg-10">
                            <div class="row">


                                <div class="col-lg-4 pt-2" style="height: 55px;">
                                    <asp:Label ID="lblSummary" runat="server" CssClass="text-capitalize">Summary as on Date 01/12/2021 04:00pm</asp:Label>
                                </div>

                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="row">
                                <div class="col-lg-6 form-group" runat="server">

                                    <label>Date</label>
                                    <div class="input-group date">
                                        <asp:TextBox ID="txtrefunddate" ToolTip="Enter Transaction Date" runat="server" CssClass="form-control form-control-sm" MaxLength="10" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" TargetControlID="txtrefunddate" ValidChars="/" />
                                    </div>
                                </div>

                                <div class="col-lg-6">
                                    <div class="row">

                                        <div class="col-sm-3 pt-3 mt-2">
                                            <asp:LinkButton ID="lbtnsearch" runat="server" ToolTip="Click to Search" OnClick="lbtnSearch_Click" OnClientClick="return ShowLoading()" CssClass="btn btn-primary btn-sm">
                                    <i class="fa fa-search"></i></asp:LinkButton>
                                        </div>
                                        <div class="col-sm-3 pt-3 mt-2 ml-2">
                                            <asp:LinkButton ID="lbtnreset" runat="server" ToolTip="Click to Reset" OnClick="lbtnreset_Click" OnClientClick="return ShowLoading()" CssClass="btn btn-danger btn-sm">
                                    <i class="fa fa-redo"></i></asp:LinkButton>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>





                <div class="row">
                    <div class="col-sm-3 flex-column d-flex stretch-card">
                        <div class="card" style="min-height: 394px;">
                            <div class="card-body">
                                <div class="row">
                                    <asp:Label ID="Label1" runat="server" Text="Please Note:" Font-Bold="true" Font-Size="Large"></asp:Label>
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Size="Small">1. Here you can download the cancellation scroll for T + 1 day.</asp:Label>
                                    <br />
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Size="Small">2. For Any Query, Please Contact Us .</asp:Label>
                                    <br />
                                    <asp:Label runat="server" CssClass="form-control-label" Font-Size="Small"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-9 flex-column d-flex stretch-card" id="pnlReport" visible="False" runat="server">
                        <div class="card">

                            <div class="card-header bg-white">
                                <div class="row">
                                    <div class="col-lg-10">
                                       
                                        <asp:Label ID="lblReportName" Font-Bold="true" runat="server" Font-Size="Large" Text="Pending For Refund Transaction"></asp:Label>
                                        <br />
                                       
                                            <asp:Label  runat="server" Text="Summary | " Font-Bold="true" Font-Size="Medium"></asp:Label>
                                            <asp:Label ID="lblsmry" runat="server" Text=""></asp:Label>
                                      
                                    </div>
                                    <div class="col-lg-2 float-right ">
                                        <asp:LinkButton ID="lbtnpdf" runat="server" OnClick="lbtnpdf_Click" ToolTip="Click to Download" CssClass="btn btn-danger btn-sm"><i class="fa fa-download"> Download</i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                            <div class="card-body table-responsive" style="min-height: 320px;">
                                <asp:GridView ID="GvPgRefund" runat="server" GridLines="None" CssClass="table" AllowPaging="true" OnPageIndexChanging="GvPgRefund_PageIndexChanging"
                                    PageSize="10" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Billdesk Transaction Id">
                                            <ItemTemplate>
                                                <%#Eval("billdesk_trans_id_") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transaction Date">
                                            <ItemTemplate>
                                                <%#Eval("transactiondate") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Id">
                                            <ItemTemplate>
                                                <%#Eval("orderid") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Amount <br> ⟨₹⟩">
                                            

                                            <ItemTemplate>
                                                <%#Eval("totalamount") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount To Refund <br> ⟨₹⟩">
                                            <ItemTemplate>
                                                <%#Eval("amountrefund") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cancel Reference No." Visible="false">
                                            <ItemTemplate>
                                                <%#Eval("cancel_ref_no_") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>


                    <div class="col-sm-9 flex-column d-flex stretch-card" style="margin-top:150px;" runat="server" id="pnlMsg" visible="True">
                        <center>
                            <asp:Label runat="server" Text="To generate report Click on Search Button." Font-Bold="true" Font-Size="40px" ForeColor="LightGray"></asp:Label>

                        </center>
                    </div>


                </div>

            </div>
        </div>
    </div>






    <div class="row">
        <cc1:ModalPopupExtender ID="mpError" runat="server" PopupControlID="pnlError" CancelControlID="lbtnerrorclose"
            TargetControlID="Button3" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlError" runat="server" Style="position: fixed; display: none">
            <div class="card" style="min-width: 350px; max-width: 650px;">
                <div class="card-header">
                    <h4 class="card-title m-0">Please Check
                    </h4>
                </div>
                <div class="card-body py-2 px-3" style="min-height: 100px; max-height: 70vh; overflow: auto;">
                    <asp:Label ID="lblerrmsg" runat="server" Font-Size="18px"></asp:Label>
                </div>
                <div class="card-footer text-right ">
                    <asp:LinkButton ID="lbtnerrorclose" runat="server" CssClass="btn btn-danger"> OK </asp:LinkButton>
                </div>
            </div>
            <br />
            <div style="visibility: hidden;">
                <asp:Button ID="Button3" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>


</asp:Content>



