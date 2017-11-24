using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMS.Models;
using OMS.Services;

namespace OMS.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                {
                    ViewBag.Userid = HttpContext.Session.GetString("UserId");
                    ViewBag.Username = HttpContext.Session.GetString("UserName");
                    List<Customer> getCustomerList = CloudantService.GetCustomerList();
                    return View(getCustomerList);
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login");
            }            
        }

        // GET: Customer/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer customer = CloudantService.GetCustomerInfo(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            Customer district = new Customer();
            district.WatehouseLists = CloudantService.GetWarehouseList();
            district.DistrictLists = CloudantService.GetDistrictList();
            return View(district);
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                string sId = CloudantService.GetUniqueId("CUSTOMER");
                Customer customer = new Customer();
                customer._id = sId;
                customer.internalDocType = "Customer";
                customer.TableName = "T_Customer";
                customer.C_ID = sId;
                customer.C_W_ID = collection["C_W_ID"].ToString();
                customer.C_D_ID = collection["C_D_ID"].ToString();
                customer.C_FIRST = collection["C_FIRST"].ToString();
                customer.C_MIDDLE = collection["C_MIDDLE"].ToString();
                customer.C_LAST = collection["C_LAST"].ToString();
                customer.C_STREET_1 = collection["C_STREET_1"].ToString();
                customer.C_STREET_2 = collection["C_STREET_2"].ToString();
                customer.C_CITY = collection["C_CITY"].ToString();
                customer.C_STATE = collection["C_STATE"].ToString();
                customer.C_ZIP = collection["C_ZIP"].ToString();
                customer.C_PHONE = collection["C_PHONE"].ToString();
                customer.C_SINCE = Convert.ToDateTime( collection["C_SINCE"]);
                customer.C_CREDIT = collection["C_CREDIT"].ToString();
                customer.C_CREDIT_LIM = Convert.ToDouble(collection["C_CREDIT_LIM"]);
                customer.C_DISCOUNT = Convert.ToDouble(collection["C_DISCOUNT"]);
                customer.C_BALANCE = Convert.ToDouble(collection["C_BALANCE"]);
                customer.C_YTD_PAYMENT = Convert.ToDouble(collection["C_YTD_PAYMENT"]);
                customer.C_DATA = collection["C_DATA"].ToString();
                string stringData = CloudantService.SaveCustomer(customer);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Customer customer = CloudantService.GetCustomerInfo(id);
            customer.WatehouseLists = CloudantService.GetWarehouseList();
            customer.DistrictLists = CloudantService.GetDistrictList();
            customer.CustomerCreditLists = CloudantService.GetCustomerCreditInfo();

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                Customer customer = CloudantService.GetCustomerInfo(id);
                customer.C_W_ID = collection["C_W_ID"].ToString();
                customer.C_D_ID = collection["C_D_ID"].ToString();
                customer.C_FIRST = collection["C_FIRST"].ToString();
                customer.C_MIDDLE = collection["C_MIDDLE"].ToString();
                customer.C_LAST = collection["C_LAST"].ToString();
                customer.C_STREET_1 = collection["C_STREET_1"].ToString();
                customer.C_STREET_2 = collection["C_STREET_2"].ToString();
                customer.C_CITY = collection["C_CITY"].ToString();
                customer.C_STATE = collection["C_STATE"].ToString();
                customer.C_ZIP = collection["C_ZIP"].ToString();
                customer.C_PHONE = collection["C_PHONE"].ToString();
                customer.C_SINCE = Convert.ToDateTime(collection["C_SINCE"]);
                customer.C_CREDIT = collection["C_CREDIT"].ToString();
                customer.C_CREDIT_LIM = Convert.ToDouble(collection["C_CREDIT_LIM"]);
                customer.C_DISCOUNT = Convert.ToDouble(collection["C_DISCOUNT"]);
                customer.C_BALANCE = Convert.ToDouble(collection["C_BALANCE"]);
                customer.C_YTD_PAYMENT = Convert.ToDouble(collection["C_YTD_PAYMENT"]);
                customer.C_DATA = collection["C_DATA"].ToString();

                string stringData = CloudantService.UpdateCustomer(customer);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}