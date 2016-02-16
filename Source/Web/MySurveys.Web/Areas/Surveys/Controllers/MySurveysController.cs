﻿namespace MySurveys.Web.Areas.Surveys.Controllers
{
    using System.Web.Mvc;
    using Services.Contracts;
    using Web.Controllers.Base;

    [Authorize]
    public class MySurveysController : BaseController
    {
        public MySurveysController(IUserService userService)
            : base(userService)
        {
        }

        //// GET: Surveys/MySurveys/All
        public ActionResult Index()
        {
            return this.View();
        }

        //// GET: Surveys/MySurveys/Create
        public ActionResult Create()
        {
            return this.View();
        }
    }
}