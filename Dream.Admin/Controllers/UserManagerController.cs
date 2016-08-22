using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Dream.Admin.BLL;
using NiceCode;

namespace Dream.Admin.Controllers
{
    [ValidateAdmin]
    public class UserManagerController : Controller
    {
        public ActionResult Index(string key, int pageIndex = 0, int pageSize = 20)
        {
            var ub = new UserBusiness();
            var model = ub.UserList(key, pageIndex: pageIndex, pageSize: pageSize);
            ViewBag.Key = key;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;
            ViewBag.Tag = "UserManager";
            ViewBag.Total = ub.Total;
            return View(model);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Tag = "UserManager";
            return View(new UserBusiness().UserDetails(id));
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, string nickname, decimal money)
        {
            string headShowUrl = string.Empty;
            if (Request.Files.Count > 0)
            {
                var result = SafeUpload.Save(Request.Files[0]);
                if (result != null)
                {
                    headShowUrl = result.AbsoluteURL;
                }
            }

            return Json(new UserBusiness().EditUser(id, nickname, headShowUrl, money));
        }
    }
}