﻿namespace MySurveys.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Areas.Surveys.ViewModels.Filling;
    using Base;
    using MvcTemplate.Web.Infrastructure.Mapping;
    using Services.Contracts;

    public class HomeController : BaseController
    {
        public HomeController(IUserService userService, ISurveyService surveyService)
            : base(userService)
        {
            this.SurveyService = surveyService;
        }

        public ISurveyService SurveyService { get; set; }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Error()
        {
            return this.View();
        }

        [ChildActionOnly]
        public ActionResult MostPopularSurveys()
        {
            var surveys = this.Cache.Get(
                "mostPopular",
                () => this.SurveyService
                            .GetMostPopular(3)
                            .To<SurveyViewModel>()
                            .ToList(),
                            15 * 60);

            return this.PartialView("_MostPopularSurveysPartial", surveys);
        }
    }
}