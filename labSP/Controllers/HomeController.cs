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
            ViewBag.answer1 = Newtonsoft.Json.JsonConvert.SerializeObject(Output.answers[0]);
            ViewBag.answer2 = Newtonsoft.Json.JsonConvert.SerializeObject(Output.answers[1]);
            ViewBag.answer3 = Newtonsoft.Json.JsonConvert.SerializeObject(Output.answers[2]);
            return View(Output);
        }

        [HttpGet]
        public ActionResult Contact(TaxiInput ti)
        {
            TaxiOutput to = new TaxiOutput();           
            if (ti.distance == 0 && ti.kolvo == 0)
            {
                to.distance = 0;
                to.kolvo = 0;
                to.time = 0;
                to.value = 0;
            }
            else
            {
                to = taxcalc(ti.distance, ti.kolvo);
            }
            return View(to);
        }

        public TaxiOutput taxcalc(double dist, double kolvo)
        {
            double time = 113737440.341 + 129754.129 + 47.605 * (kolvo - 277438.833) + 152.969 * (1 - 172449.833) + 162.7 * (dist - 456547.267);
            double value = 1995756 + 1857.878 + 0.007 * (time - 113737438.667) + 2.13 * (dist - 456547.267) + 0.041 * (kolvo - 277438.833) + 1.262 * (1 - 172449.833);
            TaxiOutput to = new TaxiOutput() { value = Math.Round(value,2), time = Math.Round(time,2), distance = dist, kolvo = kolvo };
            return to;
        }
    }
}