using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class E_tripchart_current : System.Web.UI.Page
{
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    Int32 totalSeats;
    decimal totalFare;
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (Session["p_tripcode"].ToString() != null && Session["p_tripcode"].ToString() != "")
        {
            loadxml();
            loaddata();
        }
        else
        {
            panelError.Visible = true;
            pnldetails.Visible = false;
        }
    }

    private void loaddata()
    {
        try
        {
            DataTable dt;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.f_get_current_trip_dtls");
            MyCommand.Parameters.AddWithValue("sp_tripcode", Session["p_tripcode"].ToString());
            MyCommand.Parameters.AddWithValue("sp_updatedby", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lblbkngstn.Text = "Booking Station " + dt.Rows[0]["bookingstation"].ToString();
                        lblservice.Text = dt.Rows[0]["servicetype_name"].ToString();
                        lblbusno.Text = "Bus No " + dt.Rows[0]["busno"].ToString() + "<br/> (" + dt.Rows[0]["officename"].ToString() + ")";
                        lblRoute.Text = "Route " + dt.Rows[0]["routename"].ToString();
                        lblConductor.Text = "Conductor " + dt.Rows[0]["conduvtor_name"].ToString();
                        lblDeprtDateTime.Text = "Deprt.Time " + dt.Rows[0]["triptime"].ToString() + "<br/> Trip Date " + dt.Rows[0]["tripdatde"].ToString();
                        int i = 0;
                        foreach (DataRow drow in dt.Rows)
                        {
                            System.Web.UI.HtmlControls.HtmlTableRow r = new System.Web.UI.HtmlControls.HtmlTableRow();
                            tbl.Controls.Add(r);
                            System.Web.UI.WebControls.Label TRANSACTIONID = new System.Web.UI.WebControls.Label();
                            System.Web.UI.WebControls.Label stations = new System.Web.UI.WebControls.Label();
                            Literal TRANSACTIONIDbr = new Literal();
                            System.Web.UI.HtmlControls.HtmlTableCell c2 = new System.Web.UI.HtmlControls.HtmlTableCell();
                            r.Controls.Add(c2);
                            TRANSACTIONID.Text = dt.Rows[i]["ticketno"].ToString() + "<br/>";

                            if (dt.Rows[0]["specialtrip_yn"].ToString() == "Y")
                                TRANSACTIONID.Text = TRANSACTIONID.Text + dt.Rows[i]["trvlrname"] + ", " + dt.Rows[i]["idproof"].ToString() + "<br/>";

                            stations.Text = dt.Rows[i]["fr_station"].ToString() + "-" + dt.Rows[i]["tostation"].ToString();
                            c2.Controls.Add(TRANSACTIONID);
                            c2.Controls.Add(stations);
                            System.Web.UI.WebControls.Label SEATCOUNT = new System.Web.UI.WebControls.Label();
                            Literal SEATCOUNTBr = new Literal();
                            System.Web.UI.HtmlControls.HtmlTableCell c3 = new System.Web.UI.HtmlControls.HtmlTableCell();
                            r.Controls.Add(c3);
                            if (dt.Rows[0]["specialtrip_yn"].ToString() == "Y")
                                SEATCOUNT.Text = dt.Rows[i]["seatno"].ToString() + "<br/>(" + dt.Rows[i]["amountfare"].ToString() + " ₹)";
                            else
                                SEATCOUNT.Text = dt.Rows[i]["totalseat_booked"].ToString() + "<br/>(" + dt.Rows[i]["amounttotal"].ToString() + " ₹)";
                            c3.Controls.Add(SEATCOUNT);
                            totalSeats += Convert.ToInt32(dt.Rows[i]["totalseat_booked"].ToString());
                            totalFare += Convert.ToDecimal(dt.Rows[i]["amounttotal"].ToString());


                            i = i + 1;
                        }
                        lbltotseatsbook.Text = "Total Seats Booked " + totalSeats.ToString() + "<br/>";
                        lbltotfare.Text = "Total Fare " + totalFare.ToString();
                        lblbkngcntr.Text = dt.Rows[0]["countername"].ToString();
                        lblbkngby.Text = "Booked By " + dt.Rows[0]["book_by_user_code"].ToString();
                        lblprintdt.Text = DateTime.Now.ToString();
                        panelError.Visible = false;
                        pnldetails.Visible = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            panelError.Visible = true;
        }
    }

    private void loadxml()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("CommonData.xml"));
        XmlNodeList deptname = doc.GetElementsByTagName("dept_Name_en");
        lblDeptName.Text = deptname.Item(0).InnerXml;
    }
}