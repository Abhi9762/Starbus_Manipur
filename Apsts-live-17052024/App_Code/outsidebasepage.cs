using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for outsidebasepage
/// </summary>
public class outsidebasepage : System.Web.UI.Page
{
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private NpgsqlCommand MyCommand = new NpgsqlCommand();
    DataTable MyTable = new DataTable();
    sbBLL bll = new sbBLL();
    sbCommonFunc clscommon = new sbCommonFunc();
    protected void Page_Init(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(20));
        Response.Cache.SetNoStore();
        Response.AddHeader("Cache-control", "no-store, must-revalidate,private,no-cache");
        Response.AddHeader("PRAGMA", "NO-Cache");
        Response.Expires = -1;
        Response.Cache.SetValidUntilExpires(false);
        CheckSecurity();
    }

    private void CheckSecurity()
    {
        try
        {
            string currentPage = GetCurrentPageName();
            getModuleDetails(currentPage);
        }
        catch (Exception ex)
        {
            Response.Redirect("Errorpage.aspx");
        }
    }

   
    private string GetCurrentPageName()
    {
        string[] segments = Request.Url.Segments;
        string currentPage = segments[segments.Length - 1];
        int queryIndex = currentPage.IndexOf('?');
        if (queryIndex != -1)
        {
            currentPage = currentPage.Substring(0, queryIndex);
        }
        return currentPage;
    }
    protected bool getModuleDetails(string currentPage)
    {
        try
        {
            using (NpgsqlCommand MyCommand = new NpgsqlCommand())
            {
                MyCommand.Parameters.Clear();
                MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.fn_get_module_details");
                MyCommand.Parameters.AddWithValue("@p_page_url", currentPage);
                MyTable = bll.SelectAll(MyCommand);
                if (MyTable.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    Response.Redirect("Errorpage.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Errorpage.aspx");
        }
        return false;
    }
}