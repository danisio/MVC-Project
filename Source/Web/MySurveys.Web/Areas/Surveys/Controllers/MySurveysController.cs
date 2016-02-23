namespace MySurveys.Web.Areas.Surveys.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Base;
    using Models;
    using Services.Contracts;
    using ViewModels.Creating;
    using Web.Controllers.Base;

    [Authorize]
    public class MySurveysController : BaseScrollController
    {
        private static List<QuestionViewModel> questions = new List<QuestionViewModel>();
        private IQuestionService questionService;

        public MySurveysController(IUserService userService, ISurveyService surveyService, IQuestionService questionService)
            : base(userService, surveyService)
        {
            this.questionService = questionService;
        }

        //// GET: Surveys/MySurveys/Create
        [HttpGet]
        public ActionResult Create()
        {
            return this.View(questions);
        }

        //// GET: Surveys/MySurveys/ViewForm
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
        public ActionResult SaveSurvey(ViewModels.Creating.SurveyViewModel model)
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
                this.SurveyService.Add(viewModel);

                questions.Clear();
                return this.RedirectToAction("Index", "Surveys", new { area = "Surveys" });
            }

            return this.RedirectToAction("Create");
        }

        //// GET: Surveys/MySurveys/EditSurvey
        [HttpGet]
        public ActionResult EditSurvey(int id)
        {
            var survey = this.SurveyService.GetById(id);
            if (survey != null)
            {
                var viewModel = this.Mapper.Map<ViewModels.Editing.SurveyViewModel>(survey);
                return this.PartialView("_EditSurveyPartial", viewModel);
            }

            return this.View();
        }

        //// POST: Surveys/MySurveys/EditQuestion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSurvey(ViewModels.Editing.SurveyViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = this.SurveyService.GetById(model.Id);
                var mapped = this.Mapper.Map(model, dbModel);

                this.SurveyService.Update(mapped);
            }

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteSurvey(int id)
        {
            return this.PartialView("_DeleteSurveyPartial", id);
        }


        [HttpPost]
        public ActionResult DeleteSurveyConfirmation()
        {
            int idAsInt;
            try
            {
                idAsInt = int.Parse(Request.Form["id"]);
                var survey = this.SurveyService.GetById(idAsInt);
            }
            catch (Exception)
            {
                throw new HttpException(404, "Survey not found");
            }

            this.SurveyService.Delete(idAsInt);

            return this.RedirectToAction("Index");
        }

        protected override IQueryable<Survey> GetData()
        {
            return this.SurveyService
                        .GetAll()
                        .Where(s => s.AuthorId == this.CurrentUser.Id);
        }
    }
}