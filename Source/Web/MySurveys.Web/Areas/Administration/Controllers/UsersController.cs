namespace MySurveys.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Kendo.Mvc.UI;
    using Models;
    using MvcTemplate.Web.Infrastructure.Mapping;
    using Services.Contracts;
    using ViewModels;

    public class UsersController : AdminController
    {
        public UsersController(ISurveyService surveyService, IUserService userService)
              : base(surveyService, userService)
        {
        }

        //// GET: Administration/Users
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, UserViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var dbModel = this.UserService.GetById(model.Id);
                var mapped = this.Mapper.Map(model, dbModel);

                this.UserService.Update(mapped);
            }

            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, UserViewModel model)
        {
            base.Destroy<UserViewModel>(request, model, model.Id);

            return this.GridOperation(model, request);
        }

        protected override IEnumerable GetData()
        {
            return this.UserService
                       .GetAll()
                       .To<UserViewModel>();
        }

        protected override void Delete<T>(object id)
        {
            this.UserService.Delete(id);
        }
    }
}