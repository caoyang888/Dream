using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dream.Admin.Controllers
{
    public class CommonController : Controller
    {
        public ActionResult NavTop()
        {
            return View();
        }

        public ActionResult Pagination(int pageIndex, int pageSize, int total)
        {
            ViewBag.PageIndex = pageIndex;
            ViewBag.Total = total;
            ViewBag.PageSize = pageSize;
            return View();
        }
    }
}