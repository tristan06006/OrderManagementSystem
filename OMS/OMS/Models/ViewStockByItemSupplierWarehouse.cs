using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.Models
{
    public class ViewStockByItemSupplierWarehouse : ToDoItem
    {
        public string TableName { get; set; }
        public string S_ID { get; set; }
        public string S_W_ID { get; set; }
        public string S_I_ID { get; set; }
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


    public class ViewStockByItemSupplierWarehouseDocument
    {
        public int total_rows { get; set; }
        public int offset { get; set; }
        public ViewStockByItemSupplierWarehouseRow[] rows { get; set; }
    }

    public class ViewStockByItemSupplierWarehouseRow
    {
        public string id { get; set; }
        public string[] key { get; set; }
        public string value { get; set; }
        public ViewStockByItemSupplierWarehouseDoc doc { get; set; }
    }

    public class ViewStockByItemSupplierWarehouseDoc
    {
        public string _id { get; set; }
        public string _rev { get; set; }
        public string internalDocType { get; set; }
        public string TableName { get; set; }
        public string S_ID { get; set; }
        public string S_W_ID { get; set; }
        public string S_I_ID { get; set; }
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
