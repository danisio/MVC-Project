namespace MySurveys.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Base;
    using Areas.Surveys.ViewModels;
    using Services.Contracts;
    using Infrastructure.Caching;

    public class HomeController : BaseController
    {
        public HomeController(ISurveyService surveyService, IUserService userService)
            : base(surveyService, userService)
        {
        }

        public ActionResult Index()
        {
            var all = this.SurveyService.GetAll().ToList();
            return this.View(all);
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
                            .GetMostPopular(5)
                            .ProjectTo<SurveyViewModel>(SurveyViewModel.Configuration)
                            .ToList(),
                30 * 60);

            return PartialView("_MostPopularSurveysPartial", surveys);
        }
    }
}