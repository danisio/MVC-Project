namespace MySurveys.Controllers.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
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
        const string SurveyTitle = "Test survey title";
        const string UserEmail = "user@test.com";
        const string UserName = "user";
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
                .Returns(new User { Email = UserEmail, UserName = UserName, });

            surveysServiceMock
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new Survey { Title = SurveyTitle, Author = userServiceMock.Object.GetById("dsds"), AuthorId = userServiceMock.Object.GetById("dsss").Id, IsPublic = true });

            controller = new PublicController(surveysServiceMock.Object, userServiceMock.Object);
        }

        [Test]
        public void DetailsShouldRenderCorrectView()
        {
            controller
                .WithCallTo(x => x.Details("fslf"))
                .ShouldRenderView("Details");
        }

        [Test]
        public void DetailsShouldRenderCorrectView2()
        {
            controller
                .WithCallTo(x => x.Details("fslf"))
                .ShouldRenderView("Details");
        }
    }
}
