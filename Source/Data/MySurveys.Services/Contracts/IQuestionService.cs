namespace MySurveys.Services.Contracts
{
    using System.Linq;
    using Models;

    public interface IQuestionService
    {
        IQueryable<Question> GetAll();

        Question GetById(object id);

        Question Update(Question question);

        Question GetNext(Question question, int possibleAnswerId);

        void Delete(object id);
    }
}
