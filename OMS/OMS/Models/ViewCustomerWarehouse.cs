using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.Models
{
    public class ViewCustomerWarehouse
    {
        public string _id { get; set; }
        public string _rev { get; set; }
        public string C_ID { get; set; }
        public string C_D_ID { get; set; }
        public string C_D_NAME { get; set; }
        public string C_W_ID { get; set; }
        public string C_W_NAME { get; set; }
        public string C_CREDIT { get; set; }
        public string C_LAST { get; set; }
        public double C_DISCOUNT { get; set; }
        public double W_TAX { get; set; }
    }


    public class ViewCustomerWarehouseDocument
    {
        public int total_rows { get; set; }
        public int offset { get; set; }
        public ViewCustomerWarehouseRow[] rows { get; set; }
    }

    public class ViewCustomerWarehouseRow
    {
        public string id { get; set; }
        public string[] key { get; set; }
        public string value { get; set; }
        public ViewCustomerWarehouseDoc doc { get; set; }
    }

    public class ViewCustomerWarehouseDoc
    {
        public string _id { get; set; }
        public string _rev { get; set; }
        public string internalDocType { get; set; }
        public string TableName { get; set; }
        public string C_ID { get; set; }
        public string C_W_ID { get; set; }
        public string C_D_ID { get; set; }
        public string C_FIRST { get; set; }
        public string C_MIDDLE { get; set; }
        public string C_LAST { get; set; }
        public string C_STREET_1 { get; set; }
        public string C_STREET_2 { get; set; }
        public string C_CITY { get; set; }
        public string C_STATE { get; set; }
        public string C_ZIP { get; set; }
        public string C_PHONE { get; set; }
        public DateTime C_SINCE { get; set; }
        public string C_CREDIT { get; set; }
        public double C_CREDIT_LIM { get; set; }
        public double C_DISCOUNT { get; set; }
        public double C_BALANCE { get; set; }
        public double C_YTD_PAYMENT { get; set; }
        public double C_PAYMENT_CNT { get; set; }
        public double C_DELIVERY_CNT { get; set; }
        public string C_DATA { get; set; }
    }



}
