namespace MySurveys.Web.Areas.Surveys.ViewModels.Creating
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using Infrastructure.IdBinder;
    using Models;
    using MvcTemplate.Web.Infrastructure.Mapping;
    public class SurveyViewModel : IMapFrom<Survey>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(100), MinLength(3)]
        //[UIHint("CustomString")]
        public string Title { get; set; }

        //[Display(Name = "Author")]
        //[HiddenInput(DisplayValue = false)]
        //[UIHint("CustomString")]
        //public string AuthorUsername { get; set; }

        public string AuthorId { get; set; }

        [UIHint("CustomBool")]
        public bool IsPublic { get; set; }

        public IList<QuestionViewModel> Questions { get; set; }

        //public ICollection<ResponseViewModel> Responses { get; set; }

        [Display(Name = "Total Questions")]
        public int TotalQuestions { get; set; }

        //[Display(Name = "Total Participants")]
        //public int TotalResponses { get; set; }

        //public string Url
        //{
        //    get
        //    {
        //        IIdentifierProvider identifier = new IdentifierProvider();
        //        return $"/Surveys/Public/Details/{identifier.EncodeId(this.Id)}";
        //    }
        //}

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Survey, SurveyViewModel>()
                //.ForMember(s => s.AuthorUsername, opt => opt.MapFrom(u => u.Author.UserName))
                .ForMember(s => s.TotalQuestions, opt => opt.MapFrom(q => q.Questions.Count))
                //.ForMember(s => s.TotalResponses, opt => opt.MapFrom(r => r.Responses.Count))
                .ReverseMap();
        }
    }
}