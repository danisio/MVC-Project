namespace MySurveys.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Models;
    using Services.Contracts;
    using ViewModels;

    public class UsersController : AdminController
    {
        private IMapper mapper { get; set; }
                        
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
        public ActionResult Update([DataSourceRequest]
                                   DataSourceRequest request, UserViewModel model)
        {
            var list = new List<UserViewModel>();

            if (model != null && this.ModelState.IsValid)
            {
                var dbModel = this.UserService.GetById(model.Id);
                mapper = UserViewModel.Configuration.CreateMapper();
                var mapped = mapper.Map<UserViewModel, User>(model, dbModel);
                this.UserService.Update(mapped);
                list.Add(model);
            }

            return this.Json(list.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]
                                    DataSourceRequest request, UserViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                this.UserService.Delete(model.Id);
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }
    }
}