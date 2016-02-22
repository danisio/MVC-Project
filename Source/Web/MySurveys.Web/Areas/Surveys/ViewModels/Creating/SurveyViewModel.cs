namespace MySurveys.Web.Areas.Surveys.ViewModels.Creating
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using Models;
    using MvcTemplate.Web.Infrastructure.Mapping;

    public class SurveyViewModel : IMapFrom<Survey>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(100), MinLength(3)]
        public string Title { get; set; }

        public string AuthorId { get; set; }

        [UIHint("CustomBool")]
        public bool IsPublic { get; set; }

        public IList<QuestionViewModel> Questions { get; set; }

        [Display(Name = "Total Questions")]
        public int TotalQuestions { get; set; }
        
        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Survey, SurveyViewModel>()
                .ForMember(s => s.TotalQuestions, opt => opt.MapFrom(q => q.Questions.Count))
                .ReverseMap();
        }
    }
}