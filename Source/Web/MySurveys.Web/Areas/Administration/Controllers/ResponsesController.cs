namespace MySurveys.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Web.Mvc;
    using Kendo.Mvc.UI;
    using MvcTemplate.Web.Infrastructure.Mapping;
    using MySurveys.Services.Contracts;
    using MySurveys.Web.Areas.Administration.ViewModels;

    public class ResponsesController : AdminController
    {
        private IResponseService responseService;

        public ResponsesController(IUserService userService, IResponseService responseService)
            : base(userService)
        {
            this.responseService = responseService;
        }

        //// GET: Administration/Responses
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ResponseViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var dbModel = this.responseService.GetById(model.Id);
                var mapped = this.Mapper.Map(model, dbModel);

                this.responseService.Update(mapped);
            }

            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ResponseViewModel model)
        {
            base.Destroy<ResponseViewModel>(request, model, model.Id);

            return this.GridOperation(model, request);
        }

        protected override IEnumerable GetData()
        {
            return this.responseService
                       .GetAll()
                       .To<ResponseViewModel>();
        }

        protected override void Delete<T>(object id)
        {
            this.responseService.Delete(id);
        }
    }
}