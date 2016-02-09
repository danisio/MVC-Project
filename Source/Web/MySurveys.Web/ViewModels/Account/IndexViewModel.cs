namespace MySurveys.Web.ViewModels.Account
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class IndexViewModel
    {
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Registration date")]
        public DateTime CreateOn { get; set; }
        
        [Display(Name = "Total surveys")]
        public int TotalSurveys { get; set; }
    }
}