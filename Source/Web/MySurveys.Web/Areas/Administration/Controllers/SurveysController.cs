namespace MySurveys.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.UI;
    using Services.Contracts;

    using Model = Models.Survey;
    using ViewModel = ViewModels.SurveyViewModel;

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

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var dbModel = this.SurveyService.GetById(model.Id);
                this.Mapper = ViewModel.Configuration.CreateMapper();
                var mapped = Mapper.Map<ViewModel, Model>(model, dbModel);
                this.SurveyService.Update(mapped);
            }

            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Destroy<ViewModel>(request, model, model.Id);

            return this.GridOperation(model, request);
        }

        protected override IEnumerable GetData()
        {
            return this.SurveyService
                       .GetAll()
                       .ProjectTo<ViewModel>(ViewModel.Configuration);
        }

        protected override void Delete<T>(object id)
        {
            this.SurveyService.Delete(id);
        }
    }
}