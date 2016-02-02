namespace MySurveys.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data.Common;

    public class Category : DeletableEntity
    {
        private ICollection<Survey> surveys;

        public Category()
        {
            this.surveys = new HashSet<Survey>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100), MinLength(3)]
        public string Name { get; set; }

        public virtual ICollection<Survey> Surveys
        {
            get { return this.surveys; }
            set { this.surveys = value; }
        }
    }
}
