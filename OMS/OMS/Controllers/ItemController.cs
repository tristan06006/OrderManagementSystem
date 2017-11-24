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
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Index()
        {
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                {
                    ViewBag.Userid = HttpContext.Session.GetString("UserId");
                    ViewBag.Username = HttpContext.Session.GetString("UserName");
                    List<Item> itemList = CloudantService.GetItemList();
                    return View(itemList);
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

        // GET: Item/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item item = CloudantService.GetItemInfo(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Item/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Item/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                string sId = CloudantService.GetUniqueId("ITEM");
                Item item = new Item();
                item._id = sId;
                item.internalDocType = "Item";
                item.TableName = "T_Item";
                item.I_ID = sId;
                item.I_IM_ID = "IMAGE_"+sId;
                item.I_NAME = collection["I_NAME"].ToString();
                item.I_DATA = collection["I_DATA"].ToString();         
                item.I_PRICE = Convert.ToDouble(collection["I_PRICE"]);

                string stringData = CloudantService.SaveItem(item);
   

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Item/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item item = CloudantService.GetItemInfo(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Item/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                Item ware = CloudantService.GetItemInfo(id);
                ware.I_NAME = collection["I_NAME"].ToString();
                ware.I_IM_ID = collection["I_IM_ID"].ToString();
                ware.I_DATA = collection["I_DATA"].ToString();
                ware.I_PRICE = Convert.ToDouble(collection["I_PRICE"]);
                string stringData = CloudantService.UpdateItem(ware);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Item/Delete/5
        public ActionResult Delete(string id)
        {
            CloudantService.DeleteItem(id);
            return RedirectToAction("Index");
        }

        // POST: Item/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {

                CloudantService.DeleteItem(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}