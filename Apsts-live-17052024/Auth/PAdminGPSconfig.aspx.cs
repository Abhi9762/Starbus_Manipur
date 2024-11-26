using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_PAdminGPSconfig : BasePage 
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvGPSUrls.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                ((CheckBox)row.FindControl("cbGPSurls")).Attributes.Add("onchange", "javascript:TextboxAutoEnableAndDisable(" + (row.RowIndex) + ");");
            }
        }

        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            Session["_moduleName"] = "GPS config";
            LoadGPSurls();
        }
    }

    #region "Method"
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
        }
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Please Note", msg, "Close");
        Response.Write(popup);
    }
    private void SuccessMsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "OK");
        Response.Write(popup);
    }
    private void insertGPSUrls()//M1
    {
        try
        {
            for (int i = 0; i < gvGPSUrls.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gvGPSUrls.Rows[i].FindControl("cbGPSurls");
                HiddenField agencycode = (HiddenField)gvGPSUrls.Rows[i].FindControl("hdnAgencyCode");
                string ipaddress = HttpContext.Current.Request.UserHostAddress;
                TextBox url = (TextBox)gvGPSUrls.Rows[i].FindControl("tbGpsUrls");
                if (chk.Checked == true)
                {

                    string GPS;
                    GPS = url.Text.Trim().ToString();
                    NpgsqlCommand MyCommand = new NpgsqlCommand();
                    MyCommand.Parameters.Clear();
                    string Mresult = "";
                    MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.portal_gps_urlupdate");
                    MyCommand.Parameters.AddWithValue("p_agencycode", agencycode.Value.ToString());
                    MyCommand.Parameters.AddWithValue("p_gpsurls", GPS);
                    MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
                    MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
                    Mresult = bll.UpdateAll(MyCommand);
                    if (Mresult == "Success")
                    {
                        SuccessMsg("GPS Urls Saved Successfully");
                    }
                    else
                    {
                        Errormsg("Error Ocurred !Please Check");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGPSconfig.aspx-0001", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    protected void LoadGPSurls()//M2
    {
        try
        {
            lbtnSave.Visible = false;
            lblNoData.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_gps_urls");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvGPSUrls.DataSource = dt;
                gvGPSUrls.DataBind();
                gvGPSUrls.Visible = true;
                lbtnSave.Visible = true;
                lblNoData.Visible = false;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGPSconfig.aspx-0002", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    protected void GPSHistoryList()//M3
    {
        try
        {
            gvHistory.Visible = false;
            lblNoHistory.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_gps_urls_history");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvHistory.DataSource = dt;
                gvHistory.DataBind();
                gvHistory.Visible = true;
                lblNoHistory.Visible = false;


            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGPSconfig.aspx-0003", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
        }
    }
    #endregion

    #region "Event"
    protected void lbtnHelp_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Errormsg("Coming Soon");

    }
    protected void lbtnViewHistory_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        mplimithistory.Show();
        GPSHistoryList();
    }
    protected void gvGPSUrls_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvGPSUrls.PageIndex = e.NewPageIndex;
        LoadGPSurls();
    }    
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        int count = 0;
        int a = 0;
        for (int i = 0; i < gvGPSUrls.Rows.Count; i++)
        {
            TextBox url = (TextBox)gvGPSUrls.Rows[i].FindControl("tbGpsUrls");           
            if (string.IsNullOrEmpty(url.Text.Trim().ToString()) == true || url.Text.Trim().ToString() == "" || _validation.isValidURL(url.Text))
            {
                count = count + 1;
            }
            CheckBox chk = (CheckBox)gvGPSUrls.Rows[i].FindControl("cbGPSurls");           
            if (chk.Checked == true)
            {
                a = a + 1;
            }

        }
        if (count > 0 )
        {
            Errormsg("GPS url cannot be blank and select checkbox");
        }
        else if(a < 1)
        {
            Errormsg("At least Select one checkbox.");
        }     
        else
        {
            Session["Action"] = "S";
            lblConfirmation.Text = "Do you want to Save GPS Urls?";
            mpConfirmation.Show();
        }       
    }      
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Session["Action"].ToString() == "S")
        {
            insertGPSUrls();
        }
    }
    protected void gvHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHistory.PageIndex = e.NewPageIndex;
        GPSHistoryList();
        mplimithistory.Show();
    }
    protected void gvGPSUrls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkbox = (CheckBox)e.Row.FindControl("cbGPSurls");

            if (e.Row.Cells[2].Text != "")
            {
                chkbox.Enabled = false;
            }
            else
            {
                chkbox.Enabled = true;
            }
        }
    }
    #endregion


}
