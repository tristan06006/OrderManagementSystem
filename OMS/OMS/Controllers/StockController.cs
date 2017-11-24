using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMS.Services;
using OMS.Models;

namespace OMS.Controllers
{
    public class StockController : Controller
    {
        // GET: Stock
        public ActionResult Index()
        {
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                {
                    ViewBag.Userid = HttpContext.Session.GetString("UserId");
                    ViewBag.Username = HttpContext.Session.GetString("UserName");
                    List<Stock> getStockList = CloudantService.GetStockList();
                    return View(getStockList);
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

        // GET: Stock/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Stock stock = CloudantService.GetStockInfo(id);
          
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // GET: Stock/Create
        public ActionResult Create()
        {
            Stock stock = new Stock();
            stock.WatehouseLists = CloudantService.GetWarehouseList();
            stock.ItemLists = CloudantService.GetItemList();
            return View(stock);
        }

        // POST: Stock/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                List<District> DistrictList = CloudantService.GetDistrictListByWarehouseId(collection["S_W_ID"].ToString());
                if (DistrictList.Count > 0)
                {
                    double qty = Convert.ToDouble(collection["S_QUANTITY"]) / 10;
                    string sId = CloudantService.GetUniqueId("STOCK");
                    Stock stock = new Stock();
                    stock._id = sId;
                    stock.internalDocType = "Stock";
                    stock.TableName = "T_Stock";
                    stock.S_ID = sId;
                    stock.S_I_ID = collection["S_I_ID"].ToString();
                    stock.S_W_ID = collection["S_W_ID"].ToString();
                    stock.S_QUANTITY = Convert.ToDouble(collection["S_QUANTITY"]);
                    stock.S_DIST_01_ID = DistrictList[0].D_ID.ToString();
                    stock.S_DIST_01_VALUE = qty;
                    stock.S_DIST_02_ID = DistrictList[1].D_ID.ToString();
                    stock.S_DIST_02_VALUE = qty;
                    stock.S_DIST_03_ID = DistrictList[2].D_ID.ToString();
                    stock.S_DIST_03_VALUE = qty;
                    stock.S_DIST_04_ID = DistrictList[3].D_ID.ToString();
                    stock.S_DIST_04_VALUE = qty;
                    stock.S_DIST_05_ID = DistrictList[4].D_ID.ToString();
                    stock.S_DIST_05_VALUE = qty;
                    stock.S_DIST_06_ID = DistrictList[5].D_ID.ToString();
                    stock.S_DIST_06_VALUE = qty;
                    stock.S_DIST_07_ID = DistrictList[6].D_ID.ToString();
                    stock.S_DIST_07_VALUE = qty;
                    stock.S_DIST_08_ID = DistrictList[7].D_ID.ToString();
                    stock.S_DIST_08_VALUE = qty;
                    stock.S_DIST_09_ID = DistrictList[8].D_ID.ToString();
                    stock.S_DIST_09_VALUE = qty;
                    stock.S_DIST_10_ID = DistrictList[9].D_ID.ToString();
                    stock.S_DIST_10_VALUE = qty;
                    stock.S_YTD = Convert.ToDouble(collection["S_YTD"]);
                    stock.S_DATA = collection["S_DATA"].ToString();

                    string stringData = CloudantService.SaveStock(stock);
                }
                return RedirectToAction("Index");


            }
            catch
            {
                return View();
            }
        }

        // GET: Stock/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Stock stock = CloudantService.GetStockInfo(id);
            stock.WatehouseLists = CloudantService.GetWarehouseList();
            stock.ItemLists = CloudantService.GetItemList();

            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // POST: Stock/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                List<District> DistrictList = CloudantService.GetDistrictListByWarehouseId(collection["S_W_ID"].ToString());
                if (DistrictList.Count > 0)
                {
                    double qty = Convert.ToDouble(collection["S_QUANTITY"]) / 10;

                    Stock stock = CloudantService.GetStockInfo(id);
                    stock.S_I_ID = collection["S_I_ID"].ToString();
                    stock.S_W_ID = collection["S_W_ID"].ToString();
                    stock.S_QUANTITY = Convert.ToDouble(collection["S_QUANTITY"]);
                    stock.S_DIST_01_ID = DistrictList[0].D_ID.ToString();
                    stock.S_DIST_01_VALUE = qty;
                    stock.S_DIST_02_ID = DistrictList[1].D_ID.ToString();
                    stock.S_DIST_02_VALUE = qty;
                    stock.S_DIST_03_ID = DistrictList[2].D_ID.ToString();
                    stock.S_DIST_03_VALUE = qty;
                    stock.S_DIST_04_ID = DistrictList[3].D_ID.ToString();
                    stock.S_DIST_04_VALUE = qty;
                    stock.S_DIST_05_ID = DistrictList[4].D_ID.ToString();
                    stock.S_DIST_05_VALUE = qty;
                    stock.S_DIST_06_ID = DistrictList[5].D_ID.ToString();
                    stock.S_DIST_06_VALUE = qty;
                    stock.S_DIST_07_ID = DistrictList[6].D_ID.ToString();
                    stock.S_DIST_07_VALUE = qty;
                    stock.S_DIST_08_ID = DistrictList[7].D_ID.ToString();
                    stock.S_DIST_08_VALUE = qty;
                    stock.S_DIST_09_ID = DistrictList[8].D_ID.ToString();
                    stock.S_DIST_09_VALUE = qty;
                    stock.S_DIST_10_ID = DistrictList[9].D_ID.ToString();
                    stock.S_DIST_10_VALUE = qty;
                    stock.S_YTD = Convert.ToDouble(collection["S_YTD"]);
                    stock.S_DATA = collection["S_DATA"].ToString();

                    string stringData = CloudantService.UpdateStock(stock);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Stock/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Stock/Delete/5
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