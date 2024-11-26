<%@ Application Language="C#" %>
<%@ Import namespace="Npgsql" %>
<script RunAt="server">
   
    private Npgsql.NpgsqlCommand MyCommand;
    System.Data.DataTable dt = new System.Data.DataTable();
    private sbBLL bll = new sbBLL();
   
    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {

        MyCommand = new Npgsql.NpgsqlCommand();
        Int64 Mcntr;
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "public.f_update_hitcntr");
        dt = bll.SelectAll(MyCommand);
        if (dt.TableName == "Success")
        {
            if (dt.Rows.Count > 0)
            {
                Mcntr = Convert.ToInt64(dt.Rows[0]["CntrNo"].ToString());
                Session["_vstrCntr"] = Mcntr.ToString();
            }
            else
            {
                Session["_vstrCntr"] = "0";
            }
        }
        else
        {
            Session["_vstrCntr"] = "0";
        }

    }

    void Session_End(object sender, EventArgs e)
    {
        if (Session["_LogID"] == null)
        {
            
        }
        else
        {
            NpgsqlCommand MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_insert_logout_log");
            MyCommand.Parameters.AddWithValue("p_logid", Convert.ToInt32(Session["_LogID"]));
            string Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {

            }
        }
        Session.Abandon();
    }

</script>
