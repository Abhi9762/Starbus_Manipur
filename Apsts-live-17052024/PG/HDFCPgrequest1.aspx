<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HDFCPgrequest.aspx.cs" Inherits="PG_HDFCPgrequest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        window.onload = function () {
            var d = new Date().getTime();
            document.getElementById("tid").value = d;
        };
        function onLoadSubmit() {
            document.getElementById("frmTransaction").submit();
            //document.frmTransaction.submit();
        }
    </script>
</head>
<body onload="onLoadSubmit();">
    <form method="post" action="ccavRequestHandler.aspx" name="frmTransaction" id="frmTransaction" runat="server">
        <input type="text" name="tid" id="tid" readonly hidden />
        <input name="merchant_id" type="hidden" value="<%=merchant_id%>" />
        <input name="order_id" type="hidden" value="<%=order_id%>" />
        <input name="amount" type="hidden" value="<%=amount%>" />
        <input name="currency" type="hidden" value="<%=currency%>" />
        <input name="redirect_url" type="hidden" value="<%=redirect_url%>" />
        <input name="cancel_url" type="hidden" value="<%=cancel_url%>" />
        <input name="billing_name" type="hidden" value="<%=billing_name%>" />
        <input name="billing_address" type="hidden" value="" />
        <input name="billing_city" type="hidden" value="" />
        <input name="billing_state" type="hidden" value="" />
        <input name="billing_zip" type="hidden" value="" />
        <input name="billing_country" type="hidden" value="" />
        <input name="billing_tel" type="hidden" value="<%=billing_tel%>" />
        <input name="billing_email" type="hidden" value="<%=billing_email%>" />
        <input name="delivery_name" type="hidden" value="" />
        <input name="delivery_address" type="hidden" value="" />
        <input name="delivery_city" type="hidden" value="" />
        <input name="delivery_state" type="hidden" value="" />
        <input name="delivery_zip" type="hidden" value="" />
        <input name="delivery_country" type="hidden" value="" />
        <input name="delivery_tel" type="hidden" value="" />
        <input name="merchant_param1" type="hidden" value="" />
        <input name="merchant_param2" type="hidden" value="" />
        <input name="merchant_param3" type="hidden" value="" />
        <input name="merchant_param4" type="hidden" value="" />
        <input name="merchant_param5" type="hidden" value="" />
        <input name="promo_code" type="hidden" value="" />
        <input name="customer_identifier" type="hidden" value="" />
        <noscript>                     
                        &nbsp;<br />
                        <center>
                            <font size="3" color="#3b4455">JavaScript is currently disabled or is not supported
                                by your browser.<br />
                                Please click Submit to continue the processing of your transaction.<br />
                                &nbsp;<br />
                            </font>
                        </center>
                    </noscript>
    </form>
</body>
</html>
