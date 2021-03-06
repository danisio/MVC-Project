﻿namespace MySurveys.Services
{
    using System.Linq;
    using Contracts;
    using Data.Repository;
    using Models;
    using Web.Infrastructure.IdBinder;

    public class SurveyService : ISurveyService
    {
        private IRepository<Survey> surveys;
        private IRepository<Question> questions;
        private IRepository<PossibleAnswer> possibleAnswers;

        private IIdentifierProvider identifierProvider;

        public SurveyService(IRepository<Survey> surveys, IRepository<Question> questions, IIdentifierProvider identifierProvider, IRepository<PossibleAnswer> possibleAnswers)
        {
            this.surveys = surveys;
            this.questions = questions;
            this.possibleAnswers = possibleAnswers;
            this.identifierProvider = identifierProvider;
        }

        public IQueryable<Survey> GetAll()
        {
            return this.surveys
                       .All()
                       .OrderByDescending(s => s.Responses.Count);
        }

        public IQueryable<Survey> GetAllPublic()
        {
            return this.surveys
                       .All()
                       .Where(s => s.IsPublic == true)
                       .OrderByDescending(s => s.Responses.Count);
        }

        public Survey GetById(int id)
        {
            return this.surveys.GetById(id);
        }

        public Survey GetById(string id)
        {
            var idAsInt = this.identifierProvider.DecodeId(id);
            return this.surveys.GetById(idAsInt);
        }

        public Survey Update(Survey survey)
        {
            this.surveys.Update(survey);
            this.surveys.SaveChanges();

            return survey;
        }

        public void Delete(object id)
        {
            var survey = this.surveys.GetById(id);
            this.surveys.Delete(id);

            foreach (var quest in survey.Questions)
            {
                var question = this.questions.GetById(quest.Id);
                this.questions.Delete(quest.Id);

                foreach (var amswer in question.PossibleAnswers)
                {
                    this.possibleAnswers.Delete(amswer.Id);
                }
            }

            this.possibleAnswers.SaveChanges();
            this.questions.SaveChanges();
            this.surveys.SaveChanges();
        }

        public IQueryable<Survey> GetMostPopular(int numberOfSurveys)
        {
            return this.surveys
                                .All()
                                .OrderByDescending(x => x.Responses.Count)
                                .Take(numberOfSurveys);
        }

        public Survey Add(Survey survey)
        {
            this.surveys.Add(survey);
            this.surveys.SaveChanges();

            return survey;
        }
    }
}
