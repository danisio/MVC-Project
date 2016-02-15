namespace MySurveys.Web.Areas.Surveys.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Models;
    using Attributes;
    using Services.Contracts;
    using ViewModels;
    using Web.Controllers.Base;
    using MvcTemplate.Web.Infrastructure.Mapping;
    using System.Collections.Generic;
    using Base;

    [Authorize]
    public class SurveysController : BaseScrollController
    {
        public SurveysController(ISurveyService surveyService, IUserService userService, IQuestionService questionService)
            : base(surveyService, userService)
        {
            this.QuestionService = questionService;
        }

        public IQuestionService QuestionService { get; set; }

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
                return this.RedirectToAction("Login", "Account", new { area = String.Empty });
            }

            var viewModel = this.Mapper.Map<SurveyViewModel>(survey);
            //viewModel.IpAddress = this.GetUserIP();
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

                var newAnswer = new Answer()
                {
                    PossibleAnswerId = possibleAnswerId,
                    ResponseId = 1
                };

                //dbQuestion.Answers.Add(newAnswer);

                var nextQuestion = this.QuestionService.GetNext(dbQuestion, possibleAnswerId);

                if (nextQuestion != null)
                {
                    var viewModel = this.Mapper.Map<QuestionViewModel>(nextQuestion);
                    return this.View(viewModel);
                }
                else
                {
                    this.QuestionService.Update(dbQuestion);
                    this.TempData["fin"] = "Thank you";
                }


                return this.RedirectToActionPermanent("Index", "Home", new { area = String.Empty });
            }

            throw new HttpException(404, "Question not found");
        }

        protected override IQueryable<Survey> GetData()
        {
            return this.SurveyService
                        .GetAll();
        }
    }
}