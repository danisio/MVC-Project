namespace MySurveys.Web.Areas.Surveys.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using MvcTemplate.Web.Infrastructure.Mapping;
    using Models;

    public class PossibleAnswerViewModel : IMapFrom<PossibleAnswer>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(200), MinLength(2)]
        public string Content { get; set; }

        public int QuestionId { get; set; }

        public ICollection<AnswerViewModel> Answers { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<PossibleAnswer, PossibleAnswerViewModel>()
            .ForMember(s => s.QuestionId, opt => opt.MapFrom(u => u.Question.Id))
            .ReverseMap();
        }
    }
}