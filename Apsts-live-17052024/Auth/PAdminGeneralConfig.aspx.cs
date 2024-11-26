using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Auth_PAdminGeneralConfig : BasePage
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
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            Session["_moduleName"] = "General Configuration";
            initpnl();
            pnlAdvancedaysbooking.Visible = true;

            //***********************Advance days booking
            loadAdvancebookingdays();
            LoadAdvandayshistory();

            //***********************Trip Chart Generate/Booking
            getGenralconfig();
            loadAdvBookingTimeHistory();

            //***********************Extra Seat Payment
            loadseattype(ddlseattype);

            getseatpmttypeConfig();
            getseatpmttypeHistory();



            //***********************Payment Gateway Addition
//return;
            loadpaymentStatus();
        //    Loadpmtstatushistory();


            Session["Web"] = null;
            Session["Mobile"] = null;


            //***********************Maximum Seat at a time
            getseatavailable();
            getseatavailableupdate();

            //***********************Advertisement On Ticket
            getCurrentExtraText();
            getHistoryExtraText();

            //***********************Ticket Type /Mode
            loadTicketingTypeStatus();
            getTickettypeupdate();
            LoadTicketStatushistory();

        }
    }

    #region Method
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
        }
    }
    private void initpnl()
    {
        pnlAdvancedaysbooking.Visible = false;
        pnlAdvanceBookingTripChartTime.Visible = false;
        pnlSeatsExtraPayment.Visible = false;
        pnlPmntGatewayStatus.Visible = false;
        pnlTravelerSeatAvailability.Visible = false;
        pnlTicketExtraText.Visible = false;
        pnlWaybillTicketingType.Visible = false;
        tbadvancedays.Text = "";
        //  ddlseattype.SelectedValue = "0";
        tbseatvalue.Text = "";
        tbPaymentName.Text = "";
        //  ddlstatus.SelectedValue = "0";
        tbTripChartGeneraterBooking.Text = "";
        tbTripChartGeneraterTripC.Text = "";

    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "Close");
        Response.Write(popup);
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Please Note", msg, "Close");
        Response.Write(popup);
    }
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupLarge("I", "Information", msg, "Close");
        Response.Write(popup);
    }

    #region Advance Booking Days
    private void LoadAdvandayshistory()//M1
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_advancedays_history");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    gvAdvDayHistory.DataSource = MyTable;
                    gvAdvDayHistory.DataBind();
                    gvAdvDayHistory.Visible = true;
                    pnlAdvDayNoRecord.Visible = false;
                    lbtnAdvancedaysbookingHistoryDownload.Visible = true;
                }
                else
                {
                    pnlAdvDayNoRecord.Visible = true;
                    gvAdvDayHistory.Visible = false;
                }
            }
            else
            {
                pnlAdvDayNoRecord.Visible = true;
                gvAdvDayHistory.Visible = false;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0001", ex.Message.ToString());
            pnlAdvDayNoRecord.Visible = true;
            gvAdvDayHistory.Visible = false;
        }
    }
    private void loadAdvancebookingdays()//M2
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_advance_days");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    gvAdvancebookingdays.DataSource = MyTable;
                    gvAdvancebookingdays.DataBind();
                    gvAdvancebookingdays.Visible = true;
                    pnladvancenostatusrecord.Visible = false;
                }
                else
                {
                    pnladvancenostatusrecord.Visible = true;
                    gvAdvancebookingdays.Visible = false;
                }
            }
            else
            {
                pnladvancenostatusrecord.Visible = true;
                gvAdvancebookingdays.Visible = false;
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0002", ex.Message.ToString());
            pnladvancenostatusrecord.Visible = true;
            gvAdvancebookingdays.Visible = false;
        }
    }
    private bool validvalueAddayes()//M3
    {
        try
        {
            string msg = "";
            int msgcont = 0;

            if (_validation.IsValidInteger(tbadvancedays.Text, 1, 3) == false)
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Please Enter Advance Days" + "<br />";

            }
            else if (int.Parse(tbadvancedays.Text.ToString()) == 0)
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Please Enter minimum 1 Days" + "<br />";

            }
            if (msgcont > 0)
            {
                Errormsg(msg);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0003", ex.Message.ToString());
            Errormsg(ex.Message.ToString());
            return false;
        }
    }
    private bool savedetailsAddayes()//M4
    {
        try
        {
            string UpdatedBy = Session["_UserCode"].ToString();
            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            string Mresult = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_gcadvancebooking_insert");
            MyCommand.Parameters.AddWithValue("p_advancedays", Convert.ToInt32(tbadvancedays.Text.ToString()));
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);

            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0004", ex.Message.ToString());
            return false;
        }
    }
    private void ExportAdvancedaysbookingHistory()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=AdvancedaysbookingHistory.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // To Export all pages
            gvAdvDayHistory.AllowPaging = false;
            this.LoadAdvandayshistory();

            gvAdvDayHistory.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvAdvDayHistory.HeaderRow.Cells)
                cell.BackColor = gvAdvDayHistory.HeaderStyle.BackColor;
            foreach (GridViewRow row in gvAdvDayHistory.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                        cell.BackColor = gvAdvDayHistory.AlternatingRowStyle.BackColor;
                    else
                        cell.BackColor = gvAdvDayHistory.RowStyle.BackColor;
                    cell.CssClass = "textmode";
                }
            }

            gvAdvDayHistory.RenderControl(hw);
            // style to format numbers to string
            string style = "<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    #endregion

    #region Trip Chart Generater/ Booking Closing Time
    private void getGenralconfig()//M5
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();

            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_genconfig");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    if (DBNull.Value.Equals((MyTable.Rows[0]["bookingclosing_time"].ToString())))
                        lblbookingmin.Text = "0";
                    else
                        lblbookingmin.Text = MyTable.Rows[0]["bookingclosing_time"].ToString();
                    if (DBNull.Value.Equals((MyTable.Rows[0]["tripchartgenrate_time"].ToString())))
                        lbltripchartmin.Text = "0";
                    else
                        lbltripchartmin.Text = MyTable.Rows[0]["tripchartgenrate_time"].ToString();
                    if (DBNull.Value.Equals((MyTable.Rows[0]["actionby"].ToString())))
                        lblAdUPDATEDBY.Text = "N/A";
                    else
                        lblAdUPDATEDBY.Text = MyTable.Rows[0]["actionby"].ToString();
                    lblAdupdataiondatetime.Text = MyTable.Rows[0]["actiondate"].ToString();
                    lblbookingmin.Visible = true;
                    lbltripchartmin.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0005", ex.Message.ToString());

            lblbookingmin.Visible = false;
            lbltripchartmin.Visible = false;
        }
    }
    private void loadAdvBookingTimeHistory()//M6
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();

            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_advtimes_history");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    gvTripChartGeneraterHistory.DataSource = MyTable;
                    gvTripChartGeneraterHistory.DataBind();
                    gvTripChartGeneraterHistory.Visible = true;
                    pnlTripChartGeneraterNoRecord.Visible = false;
                    lbtnDownloadTripChartGenerater.Visible = true;
                }
                else
                {
                    pnlTripChartGeneraterNoRecord.Visible = true;
                    gvTripChartGeneraterHistory.Visible = false;
                }
            }
            else
            {
                pnlTripChartGeneraterNoRecord.Visible = true;
                gvTripChartGeneraterHistory.Visible = false;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0006", ex.Message.ToString());
            pnlTripChartGeneraterNoRecord.Visible = true;
            gvTripChartGeneraterHistory.Visible = false;
        }
    }
    private bool validvalueMinConfig()//M7
    {
        try
        {
            string msg = "";
            int msgcont = 0;
            string Booking = tbTripChartGeneraterBooking.Text;
            string TripChart = tbTripChartGeneraterBooking.Text;


            if (_validation.IsValidInteger(tbTripChartGeneraterBooking.Text, 1, tbTripChartGeneraterBooking.MaxLength) == false)
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Please Enter Advance Booking Minute" + "<br />";

            }
            if (_validation.IsValidInteger(tbTripChartGeneraterTripC.Text, 1, tbTripChartGeneraterTripC.MaxLength) == false)
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Please Enter  Advance Trip Chart Minute" + "<br />";

            }
            //else if (Booking < TripChart)
            //{
            //    msgcont = msgcont + 1;
            //    msg = msg + msgcont.ToString() + " Advance Booking Minute should be equal and greater Advance Trip Chart Minute" + "<br />";

            //}
            if (msgcont > 0)
            {
                Errormsg(msg);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0007", ex.Message.ToString());
            return false;
        }
    }
    private bool savedetailsMinConfig()//M8
    {
        try
        {
            string IpAddress = HttpContext.Current.Request.UserHostAddress;
            string UpdatedBy = Session["_UserCode"].ToString();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_gctripbooking_insertupdate");
            MyCommand.Parameters.AddWithValue("p_bookingmin", Convert.ToInt16(tbTripChartGeneraterBooking.Text.ToString()));
            MyCommand.Parameters.AddWithValue("p_tripchartmin", Convert.ToInt16(tbTripChartGeneraterTripC.Text.ToString()));
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                return true;
            }
            else
                return false;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0008", ex.Message.ToString());

            return false;
        }
    }
    private void ExportTripChartGenerater()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=TripChart_BookingClose.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // To Export all pages
            gvTripChartGeneraterHistory.AllowPaging = false;
            this.loadAdvBookingTimeHistory();

            gvAdvDayHistory.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvTripChartGeneraterHistory.HeaderRow.Cells)
                cell.BackColor = gvTripChartGeneraterHistory.HeaderStyle.BackColor;
            foreach (GridViewRow row in gvTripChartGeneraterHistory.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                        cell.BackColor = gvTripChartGeneraterHistory.AlternatingRowStyle.BackColor;
                    else
                        cell.BackColor = gvTripChartGeneraterHistory.RowStyle.BackColor;
                    cell.CssClass = "textmode";
                }
            }

            gvTripChartGeneraterHistory.RenderControl(hw);
            // style to format numbers to string
            string style = "<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    #endregion

    #region Extra Seats  Payment
    private void getseatpmttypeConfig()//M9
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_pmttype_config");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    gvseatextrapmt.DataSource = MyTable;
                    gvseatextrapmt.DataBind();
                    gvseatextrapmt.Visible = true;
                    pnlnoExtraNoRecord.Visible = false;
                }
                else
                {
                    pnlnoExtraNoRecord.Visible = true;
                    gvseatextrapmt.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0009", ex.Message.ToString());
            pnlnoExtraNoRecord.Visible = true;
            gvseatextrapmt.Visible = false;
        }
    }
    private void getseatpmttypeHistory()//M10
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_pmttype_history");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    gvSeatsExtraPaymenthistory.DataSource = MyTable;
                    gvSeatsExtraPaymenthistory.DataBind();
                    gvSeatsExtraPaymenthistory.Visible = true;
                    pnlExtraSeatNoRecord.Visible = false;
                    lbtnDownloadseatsExtraPaymenthistory.Visible = true;
                }
                else
                {
                    pnlExtraSeatNoRecord.Visible = true;
                    gvSeatsExtraPaymenthistory.Visible = false;
                }
            }
            else
            {
                pnlExtraSeatNoRecord.Visible = true;
                gvSeatsExtraPaymenthistory.Visible = false;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0010", ex.Message.ToString());
            pnlExtraSeatNoRecord.Visible = true;
            gvSeatsExtraPaymenthistory.Visible = false;
        }
    }
    private bool validvalueSeatpmtValue()//M11
    {

        try
        {

            string msg = "";
            int msgcont = 0;
            if (ddlseattype.SelectedValue == "0")
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Please Select Seat Type.<br>";
            }

            if (_validation.IsValidInteger(tbseatvalue.Text, 1, tbseatvalue.MaxLength) == false)
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Please Enter Extra Seat Payment Percent" + "<br />";

            }

            if (msgcont > 0)
            {
                Errormsg(msg);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0011", ex.Message.ToString());

            return false;
        }

    }
    private bool savedetailsSeatpmtValue()//M12
    {
        try
        {
            string IpAddress = HttpContext.Current.Request.UserHostAddress;
            string UpdatedBy = Session["_UserCode"].ToString();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_extraseatspmt_insertupdate");
            MyCommand.Parameters.AddWithValue("p_paymentcategory_code", ddlseattype.SelectedValue.ToString());
            MyCommand.Parameters.AddWithValue("p_amount_percentage", Convert.ToInt32(tbseatvalue.Text.ToString()));
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                ddlseattype.SelectedValue = "0";
                tbseatvalue.Text = "";
                return true;

            }
            else
                return false;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0012", ex.Message.ToString());

            return false;
        }
    }
    private void ExportseatsExtraPaymentHistory()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=ExtraSeatsPaymentHistory.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // To Export all pages
            gvSeatsExtraPaymenthistory.AllowPaging = false;
            this.getseatpmttypeHistory();

            gvAdvDayHistory.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvSeatsExtraPaymenthistory.HeaderRow.Cells)
                cell.BackColor = gvSeatsExtraPaymenthistory.HeaderStyle.BackColor;
            foreach (GridViewRow row in gvSeatsExtraPaymenthistory.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                        cell.BackColor = gvSeatsExtraPaymenthistory.AlternatingRowStyle.BackColor;
                    else
                        cell.BackColor = gvSeatsExtraPaymenthistory.RowStyle.BackColor;
                    cell.CssClass = "textmode";
                }
            }

            gvSeatsExtraPaymenthistory.RenderControl(hw);
            // style to format numbers to string
            string style = "<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    private void loadseattype(DropDownList ddlseattype)//M29
    {
        try
        {
            ddlseattype.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_bus_layout_get_pmttypelist");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlseattype.DataSource = dt;
                ddlseattype.DataTextField = "paymentcategory_name";
                ddlseattype.DataValueField = "paymentcategory_code";
                ddlseattype.DataBind();
            }
            ddlseattype.Items.Insert(0, "Select");
            ddlseattype.Items[0].Value = "0";
            ddlseattype.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlseattype.Items.Insert(0, "Select");
            ddlseattype.Items[0].Value = "0";
            ddlseattype.SelectedIndex = 0;
            _common.ErrorLog("PAdminGeneralConfig.aspx-0013", ex.Message.ToString());
        }
    }
    #endregion

    #region Payment Gateway Addition
    private void Loadpmtstatushistory()//M13
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_pmt_statushistory");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    gvPmtstatushistory.DataSource = MyTable;
                    gvPmtstatushistory.DataBind();
                    gvPmtstatushistory.Visible = true;
                    pnlPmtstatusNoRecord.Visible = false;
                    lbtnPaymentGatewayStatusHistoryDownload.Visible = true;
                }
                else
                {
                    pnlPmtstatusNoRecord.Visible = true;
                    gvPmtstatushistory.Visible = false;
                }
            }
            else
            {
                pnlPmtstatusNoRecord.Visible = true;
                gvPmtstatushistory.Visible = false;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0014", ex.Message.ToString());
            pnlPmtstatusNoRecord.Visible = true;
            gvPmtstatushistory.Visible = false;
        }
    }
    private void loadpaymentStatus()//M14
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_pmt_gatewayliststatus");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    gvPmtgatewaystatus.DataSource = MyTable;
                    gvPmtgatewaystatus.DataBind();
                    pnlPmtgatewayNodata.Visible = false;
                    gvPmtgatewaystatus.Visible = true;
                }
                else
                {
                    pnlPmtgatewayNodata.Visible = true;
                    gvPmtgatewaystatus.Visible = false;
                }
            }
            else
            {
                pnlPmtgatewayNodata.Visible = true;
                gvPmtgatewaystatus.Visible = false;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0015", ex.Message.ToString());
            pnlPmtstatusNoRecord.Visible = true;
            gvPmtgatewaystatus.Visible = false;
        }
    }
    private void ExportgvPmtstatushistoryHistory()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=PaymentGatewayAdditionHistory.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // To Export all pages
            gvPmtstatushistory.AllowPaging = false;
            this.Loadpmtstatushistory();

            gvAdvDayHistory.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvPmtstatushistory.HeaderRow.Cells)
                cell.BackColor = gvPmtstatushistory.HeaderStyle.BackColor;
            foreach (GridViewRow row in gvPmtstatushistory.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                        cell.BackColor = gvPmtstatushistory.AlternatingRowStyle.BackColor;
                    else
                        cell.BackColor = gvPmtstatushistory.RowStyle.BackColor;
                    cell.CssClass = "textmode";
                }
            }

            gvPmtstatushistory.RenderControl(hw);
            // style to format numbers to string
            string style = "<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    private bool validvaluepmtStatus()//M15
    {
        try
        {
            string msg = "";
            int msgcont = 0;

            if (tbPaymentName.Text == "")
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Please Enter Payment Name" + "<br />";
            }

            if (Session["Web"] == null)
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Please Choose Web Portal Image.<br>";
            }
            if (Session["Mobile"] == null)
            {
                msgcont = msgcont + 1;
                msg = msg + msgcont.ToString() + ". Please Choose Mobile Application Image.<br>";
            }

            if (msgcont > 0)
            {
                Errormsg(msg);
                return false;
            }
            return true;
        }

        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0016", ex.Message.ToString());

            return false;
        }
    }
    private bool savedetailspmtStatus()//M16
    {
        try
        {
            byte[] Fileweb = null;
            byte[] Filemob = null;
            Fileweb = Session["Web"] as byte[];
            Filemob = Session["Mobile"] as byte[];
            int gatewayid = Convert.ToInt32(0);
            string IpAddress = HttpContext.Current.Request.UserHostAddress;
            string UpdatedBy = Session["_UserCode"].ToString();
            string Status = "A";
            string Action = Session["Action"].ToString();
            if (Action != "S")
            {
                gatewayid = Convert.ToInt32(Session["p_gateway_id"].ToString());
            }
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_gcpmtgateway_insertUpdate");
            MyCommand.Parameters.AddWithValue("p_action", Action);
            MyCommand.Parameters.AddWithValue("p_gateway_id", gatewayid);
            MyCommand.Parameters.AddWithValue("p_gatewayname", tbPaymentName.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_status", Status);
            MyCommand.Parameters.AddWithValue("p_web", Fileweb);
            MyCommand.Parameters.AddWithValue("p_mob", Filemob);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_description", tbDescription.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_req_url", tbRequestUrl.Text.ToString());
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                if (Session["Action"].ToString() == "S")
                {
                    Successmsg("Payment gateway  Details successfully Saved");
                }
                if (Session["Action"].ToString() == "U")
                {
                    Successmsg("Payment gateway successfully updated");
                }  
                loadpaymentStatus();
                Loadpmtstatushistory();
                updateImages(tbPaymentName.Text.ToString());
                ResetPaymentGateway();
                return true;
            }
            else
            {
                _common.ErrorLog("PAdminGeneralConfig.aspx-0017", Mresult);
                _common.ErrorLog("PAdminGeneralConfig-PaymentGatewayAdd-M16", Mresult);
                return false;
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0018", ex.Message.ToString());
            return false;
        }
    }
    private void updateImages(string pgname)
    {
        try
        {
            byte[] Fileweb = null;
            byte[] Filemob = null;
            // Fileweb = Session["Web"] as byte[];
            //Filemob = Session["Mobile"] as byte[];
            string saveDirectory = "../DBimg/PG/";
            string fileName = "";
            string fileSavePath = "";
            if (Session["Web"] != null | Session["Web"].ToString() == "")
            {
                fileName = pgname + "_W.png";
                fileSavePath = Path.Combine(saveDirectory, fileName);
                Fileweb = (byte[])Session["Web"];
                System.IO.File.WriteAllBytes(Server.MapPath(fileSavePath), Fileweb);
            }

            if (Session["Mobile"] != null | Session["Mobile"].ToString() == "")
            {
                fileName = pgname + "_M.png";
                fileSavePath = Path.Combine(saveDirectory, fileName);
                Filemob = (byte[])Session["Mobile"];
                System.IO.File.WriteAllBytes(Server.MapPath(fileSavePath), Filemob);

            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0019", ex.Message.ToString());
            Errormsg("Error,while saving Uploaded File");
            return;
        }
    }
    private void ResetPaymentGateway()
    {
        tbPaymentName.Text = "";
        lblAddPmt.Visible = true;
        lblUpdatePmt.Visible = false;
        lbtnUpdatePmt.Visible = false;
        lbtnPStatusreset.Visible = true;
        lbtnPmtCancel.Visible = false;
        lbtnPStatusupdate.Visible = true;
        ImgWebPortal.Visible = false;
        imgMobileApp.Visible = false;

        lbtncloseWebImage.Visible = false;
        lbtncloseMobileImage.Visible = false;

        //  ddlstatus.SelectedValue = "0";
        loadpaymentStatus();
    }
    private bool updatepmtStatus()//M17
    {
        try
        {
            string IpAddress = HttpContext.Current.Request.UserHostAddress;
            string UpdatedBy = Session["_UserCode"].ToString();
            int paymentGateway = Convert.ToInt32(Session["gatewayid"].ToString());

            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_gcpmtgateway_change_status");
            MyCommand.Parameters.AddWithValue("p_pmtgatewayid", paymentGateway);
            MyCommand.Parameters.AddWithValue("p_status", Session["status"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                Successmsg("Status changes successfully");
                ResetPaymentGateway();

                return true;
            }
            else
                return false;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0020", ex.Message.ToString());
            Errormsg(ex.Message);
            return false;
        }
    }
    public static string GetMimeDataOfFile(HttpPostedFile file)
    {
        IntPtr mimeout = default(IntPtr);
        int MaxContent = Convert.ToInt32(file.ContentLength);
        if (MaxContent > 4096)
        {
            MaxContent = 4096;
        }

        byte[] buf = new byte[MaxContent - 1 + 1];
        file.InputStream.Read(buf, 0, MaxContent);
        int MimeSampleSize = 256;


        string mimeType = System.Web.MimeMapping.GetMimeMapping(file.FileName);
        return mimeType;
    }
    private bool CheckFileExtention(FileUpload FileMobileApp)
    {
        try
        {
            bool fileExtensionOK = false;
            string fileExtension;
            fileExtension = System.IO.Path.GetExtension(FileMobileApp.FileName).ToLower();
            string[] allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };
            for (int i = 0; i <= allowedExtensions.Length - 1; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    fileExtensionOK = true;
                }
            }
            return fileExtensionOK;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0021", ex.Message.ToString());
            Errormsg("Something went wrong with image (allow only .jpg, .png, .jpeg image). ");
            return default(Boolean);
        }
    }
    private bool CheckFileExtentionWeb(FileUpload FileWebPortal)
    {
        try
        {
            bool fileExtensionOK = false;
            string fileExtension;
            fileExtension = System.IO.Path.GetExtension(FileWebPortal.FileName).ToLower();
            string[] allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };
            for (int i = 0; i <= allowedExtensions.Length - 1; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    fileExtensionOK = true;
                }
            }
            return fileExtensionOK;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0022", ex.Message.ToString());
            Errormsg("Something went wrong with image (allow only .jpg, .png, .jpeg image). ");
            return default(Boolean);
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
            _common.ErrorLog("PAdminGeneralConfig.aspx-0023", ex.Message.ToString());
            return null;
        }
    }
    public byte[] convertByteFile(System.Web.UI.WebControls.FileUpload fuFileUpload)
    {
        int intFileLength;
        byte[] byteData = null;
        if (fuFileUpload.HasFile)
        {

            // Check File Extention
            if (CheckFileExtention(fuFileUpload) == true)
            {
                intFileLength = fuFileUpload.PostedFile.ContentLength;
                byteData = new byte[intFileLength + 1];
                byteData = fuFileUpload.FileBytes;
            }
        }
        else
        {
            intFileLength = fuFileUpload.PostedFile.ContentLength;
            byteData = new byte[intFileLength + 1];
            byteData = fuFileUpload.FileBytes;
        }
        return byteData;
    }

    #endregion

    #region Maximum Seat Booking at a Time
    private void getseatavailable()//M18
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_seatavailable");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    gvtravelerseatavailable.DataSource = MyTable;
                    gvtravelerseatavailable.DataBind();
                    pnlNoRecord.Visible = false;
                    gvtravelerseatavailable.Visible = true;
                    lbtnsavetravelerseat.Visible = true;
                }
                else
                {
                    pnlNoRecord.Visible = true;
                    gvtravelerseatavailable.Visible = false;
                    lbtnsavetravelerseat.Visible = false;
                }
            }
            else
            {
                pnlNoRecord.Visible = true;
                gvtravelerseatavailable.Visible = false;
                lbtnsavetravelerseat.Visible = false;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0024", ex.Message.ToString());
            pnlNoRecord.Visible = true;
            gvtravelerseatavailable.Visible = false;
            lbtnsavetravelerseat.Visible = false;

        }
    }
    private void getseatavailableupdate()//M19
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_seatavailable_lastupdate");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    lbllastupdated.Text = MyTable.Rows[0]["service_name"].ToString() + " Bus Service";
                    lblseatupdataiondatetime.Text = MyTable.Rows[0]["actiondate"].ToString();
                    lblseatUPDATEDBY.Text = MyTable.Rows[0]["actionby"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0025", ex.Message.ToString());

        }
    }
    private void loadseatavailableHistory()//M20
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_seatavailable_history");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    gvTravelerSeatAvailabilityHistory.DataSource = MyTable;
                    gvTravelerSeatAvailabilityHistory.DataBind();
                    gvTravelerSeatAvailabilityHistory.Visible = true;
                    pnlTravelerSeatNoRecord.Visible = false;
                    lbtnDownloadTravelerSeatAvailability.Visible = true;
                }
                else
                {
                    gvTravelerSeatAvailabilityHistory.Visible = false;
                    pnlTravelerSeatNoRecord.Visible = true;
                }
            }
            else
            {
                gvTravelerSeatAvailabilityHistory.Visible = false;
                pnlTravelerSeatNoRecord.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0026", ex.Message.ToString());
            gvTravelerSeatAvailabilityHistory.Visible = false;
            pnlTravelerSeatNoRecord.Visible = true;

        }
    }
    private void ExportgvTravelerSeatAvailabilityHistory()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=MaximumSeatBookingHistory.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // To Export all pages
            gvTravelerSeatAvailabilityHistory.AllowPaging = false;
            this.loadseatavailableHistory();

            gvAdvDayHistory.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvTravelerSeatAvailabilityHistory.HeaderRow.Cells)
                cell.BackColor = gvTravelerSeatAvailabilityHistory.HeaderStyle.BackColor;
            foreach (GridViewRow row in gvTravelerSeatAvailabilityHistory.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                        cell.BackColor = gvTravelerSeatAvailabilityHistory.AlternatingRowStyle.BackColor;
                    else
                        cell.BackColor = gvTravelerSeatAvailabilityHistory.RowStyle.BackColor;
                    cell.CssClass = "textmode";
                }
            }

            gvTravelerSeatAvailabilityHistory.RenderControl(hw);
            // style to format numbers to string
            string style = "<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    private bool validvalueTravelerSeat(string newvalue)
    {
        int value = Convert.ToInt32(newvalue);
        if (value < 1 | value > 6)
        {
            return false;
        }
        return true;
    }
    private void resetUpdateSeat()
    {
        foreach (GridViewRow row in gvtravelerseatavailable.Rows)
        {
            TextBox tb = row.FindControl("tbnewseat") as TextBox;
            CheckBox chk = row.FindControl("cbMaxSeat") as CheckBox;
            tb.Text = "";
            chk.Checked = false;
        }
    }
    private void UpdateMaximumSeat()//M21
    {
        try
        {
            int count = 0;
            string UpdatedBy = Session["_UserCode"].ToString();
            for (int i = 0; i < gvtravelerseatavailable.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gvtravelerseatavailable.Rows[i].FindControl("cbMaxSeat");
                HiddenField hfserviceid = (HiddenField)gvtravelerseatavailable.Rows[i].FindControl("serviceid");
                TextBox tb = (TextBox)gvtravelerseatavailable.Rows[i].FindControl("tbnewseat");
                Label lb = (Label)gvtravelerseatavailable.Rows[i].FindControl("lblBusService");
                string Entity = tb.Text; ;
                Entity = tb.Text.Trim().ToString();
                string serviceid = hfserviceid.Value;
                if (chk.Checked == true)
                {
                    if (Entity != "")
                    {
                        if (validvalueTravelerSeat(Entity) == false)
                        {
                            Errormsg("Please insert values between 1 to 6 " + lb.Text.ToString() + " Service");
                            return;
                        }
                        NpgsqlCommand MyCommand = new NpgsqlCommand();
                        string updatedby = Session["_UserCode"].ToString();
                        string IpAddress = HttpContext.Current.Request.UserHostAddress;

                        MyCommand.Parameters.Clear();
                        string Mresult = "";
                        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_travelseatavailab_insertupdate");
                        MyCommand.Parameters.AddWithValue("p_service_type_code", Convert.ToInt16(serviceid));
                        MyCommand.Parameters.AddWithValue("p_current_seats", Convert.ToInt16(Entity));
                        MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
                        MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);

                        Mresult = bll.UpdateAll(MyCommand);
                        if (Mresult == "Success")
                        {

                        }
                        else
                        {
                            count = count + 1;
                        }
                    }
                }
                else
                {
                    count = count + 1;
                }

            }
            if (count == 0)
            {
                resetUpdateSeat();
                getseatavailable();
                loadseatavailableHistory();
            }
            Successmsg("Traveler Seat Availability Updated Successfully");
            resetUpdateSeat();
            getseatavailable();
            loadseatavailableHistory();
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0027", ex.Message.ToString());
            Errormsg(ex.Message);
        }
    }
    #endregion

    #region  Advertisement On Ticket
    private void getCurrentExtraText()//M22
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();

            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_ticketextra_texselect");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    string text = MyTable.Rows[0]["adverton_ticket"].ToString();
                    lblTicketExtraTextCurrent.Text = text;
                    pnlnoTicketExtraText.Visible = false;
                    lblTicketExtraTextCurrent.Visible = true;
                    lbtnTicketExtraTextRemove.Visible = true;
                    if (text == "0")
                    {
                        lblTicketExtraTextCurrent.Text = "No Extra text available for ticket.";
                        pnlnoTicketExtraText.Visible = true;
                        lblTicketExtraTextCurrent.Visible = false;
                        lbtnTicketExtraTextRemove.Visible = false;
                    }
                }
                else
                {
                    lblTicketExtraTextCurrent.Text = "No Extra text available for ticket.";
                    pnlnoTicketExtraText.Visible = true;
                    lblTicketExtraTextCurrent.Visible = false;
                    lbtnTicketExtraTextRemove.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            pnlnoTicketExtraText.Visible = true;
            lblTicketExtraTextCurrent.Visible = false;
            lbtnTicketExtraTextRemove.Visible = false;
            _common.ErrorLog("PAdminGeneralConfig.aspx-0028", ex.Message.ToString());
            lblTicketExtraTextCurrent.Text = "No Extra text available for ticket. Contact to helpdesk with error" + ex.ToString();
        }
    }
    private void getHistoryExtraText()//M23
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_ticketextra_texselect_history");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    gvHistoryTicketExtraText.DataSource = MyTable;
                    gvHistoryTicketExtraText.DataBind();
                    gvHistoryTicketExtraText.Visible = true;
                    pnlAdveNoRecord.Visible = false;
                    lbtnDownloadTicketExtraText.Visible = true;
                }
                else
                {
                    pnlAdveNoRecord.Visible = true;
                    gvHistoryTicketExtraText.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0029", ex.Message.ToString());
            pnlAdveNoRecord.Visible = true;
            gvHistoryTicketExtraText.Visible = false;
        }
    }
    private void ExportgvHistoryTicketExtraText()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=AdvertisementOnTicketHistory.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // To Export all pages
            gvHistoryTicketExtraText.AllowPaging = false;
            this.getHistoryExtraText();

            gvHistoryTicketExtraText.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvHistoryTicketExtraText.HeaderRow.Cells)
                cell.BackColor = gvHistoryTicketExtraText.HeaderStyle.BackColor;
            foreach (GridViewRow row in gvHistoryTicketExtraText.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                        cell.BackColor = gvHistoryTicketExtraText.AlternatingRowStyle.BackColor;
                    else
                        cell.BackColor = gvHistoryTicketExtraText.RowStyle.BackColor;
                    cell.CssClass = "textmode";
                }
            }

            gvHistoryTicketExtraText.RenderControl(hw);
            // style to format numbers to string
            string style = "<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    private void saveExtraText(string extraText)//M24
    {
        try
        {
            string IpAddress = HttpContext.Current.Request.UserHostAddress;
            string UpdatedBy = Session["_UserCode"].ToString();
            string Maction = Session["_Action"].ToString();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_gc_advertisementontext_insertupdate");
            MyCommand.Parameters.AddWithValue("p_action", Maction);
            MyCommand.Parameters.AddWithValue("p_text", extraText);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    getCurrentExtraText();
                    tbTicketExtraText.Text = "";
                    getHistoryExtraText();
                    Successmsg("Save Successfully.");
                }
                else
                    Errormsg("Something went wrong Please try again.");
            }

        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0030", ex.Message.ToString());

        }
    }

    #endregion

    #region Ticket Type /Mode

    private void ExportgvTicketTypeHistory()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=TicketTypeModeHistory.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // To Export all pages
            gvTicketTypeHistory.AllowPaging = false;
            this.LoadTicketStatushistory();

            gvTicketTypeHistory.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvTicketTypeHistory.HeaderRow.Cells)
                cell.BackColor = gvTicketTypeHistory.HeaderStyle.BackColor;
            foreach (GridViewRow row in gvTicketTypeHistory.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                        cell.BackColor = gvTicketTypeHistory.AlternatingRowStyle.BackColor;
                    else
                        cell.BackColor = gvTicketTypeHistory.RowStyle.BackColor;
                    cell.CssClass = "textmode";
                }
            }

            gvTicketTypeHistory.RenderControl(hw);
            // style to format numbers to string
            string style = "<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    private bool saveTicketStatus()//M25
    {
        try
        {
            string IpAddress = HttpContext.Current.Request.UserHostAddress;
            string UpdatedBy = Session["_UserCode"].ToString();
            int Tickettype = Convert.ToInt32(Session["Tickettype"].ToString());

            string Mresult = "";
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_ticketingchange_status");
            MyCommand.Parameters.AddWithValue("p_mode_id", Tickettype);
            MyCommand.Parameters.AddWithValue("p_status", Session["ticketstatus"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                Successmsg(" Ticket Status changes successfully");
                loadTicketingTypeStatus();
                getTickettypeupdate();
                return true;
            }

            else
                return false;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0031", ex.Message.ToString());

            return false;
        }
    }
    private void getTickettypeupdate()//M26
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_ticketing_typelastupdate");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    lblTicketType.Text = MyTable.Rows[0]["modename"].ToString() + " Ticket Type";
                    lblLastUpdationOn.Text = MyTable.Rows[0]["actiondate"].ToString();
                    lbllastUpdatedBy.Text = MyTable.Rows[0]["actionby"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0032", ex.Message.ToString());

        }
    }
    private void loadTicketingTypeStatus()//M27
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_ticketing_typestatus");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.TableName == "Success")
            {
                if (MyTable.Rows.Count > 0)
                {
                    gvTicketTypes.DataSource = MyTable;
                    gvTicketTypes.DataBind();
                    gvTicketTypes.Visible = true;
                    pnlTicketNoRecord.Visible = false;
                }
                else
                {
                    gvTicketTypes.Visible = false;
                    pnlTicketNoRecord.Visible = true;
                }
            }
            else
            {
                gvTicketTypes.Visible = false;
                pnlTicketNoRecord.Visible = true;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0033", ex.Message.ToString());
            gvTicketTypes.Visible = false;
            pnlTicketNoRecord.Visible = true;
        }
    }
    private void LoadTicketStatushistory()//M28
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_ticketing_typehistory");
            DataTable MyTable = bll.SelectAll(MyCommand);
            if (MyTable.Rows.Count > 0)
            {
                gvTicketTypeHistory.DataSource = MyTable;
                gvTicketTypeHistory.DataBind();
                gvTicketTypeHistory.Visible = true;
                pnlTicketIssueNoRecord.Visible = false;
                lbtnDownloadWaybillTicketingType.Visible = true;
            }
            else
            {
                pnlTicketIssueNoRecord.Visible = true;
                gvTicketTypeHistory.Visible = false;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig.aspx-0034", ex.Message.ToString());
            pnlTicketIssueNoRecord.Visible = true;
            gvTicketTypeHistory.Visible = false;
        }
    }

    #endregion


    #endregion

    #region Events
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Session["Action"].ToString() == "Status")
        {
            updatepmtStatus();

        }
        if (Session["Action"].ToString() == "TicketStatus")
        {
            saveTicketStatus();

        }
        if (Session["Action"].ToString() == "1")
        {
            if (savedetailsAddayes() == true)
            {
                loadAdvancebookingdays();
                LoadAdvandayshistory();
                tbadvancedays.Text = "";
                Successmsg("Advance Days from online Booking Successfully Updated");
            }
            else
                Errormsg("Error,while saving");
        }
        if (Session["Action"].ToString() == "2")
        {
            if (savedetailsMinConfig() == true)
            {
                getGenralconfig();
                loadAdvBookingTimeHistory();
                tbTripChartGeneraterBooking.Text = "";
                tbTripChartGeneraterTripC.Text = "";
                Successmsg("Advance Booking/TripChart minute Successfully Save/Update");
            }
            else
                Errormsg("Error,while saving");
        }
        if (Session["Action"].ToString() == "3")
        {
            if (savedetailsSeatpmtValue() == true)
            {
                getseatpmttypeConfig();
                getseatpmttypeHistory();
                ddlseattype.SelectedValue = "0";
                tbseatvalue.Text = "";
                Successmsg("Extra Seats Payment Successfully Save/Update");
            }
            else
                Errormsg("Error Ocurred !Please Check");
        }
        if (Session["Action"].ToString() == "S" || Session["Action"].ToString() == "U")
        {
            savedetailspmtStatus();
        }
        if (Session["Action"].ToString() == "5")
        {
            UpdateMaximumSeat();

        }
        if (Session["Action"].ToString() == "6")
        {
            Session["_Action"] = "S";
            saveExtraText(tbTicketExtraText.Text.Trim());
        }
        if (Session["Action"].ToString() == "7")
        {
            Session["_Action"] = "D";
            saveExtraText(tbTicketExtraText.Text.Trim());

        }
    }
    #region Advance Booking Days 
    protected void lbtnAdvancedaysbooking_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        initpnl();
        pnlAdvancedaysbooking.Visible = true;

    }
    protected void lbtnAdvanceDayBViewHistory_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        mpAdvDayBHistory.Show();
        LoadAdvandayshistory();
    }
    protected void gvAdvDayHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAdvDayHistory.PageIndex = e.NewPageIndex;
        LoadAdvandayshistory();
        mpAdvDayBHistory.Show();
    }
    protected void lbtnAdvancedaysbookingHistoryDownload_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ExportAdvancedaysbookingHistory();
    }
    protected void lbtnresetadvancedays_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        tbadvancedays.Text = "";
        loadAdvancebookingdays();
        LoadAdvandayshistory();
    }
    protected void lbtnsaveadvancedays_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvalueAddayes() == false)
            return;
        Session["Action"] = "1";
        lblConfirmation.Text = "Do you want Update Online advance days?";
        mpConfirmation.Show();

    }
    protected void lbtnViewHelp_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "1. Here you can configure settings like Advance Booking Days.<br/>";
        InfoMsg(msg);
    }

    #endregion

    #region Trip Chart Generater/ Booking Closing Time
    protected void lbtnAdvanceBookingTripChartTime_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        initpnl();
        pnlAdvanceBookingTripChartTime.Visible = true;

    }
    protected void lbtnTripChartGenerater_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        mpTripChartGenerater.Show();
        loadAdvBookingTimeHistory();
    }
    protected void lbtnSaveTripChartGenerater_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvalueMinConfig() == false)
        {
            return;
        }

        Session["Action"] = "2";
        lblConfirmation.Text = "Do you want Update Booking/TripChart minute?";
        mpConfirmation.Show();
    }
    protected void lbtnResetTripChartGenerater_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        getGenralconfig();
        tbTripChartGeneraterBooking.Text = "";
        tbTripChartGeneraterTripC.Text = "";
    }
    protected void gvAdvBookingTimeHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTripChartGeneraterHistory.PageIndex = e.NewPageIndex;
        loadAdvBookingTimeHistory();
        mpTripChartGenerater.Show();
    }
    protected void lbtnDownloadTripChartGenerater_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ExportTripChartGenerater();
    }
    protected void lbtnhelp_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "1. Here you can configure settings like Trip Chart Generater/ Booking Closing Time.<br/>";
        InfoMsg(msg);
    }

    #endregion

    #region Extra  Seats Payment
    protected void lbtnSeatsExtraPayment_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        initpnl();
        pnlSeatsExtraPayment.Visible = true;

    }
    protected void lbtnseatsExtraPaymentHistory_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        mpSeatsExtraPaymentHistory.Show();
        getseatpmttypeHistory();
    }
    protected void lbtnsaveseatpmtvalue_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvalueSeatpmtValue() == false)
            return;
        Session["Action"] = "3";
        lblConfirmation.Text = "Do you want Save Extra Seats Payment?";
        mpConfirmation.Show();

    }
    protected void lbtnresetseatpmtvalue_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        getseatpmttypeConfig();
        getseatpmttypeHistory();
        ddlseattype.SelectedValue = "0";
        tbseatvalue.Text = "";
    }
    protected void gvSeatsExtraPaymenthistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSeatsExtraPaymenthistory.PageIndex = e.NewPageIndex;
        getseatpmttypeHistory();
        mpSeatsExtraPaymentHistory.Show();
    }
    protected void lbtnDownloadseatsExtraPaymenthistory_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ExportseatsExtraPaymentHistory();
    }
    protected void lbtnseatshelp_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "1. Here you can configure settings like  Extra Seats payment.<br/>";
        InfoMsg(msg);
    }
    #endregion

    #region Payment Gateway Addition
    protected void lbtnPaymentGatewayStatus_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        initpnl();
        pnlPmntGatewayStatus.Visible = true;

    }
    protected void lbtnPaymentGatewayStatusHistory_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        mpPaymentGateway.Show();
        Loadpmtstatushistory();
    }
    protected void gvPmtgatewaystatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPmtgatewaystatus.PageIndex = e.NewPageIndex;
        Loadpmtstatushistory();
        mpPaymentGateway.Show();
    }
    protected void lbtnPaymentGatewayStatusHistoryDownload_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ExportgvPmtstatushistoryHistory();
    }
    protected void lbtnPStatusupdate_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvaluepmtStatus() == false)
            return;
        Session["Action"] = "S";
        lblConfirmation.Text = "Do you want Add Payment gateway Details ?";
        mpConfirmation.Show();

    }
    protected void lbtnUpdatePmt_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (validvaluepmtStatus() == false)
            return;
        Session["Action"] = "U";
        lblConfirmation.Text = "Do you want Add Payment gateway Details ?";
        mpConfirmation.Show();
    }
    protected void lbtnPmtCancel_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ResetPaymentGateway();
    }
    protected void lbtnPStatusreset_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ResetPaymentGateway();

    }
    protected void gvPmtstatushistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvPmtstatushistory.PageIndex = e.NewPageIndex;
        Loadpmtstatushistory();
        mpPaymentGateway.Show();
    }
    protected void gvPmtgatewaystatus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        //int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "Updation")
        {
            lblUpdatePmt.Visible = true;
            lblAddPmt.Visible = false;
            lbtnUpdatePmt.Visible = true;
            lbtnPStatusupdate.Visible = false;
            lbtnPmtCancel.Visible = true;
            lbtnPStatusreset.Visible = false;
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Session["p_gateway_id"] = gvPmtgatewaystatus.DataKeys[row.RowIndex]["gatewayid"].ToString();

            string gatewayname = gvPmtgatewaystatus.DataKeys[row.RowIndex]["gatewayname"].ToString();
            string gatewayDescription = gvPmtgatewaystatus.DataKeys[row.RowIndex]["descr"].ToString();
            string gatewayReqUrl = gvPmtgatewaystatus.DataKeys[row.RowIndex]["url"].ToString();
            string Status = gvPmtgatewaystatus.DataKeys[row.RowIndex]["statusname"].ToString();

            Session["Web"] = gvPmtgatewaystatus.DataKeys[row.RowIndex]["img_web"];
            Byte[] imgbytes = (Byte[])Session["Web"];
            ImgWebPortal.ImageUrl = GetImage(imgbytes);
            ImgWebPortal.Visible = true;

            Session["Mobile"] = gvPmtgatewaystatus.DataKeys[row.RowIndex]["img_app"];
            Byte[] mobile = (Byte[])Session["Mobile"];
            imgMobileApp.ImageUrl = GetImage(mobile);
            imgMobileApp.Visible = true;
            lbtncloseWebImage.Visible = true;
            lbtncloseMobileImage.Visible = true;
            tbPaymentName.Text = gatewayname;
            tbDescription.Text = gatewayDescription;
            tbRequestUrl.Text = gatewayReqUrl;
            Session["Action"] = "U";
            //Session["webcount"] = "W";
            //Session["mobcount"] = "M";

        }
        if (e.CommandName == "ActiveDeactive")
        {
            try
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                string gatewayId = gvPmtgatewaystatus.DataKeys[row.RowIndex]["gatewayid"].ToString();
                string Status = gvPmtgatewaystatus.DataKeys[row.RowIndex]["statusname"].ToString();
                string SendStatus = "";

                //if (gatewayId.ToString() == null)
                if (String.IsNullOrEmpty(gatewayId.ToString()))
                {
                    Errormsg("Invalid Gateway");
                }
                if (Status != null)
                {

                    if (Status == "A")
                    {
                        SendStatus = "D";
                    }
                    else if (Status == "D")
                    {
                        SendStatus = "A";
                    }
                }
                Session["gatewayid"] = gatewayId;
                Session["status"] = SendStatus;
                Session["Action"] = "Status";
                lblConfirmation.Text = "Do you want to Change Payment Gateway Status?";
                mpConfirmation.Show();
            }
            catch (Exception ex)
            {
                _common.ErrorLog("PAdminGeneralConfig-PaymentGateway-E2", ex.Message.ToString());
            }
        }
    }//Error code- E2
    protected void gvPmtgatewaystatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnUpdatepmtStatus = (LinkButton)e.Row.FindControl("lbtnUpdatepmtStatus");
            LinkButton lbtnpmtActiveDeactive = (LinkButton)e.Row.FindControl("lbtnpmtActiveDeactive");
            LinkButton lnkbtnDecative = (LinkButton)e.Row.FindControl("lnkbtnDecative");
            // LinkButton lbtnDiscontinue = (LinkButton)e.Row.FindControl("lbtnDiscontinue");
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            lbtnpmtActiveDeactive.Visible = false;
            // lbtnDiscontinue.Visible = false;

            if (rowView["statusname"].ToString() == "A")
            {
                lnkbtnDecative.Visible = true;
            }
            else if (rowView["statusname"].ToString() == "D")
            {
                lbtnpmtActiveDeactive.Visible = true;
                lbtnUpdatepmtStatus.Visible = false;

            }


        }
    }
    protected void lbtnpmtgatewayhelp_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "1. Here you can configure settings like Payment Gateway Management etc.<br/>";
        InfoMsg(msg);
    }
    protected void btnUploadWebPortal_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (!FileWebPortal.HasFile)
        {

            Errormsg("Please select report first");

            return;
        }
        string _fileFormat = GetMimeDataOfFile(FileWebPortal.PostedFile);
        if ((_fileFormat == "image/png") || (_fileFormat == "image/jpeg"))
        {
        }
        else
        {
            Errormsg("File must png, jpg, jpeg type");

            return;
        }
        if (!CheckFileExtentionWeb(FileWebPortal))
        {
            Errormsg("File must be png, jpg, jpeg type");

            return;
        }

        decimal size = Math.Round((Convert.ToDecimal(FileWebPortal.PostedFile.ContentLength) / Convert.ToDecimal(2024)), 2);
        if (size > 1024 || size < 10)
        {
            Errormsg("File size must be between 10 kb to 1 Mb");

            return;
        }

        //System.Drawing.Image img = System.Drawing.Image.FromStream(FileWebPortal.PostedFile.InputStream);
        //int height = img.Height;
        //int width = img.Width;
        // size = FileWebPortal.PostedFile.ContentLength;
        //string dimension = width.ToString() + "*" + height.ToString();
        //if (size > 2024 && dimension != "200*100")
        //{
        //    Errormsg("File height and width must be between 200 pixel to 100 pixel ");
        //    return;
        //}
        byte[] PhotoImage = null;
        PhotoImage = convertByteFile(FileWebPortal);
        ImgWebPortal.ImageUrl = GetImage(PhotoImage);
        ImgWebPortal.Visible = true;
        lbtncloseWebImage.Visible = true;
        Session["Web"] = FileWebPortal.FileBytes;
        Session["webcount"] = "W";
    }
    protected void btnUploadMobileApp_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (!FileMobileApp.HasFile)
        {

            Errormsg("Please select report first");
            return;
        }
        string _fileFormat = GetMimeDataOfFile(FileMobileApp.PostedFile);
        if ((_fileFormat == "image/png") || (_fileFormat == "image/jpeg"))
        {
        }
        else
        {
            Errormsg("File must be png, jpg, jpeg type");
            return;
        }
        if (!CheckFileExtention(FileMobileApp))
        {
            Errormsg("File must be png, jpg, jpeg type");
            return;
        }

        decimal size = Math.Round((Convert.ToDecimal(FileMobileApp.PostedFile.ContentLength) / Convert.ToDecimal(1024)), 2);
        if (size > 1024 || size < 10)
        {
            Errormsg("File size must be between 10 kb to 1 mb");
            return;
        }
        byte[] PhotoImage = null;
        PhotoImage = convertByteFile(FileMobileApp);
        imgMobileApp.ImageUrl = GetImage(PhotoImage);
        imgMobileApp.Visible = true;
        lbtncloseMobileImage.Visible = true;
        Session["Mobile"] = FileMobileApp.FileBytes;
        Session["mobcount"] = "M";
    }
    protected void lbtncloseWebImage_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["Web"] = null;
        ImgWebPortal.Visible = false;
        lbtncloseWebImage.Visible = false;
    }
    protected void lbtncloseMobileImage_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["Mobile"] = null;
        imgMobileApp.Visible = false;
        lbtncloseMobileImage.Visible = false;
    }
    #endregion

    #region Maximum Seat Booking at a Time
    protected void lbtnTravelerSeatAvailability_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        initpnl();

        pnlTravelerSeatAvailability.Visible = true;

    }
    protected void lbtnTravelerSeatAvailabilityHistory_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        mpTravelerSeatAvailabilityHistory.Show();
        loadseatavailableHistory();
    }
    protected void lbtnDownloadTravelerSeatAvailability_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ExportgvTravelerSeatAvailabilityHistory();
    }
    protected void gvTravelerSeatAvailabilityHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTravelerSeatAvailabilityHistory.PageIndex = e.NewPageIndex;
        loadseatavailableHistory();
        mpTravelerSeatAvailabilityHistory.Show();

    }
    protected void lbtsavetravelerseat_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();

        try
        {
            int count = 0;
            foreach (GridViewRow row in gvtravelerseatavailable.Rows)
            {
                CheckBox chk = row.FindControl("cbMaxSeat") as CheckBox;
                Label lblservice = row.FindControl("lblBusService") as Label;
                if (chk.Checked == false)
                {
                    count = count + 1;
                }
                else
                {
                    count = 0;
                    TextBox tb = row.FindControl("tbnewseat") as TextBox;
                    string newvalue = tb.Text;
                    if (newvalue == "")
                    {
                        chk.Checked = false;
                        count = count + 1;
                        break;
                    }
                }
                if (count == 0)
                {
                    break;

                }
            }
            if (count > 0)
            {
                Errormsg("Please Checked at least one Service Type and insert values between 1 to 6 Checked service");
                return;
            }
            Session["Action"] = "5";
            lblConfirmation.Text = "Do you want Save/Update Traveler Booking Max Seat A time ?";
            mpConfirmation.Show();
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminGeneralConfig-MaximumSeatBooking-E1", ex.Message.ToString());
        }
    }//Error code- E1
    protected void gvtravelerseatavailable_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvtravelerseatavailable.PageIndex = e.NewPageIndex;
        getseatavailable();
    }
    protected void lbtntravelerhelp_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "1. Here you can configure settings like Maximum Seat Booking at a Time etc.<br/>";
        InfoMsg(msg);
    }
    #endregion

    #region  Advertisement On Ticket
    protected void lbtnTicketExtraText_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        initpnl();
        pnlTicketExtraText.Visible = true;

    }
    protected void lbtnTicketExtraTextHistory_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        mpgvHistoryTicketExtraText.Show();
        getHistoryExtraText();

    }
    protected void lbtnDownloadTicketExtraText_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ExportgvHistoryTicketExtraText();
    }
    protected void gvHistoryTicketExtraText_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHistoryTicketExtraText.PageIndex = e.NewPageIndex;
        getHistoryExtraText();
        mpgvHistoryTicketExtraText.Show();
    }
    protected void lbtnTicketExtraTextSave_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string text = tbTicketExtraText.Text.Trim();
        if (text.Length < 10 | text.Length > 200)
        {
            Errormsg("Please enter valid text. Minimum 10 character and Maximum 200 character.");
            return;
        }
        Session["Action"] = "6";
        lblConfirmation.Text = "Do you want Save ticket bottom text?";
        mpConfirmation.Show();
    }
    protected void lbtnTicketExtraTextRemove_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Session["Action"] = "7";
        lblConfirmation.Text = "Do you want Remove ticket bottom text?";
        mpConfirmation.Show();

    }
    protected void lbtntickethelp_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "1. Here you can configure settings like Advertisement On Ticket.<br/>";
        InfoMsg(msg);
    }
    #endregion

    #region Ticket Type /Mode
    protected void lbtnBusTicketingType_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        initpnl();

        pnlWaybillTicketingType.Visible = true;

    }
    protected void lbtnbusbillTicketingType_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        mpTicketTypeHistory.Show();
        LoadTicketStatushistory();
    }
    protected void lbtnDownloadWaybillTicketingType_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ExportgvTicketTypeHistory();
    }
    protected void gvTicketTypeHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTicketTypeHistory.PageIndex = e.NewPageIndex;
        LoadTicketStatushistory();
        mpTicketTypeHistory.Show();
    }
    protected void gvTicketTypes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "ActiveDeactive")
        {
            try
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                string Tickettype = gvTicketTypes.DataKeys[index].Values["modeid"].ToString();
                string ticketstatus = gvTicketTypes.DataKeys[index].Values["p_status"].ToString();
                string SendStatus = "";

                if (Tickettype.ToString() == null)
                {
                    Errormsg("Invalid  Ticket Type");
                }
                if (ticketstatus != null)
                {

                    if (ticketstatus == "A")
                    {
                        SendStatus = "D";
                    }
                    else if (ticketstatus == "D")
                    {
                        SendStatus = "A";
                    }
                }
                Session["Tickettype"] = Tickettype;
                Session["ticketstatus"] = SendStatus;
                Session["Action"] = "TicketStatus";
                lblConfirmation.Text = "Do you want to Change Ticket Type Status?";
                mpConfirmation.Show();
            }
            catch (Exception ex)
            {
                _common.ErrorLog("PAdminGeneralConfig-TicketIssueMode-E1", ex.Message.ToString());
            }
        }

    }//Error code-E1
    protected void lbtnbilltickethelp_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        string msg = "";
        msg = msg + "1. Here you can configure settings likeTicket Type /Mode.<br/>";
        InfoMsg(msg);
    }
    #endregion

    #endregion



}