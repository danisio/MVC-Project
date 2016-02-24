namespace MySurveys.Web.Areas.Surveys.ViewModels.Details
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Models;
    using MvcTemplate.Web.Infrastructure.Mapping;

    public class QuestionViewModel : IMapFrom<Question>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public List<PossibleAnswerViewModel> PossibleAnswers { get; set; }

        public List<AnswerViewModel> Answers { get; set; }
    }
}