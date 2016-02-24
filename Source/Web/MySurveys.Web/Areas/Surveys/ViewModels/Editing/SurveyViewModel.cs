namespace MySurveys.Web.Areas.Surveys.ViewModels.Editing
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using Models;
    using MvcTemplate.Web.Infrastructure.Mapping;

    public class SurveyViewModel : IMapFrom<Survey>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [UIHint("CustomString")]
        public string Title { get; set; }

        [UIHint("CustomBool")]
        public bool IsPublic { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Survey, SurveyViewModel>()
                .ReverseMap();
        }
    }
}