using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PayloadCLass
/// </summary>

	public class AdditionalInfo
	{
		public string additional_info1 { get; set; }
		public string additional_info2 { get; set; }
		public string additional_info3 { get; set; }
}
	public class Device
	{
		public string init_channel { get; set; }
		public string ip { get; set; }
		public string user_agent { get; set; }
		public string accept_header { get; set; }
		public string fingerprintid { get; set; }
		public string browser_tz { get; set; }
		public string browser_color_depth { get; set; }
		public string browser_java_enabled { get; set; }
		public string browser_screen_height { get; set; }
		public string browser_screen_width { get; set; }
		public string browser_language { get; set; }
		public string browser_javascript_enabled { get; set; }
	}
	public class payload
	{
		public string mercid { get; set; }
		public string orderid { get; set; }
		public string amount { get; set; }
		public string order_date { get; set; }
		public string currency { get; set; }
		public string ru { get; set; }
		public AdditionalInfo additional_info { get; set; }
		public string itemcode { get; set; }
		public Device device { get; set; }
	}

public class refundPayload
{
	public string transactionid { get; set; }
	public string orderid { get; set; }
	
	public string mercid { get; set; }
	public string transaction_date { get; set; }
	public string txn_amount { get; set; }
	public string refund_amount { get; set; }
	public string currency { get; set; }
	public string merc_refund_ref_no { get; set; }
	
}

public class refundStatusPayload
{

    public string mercid { get; set; }


    public string merc_refund_ref_no { get; set; }

}
