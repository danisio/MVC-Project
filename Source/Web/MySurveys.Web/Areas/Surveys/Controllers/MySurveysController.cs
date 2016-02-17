namespace MySurveys.Web.Areas.Surveys.Controllers
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Services.Contracts;
    using ViewModels;
    using Web.Controllers.Base;
    using Models;
    [Authorize]
    public class MySurveysController : BaseController
    {
        private static List<QuestionViewModel> questions = new List<QuestionViewModel>();
        private ISurveyService surveyService;

        public MySurveysController(IUserService userService, ISurveyService surveyService)
            : base(userService)
        {
            this.surveyService = surveyService;
        }

        //// GET: Surveys/MySurveys/All
        public ActionResult Index()
        {
            return this.View(questions);
        }

        //// GET: Surveys/MySurveys/Create
        [HttpGet]
        public ActionResult Create()
        {
            return this.View(questions.OrderBy(q => q.Index).ToList());
        }

        //// POST: Surveys/MySurveys/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(dynamic model)
        //{

        //    return RedirectToAction("Create");
        //}

        public ActionResult ViewForm()
        {
            return PartialView("_AddNewQuestionPartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNew(QuestionViewModel model)
        {
            if (!model.IsDependsOn)
            {
                model.Index = (questions.Count == 0 ? 0 : questions.Where(q => q.IsDependsOn == false).Count()) + 1;
            }

            for (int i = 0; i < model.PossibleAnswers.Count; i++)
            {
                var currentAnswer = model.PossibleAnswers[i];
                if (currentAnswer.Content == null)
                {
                    model.PossibleAnswers.Remove(currentAnswer);
                }
            }

            questions.Add(model);
            //if (model.IsDependsOn==true || )
            //{
            //    return PartialView("_AddNewQuestionPartial");
            //}

            return RedirectToAction("Create");
        }

        public ActionResult DeleteQuestion(int id)
        {
            questions.RemoveAt(id);
            return RedirectToAction("Create");
        }

        [HttpGet]
        public ActionResult EditQuestion(int id)
        {
            List<SelectListItem> dropdownItems = questions
               .Select(item => new SelectListItem
               {
                   Value = item.Content,
                   Text = item.Content
               })
               .ToList();
            ViewBag.Questions = dropdownItems;
            var question = questions[id];
            return PartialView("_EditQuestionPartial", question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditQuestion(QuestionViewModel model)
        {

            return RedirectToAction("Create");
        }

        [HttpGet]
        public ActionResult SaveSurvey()
        {
            return PartialView("_SaveSurveyPartial");
        }

        [HttpPost]
        public ActionResult SaveSurvey(SurveyViewModel model)
        {
            var newSurvey = new SurveyViewModel()
            {
                IsPublic = model.IsPublic,
                Title = model.Title,
                AuthorId = this.CurrentUser.Id,
                Questions = questions
            };

            var viewModel = this.Mapper.Map<Survey>(newSurvey);

            this.surveyService.Add(viewModel);
            questions.Clear();
            return RedirectToAction("Create");
        }
    }
}