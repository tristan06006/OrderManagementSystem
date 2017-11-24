using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMS.Services;
using OMS.Models;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using OfficeOpenXml;
using Microsoft.AspNetCore.Hosting;

namespace OMS.Controllers
{
    public class DistrictController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public DistrictController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        // GET: District
        public ActionResult Index()
        {
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                {
                    ViewBag.Userid = HttpContext.Session.GetString("UserId");
                    ViewBag.Username = HttpContext.Session.GetString("UserName");
                    List<District> getDistrictList = CloudantService.GetDistrictList();
                    return View(getDistrictList);
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

        // GET: District/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            District district = CloudantService.GetDistrictInfo(id);
            Warehouse ware = CloudantService.GetWarehouseInfo(district.D_W_ID);
            district.D_W_NAME = ware.W_NAME;

            if (district == null)
            {
                return NotFound();
            }

            return View(district);
        }

        // GET: District/Create
        public ActionResult Create()
        {
            District district = new District();
            district.WatehouseLists = CloudantService.GetWarehouseList();
            return View(district);
        }

        // POST: District/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {

                string sId = CloudantService.GetUniqueId("DISTRICT");
                District district = new District();
                district._id = sId;
                district.internalDocType = "District";
                district.TableName = "T_District";
                district.D_ID = sId;
                district.D_W_ID = collection["D_W_ID"].ToString();
                district.D_NAME = collection["D_NAME"].ToString();
                district.D_STREET_1 = collection["D_STREET_1"].ToString();
                district.D_STREET_2 = collection["D_STREET_2"].ToString();
                district.D_CITY = collection["D_CITY"].ToString();
                district.D_STATE = collection["D_STATE"].ToString();
                district.D_ZIP = collection["D_ZIP"].ToString();
                district.D_TAX = Convert.ToDouble(collection["D_TAX"]);
                district.D_YTD = Convert.ToDouble(collection["D_YTD"]);
                district.D_NEXT_O_ID = DateTimeOffset.Now.ToString("yyyyMMddHHmmssffffff");

                string stringData = CloudantService.SaveDistrict(district);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadUsingExcell(IFormCollection collection)
        {
            FileInfo file = new FileInfo(collection["FileToUpload"]);
            string webRootPath = _hostingEnvironment.WebRootPath + @"\fileuploads";
            string contentRootPathWithFile = _hostingEnvironment.ContentRootPath + @"\" + file.Name;
            double n;

            try
            {
                if (!Directory.Exists(contentRootPathWithFile))
                {
                    file.CopyTo(contentRootPathWithFile);
                }

                if (!Directory.Exists(webRootPath + @"\" + file.Name))
                {
                    file.CopyTo(webRootPath + @"\" + file.Name);
                }

                using (ExcelPackage package = new ExcelPackage(file))
                {

                    StringBuilder sb = new StringBuilder();
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;
                    //bool bHeaderRow = true;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        string sId = CloudantService.GetUniqueId("DISTRICT");
                        District district = new District();
                        district._id = sId;
                        district.internalDocType = "District";
                        district.TableName = "T_District";
                        district.D_ID = sId;
                        district.D_W_ID = collection["D_W_ID"].ToString();
                        district.D_NAME = worksheet.Cells[row, 1].Value.ToString();
                        district.D_STREET_1 = worksheet.Cells[row, 2].Value.ToString();
                        district.D_STREET_2 = worksheet.Cells[row, 3].Value.ToString();
                        district.D_CITY = worksheet.Cells[row, 4].Value.ToString();
                        district.D_STATE = worksheet.Cells[row, 5].Value.ToString();
                        district.D_ZIP = worksheet.Cells[row, 6].Value.ToString();
                        district.D_TAX = double.TryParse(worksheet.Cells[row, 7].Value.ToString(), out n) ? Convert.ToDouble(worksheet.Cells[row, 7].Value.ToString()) : 0;
                        district.D_YTD = double.TryParse(worksheet.Cells[row, 8].Value.ToString(), out n) ? Convert.ToDouble(worksheet.Cells[row, 8].Value.ToString()) : 0;
                        district.D_NEXT_O_ID = worksheet.Cells[row, 9].Value.ToString();

                        string stringData = CloudantService.SaveDistrict(district);

                    }

                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        // GET: District/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            District district = CloudantService.GetDistrictInfo(id);
            district.WatehouseLists = CloudantService.GetWarehouseList();

            if (district == null)
            {
                return NotFound();
            }

            return View(district);
        }

        // POST: District/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                District district = CloudantService.GetDistrictInfo(id);
                district.D_W_ID = collection["D_W_ID"].ToString();
                district.D_NAME = collection["D_NAME"].ToString();
                district.D_STREET_1 = collection["D_STREET_1"].ToString();
                district.D_STREET_2 = collection["D_STREET_2"].ToString();
                district.D_CITY = collection["D_CITY"].ToString();
                district.D_STATE = collection["D_STATE"].ToString();
                district.D_ZIP = collection["D_ZIP"].ToString();
                district.D_TAX = Convert.ToDouble(collection["D_TAX"]);
                district.D_YTD = Convert.ToDouble(collection["D_YTD"]);
                district.D_NEXT_O_ID = "DISTRICT_NEXT_ORDER_" + DateTimeOffset.Now.ToString("yyyyMMddHHmmssffffff");
                string stringData = CloudantService.UpdateDistrict(district);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: District/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: District/Delete/5
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