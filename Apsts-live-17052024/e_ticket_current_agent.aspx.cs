using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class e_ticket_current_agent : System.Web.UI.Page
{
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["p_ticketno"] = "72F70010520241000004";
        if (Session["p_ticketno"].ToString() != null && Session["p_ticketno"].ToString() != "")
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
    private void loaddata()//M1
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "current_booking.f_get_current_tkt_dtls");
            MyCommand.Parameters.AddWithValue("p_ticketno", Session["p_ticketno"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lbltktNo.Text = "Ticket No " + dt.Rows[0]["ticketno"].ToString() + " " + dt.Rows[0]["service_type_name"].ToString();
                    lblbusno.Text = "Bus No <b>" + dt.Rows[0]["bus_registrationno"].ToString() + "</b><br/> (" + dt.Rows[0]["office_name"] + ")";
                    lblstn.Text = dt.Rows[0]["from_station_name"] + " To " + dt.Rows[0]["to_station_name"].ToString();
                    lblfare.Text = "Fare Per Seat - <b>₹ " + dt.Rows[0]["amountfare"].ToString() + "</b>";
                    lblcommission.Text= "Extra Commission - <b>₹ " + dt.Rows[0]["current_commission"].ToString() + "</b>";
                    lbltotfare.Text = "Total Fare " + dt.Rows[0]["current_commission"].ToString() + "+(" + dt.Rows[0]["total_seatsbooked"].ToString() + "*" + dt.Rows[0]["amountfare"].ToString() + ") - <b>₹ " + dt.Rows[0]["amounttotal"].ToString() + "</b>";
                    lbltotseat.Text = "Total Seats " + dt.Rows[0]["total_seatsbooked"].ToString();
                    lbltotdistance.Text = "Total Distance " + dt.Rows[0]["total_distkm"].ToString();
                    lblseatno.Text = "Seat(s) No - <b>" + dt.Rows[0]["seat_number"].ToString() + "</b>";
                    lblConductor.Text = "Conductor " + dt.Rows[0]["conductor_name"].ToString();
                    //lblbkngcntr.Text = "Booking Counter " & dt.Rows(0)("booked_by_user_code")
                    lblbookedby.Text = "Booked By " + dt.Rows[0]["bookedby_type"].ToString()+"(" + dt.Rows[0]["booked_by_usercode"].ToString()+")";
                    lblbookingdt.Text = dt.Rows[0]["booking_datetime"].ToString();
                    panelError.Visible = false;
                    pnldetails.Visible = true;
                }
                else
                {
                    panelError.Visible = true;
                    pnldetails.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            panelError.Visible = true;
            pnldetails.Visible = false;
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