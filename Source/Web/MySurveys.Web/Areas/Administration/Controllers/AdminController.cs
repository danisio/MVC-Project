namespace MySurveys.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;
    using Common;
    using Services.Contracts;
    using Web.Controllers.Base;

    [Authorize(Roles = GlobalConstants.AdminRoleName)]
    public abstract class AdminController : BaseController
    {
        public AdminController(ISurveyService surveyService, IUserService userService)
            :base(surveyService, userService)
        {
        }
    }
}