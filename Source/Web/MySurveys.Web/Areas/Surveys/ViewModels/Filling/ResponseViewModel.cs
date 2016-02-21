namespace MySurveys.Web.Areas.Surveys.ViewModels.Filling
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using MvcTemplate.Web.Infrastructure.Mapping;
    using MySurveys.Models;

    public class ResponseViewModel : IMapFrom<Response>, IHaveCustomMappings
    {
        [Key]
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public int SurveyId { get; set; }

        public ICollection<AnswerViewModel> Answers { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Response, ResponseViewModel>()
              .ForMember(s => s.AuthorId, opt => opt.MapFrom(u => u.Author.Id))
              .ForMember(s => s.SurveyId, opt => opt.MapFrom(u => u.Survey.Id))
              .ReverseMap();
        }
    }
}