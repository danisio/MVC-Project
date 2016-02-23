namespace MySurveys.Controllers.Tests
{
    using Models;
    using Moq;
    using NUnit.Framework;
    using Services.Contracts;
    using TestStack.FluentMVCTesting;
    using Web.Areas.Surveys.Controllers;
    using Web.Areas.Surveys.ViewModels.Filling;
    using Web.Infrastructure.Mapping;

    [TestFixture]
    public class PublicControllerTests
    {
        private const string SurveyTitle = "Test survey title";
        private const string UserEmail = "user@test.com";
        private const string UserName = "user";
        private PublicController controller;

        [SetUp]
        public void SetUp()
        {
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(PublicController).Assembly);
            var surveysServiceMock = new Mock<ISurveyService>();
            var userServiceMock = new Mock<IUserService>();

            userServiceMock
                .Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(new User { Email = UserEmail, UserName = UserName, PasswordHash = "123" });

            surveysServiceMock
                .Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(new Survey { Id = 1, Title = SurveyTitle, Author = userServiceMock.Object.GetById("dsds"), AuthorId = userServiceMock.Object.GetById("dsss").Id, IsPublic = true });

            this.controller = new PublicController(surveysServiceMock.Object, userServiceMock.Object);
        }

        [Test]
        public void DetailsShouldRenderCorrectView()
        {
            this.controller
                .WithCallTo(x => x.Details("fslf"))
                .ShouldRenderView("Details");
        }

        [Test]
        public void DetailsShouldRenderCorrectViewModel()
        {
            this.controller
                .WithCallTo(x => x.Details("fslf"))
                .ShouldRenderView("Details")
                .WithModel<SurveyViewModel>(
                    viewModel =>
                    {
                        Assert.AreEqual(SurveyTitle, viewModel.Title);
                    }).AndNoModelErrors();
        }
    }
}
