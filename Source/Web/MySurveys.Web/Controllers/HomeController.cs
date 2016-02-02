namespace MySurveys.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data.Repository;
    using MySurveys.Models;
    using Ninject;

    public class HomeController : Controller
    {
        [Inject]
        public IRepository<Question> Questions { get; set; }

        public ActionResult Index()
        {
            var all = Questions.All();
            return View(all);
        }
    }
}