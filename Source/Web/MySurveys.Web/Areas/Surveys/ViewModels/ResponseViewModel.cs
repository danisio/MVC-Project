namespace MySurveys.Web.Areas.Surveys.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MvcTemplate.Web.Infrastructure.Mapping;
    using MySurveys.Models;

    public class ResponseViewModel : IMapFrom<Response>
    {
        [Key]
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public string SurveyId { get; set; }

        public ICollection<AnswerViewModel> Answers { get; set; }
    }
}