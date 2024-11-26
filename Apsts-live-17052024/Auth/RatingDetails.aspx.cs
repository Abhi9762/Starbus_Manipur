using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_RatingDetails : System.Web.UI.Page
{
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        lbldate.Text = "Rating Details For Journey Date:  " + Session["journey_date"];
        getratinggrid();
    }
    public void getratinggrid()
    {
        MyCommand = new NpgsqlCommand();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_rating_info");
        MyCommand.Parameters.AddWithValue("@p_date", Session["journey_date"]);
        dt = bll.SelectAll(MyCommand);
        if (dt.Rows.Count > 0)
        {
            gvRating.DataSource = dt;
            gvRating.DataBind();
            pnlMsg.Visible = false;
            lbtnback.Visible = true;
            pnlReport.Visible = true;
        }
        else
        {

            pnlMsg.Visible = true;

            pnlReport.Visible = false;
            gvRating.DataSource = dt;
            gvRating.DataBind();
        }
    }

    protected void lbtnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("SysAdmFeedbackRating1.aspx");
    }

    protected void gvRating_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Email")
        {

            LabelMpEmlTo.Text = "";
            TextBoxMpEmlSubject.Text = "";
            TextBoxMpEmlMessage.Text = "";
            string userId = gvRating.DataKeys[index].Values["bookedbyusercode"].ToString();
            string ticketNo = gvRating.DataKeys[index].Values["ticketno"].ToString();

            LabelMpEmlTo.Text = userId;
            TextBoxMpEmlSubject.Text = "Thanks for sharing your valuable feedback (Ticket No. " + ticketNo + ")";
            ModalPopupEmail.Show();
        }

        if (e.CommandName == "Sms")
        {
            LabelMpMsgTo.Text = "";
            TextBoxMpMessage.Text = "";
            string userId = gvRating.DataKeys[index].Values["bookedbyusercode"].ToString();
            HiddenFieldTicketNoForSMS.Value = gvRating.DataKeys[index].Values["ticketno"].ToString();
            LabelMpMsgTo.Text = userId;
            ModalPopupMessage.Show();
        }



    }

    protected void gvRating_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType== DataControlRowType.DataRow)
        {
            LinkButton lbtnemail = e.Row.Cells[9].FindControl("lbtnemail") as LinkButton;
            if (e.Row.Cells[2].Text == "")
            {
                lbtnemail.Visible = false;

            }
            else
            {
                lbtnemail.Visible = true;
            }
        }
    }
    protected void LinkButtonMpMsgSend_Click(object sender, EventArgs e)
    {

        //if(TextBoxMpMessage.Text = "")
        //{

        //}
        //mpConfirmation.Show();
    }
}