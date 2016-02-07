namespace MySurveys.Web.Areas.Administration.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Net.Http;
    using System.Web;
    using System.Web.Mvc;
    using Models;
    using Infrastructure.Mapping;
    using Base;
    using AutoMapper;

    public class SurveyViewModel : AdministrationViewModel, IMapFrom<Survey>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int? SurveyId { get; set; }

        [Required]
        [StringLength(100), MinLength(3)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Author")]
        public string AuthorUsername { get; set; }

        public int TotalQuestions { get; set; }

        public void CreateMappings()
        {
            IConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Survey, SurveyViewModel>()
                    .ForMember(s => s.TotalQuestions, opt => opt.MapFrom(q => q.Questions.Count))
                    .ForMember(s => s.AuthorUsername, opt => opt.MapFrom(u => u.Author.UserName));
            });
        }
    }
}