using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySurveys.Web.Controllers
{
    public class SurveysController : Controller
    {
        // GET: Surveys
        public ActionResult Index()
        {
            return View();
        }
    }
}