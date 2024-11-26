using System;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_AGe_reciept : System.Web.UI.Page
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
        getdetails(Session["transrefno"].ToString());
    }

    private void getdetails(string trnsrefno)
    {

        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent.getagtransactiondetails");
            MyCommand.Parameters.AddWithValue("p_trnsrefno", trnsrefno);
            dt = bll.SelectAll(MyCommand);
            if (dt.TableName == "Success")
            {
                if (dt.Rows.Count > 0)
                {
                    lblAGcode.Text = dt.Rows[0]["agent_code"].ToString();
                    lblTransactionNo.Text = dt.Rows[0]["transaction_refno"].ToString();
                    lblagname.Text = dt.Rows[0]["agent_name"].ToString();
                    lblagtype.Text = dt.Rows[0]["agent_type"].ToString();
                    lbldepositamt.Text = dt.Rows[0]["transaction_amount"].ToString() + "₹";
                    lbltransinitdate.Text = dt.Rows[0]["transaction_init_date"].ToString();
                    lbltranscmpltedate.Text = DateTime.Now.ToString();
                    msg.Visible = false;
                    details.Visible = true;
                }
            }
            else
            {
                msg.Visible = true;
                details.Visible = false;
            }
        }
        catch (Exception ex)
        {
            msg.Visible = false;
            details.Visible = true;
        }
    }


}