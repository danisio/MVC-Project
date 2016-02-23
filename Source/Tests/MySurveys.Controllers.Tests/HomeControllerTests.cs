namespace MySurveys.Controllers.Tests
{
    using Models;
    using Moq;
    using NUnit.Framework;
    using Services.Contracts;
    using TestStack.FluentMVCTesting;
    using Web.Controllers;
    using Web.Infrastructure.Mapping;

    [TestFixture]
    public class HomeControllerTests
    {
        private const string SurveyTitle = "Test survey title";
        private const string UserEmail = "user@test.com";
        private const string UserName = "user";

        private HomeController controller;

        [SetUp]
        public void SetUp()
        {
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(HomeController).Assembly);
            var surveysServiceMock = new Mock<ISurveyService>();
            var userServiceMock = new Mock<IUserService>();

            userServiceMock
                .Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(new User { Email = UserEmail, UserName = UserName, PasswordHash = "123" });

            surveysServiceMock
                .Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(new Survey { Id = 1, Title = SurveyTitle, Author = userServiceMock.Object.GetById("dsds"), AuthorId = userServiceMock.Object.GetById("dsss").Id, IsPublic = true });

            this.controller = new HomeController(userServiceMock.Object, surveysServiceMock.Object);
        }

        [Test]
        public void IndexShouldRenderCorrectView()
        {
            this.controller
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();
        }
    }
}
