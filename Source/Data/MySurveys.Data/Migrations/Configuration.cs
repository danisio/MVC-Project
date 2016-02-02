namespace MySurveys.Data.Migrations
{
    using System.Linq;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<MySurveysDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MySurveysDbContext context)
        {
            if (!context.Questions.Any())
            {
                context.Questions.Add(new Models.Question() { Title = "Test question number 1" });
                context.Questions.Add(new Models.Question() { Title = "Test question number 2" });
                context.Questions.Add(new Models.Question() { Title = "Test question number 3" });
            }
        }
    }
}
