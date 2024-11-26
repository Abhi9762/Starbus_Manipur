using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for sbLoaderNdPopup
/// </summary>
public class sbLoaderNdPopup
{
    string name = "Starbus * MSTS";
    public string getLoaderHtml()
    {
        // Please Note ********************
        // set below code in HTML part in <Form> tag (HTML part)
        //
        //<body onload="HideLoading()">  
        // <%
        //    sbLoaderNdPopup dd = new sbLoaderNdPopup();
        //    string loader = dd.getLoaderHtml();
        //    Response.Write(loader);
        // %>
        //  OnClientClick="ShowLoading()"

        string html = "";
        html += "<style> ";
        html += "#loader { position: fixed; left: 0px; top: 0px; width: 100%;  height: 100%; z-index: 9999; background: rgb(0, 0, 0, .65);   }";
        html += " .waviy { position: relative;  -webkit-box-reflect: below -20px linear-gradient(transparent, rgba(0,0,0,.2));   text-align: center;  top: 50%; }";
        html += " .waviy span {  position: relative;  display: inline-block;  font-size: 20px;  color: #fff; animation: waviy 1s infinite; animation-delay: calc(.024s * var(--i));}";
        html += " *, ::after, ::before { box-sizing: border-box; }";
        html += " @keyframes waviy{ 0%,40%,100% { transform: translateY(0);  } 20% { transform: translateY(-20px);  } }";
        html += " </style>";
        html += " <script type = 'text/javascript'> ";
        //html += "$(window).on('load', function (){HideLoading();});";
        html += "   function ShowLoading(){ var div = document.getElementById('loader');    div.style.display = 'block';}";
        html += "function HideLoading(){ var div = document.getElementById('loader');   div.style.display = 'none';}";
        html += "</script>";

        html += "<div id='loader'>";
        html += "<div Class='waviy'>";

        int index = 1;
        foreach (char c in name)
        {
            html += "<span style = '--i: " + index + "' > &nbsp;" + c + "</span>";
            index++;
        }

        html += " </div> </div>";
        return html;
    }

    public string modalPopupLarge(string popType, string heading, string message, string footerCloseButtonText)
    {
        return modalPopupComman(popType, heading, message, footerCloseButtonText, "L");
    }

    public string modalPopupSmall(string popType, string heading, string message, string footerCloseButtonText)
    {
        return modalPopupComman(popType, heading, message, footerCloseButtonText, "S");
    }
    
    public string confirmAndRedirectModalPopup(string popType, string heading, string message, string footerYesButtonText, string footerNoButtonText, string fullURLWithoutDomain)
    {
        string popId = popType + "_" + randomIDNumber() + "modalPopup";
        string closeFunName = popType + "_" + randomIDNumber() + "FunClose";
        string redirectFunName = popType + "_" + randomIDNumber() + "FunRedirect";

        string html = "";

        html += "<style> ";
        html += ".CommonModalPopup {position: fixed; z-index: 999; left: 0px; top: 0px; width: 100%;  overflow: auto; background: rgb(0, 0, 0); background-color: rgba(0,0,0,0.4); -webkit-animation-name: fadeIn; -webkit-animation-duration: 0.4s; animation-name: fadeIn; animation-duration: 0.4s;  }";
        html += " -webkit-animation-name: slideIn; -webkit-animation-duration: 0.4s; animation-name: slideIn;  animation-duration: 0.4s; }";

        html += "@-webkit-keyframes slideIn { from {bottom: -300px; opacity: 0}  to {bottom: 0; opacity: 1}}";
        html += "@keyframes slideIn { from {bottom: -300px; opacity: 0} to {bottom: 0; opacity: 1}}";
        html += "@-webkit-keyframes fadeIn { from {opacity: 0}  to {opacity: 1}}";
        html += "@keyframes fadeIn { from {opacity: 0} to {opacity: 1}}";

        html += " </style>";


        html += " <script type = 'text/javascript'> ";
        html += " function " + closeFunName + "() {  var myCommonModalPopupp = document.getElementById('" + popId + "');  myCommonModalPopupp.style.display = 'none';} ";

        string pathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
        string url_host = HttpContext.Current.Request.Url.AbsoluteUri.Replace(pathAndQuery, "");


        html += " function " + redirectFunName + "() { var myCommonModalPopupp = document.getElementById('" + popId + "');  myCommonModalPopupp.style.display = 'none'; location.href = '" + url_host + "/" + fullURLWithoutDomain + "'; } ";
        //   html += " document.body.appendChild(myCommonModalPopup) ";

        html += "</script>";

        html += "<div  id='" + popId + "' class='CommonModalPopup'>";

        html += "<div class='modal modal-dialog modal-dialog-centered modal-lg'>";

        html += "<div class='modal-content'>";

        html += "<div class='modal-header'>";
        html += "";
        html += " <h3 style=''>" + heading + "</h3>";
        html += "</div>";

        html += "<div class='modal-body overflow-auto' style='max-height: 55vh;'>";
        html += " <p style='margin: 0px;'>" + message + "</p>";
        html += "</div>";

        html += "<div class='modal-footer'>";
        html += " <span class='btn btn-success'  onclick='" + closeFunName + "()'><i class='fa fa-time'></i> &nbsp;" + footerNoButtonText + "</span> ";
        html += " <span class='btn btn-danger'  onclick='" + redirectFunName + "(); return false; '><i class='fa fa-time'></i> &nbsp;" +  footerYesButtonText + "</span> ";
        html += "</div>";
        
        html += "</div>";
        html += "</div>";
        html += "</div>";
        return html;
    }
       
