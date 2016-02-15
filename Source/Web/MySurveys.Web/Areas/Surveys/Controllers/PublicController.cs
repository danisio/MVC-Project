﻿namespace MySurveys.Web.Areas.Surveys.Controllers
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Base;
    using Models;
    using MvcTemplate.Web.Infrastructure.Mapping;
    using Services.Contracts;
    using ViewModels;
    public class PublicController : BaseScrollController
    {
        public PublicController(ISurveyService surveyService, IUserService userService)
            : base(surveyService, userService)
        {
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

        protected override IQueryable<Survey> GetData()
        {
            return this.SurveyService
                        .GetAllPublic();
        }
    }
}