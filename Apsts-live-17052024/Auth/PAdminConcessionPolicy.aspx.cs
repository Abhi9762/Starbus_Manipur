using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_PAdminConcessionPolicy : BasePage 
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
            Session["_moduleName"] = "Concession Policy";

            ServiceTypeList();
            IDProofTypeList();
            DocumentProofTypeList();
          
            BusPassTypeList();
            ConcessionList();
        }
    }

    #region "Methods"
    private void CsrfTokenValidate()
    {
        if (hidtoken.Value != Session["rndNoCheck"].ToString())
        {
            Response.Redirect("errorpage.aspx");
            return;
        }
    }
    private void ServiceTypeList()
    {
        try
        {
            DataTable dt;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear(); 
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_servicetype_getlist");

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlServiceType.DataSource = dt;
                    ddlServiceType.DataTextField = "servicetype_name_en";
                    ddlServiceType.DataValueField = "srtpid";
                    ddlServiceType.DataBind();
                }
            }
            else
            {
                ddlServiceType.Items.Insert(0, "SELECT");
                ddlServiceType.Items[0].Value = "0";
                ddlServiceType.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ddlServiceType.Items.Insert(0, "SELECT");
            ddlServiceType.Items[0].Value = "0";
            ddlServiceType.SelectedIndex = 0;
            _common.ErrorLog("PAdminConcessionPolicy.aspx-0001", ex.Message.ToString());
        }
    }
    private void IDProofTypeList()
    {
        try
        {
            ddlidverification.Items.Clear();
            DataTable dt;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_bus_pass_document_id_proof");

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlidverification.DataSource = dt;
                    ddlidverification.DataTextField = "documenttype_name";
                    ddlidverification.DataValueField = "document_typeid";
                    ddlidverification.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
            _common.ErrorLog("PAdminConcessionPolicy.aspx-0002", ex.Message.ToString());
        }
    }
    private void DocumentProofTypeList()
    {
        try
        {
            ddldocumentverification.Items.Clear();
            DataTable dt;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_bus_pass_document_id_proof");

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddldocumentverification.DataSource = dt;
                    ddldocumentverification.DataTextField = "documenttype_name";
                    ddldocumentverification.DataValueField = "document_typeid";
                    ddldocumentverification.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
            _common.ErrorLog("PAdminConcessionPolicy.aspx-0003", ex.Message.ToString());
        }
    }
    private void BusPassTypeList()
    {
        try
        {
            ddlBusPass.Items.Clear();
            DataTable dt;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_buspass_type");
            

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    ddlBusPass.DataSource = dt;
                    ddlBusPass.DataTextField = "buspasstypename";
                    ddlBusPass.DataValueField = "buspasstypeid";
                    ddlBusPass.DataBind();
                }
            }

            ddlBusPass.Items.Insert(0, "Select");
            ddlBusPass.Items[0].Value = "0";
            ddlBusPass.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlBusPass.Items.Insert(0, "Select");
            ddlBusPass.Items[0].Value = "0";
            ddlBusPass.SelectedIndex = 0;
            _common.ErrorLog("PAdminConcessionPolicy.aspx-0004", ex.Message.ToString());
        }
    }
    protected static DataTable GetIDproof(string flag, string documentidprooffornewtypes)
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
                    table.Rows.Add(ss[i], dt.Rows[0]["name"].ToString());
            }
        }

        return table;
    }
    public void resetcontrol()
    {
        ddlBusPass.SelectedIndex = 0;
        ddlBusPass.SelectedValue = "0";

        tbConsessionName.Text = "";
        tbConsessionName.Enabled = true;

        tbConsessionNameLocal.Text = "";
        tbConsessionNameLocal.Enabled = true;

        tbabbr.Text = "";
        tbabbr.Enabled = true;

        tbRemark.Text = "";
        tbRemark.Enabled = true;

        tbconcessionfare.Enabled = true;
        tbconcessionfare.Text = "100";
        tbconcessiontax.Enabled = true;
        tbconcessiontax.Text = "100";

        rbtEBTMprintyn.SelectedValue = "Y";

        chkGender.Checked = false;
        chkGender.Enabled = true;

        ddlGender.Visible = false;
        ddlGender.Enabled = true;
        ddlGender.SelectedIndex = 0;

        lblgender.Visible = false;

        chkKms.Checked = false;
        chkKms.Enabled = true;
        tbKms.Visible = false;
        tbKms.Enabled = true;
        tbKms.Text = "";

        chkAgeGroup.Checked = false;
        chkAgeGroup.Enabled = true;

        pnlAge.Visible = false;
        tbMinAge.Enabled = true;
        tbMaxAge.Enabled = true;
        tbMinAge.Text = "";
        tbMaxAge.Text = "";
        chkServiceType.Checked = false;
        chkServiceType.Enabled = true;
        ddlServiceType.Visible = false;
        lblservicetype.Visible = false;
        ServiceTypeList();
        chkState.Checked = false;
        chkState.Enabled = true;
        pnlState.Visible = false;
        chkWithinState.Enabled = true;
        chkWithinState.Checked = false;
        chkOutsideState.Enabled = true;
        chkOutsideState.Checked = false;
        chkAttendant.Checked = false;
        chkallowotherconcession.Checked = false;
        chkonlineverification.Checked = false;
        chkidverification.Checked = false;
        IDProofTypeList();
        ddlidverification.Visible = false;
        chkdocumentverification.Checked = false;
        DocumentProofTypeList();
        ddldocumentverification.Visible = false;
        lbtnSave.Visible = true;
        lbtnUpdate.Visible = false;
    }
    private bool Validaion()
    {
        try
        {
            int count = 0;
            string msg = "";
            
            if (_validation.IsValidString(tbConsessionName.Text, 3, tbConsessionName.MaxLength) == false)
            {
                count += 1;
                msg = msg + count + ". Enter Valid Concession Name In English.<br/>";
            }

            if (_validation.IsValidString(tbRemark.Text, 3, tbRemark.MaxLength) == false)
            {
                count += 1;
                msg = msg + count + ".  Enter Valid Remark.<br/>";
            }

            if (ddlBusPass.SelectedIndex < 0)
            {
                count += 1;
                msg = msg + count + ". Select Bus Pass Type<br/>";
            }

            if (_validation.IsValidInteger(tbconcessionfare.Text, 1, 3) == false)
            {
                count += 1;
                msg = msg + count + ".  Enter Valid Concession Percentage On Fare.<br/>";
            }

            if (_validation.IsValidInteger(tbconcessiontax.Text, 1, 3) == false)
            {
                count += 1;
                msg = msg + count + ".  Enter Valid Concession Percentage On Tax.<br/>";
            }

            if (chkGender.Checked == true)
            {
                if (ddlGender.SelectedValue == "0")
                {
                    count += 1;
                    msg = msg + count + ". Select at least One gender. <br/>";
                }
            }

            if (chkKms.Checked == true)
            {
                if (tbKms.Text == "")
                {
                    count += 1;
                    msg = msg + count + ". Enter Number of KMs. <br/>";
                }
            }

            if (chkAgeGroup.Checked == true)
            {
                if (tbMinAge.Text == "" && tbMaxAge.Text == "")
                {
                    count += 1;
                    msg = msg + count + ". Select Minimum or Maximum Age.<br/>";
                }
            }


            if (chkState.Checked == true)
            {
                if (chkWithinState.Checked == false & chkOutsideState.Checked == false)
                {
                    count += 1;
                    msg = msg + count + ". Select Journey Allowed State<br/>";
                }
            }
            if (chkonlineverification.Checked == false & chkidverification.Checked == false & chkdocumentverification.Checked == false)
            {
                count += 1;
                msg = msg + count + ". Select at least one verification Mode<br/>";
            }
            if (count > 0)
            {
                Errormsg(msg);
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            _common.ErrorLog("PAdminConcessionPolicy.aspx-0005", ex.Message.ToString());
            return false;
        }
    }
    private void saveConcession()
    {
        try
        {
            string Status = "A";
            string concessionName = "";
            string concessionNameL = "";
            string concessionAbbr = "";
            string Remark = "";
            string passType = "";
            decimal concession_fare = 0;
            decimal concession_tax = 0;
            string EBTMPrint_yn = "Y";
            string specificGenderYN = "N";
            string GenderTypes = "";
            string TravelNoOfKmsYN = "N";
            int NoofKms = 0;
            string SpecificAgeGroupYN = "N";
            int MinAge = 0;
            int MaxAge = 0;
            string ServiceTypeYN = "N";
            string Servicetypes = "";
            string RistrictedStateYN = "N";
            string WithinStateYN = "N";
            string OtherStateYN = "N";
            string AdditionalAttendentYN = "N";
            string Otherconcession = "N";
            string OnlineVerificationYN = "N";
            string IdVerificationYN = "N";
            string idverification = "";
            string DocumentVerificationYN = "N";
            string Documentverification = "";
            string OnlineConcessionYN = "N";

            int concessionid = Convert.ToInt16(Session["ConcessionId"].ToString());
            concessionName = tbConsessionName.Text;
            concessionNameL = tbConsessionNameLocal.Text;
            concessionAbbr = tbabbr.Text;
            Remark = tbRemark.Text;
            passType = ddlBusPass.SelectedValue;
            concession_fare = Convert.ToDecimal(tbconcessionfare.Text.ToString());
            concession_tax = Convert.ToDecimal(tbconcessiontax.Text.ToString());
            EBTMPrint_yn = rbtEBTMprintyn.SelectedValue.ToString();
            OnlineConcessionYN = rbtapplicableonline.SelectedValue.ToString();

            if (chkGender.Checked == true)
            {
                specificGenderYN = "Y";
                if (ddlGender.SelectedValue != "0")
                {
                    foreach (ListItem item in ddlGender.Items)
                    {
                        if (item.Selected)
                            GenderTypes += item.Value + ",";
                    }
                    if (GenderTypes.Length > 1)
                        GenderTypes = GenderTypes.Substring(0, GenderTypes.Length - 1);
                }
            }
            if (chkKms.Checked == true)
            {
                TravelNoOfKmsYN = "Y";
                NoofKms = Convert.ToInt32(tbKms.Text);
            }
            if (chkAgeGroup.Checked == true)
            {
                SpecificAgeGroupYN = "Y";
                MinAge = Convert.ToInt32(tbMinAge.Text);
                MaxAge = Convert.ToInt32(tbMaxAge.Text);
            }
            if (chkServiceType.Checked == true)
            {
                ServiceTypeYN = "Y";
                if (ddlServiceType.SelectedValue != "0")
                {
                    foreach (ListItem item in ddlServiceType.Items)
                    {
                        if (item.Selected)
                            Servicetypes += item.Value + ",";
                    }
                    if (Servicetypes.Length > 1)
                        Servicetypes = Servicetypes.Substring(0, Servicetypes.Length - 1);
                }
            }
            if (chkState.Checked == true)
            {
                RistrictedStateYN = "Y";
                if (chkWithinState.Checked == true)
                    WithinStateYN = "Y";
                if (chkOutsideState.Checked == true)
                    OtherStateYN = "Y";
            }
            if (chkAttendant.Checked == true)
                AdditionalAttendentYN = "Y";
            if (chkallowotherconcession.Checked == true)
                Otherconcession = "Y";
            if (chkonlineverification.Checked == true)
                OnlineVerificationYN = "Y";

            if (chkidverification.Checked == true)
            {
                IdVerificationYN = "Y";
                if (ddlidverification.SelectedValue != "0")
                {
                    foreach (ListItem item in ddlidverification.Items)
                    {
                        if (item.Selected)
                            idverification += item.Value + ",";
                    }
                    if (idverification.Length > 1)
                        idverification = idverification.Substring(0, idverification.Length - 1);
                }
            }
            if (chkdocumentverification.Checked == true)
            {
                DocumentVerificationYN = "Y";
                if (ddldocumentverification.SelectedValue != "0")
                {
                    foreach (ListItem item in ddldocumentverification.Items)
                    {
                        if (item.Selected)
                            Documentverification += item.Value + ",";
                    }
                    if (Documentverification.Length > 1)
                        Documentverification = Documentverification.Substring(0, Documentverification.Length - 1);
                }
            }
            if (Session["_action"].ToString() == "S" || Session["_action"].ToString() == "U")
                Status = "A";
            else
                Status = Session["_action"].ToString();

            string IPAddress = HttpContext.Current.Request.UserHostAddress;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            DataTable dt;

            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_concession_insertupdate");
            MyCommand.Parameters.AddWithValue("p_action", Session["_action"].ToString());
            MyCommand.Parameters.AddWithValue("p_updatedby", Session["_UserCode"].ToString());
            MyCommand.Parameters.AddWithValue("p_ipaddress", IPAddress);
            MyCommand.Parameters.AddWithValue("p_concession", Convert.ToInt16(concessionid));
            MyCommand.Parameters.AddWithValue("p_name", concessionName);
            MyCommand.Parameters.AddWithValue("p_namel", concessionNameL);
            MyCommand.Parameters.AddWithValue("p_abbr", concessionAbbr);
            MyCommand.Parameters.AddWithValue("p_remark", Remark);
            //
            MyCommand.Parameters.AddWithValue("p_bus_passtype", passType);
            MyCommand.Parameters.AddWithValue("p_concessin_fare", concession_fare);
            MyCommand.Parameters.AddWithValue("p_concessin_tax", concession_tax);
            MyCommand.Parameters.AddWithValue("p_ebtmprintyn", EBTMPrint_yn);
            MyCommand.Parameters.AddWithValue("p_specificgender_yn", specificGenderYN);
            MyCommand.Parameters.AddWithValue("p_gender_types", GenderTypes);
            MyCommand.Parameters.AddWithValue("p_noofkmsyn", TravelNoOfKmsYN);
            MyCommand.Parameters.AddWithValue("p_noofkms", NoofKms);
            MyCommand.Parameters.AddWithValue("p_agegroupyn", SpecificAgeGroupYN);
            MyCommand.Parameters.AddWithValue("p_minage", MinAge);
            MyCommand.Parameters.AddWithValue("p_maxage", MaxAge);
            MyCommand.Parameters.AddWithValue("p_servicetypeyn", ServiceTypeYN);
            MyCommand.Parameters.AddWithValue("p_servicetypes", Servicetypes);

            MyCommand.Parameters.AddWithValue("p_stateyn", RistrictedStateYN);
            MyCommand.Parameters.AddWithValue("p_withinstateyn", WithinStateYN);
            MyCommand.Parameters.AddWithValue("p_otherstateyn", OtherStateYN);
            MyCommand.Parameters.AddWithValue("p_additionalattendentyn", AdditionalAttendentYN);
            MyCommand.Parameters.AddWithValue("p_otherconcessionyn", Otherconcession);
            MyCommand.Parameters.AddWithValue("p_onlineverificationyn", OnlineVerificationYN);
            MyCommand.Parameters.AddWithValue("p_idverificationyn", IdVerificationYN);
            MyCommand.Parameters.AddWithValue("p_idverification", idverification);
            MyCommand.Parameters.AddWithValue("p_documentverificationyn", DocumentVerificationYN);
            MyCommand.Parameters.AddWithValue("p_documentverification", Documentverification);
            MyCommand.Parameters.AddWithValue("p_status", Status);
            MyCommand.Parameters.AddWithValue("p_onlineconcessionyn", OnlineConcessionYN);


            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    if (Session["_action"].ToString() == "S")
                        Successmsg("Concession Policy Successfully Added.");
                    else if (Session["_action"].ToString() == "U")
                        Successmsg("Concession Policy Successfully Updated.");
                    else if (Session["_action"].ToString() == "A")
                        Successmsg("Concession Policy Successfully Activated.");
                    else if (Session["_action"].ToString() == "D")
                        Successmsg("Concession Policy Successfully Discontinued.");
                    else if (Session["_action"].ToString() == "X")
                        Successmsg("Concession Policy Successfully Deleted.");
                    resetcontrol();
                    ConcessionList();
                }
                else
                    Errormsg("Error...");
            }
            else
                Errormsg(dt.TableName);
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            _common.ErrorLog("PAdminConcessionPolicy.aspx-0006", ex.Message.ToString());
        }
    }
    protected void ConcessionList()
    {
        try
        {
            DataTable dt;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_concession_list");

            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                lblTotalConcession.Text = dt.Rows.Count.ToString();

                string Active_Concession = string.Format("current_status = 'A'");
                DataRow[] Active_Concession_Count = dt.Select(Active_Concession);
                lblActivateConcession.Text = Active_Concession_Count.Count().ToString();

                string Deactive_Concession = string.Format("current_status = 'D'");
                DataRow[] Deactive_Concession_Count = dt.Select(Deactive_Concession);
                lblDeactConcession.Text = Deactive_Concession_Count.Count().ToString();

                gvConcession.DataSource = dt;
                gvConcession.DataBind();
                gvConcession.Visible = true;
                pnlnoRecordfound.Visible = false;

                ddlConcession.DataSource = dt;
                ddlConcession.DataTextField = "concessionname";
                ddlConcession.DataValueField = "concessionid";
                ddlConcession.DataBind();
            }
            else
            {
                gvConcession.Visible = false;
                pnlnoRecordfound.Visible = true;
            }
            ddlConcession.Items.Insert(0, "Select");
            ddlConcession.Items[0].Value = "0";
            ddlConcession.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
            ddlConcession.Items.Insert(0, "Select");
            ddlConcession.Items[0].Value = "0";
            ddlConcession.SelectedIndex = 0;
            _common.ErrorLog("PAdminConcessionPolicy.aspx-0007", ex.Message.ToString());
        }
    }
    protected void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("D", "Please Check", msg, "Close");
        Response.Write(popup);
    }
    protected void Successmsg(string msg)
    {

        string popup = _popup.modalPopupSmall("S", "Congratulations", msg, "Close");
        Response.Write(popup);
    }
    protected void ConfirmMsg(string msg)
    {
        lblConfirmation.Text = msg;
        mpConfirmation.Show();
    }
    protected void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupSmall("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    #endregion

    #region "Events"
    protected void ddlBusPass_SelectedIndexChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        try
        {
            tbconcessionfare.Enabled = false;
            tbconcessiontax.Enabled = false;
            chkGender.Enabled = false;
            ddlGender.Enabled = false;
            lblgender.Visible = false;
            ddlGender.Visible = false;

            chkKms.Enabled = false;
            tbKms.Enabled = false;

            chkAgeGroup.Enabled = false;
            tbMinAge.Enabled = false;
            tbMaxAge.Enabled = false;

            chkServiceType.Enabled = false;
            ddlServiceType.Enabled = false;
            lblservicetype.Visible = false;
            ddlServiceType.Visible = false;

            chkState.Enabled = false;
            chkWithinState.Enabled = false;
            chkOutsideState.Enabled = false;


            DataTable dt;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_bus_pass_type_list_bustypeid");
            MyCommand.Parameters.AddWithValue("p_bustypeid", Convert.ToInt32(ddlBusPass.SelectedValue));

            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                tbconcessionfare.Text = dt.Rows[0]["concessionper_fare"].ToString();
                tbconcessiontax.Text = dt.Rows[0]["concessionper_tax"].ToString();
                tbabbr.Text = dt.Rows[0]["buspass_abbr"].ToString();
                tbRemark.Text = dt.Rows[0]["descriptionen"].ToString();
                if (DBNull.Value.Equals(dt.Rows[0]["ebtmprint_yn"]) == true || dt.Rows[0]["ebtmprint_yn"].ToString() == "")
                {
                    rbtEBTMprintyn.SelectedValue = "N";
                }
                else
                {
                    rbtEBTMprintyn.SelectedValue = dt.Rows[0]["ebtmprint_yn"].ToString();
                }
                if (DBNull.Value.Equals(dt.Rows[0]["online_concession_yn"]) == true || dt.Rows[0]["online_concession_yn"].ToString() == "")
                {
                    rbtapplicableonline.SelectedValue = "N";
                }
                else
                {
                    rbtapplicableonline.SelectedValue = dt.Rows[0]["online_concession_yn"].ToString();
                }

                if (DBNull.Value.Equals(dt.Rows[0]["restrictedtogenderyn"]) == true || dt.Rows[0]["restrictedtogenderyn"].ToString()=="")
                {
                    chkGender.Checked = false;
                    ddlGender.Visible = false;
                }
                else
                {
                    chkGender.Checked = true;
                    ddlGender.SelectedValue = dt.Rows[0]["restrictedtogendertype"].ToString();
                    ddlGender.Visible = true;
                    lblgender.Text = dt.Rows[0]["RESTRICTEDTOGENDER_"].ToString();
                    lblgender.Visible = true;
                    ddlGender.Visible = false;
                }

                if (dt.Rows[0]["restrictedkmsyn"].ToString() == "N")
                {
                    chkKms.Checked = false;
                    tbKms.Text = dt.Rows[0]["restrictednoof_kms"].ToString();
                    tbKms.Visible = false;
                }
                else
                {
                    chkKms.Checked = true;
                    tbKms.Text = dt.Rows[0]["restrictednoof_kms"].ToString();
                    tbKms.Visible = true;
                }

                if (DBNull.Value.Equals(dt.Rows[0]["restrictedtoageyn"]) == true)
                {
                }
                else
                {
                    if (dt.Rows[0]["restrictedagemin"].ToString() == "0" & dt.Rows[0]["restrictedagemax"].ToString() == "0")
                    {
                        chkAgeGroup.Checked = false;
                        pnlAge.Visible = false;
                    }
                    else
                    {
                        chkAgeGroup.Checked = true;
                        pnlAge.Visible = true;
                        tbMinAge.Text = dt.Rows[0]["restrictedagemin"].ToString();
                        tbMaxAge.Text = dt.Rows[0]["restrictedagemax"].ToString();
                    }
                }
                if (dt.Rows[0]["busservicetypes_yn"].ToString() == "N")
                {
                    chkServiceType.Checked = false;
                    ddlServiceType.Visible = false;
                }
                else
                {
                    chkServiceType.Checked = true;
                    ddlServiceType.Visible = true;
                    string[] TypeCode;

                    if (!string.IsNullOrEmpty(dt.Rows[0]["tobusservice_types"].ToString()))
                    {
                        TypeCode = dt.Rows[0]["tobusservice_types"].ToString().Split(',');
                        foreach (ListItem item in ddlServiceType.Items)
                        {
                            foreach (string str in TypeCode)
                            {
                                if (item.Value == str)
                                    item.Selected = true;
                            }
                        }
                    }
                    string ss= dt.Rows[0]["service_typename"].ToString(); 
                    lblservicetype.Text = dt.Rows[0]["service_typename"].ToString();
                    lblservicetype.Visible = true;
                    ddlServiceType.Visible = false;
                   
                }

                if (dt.Rows[0]["allowedinstateyn"].ToString() == "N" & dt.Rows[0]["allowedoutsidestateyn"].ToString() == "N")
                {
                    pnlState.Visible = false;
                    chkState.Checked = false;
                    chkWithinState.Checked = false;
                }
                else if (dt.Rows[0]["allowedinstateyn"].ToString() == "Y" & dt.Rows[0]["allowedoutsidestateyn"].ToString() == "Y")
                {
                    pnlState.Visible = false;
                    chkState.Checked = false;
                    chkWithinState.Checked = false;
                }
                else if (dt.Rows[0]["allowedinstateyn"].ToString() == "Y" & dt.Rows[0]["allowedoutsidestateyn"].ToString() == "N")
                {
                    pnlState.Visible = true;
                    chkState.Checked = true;
                    chkWithinState.Checked = true;
                }
                else if (dt.Rows[0]["allowedinstateyn"].ToString() == "N" & dt.Rows[0]["allowedoutsidestateyn"].ToString() == "Y")
                {
                    pnlState.Visible = true;
                    chkState.Checked = true;
                    chkOutsideState.Checked = true;
                }



                string documentidprooffornewtypes = dt.Rows[0]["docid_new_types"].ToString();
                Session["documenttypes"] = dt.Rows[0]["docid_new_types"].ToString();
                if (Session["documenttypes"].ToString() == "")
                {
                    Session["documenttypes"] = "0";
                }
            }
            else if (ddlBusPass.SelectedValue == "0")
                resetcontrol();
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message.ToString());
        }
    }
    protected void chkGender_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (chkGender.Checked == true)
            ddlGender.Visible = true;
        else
            ddlGender.Visible = false;
    }
    protected void chkKms_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (chkKms.Checked == true)
            tbKms.Visible = true;
        else
            tbKms.Visible = false;
    }
    protected void chkAgeGroup_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (chkAgeGroup.Checked == true)
            pnlAge.Visible = true;
        else
            pnlAge.Visible = false;
    }
    protected void chkServiceType_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (chkServiceType.Checked == true)
            ddlServiceType.Visible = true;
        else
            ddlServiceType.Visible = false;
    }
    protected void chkState_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (chkState.Checked == true)
            pnlState.Visible = true;
        else
            pnlState.Visible = false;
    }
    protected void chkonlineverification_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        chkidverification.Checked = false;
        chkdocumentverification.Checked = false;
        ddlidverification.Visible = false;
        ddlidverification.Items.Clear();
        ddldocumentverification.Visible = false;
        ddldocumentverification.Items.Clear();
    }
    protected void chkidverification_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        chkdocumentverification.Checked = false;
        chkonlineverification.Checked = false;
        ddlidverification.Visible = true;
        ddldocumentverification.Visible = false;
        if (ddlBusPass.SelectedValue != "0" && ddlBusPass.SelectedValue != "")
        {
            ddlidverification.Items.Clear();
            if (Session["documenttypes"].ToString() != null)
            {
                ddlidverification.DataSource = GetIDproof("1", Session["documenttypes"].ToString());
                ddlidverification.DataTextField = "document_type_name";
                ddlidverification.DataValueField = "document_type_id";
                ddlidverification.DataBind();
            }
        }
        //ddlidverification.Items.Insert(0, "SELECT");
        //ddlidverification.Items[0].Value = "0";
        //ddlidverification.SelectedIndex = 0;
    }
    protected void chkdocumentverification_CheckedChanged(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        chkonlineverification.Checked = false;
        chkidverification.Checked = false;
        ddlidverification.Visible = false;
        ddldocumentverification.Visible = true;
        if (ddlBusPass.SelectedValue != "0")
        {
            ddldocumentverification.Items.Clear();
            if (Session["documenttypes"].ToString() != null)
            {
                ddldocumentverification.DataSource = GetIDproof("1", Session["documenttypes"].ToString());
                ddldocumentverification.DataTextField = "document_type_name";
                ddldocumentverification.DataValueField = "document_type_id";
                ddldocumentverification.DataBind();
            }
        }
        //ddldocumentverification.Items.Insert(0, "SELECT");
        //ddldocumentverification.Items[0].Value = "0";
        //ddldocumentverification.SelectedIndex = 0;
    }
    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        resetcontrol();
    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Validaion() == false)
            return;
        Session["ConcessionId"] = 0;
        ConfirmMsg("Do you Want to Add Concession Policy?");
        Session["_action"] = "S";
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        saveConcession();
    }
    protected void gvConcession_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvConcession.PageIndex = e.NewPageIndex;
        ConcessionList();
    }
    protected void gvConcession_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnactivate = (LinkButton)e.Row.FindControl("lbtnactivate");
            LinkButton lbtnDeactivate = (LinkButton)e.Row.FindControl("lbtnDeactivate");
            LinkButton lbtnupdate = (LinkButton)e.Row.FindControl("lbtnUpdateConcession");
            LinkButton lbtndelete = (LinkButton)e.Row.FindControl("lbtndelete");
            lbtnactivate.Visible = false;
            lbtnDeactivate.Visible = false;
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            if (rowView["CURRENT_STATUS"].ToString() == "A")
                lbtnDeactivate.Visible = true;
            else if (rowView["CURRENT_STATUS"].ToString() == "D")
                lbtnactivate.Visible = true;
            if (rowView["concessionid"].ToString() == "1")
            {
                lbtnactivate.Visible = false;
                lbtnDeactivate.Visible = false;
                lbtnupdate.Visible = false;
                lbtndelete.Visible = false;
            }
        }
    }
    protected void lbtnUpdate_Click(object sender, EventArgs e)
    {
        CsrfTokenValidate();
        if (Validaion() == false)
            return;
        ConfirmMsg("Do you Want to Update Concession Policy?");
        Session["_action"] = "U";
    }
    protected void gvConcession_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CsrfTokenValidate();
        if (e.CommandName == "updateconcession")
        {
            resetcontrol();
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            lblConcessionHead.Text = "Update Concession Details";
            lbtnSave.Visible = false;
            lbtnUpdate.Visible = true;

            Session["ConcessionId"] = gvConcession.DataKeys[row.RowIndex]["concessionid"].ToString();
            tbConsessionName.Text = gvConcession.DataKeys[row.RowIndex]["concessionname"].ToString();
            tbConsessionName.Enabled = false;
            tbConsessionNameLocal.Text = gvConcession.DataKeys[row.RowIndex]["concession_namelocal"].ToString();
            tbConsessionNameLocal.Enabled = false;
            tbabbr.Text = gvConcession.DataKeys[row.RowIndex]["abbrevation"].ToString(); 
            tbRemark.Text = gvConcession.DataKeys[row.RowIndex]["remark"].ToString();
            string fx= gvConcession.DataKeys[row.RowIndex]["buspass_type"].ToString();
            BusPassTypeList();
            try
            {
                ddlBusPass.SelectedValue = gvConcession.DataKeys[row.RowIndex]["buspass_type"].ToString();
            }
            catch (Exception ex)
            { }

            rbtEBTMprintyn.SelectedValue= gvConcession.DataKeys[row.RowIndex]["ebtmprintyn"].ToString();

            if (ddlBusPass.SelectedValue != "0")
                ddlBusPass_SelectedIndexChanged(ddlBusPass, EventArgs.Empty);
            else
            {
                tbconcessionfare.Text = gvConcession.DataKeys[row.RowIndex]["concessionper_fare"].ToString();
                tbconcessiontax.Text = gvConcession.DataKeys[row.RowIndex]["concessionper_tax"].ToString();
                string specific_genderyn = gvConcession.DataKeys[row.RowIndex]["genderyn"].ToString();
                if (specific_genderyn == "Y")
                {
                    string specific_gendertype = gvConcession.DataKeys[row.RowIndex]["gendertype"].ToString();
                    chkGender.Checked = true;
                    ddlGender.Visible = true;
                    string[] TypeCode;
                    if (!string.IsNullOrEmpty(specific_gendertype))
                    {
                        TypeCode = specific_gendertype.Split(',');
                        foreach (ListItem item in ddlGender.Items)
                        {
                            foreach (string str in TypeCode)
                            {
                                if (item.Value == str)
                                    item.Selected = true;
                            }
                        }
                    }
                }
                string no_of_kmsyn = gvConcession.DataKeys[row.RowIndex]["noofkmsyn"].ToString();
                if (no_of_kmsyn == "Y")
                {
                    chkKms.Checked = true;
                    tbKms.Text = gvConcession.DataKeys[row.RowIndex]["noofkms"].ToString();
                    tbKms.Visible = true;
                }
                string AGEGROUP_YN = gvConcession.DataKeys[row.RowIndex]["agegroupyn"].ToString();
                if (AGEGROUP_YN == "Y")
                {
                    chkAgeGroup.Checked = true;
                    pnlAge.Visible = true;
                    tbMinAge.Text = gvConcession.DataKeys[row.RowIndex]["minage"].ToString();
                    tbMaxAge.Text = gvConcession.DataKeys[row.RowIndex]["maxage"].ToString();
                }
                string servicetypeyn = gvConcession.DataKeys[row.RowIndex]["servicetypeyn"].ToString();
                if (servicetypeyn == "Y")
                {
                    string service_typecode = gvConcession.DataKeys[row.RowIndex]["service_typecode"].ToString();
                    chkServiceType.Checked = true;
                    ddlServiceType.Visible = true;
                    string[] ServiceTypeCode;
                    if (!string.IsNullOrEmpty(service_typecode))
                    {
                        ServiceTypeCode = service_typecode.Split(',');
                        foreach (ListItem item in ddlServiceType.Items)
                        {
                            foreach (string str in ServiceTypeCode)
                            {
                                if (item.Value == str)
                                    item.Selected = true;
                            }
                        }
                    }
                }
                string stateyn = gvConcession.DataKeys[row.RowIndex]["stateyn"].ToString();
                if (stateyn == "Y")
                {
                    chkState.Checked = true;
                    pnlState.Visible = true;
                    string WITHINSTATE_YN = gvConcession.DataKeys[row.RowIndex]["within_statyn"].ToString();
                    if (WITHINSTATE_YN == "Y")
                        chkWithinState.Checked = true;
                    string OTHERSTATE_YN = gvConcession.DataKeys[row.RowIndex]["otherstateyn"].ToString();
                    if (OTHERSTATE_YN == "Y")
                        chkOutsideState.Checked = true;
                }
            }
            string ADDITIONALATTENDENT_YN = gvConcession.DataKeys[row.RowIndex]["additionalattendentyn"].ToString();
            if (ADDITIONALATTENDENT_YN == "Y")
                chkAttendant.Checked = true;
            string OTHERCONCESSION_YN = gvConcession.DataKeys[row.RowIndex]["otherconcession_yn"].ToString();
            if (OTHERCONCESSION_YN == "Y")
                chkallowotherconcession.Checked = true;

            string ONLINEVERIFICATION_YN = gvConcession.DataKeys[row.RowIndex]["onlineverificationyn"].ToString();
            if (ONLINEVERIFICATION_YN == "Y")
                chkonlineverification.Checked = true;
            string IDVERIFICATION_YN = gvConcession.DataKeys[row.RowIndex]["idverificationyn"].ToString();
            if (IDVERIFICATION_YN == "Y")
            {
                chkidverification.Checked = true;
                ddlidverification.Visible = true;
                string IDVERIFICATION = gvConcession.DataKeys[row.RowIndex]["id_verificationyn"].ToString();
                string[] idverificationCode;
                if (!string.IsNullOrEmpty(IDVERIFICATION))
                {
                    idverificationCode = IDVERIFICATION.Split(',');
                    foreach (ListItem item in ddlidverification.Items)
                    {
                        foreach (string str in idverificationCode)
                        {
                            if (item.Value == str)
                                item.Selected = true;
                        }
                    }
                }
            }
            string DOCUMENTVERIFICATION_YN = gvConcession.DataKeys[row.RowIndex]["documentverificationyn"].ToString();
            if (DOCUMENTVERIFICATION_YN == "Y")
            {
                chkdocumentverification.Checked = true;
                ddldocumentverification.Visible = true;
                string DOCUMENTVERIFICATION = gvConcession.DataKeys[row.RowIndex]["document_verification"].ToString();
                string[] DocverificationCode;
                if (!string.IsNullOrEmpty(DOCUMENTVERIFICATION))
                {
                    DocverificationCode = DOCUMENTVERIFICATION.Split(',');
                    foreach (ListItem item in ddldocumentverification.Items)
                    {
                        foreach (string str in DocverificationCode)
                        {
                            if (item.Value == str)
                                item.Selected = true;
                        }
                    }
                }
            }
        }
        if (e.CommandName == "activate")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Session["ConcessionId"] = gvConcession.DataKeys[row.RowIndex]["concessionid"].ToString();
            Session["_action"] = "A";
            ConfirmMsg("Do you want to Activate Concession Policy ?");
        }
        if (e.CommandName == "deactivate")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Session["ConcessionId"] = gvConcession.DataKeys[row.RowIndex]["concessionid"].ToString();
            Session["_action"] = "D";
            ConfirmMsg("Do you want to Discontinue Concession Policy?");
        }
        //if (e.CommandName == "deleted")
        //{
        //    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
        //    Session["ConcessionId"] = gvConcession.DataKeys[row.RowIndex]["concessionid"].ToString();
        //    Session["_action"] = "X";
        //    ConfirmMsg("Do you want to Deleted Concession Policy?");
        //}
    }

    #endregion

}