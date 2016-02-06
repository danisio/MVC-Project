namespace MySurveys.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        // GET: Administration/Home
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Users()
        {
            return this.View();
        }

        public ActionResult Surveys()
        {
            return this.View();
        }
    }
}