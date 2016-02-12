﻿namespace MySurveys.Web.Areas.Surveys.Controllers
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

    [Authorize]
    public class SurveysController : BaseController
    {
        public SurveysController(ISurveyService surveyService, IUserService userService, IQuestionService questionService)
            : base(surveyService, userService)
        {
            this.QuestionService = questionService;
        }

        public IQuestionService QuestionService { get; set; }

        //// GET: Surveys/Surveys/Index
        public ActionResult Index()
        {
            return this.View();
        }

        //// GET: Surveys/Surveys/FillingUp
        [HttpGet]
        [AllowAnonymous]
        public ActionResult FillingUp(string id)
        {
            var survey = this.SurveyService.GetById(id); // TODO Get all data to map viewModel
            if (survey == null)
            {
                throw new HttpException(404, "Survey not found");
            }

            if (!survey.IsPublic && this.CurrentUser != null)
            {
                return this.RedirectToAction("Login", "Account");
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

        private string GetUserIP()
        {
            string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
            {
                return ipList.Split(',')[0];
            }

            return Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}