namespace MySurveys.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data.Repository;
    using Models;

    public class SurveyService : ISurveyService
    {
        private IRepository<Survey> surveys;

        public SurveyService(IRepository<Survey> surveys)
        {
            this.surveys = surveys;
        }

        public IQueryable<Survey> GetAll()
        {
            return this.surveys.All();
        }

        public Survey GetById(object id)
        {
            return this.surveys.GetById(id);
        }

        public Survey Update(Survey survey)
        {
            this.surveys.Update(survey);
            this.surveys.SaveChanges();

            return survey;
        }

        public void Delete(object id)
        {
            this.surveys.Delete(id);
            this.surveys.SaveChanges();
        }

        public IQueryable<Survey> GetMostPopular(int numberOfSurveys)
        {
            return this.surveys
                                .All()
                                .Where(x => x.IsPublic == true)
                                .OrderByDescending(x => x.Answers.Count)
                                .Take(numberOfSurveys);
        }
    }
}
