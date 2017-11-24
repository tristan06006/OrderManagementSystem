using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OMS.Models;
using System.Net.Http.Headers;
using System.Dynamic;
using System.Reflection;

namespace OMS.Services
{
    public static class CloudantService
    {
        const string user = "3e59275b-6c81-4593-b1ca-3fdc0f9024a0-bluemix";
        const string password = "529dcbca4868e7b153ad6182f564b5cb3e83bb9c43e7543bfc13e230c04f4e16";
        const string database = "inventory";
        //const string sURL = "https://3e59275b-6c81-4593-b1ca-3fdc0f9024a0-bluemix:529dcbca4868e7b153ad6182f564b5cb3e83bb9c43e7543bfc13e230c04f4e16@3e59275b-6c81-4593-b1ca-3fdc0f9024a0-bluemix.cloudant.com/inventory/_all_docs?include_docs=true";
        static string sURL = "https://3e59275b-6c81-4593-b1ca-3fdc0f9024a0-bluemix:529dcbca4868e7b153ad6182f564b5cb3e83bb9c43e7543bfc13e230c04f4e16@3e59275b-6c81-4593-b1ca-3fdc0f9024a0-bluemix.cloudant.com/inventory";

        //sURL = "https://3e59275b-6c81-4593-b1ca-3fdc0f9024a0-bluemix:529dcbca4868e7b153ad6182f564b5cb3e83bb9c43e7543bfc13e230c04f4e16@3e59275b-6c81-4593-b1ca-3fdc0f9024a0-bluemix.cloudant.com/inventory/_design/submittedView/_list/first-format/submitted-view?include_docs=true";

