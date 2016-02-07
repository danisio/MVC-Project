namespace MySurveys.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using ViewModels;
    using Services.Contracts;
    using Models;

    public class SurveysController : AdminController
    {
        public SurveysController(ISurveyService surveyService, IUserService userService)
            : base(surveyService, userService)
        {
        }

        //// GET: Administration/Surveys
        public ActionResult Index()
        {
            //var conf = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<Survey, SurveyViewModel>());
            //var all = this.SurveyService.GetAll().ProjectTo<SurveyViewModel>(conf);
            return this.View();
        }

        //// Create 

        //// Update 

        //// Destroy 
    }
}