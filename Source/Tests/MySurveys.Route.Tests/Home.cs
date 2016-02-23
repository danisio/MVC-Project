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
            this.routes = new RouteCollection();
            RouteConfig.RegisterRoutes(this.routes);
        }

        [Test]
        public void IndexActionShoulMapCorrectly()
        {
            const string CreateUrl = "/";
            this.routes
                .ShouldMap(CreateUrl)
                .To<HomeController>(HttpMethod.Get, x => x.Index());
        }

        [Test]
        public void IndexActionShoulMapCorrectly2()
        {
            const string CcreateUrl = "/Home";
            this.routes
                .ShouldMap(CcreateUrl)
                .To<HomeController>(HttpMethod.Get, x => x.Index());
        }

        [Test]
        public void IndexActionShoulMapCorrectly3()
        {
            const string CreateUrl = "/Home/Index";
            this.routes
                .ShouldMap(CreateUrl)
                .To<HomeController>(HttpMethod.Get, x => x.Index());
        }

        [Test]
        public void HasRouteWithoutController()
        {
            RouteAssert.HasRoute(this.routes, "/foo/bar/1");
        }

        [Test]
        public void DoesNotHaveOtherRoute()
        {
            RouteAssert.NoRoute(this.routes, "/foo/bar/fish/spon");
        }
    }
}
