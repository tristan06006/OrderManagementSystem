using OMS.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.Models
{
    public class User : ToDoItem
    {
        public string TableName { get; set; }
        public string U_USER_ID { get; set; }
        public string U_NAME { get; set; }
        [DataType(DataType.Password)]
        public string U_PASSWORD { get; set; }
        public string U_ADDRES { get; set; }
        public string U_CITY { get; set; }
        public string U_STATE { get; set; }
        public string U_ZIP { get; set; }
        public string U_COUNTRY { get; set; }
        public string U_EMAIL { get; set; }
        public string U_WEB { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
        public bool IsSignedIn { get; set; }
        public User()
        {
            internalDocType = "User";
            TableName = "T_User";
            U_USER_ID = string.Empty;
            U_NAME = string.Empty;
            U_PASSWORD = string.Empty;
            U_ADDRES = string.Empty;
            U_CITY = string.Empty;
            U_STATE = string.Empty;
            U_ZIP = string.Empty;
            U_COUNTRY = string.Empty;
            U_EMAIL = string.Empty;
            U_WEB = string.Empty;
            RememberMe = true;
            ReturnUrl = string.Empty;
            
        }

    }
    public class UserDocument
    {
        public int total_rows { get; set; }
        public int offset { get; set; }
        public UserRow[] rows { get; set; }

    }
    public class UserRow
    {
        public string id { get; set; }
        public string key { get; set; }
        public string value { get; set; }
        public UserDoc doc { get; set; }
    }

    

    public class UserDoc
    {
        public string _id { get; set; }
        public string _rev { get; set; }
        public string internalDocType { get; set; }
        public string TableName { get; set; }
        public string U_USER_ID { get; set; }
        public string U_NAME { get; set; }
        public string U_PASSWORD { get; set; }
        public string U_ADDRES { get; set; }
        public string U_CITY { get; set; }
        public string U_STATE { get; set; }
        public string U_ZIP { get; set; }
        public string U_COUNTRY { get; set; }
        public string U_EMAIL { get; set; }
        public string U_WEB { get; set; }
    }
}
