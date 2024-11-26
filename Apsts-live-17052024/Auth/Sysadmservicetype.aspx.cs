using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Data;
using System.Runtime.InteropServices;

public partial class Auth_Sysadmservicetype : BasePage
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    [DllImport("urlmon.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = false)]
    static extern int FindMimeFromData(IntPtr pBC,
    [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl,
    [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeParamIndex = 3)] byte[] pBuffer,
    int cbSize,
    [MarshalAs(UnmanagedType.LPWStr)] string pwzMimeProposed,
    int dwMimeFlags, out IntPtr ppwzMimeOut, int dwReserved);
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["_moduleName"] = "Service Type";
        if (!IsPostBack)
        {
            Random randomclass = new Random();
            Session["rndNoCheck"] = randomclass.Next().ToString();
            hidtoken.Value = Session["rndNoCheck"].ToString();
            Session["Web"] = null;
            Session["Mobile"] = null;
            ServiceTypeList(gvServiceType);
            loadAmenities();
            initpnl();
        }
        ServiceTypeDashCounts();
    }

    #region "Method"
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
        }
    }
    private void ServiceTypeDashCounts()//M1
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_servicetype_getsummary");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                lblSummary.Text = "Summary As on Date " + DateTime.Now;
                lbltotalservice.Text = dt.Rows[0]["totalservice"].ToString();
                lblactiveservice.Text = dt.Rows[0]["activrservice"].ToString();
                lbldiscontinuedservice.Text = dt.Rows[0]["discontinueservice"].ToString();
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("Sysadmservicetype.aspx-0001", ex.Message.ToString());
            return;
        }
    }
    private void ServiceTypeList(GridView grdview)//M2
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_servicetype_get_list");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    grdview.DataSource = dt;
                    grdview.DataBind();
                    grdview.Visible = true;

                  

                }
                else
                {
                    grdview.Visible = false;
                }
            }
            else
            {
                //Errormsg(dt.TableName);
                _common.ErrorLog("Sysadmservicetype.aspx-0002", dt.TableName);

                grdview.Visible = false;
            }
          
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("Sysadmservicetype.aspx-0003", ex.Message.ToString());
            grdview.Visible = false;
           
        }
    }
    private void loadAmenities()//M3
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_servicetype_getamenities");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlAmenities.DataSource = dt;
                    ddlAmenities.DataTextField = "amenityname";
                    ddlAmenities.DataValueField = "amenityid";
                    ddlAmenities.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("Sysadmservicetype.aspx-0004", dt.TableName);
            }
            //ddlAmenities.Items.Insert(0, "SELECT");
            //ddlAmenities.Items[0].Value = "0";
            //ddlAmenities.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("Sysadmservicetype.aspx-0005", ex.Message.ToString());
            ddlAmenities.Items.Insert(0, "SELECT");
            ddlAmenities.Items[0].Value = "0";
            ddlAmenities.SelectedIndex = 0;
        }
    }
    private bool validvalue()//M4
    {
        try
        {
            int msgcount = 0;
            string msg = "";

            if (_validation.IsValidString(tbServiceTypeNameEn.Text, 1, 50) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Service Type Name(En).<br/>";
            }
            if (_validation.isValideDecimalNumber(tbServiceTypeSpeedHill.Text, 1, 5) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Hill Speed.<br/>";
            }

            if (_validation.isValideDecimalNumber(tbServiceTypeSpeedPlain.Text, 1, 5) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Plain Speed.<br/>";
            }
            if (_validation.isValideDecimalNumber(tbServiceTypeACSurchargeperkm.Text, 1, 5) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid AC Surcharge(Per km).<br/>";
            }

            if (_validation.isValideDecimalNumber(tbServiceTypeHeatingSurcharge.Text, 1, 5) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Heating Surcharge (Per Km).<br/>";
            }

            if (cbServiceTypeLuggageWithPassenger.Checked == true)
            {
                if (_validation.IsValidInteger(tbServiceTypeLuggageRate.Text, 1, 2) == false)
                {
                    msgcount = msgcount + 1;
                    msg = msg + msgcount.ToString() + ". Enter Valid Luggage Rate.<br/>";
                }
                if (_validation.IsValidInteger(tbServiceTypeLuggageMultipleUnit.Text, 1, 2) == false)
                {
                    msgcount = msgcount + 1;
                    msg = msg + msgcount.ToString() + ". Enter Valid Luggage Multiple Unit.<br/>";
                }
            }
            if (Session["Web"] == null)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Web Portal Image.<br>";
            }
            if (Session["Mobile"] == null)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Mobile Application Image.<br>";
            }
            if (_validation.IsValidString(tbDescription.Text, 1, 200) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Description.<br/>";
            }
            if (msgcount > 0)
            {

                Errormsg(msg);
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("Sysadmservicetype-M4", ex.Message.ToString());
            return false;
        }
    }
    private void insertServiecType()//M5
    {
        try
        {
            byte[] Fileweb = null;
            Fileweb = Session["Web"] as byte[];
            byte[] Filemob = null;
            Filemob = Session["Mobile"] as byte[];
            string description = "", stNameEng = "", stNameHin = "", stStatus = "", stLugWithPsngr = "N", amenities = "";
            decimal stAcSurC = 0, stservicetax = 0, stHtSurC = 0, stSpHill = 0, stSpPlain = 0, stLugRate = 0, stLugMltipleUnit = 0,
                DriverIncentives = 0, ConductorIncentives = 0, ChildMax = 0, AdultMax = 0, reservationCharge = 0, sgst = 0, cgst = 0, igst = 0, agent_commission = 0;
            string ipaddress = HttpContext.Current.Request.UserHostAddress;


            if (Session["Action"].ToString() == "S" || Session["Action"].ToString() == "U")
            {
                stNameEng = tbServiceTypeNameEn.Text.Trim();
                stNameHin = tbServiceTypeNameHn.Text.Trim();
                stSpHill = Convert.ToDecimal(tbServiceTypeSpeedHill.Text.ToString());
                stSpPlain = Convert.ToDecimal(tbServiceTypeSpeedPlain.Text.ToString());
                stHtSurC = Convert.ToDecimal(tbServiceTypeHeatingSurcharge.Text.ToString());
                stAcSurC = Convert.ToDecimal(tbServiceTypeACSurchargeperkm.Text.ToString());
                if (!String.IsNullOrEmpty(tbOnlineReservationcharge.Text))
                {
                    reservationCharge = Convert.ToDecimal(tbOnlineReservationcharge.Text.ToString());
                }

                if (!String.IsNullOrEmpty(tbServiceTypeservicetax.Text))
                {
                    stservicetax = Convert.ToDecimal(tbServiceTypeservicetax.Text.ToString());
                }
                if (cbServiceTypeLuggageWithPassenger.Checked)
                {
                    stLugWithPsngr = "Y";
                    stLugRate = Convert.ToDecimal(tbServiceTypeLuggageRate.Text.ToString());
                    stLugMltipleUnit = Convert.ToDecimal(tbServiceTypeLuggageMultipleUnit.Text.ToString());
                }

                if (!String.IsNullOrEmpty(tbDriverIncentives.Text))
                {
                    DriverIncentives = Convert.ToDecimal(tbDriverIncentives.Text);
                }
                if (!String.IsNullOrEmpty(tbConductorIncentives.Text))
                {
                    ConductorIncentives = Convert.ToDecimal(tbConductorIncentives.Text);
                }
                if (!String.IsNullOrEmpty(tbAdult.Text))
                {
                    AdultMax = Convert.ToDecimal(tbAdult.Text);
                }
                if (!String.IsNullOrEmpty(tbChild.Text))
                {
                    ChildMax = Convert.ToDecimal(tbChild.Text);
                }
                if (!String.IsNullOrEmpty(tbDescription.Text))
                {
                    description = tbDescription.Text.ToString();
                }
                stStatus = "A";
                if (!String.IsNullOrEmpty(tbSgst.Text))
                {
                    sgst = Convert.ToDecimal(tbSgst.Text.ToString());
                }
                if (!String.IsNullOrEmpty(tbCGST.Text))
                {
                    cgst = Convert.ToDecimal(tbCGST.Text.ToString());
                }
                if (!String.IsNullOrEmpty(tbigst.Text))
                {
                    igst = Convert.ToDecimal(tbigst.Text.ToString());
                }
                if (!String.IsNullOrEmpty(tbagentcommission.Text))
                {
                    agent_commission = Convert.ToDecimal(tbagentcommission.Text.ToString());
                }

            }

            if (Session["Action"].ToString() == "A")
            {
                stStatus = "D";
            }
            if (Session["Action"].ToString() == "D")
            {
                stStatus = "A";
            }
            if (ddlAmenities.SelectedValue != "0")
            {
                foreach (ListItem item in ddlAmenities.Items)
                {
                    if (item.Selected)
                        amenities += item.Value + ",";
                }
                if (amenities.Length > 1)
                    amenities = amenities.Substring(0, amenities.Length - 1);
            }
            //amenities = ddlAmenities.SelectedValue;

            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_servicetype_insertupdate");
            MyCommand.Parameters.AddWithValue("p_action", Session["Action"].ToString());
            MyCommand.Parameters.AddWithValue("p_stid", Convert.ToInt32(Session["ServiceTypeID"].ToString()));
            MyCommand.Parameters.AddWithValue("p_stnen", stNameEng);
            MyCommand.Parameters.AddWithValue("p_stnhi", stNameHin);
            MyCommand.Parameters.AddWithValue("p_acspkm", stAcSurC);
            MyCommand.Parameters.AddWithValue("p_stax", stservicetax);
            MyCommand.Parameters.AddWithValue("p_heatspkm", stHtSurC);
            MyCommand.Parameters.AddWithValue("p_shkmh", stSpHill);
            MyCommand.Parameters.AddWithValue("p_spkmh", stSpPlain);
            MyCommand.Parameters.AddWithValue("p_status", stStatus);
            MyCommand.Parameters.AddWithValue("p_lrate", stLugRate);
            MyCommand.Parameters.AddWithValue("p_lmunit", stLugMltipleUnit);
            MyCommand.Parameters.AddWithValue("p_lwithpsngr", stLugWithPsngr);
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", ipaddress);
            MyCommand.Parameters.AddWithValue("p_reservationcharge", reservationCharge);
            MyCommand.Parameters.AddWithValue("p_amenities", amenities);
            MyCommand.Parameters.AddWithValue("p_driver_incentives", DriverIncentives);
            MyCommand.Parameters.AddWithValue("p_conductor_incentives", ConductorIncentives);
            MyCommand.Parameters.AddWithValue("p_adultmax", AdultMax);
            MyCommand.Parameters.AddWithValue("p_childmax", ChildMax);
            MyCommand.Parameters.AddWithValue("p_description", description);
            MyCommand.Parameters.AddWithValue("p_sgst", sgst);
            MyCommand.Parameters.AddWithValue("p_cgst", cgst);
            MyCommand.Parameters.AddWithValue("p_igst", igst);
            MyCommand.Parameters.AddWithValue("p_agentcommission", agent_commission);
            MyCommand.Parameters.AddWithValue("p_imgweb", (object)Fileweb ?? DBNull.Value);
            MyCommand.Parameters.AddWithValue("p_imgapp", (object)Filemob ?? DBNull.Value);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                
                ServiceTypeList(gvServiceType);
                resetControl();
                if (Session["Action"].ToString() == "S" || Session["Action"].ToString() == "U")
                {
                    Successmsg("Service Type Details Successfully Saved");
                    updateImages(stNameEng);
                }
                if (Session["Action"].ToString() == "A" || Session["Action"].ToString() == "D")
                {
                    Successmsg("Service Type Status Successfully Changed");
                    
                }
                Session["ServiceTypeID"] = null;
                pnlAddServiceType.Visible = true;
                ServiceTypeDashCounts();
            }
            else
            {
                _common.ErrorLog("Sysadmservicetype.aspx-0006", Mresult.ToString());
                Errormsg("Unable to Save Service Type Details. Please try after Some time." + Mresult);
                //resetControl();
                return;
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("Sysadmservicetype.aspx-0007", ex.Message.ToString());
            //resetControl();
            return;
        }
    }
    public void resetControl()
    {
        tbagentcommission.Text = "";
        tbOnlineReservationcharge.Text = "";
        tbServiceTypeNameEn.Text = "";
        tbServiceTypeNameHn.Text = "";
        cbServiceTypeLuggageWithPassenger.Checked = false;
        PanelLuggage.Visible = false;
        tbServiceTypeACSurchargeperkm.Text = "";
        tbServiceTypeservicetax.Text = "";
        tbServiceTypeHeatingSurcharge.Text = "";
        tbServiceTypeSpeedHill.Text = "";
        tbServiceTypeSpeedPlain.Text = "";
        lbtnSave.Visible = true;
        lbtnUpdate.Visible = false;
        lbtnReset.Visible = true;
        LabelHeader.Visible = true;
        lbtncancel.Visible = false;
        LabelHeaderUpdate.Visible = false;
        tbDriverIncentives.Text = "";
        tbConductorIncentives.Text = "";
        tbAdult.Text = "";
        tbChild.Text = "";
        tbDescription.Text = "";
        ImgWebPortal.Visible = false;
        imgMobileApp.Visible = false;
        lbtncloseWebImage.Visible = false;
        lbtncloseMobileImage.Visible = false;
        tbSgst.Text = "";
        tbCGST.Text = "";
        tbigst.Text = "";
        foreach (ListItem item in ddlAmenities.Items)
            item.Selected = false;
    }
    private void Errormsg(string msg)
    {

        string popup = _popup.modalPopupSmall("W", "Information", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "Close");
        Response.Write(popup);
    }
    private void updateImages(string ServiceType)//M12
    {
        try
        {
            byte[] Fileweb = null;
            byte[] Filemob = null;
            string saveDirectory = "../Dbimg/BusServices/";
            string fileName = "";
            string fileSavePath = "";
            if (Session["Web"] != null || Session["Web"].ToString() != "")
            {
                fileName = ServiceType + "_W.png";
                fileSavePath = Path.Combine(saveDirectory, fileName);
                Fileweb = (byte[])Session["Web"];
                System.IO.File.WriteAllBytes(Server.MapPath(fileSavePath), Fileweb);
            }

            if (Session["Mobile"] != null || Session["Mobile"].ToString() != "")
            {
                fileName = ServiceType + "_M.png";
                fileSavePath = Path.Combine(saveDirectory, fileName);
                Filemob = (byte[])Session["Mobile"];
                System.IO.File.WriteAllBytes(Server.MapPath(fileSavePath), Filemob);

            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("Sysadmservicetype.aspx-0008", ex.Message.ToString());
            return;
        }
    }
    private void initpnl()
    {
        pnlAddServiceType.Visible = true;
    }
    public static string GetMimeDataOfFile(FileUpload files)
    {
        HttpPostedFile file = files.PostedFile;
        IntPtr mimeout;

        int MaxContent = (int)file.ContentLength;
        if (MaxContent > 4096) MaxContent = 4096;

        byte[] buf = new byte[MaxContent];
        file.InputStream.Read(buf, 0, MaxContent);
        int result = FindMimeFromData(IntPtr.Zero, file.FileName, buf, MaxContent, null, 0, out mimeout, 0);

        if (result != 0)
        {
            Marshal.FreeCoTaskMem(mimeout);
            return "";
        }

        string mime = Marshal.PtrToStringUni(mimeout);
        Marshal.FreeCoTaskMem(mimeout);

        return mime.ToLower();
    }
    private bool CheckFileExtention(FileUpload FileMobileApp)//M9
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
            _common.ErrorLog("Sysadmservicetype-M9", ex.Message.ToString());
            Errormsg("Something went wrong with image (allow only .jpg, .png, .jpeg image). ");
            return default(Boolean);
        }
    }
    private bool CheckFileExtentionWeb(FileUpload FileWebPortal)//M10
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
            _common.ErrorLog("Sysadmservicetype-M10", ex.Message.ToString());
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
    private void loadcharges()//M11
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_charges");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlSTOCCharge.DataSource = dt;
                    ddlSTOCCharge.DataTextField = "chargename";
                    ddlSTOCCharge.DataValueField = "chargeid";
                    ddlSTOCCharge.DataBind();
                }
            }
            ddlSTOCCharge.Items.Insert(0, "SELECT");
            ddlSTOCCharge.Items[0].Value = "0";
            ddlSTOCCharge.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("Sysadmservicetype.aspx-0009", ex.Message.ToString());
        }
    }
    private void loadChargesGrid()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_servicetypecharges");
            MyCommand.Parameters.AddWithValue("p_srtp_id", Convert.ToInt32(Session["ServiceTypeID"].ToString()));
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                pnlNoRecord_otherCharge.Visible = false;
                grdServiceTypeOtherCharge.DataSource = dt;
                grdServiceTypeOtherCharge.DataBind();
                grdServiceTypeOtherCharge.Visible = true;
            }
            else
            {
                pnlNoRecord_otherCharge.Visible = true;
                grdServiceTypeOtherCharge.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("Sysadmservicetype.aspx-0010", ex.Message.ToString());
        }
    }
    private void insertServiecCharges()
    {
        try
        {
            int stocid = 0;
            if (Session["Action"].ToString() == "CS")
            {
                Session["Action"] = "S";
            }
            if (Session["Action"].ToString() == "CU")
            {
                Session["Action"] = "U";
                stocid =Convert.ToInt32( Session["stocid"].ToString());
            }
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            string Mresult = "";
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_insert_st_charges");
            MyCommand.Parameters.AddWithValue("p_action", Session["Action"].ToString());
            MyCommand.Parameters.AddWithValue("p_stid", Convert.ToDecimal(Session["ServiceTypeID"].ToString()));
            MyCommand.Parameters.AddWithValue("p_chargeid", Convert.ToDecimal(ddlSTOCCharge.SelectedValue));
            MyCommand.Parameters.AddWithValue("p_stocid", stocid);
            MyCommand.Parameters.AddWithValue("p_faretype", ddlSTOCFareType.SelectedValue);
            MyCommand.Parameters.AddWithValue("p_from_km", Convert.ToDecimal(txtSTOCFromKM.Text.ToString()));
            MyCommand.Parameters.AddWithValue("p_to_km", Convert.ToDecimal(txtSTOCToKM.Text.ToString()));
            MyCommand.Parameters.AddWithValue("p_charge_amt", Convert.ToDecimal(txtSTOCChargeAmount.Text.ToString()));
            MyCommand.Parameters.AddWithValue("p_eff_date", txtSTOCEffectiveDate.Text);
            MyCommand.Parameters.AddWithValue("p_update_by", Session["_UserCode"].ToString());
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count>0)
            {
                if (dt.Rows[0]["f_insert_st_charges"].ToString() == "1234")
                {
                    Errormsg("Service Type Other Charge Already Exists");
                }
                else
                {
                    if (Session["Action"].ToString() == "S")
                    {
                        Successmsg("Service Type Charges Successfully Saved");
                        loadcharges();
                        resetControlcharges();
                        loadChargesGrid();
                        PanelAddOtherCharges.Visible = true;
                        lbtnSaveOtherCharges.Visible = true;
                        lbtnUpdateOtherCharges.Visible = false;
                    }
                   
                    if (Session["Action"].ToString() == "U")
                    {
                        Successmsg("Service Type Charges Successfully Updated");
                        loadcharges();
                        resetControlcharges();
                        loadChargesGrid();
                        PanelAddOtherCharges.Visible = true;
                        lbtnSaveOtherCharges.Visible = true;
                        lbtnUpdateOtherCharges.Visible = false;
                    }
                }
            }
            else
            {
                Errormsg("There is some error" + dt.TableName);
                _common.ErrorLog("Sysadmservicetype.aspx-0010", dt.TableName);
                //resetControl();
                return;
            }
        }
        catch (Exception ex)
        {
            Errormsg("There is some error" + ex.Message);
            _common.ErrorLog("Sysadmservicetype.aspx-0011", ex.Message.ToString());
        }
    }
    #endregion

    #region "Event" 

    protected void lbtnAddServicetype_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        pnlAddServiceType.Visible = true;
    }
    protected void lbtnSave_Click(object sender, System.EventArgs e)
    {
        CsrfTokenValidate();
        Session["ServiceTypeID"] = 0;
        Session["Action"] = "S";
        if (validvalue() == false)
        {
            return;
        }
        lblConfirmation.Text = "Do you want to Save Service Type ?";
        mpConfirmation.Show();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Session["Action"].ToString() == "S")
        {
            insertServiecType();
        }
        if (Session["Action"].ToString() == "U")
        {
            insertServiecType();
        }
        if (Session["Action"].ToString() == "A")
        {
            insertServiecType();
        }
        if (Session["Action"].ToString() == "D")
        {
            insertServiecType();
        }
        if (Session["Action"].ToString() == "CS")
        {
            insertServiecCharges();
        }
        if (Session["Action"].ToString() == "CU")
        {
            insertServiecCharges();
        }
    }

    

    private void resetControlcharges()
    {
        CsrfTokenValidate();
        ddlSTOCFareType.SelectedValue = "0";
        txtSTOCChargeAmount.Text = "";
        txtSTOCEffectiveDate.Text = "";
        txtSTOCFromKM.Text = "";
        txtSTOCToKM.Text = "";
    }

    protected void gvServiceType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        try
        {
            if (e.CommandName == "ActiveYN")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Session["ServiceTypeID"] = gvServiceType.DataKeys[row.RowIndex]["srtpid"];
                Session["Action"] = gvServiceType.DataKeys[row.RowIndex]["statusa"].ToString();
                lblConfirmation.Text = "Do you want to Change Service Type Status?";
                mpConfirmation.Show();
            }
            if (e.CommandName == "UpdateServiceType")
            {
                LabelHeader.Visible = false;
                LabelHeaderUpdate.Visible = true;
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                string servicetypeEn = gvServiceType.DataKeys[row.RowIndex]["servicetype_name_en"].ToString();
                string servicetypeHi = gvServiceType.DataKeys[row.RowIndex]["servicetype_name_hi"].ToString();
                string speedhill = gvServiceType.DataKeys[row.RowIndex]["speedhill_kmh"].ToString();
                string speedplain = gvServiceType.DataKeys[row.RowIndex]["speedplain_kmh"].ToString();
                string HeatingSurcharge = gvServiceType.DataKeys[row.RowIndex]["heatscharge_pkm"].ToString();
                string ACSurcharge = gvServiceType.DataKeys[row.RowIndex]["acscharge_pkm"].ToString();
                string onlineresrvation = gvServiceType.DataKeys[row.RowIndex]["onlinereservationchargea"].ToString();
                string sgst = gvServiceType.DataKeys[row.RowIndex]["s_gst"].ToString();
                string cgst = gvServiceType.DataKeys[row.RowIndex]["c_gst"].ToString();
                string igst = gvServiceType.DataKeys[row.RowIndex]["i_gst"].ToString();
                string agent_comm = gvServiceType.DataKeys[row.RowIndex]["ag_comm"].ToString();

                if (!String.IsNullOrEmpty(gvServiceType.DataKeys[row.RowIndex]["amenitiesa"].ToString()))
                {
                    string Items = gvServiceType.DataKeys[row.RowIndex]["amenitiesa"].ToString();
                    string[] TypeCode;
                    if (!String.IsNullOrEmpty(Items))
                    {
                        TypeCode = Items.Split(',');

                        foreach (ListItem item in ddlAmenities.Items)
                        {
                            foreach (string str in TypeCode)
                            {
                                if (item.Value == str)
                                    item.Selected = true;
                            }
                        }
                    }
                }
                else
                    foreach (ListItem item in ddlAmenities.Items)
                        item.Selected = false;


                string amenities = gvServiceType.DataKeys[row.RowIndex]["amenitiesa"].ToString();
                string serviceTax = gvServiceType.DataKeys[row.RowIndex]["servicetax"].ToString();
                string LuggageAllowed = gvServiceType.DataKeys[row.RowIndex]["luggwith_psngr"].ToString();
                string Description = gvServiceType.DataKeys[row.RowIndex]["des_cription"].ToString();

                Session["Web"] = gvServiceType.DataKeys[row.RowIndex]["img_web"];
                Byte[] imgbytes = (Byte[])Session["Web"];
                ImgWebPortal.ImageUrl = GetImage(imgbytes);
                ImgWebPortal.Visible = true;

                Session["Mobile"] = gvServiceType.DataKeys[row.RowIndex]["img_app"];
                Byte[] mobile = (Byte[])Session["Mobile"];
                imgMobileApp.ImageUrl = GetImage(mobile);
                imgMobileApp.Visible = true;
                if (LuggageAllowed == "Y")
                {
                    cbServiceTypeLuggageWithPassenger.Checked = true;
                    PanelLuggage.Visible = true;
                    string laguaggerate = gvServiceType.DataKeys[row.RowIndex]["luggagerate"].ToString();
                    string laguaggemulti = gvServiceType.DataKeys[row.RowIndex]["luggmul_unit"].ToString();
                    tbServiceTypeLuggageRate.Text = laguaggerate;
                    tbServiceTypeLuggageMultipleUnit.Text = laguaggemulti;
                }
                tbServiceTypeNameEn.Text = servicetypeEn;
                tbServiceTypeNameHn.Text = servicetypeHi;
                tbServiceTypeHeatingSurcharge.Text = HeatingSurcharge;
                tbServiceTypeACSurchargeperkm.Text = ACSurcharge;
                tbServiceTypeSpeedHill.Text = speedhill;
                tbServiceTypeSpeedPlain.Text = speedplain;
                tbServiceTypeservicetax.Text = serviceTax;
                tbDescription.Text = Description;
                tbSgst.Text = sgst;
                tbCGST.Text = cgst;
                tbigst.Text = igst;
                tbagentcommission.Text = agent_comm;

                Session["ServiceTypeID"] = gvServiceType.DataKeys[row.RowIndex]["srtpid"].ToString();
                pnlAddServiceType.Visible = true;
                LabelHeaderUpdate.Visible = true;
                lbtnUpdate.Visible = true;
                lbtnSave.Visible = false;
                lbtnReset.Visible = false;
                lbtncancel.Visible = true;
                lbtncloseWebImage.Visible = true;
                lbtncloseMobileImage.Visible = true;
                Session["Action"] = "U";
            }
            if (e.CommandName == "AddCharges")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Session["ServiceTypeID"] = gvServiceType.DataKeys[row.RowIndex]["srtpid"];
                Session["Action"] = gvServiceType.DataKeys[row.RowIndex]["statusa"].ToString();
                loadcharges();
                loadChargesGrid();
                pnlAddServiceType.Visible = false;
                PanelAddOtherCharges.Visible = true;
                resetControlcharges();
                lbtnSaveOtherCharges.Visible = true;
                lbtnUpdateOtherCharges.Visible = false;
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("Sysadmservicetype-E1", ex.Message.ToString());
            return;
        }
    }
    protected void lbtnUpdate_Click(object sender, System.EventArgs e)
    {
        CsrfTokenValidate();
        Session["Action"] = "U";
        lblConfirmation.Text = "Do you want to Update Service Type Details?";
        mpConfirmation.Show();
    }
    protected void lbtnview_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Errormsg("Here you can Add/Update Bus Service Type and its other details like charges and speed limit.<br/>Service Type Fare type wise details will be added here along with effective date.<br> We can discontinue service type if it's currently not in use.");

    }
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        ServiceTypeList(gvServiceType);
    }
    protected void lbtnResetU_Click(object sender, System.EventArgs e)
    {
        CsrfTokenValidate();
        resetControl();
    }
    protected void gvServiceType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvServiceType.PageIndex = e.NewPageIndex;
        ServiceTypeList(gvServiceType);
    }
    protected void chkServiceTypeLuggageWithPassenger_CheckedChanged(object sender, EventArgs e)
    {
        if (cbServiceTypeLuggageWithPassenger.Checked)
        {
            PanelLuggage.Visible = true;
            tbServiceTypeLuggageRate.Text = "";
            tbServiceTypeLuggageMultipleUnit.Text = "";
        }
        else
        {
            PanelLuggage.Visible = false;
            tbServiceTypeLuggageRate.Text = "0";
            tbServiceTypeLuggageMultipleUnit.Text = "0";
        }
    }
    protected void lbtnReset_Click(object sender, System.EventArgs e)
    {
        CsrfTokenValidate();
        resetControl();
    }
    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        resetControl();
        pnlAddServiceType.Visible = true;
    }
    protected void lbtndownloadReport_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Errormsg("Comming soon");
    }
    protected void lbtndownload_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        Errormsg("Comming soon");
    }
    protected void btnUploadWebPortal_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (!FileWebPortal.HasFile)
        {

            Errormsg("Please select report first");

            return;
        }
        string _fileFormat = GetMimeDataOfFile(FileWebPortal);
        if ((_fileFormat == "image/png") || (_fileFormat == "image/jpeg") || (_fileFormat == "image/jpg") || (_fileFormat == "image/pjpeg") || (_fileFormat == "image/x-png"))
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

        decimal size = Math.Round((Convert.ToDecimal(FileWebPortal.PostedFile.ContentLength) / Convert.ToDecimal(1024)), 2);
        if (size > 1024 || size < 10)
        {
            Errormsg("File size must be between 10 kb to 200 kb");

            return;
        }
        byte[] PhotoImage = null;
        PhotoImage = convertByteFile(FileWebPortal);

        ImgWebPortal.ImageUrl = GetImage(PhotoImage);
        ImgWebPortal.Visible = true;
        lbtncloseWebImage.Visible = true;
        Session["Web"] = FileWebPortal.FileBytes;
    }
    protected void btnUploadMobileApp_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (!FileMobileApp.HasFile)
        {

            Errormsg("Please select report first");
            return;
        }
        string _fileFormat = GetMimeDataOfFile(FileMobileApp);
        if ((_fileFormat == "image/png") || (_fileFormat == "image/jpeg") || (_fileFormat == "image/jpg") || (_fileFormat == "image/pjpeg") || (_fileFormat == "image/x-png"))
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
            Errormsg("File size must be between 10 kb to 200 kb");
            return;
        }
        byte[] PhotoImage = null;
        PhotoImage = convertByteFile(FileMobileApp);
        imgMobileApp.ImageUrl = GetImage(PhotoImage);
        imgMobileApp.Visible = true;
        lbtncloseMobileImage.Visible = true;
        Session["Mobile"] = FileMobileApp.FileBytes;
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
    protected void gvServiceType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnUpdate = (LinkButton)e.Row.FindControl("lbtnUpdateServiceType");
            LinkButton lbtnActiveDeactive = (LinkButton)e.Row.FindControl("lbtnActiveDeactive");
            LinkButton lnkbtnDecative = (LinkButton)e.Row.FindControl("lbtnDeactivate");
            Image img = (Image)e.Row.FindControl("img");

            byte[] imgBytes = (byte[])gvServiceType.DataKeys[e.Row.RowIndex]["img_app"];
            img.ImageUrl = GetImage(imgBytes);


            DataRowView rowView = (DataRowView)e.Row.DataItem;
            lbtnActiveDeactive.Visible = false;
            lnkbtnDecative.Visible = false;
            lbtnUpdate.Visible = true;
            if (rowView["statusa"].ToString() == "D")
            {
                lbtnActiveDeactive.Visible = true;
            }
            if (rowView["is_discontinue"].ToString() == "Y" && rowView["statusa"].ToString() != "D")
            { 
                lnkbtnDecative.Visible = true;
            }
            
           



        }

    }
    protected void LinkButtonPanelAddOtherChargesCancel_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        pnlAddServiceType.Visible = true;
        PanelAddOtherCharges.Visible = false;
    }
    protected void lbtnSaveOtherCharges_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        int msgcount = 0;
        string msg = "";
        if (ddlSTOCCharge.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select Valid Charge.<br/>";
        }
        if (ddlSTOCFareType.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select Valid Fare Type.<br/>";
        }

        if (_validation.isValideDecimalNumber(txtSTOCFromKM.Text, 1, 5) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid From Km.<br/>";
        }
        if (_validation.isValideDecimalNumber(txtSTOCToKM.Text, 1, 5) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid To Km.<br/>";
        }
        if (_validation.isValideDecimalNumber(txtSTOCChargeAmount.Text, 1, 5) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid Charge Amount.<br/>";
        }
        if (txtSTOCEffectiveDate.Text == "")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid Date.<br/>";
        }
        if (Session["ServiceTypeID"] == null)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Something Went Wrong.<br>";
        }
        if (msgcount > 0)
        {
            Errormsg(msg);
            return;
        }
        Session["Action"] = "CS";
        lblConfirmation.Text = "Do you want to Save Service Charges Details?";
        mpConfirmation.Show();
    }

    protected void lbtnUpdateOtherCharges_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        int msgcount = 0;
        string msg = "";
        if (ddlSTOCCharge.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select Valid Charge.<br/>";
        }
        if (ddlSTOCFareType.SelectedValue == "0")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Select Valid Fare Type.<br/>";
        }

        if (_validation.isValideDecimalNumber(txtSTOCFromKM.Text, 1, 5) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid From Km.<br/>";
        }
        if (_validation.isValideDecimalNumber(txtSTOCToKM.Text, 1, 5) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid To Km.<br/>";
        }
        if (_validation.isValideDecimalNumber(txtSTOCChargeAmount.Text, 1, 5) == false)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid Charge Amount.<br/>";
        }
        if (txtSTOCEffectiveDate.Text == "")
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Enter Valid Date.<br/>";
        }
        if (Session["ServiceTypeID"] == null)
        {
            msgcount = msgcount + 1;
            msg = msg + msgcount.ToString() + ". Something Went Wrong.<br>";
        }
        if (msgcount > 0)
        {
            Errormsg(msg);
            return;
        }
        Session["Action"] = "CU";
        lblConfirmation.Text = "Do you want to Update Service Charges Details?";
        mpConfirmation.Show();
    }

    protected void lbtnResetOtherCharges_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        resetControlcharges();
    }
    protected void grdServiceTypeOtherCharge_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "updatecharge")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Session["stocid"] = grdServiceTypeOtherCharge.DataKeys[row.RowIndex]["stocid"];
            ddlSTOCCharge.SelectedValue = grdServiceTypeOtherCharge.DataKeys[row.RowIndex]["chargeid"].ToString();
            ddlSTOCFareType.SelectedValue = grdServiceTypeOtherCharge.DataKeys[row.RowIndex]["faretype"].ToString();
            txtSTOCFromKM.Text = grdServiceTypeOtherCharge.DataKeys[row.RowIndex]["fromkm"].ToString();
            txtSTOCToKM.Text = grdServiceTypeOtherCharge.DataKeys[row.RowIndex]["tokm"].ToString();
            txtSTOCChargeAmount.Text = grdServiceTypeOtherCharge.DataKeys[row.RowIndex]["amt"].ToString();
            txtSTOCEffectiveDate.Text = Convert.ToDateTime(grdServiceTypeOtherCharge.DataKeys[row.RowIndex]["effdate"].ToString()).ToString("dd/MM/yyyy");
            lbtnUpdateOtherCharges.Visible = true;
            lbtnSaveOtherCharges.Visible = false;
        }
    }
    #endregion






}