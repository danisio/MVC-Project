namespace MySurveys.Web.Areas.Surveys.Controllers
{
    using System.Web.Mvc;
    using Services.Contracts;

    public class HomeController : Controller
    {
        private ISurveyService surveys;

        public HomeController(ISurveyService surveys)
        {
            this.surveys = surveys;
        }
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Create()
        {
            return this.View();
        }

        public ActionResult List()
        {
            return this.View();
        }

        public ActionResult Details(int id)
        {
            return this.View(this.surveys.GetById(id));
        }
    }
}