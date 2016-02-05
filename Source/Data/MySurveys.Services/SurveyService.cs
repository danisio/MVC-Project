namespace MySurveys.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
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

        public Survey GetById(int id)
        {
            return this.surveys.GetById(id);
        }
    }
}
