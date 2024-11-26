using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Tenders : System.Web.UI.Page
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
            Session["Heading"] = "Tenders";
            string typeid = Request.QueryString["typeid"];
            getNoticeNewsCount(6);
        }
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
   
    public void getNoticeNewsCount(int typeid)//M12
    {
        try
        {
            rpttender.Visible = false;
            divnodata.Visible = true;
            //  tenderdata.Visible = false;
            DataTable dt = new DataTable();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_homenotice_news");
            MyCommand.Parameters.AddWithValue("p_type", Convert.ToInt64(6));
            MyCommand.Parameters.AddWithValue("p_notice", Convert.ToInt64(0));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    rpttender.DataSource = dt;
                    rpttender.DataBind();
                    rpttender.Visible = true;
                    divnodata.Visible = false;
                    //img.ImageUrl = GetImage((byte[])dt.Rows[0]["img1"]);

                    //lblSubject.Text = dt.Rows[0]["sub"].ToString();
                    //lblDescription.Text = dt.Rows[0]["descr"].ToString();
                    //lblValidFromDt.Text = dt.Rows[0]["validfrom"].ToString();
                    //lblValidToDt.Text = dt.Rows[0]["validto"].ToString();

                    //if (dt.Rows[0]["ctegorycode"].ToString() == "2")
                    //{
                    //    Session["Heading"] = "Alerts";
                    //    lblTitle.Text = "Alert About " + dt.Rows[0]["sub"].ToString();
                    //}

                    //if (dt.Rows[0]["ctegorycode"].ToString() == "1")
                    //{
                    //    Session["Heading"] = "Events";
                    //    lblTitle.Text = "This Event will held from " + dt.Rows[0]["validfrom"].ToString() + " To " + dt.Rows[0]["validto"].ToString();
                    //}


                    //if (dt.Rows[0]["document_"].ToString() == "")
                    //{
                    //    lblDocument.Visible = false;
                    //    lbtnDocument.Visible = false;
                    //}
                    //else
                    //{
                    //    lblDocument.Visible = true;
                    //    lbtnDocument.Visible = true;
                    //    lblDocument.Text = "For more details you may download this";
                    //    Session["docc"] = dt.Rows[0]["document_"];

                    //}

                    //if (dt.Rows[0]["url_"].ToString() == "")
                    //{
                    //    lblURL.Visible = false;
                    //    lblURLLink.Visible = false;
                    //}
                    //else
                    //{
                    //    lblURL.Visible = true;
                    //    lblURLLink.Visible = true;
                    //    lblURL.Text = "For more details you may also visit website-> ";
                    //    lblURLLink.Text = dt.Rows[0]["url_"].ToString();
                    //}
                    //divnodata.Visible = false;
                    //tenderdata.Visible = true;
                }
                else
                {
                    divnodata.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("NoticeNews-M", ex.Message.ToString());
            Errormsg(ex.Message);

        }
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Information", msg, "Close");
        Response.Write(popup);
    }

    protected void rpttender_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        foreach (RepeaterItem item in rpttender.Items)
        {
            //RepeaterItem item = e.Item;
            Label categoryid = (item.FindControl("lblCategory") as Label);
            Label noticeid = (item.FindControl("lblNotice") as Label);
            Label lbldocument = (item.FindControl("lblDocument") as Label);
            Label lblURL = (item.FindControl("lblURL") as Label);
            Label lblURLLink = (item.FindControl("lblURLLink") as Label);
            LinkButton lbtndocument = (item.FindControl("lbtnDocument") as LinkButton);
            ImageButton img= (item.FindControl("img") as ImageButton);

            DataTable dt = new DataTable();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_homenotice_news");
            MyCommand.Parameters.AddWithValue("p_type", Convert.ToInt64(categoryid.Text));
            MyCommand.Parameters.AddWithValue("p_notice", Convert.ToInt64(noticeid.Text));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    img.ImageUrl = GetImage((byte[])dt.Rows[0]["img1"]);
                    if (dt.Rows[0]["document_"].ToString() == "")
                    {
                        lbldocument.Visible = false;
                        lbtndocument.Visible = false;
                    }
                    else
                    {
                        lbldocument.Visible = true;
                        lbtndocument.Visible = true;
                        lbldocument.Text = "For more details you may download this";
                        //  Session["docc"] = dt.Rows[0]["document_"];
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
    }

    protected void rpttender_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Document")
        {
            Label lblCategory = (Label)e.Item.FindControl("lblCategory");
            Label lblNotice = (Label)e.Item.FindControl("lblNotice");
            DataTable dt = new DataTable();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_homenotice_news");
            MyCommand.Parameters.AddWithValue("p_type", Convert.ToInt64(lblCategory.Text));
            MyCommand.Parameters.AddWithValue("p_notice", Convert.ToInt64(lblNotice.Text));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    Session["docc"] = dt.Rows[0]["document_"];
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
        }
    }
}