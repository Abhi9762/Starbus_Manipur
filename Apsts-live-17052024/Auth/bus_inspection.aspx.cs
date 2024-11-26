using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Auth_bus_inspection : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            loadBuses(7);//pending
            loadBuses(2);//verified
            loadBuses(6);//faulty
        }
    }

    #region "Methods"
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupSmall("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    private void ConfirmMsg(string msg)
    {
        lblConfirmation.Text = msg;
        mpConfirmation.Show();
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
    private void loadBuses(int status)
    {
        try
        {
            if (status == 7)
            {
                divPending.Visible = true;
                grdPending.Visible = false;
            }
            if (status == 2)
            {
                grdVerified.Visible = false;
                divVerified.Visible = true;
            }
            if (status == 6)
            {
                divFaulty.Visible = true;
                grdFaulty.Visible = false;
            }
            string officeid = Session["_LDepotCode"].ToString();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_get_bus_list_verification");
            MyCommand.Parameters.AddWithValue("p_depotcode", officeid);
            MyCommand.Parameters.AddWithValue("p_search", "");
            MyCommand.Parameters.AddWithValue("p_status", status);
            dt = bll.SelectAll(MyCommand);
            //Errormsg(dt.TableName);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    if (status == 7)
                    {
                        grdPending.DataSource = dt;
                        grdPending.DataBind();
                        divPending.Visible = false;
                        grdPending.Visible = true;
                    }
                    if (status == 2)
                    {
                        grdVerified.DataSource = dt;
                        grdVerified.DataBind();
                        divVerified.Visible = false;
                        grdVerified.Visible = true;
                    }
                    if (status == 6)
                    {
                        grdFaulty.DataSource = dt;
                        grdFaulty.DataBind();
                        divFaulty.Visible = false;
                        grdFaulty.Visible = true;
                    }
                }
                else
                {
                    if (status == 7)
                    {
                        divPending.Visible = true;
                        grdPending.Visible = false;
                    }
                    if (status == 2)
                    {
                        grdVerified.Visible = false;
                        divVerified.Visible = true;
                    }
                    if (status == 6)
                    {
                        divFaulty.Visible = true;
                        grdFaulty.Visible = false;
                    }
                }
            }
            else
            {
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
    private void verifyBus()
    {
        try
        {
            string bus = Session["_busno"].ToString();
            string UpdatedBy = Session["_UserCode"].ToString();
            string ipaddress = HttpContext.Current.Request.UserHostAddress;
            string officeid = Session["_LDepotCode"].ToString();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "dutyallocation.f_verify_bus");
            MyCommand.Parameters.AddWithValue("p_busno", bus);
            MyCommand.Parameters.AddWithValue("p_status", Convert.ToInt32(ddlStatus.SelectedValue));
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["sp_status"].ToString() == "Done")
                    {
                        Successmsg("Bus Verification Marked Successfully");
                        loadBuses(7);//pending
                        loadBuses(2);//verified
                        loadBuses(6);//faulty
                    }
                    else
                    {
                        Errormsg("Something Went Wrong" + dt.Rows[0]["sp_status"].ToString());
                    }
                }
                else
                {
                    Errormsg("Something Went Wrong");
                }
            }
            else
            {
                Errormsg("Something Went Wrong");
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
    private void searchBus()
    {
        try
        {
            DataTable dt = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_bus_details");
            MyCommand.Parameters.AddWithValue("p_search", Session["_busno"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvBusSearch.DataSource = dt;
                gvBusSearch.DataBind();

                mpBusData.Show();
            }
            else
            {
                Errormsg("No Data Found, Please input correct details");
            }

        }
        catch (Exception ex)
        {
            Errormsg("Something Went Wrong");
        }
    }

    #endregion

    #region "Events"
    protected void grdPending_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPending.PageIndex = e.NewPageIndex;
        loadBuses(7);//pending
    }
    protected void grdVerified_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdVerified.PageIndex = e.NewPageIndex;
        loadBuses(2);//verified
    }
    protected void grdFaulty_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdFaulty.PageIndex = e.NewPageIndex;
        loadBuses(6);//faulty
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {

    }
    protected void grdPending_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "verify")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Session["_busno"] = grdPending.DataKeys[rowIndex]["busno"].ToString();
            string cashstatus = grdPending.DataKeys[rowIndex]["cashdeposityn"].ToString();
            if (cashstatus == "N")
            {
                Errormsg("Please Deposit Cash");
                return;
            }
            ddlStatus.SelectedValue = "0";
            mpVerifybus.Show();
        }
        if (e.CommandName == "view")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Session["_busno"] = grdPending.DataKeys[rowIndex]["busno"].ToString();
            searchBus();
        }
        
    }
    protected void grdVerified_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Session["_busno"] = grdVerified.DataKeys[rowIndex]["busno"].ToString();
            searchBus();
        }
    }
    protected void grdFaulty_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            Session["_busno"] = grdFaulty.DataKeys[rowIndex]["busno"].ToString();
            searchBus();
        }
    }
    protected void lbtnVerify_Click(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue == "0")
        {
            Errormsg("Select Bus Status");
            return;
        }
        verifyBus();
    }

    #endregion

    
}