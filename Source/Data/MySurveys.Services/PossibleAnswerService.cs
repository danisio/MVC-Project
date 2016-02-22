namespace MySurveys.Services
{
    using System.Linq;
    using Contracts;
    using Data.Repository;
    using Models;

    public class PossibleAnswerService : IPossibleAnswerService
    {
        private IRepository<PossibleAnswer> possibleAnswers;

        public PossibleAnswerService(IRepository<PossibleAnswer> possibleAnswers)
        {
            this.possibleAnswers = possibleAnswers;
        }

        public IQueryable<PossibleAnswer> GetAll()
        {
            return this.possibleAnswers.All();
        }

        public PossibleAnswer GetById(object id)
        {
            return this.possibleAnswers.GetById(id);
        }

        public PossibleAnswer Add(PossibleAnswer answer)
        {
            this.possibleAnswers.Add(answer);
            this.possibleAnswers.SaveChanges();

            return answer;
        }

        public PossibleAnswer Update(PossibleAnswer answer)
        {
            this.possibleAnswers.Update(answer);
            this.possibleAnswers.SaveChanges();

            return answer;
        }

        public void Delete(object id)
        {
            this.possibleAnswers.Delete(id);
            this.possibleAnswers.SaveChanges();
        }

    }
}
