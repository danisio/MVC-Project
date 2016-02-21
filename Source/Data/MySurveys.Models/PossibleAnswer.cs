namespace MySurveys.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data.Common;

    public class PossibleAnswer : DeletableEntity
    {
        private ICollection<Answer> answers;

        public PossibleAnswer()
        {
            this.answers = new HashSet<Answer>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200), MinLength(2)]
        public string Content { get; set; }

        public virtual Question Question { get; set; }

        public virtual ICollection<Answer> Answers
        {
            get { return this.answers; }
            set { this.answers = value; }
        }
    }
}
