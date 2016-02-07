namespace MySurveys.Web.Controllers.Base
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Models;
    using Services.Contracts;

    [HandleError]
    public class BaseController : Controller
    {
        public BaseController(ISurveyService surveyService, IUserService userService)
        {
            this.UserService = userService;
            this.SurveyService = surveyService;
        }

        protected IUserService UserService { get; set; }

        protected ISurveyService SurveyService { get; set; }

        protected User CurrentUser { get; private set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            this.CurrentUser = this.UserService
                                       .GetAll()
                                       .Where(u => u.UserName == requestContext.HttpContext.User.Identity.Name)
                                       .FirstOrDefault();

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}