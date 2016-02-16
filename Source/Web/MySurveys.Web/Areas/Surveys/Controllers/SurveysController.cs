namespace MySurveys.Web.Areas.Surveys.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Attributes;
    using Base;
    using Models;
    using Services.Contracts;
    using ViewModels;

    [Authorize]
    public class SurveysController : BaseScrollController
    {
        private static ICollection<AnswerViewModel> currentAnswers =
            new List<AnswerViewModel>();

        public SurveysController(
            ISurveyService surveyService,
            IUserService userService,
            IQuestionService questionService,
            IResponseService responseService)
            : base(userService, surveyService)
        {
            this.QuestionService = questionService;
            this.ResponseService = responseService;
        }

        public IQuestionService QuestionService { get; set; }

        public IResponseService ResponseService { get; set; }

        //// GET: Surveys/Surveys/FillingUp
        [HttpGet]
        [AllowAnonymous]
        public ActionResult FillingUp(string id)
        {
            var survey = this.SurveyService.GetById(id);
            if (survey == null)
            {
                throw new HttpException(404, "Survey not found");
            }

            if (!survey.IsPublic && this.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Account", new { area = string.Empty });
            }

            var viewModel = this.Mapper.Map<SurveyViewModel>(survey);
            var firstQuestion = viewModel.Questions.ToList()[0];
            return this.View(firstQuestion);
        }

        //// POST: Surveys/Surveys/FillingUp
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [NoCache]
        public ActionResult FillingUp(FormCollection form)
        {
            if (form != null && ModelState.IsValid)
            {
                var questionId = Convert.ToInt32(form["Id"].ToString());
                var possibleAnswerId = Convert.ToInt32(form["item.Id"].ToString());

                var dbQuestion = this.QuestionService.GetById(questionId);
                if (dbQuestion == null)
                {
                    throw new HttpException(404, "Question not found");
                }

                if (!dbQuestion.PossibleAnswers.Any(x => x.Id == possibleAnswerId))
                {
                    throw new HttpException(404, "Answer not found");
                }

                var newAnswer = new AnswerViewModel()
                {
                    QuestionId = dbQuestion.Id,
                    PossibleAnswerId = possibleAnswerId
                };

                currentAnswers.Add(newAnswer);

                var nextQuestion = this.QuestionService.GetNext(dbQuestion, possibleAnswerId);

                if (nextQuestion != null)
                {
                    var viewModel = this.Mapper.Map<QuestionViewModel>(nextQuestion);
                    return this.View(viewModel);
                }
                else
                {
                    this.SaveToDb(dbQuestion.SurveyId);
                    this.TempData["fin"] = "Thank you";
                }

                return this.RedirectToActionPermanent("Index", "Home", new { area = string.Empty });
            }

            throw new HttpException(404, "Question not found");
        }

        protected override IQueryable<Survey> GetData()
        {
            return this.SurveyService
                        .GetAll();
        }

        private void SaveToDb(int surveyId)
        {
            var currentSurvey = this.SurveyService.GetById(surveyId);

            var newResponse = new ResponseViewModel()
            {
                AuthorId = this.CurrentUser == null ? this.AnonimousUser.Id : this.CurrentUser.Id,
                Answers = currentAnswers
            };

            var mapped = this.Mapper.Map<Response>(newResponse);
            var saves = this.ResponseService.Add(mapped);
            currentSurvey.Responses.Add(saves);
            this.SurveyService.Update(currentSurvey);
        }
    }
}