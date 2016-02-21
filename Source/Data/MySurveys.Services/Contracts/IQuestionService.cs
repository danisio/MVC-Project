namespace MySurveys.Services.Contracts
{
    using System.Linq;
    using Models;

    public interface IQuestionService
    {
        IQueryable<Question> GetAll();

        Question GetById(object id);

        Question Add(Question question);

        Question Update(Question question);

        Question GetNext(Question question, string possibleAnswerContent);

        void Delete(object id);
    }
}
