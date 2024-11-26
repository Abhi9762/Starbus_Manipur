using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_depotComplaintDash : System.Web.UI.Page
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
            lblsummary.Text = "Summary As On " + DateTime.Now.ToString();
            Session["_moduleName"] = "Complaint Dashboard";
            getcount();
            Session["CType"] = "A";
            Session["CTypeName"] = "Fresh Complaints";
            loadComplaints(Session["CType"].ToString(), Session["_LDepotCode"].ToString());
        }
    }

    #region "Methods"
    private void Errormsg(string msg)
    {

        string popup = _popup.modalPopupSmall("W", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "OK");
        Response.Write(popup);
    }
    private void getcount()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "traveller_grievance.f_get_gri_count");
            MyCommand.Parameters.AddWithValue("p_depocode", Session["_LDepotCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lbtndisposed.Text = dt.Rows[0]["DISPOSE_GRI"].ToString();
                    lbtnPendinggv.Text = dt.Rows[0]["pending_gri"].ToString();
                    lbtnreject.Text = dt.Rows[0]["Reject_GRI"].ToString();
                    lbtntotalcomp.Text = dt.Rows[0]["TOTAL_GRI"].ToString();
                }
                else
                {
                    lbtndisposed.Text = "0";
                    lbtnPendinggv.Text = "0";
                    lbtnreject.Text = "0";
                    lbtntotalcomp.Text = "0";
                }
            }
            else
            {
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
        }
    }
    private void loadComplaints(string status, string depotcode)
    {
        lblgridheader.Text = Session["CTypeName"].ToString();
        try
        {
            pnlNodata.Visible = true;
            grdready.Visible = false;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "traveller_grievance.f_get_depot_complaints");
            MyCommand.Parameters.AddWithValue("gri_status", status);
            MyCommand.Parameters.AddWithValue("depot_code", depotcode);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    grdready.DataSource = dt;
                    grdready.DataBind();
                    pnlNodata.Visible = false;
                    grdready.Visible = true;
                }
                else
                {
                    pnlNodata.Visible = true;
                    grdready.Visible = false;
                }
            }
            else
            {
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
        }
    }
    #endregion

    #region "Events"
    protected void lbtnClosempGrievanceee_Click(object sender, EventArgs e)
    {
        Response.Redirect("depotComplaintDash.aspx");
    }
    protected void lbtntotalcomp_Click(object sender, EventArgs e)
    {
        Session["CType"] = "T";
        Session["CTypeName"] = "Total Complaints";
        loadComplaints(Session["CType"].ToString(), Session["_LDepotCode"].ToString());
    }
    protected void lbtnPendinggv_Click(object sender, EventArgs e)
    {
        Session["CType"] = "A";
        Session["CTypeName"] = "Fresh Complaints";
        loadComplaints(Session["CType"].ToString(), Session["_LDepotCode"].ToString());
    }
    protected void lbtnreject_Click(object sender, EventArgs e)
    {
        Session["CType"] = "X";
        Session["CTypeName"] = "Reject Complaints";
        loadComplaints(Session["CType"].ToString(), Session["_LDepotCode"].ToString());
    }
    protected void lbtndisposed_Click(object sender, EventArgs e)
    {
        Session["CType"] = "D";
        Session["CTypeName"] = "Disposed Complaints";
        loadComplaints(Session["CType"].ToString(), Session["_LDepotCode"].ToString());
    }
    protected void grdready_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdready.PageIndex = e.NewPageIndex;
        loadComplaints(Session["CType"].ToString(), Session["_LDepotCode"].ToString());
    }
    protected void grdready_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grdready_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ACTION")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

            string refNo = grdready.DataKeys[row.RowIndex]["refno"].ToString();
            Session["_grRefNo"] = refNo;
            Session["_LOGINUSER"] = "C";
            eDash.Text = "<embed src = \"dashGrievance.aspx\" style=\"height: 80vh; width: 100%\" />";
            mpGrievance.Show();
        }
    }
    #endregion

}