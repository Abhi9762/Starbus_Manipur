using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_SysAdmFeedbackRating1 : System.Web.UI.Page
{
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    ReportDocument cryrpt = new ReportDocument();
    private sbCommonFunc _common = new sbCommonFunc();
    string rptcerificate = System.Web.HttpContext.Current.Server.MapPath("FeedbackRpt/BestConductorCertificate.rpt");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            int currentYear = DateTime.Now.Year;
            int startYear = currentYear - 10;
            PopulateYears(ddlyears, startYear, currentYear);
            populateMonths(currentYear);

            int selectedYear = int.Parse(ddlyears.SelectedValue);
            int currentMonth = DateTime.Now.Month;
            getratinggrid();
            getconductorrating();
            getbusservicerrating();
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
    public void populateMonths(int selectedYear)
    {
        ddlmonth.Items.Clear();
        int currentYear = DateTime.Now.Year;
        int currentMonth = DateTime.Now.Month;

        for (int i = 1; i <= 12; i++)
        {
            if (selectedYear == currentYear && i > currentMonth)
            {

                continue;
            }

            DateTime month = new DateTime(selectedYear, i, 1);
            string monthName = month.ToString("MMMM");
            ddlmonth.Items.Add(new ListItem(monthName, i.ToString()));
        }

        // Set the selected month based on the current month
        ddlmonth.SelectedValue = currentMonth.ToString();
    }
    public static void PopulateYears(DropDownList ddlYears, int startYear, int endYear)
    {
        ddlYears.Items.Clear();

        for (int year = startYear; year <= endYear; year++)
        {
            ListItem listItem = new ListItem(year.ToString(), year.ToString());
            ddlYears.Items.Add(listItem);

            if (year == DateTime.Now.Year)
            {
                listItem.Selected = true;
            }
        }
    }
    public void getratinggrid()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.conductor_rating_info");
            MyCommand.Parameters.AddWithValue("@p_month", Convert.ToInt16(ddlmonth.SelectedValue));
            MyCommand.Parameters.AddWithValue("@p_year", Convert.ToInt16(ddlyears.SelectedValue));
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                gvRating.DataSource = dt;
                gvRating.DataBind();
                pnlMsg.Visible = false;
                pnlReport.Visible = true;
            }
            else
            {

                pnlMsg.Visible = true;
                pnlReport.Visible = false;
                gvRating.DataSource = dt;
                gvRating.DataBind();
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmFeedbackRating1.aspx-0001", ex.Message.ToString());
        }

    }
    public void getconductorrating()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.conductor_rating");
            MyCommand.Parameters.AddWithValue("@p_month", Convert.ToInt16(ddlmonth.SelectedValue));
            MyCommand.Parameters.AddWithValue("@p_year", Convert.ToInt16(ddlyears.SelectedValue));
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    lblconductormonth.Text = dt.Rows[0]["monthyear"].ToString();
                    LabelConductorName.Text = dt.Rows[0]["emp_name"].ToString();
                    LabelConductorDepot.Text = dt.Rows[0]["officename"].ToString();
                    LabelConductorMobile.Text = dt.Rows[0]["mobilenumber"].ToString();
                    lbtnconductor.Visible = true;
                    lblconductorofmonth.Visible = false;
                    LBInferiorConductorDetail.Visible = false;
                    lblconductorlowestrating.Visible = true;
                }
                else
                {
                    lblconductormonth.Text = dt.Rows[1]["monthyear"].ToString();
                    LabelConductorName.Text = dt.Rows[1]["emp_name"].ToString();
                    LabelConductorDepot.Text = dt.Rows[1]["officename"].ToString();
                    LabelConductorMobile.Text = dt.Rows[1]["mobilenumber"].ToString();
                    lblconductorlowestratingmonth.Text = dt.Rows[0]["monthyear"].ToString();
                    lblconductorlowestratingname.Text = dt.Rows[0]["emp_name"].ToString();
                    depotname.Text = dt.Rows[0]["officename"].ToString();
                    lblmobile.Text = dt.Rows[0]["mobilenumber"].ToString();
                    lbtnconductor.Visible = true;
                    lblconductorofmonth.Visible = false;
                    LBInferiorConductorDetail.Visible = true;
                    lblconductorlowestrating.Visible = false;
                }

            }
            else
            {
                lbtnconductor.Visible = false;
                lblconductorofmonth.Visible = true;
                LBInferiorConductorDetail.Visible = false;
                lblconductorlowestrating.Visible = true;

            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmFeedbackRating1.aspx-0002", ex.Message.ToString());
        }
    }
    public void getbusservicerrating()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.busservice_rating");
            MyCommand.Parameters.AddWithValue("@p_month", Convert.ToInt16(ddlmonth.SelectedValue));
            MyCommand.Parameters.AddWithValue("@p_year", Convert.ToInt16(ddlyears.SelectedValue));
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                lbltopbusmonth.Text = dt.Rows[0]["monthyear"].ToString();
                lbltopbusservicename.Text = dt.Rows[0]["servicenameen"].ToString();
                lblbustopdepot.Text = dt.Rows[0]["officename"].ToString();
                lblbuslowestratingmonth.Text = dt.Rows[1]["monthyear"].ToString();
                lblbusservicelowestratingname.Text = dt.Rows[1]["servicenameen"].ToString();
                lblbuslowestdepo.Text = dt.Rows[1]["officename"].ToString();

                LinkButton2.Visible = true;
                lblbusservice.Visible = false;
                LinkButton3.Visible = true;
                lblbuslowestrating.Visible = false;
            }
            else
            {
                LinkButton2.Visible = false;
                lblbusservice.Visible = true;
                LinkButton3.Visible = false;
                lblbuslowestrating.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmFeedbackRating1.aspx-0003", ex.Message.ToString());
        }
    }
    #endregion

    #region "Event"
    protected void lbtnconductor_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        DataTable dt = new DataTable();
        cryrpt.Load(rptcerificate);
        cryrpt.SetDataSource(dt);
        CrystalDecisions.CrystalReports.Engine.TextObject objtxtrpthd = cryrpt.ReportDefinition.Sections[1].ReportObjects["txtrpthd"] as CrystalDecisions.CrystalReports.Engine.TextObject;
        objtxtrpthd.Text = "Conductor Of Month (" + lblconductormonth.Text.ToString().Trim() + ")";
        CrystalDecisions.CrystalReports.Engine.TextObject objtxtrptname = cryrpt.ReportDefinition.Sections[1].ReportObjects["txtrptname"] as CrystalDecisions.CrystalReports.Engine.TextObject;
        objtxtrptname.Text = LabelConductorName.Text.ToString().Trim();
        CrystalDecisions.CrystalReports.Engine.TextObject objtxtrptdepot = cryrpt.ReportDefinition.Sections[1].ReportObjects["txtrptdepot"] as CrystalDecisions.CrystalReports.Engine.TextObject;
        objtxtrptdepot.Text = LabelConductorDepot.Text.ToString().Trim() + ", STARBUS";
        //CrystalDecisions.CrystalReports.Engine.TextObject objtxtmonth = cryrpt.ReportDefinition.Sections[1].ReportObjects["txtmonth"] as CrystalDecisions.CrystalReports.Engine.TextObject;
        //objtxtmonth.Text = ddlallmonth.SelectedItem.ToString();
        cryrpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Context.Response, true, "ConductorCertificate" + DateTime.Now);
    }
    protected void LBInferiorConductorDetail_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        DataTable dt = new DataTable();
        cryrpt.Load(rptcerificate);
        cryrpt.SetDataSource(dt);
        CrystalDecisions.CrystalReports.Engine.TextObject objtxtrpthd = cryrpt.ReportDefinition.Sections[1].ReportObjects["txtrpthd"] as CrystalDecisions.CrystalReports.Engine.TextObject;
        objtxtrpthd.Text = "Conductor With Lowest Rating (" + lblconductorlowestratingmonth.Text.ToString().Trim() + ")";
        CrystalDecisions.CrystalReports.Engine.TextObject objtxtrptname = cryrpt.ReportDefinition.Sections[1].ReportObjects["txtrptname"] as CrystalDecisions.CrystalReports.Engine.TextObject;
        objtxtrptname.Text = lblconductorlowestratingname.Text.ToString().Trim();
        CrystalDecisions.CrystalReports.Engine.TextObject objtxtrptdepot = cryrpt.ReportDefinition.Sections[1].ReportObjects["txtrptdepot"] as CrystalDecisions.CrystalReports.Engine.TextObject;
        objtxtrptdepot.Text = depotname.Text.ToString().Trim() + ", STARBUS";
        //CrystalDecisions.CrystalReports.Engine.TextObject objtxtmonth = cryrpt.ReportDefinition.Sections[1].ReportObjects["txtmonth"] as CrystalDecisions.CrystalReports.Engine.TextObject;
        //objtxtmonth.Text = ddlallmonth.SelectedItem.ToString();
        cryrpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Context.Response, true, "ConductorCertificate" + DateTime.Now);
    }
    protected void ddlyears_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        int selectedYear = int.Parse(ddlyears.SelectedValue);
        populateMonths(selectedYear);
        lblconductormonth.Text = "Not Available";
        lblconductorlowestratingmonth.Text = "Not Available";
        LabelConductorDepot.Text = "Not Available";
        LabelConductorMobile.Text = "Not Available";
        lblconductorlowestratingname.Text = "Not Available";
        LabelConductorName.Text = "Not Available";
        depotname.Text = "Not Available";
        lblmobile.Text = "Not Available";


        lbltopbusmonth.Text = "Not Available";
        lbltopbusservicename.Text = "Not Available";
        lblbustopdepot.Text = "Not Available";
        lblbuslowestratingmonth.Text = "Not Available";
        lblbusservicelowestratingname.Text = "Not Available";
        lblbuslowestdepo.Text = "Not Available";

        getconductorrating();
        getbusservicerrating();
        getratinggrid();
    }
    protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        lblconductormonth.Text = "Not Available";
        lblconductorlowestratingmonth.Text = "Not Available";
        LabelConductorName.Text = "Not Available";
        LabelConductorDepot.Text = "Not Available";
        LabelConductorMobile.Text = "Not Available";
        lblconductorlowestratingname.Text = "Not Available";
        depotname.Text = "Not Available";
        lblmobile.Text = "Not Available";
        lblbusmobile.Text = "Not Available";
        lblbusmob.Text = "Not Available";
        lbltopbusmonth.Text = "Not Available";
        lbltopbusservicename.Text = "Not Available";
        lblbustopdepot.Text = "Not Available";
        lblbuslowestratingmonth.Text = "Not Available";
        lblbusservicelowestratingname.Text = "Not Available";
        lblbuslowestdepo.Text = "Not Available";

        getratinggrid();
        getconductorrating();
        getbusservicerrating();
    }
    protected void gvRating_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "View")
        {
            Session["journey_date"] = gvRating.DataKeys[index].Values["journey_date"];
            Response.Redirect("RatingDetails.aspx");
            return;
        }
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        DataTable dt = new DataTable();
        cryrpt.Load(rptcerificate);
        cryrpt.SetDataSource(dt);
        CrystalDecisions.CrystalReports.Engine.TextObject objtxtrpthd = cryrpt.ReportDefinition.Sections[1].ReportObjects["txtrpthd"] as CrystalDecisions.CrystalReports.Engine.TextObject;
        objtxtrpthd.Text = "Bus Service With Lowest Rating (" + lblbuslowestratingmonth.Text.ToString().Trim() + ")";
        CrystalDecisions.CrystalReports.Engine.TextObject objtxtrptname = cryrpt.ReportDefinition.Sections[1].ReportObjects["txtrptname"] as CrystalDecisions.CrystalReports.Engine.TextObject;
        objtxtrptname.Text = lblbusservicelowestratingname.Text.ToString().Trim();
        CrystalDecisions.CrystalReports.Engine.TextObject objtxtrptdepot = cryrpt.ReportDefinition.Sections[1].ReportObjects["txtrptdepot"] as CrystalDecisions.CrystalReports.Engine.TextObject;
        objtxtrptdepot.Text = lblbuslowestdepo.Text.ToString().Trim() + ", STARBUS";
        //CrystalDecisions.CrystalReports.Engine.TextObject objtxtmonth = cryrpt.ReportDefinition.Sections[1].ReportObjects["txtmonth"] as CrystalDecisions.CrystalReports.Engine.TextObject;
        //objtxtmonth.Text = ddlallmonth.SelectedItem.ToString();
        cryrpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Context.Response, true, "BusServiceCertificate" + DateTime.Now);
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        DataTable dt = new DataTable();
        cryrpt.Load(rptcerificate);
        cryrpt.SetDataSource(dt);
        CrystalDecisions.CrystalReports.Engine.TextObject objtxtrpthd = cryrpt.ReportDefinition.Sections[1].ReportObjects["txtrpthd"] as CrystalDecisions.CrystalReports.Engine.TextObject;
        objtxtrpthd.Text = "Bus Service Of The Month (" + lbltopbusmonth.Text.ToString().Trim() + ")";
        CrystalDecisions.CrystalReports.Engine.TextObject objtxtrptname = cryrpt.ReportDefinition.Sections[1].ReportObjects["txtrptname"] as CrystalDecisions.CrystalReports.Engine.TextObject;
        objtxtrptname.Text = lbltopbusservicename.Text.ToString().Trim();
        CrystalDecisions.CrystalReports.Engine.TextObject objtxtrptdepot = cryrpt.ReportDefinition.Sections[1].ReportObjects["txtrptdepot"] as CrystalDecisions.CrystalReports.Engine.TextObject;
        objtxtrptdepot.Text = lblbustopdepot.Text.ToString().Trim() + ", STARBUS";
        //CrystalDecisions.CrystalReports.Engine.TextObject objtxtmonth = cryrpt.ReportDefinition.Sections[1].ReportObjects["txtmonth"] as CrystalDecisions.CrystalReports.Engine.TextObject;
        //objtxtmonth.Text = ddlallmonth.SelectedItem.ToString();
        cryrpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Context.Response, true, "BusServiceCertificate" + DateTime.Now);
    }
    #endregion
}