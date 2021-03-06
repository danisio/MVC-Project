﻿namespace MySurveys.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Common;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using Models;

    public class MySurveysDbContext : IdentityDbContext<User>
    {
        public MySurveysDbContext()
            : base("MySurveys", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Question> Questions { get; set; }

        public virtual IDbSet<Answer> Answers { get; set; }

        public virtual IDbSet<PossibleAnswer> PossibleAnswers { get; set; }

        public virtual IDbSet<Survey> Surveys { get; set; }

        public virtual IDbSet<Response> Responses { get; set; }

        public static MySurveysDbContext Create()
        {
            return new MySurveysDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
