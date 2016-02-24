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
    using ViewModels.Filling;
    [Authorize]
    public class SurveysController : BaseScrollController
    {
        private static ICollection<AnswerViewModel> currentAnswers = new List<AnswerViewModel>();
        private IQuestionService questionService;
        private IResponseService responseService;

        public SurveysController(
            ISurveyService surveyService,
            IUserService userService,
            IQuestionService questionService,
            IResponseService responseService)
            : base(userService, surveyService)
        {
            this.questionService = questionService;
            this.responseService = responseService;
        }

        //// GET: Surveys/Surveys/FillingUp
        [HttpGet]
        [AllowAnonymous]
        public ActionResult FillingUp(string id)
        {
            Survey survey;
            try
            {
                survey = this.SurveyService.GetById(id);
            }
            catch (Exception)
            {
                throw new HttpException(404, "Survey not found.");
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
                var possibleAnswerContent = form["Content"].ToString();

                var dbQuestion = this.ValidateFormAndAddAnswer(questionId, possibleAnswerContent);
                var nextQuestion = this.questionService.GetNext(dbQuestion, possibleAnswerContent);

                if (nextQuestion != null)
                {
                    if (!nextQuestion.PossibleAnswers.Any())
                    {
                        this.SaveToDb(dbQuestion.SurveyId);
                        currentAnswers.Clear();
                        this.TempData["fin"] = nextQuestion.Content;
                        return this.RedirectToActionPermanent("Index", "Home", new { area = string.Empty });
                    }

                    var viewModel = this.Mapper.Map<QuestionViewModel>(nextQuestion);
                    return this.View(viewModel);
                }
                else
                {
                    this.SaveToDb(dbQuestion.SurveyId);
                    currentAnswers.Clear();
                    this.TempData["fin"] = "Thank you!";
                }

                currentAnswers.Clear();
                return this.RedirectToActionPermanent("Index", "Home", new { area = string.Empty });
            }

            throw new HttpException(404, "Question not found");
        }

        [NonAction]
        protected override IQueryable<Survey> GetData()
        {
            return this.SurveyService
                        .GetAll();
        }

        [NonAction]
        private Question ValidateFormAndAddAnswer(int questionId, string possibleAnswerContent)
        {
            Question dbQuestion;
            try
            {
                dbQuestion = this.questionService.GetById(questionId);
            }
            catch (Exception)
            {
                throw new HttpException(404, "Question not found");
            }

            if (!dbQuestion.PossibleAnswers.Any(x => x.Content == possibleAnswerContent))
            {
                throw new HttpException(404, "Answer not found");
            }

            var possibleAnswer = dbQuestion.PossibleAnswers.FirstOrDefault(a => a.Content == possibleAnswerContent);
            var newAnswer = new AnswerViewModel()
            {
                QuestionId = dbQuestion.Id,
                PossibleAnswerId = possibleAnswer.Id
            };

            currentAnswers.Add(newAnswer);
            return dbQuestion;
        }

        [NonAction]
        private void SaveToDb(int surveyId)
        {
            var currentSurvey = this.SurveyService.GetById(surveyId);
            var newResponse = new ResponseViewModel()
            {
                AuthorId = this.CurrentUser == null ? this.AnonimousUser.Id : this.CurrentUser.Id,
                Answers = currentAnswers
            };

            var viewModel = this.Mapper.Map<Response>(newResponse);
            var saved = this.responseService.Add(viewModel);

            currentSurvey.Responses.Add(saved);
            this.SurveyService.Update(currentSurvey);
        }
    }
}