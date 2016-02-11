namespace MySurveys.Services.Contracts
{
    using System.Linq;
    using Models;

    public interface ISurveyService : IService
    {
        IQueryable<Survey> GetAll();

        Survey GetById(int id);

        Survey GetById(string id);

        Survey Update(Survey survey);

        void Delete(object id);

        IQueryable<Survey> GetMostPopular(int numberOfSurveys);
    }
}
