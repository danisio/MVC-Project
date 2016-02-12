namespace MySurveys.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using Base;
    using Models;
    using MvcTemplate.Web.Infrastructure.Mapping;

    public class UserViewModel : AdministrationViewModel, IMapFrom<User>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [UIHint("CustomString")]
        public string UserName { get; set; }

        [UIHint("CustomString")]
        public string Email { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int TotalSurveys { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int TotalResponses { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<User, UserViewModel>()
                         .ForMember(m => m.TotalSurveys, opt => opt.MapFrom(r => r.Surveys.Count))
                         .ForMember(m => m.TotalResponses, opt => opt.MapFrom(r => r.Responses.Count))
                         .ReverseMap();
        }
    }
}