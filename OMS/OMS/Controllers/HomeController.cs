using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using OMS.Services;
using OMS.Models;
using Microsoft.AspNetCore.Hosting;

namespace OMS.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                {
                    ViewBag.Userid = HttpContext.Session.GetString("UserId");
                    ViewBag.Username = HttpContext.Session.GetString("UserName");
                    return View();
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

       

        

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public ActionResult Register()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(IFormCollection collection)
        {
            ViewData["Message"] = "Your contact page.";
            string sId = CloudantService.GetUniqueId("USER");
            User user = new User();
            user._id = sId;
            user.internalDocType = "User";
            user.TableName = "T_User";
            user.U_ADDRES = collection["U_ADDRES"].ToString();
            user.U_CITY = collection["U_CITY"].ToString();
            user.U_COUNTRY = collection["U_COUNTRY"].ToString();
            user.U_EMAIL = collection["U_EMAIL"].ToString();
            user.U_NAME = collection["U_NAME"].ToString();
            user.U_PASSWORD = SecurityHelper.Encrypt(collection["U_PASSWORD"].ToString());
            user.U_STATE = collection["U_STATE"].ToString();
            user.U_USER_ID = collection["U_USER_ID"].ToString();
            user.U_WEB = collection["U_WEB"].ToString();
            user.U_ZIP = collection["U_ZIP"].ToString();

            string stringData = CloudantService.SaveUserRegistration(user);

            if (stringData.ToUpper().Contains("CREATED"))
            {
                ViewBag.Message = user.U_NAME + " has been successfully registered!";
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Login()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(IFormCollection collection)
        {

            User user = CloudantService.GetUserInfo(collection["U_USER_ID"].ToString(), SecurityHelper.Encrypt(collection["U_PASSWORD"].ToString()));

            if (user._id != null)
            {
                HttpContext.Session.SetString("UserId", user.U_USER_ID);
                HttpContext.Session.SetString("UserName", user.U_NAME);
                HttpContext.Session.SetString("IsLoggedIn", user.IsSignedIn.ToString());
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Username or password is wrong!");
                return View();
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
