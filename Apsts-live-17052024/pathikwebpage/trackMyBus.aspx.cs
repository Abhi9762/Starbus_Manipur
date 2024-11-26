using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Data;

public partial class pathikwebpage_trackMyBus : System.Web.UI.Page
{
    sbBLL bll = new sbBLL();
    sbValidation _SecurityCheck = new sbValidation();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    private sbCommonFunc _common = new sbCommonFunc();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string pn = Request.QueryString["PN"] == null/* TODO Change to default(_) if this is not a reference type */? "" : Request.QueryString["PN"];
          
            AesEncrptDecrpt aes = new AesEncrptDecrpt();
            string ticketno =  aes.DecryptStringAES(pn);
            loadticketdetail(ticketno);
        }
    }
    private void loadticketdetail(string ticketno)
    {
        NpgsqlCommand MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.get_active_tickets_for_track");
        MyCommand.Parameters.AddWithValue("p_userid", "0");
        MyCommand.Parameters.AddWithValue("p_ticket_no", ticketno);
        DataTable dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["bus_no"].ToString() != "NA" && dt.Rows[0]["gps_yn"].ToString() == "Y")
                {
                    Session["busno"] = dt.Rows[0]["bus_no"].ToString();
                    openiFrame();
                    lblgpsyn.Visible = false;
                    div1.Visible = true;
                }
                else
                {
                    lblgpsyn.Text = "Sorry, Bus Details Not Available";
                    lblgpsyn.Visible = true;
                    div1.Visible = false;
                }

            }
            else
            {
                lblgpsyn.Text = "Journey Yet Not Startted, You Can Track Your Bus During Journey.";
                lblgpsyn.Visible = true;
                div1.Visible = false;
            }
        }
    }
    private void openiFrame()
    {
        Literal l1 = new Literal();
        string busno = Session["busno"].ToString();
        string accesstoken = "7a9bef82c27db5be6ea4e9beff576639";
        string url = "https://t-location.intangles.com/" + busno + "?vendor-access-token=" + accesstoken + "";
        l1.Text = "<iframe name='we' id='12'  frameborder='no' scrolling='auto' height='500px' width='100%'  src='" + url + "' style='left:0; background-color:#B8B8B8;'></iframe>";
        div1.Controls.Add(l1);
    }
}