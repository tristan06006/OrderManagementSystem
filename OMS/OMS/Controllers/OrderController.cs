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
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                {
                    ViewBag.Userid = HttpContext.Session.GetString("UserId");
                    ViewBag.Username = HttpContext.Session.GetString("UserName");
                    List<Order> getOrderList = CloudantService.GetOrderList();
                    return View(getOrderList);
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

        // GET: Order/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order customer = CloudantService.GetOrderInfo(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            Order order = new Order();
            order.WatehouseLists = CloudantService.GetWarehouseList();
            order.DistrictLists = CloudantService.GetDistrictList();
            order.CustomerLists = CloudantService.GetCustomerList();
            order.ItemLists = CloudantService.GetItemList();
            order.ViewSupplierWarehouseLists = CloudantService.GetSupplierWarehouseList();
            return View(order);
        }

        [HttpPost]
        public JsonResult Save([FromBody] Order order)
        {
            //order._id= CloudantService.GetUniqueId("ORDER");
            order.internalDocType = "Order";
            order.TableName = "T_Order";
            order.O_CARRIER_ID = string.Empty;
            order.O_ENTRY_D = DateTime.UtcNow;
            order.O_OL_CNT = order.OrderLists.Count;
            string result = CloudantService.NewOrderTransaction(order);
            return Json(new { message = " Data Save successfully" });
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
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