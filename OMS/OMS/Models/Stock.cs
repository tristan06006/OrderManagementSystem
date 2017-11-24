using OMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.Models
{
    public class Stock : ToDoItem
    {
        public string TableName { get; set; }
        public string S_ID { get; set; }
        public string S_I_ID { get; set; }
        public string S_I_NAME { get; set; }
        public string S_W_ID { get; set; }
        public string S_W_NAME { get; set; }
        public double S_QUANTITY { get; set; }
        public string S_DIST_01_ID { get; set; }
        public double S_DIST_01_VALUE { get; set; }
        public string S_DIST_02_ID { get; set; }
        public double S_DIST_02_VALUE { get; set; }
        public string S_DIST_03_ID { get; set; }
        public double S_DIST_03_VALUE { get; set; }
        public string S_DIST_04_ID { get; set; }
        public double S_DIST_04_VALUE { get; set; }
        public string S_DIST_05_ID { get; set; }
        public double S_DIST_05_VALUE { get; set; }
        public string S_DIST_06_ID { get; set; }
        public double S_DIST_06_VALUE { get; set; }
        public string S_DIST_07_ID { get; set; }
        public double S_DIST_07_VALUE { get; set; }
        public string S_DIST_08_ID { get; set; }
        public double S_DIST_08_VALUE { get; set; }
        public string S_DIST_09_ID { get; set; }
        public double S_DIST_09_VALUE { get; set; }
        public string S_DIST_10_ID { get; set; }
        public double S_DIST_10_VALUE { get; set; }
        public double S_YTD { get; set; }
        public double S_ORDER_CNT { get; set; }
        public double S_REMOTE_CNT { get; set; }
        public string S_DATA { get; set; }
        public List<Warehouse> WatehouseLists { get; set; }
        public List<Item> ItemLists { get; set; }
        public Stock()
        {
            internalDocType = "Stock";
            TableName = "T_Stock";
            S_ID = string.Empty;
            S_I_ID = string.Empty;
            S_I_NAME = string.Empty;
            S_W_ID = string.Empty;
            S_W_NAME = string.Empty;
            S_QUANTITY = 0;
            S_DIST_01_ID = string.Empty;
            S_DIST_01_VALUE = 0;
            S_DIST_02_ID = string.Empty;
            S_DIST_02_VALUE = 0;
            S_DIST_03_ID = string.Empty;
            S_DIST_03_VALUE = 0;
            S_DIST_04_ID = string.Empty;
            S_DIST_04_VALUE = 0;
            S_DIST_05_ID = string.Empty;
            S_DIST_05_VALUE = 0;
            S_DIST_06_ID = string.Empty;
            S_DIST_06_VALUE = 0;
            S_DIST_07_ID = string.Empty;
            S_DIST_07_VALUE = 0;
            S_DIST_08_ID = string.Empty;
            S_DIST_08_VALUE = 0;
            S_DIST_09_ID = string.Empty;
            S_DIST_09_VALUE = 0;
            S_DIST_10_ID = string.Empty;
            S_DIST_10_VALUE = 0;
            S_YTD = 0;
            S_ORDER_CNT = 0;
            S_REMOTE_CNT = 0;
            S_DATA = string.Empty;
            //WatehouseLists = CloudantService.GetWarehouseList();
            //ItemLists = CloudantService.GetItemList();
        }

    }
    public class StockDocument
    {
        public int total_rows { get; set; }
        public int offset { get; set; }
        public StockRow[] rows { get; set; }

    }
    public class StockRow
    {
        public string id { get; set; }
        public string key { get; set; }
        public string value { get; set; }
        public StockDoc doc { get; set; }
    }

    public class StockValue
    {
        public string rev { get; set; }
    }

    public class StockDoc
    {
        public string _id { get; set; }
        public string _rev { get; set; }
        public string internalDocType { get; set; }
        public string TableName { get; set; }
        public string S_ID { get; set; }
        public string S_I_ID { get; set; }
        public string S_W_ID { get; set; }
        public double S_QUANTITY { get; set; }
        public string S_DIST_01_ID { get; set; }
        public double S_DIST_01_VALUE { get; set; }
        public string S_DIST_02_ID { get; set; }
        public double S_DIST_02_VALUE { get; set; }
        public string S_DIST_03_ID { get; set; }
        public double S_DIST_03_VALUE { get; set; }
        public string S_DIST_04_ID { get; set; }
        public double S_DIST_04_VALUE { get; set; }
        public string S_DIST_05_ID { get; set; }
        public double S_DIST_05_VALUE { get; set; }
        public string S_DIST_06_ID { get; set; }
        public double S_DIST_06_VALUE { get; set; }
        public string S_DIST_07_ID { get; set; }
        public double S_DIST_07_VALUE { get; set; }
        public string S_DIST_08_ID { get; set; }
        public double S_DIST_08_VALUE { get; set; }
        public string S_DIST_09_ID { get; set; }
        public double S_DIST_09_VALUE { get; set; }
        public string S_DIST_10_ID { get; set; }
        public double S_DIST_10_VALUE { get; set; }
        public double S_YTD { get; set; }
        public double S_ORDER_CNT { get; set; }
        public double S_REMOTE_CNT { get; set; }
        public string S_DATA { get; set; }
    }
}
