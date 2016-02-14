namespace MySurveys.Web.Areas.Surveys.Controllers
{
    using System.Web;
    using System.Web.Mvc;
    using Web.ViewModels;
    using Services.Contracts;
    using Web.Controllers.Base;
    using MvcTemplate.Web.Infrastructure.Mapping;
    using System.Linq;
    using Models;

    public class PublicController : BaseController
    {
        public PublicController(ISurveyService surveyService, IUserService userService)
            : base(surveyService, userService)
        {
        }

        //// GET: Surveys/Public/Index
        public ActionResult Index()
        {
            var surveys = this.SurveyService
                              .GetAllPublic()
                              .To<ViewModels.SurveyViewModel>();

            return this.View(surveys);
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