using labSP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace labSP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {        
            return View();
        }

        public ActionResult Answers()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Answers(MathInput m)
        {
            MathOutput Output = new MathOutput();
            Calcs cal = new Calcs();
            Output = cal.Calc(m);
            return View(Output);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}