namespace MySurveys.Web.Areas.Surveys.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using Models;

    public class SurveyViewModel
    {
        public static MapperConfiguration Configuration
        {
            get
            {
                return new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Survey, SurveyViewModel>()
                        .ForMember(s => s.TotalQuestions, opt => opt.MapFrom(q => q.Questions.Count))
                        .ForMember(s => s.TotalAnswers, opt => opt.MapFrom(q => q.Answers.Count))
                        .ForMember(s => s.AuthorUsername, opt => opt.MapFrom(u => u.Author.UserName))
                        .ReverseMap();
                });
            }
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorUsername { get; set; }

        public int TotalQuestions { get; set; }

        public int TotalAnswers { get; set; }
    }
}