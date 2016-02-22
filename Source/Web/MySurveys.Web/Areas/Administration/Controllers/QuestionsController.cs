namespace MySurveys.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Web.Mvc;
    using Kendo.Mvc.UI;
    using MvcTemplate.Web.Infrastructure.Mapping;
    using Services.Contracts;
    using ViewModels;

    public class QuestionsController : AdminController
    {
        private IQuestionService questionService;

        public QuestionsController(IUserService userService, IQuestionService questionService)
            : base(userService)
        {
            this.questionService = questionService;
        }

        //// GET: Administration/Questions
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, QuestionViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var dbModel = this.questionService.GetById(model.Id);
                var mapped = this.Mapper.Map(model, dbModel);

                this.questionService.Update(mapped);
            }

            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, QuestionViewModel model)
        {
            base.Destroy<QuestionViewModel>(request, model, model.Id);

            return this.GridOperation(model, request);
        }

        protected override IEnumerable GetData()
        {
            return this.questionService
                       .GetAll()
                       .To<QuestionViewModel>();
        }

        protected override void Delete<T>(object id)
        {
            this.questionService.Delete(id);
        }
    }
}