<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ccavRequestHandler.aspx.cs" Inherits="PG_ccavRequestHandler" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <script type="text/javascript">
          function onLoadSubmit() {
           
             document.getElementById("nonseamless").submit();
          //  document.frmTransaction.submit();
          }
          //$(document).ready(function () {
         
          //    // $("#nonseamless").submit();
          // });
       </script>
</head>
<body  onload="onLoadSubmit();">
    <form runat="server" id="nonseamless" method="post" name="redirect" action="https://test.ccavenue.com/transaction/transaction.do?command=initiateTransaction ">
    <div>
    <input type="hidden" id="encRequest" name="encRequest" value="<%=strEncRequest%>"/>
        <input type="hidden" name="access_code" id="Hidden1" value="<%=strAccessCode%>"/>
    </div>
    </form>
</body>
</html>
