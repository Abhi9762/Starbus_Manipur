
using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;


public partial class Auth_sysAdmPgRefundTransaction : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
 string current_date = DateTime.Now.AddDays(-1).ToString("dd") + "/" + DateTime.Now.AddDays(-1).ToString("MM") + "/" + DateTime.Now.AddDays(-1).ToString("yyyy");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:ss tt");
            txtrefunddate.Text = current_date;
            Session["_moduleName"] = "Billdesk Dashboard";
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();

        }
        Test();
    }


    #region "Method"
    private void Test()
    {
        try
        {
            if (GvPgRefund.Rows.Count > 0)
            {
                GvPgRefund.UseAccessibleHeader = true;
                GvPgRefund.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PgRefundTransaction.aspx-001", ex.Message.ToString());

        }
    }
    public void ExportTxt(DataTable data, string fileName)
    {
        try
        {
            HttpContext context = HttpContext.Current;

            context.Response.Clear();
            context.Response.ContentType = "text/txt";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".txt");

            foreach (DataRow row in data.Rows)
            {
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    if (i > 0)
                    {
                        context.Response.Write(",");
                    }
                    context.Response.Write(row[i].ToString());
                }
                context.Response.Write(Environment.NewLine);
            }
            context.Response.End();
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PgRefundTransaction.aspx-002", ex.Message.ToString());
        }
    }



    private DataTable loaddata()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.get_refundtransaction");
            MyCommand.Parameters.AddWithValue("p_transdate", txtrefunddate.Text);
            MyCommand.Parameters.AddWithValue("p_pmtgateway", 3);
            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {

            }


            return dt;
        }

        catch (Exception ex)
        {
            _common.ErrorLog("PgRefundTransaction.aspx-003", ex.Message.ToString());
            return null;
        }
    }


    private DataTable loadtxt()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.get_refundtransaction_scrolldownload");
            MyCommand.Parameters.AddWithValue("p_transdate", txtrefunddate.Text);
            MyCommand.Parameters.AddWithValue("p_pmtgateway", 3);
            DataTable dt = new DataTable();
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {

            }


            return dt;
        }

        catch (Exception ex)
        {
            _common.ErrorLog("PgRefundTransaction.aspx-004", ex.Message.ToString());
            return null;
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    public void errorMassage(string msg)
    {
        lblerrmsg.Text = msg;
        mpError.Show();
    }

    #endregion


    #region "Event"
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }

        DataTable ddt = loaddata();
        if (ddt.Rows.Count > 0)
        {
            lblsmry.Text = "Total No. Of Transaction <b> " + ddt.Rows.Count.ToString();
            GvPgRefund.DataSource = ddt;
            GvPgRefund.DataBind();
            pnlMsg.Visible = false;
            pnlReport.Visible = true;
            Test();
        }

        else
        {
            pnlMsg.Visible = true;
            pnlReport.Visible = false;
            lblerrmsg.Text = "No Data Found For " + txtrefunddate.Text;
            mpError.Show();
        }

    }
    protected void lbtnpdf_Click(object sender, EventArgs e)
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
        }

        DataTable dt = loadtxt();
        if (dt.Rows.Count > 0)
        {
            ExportTxt(dt, "CancellationScroll_" + DateTime.Now.ToString("yyyy/MM/dd"));
        }

    }
    protected void GvPgRefund_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GvPgRefund.PageIndex = e.NewPageIndex;
        DataTable ddt = loaddata();
        if (ddt.Rows.Count > 0)
        {
            GvPgRefund.DataSource = ddt;
            GvPgRefund.DataBind();
        }
    }

    #endregion

    protected void lbtnreset_Click(object sender, EventArgs e)
    {
        txtrefunddate.Text = "";
        pnlMsg.Visible = true;
        pnlReport.Visible = false;

    }
}