namespace MySurveys.Web.Areas.Surveys.Controllers
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Base;
    using Models;
    using Services.Contracts;
    using ViewModels;

    public class PublicController : BaseScrollController
    {
        public PublicController(ISurveyService surveyService, IUserService userService)
            : base(userService, surveyService)
        {
        }

        //// GET: Surveys/Public/Details
        public ActionResult Details(string id)
        {
            Survey survey;
            try
            {
                survey = this.SurveyService.GetById(id);
            }
            catch (System.Exception)
            {
                throw new HttpException(404, "Survey not found.");
            }

            var viewModel = this.Mapper.Map<SurveyViewModel>(survey);

            return this.View(viewModel);
        }

        protected override IQueryable<Survey> GetData()
        {
            return this.SurveyService
                        .GetAllPublic();
        }
    }
}