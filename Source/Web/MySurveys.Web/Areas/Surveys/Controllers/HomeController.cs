namespace MySurveys.Web.Areas.Surveys.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
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
    }
}