        #region Warehouse
        public static string SaveWarehouse(Warehouse warehouse)
        {
            try
            {
                var handler = new HttpClientHandler { Credentials = new NetworkCredential(user, password) };

                using (var client = CreateHttpClient(handler, user, database))
                {
                    var creationResponse = Create(client, new
                    {
                        _id = warehouse._id,
                        internalDocType = warehouse.internalDocType,
                        TableName = warehouse.TableName,
                        W_ID = warehouse.W_ID,
                        W_NAME = warehouse.W_NAME,
                        W_STREET_1 = warehouse.W_STREET_1,
                        W_STREET_2 = warehouse.W_STREET_2,
                        W_CITY = warehouse.W_CITY,
                        W_STATE = warehouse.W_STATE,
                        W_TAX = warehouse.W_TAX,
                        W_WTD = warehouse.W_WTD,
                        W_ZIP = warehouse.W_ZIP
                    });

                    if (creationResponse.StatusCode.ToString().ToUpper().Equals("CREATED"))
                    {
                        History hist = new History();
                        hist._id = "HISTORY_" + Guid.NewGuid().ToString();
                        hist.H_W_ID = warehouse.W_ID;
                        hist.H_DATE = DateTime.Now;
                        hist.OperationalTable = warehouse.TableName;
                        hist.H_AMOUNT = warehouse.W_WTD;
                        hist.H_DATA = "Insertion into " + warehouse.TableName + " with Sales Tax: " + warehouse.W_TAX.ToString() + " and Year to date balance: " + warehouse.W_WTD.ToString();

                        string responses = SaveHistory(hist, handler, client);
                    }

                    return creationResponse.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public static string UpdateWarehouse(Warehouse warehouse)
        {
            try
            {
                var handler = new HttpClientHandler { Credentials = new NetworkCredential(user, password) };

                using (var client = CreateHttpClient(handler, user, database))
                {
                    var updateResponse = Update(client, warehouse._id, new
                    {
                        _id = warehouse._id,
                        internalDocType = warehouse.internalDocType,
                        TableName = warehouse.TableName,
                        W_ID = warehouse.W_ID,
                        W_NAME = warehouse.W_NAME,
                        W_STREET_1 = warehouse.W_STREET_1,
                        W_STREET_2 = warehouse.W_STREET_2,
                        W_CITY = warehouse.W_CITY,
                        W_STATE = warehouse.W_STATE,
                        W_TAX = warehouse.W_TAX,
                        W_WTD = warehouse.W_WTD,
                        W_ZIP = warehouse.W_ZIP,
                        _rev = warehouse._rev
                    });

                    if (updateResponse.StatusCode.ToString().ToUpper().Equals("CREATED"))
                    {
                        History hist = new History();
                        hist._id = "HISTORY_" + Guid.NewGuid().ToString();
                        hist.H_W_ID = warehouse.W_ID;
                        hist.H_DATE = DateTime.Now;
                        hist.OperationalTable = warehouse.TableName;
                        hist.H_AMOUNT = warehouse.W_WTD;
                        hist.H_DATA = "Updated into " + warehouse.TableName + " with Sales Tax: " + warehouse.W_TAX.ToString() + " and Year to date balance: " + warehouse.W_WTD.ToString() + " using revisionId: " + warehouse._rev;

                        string responses = SaveHistory(hist, handler, client);
                    }

                    return updateResponse.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public static List<Warehouse> GetWarehouseList()
        {
            string stringData = GetAllDocuments("/_design/designwarehouse/_view/viewwarehouse?include_docs=true");

            WarehouseDocument oMyclass = JsonConvert.DeserializeObject<WarehouseDocument>(stringData);
            List<Warehouse> warehouseList = new List<Warehouse>();

            if (!string.IsNullOrEmpty(stringData))
            {
                if (oMyclass.total_rows > 0)
                {

                    for (int i = 0; i < oMyclass.total_rows; i++)
                    {
                        if (oMyclass.rows[i].doc.internalDocType.ToUpper().Equals("WAREHOUSE"))
                        {
                            Warehouse ware = new Warehouse();
                            ware._id = oMyclass.rows[i].doc._id;
                            ware._rev = oMyclass.rows[i].doc._rev;
                            ware.internalDocType = oMyclass.rows[i].doc.internalDocType;
                            ware.TableName = oMyclass.rows[i].doc.TableName;
                            ware.W_ID = oMyclass.rows[i].doc.W_ID;
                            ware.W_NAME = oMyclass.rows[i].doc.W_NAME;
                            ware.W_STREET_1 = oMyclass.rows[i].doc.W_STREET_1;
                            ware.W_STREET_2 = oMyclass.rows[i].doc.W_STREET_2;
                            ware.W_CITY = oMyclass.rows[i].doc.W_CITY;
                            ware.W_STATE = oMyclass.rows[i].doc.W_STATE;
                            ware.W_ZIP = oMyclass.rows[i].doc.W_ZIP;
                            ware.W_TAX = oMyclass.rows[i].doc.W_TAX;
                            ware.W_WTD = oMyclass.rows[i].doc.W_WTD;
                            warehouseList.Add(ware);
                        }
                    }

                }

            }

            return warehouseList;
        }
        public static List<ViewSupplierWarehouse> GetSupplierWarehouseList()
        {
            string stringData = GetAllDocuments("/_design/designsupplierwarehouse/_view/viewsupplierwarehouse?include_docs=true");

            ViewSupplierWarehouseDocument oMyclass = JsonConvert.DeserializeObject<ViewSupplierWarehouseDocument>(stringData);
            List<ViewSupplierWarehouse> warehouseList = new List<ViewSupplierWarehouse>();

            if (!string.IsNullOrEmpty(stringData))
            {
                if (oMyclass.total_rows > 0)
                {

                    for (int i = 0; i < oMyclass.total_rows; i++)
                    {
                        ViewSupplierWarehouse ware = new ViewSupplierWarehouse();
                        ware._id = oMyclass.rows[i].doc._id;
                        ware._rev = oMyclass.rows[i].doc._rev;
                        ware.internalDocType = oMyclass.rows[i].doc.internalDocType;
                        ware.TableName = oMyclass.rows[i].doc.TableName;
                        ware.SUPPLY_W_ID = oMyclass.rows[i].doc.W_ID;
                        ware.SUPPLY_W_NAME = oMyclass.rows[i].doc.W_NAME;
                        ware.W_STREET_1 = oMyclass.rows[i].doc.W_STREET_1;
                        ware.W_STREET_2 = oMyclass.rows[i].doc.W_STREET_2;
                        ware.W_CITY = oMyclass.rows[i].doc.W_CITY;
                        ware.W_STATE = oMyclass.rows[i].doc.W_STATE;
                        ware.W_ZIP = oMyclass.rows[i].doc.W_ZIP;
                        ware.W_TAX = oMyclass.rows[i].doc.W_TAX;
                        ware.W_WTD = oMyclass.rows[i].doc.W_WTD;
                        warehouseList.Add(ware);

                    }

                }

            }

            return warehouseList;
        }
        public static Warehouse GetWarehouseInfo(string warehouseId)
        {
            string stringData = GetSingleDocument(warehouseId);
            Warehouse warehouse = JsonConvert.DeserializeObject<Warehouse>(stringData);
            return warehouse;
        }

        public static ViewCustomerWarehouse GetWarehouseWiseCustomerList(string sCustomerId, string sWarehouseId, string sDistrictId)
        {
            string stringData = GetAllDocuments("/_design/orderdesign/_view/viewcustomerwarehouse?key=[%22" + sCustomerId + "%22,%22" + sWarehouseId + "%22,%22" + sDistrictId + "%22]&include_docs=true");

            ViewCustomerWarehouseDocument oMyclass = JsonConvert.DeserializeObject<ViewCustomerWarehouseDocument>(stringData);
            ViewCustomerWarehouse ware = new ViewCustomerWarehouse();

            if (!string.IsNullOrEmpty(stringData))
            {
                if (oMyclass.total_rows > 0)
                {
                    int i = 0;
                    Warehouse warehouse = GetWarehouseInfo(oMyclass.rows[i].doc.C_W_ID);
                    ware._id = oMyclass.rows[i].doc._id;
                    ware._rev = oMyclass.rows[i].doc._rev;
                    ware.C_D_ID = oMyclass.rows[i].doc.C_D_ID;
                    ware.C_W_ID = oMyclass.rows[i].doc.C_W_ID;
                    ware.C_ID = oMyclass.rows[i].doc.C_ID;
                    ware.C_DISCOUNT = oMyclass.rows[i].doc.C_DISCOUNT;
                    ware.C_LAST = oMyclass.rows[i].doc.C_LAST;
                    ware.C_CREDIT = oMyclass.rows[i].doc.C_CREDIT;
                    ware.W_TAX = warehouse.W_TAX;

                }

            }

            return ware;
        }

        public static string GetUniqueId(string tableName)
        {
            string sId = tableName.ToUpper() + "_" + Guid.NewGuid();
            string stringData = GetSingleDocument(sId);
            if (stringData.ToLower().Contains("not_found") || stringData.ToLower().Contains("missing"))
            {

                return sId;
            }
            else
            {
                sId = GetUniqueId(tableName);
                return sId;
            }

        }

        #endregion

        #region District
        public static string SaveDistrict(District district)
        {
            try
            {
                var handler = new HttpClientHandler { Credentials = new NetworkCredential(user, password) };

                using (var client = CreateHttpClient(handler, user, database))
                {
                    var creationResponse = Create(client, new
                    {
                        _id = district._id,
                        internalDocType = district.internalDocType,
                        TableName = district.TableName,
                        D_ID = district.D_ID,
                        D_W_ID = district.D_W_ID,
                        D_NAME = district.D_NAME,
                        D_STREET_1 = district.D_STREET_1,
                        D_STREET_2 = district.D_STREET_2,
                        D_CITY = district.D_CITY,
                        D_STATE = district.D_STATE,
                        D_TAX = district.D_TAX,
                        D_YTD = district.D_YTD,
                        D_ZIP = district.D_ZIP,
                        D_NEXT_O_ID = district.D_NEXT_O_ID
                    });

                    if (creationResponse.StatusCode.ToString().ToUpper().Equals("CREATED"))
                    {
                        History hist = new History();
                        hist._id = "HISTORY_" + Guid.NewGuid().ToString();
                        hist.H_D_ID = district.D_ID;
                        hist.H_W_ID = district.D_W_ID;
                        hist.H_DATE = DateTime.Now;
                        hist.OperationalTable = district.TableName;
                        hist.H_AMOUNT = district.D_YTD;
                        hist.H_DATA = "Insert into " + district.TableName + " with Sales Tax: " + district.D_TAX.ToString() + " and Year to date balance: " + district.D_YTD.ToString();

                        string responses = SaveHistory(hist, handler, client);
                    }

                    return creationResponse.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public static string UpdateDistrict(District district)
        {
            try
            {
                var handler = new HttpClientHandler { Credentials = new NetworkCredential(user, password) };

                using (var client = CreateHttpClient(handler, user, database))
                {
                    var updateResponse = Update(client, district._id, new
                    {
                        _id = district._id,
                        internalDocType = district.internalDocType,
                        TableName = district.TableName,
                        D_ID = district.D_ID,
                        D_W_ID = district.D_W_ID,
                        D_NAME = district.D_NAME,
                        D_STREET_1 = district.D_STREET_1,
                        D_STREET_2 = district.D_STREET_2,
                        D_CITY = district.D_CITY,
                        D_STATE = district.D_STATE,
                        D_TAX = district.D_TAX,
                        D_YTD = district.D_YTD,
                        D_ZIP = district.D_ZIP,
                        D_NEXT_O_ID = district.D_NEXT_O_ID,
                        _rev = district._rev
                    });

                    if (updateResponse.StatusCode.ToString().ToUpper().Equals("CREATED"))
                    {
                        History hist = new History();
                        hist._id = "HISTORY_" + Guid.NewGuid().ToString();
                        hist.H_D_ID = district.D_ID;
                        hist.H_W_ID = district.D_W_ID;
                        hist.H_DATE = DateTime.Now;
                        hist.OperationalTable = district.TableName;
                        hist.H_AMOUNT = district.D_YTD;
                        hist.H_DATA = "Insert into " + district.TableName + " with Sales Tax: " + district.D_TAX.ToString() + " and Year to date balance: " + district.D_YTD.ToString() + " using revisionId: " + district._rev;

                        string responses = SaveHistory(hist, handler, client);
                    }

                    return updateResponse.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public static District GetDistrictInfo(string districtId)
        {
            string stringData = GetSingleDocument(districtId);
            District district = JsonConvert.DeserializeObject<District>(stringData);
            return district;
        }
        public static List<District> GetDistrictList()
        {
            string stringData = GetAllDocuments("/_design/designdistrict/_view/viewdistrict?include_docs=true");
            DistrictDocument oMyclass = JsonConvert.DeserializeObject<DistrictDocument>(stringData);
            List<District> districtList = new List<District>();

            if (!string.IsNullOrEmpty(stringData))
            {
                if (oMyclass.total_rows > 0)
                {
                    for (int i = 0; i < oMyclass.total_rows; i++)
                    {
                        if (oMyclass.rows[i].doc.internalDocType.ToUpper().Equals("DISTRICT"))
                        {
                            Warehouse dist = JsonConvert.DeserializeObject<Warehouse>(CloudantService.GetSingleDocument(oMyclass.rows[i].doc.D_W_ID));

                            District district = new District();
                            district._id = oMyclass.rows[i].doc._id;
                            district._rev = oMyclass.rows[i].doc._rev;
                            district.internalDocType = oMyclass.rows[i].doc.internalDocType;
                            district.TableName = oMyclass.rows[i].doc.TableName;
                            district.D_ID = oMyclass.rows[i].doc.D_ID;
                            district.D_W_ID = oMyclass.rows[i].doc.D_W_ID;
                            district.D_W_NAME = dist.W_NAME;
                            district.D_NAME = oMyclass.rows[i].doc.D_NAME;
                            district.D_STREET_1 = oMyclass.rows[i].doc.D_STREET_1 + ", " + oMyclass.rows[i].doc.D_STREET_2;
                            district.D_STREET_2 = oMyclass.rows[i].doc.D_STREET_2;
                            district.D_CITY = oMyclass.rows[i].doc.D_CITY;
                            district.D_STATE = oMyclass.rows[i].doc.D_STATE;
                            district.D_ZIP = oMyclass.rows[i].doc.D_ZIP;
                            district.D_TAX = oMyclass.rows[i].doc.D_TAX;
                            district.D_YTD = oMyclass.rows[i].doc.D_YTD;
                            district.D_NEXT_O_ID = oMyclass.rows[i].doc.D_NEXT_O_ID;
                            districtList.Add(district);
                        }
                    }
                }
            }

            return districtList;
        }
        public static List<District> GetDistrictListByWarehouseId(string sWarehouseId)
        {
            string stringData = GetAllDocuments("/_design/designdistrictlistbywarehouse/_view/viewdistrictlistbywarehouse?key=[%22" + sWarehouseId + "%22]&include_docs=true");
            ViewWarehouseWiseDistrictDocument oMyclass = JsonConvert.DeserializeObject<ViewWarehouseWiseDistrictDocument>(stringData);
            List<District> districtList = new List<District>();

            if (oMyclass.total_rows > 0)
            {
                for (int i = 0; i < oMyclass.offset; i++)
                {
                    District district = new District();
                    district._id = oMyclass.rows[i].doc._id;
                    district._rev = oMyclass.rows[i].doc._rev;
                    district.internalDocType = oMyclass.rows[i].doc.internalDocType;
                    district.TableName = oMyclass.rows[i].doc.TableName;
                    district.D_ID = oMyclass.rows[i].doc.D_ID;
                    district.D_W_ID = oMyclass.rows[i].doc.D_W_ID;
                    district.D_NAME = oMyclass.rows[i].doc.D_NAME;
                    district.D_STREET_1 = oMyclass.rows[i].doc.D_STREET_1 + ", " + oMyclass.rows[i].doc.D_STREET_2;
                    district.D_STREET_2 = oMyclass.rows[i].doc.D_STREET_2;
                    district.D_CITY = oMyclass.rows[i].doc.D_CITY;
                    district.D_STATE = oMyclass.rows[i].doc.D_STATE;
                    district.D_ZIP = oMyclass.rows[i].doc.D_ZIP;
                    district.D_TAX = oMyclass.rows[i].doc.D_TAX;
                    district.D_YTD = oMyclass.rows[i].doc.D_YTD;
                    district.D_NEXT_O_ID = oMyclass.rows[i].doc.D_NEXT_O_ID;
                    districtList.Add(district);
                }

            }

            return districtList;
        }

        #endregion

        #region Customer
        public static string SaveCustomer(Customer customer)
        {
            try
            {
                var handler = new HttpClientHandler { Credentials = new NetworkCredential(user, password) };

                using (var client = CreateHttpClient(handler, user, database))
                {
                    var creationResponse = Create(client, new
                    {
                        _id = customer._id,
                        internalDocType = customer.internalDocType,
                        TableName = customer.TableName,
                        C_ID = customer.C_ID,
                        C_W_ID = customer.C_W_ID,
                        C_D_ID = customer.C_D_ID,
                        C_FIRST = customer.C_FIRST,
                        C_MIDDLE = customer.C_MIDDLE,
                        C_LAST = customer.C_LAST,
                        C_STREET_1 = customer.C_STREET_1,
                        C_STREET_2 = customer.C_STREET_2,
                        C_CITY = customer.C_CITY,
                        C_STATE = customer.C_STATE,
                        C_ZIP = customer.C_ZIP,
                        C_PHONE = customer.C_PHONE,
                        C_SINCE = customer.C_SINCE,
                        C_CREDIT = customer.C_CREDIT,
                        C_CREDIT_LIM = customer.C_CREDIT_LIM,
                        C_DISCOUNT = customer.C_DISCOUNT,
                        C_BALANCE = customer.C_BALANCE,
                        C_YTD_PAYMENT = customer.C_YTD_PAYMENT,
                        C_PAYMENT_CNT = customer.C_PAYMENT_CNT,
                        C_DELIVERY_CNT = customer.C_DELIVERY_CNT,
                        C_DATA = customer.C_DATA
                    });

                    if (creationResponse.StatusCode.ToString().ToUpper().Equals("CREATED"))
                    {
                        History hist = new History();
                        hist._id = "HISTORY_" + Guid.NewGuid().ToString();
                        hist.H_C_ID = customer.C_ID;
                        hist.H_C_D_ID = customer.C_D_ID;
                        hist.H_C_W_ID = customer.C_W_ID;
                        hist.H_D_ID = customer.C_D_ID;
                        hist.H_W_ID = customer.C_W_ID;
                        hist.H_DATE = DateTime.Now;
                        hist.OperationalTable = customer.TableName;
                        hist.H_AMOUNT = customer.C_YTD_PAYMENT;
                        hist.H_DATA = "Insert into " + customer.TableName + " with Balance: " + customer.C_BALANCE.ToString() + " and credit info: " + customer.C_CREDIT;

                        string responses = SaveHistory(hist, handler, client);
                    }

                    return creationResponse.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public static string UpdateCustomer(Customer customer)
        {
            try
            {
                var handler = new HttpClientHandler { Credentials = new NetworkCredential(user, password) };

                using (var client = CreateHttpClient(handler, user, database))
                {
                    var updateResponse = Update(client, customer._id, new
                    {
                        _id = customer._id,
                        internalDocType = customer.internalDocType,
                        TableName = customer.TableName,
                        C_ID = customer.C_ID,
                        C_W_ID = customer.C_W_ID,
                        C_D_ID = customer.C_D_ID,
                        C_FIRST = customer.C_FIRST,
                        C_MIDDLE = customer.C_MIDDLE,
                        C_LAST = customer.C_LAST,
                        C_STREET_1 = customer.C_STREET_1,
                        C_STREET_2 = customer.C_STREET_2,
                        C_CITY = customer.C_CITY,
                        C_STATE = customer.C_STATE,
                        C_ZIP = customer.C_ZIP,
                        C_PHONE = customer.C_PHONE,
                        C_SINCE = customer.C_SINCE,
                        C_CREDIT = customer.C_CREDIT,
                        C_CREDIT_LIM = customer.C_CREDIT_LIM,
                        C_DISCOUNT = customer.C_DISCOUNT,
                        C_BALANCE = customer.C_BALANCE,
                        C_YTD_PAYMENT = customer.C_YTD_PAYMENT,
                        C_PAYMENT_CNT = customer.C_PAYMENT_CNT,
                        C_DELIVERY_CNT = customer.C_DELIVERY_CNT,
                        C_DATA = customer.C_DATA,
                        _rev = customer._rev
                    });

                    if (updateResponse.StatusCode.ToString().ToUpper().Equals("CREATED"))
                    {
                        History hist = new History();
                        hist._id = "HISTORY_" + Guid.NewGuid().ToString();
                        hist.H_C_ID = customer.C_ID;
                        hist.H_C_D_ID = customer.C_D_ID;
                        hist.H_C_W_ID = customer.C_W_ID;
                        hist.H_D_ID = customer.C_D_ID;
                        hist.H_W_ID = customer.C_W_ID;
                        hist.H_DATE = DateTime.Now;
                        hist.OperationalTable = customer.TableName;
                        hist.H_AMOUNT = customer.C_YTD_PAYMENT;
                        hist.H_DATA = "Updated into " + customer.TableName + " with Balance: " + customer.C_BALANCE.ToString() + " and credit info: " + customer.C_CREDIT + " using revisionId: " + customer._rev;

                        string responses = SaveHistory(hist, handler, client);
                    }

                    return updateResponse.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public static Customer GetCustomerInfo(string customerId)
        {
            string stringData = GetSingleDocument(customerId);
            Customer customer = JsonConvert.DeserializeObject<Customer>(stringData);
            Warehouse ware = GetWarehouseInfo(customer.C_W_ID);
            District district = GetDistrictInfo(customer.C_D_ID);
            customer.C_W_NAME = ware.W_NAME;
            customer.C_D_NAME = district.D_NAME;
            return customer;
        }
        public static List<CustomerCredit> GetCustomerCreditInfo()
        {

            List<CustomerCredit> districtList = new List<CustomerCredit>();
            for (int i = 0; i < 2; i++)
            {
                CustomerCredit district = new CustomerCredit();
                if (i == 0)
                {
                    district.id = "GC";
                    district.name = "Good";
                    districtList.Add(district);
                }
                else if (i == 1)
                {
                    district.id = "BC";
                    district.name = "Bad";
                    districtList.Add(district);
                }
            }

            return districtList;
        }
        public static List<Customer> GetCustomerList()
        {
            string stringData = GetAllDocuments("/_design/designcustomer/_view/viewcustomer?include_docs=true");
            CustomerDocument oMyclass = JsonConvert.DeserializeObject<CustomerDocument>(stringData);
            List<Customer> customerList = new List<Customer>();

            if (!string.IsNullOrEmpty(stringData))
            {
                if (oMyclass.total_rows > 0)
                {
                    for (int i = 0; i < oMyclass.total_rows; i++)
                    {

                        Warehouse ware = JsonConvert.DeserializeObject<Warehouse>(CloudantService.GetSingleDocument(oMyclass.rows[i].doc.C_W_ID));
                        District dist = JsonConvert.DeserializeObject<District>(CloudantService.GetSingleDocument(oMyclass.rows[i].doc.C_D_ID));

                        Customer customer = new Customer();
                        customer._id = oMyclass.rows[i].doc._id;
                        customer._rev = oMyclass.rows[i].doc._rev;
                        customer.internalDocType = oMyclass.rows[i].doc.internalDocType;
                        customer.TableName = oMyclass.rows[i].doc.TableName;
                        customer.C_ID = oMyclass.rows[i].doc.C_ID;
                        customer.C_W_ID = oMyclass.rows[i].doc.C_W_ID;
                        customer.C_W_NAME = ware.W_NAME;
                        customer.C_D_ID = oMyclass.rows[i].doc.C_D_ID;
                        customer.C_D_NAME = dist.D_NAME;
                        customer.C_FIRST = oMyclass.rows[i].doc.C_FIRST + " " + oMyclass.rows[i].doc.C_MIDDLE + " " + oMyclass.rows[i].doc.C_LAST;
                        customer.C_MIDDLE = oMyclass.rows[i].doc.C_MIDDLE;
                        customer.C_LAST = oMyclass.rows[i].doc.C_LAST;
                        customer.C_STREET_1 = oMyclass.rows[i].doc.C_STREET_1 + ", " + oMyclass.rows[i].doc.C_STREET_2 + ", " + oMyclass.rows[i].doc.C_CITY + ", " + oMyclass.rows[i].doc.C_STATE + ", " + oMyclass.rows[i].doc.C_ZIP;
                        customer.C_STREET_2 = oMyclass.rows[i].doc.C_STREET_2;
                        customer.C_CITY = oMyclass.rows[i].doc.C_CITY;
                        customer.C_STATE = oMyclass.rows[i].doc.C_STATE;
                        customer.C_ZIP = oMyclass.rows[i].doc.C_ZIP;
                        customer.C_PHONE = oMyclass.rows[i].doc.C_PHONE;
                        customer.C_YTD_PAYMENT = oMyclass.rows[i].doc.C_YTD_PAYMENT;
                        customer.C_SINCE = oMyclass.rows[i].doc.C_SINCE;
                        customer.C_CREDIT = oMyclass.rows[i].doc.C_CREDIT;
                        customer.C_CREDIT_LIM = oMyclass.rows[i].doc.C_CREDIT_LIM;
                        customer.C_DISCOUNT = oMyclass.rows[i].doc.C_DISCOUNT;
                        customer.C_BALANCE = oMyclass.rows[i].doc.C_BALANCE;
                        customer.C_PAYMENT_CNT = oMyclass.rows[i].doc.C_PAYMENT_CNT;
                        customer.C_DELIVERY_CNT = oMyclass.rows[i].doc.C_DELIVERY_CNT;
                        customer.C_DATA = oMyclass.rows[i].doc.C_DATA;
                        customerList.Add(customer);

                    }
                }
            }

            return customerList;
        }

        #endregion

        #region Item
        public static List<Item> GetItemList()
        {
            string stringData = GetAllDocuments("/_design/designitem/_view/viewitem?include_docs=true");

            ItemDocument oMyclass = JsonConvert.DeserializeObject<ItemDocument>(stringData);
            List<Item> itemList = new List<Item>();

            if (!string.IsNullOrEmpty(stringData))
            {
                if (oMyclass.total_rows > 0)
                {

                    for (int i = 0; i < oMyclass.total_rows; i++)
                    {

                        Item item = new Item();
                        item._id = oMyclass.rows[i].doc._id;
                        item._rev = oMyclass.rows[i].doc._rev;
                        item.internalDocType = oMyclass.rows[i].doc.internalDocType;
                        item.TableName = oMyclass.rows[i].doc.TableName;
                        item.I_ID = oMyclass.rows[i].doc.I_ID;
                        item.I_NAME = oMyclass.rows[i].doc.I_NAME;
                        item.I_IM_ID = oMyclass.rows[i].doc.I_IM_ID;
                        item.I_PRICE = oMyclass.rows[i].doc.I_PRICE;
                        item.I_DATA = oMyclass.rows[i].doc.I_DATA;

                        itemList.Add(item);

                    }

                }

            }

            return itemList;
        }
        public static string SaveItem(Item item)
        {
            try
            {
                var handler = new HttpClientHandler { Credentials = new NetworkCredential(user, password) };

                using (var client = CreateHttpClient(handler, user, database))
                {
                    var creationResponse = Create(client, new
                    {
                        _id = item._id,
                        internalDocType = item.internalDocType,
                        TableName = item.TableName,
                        I_ID = item.I_ID,
                        I_IM_ID = item.I_IM_ID,
                        I_NAME = item.I_NAME,
                        I_PRICE = item.I_PRICE,
                        I_DATA = item.I_DATA
                    });
                    return creationResponse.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public static string UpdateItem(Item item)
        {
            try
            {
                var handler = new HttpClientHandler { Credentials = new NetworkCredential(user, password) };

                using (var client = CreateHttpClient(handler, user, database))
                {
                    var updateResponse = Update(client, item._id, new
                    {
                        _id = item._id,
                        internalDocType = item.internalDocType,
                        TableName = item.TableName,
                        I_ID = item.I_ID,
                        I_IM_ID = item.I_IM_ID,
                        I_NAME = item.I_NAME,
                        I_PRICE = item.I_PRICE,
                        I_DATA = item.I_DATA,
                        _rev = item._rev
                    });
                    return updateResponse.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public static Item GetItemInfo(string itemId)
        {
            string stringData = GetSingleDocument(itemId);
            Item item = JsonConvert.DeserializeObject<Item>(stringData);
            return item;
        }
        public static string DeleteItem(string itemId)
        {
            var handler = new HttpClientHandler { Credentials = new NetworkCredential(user, password) };
            using (var client = CreateHttpClient(handler, user, database))
            {
                Item item = JsonConvert.DeserializeObject<Item>(GetSingleDocument(itemId));
                var stringData = Delete(client, item._id, item._rev);
                return stringData.StatusCode.ToString();
            }
        }
        public static string DeleteOrder(string orderId)
        {
            var handler = new HttpClientHandler { Credentials = new NetworkCredential(user, password) };
            using (var client = CreateHttpClient(handler, user, database))
            {
                Order item = JsonConvert.DeserializeObject<Order>(GetSingleDocument(orderId));
                var stringData = Delete(client, item._id, item._rev);
                return stringData.StatusCode.ToString();
            }
        }

        public static string DeleteNewOrder(string newOrderId)
        {
            var handler = new HttpClientHandler { Credentials = new NetworkCredential(user, password) };
            using (var client = CreateHttpClient(handler, user, database))
            {
                NewOrder item = JsonConvert.DeserializeObject<NewOrder>(GetSingleDocument(newOrderId));
                var stringData = Delete(client, item._id, item._rev);
                return stringData.StatusCode.ToString();
            }
        }

        public static string DeleteOrderLine(string orderLineId)
        {
            var handler = new HttpClientHandler { Credentials = new NetworkCredential(user, password) };
            using (var client = CreateHttpClient(handler, user, database))
            {
                OrderLine item = JsonConvert.DeserializeObject<OrderLine>(GetSingleDocument(orderLineId));
                var stringData = Delete(client, item._id, item._rev);
                return stringData.StatusCode.ToString();
            }
        }

        #endregion

        #region Stock
        public static Stock GetStockInfo(string stockId)
        {
            string stringData = GetSingleDocument(stockId);
            Stock stock = JsonConvert.DeserializeObject<Stock>(stringData);
            Warehouse ware = GetWarehouseInfo(stock.S_W_ID);
            Item item = GetItemInfo(stock.S_I_ID);
            stock.S_W_NAME = ware.W_NAME;
            stock.S_I_NAME = item.I_NAME;
            return stock;
        }
        public static string SaveStock(Stock stock)
        {
            try
            {
                var handler = new HttpClientHandler { Credentials = new NetworkCredential(user, password) };

                using (var client = CreateHttpClient(handler, user, database))
                {
                    var creationResponse = Create(client, new
                    {
                        _id = stock._id,
                        internalDocType = stock.internalDocType,
                        TableName = stock.TableName,
                        S_ID = stock.S_ID,
                        S_W_ID = stock.S_W_ID,
                        S_I_ID = stock.S_I_ID,
                        S_QUANTITY = stock.S_QUANTITY,
                        S_DIST_01_ID = stock.S_DIST_01_ID,
                        S_DIST_01_VALUE = stock.S_DIST_01_VALUE,
                        S_DIST_02_ID = stock.S_DIST_02_ID,
                        S_DIST_02_VALUE = stock.S_DIST_02_VALUE,
                        S_DIST_03_ID = stock.S_DIST_03_ID,
                        S_DIST_03_VALUE = stock.S_DIST_03_VALUE,
                        S_DIST_04_ID = stock.S_DIST_04_ID,
                        S_DIST_04_VALUE = stock.S_DIST_04_VALUE,
                        S_DIST_05_ID = stock.S_DIST_05_ID,
                        S_DIST_05_VALUE = stock.S_DIST_05_VALUE,
                        S_DIST_06_ID = stock.S_DIST_06_ID,
                        S_DIST_06_VALUE = stock.S_DIST_06_VALUE,
                        S_DIST_07_ID = stock.S_DIST_07_ID,
                        S_DIST_07_VALUE = stock.S_DIST_07_VALUE,
                        S_DIST_08_ID = stock.S_DIST_08_ID,
                        S_DIST_08_VALUE = stock.S_DIST_08_VALUE,
                        S_DIST_09_ID = stock.S_DIST_09_ID,
                        S_DIST_09_VALUE = stock.S_DIST_09_VALUE,
                        S_DIST_10_ID = stock.S_DIST_10_ID,
                        S_DIST_10_VALUE = stock.S_DIST_10_VALUE,
                        S_YTD = stock.S_YTD,
                        S_ORDER_CNT = stock.S_ORDER_CNT,
                        S_REMOTE_CNT = stock.S_REMOTE_CNT,
                        S_DATA = stock.S_DATA
                    });

                    if (creationResponse.StatusCode.ToString().ToUpper().Equals("CREATED"))
                    {
                        History hist = new History();
                        hist._id = "HISTORY_" + Guid.NewGuid().ToString();
                        hist.H_W_ID = stock.S_W_ID;
                        hist.H_DATE = DateTime.Now;
                        hist.OperationalTable = stock.TableName;
                        hist.H_AMOUNT = stock.S_YTD;
                        hist.H_DATA = "Insert into " + stock.TableName + " with Quantity: " + stock.S_QUANTITY.ToString();

                        string responses = SaveHistory(hist, handler, client);
                    }

                    return creationResponse.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public static string UpdateStock(Stock stock)
        {
            try
            {
                var handler = new HttpClientHandler { Credentials = new NetworkCredential(user, password) };

                using (var client = CreateHttpClient(handler, user, database))
                {
                    var updateResponse = Update(client, stock._id, new
                    {
                        _id = stock._id,
                        internalDocType = stock.internalDocType,
                        TableName = stock.TableName,
                        S_ID = stock.S_ID,
                        S_W_ID = stock.S_W_ID,
                        S_I_ID = stock.S_I_ID,
                        S_QUANTITY = stock.S_QUANTITY,
                        S_DIST_01_ID = stock.S_DIST_01_ID,
                        S_DIST_01_VALUE = stock.S_DIST_01_VALUE,
                        S_DIST_02_ID = stock.S_DIST_02_ID,
                        S_DIST_02_VALUE = stock.S_DIST_02_VALUE,
                        S_DIST_03_ID = stock.S_DIST_03_ID,
                        S_DIST_03_VALUE = stock.S_DIST_03_VALUE,
                        S_DIST_04_ID = stock.S_DIST_04_ID,
                        S_DIST_04_VALUE = stock.S_DIST_04_VALUE,
                        S_DIST_05_ID = stock.S_DIST_05_ID,
                        S_DIST_05_VALUE = stock.S_DIST_05_VALUE,
                        S_DIST_06_ID = stock.S_DIST_06_ID,
                        S_DIST_06_VALUE = stock.S_DIST_06_VALUE,
                        S_DIST_07_ID = stock.S_DIST_07_ID,
                        S_DIST_07_VALUE = stock.S_DIST_07_VALUE,
                        S_DIST_08_ID = stock.S_DIST_08_ID,
                        S_DIST_08_VALUE = stock.S_DIST_08_VALUE,
                        S_DIST_09_ID = stock.S_DIST_09_ID,
                        S_DIST_09_VALUE = stock.S_DIST_09_VALUE,
                        S_DIST_10_ID = stock.S_DIST_10_ID,
                        S_DIST_10_VALUE = stock.S_DIST_10_VALUE,
                        S_YTD = stock.S_YTD,
                        S_ORDER_CNT = stock.S_ORDER_CNT,
                        S_REMOTE_CNT = stock.S_REMOTE_CNT,
                        S_DATA = stock.S_DATA,
                        _rev = stock._rev
                    });

                    if (updateResponse.StatusCode.ToString().ToUpper().Equals("CREATED"))
                    {
                        History hist = new History();
                        hist._id = "HISTORY_" + Guid.NewGuid().ToString();
                        hist.H_W_ID = stock.S_W_ID;
                        hist.H_DATE = DateTime.Now;
                        hist.OperationalTable = stock.TableName;
                        hist.H_AMOUNT = stock.S_YTD;
                        hist.H_DATA = "Updated into " + stock.TableName + " with Quantity: " + stock.S_QUANTITY.ToString() + " using revisionId: " + stock._rev;

                        string responses = SaveHistory(hist, handler, client);
                    }

                    return updateResponse.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public static List<Stock> GetStockList()
        {
            string stringData = GetAllDocuments("/_design/designstock/_view/viewstock?include_docs=true");
            StockDocument oMyclass = JsonConvert.DeserializeObject<StockDocument>(stringData);
            List<Stock> stockList = new List<Stock>();

            if (!string.IsNullOrEmpty(stringData))
            {
                if (oMyclass.total_rows > 0)
                {
                    for (int i = 0; i < oMyclass.total_rows; i++)
                    {

                        Warehouse ware = JsonConvert.DeserializeObject<Warehouse>(GetSingleDocument(oMyclass.rows[i].doc.S_W_ID));
                        Item item = JsonConvert.DeserializeObject<Item>(GetSingleDocument(oMyclass.rows[i].doc.S_I_ID));
                        Stock stock = new Stock();
                        stock._id = oMyclass.rows[i].doc._id;
                        stock._rev = oMyclass.rows[i].doc._rev;
                        stock.internalDocType = oMyclass.rows[i].doc.internalDocType;
                        stock.TableName = oMyclass.rows[i].doc.TableName;
                        stock.S_ID = oMyclass.rows[i].doc.S_ID;
                        stock.S_W_ID = oMyclass.rows[i].doc.S_W_ID;
                        stock.S_W_NAME = ware.W_NAME;
                        stock.S_I_ID = oMyclass.rows[i].doc.S_I_ID;
                        stock.S_I_NAME = item.I_NAME;
                        stock.S_QUANTITY = oMyclass.rows[i].doc.S_QUANTITY;
                        stock.S_DIST_01_ID = oMyclass.rows[i].doc.S_DIST_01_ID;
                        stock.S_DIST_01_VALUE = oMyclass.rows[i].doc.S_DIST_01_VALUE;
                        stock.S_DIST_02_ID = oMyclass.rows[i].doc.S_DIST_02_ID;
                        stock.S_DIST_02_VALUE = oMyclass.rows[i].doc.S_DIST_02_VALUE;
                        stock.S_DIST_03_ID = oMyclass.rows[i].doc.S_DIST_03_ID;
                        stock.S_DIST_03_VALUE = oMyclass.rows[i].doc.S_DIST_03_VALUE;
                        stock.S_DIST_04_ID = oMyclass.rows[i].doc.S_DIST_04_ID;
                        stock.S_DIST_04_VALUE = oMyclass.rows[i].doc.S_DIST_04_VALUE;
                        stock.S_DIST_05_ID = oMyclass.rows[i].doc.S_DIST_05_ID;
                        stock.S_DIST_05_VALUE = oMyclass.rows[i].doc.S_DIST_05_VALUE;
                        stock.S_DIST_06_ID = oMyclass.rows[i].doc.S_DIST_06_ID;
                        stock.S_DIST_06_VALUE = oMyclass.rows[i].doc.S_DIST_06_VALUE;
                        stock.S_DIST_07_ID = oMyclass.rows[i].doc.S_DIST_07_ID;
                        stock.S_DIST_07_VALUE = oMyclass.rows[i].doc.S_DIST_07_VALUE;
                        stock.S_DIST_08_ID = oMyclass.rows[i].doc.S_DIST_08_ID;
                        stock.S_DIST_08_VALUE = oMyclass.rows[i].doc.S_DIST_08_VALUE;
                        stock.S_DIST_09_ID = oMyclass.rows[i].doc.S_DIST_09_ID;
                        stock.S_DIST_09_VALUE = oMyclass.rows[i].doc.S_DIST_09_VALUE;
                        stock.S_DIST_10_ID = oMyclass.rows[i].doc.S_DIST_10_ID;
                        stock.S_DIST_10_VALUE = oMyclass.rows[i].doc.S_DIST_10_VALUE;
                        stock.S_YTD = oMyclass.rows[i].doc.S_YTD;
                        stock.S_ORDER_CNT = oMyclass.rows[i].doc.S_ORDER_CNT;
                        stock.S_REMOTE_CNT = oMyclass.rows[i].doc.S_REMOTE_CNT;
                        stock.S_DATA = oMyclass.rows[i].doc.S_DATA;
                        stockList.Add(stock);

                    }
                }
            }

            return stockList;
        }
        public static Stock GetStocInfoBySupplierWarehouseAndItem(string sItemId, string sSupplierWarehouseId)
        {
            string stringData = GetAllDocuments("/_design/designstockbyItembupplierwarehouse/_view/viewstockbyItemsupplierwarehouse?key=[%22" + sItemId + "%22,%22" + sSupplierWarehouseId + "%22]&include_docs=true");
            ViewStockByItemSupplierWarehouseDocument oMyclass = JsonConvert.DeserializeObject<ViewStockByItemSupplierWarehouseDocument>(stringData);

            Stock stock = new Stock();

            if (oMyclass.total_rows > 0)
            {
                string aa = oMyclass.rows[0].doc._id;
                int i = 0;
                stock._id = oMyclass.rows[i].doc._id;
                stock._rev = oMyclass.rows[i].doc._rev;
                stock.internalDocType = oMyclass.rows[i].doc.internalDocType;
                stock.TableName = oMyclass.rows[i].doc.TableName;
                stock.S_ID = oMyclass.rows[i].doc.S_ID;
                stock.S_W_ID = oMyclass.rows[i].doc.S_W_ID;
                stock.S_I_ID = oMyclass.rows[i].doc.S_I_ID;
                stock.S_QUANTITY = oMyclass.rows[i].doc.S_QUANTITY;
                stock.S_DIST_01_ID = oMyclass.rows[i].doc.S_DIST_01_ID;
                stock.S_DIST_01_VALUE = oMyclass.rows[i].doc.S_DIST_01_VALUE;
                stock.S_DIST_02_ID = oMyclass.rows[i].doc.S_DIST_02_ID;
                stock.S_DIST_02_VALUE = oMyclass.rows[i].doc.S_DIST_02_VALUE;
                stock.S_DIST_03_ID = oMyclass.rows[i].doc.S_DIST_03_ID;
                stock.S_DIST_03_VALUE = oMyclass.rows[i].doc.S_DIST_03_VALUE;
                stock.S_DIST_04_ID = oMyclass.rows[i].doc.S_DIST_04_ID;
                stock.S_DIST_04_VALUE = oMyclass.rows[i].doc.S_DIST_04_VALUE;
                stock.S_DIST_05_ID = oMyclass.rows[i].doc.S_DIST_05_ID;
                stock.S_DIST_05_VALUE = oMyclass.rows[i].doc.S_DIST_05_VALUE;
                stock.S_DIST_06_ID = oMyclass.rows[i].doc.S_DIST_06_ID;
                stock.S_DIST_06_VALUE = oMyclass.rows[i].doc.S_DIST_06_VALUE;
                stock.S_DIST_07_ID = oMyclass.rows[i].doc.S_DIST_07_ID;
                stock.S_DIST_07_VALUE = oMyclass.rows[i].doc.S_DIST_07_VALUE;
                stock.S_DIST_08_ID = oMyclass.rows[i].doc.S_DIST_08_ID;
                stock.S_DIST_08_VALUE = oMyclass.rows[i].doc.S_DIST_08_VALUE;
                stock.S_DIST_09_ID = oMyclass.rows[i].doc.S_DIST_09_ID;
                stock.S_DIST_09_VALUE = oMyclass.rows[i].doc.S_DIST_09_VALUE;
                stock.S_DIST_10_ID = oMyclass.rows[i].doc.S_DIST_10_ID;
                stock.S_DIST_10_VALUE = oMyclass.rows[i].doc.S_DIST_10_VALUE;
                stock.S_YTD = oMyclass.rows[i].doc.S_YTD;
                stock.S_ORDER_CNT = oMyclass.rows[i].doc.S_ORDER_CNT;
                stock.S_REMOTE_CNT = oMyclass.rows[i].doc.S_REMOTE_CNT;
                stock.S_DATA = oMyclass.rows[i].doc.S_DATA;

            }

            return stock;
        }

        #endregion

        #region History
        public static List<History> GetHistoryList()
        {
            string stringData = GetAllDocuments("");

            HistoryDocument oMyclass = JsonConvert.DeserializeObject<HistoryDocument>(stringData);
            List<History> historyList = new List<History>();

            if (!string.IsNullOrEmpty(stringData))
            {
                if (oMyclass.total_rows > 0)
                {

                    for (int i = 0; i < oMyclass.total_rows; i++)
                    {
                        if (oMyclass.rows[i].doc.TableName.ToUpper().Equals("T_HISTORY"))
                        {
                            History history = new History();
                            history._id = oMyclass.rows[i].doc._id;
                            history._rev = oMyclass.rows[i].doc._rev;
                            history.internalDocType = oMyclass.rows[i].doc.internalDocType;
                            history.TableName = oMyclass.rows[i].doc.TableName;
                            history.OperationalTable = oMyclass.rows[i].doc.OperationalTable;
                            history.H_C_ID = oMyclass.rows[i].doc.H_C_ID;
                            history.H_C_D_ID = oMyclass.rows[i].doc.H_C_D_ID;
                            history.H_C_W_ID = oMyclass.rows[i].doc.H_C_W_ID;
                            history.H_D_ID = oMyclass.rows[i].doc.H_D_ID;
                            history.H_W_ID = oMyclass.rows[i].doc.H_W_ID;
                            history.H_DATE = oMyclass.rows[i].doc.H_DATE;
                            history.H_AMOUNT = oMyclass.rows[i].doc.H_AMOUNT;
                            history.H_DATA = oMyclass.rows[i].doc.H_DATA;

                            historyList.Add(history);
                        }
                    }

                }

            }

            return historyList;
        }
        public static string SaveHistory(History history, HttpClientHandler handler, HttpClient client)
        {
            try
            {
                //var handler = new HttpClientHandler { Credentials = new NetworkCredential(user, password) };

                //using (var client = CreateHttpClient(handler, user, database))
                //{
                var creationResponse = Create(client, new
                {
                    _id = history._id,
                    internalDocType = history.internalDocType,
                    TableName = history.TableName,
                    OperationalTable = history.OperationalTable,
                    H_C_ID = history.H_C_ID,
                    H_C_D_ID = history.H_C_D_ID,
                    H_C_W_ID = history.H_C_W_ID,
                    H_D_ID = history.H_D_ID,
                    H_W_ID = history.H_W_ID,
                    H_DATE = history.H_DATE,
                    H_AMOUNT = history.H_AMOUNT,
                    H_DATA = history.H_DATA
                });

                return creationResponse.StatusCode.ToString();
                //}
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public static History GetHistoryInfo(string historyId)
        {
            string stringData = GetSingleDocument(historyId);
            History history = JsonConvert.DeserializeObject<History>(stringData);
            return history;
        }

        #endregion

        #region UserInfo
        public static string SaveUserRegistration(User history)
        {
            try
            {
                var handler = new HttpClientHandler { Credentials = new NetworkCredential(user, password) };
                using (var client = CreateHttpClient(handler, user, database))
                {
                    var creationResponse = Create(client, new
                    {
                        _id = history._id,
                        internalDocType = history.internalDocType,
                        TableName = history.TableName,
                        U_ADDRES = history.U_ADDRES,
                        U_CITY = history.U_CITY,
                        U_COUNTRY = history.U_COUNTRY,
                        U_EMAIL = history.U_EMAIL,
                        U_NAME = history.U_NAME,
                        U_PASSWORD = history.U_PASSWORD,
                        U_STATE = history.U_STATE,
                        U_USER_ID = history.U_USER_ID,
                        U_WEB = history.U_WEB,
                        U_ZIP = history.U_ZIP
                    });

                    return creationResponse.StatusCode.ToString();
                }

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public static User GetUserInfo(string userId, string pass)
        {
            string stringData = GetAllDocuments("/_design/designuser/_view/viewuser?include_docs=true");
            UserDocument oMyclass = JsonConvert.DeserializeObject<UserDocument>(stringData);
            User user = new User();

            if (!string.IsNullOrEmpty(stringData))
            {
                if (oMyclass.total_rows > 0)
                {
                    for (int i = 0; i < oMyclass.total_rows; i++)
                    {
                        if (oMyclass.rows[i].doc.TableName != null)
                        {
                            if (oMyclass.rows[i].doc.TableName.ToUpper().Equals("T_USER") && oMyclass.rows[i].doc.U_USER_ID.ToUpper().Equals(userId.ToUpper()) && oMyclass.rows[i].doc.U_PASSWORD.ToUpper().Equals(pass.ToUpper()))
                            {

                                user._id = oMyclass.rows[i].doc._id;
                                user._rev = oMyclass.rows[i].doc._rev;
                                user.internalDocType = oMyclass.rows[i].doc.internalDocType;
                                user.TableName = oMyclass.rows[i].doc.TableName;
                                user.IsSignedIn = true;
                                user.U_ADDRES = oMyclass.rows[i].doc.U_ADDRES;
                                user.U_CITY = oMyclass.rows[i].doc.U_CITY;
                                user.U_COUNTRY = oMyclass.rows[i].doc.U_COUNTRY;
                                user.U_EMAIL = oMyclass.rows[i].doc.U_EMAIL;
                                user.U_NAME = oMyclass.rows[i].doc.U_NAME;
                                user.U_PASSWORD = oMyclass.rows[i].doc.U_PASSWORD;
                                user.U_STATE = oMyclass.rows[i].doc.U_STATE;
                                user.U_USER_ID = oMyclass.rows[i].doc.U_USER_ID;
                                user.U_WEB = oMyclass.rows[i].doc.U_WEB;
                                user.U_ZIP = oMyclass.rows[i].doc.U_ZIP;
                            }
                        }
                    }
                }
            }

            return user;
        }

        #endregion

        #region Order

        public static string NewOrderTransaction(Order order)
        {
            bool updateDistrict = false;
            bool insertOrder = false;
            bool insertNewOrder = false;
            bool selectStockNotFound = false;
            bool selectItemNotFound = false;

            List<Transactions> transactionList = new List<Transactions>();
            Transactions transaction;
            List<TransactionItem> trItemList;
            TransactionItem trI;
            try
            {
                double total = 0;
                double cDiscount = 0;
                double cWTax = 0;
                double dDTax = 0;
                string cCredit = string.Empty;
                string cLastName = string.Empty;
                string orderId = string.Empty;

                ViewCustomerWarehouse cst = GetWarehouseWiseCustomerList(order.O_C_ID, order.O_W_ID, order.O_D_ID);

                if (cst == null)
                {
                    IsRollBack(transactionList);
                    return "Error";
                }
                cLastName = cst.C_LAST;
                cDiscount = cst.C_DISCOUNT;
                cCredit = cst.C_CREDIT;
                cWTax = cst.W_TAX;


                District district = GetDistrictInfo(order.O_D_ID);
                orderId = district.D_NEXT_O_ID;
                dDTax = district.D_TAX;
                district.D_NEXT_O_ID = district.D_NEXT_O_ID + "1";
                string sUpdateDistrictStatus = UpdateDistrict(district);

                transaction = new Transactions();
                transaction.operationType = "Update";
                transaction.operationOnTable = "District";
                transaction.operationOnTableId = district._id;
                transaction.operationOnTableRevId = district._rev;
                trItemList = new List<TransactionItem>();
                trI = new TransactionItem();
                trI.key = "D_NEXT_O_ID";
                trI.value = orderId;
                trItemList.Add(trI);
                transaction.TransactionItem = trItemList;

                transactionList.Add(transaction);

                if (!sUpdateDistrictStatus.ToUpper().Equals("CREATED"))
                {
                    IsRollBack(transactionList);
                    return "Error";

                }


                order._id = GetUniqueId("ORDER");
                order.O_ID = orderId;
                string sInsertOrderStatus = SaveOrder(order);

                transaction = new Transactions();
                transaction.operationType = "Insert";
                transaction.operationOnTable = "Order";
                transaction.operationOnTableId = order._id;
                transactionList.Add(transaction);

                if (!sInsertOrderStatus.ToUpper().Equals("CREATED"))
                {
                    IsRollBack(transactionList);
                    return "Error";

                }

                NewOrder newOrder = new NewOrder();
                newOrder._id = GetUniqueId("NEWORDER");
                newOrder.NO_O_ID = orderId;
                newOrder.NO_D_ID = order.O_D_ID;
                newOrder.NO_W_ID = order.O_W_ID;
                string sInsertNewOrderStatus = SaveNewOrder(newOrder);

                transaction = new Transactions();
                transaction.operationType = "Insert";
                transaction.operationOnTable = "NewOrder";
                transaction.operationOnTableId = newOrder._id;
                transactionList.Add(transaction);

                if (!sInsertNewOrderStatus.ToUpper().Equals("CREATED"))
                {
                    IsRollBack(transactionList);

                }

                for (int ol_number = 0; ol_number < order.OrderLists.Count; ol_number++)
                {
                    string ol_i_id = order.OrderLists[ol_number].OL_I_ID;
                    string ol_supplier_w_id = order.OrderLists[ol_number].OL_SUPPLY_W_ID;
                    double ol_quantity = order.OrderLists[ol_number].OL_QUANTITY;
                    double ol_amount = 0;
                    double s_quantity = 0;


                    Item item = GetItemInfo(ol_i_id);
                    if (item == null)
                    {
                        IsRollBack(transactionList);
                        return "Error";
                    }

                    Stock stock = GetStocInfoBySupplierWarehouseAndItem(order.OrderLists[ol_number].OL_I_ID, order.OrderLists[ol_number].OL_SUPPLY_W_ID);

                    if (stock == null)
                    {
                        IsRollBack(transactionList);
                        return "Error";
                    }

                    transaction = new Transactions();
                    transaction.operationType = "Update";
                    transaction.operationOnTable = "Stock";
                    transaction.operationOnTableId = stock._id;

                    trItemList = new List<TransactionItem>();

                    trI = new TransactionItem();
                    trI.key = "S_YTD";
                    trI.value = stock.S_YTD.ToString();
                    trItemList.Add(trI);


                    trI = new TransactionItem();
                    trI.key = "S_ORDER_CNT";
                    trI.value = stock.S_ORDER_CNT.ToString();
                    trItemList.Add(trI);


                    trI = new TransactionItem();
                    trI.key = "S_QUANTITY";
                    trI.value = stock.S_QUANTITY.ToString();
                    trItemList.Add(trI);

                    transaction.TransactionItem = trItemList;

                    transactionList.Add(transaction);


                    string bg = string.Empty;
                    if ((item.I_DATA.IndexOf("originial") != -1) && (stock.S_DATA.IndexOf("originial") != -1))
                    {
                        bg = "B";
                    }
                    {
                        bg = "G";
                    }

                    if (stock.S_QUANTITY > ol_quantity)
                    {
                        s_quantity = stock.S_QUANTITY - ol_quantity;
                    }
                    else
                    {
                        s_quantity = stock.S_QUANTITY - ol_quantity + 91;
                    }

                    if (!order.O_ALL_LOCAL)
                    {
                        stock.S_REMOTE_CNT = stock.S_REMOTE_CNT + 1;
                    }
                    stock.S_YTD = stock.S_YTD + ol_quantity;
                    stock.S_ORDER_CNT = stock.S_ORDER_CNT + 1;
                    stock.S_QUANTITY = s_quantity;
                    stock.S_I_ID = ol_i_id;
                    stock.S_W_ID = ol_supplier_w_id;
                    string sUpdateStockStatus = UpdateStock(stock);

                    if (!sUpdateStockStatus.ToUpper().Equals("CREATED"))
                    {
                        IsRollBack(transactionList);
                        return "Error";
                    }

                    ol_amount = ol_quantity * item.I_PRICE; //* (1 + cWTax + dDTax) * (1 - cDiscount);
                    total += ol_amount;

                    OrderLine orderline = new OrderLine();
                    orderline._id = GetUniqueId("ORDERLINE");
                    orderline.OL_O_ID = orderId;
                    orderline.OL_D_ID = order.O_D_ID;
                    orderline.OL_W_ID = order.O_D_ID;
                    orderline.OL_NUMBER = orderId;
                    orderline.OL_I_ID = ol_i_id;
                    orderline.OL_SUPPLY_W_ID = ol_supplier_w_id;
                    orderline.OL_BG = bg;
                    orderline.OL_QUANTITY = ol_quantity;
                    orderline.OL_AMOUNT = ol_amount;
                    orderline.OL_DIST_INFO = order.O_D_ID; //stock.S_DIST_01_ID;

                    string sSaveOrderLineStatus = SaveOrderLine(orderline);

                    transaction = new Transactions();
                    transaction.operationType = "Insert";
                    transaction.operationOnTable = "OrderLine";
                    transaction.operationOnTableId = orderline._id;
                    transactionList.Add(transaction);


                    if (!sSaveOrderLineStatus.ToUpper().Equals("CREATED"))
                    {
                        IsRollBack(transactionList);
                        return "Error";

                    }
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                IsRollBack(transactionList);
                return ex.Message.ToString();
            }

        }
        private static bool IsRollBack(List<Transactions> transactionsList)
        {
            bool isBack = true;

            if (transactionsList.Count > 0)
            {
                foreach (Transactions tr in transactionsList)
                {
                    if (tr.operationType.ToLower().Equals("insert"))
                    {
                        if (tr.operationOnTable.ToLower().Equals("orderline"))
                        {
                            DeleteOrderLine(tr.operationOnTableId);
                        }
                        else if (tr.operationOnTable.ToLower().Equals("neworder"))
                        {
                            DeleteNewOrder(tr.operationOnTableId);
                        }
                        else if (tr.operationOnTable.ToLower().Equals("order"))
                        {
                            DeleteOrder(tr.operationOnTableId);
                        }
                    }

                    else if (tr.operationType.ToLower().Equals("update"))
                    {
                        if (tr.operationOnTable.ToLower().Equals("district"))
                        {
                            District dst = GetDistrictInfo(tr.operationOnTableId);
                            dst.D_NEXT_O_ID = tr.TransactionItem[0].value;
                            UpdateDistrict(dst);
                        }
                        else if (tr.operationOnTable.ToLower().Equals("stock"))
                        {
                            Stock stk = GetStockInfo(tr.operationOnTableId);
                            foreach (TransactionItem trItem in tr.TransactionItem)
                            {
                                if (trItem.key == "S_YTD")
                                    stk.S_YTD = Convert.ToDouble(trItem.value);
                                else if (trItem.key == "S_ORDER_CNT")
                                    stk.S_ORDER_CNT = Convert.ToDouble(trItem.value);
                                else if (trItem.key == "S_QUANTITY")
                                    stk.S_QUANTITY = Convert.ToDouble(trItem.value);
                            }
                            UpdateStock(stk);
                        }

                    }
                }
            }

            return isBack;
        }
        public static string SaveNewOrder(NewOrder newOrder)
        {
            try
            {
                var handler = new HttpClientHandler { Credentials = new NetworkCredential(user, password) };

                using (var client = CreateHttpClient(handler, user, database))
                {
                    var creationResponse = Create(client, new
                    {
                        _id = newOrder._id,
                        internalDocType = newOrder.internalDocType,
                        TableName = newOrder.TableName,
                        NO_O_ID = newOrder.NO_O_ID,
                        NO_D_ID = newOrder.NO_D_ID,
                        NO_W_ID = newOrder.NO_W_ID

                    });

                    return creationResponse.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public static string SaveOrder(Order order)
        {
            try
            {
                var handler = new HttpClientHandler { Credentials = new NetworkCredential(user, password) };

                using (var client = CreateHttpClient(handler, user, database))
                {
                    var creationResponse = Create(client, new
                    {
                        _id = order._id,
                        internalDocType = order.internalDocType,
                        TableName = order.TableName,
                        O_ID = order.O_ID,
                        O_D_ID = order.O_D_ID,
                        O_W_ID = order.O_W_ID,
                        O_C_ID = order.O_C_ID,
                        O_ENTRY_D = order.O_ENTRY_D,
                        O_OL_CNT = order.O_OL_CNT,
                        O_ALL_LOCAL = order.O_ALL_LOCAL

                    });

                    return creationResponse.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public static string SaveOrderLine(OrderLine orderline)
        {
            try
            {
                var handler = new HttpClientHandler { Credentials = new NetworkCredential(user, password) };

                using (var client = CreateHttpClient(handler, user, database))
                {
                    var creationResponse = Create(client, new
                    {
                        _id = orderline._id,
                        internalDocType = orderline.internalDocType,
                        TableName = orderline.TableName,
                        OL_O_ID = orderline.OL_O_ID,
                        OL_D_ID = orderline.OL_D_ID,
                        OL_W_ID = orderline.OL_W_ID,
                        OL_NUMBER = orderline.OL_NUMBER,
                        OL_I_ID = orderline.OL_I_ID,
                        OL_SUPPLY_W_ID = orderline.OL_SUPPLY_W_ID,
                        OL_QUANTITY = orderline.OL_QUANTITY,
                        OL_AMOUNT = orderline.OL_AMOUNT,
                        OL_DIST_INFO = orderline.OL_DIST_INFO,
                        OL_BG = orderline.OL_BG

                    });

                    return creationResponse.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public static List<Order> GetOrderList()
        {
            string stringData = GetAllDocuments("/_design/designorder/_view/vieworder?include_docs=true");

            OrderDocument oMyclass = JsonConvert.DeserializeObject<OrderDocument>(stringData);
            List<Order> itemList = new List<Order>();

            if (!string.IsNullOrEmpty(stringData))
            {
                if (oMyclass.total_rows > 0)
                {

                    for (int i = 0; i < oMyclass.total_rows; i++)
                    {
                        Warehouse ware = GetWarehouseInfo(oMyclass.rows[i].doc.O_W_ID);
                        Customer cst = GetCustomerInfo(oMyclass.rows[i].doc.O_C_ID);
                        District dst = GetDistrictInfo(oMyclass.rows[i].doc.O_D_ID);

                        Order item = new Order();
                        item._id = oMyclass.rows[i].doc._id;
                        item._rev = oMyclass.rows[i].doc._rev;
                        item.internalDocType = oMyclass.rows[i].doc.internalDocType;
                        item.TableName = oMyclass.rows[i].doc.TableName;
                        item.O_ID = oMyclass.rows[i].doc.O_ID;
                        item.O_D_ID = oMyclass.rows[i].doc.O_D_ID;
                        item.O_D_NAME = dst.D_NAME;
                        item.O_W_ID = oMyclass.rows[i].doc.O_W_ID;
                        item.O_W_NAME = ware.W_NAME;
                        item.O_C_ID = oMyclass.rows[i].doc.O_C_ID;
                        item.O_C_NAME = cst.C_LAST;
                        item.O_ENTRY_D = oMyclass.rows[i].doc.O_ENTRY_D;
                        item.O_OL_CNT = oMyclass.rows[i].doc.O_OL_CNT;
                        item.O_C_ID = oMyclass.rows[i].doc.O_C_ID;
                        item.O_ALL_LOCAL = oMyclass.rows[i].doc.O_ALL_LOCAL;
                        itemList.Add(item);

                    }

                }

            }

            return itemList;
        }
        public static Order GetOrderInfo(string orderId)
        {
            string stringData = GetSingleDocument(orderId);
            Order order = JsonConvert.DeserializeObject<Order>(stringData);
            Warehouse ware = GetWarehouseInfo(order.O_W_ID);
            District district = GetDistrictInfo(order.O_D_ID);
            Customer customer = GetCustomerInfo(order.O_C_ID);
            order.O_W_NAME = ware.W_NAME;
            order.O_D_NAME = district.D_NAME;
            order.O_C_NAME = customer.C_LAST;
            order.O_C_CEDIT = customer.C_CREDIT;
            order.O_C_DISCOUNT = customer.C_DISCOUNT;
            order.O_W_TAX = ware.W_TAX;
            order.O_D_TAX = district.D_TAX;
            order.OrderLineItems = GetOrderLineList(order.O_ID);
            return order;
        }

        public static List<ViewOrderLineByOrder> GetOrderLineList(string orderId)
        {
            string stringData = GetAllDocuments("/_design/designorderlinebyorderid/_view/vieworderlinebyorderid?key=[%22" + orderId + "%22]&include_docs=true");

            ViewOrderLineByOrderDocument oMyclass = JsonConvert.DeserializeObject<ViewOrderLineByOrderDocument>(stringData);
            List<ViewOrderLineByOrder> itemList = new List<ViewOrderLineByOrder>();

            if (!string.IsNullOrEmpty(stringData))
            {
                if (oMyclass.total_rows > 0)
                {

                    for (int i = 0; i < oMyclass.total_rows; i++)
                    {
                        Warehouse ware = GetWarehouseInfo(oMyclass.rows[i].doc.OL_SUPPLY_W_ID);
                        Item itm = GetItemInfo(oMyclass.rows[i].doc.OL_I_ID);

                        ViewOrderLineByOrder item = new ViewOrderLineByOrder();
                        item.OL_I_ID = oMyclass.rows[i].doc.OL_I_ID;
                        item.OL_I_NAME = itm.I_NAME;
                        item.OL_SUPPLY_W_ID = oMyclass.rows[i].doc.OL_SUPPLY_W_ID;
                        item.OL_SUPPLY_W_NAME = ware.W_NAME;
                        item.OL_QUANTITY = oMyclass.rows[i].doc.OL_QUANTITY;
                        item.OL_BG = oMyclass.rows[i].doc.OL_BG;
                        item.OL_PRICE = itm.I_PRICE;
                        item.OL_Stock = 0;
                        item.OL_AMOUNT = oMyclass.rows[i].doc.OL_AMOUNT;
                        itemList.Add(item);

                    }

                }

            }

            return itemList;
        }

        #endregion

        #region old Dynamic Code
        public static string Save<T>(T obj) where T : class
        {
            try
            {
                var handler = new HttpClientHandler { Credentials = new NetworkCredential(user, password) };

                using (var client = CreateHttpClient(handler, user, database))
                {

                    var creationResponse = Create(client, obj);
                    return creationResponse.ToString();

                }


            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static string Save(Dictionary<string, object> openWith)
        {
            try
            {
                var handler = new HttpClientHandler { Credentials = new NetworkCredential(user, password) };
                using (var client = CreateHttpClient(handler, user, database))
                {

                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, object> kvp in openWith)
                    {

                        dictionary.Add(kvp.Key, kvp.Value);

                    }

                    var obj = ObjectFromDictionary<Warehouse>(dictionary);

                    //object someObject = dictionary.ToObject<object>();

                    var creationResponse = Create(client, obj);

                }


                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static T ToObject<T>(this IDictionary<string, object> source) where T : class, new()
        {
            T someObject = new T();
            Type someObjectType = someObject.GetType();

            foreach (KeyValuePair<string, object> item in source)
            {
                someObjectType.GetProperty(item.Key).SetValue(someObject, item.Value, null);
            }

            return someObject;
        }
        public static IDictionary<string, object> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );

        }
        private static T ObjectFromDictionary<T>(IDictionary<string, object> dict) where T : class
        {
            Type type = typeof(T);
            T result = (T)Activator.CreateInstance(type);
            try
            {
                foreach (var item in dict)
                {
                    if (IsNumeric(item.Value))
                    {
                        type.GetProperty(item.Key).SetValue(result, Convert.ToDouble(item.Value), null);
                    }
                    else
                        type.GetProperty(item.Key).SetValue(result, Convert.ToString(item.Value), null);

                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static System.Boolean IsNumeric(System.Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch { } // just dismiss errors but return false
            return false;
        }
        private static IDictionary<string, object> ObjectToDictionary<T>(T item) where T : class
        {
            Type myObjectType = item.GetType();
            IDictionary<string, object> dict = new Dictionary<string, object>();
            var indexer = new object[0];
            PropertyInfo[] properties = myObjectType.GetProperties();
            foreach (var info in properties)
            {
                var value = info.GetValue(item, indexer);
                dict.Add(info.Name, value);
            }
            return dict;
        }

        #endregion

        #region Common Service Code
        public static string GetAllDocuments(string sURLAppend)
        {
            try
            {
                var handler = new HttpClientHandler { Credentials = new NetworkCredential(user, password) };
                using (HttpClient client = CreateHttpClient(handler, user, database))
                {
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    HttpResponseMessage response = client.GetAsync(sURL + sURLAppend).Result;
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    return stringData.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public static string GetSingleDocument(string id)
        {
            try
            {
                string sRetrieveURL = "https://3e59275b-6c81-4593-b1ca-3fdc0f9024a0-bluemix:529dcbca4868e7b153ad6182f564b5cb3e83bb9c43e7543bfc13e230c04f4e16@3e59275b-6c81-4593-b1ca-3fdc0f9024a0-bluemix.cloudant.com/inventory/";
                var handler = new HttpClientHandler { Credentials = new NetworkCredential(user, password) };
                using (HttpClient client = CreateHttpClient(handler, user, database))
                {
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    HttpResponseMessage response = client.GetAsync(sRetrieveURL + id).Result;
                    var stringData = response.Content.ReadAsStringAsync().Result;
                    return stringData.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public static string GetAll(HttpClient client)
        {
            using (var streamReader = new StreamReader(client.GetStreamAsync("https://3e59275b-6c81-4593-b1ca-3fdc0f9024a0-bluemix:529dcbca4868e7b153ad6182f564b5cb3e83bb9c43e7543bfc13e230c04f4e16@3e59275b-6c81-4593-b1ca-3fdc0f9024a0-bluemix.cloudant.com").Result))
            {
                var responseContent = (JObject)JToken.ReadFrom(new JsonTextReader(streamReader));
                return responseContent.ToString();
            }


        }
        public static HttpResponseMessage Create(HttpClient client, object doc)
        {
            var json = JsonConvert.SerializeObject(doc);
            return client.PostAsync("", new StringContent(json, Encoding.UTF8, "application/json")).Result;
        }
        public static HttpResponseMessage Read(HttpClient client, string id)
        {
            return client.GetAsync(id).Result;
        }
        public static HttpResponseMessage Update(HttpClient client, string id, object doc)
        {
            var json = JsonConvert.SerializeObject(doc);
            return client.PutAsync(id, new StringContent(json, Encoding.UTF8, "application/json")).Result;
        }
        public static HttpResponseMessage Delete(HttpClient client, string id, string rev)
        {
            return client.DeleteAsync(id + "?rev=" + rev).Result;
        }
        public static HttpClient CreateHttpClient(HttpClientHandler handler, string user, string database)
        {
            return new HttpClient(handler)
            {
                BaseAddress = new Uri(string.Format("https://{0}.cloudant.com/{1}/", user, database))
            };
        }
        public static void PrintResponse(HttpResponseMessage response)
        {
            Console.WriteLine("Status code: {0}", response.StatusCode);
            Console.WriteLine(Convert.ToString(response));
        }
        public static string GetString(string propertyName, HttpResponseMessage creationResponse)
        {
            using (var streamReader = new StreamReader(creationResponse.Content.ReadAsStreamAsync().Result))
            {
                var responseContent = (JObject)JToken.ReadFrom(new JsonTextReader(streamReader));
                return responseContent[propertyName].Value<string>();
            }
        }

        #endregion
    }
}
