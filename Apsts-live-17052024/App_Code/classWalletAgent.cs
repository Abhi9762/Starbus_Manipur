using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;



public class classWalletAgent
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    public classWalletAgent()
    {
      
    }

    public DataTable saveWalletTxn(string userId, string txnRefrence, string txnId, string txnTypeCode,string refrence, string amount)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent_wallet.wallet_txn_update");
            MyCommand.Parameters.AddWithValue("@p_userid", userId);
            MyCommand.Parameters.AddWithValue("@p_txnrefrence", txnRefrence);
            MyCommand.Parameters.AddWithValue("@p_txntypecode", txnTypeCode);
            MyCommand.Parameters.AddWithValue("@p_refrence", refrence);
            MyCommand.Parameters.AddWithValue("@p_amount", amount);
            MyCommand.Parameters.AddWithValue("@p_userid", userId);
            dt = bll.SelectAll(MyCommand);
            return dt;
        }
        catch (Exception ex)
        {

            return null;
        }
    }
    public DataTable GetWalletDetailDt(string userId)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent_wallet.wallet_detail");
            MyCommand.Parameters.AddWithValue("@p_userid",userId);
            dt = bll.SelectAll(MyCommand);
            return dt;
        }
        catch (Exception ex)
        {
           
            return null; 
        }
    }
    public DataTable getWalletTransactions_dt(string userId, string txnCounts, string txnOrder_FL)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent_wallet.wallet_transactions");
            MyCommand.Parameters.AddWithValue("@p_userid", userId);
            MyCommand.Parameters.AddWithValue("@p_txn_count", Convert.ToInt32(txnCounts));
            MyCommand.Parameters.AddWithValue("@p_txn_order_fl", txnOrder_FL);
            dt = bll.SelectAll(MyCommand);
            return dt;
        }
        catch (Exception ex)
        {
         
            return null; 
        }
    }

    public DataTable getWalletTransactions_dt_bydate(string userId, string fromdate, string todate)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent_wallet.fn_get_wallet_transactions");
            MyCommand.Parameters.AddWithValue("@p_userid", userId);
            MyCommand.Parameters.AddWithValue("@p_fdate", fromdate);
            MyCommand.Parameters.AddWithValue("@p_tdate", todate);
            dt = bll.SelectAll(MyCommand);
            return dt;
        }
        catch (Exception ex)
        {

            return null;
        }
    }
    public DataTable gettopupTransactions_dt(string userId)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent_wallet.topup_transactions");
            MyCommand.Parameters.AddWithValue("@p_userid", userId);
            dt = bll.SelectAll(MyCommand);
            return dt;
        }
        catch (Exception ex)
        {

            return null;
        }
    }
    public DataTable gettopupTransactions_dt_bydate(string userId, string fromdate, string todate)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent_wallet.fn_get_topup_transactions");
            MyCommand.Parameters.AddWithValue("@p_userid", userId);
            MyCommand.Parameters.AddWithValue("@p_fdate", fromdate);
            MyCommand.Parameters.AddWithValue("@p_tdate", todate);
            dt = bll.SelectAll(MyCommand);
            return dt;
        }
        catch (Exception ex)
        {

            return null;
        }
    }
    public DataTable walletTopup_start_completed(string txnRefrence, string userId, string amount,string first_last)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent_wallet.wallet_topup_transactions");
            MyCommand.Parameters.AddWithValue("@p_refno", txnRefrence);
            MyCommand.Parameters.AddWithValue("@p_userid", userId);
            MyCommand.Parameters.AddWithValue("@p_amount", amount);
            MyCommand.Parameters.AddWithValue("@p_txn_fl", first_last);
            dt = bll.SelectAll(MyCommand);
            return dt;
        }
        catch (Exception ex)
        {

            return null;
        }
    }
    public DataTable gettopupTransactions_status(string userId, string txnrefno)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            DataTable dt = new DataTable();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "agent_wallet.fn_get_wallet_topup_txn_status");
            MyCommand.Parameters.AddWithValue("@p_userid", userId);
            MyCommand.Parameters.AddWithValue("@p_refno", txnrefno);
            dt = bll.SelectAll(MyCommand);
            return dt;
        }
        catch (Exception ex)
        {

            return null;
        }
    }
}