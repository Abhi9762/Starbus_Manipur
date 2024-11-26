using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pathikwebpage_grievancedetail : System.Web.UI.Page
{
    sbValidation _SecurityCheck = new sbValidation();
    NpgsqlCommand MyCommand;
    sbBLL bll = new sbBLL();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    classAlarm obj = new classAlarm();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AesEncrptDecrpt aes = new AesEncrptDecrpt();
            string encrypt_refno = Request.QueryString["refno"] == null/* TODO Change to default(_) if this is not a reference type */? "" : Request.QueryString["refno"];
            string refno = aes.DecryptStringAES(encrypt_refno);
            if (refno == "")
            { pnldetails.Visible = false; }
            else
            {
                loadGrievanceDetail(refno);
                GetGRefTrackDetails(refno);
                
            }
        }
    }
    private void loadGrievanceDetail(string refNo)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = obj.getGrievanceDetail(refNo);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    string refno = dt.Rows[0]["g_refno"].ToString();
                    string catname = dt.Rows[0]["category_name"].ToString();
                    string subcatname = dt.Rows[0]["sub_categoryname"].ToString();
                    string remark = dt.Rows[0]["g_remark"].ToString();
                    string datetime = dt.Rows[0]["g_datetime"].ToString();
                    string bus_no = dt.Rows[0]["bus_no"].ToString();
                    string ticketno = dt.Rows[0]["ticket_no"].ToString();
                    // Dim  As Byte() pic1 = CType(dt.Rows(0)("gpic1"), Byte())
                    // Dim  As Byte() pic2 = CType(dt.Rows(0)("gpic2"), Byte())
                    byte[] pic1 = (byte[])dt.Rows[0]["gpic_1"];
                    byte[] pic2 = (byte[])dt.Rows[0]["gpic_2"];
                    lblrefno.Text = refno;
                    lblgrvtype.Text = catname + " - " + subcatname;
                    lblremark.Text = remark;
                    if (bus_no == "")
                    { lblbusno.Text = "N/A"; }
                    else
                    { lblbusno.Text = bus_no; }
                    lbldate.Text = datetime;
                    if (pic1.Length == 0)
                    {
                        img1.Visible = false;
                    }
                    else
                    {
                        img1.ImageUrl = GetImage(pic1);
                    }
                    if (pic2.Length == 0)
                    {
                        img2.Visible = false;
                    }
                    else
                    {
                        img2.ImageUrl = GetImage(pic2);
                    }
                    if (pic2.Length == 0 && pic1.Length == 0)
                    {
                        img1.Visible = false;
                        img2.Visible = false;
                        lblnoimages.Visible = true;
                    }
                    pnldetails.Visible = true;
                }
                else
                {
                    pnldetails.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void GetGRefTrackDetails(string refNo)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = obj.GetGRefTrackDetails(refNo);
            if (dt.TableName == "Success")
            {
                grTransactions.DataSource = dt;
                grTransactions.DataBind();
            }
        }
        catch (Exception ex)
        {

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
}