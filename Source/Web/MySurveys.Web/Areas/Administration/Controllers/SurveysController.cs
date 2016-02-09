namespace MySurveys.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Models;
    using Services.Contracts;
    using ViewModels;

    public class SurveysController : AdminController
    {
        private IMapper mapper { get; set; }

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

        [HttpPost]
        public ActionResult Update([DataSourceRequest]
                                   DataSourceRequest request, SurveyViewModel model)
        {
            var list = new List<SurveyViewModel>();

            if (model != null && this.ModelState.IsValid)
            {
                var dbModel = this.SurveyService.GetById(model.Id);
                mapper = SurveyViewModel.Configuration.CreateMapper();
                var mapped = mapper.Map<SurveyViewModel, Survey>(model, dbModel);
                this.SurveyService.Update(mapped);
                list.Add(model);
            }

            return this.Json(list.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]
                                    DataSourceRequest request, SurveyViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                this.SurveyService.Delete(model.Id);
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }
    }
}