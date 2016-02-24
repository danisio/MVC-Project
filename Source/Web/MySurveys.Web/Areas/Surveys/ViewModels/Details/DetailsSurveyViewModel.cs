namespace MySurveys.Web.Areas.Surveys.ViewModels.Details
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Models;
    using MvcTemplate.Web.Infrastructure.Mapping;

    public class DetailsSurveyViewModel : IMapFrom<Survey>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Title { get; set; }

        public IList<QuestionViewModel> Questions { get; set; }
    }
}