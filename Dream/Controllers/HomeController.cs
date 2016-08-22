using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dream.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VideoDetails(int vid)
        {
            return View();
        }

        public ActionResult UserCenter(int userId = 0)
        {
            return View();
        }
        public ActionResult History(int userId,int cate)
        {
            return View();
        }

        public ActionResult SignIn()
        {
            return View();
        }
    }
}