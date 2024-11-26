using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NoticeNewsDetails : System.Web.UI.Page
{
    private sbCommonFunc _common = new sbCommonFunc();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();

    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    private sbBLL bll = new sbBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Heading"] = "Notice News";
            string typeid = Request.QueryString["typeid"];
            getNoticeNewsCount(typeid);
        }

    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Information", msg, "Close");
        Response.Write(popup);
    }
    public string GetImage(object img)
    {
        try
        {
            return "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public void getNoticeNewsCount(string typeid)//
    {
        try
        {
            DataTable dt = new DataTable();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_homenotice_news");
            MyCommand.Parameters.AddWithValue("p_type", 3);
            MyCommand.Parameters.AddWithValue("p_notice", Convert.ToInt64(typeid));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblSubject.Text = dt.Rows[0]["sub"].ToString();
                    lblDescription.Text = dt.Rows[0]["descr"].ToString();
                    if (dt.Rows[0]["document_"].ToString() == "")
                    {
                        lblDocument.Visible = false;
                        lbtnDocument.Visible = false;
                    }
                    else
                    {
                        lblDocument.Visible = true;
                        lbtnDocument.Visible = true;
                        lblDocument.Text = "For more details you may download this";
                        Session["docc"] = dt.Rows[0]["document_"];
                       
                    }

                    if (dt.Rows[0]["url_"].ToString() == "")
                    {
                        lblURL.Visible = false;
                        lblURLLink.Visible = false;
                    }
                    else
                    {
                        lblURL.Visible = true;
                        lblURLLink.Visible = true;
                        lblURL.Text = "For more details you may also visit website-> ";
                        lblURLLink.Text =  dt.Rows[0]["url_"].ToString();
                    }



                  
                    Session["noticeid"] = dt.Rows[0]["noticeid"].ToString();

                }

            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("NoticeNews-M", ex.Message.ToString());
            Errormsg(ex.Message);

        }
    }

    protected void lbtnDocument_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=" + "File.pdf"); // to open file prompt Box open or Save file  
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.BinaryWrite((byte[])Session["docc"]);
        Response.End();
    }
}