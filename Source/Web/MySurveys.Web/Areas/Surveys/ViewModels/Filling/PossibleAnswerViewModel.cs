namespace MySurveys.Web.Areas.Surveys.ViewModels.Filling
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using Models;
    using MvcTemplate.Web.Infrastructure.Mapping;

    public class PossibleAnswerViewModel : IMapFrom<PossibleAnswer>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Possible answer")]
        public string Content { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public ICollection<AnswerViewModel> Answers { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<PossibleAnswer, PossibleAnswerViewModel>()
            .ForMember(s => s.QuestionId, opt => opt.MapFrom(u => u.Question.Id))
            .ReverseMap();
        }
    }
}