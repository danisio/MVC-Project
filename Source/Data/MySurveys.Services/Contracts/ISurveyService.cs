namespace MySurveys.Services.Contracts
{
    using System.Linq;
    using Models;

    public interface ISurveyService : IService
    {
        IQueryable<Survey> GetAll();

        Survey GetById(object id);

        Survey Update(Survey survey);

        void Delete(object id);
    }
}
