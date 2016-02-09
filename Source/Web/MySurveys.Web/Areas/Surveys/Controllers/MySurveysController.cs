namespace MySurveys.Web.Areas.Surveys.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class MySurveysController : Controller
    {
        //// GET: Surveys/MySurveys/All
        public ActionResult Index()
        {
            return this.View();
        }

        //// GET: Surveys/MySurveys/Create
        public ActionResult Create()
        {
            return this.View();
        }
    }
}