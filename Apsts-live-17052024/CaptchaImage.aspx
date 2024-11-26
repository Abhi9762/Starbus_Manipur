<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaptchaImage.aspx.cs" Inherits="CaptchaImage" %>
<%@import Namespace="System.Drawing.Imaging" %>
<%@import Namespace="CaptchaDLL" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script runat="server">
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if(Session["CaptchaImage"]!=null)
            {
                CaptchaImage ci = new CaptchaImage(Session["CaptchaImage"].ToString(), 200, 30, "Century Schoolbook", System.Drawing.Color.FromArgb(165, 163, 166), System.Drawing.Color.FromArgb(252, 252, 253));               
                Response.Clear();
                Response.ContentType = "image/jpeg";
                ci.Image.Save(Response.OutputStream, ImageFormat.Jpeg);
                ci.Dispose();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
