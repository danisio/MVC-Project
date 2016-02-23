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
        private SurveysAreaRegistration areaRegistration;

        [SetUp]
        public void AreaRegistration()
        {
            this.routes = new RouteCollection();

            this.areaRegistration = new SurveysAreaRegistration();
            AreaRegistrationContext context = new AreaRegistrationContext(this.areaRegistration.AreaName, this.routes);
            this.areaRegistration.RegisterArea(context);
        }

        [Test]
        public void SurveysRouteById()
        {
            const string Url = "/Surveys/Public/Details/My4xMjMxMjMxMzEyMw==";
            this.routes
                .ShouldMap(Url)
                .To<PublicController>(HttpMethod.Get, c => c.Details("My4xMjMxMjMxMzEyMw=="));
        }

        [Test]
        public void PublicIndexAreaRegistration()
        {
            this.routes
                .ShouldMap("/Surveys/Public")
                .To<PublicController>(HttpMethod.Get, x => x.GetSurveys((int?)null));
        }

        [Test]
        public void SurveysFillingAreaRegistration()
        {
            const string Url = "/Surveys/Surveys/FillingUp?id=My4xMjMxMjMxMzEyMw%3D%3D";
            this.routes
                .ShouldMap(Url)
                .To<SurveysController>(HttpMethod.Get, x => x.FillingUp("My4xMjMxMjMxMzEyMw%3D%3D"));
        }

        [Test]
        public void HasDefaultRouteToArea()
        {
            RouteAssert.HasRoute(this.routes, "/Surveys");
            RouteAssert.HasRoute(this.routes, "/Surveys/Public");
        }

        [Test]
        public void AreaRouteHasControllerAndAction()
        {
            RouteAssert.HasRoute(this.routes, "/Surveys", new { Controller = "Surveys", Action = "GetSurveys" });
            RouteAssert.HasRoute(this.routes, "/Surveys/Public", new { Controller = "Public", Action = "GetSurveys" });
        }

        [Test]
        public void AreaRouteHasAreaName()
        {
            RouteAssert.HasRoute(this.routes, "/Surveys", new { Area = "Surveys", Controller = "Surveys", Action = "GetSurveys" });
            RouteAssert.HasRoute(this.routes, "/Surveys/Public", new { Area = "Surveys", Controller = "Public", Action = "GetSurveys" });
        }

        [Test]
        public void CreateSurveyActionShoulMapCorrectly()
        {
            const string Url = "/Surveys/MySurveys/Create";
            this.routes
                .ShouldMap(Url)
                .To<MySurveysController>(HttpMethod.Get, x => x.Create());
        }

        [Test]
        public void AddNewQuestionActionShoulMapCorrectly()
        {
            const string Url = "/Surveys/MySurveys/ViewForm";
            this.routes
                .ShouldMap(Url)
                .To<MySurveysController>(HttpMethod.Get, x => x.ViewForm());
        }

        [Test]
        public void SaveNewQuestionActionShoulMapCorrectly()
        {
            const string Url = "/Surveys/MySurveys/AddNew";
            this.routes
                .ShouldMap(Url)
                .To<MySurveysController>(HttpMethod.Post, x => x.AddNew(null));
        }

        [Test]
        public void EditQuestionActionShoulMapCorrectly()
        {
            const string Url = "/Surveys/MySurveys/EditQuestion";
            this.routes
                .ShouldMap(Url)
                .To<MySurveysController>(HttpMethod.Get, x => x.EditQuestion(null, null));
        }

        [Test]
        public void StartCreatingSurveyActionShoulMapCorrectly()
        {
            const string Url = "/Surveys/MySurveys/SaveSurvey";
            this.routes
                .ShouldMap(Url)
                .To<MySurveysController>(HttpMethod.Get, x => x.SaveSurvey());
        }

        [Test]
        public void SaveCreatingSurveyActionShoulMapCorrectly()
        {
            const string Url = "/Surveys/MySurveys/SaveSurvey";
            this.routes
                .ShouldMap(Url)
                .To<MySurveysController>(HttpMethod.Post, x => x.SaveSurvey(null));
        }
    }
}
