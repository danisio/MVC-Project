namespace MySurveys.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;

    using AutoMapper.QueryableExtensions;
    using ViewModels;
    using Services.Contracts;
    using Models;
    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

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