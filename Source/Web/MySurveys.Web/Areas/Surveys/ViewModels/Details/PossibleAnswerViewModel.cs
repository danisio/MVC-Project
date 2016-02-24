namespace MySurveys.Web.Areas.Surveys.ViewModels.Details
{
    using Models;
    using MvcTemplate.Web.Infrastructure.Mapping;

    public class PossibleAnswerViewModel : IMapFrom<PossibleAnswer>
    {
        public int Id { get; set; }

        public string Content { get; set; }
    }
}