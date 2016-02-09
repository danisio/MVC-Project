namespace MySurveys.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.Extensions;
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

        public JsonResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.UserService.GetAll()
                             .ProjectTo<UserViewModel>(UserViewModel.Configuration);

            return this.Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]
                                   DataSourceRequest request, ViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var mapper = UserViewModel.Configuration.CreateMapper();
                var dbModel = mapper.Map<UserViewModel, Model>(model);
                this.UserService.Add(dbModel);

                if (dbModel != null)
                {
                    model.Id = dbModel.Id;
                }
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }
    }
}