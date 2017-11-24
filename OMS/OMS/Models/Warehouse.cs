using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.Models
{
    public class Warehouse : ToDoItem
    {
        public string TableName { get; set; }
        public string W_ID { get; set; }
        public string W_NAME { get; set; }
        public string W_STREET_1 { get; set; }
        public string W_STREET_2 { get; set; }
        public string W_CITY { get; set; }
        public string W_STATE { get; set; }
        public double W_TAX { get; set; }
        public double W_WTD { get; set; }
        public string W_ZIP { get; set; }

        public Warehouse()
        {
            internalDocType = "Warehouse";
            TableName = "T_Warehouse";
            W_ID = string.Empty;
            W_NAME = string.Empty;
            W_STREET_1 = string.Empty;
            W_STREET_2 = string.Empty;
            W_CITY = string.Empty;
            W_STATE = string.Empty;
            W_TAX = 0;
            W_WTD = 0;
            W_ZIP = string.Empty;
        }

    }
    public class WarehouseDocument
    {
        public int total_rows { get; set; }
        public int offset { get; set; }
        public WarehouseRow[] rows { get; set; }

    }
    public class WarehouseRow
    {
        public string id { get; set; }
        public string key { get; set; }
        //public WarehouseValue value { get; set; }
        public string value { get; set; }
        public WarehouseDoc doc { get; set; }
    }

    public class WarehouseValue
    {
        public string rev { get; set; }
    }

    public class WarehouseDoc
    {
        public string _id { get; set; }
        public string _rev { get; set; }
        public string internalDocType { get; set; }
        public string TableName { get; set; }
        public string W_ID { get; set; }
        public string W_NAME { get; set; }
        public string W_STREET_1 { get; set; }
        public string W_STREET_2 { get; set; }
        public string W_CITY { get; set; }
        public string W_STATE { get; set; }
        public double W_TAX { get; set; }
        public double W_WTD { get; set; }
        public string W_ZIP { get; set; }
    }

}
