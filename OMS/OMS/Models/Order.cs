using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.Models
{
    public class Order : ToDoItem
    {
        public string TableName { get; set; }
        public string O_ID { get; set; }
        public string O_D_ID { get; set; }
        public string O_D_NAME { get; set; }
        public string O_W_ID { get; set; }
        public string O_W_NAME { get; set; }
        public string O_C_ID { get; set; }
        public string O_C_NAME { get; set; }
        public string O_C_CEDIT { get; set; }
        public double O_C_DISCOUNT { get; set; }
        public double O_W_TAX { get; set; }
        public double O_D_TAX { get; set; }
        public DateTime O_ENTRY_D { get; set; }
        public string O_CARRIER_ID { get; set; }
        public int O_OL_CNT { get; set; }
        public bool O_ALL_LOCAL { get; set; }
        public string OL_I_ID { get; set; }
        public string OL_SUPPLY_W_ID { get; set; }
        public double OL_QUANTITY { get; set; }
        public List<Warehouse> WatehouseLists { get; set; }
        public List<District> DistrictLists { get; set; }
        public List<Customer> CustomerLists { get; set; }
        public List<Item> ItemLists { get; set; }
        public List<ViewSupplierWarehouse> ViewSupplierWarehouseLists { get; set; }
        public List<OrderItem> OrderLists { get; set; }
        public List<ViewOrderLineByOrder> OrderLineItems { get; set; }

        public Order()
        {
            internalDocType = "Order";
            TableName = "T_Order";
            O_ID = string.Empty;
            O_D_ID = string.Empty;
            O_W_ID = string.Empty;
            O_C_ID = string.Empty;
            O_CARRIER_ID = string.Empty;
            O_ENTRY_D = DateTime.Now;
            O_OL_CNT = 0;
            O_ALL_LOCAL = false;
            OL_I_ID = string.Empty;
            OL_SUPPLY_W_ID = string.Empty;
            OL_QUANTITY = 0;
        }

    }

    public class OrderItem
    {
        public string OL_I_ID { get; set; }
        public string OL_SUPPLY_W_ID { get; set; }
        public double OL_QUANTITY { get; set; }

    }

   
    public class OrderDocument
    {
        public int total_rows { get; set; }
        public int offset { get; set; }
        public OrderRow[] rows { get; set; }

    }
    public class OrderRow
    {
        public string id { get; set; }
        public string key { get; set; }
        public string value { get; set; }
        public OrderDoc doc { get; set; }
    }

    public class OrderValue
    {
        public string rev { get; set; }
    }

    public class OrderDoc
    {
        public string _id { get; set; }
        public string _rev { get; set; }
        public string internalDocType { get; set; }
        public string TableName { get; set; }
        public string O_ID { get; set; }
        public string O_D_ID { get; set; }
        public string O_W_ID { get; set; }
        public string O_C_ID { get; set; }
        public DateTime O_ENTRY_D { get; set; }
        public string O_CARRIER_ID { get; set; }
        public int O_OL_CNT { get; set; }
        public bool O_ALL_LOCAL { get; set; }
    }

}
