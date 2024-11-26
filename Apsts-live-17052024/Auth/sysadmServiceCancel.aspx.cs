using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Auth_sysadmServiceCancel : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    CommonSMSnEmail sms = new CommonSMSnEmail();
    string commonerror = "There is some error. Please contact the administrator or try again after some time.";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            Session["_moduleName"] = "Service Cancel/Block";
            loadServiceTypes(ddlservicetype);
            loadServiceCount();
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
    private void Successmsg(string msg)
    {

        string popup = _popup.modalPopupSmall("S", "Congratulations", msg, "Close");
        Response.Write(popup);
    }
    private void Errormsg(string msg)
    {

        string popup = _popup.modalPopupSmall("W", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupSmall("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    private void loadServiceTypes(DropDownList ddlservicetype)
    {
        try
        {
            ddlservicetype.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_servicetype_getlist");
            //MyCommand.Parameters.AddWithValue("p_srtp_id", 0);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlservicetype.DataSource = dt;
                ddlservicetype.DataTextField = "servicetype_name_en";
                ddlservicetype.DataValueField = "srtpid";
                ddlservicetype.DataBind();
            }
            ddlservicetype.Items.Insert(0, "select");
            ddlservicetype.Items[0].Value = "0";
            ddlservicetype.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlservicetype.Items.Insert(0, "select");
            ddlservicetype.Items[0].Value = "0";
            ddlservicetype.SelectedIndex = 0;
            _common.ErrorLog("sysadmServiceCancel.aspx-0001", ex.Message.ToString());
        }
    }
    private bool validvalue()
    {
        try
        {
            int msgcnt = 0;
            string msg = "";
            if (txtblockdate.Text == "")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Invalid Trip Date. It should be greater or equal to current date. <br/>";

            }
            else
            {

                DateTime date = DateTime.ParseExact(txtblockdate.Text.Trim(), "dd/MM/yyyy", null);
                // DateTime nowdate = DateTime.ParseExact(.ToString(), "dd/MM/yyyy", null);
                if (_validation.IsValidString(txtblockdate.Text.Trim(), 8, 10) == false)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Invalid Trip Date. It should be greater or equal to current date. <br/>";
                }
                else if (date < DateTime.Now.Date)
                {
                    msgcnt = msgcnt + 1;
                    msg = msg + msgcnt.ToString() + ". Invalid Trip Date. It should be greater or equal to current date. <br/>";
                }
            }
            if (ddlservicetype.SelectedValue == "0")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Select Valid Service. <br/>";
            }
            else if (_validation.IsValidInteger(ddlservicetype.SelectedValue, 1, 2) == false)
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Select Valid Service. <br/>";
            }

            if (msgcnt > 0)
            {
                Errormsg(msg);
                return false;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("DepotServiceCancel-M2", ex.Message.ToString());
            return false;

        }
        return true;
    }
    private void GetService(string action, GridView gvservice, HtmlGenericControl htmlGenericControl, TextBox txtservicecode)//M3
    {
        try
        {
            string Servicecode = "0";
            if (!String.IsNullOrEmpty(txtservicecode.Text))
            {
                Servicecode = txtservicecode.Text.ToString();
            }
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_service_blocking");
            MyCommand.Parameters.AddWithValue("p_action", action);
            MyCommand.Parameters.AddWithValue("p_depocode", "0");
            MyCommand.Parameters.AddWithValue("p_date", txtblockdate.Text);
            MyCommand.Parameters.AddWithValue("p_servicetypecode", Convert.ToInt64(ddlservicetype.SelectedValue.ToString()));
            MyCommand.Parameters.AddWithValue("p_servicecode", Convert.ToInt64(Servicecode));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    if (action == "A")
                    {
                        DataRow[] dr = dt.Select("minutes>0");
                        int count = dr.Length;
                        if (count > 0)
                        {
                            gvservice.DataSource = dr.CopyToDataTable();
                            gvservice.DataBind();
                            gvservice.Visible = true;
                            htmlGenericControl.Visible = false;
                        }
                        else
                        {
                            gvservice.Visible = false;
                            htmlGenericControl.Visible = true;
                        }
                    }
                    if (action == "B")
                    {
                        DataRow[] dr = dt.Select("minutes>0");
                        int count = dr.Length;
                        if (count > 0)
                        {
                            gvservice.DataSource = dr.CopyToDataTable();
                            gvservice.DataBind();
                            gvservice.Visible = true;
                            htmlGenericControl.Visible = false;
                        }
                        else
                        {
                            gvservice.Visible = false;
                            htmlGenericControl.Visible = true;
                        }
                    }
                    if (action == "C")
                    {
                        gvservice.DataSource = dt;
                        gvservice.DataBind();
                        gvservice.Visible = true;
                        htmlGenericControl.Visible = false;
                    }
                }
                else
                {
                    gvservice.Visible = false;
                    htmlGenericControl.Visible = true;
                }
            }
            else
            {
                gvservice.Visible = false;
                htmlGenericControl.Visible = true;
            }
        }
        catch (Exception ex)
        {
            gvServices.Visible = false;
            htmlGenericControl.Visible = true;
            _common.ErrorLog("sysadmServiceCancel.aspx-0002", ex.Message.ToString());
        }
    }
    private void loadServiceCount()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_service_blocking_count");
            MyCommand.Parameters.AddWithValue("p_depocode", "0");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                p_totservice.Text = dt.Rows[0]["tot_service"].ToString();
                p_blockservice.Text = dt.Rows[0]["block_service"].ToString();
                p_blcklast.Text = dt.Rows[0]["blocklast_service"].ToString();
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sysadmServiceCancel.aspx-0003", ex.Message.ToString());
        }
    }
    private void ViewTicketDetails(string strpid, string blockeddate)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_view_blocked_service_ticket");
            MyCommand.Parameters.AddWithValue("p_strpid", Convert.ToInt64(strpid));
            MyCommand.Parameters.AddWithValue("p_date", blockeddate);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                grdtkt.DataSource = dt;
                grdtkt.DataBind();
                mptktview.Show();
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sysadmServiceCancel.aspx-0004", ex.Message.ToString());
        }
    }
    private void blockService()
    {
        try
        {
            string Mreason = "";
            Mreason = "Service Code " + lblServiceCode.Text.Trim() + Session["_Triptype"].ToString() + Session["_STRP"].ToString() + " From " + lblSource.Text + " To " + lblDestination.Text + " Date/Time " + txtblockdate.Text.Trim() + " " + lbldeparttime.Text.Trim() + " has been cancelled due to " + tbReason.Text.Trim();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_blocked_service");
            MyCommand.Parameters.AddWithValue("p_servicecode", Convert.ToInt64(lblServiceCode.Text.Trim()));
            MyCommand.Parameters.AddWithValue("p_strp_id", Convert.ToInt32(Session["_STRP"].ToString()));
            MyCommand.Parameters.AddWithValue("p_journeydate", txtblockdate.Text.Trim());
            MyCommand.Parameters.AddWithValue("p_journeytype", Session["_Triptype"].ToString());
            MyCommand.Parameters.AddWithValue("p_reaseon", Mreason);
            MyCommand.Parameters.AddWithValue("p_blockby", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
		 int i = 0;
                foreach (DataRow drow in dt.Rows)
                {
                    string pnr = dt.Rows[i]["ticketno"].ToString();
                    string bookby = dt.Rows[i]["book_by_type_code"].ToString();
                    string trvlmob = dt.Rows[i]["mobile"].ToString();
                    
                    sms.ServiceBlockCancel(bookby, trvlmob, pnr);
                    i = i + 1;
                }
                tbReason.Text = "";
                lblSource.Text = "";
                lblDestination.Text = "";
                Successmsg("Service Successfully Blocked");
                GetService("C", grdblockedservice, lblhaveblockedservicesmsg, txtblockedservicecode);
                GetService("B", grdbookedservice, lblhavebookedservicesmsg, txthavebookedservicecode);
                GetService("A", gvServices, lblhavenotbookedservicesmsg, txthavenotbookedservicecode);
            }
            else
            {
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
            _common.ErrorLog("sysadmServiceCancel.aspx-0005", ex.Message.ToString());
        }
    }
    #endregion

    #region "Events"
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvalue() == false)
        {
            return;
        }
        GetService("C", grdblockedservice, lblhaveblockedservicesmsg, txtblockedservicecode);
        GetService("B", grdbookedservice, lblhavebookedservicesmsg, txthavebookedservicecode);
        GetService("A", gvServices, lblhavenotbookedservicesmsg, txthavenotbookedservicecode);
    }
    protected void grdblockedservice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        CsrfTokenValidate();
        grdblockedservice.PageIndex = e.NewPageIndex;
        GetService("C", grdblockedservice, lblhaveblockedservicesmsg, txtblockedservicecode);
    }
    protected void grdbookedservice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        CsrfTokenValidate();
        grdbookedservice.PageIndex = e.NewPageIndex;
        GetService("B", grdbookedservice, lblhavebookedservicesmsg, txthavebookedservicecode);
    }
    protected void gvServices_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvServices.PageIndex = e.NewPageIndex;
        GetService("A", gvServices, lblhavenotbookedservicesmsg, txthavenotbookedservicecode);
    }
    protected void grdbookedservice_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "Select")
        {
            lblServiceCode.Text = grdbookedservice.DataKeys[index].Values["service_code"].ToString();
            lbltripdirection.Text = grdbookedservice.DataKeys[index].Values["journey_type"].ToString();
            lblSourcess.Text = grdbookedservice.DataKeys[index].Values["servicename"].ToString();
            lblSource.Text = grdbookedservice.DataKeys[index].Values["src"].ToString();
            Session["_lblSource"] = grdbookedservice.DataKeys[index].Values["src"].ToString();
            lblDestination.Text = grdbookedservice.DataKeys[index].Values["des"].ToString();
            Session["_lblDest"] = grdbookedservice.DataKeys[index].Values["des"].ToString();
            // lblJourneyType.Text = gvServices.DataKeys(index).Values("Journeytype").ToString
            Session["_Triptype"] = grdbookedservice.DataKeys[index].Values["journey_type"].ToString();
            lbldeparttime.Text = grdbookedservice.DataKeys[index].Values["departure_time"].ToString();
            Session["_STRP"] = grdbookedservice.DataKeys[index].Values["strpid"].ToString();
            mpsevicedetails.Show();
        }
        if (e.CommandName == "View")
        {
            ViewTicketDetails(grdbookedservice.DataKeys[index].Values["strpid"].ToString(), txtblockdate.Text);
        }

    }
    protected void gvServices_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "Select")
        {
            lblServiceCode.Text = gvServices.DataKeys[index].Values["service_code"].ToString();
            lbltripdirection.Text = gvServices.DataKeys[index].Values["journey_type"].ToString();
            lblSourcess.Text = gvServices.DataKeys[index].Values["servicename"].ToString();
            lblSource.Text = gvServices.DataKeys[index].Values["src"].ToString();
            Session["_lblSource"] = gvServices.DataKeys[index].Values["src"].ToString();
            lblDestination.Text = gvServices.DataKeys[index].Values["des"].ToString();
            Session["_lblDest"] = gvServices.DataKeys[index].Values["des"].ToString();
            Session["_Triptype"] = gvServices.DataKeys[index].Values["journey_type"].ToString();
            Session["_STRP"] = gvServices.DataKeys[index].Values["strpid"].ToString();
            lbldeparttime.Text = gvServices.DataKeys[index].Values["departure_time"].ToString();
            mpsevicedetails.Show();
        }
    }
    protected void lbtnhavenotbookedservicesearch_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (txtblockdate.Text == "")
        {
            Errormsg("Please Enter Blocked Date");
            return;
        }
        if (ddlservicetype.SelectedValue == "0")
        {
            Errormsg("Please Select Service Type");
            return;
        }
        if (txthavenotbookedservicecode.Text == "")
        {
            Errormsg("Please Enter Service Code");
            return;
        }
        GetService("A", gvServices, lblhavenotbookedservicesmsg, txthavenotbookedservicecode);

    }
    protected void lbtnhavebookedservicesearch_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (txtblockdate.Text == "")
        {
            Errormsg("Please Enter Blocked Date");
            return;
        }
        if (ddlservicetype.SelectedValue == "0")
        {
            Errormsg("Please Select Service Type");
            return;
        }
        if (txthavebookedservicecode.Text == "")
        {
            Errormsg("Please Enter Service Code");
            return;
        }
        GetService("B", grdbookedservice, lblhavebookedservicesmsg, txthavebookedservicecode);

    }
    protected void lbtnblockedclick_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (txtblockdate.Text == "")
        {
            Errormsg("Please Enter Blocked Date");
            return;
        }
        if (ddlservicetype.SelectedValue == "0")
        {
            Errormsg("Please Select Service Type");
            return;
        }
        if (txtblockedservicecode.Text == "")
        {
            Errormsg("Please Enter Service Code");
            return;
        }
        GetService("C", grdblockedservice, lblhaveblockedservicesmsg, txtblockedservicecode);

    }
    protected void btnServiceBlock_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (tbReason.Text == "")
        {
            Errormsg(" Please enter a valid Reason. ");
            return;
        }
        blockService();
    }
    #endregion
}