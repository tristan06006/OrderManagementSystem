using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.Models
{
    public class ViewWarehouseWiseDistrict : ToDoItem
    {
        public string TableName { get; set; }
        public string D_ID { get; set; }
        public string D_W_ID { get; set; }
        public string D_W_NAME { get; set; }
        public string D_NAME { get; set; }
        public string D_STREET_1 { get; set; }
        public string D_STREET_2 { get; set; }
        public string D_CITY { get; set; }
        public string D_STATE { get; set; }
        public string D_ZIP { get; set; }
        public double D_TAX { get; set; }
        public double D_YTD { get; set; }
        public string D_NEXT_O_ID { get; set; }
    }


    public class ViewWarehouseWiseDistrictDocument
    {
        public int total_rows { get; set; }
        public int offset { get; set; }
        public ViewWarehouseWiseDistrictRow[] rows { get; set; }
    }

    public class ViewWarehouseWiseDistrictRow
    {
        public string id { get; set; }
        public string[] key { get; set; }
        public string value { get; set; }
        public ViewWarehouseWiseDistrictDoc doc { get; set; }
    }

    public class ViewWarehouseWiseDistrictDoc
    {
        public string _id { get; set; }
        public string _rev { get; set; }
        public string internalDocType { get; set; }
        public string TableName { get; set; }
        public string D_ID { get; set; }
        public string D_W_ID { get; set; }
        public string D_NAME { get; set; }
        public string D_STREET_1 { get; set; }
        public string D_STREET_2 { get; set; }
        public string D_CITY { get; set; }
        public string D_STATE { get; set; }
        public double D_TAX { get; set; }
        public double D_YTD { get; set; }
        public string D_ZIP { get; set; }
        public string D_NEXT_O_ID { get; set; }
    }

}
