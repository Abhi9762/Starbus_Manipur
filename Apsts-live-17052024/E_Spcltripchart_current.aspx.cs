using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class E_Spcltripchart_current : System.Web.UI.Page
{
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    string gstn = "04AAALC1790P1ZZ";
    int ReplicationID;
    Int32 totalSeats;
    decimal totalFare;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["p_tripcode"].ToString() != null && Session["p_tripcode"].ToString() != "")
        {
            loadxml();
            loaddata();
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
                    lblgstn.Text = "GSTN.- " + gstn.ToString();
                    lblbkngstn.Text = dt.Rows[0]["bookingstation"].ToString();
                    lblservice.Text = dt.Rows[0]["servicetype_name"].ToString();
                    lblbusno.Text = dt.Rows[0]["busno"].ToString();
                    lblRoute.Text = dt.Rows[0]["routename"].ToString();
                    lbldepot.Text = dt.Rows[0]["officename"].ToString();
                    lblConductor.Text = dt.Rows[0]["conduvtor_name"].ToString();
                    lblDeprtDateTime.Text = dt.Rows[0]["tripdatde"].ToString() + " " + dt.Rows[0]["triptime"].ToString();
                    int i = 0;
                    foreach (DataRow drow in dt.Rows)
                    {
                        System.Web.UI.HtmlControls.HtmlTableRow r = new System.Web.UI.HtmlControls.HtmlTableRow();
                        tbl.Controls.Add(r);

                        System.Web.UI.WebControls.Label lblSEAT_NO = new System.Web.UI.WebControls.Label();
                        System.Web.UI.WebControls.Label lblTICKET_NO = new System.Web.UI.WebControls.Label();
                        System.Web.UI.WebControls.Label lblTRAVELLER_NAME = new System.Web.UI.WebControls.Label();
                        System.Web.UI.WebControls.Label lblIDProof = new System.Web.UI.WebControls.Label();
                        System.Web.UI.WebControls.Label lblSpecialRefNo = new System.Web.UI.WebControls.Label();
                        System.Web.UI.WebControls.Label lblAMOUNT_FARE = new System.Web.UI.WebControls.Label();


                        System.Web.UI.HtmlControls.HtmlTableCell c1 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell c2 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell c3 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell c4 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell c5 = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlTableCell c6 = new System.Web.UI.HtmlControls.HtmlTableCell();

                        r.Controls.Add(c1);
                        r.Controls.Add(c2);
                        r.Controls.Add(c3);
                        r.Controls.Add(c4);
                        r.Controls.Add(c5);
                        r.Controls.Add(c6);

                        lblSEAT_NO.Text = dt.Rows[i]["seatno"].ToString();
                        lblTICKET_NO.Text = dt.Rows[i]["ticketno"].ToString() + "<br />" + dt.Rows[i]["fr_station"] + "-" + dt.Rows[i]["tostation"].ToString();
                        lblTRAVELLER_NAME.Text = dt.Rows[i]["trvlrname"].ToString();
                        lblIDProof.Text = dt.Rows[i]["idproof"].ToString();
                        lblSpecialRefNo.Text = dt.Rows[i]["trvlrrefno"].ToString();
                        lblAMOUNT_FARE.Text = dt.Rows[i]["amountfare"].ToString() + " ₹";


                        c1.Controls.Add(lblSEAT_NO);
                        c2.Controls.Add(lblTICKET_NO);
                        c3.Controls.Add(lblTRAVELLER_NAME);
                        c4.Controls.Add(lblIDProof);
                        c5.Controls.Add(lblSpecialRefNo);
                        c6.Controls.Add(lblAMOUNT_FARE);


                        totalSeats += Convert.ToInt32(dt.Rows[i]["totalseat_booked"].ToString());
                        totalFare += Convert.ToDecimal(dt.Rows[i]["amounttotal"].ToString());


                        i = i + 1;
                    }
                    lbltotseatsbook.Text = totalSeats.ToString();
                    lbltotfare.Text = totalFare.ToString() + " ₹";
                    // lblbkngcntr.Text = dt.Rows(0)("counter_name").ToString()
                    lblbkngby.Text = "Booked By:- " + dt.Rows[0]["countername"].ToString() + "(" + dt.Rows[0]["book_by_user_code"].ToString() + ")";
                    lbltripstartdatetime.Text = DateTime.Now.ToString();
                }
            }


        }
        catch (Exception ex)
        {

        }
    }

    private void loadxml()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("CommonData.xml"));
        XmlNodeList deptname = doc.GetElementsByTagName("dept_Name_en");
        lblDepartmentName.Text = deptname.Item(0).InnerXml;
    }
}