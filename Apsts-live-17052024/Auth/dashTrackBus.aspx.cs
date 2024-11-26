using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.IO;
using System.Xml;
using System.Collections;
using System.Drawing;

public partial class Auth_dashTrackBus : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["BUS_OR_EMP"].ToString() == "Emp")
            {
                lblempcode.Text = Session["_empname"].ToString();
                lblRepOffice.Text = Session["_repoffice"].ToString();
                lblPostingoffice.Text = Session["_posoffice"].ToString();
                divBusDetails.Visible = false;
                divEmpDetails.Visible = true;
                loadEmployeeDutyDetails(Session["_empcode"].ToString());
                lblBusemp1.Visible = true;
                lblBusemp.Visible = true;
            }
            if (Session["BUS_OR_EMP"].ToString() == "Bus")
            {
                lblbusno.Text = Session["_busno"].ToString();
                lblDepot.Text = Session["_depot"].ToString();
                lblServiceType.Text = Session["_servicetype"].ToString();
                divBusDetails.Visible = true;
                divEmpDetails.Visible = false;
                lblBusemp1.Visible = false;
                lblBusemp.Visible = false;
                if (Session["_gps"].ToString() == "Installed")
                {
                    openiframe(Session["_busno"].ToString());
                    div1.Visible = true;
                    div2.Visible = false;
                }
                else
                {
                    div1.Visible = false;
                    div2.Visible = true;
                }
                loadBusDutyDetails(lblbusno.Text);
            }
        }
    }

    #region"Methods"
    private void openiframe(string busno)
    {
        Literal l1 = new Literal();
        busno = busno.Replace("-", "");
        string accesstoken = "7a9bef82c27db5be6ea4e9beff576639";
        string url = "https://t-location.intangles.com/" + busno + "?vendor-access-token=" + accesstoken + "";
        l1.Text = "<iframe name='we' id='12'  frameborder='no' scrolling='auto' height='700px' width='100%'  src='" + url + "' style='left:0; background-color:#B8B8B8;'></iframe>";
        div1.Controls.Add(l1);
    }
    private void loadBusDutyDetails(string busno)
    {
        try
        {
            divDutyDetails.Visible = false;
            divNoDuty.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getbus_dutydetails");
            MyCommand.Parameters.AddWithValue("p_busno", busno);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                lblDutySlipno.Text = dt.Rows[0]["dutyno"].ToString();
                lblDutyDate.Text = dt.Rows[0]["j_date"].ToString();
                lblDriver.Text = dt.Rows[0]["driver"].ToString();
                lblConductor.Text = dt.Rows[0]["conductor"].ToString();
                lblRoute.Text = dt.Rows[0]["route_"].ToString();
                divNoDuty.Visible = false;
                divDutyDetails.Visible = true;
            }
            else
            {
                divDutyDetails.Visible = false;
                divNoDuty.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sysadmbustracking.aspx-0002", ex.Message.ToString());
        }

    }

    private void loadEmployeeDutyDetails(string empcode)
    {
        try
        {
            divDutyDetails.Visible = false;
            divNoDuty.Visible = true;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getemp_dutydetails");
            MyCommand.Parameters.AddWithValue("p_empcode", empcode);
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                lblDutySlipno.Text = dt.Rows[0]["dutyno"].ToString();
                lblDutyDate.Text = dt.Rows[0]["j_date"].ToString();
                lblDriver.Text = dt.Rows[0]["driver"].ToString();
                lblConductor.Text = dt.Rows[0]["conductor"].ToString();
                lblRoute.Text = dt.Rows[0]["route_"].ToString();
                lblBusemp.Text = dt.Rows[0]["busno"].ToString();
                if (dt.Rows[0]["gpsyn_"].ToString() == "Y")
                {
                    openiframe(lblBusemp.Text);
                    div1.Visible = true;
                    div2.Visible = false;
                }
                else
                {
                    div1.Visible = false;
                    div2.Visible = true;
                }
                divNoDuty.Visible = false;
                divDutyDetails.Visible = true;
            }
            else
            {
                divDutyDetails.Visible = false;
                divNoDuty.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("sysadmbustracking.aspx-0002", ex.Message.ToString());
        }
    }
    #endregion
}