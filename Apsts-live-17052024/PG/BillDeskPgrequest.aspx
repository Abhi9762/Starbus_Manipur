<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BillDeskPgrequest.aspx.cs" Inherits="PG_BillDeskPgrequest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript">
         function preventBack() { window.history.forward(); }
         setTimeout("preventBack()", 0);
         window.onunload = function () { null };
    </script>
    <script language="javascript">
        function onLoadSubmit() {
            document.merchantForm.submit();
        }
        function Submit1_onclick() {

        }

    </script>
</head>
<body onload="onLoadSubmit();">
	<br />&nbsp;<br />
	
	<center><font size="5" color="#3b4455">Transaction is being processed,<br/>Please wait ...</font></center>
	<form name="merchantForm" method="post" action="https://uat.billdesk.com/pgidsk/PGIMerchantPayment" autocomplete="off">
   
	<input type="hidden" name="msg" value="<%=passwordHashSha1%>"/>

	<noscript>
		<br />&nbsp;<br />
		<center>
		<font size="3" color="#3b4455">
		JavaScript is currently disabled or is not supported by your browser.<br />
		Please click Submit to continue the processing of your transaction.<br />&nbsp;<br />
		<input type="submit" id="Submit1" onclick="return Submit1_onclick()" />
		</font>
		</center>
	</noscript>
	</form>
</body>
</html>
