using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BILLDESK
/// </summary>
public class BILLDESK
{
    sbValidation _validation = new sbValidation();
    private NpgsqlCommand MyCommand;
    DataTable dt = new DataTable();
    private sbBLL bll = new sbBLL();
    public bool BILLDESKRequestNew(string request_id, string merchant_id, string order_id, string txn_amount, 
        string currency_type, string type_field1, string security_id, string type_field2, string email,
        string mobile, string billing_name, string pg, string return_url, string checksum_key,
        string checksum, string request_string)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_billdeskrequest_new");
            MyCommand.Parameters.AddWithValue("p_request_id", request_id);
            MyCommand.Parameters.AddWithValue("p_merchant_id", merchant_id);
            MyCommand.Parameters.AddWithValue("p_order_id", order_id);
            MyCommand.Parameters.AddWithValue("p_txn_amount",txn_amount);
            MyCommand.Parameters.AddWithValue("p_currency_type",   currency_type);
            MyCommand.Parameters.AddWithValue("p_type_field1", type_field1);
            MyCommand.Parameters.AddWithValue("p_security_id", security_id);
            MyCommand.Parameters.AddWithValue("p_type_field2", type_field2);
            MyCommand.Parameters.AddWithValue("p_email", email);
            MyCommand.Parameters.AddWithValue("p_mobile", mobile);
            MyCommand.Parameters.AddWithValue("p_billing_name", billing_name);
            MyCommand.Parameters.AddWithValue("p_pg", pg);
            MyCommand.Parameters.AddWithValue("p_return_url", return_url);
            MyCommand.Parameters.AddWithValue("p_checksum_key", checksum_key);
            MyCommand.Parameters.AddWithValue("p_checksum", checksum);
            MyCommand.Parameters.AddWithValue("p_request_string", request_string);

            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            if (dtt.TableName == "Success")
            {
                if (dtt.Rows[0]["status"].ToString() == "SUCCESS")
                {
                    return true;
                } 
                else
                {
                    return false;
                }
            }
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public bool BILLDESKRequest(string request_id, string merchant_id, string order_id, string txn_amount,
        string currency_type, string type_field1, string security_id, string type_field2, string email,
        string mobile, string billing_name, string pg, string return_url, string checksum_key,
        string checksum, string request_string)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_billdeskrequest");
            MyCommand.Parameters.AddWithValue("p_request_id", request_id);
            MyCommand.Parameters.AddWithValue("p_merchant_id", merchant_id);
            MyCommand.Parameters.AddWithValue("p_order_id", order_id);
            MyCommand.Parameters.AddWithValue("p_txn_amount", txn_amount);
            MyCommand.Parameters.AddWithValue("p_currency_type", currency_type);
            MyCommand.Parameters.AddWithValue("p_type_field1", type_field1);
            MyCommand.Parameters.AddWithValue("p_security_id", security_id);
            MyCommand.Parameters.AddWithValue("p_type_field2", type_field2);
            MyCommand.Parameters.AddWithValue("p_email", email);
            MyCommand.Parameters.AddWithValue("p_mobile", mobile);
            MyCommand.Parameters.AddWithValue("p_billing_name", billing_name);
            MyCommand.Parameters.AddWithValue("p_pg", pg);
            MyCommand.Parameters.AddWithValue("p_return_url", return_url);
            MyCommand.Parameters.AddWithValue("p_checksum_key", checksum_key);
            MyCommand.Parameters.AddWithValue("p_checksum", checksum);
            MyCommand.Parameters.AddWithValue("p_request_string", request_string);

            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            if (dtt.TableName == "Success")
            {
                if (dtt.Rows[0]["status"].ToString() == "SUCCESS")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
public DataTable BILLDESKResponseNew(string MerchantID,
         string OrderID, string TxnReferenceNo, string BankReferenceNo, string TxnAmount, string BankID, string BankMerchantID,
         string TxnType, string CurrencyName, string ItemCode, string SecurityType,
         string SecurityID, string SecurityPassword, string TxnDate, string AuthStatus, string SettlementType,
         string ErrorStatus, string ErrorDescription, string CheckSum, string Responsemsg, string checksumkey,
         string MstringForCheckSum)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_billdeskresponse_new");

            MyCommand.Parameters.AddWithValue("p_merchant_id", MerchantID);
            MyCommand.Parameters.AddWithValue("p_order_id", OrderID);
            MyCommand.Parameters.AddWithValue("p_txn_ref_no", TxnReferenceNo);
            MyCommand.Parameters.AddWithValue("p_bank_ref_no", BankReferenceNo);
            MyCommand.Parameters.AddWithValue("p_txn_amount", TxnAmount);
            MyCommand.Parameters.AddWithValue("p_bank_id", BankID);
            MyCommand.Parameters.AddWithValue("p_bank_merchant_id", BankMerchantID);
            MyCommand.Parameters.AddWithValue("p_txn_type", TxnType);
            MyCommand.Parameters.AddWithValue("p_currency_type", CurrencyName);
            MyCommand.Parameters.AddWithValue("p_item_code", ItemCode);
            MyCommand.Parameters.AddWithValue("p_security_type", SecurityType);
            MyCommand.Parameters.AddWithValue("p_security_id", SecurityID);
            MyCommand.Parameters.AddWithValue("p_security_password", SecurityPassword);
            MyCommand.Parameters.AddWithValue("p_txn_date", TxnDate);
            MyCommand.Parameters.AddWithValue("p_status", AuthStatus);

            MyCommand.Parameters.AddWithValue("p_settlement_type", SettlementType);
            MyCommand.Parameters.AddWithValue("p_error_status", ErrorStatus);
            MyCommand.Parameters.AddWithValue("p_error_description", ErrorDescription);
            MyCommand.Parameters.AddWithValue("p_checksum", CheckSum);
            MyCommand.Parameters.AddWithValue("p_checksum_key", checksumkey);
            MyCommand.Parameters.AddWithValue("p_response_string", MstringForCheckSum);

            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            if (dtt.TableName == "Success")
            {
                return dtt;
            }
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }
    public DataTable BILLDESKResponse(string MerchantID,
         string OrderID, string TxnReferenceNo, string BankReferenceNo, string TxnAmount, string BankID, string BankMerchantID,
         string TxnType, string CurrencyName, string ItemCode, string SecurityType,
         string SecurityID, string SecurityPassword, string TxnDate, string AuthStatus, string SettlementType,
         string ErrorStatus, string ErrorDescription, string CheckSum, string Responsemsg, string checksumkey,
         string MstringForCheckSum)
    {
        try
        {
            MyCommand = new NpgsqlCommand();
            MyCommand.Parameters.Clear();
            MyCommand.Parameters.AddWithValue("@StoredProcedure", "pg.f_billdeskresponse");
            
            MyCommand.Parameters.AddWithValue("p_merchant_id", MerchantID);
            MyCommand.Parameters.AddWithValue("p_order_id",OrderID);
            MyCommand.Parameters.AddWithValue("p_txn_ref_no", TxnReferenceNo);
            MyCommand.Parameters.AddWithValue("p_bank_ref_no", BankReferenceNo);
            MyCommand.Parameters.AddWithValue("p_txn_amount", TxnAmount);
            MyCommand.Parameters.AddWithValue("p_bank_id", BankID);
            MyCommand.Parameters.AddWithValue("p_bank_merchant_id", BankMerchantID);
            MyCommand.Parameters.AddWithValue("p_txn_type", TxnType);
            MyCommand.Parameters.AddWithValue("p_currency_type", CurrencyName);
            MyCommand.Parameters.AddWithValue("p_item_code", ItemCode);
            MyCommand.Parameters.AddWithValue("p_security_type", SecurityType);
            MyCommand.Parameters.AddWithValue("p_security_id", SecurityID);
            MyCommand.Parameters.AddWithValue("p_security_password", SecurityPassword);
            MyCommand.Parameters.AddWithValue("p_txn_date", TxnDate);
            MyCommand.Parameters.AddWithValue("p_status", AuthStatus);
           
            MyCommand.Parameters.AddWithValue("p_settlement_type", SettlementType);
            MyCommand.Parameters.AddWithValue("p_error_status", ErrorStatus);
            MyCommand.Parameters.AddWithValue("p_error_description", ErrorDescription);
            MyCommand.Parameters.AddWithValue("p_checksum", CheckSum);
            MyCommand.Parameters.AddWithValue("p_checksum_key", checksumkey);
            MyCommand.Parameters.AddWithValue("p_response_string", MstringForCheckSum);
            
            DataTable dtt = new DataTable();
            dtt = bll.SelectAll(MyCommand);
            if (dtt.TableName == "Success")
            {
                return dtt;
            }
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

}