namespace MySurveys.Web.Areas.Surveys.ViewModels.Creating
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
        [StringLength(200), MinLength(2)]
        public string Content { get; set; }

        public int SurveyId { get; set; }

        public string ParentContent { get; set; }

        [Display(Name = "Is dynamic?")]
        [UIHint("CustomBool")]
        public bool IsDependsOn { get; set; }

        public List<PossibleAnswerViewModel> PossibleAnswers { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Question, QuestionViewModel>()
                 .ReverseMap();
        }
    }
}