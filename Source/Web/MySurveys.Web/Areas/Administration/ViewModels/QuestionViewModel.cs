namespace MySurveys.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Base;
    using Models;

    public class QuestionViewModel : AdministrationViewModel, IMapFrom<Question>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(50), MinLength(3)]
        [UIHint("CustomString")]
        public string Content { get; set; }

        [Display(Name = "Survey Title")]
        [HiddenInput(DisplayValue = false)]
        public string SurveyTitle { get; set; }


        [Display(Name = "Parent")]
        [HiddenInput(DisplayValue = false)]
		public int ParentId { get; set; }
        public string ParentContent { get; set; }

        [Display(Name = "Total Answers")]
        [HiddenInput(DisplayValue = false)]
        public int TotalPossibleAnswers { get; set; }

        [Display(Name = "Is Dynamic?")]
        [HiddenInput(DisplayValue = false)]
        public bool IsDependsOn { get; set; }
    }
}