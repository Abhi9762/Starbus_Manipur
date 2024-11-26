using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class traveller_ticketStatus : BasePage 
{

    sbBLL bll = new sbBLL();
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    string commonerror = "There is some error. Please contact to helpdesk or try again after some time.";
    protected void Page_Load(object sender, EventArgs e)
    {
        checkUser();

        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            Session["_moduleName"] = "Ticket Status";
            onload();
        }
    }


    #region "Motheds"
    private void checkUser()
    {
        if (Session["_RoleCode"] == null)
        {
            Response.Redirect("../errorpage.aspx");
            return;
        }
        if (_security.isSessionExist(Session["_UserCode"]) == true)
        {
            Session["_UserCode"] = Session["_UserCode"];
        }
        else
        {
            Response.Redirect("errorpage.aspx");
        }
    }
    private void CheckTokan()
    {
        if (Session["rndNoCheck"] == null || Session["rndNoCheck"].ToString() == "")
        {
            Response.Redirect("../errorpage.aspx");
        }

        if (Session["rndNoCheck"].ToString() != hidtoken.Value.ToString())
        {
            Response.Redirect("../errorpage.aspx");
        }
    }
    void PageOpen(string title, string src)
    {
        lblTitle.InnerText = title;
        embedPage.Src = src;
        mpePage.Show();
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Information", msg, "Close");
        Response.Write(popup);
    }

    public void openSubDetailsWindow(string MModuleName)
    {
        try
        {
            string murl;
            murl = MModuleName + "?rt=" + DateTime.Now.ToString();
            string b = Request.Browser.Type.Substring(0, 2);
            if (b.ToUpper().Trim() == "IE")
                Response.Write("<SCRIPT language='javascript'>window.showModalDialog('" + murl + "','name','dialogWidth:830px;dialogHeight:900px');</script>");
            else
            {
                // Dim url As String = "GenQrySchStages.aspx"
                string fullURL = "window.open('" + murl + "', '_blank', 'height=900,width=830,status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
                // ScriptManager.RegisterStartupScript(Me, GetType(String), "OPEN_WINDOW", fullURL, True)
                string script = "window.open('" + fullURL + "','')";
                if ((ClientScript.IsClientScriptBlockRegistered("NewWindow") == false))
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "NewWindow", fullURL, true); // 
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("ticketStatus.aspx-0001", ex.Message);
        }
    }
    public void onload()
    {
        try
        {
            if (_security.isSessionExist(Session["TicketNumber"]) == true)
            {
                loadJourneyDetails(Session["TicketNumber"].ToString());
                loadPassengerDetails(Session["TicketNumber"].ToString());
                CommonSMSnEmail sms = new CommonSMSnEmail();
                sms.sendTicketConfirm_SMSnEMAIL(Session["TicketNumber"].ToString(), Session["_UserCode"].ToString(), "");

            }
            else
            {
                Response.Redirect("errorpage.aspx");
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.ToString());
            _common.ErrorLog("ticketStatus.aspx-0002", ex.Message);
        }
    }
    private void loadJourneyDetails(string ticketNo)//M1
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = obj.getJourneyDetails(ticketNo, "0");

            if (dt.Rows.Count > 0)
            {
                lblTicketNo.Text = dt.Rows[0]["_ticketno"].ToString();
                lblFromStation.Text = dt.Rows[0]["fromstn_name"].ToString();
                lblToStation.Text = dt.Rows[0]["tostn_name"].ToString();
                lblServiceType.Text = dt.Rows[0]["service_type_name"].ToString();
                lblDate.Text = dt.Rows[0]["journeydate"].ToString();
                lblDeparture.Text = dt.Rows[0]["trip_time"].ToString();
                lblBoardingStation.Text = dt.Rows[0]["boardingstn_name"].ToString();

                if (dt.Rows[0]["current_status"].ToString() == "A")
                {
                    pnlFail.Visible = false;
                    pnlSuccess.Visible = true;
                }
                else
                {
                    pnlFail.Visible = true;
                    pnlSuccess.Visible = false;
                }
            }
            else
            {
                pnlFail.Visible = true;
                pnlSuccess.Visible = false;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("ticketStatus.aspx-0003", ex.Message);
        }
    }
    private void loadPassengerDetails(string ticketNo)//M2
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = obj.getPassengerDetails(ticketNo, "0");

            if (dt.Rows.Count > 0)
            {
                gvPassengers.DataSource = dt;
                gvPassengers.DataBind();
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("ticketStatus.aspx-0004", ex.Message);
        }
    }
    #endregion

    #region "Event"
    protected void lbtnPrint_Click(object sender, EventArgs e)
    {
        CheckTokan();
        if (_security.isSessionExist(Session["TicketNumber"]) == true)
        {
            Session["p_ticketNo"] = Session["TicketNumber"];
            PageOpen("e-Ticket", "../E_ticket.aspx");
            //openSubDetailsWindow("../E_ticket.aspx");
        }
        else
        {
            Response.Redirect("errorpage.aspx");
        }
    }
    protected void lbtnHome_Click(object sender, EventArgs e)
    {
        CheckTokan();
        Session["_RNDIDENTIFIERSTRVL"] = _security.GeneratePassword(10, 5);
        Session.Remove("TicketNumber");
        Response.Redirect("Dashboard.aspx");
    }
    #endregion

}
