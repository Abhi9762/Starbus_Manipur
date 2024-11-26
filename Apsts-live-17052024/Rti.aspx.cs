using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Rti : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["heading"] = "RTI Manual";
        loadGenralData();
    }
    private void loadGenralData()//M1
    {
        try
        {
            imgrti1.Visible = true;
            imgrti2.Visible = true;
            imgrti3.Visible = true;
            emrti1.Visible = false;
            emrti2.Visible = false;
            emrti3.Visible = false;
            sbXMLdata obj = new sbXMLdata();
            DataTable dtRTI = new DataTable();
            dtRTI = obj.loadRti();
            if (dtRTI.Rows.Count > 0)
            {
                if (!DBNull.Value.Equals(dtRTI.Rows[0]["rti1"]) && dtRTI.Rows[0]["rti1"].ToString() != "")
                {
                    emrti1.Src = "manuals/" + dtRTI.Rows[0]["rti1"].ToString();
                    imgrti1.Visible = false;
                    emrti1.Visible = true;
                }
                if (!DBNull.Value.Equals(dtRTI.Rows[0]["rti2"]) && dtRTI.Rows[0]["rti2"].ToString() !="")
                {
                    emrti2.Src = "manuals/" + dtRTI.Rows[0]["rti2"].ToString();
                    imgrti2.Visible = false;
                    emrti2.Visible = true;
                }
                if (!DBNull.Value.Equals(dtRTI.Rows[0]["rti3"]) && dtRTI.Rows[0]["rti3"].ToString() != "")
                {
                    emrti3.Src = "manuals/" + dtRTI.Rows[0]["rti3"].ToString();
                    imgrti3.Visible = false;
                    emrti3.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
}