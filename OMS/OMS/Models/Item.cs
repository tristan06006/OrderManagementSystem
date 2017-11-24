using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.Models
{
    public class Item : ToDoItem
    {
        public string TableName { get; set; }
        public string I_ID { get; set; }
        public string I_IM_ID { get; set; }
        public string I_NAME { get; set; }
        public double I_PRICE { get; set; }
        public string I_DATA { get; set; }

        public Item()
        {
            TableName = "T_Item";
            internalDocType = "Item";
            I_ID = string.Empty;
            I_IM_ID = string.Empty;
            I_NAME = string.Empty;
            I_PRICE = 0;
            I_DATA = string.Empty;
           
        }
    }

    public class ItemDocument
    {
        public int total_rows { get; set; }
        public int offset { get; set; }
        public ItemRow[] rows { get; set; }

    }
    public class ItemRow
    {
        public string id { get; set; }
        public string key { get; set; }
        public string value { get; set; }
        public ItemDoc doc { get; set; }
    }

    public class ItemValue
    {
        public string rev { get; set; }
    }

    public class ItemDoc
    {
        public string _id { get; set; }
        public string _rev { get; set; }
        public string internalDocType { get; set; }
        public string TableName { get; set; }
        public string I_ID { get; set; }
        public string I_IM_ID { get; set; }
        public string I_NAME { get; set; }
        public double I_PRICE { get; set; }
        public string I_DATA { get; set; }
    }
}
