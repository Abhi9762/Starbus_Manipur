using System;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Globalization;


public partial class NewBusPassRegistration : outsidebasepage
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
        if (!IsPostBack)
        {
            if (sbXMLdata.checkModuleCategory("71") == false)
            {
                Response.Redirect("Errorpage.aspx");
            }
            tbDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            Session["Heading"] = "Apply For New Bus Pass";
            Session["Heading"] = "Apply For New Bus Pass";

            Session["IDProof"] = null;
            Session["AddProof"] = null;
            Session["Photo"] = null;

            RefreshCaptcha();
            BusPassTypeList();
            LoadRoute();
            loadState(ddlState);
            loadStationFrom(ddlFrom, "0");
            loadStationFrom(ddlTo, "0");
        }
        else
        {
            if (Request.Form[tbDate.UniqueID] != null)
            {
                if (Request.Form[tbDate.UniqueID].Length > 0)
                {
                    tbDate.Text = Request.Form[tbDate.UniqueID];
                    //CalendarExtender1.SelectedDate = DateTime.Parse(Request.Form[tbDate.UniqueID]);
                }
            }
        }

    }

    #region "Methods"


    private void loadState(DropDownList ddlstate)//M3
    {
        try
        {
            ddlstate.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_states");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlstate.DataSource = dt;
                    ddlstate.DataTextField = "stname";
                    ddlstate.DataValueField = "stcode";
                    ddlstate.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("NewBusPassRegistration", dt.TableName);
            }
            ddlstate.Items.Insert(0, "SELECT");
            ddlstate.Items[0].Value = "0";
            ddlstate.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlstate.Items.Insert(0, "SELECT");
            ddlstate.Items[0].Value = "0";
            ddlstate.SelectedIndex = 0;
            _common.ErrorLog("NewBusPassRegistration", ex.Message.ToString());
        }
    }
    private void loadDistrict(string State_code, DropDownList ddldistrict)
    {
        try
        {
            ddldistrict.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_district");
            MyCommand.Parameters.AddWithValue("p_statecode", State_code);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddldistrict.DataSource = dt;
                    ddldistrict.DataTextField = "distname";
                    ddldistrict.DataValueField = "distcode";
                    ddldistrict.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("NewBusPassRegistration", dt.TableName);
            }

            ddldistrict.Items.Insert(0, "SELECT");
            ddldistrict.Items[0].Value = "0";
            ddldistrict.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddldistrict.Items.Insert(0, "SELECT");
            ddldistrict.Items[0].Value = "0";
            ddldistrict.SelectedIndex = 0;
            _common.ErrorLog("NewBusPassRegistration", ex.Message.ToString());
        }
    }
    public void loadCity()
    {
        try
        {
            ddlCity.Items.Clear();
            string State = ddlState.SelectedValue.ToString();
            string District = ddlDistrict.SelectedValue.ToString();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_city");
            MyCommand.Parameters.AddWithValue("p_statecode", State);
            MyCommand.Parameters.AddWithValue("p_distcode", District);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlCity.DataSource = dt;
                    ddlCity.DataTextField = "ctyname";
                    ddlCity.DataValueField = "ctycode";
                    ddlCity.DataBind();
                }
            }
            else
            {
                _common.ErrorLog("NewBusPassRegistration", dt.TableName);
            }
            ddlCity.Items.Insert(0, "SELECT");
            ddlCity.Items[0].Value = "0";
            ddlCity.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlCity.Items.Insert(0, "SELECT");
            ddlCity.Items[0].Value = "0";
            ddlCity.SelectedIndex = 0;
            _common.ErrorLog("NewBusPassRegistration", ex.Message.ToString());
        }
    }
    private void RefreshCaptcha()
    {
        tbcaptchacode.Text = "";
        Session["CaptchaImage"] = getRandom();
    }
    public string getRandom()
    {
        Random random = new Random();
        const string src = "0123456789";
        int i;
        string _random = "";
        for (i = 0; i <= 5; i++)
        {
            _random += src[random.Next(0, src.Length)];//random.Next(0, 9).ToString();
        }

        return _random;
    }
    private void BusPassTypeList()
    {
        try
        {
            ddlbuspassType.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "bus_pass_type_list_bustypeid");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {


                    ddlbuspassType.DataSource = dt;
                    ddlbuspassType.DataTextField = "buspasstypename";
                    ddlbuspassType.DataValueField = "buspasstypeid";
                    ddlbuspassType.DataBind();
                }
            }

            ddlbuspassType.Items.Insert(0, "Select");
            ddlbuspassType.Items[0].Value = "0";
            ddlbuspassType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlbuspassType.Items.Insert(0, "Select");
            ddlbuspassType.Items[0].Value = "0";
            ddlbuspassType.SelectedIndex = 0;
        }
    }
    private void ResetControl()
    {
        tbValidityFrom.Text = "";
        tbValidityFrom.Enabled = false;
        tbValidityTo.Text = "";
        pnlinstruction.Visible = false;
        tbName.Text = "";
        tbFatherName.Text = "";
        tbDate.Text = "";
        ddlGender.SelectedValue = "0";
        tbMobile.Text = "";
        tbEmail.Text = "";
        ddlRoute.SelectedValue = "0";
        ddlFrom.SelectedValue = "0";
        ddlTo.SelectedValue = "0";
        ddlState.SelectedValue = "0";
        ddlDistrict.SelectedValue = "0";
        ddlCity.SelectedValue = "0";
        tbAddress.Text = "";
        tbPincode.Text = "";
        rbtnAddressProof.SelectedValue = null;
        rbtnIdProof.SelectedValue = null;
        ddlbuspassType.SelectedValue = "0";
        tbAdharNumber.Text = "";
        tbConfirmAdharNumber.Text = "";
        hdAdharNumber.Value = "";
        hdConfirmAdharNumber.Value = "";
        Session["IDProof"] = null;
        Session["AddProof"] = null;
        Session["Photo"] = null;
        ddlGender.Enabled = true;
        pnlAddProof.Visible = false;
        pnlAddProofNew.Visible = false;
        pnlIDProofNew.Visible = false;
        pnlinstruction.Visible = false;
        pnlPhoto.Visible = false;
        pnlRoute.Visible = false;
        pnlDocument.Visible = false;
        ddlServiceType.SelectedIndex = 0;
        tbValidityFrom.Text = "";
        tbValidityTo.Text = "";
        RefreshCaptcha();
    }
    private void ResetControl1()
    {
        tbValidityFrom.Text = "";
        tbValidityFrom.Enabled = false;
        tbValidityTo.Text = "";
        pnlinstruction.Visible = false;
        tbName.Text = "";
        tbFatherName.Text = "";
        tbDate.Text = "";
        ddlGender.SelectedValue = "0";
        tbMobile.Text = "";
        tbEmail.Text = "";
        ddlRoute.SelectedValue = "0";
        ddlFrom.SelectedValue = "0";
        ddlTo.SelectedValue = "0";
        ddlState.SelectedValue = "0";
        ddlDistrict.SelectedValue = "0";
        ddlCity.SelectedValue = "0";
        tbAddress.Text = "";
        tbPincode.Text = "";
        rbtnAddressProof.SelectedValue = null;
        rbtnIdProof.SelectedValue = null;
        //ddlbuspassType.SelectedValue = "0";
        tbAdharNumber.Text = "";
        tbConfirmAdharNumber.Text = "";
        hdAdharNumber.Value = "";
        hdConfirmAdharNumber.Value = "";
        Session["IDProof"] = null;
        Session["AddProof"] = null;
        Session["Photo"] = null;
        ddlGender.Enabled = true;
        pnlAddProof.Visible = false;
        pnlAddProofNew.Visible = false;
        pnlIDProofNew.Visible = false;
        pnlinstruction.Visible = false;
        pnlPhoto.Visible = false;
        pnlRoute.Visible = false;
        pnlDocument.Visible = false;
        ddlServiceType.SelectedIndex = 0;
        tbValidityFrom.Text = "";
        tbValidityTo.Text = "";
        RefreshCaptcha();
    }
    private static DataTable GetBusServiceType(string flag, string applicableToBusServiceType)
    {
        string[] ss = applicableToBusServiceType.Split(',');
        DataTable table = new DataTable();
        table.Columns.Add("document_type_id", typeof(int));
        table.Columns.Add("document_type_name", typeof(string));

        for (int i = 0; i < ss.Length; i++)
        {

            DataTable dt = new DataTable();
            sbBLL bll = new sbBLL();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.f_get_bus_pass_details");
            MyCommand.Parameters.AddWithValue("@p_flag", flag);
            MyCommand.Parameters.AddWithValue("@p_code", ss[i]);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    table.Rows.Add(ss[i], dt.Rows[0]["name"]);
                }
            }
        }

        return table;
    }
    private static DataTable GetIDproof(string flag, string documentIDProofForNewTypes)
    {
        string[] ss = documentIDProofForNewTypes.Split(',');
        DataTable table = new DataTable();
        table.Columns.Add("document_type_id", typeof(int));
        table.Columns.Add("document_type_name", typeof(string));

        for (int i = 0; i < ss.Length; i++)
        {


            DataTable dt = new DataTable();
            sbBLL bll = new sbBLL();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.f_get_bus_pass_details");
            MyCommand.Parameters.AddWithValue("@p_flag", flag);
            MyCommand.Parameters.AddWithValue("@p_code", ss[i]);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    table.Rows.Add(ss[i], dt.Rows[0]["name"]);
                }
            }
        }

        return table;
    }
    private static DataTable GetAddProof(string flag, string documentAddressForNewTypes)
    {
        string[] ss = documentAddressForNewTypes.Split(',');
        DataTable table = new DataTable();
        table.Columns.Add("document_type_id", typeof(int));
        table.Columns.Add("document_type_name", typeof(string));

        for (int i = 0; i < ss.Length; i++)
        {
            DataTable dt = new DataTable();
            sbBLL bll = new sbBLL();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.f_get_bus_pass_details");
            MyCommand.Parameters.AddWithValue("@p_flag", flag);
            MyCommand.Parameters.AddWithValue("@p_code", ss[i]);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    table.Rows.Add(ss[i], dt.Rows[0]["name"]);
                }
            }
        }

        return table;
    }
    private void Errormsg(string msg)
    {
        lblerrormsg.Text = msg;
        mperror.Show();
        RefreshCaptcha();
    }
    private void SuccessMsg(string msg)
    {
        lblsuccessmsg.Text = msg;
        mpsuccess.Show();
    }
    private void ConfirmMsg(string msg)
    {
        lblConfirmation.Text = msg;
        mpConfirmation.Show();
    }
    private static DataTable GetApplicableCharges(string flag, string documentAddressForNewTypes)
    {
        string[] ss = documentAddressForNewTypes.Split(',');
        DataTable table = new DataTable();
        table.Columns.Add("document_type_id", typeof(int));
        table.Columns.Add("document_type_name", typeof(string));
        table.Columns.Add("ABBR", typeof(string));
        table.Columns.Add("Charges", typeof(string));

        for (int i = 0; i < ss.Length; i++)
        {
            DataTable dt = new DataTable();
            sbBLL bll = new sbBLL();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();

            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.f_get_bus_pass_details");
            MyCommand.Parameters.AddWithValue("@p_flag", flag);
            MyCommand.Parameters.AddWithValue("@p_code", ss[i]);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    table.Rows.Add(ss[i], dt.Rows[0]["name"], dt.Rows[0]["chargetypeabbr"], dt.Rows[0]["chargeamt"]);
                }
            }
        }

        return table;
    }
    private string getMimeDateNew(byte[] bytes)
    {
        IntPtr mimeout;
        int MaxContent = bytes.Length;
        if (MaxContent > 4096) MaxContent = 4096;
        
        byte[] buf = new byte[MaxContent];
        
        //bytes.

        //ile.InputStream.Read(buf, 0, MaxContent);
        //int result = FindMimeFromData(IntPtr.Zero, file.FileName, buf, MaxContent, null, 0, out mimeout, 0);

        return "";
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
    public byte[] convertByteFile(System.Web.UI.WebControls.FileUpload fuFileUpload)
    {
        int intFileLength;
        byte[] byteData = null;

        if (fuFileUpload.HasFile)
        {
            if (CheckFileExtension(fuFileUpload) == true)
            {
                intFileLength = fuFileUpload.PostedFile.ContentLength;
                byteData = new byte[intFileLength];
                byteData = fuFileUpload.FileBytes;
            }
        }
        else
        {
            intFileLength = fuFileUpload.PostedFile.ContentLength;
            byteData = new byte[intFileLength];
            byteData = fuFileUpload.FileBytes;
        }

        return byteData;
    }
    private bool CheckFileExtension(FileUpload fuFileUpload)
    {
        try
        {
            bool fileExtensionOK = false;
            string fileExtension = System.IO.Path.GetExtension(fuFileUpload.FileName).ToLower();
            string[] allowedExtensions = { ".jpg", ".png", ".jpeg" };

            for (int i = 0; i < allowedExtensions.Length; i++)
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
            Errormsg("Something went wrong with image (allow only .jpg, .png, .jpeg image).");
            return false; // or handle the exception as needed
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
    private bool Validation()
    {
        try
        {
            int count = 0;
            string msg = "";

            if (ddlbuspassType.SelectedValue == "0")
            {
                Errormsg("Select Bus Pass Type <br/>");
                return false;
            }

            if ((Session["CaptchaImage"] == null || tbcaptchacode.Text.ToLower() != Session["CaptchaImage"].ToString().ToLower()))
            {
                Errormsg("Invalid Security Code(Shown in Image). Please Try Again");
                return false;
            }


            if (ddlbuspassType.SelectedValue != "0")
            {
                MyCommand = new NpgsqlCommand();
                MyCommand.Parameters.Clear();
                MyCommand.Parameters.AddWithValue("@StoredProcedure", "bus_pass_load");
                MyCommand.Parameters.AddWithValue("@p_id", Convert.ToInt32(ddlbuspassType.SelectedValue));
                dt = bll.SelectAll(MyCommand);
                if (dt.TableName == "Success")
                {
                    if (dt.Rows.Count > 0)
                    {
                        string restrictedkmsyn = dt.Rows[0]["restrictedkmsyn"].ToString();
                        string journeyallowedinstateyn = dt.Rows[0]["allowedinstateyn"].ToString();
                        string journeyallowedoutsidestateyn = dt.Rows[0]["allowedoutsidestateyn"].ToString();
                        string advancefareyn = dt.Rows[0]["advance_fare_yn"].ToString();
                        string issuencetype = dt.Rows[0]["issuence_type"].ToString();
                        string dbt_yn = dt.Rows[0]["dbt_yn"].ToString();
                        string documentidprooffornewyn = dt.Rows[0]["docidnewyn"].ToString();
                        string documentaddressfornewyn = dt.Rows[0]["docaddnewyn"].ToString();
                        string photorequire_newyn = dt.Rows[0]["photonewyn"].ToString();

                        if (documentidprooffornewyn == "Y")
                        {
                            if (Session["IDProof"] == null)
                            {
                                count++;
                                msg += count + ". Upload ID Proof<br/>";
                            }
                            if (txtidproofno.Text.Length <= 3)
                            {
                                count++;
                                msg += count + ". Enter Uploaded ID Proof Document Number<br/>";
                            }
                        }

                        if (documentaddressfornewyn == "Y")
                        {
                            if (Session["AddProof"] == null)
                            {
                                count++;
                                msg += count + ". Upload Address Proof<br/>";
                            }
                            if (txtaddressproofno.Text.Length <= 3)
                            {
                                count++;
                                msg += count + ". Enter Uploaded Address Proof Document Number<br/>";
                            }
                        }
                        if (tbAdharNumber.Text == "")
                        {
                            count++;
                            msg += count + ". Enter valid Adhar Number<br/>";
                        }

                        if (tbConfirmAdharNumber.Text == "")
                        {
                            count++;
                            msg += count + ". Enter valid Adhar Confirmation Number<br/>";
                        }
                        if (hdAdharNumber.Value == "")
                        {
                            count++;
                            msg += count + ". Enter valid Adhar Number<br/>";
                        }

                        if (hdConfirmAdharNumber.Value == "")
                        {
                            count++;
                            msg += count + ". Enter valid Adhar Confirmation Number<br/>";
                        }

                        if (photorequire_newyn == "Y")
                        {
                            if (Session["Photo"] == null)
                            {
                                count++;
                                msg += count + ". Upload Photo<br/>";
                            }
                        }
                    }

                    if (!_validation.IsValidString(tbName.Text, 3, tbName.MaxLength))
                    {
                        count++;
                        msg += count + ". Enter Valid Name<br/>";
                    }

                    if (!_validation.IsValidString(tbFatherName.Text, 3, tbFatherName.MaxLength))
                    {
                        count++;
                        msg += count + ". Enter Valid father's Name<br/>";
                    }

                    if (tbDate.Text == "")
                    {
                        count++;
                        msg += count + ". Enter Valid Date of birth<br/>";
                    }

                    if (!_validation.IsValidString(tbMobile.Text, 10, tbMobile.MaxLength))
                    {
                        count++;
                        msg += count + ". Enter Valid Mobile Number <br/>";
                    }

                    if (!_validation.isValideMailID(tbEmail.Text))
                    {
                        count++;
                        msg += count + ". Enter Valid Mail ID Name<br/>";
                    }

                    if (ddlGender.SelectedValue == "0")
                    {
                        count++;
                        msg += count + ". Select Gender<br/>";
                    }

                    if (ddlState.SelectedValue == "0")
                    {
                        count++;
                        msg += count + ". Select State<br/>";
                    }

                    if (ddlDistrict.SelectedValue == "0")
                    {
                        count++;
                        msg += count + ". Select District<br/>";
                    }

                    if (ddlCity.SelectedValue == "0")
                    {
                        count++;
                        msg += count + ". Select City<br/>";
                    }

                    if (!_validation.IsValidString(tbAddress.Text, 4, tbAddress.MaxLength))
                    {
                        count++;
                        msg += count + ". Enter Valid Address <br/>";
                    }

                    if (!_validation.IsValidString(tbPincode.Text, 6, 6))
                    {
                        count++;
                        msg += count + ". Enter Valid Pincode <br/>";
                    }

                    if (count > 0)
                    {
                        RefreshCaptcha();
                        Errormsg(msg);
                        return false;
                    }

                    return true;
                }


            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }
    private void loadStationFrom(DropDownList ddlPlace, String mstatioNCode)
    {
        try
        {
            ddlPlace.Items.Clear();
            string MrouteID = "";

            if (ddlRoute.SelectedIndex > 0)
            {
                MrouteID = ddlRoute.SelectedValue;

                MyCommand = new NpgsqlCommand();
                MyCommand.Parameters.Clear();
                MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.utconline_stngetroutewise");
                MyCommand.Parameters.AddWithValue("@spstationcodeincluded", Convert.ToInt16(mstatioNCode));
                MyCommand.Parameters.AddWithValue("@sp_routeid", Convert.ToInt16(MrouteID));
                dt = bll.SelectAll(MyCommand);
                if (dt.TableName == "Success")
                {
                    if (dt.Rows.Count > 0)
                    {
                        ddlPlace.DataValueField = "stonid";
                        ddlPlace.DataTextField = "stationnameeng";
                        ddlPlace.DataSource = dt;
                        ddlPlace.DataBind();
                    }
                }
            }

            ddlPlace.Items.Insert(0, "Select");
            ddlPlace.Items[0].Value = "0";
            ddlPlace.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlPlace.Items.Clear();
            ddlPlace.Items.Insert(0, "Select");
            ddlPlace.Items[0].Value = "0";
            ddlPlace.SelectedIndex = 0;
        }
    }
    private int getStationstate(string stationcode)
    {
        try
        {
            int _TStatecode = 0;

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.pass_validstate");
            MyCommand.Parameters.AddWithValue("@p_stationcode", stationcode);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    _TStatecode = Convert.ToInt32(dt.Rows[0]["statecode"].ToString());
                }
            }

            return _TStatecode;
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
            return 0;
        }

    }
    public int validKm(string _routeID, string _frmStn, string _toStn)
    {
        try
        {
            int _TDistance = 0;

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.utconline_mst_stndistance");
            MyCommand.Parameters.AddWithValue("@p_routeid", Convert.ToInt32(_routeID));
            MyCommand.Parameters.AddWithValue("@p_frmstation", Convert.ToInt32(_frmStn));
            MyCommand.Parameters.AddWithValue("@p_tostation", Convert.ToInt32(_toStn));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    _TDistance = Convert.ToInt32(dt.Rows[0]["total_distkm"].ToString());
                    return _TDistance;
                }
            }

            return 0;
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            return 0;
        }
    }

    //public string Encrypt(string plainText)
    //{
    //    if (plainText == null)
    //        return "";
    //    string key = "adHKEY1234";
    //    var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
    //    var passwordBytes = Encoding.UTF8.GetBytes(key);
    //    passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
    //    var bytesEncrypted = Encrypt(bytesToBeEncrypted, passwordBytes);
    //    return Convert.ToBase64String(bytesEncrypted);
    //}

    //private byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
    //{
    //    byte[] encryptedBytes = null;
    //    var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

    //    using (MemoryStream ms = new MemoryStream())
    //    {
    //        using (RijndaelManaged AES = new RijndaelManaged())
    //        {
    //            var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
    //            AES.KeySize = 256;
    //            AES.BlockSize = 128;
    //            AES.Key = key.GetBytes(AES.KeySize / 8);
    //            AES.IV = key.GetBytes(AES.BlockSize / 8);
    //            AES.Mode = CipherMode.CBC;
    //            using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
    //            {
    //                cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
    //                cs.Close();
    //            }

    //            encryptedBytes = ms.ToArray();
    //        }
    //    }

    //    return encryptedBytes;
    //}

    public void LoadRoute()
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            ddlRoute.Items.Clear();
            DataTable dt;
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_routes");

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlRoute.DataValueField = "routeid";
                    ddlRoute.DataTextField = "routename";
                    ddlRoute.DataSource = dt;
                    ddlRoute.DataBind();
                }
            }
            ddlRoute.Items.Insert(0, "Select");
            ddlRoute.Items[0].Value = "0";
            ddlRoute.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlRoute.Items.Insert(0, "Select");
            ddlRoute.Items[0].Value = "0";
            ddlRoute.SelectedIndex = 0;
        }
    }
    protected void SaveNewPassRequest()
    {
        try
        {
            string name, fname, gender, mobile, mail, address, pincode, adharno = "", confirmadharno, Extracharges, charges, servicetype = "";
            decimal fare, TotalExtraCharges = 0, statecode, district, city;
            int fromstation, tostation, routeid, passtype, cateogry;
            DataTable abbrcharges;
            byte[] Idproof = (byte[])Session["IDProof"];
            byte[] AddProof = (byte[])Session["AddProof"];
            byte[] Photo = (byte[])Session["Photo"];
            passtype = Convert.ToInt32(ddlbuspassType.SelectedValue.ToString());
            name = tbName.Text;
            fare = Convert.ToDecimal(Session["fare"]);
            fname = tbFatherName.Text;
            charges = Session["buspasscharges_id"].ToString();
            cateogry = Convert.ToInt32(Session["BUSPASS_CATEGORY_ID"].ToString());
            if (ddlServiceType.SelectedValue != "0")
            {
                foreach (ListItem item in ddlServiceType.Items)
                {
                    if (item.Selected)
                        servicetype += item.Value + ",";
                }
                if (servicetype.Length > 1)
                    servicetype = servicetype.Substring(0, servicetype.Length - 1);
            }
            if (charges != "")
            {
                abbrcharges = GetApplicableCharges("3", charges);
                string[] ss;
                ss = charges.Split(',');
                string a = "";
                for (int i = 0; i <= ss.Length - 1; i++)
                {
                    Extracharges = GetApplicableCharges("3", charges).Rows[i]["Charges"].ToString();
                    TotalExtraCharges = Convert.ToDecimal(GetApplicableCharges("3", charges).Rows[i]["Charges"].ToString()) + TotalExtraCharges;
                }
            }

            DateTime? dob = null;
            if (!string.IsNullOrEmpty(tbDate.Text))
            {
                dob = DateTime.ParseExact(tbDate.Text.Trim(), "dd/MM/yyyy", null);
            }

            DateTime? validityFrom = null;
            if (!string.IsNullOrEmpty(tbValidityFrom.Text))
            {
                validityFrom = DateTime.ParseExact(tbValidityFrom.Text.Trim(), "dd/MM/yyyy", null);
            }

            DateTime? validityTo = null;
            if (!string.IsNullOrEmpty(tbValidityTo.Text))
            {
                validityTo = DateTime.ParseExact(tbValidityTo.Text.Trim(), "dd/MM/yyyy", null);
            }



            gender = ddlGender.SelectedValue;
            mobile = tbMobile.Text;
            mail = tbEmail.Text;
            routeid = Convert.ToInt32(ddlRoute.SelectedValue);
            address = tbAddress.Text;
            pincode = tbPincode.Text;
            //if (tbAdharNumber.Text != "")
            //    adharno = tbAdharNumber.Text;
            //if (tbConfirmAdharNumber.Text != "")
            //    confirmadharno = tbConfirmAdharNumber.Text;
            statecode = Convert.ToInt64(ddlState.SelectedValue);
            district = Convert.ToInt64(ddlDistrict.SelectedValue);
            city = Convert.ToInt64(ddlCity.SelectedValue);
            fromstation = Convert.ToInt32(ddlFrom.SelectedValue);
            tostation = Convert.ToInt32(ddlTo.SelectedValue);

            int idproofid;
            if (!(Session["IDProof"] == null || Session["IDProof"].ToString() == ""))
            {
                idproofid = Convert.ToInt16(rbtnIdProof.SelectedValue.ToString());
            }

            else
            {
                idproofid = 0;
            }

            int addproofid;

            if (!(Session["AddProof"] == null || Session["AddProof"].ToString() == ""))
            {
                addproofid = Convert.ToInt16(rbtnAddressProof.SelectedValue.ToString());
            }
            else
            {
                addproofid = 0;
            }
            string IDProofNo = txtidproofno.Text;
            string AddressProofNo = txtaddressproofno.Text;
            if (Session["tbConfirmAdharNumber"].ToString() != Session["tbAdharNumber"].ToString())
            {
                Errormsg("Aadhar No. Should Be Same");
            }
            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.bus_pass_new_request");
            MyCommand.Parameters.AddWithValue("p_pass_type", passtype);
            MyCommand.Parameters.AddWithValue("p_name", name);
            MyCommand.Parameters.AddWithValue("p_fname", fname);
            MyCommand.Parameters.AddWithValue("p_dob", tbDate.Text);
            MyCommand.Parameters.AddWithValue("p_gender", gender);
            MyCommand.Parameters.AddWithValue("p_mobile", mobile);
            MyCommand.Parameters.AddWithValue("p_mail", mail);
            MyCommand.Parameters.AddWithValue("p_routeid", routeid);
            MyCommand.Parameters.AddWithValue("p_fromstation", fromstation);
            MyCommand.Parameters.AddWithValue("p_tostation", tostation);
            MyCommand.Parameters.AddWithValue("p_statecode", statecode);
            MyCommand.Parameters.AddWithValue("p_district", district);
            MyCommand.Parameters.AddWithValue("p_city", city);
            MyCommand.Parameters.AddWithValue("p_address", address);
            MyCommand.Parameters.AddWithValue("p_pincode", pincode);
            MyCommand.Parameters.AddWithValue("p_adharno", Session["tbAdharNumber"].ToString());
            MyCommand.Parameters.AddWithValue("p_charges", charges);
            MyCommand.Parameters.AddWithValue("p_fare", fare);
            MyCommand.Parameters.AddWithValue("p_extraamount", TotalExtraCharges);
            MyCommand.Parameters.AddWithValue("p_cateogry", cateogry);
            MyCommand.Parameters.AddWithValue("p_servicetype", servicetype);
            MyCommand.Parameters.AddWithValue("p_periodfrom", tbValidityFrom.Text);
            MyCommand.Parameters.AddWithValue("p_validupto", tbValidityTo.Text);
            MyCommand.Parameters.AddWithValue("p_idproof", (object)Idproof ?? DBNull.Value);
            MyCommand.Parameters.AddWithValue("p_addproof", (object)AddProof ?? DBNull.Value);
            MyCommand.Parameters.AddWithValue("p_photo", (object)Photo ?? DBNull.Value);
            MyCommand.Parameters.AddWithValue("p_idproofid", idproofid);
            MyCommand.Parameters.AddWithValue("p_addproofid", addproofid);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("p_applytype", "T");
            MyCommand.Parameters.AddWithValue("p_applytypeby", mobile);
            MyCommand.Parameters.AddWithValue("p_aadharmask", Session["AadharMasking"]);
            MyCommand.Parameters.AddWithValue("p_idproofno", IDProofNo);
            MyCommand.Parameters.AddWithValue("p_addressproofno", AddressProofNo);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0  && dt.Rows[0]["sp_curr_tran_ref"].ToString()!= "EXCEPTION")
                {
                    Session["_RNDIDENTIFIERMSTAUTH"] = _validation.GeneratePassword(10, 5);
                    Session["_otp"] = getRandom();
                    //comm.sendOTP_SMS(Session["_otp"].ToString(), mobile, 1);
                    Session["currtranrefno"] = dt.Rows[0]["sp_curr_tran_ref"];
                    Session["PageStep1"] = 1;
                    Response.Redirect("ConfirmBusPassRequest.aspx");
                    ResetControl();
                }
            }
            else
            {
                Errormsg("Your Pass Request Not Proceed Rigth Now Please Try After Some Time");

            }

        }
        catch (Exception ex)
        {
            Errormsg("Your Pass Request Not Proceed Rigth Now Please Try After Some Time" + ex.Message);

        }
    }
    private bool FileContainsMaliciousCode(byte[] bytes)
    {
        string fileContent = "";

        using (MemoryStream stream = new MemoryStream(bytes))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                fileContent = reader.ReadToEnd();
            }
        }
        string[] maliciousPatterns = { "<script>", "javascript:", "javascript", "eval(", "src=", "onmouseover=", "<iframe>", "<object>", "<embed>", "<form>", "<svg>", "XSS" };
        foreach (string pattern in maliciousPatterns)
        {
            if (fileContent.ToUpper().Contains(pattern.ToUpper()))
            {
                return true;
            }
        }
        return false;
    }

    #endregion

    #region "Events"
    protected void ddlbuspassType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ResetControl1();
        if (ddlbuspassType.SelectedValue == "0")
        {
            pnlinstruction.Visible = false;
            lbtnInfo.Visible = false;
            ResetControl();
        }
        else
        {
            pnlinstruction.Visible = true;
            lbtnInfo.Visible = true;
        }

        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "bus_pass_load");
            MyCommand.Parameters.AddWithValue("@p_id", Convert.ToInt32(ddlbuspassType.SelectedValue));
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {

                if (dt.Rows.Count > 0)
                {

                    string GenderYN = dt.Rows[0]["restrictedtogenderyn"].ToString();
                    string restrictedtogendertype = dt.Rows[0]["restricetdtogendertype"].ToString();
                    string restrictedtoageyn = dt.Rows[0]["restrictedtoageyn"].ToString();
                    string Age_Max = dt.Rows[0]["restrictedagemax"].ToString();
                    string Age_Min = dt.Rows[0]["restrictedagemin"].ToString();
                    string restrictedkmsyn = dt.Rows[0]["restrictedkmsyn"].ToString();
                    string journeyallowedinstateyn = dt.Rows[0]["allowedinstateyn"].ToString();
                    string journeyallowedoutsidestateyn = dt.Rows[0]["allowedoutsidestateyn"].ToString();
                    string advancefareyn = dt.Rows[0]["advance_fare_yn"].ToString();
                    string issuencetype = dt.Rows[0]["issuence_type"].ToString();
                    string dbt_yn = dt.Rows[0]["dbt_yn"].ToString();
                    string documentidprooffornewyn = dt.Rows[0]["docidnewyn"].ToString();
                    string documentidprooffornewtypes = dt.Rows[0]["docidnewtypes"].ToString();
                    string documentaddressfornewtypes = dt.Rows[0]["docaddnewtypes"].ToString();
                    string documentaddressfornewyn = dt.Rows[0]["docaddnewyn"].ToString();
                    string photorequire_newyn = dt.Rows[0]["photonewyn"].ToString();
                    string buspasscharges_id = dt.Rows[0]["buspasschargesid"].ToString();
                    string validityinday = dt.Rows[0]["validity_in_days"].ToString();
                    string validityMonth = dt.Rows[0]["validity_month"].ToString();
                    string validityUpto = dt.Rows[0]["pass_validity"].ToString();

                    string applicabletobusservicetype = dt.Rows[0]["tobusservicetypes"].ToString();
                    string applicabletobusservicetypesyn = dt.Rows[0]["busservicetypesyn"].ToString();
                    string BUSPASS_CATEGORY_ID = dt.Rows[0]["buspasscategoryid"].ToString();
                    string NOOFDAYS = dt.Rows[0]["no_of_days"].ToString();
                    Session["VALIDITYINDAYS"] = validityinday;
                    Session["ValidFor"] = journeyallowedinstateyn;

                    if (restrictedkmsyn == "Y")
                    {
                        Session["AllowDtnc"] = dt.Rows[0]["restrictednoofkms"].ToString();
                    }
                    else
                    {
                        Session["AllowDtnc"] = "0";
                    }

                    //CalendarExtender2.Enabled = true;

                    if (issuencetype == "I")
                    {
                        tbValidityFrom.Text = DateTime.Now.Date.AddDays(1).ToString("dd/MM/yyyy");
                        //CalendarExtender2.StartDate = DateTime.Now.AddDays(1);
                        // CalendarExtender2.EndDate = DateTime.Now.AddDays(1);
                        // CalendarExtender2.Enabled = false;

                        if (validityMonth == "0")
                        {
                            if (validityinday != "0")
                            {
                                tbValidityTo.Text = DateTime.Now.Date.AddDays(Convert.ToInt32(Session["VALIDITYINDAYS"].ToString()) + 1).ToString("dd/MM/yyyy");
                            }
                        }
                        else
                        {
                            int validityMonthnumber = Convert.ToInt32(validityMonth);
                            int currentMonthnumber = Convert.ToInt32(DateTime.Now.Date.ToString("MM"));
                            string ValidYear = DateTime.Now.ToString("yyyy");

                            if (currentMonthnumber > validityMonthnumber)
                            {
                                ValidYear = DateTime.Now.AddYears(1).ToString("yyyy");
                            }

                            int lastday = DateTime.DaysInMonth(Convert.ToInt32(ValidYear), validityMonthnumber);
                            string upto = lastday + "/" + validityMonth + "/" + ValidYear;
                            tbValidityTo.Text = upto;
                        }

                        tbValidityFrom.Enabled = false;
                        tbValidityTo.Enabled = false;
                    }
                    else if (issuencetype == "A")
                    {
                        //CalendarExtender2.StartDate = DateTime.Now.AddDays(8);
                        //CalendarExtender2.EndDate = DateTime.Now.AddDays(38);
                        //CalendarExtender2.Enabled = true;
                        tbValidityFrom.Text = DateTime.Now.Date.AddDays(8).ToString("dd/MM/yyyy");

                        if (validityMonth == "0")
                        {
                            if (validityinday != "0")
                            {
                                tbValidityTo.Text = DateTime.Now.Date.AddDays(8 + Convert.ToInt32(Session["VALIDITYINDAYS"].ToString()) + 1).ToString("dd/MM/yyyy");
                            }
                        }
                        else
                        {
                            int validityMonthnumber = Convert.ToInt32(validityMonth);
                            int currentMonthnumber = Convert.ToInt32(DateTime.Now.Date.ToString("MM"));
                            string ValidYear = DateTime.Now.Date.ToString("yyyy");

                            if (currentMonthnumber > validityMonthnumber)
                            {
                                ValidYear = DateTime.Now.AddYears(1).Date.ToString("yyyy");
                            }

                            int lastday = DateTime.DaysInMonth(Convert.ToInt32(ValidYear), validityMonthnumber);
                            string upto = lastday + "/" + validityMonth + "/" + ValidYear;
                            tbValidityTo.Text = upto;
                        }

                        tbValidityFrom.Enabled = true;
                    }

                    Session["buspasscharges_id"] = buspasscharges_id;
                    Session["BUSPASS_CATEGORY_ID"] = BUSPASS_CATEGORY_ID;

                    if (applicabletobusservicetypesyn != "N")
                    {
                        Session["servicetype"] = applicabletobusservicetype;
                    }

                    lblEligibility.Text = "<b>Eligibility of Pass </b><br/> " + dt.Rows[0]["buspasscategoryname"].ToString();
                    lblEligibilityInfo.Text = "<b>Eligibility of Pass  </b><br/>" + dt.Rows[0]["buspasscategoryname"].ToString();

                    if (restrictedtoageyn == "Y")
                    {
                        if (Convert.ToInt32(Age_Min) == 0)
                        {
                            Age_Min = "18";
                        }

                        if (Convert.ToInt32(Age_Max) == 0)
                        {
                            Age_Max = "150";
                        }

                        //CalendarExtender1. = DateTime.Now.Date.AddYears(-Convert.ToInt32(Age_Max));
                        //CalendarExtender1.SelectedDate = DateTime.Now.AddYears(-Convert.ToInt32(Age_Max));
                        //CalendarExtender1.EndDate = DateTime.Now.Date.AddYears(-Convert.ToInt32(Age_Min));
                    }
                    else
                    {
                        //CalendarExtender1.EndDate = DateTime.Now.AddYears(-18);
                        //CalendarExtender1.SelectedDate = DateTime.Now.AddYears(-18);
                        //CalendarExtender1.StartDate = DateTime.Now.AddYears(-150);
                    }

                    if (journeyallowedinstateyn == "Y")
                    {
                        lblState.Text = "<br><b>Pass Allowed </b><br/>within State Only";
                        lblStateInfo.Text = "<br><b>Pass Allowed </b><br/> within State Only";
                    }
                    else if (journeyallowedoutsidestateyn == "Y")
                    {
                        lblState.Text = "<br><b>Pass Allowed</b><br/> Outside State Only";
                    }
                    else if (journeyallowedinstateyn == "Y" && journeyallowedoutsidestateyn == "Y")
                    {
                        lblState.Text = "<br><b>Pass Allowed </b><br/>within State as well as outside state";
                    }

                    if (validityMonth == "0")
                    {
                        if (validityinday != "0")
                        {
                            lblvalidity.Text = "<br><b> Validity </b><br/> " + validityinday + " Days";
                            lblvalidityInfo.Text = "<br> <b>Validity </b><br/> " + validityinday + " Days";
                        }
                    }
                    else
                    {
                        lblvalidity.Text = "<br><b> Validity </b><br/> " + validityUpto;
                        lblvalidityInfo.Text = "<br> <b>Validity </b><br/> " + validityUpto;
                    }

                    ddlServiceType.Items.Clear();

                    if (applicabletobusservicetypesyn == "N")
                    {
                        ddlServiceType.Items.Insert(0, "All");
                        ddlServiceType.Items[0].Value = "0";
                        ddlServiceType.SelectedIndex = 0;
                    }

                    if (applicabletobusservicetypesyn == "Y")
                    {
                        if (!string.IsNullOrEmpty(applicabletobusservicetype))
                        {
                            GetBusServiceType("4", applicabletobusservicetype);
                            string[] ss = applicabletobusservicetype.Split(',');
                            string idProof = "";
                            string a = "";

                            for (int i = 0; i < ss.Length; i++)
                            {
                                idProof = GetBusServiceType("4", applicabletobusservicetype).Rows[i]["document_type_name"].ToString();

                                if (!string.IsNullOrEmpty(a))
                                {
                                    a = a + "," + idProof;
                                }
                                else
                                {
                                    a = idProof;
                                }

                                lblservicetype.Text = "<br><b> Applicable Bus Service Type </b><br/> " + a;
                                lblservicetypeInfo.Text = "<br><b> Applicable Bus Service Type </b><br/> " + a;
                                ddlServiceType.DataSource = GetBusServiceType("4", applicabletobusservicetype);
                                ddlServiceType.DataTextField = "document_type_name";
                                ddlServiceType.DataValueField = "document_type_id";
                                ddlServiceType.DataBind();
                            }

                            if (applicabletobusservicetype.Length < 1)
                            {
                                ddlServiceType.Items.Insert(0, "Select");
                                ddlServiceType.Items[0].Value = "0";
                                ddlServiceType.SelectedIndex = 0;
                            }
                        }
                    }

                    if (documentidprooffornewyn == "Y")
                    {
                        pnlIDProofNew.Visible = true;

                        if (!string.IsNullOrEmpty(documentidprooffornewtypes))
                        {
                            GetIDproof("1", documentidprooffornewtypes);
                            string[] ss = documentidprooffornewtypes.Split(',');
                            string idProof = "";
                            string a = "";

                            for (int i = 0; i < ss.Length; i++)
                            {
                                idProof = GetIDproof("1", documentidprooffornewtypes).Rows[i]["document_type_name"].ToString();

                                if (!string.IsNullOrEmpty(a))
                                {
                                    a = a + "," + idProof;
                                }
                                else
                                {
                                    a = idProof;
                                }

                                lblIDDocuments.Text = "<br><b> Applicable Id Proof Document </b><br/> " + a;
                                lblIDDocumentsInfo.Text = "<br><b> Applicable Id Proof Document </b><br/> " + a;
                                rbtnIdProof.DataSource = GetIDproof("1", documentidprooffornewtypes);
                                rbtnIdProof.DataTextField = "document_type_name";
                                rbtnIdProof.DataValueField = "document_type_id";
                                rbtnIdProof.DataBind();
                            }
                        }
                    }
                    else
                    {
                        pnlIDProofNew.Visible = false;
                    }

                    if (documentaddressfornewyn == "Y")
                    {
                        pnlAddProofNew.Visible = true;

                        if (!string.IsNullOrEmpty(documentaddressfornewtypes))
                        {
                            GetAddProof("2", documentaddressfornewtypes);
                            string[] ss = documentaddressfornewtypes.Split(',');
                            string addProof = "";
                            string a = "";

                            for (int i = 0; i < ss.Length; i++)
                            {
                                addProof = GetAddProof("2", documentaddressfornewtypes).Rows[i]["document_type_name"].ToString();

                                if (!string.IsNullOrEmpty(a))
                                {
                                    a = a + "," + addProof;
                                }
                                else
                                {
                                    a = addProof;
                                }

                                lbladdDocuments.Text = "<br><b> Applicable Address Proof Document </b><br/> " + a;
                                lbladdDocumentsInfo.Text = "<br><b> Applicable Address Proof Document </b><br/> " + a;

                                rbtnAddressProof.DataSource = GetAddProof("2", documentaddressfornewtypes);
                                rbtnAddressProof.DataTextField = "document_type_name";
                                rbtnAddressProof.DataValueField = "document_type_id";
                                rbtnAddressProof.DataBind();
                            }
                        }
                        else
                        {
                            pnlAddProofNew.Visible = false;
                        }

                        // Other code here...

                        if (!string.IsNullOrEmpty(buspasscharges_id))
                        {
                            GetApplicableCharges("3", buspasscharges_id);
                            string[] ss = buspasscharges_id.Split(',');

                            string a = "";

                            if (ss.Length > 0)
                            {
                                foreach (string chargeId in ss)
                                {
                                    string charge = GetApplicableCharges("3", chargeId).Rows[0]["document_type_name"].ToString();

                                    if (!string.IsNullOrEmpty(a))
                                    {
                                        a += "," + charge;
                                    }
                                    else
                                    {
                                        a = charge;
                                    }
                                }

                                lblChargesApplicable.Text = "<br><b>  Charge Applicable </b><br/> " + a;
                                lblChargesApplicableInfo.Text = "<br> <b> Charge Applicable  </b><br/> " + a;
                            }
                        }

                        if (restrictedtogendertype == "F")
                        {
                            ddlGender.SelectedValue = "F";
                            ddlGender.Enabled = false;
                        }
                        else
                        {
                            ddlGender.SelectedValue = null;
                            ddlGender.Enabled = true;
                        }



                        if (journeyallowedinstateyn == "Y" && restrictedkmsyn == "Y" && advancefareyn == "Y")
                        {
                            pnlRoute.Visible = true;
                        }
                        else
                        {
                            pnlRoute.Visible = false;
                        }



                        if (documentidprooffornewyn == "N" && documentaddressfornewyn == "N")
                        {
                            pnlDocument.Visible = false;
                        }
                        else
                        {
                            pnlDocument.Visible = true;
                        }



                        if (photorequire_newyn == "Y")
                        {
                            pnlPhoto.Visible = true;
                        }
                        else
                        {
                            pnlPhoto.Visible = false;
                        }
                    }
                    else if (ddlbuspassType.SelectedValue == "0")
                    {
                        ResetControl();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
        }

    }
    protected void lbtnInfo_Click(object sender, EventArgs e)
    {
        lblAbout.Text = "Abount" + ddlbuspassType.SelectedItem.ToString() + " Pass Type";
        mpInfo.Show();
    }
    protected void lbtnRefresh_Click(object sender, EventArgs e)
    {
        RefreshCaptcha();
    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        if (Validation() == false)
        {
            return;
        }

        RefreshCaptcha();
        Session["Action"] = "S";
        ConfirmMsg("Do You Want To Proceed For New Bus Pass request ?");


    }
    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        ResetControl();
    }
    protected void lbtncloseWebImage_Click(object sender, EventArgs e)
    {
        Session["Photo"] = null;
        ImgWebPortal.Visible = false;
        lbtncloseWebImage.Visible = false;
    }
    protected void btnUploadWebPortal_Click(object sender, EventArgs e)
    {
        if (!FileWebPortal.HasFile)
        {
            Errormsg("Please select Web Image first");
            return;
        }

        string _fileFormat = GetMimeDataOfFile(FileWebPortal);

        if (_fileFormat == "image/png" || _fileFormat == "image/jpg" || _fileFormat == "image/jpeg" || _fileFormat == "image/x-png" || _fileFormat == "image/pjpeg")
        {
            if (!CheckFileExtension(FileWebPortal))
            {
                Errormsg("File must be of type PNG/jpg/jpeg");
                return;
            }

            decimal size = Math.Round((Convert.ToDecimal(FileWebPortal.PostedFile.ContentLength) / Convert.ToDecimal(1024)), 2);

            if (size > 1024 || size < 10)
            {
                Errormsg("File size must be between 10 kb to 1 Mb");
                return;
            }

            byte[] PhotoImage = convertByteFile(FileWebPortal);
            ImgWebPortal.ImageUrl = GetImage(PhotoImage);
            ImgWebPortal.Visible = true;
            lbtncloseWebImage.Visible = true;
            Session["Photo"] = FileWebPortal.FileBytes;
            Session["webcount"] = "W";
        }

        else
        {
            Errormsg("File must be of type PNG");
            return;
        }



    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadDistrict(ddlState.SelectedValue, ddlDistrict);
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCity();
    }
    protected void btnUploadDoc_Click(object sender, EventArgs e)
    {
        if (fileUpload.HasFile)
        { 
            Session["IDProof"] = null;
            Byte[] bytes= fileUpload.FileBytes;
            if (FileContainsMaliciousCode(bytes) == true)
            {
                Errormsg("Invalid File. Please upload a different file.");
                return;
            }
            string _fileFormat = GetMimeDataOfFile(fileUpload);
            string _NewFileName = "";
            if (_fileFormat == "application/pdf")
            {
                _NewFileName += ".pdf";
            }
            else
            {
                Errormsg("Please select Pdf file only.");
                return;
            }

            int cnt = fileUpload.PostedFile.FileName.Split('.').Length - 1;

            if (cnt > 1)
            {
                Errormsg("Please select Pdf file only.");
                return;
            }

            int length = fileUpload.PostedFile.ContentLength/1024;

            if (length == 0)
            {
                Errormsg("Document has not been uploaded.");
                return;
            }

            if (length > 1024)
            {
                Errormsg("File size should be less than 1MB.");
                return;
            }
            if (fileUpload.FileName.Length <= 50)
            {
                Session["IDProof"] = fileUpload.FileBytes;
                lblIDpdf.Text = fileUpload.FileName;
                Session["pdfName"] = fileUpload.FileName;
                lblIDpdf.Visible = true;
            }
            else
            {
                Errormsg("File Name Should be less than 50 Char.");
                return;
            }
            Session["IDProof"] = fileUpload.FileBytes;
        }

    }

    protected void btnAddproof_Click(object sender, EventArgs e)
    {
        
        if (fileaddproof.HasFile)
        {
            
            Session["AddProof"] = null;

            Byte[] bytes = fileaddproof.FileBytes;
            if (FileContainsMaliciousCode(bytes) == true)
            {
                Errormsg("Invalid File. Please upload a different file.");
                return;
            }

            string _fileFormat = GetMimeDataOfFile(fileaddproof);
            string _NewFileName = "";
            if (_fileFormat == "application/pdf")
            {
                _NewFileName += ".pdf";
            }
            else
            {
                Errormsg("Please select Pdf file only.");
                return;
            }
           

            

            int cnt = fileaddproof.PostedFile.FileName.Split('.').Length - 1;

            if (cnt > 1)
            {
                Errormsg("Please select Pdf file only.");
                return;
            }

            int length = fileaddproof.PostedFile.ContentLength/1024;

            if (length == 0)
            {
                Errormsg("Document has not been uploaded.");
                return;
            }

            if (length > 1024)
            {
                Errormsg("File size should be less than 1MB.");
                return;
            }
            if (fileaddproof.FileName.Length <= 50)
            {
                Session["AddProof"] = fileaddproof.FileBytes;
                lbladd.Text = fileaddproof.FileName;
                Session["pdfName"] = fileaddproof.FileName;
                lbladd.Visible = true;
            }
            else
            {
                Errormsg("File Name Should be less than 50 Char.");
                return;
            }
            Session["AddProof"] = fileaddproof.FileBytes;
        }

    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        SaveNewPassRequest();
    }
    protected void rbtnAddressProof_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnAddressProof.SelectedValue != null)
        {
            pnlAddProof.Visible = true;
        }
        else
        {
            pnlAddProof.Visible = false;
        }

    }
    protected void rbtnIdProof_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnIdProof.SelectedValue != null)
        {
            pnlIdProof.Visible = true;
        }
        else
        {
            pnlIdProof.Visible = false;
        }

    }
    protected void tbConfirmAdharNumber_TextChanged(object sender, EventArgs e)
    {
        try
        {
         string sAdhaar = hdAdharNumber.Value.ToString();
            string CAdhaar = hdConfirmAdharNumber.Value.ToString();

           
                if (sAdhaar != CAdhaar)
                {
                    Errormsg(" Aadhar number and confirm Aadhar number should be same. Please Valid Enter Valid Adhaar Number<br/>");
                    tbConfirmAdharNumber.Text = "";
                    tbAdharNumber.Text = "";
                    hdAdharNumber.Value = "";
                    hdConfirmAdharNumber.Value = "";
                return;
                }
                
            
            
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.checkrecordaadhar");
            MyCommand.Parameters.AddWithValue("@p_aadhar",  hdAdharNumber.Value);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["chkaadhr"]) > 0)
                    {
                        Errormsg("Bus pass already Apply/Issued with this Aadhar Number ");
                        hdAdharNumber.Value = "";
                        hdConfirmAdharNumber.Value = "";
                        tbAdharNumber.Text = "";
                        tbConfirmAdharNumber.Text = "";
                        return;
                    }
                }
            } 

            string st = hdAdharNumber.Value;
            Session["AadharMasking"] = st.Substring(8, 4);

            if (hdAdharNumber.Value != "")
            {
               
                Session["tbAdharNumber"] = hdAdharNumber.Value;
                hdAdharNumber.Value = "XXXXXXXXXXXX";
            }
            if (hdConfirmAdharNumber.Value != "")
            {
               
                Session["tbConfirmAdharNumber"] =hdConfirmAdharNumber.Value;
                hdConfirmAdharNumber.Value = "XXXXXXXXXXXX";
            }
        }
        catch (Exception ex)
        {
            Errormsg("Bus pass already Apply/Issued with this Aadhar Number " + ex.Message);
            hdAdharNumber.Value = "";
            hdConfirmAdharNumber.Value = "";
            tbConfirmAdharNumber.Text = "";
            tbAdharNumber.Text = "";
        }

    }
    protected void tbValidityFrom_TextChanged(object sender, EventArgs e)
    {
        int validity = (int)Session["VALIDITYINDAYS"];
        tbValidityTo.Text = Convert.ToDateTime(tbValidityFrom.Text).AddDays(validity).ToString();

    }
    protected void ddlTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int statecode = getStationstate(ddlTo.SelectedValue);

            if (Session["ValidFor"].ToString() == null || Session["ValidFor"].ToString() == "")
            {
                Errormsg("Pass Validation Not Available please Contact UTC Pass Admin");
                loadStationFrom(ddlTo, ddlFrom.SelectedValue);
                return;
            }
            else
            {
                if (Session["ValidFor"].ToString() == "Y")
                {
                    if (statecode != 5)
                    {
                        Errormsg("Pass Valid Only for Uttarakhand State please Select Uttarakhand stations");
                        loadStationFrom(ddlTo, ddlFrom.SelectedValue);
                        return;
                    }
                }
                else if (Session["ValidFor"].ToString() == "N")
                {
                    if (statecode == 5)
                    {
                        Errormsg("Pass Valid for other than Uttarakhand State please Select other stations");
                        loadStationFrom(ddlTo, ddlFrom.SelectedValue);
                        return;
                    }
                }
            }

            int _km = validKm(ddlRoute.SelectedValue, ddlFrom.SelectedValue, ddlTo.SelectedValue);
            int allowedkm = 0;

            if (!(Session["AllowDtnc"].ToString() == null || Session["AllowDtnc"].ToString() == ""))
            {
                allowedkm = Convert.ToInt32(Session["AllowDtnc"].ToString().Trim());

                if (allowedkm > 0)
                {
                    if (_km > allowedkm)
                    {
                        Errormsg("Please select different stations as the distance between selected stations is greater than " + allowedkm.ToString().Trim());
                        loadStationFrom(ddlTo, ddlFrom.SelectedValue);
                        return;
                    }
                }
            }
            else
            {
                Errormsg("Allow Km distance station's pass is not available.");
                loadStationFrom(ddlTo, ddlFrom.SelectedValue);
                return;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

    }

    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadStationFrom(ddlFrom, "0");
    }

    protected void ddlFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadStationFrom(ddlTo, ddlFrom.SelectedValue);
        int statecode = getStationstate(ddlFrom.SelectedValue);

    }
}

#endregion