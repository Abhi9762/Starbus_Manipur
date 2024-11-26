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

public partial class Auth_DepotServiceCancel : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    string commonerror = "There is some error. Please contact the administrator or try again after some time.";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:ss tt");
        if (!IsPostBack)
        {
            Session["_moduleName"] = "Service Cancel/Block";
            loadServiceTypes(ddlservicetype);
            loadServiceTypes(ddlrptservicetype);

        }
    }

    #region "Method"
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
    private void loadServiceTypes(DropDownList ddlservicetype)//M1
    {
        try
        {
            ddlservicetype.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_servicetype_getlist");
            MyCommand.Parameters.AddWithValue("p_srtp_id", 0);
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
            _common.ErrorLog("DepotServiceCancel-M1", ex.Message.ToString());
        }
    }
    private bool validvalue()//M2
    {
        try
        {
            int msgcnt = 0;
            string msg = "";
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
    private void GetService(GridView gvDepotServices, Panel pnlNoDepotService)//M3
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
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_service_get_blocking");
            MyCommand.Parameters.AddWithValue("p_depotcode", Session["_LDepotCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_date", txtblockdate.ToString());
            MyCommand.Parameters.AddWithValue("p_servicetypecode", Convert.ToInt16(ddlservicetype.SelectedValue.ToString()));
            MyCommand.Parameters.AddWithValue("p_servicecode", Servicecode);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvDepotServices.DataSource = dt;
                    gvDepotServices.DataBind();
                    gvDepotServices.Visible = true;
                    pnlNoDepotService.Visible = false;
                }
                else
                {
                    gvDepotServices.Visible = false;
                    pnlNoDepotService.Visible = true;
                }
            }
            else
            {
                gvDepotServices.Visible = false;
                pnlNoDepotService.Visible = true;
            }
        }
        catch (Exception ex)
        {
            gvDepotServices.Visible = false;
            pnlNoDepotService.Visible = true;
            _common.ErrorLog("DepotServiceCancel-M3", ex.Message.ToString());
        }
    }
    private void LoadTrip(GridView gvtrips, string dsvcID, Panel pnlNoTrips)//M4
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_service_get_trips");
            MyCommand.Parameters.AddWithValue("p_depotcode", Session["_LDepotCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_date", txtblockdate.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_servicetypecode", Convert.ToInt16(ddlservicetype.SelectedValue.ToString()));
            MyCommand.Parameters.AddWithValue("p_servicecode", Convert.ToInt64(dsvcID));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvtrips.DataSource = dt;
                    gvtrips.DataBind();
                    gvtrips.Visible = true;
                    pnlNoTrips.Visible = false;
                }
                else
                {
                    gvtrips.Visible = false;
                    pnlNoTrips.Visible = true;
                }
            }
            else
            {
                gvtrips.Visible = false;
                pnlNoTrips.Visible = true;
            }
        }
        catch (Exception ex)
        {
            gvtrips.Visible = false;
            pnlNoTrips.Visible = true;
            _common.ErrorLog("DepotServiceCancel-M4", ex.Message.ToString());
        }
    }
    private void loadblockreason(DropDownList ddlreason)//M5
    {
        try
        {
            ddlreason.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_blockreason");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlreason.DataSource = dt;
                ddlreason.DataTextField = "blockedreasonname";
                ddlreason.DataValueField = "blockedreasoncode";
                ddlreason.DataBind();
            }
            ddlreason.Items.Insert(0, "Select");
            ddlreason.Items[0].Value = "0";
            ddlreason.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlreason.Items.Insert(0, "Select");
            ddlreason.Items[0].Value = "0";
            ddlreason.SelectedIndex = 0;
            _common.ErrorLog("DepotServiceCancel-M5", ex.Message.ToString());
        }
    }
    private bool validvalueTrip()//M6
    {
        try
        {
            int msgcnt = 0;
            string msg = "";
            if (ddlreason.SelectedValue == "0")
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Select Block Reason. <br/>";
            }
            if (txtremark.Text.Length == 0)
            {
                msgcnt = msgcnt + 1;
                msg = msg + msgcnt.ToString() + ". Enter Block Reason Remark. <br/>";
            }
            if (msgcnt > 0)
            {
                Errormsg(msg);
                return false;
            }

        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
            _common.ErrorLog("DepotServiceCancel-M6", ex.Message.ToString());
            return false;
        }
        return true;
    }
    private void BlockTrip()//M7
    {
        try
        {
            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_trip_block");
            MyCommand.Parameters.AddWithValue("p_depot_service_code", Convert.ToInt64(Session["dsvcID"].ToString()));
            MyCommand.Parameters.AddWithValue("p_triptype", Session["trip_type"].ToString().Trim());
            MyCommand.Parameters.AddWithValue("p_trip", Convert.ToInt16(Session["strp_id"].ToString()));
            MyCommand.Parameters.AddWithValue("p_journey_date", txtblockdate.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_reason_code", Convert.ToInt16(ddlreason.SelectedValue.ToString()));
            MyCommand.Parameters.AddWithValue("p_remark", txtremark.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    string blockrefno = dt.Rows[0]["p_blockrefno"].ToString();
                    if (blockrefno == "ERROR" || blockrefno == "EXCEPTION")
                    {
                        Errormsg(commonerror);
                    }
                    else
                    {
                        Successmsg("Trip Successfully Blocked");
                        LoadTrip(gvtrips, Session["dsvcID"].ToString(), pnlNoTrips);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("DepotServiceCancel-M7", ex.Message.ToString());
        }
    }
    #endregion

    #region "Event"
    protected void lbtnsearch_Click(object sender, EventArgs e)
    {
        if (validvalue() == false)
        {
            return;
        }
        GetService(gvDepotServices, pnlNoDepotService);
        gvtrips.Visible = false;
        pnlNoTrips.Visible = true;
    }
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        string msg = "";
        msg = msg + "1. Check date and service type before blocking.<br/>";
        msg = msg + "2. Once a service is blocked, that service cannot be unblocked.<br/>";
        msg = msg + "3. If service tickets are booked online, then their cancellation will be auto.<br/>";
        msg = msg + "4. If there are booking tickets from the counter or agent, then they will be cancelled only, their refund will be from the respective counter and agent.<br/>";
        InfoMsg(msg);
    }
    protected void gvDepotServices_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDepotServices.PageIndex = e.NewPageIndex;
        GetService(gvDepotServices, pnlNoDepotService);
    }
    protected void gvDepotServices_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "viewtrips")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string dsvcID = gvDepotServices.DataKeys[index].Values["dsvc_id"].ToString();
            Session["dsvcID"] = dsvcID;
            LoadTrip(gvtrips, dsvcID, pnlNoTrips);
        }
    }
    protected void gvtrips_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Session["trip_type"] = gvtrips.DataKeys[index].Values["trip_direction"].ToString();
            Session["strp_id"] = gvtrips.DataKeys[index].Values["strp_id"].ToString();
            trip.Text = "Block/Cancel Trip  " + gvtrips.DataKeys[index].Values["fromstaion"].ToString() + " - " + gvtrips.DataKeys[index].Values["tostation"].ToString() + " - " + gvtrips.DataKeys[index].Values["trip_type"].ToString();
            loadblockreason(ddlreason);
            mpConfirmation.Show();
        }
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (validvalueTrip() == false)
        {
            mpConfirmation.Show();
            return;
        }
        BlockTrip();
    }
    #endregion







}