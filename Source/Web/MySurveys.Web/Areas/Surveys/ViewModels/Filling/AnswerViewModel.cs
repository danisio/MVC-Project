﻿namespace MySurveys.Web.Areas.Surveys.ViewModels.Filling
{
    using System.Web.Mvc;
    using AutoMapper;
    using Models;
    using MvcTemplate.Web.Infrastructure.Mapping;

    public class AnswerViewModel : IMapFrom<Answer>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public int PossibleAnswerId { get; set; }

        public int ResponseId { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Answer, AnswerViewModel>()
               .ForMember(s => s.QuestionId, opt => opt.MapFrom(r => r.QuestionId))
               .ForMember(s => s.PossibleAnswerId, opt => opt.MapFrom(r => r.PossibleAnswerId))
               .ForMember(s => s.ResponseId, opt => opt.MapFrom(r => r.ResponseId))
               .ReverseMap();
        }
    }
}