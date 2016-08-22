using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Dream.Admin.BLL;

namespace Dream.Admin.Controllers
{
    [ValidateAdmin]
    public class SystemManagerController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Tag = "SystemManager";
            return View();
        }

        public ActionResult SystemLog(string key, int pageIndex = 0, int pageSize = 20)
        {
            var sc = new SystemCore();
            var model = sc.SystemLog(key, pageIndex, pageSize);
            ViewBag.Tag = "SystemManager";
            ViewBag.Total = sc.Total;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;
            ViewBag.Key = key;
            return View(model);
        }

        public ActionResult SignInLog(string key, int pageIndex = 0, int pageSize = 20)
        {
            var sc = new SystemCore();
            var model = sc.SignInLog(key, pageIndex, pageSize);
            ViewBag.Tag = "SystemManager";
            ViewBag.Total = sc.Total;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;
            ViewBag.Key = key;
            return View(model);
        }

        [HttpGet]
        public ActionResult AddAdmin()
        {
            ViewBag.Tag = "SystemManager";
            return View();
        }

        [HttpPost]
        public ActionResult AddAdmin(string username, string name, string password)
        {
            return Json(new SystemCore().AddAdmin(username, name, password));
        }

        public ActionResult AdminList()
        {
            return View(new SystemCore().AdminList());
        }
    }
}