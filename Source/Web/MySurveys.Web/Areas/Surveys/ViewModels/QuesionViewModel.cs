namespace MySurveys.Web.Areas.Surveys.ViewModels
{
    using System;
    using AutoMapper;
    using Infrastructure.Mapping;
    using MySurveys.Models;

    public class QuesionViewModel : IMapFrom<Question>
    {
        public string Title { get; set; }
    }
}