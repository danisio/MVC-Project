namespace MySurveys.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using Base;
    using Models;
    using MvcTemplate.Web.Infrastructure.Mapping;

    public class PossibleAnswerViewModel : AdministrationViewModel, IMapFrom<PossibleAnswer>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(200), MinLength(2)]
        [UIHint("CustomString")]
        public string Content { get; set; }

        [Display(Name = "Question Content")]
        [UIHint("CustomString")]
        public string QuestionContent { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<PossibleAnswer, PossibleAnswerViewModel>()
                 .ForMember(r => r.QuestionContent, opt => opt.MapFrom(r => r.Question.Content))
                 .ReverseMap();
        }
    }
}