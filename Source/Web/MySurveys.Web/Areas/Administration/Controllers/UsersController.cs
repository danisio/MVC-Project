namespace MySurveys.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.UI;
    using Services.Contracts;
    using ViewModels;

    using Model = Models.User;
    using ViewModel = ViewModels.UserViewModel;

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
                this.Mapper = ViewModel.Configuration.CreateMapper();
                var mapped = Mapper.Map<ViewModel, Model>(model, dbModel);
                this.UserService.Update(mapped);
            }

            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Destroy<ViewModel>(request, model, model.Id);

            return this.GridOperation(model, request);
        }

        protected override IEnumerable GetData()
        {
            return this.UserService
                       .GetAll()
                       .ProjectTo<ViewModel>(ViewModel.Configuration);
        }

        protected override void Delete<T>(object id)
        {
            this.UserService.Delete(id);
        }
    }
}