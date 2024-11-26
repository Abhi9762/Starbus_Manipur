using CaptchaDLL;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class busPass : outsidebasepage
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
            if (sbXMLdata.checkModuleCategory("71") == false)
            {
                Response.Redirect("Errorpage.aspx");
            }
            Session["Heading"] = "Bus Passes"; 
            Session["Heading1"] = "Bus Passes";

            GetBusPassDetails();
            RefreshCaptcha();

            if (Session["BusPass"] != null)
            {
                rbtdownload.Checked = true;
            }
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


    protected void loadPassTxnDetails()
    {
        try
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            DataTable dt;
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.get_pass_request_dtls");
            MyCommand.Parameters.AddWithValue("p_currentrefno", tbPassRefno.Text.ToString());
            MyCommand.Parameters.AddWithValue("p_dob", tbdob.Text.ToString());

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    Session["Passno"] = dt.Rows[0]["pass_number"];
                    if (rbtchkstatus.Checked == true)
                    {
                        lblhd.Text = "Status Of Your Application";
                        if (dt.Rows[0]["current_status"].ToString() == "S")
                            lblmsg.Text = "Dear" + dt.Rows[0]["ctzname"] + ", <br /> Staus of your " + dt.Rows[0]["psngrtypename"] + " Application For Vide Registration Number " + dt.Rows[0]["currtranref_no"] + " as on " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + " is under process";
                        else if (dt.Rows[0]["current_status"].ToString() == "A")
                        {
                            lblmsg.Text = " Dear " + dt.Rows[0]["ctzname"] + ", <br /> Status of your " + dt.Rows[0]["psngrtypename"] + " Application for Vide Registration Number " + dt.Rows[0]["currtranref_no"] + " as on " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + " is approved ,So you can download E-Pass";
                            lbtndownload.Visible = true;
                            if (Convert.ToDateTime(dt.Rows[0]["periodto"]) < DateTime.Now.Date)
                            {
                                lblmsg.Text = lblmsg.Text + "<br/>Your Pass Number " + dt.Rows[0]["pass_number"] + " has been expired";
                                lbtndownload.Visible = false;
                            }
                        }
                        else if (dt.Rows[0]["current_status"].ToString() == "C")
                            lblmsg.Text = " Dear " + dt.Rows[0]["ctzname"] + ", <br /> Status of your " + dt.Rows[0]["psngrtypename"] + " Application for Vide Registration Number " + dt.Rows[0]["currtranref_no"] + " as on " + DateTime.Now.ToString("dd/MM/yyyy  hh:mm tt") + " is Rejected Due to pass Reason";
                    }
                    if (rbtchkvalidity.Checked == true)
                    {
                        lblhd.Text = "Validity Of Your Pass";
                        if (dt.Rows[0]["current_status"].ToString() == "S")
                            lblmsg.Text = " Dear " + dt.Rows[0]["ctzname"] + ", <br /> Staus of your " + dt.Rows[0]["psngrtypename"] + " Application For Vide Registration Number " + dt.Rows[0]["currtranref_no"] + " as on " + DateTime.Now.ToString("dd/MM/yyyy  hh:mm tt") + " is under process";
                        else if (dt.Rows[0]["current_status"].ToString() == "A")
                        {
                            lblmsg.Text = "Your Pass Number " + dt.Rows[0]["pass_number"] + " is Valid Upto " + dt.Rows[0]["periodto"] + ", so Download your e-pass";
                            lbtndownload.Visible = true;
                            if (Convert.ToDateTime(dt.Rows[0]["periodto"]) < DateTime.Now.Date)
                            {
                                lblmsg.Text = "Your Pass Number " + dt.Rows[0]["pass_number"] + " has been expired";
                                lbtndownload.Visible = false;
                            }
                        }
                        else if (dt.Rows[0]["current_status"].ToString() == "C")
                            lblmsg.Text = " Dear " + dt.Rows[0]["ctzname"] + ", <br /> Status of your " + dt.Rows[0]["psngrtypename"] + " Application for Registration Number " + dt.Rows[0]["currtranref_no"] + " as on " + DateTime.Now.ToString("dd/MM/yyyy  hh:mm tt") + " is Rejected Due to pass Reason";
                    }
                    if (rbtdownload.Checked == true)
                    {
                        lblhd.Text = "Download E-Pass";
                        if (dt.Rows[0]["current_status"].ToString() == "S")
                            lblmsg.Text = " Dear " + dt.Rows[0]["ctzname"] + ", <br /> Staus of your " + dt.Rows[0]["psngrtypename"] + " Application For Vide Registration Number " + dt.Rows[0]["currtranref_no"] + " as on " + DateTime.Now.ToString("dd/MM/yyyy  hh:mm tt") + " is under process";
                        else if (dt.Rows[0]["current_status"].ToString() == "A")
                        {
                            lblmsg.Text = "Your Pass Number " + dt.Rows[0]["pass_number"] + " is Valid Upto " + dt.Rows[0]["periodto"] + ", so Download your e-pass";
                            lbtndownload.Visible = true;
                            if (Convert.ToDateTime(dt.Rows[0]["periodto"]) < DateTime.Now.Date)
                            {
                                lblmsg.Text = "Your Pass Number " + dt.Rows[0]["pass_number"] + " has been expired";
                                lbtndownload.Visible = false;
                            }
                        }
                        else if (dt.Rows[0]["current_status"].ToString() == "C")
                            lblmsg.Text = " Dear " + dt.Rows[0]["ctzname"] + ", <br /> Status of your " + dt.Rows[0]["psngrtypename"] + " Application for Registration Number " + dt.Rows[0]["currtranref_no"] + " as on " + DateTime.Now.ToString("dd/MM/yyyy  hh:mm tt") + " is Rejected Due to pass Reason";
                    }
                    resetctrl();
                    mpConfirmation.Show();
                }
                else
                    Errormsg("Invalid Pass Ref. No and Date Of Birth");
            }
            else
                Errormsg("Invalid Pass Ref. No and Date Of Birth" + dt.TableName);
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Enter Valid Pass Referance Number, Date Of Birth And security text.<br /> You can check pass transaction status, Pass Validity And Download E_Pass";
            return;
        }
    }
    private void GetBusPassDetails()
    {
        try
        {



            NpgsqlCommand MyCommand = new NpgsqlCommand();
            DataTable dt;
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_bus_pass_typelist");
            MyCommand.Parameters.AddWithValue("@p_categoryid", 0);

            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {

                    DataView view = new DataView(dt);
                    DataTable distinctValues = view.ToTable(true, "busspass_categoryname", "buspass_categorycount");

                    rptrservicetypecount.DataSource = distinctValues;
                    rptrservicetypecount.DataBind();

                    rptBusPassType.DataSource = dt;
                    rptBusPassType.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            // Handle the exception as needed
        }
    }
    protected void lbtnRefresh_Click(object sender, EventArgs e)
    {
        RefreshCaptcha();
    }

    protected void lbtnreset_Click(object sender, EventArgs e)
    {
        mpConfirmation.Show();
        resetctrl();
    }

    private void resetctrl()
    {
        rbtchkstatus.Checked = false;
        rbtchkvalidity.Checked = false;
        rbtdownload.Checked = false;
        tbPassRefno.Text = "";
        tbdob.Text = "";
        RefreshCaptcha();
    }

    protected void lbtnproceed_Click(object sender, EventArgs e)
    {
        if (!ValidValue())
        {
            resetctrl();
            return;
        }
       loadPassTxnDetails();
    }

    private bool ValidValue()
    {
        try
        {
            string msg = "";
            int msgcount = 0;

            if (!rbtchkstatus.Checked && !rbtchkvalidity.Checked && !rbtdownload.Checked)
            {
                msgcount++;
                msg += msgcount + ". Select at least one service <br/>";
            }

            if (tbPassRefno.Text.Length <= 0)
            {
                msgcount++;
                msg += msgcount + ". Enter Pass Ref. Number <br/>";
            }

            if (tbdob.Text == "")
            {
                msgcount++;
                msg += msgcount + ". Enter Valid Date of birth <br/>";
            }
            else
            {
                if (Convert.ToDateTime(tbdob.Text) > DateTime.Now)
                {
                    msgcount++;
                    msg += msgcount + ". Enter Valid Date of birth <br/>";
                }
            }

            if (!(Session["CaptchaImage"] != null && tbcaptchacode.Text.ToLower() == Session["CaptchaImage"].ToString().ToLower()))
            {
                msgcount++;
                msg += msgcount + ". Invalid Security Code(Shown in Image). Please Try Again <br/>";
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

    private void Errormsg(string msg)
    {
        lblerrormsg.Text = msg;
        mperror.Show();
    }
    protected void lbtndownload_Click(object sender, EventArgs e)
    {
        mpPass.Show();
        // openSubDetailsWindow("PassUTC/Bus_Pass.aspx");
        openSubDetailsWindow("Bus_Pass.aspx");
    }

    public void openSubDetailsWindow(string MModuleName)
    {
        try
        {
            string murl = MModuleName + "?rt=" + DateTime.Now.ToString();

            if (Request.Browser.Type.ToUpper().Trim().Substring(0, 2) == "IE")
            {
                Response.Write("<SCRIPT language='javascript'>window.showModalDialog('" + murl + "','name','dialogWidth:100px;dialogHeight:100px');</script>");
            }
            else
            {
                // string url = "GenQrySchStages.aspx";
                string fullURL = "window.open('" + murl + "', '_blank', 'height=520,width=1200,status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
                // ScriptManager.RegisterStartupScript(this, GetType(string), "OPEN_WINDOW", fullURL, true);
                string script = "window.open('" + fullURL + "','')";
                if (!ClientScript.IsClientScriptBlockRegistered("NewWindow"))
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "NewWindow", fullURL, true);
                }
            }
        }
        catch (Exception ex)
        {
            // Handle the exception as needed
        }
    }


}