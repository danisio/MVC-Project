namespace MySurveys.Web.Areas.Surveys.ViewModels.Filling
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using Models;
    using MvcTemplate.Web.Infrastructure.Mapping;

    public class QuestionViewModel : IMapFrom<Question>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public int SurveyId { get; set; }

        public string ParentContent { get; set; }

        public string SurveyTitle { get; set; }

        [Display(Name = "Is dynamic?")]
        [UIHint("CustomBool")]
        public bool IsDependsOn { get; set; }

        public List<PossibleAnswerViewModel> PossibleAnswers { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Question, QuestionViewModel>()
                 .ForMember(s => s.SurveyTitle, opt => opt.MapFrom(q => q.Survey.Title))
                 .ReverseMap();
        }
    }
}