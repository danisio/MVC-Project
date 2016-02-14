namespace MySurveys.Web.Areas.Surveys.ViewModels
{
    using System.Web.Mvc;
    using MvcTemplate.Web.Infrastructure.Mapping;
    using Models;

    public class AnswerViewModel : IMapFrom<Answer>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public int SurveyId { get; set; }

        public int QuestionId { get; set; }

        public int PossibleAnswerId { get; set; }

        public int ResponseId { get; set; }
    }
}