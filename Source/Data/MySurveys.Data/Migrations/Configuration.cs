namespace MySurveys.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using MySurveys.Common;

    public sealed class Configuration : DbMigrationsConfiguration<MySurveysDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MySurveysDbContext context)
        {
            this.SeedRoles(context);
            this.SeedUsers(context);
            //this.SeedSurveys(context);
        }

        //private void SeedSurveys(MySurveysDbContext context)
        //{
        //    if (!context.Surveys.Any())
        //    {
        //        var newSurvey = new Survey()
        //        {
        //            AuthorId = context.Users.FirstOrDefault().Id,
        //            Title = "Public survey",
        //            IsPublic = true
        //        };

        //        context.Surveys.Add(newSurvey);
        //        context.SaveChanges();

        //        var firstQuestion = new Question() { Content = "Male or Female?" };
        //        var maleAnswer = new PossibleAnswer() { Content = "Male" };
        //        var femaleAnswer = new PossibleAnswer() { Content = "Female" };

        //        firstQuestion.PossibleAnswers.Add(maleAnswer);
        //        firstQuestion.PossibleAnswers.Add(femaleAnswer);

        //        newSurvey.Questions.Add(firstQuestion);
        //        context.SaveChanges();

        //        var secondQuestion = new Question()
        //        {
        //            Content = "Do you smoke?",
        //            ParentContent = firstQuestion.PossibleAnswers.FirstOrDefault().Content
        //        };

        //        secondQuestion.IsDependsOn = true;
        //        var yesAnswer = new PossibleAnswer() { Content = "Yes" };
        //        var noAnswer = new PossibleAnswer() { Content = "No" };

        //        secondQuestion.PossibleAnswers.Add(yesAnswer);
        //        secondQuestion.PossibleAnswers.Add(noAnswer);

        //        newSurvey.Questions.Add(secondQuestion);
        //        context.SaveChanges();

        //        var thirdQuestion = new Question()
        //        {
        //            Content = "What's your age?",
        //            ParentContent = yesAnswer.Content
        //        };

        //        var underAnswer = new PossibleAnswer() { Content = "Under 25" };
        //        var overAnswer = new PossibleAnswer() { Content = "Over 25" };

        //        thirdQuestion.PossibleAnswers.Add(underAnswer);
        //        thirdQuestion.PossibleAnswers.Add(overAnswer);

        //        newSurvey.Questions.Add(thirdQuestion);
        //        context.SaveChanges();

        //        var fourthQuestion = new Question()
        //        {
        //            Content = "Sorry, you are not suitable for this survey.",
        //            ParentContent = noAnswer.Content
        //        };

        //        newSurvey.Questions.Add(fourthQuestion);
        //        context.SaveChanges();

        //        var newSurvey1 = new Survey()
        //        {
        //            AuthorId = context.Users.FirstOrDefault().Id,
        //            Title = "Private survey"
        //        };

        //        context.Surveys.Add(newSurvey1);
        //        context.SaveChanges();

        //        var firstQuestion1 = new Question() { Content = "Male or Female?" };
        //        var maleAnswer1 = new PossibleAnswer() { Content = "Male" };
        //        var femaleAnswer1 = new PossibleAnswer() { Content = "Female" };

        //        firstQuestion1.PossibleAnswers.Add(maleAnswer1);
        //        firstQuestion1.PossibleAnswers.Add(femaleAnswer1);

        //        newSurvey1.Questions.Add(firstQuestion1);
        //        context.SaveChanges();

        //        var secondQuestion1 = new Question()
        //        {
        //            Content = "Do you smoke?",
        //            ParentContent = firstQuestion1.PossibleAnswers.FirstOrDefault().Content
        //        };

        //        secondQuestion1.IsDependsOn = true;
        //        var yesAnswer1 = new PossibleAnswer() { Content = "Yes" };
        //        var noAnswer1 = new PossibleAnswer() { Content = "No" };

        //        secondQuestion1.PossibleAnswers.Add(yesAnswer1);
        //        secondQuestion1.PossibleAnswers.Add(noAnswer1);

        //        newSurvey1.Questions.Add(secondQuestion1);
        //        context.SaveChanges();

        //        var thirdQuestion1 = new Question()
        //        {
        //            Content = "What's your age?",
        //            ParentContent = yesAnswer1.Content
        //        };

        //        var underAnswer1 = new PossibleAnswer() { Content = "Under 25" };
        //        var overAnswer1 = new PossibleAnswer() { Content = "Over 25" };

        //        thirdQuestion1.PossibleAnswers.Add(underAnswer1);
        //        thirdQuestion1.PossibleAnswers.Add(overAnswer1);

        //        newSurvey1.Questions.Add(thirdQuestion1);
        //        context.SaveChanges();

        //        var fourthQuestion1 = new Question()
        //        {
        //            Content = "Sorry, you are not suitable for this survey.",
        //            ParentContent = noAnswer1.Content
        //        };

        //        newSurvey1.Questions.Add(fourthQuestion1);
        //        context.SaveChanges();
        //    }
        //}

        private void SeedUsers(MySurveysDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var userManager = new UserManager<User>(new UserStore<User>(context));
            var admin = new User()
            {
                UserName = "admin@site.com",
                Email = "admin@site.com"
            };

            var anonimous = new User()
            {
                UserName = "AnonimousUser",
                Email = "AnonimousUser@site.com"
            };

            userManager.Create(admin, "admin123456");
            userManager.Create(anonimous, "123456");

            userManager.AddToRole(admin.Id, GlobalConstants.AdminRoleName);
            context.SaveChanges();
        }

        private void SeedRoles(MySurveysDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var adminRole = new IdentityRole { Name = "Administrator" };
            roleManager.Create(adminRole);

            context.SaveChanges();
        }
    }
}
