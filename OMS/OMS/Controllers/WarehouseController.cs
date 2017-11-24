using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMS.Services;
using OMS.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;

namespace OMS.Controllers
{
    public class WarehouseController : Controller
    {
        // GET: Warehouse
        public ActionResult Index()
        {       
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                {
                    ViewBag.Userid = HttpContext.Session.GetString("UserId");
                    ViewBag.Username = HttpContext.Session.GetString("UserName");
                    List<Warehouse> warehouseList = CloudantService.GetWarehouseList();
                    return View(warehouseList);
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

        // GET: Warehouse/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Warehouse ware = CloudantService.GetWarehouseInfo(id);

            if (ware == null)
            {
                return NotFound();
            }

            return View(ware);

        }

        // GET: Warehouse/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Warehouse/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                string sId = CloudantService.GetUniqueId("WAREHOUSE");
                Warehouse ware = new Warehouse();
                ware._id = sId;
                ware.internalDocType = "Warehouse";
                ware.TableName = "T_Warehouse";
                ware.W_ID = sId;
                ware.W_NAME = collection["W_NAME"].ToString();
                ware.W_STREET_1 = collection["W_STREET_1"].ToString();
                ware.W_STREET_2 = collection["W_STREET_2"].ToString();
                ware.W_CITY = collection["W_CITY"].ToString();
                ware.W_STATE = collection["W_STATE"].ToString();
                ware.W_ZIP = collection["W_ZIP"].ToString();
                ware.W_TAX = Convert.ToDouble(collection["W_TAX"]);
                ware.W_WTD = Convert.ToDouble(collection["W_WTD"]);
                string stringData = CloudantService.SaveWarehouse(ware);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Warehouse/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Warehouse ware = CloudantService.GetWarehouseInfo(id);

            if (ware == null)
            {
                return NotFound();
            }

            return View(ware);
        }

        // POST: Warehouse/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                Warehouse ware = CloudantService.GetWarehouseInfo(id);
                ware.W_NAME = collection["W_NAME"].ToString();
                ware.W_STREET_1 = collection["W_STREET_1"].ToString();
                ware.W_STREET_2 = collection["W_STREET_2"].ToString();
                ware.W_CITY = collection["W_CITY"].ToString();
                ware.W_STATE = collection["W_STATE"].ToString();
                ware.W_ZIP = collection["W_ZIP"].ToString();
                ware.W_TAX = Convert.ToDouble(collection["W_TAX"]);
                ware.W_WTD = Convert.ToDouble(collection["W_WTD"]);
                string stringData = CloudantService.UpdateWarehouse(ware);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Warehouse/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Warehouse/Delete/5
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