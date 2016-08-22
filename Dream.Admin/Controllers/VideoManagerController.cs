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
    public class VideoManagerController : Controller
    {
        public ActionResult Index(string key, int pageIndex = 0, int pageSize = 20)
        {
            var vb = new VideoBusiness();
            var model = vb.VideoList(key, pageIndex: pageIndex, pageSize: pageSize);
            ViewBag.Key = key;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;
            ViewBag.Tag = "VideoManager";
            ViewBag.Total = vb.Total;
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var vb = new VideoBusiness();
            var lb = new LecturerBusiness();
            ViewBag.Tag = "VideoManager";
            ViewBag.Categories = vb.Categories();
            ViewBag.Lecturers = lb.Lecturers();
            return View(vb.VideoDetails(id));
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, string name, string profile, string studyTarget, int lecturerId, decimal price, int categoryId)
        {
            string coverImage = string.Empty;
            if (Request.Files.Count > 0)
            {
                var result = SafeUpload.Save(Request.Files[0]);
                if (result != null)
                {
                    coverImage = result.AbsoluteURL;
                }
            }

            return Json(new VideoBusiness().EditVideo(id, name, coverImage, profile, studyTarget, lecturerId, price, categoryId));
        }

        public ActionResult Del(int id)
        {
            return Json(new VideoBusiness().DeleteVideo(id));
        }

        [ValidateInput(false)]
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Tag = "VideoManager";
            var vb = new VideoBusiness();
            var lb = new LecturerBusiness();
            ViewBag.Categories = vb.Categories();
            ViewBag.Lecturers = lb.Lecturers();
            return View();
        }

        [HttpPost]
        public ActionResult Add(string name, string profile, string studyTarget, int lecturerId, decimal price, int categoryId)
        {
            string coverImage = string.Empty;
            if (Request.Files.Count > 0)
            {
                var result = SafeUpload.Save(Request.Files[0]);
                if (result != null)
                {
                    coverImage = result.AbsoluteURL;
                }
            }
            return Json(new VideoBusiness().AddVideo(name, coverImage, profile, studyTarget, lecturerId, price, categoryId));
        }

        public ActionResult Categories()
        {
            ViewBag.Tag = "VideoManager";
            return View(new VideoBusiness().Categories());
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(string name)
        {
            return Json(new VideoBusiness().AddCategory(name));
        }

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            return View(new VideoBusiness().CategoryInfo(id));
        }

        [HttpPost]
        public ActionResult EditCategory(int id, string name)
        {
            return Json(new VideoBusiness().EditCategory(id, name));
        }
    }
}