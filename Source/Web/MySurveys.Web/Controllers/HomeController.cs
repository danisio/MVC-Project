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
        private ICacheService cacheService;

        public HomeController(ISurveyService surveyService, IUserService userService, ICacheService cacheService)
            : base(surveyService, userService)
        {
            this.cacheService = cacheService;
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
        //[OutputCache(Duration = 60 * 60)]
        public ActionResult MostPopularSurveys()
        {
            var surveys = this.cacheService.Get(
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