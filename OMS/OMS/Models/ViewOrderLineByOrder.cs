using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.Models
{
    public class ViewOrderLineByOrder
    {
        public string OL_I_ID { get; set; }
        public string OL_I_NAME { get; set; }
        public string OL_SUPPLY_W_ID { get; set; }
        public string OL_SUPPLY_W_NAME { get; set; }
        public double OL_QUANTITY { get; set; }
        public double OL_Stock { get; set; }
        public string OL_BG { get; set; }
        public double OL_PRICE { get; set; }
        public double OL_AMOUNT { get; set; }
    }


    public class ViewOrderLineByOrderDocument
    {
        public int total_rows { get; set; }
        public int offset { get; set; }
        public ViewOrderLineByOrderRow[] rows { get; set; }
    }

    public class ViewOrderLineByOrderRow
    {
        public string id { get; set; }
        public string[] key { get; set; }
        public string value { get; set; }
        public ViewOrderLineByOrderDoc doc { get; set; }
    }

    public class ViewOrderLineByOrderDoc
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
        public int OL_QUANTITY { get; set; }
        public int OL_AMOUNT { get; set; }
        public string OL_DIST_INFO { get; set; }
        public string OL_BG { get; set; }
    }

}
