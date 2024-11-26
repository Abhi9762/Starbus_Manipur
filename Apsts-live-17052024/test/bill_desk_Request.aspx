<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bill_desk_Request.aspx.cs" Inherits="BILLDESK_PG_bill_desk_Request" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<script src="assets/vendor/jquery/dist/jquery.min.js"></script>--%>
    <script type="module" src="https://uat1.billdesk.com/merchant-uat/sdk/dist/billdesksdk/billdesksdk.esm.js"></script>
<script nomodule="" src="https://uat1.billdesk.com/merchant-uat/sdk/dist/billdesksdk.js"></script>
<link href="https://uat1.billdesk.com/merchant-uat/sdk/dist/billdesksdk/billdesksdk.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="HiddenField2" runat="server" />
            <asp:HiddenField ID="HiddenField3" runat="server" />
        </div>
    </form>
</body>
    <script>
        var flow_config = {
    merchantId: "ARUNACUAT",
bdOrderId: document.getElementById("<%= HiddenField1.ClientID %>").value, // get from orderCreate response
            authToken: document.getElementById("<%= HiddenField2.ClientID %>").value,
            returnUrl: document.getElementById("<%= HiddenField3.ClientID %>").value,
childWindow: true,
crossButtonHandling: 'Y',
retryCount: 0
};
var responseHandler = function (txn) {
if (txn.response) {
alert("callback received status:: ", txn.status);
alert("callback received response:: ", txn.response);
}
};
        var config = {
flowConfig: flow_config,
flowType: "payments"
};
        window.onload = function () {
            debugger;
window.loadBillDeskSdk(config);
};
</script>
</html>