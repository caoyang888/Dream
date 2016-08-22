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
    public class LecturerManagerController : Controller
    {
        public ActionResult Index(string key, int pageIndex = 0, int pageSize = 20)
        {
            ViewBag.Key = key;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;
            ViewBag.Tag = "VideoManager";
            var model = new LecturerBusiness().Lecturers(key);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Tag = "VideoManager";
            return View(new LecturerBusiness().LecturerDetails(id));
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, string nickname, string profile)
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

            return Json(new LecturerBusiness().Edit(id, nickname, profile, headShowUrl));
        }
        
        [ValidateInput(false)]
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Tag = "VideoManager";
            return View();
        }

        [HttpPost]
        public ActionResult Add(string nickname, string profile)
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
            return Json(new LecturerBusiness().Add(nickname, profile, headShowUrl));
        }
    }
}