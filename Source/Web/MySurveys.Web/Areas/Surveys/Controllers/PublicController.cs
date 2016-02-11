namespace MySurveys.Web.Areas.Surveys.Controllers
{
    using System.Web;
    using System.Web.Mvc;
    using Services.Contracts;
    using ViewModels;
    using Web.Controllers.Base;

    public class PublicController : BaseController
    {
        public PublicController(ISurveyService surveyService, IUserService userService)
            : base(surveyService, userService)
        {
        }

        //// GET: Surveys/Public/Index
        public ActionResult Index()
        {
            return this.View();
        }

        //// GET: Surveys/Public/Details
        public ActionResult Details(string id)
        {
            var survey = this.SurveyService.GetById(id);
            if (survey == null)
            {
                throw new HttpException(404, "Survey not found");
            }

            var viewModel = this.Mapper.Map<SurveyViewModel>(survey);

            return this.View(viewModel);
        }
    }
}