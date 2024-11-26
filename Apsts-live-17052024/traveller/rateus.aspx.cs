using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class traveller_rateus : BasePage 
{
    sbBLL bll = new sbBLL();
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    static int gvindexx = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        checkUser();
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            Session["_moduleName"] = "Rating";
            loadRating(Session["_UserCode"].ToString());
        }
    }

    #region "Method"
    private void checkUser()
    {
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
    private void loadRating(string userId)//M1
    {
        try
        {
            pnlNoTickets.Visible = true;
            wsClass obj = new wsClass();
            DataTable dt = obj.getRatingTicketsNew(userId);
            if (dt.Rows.Count > 0)
            {
                gvRatingTickets.DataSource = dt;
                gvRatingTickets.DataBind();
                pnlNoTickets.Visible = false;
            }
            else
            {
                Response.Redirect("dashboard.aspx");
            }
        }
        catch (Exception ex)
        {
            pnlNoTickets.Visible = true;
            _common.ErrorLog("rateus.aspx-0001", ex.Message);
        }
    }
    private void saveRating(string ticketNo, string userId, Int16 portalRating, Int16 staffRating, Int16 busRating, string portalFeedback, string staffFeedback, string busFeedback)//M2
    {
        try
        {
            string ip = wsClass.getIPAddress();
            wsClass obj = new wsClass();
            DataTable dt = obj.saveRatingsNew(userId,ticketNo, portalRating.ToString(), staffRating.ToString(), busRating.ToString(), portalFeedback, staffFeedback, busFeedback, ip);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["rslt"].ToString() == "done")
                {
                    successmsg("Rate save successfully");
                    loadRating(Session["_UserCode"].ToString());
                }
                else
                {
                    Errormsg("Fail to save.");
                }
            }
            else
            {
                Errormsg("fail to save.");
            }
        }
        catch (Exception ex)
        {
            pnlNoTickets.Visible = true;
            _common.ErrorLog("rateus.aspx-0002", ex.Message);
        }
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Information", msg, "Close");
        Response.Write(popup);
    }
    private void successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Information", msg, "Ok");
        Response.Write(popup);
    }
    private bool validRate(int rate, string text)
    {
        if (rate < 3 && text.Length < 10)
        {
            return false;
        }
        return true;
    }
    #endregion

    #region "Event"
    protected void rcStaff_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        CheckTokan();
        AjaxControlToolkit.Rating rr = (AjaxControlToolkit.Rating)gvRatingTickets.Rows[gvindexx].FindControl("rcStaff");
        TextBox tb = (TextBox)gvRatingTickets.Rows[gvindexx].FindControl("tbRateStaff");

        int rate = (int)rr.CurrentRating;
        tb.Visible = false;
        if (rate < 3)
        {
            tb.Text = "";
            tb.Visible = true;
        }
    }
    protected void rcBus_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        CheckTokan();
        AjaxControlToolkit.Rating rr = (AjaxControlToolkit.Rating)gvRatingTickets.Rows[gvindexx].FindControl("rcBus");
        TextBox tb = (TextBox)gvRatingTickets.Rows[gvindexx].FindControl("tbRateBus");

        int rate = (int)rr.CurrentRating;
        tb.Visible = false;
        if (rate < 3)
        {
            tb.Text = "";
            tb.Visible = true;
        }
    }
    protected void rcPortal_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        CheckTokan();
        AjaxControlToolkit.Rating rr = (AjaxControlToolkit.Rating)gvRatingTickets.Rows[gvindexx].FindControl("rcPortal");
        TextBox tb = (TextBox)gvRatingTickets.Rows[gvindexx].FindControl("tbRatePortal");

        int rate = (int)rr.CurrentRating;
        tb.Visible = false;
        if (rate < 3)
        {
            tb.Text = "";
            tb.Visible = true;
        }
    }
    protected void gvRatingTickets_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CheckTokan();
        if (e.CommandName == "VIEWDETAIL")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            gvindexx = rowIndex;
            foreach (GridViewRow row in gvRatingTickets.Rows)
            {
                LinkButton lbtnn = (LinkButton)row.FindControl("lbtnRate");
                Panel pnll = (Panel)row.FindControl("pnlRate");
                pnll.Visible = false;
                lbtnn.Visible = true;
                row.BackColor = System.Drawing.Color.White;
            }
            LinkButton lbtn = (LinkButton)gvRatingTickets.Rows[rowIndex].FindControl("lbtnRate");
            Panel pnl = (Panel)gvRatingTickets.Rows[rowIndex].FindControl("pnlRate");
            pnl.Visible = true;
            lbtn.Visible = false;
            gvRatingTickets.Rows[rowIndex].BackColor = System.Drawing.Color.WhiteSmoke;

            AjaxControlToolkit.Rating rStaff = (AjaxControlToolkit.Rating)gvRatingTickets.Rows[rowIndex].FindControl("rcStaff");
            rStaff.CurrentRating = 0;
            TextBox tbStaff = (TextBox)gvRatingTickets.Rows[rowIndex].FindControl("tbRateStaff");
            tbStaff.Text = "";
            tbStaff.Visible = false;

            AjaxControlToolkit.Rating rBus = (AjaxControlToolkit.Rating)gvRatingTickets.Rows[rowIndex].FindControl("rcBus");
            rBus.CurrentRating = 0;
            TextBox tbBus = (TextBox)gvRatingTickets.Rows[rowIndex].FindControl("tbRateBus");
            tbBus.Text = "";
            tbBus.Visible = false;

            AjaxControlToolkit.Rating rPortal = (AjaxControlToolkit.Rating)gvRatingTickets.Rows[rowIndex].FindControl("rcPortal");
            rPortal.CurrentRating = 0;
            TextBox tbPortal = (TextBox)gvRatingTickets.Rows[rowIndex].FindControl("tbRatePortal");
            tbPortal.Text = "";
            tbPortal.Visible = false;


        }
        if (e.CommandName == "SAVERATING")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string ticketNo = gvRatingTickets.DataKeys[rowIndex]["ticket_no"].ToString();
            string userId = gvRatingTickets.DataKeys[rowIndex]["booked_by"].ToString();

            AjaxControlToolkit.Rating rStaff = (AjaxControlToolkit.Rating)gvRatingTickets.Rows[rowIndex].FindControl("rcStaff");
            TextBox tbStaff = (TextBox)gvRatingTickets.Rows[rowIndex].FindControl("tbRateStaff");

            AjaxControlToolkit.Rating rBus = (AjaxControlToolkit.Rating)gvRatingTickets.Rows[rowIndex].FindControl("rcBus");
            TextBox tbBus = (TextBox)gvRatingTickets.Rows[rowIndex].FindControl("tbRateBus");

            AjaxControlToolkit.Rating rPortal = (AjaxControlToolkit.Rating)gvRatingTickets.Rows[rowIndex].FindControl("rcPortal");
            TextBox tbPortal = (TextBox)gvRatingTickets.Rows[rowIndex].FindControl("tbRatePortal");

            Int16 rateStaff = (Int16)rStaff.CurrentRating;
            Int16 rateBus = (Int16)rBus.CurrentRating;
            Int16 ratePortal = (Int16)rPortal.CurrentRating;

            string feedbackStaff = tbStaff.Text.Trim();
            string feedbackBus = tbBus.Text.Trim();
            string feedbackPortal = tbPortal.Text.Trim();

            if (rateStaff == 0 || rateBus == 0 || ratePortal == 0)
            {
                Errormsg("Select Stars");
                return;
            }
            if (validRate(rateStaff, feedbackStaff) == false)
            {
                Errormsg("You are giving less then 3 star so please enter feedback for Driver/Conductor (Minimum 10 characters)");
                return;
            }
            if (validRate(rateBus, feedbackBus) == false)
            {
                Errormsg("You are giving less then 3 star so please enter feedback for Bus (Minimum 10 characters)");
                return;
            }
            if (validRate(ratePortal, feedbackPortal) == false)
            {
                Errormsg("You are giving less then 3 star so please enter feedback for Portal (Minimum 10 characters)");
                return;
            }
            saveRating(ticketNo, userId, ratePortal, rateStaff, rateBus, feedbackPortal, feedbackStaff, feedbackBus);
        }
    }
    #endregion


}