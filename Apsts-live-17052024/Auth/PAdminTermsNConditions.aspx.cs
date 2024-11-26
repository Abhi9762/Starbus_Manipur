using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_PAdminTermsNConditions : BasePage
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string p_action = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        Session["_moduleName"] = "Content Managment System";
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            loadTermsNConditions();
        }
    }

    #region "Methods"
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
        }
    }
    public void loadTermsNConditions()//M1
    {

        try
        {
            lblupdatemsg.Text = "Update Terms and Conditions for Ticket";
            tbTermsNCondition.Text = "";
            lblUpdationDate.Text = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_terms_condition_getdetails");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    tbTermsNCondition.Text = HttpUtility.HtmlDecode(dt.Rows[0][1].ToString());
                    lblUpdationDate.Text = "<br/>Last Updated on " + dt.Rows[0][0].ToString();

                }
            }
        }
        catch (Exception ex)
        {
            tbTermsNCondition.Text = "";
            lblUpdationDate.Text = "";
            _common.ErrorLog("PAdminTermsNConditions.aspx-0001", ex.Message.ToString());
        }
    }
    public void loadprivacypolicy()//M2
    {
        try
        {
            lblupdatemsg.Text = "Update Privacy Policy for Weibsite";
            tbTermsNCondition.Text = "";
            lblUpdationDate.Text = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_privacy_policy_getdetails");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    tbTermsNCondition.Text = HttpUtility.HtmlDecode(dt.Rows[0][0].ToString());
                  //  lblUpdationDate.Text = "<br/>Last Updated on " + dt.Rows[0][0].ToString();

                }
            }
        }
        catch (Exception ex)
        {
            tbTermsNCondition.Text = "";
            lblUpdationDate.Text = "";
            _common.ErrorLog("PAdminTermsNConditions.aspx-0002", ex.Message.ToString());
        }
    }
    public void loaddisclaimer()//M3
    {
        try
        {
            lblupdatemsg.Text = "Update Disclaimer for Website";
            tbTermsNCondition.Text = "";
            lblUpdationDate.Text = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_disclaimer_getdetails");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    tbTermsNCondition.Text = HttpUtility.HtmlDecode(dt.Rows[0][1].ToString());
                    lblUpdationDate.Text = "<br/>Last Updated on " + dt.Rows[0][0].ToString();

                }
            }
        }
        catch (Exception ex)
        {
            tbTermsNCondition.Text = "";
            lblUpdationDate.Text = "";
            _common.ErrorLog("PAdminTermsNConditions.aspx-0003", ex.Message.ToString());
        }
    }
    private void loadPHistory()
    {
        try
        {
            lblupdatemsg.Text = "Update History for Website";
            tbTermsNCondition.Text = "";
            lblUpdationDate.Text = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_history_getdetails");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    tbTermsNCondition.Text = HttpUtility.HtmlDecode(dt.Rows[0][0].ToString());
                  //  lblUpdationDate.Text = "<br/>Last Updated on " + dt.Rows[0][0].ToString();

                }
            }
        }
        catch (Exception ex)
        {
            tbTermsNCondition.Text = "";
            lblUpdationDate.Text = "";
            _common.ErrorLog("PAdminTermsNConditions.aspx-0004", ex.Message.ToString());
        }
    }
    private void loadLabour()
    {
        try
        {
            lblupdatemsg.Text = "Update Labour Welfare for Website";
            tbTermsNCondition.Text = "";
            lblUpdationDate.Text = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_labourwelfare_getdetails");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    tbTermsNCondition.Text = HttpUtility.HtmlDecode(dt.Rows[0][1].ToString());
                    lblUpdationDate.Text = "<br/>Last Updated on " + dt.Rows[0][0].ToString();

                }
            }
        }
        catch (Exception ex)
        {
            tbTermsNCondition.Text = "";
            lblUpdationDate.Text = "";
            _common.ErrorLog("PAdminTermsNConditions.aspx-0005", ex.Message.ToString());
        }
    }
    public void saveTermsNConditions(string P_action)//M4
    {
        try
        {
            string mTermsconditiontext = HttpUtility.HtmlEncode(tbTermsNCondition.Text.ToString());
            string mUpdatedBy = Session["_UserCode"].ToString();
            string mIpAddress = HttpContext.Current.Request.UserHostAddress;

            string result = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_terms_condition_insertupdate");
            MyCommand.Parameters.AddWithValue("p_termcondition_text", mTermsconditiontext);
            MyCommand.Parameters.AddWithValue("p_updatedby", mUpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", mIpAddress);
            MyCommand.Parameters.AddWithValue("p_action", P_action);
            result = bll.UpdateAll(MyCommand);


            if (result == "Success")
            {
                string msg = "";
                Session["p_action"] = null;
                if (rbtntermsncndi.Checked == true)
                {
                    msg = "Ticket Terms and Conditions have successfully been saved";
                }
                if (rbtnpripolcy.Checked == true)
                {
                    msg = "Website Privacy Policy have successfully been saved";
                }
                if (rbtndisc.Checked == true)
                {
                    msg = "Website Disclaimer have successfully been saved";
                }
                if (rbtnLabour.Checked == true)
                {
                    msg = "Labour Welfare have successfully been saved";
                }
                if (rbtnhistory.Checked == true)
                {
                    msg = "History have successfully been saved";
                }
                Successmsg(msg);
            }
            else
            {
                Errormsg("Error Occurred while saving, please contact systenm admin");
                _common.ErrorLog("PAdminTermsNConditions.aspx-0006", result);
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error.");
            _common.ErrorLog("PAdminTermsNConditions.aspx-0007", ex.Message.ToString());
        }
    }
    private void loadHistory()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_terms_condition_getHistory");
            dt = bll.SelectAll(MyCommand);

            if (dt.Rows.Count > 0)
            {
                gvHistory.DataSource = dt;
                gvHistory.DataBind();
                mpViewHistory.Show();
            }
            else
            {
                Errormsg("Sorry details have not been updated yet");
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminTermsNConditions.aspx-0008", ex.Message.ToString());
        }
       

    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Congratulations", msg, "Close");
        Response.Write(popup);
    }
    private void ConfirmMsg(string msg)
    {
        lblConfirmation.Text = msg;
        mpConfirmation.Show();
    }
    #endregion

    #region "Events"
    protected void rbtntermsncndi_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["_moduleName"] = "Terms and Conditions";
        loadTermsNConditions();
    }
    protected void rbtnpripolcy_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["_moduleName"] = "Privacy Policy";
        loadprivacypolicy();
    }
    protected void rbtndisc_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["_moduleName"] = "Disclaimer";
        loaddisclaimer();
    }
    protected void rbtnLabour_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["_moduleName"] = "Labour";
        loadLabour();
    }
    protected void rbtnhistory_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["_moduleName"] = "History";
        loadPHistory();
    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
 string htmltext = HttpUtility.HtmlDecode(tbTermsNCondition.Text.ToLower().ToString());
       // if ( htmltext.Contains("method") || htmltext.Contains("form") || htmltext.Contains("input type") || htmltext.Contains("checkbox") || htmltext.Contains("button") || htmltext.Contains("textbox") || htmltext.Contains("<body") || htmltext.Contains("<head"))
            if (htmltext.Contains("method") || htmltext.Contains("FORM") || htmltext.Contains("input type") || htmltext.Contains("checkbox") || htmltext.Contains("button") || htmltext.Contains("textbox") || htmltext.Contains("<body") || htmltext.Contains("<head"))

            {
                Errormsg("Invalid Text");
            return;
        }
        if (rbtntermsncndi.Checked == true)
        {
            Session["p_action"] = "T";
            msg = "Do you want to Save/Update Terms & Condition ?";
        }
        if (rbtnpripolcy.Checked == true)
        {
            Session["p_action"] = "P";
            msg = "Do you want to Save/Update Privacy Policy ?";
        }
        if (rbtndisc.Checked == true)
        {
            Session["p_action"] = "D";
            msg = "Do you want to Save/Update Disclaimer ?";
        }
        if (rbtnLabour.Checked == true)
        {
            Session["p_action"] = "L";
            msg = "Do you want to Save/Update Labour Welfare ?";
        }
        if (rbtnhistory.Checked == true)
        {
            Session["p_action"] = "H";
            msg = "Do you want to Save/Update History ?";
        }
        ConfirmMsg(msg);

    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        saveTermsNConditions(Session["p_action"].ToString());
    }
    protected void lbtnResetT_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadTermsNConditions();
    }
    protected void lbtnView_History(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        loadHistory();
    }
    protected void gvHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHistory.PageIndex = e.NewPageIndex;
        loadHistory();


    }//  E6
    #endregion




    

    
}