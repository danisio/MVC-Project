namespace MySurveys.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Web.Mvc;
    using Kendo.Mvc.UI;
    using MvcTemplate.Web.Infrastructure.Mapping;
    using MySurveys.Services.Contracts;
    using ViewModels;

    public class PossibleAnswersController : AdminController
    {
        private IPossibleAnswerService possibleAnswers;

        public PossibleAnswersController(IUserService userService, IPossibleAnswerService possibleAnswers)
            : base(userService)
        {
            this.possibleAnswers = possibleAnswers;
        }

        //// GET: Administration/PossibleAnswers
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, PossibleAnswerViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var dbModel = this.possibleAnswers.GetById(model.Id);
                var mapped = this.Mapper.Map(model, dbModel);

                this.possibleAnswers.Update(mapped);
            }

            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, PossibleAnswerViewModel model)
        {
            base.Destroy<PossibleAnswerViewModel>(request, model, model.Id);

            return this.GridOperation(model, request);
        }

        protected override IEnumerable GetData()
        {
            return this.possibleAnswers
                       .GetAll()
                       .To<PossibleAnswerViewModel>();
        }

        protected override void Delete<T>(object id)
        {
            this.possibleAnswers.Delete(id);
        }
    }
}