namespace MySurveys.Web.Areas.Administration.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using Base;
    using Models;
    using MvcTemplate.Web.Infrastructure.Mapping;

    public class ResponseViewModel : AdministrationViewModel, IMapFrom<Response>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "User")]
        public string AuthorUserName { get; set; }

        [Display(Name = "Survey Title")]
        public string SurveyTitle { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Response, ResponseViewModel>()
                        .ForMember(r => r.AuthorUserName, opt => opt.MapFrom(r => r.Author.UserName))
                        .ForMember(r => r.SurveyTitle, opt => opt.MapFrom(r => r.Survey.Title))
                        .ReverseMap();
        }
    }
}