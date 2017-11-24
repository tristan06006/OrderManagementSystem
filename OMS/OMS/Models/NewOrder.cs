using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.Models
{
    public class NewOrder : ToDoItem
    {
        public string TableName { get; set; }
        public string NO_O_ID { get; set; }
        public string NO_D_ID { get; set; }
        public string NO_W_ID { get; set; }

        public NewOrder()
        {
            internalDocType = "NewOrder";
            TableName = "T_NewOrder";
            NO_O_ID = string.Empty;
            NO_D_ID = string.Empty;
            NO_W_ID = string.Empty;
        }

    }
    public class NewOrderDocument
    {
        public int total_rows { get; set; }
        public int offset { get; set; }
        public NewOrderRow[] rows { get; set; }

    }
    public class NewOrderRow
    {
        public string id { get; set; }
        public string key { get; set; }
        public string value { get; set; }
        public NewOrderDoc doc { get; set; }
    }

    public class NewOrderValue
    {
        public string rev { get; set; }
    }

    public class NewOrderDoc
    {
        public string _id { get; set; }
        public string _rev { get; set; }
        public string internalDocType { get; set; }
        public string TableName { get; set; }
        public string NO_O_ID { get; set; }
        public string NO_D_ID { get; set; }
        public string NO_W_ID { get; set; }
    }
}
