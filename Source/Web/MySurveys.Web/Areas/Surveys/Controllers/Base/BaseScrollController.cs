namespace MySurveys.Web.Areas.Surveys.Controllers.Base
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Models;
    using MvcTemplate.Web.Infrastructure.Mapping;
    using Services.Contracts;
    using ViewModels.Filling;
    using Web.Controllers.Base;

    public abstract class BaseScrollController : BaseController
    {
        public const int RecordsPerPage = 10;

        public BaseScrollController(IUserService userService, ISurveyService surveyService)
            : base(userService)
        {
            this.SurveyService = surveyService;
        }

        protected ISurveyService SurveyService { get; set; }

        //// GET: Surveys/Surveys/Index
        public ActionResult Index()
        {
            ViewBag.RecordsPerPage = RecordsPerPage;
            return this.RedirectToAction("GetSurveys");
        }

        public ActionResult GetSurveys(int? pageNum)
        {
            pageNum = pageNum ?? 0;
            ViewBag.IsEndOfRecords = false;
            if (Request.IsAjaxRequest())
            {
                var surveys = this.GetRecordsForPage(pageNum.Value);
                ViewBag.IsEndOfRecords = surveys.Any() && ((pageNum.Value * RecordsPerPage) >= surveys.Last().Key);
                return this.PartialView("_SurveysPartial", surveys);
            }
            else
            {
                this.LoadAllSurveysToSession();
                ViewBag.Surveys = this.GetRecordsForPage(pageNum.Value);
                return this.View("Index");
            }
        }

        public Dictionary<int, SurveyViewModel> GetRecordsForPage(int pageNum)
        {
            Dictionary<int, SurveyViewModel> surveys = Session["Surveys"] as Dictionary<int, SurveyViewModel>;

            int from = pageNum * RecordsPerPage;
            int to = from + RecordsPerPage;

            return surveys
                .Where(x => x.Key > from && x.Key <= to)
                .OrderBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public void LoadAllSurveysToSession()
        {
            var surveys = this.GetData()
                            .To<SurveyViewModel>();

            int surveyIndex = 1;
            this.Session["Surveys"] = surveys.ToDictionary(x => surveyIndex++, x => x);
            ViewBag.TotalNumberCustomers = surveys.Count();
        }

        protected abstract IQueryable<Survey> GetData();
    }
}