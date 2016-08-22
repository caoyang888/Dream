using Dream.Admin.BLL;
using Dream.Model.Business.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dream.Admin.Controllers
{
    public class PortalController : Controller
    {
        [HttpGet]
        public ActionResult SignIn()
        {
            AdminSignInModel model = new AdminSignInModel();
            model.Message = Request.QueryString["Message"];
            return View(model);
        }

        [HttpPost]
        public ActionResult SignIn(string username, string password)
        {
            SystemCore sc = new SystemCore();
            if (sc.SignIn(username, password))
            {
                return Redirect("/Portal/Index");
            }
            else
            {
                return RedirectToAction("SignIn", new { Message = "用户名或者密码错误！" });
            }
        }

        public ActionResult SignOut()
        {
            Session["admin"] = null;
            return Redirect("/Portal/SignIn");
        }

        public ActionResult FirstChangePassword()
        {
            return View();
        }

        [ValidateAdmin]
        public ActionResult Index()
        {
            return View();
        }
    }
}