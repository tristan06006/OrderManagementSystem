using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.Models
{
    public class ViewSupplierWarehouse : ToDoItem
    {
        public string TableName { get; set; }
        public string SUPPLY_W_ID { get; set; }
        public string SUPPLY_W_NAME { get; set; }
        public string W_STREET_1 { get; set; }
        public string W_STREET_2 { get; set; }
        public string W_CITY { get; set; }
        public string W_STATE { get; set; }
        public double W_TAX { get; set; }
        public double W_WTD { get; set; }
        public string W_ZIP { get; set; }

        public ViewSupplierWarehouse()
        {
            internalDocType = "Warehouse";
            TableName = "V_Warehouse";
            SUPPLY_W_ID = string.Empty;
            SUPPLY_W_NAME = string.Empty;
            W_STREET_1 = string.Empty;
            W_STREET_2 = string.Empty;
            W_CITY = string.Empty;
            W_STATE = string.Empty;
            W_TAX = 0;
            W_WTD = 0;
            W_ZIP = string.Empty;
        }
    }


    public class ViewSupplierWarehouseDocument
    {
        public int total_rows { get; set; }
        public int offset { get; set; }
        public ViewSupplierWarehouseRow[] rows { get; set; }
    }

    public class ViewSupplierWarehouseRow
    {
        public string id { get; set; }
        public string key { get; set; }
        public ViewSupplierWarehouseValue value { get; set; }
        public ViewSupplierWarehouseDoc doc { get; set; }
    }

    public class ViewSupplierWarehouseValue
    {
        public string _id { get; set; }
    }

    public class ViewSupplierWarehouseDoc
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
