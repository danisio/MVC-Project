namespace MySurveys.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using Base;
    using Models;

    public class UserViewModel : AdministrationViewModel
    {
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

        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [UIHint("CustomString")]
        public string UserName { get; set; }

        [UIHint("CustomString")]
        public string Email { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int TotalSurveys { get; set; }
    }
}