namespace MySurveys.Web.Areas.Surveys.Controllers
{
    using System.Web.Mvc;
    using Services.Contracts;

    public class SurveysController : Controller
    {
        private ISurveyService surveys;

        public SurveysController(ISurveyService surveys)
        {
            this.surveys = surveys;
        }

        //// GET: Surveys/Surveys/All
        public ActionResult Index()
        {
            return this.View();
        }

        //// GET: Surveys/Surveys/Details
        public ActionResult Details(int id)
        {
            return this.View(this.surveys.GetById(id));
        }
    }
}