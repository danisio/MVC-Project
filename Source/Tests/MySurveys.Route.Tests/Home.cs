namespace MySurveys.Route.Tests
{
    using System.Net.Http;
    using System.Web.Routing;
    using MvcRouteTester;
    using NUnit.Framework;
    using Web;
    using Web.Controllers;

    [TestFixture]
    public class Home
    {
        private RouteCollection routes;

        [SetUp]
        public void RoutesRegistration()
        {
            routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
        }

        [Test]
        public void IndexActionShoulMapCorrectly()
        {
            const string createUrl = "/";
            routes
                .ShouldMap(createUrl)
                .To<HomeController>(HttpMethod.Get, x => x.Index());
        }

        [Test]
        public void IndexActionShoulMapCorrectly2()
        {
            const string createUrl = "/Home";
            routes
                .ShouldMap(createUrl)
                .To<HomeController>(HttpMethod.Get, x => x.Index());
        }

        [Test]
        public void IndexActionShoulMapCorrectly3()
        {
            const string createUrl = "/Home/Index";
            routes
                .ShouldMap(createUrl)
                .To<HomeController>(HttpMethod.Get, x => x.Index());
        }

        [Test]
        public void HasRouteWithoutController()
        {
            RouteAssert.HasRoute(routes, "/foo/bar/1");
        }

        [Test]
        public void DoesNotHaveOtherRoute()
        {
            RouteAssert.NoRoute(routes, "/foo/bar/fish/spon");
        }
    }
}
