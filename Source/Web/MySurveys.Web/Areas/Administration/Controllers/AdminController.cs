namespace MySurveys.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Web.Mvc;
    using Common;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Contracts;
    using Web.Controllers.Base;

    [Authorize(Roles = GlobalConstants.AdminRoleName)]
    public abstract class AdminController : BaseController
    {
        public AdminController(ISurveyService surveyService, IUserService userService)
            : base(surveyService, userService)
        {
        }

        [HttpPost]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var data = this.GetData()
                           .ToDataSourceResult(request);

            return this.Json(data);
        }

        [NonAction]
        public ActionResult Destroy<TViewModel>([DataSourceRequest]DataSourceRequest request, TViewModel model, object id)
            where TViewModel : class
        {
            if (model != null && this.ModelState.IsValid)
            {
                this.Delete<TViewModel>(id);
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }

        protected abstract IEnumerable GetData();

        protected abstract void Delete<T>(object id) where T : class;

        protected JsonResult GridOperation<T>(T model, [DataSourceRequest]DataSourceRequest request)
        {
            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }
    }
}