namespace MySurveys.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using Base;
    using Models;
    using MvcTemplate.Web.Infrastructure.Mapping;

    public class SurveyViewModel : AdministrationViewModel, IMapFrom<Survey>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(100), MinLength(3)]
        [UIHint("CustomString")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Author")]
        [HiddenInput(DisplayValue = false)]
        public string AuthorUsername { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int TotalQuestions { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int TotalResponses { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Survey, SurveyViewModel>()
                         .ForMember(s => s.TotalQuestions, opt => opt.MapFrom(q => q.Questions.Count))
                         .ForMember(s => s.TotalResponses, opt => opt.MapFrom(q => q.Responses.Count))
                         .ForMember(s => s.AuthorUsername, opt => opt.MapFrom(u => u.Author.UserName))
                         .ReverseMap();
        }
    }
}