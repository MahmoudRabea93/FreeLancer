using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalSchool.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult SubjectIndex()
        {
            return View();
        }
        [Authorize]
        public ActionResult DaleelIndex()
        {
            return View();
        }
        [Authorize]
        public ActionResult addSubjectIndex()
        {
            return View();
        }
        [NonAction]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [NonAction]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}