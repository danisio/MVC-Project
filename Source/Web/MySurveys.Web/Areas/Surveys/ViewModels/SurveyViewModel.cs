namespace MySurveys.Web.Areas.Surveys.ViewModels
{
    using AutoMapper;
    using Infrastructure.IdBinder;
    using Models;
    using MvcTemplate.Web.Infrastructure.Mapping;

    public class SurveyViewModel : IMapFrom<Survey>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorUsername { get; set; }

        public int TotalQuestions { get; set; }

        public int TotalAnswers { get; set; }

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
                .ForMember(s => s.TotalQuestions, opt => opt.MapFrom(q => q.Questions.Count))
                .ForMember(s => s.TotalAnswers, opt => opt.MapFrom(q => q.Answers.Count))
                .ForMember(s => s.AuthorUsername, opt => opt.MapFrom(u => u.Author.UserName))
                .ReverseMap();
        }
    }
}