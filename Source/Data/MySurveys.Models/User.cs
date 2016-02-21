namespace MySurveys.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Data.Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser, IAuditInfo, IDeletableEntity
    {
        private ICollection<Survey> surveys;
        private ICollection<Response> responses;

        public User()
        {
            // This will prevent UserManager.CreateAsync from causing exception
            this.CreatedOn = DateTime.Now;
            this.surveys = new HashSet<Survey>();
            this.responses = new HashSet<Response>();
        }

        public DateTime CreatedOn { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public ICollection<Survey> Surveys
        {
            get { return this.surveys; }
            set { this.surveys = value; }
        }

        public ICollection<Response> Responses
        {
            get { return this.responses; }
            set { this.responses = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            //// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            //// Add custom user claims here
            return userIdentity;
        }
    }
}
