namespace MySurveys.Web.Areas.Surveys.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Models;
    using Services.Contracts;
    using ViewModels.Creating;
    using Web.Controllers.Base;

    [Authorize]
    public class MySurveysController : BaseController
    {
        private static List<QuestionViewModel> questions = new List<QuestionViewModel>();
        private ISurveyService surveyService;
        private IQuestionService questionService;

        public MySurveysController(IUserService userService, ISurveyService surveyService, IQuestionService questionService)
            : base(userService)
        {
            this.surveyService = surveyService;
            this.questionService = questionService;
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
            return this.View(questions);
        }

        //// GET: Surveys/MySurveys/AddNew
        public ActionResult ViewForm()
        {
            return this.PartialView("_AddNewQuestionPartial");
        }

        //// POST: Surveys/MySurveys/AddNew
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNew(QuestionViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                model.PossibleAnswers.RemoveAll(item => item.Content == null);
                questions.Add(model);
            }

            return this.RedirectToAction("Create");
        }

        [HttpGet]
        public ActionResult DeleteQuestion(int id)
        {
            questions.RemoveAt(id);
            return this.RedirectToAction("Create");
        }

        //// GET: Surveys/MySurveys/EditQuestion
        [HttpGet]
        public ActionResult EditQuestion(int id, string content)
        {
            List<SelectListItem> dropdownItems = questions
               .Where(item => item.Content != content && item.ParentContent == null)
               .Select(item => new SelectListItem
               {
                   Value = item.Content,
                   Text = item.Content
               })
               .ToList();

            ViewBag.Questions = dropdownItems;
            var question = questions[id];

            return this.PartialView("_EditQuestionPartial", question);
        }

        //// POST: Surveys/MySurveys/EditQuestion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditQuestion(QuestionViewModel model, FormCollection form)
        {
            if (model != null && ModelState.IsValid)
            {
                var answers = form["item.Content"].ToString().Split(',');
                if (model.IsDependsOn)
                {
                    var selected = form["Questions"].ToString().Split(',');
                    if (answers.Length != selected.Length)
                    {
                        return this.PartialView("_EditQuestionPartial", model);
                    }

                    for (int i = 0; i < answers.Length; i++)
                    {
                        var answer = questions.SelectMany(q => q.PossibleAnswers).FirstOrDefault(a => a.Content == answers[i]);
                        var question = questions.Where(q => q.Content == selected[i]).FirstOrDefault();
                        question.ParentContent = model.Content + "|" + answer.Content;
                    }
                }
                else
                {
                    var selected = form["Questions"].ToString();
                    var question = questions.FirstOrDefault(q => q.Content == selected);
                    question.ParentContent = model.Content;
                }
            }

            return this.RedirectToAction("Create");
        }

        //// GET:Surveys/MySuerveys/SaveSurvey
        [HttpGet]
        public ActionResult SaveSurvey()
        {
            return this.PartialView("_SaveSurveyPartial");
        }

        //// POST:Surveys/MySuerveys/SaveSurvey
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSurvey(SurveyViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                if (questions.Where(q => q.ParentContent == null).Count() > 1)
                {
                    this.TempData["error"] = "Please specify the next question for each answer from EDIT menu.";
                    return this.RedirectToAction("Create", model);
                }

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
                return this.RedirectToAction("Index", "Surveys", new { area = "Surveys" });
            }

            return this.RedirectToAction("Create");
        }
    }
}