using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.Models
{
    public class History : ToDoItem
    {
        public string TableName { get; set; }
        public string OperationalTable { get; set; }
        public string H_C_ID { get; set; }
        public string H_C_D_ID { get; set; }
        public string H_C_W_ID { get; set; }
        public string H_D_ID { get; set; }
        public string H_W_ID { get; set; }
        public DateTime H_DATE { get; set; }
        public double H_AMOUNT { get; set; }
        public string H_DATA { get; set; }

        public History()
        {
            internalDocType = "History";
            TableName = "T_History";
            OperationalTable = string.Empty;
            H_C_ID = string.Empty;
            H_C_D_ID = string.Empty;
            H_C_W_ID = string.Empty;
            H_D_ID = string.Empty;
            H_W_ID = string.Empty;
            H_DATE = DateTime.Now;
            H_AMOUNT = 0;
            H_DATA = string.Empty;
        }

    }
    public class HistoryDocument
    {
        public int total_rows { get; set; }
        public int offset { get; set; }
        public HistoryRow[] rows { get; set; }

    }
    public class HistoryRow
    {
        public string id { get; set; }
        public string key { get; set; }
        public string value { get; set; }
        public HistoryDoc doc { get; set; }
    }

    public class HistoryValue
    {
        public string rev { get; set; }
    }

    public class HistoryDoc
    {
        public string _id { get; set; }
        public string _rev { get; set; }
        public string internalDocType { get; set; }
        public string TableName { get; set; }
        public string OperationalTable { get; set; }
        public string H_C_ID { get; set; }
        public string H_C_D_ID { get; set; }
        public string H_C_W_ID { get; set; }
        public string H_D_ID { get; set; }
        public string H_W_ID { get; set; }
        public DateTime H_DATE { get; set; }
        public double H_AMOUNT { get; set; }
        public string H_DATA { get; set; }
    }
}
