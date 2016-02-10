namespace MySurveys.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.UI;
    using Services.Contracts;

    using Model = Models.Question;
    using ViewModel = ViewModels.QuestionViewModel;

    public class QuestionsController : AdminController
    {
        public IQuestionService QuestionService { get; set; }

        public QuestionsController(IQuestionService questionService, ISurveyService surveyService, IUserService userService)
            : base(surveyService, userService)
        {
            this.QuestionService = questionService;
        }

        //// GET: Administration/Questions
        public ActionResult Index()
        {
            return this.View();
        }

        protected override IEnumerable GetData()
        {
            return this.QuestionService
                       .GetAll()
                       .ProjectTo<ViewModel>(ViewModel.Configuration);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var dbModel = this.QuestionService.GetById(model.Id);
                mapper = ViewModel.Configuration.CreateMapper();
                var mapped = mapper.Map<ViewModel, Model>(model, dbModel);
                this.QuestionService.Update(mapped);
            }

            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Destroy<ViewModel>(request, model, model.Id);

            return this.GridOperation(model, request);
        }

        protected override void Delete<T>(object id)
        {
            this.QuestionService.Delete(id);
        }
    }
}