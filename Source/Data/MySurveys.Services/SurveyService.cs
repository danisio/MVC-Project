namespace MySurveys.Services
{
    using System.Linq;
    using Contracts;
    using Data.Repository;
    using Models;
    using Web.Infrastructure.IdBinder;

    public class SurveyService : ISurveyService
    {
        private IRepository<Survey> surveys;
        private IIdentifierProvider identifierProvider;

        public SurveyService(IRepository<Survey> surveys, IIdentifierProvider identifierProvider)
        {
            this.surveys = surveys;
            this.identifierProvider = identifierProvider;
        }

        public IQueryable<Survey> GetAll()
        {
            return this.surveys.All();
        }

        public IQueryable<Survey> GetAllPublic()
        {
            return this.surveys
                       .All()
                       .Where(s => s.IsPublic == true);
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
            this.surveys.Delete(id);
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
