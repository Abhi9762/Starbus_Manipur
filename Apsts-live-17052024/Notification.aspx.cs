using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Notification : System.Web.UI.Page
{
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["heading"] = "Notifications";
        loadLatestNotice();
        loadArchiveNotice();
    }

    private void loadArchiveNotice()
    {
        divArchive.Visible = true;
        grdArchive.Visible = false;
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_notice_news_details");
        MyCommand.Parameters.AddWithValue("p_type", "A");
        dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                grdArchive.DataSource = dt;
                grdArchive.DataBind();
                divArchive.Visible = false;
                grdArchive.Visible = true;
            }
            else
            {
                divArchive.Visible = true;
                grdArchive.Visible = false;
            }
        }
    }

    private void loadLatestNotice()
    {
        divLatest.Visible = true;
        grdLatest.Visible = false;
           MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_notice_news_details");
        MyCommand.Parameters.AddWithValue("p_type", "L");
        dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                grdLatest.DataSource = dt;
                grdLatest.DataBind();
                divLatest.Visible = false;
                grdLatest.Visible = true;
            }
            else
            {
                divLatest.Visible = true;
                grdLatest.Visible = false;
            }
        }
    }

    protected void grdLatest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdLatest.PageIndex = e.NewPageIndex;
        loadLatestNotice();
    }

    protected void grdArchive_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdArchive.PageIndex = e.NewPageIndex;
        loadArchiveNotice();
    }
}