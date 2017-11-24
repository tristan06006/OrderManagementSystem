using OMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.Models
{
    public class Customer : ToDoItem
    {
        public string TableName { get; set; }
        public string C_ID { get; set; }
        public string C_D_ID { get; set; }
        public string C_D_NAME { get; set; }
        public string C_W_ID { get; set; }
        public string C_W_NAME { get; set; }
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
        public string C_CREDIT_NAME { get; set; }
        public double C_CREDIT_LIM { get; set; }
        public double C_DISCOUNT { get; set; }
        public double C_BALANCE { get; set; }
        public double C_YTD_PAYMENT { get; set; }
        public double C_PAYMENT_CNT { get; set; }
        public double C_DELIVERY_CNT { get; set; }
        public string C_DATA { get; set; }
        public List<Warehouse> WatehouseLists { get; set; }
        public List<District> DistrictLists { get; set; }
        public List<CustomerCredit> CustomerCreditLists { get; set; }
        public Customer()
        {
            TableName = "T_Customer";
            internalDocType = "Customer";
            C_ID = string.Empty;
            C_D_ID = string.Empty;
            C_D_NAME = string.Empty;
            C_W_ID = string.Empty;
            C_W_NAME = string.Empty;
            C_FIRST = string.Empty;
            C_MIDDLE = string.Empty;
            C_LAST = string.Empty;
            C_STREET_1 = string.Empty;
            C_STREET_2 = string.Empty;
            C_CITY = string.Empty;
            C_STATE = string.Empty;
            C_ZIP = string.Empty;
            C_PHONE = string.Empty;
            C_SINCE = DateTime.Now;
            C_CREDIT = "GC"; //"BC"
            C_CREDIT_NAME = string.Empty;
            C_CREDIT_LIM = 0;
            C_DISCOUNT = 0;
            C_BALANCE = 0;
            C_YTD_PAYMENT = 0;
            C_PAYMENT_CNT = 0;
            C_DELIVERY_CNT = 0;
            C_DATA = string.Empty;
            CustomerCreditLists = CloudantService.GetCustomerCreditInfo();
        }
    }

   

    public class CustomerCredit
    {
        public string id { get; set; }
        public string name { get; set; }
    }
    public class CustomerDocument
    {
        public int total_rows { get; set; }
        public int offset { get; set; }
        public CustomerRow[] rows { get; set; }
    }
    public class CustomerRow
    {
        public string id { get; set; }
        public string key { get; set; }
        public string value { get; set; }
        public CustomerDoc doc { get; set; }
    }

    public class CustomerValue
    {
        public string rev { get; set; }
    }

    public class CustomerDoc
    {
        public string _id { get; set; }
        public string _rev { get; set; }
        public string internalDocType { get; set; }
        public string TableName { get; set; }
        public string C_ID { get; set; }
        public string C_D_ID { get; set; }
        public string C_W_ID { get; set; }
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
