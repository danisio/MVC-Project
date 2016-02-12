namespace MySurveys.Web.Areas.Surveys.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Models;
    using MvcTemplate.Web.Infrastructure.Mapping;

    public class QuestionViewModel : IMapFrom<Question>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200), MinLength(3)]
        public string Content { get; set; }

        public int SurveyId { get; set; }

        public int? ParentPossibleAnswerId { get; set; }

        public bool IsDependsOn { get; set; }

        public ICollection<AnswerViewModel> Answers { get; set; }

        public ICollection<PossibleAnswerViewModel> PossibleAnswers { get; set; }
    }
}