    private string modalPopupComman(string popType, string heading, string message, string footerCloseButtonText, string large_small)
    {
        string popId = popType + "_" + randomIDNumber() + "modalPopup";
        string closeFunName = popType + "_" + randomIDNumber() + "FunClose";

        string html = "";
        html += "<style> ";
        html += ".CommonModalPopup {position: fixed; z-index: 1; left: 0px; top: 0px; width: 100%;  overflow: auto; background: rgb(0, 0, 0); background-color: rgba(0,0,0,0.4); -webkit-animation-name: fadeIn; -webkit-animation-duration: 0.4s; animation-name: fadeIn; animation-duration: 0.4s;  }";
        html += " -webkit-animation-name: slideIn; -webkit-animation-duration: 0.4s; animation-name: slideIn;  animation-duration: 0.4s; }";

        html += "@-webkit-keyframes slideIn { from {bottom: -300px; opacity: 0}  to {bottom: 0; opacity: 1}}";
        html += "@keyframes slideIn { from {bottom: -300px; opacity: 0} to {bottom: 0; opacity: 1}}";
        html += "@-webkit-keyframes fadeIn { from {opacity: 0}  to {opacity: 1}}";
        html += "@keyframes fadeIn { from {opacity: 0} to {opacity: 1}}";

        html += " </style>";

        html += " <script type = 'text/javascript'> ";
        html += " function " + closeFunName + "() {  var myCommonModalPopupp = document.getElementById('" + popId + "');  myCommonModalPopupp.style.display = 'none';} ";
        html += "</script>";

        html += "<div  id='" + popId + "' class='CommonModalPopup'>";


        if (large_small == "L")
        {
            html += "<div class='modal modal-dialog modal-dialog-centered modal-lg'>";
        }
        else { html += "<div class='modal modal-dialog modal-dialog-centered'>"; }


        html += "<div class='modal-content'>";

        html += "<div class='modal-header'>";
        html += "";
        html += " <h3 class='mb-0'>" + heading + "</h3>";
        html += "</div>";

        html += "<div class='modal-body py-2 overflow-auto' style='max-height: 55vh;'>";
        html += " <p style='margin: 0px; color:black;'>" + message + "</p>";
        html += "</div>";


        string popupColorClas = "";
        switch (popType.ToUpper())
        {
            case "S":
                popupColorClas += " btn btn-success ";
                break;
            case "W":
                popupColorClas += " btn btn-warning ";
                break;
            case "I":
                popupColorClas += " btn btn-info ";
                break;
            case "D":
                popupColorClas += " btn btn-danger ";
                break;
            case "P":
                popupColorClas += " btn btn-primary ";
                break;
            default:
                popupColorClas += " btn btn-default ";
                break;
        }

        html += "<div class='modal-footer'>";
        html += " <span class='"+ popupColorClas + "'  onclick='" + closeFunName + "()'><i class='fa fa-time'></i> &nbsp;" + footerCloseButtonText + "</span> ";
        html += "</div>";

        html += "</div>";
        html += "</div>";
        html += "</div>";
        return html;
    }
    
    private readonly Random _random = new Random();
    private string randomIDNumber()
    {
        return _random.Next(11111, 99999).ToString() ;
    }

}