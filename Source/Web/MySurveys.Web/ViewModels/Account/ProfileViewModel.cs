namespace MySurveys.Web.ViewModels.Account
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ProfileViewModel
    {
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Registration date")]
        public DateTime CreateOn { get; set; }
    }
}