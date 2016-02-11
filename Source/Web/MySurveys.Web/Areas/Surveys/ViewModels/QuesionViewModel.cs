namespace MySurveys.Web.Areas.Surveys.ViewModels
{
    using System;
    using AutoMapper;
    using Models;
    using MvcTemplate.Web.Infrastructure.Mapping;

    public class QuesionViewModel : IMapFrom<Question>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            
        }
    }
}