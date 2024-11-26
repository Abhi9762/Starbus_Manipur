using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_SysAdmETMRegistration : System.Web.UI.Page
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
        lblSummary.Text = "Summary as on Date " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
        Session["_moduleName"] = "ETM Management";
        if (IsPostBack == false)
        {
            LoadStore(ddlRecStore);
            ddlRecStore.SelectedValue = Session["StoreId"].ToString();
            ddlRecStore.Enabled = false;
            LoadAgencies();
            LoadETMmakeModel();
            LoadETMtype();
            LoadETMStatus();
            loadEtmCount();                  
            loadEtmDetails();
            Session["Invoice"] = null;
            LoadETMBranch();
            LoadOtherStore();
        }
       

    }
    #region "Methods"
    public void LoadStore(DropDownList ddlStoreOfc)
    {
        try
        {
            ddlStoreOfc.Items.Clear();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_office");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlStoreOfc.DataSource = dt;
                ddlStoreOfc.DataTextField = "officename";
                ddlStoreOfc.DataValueField = "officeid";
                ddlStoreOfc.DataBind();
            }
            ddlStoreOfc.Items.Insert(0, "SELECT");
            ddlStoreOfc.Items[0].Value = "0";
            ddlStoreOfc.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlStoreOfc.Items.Insert(0, "SELECT");
            ddlStoreOfc.Items[0].Value = "0";
            ddlStoreOfc.SelectedIndex = 0;
            _common.ErrorLog("SysAdmETM-M4", ex.Message.ToString());
        }
    }
    public void LoadETMmakeModel()//M1
    {
        try
        {
            ddlMakeModel.Items.Clear();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_etmmakemodel");

            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlMakeModel.DataSource = dt;
                ddlMakeModel.DataTextField = "etmmakemodelname";
                ddlMakeModel.DataValueField = "etmmakemodelid";
                ddlMakeModel.DataBind();
            }
            ddlMakeModel.Items.Insert(0, "SELECT");
            ddlMakeModel.Items[0].Value = "0";
            ddlMakeModel.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlMakeModel.Items.Insert(0, "SELECT");
            ddlMakeModel.Items[0].Value = "0";
            ddlMakeModel.SelectedIndex = 0;
            _common.ErrorLog("SysAdmETM-M1", ex.Message.ToString());
        }
    }
    public void LoadETMtype()//M2
    {
        try
        {
            ddlETMType.Items.Clear();
            ddlETMType.Items.Clear();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_etmtype");

            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlETMType.DataSource = dt;
                ddlETMType.DataTextField = "etmtype_name";
                ddlETMType.DataValueField = "etmtype_id";
                ddlETMType.DataBind();


            }
            ddlETMType.Items.Insert(0, "SELECT");
            ddlETMType.Items[0].Value = "0";
            ddlETMType.SelectedIndex = 0;


        }
        catch (Exception ex)
        {
            ddlETMType.Items.Insert(0, "SELECT");
            ddlETMType.Items[0].Value = "0";
            ddlETMType.SelectedIndex = 0;


            _common.ErrorLog("SysAdmETM-M2", ex.Message.ToString());
        }
    }
    public void LoadETMStatus()//M3
    {
        try
        {
            ddlStatus.Items.Clear();
            string statustype = "O";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getetmstatus");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlStatus.DataSource = dt;
                ddlStatus.DataTextField = "status_name";
                ddlStatus.DataValueField = "status_id";
                ddlStatus.DataBind();
            }

        }
        catch (Exception ex)
        {
            ddlStatus.Items.Insert(0, "SELECT");
            ddlStatus.Items[0].Value = "0";
            ddlStatus.SelectedIndex = 0;
            _common.ErrorLog("SysAdmETM-M3", ex.Message.ToString());
        }
    }
    public void LoadAgencies()//M4
    {
        try
        {
            ddlAgency.Items.Clear();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_agencies_fordropdown");
            MyCommand.Parameters.AddWithValue("p_itemid", "1");
            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlAgency.DataSource = dt;
                ddlAgency.DataTextField = "agencyname";
                ddlAgency.DataValueField = "agency_id";
                ddlAgency.DataBind();
            }
            ddlAgency.Items.Insert(0, "SELECT");
            ddlAgency.Items[0].Value = "0";
            ddlAgency.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlAgency.Items.Insert(0, "SELECT");
            ddlAgency.Items[0].Value = "0";
            ddlAgency.SelectedIndex = 0;
            _common.ErrorLog("SysAdmETM-M4", ex.Message.ToString());
        }
    }
    public void LoadETMBranch()//M5
    {
        try
        {
            ddlETMBranch.Items.Clear();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_etmstore");
            MyCommand.Parameters.AddWithValue("@p_storeofficelvl", Session["StoreOfficeLvl"].ToString());
            MyCommand.Parameters.AddWithValue("@p_storeid", Session["StoreOfficeid"].ToString());

            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {
                ddlETMBranch.DataSource = dt;
                ddlETMBranch.DataTextField = "officename";
                ddlETMBranch.DataValueField = "officeid";
                ddlETMBranch.DataBind();
           


            }
          
          
            ddlETMBranch.Items.Insert(0, "SELECT");
            ddlETMBranch.Items[0].Value = "0";
            ddlETMBranch.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlETMBranch.Items.Insert(0, "SELECT");
            ddlETMBranch.Items[0].Value = "0";
            ddlETMBranch.SelectedIndex = 0;
            _common.ErrorLog("SysAdmETM-M5", ex.Message.ToString());
        }
    }


    public void LoadOtherStore()//M5
    {
        try
        {
            ddlotherStore.Items.Clear();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_otherstore");

            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {

                ddlotherStore.DataSource = dt;
                ddlotherStore.DataTextField = "officename";
                ddlotherStore.DataValueField = "officeid";
                ddlotherStore.DataBind();
              
            }
          
            ddlotherStore.Items.Insert(0, "SELECT");
            ddlotherStore.Items[0].Value = "0";
            ddlotherStore.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ddlotherStore.Items.Insert(0, "SELECT");
            ddlotherStore.Items[0].Value = "0";
            ddlotherStore.SelectedIndex = 0;
            _common.ErrorLog("SysAdmETM-M5", ex.Message.ToString());
        }
    }


    public void loadEtmDetails()//M6
    {
        try
        {

            Int32 etmStatus = Convert.ToInt16(ddlStatus.SelectedValue);

            gvETMDetails.Visible = false;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getetmdetails");
            MyCommand.Parameters.AddWithValue("p_status", etmStatus);

            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {

                gvETMDetails.DataSource = dt;
                gvETMDetails.DataBind();
                gvETMDetails.Visible = true;
                pnlMsg.Visible = false;
                GetEtmDtails.Visible = true;
                gvETMDetails.UseAccessibleHeader = true;
                gvETMDetails.HeaderRow.TableSection = TableRowSection.TableHeader;


            }

            else
            {
                pnlMsg.Visible = true;
                GetEtmDtails.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
            _common.ErrorLog("SysAdmETM-M6", ex.Message.ToString());
        }
    }
    public void loadEtmCount()//M6
    {
        try
        {

            gvETMDetails.Visible = false;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_getetmCount");

            dt = bll.SelectAll(MyCommand);
            if (dt.Rows.Count > 0)
            {

                lbtnAvailableforissuance.Text = dt.Rows[0]["Available_forissuance"].ToString();
                lbtnPendingforlock.Text = dt.Rows[0]["Pending_forlock"].ToString();
                lbtnissuedetm.Text = dt.Rows[0]["issuedto_etmbranchstore"].ToString();
            }
        }
        catch (Exception ex)
        {
            Errormsg(ex.Message);
            _common.ErrorLog("SysAdmETM-M6", ex.Message.ToString());
        }
    }

    public void clearData()
    {
        lblETMHead.Text = "Add ETM Details";
        ddlETMType.SelectedValue = "0";
        tbSerial.Text = "";
        ddlMakeModel.SelectedValue = "0";
        ddlPurchaseMode.SelectedValue = "0";
        tbIMEI1.Text = "";
        tbIMEI2.Text = "";
        ddlAgency.SelectedValue = "0";
        tbInvoiceNo.Text = "";
        tbInvoiceDate.Text = "";
        tbAmount.Text = "";
        //ddlRecStore.SelectedValue = "0";
        tbReceiveBy.Text = "";
        tbReceivedOnDate.Text = "";
        Session["_etmRefNo"] = "";
        Session["_action"] = "";
        Session["Invoice"] = "";
        ImgInvoice.Visible = false;
        pnlAddETM.Visible = true;

        lbtnSave.Visible = true;
        lbtnSaveLock.Visible = true;
        lbtnUpdate.Visible = false;
        lbtnReset.Visible = true;
        lbtnCancel.Visible = false;
    }
    private Boolean validETM()
    {
        try
        {
            int count = 0;
            string msg = "";


            if (ddlETMType.SelectedValue == "0")
            {
                count++;
                msg = msg + count + ". Select ETM Type <br/>";
            }
            if (_validation.IsValidString(tbSerial.Text, 5, 50) == false)
            {
                count++;
                msg = msg + count + ". Enter Valid ETM Serial No.<br/>";
            }

            if (_validation.IsValidString(tbReceiveBy.Text, 5, 50) == false)
            {
                count++;
                msg = msg + count + ".  Enter Valid Receive By Name<br/>";
            }
            if (ddlETMType.SelectedValue == "1")
            {
                if (_validation.IsValidString(tbIMEI1.Text, 15, tbIMEI1.MaxLength) == false)
                {
                    count++;
                    msg = msg + count + ".  Enter Valid IMEI No.1<br/>";
                }
                if (_validation.IsValidString(tbIMEI2.Text, 0, tbIMEI2.MaxLength) == false)
                {
                    count++;
                    msg = msg + count + ".  Enter Valid IMEI No.2<br/>";
                }
            }
            if (tbInvoiceDate.Text == "")
            { }
            else
            {
                if (_validation.IsDate(tbInvoiceDate.Text) == false)
                {
                    count++;
                    msg = msg + count + ".  Enter Valid Invoice Date<br/>";
                }
            }
            if (_validation.IsDate(tbReceivedOnDate.Text) == false)
            {
                count++;
                msg = msg + count + ". Enter Valid Date Of ETM Receive <br/>";
            }

            if (_validation.IsValidString(tbInvoiceNo.Text, 0, 50) == false)
            {
                count++;
                msg = msg + count + ". Enter Valid Invoice No <br/>";
            }
            if (_validation.IsValidString(tbAmount.Text, 0, 50) == false)
            {
                count++;
                msg = msg + count + ". Enter Valid Amount <br/>";
            }
            if (ddlAgency.SelectedValue == "0")
            {
                count++;
                msg = msg + count + ". Select Agency Type <br/>";
            }


            if (ddlMakeModel.SelectedValue == "0")
            {
                count++;
                msg = msg + count + ". Select ETM Make Model <br/>";
            }
            if (ddlPurchaseMode.SelectedValue == "0")
            {
                count++;
                msg = msg + count + ". Select ETM Purchase Mode <br/>";
            }
            if (ddlRecStore.SelectedValue == "0")
            {
                count++;
                msg = msg + count + ". Select ETM Receive At <br/>";
            }
            if (_validation.IsDate(tbReceivedOnDate.Text) == true)
            {
                DateTime receivedOnDate = DateTime.ParseExact(tbReceivedOnDate.Text, "dd/MM/yyyy", null);
                if (receivedOnDate > DateTime.Now.Date)
                {
                    count = count + 1;
                    msg = msg + count.ToString() + ". Receiving Date should be less than today's date.<br/>";
                }
            }
            if (_validation.IsDate(tbInvoiceDate.Text) == true)
            {
                DateTime invoiceDate = DateTime.ParseExact(tbInvoiceDate.Text, "dd/MM/yyyy", null);
                if (invoiceDate > DateTime.Now.Date)
                {
                    count = count + 1;
                    msg = msg + count.ToString() + ". Invoice Date should be less than today's date.<br/>";
                }
            }
            if (_validation.IsDate(tbReceivedOnDate.Text) == true && _validation.IsDate(tbInvoiceDate.Text) == true)
            {
                DateTime receivedOnDate = DateTime.ParseExact(tbReceivedOnDate.Text, "dd/MM/yyyy", null);
                DateTime invoiceDate = DateTime.ParseExact(tbInvoiceDate.Text, "dd/MM/yyyy", null);
                if (invoiceDate > receivedOnDate)
                {
                    count = count + 1;
                    msg = msg + count.ToString() + ". Receiving Date should be less than invoice date.<br/>";
                }
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

            return false;
        }
    }
    private void saveETMDetails()//M7
    {
        try
        {
            string etmRefNo = Session["_etmRefNo"].ToString();
            Int32 etmType = Convert.ToInt32(ddlETMType.SelectedValue.ToString());
            string serialno = tbSerial.Text.ToString();
            string IMEI1 = tbIMEI1.Text.ToString();
            string IMEI2 = tbIMEI2.Text.ToString();
            string otherModel = tbMakeModel.Text.ToString();
            Int32 makemodel = Convert.ToInt32(ddlMakeModel.SelectedValue.ToString());
            string purchaseMode = ddlPurchaseMode.SelectedValue.ToString();
            Int32 agency = Convert.ToInt32(ddlAgency.SelectedValue.ToString());
            string invoiceNo = tbInvoiceNo.Text.ToString();
            string invoiceDate = tbInvoiceDate.Text.ToString();
            string amount = tbAmount.Text.ToString();
            string recStore = ddlRecStore.SelectedValue.ToString();
            string receivedBy = tbReceiveBy.Text.ToString();
            string receiveOn = tbReceivedOnDate.Text.ToString();
            int assignedoffice = Convert.ToInt32(Session["assignedoffice"]);
            byte[] bytes = null;
            bytes = Session["Invoice"] as byte[];
            //byte[] bytes = (byte[])Session["Invoice"];
            string UpdatedBy = Session["_UserCode"].ToString();
            string IpAddress = HttpContext.Current.Request.UserHostAddress;
            string action = Session["_action"].ToString();
            string result = "";
            NpgsqlParameter spResult;
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_etminsert");
            MyCommand.Parameters.AddWithValue("p_action", action);
            MyCommand.Parameters.AddWithValue("p_etmrefno", etmRefNo);
            MyCommand.Parameters.AddWithValue("p_etmtype", etmType);
            MyCommand.Parameters.AddWithValue("p_serialno", serialno);
            MyCommand.Parameters.AddWithValue("p_imei1", IMEI1);
            MyCommand.Parameters.AddWithValue("p_imei2", IMEI2);
            MyCommand.Parameters.AddWithValue("p_makemodel", makemodel);
            MyCommand.Parameters.AddWithValue("p_othermodel", otherModel);
            MyCommand.Parameters.AddWithValue("p_purchasemode", purchaseMode);
            MyCommand.Parameters.AddWithValue("p_agency", agency);
            MyCommand.Parameters.AddWithValue("p_invoiceno", invoiceNo);
            MyCommand.Parameters.AddWithValue("p_invoicedate", invoiceDate);
            MyCommand.Parameters.AddWithValue("p_amount", amount);
            MyCommand.Parameters.AddWithValue("p_recstore", recStore);
            MyCommand.Parameters.AddWithValue("p_recby", receivedBy);
            MyCommand.Parameters.AddWithValue("p_recondate", receiveOn);
            MyCommand.Parameters.AddWithValue("p_updatedby", UpdatedBy);
            MyCommand.Parameters.AddWithValue("p_ipaddress", IpAddress);
            MyCommand.Parameters.AddWithValue("p_assignedoffice", assignedoffice);
            MyCommand.Parameters.AddWithValue("p_invoice", (object)bytes ?? DBNull.Value);
 
            dt = bll.SelectAll(MyCommand);


            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["result"].ToString() == "Y")
                {
                    if (Session["_action"].ToString() == "L")
                    {
                        Successmsg("ETM details has been locked Successfully");
                        gvETMDetails.Visible = true;
                    }
                    else if (Session["_action"].ToString() == "U")
                    {
                        Successmsg("ETM updated Successfully");
                    }
                    else if (Session["_action"].ToString() == "D")
                    {
                        Successmsg("ETM Deleted Successfully");
                    }
                    else if (Session["_action"].ToString() == "SD")
                    {
                        Successmsg("ETM details has been saved as draft");
                    }
                    else if (Session["_action"].ToString() == "I")
                    {
                        Successmsg("ETM Issued Successfully");
                    }
                    else if (Session["_action"].ToString() == "T")
                    {
                        Successmsg("ETM Transfered Successfully");
                    }
                    else
                    {
                        Successmsg("ETM details has been Saved & Locked successfully");
                    }
                    Session["_etmRefNo"] = "";
                    Session["_action"] = "";
                    Session["assignedoffice"] = "";
                    clearData();
                    loadEtmCount();
                    loadEtmDetails();
                }
                else
                {
                    result = dt.Rows[0]["result"].ToString();
                    Errormsg(result);
                }
            }
            else
            {
                Errormsg("There is some error1.");
            }
        }


        catch (Exception ex)
        {
            Errormsg("There is some error2." + ex.Message.ToString());
            _common.ErrorLog("SysAdmETM-M7", ex.Message.ToString());
        }
    }


    private void Errormsg(string msg)
    {
        string popup = _popup.modalPopupSmall("W", "Please Note", msg, "Close");
        Response.Write(popup);
    }
    private void Successmsg(string msg)
    {
        string popup = _popup.modalPopupSmall("S", "Success", msg, "OK");
        Response.Write(popup);
    }
    private void ConfirmMsg(string msg)
    {
        lblConfirmation.Text = msg;
        mpConfirmation.Show();
    }
    private void InfoMsg(string msg)
    {
        string popup = _popup.modalPopupSmall("I", "Information", msg, "Close");
        Response.Write(popup);
    }
    #endregion
    #region "Events"
    protected void lbtndwnldinst_Click(object sender, EventArgs e)
    {
        string msg = "";
        msg = msg + "Coming Soon";
        InfoMsg(msg);
    }
    protected void gvETMDetails_RowCommand(object sender, GridViewCommandEventArgs e)//E1
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "updateETM")
            {
                String etmrefno = gvETMDetails.DataKeys[index].Values["etmrefno"].ToString();
                Session["_etmRefNo"] = etmrefno;
                lblETMHead.Text = "Update ETM Details of " + gvETMDetails.DataKeys[index].Values["etmtypename"].ToString() + "-" + gvETMDetails.DataKeys[index].Values["etmserialno"].ToString();
                ddlETMType.SelectedValue = gvETMDetails.DataKeys[index].Values["etmtype"].ToString();
                tbSerial.Text = gvETMDetails.DataKeys[index].Values["etmserialno"].ToString();
                ddlMakeModel.SelectedValue = gvETMDetails.DataKeys[index].Values["etmmake"].ToString();
                ddlPurchaseMode.SelectedValue = gvETMDetails.DataKeys[index].Values["purchasemode"].ToString();
                divIMEI1.Visible = false;
                divIMEI2.Visible = false;
                if (ddlETMType.SelectedValue == "1")
                {
                    divIMEI1.Visible = true;
                    divIMEI2.Visible = true;
                }
                tbIMEI1.Text = gvETMDetails.DataKeys[index].Values["imeino1"].ToString();
                tbIMEI2.Text = gvETMDetails.DataKeys[index].Values["imeino2"].ToString();
                ddlAgency.SelectedValue = gvETMDetails.DataKeys[index].Values["agencyid"].ToString();
                tbInvoiceNo.Text = gvETMDetails.DataKeys[index].Values["invoice_no"].ToString();
                tbInvoiceDate.Text = gvETMDetails.DataKeys[index].Values["invoice_date"].ToString();
                tbAmount.Text = gvETMDetails.DataKeys[index].Values["invoice_amount"].ToString();
                ddlRecStore.SelectedValue = Session["StoreId"].ToString();
                ddlRecStore.Enabled = false;
                ddlRecStore.SelectedValue = gvETMDetails.DataKeys[index].Values["rec_office"].ToString();
                tbReceiveBy.Text = gvETMDetails.DataKeys[index].Values["receivedby"].ToString();
                tbReceivedOnDate.Text = gvETMDetails.DataKeys[index].Values["receivingdate"].ToString();

                if (gvETMDetails.DataKeys[index].Values["invoice_doc"].ToString() != "")
                {
                    byte[] PhotoImage = (byte[])gvETMDetails.DataKeys[index].Values["invoice_doc"];
                    ImgInvoice.ImageUrl = GetImage(PhotoImage);
                    ImgInvoice.Visible = true;
                    Session["Invoice"] = PhotoImage;
                }
                else
                {
                    ImgInvoice.Visible = false;
                    Session["Invoice"] = null;
                }

                Session["_action"] = "U";
                lbtnSave.Visible = false;
                lbtnSaveLock.Visible = false;
                lbtnUpdate.Visible = true;
                lbtnReset.Visible = false;
                lbtnCancel.Visible = true;

                pnlAddETM.Visible = true;
                grdpara.Visible = false;
                GetEtmDtails.Visible = false;

                gvETMDetails.Visible = false;
            }
            else if (e.CommandName == "viewETM")
            {
                string etmRefNo = gvETMDetails.DataKeys[index].Values["etmrefno"].ToString();
                Session["_etmRefNo"] = etmRefNo;


                //if (gvETMDetails.DataKeys[index].Values["etmtypename"].ToString() != "")
                //{
                //    lblETMName.Text = "Details of ETM (" + gvETMDetails.DataKeys[index].Values["etmtypename"].ToString() + " - " + gvETMDetails.DataKeys[index].Values["etmserialno"].ToString() + ")";
                //}
                //if (gvETMDetails.DataKeys[index].Values["makemodel"].ToString() != "")
                //{
                //    lblmakemodelname.Text = gvETMDetails.DataKeys[index].Values["makemodel"].ToString();
                //}
                //if (gvETMDetails.DataKeys[index].Values["purchasemode"].ToString() != "")
                //{
                //    if (gvETMDetails.DataKeys[index].Values["purchasemode"].ToString() == "P")
                //    {
                //        lblPurchaseMode.Text = "Purchase";
                //    }
                //    else
                //    {
                //        lblPurchaseMode.Text = "Rent";
                //    }
                //}
                //if (gvETMDetails.DataKeys[index].Values["imeino1"].ToString() != "")
                //{
                //    lblIMEI1.Text = gvETMDetails.DataKeys[index].Values["imeino1"].ToString();
                //}
                //if (gvETMDetails.DataKeys[index].Values["imeino2"].ToString() != "")
                //{
                //    lblIMEI2.Text = gvETMDetails.DataKeys[index].Values["imeino2"].ToString();
                //}
                //if (gvETMDetails.DataKeys[index].Values["agency"].ToString() != "")
                //{
                //    lblAgency.Text = gvETMDetails.DataKeys[index].Values["agency"].ToString();
                //}
                //if (gvETMDetails.DataKeys[index].Values["invoice_no"].ToString() != "")
                //{
                //    lblInvoiceNo.Text = gvETMDetails.DataKeys[index].Values["invoice_no"].ToString();
                //}
                //if (gvETMDetails.DataKeys[index].Values["invoice_date"].ToString() != "")
                //{
                //    lblInvoiceDate.Text = gvETMDetails.DataKeys[index].Values["invoice_date"].ToString();
                //}
                //if (gvETMDetails.DataKeys[index].Values["invoice_amount"].ToString() != "")
                //{
                //    lblAmount.Text = gvETMDetails.DataKeys[index].Values["invoice_amount"].ToString();
                //}
                //if (gvETMDetails.DataKeys[index].Values["rec_officename"].ToString() != "")
                //{
                //    lblRecStore.Text = gvETMDetails.DataKeys[index].Values["rec_officename"].ToString();
                //}
                //if (gvETMDetails.DataKeys[index].Values["receivedby"].ToString() != "")
                //{
                //    lblReceivedBy.Text = gvETMDetails.DataKeys[index].Values["receivedby"].ToString();
                //}
                //if (gvETMDetails.DataKeys[index].Values["receivingdate"].ToString() != "")
                //{
                //    lblReceiveOn.Text = gvETMDetails.DataKeys[index].Values["receivingdate"].ToString();
                //}
                //if (gvETMDetails.DataKeys[index].Values["issued_office"].ToString() != "")
                //{
                //    lblissuestore.Text = gvETMDetails.DataKeys[index].Values["issued_office"].ToString();
                //}
                //pnlAddETM.Visible = false;
                mpview.Show();
            }
            else if (e.CommandName == "issueETM")
            {
                string etmRefNo = gvETMDetails.DataKeys[index].Values["etmrefno"].ToString();
                Session["_etmRefNo"] = etmRefNo;
                Session["_action"] = "I";
                Session["Invoice"] = null;
                LoadETMBranch();
                mpissuetm.Show();
            }
            else if (e.CommandName == "TransferETM")
            {
                string etmRefNo = gvETMDetails.DataKeys[index].Values["etmrefno"].ToString();
                Session["_etmRefNo"] = etmRefNo;
                Session["_action"] = "T";
                Session["Invoice"] = null;
                LoadOtherStore();
                mptransfetm.Show();
            }
            else if (e.CommandName == "LockETM")
            {
                string etmRefNo = gvETMDetails.DataKeys[index].Values["etmrefno"].ToString();
                Session["_etmRefNo"] = etmRefNo;
                Session["_action"] = "L";
                Session["Invoice"] = null;
                ConfirmMsg("Do you want to lock ETM details?");

            }
            else if (e.CommandName == "deleteETM")
            {
                string etmRefNo = gvETMDetails.DataKeys[index].Values["etmrefno"].ToString();
                Session["_etmRefNo"] = etmRefNo;
                Session["_action"] = "D";
                Session["Invoice"] = null;
                ConfirmMsg("Do you want to delete ETM details?");
            }
        }
        catch (Exception ex)
        {
            _common.ErrorLog("SysAdmETM-E1", ex.Message.ToString());
        }
    }

    public DataTable  LoadETMBranch2()//M5
    {
        try
        {
            ddlETMBranch.Items.Clear();

            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_etmstore");
            MyCommand.Parameters.AddWithValue("@p_storeofficelvl", Session["StoreOfficeLvl"].ToString());
            MyCommand.Parameters.AddWithValue("@p_storeid", Session["StoreOfficeid"].ToString());

            dt = bll.SelectAll(MyCommand);
          

        }
        catch (Exception ex)
        {
            ddlETMBranch.Items.Insert(0, "SELECT");
            ddlETMBranch.Items[0].Value = "0";
            ddlETMBranch.SelectedIndex = 0;
            _common.ErrorLog("SysAdmETM-M5", ex.Message.ToString());
        }
        return dt;
    }
    protected void gvETMDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnviewETMs = (LinkButton)e.Row.FindControl("lbtnviewETMs");
            LinkButton lbtntransferETM = (LinkButton)e.Row.FindControl("lbtntransferETM");
            LinkButton lbtnissueETM = (LinkButton)e.Row.FindControl("lbtnissueETM");
            LinkButton lbtnUpdateETM = (LinkButton)e.Row.FindControl("lbtnUpdateETM");
            LinkButton lbtnLockETM = (LinkButton)e.Row.FindControl("lbtnLockETM");
            LinkButton lbtnDeleteETM = (LinkButton)e.Row.FindControl("lbtnDeleteETM");
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            DataTable dt = LoadETMBranch2();
        if(dt.Rows.Count>0)
            {
                lbtnissueETM.Visible = true;
                lbtntransferETM.Visible = false;
            }
            else
            {
                lbtnissueETM.Visible = false;
                lbtntransferETM.Visible = true;
            }
            if (rowView["statustypeid"].ToString() == "0")
            {
                lbtnviewETMs.Visible = false;
                lbtnissueETM.Visible = false;
                lbtntransferETM.Visible = false;
                lbtnUpdateETM.Visible = true;
                lbtnLockETM.Visible = true;
                lbtnDeleteETM.Visible = true;
              
            }
            else if(rowView["statustypeid"].ToString() == "1")
            {
                lbtnviewETMs.Visible = true;
             
                lbtnUpdateETM.Visible = false;
                lbtnLockETM.Visible = false;
                lbtnDeleteETM.Visible = false;
            }

            else
            {
             
                lbtnviewETMs.Visible = true;
                lbtnissueETM.Visible = false;
                lbtnUpdateETM.Visible = false;
                lbtnLockETM.Visible = false;
                lbtnDeleteETM.Visible = false;
                lbtntransferETM.Visible = false;
            }
        }
    }
    protected void gvETMDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvETMDetails.PageIndex = e.NewPageIndex;
        loadEtmDetails();
        loadEtmCount();
    }
    protected void lbtnYesConfirmation_Click(object sender, EventArgs e)
    {
        if (Session["_action"].ToString() == "SD")
        {
            Session["_etmRefNo"] = "";
            saveETMDetails();
            pnletmcount.Visible = true;
            instr.Visible = false;
            pnlAddETM.Visible = false;
            grdpara.Visible = true;
            GetEtmDtails.Visible = true;
            gvETMDetails.Visible = true;
            pnlMsg.Visible = false;


        }
        else if (Session["_action"].ToString() == "SL")
        {
            Session["_etmRefNo"] = "";
            saveETMDetails();
            pnletmcount.Visible = true;
            instr.Visible = false;
            pnlAddETM.Visible = false;
            grdpara.Visible = true;
            GetEtmDtails.Visible = true;
            gvETMDetails.Visible = true;
            pnlMsg.Visible = false;
        }
        else if (Session["_action"].ToString() == "U" || Session["_action"].ToString() == "L" || Session["_action"].ToString() == "L" || Session["_action"].ToString() == "D")
        {
            saveETMDetails();
            pnlAddETM.Visible = false;
        }


        else if (Session["_action"].ToString() == "I")
        {

            saveETMDetails();
            pnletmcount.Visible = true;
            instr.Visible = false;
            pnlAddETM.Visible = false;
            grdpara.Visible = true;
            GetEtmDtails.Visible = true;
            gvETMDetails.Visible = true;
            pnlMsg.Visible = false;
        }

        else if (Session["_action"].ToString() == "T")
        {

            saveETMDetails();
            pnletmcount.Visible = true;
            instr.Visible = false;
            pnlAddETM.Visible = false;
            grdpara.Visible = true;
            GetEtmDtails.Visible = true;
            gvETMDetails.Visible = true;
            pnlMsg.Visible = false;
        }
    }
    protected void lbtnUpdate_Click(object sender, EventArgs e)
    {
        if (validETM() == false)
        {
            return;
        }
        lblConfirmation.Text = "Do you want to update ETM details?";
        mpConfirmation.Show();
    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        if (validETM() == false)
        {
            return;
        }
        Session["_action"] = "SD";
        lblConfirmation.Text = "Do you want to Save ETM as Draft?";
        mpConfirmation.Show();
    }
    protected void lbtnReset_Click(object sender, EventArgs e)
    {
        clearData();
    }
    protected void lbtnSaveLock_Click(object sender, EventArgs e)
    {
        if (validETM() == false)
        {
            return;
        }
        Session["_action"] = "SL";
        lblConfirmation.Text = "Do you want to Save and Lock ETM?";
        mpConfirmation.Show();
    }
    protected void btnUploadInvoice_Click(object sender, EventArgs e)
    {
        if (!FileInvoice.HasFile)
        {
            Errormsg("Please select invoice first");
            return;
        }
        string _fileFormat = GetMimeDataOfFile(FileInvoice.PostedFile);
        if ((_fileFormat == "image/png") || (_fileFormat == "image/jpeg"))
        {
        }
        else
        {
            Errormsg("File must png, jpg, jpeg type");
            return;
        }
        if (!CheckFileExtention(FileInvoice))
        {
            Errormsg("File must be png, jpg, jpeg type");

            return;
        }

        decimal size = Math.Round((Convert.ToDecimal(FileInvoice.PostedFile.ContentLength) / Convert.ToDecimal(1024)), 2);
        if (size > 1024)
        {
            Errormsg("File size must be less than 1MB");
            return;
        }

        byte[] PhotoImage = null;
        PhotoImage = convertByteFile(FileInvoice);
        ImgInvoice.ImageUrl = GetImage(PhotoImage);
        ImgInvoice.Visible = true;
        Session["Invoice"] = FileInvoice.FileBytes;
    }
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        loadEtmDetails();
        //loadEtmCount();
    }

    protected void lbtnview_Click(object sender, EventArgs e)
    {
        string msg = "";
        msg = msg + "1.  Add ETM details along with its receiving office.<br/>";
        msg = msg + "2.  If ETM is issued to ETM Branch then it will be available for waybill issuance but if ETM is issued to Store then Store will first receive it then issue it to another store or ETM Branch.<br/>";
        msg = msg + "3.  View/Update/Delete ETM Details<br/>";
        msg = msg + "4.  Only Drafts ETM can be updated or deleted. Once Locked you cannot update or delete the ETM<br/>";
        msg = msg + "5.  Lock drafts ETM<br/>";
        InfoMsg(msg);
    }

    protected void ddlETMType_SelectedIndexChanged(object sender, EventArgs e)
    {
        divIMEI1.Visible = false;
        divIMEI2.Visible = false;
        if (ddlETMType.SelectedValue == "1")
        {
            divIMEI1.Visible = true;
            divIMEI2.Visible = true;
        }
    }
    protected void ddlMakeModel_SelectedIndexChanged(object sender, EventArgs e)
    {
        divMakeModelName.Visible = false;
        if (ddlMakeModel.SelectedValue == "99")
        {
            divMakeModelName.Visible = true;
        }
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadEtmDetails();
    }

    protected void lbtnPendingforlock_Click(object sender, EventArgs e)
    {
        ddlStatus.SelectedValue = "0";
        loadEtmDetails();
    }

    protected void lbtnAvailableforissuance_Click(object sender, EventArgs e)
    {
        ddlStatus.SelectedValue = "1";
        loadEtmDetails();
    }
  

    protected void lbtnissuedetm_Click(object sender, EventArgs e)
    {
        ddlStatus.SelectedValue = "2";
        loadEtmDetails();
    }
    protected void lbtnsaveissueetm_Click(object sender, EventArgs e)
    {
        int count = 0;
        string msg = "";


        if (ddlETMBranch.SelectedValue == "0")
        {
            count++;
            msg = msg + count + ". Select ETM Branch <br/>";
        }
        if (count > 0)
        {
            Errormsg(msg);
            return;
        }
        Session["_action"] = "I";  
        Session["assignedoffice"] = ddlETMBranch.SelectedItem.Value;
        ConfirmMsg("Do you want to Issue ETM ?");
    }

    protected void lbtntransferetm_Click(object sender, EventArgs e)
    {
        int count = 0;
        string msg = "";


        if (ddlotherStore.SelectedValue == "0")
        {
            count++;
            msg = msg + count + ". Select Store <br/>";
        }
        if (count > 0)
        {
            Errormsg(msg);
            return;
        }
        Session["_action"] = "T";
        Session["assignedoffice"] = ddlotherStore.SelectedItem.Value;
        ConfirmMsg("Do you want to Transfer ETM ?");
    }

    protected void lbtnAddETM_Click(object sender, EventArgs e)
    {
        //clearData();
        pnletmcount.Visible = false;
        instr.Visible = true;
        pnlAddETM.Visible = true;
        grdpara.Visible = false;
        GetEtmDtails.Visible = false;
        gvETMDetails.Visible = false;
        pnlMsg.Visible = false;
    }

    protected void lbtncanceladdetm_Click(object sender, EventArgs e)
    {
        clearData();
        pnletmcount.Visible = true;
        instr.Visible = false;
        pnlAddETM.Visible = false;
        gvETMDetails.Visible = true;
        grdpara.Visible = true;
        GetEtmDtails.Visible = true;
    }

    protected void lbtnViewETMCancel_Click(object sender, EventArgs e)
    {
        pnlAddETM.Visible = false;
        gvETMDetails.Visible = true;

        GetEtmDtails.Visible = true;
    }

    #endregion
    #region "Upload Mime Check"
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
    private bool CheckFileExtention(FileUpload FileInvoice)
    {
        try
        {
            bool fileExtensionOK = false;
            string fileExtension;
            fileExtension = System.IO.Path.GetExtension(FileInvoice.FileName).ToLower();
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
            Errormsg("Something went wrong with image (allow only .jpg, .png, .jpeg image). ");
            return default(Boolean);
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




    #endregion






  
}