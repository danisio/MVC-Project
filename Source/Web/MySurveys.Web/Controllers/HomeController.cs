namespace MySurveys.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Areas.Surveys.ViewModels;
    using Base;
    using MvcTemplate.Web.Infrastructure.Mapping;
    using Services.Contracts;

    public class HomeController : BaseController
    {
        public HomeController(ISurveyService surveyService, IUserService userService)
            : base(surveyService, userService)
        {
        }

        public ActionResult Index()
        {
            var all = this.SurveyService.GetAllPublic().ToList();
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
                            .GetMostPopular(3)
                            .To<SurveyViewModel>()
                            .ToList(),
                            15 * 60);

            return this.PartialView("_MostPopularSurveysPartial", surveys);
        }
    }
}