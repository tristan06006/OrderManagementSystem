using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.Models
{
    public class OrderLine : ToDoItem
    {
        public string TableName { get; set; }
        public string OL_O_ID { get; set; }
        public string OL_D_ID { get; set; }
        public string OL_W_ID { get; set; }
        public string OL_NUMBER { get; set; }
        public string OL_I_ID { get; set; }
        public string OL_SUPPLY_W_ID { get; set; }
        public DateTime OL_DELIVERY_D { get; set; }
        public double OL_QUANTITY { get; set; }
        public double OL_AMOUNT { get; set; }
        public string OL_DIST_INFO { get; set; }
        public string OL_BG { get; set; }

        public OrderLine()
        {
            internalDocType = "OrderLine";
            TableName = "T_OrderLine";
            OL_O_ID = string.Empty;
            OL_D_ID = string.Empty;
            OL_W_ID = string.Empty;
            OL_NUMBER = string.Empty;
            OL_I_ID = string.Empty;
            OL_SUPPLY_W_ID = string.Empty;
            //OL_DELIVERY_D = DateTime.Now;
            OL_QUANTITY = 0;
            OL_AMOUNT = 0;
            OL_DIST_INFO = string.Empty;
            OL_BG = string.Empty;
        }

    }
    public class OrderLineDocument
    {
        public int total_rows { get; set; }
        public int offset { get; set; }
        public OrderLineRow[] rows { get; set; }

    }
    public class OrderLineRow
    {
        public string id { get; set; }
        public string key { get; set; }
        public string value { get; set; }
        public OrderLineDoc doc { get; set; }
    }

    public class OrderLineValue
    {
        public string rev { get; set; }
    }

    public class OrderLineDoc
    {
        public string _id { get; set; }
        public string _rev { get; set; }
        public string internalDocType { get; set; }
        public string TableName { get; set; }
        public string OL_O_ID { get; set; }
        public string OL_D_ID { get; set; }
        public string OL_W_ID { get; set; }
        public string OL_NUMBER { get; set; }
        public string OL_I_ID { get; set; }
        public string OL_SUPPLY_W_ID { get; set; }
        public DateTime OL_DELIVERY_D { get; set; }
        public double OL_QUANTITY { get; set; }
        public double OL_AMOUNT { get; set; }
        public string OL_DIST_INFO { get; set; }
    }
}
