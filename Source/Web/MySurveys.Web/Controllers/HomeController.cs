namespace MySurveys.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Base;
    using Services.Contracts;

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
    }
}