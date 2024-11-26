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

public partial class Auth_sysadmbustracking : System.Web.UI.Page
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
        lblSummary.Text = "Summary as on Date "+ DateTime.Now.ToString();
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            LoadBusList();
            
        }
        Test();
    }
    #region Methods
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
        }
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {

        string popup = _popup.modalPopupSmall("S", "Congratulations", msg, "Close");
        Response.Write(popup);
    }
    protected void LoadBusList()
    {
        try
        {
            divNoBuses.Visible = true;
            divBusDetails.Visible = false;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getbus_for_track");
            MyCommand.Parameters.AddWithValue("p_busno", "");
            dt = bll.SelectAll(MyCommand);
            DataRow[] dr_y= dt.Select("gps_yn='Y'");
            DataRow[] dr_n = dt.Select("gps_yn='N'");
            lblTotalBus.Text = dt.Rows.Count.ToString();
            lblConfiguared.Text = dr_y.Length.ToString();
            lblNotConfigured.Text = dr_n.Length.ToString();

            int counter =  dr_y.Length;

            if (counter > 0)
            {
               
                divNoBuses.Visible = false;
                divBusDetails.Visible = true;
                gvBusList.DataSource = dr_y.CopyToDataTable();
                gvBusList.DataBind();
                gvBusList.Visible = true;
                divtrackbusno.Visible = true;
                divtrackbus.Visible = false;
                Test();               
            }
            else
            {
                divNoBuses.Visible = true;
                divBusDetails.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("sysadmbustracking.aspx-0001", ex.Message.ToString());
        }
    }
    private void Test()
    {
        if (gvBusList.Rows.Count > 0)
        {
            gvBusList.UseAccessibleHeader = true;
            gvBusList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

    }
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
            div1.Visible = false;
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
                div1.Visible = true;
            }
            else
            {
                div1.Visible = true;
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
    #region Events
    protected void gvBusList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "viewBus")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string busno = gvBusList.DataKeys[rowIndex]["busno"].ToString();
            string depot = gvBusList.DataKeys[rowIndex]["depot"].ToString();
            string ServiceType= gvBusList.DataKeys[rowIndex]["servicetype"].ToString();
            openiframe(busno);
            divtrackbus.Visible = true;
            divtrackbusno.Visible = false;
            lblbusno.Text = busno;
            lblDepot.Text = depot;
            lblServiceType.Text = ServiceType;
            loadBusDutyDetails(lblbusno.Text);
        }
    }
    protected void lbtndutydetails_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Response.Redirect("sysadmbustracking.aspx");
    }
    
    #endregion


}