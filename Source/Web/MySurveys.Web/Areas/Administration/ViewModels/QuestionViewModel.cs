namespace MySurveys.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using Base;
    using Models;
    using MvcTemplate.Web.Infrastructure.Mapping;

    public class QuestionViewModel : AdministrationViewModel, IMapFrom<Question>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(50), MinLength(3)]
        [UIHint("CustomString")]
        public string Content { get; set; }

        [Display(Name = "Survey Title")]
        [HiddenInput(DisplayValue = false)]
        public string SurveyTitle { get; set; }

        [Display(Name = "Parent Answer Id")]
        [HiddenInput(DisplayValue = false)]
        public int ParentId { get; set; }

        [Display(Name = "Total Answers")]
        [HiddenInput(DisplayValue = false)]
        public int TotalPossibleAnswers { get; set; }

        [Display(Name = "Is Dynamic?")]
        [HiddenInput(DisplayValue = false)]
        public bool IsDependsOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Question, QuestionViewModel>()
                         .ForMember(s => s.SurveyTitle, opt => opt.MapFrom(q => q.Survey.Title))
                         .ForMember(s => s.TotalPossibleAnswers, opt => opt.MapFrom(a => a.PossibleAnswers.Count))
                         .ReverseMap();
        }
    }
}