namespace MySurveys.Web.Areas.Surveys.Controllers
{
    using System.Web.Mvc;
    using ViewModels;
    using Services.Contracts;
    using Web.Controllers.Base;
    using System.Web;
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
            return View();
        }

        //// GET: Surveys/Public/Details
        public ActionResult Details(int id)
        {
            var model = this.SurveyService.GetById(id);
            if (model == null)
            {
                throw new HttpException(404, "Survey not found");
            }

            this.Mapper = SurveyViewModel.Configuration.CreateMapper();
            var mapped = this.Mapper.Map<Survey,SurveyViewModel>(model);
            return this.View(mapped);
        }
    }
}