namespace MySurveys.Web.Areas.Surveys.ViewModels.Creating
{
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
        [StringLength(200), MinLength(2)]
        public string Content { get; set; }

        public int QuestionId { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<PossibleAnswer, PossibleAnswerViewModel>()
            .ForMember(s => s.QuestionId, opt => opt.MapFrom(u => u.Question.Id))
            .ReverseMap();
        }
    }
}