using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
public class HDFC
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    public HDFC()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable get_PG()
    {
        try
        {
            DataTable mytable = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_get_online_pg");
            mytable = bll.SelectAll(MyCommand);
            return mytable;
        }
        catch (Exception ex)
        {
            DataTable dtt = new DataTable();
            return dtt;
        }
    }
 public DataTable getactiveAll_PG()
    {
        try
        {
            DataTable mytable = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_get_pg");
            mytable = bll.SelectAll(MyCommand);
            return mytable;
        }
        catch (Exception ex)
        {
            DataTable dtt = new DataTable();
            return dtt;
        }
    }
    public DataTable get_PG_requestURL(int pgid)
    {
        try
        {
            DataTable mytable = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_get_pg_req_url");
            MyCommand.Parameters.AddWithValue("p_pg_id", pgid);
            mytable = bll.SelectAll(MyCommand);
            return mytable;
        }
        catch (Exception ex)
        {
            DataTable dtt = new DataTable();
            return dtt;
        }
    }
    public bool HDFCRequest(string requestid, string merchant_id, string order_id, string currency, string amount, string redirect_url, string cancel_url, string language, string billing_name, string billing_address, string billing_city, string billing_state, string billing_zip, string billing_country, string billing_tel, string billing_email, string delivery_name, string delivery_address, string delivery_city, string delivery_state, string delivery_zip, string delivery_country, string delivery_tel, string merchant_param1, string merchant_param2, string merchant_param3, string merchant_param4, string merchant_param5, string promo_code, string customer_identifier, string strEncRequest)
    {
        try
        {
            string Mresult = "";
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_hdfcrequest");
            MyCommand.Parameters.AddWithValue("p_requestid", requestid);
            MyCommand.Parameters.AddWithValue("p_merchant_id", merchant_id);
            MyCommand.Parameters.AddWithValue("p_order_id", order_id);
            MyCommand.Parameters.AddWithValue("p_currency", currency);
            MyCommand.Parameters.AddWithValue("p_amount", amount);
            MyCommand.Parameters.AddWithValue("p_redirect_url", redirect_url);
            MyCommand.Parameters.AddWithValue("p_cancel_url", cancel_url);
            MyCommand.Parameters.AddWithValue("p_language", language);
            MyCommand.Parameters.AddWithValue("p_billing_name", billing_name);
            MyCommand.Parameters.AddWithValue("p_billing_address", billing_address);
            MyCommand.Parameters.AddWithValue("p_billing_city", billing_city);
            MyCommand.Parameters.AddWithValue("p_billing_state", billing_state);
            MyCommand.Parameters.AddWithValue("p_billing_zip", billing_zip);
            MyCommand.Parameters.AddWithValue("p_billing_country", billing_country);
            MyCommand.Parameters.AddWithValue("p_billing_tel", billing_tel);
            MyCommand.Parameters.AddWithValue("p_billing_email", billing_email);
            MyCommand.Parameters.AddWithValue("p_delivery_name", delivery_name);
            MyCommand.Parameters.AddWithValue("p_delivery_address", delivery_address);
            MyCommand.Parameters.AddWithValue("p_delivery_city", delivery_city);
            MyCommand.Parameters.AddWithValue("p_delivery_state", delivery_state);
            MyCommand.Parameters.AddWithValue("p_delivery_zip", delivery_zip);
            MyCommand.Parameters.AddWithValue("p_delivery_country", delivery_country);
            MyCommand.Parameters.AddWithValue("p_delivery_tel", delivery_tel);
            MyCommand.Parameters.AddWithValue("p_merchant_param1", merchant_param1);
            MyCommand.Parameters.AddWithValue("p_merchant_param2", merchant_param2);
            MyCommand.Parameters.AddWithValue("p_merchant_param3", merchant_param3);
            MyCommand.Parameters.AddWithValue("p_merchant_param4", merchant_param4);
            MyCommand.Parameters.AddWithValue("p_merchant_param5", merchant_param5);
            MyCommand.Parameters.AddWithValue("p_promo_code", promo_code);
            MyCommand.Parameters.AddWithValue("p_customer_identifier", customer_identifier);
            MyCommand.Parameters.AddWithValue("p_strencrequest", strEncRequest);
            Mresult = bll.UpdateAll(MyCommand);
            if (Mresult == "Success")
            {
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public DataTable HDFCResponse(string order_id, string tracking_id, string bank_ref_no, string order_status, string failure_message, string payment_mode, string card_name, string status_code, string status_message, string currency, string amount, string billing_name, string billing_address, string billing_city, string billing_state, string billing_zip, string billing_country, string billing_tel, string billing_email, string delivery_name, string delivery_address, string delivery_city, string delivery_state, string delivery_zip, string delivery_country, string delivery_tel, string merchant_param1, string merchant_param2, string merchant_param3, string merchant_param4, string merchant_param5, string vault, string offer_type, string offer_code, string discount_value, string mer_amount, string eci_value, string retry, string response_code, string billing_notes, string trans_date, string bin_country, string token_eligibility, string encResponse)
    {
        try
        {
            DataTable mytable = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_hdfcresponse");
            MyCommand.Parameters.AddWithValue("p_order_id", order_id);
            MyCommand.Parameters.AddWithValue("p_tracking_id", tracking_id);
            MyCommand.Parameters.AddWithValue("p_bank_ref_no", bank_ref_no);
            MyCommand.Parameters.AddWithValue("p_order_status", order_status);
            MyCommand.Parameters.AddWithValue("p_failure_message", failure_message);
            MyCommand.Parameters.AddWithValue("p_payment_mode", payment_mode);
            MyCommand.Parameters.AddWithValue("p_card_name", card_name);
            MyCommand.Parameters.AddWithValue("p_status_code", status_code);
            MyCommand.Parameters.AddWithValue("p_status_message", status_message);
            MyCommand.Parameters.AddWithValue("p_currency", currency);
            MyCommand.Parameters.AddWithValue("p_amount", amount);
            MyCommand.Parameters.AddWithValue("p_billing_name", billing_name);
            MyCommand.Parameters.AddWithValue("p_billing_address", billing_address);
            MyCommand.Parameters.AddWithValue("p_billing_city", billing_city);
            MyCommand.Parameters.AddWithValue("p_billing_state", billing_state);
            MyCommand.Parameters.AddWithValue("p_billing_zip", billing_zip);
            MyCommand.Parameters.AddWithValue("p_billing_country", billing_country);
            MyCommand.Parameters.AddWithValue("p_billing_tel", billing_tel);
            MyCommand.Parameters.AddWithValue("p_billing_email", billing_email);
            MyCommand.Parameters.AddWithValue("p_delivery_name", delivery_name);
            MyCommand.Parameters.AddWithValue("p_delivery_address", delivery_address);
            MyCommand.Parameters.AddWithValue("p_delivery_city", delivery_city);
            MyCommand.Parameters.AddWithValue("p_delivery_state", delivery_state);
            MyCommand.Parameters.AddWithValue("p_delivery_zip", delivery_zip);
            MyCommand.Parameters.AddWithValue("p_delivery_country", delivery_country);
            MyCommand.Parameters.AddWithValue("p_delivery_tel", delivery_tel);
            MyCommand.Parameters.AddWithValue("p_merchant_param1", merchant_param1);
            MyCommand.Parameters.AddWithValue("p_merchant_param2", merchant_param2);
            MyCommand.Parameters.AddWithValue("p_merchant_param3", merchant_param3);
            MyCommand.Parameters.AddWithValue("p_merchant_param4", merchant_param4);
            MyCommand.Parameters.AddWithValue("p_merchant_param5", merchant_param5);
            MyCommand.Parameters.AddWithValue("p_vault", vault);
            MyCommand.Parameters.AddWithValue("p_offer_type", offer_type);
            MyCommand.Parameters.AddWithValue("p_offer_code", offer_code);
            MyCommand.Parameters.AddWithValue("p_discount_value", discount_value);
            MyCommand.Parameters.AddWithValue("p_mer_amount", mer_amount);
            MyCommand.Parameters.AddWithValue("p_eci_value", eci_value);
            MyCommand.Parameters.AddWithValue("p_retry", retry);
            MyCommand.Parameters.AddWithValue("p_response_code", response_code);
            MyCommand.Parameters.AddWithValue("p_billing_notes", billing_notes);
            MyCommand.Parameters.AddWithValue("p_trans_date", trans_date);
            MyCommand.Parameters.AddWithValue("p_bin_country", bin_country);
            MyCommand.Parameters.AddWithValue("p_token_eligibility", token_eligibility);
            MyCommand.Parameters.AddWithValue("p_encresponse", encResponse);
            mytable = bll.SelectAll(MyCommand);
            return mytable;
        }
        catch (Exception ex)
        {
            DataTable dt = new DataTable();
            return dt;
        }
    }
    public DataTable getUserDetails_after_PG(string orderId, string requestId)
    {
        try
        {
            DataTable mytable = new DataTable();
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_getuserdetail_after_pg");
            MyCommand.Parameters.AddWithValue("p_orderid", orderId);
            MyCommand.Parameters.AddWithValue("p_requestid", requestId);
            mytable = bll.SelectAll(MyCommand);
            return mytable;
        }
        catch (Exception e)
        {
            DataTable dtt = new DataTable();
            return dtt;
        }
    }
}