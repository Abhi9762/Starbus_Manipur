using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Auth_cashDepositSlip : System.Web.UI.Page
{
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            loadCashDepositSlip();
            loadGeneralData();
        }
    }

    private void loadGeneralData()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("../CommonData.xml"));
        XmlNodeList deptname = doc.GetElementsByTagName("dept_Name_en");
        lblHeading.Text = deptname.Item(0).InnerXml;
    }

    private void loadCashDepositSlip()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "etm.f_get_cash_deposit_slip");
            MyCommand.Parameters.AddWithValue("p_waybillrefno", Session["_WAYBILLID"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    details.Visible = true;
                    msg.Visible = false;
                    lblRefNo.Text = dt.Rows[0]["waybillid"].ToString();
                    lblDutyDate.Text = dt.Rows[0]["departuretime"].ToString();
                    lblBusNo.Text = dt.Rows[0]["busno"].ToString() + "(" + dt.Rows[0]["bustype"].ToString() + ")";
                    lblDriver1.Text = dt.Rows[0]["driver1name"].ToString() + "," + dt.Rows[0]["driver2name"].ToString();
                    lblConductor1.Text = dt.Rows[0]["conductor1name"].ToString() + "," + dt.Rows[0]["conductor2name"].ToString();
                    lblServiceName.Text = dt.Rows[0]["servicename"].ToString() + "<br>(" + dt.Rows[0]["office_name"].ToString() + ")";
                    lblJIName.Text = dt.Rows[0]["juniorinchargename"].ToString();
                    lblJIDesignation.Text = dt.Rows[0]["jidesignationname"].ToString();
                    lblETM.Text = dt.Rows[0]["etmserialno"].ToString();
                    lblCondDeposit.Text =Convert.ToDecimal( dt.Rows[0]["deposited_amt"].ToString()).ToString("0.##");
                }
                else
                {
                    details.Visible = true;
                    msg.Visible = false;
                }
            }
        }
        catch (Exception ex)
        { }

    }
}