namespace MySurveys.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Web.Mvc;
    using Kendo.Mvc.UI;
    using MvcTemplate.Web.Infrastructure.Mapping;
    using Services.Contracts;
    using ViewModels;

    public class SurveysController : AdminController
    {
        private ISurveyService surveyService;

        public SurveysController(IUserService userService, ISurveyService surveyService)
            : base(userService)
        {
            this.surveyService = surveyService;
        }

        //// GET: Administration/Surveys
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, SurveyViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var dbModel = this.surveyService.GetById(model.Id);
                var mapped = this.Mapper.Map(model, dbModel);

                this.surveyService.Update(mapped);
            }

            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, SurveyViewModel model)
        {
            base.Destroy<SurveyViewModel>(request, model, model.Id);

            return this.GridOperation(model, request);
        }

        protected override IEnumerable GetData()
        {
            return this.surveyService
                       .GetAll()
                       .To<SurveyViewModel>();
        }

        protected override void Delete<T>(object id)
        {
            this.surveyService.Delete(id);
        }
    }
}