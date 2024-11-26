using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for JsonStringToDatabase
/// </summary>
public class JsonStringToDatabase
{
    public class MyData
    {
        public string value1 { get; set; }
        public string value2 { get; set; }
        public List<Dictionary<string, object>> json1 { get; set; }
        public List<Dictionary<string, object>> json2 { get; set; }
        public List<Dictionary<string, object>> json3 { get; set; }
    }


    public DataTable getDataSet(string jsonString)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        List<MyData> myDataList = serializer.Deserialize<List<MyData>>(jsonString);

        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("value1", typeof(string));
        dataTable.Columns.Add("value2", typeof(string));
        dataTable.Columns.Add("json1", typeof(string)); // This is a JSON string representation
        dataTable.Columns.Add("json2", typeof(string)); // This is a JSON string representation
        dataTable.Columns.Add("json3", typeof(string)); // This is a JSON string representation

        // Iterate through the List<MyData> and add rows to the DataTable
        foreach (var item in myDataList)
        {
            DataRow row = dataTable.NewRow();
            row["value1"] = item.value1;
            row["value2"] = item.value2;


            if (item.json1 != null)
            {
                if (item.json1.Count > 0)
                {
                    row["json1"] = serializer.Serialize(item.json1); // Serialize the List<MySubData> back to JSON
                }
            }

            if (item.json2 != null)
            {
                if (item.json2.Count > 0)
                {
                    row["json2"] = serializer.Serialize(item.json2); // Serialize the List<MySubData> back to JSON
                }
            }
            if (item.json3 != null)
            {
                if (item.json3.Count > 0)
                {

                    row["json3"] = serializer.Serialize(item.json3); // Serialize the List<MySubData> back to JSON
                }
            }
            dataTable.Rows.Add(row);
        }

        return dataTable;

    }

}