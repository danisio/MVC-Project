namespace MySurveys.Web.Areas.Surveys.Controllers
{
    using System.Web.Mvc;
    using Services.Contracts;
    using Web.Controllers.Base;

    [Authorize]
    public class SurveysController : BaseController
    {
        public SurveysController(ISurveyService surveyService, IUserService userService)
            : base(surveyService, userService)
        {
        }

        //// GET: Surveys/Surveys/All
        public ActionResult Index()
        {
            return this.View();
        }
    }
}