<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/StnInchgemaster.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="StInchargeCatalogue.aspx.cs" Inherits="Auth_StInchargeCatalogue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            var todayDate = new Date().getDate();
            var endD = new Date(new Date().setDate(todayDate));
            var currDate = new Date();
            // var startDt = $("[id$=hdstartdate]").val();
            $('[id*=txtDepositDate]').datepicker({
                //  startDate: startDt,
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
    <asp:HiddenField ID="hidtoken" runat="server" />
    <%--<div class="container-fluid pt-top ">
        <div class="row mt-4">
            <div class="col-md-4  stretch-card transparent">
                <div class="card card-dark-blue">
                    <div class="card-body">
                        <div class="card-heading">Cashier Deposit Entry</div>
                        <div class="total-tx mb-4">Cashier Deposit Entry</div>
                        <div class="col text-right">
                            <a href="#!" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="#" class="btn btn-sm btn-primary">Explore</a>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-md-4  stretch-card transparent">
                <div class="card card-dark-blue">
                    <div class="card-body">
                        <div class="card-heading">Target Diesel & Income Entry</div>
                        <div class="total-tx mb-4">Target Diesel & Income Entry</div>
                        <div class="col text-right">
                            <a href="#!" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="stinchargebusservtargetdiesel.aspx" class="btn btn-sm btn-primary">explore</a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4  stretch-card transparent">
                <div class="card card-dark-blue">
                    <div class="card-body">
                        <div class="card-heading">Diesel Refueling</div>
                        <div class="total-tx mb-4">Diesel Refueling</div>
                        <div class="col text-right">
                            <a href="#!" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="StnlnchargeRefillingTankMgmmt.aspx" class="btn btn-sm btn-primary">Explore</a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4 stretch-card transparent">
                <div class="card card-dark-blue">
                    <div class="card-body">
                        <div class="card-heading">Way Bill KMs verifcation</div>
                        <div class="total-tx mb-4">
                            Way Bill KMs verifcation

                        </div>
                        <div class="col text-right">
                            <a href="#!" class="btn btn-sm btn-success"><i class="ni ni-cloud-download-95 mttop "></i></a><a href="StInchargeETMVerify.aspx" class="btn btn-sm btn-primary">Explore</a>
                        </div>

                    </div>
                </div>
            </div>
        </div>
	</div>--%>
    <div class="container-fluid" style="padding-top: 10px">
        <asp:Panel ID="pnldieselalert" Visible="false" runat="server">
            <div class="row">
                <div class="col-lg-12">
                    <marquee> <asp:Label ID="lblAlertTank" runat="server" CssClass="text-danger" Font-Bold="true" Text="having 20% fuel"></asp:Label></marquee>
                </div>
            </div>
        </asp:Panel>
        <div class="row mt-2">
            <div class="col-lg-8 ">
                <h3>Please Note:</h3>
                <asp:Label runat="server" Text="1. Here Station Incharge Work Can Be Done."> </asp:Label><br />
                <asp:Label runat="server" Text="2. Please Start By Selecting Station Incharge Office."> </asp:Label>
                <asp:Label runat="server" Text="3. Download Manual"></asp:Label>
                                 <asp:LinkButton href="../Auth/UserManuals/Station Incharge/Help Document for Station_Incharge.pdf" target="_blank" runat="server" CssClass="btn btn-success btn-sm" ToolTip="Click here for manual."><i class="fa fa-download"></i></asp:LinkButton>
          
            </div>
            <div class="col-lg-2">
                Select Station Incharge Office 
            </div>
            <div class="col-lg-2 ">
                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlssioffice" OnClientClick="return ShowLoading()" AutoPostBack="true" OnSelectedIndexChanged="ddlssioffice_SelectedIndexChanged"></asp:DropDownList>
            </div>

            <div class="col-lg-12 ">
                <hr />
            </div>
            <hr />


        </div>
        <asp:Panel ID="pnlData" runat="server">
            <div class="row">
                <div class="col-lg-4">


                    <p class="mb-0">Quick Links</p>
                    <div class="row  mb-3">
                        <div class="col-sm-6">
                            <asp:LinkButton runat="server" CssClass="btn btn-success w-100" OnClick="lbtnDieselRefuel_Click">Diesel Refueling</asp:LinkButton>

                        </div>
                        <div class="col-sm-6">
                            <asp:LinkButton runat="server" CssClass="btn btn-success w-100" OnClick="lbtntargetdieselentry_Click">Target Diesel Entry</asp:LinkButton>
                        </div>
                        <div class="col-sm-6 mt-2">
                            <asp:LinkButton runat="server" CssClass="btn btn-success w-100" OnClick="lbtnpremature_Click">Premature Waybill Closure</asp:LinkButton>
                        </div>
                        <div class="col-sm-6 mt-2">
                            <asp:LinkButton runat="server" Visible="true" CssClass="btn btn-success w-100" OnClick="lbtnExcessPayment_Click1">Excess Payment Waybill</asp:LinkButton>
                        </div>
                    </div>
                    <p class="mb-0">
                        Summary As On<asp:Label CssClass="ml-1" runat="server" ID="lbldatetime"></asp:Label>
                    </p>
                    <div class="card mb-2 shadow">
                        <div class="card-header">
                            <div class="card-title mb-0">Cashier Deposit</div>
                        </div>
                        <div class="card-body">

                            <div class="row">
                                <div class="col-md-4">
                                    <h4 class="mb-0">
                                        <asp:Label ID="lbltotamt" runat="server" Text="0"></asp:Label></h4>
                                    <p class="mb-0" style="font-size: 9pt;">
                                        Total Deposit<br />
                                        Entry
                                    </p>

                                </div>
                                <div class="col-md-4">

                                    <h4 class="mb-0">
                                        <asp:Label ID="lblVerifyamt" runat="server" Text="0"></asp:Label></h4>
                                    <p class="mb-0" style="font-size: 9pt;">
                                        Verified Deposit<br />
                                        Till Date
                                    </p>

                                </div>
                                <div class="col-md-4">

                                    <h4 class="mb-0">
                                        <asp:Label ID="lblpendingamt" runat="server" Text="0"></asp:Label></h4>
                                    <p class="mb-0" style="font-size: 9pt;">
                                        Pending Deposit<br />
                                        Verification
                                    </p>

                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="card mb-2 shadow">
                        <div class="card-header">
                            <div class="card-title mb-0">WayBill Verification</div>
                        </div>
                        <div class="card-body">

                            <div class="row">
                                <div class="col-md-4">
                                    <h4 class="mb-0">
                                        <asp:Label ID="lbltotWayBill" runat="server" Text="0"></asp:Label></h4>
                                    <p class="mb-0" style="font-size: 9pt;">Total Entry</p>

                                </div>
                                <div class="col-md-4 ">
                                    <h4 class="mb-0">
                                        <asp:Label ID="lblverifyWayBill" runat="server" Text="0"></asp:Label></h4>
                                    <p class="mb-0" style="font-size: 9pt;">Verified Till Date</p>

                                </div>
                                <div class="col-md-4">
                                    <h4 class="mb-0">
                                        <asp:Label ID="lblpendingWayBill" runat="server" Text="0"></asp:Label></h4>
                                    <p class="mb-0" style="font-size: 9pt;">Pending Verification</p>

                                </div>
                            </div>


                        </div>
                    </div>

                    <div class="card mb-2 shadow">
                        <div class="card-header">
                            <div class="card-title mb-0">Target Diesel Entry Summary</div>
                        </div>
                        <div class="card-body">

                            <div class="row">
                                <div class="col-md-3">
                                    <h4 class="mb-0">
                                        <asp:Label ID="lbltotservice" runat="server" Text="0"></asp:Label></h4>
                                    <p class="mb-0" style="font-size: 9pt; line-height: 12pt;">
                                        Total<br />
                                        Service
                                    </p>

                                </div>
                                <div class="col-md-3">
                                    <h4 class="mb-0">
                                        <asp:Label ID="lblactiveservice" runat="server" Text="0"></asp:Label></h4>
                                    <p class="mb-0" style="font-size: 9pt; line-height: 12pt;">
                                        Total<br />
                                        Active
                                    </p>

                                </div>

                                <div class="col-md-3">
                                    <h4 class="mb-0">
                                        <asp:Label ID="lblexpire2dayservice" runat="server" Text="0"></asp:Label></h4>
                                    <p class="mb-0" style="font-size: 9pt; line-height: 12pt;">
                                        Expire in<br />
                                        2 Days
                                    </p>

                                </div>
                                <div class="col-md-3">
                                    <h4 class="mb-0">
                                        <asp:Label ID="lblexpireservice" runat="server" Text="0"></asp:Label></h4>
                                    <p class="mb-0" style="font-size: 9pt; line-height: 12pt;">
                                        Expired<br />
                                        Services
                                    </p>

                                </div>
                            </div>
                        </div>

                    </div>


                </div>
                <div class="col-lg-8 pr-0">
                    <%-- <div class="card mb-3 shadow">
                        <div class="card-header">
                            <div class="row">
                                <div class="col-lg-12">
                                    <h4 style="color: black; font-size: 14pt">Quick Links</h4>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row mt-1">
                                <div class="col-md-3  stretch-card transparent" style="display: none">
                                    <div class="card card-dark-blue" style="height: 100px;">
                                        <div class="card-body">
                                            <div class="card-heading" style="font-size: 15px;">Invoice Entry</div>
                                            <div class="total-tx mb-4" style="font-size: 12px;"></div>
                                            <div class="col-md-12  stretch-card transparent text-center">
                                                <asp:Label runat="server" Text="" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                            </div>
                                            <div class="col text-right" runat="server" visible="true">
                                                <asp:LinkButton OnClick="Unnamed_Click" class="btn btn-sm btn-success" runat="server"><i class="ni ni-cloud-download-95 mttop"></i></asp:LinkButton>
                                                <asp:LinkButton class="btn btn-sm btn-primary" OnClick="Unnamed_Click" runat="server"><i class="fa fa-external-link-alt"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3  stretch-card transparent" style="display: none">
                                    <div class="card card-dark-blue" style="height: 100px;">
                                        <div class="card-body">
                                            <div class="card-heading" style="font-size: 15px;">Way Bill KMs verifcation</div>
                                            <div class="total-tx mb-4" style="font-size: 12px;"></div>
                                            <div class="col-md-12  stretch-card transparent text-center">
                                                <asp:Label runat="server" Text="" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                            </div>
                                            <div class="col text-right" runat="server" visible="true">
                                                <asp:LinkButton OnClick="Unnamed_Click" class="btn btn-sm btn-success" runat="server"><i class="ni ni-cloud-download-95 mttop"></i></asp:LinkButton>
                                                <asp:LinkButton class="btn btn-sm btn-primary" OnClick="Unnamed_Click" runat="server"><i class="fa fa-external-link-alt"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3  stretch-card transparent">
                                    <div class="card card-dark-blue" style="height: 100px;">
                                        <div class="card-body">
                                            <div class="card-heading" style="font-size: 15px;">Diesel Refueling</div>
                                            <div class="total-tx mb-4" style="font-size: 12px;"></div>
                                            <div class="col-md-12  stretch-card transparent text-center">
                                                <asp:Label runat="server" Text="" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                            </div>
                                            <div class="col text-right" runat="server" visible="true">
                                                <asp:LinkButton OnClick="Unnamed_Click" class="btn btn-sm btn-success" runat="server"><i class="ni ni-cloud-download-95 mttop"></i></asp:LinkButton>
                                                <asp:LinkButton class="btn btn-sm btn-primary" ID="lbtnDieselRefuel" OnClick="lbtnDieselRefuel_Click" runat="server"><i class="fa fa-external-link-alt"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--<div class="col-md-3  stretch-card transparent">
                                <div class="card card-dark-blue" style="height: 100px;">
                                    <div class="card-body">
                                        <div class="card-heading" style="font-size: 15px;">Filling Station Management</div>
                                        <div class="total-tx mb-4" style="font-size: 12px;"></div>
                                        <div class="col-md-12  stretch-card transparent text-center">
                                            <asp:Label runat="server" Text="" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                        </div>
                                        <div class="col text-right" runat="server" visible="true">
                                            <asp:LinkButton OnClick="Unnamed_Click" class="btn btn-sm btn-success" runat="server"><i class="ni ni-cloud-download-95 mttop"></i></asp:LinkButton>
                                            <asp:LinkButton class="btn btn-sm btn-primary" OnClick="Unnamed_Click" runat="server"><i class="fa fa-external-link-alt"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                                <div class="col-md-3  stretch-card transparent">
                                    <div class="card card-dark-blue" style="height: 100px;">
                                        <div class="card-body">
                                            <div class="card-heading" style="font-size: 15px;">Traget Diesel Entry</div>
                                            <div class="total-tx mb-4" style="font-size: 12px;"></div>
                                            <div class="col-md-12  stretch-card transparent text-center">
                                                <asp:Label runat="server" Text="" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                            </div>
                                            <div class="col text-right" runat="server" visible="true">
                                                <asp:LinkButton class="btn btn-sm btn-success" runat="server"><i class="ni ni-cloud-download-95 mttop"></i></asp:LinkButton>
                                                <asp:LinkButton class="btn btn-sm btn-primary" OnClick="lbtntargetdieselentry_Click" ID="lbtntargetdieselentry" runat="server"><i class="fa fa-external-link-alt"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>--%>
                    <div class="card mb-3 shadow">
                        <div class="card-header">
                            <div class="card-title">
                                <h3>Pending For Verification</h3>
                            </div>
                        </div>
                        <div class="card-body" style="min-height: 60vh;">
                            <div>
                                <div class="row">
                                    <div class="col-lg-12 mb-2">
                                        Cash Deposit Verification
                                    </div>
                                </div>
                                <div class="row mb-2">
                                    <div class="col-lg-4">
                                        <asp:DropDownList ID="ddlstatus" AutoPostBack="true" runat="server" ToolTip="Select Cashier Deposit Payment" CssClass="form-control" Style="font-size: 10pt; display: inline; height: 30px;">
                                            <asp:ListItem Value="3" Text="All" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Verified"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Pending for Verify"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:TextBox class="textbox" CssClass="form-control" runat="server" ID="txtDepositDate" MaxLength="10" placeholder="DD/MM/YYYY"
                                            Text="" Style="font-size: 10pt; height: 30px; width: 85%; margin-right: -10px;" ToolTip="Select Deposit Date" autocomplete="off"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Numbers,Custom"
                                            TargetControlID="txtDepositDate" ValidChars="/" />

                                    </div>
                                    <div class="col-lg-4">
                                        <asp:LinkButton ID="lnkbtnSearch" OnClick="lnkbtnSearch_Click" runat="server" CommandName="Save" ToolTip="Click here for Search" CssClass="btn btn-sm btn-success"><i class="fa fa-floppy-o"></i>&nbsp;Search</asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnReset" OnClick="lnkbtnReset_Click" runat="server" CommandName="Reset" ToolTip="Click here for reset" CssClass="btn btn-sm btn-danger"><i class="fa fa-refresh"></i>&nbsp;Reset</asp:LinkButton>
                                    </div>
                                </div>

                                <asp:GridView ID="grdAmtVerifyornot" runat="server" AutoGenerateColumns="False" PageSize="4" GridLines="None" AllowPaging="true"
                                    CssClass="table table-striped" OnRowCommand="grdAmtVerifyornot_RowCommand" OnPageIndexChanging="grdAmtVerifyornot_PageIndexChanging" HeaderStyle-VerticalAlign="Top" PagerStyle-CssClass="GridPager" Font-Size="9pt" Width="100%"
                                    DataKeyNames="idd ,receipt_no ,amt ,deposit_date ,receipts ,chst_id ,
				 verified_by ,rejectby ,officename,filebytes "
                                    OnRowDataBound="grdAmtVerifyornot_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Chest" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblchestname" runat="server" Text='<%#Eval("officename")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Receipt No." ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblreceiptno" runat="server" Text='<%#Eval("receipt_no")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblamount" runat="server" Text='<%#Eval("amt")%>'></asp:Label>&nbsp;<i class="fa fa-rupee"></i>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank Deposit Date" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDepositDate" runat="server" Text='<%#Eval("deposit_date")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Receipt" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnViewReceipt" runat="server" CommandName="viewReceipt" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'>View Receipt</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnapprove" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' runat="server" ToolTip="Click here Approve Amount" CssClass="btn btn-sm btn-warning" CommandName="ApproveAmt"><i class="fa fa-check"></i></asp:LinkButton>
                                                <asp:LinkButton ID="btnReject" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' runat="server" Visible="false" ToolTip="Click here Reject Amount" CssClass="btn btn-sm btn-danger" CommandName="RejectAmt"><i class="fa fa-times"></i></asp:LinkButton>
                                                <asp:LinkButton ID="btnView" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' runat="server" ToolTip="Click here View Details" CssClass="btn btn-sm btn-info" CommandName="ViewDetails"><i class="fa fa-eye "></i></asp:LinkButton>
                                           
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    
                                </asp:GridView>
                                <asp:Panel ID="pnlNoRecord" runat="server" Width="100%" Visible="true">
                                    <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                        <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
                                            No Record Available<br />
                                        </div>
                                    </div>
                                </asp:Panel>
                                <hr />
                            </div>
                            <div>
                                <div class="row">
                                    <div class="col-lg-12 mb-2">
                                        Way Bill Verification
                                    </div>
                                </div>


                                <asp:GridView ID="grdWaybillVerified" runat="server" AutoGenerateColumns="False" PageSize="4" GridLines="None" AllowPaging="true"
                                    CssClass="table table-striped" OnRowCommand="grdWaybillVerified_RowCommand" OnPageIndexChanging="grdWaybillVerified_PageIndexChanging"
                                    HeaderStyle-VerticalAlign="Top" PagerStyle-CssClass="GridPager" Font-Size="9pt" Width="100%"
                                    DataKeyNames="dutyrefno_,scheduledkm,deadkm_ ,actualkm_ ">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Waybill No." ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%#Eval("dutyrefno_")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Subjected Km." ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblreceiptno" runat="server" Text='<%#Eval("scheduledkm")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dead Km." ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblamount" runat="server" Text='<%#Eval("deadkm_")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actual Km." ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDepositDate" runat="server" Text='<%#Eval("actualkm_")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnapprove" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' runat="server" ToolTip="Click here for Approve Waybill Km." CssClass="btn btn-sm btn-warning" CommandName="ApproveWaybill"><i class="fa fa-check"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                </asp:GridView>
                                <asp:Panel ID="pnlNorecordwaybill" runat="server" Width="100%" Visible="true">
                                    <div class="col-md-12 p-0" style="text-align: center; text-transform: uppercase;">
                                        <div class="col-md-12 busListBox" style="color: #e3e3e3; padding-top: 50px; padding-bottom: 50px; font-size: 33px; font-weight: bold;">
                                            No Record Available<br />
                                        </div>
                                    </div>
                                </asp:Panel>
                                <hr />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlNoData" runat="server">
            <div class="row">
                <div class="col-12 text-center text-gray mt-5">
                    <asp:Label runat="server" Font-Bold="true" Font-Size="40px" Text="Please Select Station Incharge Office"></asp:Label>
                </div>
            </div>
        </asp:Panel>
    </div>
    <div class="row">
        <cc1:ModalPopupExtender ID="mpviewdocment" runat="server" PopupControlID="pnlviewdocment" CancelControlID="btnclose"
            TargetControlID="Button2" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlviewdocment" runat="server">
            <div class="card" style="margin-top: 100px;">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-6">
                            <h4 class="card-title text-left mb-0">View Document
                            </h4>
                        </div>
                        <div class="col-lg-5  float-end">
                        </div>
                        <div class="col-lg-1  float-end">
                            <asp:LinkButton ID="btnclose" runat="server" CssClass="btn btn-danger"> <i class="fa fa-times"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="card-body text-left pt-2" style="min-height: 100px;">
                   <%-- <embed src="../Pass/ViewDocument.aspx" style="height: 75vh; width: 70vw" />--%>
                     <asp:Image ID="img" runat="server" Visible="false" Style="border-width: 0px; height: 400px; width: 600px; border: 2px solid #eaf4ff;" />
                                                      
                    <div style="visibility: hidden;">
                        <asp:Button ID="Button2" runat="server" Text="" />
                    </div>
                </div>
            </div>
        </asp:Panel>
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
                        <asp:LinkButton ID="lbtnYesConfirmation" OnClick="lbtnYesConfirmation_Click" runat="server" CssClass="btn btn-success btn-sm"> <i class="fa fa-check"></i> Yes </asp:LinkButton>
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
    <div class="row">
        <cc1:ModalPopupExtender ID="mpdepositdetails" runat="server" PopupControlID="pnldepositdetails"
            CancelControlID="lbtnclose" TargetControlID="Button3" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnldepositdetails" runat="server" Style="position: fixed;">
            <div class="card">
                <div class="card-header" style="background-color: #ffc107; padding-bottom: 3px;">
                    <h5 class="card-title">
                        <span>Chest Bank Deposit Details</span>
                    </h5>
                </div>
                <div class="card-body" style="min-height: 100px; padding-bottom: 0px;">
                    <table style="width: 100%;" class="table">
                        <tr>
                            <td>
                                <span style="">Chest Name</span>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="plblchtname" ForeColor="black" Text="NA"></asp:Label>
                            </td>
                            <td>
                                <span style="">Bank Name</span>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="plblbnkname" ForeColor="black" Text="NA"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span style="">Deposit By</span>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="plbldoptby" ForeColor="black" Text="NA"></asp:Label>
                            </td>
                            <td>
                                <span style="">Bank Deposit Date</span>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="plbldoptdate" ForeColor="black" Text="NA"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span style="">Deposit Amount</span>
                            </td>
                            <td>
                                <span>&#8377;</span>
                                <asp:Label runat="server" ID="plblamt" ForeColor="black" Text="NA"></asp:Label>
                            </td>
                            <td>
                                <span style="">Receipt Number</span>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="plblreceipt" ForeColor="black" Text="NA"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span style="">Deposit Entry Date</span>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="plbldoptentrydate" ForeColor="black" Text="NA"></asp:Label>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <span style="">Verify By</span>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="plblverifyby" ForeColor="black" Text="NA"></asp:Label>
                            </td>
                            <td>
                                <span style="">Verify Date</span>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="plblverifydate" ForeColor="black" Text="NA"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="card-footer">
                    <asp:LinkButton ID="lbtnclose" runat="server" CssClass="btn btn-warning" Style="height: 30px; width: 90px; padding-top: 4px; font-size: 10pt; border-radius: 4px; float: right;"> <i class="fa fa-check"></i> OK </asp:LinkButton>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button3" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
    <div class="row">
        <cc1:ModalPopupExtender ID="mpPrematureWaybillClosure" runat="server" PopupControlID="pnlWaybillClosure"
            CancelControlID="LinkButton4" TargetControlID="Button5" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlWaybillClosure" runat="server" Style="position: fixed;">
            <div class="modal-content mt-5" style="width: 80vw;">
                <div class="card w-100">
                    <div class="card-header py-3">
                        <div class="row">
                            <div class="col">
                                <h3 class="m-0">Premature Waybill Closure</h3>
                            </div>
                            <div class="col-auto">
                                <asp:LinkButton ID="LinkButton4" runat="server" Style="color: Red; cursor: pointer;"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="card-body p-2">

                        <asp:Literal ID="eDepositedSlip" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
            <div style="visibility: hidden;">
                <asp:Button ID="Button5" runat="server" Text="" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>

