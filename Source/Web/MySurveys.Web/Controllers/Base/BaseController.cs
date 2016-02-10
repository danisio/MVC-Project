﻿namespace MySurveys.Web.Controllers.Base
{
    using System;
    using System.Linq;
    using System.Web;
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

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            if (this.Request.IsAjaxRequest())
            {
                var exception = filterContext.Exception as HttpException;

                if (exception != null)
                {
                    this.Response.StatusCode = exception.GetHttpCode();
                    this.Response.StatusDescription = exception.Message;
                }
            }
            else
            {
                var controllerName = this.ControllerContext.RouteData.Values["Controller"].ToString();
                var actionName = this.ControllerContext.RouteData.Values["Action"].ToString();
                this.View("Error", new HandleErrorInfo(filterContext.Exception, controllerName, actionName)).ExecuteResult(this.ControllerContext);
            }

            filterContext.ExceptionHandled = true;
        }
    }
}