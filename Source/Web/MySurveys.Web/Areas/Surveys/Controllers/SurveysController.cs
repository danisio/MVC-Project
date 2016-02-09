namespace MySurveys.Web.Areas.Surveys.Controllers
{
    using System.Web.Mvc;
    using Web.Controllers.Base;
    using Services.Contracts;

    public class SurveysController : BaseController
    {
        public SurveysController(ISurveyService surveyService, IUserService userService)
            :base(surveyService, userService)
        {
        }

        //// GET: Surveys/Surveys/All
        public ActionResult Index()
        {
            return this.View();
        }

        //// GET: Surveys/Surveys/Details
        public ActionResult Details(int id)
        {
            return this.View(this.SurveyService.GetById(id));
        }
    }
}