namespace MySurveys.Services.Contracts
{
    using System.Linq;
    using Models;

    public interface IPossibleAnswerService : IService
    {
        IQueryable<PossibleAnswer> GetAll();
        
        PossibleAnswer GetById(object id);

        PossibleAnswer Add(PossibleAnswer answer);

        PossibleAnswer Update(PossibleAnswer answer);

        void Delete(object id);
    }
}
