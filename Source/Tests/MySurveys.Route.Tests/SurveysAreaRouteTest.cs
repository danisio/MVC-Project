namespace MySurveys.Route.Tests
{
    using System.Net.Http;
    using System.Web.Mvc;
    using System.Web.Routing;
    using MvcRouteTester;
    using NUnit.Framework;
    using Web.Areas.Surveys;
    using Web.Areas.Surveys.Controllers;

    [TestFixture]
    public class SurveysAreaRouteTest
    {
        private RouteCollection routes;
        SurveysAreaRegistration areaRegistration;

        [SetUp]
        public void AreaRegistration()
        {
            routes = new RouteCollection();

            areaRegistration = new SurveysAreaRegistration();
            AreaRegistrationContext context = new AreaRegistrationContext(areaRegistration.AreaName, routes);
            areaRegistration.RegisterArea(context);
        }

        [Test]
        public void SurveysRouteById()
        {
            const string Url = "/Surveys/Public/Details/My4xMjMxMjMxMzEyMw==";
            routes
                .ShouldMap(Url)
                .To<PublicController>(HttpMethod.Get, c => c.Details("My4xMjMxMjMxMzEyMw=="));
        }

        [Test]
        public void PublicIndexAreaRegistration()
        {
            routes
                .ShouldMap("/Surveys/Public")
                .To<PublicController>(HttpMethod.Get, x => x.GetSurveys((int?)null));
        }

        [Test]
        public void SurveysFillingAreaRegistration()
        {
            const string url = "/Surveys/Surveys/FillingUp?id=My4xMjMxMjMxMzEyMw%3D%3D";
            routes
                .ShouldMap(url)
                .To<SurveysController>(HttpMethod.Get, x => x.FillingUp("My4xMjMxMjMxMzEyMw%3D%3D"));
        }

        [Test]
        public void HasDefaultRouteToArea()
        {
            RouteAssert.HasRoute(routes, "/Surveys");
            RouteAssert.HasRoute(routes, "/Surveys/Public");
        }

        [Test]
        public void AreaRouteHasControllerAndAction()
        {
            RouteAssert.HasRoute(routes, "/Surveys", new { Controller = "Surveys", Action = "GetSurveys" });
            RouteAssert.HasRoute(routes, "/Surveys/Public", new { Controller = "Public", Action = "GetSurveys" });
        }

        [Test]
        public void AreaRouteHasAreaName()
        {
            RouteAssert.HasRoute(routes, "/Surveys", new { Area = "Surveys", Controller = "Surveys", Action = "GetSurveys" });
            RouteAssert.HasRoute(routes, "/Surveys/Public", new { Area = "Surveys", Controller = "Public", Action = "GetSurveys" });
        }

        [Test]
        public void CreateSurveyActionShoulMapCorrectly()
        {
            const string createUrl = "/Surveys/MySurveys/Create";
            routes
                .ShouldMap(createUrl)
                .To<MySurveysController>(HttpMethod.Get, x => x.Create());
        }

        [Test]
        public void AddNewQuestionActionShoulMapCorrectly()
        {
            const string createUrl = "/Surveys/MySurveys/ViewForm";
            routes
                .ShouldMap(createUrl)
                .To<MySurveysController>(HttpMethod.Get, x => x.ViewForm());
        }

        [Test]
        public void SaveNewQuestionActionShoulMapCorrectly()
        {
            const string createUrl = "/Surveys/MySurveys/AddNew";
            routes
                .ShouldMap(createUrl)
                .To<MySurveysController>(HttpMethod.Post, x => x.AddNew(null));
        }

        [Test]
        public void EditQuestionActionShoulMapCorrectly()
        {
            const string createUrl = "/Surveys/MySurveys/EditQuestion";
            routes
                .ShouldMap(createUrl)
                .To<MySurveysController>(HttpMethod.Get, x => x.EditQuestion(null, null));
        }

        [Test]
        public void StartCreatingSurveyActionShoulMapCorrectly()
        {
            const string createUrl = "/Surveys/MySurveys/SaveSurvey";
            routes
                .ShouldMap(createUrl)
                .To<MySurveysController>(HttpMethod.Get, x => x.SaveSurvey());
        }

        [Test]
        public void SaveCreatingSurveyActionShoulMapCorrectly()
        {
            const string createUrl = "/Surveys/MySurveys/SaveSurvey";
            routes
                .ShouldMap(createUrl)
                .To<MySurveysController>(HttpMethod.Post, x => x.SaveSurvey(null));
        }
    }
}
