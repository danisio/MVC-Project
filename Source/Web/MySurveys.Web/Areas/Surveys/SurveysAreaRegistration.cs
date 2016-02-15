namespace MySurveys.Web.Areas.Surveys
{
    using System.Web.Mvc;

    public class SurveysAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Surveys";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
               "Surveys_Scroll",
               "Surveys/{controller}/{action}",
               new { controller = "Surveys", action = "GetSurveys" },
               new string[] { "MySurveys.Web.Areas.Surveys.Controllers" });

            context.MapRoute(
               "Surveys_default",
               "Surveys/{controller}/{action}/{id}",
               new { controller = "Surveys", action = "Index", id = UrlParameter.Optional },
               new string[] { "MySurveys.Web.Areas.Surveys.Controllers" });
        }
    }
}