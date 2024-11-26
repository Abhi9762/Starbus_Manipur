using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_tripChartGenerateSuccess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         if (!IsPostBack)
        {
            sendSms();
        }
    }

    private void sendSms()
    {
        try
        {
            wsClass obj = new wsClass();
            DataTable dt = obj.gettrip_details(Session["p_tripcode"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["generated_by"].ToString() == "")
                { }
                else
                {
                    string servicecode = dt.Rows[0]["service_code"].ToString() + "/" + dt.Rows[0]["servicetype_name"].ToString();
                    string journeydate = dt.Rows[0]["j_date"].ToString();
                    string deptime = dt.Rows[0]["dept_time"].ToString();
                    string frmst = dt.Rows[0]["fstation_name"].ToString();
                    string tostn = dt.Rows[0]["tstation_name"].ToString();
		    string conductorno = dt.Rows[0]["conductor_mobile"].ToString();
                    string driverrno = dt.Rows[0]["driver_mobile"].ToString();

			
                    string busno = string.IsNullOrEmpty(dt.Rows[0]["bus_code"].ToString()) ? "N/A" : dt.Rows[0]["bus_code"].ToString();
                    DataTable dtt = obj.gettrip_psngr_details(Session["p_tripcode"].ToString());
                    string totalseat = dtt.Rows.Count.ToString();
                   
                     if (conductorno.Length ==0)
                    {
                        conductorno = driverrno;
                    }

                    CommonSMSnEmail sms = new CommonSMSnEmail();

                    //send conductor sms
                    sms.TripChartForConductor(Session["p_tripcode"].ToString(), conductorno, frmst, tostn, deptime, busno, totalseat);


                    int i = 0;
                    foreach (DataRow drow in dtt.Rows)
                    {
                        string pnr = dtt.Rows[i]["ticketno"].ToString();
                        string from = dtt.Rows[i]["from_station"].ToString();
                        string to = dtt.Rows[i]["to_station"].ToString();
                        string trvlmob = dtt.Rows[i]["trvl_mob"].ToString();

 			sms.TripChartForTraveller(pnr, trvlmob, from, to, deptime, busno, conductorno);
                        //send traveller sms


                       
                        i = i + 1;
                    }
                    

                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            //Errormsg("Something Went Wrong");
        }
    }

    protected void Unnamed_Click(object sender, EventArgs e)
    {
        openpage("tripchart.aspx");
    }
    private void openpage(string pagename)
    {
        tkt.Src = pagename;
        mpTripchart.Show();
    }
}