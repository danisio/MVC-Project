﻿namespace MySurveys.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using Base;
    using Models;

    public class SurveyViewModel : AdministrationViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(100), MinLength(3)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Author")]
        public string AuthorUsername { get; set; }

        public int TotalQuestions { get; set; }

        public static MapperConfiguration Configuration
        {
            get
            {
                return new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<Survey, SurveyViewModel>()
                            .ForMember(s => s.TotalQuestions, opt => opt.MapFrom(q => q.Questions.Count))
                            .ForMember(s => s.AuthorUsername, opt => opt.MapFrom(u => u.Author.UserName));
                    });
            }
        }
    }
}