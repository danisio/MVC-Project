namespace MySurveys.Services
{
    using System.Linq;
    using Contracts;
    using Data.Repository;
    using Models;

    public class QuestionService : IQuestionService
    {
        private IRepository<Question> questions;

        public QuestionService(IRepository<Question> questions)
        {
            this.questions = questions;
        }

        public IQueryable<Question> GetAll()
        {
            return this.questions.All();
        }

        public Question GetById(object id)
        {
            return this.questions.GetById(id);
        }

        public Question Add(Question question)
        {
            this.questions.Add(question);
            this.questions.SaveChanges();

            return question;
        }

        public Question Update(Question question)
        {
            this.questions.Update(question);
            this.questions.SaveChanges();

            return question;
        }

        public void Delete(object id)
        {
            this.questions.Delete(id);
            this.questions.SaveChanges();
        }

        public Question GetNext(Question question, string possibleAnswerContent)
        {
            if (question.IsDependsOn)
            {
                return this.questions.All()
                                    .Where(q => q.ParentContent == possibleAnswerContent)
                                    .FirstOrDefault();
            }
            else
            {
                var all = this.questions.All();
                var possibleContent = question.PossibleAnswers.First().Content;

                return all.FirstOrDefault(q => q.ParentContent == possibleContent);
            }
        }
    }
}
