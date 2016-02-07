﻿namespace MySurveys.Web.Areas.Administration.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web;
    using Base;
    using Models;
    using System.Web.Mvc;
    using AutoMapper;

    public class UserViewModel : AdministrationViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public int TotalSurveys { get; set; }

        public static MapperConfiguration Configuration
        {
            get
            {
                return new MapperConfiguration(cfg =>
               {
                   cfg.CreateMap<User, UserViewModel>()
                   .ForMember(m => m.TotalSurveys, opt => opt.MapFrom(r => r.Surveys.Count))
                   .ReverseMap();
               });
            }
        }
    }


}