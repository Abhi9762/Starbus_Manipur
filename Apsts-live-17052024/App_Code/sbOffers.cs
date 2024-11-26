using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for sbOffers
/// </summary>
public class sbOffers
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    private sbBLL bll = new sbBLL();
    public sbOffers()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable getOffers(string Date, string getRecordCount, string getRecordDirection_FL)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_discount_get");
        MyCommand.Parameters.AddWithValue("p_date", Date);
        MyCommand.Parameters.AddWithValue("p_get_count_no", Int64.Parse(getRecordCount));
        MyCommand.Parameters.AddWithValue("p_get_order_fl", getRecordDirection_FL);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }

    public DataTable getOffers_count()
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_get_offercount");
        dt = bll.SelectAll(MyCommand);
        return dt;
    }

    public DataTable getOfferDetail(string couponId)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "auth.f_discount_get_details");
        MyCommand.Parameters.AddWithValue("p_couponid", int.Parse(couponId));
        dt = bll.SelectAll(MyCommand);
        return dt;
    }

    public DataTable applyOffer(string userid,string ticketNo, string couponCode)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_insert_offer_apply");
        MyCommand.Parameters.AddWithValue("p_userid", userid);
        MyCommand.Parameters.AddWithValue("p_ticketno", ticketNo);
        MyCommand.Parameters.AddWithValue("p_couponcode", couponCode);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }

    public DataTable removeOffer(string ticketNo, string couponCode)
    {
        MyCommand = new NpgsqlCommand();
        DataTable dt = new DataTable();
        MyCommand.Parameters.Clear();
        MyCommand.Parameters.AddWithValue("@StoredProcedure", "booking.f_insert_offer_remove");
        MyCommand.Parameters.AddWithValue("p_ticketno", ticketNo);
        MyCommand.Parameters.AddWithValue("p_couponcode", couponCode);
        dt = bll.SelectAll(MyCommand);
        return dt;
    }



}