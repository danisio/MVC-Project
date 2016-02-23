namespace MySurveys.Controllers.Tests
{
    using Models;
    using Moq;
    using NUnit.Framework;
    using Services.Contracts;
    using TestStack.FluentMVCTesting;
    using Web.Areas.Surveys.Controllers;
    using Web.Infrastructure.Mapping;

    [TestFixture]
    public class SurveysControllerTests
    {
        const string SurveyTitle = "Test survey title";
        const string UserEmail = "user@test.com";
        const string UserName = "user";

        SurveysController controller;

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
                .Returns(new User { Email = UserEmail, UserName = UserName, });

            surveysServiceMock
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new Survey { Title = SurveyTitle , Author = userServiceMock.Object.GetById("dsds"),AuthorId = userServiceMock.Object.GetById("dsss").Id, IsPublic = true});

            questionServiceMock.Setup(x => x.GetNext(It.IsAny<Question>(), It.IsAny<string>())).Returns(new Question {Content = "" });
            responseServiceMock.Setup(x => x.Update(It.IsAny<Response>())).Returns(new Response { });

            controller = new SurveysController(surveysServiceMock.Object, userServiceMock.Object, questionServiceMock.Object, responseServiceMock.Object);
        }

        //[Test]
        //public void FillingUpShoudRenderCorrectView()
        //{
        //    controller
        //        .WithCallTo(x => x.FillingUp("fslf"))
        //        .ShouldRenderView("FillingUp");
        //}
    }
}
