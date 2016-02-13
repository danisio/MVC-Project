namespace MySurveys.Web.Areas.Surveys.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using Infrastructure.IdBinder;
    using Models;
    using MvcTemplate.Web.Infrastructure.Mapping;

    public class SurveyViewModel : IMapFrom<Survey>, IHaveCustomMappings
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

        public bool IsPublic { get; set; }

        public ICollection<QuestionViewModel> Questions { get; set; }

        public ICollection<ResponseViewModel> Responses { get; set; }

        public string Url
        {
            get
            {
                IIdentifierProvider identifier = new IdentifierProvider();
                return $"/Surveys/Public/Details/{identifier.EncodeId(this.Id)}";
            }
        }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Survey, SurveyViewModel>()
                .ForMember(s => s.AuthorUsername, opt => opt.MapFrom(u => u.Author.UserName))
                .ReverseMap();
        }
    }
}