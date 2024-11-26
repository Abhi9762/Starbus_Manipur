<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/Depotmaster.master" AutoEventWireup="true" CodeFile="depotComplaintDash.aspx.cs" Inherits="Auth_depotComplaintDash" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .ModalPopupBG {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid pt-3" style="background-color: #f8f2f2; min-height: 900px">
        <div class="row">
            <div class="col-md-3">
              

                <asp:Label runat="server" Text="" Font-Size="Larger" ID="lblsummary"></asp:Label>
                <div class="row mt-2">
                    <div class="col-md-12">
                        <div class="card py-1" style="background-color: #89dd89; border-left: 5px solid #00bc6c;">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col" style="border-right: 2px solid #00bc6c;">
                                        <asp:Label runat="server" Text="Assigned Complaints" Font-Size="18px"></asp:Label>
                                    </div>
                                    <div class="col-auto px-3">
                                        <asp:LinkButton ID="lbtnPendinggv" OnClick="lbtnPendinggv_Click" runat="server" Text="0" Style="font-size: 29px; font-weight: bold; line-height: 27px; color: black; font-weight: bold;"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <div class="col-md-12">
                        <div class="card py-1" style="background-color: #e74d4d; border-left: 5px solid #ad0022;">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col" style="border-right: 2px solid #ad0022;">
                                        <asp:Label runat="server" Text="Reject Complaints" Font-Size="18px"></asp:Label>
                                    </div>
                                    <div class="col-auto px-3">
                                        <asp:LinkButton ID="lbtnreject" OnClick="lbtnreject_Click" runat="server" Text="0" Style="font-size: 29px; font-weight: bold; line-height: 27px; color: black; font-weight: bold;"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="card py-1" style="background-color: #ffb225; border-left: 5px solid #d09428;">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col" style="border-right: 2px solid #d09428;">
                                        <asp:Label runat="server" Text="Disposed Complaints" Font-Size="18px"></asp:Label>
                                    </div>
                                    <div class="col-auto px-3">
                                        <asp:LinkButton runat="server" ID="lbtndisposed" OnClick="lbtndisposed_Click" Text="0" Style="font-size: 29px; font-weight: bold; line-height: 27px; color: black; font-weight: bold;"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="card py-1" style="background-color: #e6adad; border-left: 5px solid #e46a6a;">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col" style="border-right: 2px solid #e46a6a;">
                                        <asp:Label runat="server" Text="Total Complaints" Font-Size="18px"></asp:Label>
                                    </div>
                                    <div class="col-auto px-3">
                                        <asp:LinkButton ID="lbtntotalcomp" OnClick="lbtntotalcomp_Click" runat="server" Text="0" Style="font-size: 29px; font-weight: bold; line-height: 27px; color: black; font-weight: bold;"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-9 pl-4">
                <div class="card shadow" style="min-height: 723px">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-sm-12 text-center" style="height: 40px; font-size: 15pt; color: #1c4a84; font-weight: bold;">
                                <asp:Label runat="server" ID="lblgridheader" Text="Complaints"></asp:Label>
                            </div>
                            <div class="col-sm-12 flex-column d-flex stretch-card ">
                                <div class="card-body table table-responsive" style="min-height: 300px;">
                                    <asp:Label ID="totserach" runat="server" Style="font-size: 14px; margin-top: 4px;"></asp:Label>
                                    <asp:GridView ID="grdready" runat="server" GridLines="None" CssClass="w-100" AllowPaging="true"
                                        PageSize="6" AutoGenerateColumns="false" ShowHeader="false"
                                        OnPageIndexChanging="grdready_PageIndexChanging" OnRowDataBound="grdready_RowDataBound" OnRowCommand="grdready_RowCommand" 
                                        DataKeyNames="refno">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col">
                                                            <p class="mb-0">
                                                                <%-- <%# If(Eval("STATUS").ToString() = "CONFIRMED", "<i class='fa fa-check-circle-o text-success'></i>", "<i class='fa fa-times-circle-o text-danger'></i>") %>--%>
                                                                <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="Refrence No. "></asp:Label>
                                                                <asp:Label ID="lblticket" runat="server" Text='<%#Eval("refno") %>'></asp:Label>

                                                            </p>

                                                        </div>
                                                        <div class="col">
                                                            <p class="mb-0 ">
                                                                <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Category "></asp:Label>
                                                                <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("g_categoryname") %>'></asp:Label>
                                                                <asp:Label ID="lbl1" runat="server" Text='-'></asp:Label>
                                                                <asp:Label ID="lblSubCategory" runat="server" Text='<%# Eval("g_subcategoryname") %>'></asp:Label>
                                                            </p>
                                                        </div>
                                                        <div class="col text-right">
                                                            <p class="mb-0">
                                                                <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Date "></asp:Label>
                                                                <asp:Label ID="lbljouyneydate" runat="server" Text=' <%#Eval("g_datetime") %>'></asp:Label>

                                                            </p>
                                                        </div>
                                                        <div class="col-auto">
                                                            <asp:LinkButton runat="server" CommandName="ACTION" CssClass="btn btn-success btn-sm"><i class="fa fa-eye"></i></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12 mt-1">
                                                            <p>
                                                                <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="Description "></asp:Label>
                                                                <asp:Label ID="lblGREMARK" runat="server" Style="white-space: normal;" Text='<%# Eval("g_remark") %>'></asp:Label>
                                                            </p>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="GridPager" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <%--<asp:GridView ID="grdready" OnPageIndexChanging="grdready_PageIndexChanging" OnRowDataBound="grdready_RowDataBound" OnRowCommand="grdready_RowCommand" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                    GridLines="None" DataKeyNames="gv_refno" Width="100%"
                                    class="table mb-0 table-responsive w-100" PageSize="10" Font-Size="13px">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="row">
                                                    
                                                    <div class="col">
                                                        <asp:Label ID="lblRefno" runat="server" Text='<%# Eval("gv_refno") %>'></asp:Label>
                                                    
                                                        <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("gv_categoryname") %>'></asp:Label>
                                                        <asp:Label ID="lbl1" runat="server" Text='-'></asp:Label>
                                                        <asp:Label ID="lblSubCategory" runat="server" Text='<%# Eval("gv_subcategoryname") %>'></asp:Label>
                                                    </div>
                                                    <div class="col">
                                                        <b>
                                                            <asp:Label ID="lbltktnum" runat="server" Text='<%# Eval("gv_tktno") %>'></asp:Label></b>
                                                        <asp:Label ID="lblGREMARK" runat="server" Text='<%# Eval("gv_remark") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-auto ">
                                                        <asp:Label ID="lblGDATETIME" runat="server" Text='<%# Eval("gv_datetime") %>'></asp:Label>
                                                        <asp:LinkButton runat="server" CommandName="ACTION" CssClass="btn btn-success btn-sm"><i class="fa fa-eye"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                     
                                    </Columns>
                                   
                                    <RowStyle BackColor="White" BorderColor="#333333" />
                                    <AlternatingRowStyle VerticalAlign="Middle" />
                                    <HeaderStyle ForeColor="BLACK" VerticalAlign="Middle" BackColor="#d7e1f4" />

                                </asp:GridView>--%>
                        </div>
                        <asp:Panel runat="server" ID="pnlNodata" Visible="false">
                            <div class="row">
                                <div class="col-12 text-center mt--8">
                                    <asp:Label runat="server" Text="Complaints<br/> Not <br/>Found" Font-Size="50px" ForeColor="LightGray" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <cc1:ModalPopupExtender ID="mpGrievance" runat="server" PopupControlID="pnlmpGrievance" TargetControlID="btnOpenmpGrievance"
                CancelControlID="lbtnClosemp" BackgroundCssClass="ModalPopupBG">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlmpGrievance" runat="server" Style="position: fixed;">
                <div class="modal-content mt-5" style="width: 90vw;">
                    <div class="card w-100">
                        <div class="card-header py-3">
                            <div class="row">
                                <div class="col">
                                    <h3 class="m-0">Grievance</h3>
                                </div>
                                <div class="col-auto">
                                    <asp:LinkButton ID="lbtnClosempGrievanceee" OnClick="lbtnClosempGrievanceee_Click" runat="server" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="card-body p-2">
                            <%--     <embed src = "dashGrievance.aspx" style="height: 80vh; width: 100%"  />--%>
                            <asp:Literal ID="eDash" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
                <br />
                <div style="visibility: hidden;">
                    <asp:Button ID="btnOpenmpGrievance" runat="server" Text="" />
                    <asp:Button ID="lbtnClosemp" runat="server" Text="" />
                </div>
            </asp:Panel>
        </div>
    </div>

</asp:Content>

