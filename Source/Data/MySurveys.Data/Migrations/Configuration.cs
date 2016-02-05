namespace MySurveys.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

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
            this.SeedSurveys(context);
        }

        private void SeedSurveys(MySurveysDbContext context)
        {
            if (!context.Surveys.Any())
            {
                var newSurvey = new Survey()
                {
                    AuthorId = context.Users.FirstOrDefault().Id,
                    Title = "Short Survey Test"
                };

                context.Surveys.Add(newSurvey);
                context.SaveChanges();

                var firstQuestion = new Question() { Content = "Male or Female?" };
                var maleAnswer = new PossibleAnswer() { Content = "Male" };
                var femaleAnswer = new PossibleAnswer() { Content = "Female" };

                firstQuestion.PossibleAnswers.Add(maleAnswer);
                firstQuestion.PossibleAnswers.Add(femaleAnswer);

                newSurvey.Questions.Add(firstQuestion);
                context.SaveChanges();

                var secondQuestion = new Question()
                {
                    Content = "Do you smoke?",
                    ParentPossibleAnswerId = firstQuestion.PossibleAnswers.FirstOrDefault().Id
                };

                secondQuestion.IsDependsOn = true;
                var yesAnswer = new PossibleAnswer() { Content = "Yes" };
                var noAnswer = new PossibleAnswer() { Content = "No" };

                secondQuestion.PossibleAnswers.Add(yesAnswer);
                secondQuestion.PossibleAnswers.Add(noAnswer);

                newSurvey.Questions.Add(secondQuestion);
                context.SaveChanges();

                var thirdQuestion = new Question()
                {
                    Content = "What's your age?",
                    ParentPossibleAnswerId = yesAnswer.Id
                };

                var underAnswer = new PossibleAnswer() { Content = "Under 25" };
                var overAnswer = new PossibleAnswer() { Content = "Over 25" };

                thirdQuestion.PossibleAnswers.Add(underAnswer);
                thirdQuestion.PossibleAnswers.Add(overAnswer);

                newSurvey.Questions.Add(thirdQuestion);
                context.SaveChanges();

                var fourthQuestion = new Question()
                {
                    Content = "Sorry, you are not suitable for this survey.",
                    ParentPossibleAnswerId = noAnswer.Id
                };

                newSurvey.Questions.Add(fourthQuestion);
                context.SaveChanges();
            }
        }

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
            };

            userManager.Create(admin, "admin123456");
            userManager.AddToRole(admin.Id, "Administrator");

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
