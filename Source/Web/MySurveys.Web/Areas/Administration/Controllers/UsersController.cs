namespace MySurveys.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Services.Contracts;

    public class UsersController : AdminController
    {
        public UsersController(ISurveyService surveyService, IUserService userService)
            : base(surveyService, userService)
        {
        }

        // GET: Administration/Users
        public ActionResult Index()
        {
            return this.View();
        }

        //// Create 

        //// Update 

        //// Destroy 
    }
}