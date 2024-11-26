using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookingCounters : System.Web.UI.Page
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
        Session["heading"] = "Our Booking Counters";
        loadDepo();
    }
    public void loadDepo()//M5
    {
        try
        {
            

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_get_counter");
            MyCommand.Parameters.AddWithValue("p_depotcode", "0");
            
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    grdbookingcntr.DataSource = dt;
                    grdbookingcntr.DataBind();
                    grdbookingcntr.Visible = true;
                }
            }
            else
            {
                _common.ErrorLog("SysAdmStationMgmt-M5", dt.TableName);
            }
           
        }
        catch (Exception ex)
        {
            
        }
    }
}