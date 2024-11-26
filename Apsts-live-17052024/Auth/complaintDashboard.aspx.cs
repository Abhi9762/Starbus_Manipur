using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_complaintDashboard : System.Web.UI.Page
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
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            Session["_moduleName"] = "Complaint Dashboard";
            loadPendingGri();
            getcount();
            Session["CType"] = "E";
            Session["CTypeName"] = "Fresh Complaints <span style='font-size: 10pt;'>(0 to 24 hrs)</span>";
            Session["PendingCType"] = "0";
            loadComplaints(Session["CType"].ToString());
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
    private void loadPendingGri()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "traveller_grievance.f_get_pending_gri_count");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow[] dr0to24 = dt.Select("mins<1440");
                    DataRow[] dr24to7days = dt.Select("mins>1440 and mins<10080");
                    DataRow[] dr7to15days = dt.Select("mins>10080 and mins <21600");
                    DataRow[] dr15to30days = dt.Select("mins>21600 and mins<43200");
                    DataRow[] drmore30days = dt.Select("mins>43200");
                    lbtn0to24hrs.Text = dr0to24.Length.ToString();
                    lbtn24to7days.Text = dr24to7days.Length.ToString();
                    lbtn7to15days.Text = dr7to15days.Length.ToString();
                    lbtn15to30days.Text = dr15to30days.Length.ToString();
                    lbtnmore30days.Text = drmore30days.Length.ToString();
                    lbtntotalpending.Text = Convert.ToString(dr0to24.Length + dr24to7days.Length + dr7to15days.Length + dr15to30days.Length + drmore30days.Length);
                }
                else
                {
                    lbtn0to24hrs.Text = "0";
                    lbtn24to7days.Text = "0";
                    lbtn7to15days.Text = "0";
                    lbtn15to30days.Text = "0";
                    lbtnmore30days.Text = "0";
                    lbtntotalpending.Text = "0";
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
            _common.ErrorLog("complaintDashboard.aspx-0001", ex.Message.ToString());
        }
    }
    private void getcount()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "traveller_grievance.f_get_gri_count");
            MyCommand.Parameters.AddWithValue("p_depocode", "0");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lbtndisposed.Text = dt.Rows[0]["DISPOSE_GRI"].ToString();
                    lbtnassigned.Text = dt.Rows[0]["Assign_GRI"].ToString();
                    lbtnreject.Text = dt.Rows[0]["Reject_GRI"].ToString();
                    lbtntotalcomp.Text = dt.Rows[0]["TOTAL_GRI"].ToString();
                }
                else
                {
                    lbtndisposed.Text = "0";
                    lbtnassigned.Text = "0";
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
            _common.ErrorLog("complaintDashboard.aspx-0002", ex.Message.ToString());
        }
    }
    private void loadComplaints(string status)
    {
        lblgridheader.Text = Session["CTypeName"].ToString();
        try
        {
            pnlNodata.Visible = true;
            grdready.Visible = false;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "traveller_grievance.f_get_complaints");
            MyCommand.Parameters.AddWithValue("gri_status", status);
            MyCommand.Parameters.AddWithValue("pendingct_type", Session["PendingCType"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (Session["PendingCType"].ToString() == "0")
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
                if (Session["PendingCType"].ToString() == "1")
                {
                    DataRow[] dr = dt.Select("mins<1440");
                    if (dr.Length > 0)
                    {
                        grdready.DataSource = dr.CopyToDataTable();
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
                if (Session["PendingCType"].ToString() == "2")
                {
                    DataRow[] dr = dt.Select("mins>1440 and mins <10080");
                    if (dr.Length > 0)
                    {
                        grdready.DataSource = dr.CopyToDataTable();
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
                if (Session["PendingCType"].ToString() == "3")
                {
                    DataRow[] dr = dt.Select("mins>10080 and mins <21600");
                    if (dr.Length > 0)
                    {
                        grdready.DataSource = dr.CopyToDataTable();
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
                if (Session["PendingCType"].ToString() == "4")
                {
                    DataRow[] dr = dt.Select("mins>21600 and mins <43200");
                    if (dr.Length > 0)
                    {
                        grdready.DataSource = dr.CopyToDataTable();
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
                if (Session["PendingCType"].ToString() == "5")
                {
                    DataRow[] dr = dt.Select("mins>43200 ");
                    if (dr.Length > 0)
                    {
                        grdready.DataSource = dr.CopyToDataTable();
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
                if (Session["PendingCType"].ToString() == "6")
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
            }
            else
            {
                Errormsg(dt.TableName);
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("complaintDashboard.aspx-0003", ex.Message.ToString());
        }
    }
    #endregion

    #region "Events"
    protected void grdready_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdready.PageIndex = e.NewPageIndex;
        loadComplaints(Session["CType"].ToString());
    }
    protected void grdready_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grdready_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "ACTION")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

            string refNo = grdready.DataKeys[row.RowIndex]["gv_refno"].ToString();
            Session["_grRefNo"] = refNo;
            Session["_LOGINUSER"] = "C";
            eDash.Text = "<embed src = \"dashGrievance.aspx\" style=\"height: 80vh; width: 100%\" />";
            mpGrievance.Show();

        }
    }
    protected void lbtn0to24hrs_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["CType"] = "E";
        Session["CTypeName"] = "Pending Complaints <span style='font-size: 10pt;'>(0 to 24 hrs)</span>";
        Session["PendingCType"] = "1";
        loadComplaints(Session["CType"].ToString());
    }
    protected void lbtn24to7days_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["CType"] = "E";
        Session["CTypeName"] = "Pending Complaints <span style='font-size: 10pt;'> (24hrs to 7days)</span>";
        Session["PendingCType"] = "2";
        loadComplaints(Session["CType"].ToString());
    }
    protected void lbtn7to15days_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["CType"] = "E";
        Session["CTypeName"] = "Pending Complaints <span style='font-size: 10pt;'> (7 days to 15 days)</span>";
        Session["PendingCType"] = "3";
        loadComplaints(Session["CType"].ToString());
    }
    protected void lbtn15to30days_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["CType"] = "E";
        Session["CTypeName"] = "Pending Complaints <span style='font-size: 10pt;'> (15 days to 30 days)</span>";
        Session["PendingCType"] = "4";
        loadComplaints(Session["CType"].ToString());
    }
    protected void lbtnmore30days_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["CType"] = "E";
        Session["CTypeName"] = "Pending Complaints <span style='font-size: 10pt;'> (more than 30 days)</span>";
        Session["PendingCType"] = "5";
        loadComplaints(Session["CType"].ToString());
    }
    protected void lbtntotalpending_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["CType"] = "E";
        Session["CTypeName"] = "Total Pending Complaints";
        Session["PendingCType"] = "6";
        loadComplaints(Session["CType"].ToString());
    }
    protected void lbtntotalcomp_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["CType"] = "T";
        Session["CTypeName"] = "Total Complaints";
        Session["PendingCType"] = "0";
        loadComplaints(Session["CType"].ToString());
    }
    protected void lbtndisposed_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["CType"] = "D";
        Session["CTypeName"] = "Disposed Complaints";
        Session["PendingCType"] = "0";
        loadComplaints(Session["CType"].ToString());
    }
    protected void lbtnreject_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["CType"] = "X";
        Session["CTypeName"] = "Rejected Complaints";
        Session["PendingCType"] = "0";
        loadComplaints(Session["CType"].ToString());
    }
    protected void lbtnassigned_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["CType"] = "A";
        Session["CTypeName"] = "Assigned Complaints";
        Session["PendingCType"] = "0";
        loadComplaints(Session["CType"].ToString());
    }
    protected void lbtnClosempGrievanceee_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Response.Redirect("complaintDashboard.aspx");
    }
    #endregion
}