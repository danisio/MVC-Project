namespace MySurveys.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class ChangeEmailViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "New email")]
        public string NewEmail { get; set; }

        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Confirm new email")]
        [System.ComponentModel.DataAnnotations.Compare("NewEmail", ErrorMessage = "The new email and confirmation email do not match.")]
        public string ConfirmEmail { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Editable(false)]
        public string PasswordHash { get; set; }
    }
}