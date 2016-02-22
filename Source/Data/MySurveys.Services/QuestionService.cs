namespace MySurveys.Services
{
    using System.Linq;
    using Contracts;
    using Data.Repository;
    using Models;

    public class QuestionService : IQuestionService
    {
        private IRepository<Question> questions;
        private IRepository<PossibleAnswer> possibleAnswers;

        public QuestionService(IRepository<Question> questions, IRepository<PossibleAnswer> possibleAnswers)
        {
            this.questions = questions;
            this.possibleAnswers = possibleAnswers;
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
            var question = this.questions.GetById(id);
            this.questions.Delete(id);

            foreach (var ans in question.PossibleAnswers)
            {
                this.possibleAnswers.Delete(ans.Id);
            }

            this.possibleAnswers.SaveChanges();
            this.questions.SaveChanges();
        }

        public Question GetNext(Question question, string possibleAnswerContent)
        {
            string nextQuestionContent;

            if (question.IsDependsOn)
            {
                nextQuestionContent = question.Content + "|" + possibleAnswerContent;
            }
            else
            {
                nextQuestionContent = question.Content;
            }

            return this.questions.All()
                               .FirstOrDefault(q => q.ParentContent == nextQuestionContent && q.SurveyId == question.SurveyId);
        }
    }
}
