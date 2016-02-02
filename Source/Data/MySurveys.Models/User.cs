namespace MySurveys.Models
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Data.Common;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public User()
        {
            // This will prevent UserManager.CreateAsync from causing exception
            this.CreatedOn = DateTime.Now;
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
