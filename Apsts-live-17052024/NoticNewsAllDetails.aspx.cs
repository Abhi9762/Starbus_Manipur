using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_NoticNewsAllDetails : System.Web.UI.Page
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
            Session["Heading"] = "Notice News Details";
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
    public void getNoticeNewsCount(string typeid)//M12
    {
        try
        {
            DataTable dt = new DataTable();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_homenotice_news");
            MyCommand.Parameters.AddWithValue("p_type", 0);
            MyCommand.Parameters.AddWithValue("p_notice", Convert.ToInt64(typeid));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    img.ImageUrl = GetImage((byte[])dt.Rows[0]["img1"]);
                  
                    lblSubject.Text = dt.Rows[0]["sub"].ToString();
                    lblDescription.Text = dt.Rows[0]["descr"].ToString();
                    lblValidFromDt.Text = dt.Rows[0]["validfrom"].ToString();
                    lblValidToDt.Text = dt.Rows[0]["validto"].ToString();

                    if (dt.Rows[0]["ctegorycode"].ToString() == "2")
                    {
                        Session["Heading"] = "Alerts";
                        lblTitle.Text = "Alert About " + dt.Rows[0]["sub"].ToString();
                    }

                    if (dt.Rows[0]["ctegorycode"].ToString() == "1")
                    {
                        Session["Heading"] = "Events";
                        lblTitle.Text = "This Event will held from " + dt.Rows[0]["validfrom"].ToString() + " To " + dt.Rows[0]["validto"].ToString();
                    }


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
                        lblURLLink.Text = dt.Rows[0]["url_"].ToString();
                    }

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