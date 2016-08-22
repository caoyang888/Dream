using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Dream.BLL;

namespace Dream.Web.Controllers
{
    public class APIController : Controller
    {
        public ActionResult VideoList(int cate = 0)
        {
            return Json(new VideoBusiness().GetVideos(cate), JsonRequestBehavior.AllowGet);
        }

        public ActionResult VideoDetails(int vid)
        {
            return Json(new VideoBusiness().VideoDetails(vid), JsonRequestBehavior.AllowGet);
        }

    }
}