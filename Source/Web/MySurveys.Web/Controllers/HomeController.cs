namespace MySurveys.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data.Repository;
    using MySurveys.Models;
    using Services.Contracts;
    using Ninject;
    using ViewModels;

    public class HomeController : Controller
    {
        private ISurveyService surveys;

        public HomeController(ISurveyService surveys)
        {
            this.surveys = surveys;
        }

        public ActionResult Index()
        {
            var all = this.surveys.GetAll().ToList();
            return this.View(all);
        }

        public ActionResult Error()
        {
            return this.View();
        }
    }
}