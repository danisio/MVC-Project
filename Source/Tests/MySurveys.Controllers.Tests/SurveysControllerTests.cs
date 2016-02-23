namespace MySurveys.Controllers.Tests
{
    using System.Collections.Generic;
    using Models;
    using Moq;
    using NUnit.Framework;
    using Services.Contracts;
    using TestStack.FluentMVCTesting;
    using Web.Areas.Surveys.Controllers;
    using Web.Areas.Surveys.ViewModels.Filling;
    using Web.Infrastructure.Mapping;

    [TestFixture]
    public class SurveysControllerTests
    {
        private const string SurveyTitle = "Test survey title";
        private const string UserEmail = "user@test.com";
        private const string UserName = "user";

        private SurveysController controller;

        [SetUp]
        public void SetUp()
        {
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(SurveysController).Assembly);
            var surveysServiceMock = new Mock<ISurveyService>();
            var userServiceMock = new Mock<IUserService>();
            var questionServiceMock = new Mock<IQuestionService>();
            var responseServiceMock = new Mock<IResponseService>();

            userServiceMock
                .Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(new User { Email = UserEmail, UserName = UserName, PasswordHash = "123" });

            surveysServiceMock
                .Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(new Survey { Id = 1, Title = SurveyTitle, Author = userServiceMock.Object.GetById("dsds"), AuthorId = userServiceMock.Object.GetById("dsss").Id, IsPublic = true, Questions = new List<Question> { new Question { } } });

            questionServiceMock.Setup(x => x.GetNext(It.IsAny<Question>(), It.IsAny<string>())).Returns(new Question { Content = string.Empty });
            responseServiceMock.Setup(x => x.Update(It.IsAny<Response>())).Returns(new Response { });

            this.controller = new SurveysController(surveysServiceMock.Object, userServiceMock.Object, questionServiceMock.Object, responseServiceMock.Object);
        }

        [Test]
        public void FillingUpShoudRenderCorrectView()
        {
            this.controller
                .WithCallTo(x => x.FillingUp("fewt"))
                .ShouldRenderView("FillingUp")
                .WithModel<QuestionViewModel>()
                .AndNoModelErrors();
        }
    }
}
