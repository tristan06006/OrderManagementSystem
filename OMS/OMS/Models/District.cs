using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.Models
{
    public class District : ToDoItem
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
        public IFormFile FileToUpload { get; set; }

        public List<Warehouse> WatehouseLists { get; set; }

        public District()
        {
            internalDocType = "District";
            TableName = "T_District";
            D_ID = string.Empty;
            D_W_ID = string.Empty;
            D_W_NAME = string.Empty;
            D_NAME = string.Empty;
            D_STREET_1 = string.Empty;
            D_STREET_2 = string.Empty;
            D_CITY = string.Empty;
            D_STATE = string.Empty;
            D_ZIP = string.Empty;
            D_TAX = 0;
            D_YTD = 0;
            D_NEXT_O_ID = string.Empty;

        }
    }

    public class DistrictDocument
    {
        public int total_rows { get; set; }
        public int offset { get; set; }
        public DistrictRow[] rows { get; set; }

    }
    public class DistrictRow
    {
        public string id { get; set; }
        public string key { get; set; }
        public string value { get; set; }
        public DistrictDoc doc { get; set; }
    }

    public class DistrictValue
    {
        public string rev { get; set; }
    }

    public class DistrictDoc
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
        public string D_ZIP { get; set; }
        public double D_TAX { get; set; }
        public double D_YTD { get; set; }
        public string D_NEXT_O_ID { get; set; }
    }
}
