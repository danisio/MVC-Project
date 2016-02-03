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
    using AutoMapper.QueryableExtensions;
    using ViewModels;

    public class SurveysController : Controller
    {
        [Inject]
        IRepository<Survey> Surveys { get; set; }

        public ActionResult Index()
        {
            var all = this.Surveys.All().ProjectTo<SurveyViewModel>().ToList();
            return this.View(all);
        }

        public ActionResult Create()
        {
            return this.View();
        }
    }
}