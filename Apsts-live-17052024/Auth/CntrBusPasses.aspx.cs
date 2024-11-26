using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


public partial class Auth_CntrBusPasses : System.Web.UI.Page
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    private sbCommonFunc _common = new sbCommonFunc();
    sbSecurity _security = new sbSecurity();
    sbLoaderNdPopup _popup = new sbLoaderNdPopup();
    ReportDocument cryRpt = new ReportDocument();

    string Pass_report1 = System.Web.HttpContext.Current.Server.MapPath("~/Auth/Reports/rptmstdetail.rpt");
    string Pass_Details = System.Web.HttpContext.Current.Server.MapPath("~/Auth/Reports/Pass_Transaction_Details.rpt");

    [System.Runtime.InteropServices.DllImport("urlmon.dll")]
    public static extern int FindMimeFromData(IntPtr pBC, [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeParamIndex = 3)] byte[] pBuffer, int cbSize, [MarshalAs(UnmanagedType.LPWStr)] string pwzMimeProposed, int dwMimeFlags, [MarshalAs(UnmanagedType.U4)] ref int ppwzMimeOut, int dwReserved);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["_moduleName"] = "Bus Pass";
            tbapplydate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            LoadSearchPass();
            BusPassTypeList();
            LoadRoute();
            LoadState();
            loadStationFrom(ddlFrom, 0);
            loadStationFrom(ddlTo, 0);
            // --------------Pass Query
            CategoriesTypeList();
            BusPassTypeListrpt();
            txtfrmdate.Text = DateTime.Now.Date.AddDays(-15).ToString("dd/MM/yyyy");
            txttodate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

        }
        else if (Request[tbDate.UniqueID] != null)
        {
            if (Request[tbDate.UniqueID].Length > 0)
            {
                tbDate.Text = Request[tbDate.UniqueID];

            }
        }
    }


    #region "Methods"
    private void clearsession()
    {
        Session["IDProof"] = null;
        Session["AddProof"] = null;
        Session["Photo"] = null;
    }
    private void setPassHeaderButtonCSS(Button btn)
    {
        btnPassNew.CssClass = "btn btn-default btn-sm btn-borderradius3";
        btnPassRenew.CssClass = "btn btn-default btn-sm btn-borderradius3";
        btnPassQuery.CssClass = "btn btn-default btn-sm btn-borderradius3";
        btn.CssClass = "btn btn-success btn-sm btn-borderradius3";
    }
    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Please Note", msg, "Close");
        Response.Write(popup);
    }
    private void SuccessMsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Please Note", msg, "Close");
        Response.Write(popup);
    }
    private void ConfirmMsg(string msg)
    {
        lblConfirmation.Text = msg;
        mpConfirmation.Show();
    }
    private void LoadSearchPass()
    {
        try
        {
            DataTable dt = dtPass();
            if (dt.Rows.Count > 0)
            {
                grdsearchpass.DataSource = dt;
                grdsearchpass.DataBind();
                grdsearchpass.Visible = true;
                dvsearchpass.Visible = false;
                lbtnsearchDownload.Visible = true;
            }
            else
            {
                grdsearchpass.Visible = false;
                dvsearchpass.Visible = true;
                lbtnsearchDownload.Visible = false;
            }
        }
        catch (Exception ex)
        {
            return;
        }
    }
    private DataTable dtPass()
    {
        try
        {
            string applydate = tbapplydate.Text;
            string Issuancetype = ddlIssuanceType.SelectedValue;
            string refno = "";
            string value = "";
            DataTable dt;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.get_search_pass");
            MyCommand.Parameters.AddWithValue("p_applydate", applydate);
            MyCommand.Parameters.AddWithValue("p_issuancetype", Issuancetype);
            MyCommand.Parameters.AddWithValue("p_passtype", "0");
            MyCommand.Parameters.AddWithValue("p_currntref", "");
            dt = bll.SelectAll(MyCommand);
            return dt;
        }
        catch (Exception ex)
        {
            DataTable dt = new DataTable();
            return dt;
        }
    }
    private void BusPassTypeList()
    {
        try
        {
            ddlbuspassType.Items.Clear();
            DataTable dt;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.f_get_bus_pass_type_list_bustypeid");
            MyCommand.Parameters.AddWithValue("p_bustypeid", 0);

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
    private static DataTable GetBusServiceType(string flag, string applicabletobusservicetype)
    {
        string[] ss;
        ss = applicabletobusservicetype.Split(',');
        DataTable table = new DataTable();
        table.Columns.Add("document_type_id", typeof(int));
        table.Columns.Add("document_type_name", typeof(string));


        for (int i = 0; i <= ss.Length - 1; i++)
        {
            sbBLL bll = new sbBLL();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            DataTable dt = new DataTable();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.f_get_bus_pass_details");
            MyCommand.Parameters.AddWithValue("p_flag", flag);
            MyCommand.Parameters.AddWithValue("p_code", ss[i]);
            dt = bll.SelectAll(MyCommand);

            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                    table.Rows.Add(ss[i], dt.Rows[0]["name"]);
            }
        }

        return table;
    }
    private static DataTable GetIDproof(string flag, string documentidprooffornewtypes)
    {
        string[] ss;
        ss = documentidprooffornewtypes.Split(',');
        DataTable table = new DataTable();
        table.Columns.Add("document_type_id", typeof(int));
        table.Columns.Add("document_type_name", typeof(string));

        for (int i = 0; i <= ss.Length - 1; i++)
        {
            sbBLL bll = new sbBLL();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            DataTable dt = new DataTable();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.f_get_bus_pass_details");
            MyCommand.Parameters.AddWithValue("p_flag", flag);
            MyCommand.Parameters.AddWithValue("p_code", ss[i]);
            dt = bll.SelectAll(MyCommand);

            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                    table.Rows.Add(ss[i], dt.Rows[0]["name"]);
            }
        }

        return table;
    }
    private static DataTable GetAddproof(string flag, string documentAddWithValueressfornewtypes)
    {
        string[] ss;
        ss = documentAddWithValueressfornewtypes.Split(',');
        DataTable table = new DataTable();
        table.Columns.Add("document_type_id", typeof(int));
        table.Columns.Add("document_type_name", typeof(string));


        for (int i = 0; i <= ss.Length - 1; i++)
        {
            sbBLL bll = new sbBLL();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            DataTable dt = new DataTable();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.f_get_bus_pass_details");
            MyCommand.Parameters.AddWithValue("p_flag", flag);
            MyCommand.Parameters.AddWithValue("p_code", ss[i]);

            dt = bll.SelectAll(MyCommand);

            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                    table.Rows.Add(ss[i], dt.Rows[0]["name"]);
            }
        }

        return table;
    }
    private static DataTable GetApplicableCharges(string flag, string documentAddWithValueressfornewtypes)
    {
        string[] ss;
        ss = documentAddWithValueressfornewtypes.Split(',');
        DataTable table = new DataTable();
        table.Columns.Add("document_type_id", typeof(int));
        table.Columns.Add("document_type_name", typeof(string));
        table.Columns.Add("ABBR", typeof(string));
        table.Columns.Add("Charges", typeof(string));


        for (int i = 0; i <= ss.Length - 1; i++)
        {
            sbBLL bll = new sbBLL();
            DataTable dt = new DataTable();
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.f_get_bus_pass_details_charges");
            MyCommand.Parameters.AddWithValue("p_flag", flag);
            MyCommand.Parameters.AddWithValue("p_code", ss[i]);
            dt = bll.SelectAll(MyCommand);

            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                    table.Rows.Add(ss[i], dt.Rows[0]["name"], dt.Rows[0]["abbr"], dt.Rows[0]["chargeamt"]);
            }
        }

        return table;
    }
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
    public void loadStationFrom(DropDownList ddlPlace, decimal mstatioNCode)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            ddlPlace.Items.Clear();
            string MrouteID = "";
            if (ddlRoute.SelectedIndex > 0)
            {
                MrouteID = ddlRoute.SelectedValue;
                DataTable dt;
                MyCommand.Parameters.Clear();
                MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.utconline_stngetroutewise");
                MyCommand.Parameters.AddWithValue("spstationcodeincluded", Convert.ToInt64(mstatioNCode));
                MyCommand.Parameters.AddWithValue("sp_routeid", Convert.ToInt64(MrouteID));

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
    protected int getStationstate(string stationcode)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            int _TStatecode = 0;
            DataTable dt;
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.pass_validstate");
            MyCommand.Parameters.AddWithValue("p_stationcode", stationcode);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                    _TStatecode = Convert.ToInt32(dt.Rows[0]["statecode"].ToString());
            }
            return _TStatecode;
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
            return 0;
        }
    }
    public int validKm(int _routeID, int _frmStn, int _toStn)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            int _TDistance;
            DataTable dt;
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.utconline_mst_stndistance");
            MyCommand.Parameters.AddWithValue("p_routeid", _routeID);
            MyCommand.Parameters.AddWithValue("p_frmstation", _frmStn);
            MyCommand.Parameters.AddWithValue("p_tostation", _toStn);

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
            return 0;
        }
    }
    protected void LoadState()
    {
        try
        {

            ddlState.Items.Clear();
            MyCommand = new NpgsqlCommand();
            DataTable dt;
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_states");

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlState.DataSource = dt;
                    ddlState.DataTextField = "stname";
                    ddlState.DataValueField = "stcode";
                    ddlState.DataBind();
                }
            }

            ddlState.Items.Insert(0, "Select");
            ddlState.Items[0].Value = "0";
            ddlState.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlState.Items.Insert(0, "Select");
            ddlState.Items[0].Value = "0";
            ddlState.SelectedIndex = 0;
        }
    }
    protected void LoadDistrict()
    {
        try
        {

            ddlDistrict.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_district");
            MyCommand.Parameters.AddWithValue("p_statecode", ddlState.SelectedValue);

            dt = bll.SelectAll(MyCommand);

            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlDistrict.DataSource = dt;
                    ddlDistrict.DataTextField = "distname";
                    ddlDistrict.DataValueField = "distcode";
                    ddlDistrict.DataBind();
                }
            }

            ddlDistrict.Items.Insert(0, "Select");
            ddlDistrict.Items[0].Value = "0";
            ddlDistrict.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlDistrict.Items.Insert(0, "Select");
            ddlDistrict.Items[0].Value = "0";
            ddlDistrict.SelectedIndex = 0;
        }
    }
    protected void LoadCity()
    {
        try
        {
            ddlCity.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_get_city");
            MyCommand.Parameters.AddWithValue("p_statecode", ddlState.SelectedValue);
            MyCommand.Parameters.AddWithValue("p_distcode", ddlDistrict.SelectedValue);

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

            ddlCity.Items.Insert(0, "Select");
            ddlCity.Items[0].Value = "0";
            ddlCity.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlCity.Items.Insert(0, "Select");
            ddlCity.Items[0].Value = "0";
            ddlCity.SelectedIndex = 0;
        }
    }
    public string Encrypt(string plainText)
    {
        if (plainText == null)
            return "";
        string key = "adHKEY1234";
        var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
        var passwordBytes = Encoding.UTF8.GetBytes(key);
        passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
        var bytesEncrypted = Encrypt(bytesToBeEncrypted, passwordBytes);
        return Convert.ToBase64String(bytesEncrypted);
    }
    private byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
    {
        byte[] encryptedBytes = null;
        var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        using (MemoryStream ms = new MemoryStream())
        {
            using (RijndaelManaged AES = new RijndaelManaged())
            {
                var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                AES.KeySize = 256;
                AES.BlockSize = 128;
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);
                AES.Mode = CipherMode.CBC;
                using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                    cs.Close();
                }

                encryptedBytes = ms.ToArray();
            }
        }

        return encryptedBytes;
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
    private void resetcontrol()
    {
        tbValidityFrom.Text = "";
        tbValidityFrom.Enabled = false;
        tbValidityTo.Text = "";
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
        Session["IDProof"] = null;
        Session["AddProof"] = null;
        Session["Photo"] = null;
        ddlGender.Enabled = true;
        pnlAddProof.Visible = false;
        pnlAddProofNew.Visible = false;
        //pnlDBT.Visible = false;
        pnlIDProofNew.Visible = false;
        pnlPhoto.Visible = false;
        pnlRoute.Visible = false;
        pnlDocument.Visible = false;
        ddlServiceType.SelectedIndex = 0;
        tbValidityFrom.Text = "";
        tbValidityTo.Text = "";
    }
    protected bool Validaion()
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
            else if (ddlbuspassType.SelectedValue != "0")
            {
                DataTable dt;
                NpgsqlCommand MyCommand = new NpgsqlCommand();
                MyCommand.Parameters.Clear();
                MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.f_get_bus_pass_type_list_bustypeid");
                MyCommand.Parameters.AddWithValue("p_bustypeid", ddlbuspassType.SelectedValue);

                dt = bll.SelectAll(MyCommand);

                if (dt.Rows.Count > 0)
                {
                    string restrictedkmsyn = dt.Rows[0]["restrictedkmsyn"].ToString();
                    string journeyallowedinstateyn = dt.Rows[0]["allowedinstateyn"].ToString();
                    string journeyallowedoutsidestateyn = dt.Rows[0]["allowedoutsidestateyn"].ToString();
                    string advancefareyn = dt.Rows[0]["advancefareyn"].ToString();
                    string issuencetype = dt.Rows[0]["issuence_type"].ToString();
                    string dbt_yn = dt.Rows[0]["dbt_yn"].ToString();
                    string documentidprooffornewyn = dt.Rows[0]["docid_new_yn"].ToString();
                    string documentaddressfornewyn = dt.Rows[0]["doc_add_newyn"].ToString();
                    string photorequire_newyn = dt.Rows[0]["photonew_yn"].ToString();

                    if (documentidprooffornewyn == "Y")
                    {
                        if (Session["IDProof"] == null)
                        {
                            count += 1;
                            msg = msg + count + ". Upload ID Proof<br/>";
                        }
                    }

                    if (documentaddressfornewyn == "Y")
                    {
                        if (Session["AddProof"] == null)
                        {
                            count += 1;
                            msg = msg + count + ". Upload Address Proof<br/>";
                        }
                    }
                    if (photorequire_newyn == "Y")
                    {

                        if (Session["Photo"] == null & Session["Photo"].ToString() == "")
                        {
                            Errormsg("Please Capture Photograph");
                            return false;
                        }

                    }



                    if (dbt_yn == "Y")
                    {
                        if (tbAdharNumber.Text == "")
                        {
                            count += 1;
                            msg = msg + count + ". Enter valid Adhar Number<br/>";
                        }

                        if (tbConfirmAdharNumber.Text == "")
                        {
                            count += 1;
                            msg = msg + count + ". Enter valid Adhar Confirmation Number<br/>";
                        }
                    }

                    if (photorequire_newyn == "Y")
                    {
                        if (Session["Photo"] == null)
                        {
                            count += 1;
                            msg = msg + count + ". Upload Photo<br/>";
                        }
                    }
                }

                if (_validation.IsValidString(tbName.Text, 3, tbName.MaxLength) == false)
                {
                    count += 1;
                    msg = msg + count + ". Enter Valid Name<br/>";
                }

                if (_validation.IsValidString(tbFatherName.Text, 3, tbFatherName.MaxLength) == false)
                {
                    count += 1;
                    msg = msg + count + ". Enter Valid father's Name<br/>";
                }
                if (string.IsNullOrEmpty(tbDate.Text))
                {
                    count++;
                    msg += count + ". Enter Valid Date of birth<br/>";
                }
                else
                {
                    DateTime dateOfBirth;
                    if (DateTime.TryParse(tbDate.Text, out dateOfBirth))
                    {

                        DateTime minAllowedBirthDate = DateTime.Today.AddYears(-18);


                        if (dateOfBirth > minAllowedBirthDate)
                        {

                            count++;
                            msg += count + ". Enter a valid Date of birth (should be at least 18 years ago)<br/>";
                        }

                    }
                    //else
                    //{

                    //    count++;
                    //    msg += count + ". Enter a valid Date of birth<br/>";
                    //}
                }


                if (_validation.IsValidString(tbMobile.Text, 10, tbMobile.MaxLength) == false)
                {
                    count += 1;
                    msg = msg + count + ". Enter Valid Mobile Number <br/>";
                }

                if (_validation.isValideMailID(tbEmail.Text) == false)
                {
                    count += 1;
                    msg = msg + count + ". Enter Valid Mail ID Name<br/>";
                }

                if (ddlGender.SelectedValue == "0")
                {
                    count += 1;
                    msg = msg + count + ". Select Gender<br/>";
                }

                if (ddlState.SelectedValue == "0")
                {
                    count += 1;
                    msg = msg + count + ". Select State<br/>";
                }

                if (ddlDistrict.SelectedValue == "0")
                {
                    count += 1;
                    msg = msg + count + ". Select District<br/>";
                }

                if (ddlCity.SelectedValue == "0")
                {
                    count += 1;
                    msg = msg + count + ". Select City<br/>";
                }

                if (_validation.IsValidString(tbAddress.Text, 4, tbAddress.MaxLength) == false)
                {
                    count += 1;
                    msg = msg + count + ". Enter Valid Address <br/>";
                }

                if (_validation.IsValidString(tbPincode.Text, 6, 6) == false)
                {
                    count += 1;
                    msg = msg + count + ". Enter Valid Pincode <br/>";
                }

                if (count > 0)
                {
                    Errormsg(msg);
                    return false;
                }

                return true;
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
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
            if (!string.IsNullOrEmpty(tbAdharNumber.Text))
            {
                adharno = Encrypt(tbAdharNumber.Text);
            }
            if (!string.IsNullOrEmpty(tbConfirmAdharNumber.Text))
            {
                confirmadharno = Encrypt(tbConfirmAdharNumber.Text);
            }
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
            MyCommand.Parameters.AddWithValue("p_adharno", adharno);
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
            MyCommand.Parameters.AddWithValue("p_applytype", "C");
            MyCommand.Parameters.AddWithValue("p_applytypeby", Session["_UserCntrID"]);
            MyCommand.Parameters.AddWithValue("p_aadharmask", Session["AadharMasking"].ToString());
            MyCommand.Parameters.AddWithValue("p_idproofno", IDProofNo);
            MyCommand.Parameters.AddWithValue("p_addressproofno", AddressProofNo);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    Session["_RNDIDENTIFIERMSTAUTH"] = _validation.GeneratePassword(10, 5);
                    Session["currtranrefno"] = dt.Rows[0]["sp_curr_tran_ref"];
                    Response.Redirect("CntrBusPassesConfirmation.aspx");
                    resetcontrol();
                }
            }
            else
            {
                Errormsg(dt.TableName);

            }

        }
        catch (Exception ex)
        {
            Errormsg("Your Pass Request Not Proceed Rigth Now Please Try After Some Time" + ex.Message);

        }
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

        return "123456";//_random;
    }
    private void ResetControl()
    {
        tbValidityFrom.Text = "";
        tbValidityFrom.Enabled = false;
        tbValidityTo.Text = "";
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
        Session["IDProof"] = null;
        Session["AddProof"] = null;
        Session["Photo"] = null;
        ddlGender.Enabled = true;
        pnlAddProof.Visible = false;
        pnlAddProofNew.Visible = false;
        pnlIDProofNew.Visible = false;
        pnlPhoto.Visible = false;
        pnlRoute.Visible = false;
        pnlDocument.Visible = false;
        ddlServiceType.SelectedIndex = 0;
        tbValidityFrom.Text = "";
        tbValidityTo.Text = "";
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
    public void openSubDetailsWindow(string MModuleName)
    {
        try
        {
            string murl;
            string fullURL;
            murl = MModuleName + "?rt=" + DateTime.Now.ToString();
            fullURL = "window.open('" + murl + "', '_blank', 'height=900,width=830,status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";

            if ((Request.Browser.Type.ToString().Substring(0, 2).ToUpper() == "IE"))
            {

                Response.Write("<SCRIPT language='javascript'>window.showModalDialog('" + murl + "','name','dialogWidth:830px;dialogHeight:550px');</script>");
            }
            else
            {

                string script = "window.open('" + fullURL + "','')";
                if ((ClientScript.IsClientScriptBlockRegistered("NewWindow") == false))
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "NewWindow", fullURL, true); // 
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void resetcontrolRenewTab()
    {
        pnlPassDetails.Visible = false;
        pnlPassNorecord.Visible = true;
        Session["PhotoRenew"] = null;
    }
    private bool validvalue()
    {
        try
        {
            string msg = "";
            int msgcount = 0;

            if (tbPassnNo.Text.Length <= 0)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Pass Number.";
            }
            else if (_validation.IsValidString(tbPassnNo.Text, 6, 20) == false)
            {
                msgcount = msgcount + 1;
                msg = msg + msgcount.ToString() + ". Enter Valid Pass Number.";
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
            Errormsg(ex.Message);
            return false;
        }
    }
    private void loadPassDetails()
    {
        try
        {
            pnlPassDetails.Visible = false;
            pnlPassNorecord.Visible = false;

            string PassNo, PHOTO_RENEW_YN, doc_add_renew_types, DOC_ID_RENEW_TYPES;
            PassNo = tbPassnNo.Text;
            DataTable dt;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.getrenewpassdetailscntr");
            MyCommand.Parameters.AddWithValue("p_passno", PassNo);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    if (dt.Rows[0]["renewyn"].ToString() == "N")
                    {
                        Errormsg("Your Bus Pass - " + dt.Rows[0]["passenger_type"].ToString() + " can't be renewed.");
                        lblmsg.Text = "Your Bus Pass - " + dt.Rows[0]["passenger_type"].ToString() + " can't be renewed.";
                        pnlPassDetails.Visible = false;
                        pnlPassNorecord.Visible = true;
                        return;
                    }

                    DateTime dt_PeriodTo;
                    if (DateTime.TryParseExact(dt.Rows[0]["periodto"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt_PeriodTo))
                    {
                        string ssd = (dt_PeriodTo - DateTime.Now.Date).TotalDays.ToString();
                        int p_days = Convert.ToInt32(dt.Rows[0]["noofdays"]);

                        if (Convert.ToInt32(ssd) > p_days)
                        {
                            Errormsg("Your Bus Pass - " + dt.Rows[0]["passenger_type"].ToString() + " can be renewed only before " + p_days.ToString() + " Days");
                            lblmsg.Text = "Your Bus Pass - " + dt.Rows[0]["passenger_type"].ToString() + " can be renewed only before " + p_days.ToString() + " Days";
                            pnlPassDetails.Visible = false;
                            pnlPassNorecord.Visible = true;
                            return;
                        }
                        if (dt_PeriodTo < DateTime.Now)
                        {
                            Errormsg("Your Bus Pass - " + dt.Rows[0]["passenger_type"].ToString() + "  has  been expired you can't apply for renewal pass now.");
                            lblmsg.Text = "Your Bus Pass - " + dt.Rows[0]["passenger_type"].ToString() + "  has  been expired you can't apply for renewal pass now.";
                            pnlPassDetails.Visible = false;
                            pnlPassNorecord.Visible = true;
                            return;
                        }

                        lblBusPassType.Text = dt.Rows[0]["cardtypename"] + "(" + dt.Rows[0]["passenger_type"] + ")";
                        lblName.Text = dt.Rows[0]["ctzname"].ToString();
                        lblGender.Text = dt.Rows[0]["gender_"].ToString();
                        lblDOB.Text = dt.Rows[0]["dob"].ToString();
                        lblfname.Text = dt.Rows[0]["f_name"].ToString();
                        if (string.IsNullOrEmpty(dt.Rows[0]["route_name"].ToString()) == true)
                        {
                            lblRoute.Text = "All Routes";
                        }
                        else
                        {
                            lblRoute.Text = dt.Rows[0]["route_name"].ToString();
                        }
                        if (string.IsNullOrEmpty(dt.Rows[0]["startstationname"].ToString()) == true)
                        {
                            lblFrom.Text = "All Statoins";
                        }
                        else
                        {
                            lblFrom.Text = dt.Rows[0]["startstationname"].ToString() + " - " + dt.Rows[0]["endstationname"].ToString();
                        }
                        pnlRoute.Visible = true;
                        Session["Mobileno"] = dt.Rows[0]["mobileno"];
                        lblMobileNo.Text = dt.Rows[0]["mobileno"].ToString();
                        lblEmail.Text = dt.Rows[0]["emailid"].ToString();
                        lblstateName.Text = dt.Rows[0]["statename"].ToString();
                        lblDistrict.Text = dt.Rows[0]["DISTRICTNAME"].ToString();
                        lblCity.Text = dt.Rows[0]["CITY"].ToString();
                        if (string.IsNullOrEmpty(dt.Rows[0]["ADDRESS"].ToString()) == false)
                        {
                            lblAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
                        }
                        else
                        {
                            lblAddress.Text = "N/A";
                        }
                        if (string.IsNullOrEmpty(dt.Rows[0]["ADDRESS"].ToString()) == false)
                        {
                            lblPincode.Text = dt.Rows[0]["PINCODE"].ToString();
                        }
                        else
                        {
                            lblPincode.Text = "N/A";
                        }
                        lblValidityFrom.Text = dt.Rows[0]["periodform"].ToString();
                        lblValidTo.Text = dt.Rows[0]["periodto"].ToString();
                        if (string.IsNullOrEmpty(dt.Rows[0]["service_type_name"].ToString()) == false)
                        {
                            lblServiceTypeName.Text = dt.Rows[0]["service_type_name"].ToString();
                        }

                        else
                        {
                            lblServiceTypeName.Text = "All Services";
                        }

                        if (dt.Rows[0]["doc_id_renew_yn"].ToString() == "N" & dt.Rows[0]["doc_add_renew_yn"].ToString() == "N")
                        {
                            pnlDocument.Visible = false;
                        }

                        else
                        {
                            pnlDocument.Visible = true;
                        }


                        if (dt.Rows[0]["doc_id_renew_yn"].ToString() == "Y")
                        {
                            Session["Renew_doc_ID_YN"] = "Y";
                            DOC_ID_RENEW_TYPES = dt.Rows[0]["doc_id_renew_types"].ToString();
                            pnlIDProofNew.Visible = true;
                            if (DOC_ID_RENEW_TYPES != null)
                            {
                                GetIDproof("1", DOC_ID_RENEW_TYPES);
                                string[] ss;
                                ss = DOC_ID_RENEW_TYPES.Split(',');
                                string idProof;
                                string a = "";
                                for (int i = 0; i <= ss.Length - 1; i++)
                                {
                                    idProof = GetIDproof("1", DOC_ID_RENEW_TYPES).Rows[i]["document_type_name"].ToString();

                                    if (a != "")
                                    {
                                        a = a + "," + idProof;
                                    }

                                    else
                                    {
                                        a = idProof;
                                    }


                                    rbtnIdProofRenew.DataSource = GetIDproof("1", DOC_ID_RENEW_TYPES);
                                    rbtnIdProofRenew.DataTextField = "document_type_name";
                                    rbtnIdProofRenew.DataValueField = "document_type_id";
                                    rbtnIdProofRenew.DataBind();
                                }
                            }
                        }
                        else
                        {
                            pnlIDProofNew.Visible = false;
                        }


                        if (dt.Rows[0]["doc_add_renew_yn"].ToString() == "Y")
                        {
                            Session["Renew_doc_Add_YN"] = "Y";
                            doc_add_renew_types = dt.Rows[0]["doc_add_renew_types"].ToString();
                            pnlAddProofNew.Visible = true;
                            if (doc_add_renew_types != null)
                            {
                                GetAddproof("2", doc_add_renew_types);
                                string[] ss;
                                ss = doc_add_renew_types.Split(',');
                                string addProof;
                                string a = "";

                                for (int i = 0; i <= ss.Length - 1; i++)
                                {
                                    addProof = GetAddproof("2", doc_add_renew_types).Rows[i]["document_type_name"].ToString();

                                    if (a != "")
                                    {
                                        a = a + "," + addProof;
                                    }

                                    else
                                    {
                                        a = addProof;
                                    }


                                    rbtnAddressProofRenew.DataSource = GetAddproof("2", doc_add_renew_types);
                                    rbtnAddressProofRenew.DataTextField = "document_type_name";
                                    rbtnAddressProofRenew.DataValueField = "document_type_id";
                                    rbtnAddressProofRenew.DataBind();
                                }
                            }
                        }
                        else
                        {
                            pnlAddProofReNew.Visible = false;
                        }

                        PHOTO_RENEW_YN = dt.Rows[0]["PHOTO_RENEW_YN"].ToString();
                        Session["Renew_Photo_YN"] = PHOTO_RENEW_YN;
                        if (PHOTO_RENEW_YN == "Y")
                        {
                            pnlPhoto1.Visible = true;
                        }

                        else
                        {
                            pnlPhoto1.Visible = false;

                        }
                        pnlPassDetails.Visible = true;
                    }
                    else
                    {
                        pnlPassDetails.Visible = false;
                        Errormsg("Bus Pass Doesn't Exist");
                    }
                }
                else
                {
                    pnlPassDetails.Visible = false;
                    Errormsg("Bus Pass Doesn't Exist");
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
        }
    }
    private bool ValidaionRenew()
    {
        try
        {
            if (Session["Renew_doc_ID_YN"].ToString() == "Y")
            {
                if (Session["IDProofRenew"] == null & Session["IDProofRenew"].ToString() == "")
                {
                    Errormsg("Please Select Id Proof Document");
                    return false;
                }
            }
            if (Session["Renew_doc_Add_YN"].ToString() == "Y")
            {
                if (Session["AddProofRenew"] == null & Session["AddProofRenew"].ToString() == "")
                {
                    Errormsg("Please Select AddWithValueress Proof Document");
                    return false;
                }
            }
            if (Session["Renew_Photo_YN"].ToString() == "Y")
            {
                if (Session["PhotoRenew"] == null & Session["PhotoRenew"].ToString() == "")
                {
                    Errormsg("Please Select Valid Photograph");
                    return false;
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
            return false;
        }
    }
    private void SaveRenewPassRequest()
    {
        byte[] Idproof = (byte[])Session["IDProofRenew"];
        byte[] AddProof = (byte[])Session["AddProofRenew"];
        byte[] Photo = (byte[])Session["PhotoRenew"];

        Int16 idproofid;
        if (Session["IDProofRenew"] != null | Session["IDProofRenew"].ToString() != "")
            idproofid = Convert.ToInt16(rbtnIdProofRenew.SelectedValue.ToString());
        else
            idproofid = 0;
        Int16 addproofid;
        if (Session["AddProofRenew"] != null | Session["AddProofRenew"].ToString() != "")
            addproofid = Convert.ToInt16(rbtnAddressProofRenew.SelectedValue.ToString());
        else
            addproofid = 0;
        string IPAddress = HttpContext.Current.Request.UserHostAddress;
        NpgsqlCommand MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.bus_pass_renew_request");
        MyCommand.Parameters.AddWithValue("p_passno", tbPassnNo.Text);
        MyCommand.Parameters.AddWithValue("p_idproof", (object)Idproof ?? DBNull.Value);
        MyCommand.Parameters.AddWithValue("p_addproof", (object)AddProof ?? DBNull.Value);
        MyCommand.Parameters.AddWithValue("p_idproofid", idproofid);
        MyCommand.Parameters.AddWithValue("p_addproofid", addproofid);
        MyCommand.Parameters.AddWithValue("p_photo", (object)Photo ?? DBNull.Value);
        MyCommand.Parameters.AddWithValue("p_applytype", "C");
        MyCommand.Parameters.AddWithValue("p_applytypeby", Session["_UserCntrID"].ToString());
        MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);


        dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                Session["_RNDIDENTIFIERMSTAUTH"] = _validation.GeneratePassword(10, 5);
                Session["currtranrefno"] = dt.Rows[0]["sp_curr_tran_ref"];
                Response.Redirect("CntrBusPassesConfirmation.aspx");
            }
        }
    }
    private void CategoriesTypeList()
    {
        try
        {

            NpgsqlCommand MyCommand = new NpgsqlCommand();
            DataTable dt;
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.bus_pass_category_list");
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlPassCategory.DataSource = dt;
                    ddlPassCategory.DataTextField = "buspass_categoryname";
                    ddlPassCategory.DataValueField = "buspass_categoryid";
                    ddlPassCategory.DataBind();
                }
            }
            ddlPassCategory.Items.Insert(0, "All");
            ddlPassCategory.Items[0].Value = "0";
            ddlPassCategory.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlPassCategory.Items.Insert(0, "All");
            ddlPassCategory.Items[0].Value = "0";
            ddlPassCategory.SelectedIndex = 0;
        }
    }
    private void BusPassTypeListrpt()
    {
        try
        {
            ddlPassType.Items.Clear();

            NpgsqlCommand MyCommand = new NpgsqlCommand();
            DataTable dt;
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_bus_pass_type_list_bustypeid");
            MyCommand.Parameters.AddWithValue("@p_bustypeid", 0);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlPassType.DataSource = dt;
                    ddlPassType.DataTextField = "buspasstypename";
                    ddlPassType.DataValueField = "buspasstypeid";
                    ddlPassType.DataBind();
                }
            }
            ddlPassType.Items.Insert(0, "All");
            ddlPassType.Items[0].Value = "0";
            ddlPassType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlPassType.Items.Insert(0, "All");
            ddlPassType.Items[0].Value = "0";
            ddlPassType.SelectedIndex = 0;
        }
    }
    private bool IsValidValue()
    {
        try
        {
            DateTime dtFrom;
            DateTime dtTo;

            if (!DateTime.TryParseExact(txtfrmdate.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out dtFrom))
            {
                Errormsg("Select Valid From Date");
                return false;
            }
            else if (!DateTime.TryParseExact(txttodate.Text.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out dtTo))
            {
                Errormsg("Select Valid To Date");
                return false;
            }
            else if (dtTo < dtFrom)
            {
                Errormsg("Please Enter Valid From Date");
                return false;
            }
            else if ((dtTo - dtFrom).Days > 15)
            {
                Errormsg("Please Note:- Reports can only be generated for 15 days at a time.");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("rpt_T3.aspx-006", ex.Message.ToString());
            return false;

        }
    }
    private DataTable LoadPassReport1()
    {
        try
        {
            string type = "A";

            Int16 passcategoryid = Convert.ToInt16(ddlPassCategory.SelectedValue.ToString());
            Int16 passtypeid = Convert.ToInt16(ddlPassType.SelectedValue.ToString());
            string status = ddlstatus.SelectedValue.ToString();
            string f_date = txtfrmdate.Text.ToString();
            string t_date = txttodate.Text.ToString();
            type = ddlrequesttype.SelectedValue.ToString();

            DataTable dt;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.get_pass_reports1");
            MyCommand.Parameters.AddWithValue("p_frmdate", f_date);
            MyCommand.Parameters.AddWithValue("p_todate", t_date);
            MyCommand.Parameters.AddWithValue("p_type", type);
            MyCommand.Parameters.AddWithValue("p_passtype", passcategoryid);
            MyCommand.Parameters.AddWithValue("p_psngrtype", passtypeid);
            MyCommand.Parameters.AddWithValue("p_status", status);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                    return dt;
            }
            return dt;
        }
        catch (Exception ex)
        {
            DataTable dt = new DataTable();
            return dt;
        }
    }
    private void SearchReport1()
    {
        lbtnReportdownload.Visible = false;
        grvreport1.Visible = false;
        dvreport1.Visible = true;
        DataTable dt = LoadPassReport1();
        if (dt.Rows.Count > 0)
        {
            lbtnReportdownload.Visible = true;
            grvreport1.DataSource = dt;
            grvreport1.DataBind();
            grvreport1.Visible = true;
            dvreport1.Visible = false;
        }
        else
        {
            //Errormsg("Sorry, No Record Available for selected perameter")
            return;
        }
    }
    private void GetCntrQuery()
    {
        try
        {
            string P_CntrId = ""; // Session("_UserCntrID").ToString
            string P_Date = ""; // txtsmrydate.Text.ToString
            string P_Value = tbvalue.Text.ToString();
            DataTable dt;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.cntrsmry");
            MyCommand.Parameters.AddWithValue("p_cntrid", P_CntrId);
            MyCommand.Parameters.AddWithValue("p_date", P_Date);
            MyCommand.Parameters.AddWithValue("p_value", P_Value);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    gvsmry.DataSource = dt;
                    gvsmry.DataBind();
                    gvsmry.Visible = true;
                    dvsmry.Visible = false;
                }
                else
                {
                    gvsmry.Visible = false;
                    dvsmry.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    #endregion


    #region "Events"
    protected void btnPassNew_Click(object sender, System.EventArgs e)
    {
        lblrequestmsg.Text = "New Pass Request";
        setPassHeaderButtonCSS(btnPassNew);
        pnlPassNew.Visible = true;
        pnlPassRenew.Visible = false;
        pnlPassQuery.Visible = false;
    }
    protected void btnPassRenew_Click(object sender, System.EventArgs e)
    {
        lblrequestmsg.Text = "Renew Pass Request";
        setPassHeaderButtonCSS(btnPassRenew);
        pnlPassNew.Visible = false;
        pnlPassRenew.Visible = true;
        pnlPassQuery.Visible = false;
    }
    protected void btnPassQuery_Click(object sender, System.EventArgs e)
    {
        lblrequestmsg.Text = "Pass/Transaction Query and Report";
        setPassHeaderButtonCSS(btnPassQuery);
        pnlPassNew.Visible = false;
        pnlPassRenew.Visible = false;
        pnlPassQuery.Visible = true;
        SearchReport1();
    }
    protected void lbtnsearchView_Click(object sender, EventArgs e)
    {
        LoadSearchPass();
    }
    protected void lbtnsearchDownload_Click(object sender, EventArgs e)
    {
        DataTable dt = dtPass();
        if (dt.Rows.Count > 0)
        {
            cryRpt.Load(Pass_Details);
            cryRpt.SetDataSource(dt);
            cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Context.Response, true, "Bus Depot " + DateTime.Now);
        }
        else
        {
        }
    }
    protected void grdsearchpass_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdsearchpass.PageIndex = e.NewPageIndex;
        LoadSearchPass();
    }
    protected void grdsearchpass_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            LinkButton lbtnprintpass = (LinkButton)e.Row.FindControl("lbtnprintpass");
            LinkButton lbtnprintreceipt = (LinkButton)e.Row.FindControl("lbtnprintreceipt");
            lbtnprintreceipt.Visible = false;
            lbtnprintpass.Visible = false;


            if (rowView["current_status"].ToString() == "S")
            {
                if (rowView["issuence_"].ToString() == "A")
                {
                    lbtnprintreceipt.Visible = true;
                    lbtnprintpass.Visible = false;
                }
            }
            else if (rowView["current_status"].ToString() == "A")
            {
                if (rowView["issuence_"].ToString() == "A")
                {
                    lbtnprintreceipt.Visible = true;
                    lbtnprintpass.Visible = true;
                }
                else if (rowView["issuence_"].ToString() == "I")
                {
                    lbtnprintreceipt.Visible = false;
                    lbtnprintpass.Visible = true;
                }
            }
        }
    }
    protected void grdsearchpass_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "PrintPass")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string p_PASSNUMBER = grdsearchpass.DataKeys[row.RowIndex]["pass_no"].ToString();
            Session["Passno"] = p_PASSNUMBER;
            openSubDetailsWindow("../Bus_Pass.aspx");
        }
        if (e.CommandName == "PrintReceipt")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string p_CURRTRANREFNO = grdsearchpass.DataKeys[row.RowIndex]["currtranref_no"].ToString();
            Session["currtranrefno"] = p_CURRTRANREFNO;
            openSubDetailsWindow("../Pass_reciept.aspx");
        }
    }
    protected void lbtnInfo_Click(object sender, EventArgs e)
    {
        mpInfo.Show();
    }
    protected void ddlbuspassType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlbuspassType.SelectedValue == "0")
        {
            //pnlinstruction.Visible = false;
            lbtnInfo.Visible = false;
            ResetControl();
        }
        else
        {
            //pnlinstruction.Visible = true;
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

                    //lblEligibility.Text = "<b>Eligibility of Pass </b><br/> " + dt.Rows[0]["buspasscategoryname"].ToString();
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
                        //lblState.Text = "<br><b>Pass Allowed </b><br/>within State Only";
                        lblStateInfo.Text = "<br><b>Pass Allowed </b><br/> within State Only";
                    }
                    else if (journeyallowedoutsidestateyn == "Y")
                    {
                        //lblState.Text = "<br><b>Pass Allowed</b><br/> Outside State Only";
                    }
                    else if (journeyallowedinstateyn == "Y" && journeyallowedoutsidestateyn == "Y")
                    {
                        // lblState.Text = "<br><b>Pass Allowed </b><br/>within State as well as outside state";
                    }

                    if (validityMonth == "0")
                    {
                        if (validityinday != "0")
                        {
                            // lblvalidity.Text = "<br><b> Validity </b><br/> " + validityinday + " Days";
                            lblvalidityInfo.Text = "<br> <b>Validity </b><br/> " + validityinday + " Days";
                        }
                    }
                    else
                    {
                        //lblvalidity.Text = "<br><b> Validity </b><br/> " + validityUpto;
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


                                lblservicetypeInfo.Text = "<br><b> Applicable Bus Service Type </b><br/> " + a;
                                ddlServiceType.DataSource = GetBusServiceType("4", applicabletobusservicetype);
                                ddlServiceType.DataTextField = "document_type_name";
                                ddlServiceType.DataValueField = "document_type_id";
                                ddlServiceType.DataBind();
                            }

                            if (applicabletobusservicetype.Length < 1)
                            {
                                //ddlServiceType.Items.Insert(0, "Select");
                                //ddlServiceType.Items[0].Value = "0";
                                //ddlServiceType.SelectedIndex = 0;
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

                                //lblIDDocuments.Text = "<br><b> Applicable Id Proof Document </b><br/> " + a;
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

                                //lbladdDocuments.Text = "<br><b> Applicable Address Proof Document </b><br/> " + a;
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

                                //lblChargesApplicable.Text = "<br><b>  Charge Applicable </b><br/> " + a;
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
    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadStationFrom(ddlFrom, 0);
    }
    protected void ddlFrom_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        loadStationFrom(ddlTo, Convert.ToDecimal(ddlFrom.SelectedValue));
        int statecode = getStationstate(ddlFrom.SelectedValue);
    }
    protected void ddlTo_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        int statecode = getStationstate(ddlTo.SelectedValue);
        if ((Session["ValidFor"].ToString() == null) | Session["ValidFor"].ToString() == "")
        {
            Errormsg("Pass Validation Not Available please Contact APSTS Pass Admin");
            loadStationFrom(ddlTo, Convert.ToDecimal(ddlFrom.SelectedValue));
            return;
        }
        else if (Session["ValidFor"].ToString() == "Y")
        {
            if (statecode != 12)
            {
                Errormsg("Pass Valid Only for Arunachal State please Select Arunachal stations");
                loadStationFrom(ddlTo, Convert.ToDecimal(ddlFrom.SelectedValue));
                return;
            }
        }
        else if (Session["ValidFor"].ToString() == "N")
        {
            if (statecode == 12)
            {
                Errormsg("Pass Valid for ohen then Uttarakhand State please Select other stations");
                loadStationFrom(ddlTo, Convert.ToDecimal(ddlFrom.SelectedValue));
                return;
            }
        }
        int _km = validKm(Convert.ToInt32(ddlRoute.SelectedValue), Convert.ToInt32(ddlFrom.SelectedValue), Convert.ToInt32(ddlTo.SelectedValue));
        int allowedkm = 0;
        if (Session["AllowDtnc"].ToString() != null | Session["AllowDtnc"].ToString() != "")
        {
            allowedkm = Convert.ToInt32(Session["AllowDtnc"].ToString().Trim());
            if (allowedkm > 0)
            {
                if (_km > allowedkm)
                {
                    Errormsg("Only Upto " + allowedkm.ToString().Trim() + " Km distance station's pass is available." + " Distance for this selection is " + _km.ToString() + " Km.");
                    loadStationFrom(ddlTo, Convert.ToDecimal(ddlFrom.SelectedValue));
                    return;
                }
            }
        }
        else
        {
            Errormsg("Allow Km distance station's pass is not available.");
            loadStationFrom(ddlTo, Convert.ToDecimal(ddlFrom.SelectedValue));
            return;
        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDistrict();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCity();
    }
    protected void tbValidityFrom_TextChanged(object sender, EventArgs e)
    {
        int validity;
        validity = Convert.ToInt32(Session["VALIDITYINDAYS"].ToString());
        tbValidityTo.Text = Convert.ToDateTime(tbValidityFrom.Text) + new TimeSpan(validity, 0, 0, 0).ToString();
    }
    protected void tbConfirmAdharNumber_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string sAdhaar = tbAdharNumber.Text.ToString().Trim();
            string CAdhaar = tbConfirmAdharNumber.Text.ToString().Trim();
            if (sAdhaar.Length == 12 & CAdhaar.Length == 12)
            {
                if (sAdhaar != CAdhaar)
                {
                    Errormsg(" Aadhar number and confirm Aadhar number should be same. Please Valid Enter Valid Adhaar Number" + "<Br/>");
                    tbConfirmAdharNumber.Text = "";
                }
                else
                {
                    bool isValidnumber = aadharcard.validateVerhoeff(sAdhaar);
                    bool CisValidnumber = aadharcard.validateVerhoeff(CAdhaar);
                    if (isValidnumber == false & CisValidnumber == false)
                    {
                        Errormsg("Invalid Aadhaar Number" + "<Br/>");
                        tbConfirmAdharNumber.Text = "";
                    }
                }
            }
            else
            {
                Errormsg(" Enter Valid Adhaar Number" + "<Br/>");
                tbConfirmAdharNumber.Text = "";
            }


            DataTable dt;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.checkrecordaadhar");
            MyCommand.Parameters.AddWithValue("p_aadhar", Encrypt(tbAdharNumber.Text));

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["chkaadhr"]) > 0)
                    {
                        Errormsg("Bus pass already exist with this Aadhar ID ");
                        tbAdharNumber.Text = "";
                        tbConfirmAdharNumber.Text = "";
                    }
                }
            }

            string st = tbAdharNumber.Text.ToString();
            Session["AadharMasking"] = st.Substring(8, 4);

        }
        catch (Exception ex)
        {
            Errormsg("Bus pass already exist with this Aadhar ID " + ex.Message);
            tbAdharNumber.Text = "";
            tbConfirmAdharNumber.Text = "";
        }
    }
    protected void rbtnIdProof_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnIdProof.SelectedValue != null)
            pnlIdProof.Visible = true;
        else
            pnlIdProof.Visible = false;
    }
    protected void rbtnAddressProof_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnAddressProof.SelectedValue != null)
            pnlAddProof.Visible = true;
        else
            pnlAddProof.Visible = false;
    }
    protected void btnUploadDoc_Click(object sender, System.EventArgs e)
    {
        if (fileUpload.HasFile)
        {
            Session["IDProof"] = null;
            string _fileFormat = GetMimeDataOfFile(fileUpload.PostedFile);
            string _NewFileName = "";
            if (fileUpload.FileName.Length <= 50)
            {
                Session["AddProof"] = fileUpload.FileBytes;
                lblIDpdf.Text = fileUpload.FileName;
                Session["pdfIDProofName"] = fileUpload.FileName;
                lblIDpdf.Visible = true;
            }
            if (_fileFormat == "application/pdf")
                _NewFileName += ".pdf";
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

            int length = fileUpload.PostedFile.ContentLength;

            if (length == 0)
                Errormsg("Document has not been uploaded.");

            if (length < 1024)
                Errormsg("File size should be less than 1MB.");

            Session["IDProof"] = fileUpload.FileBytes;
        }
    }
    protected void btnAddproof_Click(object sender, EventArgs e)
    {
        if (fileaddproof.HasFile)
        {
            Session["AddProof"] = null;
            string _fileFormat = GetMimeDataOfFile(fileaddproof.PostedFile);
            string _NewFileName = "";
            if (fileaddproof.FileName.Length <= 50)
            {
                Session["AddProof"] = fileaddproof.FileBytes;
                lbladd.Text = fileaddproof.FileName;
                Session["pdfAddProofName"] = fileaddproof.FileName;
                lbladd.Visible = true;
            }

            if (_fileFormat == "application/pdf")
                _NewFileName += ".pdf";
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

            int length = fileaddproof.PostedFile.ContentLength;

            if (length == 0)
                Errormsg("Document has not been uploaded.");

            if (length < 1024)
                Errormsg("File size should be less than 1MB.");

            Session["AddProof"] = fileaddproof.FileBytes;
        }
    }
    protected void btntakephoto_Click(object sender, System.EventArgs e)
    {
        Mptakephoto.Show();
    }
    protected void btnpic_Click(object sender, System.EventArgs e)
    {
        string[] imgstring = hdimg.Value.Split(',');
        byte[] PhotoImage = Convert.FromBase64String(imgstring[1]);
        ImgWebPortal.ImageUrl = GetImage(PhotoImage);
        Session["Photo"] = PhotoImage;
        Session["webcount"] = "W";
        ImgWebPortal.Visible = true;
        lbtncloseWebImage.Visible = true;
        btntakephoto.Visible = false;
        // ImgWebPortal.Visible = False
        Mptakephoto.Hide();
    }
    protected void lbtncloseWebImage_Click(object sender, EventArgs e)
    {
        Session["Photo"] = null;
        ImgWebPortal.Visible = false;
        lbtncloseWebImage.Visible = false;
        btntakephoto.Visible = true;
    }
    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        resetcontrol();
    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        if (Validaion() == false)
            return;
        Session["Action"] = "S";
        ConfirmMsg("Do You Want To Proceed For New Bus Pass request ?");
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (Session["Action"].ToString() == "S")
        {
            SaveNewPassRequest();
        }


        if (Session["Action"].ToString() == "Renew")
        {
            SaveRenewPassRequest();


        }
        else
        {
            SaveNewPassRequest();
        }

    }
    protected void lbtnSrchNwPsRqt_Click(object sender, EventArgs e)
    {
        if (validvalue() == false)
        {
            return;
        }

        resetcontrolRenewTab();
        loadPassDetails();
    }
    protected void rbtnIdProofRenew_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnIdProofRenew.SelectedValue != null)
            pnlIdProofUploadRenew.Visible = true;
        else
            pnlIdProofUploadRenew.Visible = false;
    }
    protected void btnUploadDocRenew_Click(object sender, System.EventArgs e)
    {
        Session["IDProofRenew"] = null;
        if (fileUploadRenew.HasFile)
        {
            string _fileFormat = GetMimeDataOfFile(fileUploadRenew.PostedFile);
            string _NewFileName = "";
            if (fileUploadRenew.FileName.Length <= 50)
            {
                Session["IDProofRenew"] = fileUploadRenew.FileBytes;
                lblIDpdfRenew.Text = fileUploadRenew.FileName;
                Session["pdfIDProofRenewName"] = fileUploadRenew.FileName;
                lblIDpdfRenew.Visible = true;
            }
            if (_fileFormat == "application/pdf")
                _NewFileName += ".pdf";
            else
            {
                Errormsg("Please select Pdf file only.");
                return;
            }

            int cnt = fileUploadRenew.PostedFile.FileName.Split('.').Length - 1;

            if (cnt > 1)
            {
                Errormsg("Please select Pdf file only.");
                return;
            }

            int length = fileUploadRenew.PostedFile.ContentLength;

            if (length == 0)
                Errormsg("Document has not been uploaded.");
            if (length < 1024)
                Errormsg("File size should be less than 1MB.");
            Session["IDProofRenew"] = fileUploadRenew.FileBytes;
        }
    }
    protected void rbtnAddressProofRenew_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnAddressProofRenew.SelectedValue != null)
            pnlAddProofUploadRenew.Visible = true;
        else
            pnlAddProofUploadRenew.Visible = false;
    }
    protected void btnAddproofRenew_Click(object sender, System.EventArgs e)
    {
        Session["AddProofRenew"] = null;
        if (fileaddproofRenew.HasFile)
        {
            string _fileFormat = GetMimeDataOfFile(fileaddproofRenew.PostedFile);
            string _NewFileName = "";
            if (fileaddproofRenew.FileName.Length <= 50)
            {
                Session["AddProofRenew"] = fileaddproofRenew.FileBytes;
                lbladdpdfRenew.Text = fileaddproofRenew.FileName;
                Session["pdfAddProofRenewName"] = fileaddproofRenew.FileName;
                lbladdpdfRenew.Visible = true;
            }
            if (_fileFormat == "application/pdf")
                _NewFileName += ".pdf";
            else
            {
                Errormsg("Please select Pdf file only.");
                return;
            }

            int cnt = fileaddproofRenew.PostedFile.FileName.Split('.').Length - 1;

            if (cnt > 1)
            {
                Errormsg("Please select Pdf file only.");
                return;
            }

            int length = fileaddproofRenew.PostedFile.ContentLength;

            if (length == 0)
                Errormsg("Document has not been uploaded.");
            if (length < 1024)
                Errormsg("File size should be less than 1MB.");
            Session["AddProofRenew"] = fileaddproofRenew.FileBytes;
        }
    }
    protected void btntakephoto1_Click(object sender, System.EventArgs e)
    {
        Mptakephoto1.Show();
    }
    protected void btnpic1_Click(object sender, System.EventArgs e)
    {
        string[] imgstring = hdimg1.Value.Split(',');
        byte[] PhotoImage = Convert.FromBase64String(imgstring[1]);
        ImgWebPortal1.ImageUrl = GetImage(PhotoImage);
        Session["PhotoRenew"] = PhotoImage;
        // Session("webcount") = "W"
        ImgWebPortal1.Visible = true;
        lbtncloseWebImage1.Visible = true;
        btntakephoto1.Visible = false;
        // ImgWebPortal.Visible = False
        Mptakephoto1.Hide();
    }
    protected void lbtncloseWebImage1_Click(object sender, EventArgs e)
    {
        Session["PhotoRenew"] = null;
        ImgWebPortal1.Visible = false;
        lbtncloseWebImage1.Visible = false;
        btntakephoto1.Visible = true;
    }
    protected void lbtnSaveRenew_Click(object sender, System.EventArgs e)
    {
        if (ValidaionRenew() == false)
            return;
        Session["Action"] = "Renew";
        ConfirmMsg("Do You Want To Proceed For Renewing Bus Pass request ?");
    }
    protected void lbtnReportview_Click(object sender, EventArgs e)
    {
        if (!IsValidValue())
        {
            grvreport1.Visible = false;
            return;
        }

        grvreport1.Visible = true;
        SearchReport1();

    }
    protected void lbtnReportdownload_Click(object sender, EventArgs e)
    {
        if (!IsValidValue())
        {
            return;
        }
        DataTable dt = LoadPassReport1();
        if (dt.Rows.Count > 0)
        {
            cryRpt.Load(Pass_report1);
            cryRpt.SetDataSource(dt);

            TextObject objtxtreportnumber = (TextObject)cryRpt.ReportDefinition.Sections[1].ReportObjects["txtreportnumber"];
            objtxtreportnumber.Text = "1.1";

            CrystalDecisions.CrystalReports.Engine.TextObject objtxtreportname = (TextObject)cryRpt.ReportDefinition.Sections[1].ReportObjects["txtreportname"];
            objtxtreportname.Text = "Pass Report ";

            CrystalDecisions.CrystalReports.Engine.TextObject objtxtApplyType = (TextObject)cryRpt.ReportDefinition.Sections[1].ReportObjects["txtApplyType"];
            objtxtApplyType.Text = "Pass Issue Type :- " + ddlrequesttype.SelectedItem.ToString();

            CrystalDecisions.CrystalReports.Engine.TextObject objtxtapplydate = (TextObject)cryRpt.ReportDefinition.Sections[1].ReportObjects["txtapplydate"];
            objtxtapplydate.Text = "Apply Date :- " + txtfrmdate.Text + "-" + txttodate.Text;

            CrystalDecisions.CrystalReports.Engine.TextObject objtxtpasstype = (TextObject)cryRpt.ReportDefinition.Sections[1].ReportObjects["txtpasstype"];
            objtxtpasstype.Text = "Pass Category :- " + ddlPassCategory.SelectedItem.ToString() + ", Pass Type :- " + ddlPassType.SelectedItem.ToString();

            CrystalDecisions.CrystalReports.Engine.TextObject objtxturl = (TextObject)cryRpt.ReportDefinition.Sections[4].ReportObjects["txturl"];
            objtxturl.Text = "Downloaded From -" + Request.Url.AbsoluteUri;

            cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "PassReport" + DateTime.Now + "");
        }
        else
        {
            Errormsg("Sorry, No Record Available for selected perameter");
            return;
        }
    }
    protected void grvreport1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvreport1.PageIndex = e.NewPageIndex;
        SearchReport1();
    }
    protected void lbtnsrchsumry_Click(object sender, EventArgs e)
    {
        gvsmry.Visible = false;
        dvsmry.Visible = true;
        if (tbvalue.Text.Length <= 0)
        {
            Errormsg("Enter At least 3 Char. for searching perameter");
            return;
        }
        GetCntrQuery();
    }

    #endregion


}
