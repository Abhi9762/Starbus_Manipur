using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_CntrBusPassesStatus : System.Web.UI.Page
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
            Session["_moduleName"] = "Bus Pass Status";
            GetconfirmationDetails(Session["currtranrefno"].ToString());
        }
            
    }

    #region "Methods"
    protected void GetconfirmationDetails(string currtranrefno)
    {
        try
        {
            DataTable dt;
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "buspass.getconfirmpassdetails");
            MyCommand.Parameters.AddWithValue("p_currentrefno", currtranrefno);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["transtype_"].ToString() == "N")
                        lblcardhd.Text = "New Pass - Print";
                    else if (dt.Rows[0]["transtype_"].ToString() == "R")
                        lblcardhd.Text = "Renew Pass - Print";
                    if (Session["IssuanceType"].ToString() == "I")
                    {
                        Session["Passno"] = dt.Rows[0]["passnumber"];
                        lblmsg.Text = "Request For Pass Type " + dt.Rows[0]["cardtypename_"] + " (" + dt.Rows[0]["psngr_type_name"] + ")<br />  vide Pass No. " + dt.Rows[0]["passnumber"] + " has been registered successfully.";
                        btnprintpass.Visible = true;
                        btnprintrecipt.Visible = false;
                    }
                    else if (Session["IssuanceType"].ToString() == "A")
                    {
                        Session["currtranrefno"] = dt.Rows[0]["currtranref_no"];
                        lblmsg.Text = "Request For Pass Type " + dt.Rows[0]["cardtypename_"] + " (" + dt.Rows[0]["psngr_type_name"] + ")<br />  vide Ref. No. " + dt.Rows[0]["currtranref_no"] + " has been created successfully.";
                        btnprintpass.Visible = false;
                        btnprintrecipt.Visible = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
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

    #endregion

    #region "Event"
    protected void btnprintrecipt_Click(object sender, EventArgs e)
    {
        openSubDetailsWindow("../Pass_reciept.aspx");
    }
    protected void btnprintpass_Click(object sender, EventArgs e)
    {
        openSubDetailsWindow("../Bus_Pass.aspx");
    }
    protected  void lbtnback_Click(object sender, EventArgs e)
    {
        Session["_RNDIDENTIFIERCNTRAUTH"] = _validation.GeneratePassword(10, 5);
        Response.Redirect("CntrBusPasses.aspx");
    }
    #endregion
}