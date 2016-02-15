//namespace MySurveys.Web.ViewModels
//{
//    using System.ComponentModel.DataAnnotations;
//    using AutoMapper;
//    using Infrastructure.IdBinder;
//    using Models;
//    using MvcTemplate.Web.Infrastructure.Mapping;

//    public class SurveyViewModel : IMapFrom<Survey>, IHaveCustomMappings
//    {
//        public int Id { get; set; }

//        [Display(Name = "Title")]
//        public string Title { get; set; }

//        [Display(Name = "Author")]
//        public string AuthorUsername { get; set; }

//        [Display(Name = "Total Questions")]
//        public int TotalQuestions { get; set; }

//        [Display(Name = "Total Participants")]
//        public int TotalResponses { get; set; }

//        public bool IsPublic { get; set; }

//        public string Url
//        {
//            get
//            {
//                IIdentifierProvider identifier = new IdentifierProvider();
//                return $"/Surveys/Public/Details/{identifier.EncodeId(this.Id)}";
//            }
//        }

//        public void CreateMappings(IMapperConfiguration configuration)
//        {
//            configuration.CreateMap<Survey, SurveyViewModel>()
//                .ForMember(s => s.TotalQuestions, opt => opt.MapFrom(q => q.Questions.Count))
//                .ForMember(s => s.TotalResponses, opt => opt.MapFrom(q => q.Responses.Count))
//                .ForMember(s => s.AuthorUsername, opt => opt.MapFrom(u => u.Author.UserName))
//                .ReverseMap();
//        }
//    }
//}