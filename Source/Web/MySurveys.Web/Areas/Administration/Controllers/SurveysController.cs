namespace MySurveys.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Contracts;
    using ViewModels;

    public class SurveysController : AdminController
    {
        public SurveysController(ISurveyService surveyService, IUserService userService)
            : base(surveyService, userService)
        {
        }

        //// GET: Administration/Surveys
        public ActionResult Index()
        {
            return this.View();
        }

        public JsonResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.SurveyService.GetAll()
                           .ProjectTo<SurveyViewModel>(SurveyViewModel.Configuration);

            return this.Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}